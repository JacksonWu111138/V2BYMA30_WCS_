using Mirle.DataBase;
using Mirle.DB.Fun.Events;
using Mirle.DB.Fun.Parameter;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.EccsSignal;
using Mirle.Gird;
using Mirle.MapController;
using Mirle.Middle;
using Mirle.Structure;
using Mirle.WebAPI.V2BYMA30.ReportInfo;
using Mirle.WriLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Mirle.DB.Proc
{
    public class clsProc
    {
        private Fun.clsCmd_Mst Cmd_Mst = new Fun.clsCmd_Mst();
        private Fun.clsLocMst LocMst = new Fun.clsLocMst();
        private Fun.clsTool tool = new Fun.clsTool();
        private Fun.clsRoutdef Routdef = new Fun.clsRoutdef();
        private Fun.clsMiddleCmd MiddleCmd = new Fun.clsMiddleCmd();
        private Fun.clsEquCmd EquCmd = new Fun.clsEquCmd();
        private Fun.clsProc proc = new Fun.clsProc();
        private clsDbConfig _config = new clsDbConfig();
        private clsDbConfig _config_WMS = new clsDbConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        private WebApiConfig _wmsApi = new WebApiConfig();
        private WebApiConfig _towerApi = new WebApiConfig();
        private DB.WMS.Proc.clsHost wms;
        private int CurrentStockInLoc = 0;
        public clsProc(clsDbConfig config, WebApiConfig WmsApi_Config, WebApiConfig TowerApi_Config, clsDbConfig config_WMS)
        {
            _config = config;
            _wmsApi = WmsApi_Config;
            _towerApi = TowerApi_Config;
            _config_WMS = config_WMS;
            wms = new WMS.Proc.clsHost(config_WMS);
        }

        public Fun.clsRoutdef GetFun_Routdef() => Routdef;

        public bool FunCheckCmdFinish_Proc(MapHost Router, bool PCBACycleRun)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetCmdMst_NotFinish(ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial ||
                                    string.IsNullOrWhiteSpace(cmd.CurLoc))
                                    continue;

                                string sDate1 = cmd.updateFailTime;
                                
                                if(!string.IsNullOrEmpty(sDate1))
                                {
                                    string sClsTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    TimeSpan ts1 = new TimeSpan(DateTime.ParseExact(sDate1,
                                                           "yyyy-MM-dd HH:mm:ss",
                                                           System.Globalization.CultureInfo.InvariantCulture
                                                           ).Ticks);
                                    TimeSpan ts2 = new TimeSpan(DateTime.ParseExact(sClsTime,
                                                           "yyyy-MM-dd HH:mm:ss",
                                                           System.Globalization.CultureInfo.InvariantCulture
                                                           ).Ticks);
                                    TimeSpan ts = ts1.Subtract(ts2).Duration();
                                    if (ts.TotalSeconds < 60) continue;
                                }
                                


                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;

                                
                                //B800撿料口順途
                                if (cmd.Stn_No.Contains(',') && Start == End && Start != null && End != null)
                                {
                                    if (cmd.Stn_No.Contains("," + Start))
                                        cmd.Stn_No = cmd.Stn_No.Replace("," + Start.LocationId, "");
                                    else
                                        cmd.Stn_No = cmd.Stn_No.Replace(Start.LocationId + ",", "");
                                    if(!Cmd_Mst.FunUpdateStnNo(cmd.Cmd_Sno, cmd.Stn_No, sRemark, db))
                                    {
                                        sRemark = $"雙撿料口更新已到達撿料口({Start})失敗！";
                                        if(!Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db))
                                        {
                                            sRemark = $"更新sRemark失敗";
                                            if (Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db))
                                            { };
                                        }
                                    }
                                        

                                }
                                else if (Start == End && Start != null && End != null)
                                {
                                    CarrierPutawayCompleteInfo putawayCompleteInfo = new CarrierPutawayCompleteInfo();
                                    CarrierRetrieveCompleteInfo retrieveCompleteInfo = new CarrierRetrieveCompleteInfo();
                                    CarrierShelfCompleteInfo shelfCompleteInfo = new CarrierShelfCompleteInfo();
                                    CarrierTransferCompleteInfo transferCompleteInfo = new CarrierTransferCompleteInfo();

                                    if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                    {
                                        sRemark = "Error: Begin失敗！";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }

                                    sRemark = "命令完成";
                                    if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finish_Wait, sRemark, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        continue;
                                    }

                                    if(Convert.ToInt32(cmd.Cmd_Sno.ToString()) < 20000)
                                    {
                                        sRemark = "Error: 上報WES命令完成失敗";
                                        ConveyorInfo con = new ConveyorInfo();
                                        switch (cmd.Cmd_Mode)
                                        {
                                            case clsConstValue.CmdMode.L2L:
                                                shelfCompleteInfo = new CarrierShelfCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    emptyTransfer = clsConstValue.YesNo.No,
                                                    jobId = cmd.JobID,
                                                    shelfId = cmd.New_Loc
                                                };

                                                if (!api.GetCarrierShelfComplete().FunReport(shelfCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    if(!Cmd_Mst.FunUpdateupdateFailTime(cmd.Cmd_Sno, sRemark, db))
                                                    {
                                                        sRemark = "Error: 更新上報WES失敗時間流程失敗";
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }

                                                break;
                                            case clsConstValue.CmdMode.S2S:
                                                con = ConveyorDef.GetBuffer(cmd.New_Loc);
                                                transferCompleteInfo = new CarrierTransferCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    jobId = cmd.JobID,
                                                    location = con.StnNo
                                                };

                                                if(cmd.New_Loc == ConveyorDef.AGV.S0_05.BufferName || cmd.New_Loc == ConveyorDef.SMTC.S0_02.BufferName)
                                                {
                                                    PositionReportInfo positionReportInfo = new PositionReportInfo
                                                    {
                                                        jobId = cmd.JobID,
                                                        carrierId = cmd.BoxID,
                                                        location = con.StnNo
                                                    };
                                                    api.GetPositionReport().FunReport(positionReportInfo, _wmsApi.IP);
                                                }

                                                if (!api.GetCarrierTransferComplete().FunReport(transferCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    if (!Cmd_Mst.FunUpdateupdateFailTime(cmd.Cmd_Sno, sRemark, db))
                                                    {
                                                        sRemark = "Error: 更新上報WES失敗時間流程失敗";
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                break;
                                            case clsConstValue.CmdMode.StockIn:
                                                putawayCompleteInfo = new CarrierPutawayCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    isComplete = clsConstValue.YesNo.Yes,
                                                    jobId = cmd.JobID,
                                                    shelfId = cmd.Loc
                                                };

                                                if (!api.GetCarrierPutawayComplete().FunReport(putawayCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    if (!Cmd_Mst.FunUpdateupdateFailTime(cmd.Cmd_Sno, sRemark, db))
                                                    {
                                                        sRemark = "Error: 更新上報WES失敗時間流程失敗";
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                break;
                                            default:
                                                if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == cmd.Stn_No).Any())
                                                    con = ConveyorDef.GetTwoNodeOneStnnoByBufferName(cmd.Stn_No).end;
                                                else
                                                    con = ConveyorDef.GetBuffer(cmd.Stn_No);

                                                if (con.BufferName == ConveyorDef.AGV.S0_05.BufferName || con.BufferName == ConveyorDef.SMTC.S0_02.BufferName)
                                                {
                                                    PositionReportInfo positionReportInfo = new PositionReportInfo
                                                    {
                                                        jobId = cmd.JobID,
                                                        carrierId = cmd.BoxID,
                                                        location = con.StnNo
                                                    };
                                                    api.GetPositionReport().FunReport(positionReportInfo, _wmsApi.IP);
                                                }

                                                retrieveCompleteInfo = new CarrierRetrieveCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    emptyTransfer = clsConstValue.YesNo.No,
                                                    isComplete = clsConstValue.YesNo.Yes,
                                                    jobId = cmd.JobID,
                                                    location = con.StnNo,
                                                    portId = con.StnNo
                                                };

                                                if (!api.GetCarrierRetrieveComplete().FunReport(retrieveCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    if (!Cmd_Mst.FunUpdateupdateFailTime(cmd.Cmd_Sno, sRemark, db))
                                                    {
                                                        sRemark = "Error: 更新上報WES失敗時間流程失敗";
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (cmd.JobID.StartsWith("EBR")) //判斷條件待修正
                                                {
                                                    EmptyBinLoadDoneInfo emptyBinLoadDoneInfo = new EmptyBinLoadDoneInfo
                                                    {
                                                        jobId = cmd.Cmd_Sno,
                                                        location = cmd.Stn_No
                                                    };
                                                    ConveyorInfo con_1 = new ConveyorInfo();
                                                    con_1 = ConveyorDef.GetBuffer(cmd.Stn_No);
                                                    if (!api.GetEmptyBinLoadDone().FunReport(emptyBinLoadDoneInfo, con_1.API.IP))
                                                    {
                                                        sRemark = $"Error: 傳送EmptyBinLoadDone給 {con_1.BufferName} 失敗, jobId = {cmd.Cmd_Sno}.";
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    
                                    db.TransactionCtrl(TransactionTypes.Commit);
                                    return true;
                                }
                                else continue;
                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool FunNormalCmd_Proc(string sAsrsStockInLocation_Sql, string sAsrsEquNo_Sql, MapHost Router, MidHost middle)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetNormalCommand(sAsrsStockInLocation_Sql, sAsrsEquNo_Sql, ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;
                                if (Start != End)
                                {
                                    if (Cmd_Mst.FunCheckWriteToMiddle(cmd.Cmd_Sno, db)) continue;

                                    Location sLoc_Start = null; Location sLoc_End = null;
                                    bool bCheck = Router.GetPath(Start, End, ref sLoc_Start, ref sLoc_End);
                                    if (bCheck == false)
                                    {
                                        sRemark = "Error: Route給出的路徑為Null，WCS給的Location => Start: <Device>" + Start.DeviceId + " <Location>" + Start.LocationId +
                                           "，End: <Device>" + End.DeviceId + " <Location>" + End.LocationId;
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial)
                                        {
                                            var con = ConveyorDef.GetBuffer(sLoc_Start.LocationId);
                                            string sCmdSno_CV = "";
                                            if (!middle.CheckIsInReady(con, ref sCmdSno_CV))
                                            {
                                                sRemark = $"Error: {con.BufferName}並非送出Ready";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }
                                            else if (sCmdSno_CV != "00000" && sCmdSno_CV != cmd.Cmd_Sno)
                                            {
                                                sRemark = $"Error: {con.BufferName}已被其他任務預約 => {sCmdSno_CV}";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }
                                            else
                                            {
                                                PositionReportInfo info = new PositionReportInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    inStock = clsConstValue.YesNo.No,
                                                    jobId = cmd.JobID,
                                                    location = sLoc_Start.LocationId
                                                };

                                                CVReceiveNewBinCmdInfo info_cv = new CVReceiveNewBinCmdInfo
                                                {
                                                    bufferId = con.BufferName,
                                                    carrierType = cmd.carrierType,
                                                    jobId = cmd.Cmd_Sno
                                                };

                                                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                {
                                                    sRemark = "Error: Begin失敗！";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                sRemark = $"預約{con.BufferName}";
                                                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    continue;
                                                }

                                                string deviceId = tool.GetDeviceId(con.BufferName);

                                                if (!Cmd_Mst.FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName, db))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: 更新CurLoc = {con.BufferName}失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (!api.GetCV_ReceiveNewBinCmd().FunReport(info_cv, con.API.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: 預約{con.BufferName}失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }


                                                //放寬PositionReport之條件，不理會WES是否成功
                                                if (Convert.ToInt32(cmd.Cmd_Sno) < 20000)
                                                    api.GetPositionReport().FunReport(info, _wmsApi.IP);

                                                db.TransactionCtrl(TransactionTypes.Commit);
                                                return true;
                                            }
                                        }

                                        if (sLoc_Start.DeviceId == sLoc_End.DeviceId)
                                        {//是電子料塔或AGV的命令//箱式倉內orASRSL2L命令

                                            string sDeviceID = "";
                                            if (sLoc_Start.DeviceId == ConveyorDef.DeviceID_Tower)
                                            {
                                                //不由Middle電子料塔控制命令
                                                sDeviceID = ConveyorDef.DeviceID_Tower;
                                                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                                {
                                                    continue;
                                                }
                                                continue;
                                            }
                                            else if (!tool.IsAGV(sLoc_Start.DeviceId, ref sDeviceID))
                                            {
                                                //箱式倉命令
                                                //ARSR L2L命令或其他內部命令
                                                continue;
                                            }

                                            ConveyorInfo conveyor = new ConveyorInfo();
                                            if(sLoc_Start.LocationTypes == LocationTypes.Conveyor)
                                            {
                                                conveyor = ConveyorDef.GetBuffer(sLoc_Start.LocationId);
                                            }

                                            ConveyorInfo conveyor_To = new ConveyorInfo();
                                            if (sLoc_End.LocationTypes == LocationTypes.Conveyor)
                                            {
                                                conveyor_To = ConveyorDef.GetBuffer(sLoc_End.LocationId);
                                            }

                                            if (!Routdef.CheckSourceIsOK_NonASRS(cmd, sLoc_Start, middle, conveyor, db)) continue;
                                            if (!Routdef.CheckDestinationIsOK_NonASRS(cmd, sLoc_End, middle, conveyor_To, db)) continue;
                                            MiddleCmd middleCmd = new MiddleCmd();
                                            if (MiddleCmd.CheckHasMiddleCmdByCmdSno(cmd.Cmd_Sno, db) == DBResult.Success) continue;
                                            if (!MiddleCmd.FunGetMiddleCmd_NonASRS(cmd, sLoc_Start, sLoc_End, ref middleCmd, sDeviceID, db)) continue;

                                            if( middleCmd.Destination == ConveyorDef.AGV.E2_35.BufferName ||
                                                middleCmd.Destination == ConveyorDef.AGV.E2_36.BufferName ||
                                                middleCmd.Destination == ConveyorDef.AGV.E2_37.BufferName )
                                            {
                                                NewCarrierToStageInfo info = new NewCarrierToStageInfo
                                                {
                                                    jobId = middleCmd.CommandID,
                                                    carrierId = middleCmd.CSTID,
                                                    stagePosition = middleCmd.Destination
                                                };
                                                ConveyorInfo con = ConveyorDef.GetBuffer(middleCmd.Destination);
                                                if(!api.GetNewCarrierToStage().FunReport(info, con.API.IP))
                                                {
                                                    sRemark = $"Error: NewCarrierToStage fail, jobId = {cmd.Cmd_Sno}, Destination = {middleCmd.Destination}.";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                            }

                                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                            {
                                                sRemark = "Error: Begin失敗！";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            sRemark = $"下達Middle層命令 => <{Fun.Parameter.clsMiddleCmd.Column.DeviceID}>{sDeviceID}";
                                            if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                continue;
                                            }

                                            if (!Cmd_Mst.FunUpdateWriteToMiddle(cmd.Cmd_Sno, clsConstValue.YesNo.Yes, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                continue;
                                            }

                                            if (!MiddleCmd.FunInsMiddleCmd(middleCmd, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = "Error: 下達Middle層命令失敗";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            db.TransactionCtrl(TransactionTypes.Commit);
                                            return true;
                                        }
                                        else
                                        {
                                            if(cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial)
                                            {
                                                var con = ConveyorDef.GetBuffer(sLoc_Start.LocationId);
                                                string sCmdSno_CV = "";
                                                if (!middle.CheckIsInReady(con, ref sCmdSno_CV))
                                                {
                                                    sRemark = $"Error: {con.BufferName}並非送出Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                                else if (sCmdSno_CV != "00000" && sCmdSno_CV != cmd.Cmd_Sno)
                                                {
                                                    sRemark = $"Error: {con.BufferName}已被其他任務預約 => {sCmdSno_CV}";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                                else
                                                {
                                                    PositionReportInfo info = new PositionReportInfo
                                                    {
                                                        carrierId = cmd.BoxID,
                                                        inStock = clsConstValue.YesNo.No,
                                                        jobId = cmd.JobID,
                                                        location = sLoc_Start.LocationId
                                                    };

                                                    CVReceiveNewBinCmdInfo info_cv = new CVReceiveNewBinCmdInfo
                                                    {
                                                        bufferId = con.BufferName,
                                                        carrierType = cmd.carrierType,
                                                        jobId = cmd.Cmd_Sno
                                                    };

                                                    if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                    {
                                                        sRemark = "Error: Begin失敗！";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }

                                                    sRemark = $"預約{con.BufferName}";
                                                    if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        continue;
                                                    }

                                                    string deviceId = tool.GetDeviceId(con.BufferName);

                                                    if (!Cmd_Mst.FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName, db))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: 更新CurLoc = {con.BufferName}失敗";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }

                                                    if (!api.GetCV_ReceiveNewBinCmd().FunReport(info_cv, con.API.IP))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: 預約{con.BufferName}失敗";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }


                                                    //放寬PositionReport之條件，不理會WES是否成功
                                                    if(Convert.ToInt32(cmd.Cmd_Sno) < 20000)
                                                        api.GetPositionReport().FunReport(info, _wmsApi.IP);
                                                    /*
                                                    if (!api.GetPositionReport().FunReport(info, _wmsApi.IP))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = "Error: 上報Position Report失敗";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }*/

                                                    db.TransactionCtrl(TransactionTypes.Commit);
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool FunAsrsCmd_Proc(DeviceInfo Device, string StockInLoc_Sql, MapHost Router, 
            WMS.Proc.clsHost wms, MidHost middle, SignalHost CrnSignal)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetCommand(Device.DeviceID, StockInLoc_Sql, ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                
                                if (!Cmd_Mst.CheckCraneStatus(cmd, Device, CrnSignal, db)) continue;

                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;
                                if (Start != End)
                                {
                                    if (Cmd_Mst.FunCheckWriteToMiddle(cmd.Cmd_Sno, db)) continue;

                                    Location sLoc_Start = null; Location sLoc_End = null;
                                    bool bCheck = Router.GetPath(Start, End, ref sLoc_Start, ref sLoc_End);
                                    if (bCheck == false)
                                    {
                                        sRemark = "Error: Route給出的路徑為Null，WCS給的Location => Start: <Device>" + Start.DeviceId + " <Location>" + Start.LocationId +
                                           "，End: <Device>" + End.DeviceId + " <Location>" + End.LocationId;
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        #region 判斷狀態
                                        if (!Routdef.CheckSourceIsOK(cmd, sLoc_Start, middle, Device, wms, db)) continue;
                                        if (!Routdef.CheckDestinationIsOK(cmd, sLoc_End, middle, Device, wms, db)) continue;
                                        iRet = MiddleCmd.CheckHasMiddleCmd(Device.DeviceID, db);
                                        if (iRet == DBResult.Success)
                                        {
                                            sRemark = $"Error: 等候Stocker{Device.DeviceID}的Fork命令淨空";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }
                                        else if (iRet == DBResult.Exception)
                                        {
                                            sRemark = $"Error: 取得Stocker{Device.DeviceID}的Fork命令失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }
                                        else { }
                                        #endregion 判斷狀態
                                        MiddleCmd middleCmd = new MiddleCmd();
                                        if (!MiddleCmd.FunGetMiddleCmd(cmd, sLoc_Start, sLoc_End, ref middleCmd, db)) continue;

                                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                        {
                                            sRemark = "Error: Begin失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        sRemark = $"下達Middle層命令 => <{Fun.Parameter.clsMiddleCmd.Column.DeviceID}>{sLoc_Start.DeviceId}";
                                        if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            continue;
                                        }

                                        if (!Cmd_Mst.FunUpdateWriteToMiddle(cmd.Cmd_Sno, clsConstValue.YesNo.Yes, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            continue;
                                        }

                                        if (!MiddleCmd.FunInsMiddleCmd(middleCmd, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = "Error: 下達Middle層命令失敗";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        db.TransactionCtrl(TransactionTypes.Commit);
                                        return true;
                                    }
                                }
                                else continue;
                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool FunAsrsCmd_DoubleCV_Proc(DeviceInfo Device, string StockInLoc_Sql, MapHost Router,
            WMS.Proc.clsHost wms, MidHost middle, SignalHost CrnSignal)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetCommand(Device.DeviceID, StockInLoc_Sql, ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, "開始檢查Crane狀態");
                                if (!Cmd_Mst.CheckCraneStatus(cmd, Device, CrnSignal, db)) continue;

                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;
                                if (Start != End)
                                {
                                    if (Cmd_Mst.FunCheckWriteToMiddle(cmd.Cmd_Sno, db)) continue;

                                    Location sLoc_Start = null; Location sLoc_End = null;
                                    bool bCheck = Router.GetPath(Start, End, ref sLoc_Start, ref sLoc_End);
                                    if (bCheck == false)
                                    {
                                        sRemark = "Error: Route給出的路徑為Null，WCS給的Location => Start: <Device>" + Start.DeviceId + " <Location>" + Start.LocationId +
                                           "，End: <Device>" + End.DeviceId + " <Location>" + End.LocationId;
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        bool IsDoubleCmd = false; 
                                        CmdMstInfo cmd_DD = new CmdMstInfo();
                                        string[] sCmdSno_CV = new string[2];
                                        #region 判斷狀態
                                        if (!Routdef.CheckSourceIsOK(cmd, sLoc_Start, middle, Device, wms, ref IsDoubleCmd, ref cmd_DD,
                                            ref sCmdSno_CV, db))
                                            continue;
                                        if(cmd.Cmd_Mode == clsConstValue.CmdMode.StockIn)
                                        {
                                            if (IsDoubleCmd)
                                            {
                                                if (!sCmdSno_CV.Where(r => r == cmd.Cmd_Sno).Any())
                                                {
                                                    sRemark = $"Error: 此序號跟Buffer的序號不一樣 => <Left>{sCmdSno_CV[0]} <Right>{sCmdSno_CV[1]}";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (!sCmdSno_CV.Where(r => r == cmd_DD.Cmd_Sno).Any())
                                                {
                                                    sRemark = $"Error: 此序號跟Buffer的序號不一樣 => <Left>{sCmdSno_CV[0]} <Right>{sCmdSno_CV[1]}";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd_DD.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (!sCmdSno_CV.Where(r => r == cmd.Cmd_Sno).Any())
                                                {
                                                    sRemark = $"Error: 此序號跟Buffer的序號{sCmdSno_CV[0]}不一樣";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                            }
                                        }
                                       

                                        if (!Routdef.CheckDestinationIsOK(cmd, sLoc_End, middle, Device, wms, IsDoubleCmd, db)) continue;

                                        iRet = MiddleCmd.CheckHasMiddleCmd(Device.DeviceID, db);
                                        if (iRet == DBResult.Success)
                                        {
                                            sRemark = $"Error: 等候Stocker{Device.DeviceID}的Fork命令淨空";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }
                                        else if (iRet == DBResult.Exception)
                                        {
                                            sRemark = $"Error: 取得Stocker{Device.DeviceID}的Fork命令失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }
                                        else { }
                                        #endregion 判斷狀態
                                        MiddleCmd middleCmd = new MiddleCmd();
                                        MiddleCmd middleCmd_DD = new MiddleCmd();
                                        if (!MiddleCmd.FunGetMiddleCmd(cmd, sLoc_Start, sLoc_End, ref middleCmd, ref middleCmd_DD, IsDoubleCmd, cmd_DD, Router, db))
                                            continue;
                                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                        {
                                            sRemark = "Error: Begin失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        sRemark = $"下達Middle層命令 => <{Fun.Parameter.clsMiddleCmd.Column.DeviceID}>{sLoc_Start.DeviceId}";
                                        if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            continue;
                                        }

                                        if(IsDoubleCmd)
                                        {
                                            if (!Cmd_Mst.FunUpdateCmdSts(cmd_DD.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                continue;
                                            }
                                        }

                                        if (!Cmd_Mst.FunUpdateWriteToMiddle(cmd.Cmd_Sno, clsConstValue.YesNo.Yes, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            continue;
                                        }

                                        //修正箱式倉右外儲位預約左外站口之錯誤
                                        if(!IsDoubleCmd && cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut)
                                        {
                                            //若判斷為單板且送到外ASRS站口，更改為送至內ASRS站口
                                            if (middleCmd.Destination == ConveyorDef.Box.B1_105.BufferName )
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_108.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_108))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            else if(middleCmd.Destination == ConveyorDef.Box.B1_093.BufferName )
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_096.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_096))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            else if (middleCmd.Destination == ConveyorDef.Box.B1_081.BufferName )
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_084.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_084))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            else if(middleCmd.Destination == ConveyorDef.Box.B1_025.BufferName )
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_028.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_028))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            else if (middleCmd.Destination == ConveyorDef.Box.B1_013.BufferName )
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_016.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_016))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            else if (middleCmd.Destination == ConveyorDef.Box.B1_001.BufferName)
                                            {
                                                middleCmd.Destination = ConveyorDef.Box.B1_004.BufferName;
                                                if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_004))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                            
                                        }

                                        if (!MiddleCmd.FunInsMiddleCmd(middleCmd, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = "Error: 下達Middle層命令失敗";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        if (IsDoubleCmd)
                                        {
                                            if (!Cmd_Mst.FunUpdateWriteToMiddle(cmd_DD.Cmd_Sno, clsConstValue.YesNo.Yes, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                continue;
                                            }

                                            if(cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut)
                                            {
                                                if (middleCmd.Destination == ConveyorDef.Box.B1_105.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_108.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_108))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                                else if (middleCmd.Destination == ConveyorDef.Box.B1_093.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_096.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_096))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                                else if (middleCmd.Destination == ConveyorDef.Box.B1_081.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_084.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_084))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                                else if (middleCmd.Destination == ConveyorDef.Box.B1_025.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_028.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_028))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                                else if (middleCmd.Destination == ConveyorDef.Box.B1_013.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_016.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_016))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                                else if (middleCmd.Destination == ConveyorDef.Box.B1_001.BufferName)
                                                {
                                                    middleCmd_DD.Destination = ConveyorDef.Box.B1_004.BufferName;
                                                    if (!middle.CheckIsOutReady(ConveyorDef.Box.B1_004))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: {middleCmd.Destination}沒發接收Ready";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }
                                                        continue;
                                                    }
                                                }
                                            }
                                            
                                            if (!MiddleCmd.FunInsMiddleCmd(middleCmd_DD, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = "Error: 下達Middle層命令失敗";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd_DD.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }
                                        }

                                        db.TransactionCtrl(TransactionTypes.Commit);
                                        return true;
                                    }
                                }
                                else continue;
                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }
        public bool FunAsrsCmd_DoubleCV_StockIn_Proc(WMS.Proc.clsHost wms, SignalHost CrnSignal, DeviceInfo device)
        {
            if((CurrentStockInLoc + 3).ToString() != device.DeviceID) return true;
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetB800StockInOrL2LCommand(ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                string sRemark = "";
                                if (cmd.Cmd_Mode == clsConstValue.CmdMode.StockIn)
                                {
                                    if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial || string.IsNullOrEmpty(cmd.Equ_No))
                                    {
                                        DataTable emptyLocDTTmp = new DataTable();
                                        int emptyIRet = wms.GetLocMst().funCheckCountForEmptyLoc(ref emptyLocDTTmp);
                                        if (emptyIRet == DBResult.Success)
                                        {
                                            int[] emptyNo = new int[3];
                                            for (int j = 0; j < 3; j++)
                                            {
                                                emptyNo[j] = Convert.ToInt32(emptyLocDTTmp.Rows[0][j].ToString());
                                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"相識倉儲位取得空儲位數: Line{(j+3).ToString()} = {emptyNo[j]}");
                                            }
                                            if (string.IsNullOrWhiteSpace(cmd.Equ_No))
                                            {
                                                bool getEquNo = false;
                                                for(int count = 0; count < emptyNo.Length; count++)
                                                {
                                                    if (emptyNo[(CurrentStockInLoc + count) % 3] > 0)
                                                    {
                                                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"成功取得空儲位於: Line{((CurrentStockInLoc + count) % 3).ToString()}");
                                                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                        {
                                                            sRemark = "Error: Begin失敗！";
                                                            if (sRemark != cmd.Remark)
                                                            {
                                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                            }
                                                            break;
                                                        }

                                                        if (Cmd_Mst.FunUpdateEquNo(cmd.Cmd_Sno, (((CurrentStockInLoc + count) % 3) + 3).ToString(), db))
                                                        {
                                                            CurrentStockInLoc++;
                                                            if (CurrentStockInLoc > 2)
                                                                CurrentStockInLoc = 0;
                                                        }
                                                        else
                                                        {
                                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                                            break;
                                                        }
                                                        sRemark = $"箱式倉設定儲位Line完成, jobId = {cmd.Cmd_Sno}.";
                                                        if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                                        {
                                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                                            break;
                                                        }

                                                        CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                                                        {
                                                            jobId = cmd.Cmd_Sno,
                                                            bufferId = cmd.Stn_No,
                                                            carrierType = clsConstValue.ControllerApi.CarrierType.Bin
                                                        };
                                                        ConveyorInfo con = new ConveyorInfo();
                                                        con = ConveyorDef.GetBuffer(cmd.Stn_No);
                                                        if(!api.GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                                                        {
                                                            sRemark = $"Error: CVReceiveNewBin fail, jobId = {cmd.Cmd_Sno}.";
                                                            if (sRemark != cmd.Remark)
                                                            {
                                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                            }
                                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                                            break;
                                                        }

                                                        db.TransactionCtrl(TransactionTypes.Commit);
                                                        getEquNo = true;
                                                        break;
                                                    }
                                                }
                                                if(!getEquNo)
                                                {
                                                    sRemark = $"箱式倉暫無空閒儲位, jobId = {cmd.Cmd_Sno}.";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else if ((cmd.CurLoc == ConveyorDef.Box.B1_037.BufferName && cmd.Equ_No == "3") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_041.BufferName && cmd.Equ_No == "4") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_045.BufferName && cmd.Equ_No == "5") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_117.BufferName && cmd.Equ_No == "3") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_121.BufferName && cmd.Equ_No == "4") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_125.BufferName && cmd.Equ_No == "5"))
                                    {
                                        if (cmd.Loc == "SHELF")
                                        {
                                            ConveyorInfo CusBuff1 = new ConveyorInfo();
                                            ConveyorInfo CusBuff2 = new ConveyorInfo();
                                            if (cmd.CurLoc == ConveyorDef.Box.B1_037.BufferName)
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_033;
                                                CusBuff2 = ConveyorDef.Box.B1_036;
                                            }
                                            else if (cmd.CurLoc == ConveyorDef.Box.B1_041.BufferName)
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_021;
                                                CusBuff2 = ConveyorDef.Box.B1_024;
                                            }
                                            else if (cmd.CurLoc == ConveyorDef.Box.B1_045.BufferName)
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_009;
                                                CusBuff2 = ConveyorDef.Box.B1_012;
                                            }
                                            else if (cmd.CurLoc == ConveyorDef.Box.B1_117.BufferName)
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_113;
                                                CusBuff2 = ConveyorDef.Box.B1_116;
                                            }
                                            else if (cmd.CurLoc == ConveyorDef.Box.B1_121.BufferName)
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_101;
                                                CusBuff2 = ConveyorDef.Box.B1_104;
                                            }
                                            else
                                            {
                                                CusBuff1 = ConveyorDef.Box.B1_089;
                                                CusBuff2 = ConveyorDef.Box.B1_092;
                                            }

                                            string[] CmdCheck = new string[2];
                                            //檢查前方Buffer的CmdSno
                                            for (int checkBuff = 0; checkBuff < 2; checkBuff++)
                                            {
                                                CmdCheck[checkBuff] = "";
                                                var BuffCheck = checkBuff == 0 ? CusBuff1 : CusBuff2;
                                                BufferStatusQueryInfo checkInfo = new BufferStatusQueryInfo
                                                {
                                                    bufferId = BuffCheck.BufferName
                                                };
                                                BufferStatusReply checkReply = new BufferStatusReply();
                                                if (api.GetBufferStatusQuery().FunReport(checkInfo, BuffCheck.API.IP, ref checkReply))
                                                {
                                                    CmdCheck[checkBuff] = checkReply.jobId;
                                                }
                                            }
                                            
                                            //前方空板
                                            if (CmdCheck[0] == "00000" && CmdCheck[1] == "00000")
                                            {
                                                string sStockInLoc = wms.GetLocMst().funSearchEmptyLoc(cmd.Equ_No);
                                                if (string.IsNullOrWhiteSpace(sStockInLoc))
                                                {
                                                    sRemark = $"Error: <EquNo> {cmd.Equ_No} 找不到新儲位。";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                {
                                                    sRemark = "Error: Begin失敗！";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (!Cmd_Mst.FunUpdateLoc(cmd.Cmd_Sno, sStockInLoc, db))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: <CmdSno> {cmd.Cmd_Sno} 無法更新入庫新儲位。";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                var ShelfBuff = ConveyorDef.GetBuffer(cmd.CurLoc);
                                                CarrierShelfReportInfo info = new CarrierShelfReportInfo
                                                {
                                                    jobId = cmd.JobID,
                                                    carrierId = cmd.BoxID,
                                                    shelfId = sStockInLoc,
                                                    shelfStatus = "IN",
                                                    disableLocation = "N"
                                                };

                                                if (!api.GetCarrierShelfReport().FunReport(info, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: 上報預約儲位<{sStockInLoc}>命令序號失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                            }
                                            //前方滿板
                                            else if ( CmdCheck[0] != "00000" && CmdCheck[1] != "00000")
                                            {
                                                sRemark = $"Error: <EquNo> {cmd.Equ_No} 前方等待搬運中。";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }
                                            //選擇前方靜電箱的兄弟
                                            else
                                            {
                                                string CmdSnoDD = CmdCheck[0] != "00000" ? CmdCheck[0] : CmdCheck[1];
                                                CmdMstInfo cmd_DD = new CmdMstInfo();
                                                if (!Cmd_Mst.FunGetCommandByCmdSno(CmdSnoDD, ref cmd_DD, db))
                                                {
                                                    sRemark = $"Error: <EquNo> {cmd.Equ_No} <CmdSno> {CmdSnoDD} 前方命令序號不存在。";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                bool IsEmpty = false;
                                                string targetLoc = wms.GetLocMst().GetLocDDandStatus(cmd_DD.Loc, ref IsEmpty);
                                                if (!string.IsNullOrWhiteSpace(targetLoc) && IsEmpty)
                                                {
                                                    if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                    {
                                                        sRemark = "Error: Begin失敗！";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }

                                                    if (!Cmd_Mst.FunUpdateLoc(cmd.Cmd_Sno, targetLoc, db))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: <CmdSno> {cmd.Cmd_Sno} 無法更新入庫新儲位(with 兄弟)。";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }

                                                    var ShelfBuff = ConveyorDef.GetBuffer(cmd.CurLoc);
                                                    CarrierShelfReportInfo info = new CarrierShelfReportInfo
                                                    {
                                                        carrierId = cmd.BoxID,
                                                        shelfId = targetLoc,
                                                        shelfStatus = "IN",
                                                        disableLocation = "N"
                                                    };

                                                    if (!api.GetCarrierShelfReport().FunReport(info, _wmsApi.IP))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        sRemark = $"Error: 下達新CmdSno<{cmd.Cmd_Sno}>命令序號失敗";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    sRemark = $"Error: <Loc> {cmd_DD.Loc} 此儲位的兄弟無法接收新入庫搬運。";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }
                                                }
                                            }
                                            
                                            db.TransactionCtrl(TransactionTypes.Commit);

                                        }
                                        else
                                        {
                                            int sRow = Convert.ToInt32(cmd.Loc.Substring(0,2));
                                            bool isEmptyDD = false;
                                            string sLocDD = wms.GetLocMst().GetLocDDandStatus(cmd.Loc, ref isEmptyDD);
                                            string newCurLoc = "";
                                            if (!isEmptyDD)
                                                newCurLoc = "CVIN";
                                            else
                                                newCurLoc = "CVWT";
                                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                            {
                                                sRemark = "Error: Begin失敗！";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            if (!Cmd_Mst.FunUpdateCurLoc(cmd.Cmd_Sno, "", newCurLoc, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = $"Error: <CmdSno> {cmd.Cmd_Sno} 更新位置CV失敗！";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                                            {
                                                jobId = cmd.Cmd_Sno,
                                                carrierType = clsConstValue.ControllerApi.CarrierType.Bin,
                                                bufferId = cmd.CurLoc
                                            };

                                            var con = ConveyorDef.GetBuffer(cmd.CurLoc);

                                            if (!api.GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = $"Error: 下達新CmdSno<{cmd.Cmd_Sno}>命令序號失敗";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }
                                            db.TransactionCtrl(TransactionTypes.Commit);
                                        }
                                    }

                                    else if ((cmd.CurLoc == ConveyorDef.Box.B1_037.BufferName && cmd.Equ_No != "3") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_041.BufferName && cmd.Equ_No != "4") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_045.BufferName && cmd.Equ_No != "5") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_117.BufferName && cmd.Equ_No != "3") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_121.BufferName && cmd.Equ_No != "4") ||
                                        (cmd.CurLoc == ConveyorDef.Box.B1_125.BufferName && cmd.Equ_No != "5"))
                                    {
                                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                        {
                                            sRemark = "Error: Begin失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        if (!Cmd_Mst.FunUpdateCurLoc(cmd.Cmd_Sno, "", "CV", db))
                                        {
                                            sRemark = $"Error: <CmdSno> {cmd.Cmd_Sno} 更新位置CV失敗！";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                                        {
                                            jobId = cmd.Cmd_Sno,
                                            carrierType = clsConstValue.ControllerApi.CarrierType.Bin,
                                            bufferId = cmd.CurLoc
                                        };

                                        var con = ConveyorDef.GetBuffer(cmd.CurLoc);

                                        if (!api.GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: 下達新CmdSno<{cmd.Cmd_Sno}>命令序號失敗";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }
                                        db.TransactionCtrl(TransactionTypes.Commit);
                                    }
                                }

                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }
        /// <summary>
        /// 處理空出/二重格的情況
        /// </summary>
        /// <returns></returns>
        public bool FunAsrsCmd_AbnormalFinish_Proc(WMS.Proc.clsHost wms)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        switch(MiddleCmd.GetAbnormalFinishCommand(ref dtTmp, db))
                        {
                            case DBResult.Success:
                                for (int i = 0; i < dtTmp.Rows.Count; i++)
                                {
                                    string sRemark = ""; string strEM = "";
                                    MiddleCmd middleCmd = tool.GetMiddleCmd(dtTmp.Rows[i]);
                                    CmdMstInfo cmd = new CmdMstInfo();
                                    if (Cmd_Mst.FunGetCommand(middleCmd.CommandID, ref cmd, ref iRet, db))
                                    {
                                        if (middleCmd.CompleteCode == clsConstValue.CompleteCode.EmptyRetrieval)
                                        {
                                            #region 空出庫流程
                                            CarrierShelfCompleteInfo shelfCompleteInfo = new CarrierShelfCompleteInfo();
                                            CarrierRetrieveCompleteInfo retrieveCompleteInfo = new CarrierRetrieveCompleteInfo();
                                            BufferInitialInfo bufferInitialInfo = new BufferInitialInfo();
                                            if (cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut)
                                            {
                                                retrieveCompleteInfo = new CarrierRetrieveCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    emptyTransfer = clsEnum.WmsApi.EmptyRetrieval.Y.ToString(),
                                                    isComplete = clsConstValue.YesNo.Yes,
                                                    jobId = cmd.JobID,
                                                    location = "",
                                                    portId = ""
                                                };

                                                bufferInitialInfo = new BufferInitialInfo
                                                {
                                                    bufferId = middleCmd.Destination
                                                };
                                            }
                                            else
                                            {
                                                shelfCompleteInfo = new CarrierShelfCompleteInfo
                                                {
                                                    carrierId = cmd.BoxID,
                                                    emptyTransfer = clsEnum.WmsApi.EmptyRetrieval.Y.ToString(),
                                                    jobId = cmd.JobID,
                                                    shelfId = cmd.Loc
                                                };
                                            }

                                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                            {
                                                sRemark = "Error: Begin失敗！";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            sRemark = "Error: 儲位空出庫";
                                            if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finish_Wait, clsEnum.Cmd_Abnormal.E2, sRemark, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                continue;
                                            }

                                            if (!MiddleCmd.FunInsertHisMiddleCmd(cmd.Cmd_Sno, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = "Error: insert MiddleCmd_His失敗";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            if (!MiddleCmd.FunDelMiddleCmd(cmd.Cmd_Sno, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = "Error: 刪除MiddleCmd失敗";
                                                if (sRemark != cmd.Remark)
                                                {
                                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                }

                                                continue;
                                            }

                                            if (cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut)
                                            {
                                                ConveyorInfo con = ConveyorDef.GetBuffer(bufferInitialInfo.bufferId);

                                                if(!api.GetBufferInitial().FunReport(bufferInitialInfo, con.API.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = "Error: 初始CV站口失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }

                                                if (!api.GetCarrierRetrieveComplete().FunReport(retrieveCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = "Error: 上報RetrieveComplete失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                if (!api.GetCarrierShelfComplete().FunReport(shelfCompleteInfo, _wmsApi.IP))
                                                {
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = "Error: 上報ShelfComplete失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                            }

                                            db.TransactionCtrl(TransactionTypes.Commit);
                                            return true;
                                            #endregion 空出庫流程
                                        }
                                        else
                                        {
                                            #region 二重格流程
                                            string sNewLoc = "";
                                            if (string.IsNullOrWhiteSpace(middleCmd.BatchID))
                                            {
                                                #region 單板
                                                clsEnum.AsrsType type = clsEnum.AsrsType.None;
                                                if (tool.CheckWhId_ASRS(middleCmd.DeviceID, ref type))
                                                {
                                                    if (type == clsEnum.AsrsType.Box)
                                                    {
                                                        sNewLoc = wms.GetLocMst().funSearchEmptyLoc_Abnormal_Proc(middleCmd.DeviceID, middleCmd.Destination);
                                                        if (proc.FunDoubleStorage_SingleProc(cmd, middleCmd, sNewLoc, _wmsApi, db)) return true;
                                                        else continue;
                                                    }
                                                    else
                                                    {
                                                        #region PCBA
                                                        EmptyShelfQueryInfo emptyShelfQueryInfo = new EmptyShelfQueryInfo
                                                        {
                                                            jobId = cmd.JobID,
                                                            lotIdCarrierId = cmd.BoxID
                                                        };
                                                        switch (middleCmd.DeviceID)
                                                        {
                                                            case "1":
                                                            case "2":
                                                                emptyShelfQueryInfo.craneId = "M80" + middleCmd.DeviceID;
                                                                break;
                                                            case "3":
                                                                emptyShelfQueryInfo.craneId = "B801";
                                                                break;
                                                            case "4":
                                                                emptyShelfQueryInfo.craneId = "B802";
                                                                break;
                                                            case "5":
                                                                emptyShelfQueryInfo.craneId = "B801";
                                                                break;
                                                            default:
                                                                emptyShelfQueryInfo.craneId = middleCmd.DeviceID;
                                                                break;
                                                        }

                                                        EmptyShelfQueryReply emptyShelfQueryResponse = new EmptyShelfQueryReply();
                                                        if (!api.GetEmptyShelfQuery().FunReport(emptyShelfQueryInfo, ref emptyShelfQueryResponse, _wmsApi.IP))
                                                        {
                                                            sRemark = "Error: 發生二重格，向WES取得新儲位失敗！";
                                                            if (sRemark != cmd.Remark)
                                                            {
                                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                            }

                                                            continue;
                                                        }

                                                        sNewLoc = emptyShelfQueryResponse.shelfId;
                                                        if (proc.FunDoubleStorage_SingleProc(cmd, middleCmd, sNewLoc, _wmsApi, db)) return true;
                                                        else continue;
                                                        #endregion PCBA
                                                    }
                                                }
                                                else
                                                {
                                                    sRemark = $"Error: 二重格找不到倉別 => <{Fun.Parameter.clsMiddleCmd.Column.DeviceID}>{middleCmd.DeviceID}";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    continue;
                                                }
                                                #endregion 單板
                                            }
                                            else
                                            {
                                                #region 雙板
                                                DataTable dtMiddleCmd = new DataTable();
                                                if (MiddleCmd.GetMiddleCmd_ByBatchID(middleCmd.BatchID, ref dtMiddleCmd, db) != DBResult.Success)
                                                {
                                                    sRemark = "Error: 取得MiddleCmd的Batch命令失敗";
                                                    if (sRemark != cmd.Remark)
                                                    {
                                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                    }

                                                    dtMiddleCmd.Dispose();
                                                    continue;
                                                }
                                                else
                                                {
                                                    if (dtMiddleCmd.Rows.Count != 2)
                                                    {
                                                        sRemark = $"Error: MiddleCmd的Batch命令個數並非兩筆 => {dtMiddleCmd.Rows.Count}";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                                        }

                                                        dtMiddleCmd.Dispose();
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        MiddleCmd[] BatchCmd = new MiddleCmd[2];
                                                        for (int iMiddle = 0; iMiddle < dtMiddleCmd.Rows.Count; iMiddle++)
                                                        {
                                                            BatchCmd[iMiddle] = tool.GetMiddleCmd(dtMiddleCmd.Rows[iMiddle]);
                                                        }

                                                        dtMiddleCmd.Dispose();
                                                        CmdMstInfo[] cmds_Batch = new CmdMstInfo[2];
                                                        bool bCheck = true;
                                                        for (int iCmd = 0; iCmd < 2; iCmd++)
                                                        {
                                                            if (!Cmd_Mst.FunGetCommand(BatchCmd[iCmd].CommandID, ref cmds_Batch[iCmd], ref iRet, db))
                                                            {
                                                                sRemark = "Error: 找不到系統命令";
                                                                if (sRemark != BatchCmd[iCmd].Remark)
                                                                {
                                                                    MiddleCmd.FunMiddleCmdUpdateRemark(BatchCmd[iCmd].CommandID, sRemark, db, ref strEM);
                                                                }
                                                                bCheck = false;
                                                                break;
                                                            }
                                                        }

                                                        if (!bCheck) continue;
                                                        else
                                                        {
                                                            string[] sNewLocs = new string[2];
                                                            if (wms.GetLocMst().CheckHasNNNN(middleCmd.DeviceID, ref sNewLocs) != DBResult.Success)
                                                            {
                                                                sRemark = "Error: 二重格找不到可以直接放兩板的新儲位";
                                                                for (int iBatch = 0; iBatch < cmds_Batch.Length; iBatch++)
                                                                {
                                                                    if (sRemark != cmds_Batch[iBatch].Remark)
                                                                    {
                                                                        Cmd_Mst.FunUpdateRemark(cmds_Batch[iBatch].Cmd_Sno, sRemark, db);
                                                                    }
                                                                }

                                                                continue;
                                                            }

                                                            if (proc.FunDoubleStorage_DoubleProc(cmds_Batch, BatchCmd, sNewLocs, _wmsApi, db)) return true;
                                                            else continue;
                                                        }
                                                    }
                                                } 
                                                #endregion 雙板
                                            } 
                                            #endregion 二重格流程
                                        }
                                    }
                                    else
                                    {
                                        sRemark = "Error: 找不到系統命令";
                                        if(sRemark != middleCmd.Remark)
                                        {
                                            MiddleCmd.FunMiddleCmdUpdateRemark(middleCmd.CommandID, sRemark, db, ref strEM);
                                        }
                                    }
                                }

                                return false;
                            case DBResult.NoDataSelect:
                                return true;
                            default:
                                return false;
                        }
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool subCraneWrR2R(DeviceInfo Device, SignalHost CrnSignal, MapHost Router,
            WMS.Proc.clsHost wms, MidHost middle)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetR2RCommand(Device.DeviceID, ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                int iEquNo_To = tool.funGetEquNoByLoc(cmd.New_Loc);
                                if (int.Parse(cmd.Equ_No) != iEquNo_To) continue;
                                if (!Cmd_Mst.CheckCraneStatus(cmd, Device, CrnSignal, db)) continue;
                                string sRemark = "";

                                #region 確定是否需要內儲位的庫對庫命令

                                if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial)
                                {
                                    Location Start = null; Location End = null;
                                    if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;
                                    if (!Routdef.CheckSourceIsOK(cmd, Start, middle, Device, wms, db))
                                        continue;
                                }

                                #endregion 確定是否需要內儲位的庫對庫命令

                                iRet = MiddleCmd.CheckHasMiddleCmd(Device.DeviceID, db);
                                if (iRet == DBResult.Success)
                                {
                                    sRemark = $"Error: 等候Stocker{Device.DeviceID}的Fork命令淨空";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    continue;
                                }
                                else if (iRet == DBResult.Exception)
                                {
                                    sRemark = $"Error: 取得Stocker{Device.DeviceID}的Fork命令失敗！";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    continue;
                                }
                                else { }

                                MiddleCmd middleCmd = new MiddleCmd();
                                if (!MiddleCmd.FunGetMiddleCmd_R2R(cmd, ref middleCmd, db)) continue;

                                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                {
                                    sRemark = "Error: Begin失敗！";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    continue;
                                }

                                sRemark = $"下達Middle層命令 => <{Fun.Parameter.clsMiddleCmd.Column.DeviceID}>{Device.DeviceID}";
                                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, sRemark, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    continue;
                                }

                                if (!MiddleCmd.FunInsMiddleCmd(middleCmd, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    sRemark = "Error: 下達Middle層命令失敗";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    continue;
                                }

                                db.TransactionCtrl(TransactionTypes.Commit);
                                return true;
                            }

                            return false;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool FunAGVTaskCancel(string sCmdSno, ref string strEM, string IP)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        string sRemark = "";
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            sRemark = "Error: Begin失敗！";
                            Cmd_Mst.FunUpdateRemark(sCmdSno, sRemark, db);
                            return false;
                        }

                        sRemark = "";
                        if(!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Cancel_Wait,sRemark, db))
                        {
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        if (!MiddleCmd.FunMiddleCmdUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Cancel_Wait, sRemark, db))
                        {
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        //call api
                        TaskCancelInfo info = new TaskCancelInfo
                        {
                            jobId = sCmdSno
                        };

                        if (!api.GetTaskCancel().FunReport(info, IP))
                        {
                            MessageBox.Show($"取消命令失敗, jobId:{info.jobId}.", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }
                        else
                        {
                            MessageBox.Show($"取消命令成功, jobId:{info.jobId}.", "Task Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunCarrierTransferCancel(string sCmdSno, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        CmdMstInfo cmd = new CmdMstInfo();
                        if(Cmd_Mst.FunGetCommandByJobID(sCmdSno, ref cmd, ref iRet, db))
                        {
                            if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Running)
                            {
                                strEM = "Error: 命令已開始執行，無法取消！";
                                return false;
                            }

                            int iRet_Middle = MiddleCmd.CheckHasMiddleCmdByCmdSno(cmd.Cmd_Sno, db);
                            if (iRet_Middle == DBResult.Exception)
                            {
                                strEM = "取得Middle命令失敗！";
                                return false;
                            }

                            int iRet_Equ = EquCmd.CheckHasEquCmdByCmdSno(cmd.Cmd_Sno, db);
                            if(iRet_Equ == DBResult.Exception)
                            {
                                strEM = "取得Equ命令失敗！";
                                return false;
                            }
                            if (iRet_Middle == DBResult.Success)
                                MiddleCmd.FunInsertHisMiddleCmd(cmd.Cmd_Sno, db);

                            string sRemark = ""; 
                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                            {
                                sRemark = "Error: Begin失敗！";
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                return false;
                            }

                            sRemark = "WES命令取消";
                            if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancel_Wait, sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            if (iRet_Middle == DBResult.Success)
                            {
                                if (!MiddleCmd.FunDelMiddleCmd(cmd.Cmd_Sno, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }

                            }

                            if (iRet_Equ == DBResult.Success)
                            {
                                if (!EquCmd.FunDelEquCmd(cmd.Cmd_Sno, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }
                            }

                            db.TransactionCtrl(TransactionTypes.Commit);
                            return true;
                        }
                        else
                        {
                            strEM = $"<JobID> {sCmdSno} => 取得命令資料失敗！";
                            return false;
                        }
                    }
                    else
                    {
                        strEM = "Error: 開啟DB失敗！";
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strEM);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunLotTransferCancel(string sCmdSno, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        CmdMstInfo cmd = new CmdMstInfo();
                        if (Cmd_Mst.FunGetCommand(sCmdSno, ref cmd, ref iRet, db))
                        {
                            if (cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Running)
                            {
                                strEM = "Error: 命令已開始執行，無法取消！";
                                return false;
                            }

                            string sRemark = "";
                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                            {
                                sRemark = "Error: Begin失敗！";
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                return false;
                            }

                            sRemark = "WES命令取消";
                            if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancel_Wait, sRemark, db))
                            {
                                strEM = "Error: 更新CmdMst.CmdSts失敗";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            //對E800C下達LotCancel
                            LotTransferCancelInfo lotcancel_info = new LotTransferCancelInfo
                            {
                                jobId = cmd.Cmd_Sno,
                                lotId = cmd.BoxID
                            };
                            if(!api.GetLotTransferCancel().FunReport(lotcancel_info, _towerApi.IP))
                            {
                                strEM = "Error: 下達LotCancel命令給E800C失敗";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            db.TransactionCtrl(TransactionTypes.Commit);
                            return true;
                        }
                        else
                        {
                            strEM = $"<JobID> {sCmdSno} => 取得命令資料失敗！";
                            return false;
                        }
                    }
                    else
                    {
                        strEM = "Error: 開啟DB失敗！";
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strEM);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunLotPutawayTransfer(CmdMstInfo cmd, string TowerIp, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        string sRemark = "";
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            strEM = "Error: 開啟Transaction失敗";
                            return false;
                        }

                        if (!Cmd_Mst.FunInsCmdMst(cmd, ref strEM, db))
                        {
                            strEM = "Error: 建立LotPutaway命令失敗";
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        PutawayTransferInfo info = new PutawayTransferInfo
                        {
                            jobId = cmd.Cmd_Sno,
                            reelId = cmd.BoxID,
                            lotSize = cmd.lotSize,
                            toShelfId = cmd.Loc
                        };

                        if (!api.GetPutawayTransfer().FunReport(info, TowerIp))
                        {
                            strEM = "Error: PutawayTransfer E800C接收失敗";
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, "", db))
                        {
                            strEM = "Error: 更改CmdSts至Running失敗";
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;
                    }
                    else
                    {
                        strEM = "Error: 開啟DB失敗！";
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strEM);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunPCBACycleRunInitial(CmdMstInfo StockCmd, CmdMstInfo M801L2LCmd, CmdMstInfo M802L2LCmd, ref string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            sRemark = "Error: Transaction begin失敗！";
                            throw new Exception(sRemark);
                        }

                        sRemark = "";
                        if(StockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(StockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }
                        
                        if(M801L2LCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(M801L2LCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }
                        
                        if(M802L2LCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(M802L2LCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }
                        

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunBoxCycleRunInitial(CmdMstInfo B801LeftStockCmd, CmdMstInfo B801RightStockCmd, CmdMstInfo B802LeftStockCmd, CmdMstInfo B802RightStockCmd,
            CmdMstInfo B803LeftStockCmd, CmdMstInfo B803RightStockCmd, CmdMstInfo B801L2LCmd, CmdMstInfo B802L2LCmd, CmdMstInfo B803L2LCmd, ref string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            sRemark = "Error: Transaction begin失敗！";
                            throw new Exception(sRemark);
                        }

                        sRemark = "";
                        if (B801LeftStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B801LeftStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B801RightStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B801RightStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B802LeftStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B802LeftStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B802RightStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B802RightStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B803LeftStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B803LeftStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B803RightStockCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B803RightStockCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B801L2LCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B801L2LCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B802L2LCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B802L2LCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        if (B803L2LCmd.Cmd_Sno != "")
                        {
                            if (!Cmd_Mst.FunInsCmdMst(B803L2LCmd, ref sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunE800CommandComplete(string sCmdSno, string sCmdMode, string sEmptyRetrieval, string sPortId, string sCarrierId, string WESAPI, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        CmdMstInfo cmd = new CmdMstInfo();
                        int newRet = 0;
                        string sRemark = "";
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            sRemark = "Error: Begin失敗！";
                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            return false;
                        }

                        if (sCmdMode == clsConstValue.CmdMode.StockIn)
                        {
                            if (!Cmd_Mst.FunGetCommand(sCmdSno, ref cmd, ref newRet, db))
                            {
                                strEM = $"Error: 取得cmdMst命令失敗, jobId = {sCmdSno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            if (sEmptyRetrieval != "F")
                            {
                                if (!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Finish_Wait, "", db))
                                {
                                    strEM = $"Error: Update CmdSts fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }

                                LotPutawayCompleteInfo info = new LotPutawayCompleteInfo
                                {
                                    jobId = cmd.JobID,
                                    lotId = cmd.BoxID,
                                    shelfId = cmd.Loc,
                                    isComplete = clsConstValue.YesNo.Yes
                                };
                                if (!api.GetLotPutawayComplete().FunReport(info, WESAPI))
                                {
                                    strEM = $"Error: LotPutawayComplete to WES fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }
                            }
                            else
                            {
                                if (!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Cancel_Wait, "", db))
                                {
                                    strEM = $"Error: Update CmdSts fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }

                                //WCSCancelInfo info = new WCSCancelInfo
                                //{
                                //    lotIdCarrierId = sCarrierId,
                                //    cancelType = clsConstValue.WesApi.CancelType.Lot_Putaway
                                //};
                                //if (!api.GetWCSCancel().FunReport(info, WESAPI))
                                //{
                                //    strEM = $"Error: LotPutawayCancel to WES fail, jobId = {sCmdSno}.";
                                //    db.TransactionCtrl(TransactionTypes.Rollback);
                                //    return false;
                                //}
                            }
                        }
                        else if (sCmdMode == clsConstValue.CmdMode.StockOut)
                        {
                            if (!Cmd_Mst.FunGetCommand(sCmdSno, ref cmd, ref newRet, db))
                            {
                                strEM = $"Error: 取得cmdMst命令失敗, jobId = {sCmdSno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            LotRetrieveCompleteInfo info = new LotRetrieveCompleteInfo
                            {
                                jobId = cmd.JobID,
                                lotId = cmd.BoxID,
                                portId = sPortId,
                                carrierId = sCarrierId
                            };

                            if (sEmptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.Normal)
                            {
                                if (!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Finish_Wait, "", db))
                                {
                                    strEM = $"Error: Update CmdSts fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }
                                info.isComplete = clsConstValue.YesNo.Yes;
                                info.disableLocation = "N";
                            }
                            else if (sEmptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.EmptyRetrieve)
                            {
                                sRemark = "異常：空出庫";
                                if (!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Cancel_Wait, clsEnum.Cmd_Abnormal.E2, sRemark, db))
                                {
                                    strEM = $"Error: Update cmdSts【空出庫】 fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }
                                info.isComplete = clsConstValue.YesNo.Yes;
                                info.emptyTransfer = clsConstValue.YesNo.Yes;
                                info.disableLocation = clsConstValue.YesNo.No;
                            }
                            else if (sEmptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.RetrieveFail)
                            {
                                sRemark = "異常：料捲取出失敗";
                                if (!Cmd_Mst.FunUpdateCmdSts(sCmdSno, clsConstValue.CmdSts.strCmd_Cancel_Wait, clsEnum.Cmd_Abnormal.EP, sRemark, db))
                                {
                                    strEM = $"Error: Update cmdSts【料捲取出失敗】 fail, jobId = {sCmdSno}.";
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    return false;
                                }
                                info.isComplete = clsConstValue.YesNo.Yes;
                                info.emptyTransfer = clsConstValue.YesNo.Yes;
                                info.disableLocation = clsConstValue.YesNo.Yes;
                            }
                            else
                            {
                                strEM = $"Error: emptyRetrieval 格式不合, jobId = {sCmdSno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            if (!api.GetLotRetrieveComplete().FunReport(info, WESAPI))
                            {
                                strEM = $"Error: LotRetrieveComplete to WES fail, jobId = {sCmdSno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }
                        }

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;
                    }
                    else
                    {
                        strEM = "Error: 開啟DB失敗！";
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strEM);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
        
        public bool FunSendEmptyRackToTower(ConveyorInfo toBuffer, CmdMstInfo cmd, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                        {
                            strEM = "Error: Begin失敗！";
                            return false;
                        }

                        if (!Cmd_Mst.FunInsCmdMst(cmd, ref strEM, db))
                        {
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            return false;
                        }

                        //上報WES此料架站口已有命令正在使用
                        PortStatusUpdateInfo info = new PortStatusUpdateInfo
                        {
                            portId = toBuffer.StnNo,
                            portStatus = ((int)clsEnum.WmsApi.portStatus.Processing).ToString()
                        };
                        if(!api.GetPortStatusUpdate().FunReport(info, _wmsApi.IP))
                        {
                            db.TransactionCtrl(TransactionTypes.Rollback);
                            throw new Exception($"Error: PortStatusUpdate to WES fail, portId = {info.portId}.");
                        }

                        db.TransactionCtrl(TransactionTypes.Commit);
                        return true;

                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
    }
}
