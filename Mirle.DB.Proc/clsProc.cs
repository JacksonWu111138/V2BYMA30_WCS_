using Mirle.DataBase;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.EccsSignal;
using Mirle.MapController;
using Mirle.Middle;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.DB.Proc
{
    public class clsProc
    {
        private Fun.clsCmd_Mst Cmd_Mst = new Fun.clsCmd_Mst();
        private Fun.clsLocMst LocMst = new Fun.clsLocMst();
        private Fun.clsTool tool = new Fun.clsTool();
        private Fun.clsRoutdef Routdef = new Fun.clsRoutdef();
        private Fun.clsMiddleCmd MiddleCmd = new Fun.clsMiddleCmd();
        private clsDbConfig _config = new clsDbConfig();
        public clsProc(clsDbConfig config)
        {
            _config = config;
        }

        public Fun.clsRoutdef GetFun_Routdef() => Routdef;

        public bool FunNormalCmd_Proc(string sAsrsStockInLocation_Sql, string sAsrsEquNo_Sql)
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
                                if (!Cmd_Mst.CheckCraneStatus(cmd, Device, CrnSignal, db)) continue;

                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!Routdef.FunGetLocation(cmd, Router, ref Start, ref End, db)) continue;
                                if (Start != End)
                                {
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
                                        bool IsDoubleCmd = false; CmdMstInfo cmd_DD = new CmdMstInfo();
                                        #region 判斷狀態
                                        if (!Routdef.CheckSourceIsOK(cmd, sLoc_Start, middle, Device, wms, ref IsDoubleCmd, ref cmd_DD, db)) continue;
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
    }
}
