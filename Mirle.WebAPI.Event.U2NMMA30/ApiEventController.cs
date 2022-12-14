using System;
using System.Data;
using Mirle.DB.Object;
using Mirle.DB.Proc;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.DataBase;
using Mirle.Middle;
using Mirle.WebAPI.Event.U2NMMA30.Models;
using Mirle.WebAPI.V2BYMA30;
using Mirle.WebAPI.V2BYMA30.ReportInfo;
using System.Web.Http;
using Mirle.Structure;
using Newtonsoft.Json;
using static Mirle.Structure.Info.VIDEnums;
using static System.Net.WebRequestMethods;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Linq;
using PutawayTransferInfo = Mirle.WebAPI.V2BYMA30.ReportInfo.PutawayTransferInfo;
using RetrieveTransferInfo = Mirle.WebAPI.V2BYMA30.ReportInfo.RetrieveTransferInfo;
using static Mirle.Def.clsEnum;
using Mirle.DB.Fun;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static Mirle.Def.clsConstValue;
using System.Windows.Forms;
using System.Collections.Generic;
using static Mirle.Def.clsEnum.ControllerApi;
using Mirle.Structure.Info;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Mirle.WriLog;

namespace Mirle.WebAPI.Event
{
    public class WCSController : ApiController
    {
        private DB.Fun.clsTool tool = new DB.Fun.clsTool();

        public WCSController()
        {
        }

        #region WES-->WCS

        [Route("WCS/CARRIER_TRANSFER")]
        [HttpPost]
        public IHttpActionResult CARRIER_TRANSFER([FromBody] CarrierTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER start!");
            try
            {
                //檢查輸入內容是否必要值為空

                if (string.IsNullOrEmpty(Body.carrierId))
                    throw new Exception($"Error: CarrierId為空，jobId = {Body.jobId}.");
                else if (string.IsNullOrEmpty(Body.carrierType))
                    throw new Exception($"Error: CarrierType為空，jobId = {Body.jobId}.");
                else if (string.IsNullOrEmpty(Body.fromLocation))
                    throw new Exception($"Error: FromLocation為空，jobId = {Body.jobId}.");
                else if (string.IsNullOrEmpty(Body.toLocation))
                    throw new Exception($"Error: ToLocation為空，jobId = {Body.jobId}");
                else if (Body.fromLocation != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == Body.fromLocation))
                    throw new Exception($"Error: FromLocation錯誤，fromLocation = {Body.fromLocation} and jobId = {Body.jobId}.");
                else if (Body.toLocation != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == Body.toLocation))
                    throw new Exception($"Error: ToLocation錯誤，toLocation = {Body.toLocation} and jobId = {Body.jobId}.");


                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(Body.carrierId, Body.priority, ref strEM))
                        throw new Exception($"<{Body.jobId}> {strEM}");
                }
                else
                {
                    cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                    if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                    {
                        throw new Exception($"<{Body.jobId}>取得序號失敗！");
                    }

                    cmd.BoxID = Body.carrierId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.S2S;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";
                    cmd.Loc = "";
                    cmd.Equ_No = "";
                    cmd.EXP_Date = "";
                    cmd.JobID = Body.jobId;
                    cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                    if (Body.toLocation == ConveyorDef.WES_B800CV)
                    {
                        int count = 0; bool check = false;
                        ConveyorInfo B800CV = new ConveyorInfo();
                        while (count < ConveyorDef.GetB800CV_List().Count())
                        {
                            B800CV = ConveyorDef.GetB800CV();
                            if (clsMiddle.GetMiddle().CheckIsOutReady(B800CV))
                            {
                                cmd.New_Loc = B800CV.BufferName;
                                check = true;
                                break;
                            }
                            count++;
                        }
                        if (!check)
                        {
                            throw new Exception("Error: B800CV 無InReady站口");
                        }
                    }
                    else if(ConveyorDef.GetSharingNode().Where(r => r.Stn_No == Body.toLocation).Any())
                    {
                        TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                        Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(Body.toLocation);
                        cmd.New_Loc = Cv_to.end.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.toLocation);
                        cmd.New_Loc = cv_to.BufferName;
                    }

                    cmd.Prty = Body.priority;
                    cmd.Remark = "";

                    if (Body.fromLocation == ConveyorDef.WES_B800CV)
                    {
                        int count = 0; bool check = false;
                        ConveyorInfo B800CV = new ConveyorInfo();
                        while (count < ConveyorDef.GetB800CV_List().Count())
                        {
                            B800CV = ConveyorDef.GetB800CV();
                            if (clsMiddle.GetMiddle().CheckIsInReady(B800CV))
                            {
                                cmd.Stn_No = B800CV.BufferName;
                                check = true;
                                break;
                            }
                            count++;
                        }
                        if (!check)
                        {
                            throw new Exception("Error: B800CV 無InReady站口");
                        }
                    }
                    else if (ConveyorDef.GetSharingNode().Where(r => r.Stn_No == Body.fromLocation).Any())
                    {
                        TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                        Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(Body.fromLocation);
                        cmd.Stn_No = Cv_to.start.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.fromLocation);
                        cmd.Stn_No = cv_to.BufferName;
                    }
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Def.clsTool.FunSwitchCarrierType(Body.carrierType);

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>/CARRIER_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CARRIER_PUTAWAY_TRANSFER")]
        [HttpPost]
        public IHttpActionResult CARRIER_PUTAWAY_TRANSFER([FromBody] CarrierPutawayTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_PUTAWAY_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_PUTAWAY_TRANSFER start!");
            try
            {
                //檢查輸入內容是否必要值為空或錯誤
                
                if (string.IsNullOrEmpty(Body.carrierId))
                    throw new Exception($"Error: CarrierId為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.carrierType))
                    throw new Exception($"Error: CarrierType為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.fromPortId))
                    throw new Exception($"Error: FromLocation為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.toShelfId))
                    throw new Exception($"Error: ToLocation為空，jobId = {Body.jobId}");
                else if (Body.fromPortId != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == Body.fromPortId))
                    throw new Exception($"Error: FromLocation錯誤，fromLocation = {Body.fromPortId} and jobId = {Body.jobId}.");


                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(Body.carrierId, Body.priority, ref strEM))
                        throw new Exception($"<{Body.jobId}> {strEM}");
                }
                else
                {
                    cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                    if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                    {
                        throw new Exception($"<{Body.jobId}>取得序號失敗！");
                    }

                    cmd.BoxID = Body.carrierId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.toShelfId;
                    cmd.Equ_No = cmd.Loc == "SHELF" ? "" : tool.funGetEquNoByLoc(cmd.Loc).ToString();

                    cmd.EXP_Date = "";
                    cmd.JobID = Body.jobId;
                    cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmd.New_Loc = "";
                    cmd.Prty = Body.priority;
                    cmd.Remark = "";

                    if (Body.fromPortId == "B800CV")
                    {
                        cmd.Stn_No = ConveyorDef.Box.B1_054.BufferName;
                    }
                    else if (ConveyorDef.GetSharingNode().Where(r => r.Stn_No == Body.fromPortId).Any())
                    {
                        TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                        Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(Body.fromPortId);
                        cmd.New_Loc = Cv_to.start.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.fromPortId);
                        cmd.Stn_No = cv_to.BufferName;
                    }
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Def.clsTool.FunSwitchCarrierType(Body.carrierType);

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_PUTAWAY_TRANSFER record end!"); 
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_PUTAWAY_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");

                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CARRIER_RETRIEVE_TRANSFER")]
        [HttpPost]
        public IHttpActionResult CARRIER_RETRIEVE_TRANSFER([FromBody] CarrierRetrieveTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_RETRIEVE_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_RETRIEVE_TRANSFER start!");
            try
            {
                //檢查輸入內容是否必要值為空
                
                if (string.IsNullOrEmpty(Body.carrierId))
                    throw new Exception($"Error: CarrierId為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.carrierType))
                    throw new Exception($"Error: CarrierType為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.fromShelfId))
                    throw new Exception($"Error: FromLocation為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.toLocation))
                    throw new Exception($"Error: ToLocation為空，jobId = {Body.jobId}");

                if(Body.toLocation.Contains(","))
                {
                    string[] toLocationList = Body.toLocation.Split(',');
                    for(int i = 0; i < toLocationList.Length; i++)
                    {
                        if (toLocationList[i] != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == toLocationList[i]))
                            throw new Exception($"Error: ToLocation錯誤，toLocation = {Body.toLocation} and jobId = {Body.jobId}.");
                    }
                }
                else if (Body.toLocation != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == Body.toLocation))
                    throw new Exception($"Error: ToLocation錯誤，toLocation = {Body.toLocation} and jobId = {Body.jobId}.");


                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(Body.carrierId, Body.priority, ref strEM))
                        throw new Exception($"<{Body.jobId}> {strEM}");
                }
                else
                {
                    cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                    if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                    {
                        throw new Exception($"<{Body.jobId}>取得序號失敗！");
                    }

                    cmd.BoxID = Body.carrierId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.fromShelfId;
                    cmd.Equ_No = tool.funGetEquNoByLoc(cmd.Loc).ToString();

                    cmd.EXP_Date = "";
                    cmd.JobID = Body.jobId;
                    cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmd.New_Loc = "";
                    cmd.Prty = Body.priority;
                    cmd.Remark = "";

                    if (Body.toLocation.Contains(','))
                    {
                        string[] destination = Body.toLocation.Split(',');

                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(destination[0]);
                        cmd.Stn_No = cv_to.BufferName;
                        for (int i = 1; i < destination.Length; i++)
                        {
                            cv_to = ConveyorDef.GetBuffer_ByStnNo(destination[i]);
                            cmd.Stn_No += "," + cv_to.BufferName;
                        }
                    }
                    else if (Body.toLocation == ConveyorDef.WES_B800CV)
                    {
                        int count = 0; bool check = false;
                        ConveyorInfo B800CV = new ConveyorInfo();
                        while (count < ConveyorDef.GetB800CV_List().Count())
                        {
                            B800CV = ConveyorDef.GetB800CV();
                            if (clsMiddle.GetMiddle().CheckIsInReady(B800CV))
                            {
                                cmd.Stn_No = B800CV.BufferName;
                                check = true;
                                break;
                            }
                            count++;
                        }
                        if (!check)
                        {
                            throw new Exception("Error: B800CV 無InReady儲位");
                        }
                    }
                    else if (ConveyorDef.GetSharingNode().Where(r => r.Stn_No == Body.toLocation).Any())
                    {
                        TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                        Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(Body.toLocation);
                        cmd.Stn_No = Cv_to.end.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.toLocation);
                        cmd.Stn_No = cv_to.BufferName;
                    }
                    


                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Def.clsTool.FunSwitchCarrierType(Body.carrierType);

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_RETRIEVE_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_RETRIEVE_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");

                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CARRIER_SHELF_TRANSFER")]
        [HttpPost]
        public IHttpActionResult CARRIER_SHELF_TRANSFER([FromBody] CarrierShelfTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_SHELF_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_SHELF_TRANSFER start!");
            try
            {
                //檢查輸入內容是否必要值為空
                
                if (string.IsNullOrEmpty(Body.carrierId))
                    throw new Exception($"Error: CarrierId為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.carrierType))
                    throw new Exception($"Error: CarrierType為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.fromShelfId))
                    throw new Exception($"Error: FromLocation為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.toShelfId))
                    throw new Exception($"Error: ToLocation為空，jobId = {Body.jobId}");


                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(Body.carrierId, Body.priority, ref strEM))
                        throw new Exception($"<{Body.jobId}> {strEM}");
                }
                else
                {
                    cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                    if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                    {
                        throw new Exception($"<{Body.jobId}>取得序號失敗！");
                    }

                    cmd.BoxID = Body.carrierId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.L2L;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.fromShelfId;
                    cmd.Equ_No = tool.funGetEquNoByLoc(cmd.Loc).ToString();

                    cmd.EXP_Date = "";
                    cmd.JobID = Body.jobId;
                    cmd.NeedShelfToShelf = clsEnum.NeedL2L.Y.ToString();

                    cmd.New_Loc = Body.toShelfId;

                    cmd.Prty = Body.priority;
                    cmd.Remark = "";
                    cmd.Stn_No = "";
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Body.carrierType;

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_SHELF_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_SHELF_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");

                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CARRIER_TRANSFER_CANCEL")]
        [HttpPost]
        public IHttpActionResult CARRIER_TRANSFER_CANCEL([FromBody] CarrierTransferCancelInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER_CANCEL> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER_CANCEL start!");
            try
            {
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetProc().FunCarrierTransferCancel(Body.jobId, ref strEM))
                    throw new Exception(strEM);

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER_CANCEL record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER_CANCEL> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");

                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/LOT_PUTAWAY_TRANSFER")]
        [HttpPost]
        public IHttpActionResult LOT_PUTAWAY_TRANSFER([FromBody] LotPutawayTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_PUTAWAY_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.LotReply rMsg = new U2NMMA30.Models.LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_PUTAWAY_TRANSFER start!");
            try
            {
                //檢查輸入內容是否必要值為空

                if (string.IsNullOrEmpty(Body.lotId))
                    throw new Exception($"Error: LotId為空，jobId = {Body.jobId}");
                else if (string.IsNullOrEmpty(Body.fromPortId))
                    throw new Exception($"Error: FromLocation為空，jobId = {Body.jobId}");

                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(Body.lotId, Body.priority, ref strEM))
                        throw new Exception($"<{Body.jobId}> {strEM}");
                }
                else
                {

                    cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                    if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                    {
                        throw new Exception($"<{Body.jobId}>取得序號失敗！");
                    }

                    cmd.BoxID = Body.lotId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.toShelfId;
                    cmd.Equ_No = "7";

                    cmd.EXP_Date = "";
                    cmd.JobID = Body.jobId;
                    cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                    cmd.New_Loc = "";
                    cmd.Prty = Body.priority;
                    cmd.Remark = "";

                    if (Body.fromPortId == ConveyorDef.WES_B800CV)
                    {
                        int count = 0; bool check = false;
                        ConveyorInfo B800CV = new ConveyorInfo();
                        while (count < ConveyorDef.GetB800CV_List().Count())
                        {
                            B800CV = ConveyorDef.GetB800CV();
                            if (clsMiddle.GetMiddle().CheckIsInReady(B800CV))
                            {
                                cmd.Stn_No = B800CV.BufferName;
                                check = true;
                                break;
                            }
                            count++;
                        }
                        if (!check)
                        {
                            throw new Exception("Error: B800CV 無InReady站口");
                        }
                    }
                    else if (ConveyorDef.GetSharingNode().Where(r => r.Stn_No == Body.fromPortId).Any())
                    {
                        TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                        Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(Body.fromPortId);
                        cmd.Stn_No = Cv_to.start.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.fromPortId);
                        cmd.Stn_No = cv_to.BufferName;
                        cmd.CurDeviceID = cv_to.DeviceId;
                        cmd.CurLoc = cv_to.BufferName;
                    }
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";

                    cmd.carrierType = clsConstValue.WesApi.CarrierType.Lot;
                    cmd.lotSize = Body.lotSize;
                    //以下用transaction取代
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);

                    PutawayTransferInfo info = new PutawayTransferInfo
                    {
                        jobId = cmd.Cmd_Sno,
                        reelId = cmd.BoxID,
                        lotSize = cmd.lotSize,
                        toShelfId = cmd.Loc
                    };
                    if (!clsAPI.GetAPI().GetPutawayTransfer().FunReport(info, clsAPI.GetTowerApiConfig().IP))
                        throw new Exception($"Error: PutawayTransfer E800C接收失敗, jobId = {cmd.Cmd_Sno}.");

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, ""))
                        throw new Exception($"Error: 更改CmdSts至Running失敗, jobId = {cmd.Cmd_Sno}.");
                    //以上用transaction取代

                    //transaction方法
                    /*
                    if (!clsDB_Proc.GetDB_Object().GetProc().FunLotPutawayTransfer(cmd, clsAPI.GetTowerApiConfig().IP, ref strEM))
                        throw new Exception(strEM);
                    */
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_PUTAWAY_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_PUTAWAY_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/LOT_RETRIEVE_TRANSFER")]
        [HttpPost]
        public IHttpActionResult LOT_RETRIEVE_TRANSFER([FromBody] U2NMMA30.Models.LotRetrieveTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_RETRIEVE_TRANSFER start!");
            try
            {
                CmdMstInfo cmd = new CmdMstInfo();
                string strEM = "";
                if (Body.priority == "1")
                {   //更新優先級
                    foreach (var lot in Body.lotList)
                    {
                        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(lot.lotId, Body.priority, ref strEM))
                        {
                            throw new Exception($"<{Body.jobId}> {strEM}");
                        }
                    }
                }
                else
                {

                    V2BYMA30.ReportInfo.LotRetrieveTransferInfo info = new V2BYMA30.ReportInfo.LotRetrieveTransferInfo();
                    info.lotList = new List<LotListInfo>();
                    //toPortList [1] => E800-5, [2] => E800-6, [3] => E800-7, [4] => E800-8
                    int[] toPortList = new int[4];
                    for (int i = 0; i < toPortList.Length; i++)
                        toPortList[i] = 0;
                    foreach(var lot in Body.lotList)
                    {
                        //檢查輸入內容是否必要值為空

                        if (string.IsNullOrEmpty(lot.lotId))
                            throw new Exception($"Error: 存在LotId為空，jobId = {Body.jobId}");
                        else if (string.IsNullOrEmpty(lot.fromShelfId))
                            throw new Exception($"Error: 存在FromShelf為空，LotId = {lot.lotId} and jobId = {Body.jobId}");
                        else if (string.IsNullOrEmpty(lot.toPortId))
                            throw new Exception($"Error: 存在ToLocation為空，LotId = {lot.lotId} and jobId = {Body.jobId}");
                        else if (lot.toPortId != ConveyorDef.WES_B800CV && !ConveyorDef.GetStations().Any(r => r.StnNo == lot.toPortId))
                            throw new Exception($"Error: 存在ToLocation錯誤，LotId = {lot.lotId}, toLocation = {lot.toPortId} and jobId = {Body.jobId}.");

                    }


                    foreach (var lot in Body.lotList)
                    {
                        LotListInfo oklot = new LotListInfo();
                        cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                        if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                        {
                            throw new Exception($"<{Body.jobId}>取得序號失敗！");
                        }

                        cmd.BoxID = lot.lotId;
                        cmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                        cmd.CurDeviceID = "";
                        cmd.CurLoc = "";
                        cmd.End_Date = "";

                        cmd.Loc = lot.fromShelfId;
                        cmd.Equ_No = "7";
                        cmd.carrierType = clsConstValue.WesApi.CarrierType.Lot;

                        cmd.EXP_Date = "";
                        cmd.JobID = Body.jobId;
                        cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                        cmd.New_Loc = "";
                        cmd.Prty = Body.priority;
                        cmd.Remark = "";

                        if (lot.toPortId == ConveyorDef.WES_B800CV)
                        {
                            int count = 0; bool check = false;
                            ConveyorInfo B800CV = new ConveyorInfo();
                            while (count < ConveyorDef.GetB800CV_List().Count())
                            {
                                B800CV = ConveyorDef.GetB800CV();
                                if (clsMiddle.GetMiddle().CheckIsOutReady(B800CV))
                                {
                                    cmd.Stn_No = B800CV.BufferName;
                                    check = true;
                                    break;
                                }
                                count++;
                            }
                            if (!check)
                            {

                                throw new Exception("Error: B800CV 無OutReady儲位");
                            }
                        }
                        else if (lot.toPortId == "E800-8")
                        {
                            cmd.Stn_No = lot.toPortId;
                        }
                        else if (ConveyorDef.GetSharingNode().Where(r => r.Stn_No == lot.toPortId).Any())
                        {
                            TwoNodeOneStnnoInfo Cv_to = new TwoNodeOneStnnoInfo();
                            Cv_to = ConveyorDef.GetTwoNodeOneStnnoByStnNo(lot.toPortId);
                            cmd.Stn_No = Cv_to.end.BufferName;
                        }
                        else
                        {
                            var cv_to = ConveyorDef.GetBuffer_ByStnNo(lot.toPortId);
                            cmd.Stn_No = cv_to.BufferName;
                        }

                        if (lot.toPortId == "E800-5")
                            toPortList[0] = 1;
                        else if (lot.toPortId == "E800-6")
                            toPortList[1] = 1;
                        else if (lot.toPortId == "E800-7")
                            toPortList[2] = 1;
                        else if (lot.toPortId == "E800-8")
                            toPortList[3] = 1;

                        cmd.rackLocation = lot.rackLocation;
                        cmd.largest = lot.largest;
                        cmd.Host_Name = "WES";
                        cmd.Zone_ID = "";
                        //cmd.carrierType = Body.carrierType;

                        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        {
                            if (!clsDB_Proc.GetDB_Object().GetLotRetrieveNG().FunRetrieveNG_Occur(cmd.Cmd_Sno, cmd.JobID, lot.lotId, ref strEM))
                            {
                                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, strEM);
                                continue;
                            }
                            continue;
                        }

                        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Running, ""))
                            throw new Exception($"Error: 更改CmdSts至Running失敗, jobId = {cmd.Cmd_Sno}.");

                        oklot.cmdSno = cmd.Cmd_Sno;
                        oklot.lotId = lot.lotId;
                        oklot.toPortId = lot.toPortId;
                        oklot.fromShelfId = lot.fromShelfId;
                        oklot.rackLocation = lot.rackLocation;
                        oklot.largest = lot.largest;

                        info.lotList.Add(oklot);
                        oklot = null;
                    }
                    info.priority = Body.priority;
                    if (!clsAPI.GetAPI().GetLotRetrieveTransfer().FunReport(info, clsAPI.GetTowerApiConfig().IP))
                        throw new Exception($"Error: LotRetrieveTransfer to E800 fail, jobId = {Body.jobId}");
                    //E800端出庫失敗後如何回覆lotId?
                    for(int i = 0; i< toPortList.Length; i++)
                    {
                        if (toPortList[i] == 1)
                        {
                            PortStatusUpdateInfo info2 = new PortStatusUpdateInfo
                            {
                                portStatus = ((int)clsEnum.WmsApi.portStatus.Processing).ToString(),
                            };
                            switch (i)
                            {
                                case 0:
                                    info2.portId = "E800-5";
                                    break;
                                case 1:
                                    info2.portId = "E800-6";
                                    break;
                                case 2:
                                    info2.portId = "E800-7";
                                    break;
                                case 3:
                                    info2.portId = "E800-8";
                                    break;
                            }

                            if (!clsAPI.GetAPI().GetPortStatusUpdate().FunReport(info2, clsAPI.GetWesApiConfig().IP))
                                throw new Exception($"Error: PortStatusUpdate to WES fail, WES's jobId = {cmd.JobID}.");
                        }
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_RETRIEVE_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/LOT_SHELF_TRANSFER")]
        [HttpPost]
        public IHttpActionResult LOT_SHELF_TRANSFER([FromBody] LotShelfTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_SHELF_TRANSFER> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.LotReply rMsg = new U2NMMA30.Models.LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_SHELF_TRANSFER start!");
            try
            {
                //不會出現，E800C沒有這個功能

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_SHELF_TRANSFER record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_SHELF_TRANSFER> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/LOT_TRANSFER_CANCEL")]
        [HttpPost]
        public IHttpActionResult LOT_TRANSFER_CANCEL([FromBody] U2NMMA30.Models.LotTransferCancelInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_TRANSFER_CANCEL> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.LotReply rMsg = new U2NMMA30.Models.LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_TRANSFER_CANCEL start!");
            try
            {
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetProc().FunLotTransferCancel(Body.jobId, ref strEM))
                    throw new Exception(strEM);

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_TRANSFER_CANCEL record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_TRANSFER_CANCEL> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }
        #endregion

        #region Controllers-->WCS

        [Route("WCS/AGV_POS_REPORT")]
        [HttpPost]
        public IHttpActionResult AGV_POS_REPORT([FromBody] AGVPosReportInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<AGV_POS_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>AGV_POS_REPORT start!");
            try
            {
                string strEM = "";
                if(Body.currentLoc != ConveyorDef.AGV.E2_35.BufferName && Body.currentLoc != ConveyorDef.AGV.E2_36.BufferName && Body.currentLoc != ConveyorDef.AGV.E2_37.BufferName)
                {
                    //執行BufferRoll
                    ConveyorInfo conveyor = new ConveyorInfo();
                    conveyor = ConveyorDef.GetBuffer(Body.currentLoc);

                    BufferRollInfo info = new BufferRollInfo { jobId = Body.jobId, bufferId = conveyor.BufferName };

                    if (!clsAPI.GetAPI().GetBufferRoll().FunReport(info, conveyor.API.IP))
                        throw new Exception($"Error: BufferRoll to {Body.currentLoc} fail. jobId = {Body.jobId}.");
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>AGV_POS_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<AGV_POS_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/ALARM_HAPPEN_REPORT")]
        [HttpPost]
        public IHttpActionResult ALARM_HAPPEN_REPORT([FromBody] AlarmInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<ALARM_HAPPEN_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>ALARM_HAPPEN_REPORT start!");
            try
            {
                string strEM = "";
                if (Body.status == ((int)clsEnum.AlarmSts.OnGoing).ToString())
                {
                    if (!clsDB_Proc.GetDB_Object().GetAlarmCVCLog().FunAlarm_Occur(Body.jobId, Body.deviceId, Body.alarmCode,
                        Body.alarmDef, Body.bufferId, Body.happenTime, ref strEM))
                        throw new Exception(strEM);
                }
                else if (Body.status == ((int)clsEnum.AlarmSts.Clear).ToString())
                {
                    if (!clsDB_Proc.GetDB_Object().GetAlarmCVCLog().FunAlarm_Solved(Body.jobId, Body.deviceId, Body.alarmCode,
                        Body.alarmDef, Body.bufferId, Body.happenTime, ref strEM))
                        throw new Exception(strEM);
                }
                else throw new Exception($"Error: status格式有錯, jobId = {Body.jobId}. clsEnum.AlarmSts.OnGoing.ToString() = {clsEnum.AlarmSts.OnGoing.ToString()}.");


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>ALARM_HAPPEN_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<ALARM_HAPPEN_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/BCR_CHECK_REQUEST")]
        [HttpPost]
        public IHttpActionResult BCR_CHECK_REQUEST([FromBody] BCRCheckInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BCR_CHECK_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST start!");
            try
            {
                CmdMstInfo cmd = new CmdMstInfo();
                bool check = false;
                check = clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckHasCommand_ByBoxID(Body.barcode, ref cmd);
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string deviceId = tool.GetDeviceId(Body.location);

                //待加入Cycle Run判斷
                if (check)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{cmd.Cmd_Sno}>This BCRCheck exist.");
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, Body.location))
                        throw new Exception($"Error: UpdateCurLoc Fail. jobId = {Body.jobId}");

                    //transaction
                    if ((cmd.Cmd_Mode == clsConstValue.CmdMode.S2S && Body.location == cmd.New_Loc) ||
                        (cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut && Body.location == cmd.Stn_No) && 
                        Body.location != ConveyorDef.E04.LO1_07.BufferName)
                    {
                        if(Body.location == ConveyorDef.Tower.E1_04.BufferName)
                        {
                            rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                            rMsg.returnComment = "";

                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BCR_CHECK_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                            return Json(rMsg);
                        }

                        throw new Exception($"Error: 目前抵達命令終點，稍後等待命令清除！ jobId = {cmd.Cmd_Sno}.");
                    }
                    else if (Body.location == ConveyorDef.Box.B1_054.BufferName && cmd.Cmd_Mode == clsConstValue.CmdMode.S2S && 
                             (cmd.New_Loc == ConveyorDef.AGV.B1_070.BufferName || cmd.New_Loc == ConveyorDef.AGV.B1_074.BufferName || cmd.New_Loc == ConveyorDef.AGV.B1_070.BufferName))
                    {
                        throw new Exception($"Error: 箱式倉站對站命令尚未結束，稍後等待命令清除！ jobId = {cmd.Cmd_Sno}.");
                    }

                    if(
                       (
                       ((Body.location == ConveyorDef.Box.B1_037.BufferName || Body.location == ConveyorDef.Box.B1_117.BufferName) && cmd.Equ_No == "3") ||
                       ((Body.location == ConveyorDef.Box.B1_041.BufferName || Body.location == ConveyorDef.Box.B1_121.BufferName) && cmd.Equ_No == "4") ||
                       ((Body.location == ConveyorDef.Box.B1_045.BufferName || Body.location == ConveyorDef.Box.B1_125.BufferName) && cmd.Equ_No == "5")
                       )
                       && cmd.Cmd_Mode == clsConstValue.CmdMode.StockIn 
                       )
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{cmd.Cmd_Sno}>This BCRCheck 是入庫命令在正確流道上，等待箱式倉入庫時序處理.");
                    }
                    else if (con.ControllerID == clsControllerID.pCBA_ControllerID)
                    {
                        if(clsCycleRun.GetPCBAcycleRun())
                        {
                            CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                            {
                                jobId = cmd.Cmd_Sno,
                                carrierType = clsConstValue.ControllerApi.CarrierType.Mag,
                                bufferId = Body.location
                            };
                            if(!clsAPI.GetAPI().GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                            {
                                throw new Exception($"Error: Cycle Run 寫入序號失敗, BufferId = {Body.location} and jobId = {cmd.Cmd_Sno}.");
                            }
                        }
                    }
                    else
                    {

                        if((Body.location == ConveyorDef.Box.B1_142.BufferName || Body.location == ConveyorDef.Box.B1_147.BufferName) && cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut)
                        {
                            //順途也要上報至MES至詢問「該箱是否完成搬運」
                            CarrierReturnNextInfo checkInfo = new CarrierReturnNextInfo
                            {
                                carrierId = Body.barcode,
                                fromLocation = con.StnNo,
                                carrierType = clsConstValue.WesApi.CarrierType.Bin,
                                isEmpty = clsConstValue.YesNo.Yes
                            };
                            CarrierReturnNextReply checkReply = new CarrierReturnNextReply();
                            if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(checkInfo, ref checkReply, clsAPI.GetWesApiConfig().IP))
                                throw new Exception($"Error: Carrier Return Next fail, 建料未完成或詢問是否完成撿料失敗");
                        }
                        
                        CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                        {
                            jobId = cmd.Cmd_Sno,
                            bufferId = Body.location,
                            carrierType = Body.carrierType
                        };

                        con = ConveyorDef.GetBuffer(Body.location);
                        if (!clsAPI.GetAPI().GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                            throw new Exception($"Error: CV_RECEIVE_NEW_BIN fail. jobId = {Body.jobId}");

                        if (Body.location == ConveyorDef.Tower.E1_04.BufferName)
                        {
                            rMsg.returnCode = clsConstValue.ApiReturnCode.Waitretry;
                            rMsg.returnComment = "";

                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BCR_CHECK_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                            return Json(rMsg);
                        }
                    }
                    
                }
                else if (!check)
                {
                    if(!string.IsNullOrEmpty(Body.jobId) && Body.jobId.Contains("EMPTY"))
                    {
                        //空箱回庫流程(有bcr)
                        EmptyESDCarrierUnloadInfo info = new EmptyESDCarrierUnloadInfo
                        {
                            location = con.StnNo,
                            carrierId = Body.barcode
                        };

                        if (ConveyorDef.GetSharingNode().Any(r => r.end.BufferName == Body.location || r.start.BufferName == Body.location))
                        {
                            foreach ( var v in ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == Body.location || r.start.BufferName == Body.location))
                                info.location = v.Stn_No;
                        }

                        if (!clsAPI.GetAPI().GetEmptyESDCarrierUnload().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: EmptyESDCarrierUnload 上報WES失敗, barcode = {Body.barcode}.");

                    }
                    else if (Body.location == ConveyorDef.Box.B1_037.BufferName || Body.location == ConveyorDef.Box.B1_041.BufferName ||
                        Body.location == ConveyorDef.Box.B1_045.BufferName || Body.location == ConveyorDef.Box.B1_054.BufferName ||
                        Body.location == ConveyorDef.Box.B1_117.BufferName || Body.location == ConveyorDef.Box.B1_121.BufferName ||
                        Body.location == ConveyorDef.Box.B1_125.BufferName || Body.location == ConveyorDef.Box.B1_134.BufferName ||
                        Body.location == ConveyorDef.AGV.B1_070.BufferName || Body.location == ConveyorDef.AGV.B1_074.BufferName ||
                        Body.location == ConveyorDef.AGV.B1_078.BufferName )
                    {
                        //待加入空箱回庫之判斷: jobId contains Empty, 上報EmptyESDCarrierUnload
                        CarrierPutawayCheckInfo info = new CarrierPutawayCheckInfo
                        {
                            portId = ConveyorDef.WES_B800CV,
                            carrierId = Body.barcode,
                            storageType = clsConstValue.WesApi.StorageType.Box
                        }; 
                        
                        if (!clsAPI.GetAPI().GetCarrierPutawayCheck().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: Sending CarrierPutawayCheck to WES fail, jobId = {Body.jobId}.");
                    }
                    else if (Body.location == ConveyorDef.AGV.M1_10.BufferName || Body.location == ConveyorDef.AGV.M1_20.BufferName ||
                            Body.location == ConveyorDef.AGV.M1_05.BufferName || Body.location == ConveyorDef.AGV.M1_15.BufferName)
                    //M1_05, M1_15為故障模式可能會使用之入庫點
                    {
                        if (!clsCycleRun.GetPCBAcycleRun())
                        {
                            //正常模式
                            CarrierPutawayCheckInfo info = new CarrierPutawayCheckInfo
                            {
                                portId = con.StnNo,
                                carrierId = Body.barcode,
                                storageType = clsConstValue.WesApi.StorageType.PCBA
                            };

                            if (!clsAPI.GetAPI().GetCarrierPutawayCheck().FunReport(info, clsAPI.GetWesApiConfig().IP))
                                throw new Exception($"Error: Sending CarrierPutawayCheck to WES fail, jobId = {Body.jobId}.");
                        }
                        else
                        {
                            //Cycle Run 模式

                        }
                        
                    }
                    else if (Body.location == ConveyorDef.AGV.LO3_01.BufferName && Body.carrierType == clsConstValue.ControllerApi.CarrierType.Bin)
                    {
                        //1220更改，上報 Empty ESDCarrier Unload，由WES下達回庫命令
                        //加入名稱電梯LO3_01名稱為 LIFT5
                        EmptyESDCarrierUnloadInfo info = new EmptyESDCarrierUnloadInfo
                        {
                            carrierId = Body.barcode,
                            location = ConveyorDef.AGV.LO3_01.StnNo
                        };

                        if(!clsAPI.GetAPI().GetEmptyESDCarrierUnload().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        {
                            throw new Exception($"Error: 傳送EmptyESDCarrierUnload to WES 失敗，BoxId = {Body.barcode}.");
                        }

                        //1220更改，取消
                        #region WCS自行生成空靜電箱回庫命令
                        /*
                        string strEM = "";
                        cmd = new CmdMstInfo();
                        cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                        if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                        {
                            throw new Exception($"<{Body.jobId}>取得序號失敗！");
                        }

                        cmd.BoxID = Body.barcode;
                        cmd.Cmd_Mode = clsConstValue.CmdMode.S2S;
                        cmd.CurDeviceID = "";
                        cmd.CurLoc = "";
                        cmd.End_Date = "";
                        cmd.Loc = "";
                        cmd.Equ_No = "";
                        cmd.EXP_Date = "";
                        cmd.JobID = Body.jobId;
                        cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                        ConveyorInfo B800CV = new ConveyorInfo();
                        int count = 0; check = false;
                        while (count < ConveyorDef.GetB800CV_List().Count())
                        {
                            B800CV = ConveyorDef.GetB800CV();
                            if (clsMiddle.GetMiddle().CheckIsOutReady(B800CV))
                            {
                                cmd.New_Loc = B800CV.BufferName;
                                check = true;
                                break;
                            }
                            count++;
                        }
                        if (!check)
                        {
                            throw new Exception("Error: B800CV 無接收ready站口");
                        }
                        cmd.Prty = "5";
                        cmd.Remark = "";

                        cmd.Stn_No = Body.location;

                        cmd.Host_Name = "WCS";
                        cmd.Zone_ID = "";
                        cmd.carrierType = clsConstValue.ControllerApi.CarrierType.Bin;

                        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                            throw new Exception(strEM);
                        */
                        #endregion
                    }
                    else if (Body.location == ConveyorDef.AGV.LO3_01.BufferName && Body.carrierType == clsConstValue.ControllerApi.CarrierType.Mag)
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST 電梯口檢測到空Mag須回庫");

                        EmptyMagazineUnloadInfo emptyMagUnload = new EmptyMagazineUnloadInfo
                        {
                            carrierId = Body.barcode,
                            location = ConveyorDef.AGV.LO3_01.StnNo
                        };

                        if (!clsAPI.GetAPI().GetEmptyMagazineUnload().FunReport(emptyMagUnload, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: 上WES報EmptyMagzineUnload失敗, CarrierID = {emptyMagUnload.carrierId} and location = {emptyMagUnload.location}.");

                    }
                    else if (Body.location == ConveyorDef.E04.LO1_07.BufferName)
                    {
                        //到十樓需寫假帳使CV繼續至人員可操控區域
                        CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                        {
                            jobId = "99449",
                            bufferId = ConveyorDef.E04.LO1_07.BufferName,
                            carrierType = clsConstValue.ControllerApi.CarrierType.Bin
                        };
                        if (!clsAPI.GetAPI().GetCV_ReceiveNewBinCmd().FunReport(info, con.API.IP))
                            throw new Exception($"Error: CVReceiveNewBinCmd fail to LIFT4C. BufferId = {info.bufferId}.");

                    }
                    else
                    {


                        CarrierReturnNextInfo info = new CarrierReturnNextInfo
                        {
                            carrierId = Body.barcode,
                            isEmpty = clsConstValue.YesNo.No,
                            fromLocation = con.StnNo,
                            carrierType = Def.clsTool.FunSwitchCarrierType(Body.carrierType)
                        };
                        if(ConveyorDef.GetSharingNode().Any(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName))
                        {
                            info.fromLocation = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                        CarrierReturnNextReply reply = new CarrierReturnNextReply();    

                        if (clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                        {
                            if (reply.returnStocker == clsConstValue.YesNo.Yes && Body.location.Contains("B1"))
                            {
                                CarrierPutawayCheckInfo info_2 = new CarrierPutawayCheckInfo
                                {
                                    portId = con.StnNo,
                                    carrierId = Body.barcode,
                                    storageType = "B800"
                                };
                                if(!clsAPI.GetAPI().GetCarrierPutawayCheck().FunReport(info_2, clsAPI.GetWesApiConfig().IP))
                                    throw new Exception($"Error: Sending CarrierPutawayCheck to WES fail, jobId = {Body.jobId}");
                            }
                        }
                        else
                            throw new Exception($"Error: Sending CarrierReturnNext to WES fail, jobId = {Body.jobId}");
                    }

                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BCR_CHECK_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/BIN_EMPTY_LEAVE_REQUEST")]
        [HttpPost]
        public IHttpActionResult BIN_EMPTY_LEAVE_REQUEST([FromBody] BinEmptyLeaveInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BIN_EMPTY_LEAVE_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            BinReply rMsg = new BinReply
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId,
                binId = Body.binId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BIN_EMPTY_LEAVE_REQUEST start!");
            try
            {
                string strEM = "";
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.position);
                string deviceId = tool.GetDeviceId(Body.position);

                CmdMstInfo cmd = new CmdMstInfo();
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, deviceId, Body.position))
                    {
                        strEM = $"Error: Update CmdMst curLoc fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }
                else
                {
                    EmptyCarrierUnloadInfo info = new EmptyCarrierUnloadInfo
                    {
                        jobId = Body.jobId,
                        carrierId = Body.binId,
                        location = con.StnNo
                    }; 
                    if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                    {
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                    }
                    if (!clsAPI.GetAPI().GetEmptyCarrierUnload().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    {
                        strEM = $"Error: EmptyCarrierUnload to WES fail, jobid = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BIN_EMPTY_LEAVE_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BIN_EMPTY_LEAVE_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //未撰寫
        [Route("WCS/BLOCK_STATUS_CHANGE")]
        [HttpPost]
        public IHttpActionResult BLOCK_STATUS_CHANGE([FromBody] BlockStatusInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BLOCK_STATUS_CHANGE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BLOCK_STATUS_CHANGE start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BLOCK_STATUS_CHANGE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BLOCK_STATUS_CHANGE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CMD_DESTINATION_CHECK")]
        [HttpPost]
        public IHttpActionResult CMD_DESTINATION_CHECK([FromBody] CmdDestinationCheckInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DESTINATION_CHECK> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            CmdDestinationCheckReply rMsg = new CmdDestinationCheckReply
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DESTINATION_CHECK start!");
            try
            {
                CmdMstInfo cmd = new CmdMstInfo();
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                    throw new Exception($"Error: CMDMST與middle都沒有此命令, jobId = {Body.jobId}.");
                else if (cmd.Cmd_Mode == CmdMode.StockOut)
                {
                    if (cmd.Stn_No.Contains(","))
                    {
                        string[] locations = cmd.Stn_No.Split(',');

                        //找尋當下閒置的撿料口
                        BufferStatusQueryInfo info = new BufferStatusQueryInfo
                        {
                            jobId = cmd.Cmd_Sno
                        };
                        BufferStatusReply bufferStatusReply = new BufferStatusReply();

                        bool isFindLocation = false;
                        for (int i = 0; i < locations.Length; i++)
                        {
                            info.bufferId = locations[i];
                            if (!clsAPI.GetAPI().GetBufferStatusQuery().FunReport(info, clsAPI.GetBoxApiConfig().IP, ref bufferStatusReply))
                                throw new Exception($"Error: 詢問箱式倉撿料口失敗, jobId = {cmd.Cmd_Sno}.");

                            //判斷撿料口若無命令序號則為空閒
                            if(bufferStatusReply.jobId == "00000" || string.IsNullOrEmpty(bufferStatusReply.jobId))
                            {
                                if (locations[i] == ConveyorDef.Box.B1_147.BufferName)
                                    rMsg.toLocation = "B1-146";
                                else if (locations[i] == ConveyorDef.Box.B1_142.BufferName)
                                    rMsg.toLocation = "B1-141";
                                isFindLocation = true;
                                break;
                            }
                        }

                        if (!isFindLocation)
                        {
                            rMsg.toLocation = "GO";
                        }
                    }
                    else if ((cmd.Stn_No != ConveyorDef.Box.B1_062.BufferName && cmd.Stn_No != ConveyorDef.Box.B1_067.BufferName &&
                            cmd.Stn_No != ConveyorDef.Box.B1_142.BufferName && cmd.Stn_No != ConveyorDef.Box.B1_147.BufferName) &&
                            !ConveyorDef.GetLifetNode_List().Any(r => r.BufferName == Body.location))
                    {
                        if (cmd.boxStockOutAgv == "")
                        {
                            cmd.boxStockOutAgv = ConveyorDef.GetB800CVOut().BufferName;
                            if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateboxStockOutAgv(Body.jobId, cmd.boxStockOutAgv))
                                rMsg.toLocation = cmd.boxStockOutAgv;
                            else
                                throw new Exception($"Error: 更新boxStockOutAgv失敗, jobId = {Body.jobId}.");
                        }
                        else
                        {
                            rMsg.toLocation = cmd.boxStockOutAgv;
                        }
                    }
                    else if (cmd.Stn_No == ConveyorDef.Box.B1_062.BufferName)
                    {
                        rMsg.toLocation = "B1-061";
                    }
                    else if (cmd.Stn_No == ConveyorDef.Box.B1_067.BufferName)
                    {
                        rMsg.toLocation = "B1-066";
                    }
                    else if (cmd.Stn_No == ConveyorDef.Box.B1_142.BufferName)
                    {
                        rMsg.toLocation = "B1-141";
                    }
                    else if (cmd.Stn_No == ConveyorDef.Box.B1_147.BufferName)
                    {
                        rMsg.toLocation = "B1-146";
                    }
                    else if (ConveyorDef.GetLifetNode_List().Any(r => r.BufferName == Body.location))
                    {
                        if (cmd.Stn_No == ConveyorDef.E04.LO1_07.BufferName)
                        {
                            rMsg.toLocation = ConveyorDef.E04.LO1_07.BufferName;
                        }
                        else if (ConveyorDef.GetSharingNode3F().Any(r => r.end.BufferName == cmd.Stn_No || r.start.BufferName == cmd.Stn_No)
                            || ConveyorDef.GetNode_3F().Any(r => r.BufferName == cmd.Stn_No))
                            rMsg.toLocation = ConveyorDef.AGV.LO4_04.BufferName;
                        else if (ConveyorDef.GetSharingNode5F().Any(r => r.end.BufferName == cmd.Stn_No || r.start.BufferName == cmd.Stn_No)
                            || ConveyorDef.GetNode_5F().Any(r => r.BufferName == cmd.Stn_No))
                            rMsg.toLocation = ConveyorDef.AGV.LO5_04.BufferName;
                        else if (ConveyorDef.GetSharingNode6F().Any(r => r.end.BufferName == cmd.Stn_No || r.start.BufferName == cmd.Stn_No)
                            || ConveyorDef.GetNode_6F().Any(r => r.BufferName == cmd.Stn_No))
                            rMsg.toLocation = ConveyorDef.AGV.LO6_04.BufferName;
                        else
                            rMsg.toLocation = ConveyorDef.AGV.LO3_01.BufferName;
                    }
                }
                else if (cmd.Cmd_Mode == CmdMode.StockIn)
                {
                    if (cmd.CurLoc == "CVIN")
                    {
                        if (cmd.Loc != "Shelf")
                        {
                            int temp = Convert.ToInt32(cmd.Loc.Substring(0, 2));
                            if (temp > 8 && temp <= 12)
                            {
                                if (Body.location == ConveyorDef.Box.B1_037.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_031.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_111.BufferName;
                            }
                            else if (temp > 12 && temp <= 16)
                            {
                                if (Body.location == ConveyorDef.Box.B1_041.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_019.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_099.BufferName;
                            }
                            else if (temp > 16 && temp <= 20)
                            {
                                if (Body.location == ConveyorDef.Box.B1_045.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_007.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_087.BufferName;
                            }
                        }
                    }
                    else if (cmd.CurLoc == "CVWT")
                    {
                        if (cmd.Loc != "Shelf")
                        {
                            int temp = Convert.ToInt32(cmd.Loc.Substring(0, 2));
                            if (temp == 10 || temp == 11)
                            {
                                if (Body.location == ConveyorDef.Box.B1_037.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_033.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_113.BufferName;
                            }
                            else if (temp == 9 || temp == 12)
                            {
                                if (Body.location == ConveyorDef.Box.B1_037.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_036.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_116.BufferName;
                            }
                            else if (temp == 14 || temp == 15)
                            {
                                if (Body.location == ConveyorDef.Box.B1_041.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_021.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_101.BufferName;
                            }
                            else if (temp == 13 || temp == 16)
                            {
                                if (Body.location == ConveyorDef.Box.B1_041.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_024.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_104.BufferName;
                            }
                            else if (temp == 18 || temp == 19)
                            {
                                if (Body.location == ConveyorDef.Box.B1_045.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_009.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_089.BufferName;
                            }
                            else if (temp == 17 || temp == 20)
                            {
                                if (Body.location == ConveyorDef.Box.B1_045.BufferName)
                                    rMsg.toLocation = ConveyorDef.Box.B1_012.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.Box.B1_092.BufferName;
                            }
                        }
                    }
                    else
                        rMsg.toLocation = "GO";
                }
                else if (cmd.Cmd_Mode == CmdMode.S2S)
                {
                    if (ConveyorDef.GetLifetNode_List().Any(r => r.BufferName == Body.location))
                    {
                        //Lift4C只有兩樓交換
                        if(Body.location == ConveyorDef.E04.LO1_02.BufferName)
                        {
                            rMsg.toLocation = ConveyorDef.AGV.LO2_04.BufferName;
                        }
                        else if (Body.location == "LO2-02")
                        {
                            rMsg.toLocation = ConveyorDef.E04.LO1_07.BufferName;
                        }
                        else //Lift5C
                        {
                            if (ConveyorDef.GetSharingNode3F().Any(r => r.end.BufferName == cmd.New_Loc || r.start.BufferName == cmd.New_Loc) ||
                                ConveyorDef.GetNode_3F().Any(r => r.BufferName == cmd.New_Loc))
                                rMsg.toLocation = ConveyorDef.AGV.LO4_04.BufferName;
                            else if (ConveyorDef.GetSharingNode5F().Any(r => r.end.BufferName == cmd.New_Loc || r.start.BufferName == cmd.New_Loc) ||
                                    ConveyorDef.GetNode_5F().Any(r => r.BufferName == cmd.New_Loc))
                                rMsg.toLocation = ConveyorDef.AGV.LO5_04.BufferName;
                            else if (ConveyorDef.GetSharingNode6F().Any(r => r.end.BufferName == cmd.New_Loc || r.start.BufferName == cmd.New_Loc) || 
                                    ConveyorDef.GetNode_6F().Any(r => r.BufferName == cmd.New_Loc))
                                rMsg.toLocation = ConveyorDef.AGV.LO6_04.BufferName;
                            else
                                rMsg.toLocation = ConveyorDef.AGV.LO3_01.BufferName;
                        }
                    }
                    else if ((cmd.New_Loc != ConveyorDef.Box.B1_062.BufferName && cmd.New_Loc != ConveyorDef.Box.B1_067.BufferName &&
                            cmd.New_Loc != ConveyorDef.Box.B1_142.BufferName && cmd.New_Loc != ConveyorDef.Box.B1_147.BufferName) &&
                            !ConveyorDef.GetLifetNode_List().Where(r => r.BufferName == Body.location).Any())
                    {
                        if (cmd.boxStockOutAgv == "")
                        {
                            cmd.boxStockOutAgv = ConveyorDef.GetB800CVOut().BufferName;
                            if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateboxStockOutAgv(Body.jobId, cmd.boxStockOutAgv))
                                rMsg.toLocation = cmd.boxStockOutAgv;
                            else
                                throw new Exception($"Error: 更新boxStockOutAgv失敗, jobId = {Body.jobId}.");
                        }
                        else
                        {
                            rMsg.toLocation = cmd.boxStockOutAgv;
                        }
                    }
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DESTINATION_CHECK record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DESTINATION_CHECK> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/CMD_DOUBLE_STORAGE_REQUEST")]
        [HttpPost]
        public IHttpActionResult CMD_DOUBLE_STORAGE_REQUEST([FromBody] CmdDoubleStorageInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DOUBLE_STORAGE_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            CmdDoubleStorageReply rMsg = new CmdDoubleStorageReply
            {
                reelId = Body.reelId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DOUBLE_STORAGE_REQUEST start!");
            try
            {

                EmptyShelfQueryInfo info = new EmptyShelfQueryInfo
                {
                    lotIdCarrierId = Body.reelId
                };
                switch (Body.locationId.Substring(0, 1))
                {
                    case "A":
                        info.craneId = clsConstValue.WesApi.CraneId.E801;
                        break;
                    case "B":
                        info.craneId = clsConstValue.WesApi.CraneId.E802;
                        break;
                    case "C":
                        info.craneId = clsConstValue.WesApi.CraneId.E803;
                        break;
                    case "D":
                        info.craneId = clsConstValue.WesApi.CraneId.E804;
                        break;
                    case "E":
                        info.craneId = clsConstValue.WesApi.CraneId.E805;
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(Body.locationId), "Error: E800 儲位ID不合格式.");
                }

                EmptyShelfQueryReply reply = new EmptyShelfQueryReply();
                if (!clsAPI.GetAPI().GetEmptyShelfQuery().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: EmptyShelfQuery to WES fail, jobId = {Body.jobId}.");

                LotShelfReportInfo info_2 = new LotShelfReportInfo
                {
                    shelfId = reply.shelfId,
                    shelfStatus = "IN",
                    lotId = reply.lotIdCarrierId,
                    disableLocation = Body.doubleStorage == "Y" ? clsConstValue.YesNo.Yes : clsConstValue.YesNo.No
                };

                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunShelfReportToWes(Body.jobId, info_2, ref strEM))
                    throw new Exception(strEM);

                rMsg.newLoc = reply.shelfId;
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DOUBLE_STORAGE_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DOUBLE_STORAGE_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/COMMAND_COMPLETE")]
        [HttpPost]
        public IHttpActionResult COMMAND_COMPLETE([FromBody] CommandCompleteInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<COMMAND_COMPLETE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>COMMAND_COMPLETE start!");
            try
            {
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetProc().FunE800CommandComplete(Body.jobId, Body.cmdMode, Body.emptyRetrieval, Body.portId, Body.carrierId, clsAPI.GetWesApiConfig().IP, ref strEM))
                    throw new Exception(strEM);
                #region 修正
                //if (Body.cmdMode == clsConstValue.CmdMode.StockIn)
                //{
                //    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                //        throw new Exception($"Error: Update CmdSts fail, jobId = {Body.jobId}.");

                //    CmdMstInfo cmd = new CmdMstInfo();
                //    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                //        throw new Exception($"Error: 取得cmdMst命令失敗, jobId = {Body.jobId}.");

                //    LotPutawayCompleteInfo info = new LotPutawayCompleteInfo
                //    {
                //        jobId = cmd.JobID,
                //        lotId = cmd.BoxID,
                //        shelfId = cmd.Loc,
                //        isComplete = clsConstValue.YesNo.Yes
                //    };
                //    if (!clsAPI.GetAPI().GetLotPutawayComplete().FunReport(info, clsAPI.GetWesApiConfig().IP))
                //        throw new Exception($"Error: LotPutawayComplete to WES fail, jobId = {Body.jobId}.");
                //}
                //if (Body.cmdMode == clsConstValue.CmdMode.StockOut)
                //{
                //    CmdMstInfo cmd = new CmdMstInfo();
                //    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                //        throw new Exception($"Error: 取得cmdMst命令失敗, jobId = {Body.jobId}.");

                //    LotRetrieveCompleteInfo info = new LotRetrieveCompleteInfo
                //    {
                //        jobId = cmd.JobID,
                //        lotId = cmd.BoxID,
                //        portId = Body.portId,
                //        carrierId = Body.carrierId
                //    };

                //    if (Body.emptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.Normal)
                //    {
                //        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                //            throw new Exception($"Error: Update CmdSts fail, jobId = {Body.jobId}.");
                //        info.isComplete = clsConstValue.YesNo.Yes;
                //        info.disableLocation = "N";
                //    }
                //    else if (Body.emptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.EmptyRetrieve)
                //    {
                //        string sRemark = "異常：空出庫";
                //        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Cancel_Wait,
                //            clsEnum.Cmd_Abnormal.E2, sRemark))
                //            throw new Exception($"Error: Update cmdSts【空出庫】 fail, jobId = {Body.jobId}.");
                //        info.isComplete = clsConstValue.YesNo.No;
                //        info.emptyTransfer = clsConstValue.YesNo.Yes;
                //        info.disableLocation = clsConstValue.YesNo.Yes;
                //    }
                //    else if (Body.emptyRetrieval == clsConstValue.WesApi.EmptyRetrieval.RetrieveFail)
                //    {
                //        string sRemark = "異常：料捲取出失敗";
                //        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Cancel_Wait,
                //            clsEnum.Cmd_Abnormal.EP, sRemark))
                //            throw new Exception($"Error: Update cmdSts【料捲取出失敗】 fail, jobId = {Body.jobId}.");

                //        info.isComplete = clsConstValue.YesNo.No;
                //        info.disableLocation = clsConstValue.YesNo.Yes;
                //    }
                //    else
                //    {
                //        throw new Exception($"Error: emptyRetrieval 格式不合, jobId = {Body.jobId}.");
                //    }

                //    if (!clsAPI.GetAPI().GetLotRetrieveComplete().FunReport(info, clsAPI.GetWesApiConfig().IP))
                //        throw new Exception($"Error: LotRetrieveComplete to WES fail, jobId = {Body.jobId}.");
                //}
                #endregion 修正


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>COMMAND_COMPLETE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<COMMAND_COMPLETE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //未撰寫
        [Route("WCS/CONTROL_CHANGE")]
        [HttpPost]
        public IHttpActionResult CONTROL_CHANGE([FromBody] ControlChangeInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CONTROL_CHANGE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CONTROL_CHANGE start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CONTROL_CHANGE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CONTROL_CHANGE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/EMPTY_BIN_LOAD_REQUEST")]
        [HttpPost]
        public IHttpActionResult EMPTY_BIN_LOAD_REQUEST([FromBody] EmptyBinLoadRequestInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<EMPTY_BIN_LOAD_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>EMPTY_BIN_LOAD_REQUEST start!");
            try
            {
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string strEM = "";
                int MaxNumberOfcmd;

                //是否要將Bcr改至撿料口，待討論//後不移動
                if (Body.location == "B1-061")
                    con = ConveyorDef.Box.B1_062;
                else if (Body.location == "B1-066")
                    con = ConveyorDef.Box.B1_067;

                if(Body.location == ConveyorDef.Line.A4_10.BufferName || Body.location == ConveyorDef.Line.A4_14.BufferName)
                {
                    MaxNumberOfcmd = 2;

                    if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckMaximumNumberCmdToBuffer(con.BufferName, MaxNumberOfcmd, ref strEM))
                    {
                        EmptyMagazineLoadRequestInfo info = new EmptyMagazineLoadRequestInfo();
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(Body.location).Stn_No;
                        info.jobId = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (!clsAPI.GetAPI().GetEmptyMagazineLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        {
                            throw new Exception($"Error: EmptyMagazineLoadRequest to WES fail, location = {Body.location}.");
                        }
                    }
                    else
                    {
                        if(!strEM.Contains("提醒"))
                            throw new Exception(strEM);
                    }
                    
                }
                else
                {
                    if (con.ControllerID == clsControllerID.e04_ControllerID)
                        MaxNumberOfcmd = 4;
                    else MaxNumberOfcmd = 2;

                    if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckMaximumNumberCmdToBuffer(con.BufferName, MaxNumberOfcmd, ref strEM))
                    {
                        EmptyESDCarrierLoadRequestInfo info = new EmptyESDCarrierLoadRequestInfo
                        {
                            location = con.StnNo,
                            reqQty = Body.reqQty,
                            withClapBoard = clsConstValue.YesNo.No
                        };
                        if (Body.location.Contains("B1"))
                            info.jobId = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if (ConveyorDef.GetSharingNode().Any(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName))
                        {
                            info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                        if (!clsAPI.GetAPI().GetEmptyESDCarrierLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: Send Empty ESDCarrier Load Request to WES fail, location = {Body.location}.");
                    }
                    else
                    {
                        if (!strEM.Contains("提醒"))
                            throw new Exception(strEM);
                    }
                }
                

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                if (strEM.Contains("提醒"))
                    rMsg.returnComment = strEM;

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>EMPTY_BIN_LOAD_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<EMPTY_BIN_LOAD_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //未撰寫
        [Route("WCS/FORK_STATUS_REPORT")]
        [HttpPost]
        public IHttpActionResult FORK_STATUS_REPORT([FromBody] ForkStatusReportInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<FORK_STATUS_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>FORK_STATUS_REPORT start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>FORK_STATUS_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<FORK_STATUS_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        //未撰寫
        [Route("WCS/LOCATION_DISABLE_REQUEST")]
        [HttpPost]
        public IHttpActionResult LOCATION_DISABLE_REQUEST([FromBody] LocationDisableRequestInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOCATION_DISABLE_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOCATION_DISABLE_REQUEST start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOCATION_DISABLE_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOCATION_DISABLE_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/LOT_NG_REPORT")]
        [HttpPost]
        public IHttpActionResult LOT_NG_REPORT([FromBody] LotNGReportInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_NG_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_NG_REPORT start!");
            try
            {
                NGPositionReportInfo info = new NGPositionReportInfo
                {
                    lotId = Body.reelId,
                    ngLocation = Body.locationId
                };

                if (clsAPI.GetAPI().GetNGPositionReport().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: 傳送WES NGPositionReport 失敗, lotId = {info.lotId}.");


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_NG_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_NG_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }
        
        [Route("WCS/LOT_RETRIEVE_CANCEL")]
        [HttpPost]
        public IHttpActionResult LOT_RETRIEVE_CANCEL([FromBody] ReelIdInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_CANCEL> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_RETRIEVE_CANCEL start!");
            try
            {
                string strEM = "";
                CmdMstInfo cmd = new CmdMstInfo();
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand_byBoxID(Body.reelId, ref cmd) != DBResult.Success)
                    throw new Exception($"Error: E800 LotRetrieveCancel 取得cmdmst命令失敗! lotId = {Body.reelId}.");
                if (!clsDB_Proc.GetDB_Object().GetLotRetrieveNG().FunRetrieveNG_Occur(cmd.Cmd_Sno, cmd.JobID, Body.reelId, ref strEM))
                    throw new Exception(strEM);


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_RETRIEVE_CANCEL record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_CANCEL> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/MODE_CHANGE")]
        [HttpPost]
        public IHttpActionResult MODE_CHANGE([FromBody] ModeChangeInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<MODE_CHANGE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>MODE_CHANGE start!");
            try
            {
                //正在撰寫
                //mode == 1正常，mode ==2異常，mode == 3盤點
                //PCBA以M1-05, M1-10, M1-15, M1-20分別代表四條線

                EQPStatusUpdateInfo EQPinfo = new EQPStatusUpdateInfo
                {
                    craneId = Body.craneId,
                    craneStatus = "1",
                };

                //初始Conveyor點
                ConveyorInfo outAGV = new ConveyorInfo();
                ConveyorInfo outCV = new ConveyorInfo();
                ConveyorInfo inCV = new ConveyorInfo();
                ConveyorInfo inAGV = new ConveyorInfo();
                switch (Body.craneId)
                {
                    case clsConstValue.WesApi.CraneId.M801:
                        outAGV = ConveyorDef.AGV.M1_15;
                        outCV = ConveyorDef.PCBA.M1_11;
                        inCV = ConveyorDef.PCBA.M1_16;
                        inAGV = ConveyorDef.AGV.M1_20;
                        break;
                    case clsConstValue.WesApi.CraneId.M802:
                        outAGV = ConveyorDef.AGV.M1_05;
                        outCV = ConveyorDef.PCBA.M1_01;
                        inCV = ConveyorDef.PCBA.M1_06;
                        inAGV = ConveyorDef.AGV.M1_10;
                        break;
                    default:
                        throw new Exception($"Error: 傳遞craneId格式有誤。 craneId = {Body.craneId}");
                }

                //初始路徑啟用/禁用狀況
                bool normalOut = false; bool normalIn = false;  bool abnormalOut = false; bool abnormalIn = false;

                if (Body.outPortMode == clsConstValue.ControllerApi.M800Mode.Normal)
                {
                    if (Body.inPortMode == clsConstValue.ControllerApi.M800Mode.Normal)
                    {
                        normalOut = true;
                        normalIn = true;
                        switch(Body.craneId)
                        {
                            case clsConstValue.WesApi.CraneId.M801:
                                ConveyorDef.AGV.M1_15.StnNo = "M800-3";
                                ConveyorDef.AGV.M1_20.StnNo = "M800-1";
                                break;
                            case clsConstValue.WesApi.CraneId.M802:
                                ConveyorDef.AGV.M1_05.StnNo = "M800-4";
                                ConveyorDef.AGV.M1_10.StnNo = "M800-2";
                                break;
                        }
                    }
                    else if (Body.inPortMode == clsConstValue.ControllerApi.M800Mode.Malfunction)
                    {
                        abnormalIn = true;
                        normalOut = true;
                        switch (Body.craneId)
                        {
                            case clsConstValue.WesApi.CraneId.M801:
                                ConveyorDef.AGV.M1_15.StnNo = "M800-1";
                                ConveyorDef.AGV.M1_20.StnNo = "M800-1_Malfunction";
                                break;
                            case clsConstValue.WesApi.CraneId.M802:
                                ConveyorDef.AGV.M1_05.StnNo = "M800-2";
                                ConveyorDef.AGV.M1_10.StnNo = "M800-2_Malfunction";
                                break;
                        }
                    }
                }
                else if (Body.outPortMode == clsConstValue.ControllerApi.M800Mode.Malfunction)
                {
                    if (Body.inPortMode == clsConstValue.ControllerApi.M800Mode.Normal)
                    {
                        abnormalOut = true;
                        normalIn = true;
                        switch (Body.craneId)
                        {
                            case clsConstValue.WesApi.CraneId.M801:
                                ConveyorDef.AGV.M1_15.StnNo = "M800-3_Malfunction";
                                ConveyorDef.AGV.M1_20.StnNo = "M800-1";
                                break;
                            case clsConstValue.WesApi.CraneId.M802:
                                ConveyorDef.AGV.M1_05.StnNo = "M800-4_Malfunction";
                                ConveyorDef.AGV.M1_10.StnNo = "M800-2";
                                break;
                        }
                    }
                    else if (Body.inPortMode == clsConstValue.ControllerApi.M800Mode.Malfunction)
                    {
                        EQPinfo.craneId = "0";
                    }
                }
                else if (Body.outPortMode == clsConstValue.ControllerApi.M800Mode.Cycle && Body.inPortMode == clsConstValue.ControllerApi.M800Mode.Cycle)
                {

                }
                else
                    throw new Exception($"Error: 傳送mode格式有誤, inPortMode = {Body.inPortMode} and outPortMode = {Body.outPortMode}.");

                //禁用/啟用路徑點位初始
                Location OutCV = clsMapController.GetMapHost().GetLocation(outCV.DeviceId, outCV.BufferName);
                Location OutAGV = clsMapController.GetMapHost().GetLocation(outAGV.DeviceId, outAGV.BufferName);
                Location InCV = clsMapController.GetMapHost().GetLocation(inCV.DeviceId, inCV.BufferName);
                Location InAGV = clsMapController.GetMapHost().GetLocation(inAGV.DeviceId, inAGV.BufferName);
                Location leftFork = clsMapController.GetMapHost().GetLocation(inCV.DeviceId, Location.LocationID.LeftFork.ToString());
                Location shelf = clsMapController.GetMapHost().GetLocation(inCV.DeviceId, LocationTypes.Shelf.ToString());

                //正常出庫
                if (!clsMapController.GetMapHost().EnablePath(leftFork, OutCV, normalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{leftFork.LocationId}->{OutCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(shelf, OutCV, normalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{shelf.LocationId}->{OutCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(OutCV, OutAGV, normalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{OutCV.LocationId}->{OutAGV.LocationId} fail.");

                //異常出庫
                if (!clsMapController.GetMapHost().EnablePath(leftFork, InCV, abnormalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{leftFork.LocationId}->{InCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(shelf, InCV, abnormalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{shelf.LocationId}->{InCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(InCV, InAGV, abnormalOut))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{InCV.LocationId}->{InAGV.LocationId} fail.");

                //正常入庫
                if (!clsMapController.GetMapHost().EnablePath(InAGV, InCV, normalIn))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{InAGV.LocationId}->{InCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(InCV, shelf, normalIn))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{InCV.LocationId}->{shelf.LocationId} fail.");

                //異常入庫
                if (!clsMapController.GetMapHost().EnablePath(OutAGV, OutCV, abnormalIn))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{OutAGV.LocationId}->{OutCV.LocationId} fail.");
                if (!clsMapController.GetMapHost().EnablePath(OutCV, shelf, abnormalIn))
                    clsWriLog.Log.FunWriLog(clsLog.Type.Error, $"更新PCBA路徑失敗：禁用路徑{OutCV.LocationId}->{shelf.LocationId} fail.");

                //上報WES Crane是否可以入出庫
                if (!clsAPI.GetAPI().GetEQPStatusUpdate().FunReport(EQPinfo, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: 上報WES EQP Status Update失敗, CraneId = {EQPinfo.craneId} and CraneStatus = {EQPinfo.craneStatus}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(clsLog.Type.Trace, $"<{Body.jobId}>MODE_CHANGE record end!");
                clsWriLog.Log.FunWriLog(clsLog.Type.Trace, $"<MODE_CHANGE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/POSITION_REPORT")]
        [HttpPost]
        public IHttpActionResult POSITION_REPORT([FromBody] U2NMMA30.Models.PositionReportInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<POSITION_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>POSITION_REPORT start!");
            try
            {
                CmdMstInfo cmd = new CmdMstInfo();
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                    throw new Exception($"Error: Get CmdMst fail, jobId = {Body.jobId}.");


                ConveyorInfo con = new ConveyorInfo();

                //箱式倉減料口定位設定
                switch (Body.position)
                {
                    case "B1-061":
                        con = ConveyorDef.Box.B1_062;
                        break;
                    case "B1-066":
                        con = ConveyorDef.Box.B1_067;
                        break;
                    case "B1-141":
                        con = ConveyorDef.Box.B1_142;
                        break;
                    case "B1-146":
                        con = ConveyorDef.Box.B1_147;
                        break;
                    case "CV":
                        if(cmd.carrierType == clsConstValue.WesApi.CarrierType.Lot)
                            con = ConveyorDef.Tower.E1_10;
                        else
                            con = ConveyorDef.GetBuffer(Body.position);
                        break;
                    default:
                        con = ConveyorDef.GetBuffer(Body.position);
                        break;
                }

                string deviceId = con.DeviceId != "" ? con.DeviceId : con.ControllerID;
                
                //貨物到電梯內 或 AGV上
                if(Body.position.Contains("LI2"))
                {
                    con.BufferName = "LIFT5";
                    deviceId = con.BufferName+"C";
                }
                else if(Body.position.Contains("LI1"))
                {
                    con.BufferName = "LIFT4";
                    deviceId = con.BufferName+"C";
                }
                else if(Body.position == "AGV")
                {
                    con.BufferName = Body.position;
                    deviceId = "AGV";
                }


                int cmdSno = Convert.ToInt32(cmd.Cmd_Sno);
                if (cmdSno > 20000)
                {
                    //若為WCS自行生成命令，不上報WES
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>POSITION_REPORT 此為WCS自行生成命令，不上報WES!");
                }
                else if (Body.carrierType == clsConstValue.ControllerApi.CarrierType.Lot)
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, ConveyorDef.DeviceID_Tower, Body.position))
                        throw new Exception($"Error: Update CmdMst fail, jobId = {Body.jobId}.");
                    
                    LotPositionReportInfo info = new LotPositionReportInfo
                    {
                        jobId = cmd.JobID,
                        lotId = Body.id,
                        location = con.StnNo != null ? con.StnNo : con.BufferName
                    };

                    //於E1-10上即將入庫，位置使用"CV"上報WES
                    if (Body.position == "CV")
                        info.location = "CV";

                    if (!clsAPI.GetAPI().GetLotPositionReport().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: LotPositionReport to WES fail, jobId = {Body.jobId}.");
                }
                else if (Body.carrierType == clsConstValue.ControllerApi.CarrierType.Rack ||
                        Body.carrierType == clsConstValue.ControllerApi.CarrierType.Bin ||
                        Body.carrierType == clsConstValue.ControllerApi.CarrierType.Mag)
                {
                    V2BYMA30.ReportInfo.PositionReportInfo info = new V2BYMA30.ReportInfo.PositionReportInfo
                    {
                        jobId = cmd.JobID,
                        carrierId = cmd.BoxID,
                        location = con.StnNo == null ? con.BufferName : con.StnNo
                    };

                    if(clsAPI.GetAPI().GetPositionReport().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    {
                        //不論WES是否PositionReport成功
                        //throw new Exception($"Error: PositionReport to WES fail, jobId = {Body.jobId}.");
                    }
                }

                MiddleCmd middle = new MiddleCmd();

                if(cmd.Cmd_Mode == clsConstValue.CmdMode.L2L || cmd.Cmd_Mode == clsConstValue.CmdMode.StockIn && cmd.Stn_No == Body.position)
                {
                    //新生成的命令，待時序處理起點預約
                }
                else if (
                         (
                         (cmd.Cmd_Mode == clsConstValue.CmdMode.StockOut && Body.position == cmd.Stn_No) || 
                         (cmd.Cmd_Mode == clsConstValue.CmdMode.S2S && Body.position == cmd.New_Loc)
                         ) && 
                         ConveyorDef.GetAGV_8FPort().Any(r => r.BufferName == Body.position) &&
                         clsDB_Proc.GetDB_Object().GetMiddleCmd().FunGetMiddleCmdbyCommandID(Body.jobId, ref middle))
                {
                    //若為出庫or站對站命令終點 & 終點為AGVport口 & Middle 尚有該筆AGV命令未完成，則待AGV命令完成再更新cmdMst
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>POSITION_REPORT 該命令為AGV搬運終點，待AGV上報搬運完成後更新CurLocation！");
                }
                else
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName))
                        throw new Exception($"Error: Update CurLoc fail, jobId = {Body.jobId}.");
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>POSITION_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<POSITION_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/RACK_AWAY_INFO")]
        [HttpPost]
        public IHttpActionResult RACK_AWAY_INFO([FromBody] RackInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_AWAY_INFO> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_AWAY_INFO start!");
            try
            {
                string strEM = "";

                CmdMstInfo ccmd = new CmdMstInfo();
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref ccmd))
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                    {
                        strEM = $"Error: Update CmdMst cmdsts fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }
                else
                {
                    if (Body.rackId.Contains("UNKNOWN") )
                    {
                        if (Body.stagePosition == ConveyorDef.AGV.S0_05.BufferName)
                        {
                            RackRequestInfo info = new RackRequestInfo
                            {
                                location = Body.stagePosition
                            };

                            if (!clsAPI.GetAPI().GetRackRequest().FunReport(info, clsAPI.GetAgvcApiConfig().IP))
                            {
                                strEM = $"Error: Call AGV RackRequest fail, jobId = {Body.jobId}.";
                                throw new Exception(strEM);
                            }
                        }
                        else
                        {
                            CmdMstInfo cmd = new CmdMstInfo();
                            cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                            if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                            {
                                throw new Exception($"<{Body.jobId}>取得序號失敗！");
                            }

                            cmd.BoxID = Body.rackId;
                            cmd.Cmd_Mode = clsConstValue.CmdMode.S2S;
                            cmd.CurDeviceID = "";
                            cmd.CurLoc = "";
                            cmd.End_Date = "";

                            cmd.Loc = "";
                            cmd.Equ_No = "";

                            cmd.EXP_Date = "";
                            cmd.JobID = Body.jobId;
                            cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                            cmd.New_Loc = ConveyorDef.AGV.S0_05.BufferName;

                            cmd.Prty = "5";
                            cmd.Remark = "";
                            cmd.Stn_No = Body.stagePosition;
                            cmd.Host_Name = "WCS";
                            cmd.Zone_ID = "";
                            cmd.carrierType = "";

                            if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                                throw new Exception(strEM);
                        }
                    }
                    else
                    {
                        ConveyorInfo con = new ConveyorInfo();
                        con = ConveyorDef.GetBuffer(Body.stagePosition);
                        string deviceId = tool.GetDeviceId(Body.stagePosition);

                        CmdMstInfo cmd = new CmdMstInfo();
                        if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                        {
                            if (clsDB_Proc.GetDB_Object().GetMiddleCmd().CheckHasMiddleCmdbyCSTID(Body.rackId))
                            {
                                string sRemark = "命令完成";
                                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateCmdSts(Body.jobId, clsConstValue.CmdSts_MiddleCmd.strCmd_Finish_Wait, sRemark))
                                {
                                    strEM = $"Error: Update Middle cmdsts fail, jobId = {Body.jobId}.";
                                    throw new Exception(strEM);
                                }
                            }
                            else
                            {
                                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, deviceId, Body.stagePosition))
                                {
                                    strEM = $"Error: Update CmdMst curLoc fail, jobId = {Body.jobId}.";
                                    throw new Exception(strEM);
                                }
                            }
                        }
                        else
                        {
                            CarrierReturnNextInfo info = new CarrierReturnNextInfo
                            {
                                carrierId = Body.rackId,
                                fromLocation = con.StnNo,
                                carrierType = clsConstValue.WesApi.CarrierType.Rack
                            };
                            CarrierReturnNextReply reply = new CarrierReturnNextReply();

                            if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                            {
                                strEM = $"Error: CarrierRetrurnNext fail, jobid = {Body.jobId}.";
                                throw new Exception(strEM);
                            }
                        }
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_AWAY_INFO record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_AWAY_INFO> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/RACK_COMMAND_DONE")]
        [HttpPost]
        public IHttpActionResult RACK_COMMAND_DONE([FromBody] RackInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_COMMAND_DONE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_COMMAND_DONE start!");
            try
            {
                string strEM = "";
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.stagePosition);

                CmdMstInfo cmd = new CmdMstInfo();
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                    {
                        strEM = $"Error: Update CmdMst cmdsts fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }
                else
                {
                    CarrierReturnNextInfo info = new CarrierReturnNextInfo
                    {
                        carrierId = Body.rackId,
                        fromLocation = con.StnNo,
                        carrierType = clsConstValue.WesApi.CarrierType.Rack
                    };
                    CarrierReturnNextReply reply = new CarrierReturnNextReply();

                    if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                    {
                        strEM = $"Error: CarrierRetrurnNext fail, jobid = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_COMMAND_DONE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_COMMAND_DONE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/RACK_ID_REPORT")]
        [HttpPost]
        public IHttpActionResult RACK_ID_REPORT([FromBody] RackIdInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_ID_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_ID_REPORT start!");
            try
            {
                string strEM = "";
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.rackLoc);
                string deviceId = tool.GetDeviceId(Body.rackLoc);

                CmdMstInfo cmd = new CmdMstInfo();
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand_byBoxID(Body.rackId, ref cmd) == DBResult.Success && 
                    (cmd.Cmd_Mode == clsConstValue.CmdMode.S2S && Body.rackLoc != cmd.New_Loc))
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName))
                    {
                        strEM = $"Error: Update CurLoc fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
                    }

                }
                else
                {
                    BufferStatusQueryInfo bufferStatusQueryInfo = new BufferStatusQueryInfo
                    {
                        bufferId = Body.rackLoc
                    };
                    BufferStatusReply bufferStatusReply = new BufferStatusReply();

                    if(!clsAPI.GetAPI().GetBufferStatusQuery().FunReport(bufferStatusQueryInfo, con.API.IP, ref bufferStatusReply))
                    {
                        throw new Exception($"Error: BufferStatusQuery for {con.BufferName} fail, URL = {con.API.IP}/BUFFER_STATUS_QUERY");
                    }

                    if(bufferStatusReply.isEmpty == clsConstValue.YesNo.Yes)
                    {
                        //上報WES空料架需離開線邊倉
                        RemoveRackShowInfo removeRackShowInfo = new RemoveRackShowInfo
                        {
                            carrierId = Body.rackId,
                            location = con.StnNo
                        };
                        clsAPI.GetAPI().GetRemoveRackShow().FunReport(removeRackShowInfo, clsAPI.GetWesApiConfig().IP);

                        //詢問電子料塔是否有空閒站口
                        bool getEmptyStation = false;
                        for (int i = 0; i < 3; i ++)
                        {
                            switch(i)
                            {
                                case 0:
                                    bufferStatusQueryInfo.bufferId = ConveyorDef.AGV.E2_35.BufferName;
                                    break;
                                case 1:
                                    bufferStatusQueryInfo.bufferId = ConveyorDef.AGV.E2_36.BufferName;
                                    break;
                                case 2:
                                    bufferStatusQueryInfo.bufferId = ConveyorDef.AGV.E2_37.BufferName;
                                    break;
                            }
                            bufferStatusReply = new BufferStatusReply();
                            if (!clsAPI.GetAPI().GetBufferStatusQuery().FunReport(bufferStatusQueryInfo, clsAPI.GetTowerApiConfig().IP, ref bufferStatusReply))
                            {
                                throw new Exception($"Error: BufferStatusQuery for {bufferStatusQueryInfo.bufferId} fail. 空料架詢問電子料塔是否可接收空料架流程失敗.");
                            }

                            if(bufferStatusReply.ready == ((int)clsEnum.Ready.OUT).ToString())
                            {
                                getEmptyStation = true;
                                cmd = new CmdMstInfo();

                                cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                                if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                                {
                                    throw new Exception($"<{Body.jobId}>取得序號失敗！");
                                }

                                cmd.BoxID = Body.rackId;
                                cmd.Cmd_Mode = clsConstValue.CmdMode.S2S;
                                cmd.CurDeviceID = "";
                                cmd.CurLoc = "";
                                cmd.End_Date = "";
                                cmd.Loc = "";
                                cmd.Equ_No = "";
                                cmd.EXP_Date = "";
                                cmd.JobID = Body.jobId;
                                cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                                cmd.New_Loc = bufferStatusReply.bufferId;

                                cmd.Prty = "5";
                                cmd.Remark = "";

                                cmd.Stn_No = Body.rackLoc;

                                cmd.Host_Name = "WCS";
                                cmd.Zone_ID = "";
                                cmd.carrierType = clsConstValue.ControllerApi.CarrierType.Rack;

                                con = ConveyorDef.GetBuffer(bufferStatusReply.bufferId);

                                if (!clsDB_Proc.GetDB_Object().GetProc().FunSendEmptyRackToTower(con, cmd, ref strEM))
                                    throw new Exception(strEM);

                                break;

                            }
                        }
                        if(!getEmptyStation)
                        {
                            RackLeavingNGInfo rackLeavingNG = new RackLeavingNGInfo
                            {
                                bufferId = Body.rackLoc
                            };
                            if(!clsAPI.GetAPI().GetRackLeavingNG().FunReport(rackLeavingNG, con.API.IP))
                                throw new Exception($"Error: 傳送RackLeavingNG至{rackLeavingNG.bufferId}失敗, IP: {con.API.IP}/RACK_LEAVING_NG.");
                        }
                    }
                    else
                    {
                        CarrierReturnNextInfo info = new CarrierReturnNextInfo
                        {
                            carrierId = Body.rackId,
                            fromLocation = con.StnNo,
                            isEmpty = "N",
                            carrierType = clsConstValue.WesApi.CarrierType.Rack
                        };
                        CarrierReturnNextReply reply = new CarrierReturnNextReply();

                        if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                        {
                            strEM = $"Error: CarrierRetrurnNext fail to WES, jobid = {Body.jobId}.";
                            throw new Exception(strEM);
                        }
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_ID_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_ID_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/RACK_RECEIVED_INFO")]
        [HttpPost]
        public IHttpActionResult RACK_RECEIVED_INFO([FromBody] RackInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_RECEIVED_INFO> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_RECEIVED_INFO start!");
            try
            {
                string strEM = "";
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.stagePosition);

                CmdMstInfo cmd = new CmdMstInfo();

                int iRet = clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckHasCommand(Body.stagePosition, ref cmd);
                if (iRet == DBResult.NoDataSelect)
                {
                    PortStatusUpdateInfo info = new PortStatusUpdateInfo
                    {
                        portId = con.StnNo,
                        portStatus = ((int)clsEnum.WmsApi.portStatus.Pickup_Ready).ToString()
                    };

                    if (con.StnNo == ConveyorDef.AGV.E2_38.StnNo || con.StnNo == ConveyorDef.AGV.E2_39.StnNo)
                        info.portId = "E800-8";
                    
                    if (!clsAPI.GetAPI().GetPortStatusUpdate().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    {
                        strEM = $"Error: Port Status Update fail, jobId = {Body.jobId}";
                        throw new Exception(strEM);
                    }
                }
                else if (iRet == DBResult.Success)
                {
                    strEM = $"Error: 該站口已存在命令, jobId = {Body.jobId}.";
                    throw new Exception(strEM);
                }
                else
                {
                    strEM = $"Error: 該站口異常, jobId = {Body.jobId}.";
                    throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_RECEIVED_INFO record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_RECEIVED_INFO> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/RACK_TURN_REQUEST")]
        [HttpPost]
        public IHttpActionResult RACK_TURN_REQUEST([FromBody] RackInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_TURN_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_TURN_REQUEST start!");
            try
            {
                RackTurnInfo info = new RackTurnInfo
                {
                    jobId = Body.jobId,
                    location = Body.stagePosition,
                    rackId = Body.rackId
                };

                MiddleCmd middlecmd = new MiddleCmd();
                middlecmd.DeviceID = ConveyorDef.DeviceID_AGV;
                middlecmd.CommandID = Body.jobId;
                middlecmd.TaskNo = "RackTurnRequest";
                middlecmd.CSTID = Body.rackId;
                middlecmd.CmdSts = CmdSts_MiddleCmd.strCmd_WriteDeviceCmd;
                middlecmd.CmdMode = CmdMode.S2S;
                middlecmd.CrtDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                middlecmd.carrierType = clsConstValue.ControllerApi.CarrierType.Rack;

                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunInsMiddleCmd(middlecmd))
                    throw new Exception($"Error: fail to write RackTurn, jobId = {Body.jobId}.");


                if (!clsAPI.GetAPI().GetRackTurn().FunReport(info, clsAPI.GetAgvcApiConfig().IP))
                    throw new Exception($"Error: RackTurn fail, jobId = {Body.jobId}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_TURN_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_TURN_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/REEL_STOCK_IN")]
        [HttpPost]
        public IHttpActionResult REEL_STOCK_IN([FromBody] ReelIdInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<REEL_STOCK_IN> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReelStockInReply rMsg = new ReelStockInReply
            {
                reelId = Body.reelId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>REEL_STOCK_IN start!");
            try
            {

                LotPutawayCheckInfo info = new LotPutawayCheckInfo
                {
                    jobId = Body.jobId,
                    lotId = Body.reelId,
                    portId = Body.portId
                };

                if (!clsAPI.GetAPI().GetLotPutawayCheck().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: LotPutawayCheck fail, jobId = {Body.jobId}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>REEL_STOCK_IN record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<REEL_STOCK_IN> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/SMTC_EMPTY_MAGAZINE_LOAD_REQUEST")]
        [HttpPost]
        public IHttpActionResult SMTC_EMPTY_MAGAZINE_LOAD_REQUEST([FromBody] SMTCEmptyMagazineLoadRequestInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_LOAD_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_LOAD_REQUEST start!");
            try
            {
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string strEM = "";
                int MaxNumberOfcmd = 3;

                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckMaximumNumberCmdToBuffer(con.BufferName, MaxNumberOfcmd, ref strEM))
                {
                    EmptyMagazineLoadRequestInfo info = new EmptyMagazineLoadRequestInfo
                    {
                        location = con.StnNo
                    };
                    if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                    {
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                    }
                    if (!clsAPI.GetAPI().GetEmptyMagazineLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: 傳送EmptyMagazineLoadRequest 至 WES 失敗, jobId = {info.jobId}");
                }
                else
                {
                    if (!strEM.Contains("提醒"))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                if (strEM.Contains("提醒"))
                    rMsg.returnComment = strEM;

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_LOAD_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_LOAD_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/SMTC_EMPTY_MAGAZINE_UNLOAD")]
        [HttpPost]
        public IHttpActionResult SMTC_EMPTY_MAGAZINE_UNLOAD([FromBody] SMTCEmptyMagazineUnloadInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_UNLOAD> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_UNLOAD start!");
            try
            {
                CmdMstInfo cmd = new CmdMstInfo();
                bool check = false;
                check = clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckHasCommand_ByBoxID(Body.carrierId, ref cmd);
                string strEM;
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string deviceId = con.DeviceId == "" ? con.ControllerID : con.DeviceId;
                if (check)
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName))
                    {
                        strEM = $"Error: Update CmdMst curLoc fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }
                else
                {
                    EmptyMagazineUnloadInfo info = new EmptyMagazineUnloadInfo
                    {
                        carrierId = Body.carrierId,
                        location = con.StnNo
                    };
                    if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                    {
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                    }
                    if (!clsAPI.GetAPI().GetEmptyMagazineUnload().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: 傳送EmptyMagazineUnload 至 WES 失敗, jobId = {info.jobId}");
                }


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_UNLOAD record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_UNLOAD> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/SMTC_MAGAZINE_LOAD_REQUEST")]
        [HttpPost]
        public IHttpActionResult SMTC_MAGAZINE_LOAD_REQUEST([FromBody] SMTCMagazineLoadRequestInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_MAGAZINE_LOAD_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_MAGAZINE_LOAD_REQUEST start!");
            try
            {
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string strEM = "";
                int MaxNumberOfcmd = 3;

                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckMaximumNumberCmdToBuffer(con.BufferName, MaxNumberOfcmd, ref strEM))
                {
                    MagazineLoadRequestInfo info = new MagazineLoadRequestInfo
                    {
                        location = con.StnNo
                    };
                    if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                    {
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                    }

                    if (!clsAPI.GetAPI().GetMagazineLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: 傳送MagazineLoadRequest 至 WES 失敗, jobId = {info.jobId}");
                }
                else
                {
                    if (!strEM.Contains("提醒"))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                if (strEM.Contains("提醒"))
                    rMsg.returnComment = strEM;

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_MAGAZINE_LOAD_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_MAGAZINE_LOAD_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/STAGE_EMPTY_INFO")]
        [HttpPost]
        public IHttpActionResult STAGE_EMPTY_INFO([FromBody] StageEmptyInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STAGE_EMPTY_INFO> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STAGE_EMPTY_INFO start!");
            try
            {
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.stagePosition);
                string strEM = "";
                if (Body.stagePosition == ConveyorDef.AGV.E2_35.BufferName ||
                    Body.stagePosition == ConveyorDef.AGV.E2_36.BufferName ||
                    Body.stagePosition == ConveyorDef.AGV.E2_37.BufferName)
                {
                    PortStatusUpdateInfo info = new PortStatusUpdateInfo
                    {
                        portId = con.StnNo,
                        portStatus = ((int)clsEnum.WmsApi.portStatus.Empty).ToString()
                    };
                    if (!clsAPI.GetAPI().GetPortStatusUpdate().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: PortStatusUpdate to WES fail, jobId = {Body.jobId}.");
                }
                else
                {
                    int MaxNumberOfcmd;
                    if (con.ControllerID == clsControllerID.e04_ControllerID)
                        MaxNumberOfcmd = 4;
                    else MaxNumberOfcmd = 2;

                    if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckMaximumNumberCmdToBuffer(con.BufferName, MaxNumberOfcmd, ref strEM))
                    {
                        EmptyESDCarrierLoadRequestInfo info = new EmptyESDCarrierLoadRequestInfo
                        {
                            location = con.StnNo,
                            reqQty = 1,
                            withClapBoard = clsConstValue.YesNo.Yes
                        };
                        if (ConveyorDef.GetSharingNode().Any(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName))
                        {
                            info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                        if (!clsAPI.GetAPI().GetEmptyESDCarrierLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: EmptyESDCarrierLoadRequest to WES fail, jobId = {Body.jobId}.");
                    }
                    else
                    {
                        if (!strEM.Contains("提醒"))
                            throw new Exception(strEM);
                    }

                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                if(strEM.Contains("提醒"))
                    rMsg.returnComment = strEM;

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STAGE_EMPTY_INFO record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STAGE_EMPTY_INFO> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/STATUS_CHANGE_REPORT")]
        [HttpPost]
        public IHttpActionResult STATUS_CHANGE_REPORT([FromBody] StatusChangeInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STATUS_CHANGE_REPORT> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STATUS_CHANGE_REPORT start!");
            try
            {
                EQPStatusUpdateInfo info = new EQPStatusUpdateInfo
                {
                    jobId = Body.jobId,
                    craneId = $"E80{Body.chipSTKCId}",
                    craneStatus = Body.status == clsConstValue.ControllerApi.RunDown.Run ? "1" : "0"
                };
                if (!clsAPI.GetAPI().GetEQPStatusUpdate().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: EqpStatusUpdate to WES fail, jobId = {Body.jobId}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STATUS_CHANGE_REPORT record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STATUS_CHANGE_REPORT> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/TASK_COMPLETE")]
        [HttpPost]
        public IHttpActionResult TASK_COMPLETE([FromBody] BaseInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TASK_COMPLETE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE start!");
            try
            {

                # region 20230109增加詢問該站口stockBar是否正常下降(貨物定位)_未實裝
                /*
                ConveyorInfo con = new ConveyorInfo();
                MiddleCmd middleCmd = new MiddleCmd();
                if(!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunGetMiddleCmdbyCommandID(Body.jobId, ref middleCmd))
                    throw new Exception($"Error: 取得 middleCmd By CommandID 失敗, jobId = {Body.jobId}.");
                
                con = ConveyorDef.GetBuffer(middleCmd.Destination);
                if(string.IsNullOrEmpty(con.API.IP))
                    throw new Exception($"Error: 取得 AGV命令終點對應Controller IP 失敗, 終點buffer = {middleCmd.Destination}.");

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE 詢問Buffer Stock Bar Status, Buffer = {middleCmd.Destination}.");
               
                BufferStatusQueryInfo statusQueryInfo = new BufferStatusQueryInfo
                {
                    bufferId = middleCmd.Destination
                };
                BufferStatusReply statusReply = new BufferStatusReply();

                if (!clsAPI.GetAPI().GetBufferStatusQuery().FunReport(statusQueryInfo, con.API.IP, ref statusReply))
                    throw new Exception($"Error: 傳送BufferStatusQuery詢問AGV命令終點狀態失敗, jobId = {Body.jobId} and buffer = {con.BufferName}");
                else
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE  成功詢問Buffer Stock Bar Status, Buffer = {middleCmd.Destination}.");

                if (statusReply.stbSts != ((int)clsEnum.StbStatus.Down).ToString())
                    throw new Exception($"Error: AGV命令終點Port口Stock Bar非下降，請稍後再試或聯絡人員處理，jobId = {Body.jobId}, Buffer = {con.BufferName} and stbSts = {statusReply.stbSts}.");
                */

                #endregion
                //AGV命令在middle層
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateFinishByCommanId(Body.jobId, ref strEM))
                    throw new Exception(strEM);

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TASK_COMPLETE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/TRANSFER_COMMAND_DONE")]
        [HttpPost]
        public IHttpActionResult TRANSFER_COMMAND_DONE([FromBody] TransferCommandDoneInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TRANSFER_COMMAND_DONE> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TRANSFER_COMMAND_DONE start!");
            try
            {
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                    throw new Exception($"Error: Update CmdMst_cmdSts fail, jobId = {Body.jobId}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TRANSFER_COMMAND_DONE record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TRANSFER_COMMAND_DONE> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/UNKNOWN_BIN_LEAVE_INFO")]
        [HttpPost]
        public IHttpActionResult UNKNOWN_BIN_LEAVE_INFO([FromBody] UnknownBinLeaveInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<UNKNOWN_BIN_LEAVE_INFO> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>UNKNOWN_BIN_LEAVE_INFO start!");
            try
            {
                string strEM = "";
                bool doubleUnknownBin = false;
                CmdMstInfo cmd = new CmdMstInfo();
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.position);

                //判斷此buffer是否預約過
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand_byBoxID("UNKNOWN_" + Body.position + "_1", ref cmd) == DBResult.Success)
                    doubleUnknownBin = true;

                cmd = new CmdMstInfo();
                cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                {
                    throw new Exception($"<{Body.jobId}>取得序號失敗！");
                }

                cmd.BoxID = "UNKNOWN_" + Body.position;
                cmd.BoxID = doubleUnknownBin ? cmd.BoxID + "_2" : cmd.BoxID + "_1";
                cmd.Cmd_Mode = clsConstValue.CmdMode.S2S;
                cmd.CurDeviceID = "";
                cmd.CurLoc = "";
                cmd.End_Date = "";
                cmd.Loc = "";
                cmd.Equ_No = "";
                cmd.EXP_Date = "";
                cmd.JobID = Body.jobId;
                cmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                //356樓未掃Barcode之空靜電箱離開，命令只到8樓電梯口；其餘空靜電箱離開，命令直接回庫
                if(con.ControllerID == clsControllerID.sMT3C_ControllerID || con.ControllerID == clsControllerID.sMT5C_ControllerID ||
                    con.ControllerID == clsControllerID.sMT6C_ControllerID)
                {
                    cmd.New_Loc = ConveyorDef.E05.LO3_02.BufferName;
                }
                else
                {
                    ConveyorInfo B800CV = new ConveyorInfo();
                    int count = 0; bool check = false;
                    while (count < ConveyorDef.GetB800CV_List().Count())
                    {
                        B800CV = ConveyorDef.GetB800CV();
                        if (clsMiddle.GetMiddle().CheckIsOutReady(B800CV))
                        {
                            cmd.New_Loc = B800CV.BufferName;
                            check = true;
                            break;
                        }
                        count++;
                    }
                    if (!check)
                    {
                        throw new Exception("Error: B800CV 無接收ready站口");
                    }
                }

                cmd.Prty = "5";
                cmd.Remark = "";

                cmd.Stn_No = Body.position;

                cmd.Host_Name = "WCS";
                cmd.Zone_ID = "";
                cmd.carrierType = Body.carrierType == clsConstValue.ControllerApi.CarrierType.Mag ? clsConstValue.WesApi.CarrierType.Mag : clsConstValue.WesApi.CarrierType.Bin;

                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                    throw new Exception(strEM);


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>UNKNOWN_BIN_LEAVE_INFO record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<UNKNOWN_BIN_LEAVE_INFO> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        [Route("WCS/WRONG_EQU_STOCK_IN_REQUEST")]
        [HttpPost]
        public IHttpActionResult WRONG_EQU_STOCK_IN_REQUEST([FromBody] WrongEquStockInInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<WRONG_EQU_STOCK_IN_REQUEST> <WCS Get>\n{JsonConvert.SerializeObject(Body)}");

            WrongEquStockInRequestReply rMsg = new WrongEquStockInRequestReply
            {
                reelId = Body.reelId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>WRONG_EQU_STOCK_IN_REQUEST start!");
            try
            {
                EmptyShelfQueryInfo info = new EmptyShelfQueryInfo
                {
                    lotIdCarrierId = Body.reelId,
                };
                switch (Body.stockerId.Substring(0, 1))
                {
                    case "A":
                        info.craneId = clsConstValue.WesApi.CraneId.E801;
                        break;
                    case "B":
                        info.craneId = clsConstValue.WesApi.CraneId.E802;
                        break;
                    case "C":
                        info.craneId = clsConstValue.WesApi.CraneId.E803;
                        break;
                    case "D":
                        info.craneId = clsConstValue.WesApi.CraneId.E804;
                        break;
                    case "E":
                        info.craneId = clsConstValue.WesApi.CraneId.E805;
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(Body.stockerId), "Error: E800 stocker ID不合格式.");
                }
                EmptyShelfQueryReply reply = new EmptyShelfQueryReply();
                if (!clsAPI.GetAPI().GetEmptyShelfQuery().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: EmptyShelfQuery to WES fail, jobId = {Body.jobId}");
                LotShelfReportInfo info_2 = new LotShelfReportInfo
                {
                    shelfId = reply.shelfId,
                    shelfStatus = "IN",
                    lotId = reply.lotIdCarrierId,
                    disableLocation = clsConstValue.YesNo.No
                };

                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunShelfReportToWes(Body.jobId, info_2, ref strEM))
                    throw new Exception(strEM);

                rMsg.stockerId = Body.stockerId;
                rMsg.locationId = reply.shelfId;
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>WRONG_EQU_STOCK_IN_REQUEST record end!");
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<WRONG_EQU_STOCK_IN_REQUEST> <WCS Reply>\n{JsonConvert.SerializeObject(rMsg)}");
                return Json(rMsg);
            }
            catch (Exception ex)
            {
                rMsg.returnCode = clsConstValue.ApiReturnCode.Fail;
                rMsg.returnComment = ex.Message;

                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return Json(rMsg);
            }
        }

        #endregion

    }
}
