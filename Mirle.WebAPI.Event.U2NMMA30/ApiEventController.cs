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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER start!");
            try
            {
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_PUTAWAY_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_PUTAWAY_TRANSFER start!");
            try
            {
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_RETRIEVE_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_RETRIEVE_TRANSFER start!");
            try
            {
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
                        cmd.Stn_No = Cv_to.start.BufferName;
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.toLocation);
                        cmd.Stn_No = cv_to.BufferName;
                    }
                    


                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Body.carrierType;

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_RETRIEVE_TRANSFER record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_SHELF_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.CarrierReply rMsg = new U2NMMA30.Models.CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_SHELF_TRANSFER start!");
            try
            {
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CARRIER_TRANSFER_CANCEL> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_PUTAWAY_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            U2NMMA30.Models.LotReply rMsg = new U2NMMA30.Models.LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_PUTAWAY_TRANSFER start!");
            try
            {
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
                    }
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";

                    cmd.carrierType = "";
                    cmd.lotSize = Body.lotSize;

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

                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_PUTAWAY_TRANSFER record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                    foreach (var lot in Body.lotList)
                    {
                        LotListInfo oklot = new LotListInfo();
                        cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
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
                            cmd.Stn_No = Cv_to.start.BufferName;
                        }
                        else
                        {
                            var cv_to = ConveyorDef.GetBuffer_ByStnNo(lot.toPortId);
                            cmd.Stn_No = cv_to.BufferName;
                        }
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
                }


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_RETRIEVE_TRANSFER record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_SHELF_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_TRANSFER_CANCEL> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<AGV_POS_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                        throw new Exception(strEM);
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>AGV_POS_REPORT record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<ALARM_HAPPEN_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BCR_CHECK_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST start!");
            try
            {
                //以下為測試CV時使用
                /*
                rMsg.transactionId = "AUTO_" + rMsg.transactionId;
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
                return Json(rMsg);
                */
                //以上為測試CV時使用

                CmdMstInfo cmd = new CmdMstInfo();
                bool check = false;
                check = clsDB_Proc.GetDB_Object().GetCmd_Mst().FunCheckHasCommand_ByBoxID(Body.barcode, ref cmd);
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
                string deviceId = tool.GetDeviceId(Body.location);

                if (check)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{cmd.Cmd_Sno}>This BCRCheck exist.");
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, Body.location))
                        throw new Exception($"Error: UpdateCurLoc Fail. jobId = {Body.jobId}");
                }
                else if (!check)
                {
                    if (Body.location == ConveyorDef.Box.B1_037.BufferName || Body.location == ConveyorDef.Box.B1_041.BufferName ||
                        Body.location == ConveyorDef.Box.B1_045.BufferName || Body.location == ConveyorDef.Box.B1_054.BufferName ||
                        Body.location == ConveyorDef.Box.B1_117.BufferName || Body.location == ConveyorDef.Box.B1_121.BufferName ||
                        Body.location == ConveyorDef.Box.B1_125.BufferName || Body.location == ConveyorDef.Box.B1_134.BufferName ||
                        Body.location == ConveyorDef.AGV.B1_070.BufferName || Body.location == ConveyorDef.AGV.B1_074.BufferName ||
                        Body.location == ConveyorDef.AGV.B1_078.BufferName)
                    {
                        CarrierPutawayCheckInfo info = new CarrierPutawayCheckInfo
                        {
                            portId = ConveyorDef.WES_B800CV,
                            carrierId = Body.barcode,
                            storageType = "B800"
                        }; 
                        if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                        {
                            info.portId = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                        if (!clsAPI.GetAPI().GetCarrierPutawayCheck().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: Sending CarrierPutawayCheck to WES fail, jobId = {Body.jobId}.");
                    }
                    else if (Body.location == ConveyorDef.AGV.M1_10.BufferName || Body.location == ConveyorDef.AGV.M1_20.BufferName ||
                            Body.location == ConveyorDef.AGV.M1_05.BufferName || Body.location == ConveyorDef.AGV.M1_15.BufferName)
                            //M1_05, M1_15為故障模式可能會使用之入庫點
                    {
                        CarrierPutawayCheckInfo info = new CarrierPutawayCheckInfo
                        {
                            portId = con.StnNo,
                            carrierId = Body.barcode,
                            storageType = "M800"
                        };
                        if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                        {
                            info.portId = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                        if (!clsAPI.GetAPI().GetCarrierPutawayCheck().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: Sending CarrierPutawayCheck to WES fail, jobId = {Body.jobId}.");
                    }
                    else
                    {
                        //以下為無MES測試時使用
                        
                        rMsg.transactionId = "AUTO_" + rMsg.transactionId + "_ignore_CarrierReturnNext";
                        rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                        rMsg.returnComment = "";

                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
                        return Json(rMsg);
                        
                        //以上為無MES測試時使用

                        CarrierReturnNextInfo info = new CarrierReturnNextInfo
                        {
                            carrierId = Body.barcode,
                            isEmpty = "N",
                            fromLocation = con.StnNo,
                            carrierType = Def.clsTool.FunSwitchCarrierType(Body.carrierType)
                        };
                        if(ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                        {
                            info.fromLocation = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                        }
                            
                        if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, clsAPI.GetWesApiConfig().IP))
                            throw new Exception($"Error: Sending CarrierReturnNext to WES fail, jobId = {Body.jobId}");
                    }

                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BCR_CHECK_REQUEST record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BIN_EMPTY_LEAVE_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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

        [Route("WCS/BLOCK_STATUS_CHANGE")]
        [HttpPost]
        public IHttpActionResult BLOCK_STATUS_CHANGE([FromBody] BlockStatusInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<BLOCK_STATUS_CHANGE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DESTINATION_CHECK> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            CmdDestinationCheckReply rMsg = new CmdDestinationCheckReply
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DESTINATION_CHECK start!");
            try
            {
                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().CheckHasMiddleCmdbyCSTID(Body.jobId))
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
                                int.TryParse(bufferStatusReply.ready, out var ready);
                                if (ready == (int)clsEnum.ControllerApi.Ready.OutReady)
                                {
                                    rMsg.toLocation = locations[i];
                                    isFindLocation = true;
                                    break;
                                }
                            }

                            if (!isFindLocation)
                            {
                                throw new Exception($"Error: 現在無可放入之撿料口, jobId = {cmd.Cmd_Sno}.");
                            }
                        }
                        else if ((cmd.Stn_No != ConveyorDef.Box.B1_062.BufferName && cmd.Stn_No != ConveyorDef.Box.B1_067.BufferName &&
                                cmd.Stn_No != ConveyorDef.Box.B1_142.BufferName && cmd.Stn_No != ConveyorDef.Box.B1_147.BufferName) &&
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
                        else if (cmd.Stn_No == ConveyorDef.Box.B1_062.BufferName || cmd.Stn_No == ConveyorDef.Box.B1_067.BufferName ||
                            cmd.Stn_No == ConveyorDef.Box.B1_142.BufferName || cmd.Stn_No == ConveyorDef.Box.B1_147.BufferName)
                        {
                            rMsg.toLocation = cmd.Stn_No;
                        }
                        else if (ConveyorDef.GetLifetNode_List().Where(r => r.BufferName == Body.location).Any())
                        {
                            if (cmd.Stn_No == ConveyorDef.E04.LO1_07.BufferName)
                            {
                                rMsg.toLocation = ConveyorDef.E04.LO1_07.BufferName;
                            }
                            else if (ConveyorDef.GetAGV_3FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
                                rMsg.toLocation = ConveyorDef.AGV.LO4_04.BufferName;
                            else if (ConveyorDef.GetAGV_5FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
                                rMsg.toLocation = ConveyorDef.AGV.LO5_04.BufferName;
                            else if (ConveyorDef.GetAGV_6FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
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
                                    if (Body.location == ConveyorDef.Box.B1_041.BufferName)
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
                        if (ConveyorDef.GetLifetNode_List().Where(r => r.BufferName == Body.location).Any())
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
                            else 
                            {
                                if (ConveyorDef.GetAGV_3FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
                                    rMsg.toLocation = ConveyorDef.AGV.LO4_04.BufferName;
                                else if (ConveyorDef.GetAGV_5FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
                                    rMsg.toLocation = ConveyorDef.AGV.LO5_04.BufferName;
                                else if (ConveyorDef.GetAGV_6FPort().Where(r => r.BufferName == cmd.Stn_No).Any())
                                    rMsg.toLocation = ConveyorDef.AGV.LO6_04.BufferName;
                                else
                                    rMsg.toLocation = ConveyorDef.AGV.LO3_01.BufferName;
                            }
                        }
                    }
                }
                else
                {
                    MiddleCmd middle = new MiddleCmd();
                    if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunGetMiddleCmdbyCommandID(Body.jobId, ref middle))
                        throw new Exception($"Error: Get middle command fail jobId: {Body.jobId}");
                    rMsg.toLocation = middle.Destination;
                }
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";
                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DESTINATION_CHECK record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CMD_DOUBLE_STORAGE_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                        info.craneId = "E801";
                        break;
                    case "B":
                        info.craneId = "E802";
                        break;
                    case "C":
                        info.craneId = "E803";
                        break;
                    case "D":
                        info.craneId = "E804";
                        break;
                    case "E":
                        info.craneId = "E805";
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
                    disableLocation = Body.doubleStorage == "Y" ? "Y" : "N"
                };

                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunShelfReportToWes(Body.jobId, info_2, ref strEM))
                    throw new Exception(strEM);

                rMsg.newLoc = reply.shelfId;
                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CMD_DOUBLE_STORAGE_REQUEST record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<COMMAND_COMPLETE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>COMMAND_COMPLETE start!");
            try
            {
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetProc().FunCommandComplete(Body.jobId, Body.cmdMode, Body.emptyRetrieval, Body.portId, Body.carrierId, clsAPI.GetWesApiConfig().IP, ref strEM))
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

        [Route("WCS/CONTROL_CHANGE")]
        [HttpPost]
        public IHttpActionResult CONTROL_CHANGE([FromBody] ControlChangeInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<CONTROL_CHANGE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<EMPTY_BIN_LOAD_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                EmptyESDCarrierLoadRequestInfo info = new EmptyESDCarrierLoadRequestInfo
                {
                    location = con.StnNo,
                    reqQty = Body.reqQty,
                    withClapBoard = clsConstValue.YesNo.No
                };
                if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                {
                    info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                }
                if (!clsAPI.GetAPI().GetEmptyESDCarrierLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: Send Empty ESDCarrier Load Request to WES fail, location = {Body.location}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>EMPTY_BIN_LOAD_REQUEST record end!");
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

        [Route("WCS/FORK_STATUS_REPORT")]
        [HttpPost]
        public IHttpActionResult FORK_STATUS_REPORT([FromBody] ForkStatusReportInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<FORK_STATUS_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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

        [Route("WCS/LOCATION_DISABLE_REQUEST")]
        [HttpPost]
        public IHttpActionResult LOCATION_DISABLE_REQUEST([FromBody] LocationDisableRequestInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOCATION_DISABLE_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_NG_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_RETRIEVE_CANCEL> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<MODE_CHANGE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>MODE_CHANGE start!");
            try
            {
                //mode == 1正常，mode ==2異常
                //PCBA以M1-05, M1-10, M1-15, M1-20分別代表四條線


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>MODE_CHANGE record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<POSITION_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                    default:
                        con = ConveyorDef.GetBuffer(Body.position);
                        break;
                }

                string deviceId = con.DeviceId != "" ? con.DeviceId : con.ControllerID;
                

                if (Body.carrierType == clsConstValue.ControllerApi.CarrierType.Lot)
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, ConveyorDef.DeviceID_Tower, Body.position))
                        throw new Exception($"Error: Update CmdMst fail, jobId = {Body.jobId}.");
                    
                    LotPositionReportInfo info = new LotPositionReportInfo
                    {
                        jobId = cmd.JobID,
                        lotId = Body.id,
                        location = con.StnNo != null ? con.StnNo : con.BufferName
                    };

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
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(cmd.Cmd_Sno, deviceId, con.BufferName))
                    throw new Exception($"Error: Update CurLoc fail, jobId = {Body.jobId}.");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>POSITION_REPORT record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_AWAY_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                    if (Body.rackId == "UNKNOWN")
                    {
                        //待修改線邊倉的料價站(S0-05)點
                        if (Body.stagePosition == ConveyorDef.AGV.S4_49.BufferName)
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

                            cmd.New_Loc = ConveyorDef.AGV.S4_49.BufferName;//新站口，尚未有站號更新至程式

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
                            if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, clsAPI.GetWesApiConfig().IP))
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_COMMAND_DONE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                    if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    {
                        strEM = $"Error: CarrierRetrurnNext fail, jobid = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_COMMAND_DONE record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_ID_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                if (clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                {
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, deviceId, con.BufferName))
                    {
                        strEM = $"Error: Update CurLoc fail, jobId = {Body.jobId}.";
                        throw new Exception(strEM);
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
                    if (!clsAPI.GetAPI().GetCarrierReturnNext().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    {
                        strEM = $"Error: CarrierRetrurnNext fail, jobid = {Body.jobId}.";
                        throw new Exception(strEM);
                    }
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>RACK_ID_REPORT record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_RECEIVED_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                        portStatus = clsEnum.WmsApi.portStatus.Pickup_Ready.ToString()
                    };
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<RACK_TURN_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<REEL_STOCK_IN> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_LOAD_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_LOAD_REQUEST record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_EMPTY_MAGAZINE_UNLOAD> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_UNLOAD start!");
            try
            {
                ConveyorInfo con = new ConveyorInfo();
                con = ConveyorDef.GetBuffer(Body.location);
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


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_EMPTY_MAGAZINE_UNLOAD record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<SMTC_MAGAZINE_LOAD_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                MagazineLoadRequestInfo info = new MagazineLoadRequestInfo();
                info.location = con.StnNo;
                if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                {
                    info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                }

                if (!clsAPI.GetAPI().GetMagazineLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: 傳送MagazineLoadRequest 至 WES 失敗, jobId = {info.jobId}");


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>SMTC_MAGAZINE_LOAD_REQUEST record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STAGE_EMPTY_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                if (Body.stagePosition == ConveyorDef.AGV.E2_35.BufferName ||
                    Body.stagePosition == ConveyorDef.AGV.E2_36.BufferName ||
                    Body.stagePosition == ConveyorDef.AGV.E2_37.BufferName)
                {
                    PortStatusUpdateInfo info = new PortStatusUpdateInfo
                    {
                        portId = con.StnNo,
                        portStatus = clsEnum.WmsApi.portStatus.Empty.ToString()
                    };
                    if (!clsAPI.GetAPI().GetPortStatusUpdate().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: PortStatusUpdate to WES fail, jobId = {Body.jobId}.");
                }
                else
                {
                    EmptyESDCarrierLoadRequestInfo info = new EmptyESDCarrierLoadRequestInfo
                    {
                        location = con.StnNo,
                        reqQty = 1,
                        withClapBoard = clsConstValue.YesNo.Yes
                    };
                    if (ConveyorDef.GetSharingNode().Where(r => r.end.BufferName == con.BufferName || r.start.BufferName == con.BufferName).Any())
                    {
                        info.location = ConveyorDef.GetTwoNodeOneStnnoByBufferName(con.BufferName).Stn_No;
                    }
                    if (!clsAPI.GetAPI().GetEmptyESDCarrierLoadRequest().FunReport(info, clsAPI.GetWesApiConfig().IP))
                        throw new Exception($"Error: EmptyESDCarrierLoadRequest to WES fail, jobId = {Body.jobId}.");
                }

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STAGE_EMPTY_INFO record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<STATUS_CHANGE_REPORT> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STATUS_CHANGE_REPORT start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>STATUS_CHANGE_REPORT record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TASK_COMPLETE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE start!");
            try
            {
                //AGV命令在middle層
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateFinishByCommanId(Body.jobId, ref strEM))
                    throw new Exception(strEM);

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>TASK_COMPLETE record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<TRANSFER_COMMAND_DONE> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<UNKNOWN_BIN_LEAVE_INFO> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            ReplyCode rMsg = new ReplyCode
            {
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>UNKNOWN_BIN_LEAVE_INFO start!");
            try
            {
                string strEM = "";
                CmdMstInfo cmd = new CmdMstInfo();

                cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                {
                    throw new Exception($"<{Body.jobId}>取得序號失敗！");
                }

                cmd.BoxID = "";
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
                int count = 0; bool check = false;
                while (count < ConveyorDef.GetB800CV_List().Count())
                {
                    B800CV = ConveyorDef.GetB800CV();
                    if (clsMiddle.GetMiddle().CheckIsInReady(B800CV))
                    {
                        cmd.New_Loc = B800CV.BufferName;
                        check = true;
                        break;
                    }
                    count++;
                }
                if (!check)
                {
                    throw new Exception("Error: B800CV 無InReady儲位");
                }

                cmd.Prty = "5";
                cmd.Remark = "";

                cmd.Stn_No = Body.position;

                cmd.Host_Name = "WCS";
                cmd.Zone_ID = "";
                cmd.carrierType = "";

                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunInsCmdMst(cmd, ref strEM))
                    throw new Exception(strEM);


                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>UNKNOWN_BIN_LEAVE_INFO record end!");
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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<WRONG_EQU_STOCK_IN_REQUEST> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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
                        info.craneId = "E801";
                        break;
                    case "B":
                        info.craneId = "E802";
                        break;
                    case "C":
                        info.craneId = "E803";
                        break;
                    case "D":
                        info.craneId = "E804";
                        break;
                    case "E":
                        info.craneId = "E805";
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(Body.stockerId), "Error: E800 stocker ID不合格式.");
                }
                EmptyShelfQueryReply reply = new EmptyShelfQueryReply();
                if (!clsAPI.GetAPI().GetEmptyShelfQuery().FunReport(info, ref reply, clsAPI.GetWesApiConfig().IP))
                    throw new Exception($"Error: EmptyShelfQuery to WES fail, jobId = {Body.jobId}");

                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>WRONG_EQU_STOCK_IN_REQUEST record end!");
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
