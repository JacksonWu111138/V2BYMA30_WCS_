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
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace,$"<CARRIER_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

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

                    cmd.Loc_ID = Body.carrierId;
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
                                cmd.Stn_No = B800CV.StnNo;
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
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.fromLocation);
                        cmd.Stn_No = cv_to.StnNo;
                    }
                    cmd.Host_Name = "WES";
                    cmd.Zone_ID = "";
                    cmd.carrierType = Body.carrierType;

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
                    
                    cmd.Loc_ID = Body.carrierId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.toShelfId;
                    cmd.Equ_No = tool.funGetEquNoByLoc(cmd.Loc).ToString();

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
                            throw new Exception("Error: B800CV 無InReady儲位");
                        }
                    }
                    else
                    {
                        var cv_to = ConveyorDef.GetBuffer_ByStnNo(Body.fromPortId);
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

                    cmd.Loc_ID = Body.carrierId;
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

                    if (Body.toLocation == ConveyorDef.WES_B800CV)
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

                    cmd.Loc_ID = Body.carrierId;
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

                    cmd.Loc_ID = Body.lotId;
                    cmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                    cmd.CurDeviceID = "";
                    cmd.CurLoc = "";
                    cmd.End_Date = "";

                    cmd.Loc = Body.toShelfId;
                    cmd.Equ_No = tool.funGetEquNoByLoc(cmd.Loc).ToString();

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
                            throw new Exception("Error: B800CV 無InReady儲位");
                        }
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

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunLotPutawayInsCmdMst(cmd, ref strEM))
                        throw new Exception(strEM);
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
                    foreach(var lot in Body.lotList)
                    {
                        if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdatePry(lot.lotId, Body.priority, ref strEM))
                        {
                            throw new Exception($"<{Body.jobId}> {strEM}");
                        }
                    }
                }
                else
                {
                    foreach (var lot in Body.lotList)
                    {
                        cmd.Cmd_Sno = clsDB_Proc.GetDB_Object().GetSNO().FunGetSeqNo(clsEnum.enuSnoType.CMDSNO);
                        if (string.IsNullOrWhiteSpace(cmd.Cmd_Sno))
                        {
                            throw new Exception($"<{Body.jobId}>取得序號失敗！");
                        }

                        cmd.Loc_ID = lot.lotId;
                        cmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                        cmd.CurDeviceID = "";
                        cmd.CurLoc = "";
                        cmd.End_Date = "";

                        cmd.Loc = lot.fromShelfId;
                        cmd.Equ_No = tool.funGetEquNoByLoc(cmd.Loc).ToString();

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

                    }
                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunLotRetrieveInsCmdMst(cmd, ref strEM))
                    {
                        //送出【寫入cmdMst命令】失敗
                        throw new Exception(strEM);
                    }
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
                //執行BufferRoll
                ConveyorInfo conveyor = new ConveyorInfo();
                conveyor = ConveyorDef.GetBuffer(Body.currentLoc);

                BufferRollInfo info = new BufferRollInfo { jobId = Body.jobId, bufferId = conveyor.BufferName };

                if (!clsAPI.GetAPI().GetBufferRoll().FunReport(info, conveyor.API.IP))
                    throw new Exception(strEM);

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
                if(Body.status == clsEnum.AlarmSts.OnGoing.ToString())
                {
                    if (!clsDB_Proc.GetDB_Object().GetAlarmCVCLog().FunAlarm_Occur(Body.jobId, Body.deviceId, Body.alarmCode,
                        Body.alarmDef, Body.bufferId, Body.happenTime, ref strEM))
                        throw new Exception(strEM);
                }
                else
                {
                    if (!clsDB_Proc.GetDB_Object().GetAlarmCVCLog().FunAlarm_Solved(Body.jobId, Body.deviceId, Body.alarmCode,
                        Body.alarmDef, Body.bufferId, Body.happenTime, ref strEM))
                        throw new Exception(strEM);
                }


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
                if(!clsDB_Proc.GetDB_Object().GetProc().FunUpdateCmdMstCurLocOrCarrierReturnNext(Body.jobId, ConveyorDef.DeviceID_Tower
                    , Body.position, Body.binId, ref strEM))
                    throw new Exception(strEM);
                
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
                if(!clsDB_Proc.GetDB_Object().GetMiddleCmd().CheckHasMiddleCmdbyCmdSno(Body.jobId))
                    throw new Exception($"Error: MiddleCmd table has no this jobId: {Body.jobId}");
                else
                {
                    MiddleCmd middle = new MiddleCmd();
                    if(!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunGetMiddleCmdbyCommandID(Body.jobId, ref middle))
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
                //要放在哪個地方上拋?

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
                if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCmdSts(Body.jobId, clsConstValue.CmdSts.strCmd_Finish_Wait, ""))
                    throw new Exception($"Error: Update CmdSts Fail. jobId = {Body.jobId}.");

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

                if(clsDB_Proc.GetDB_Object().GetMiddleCmd().CheckHasMiddleCmdbyCmdSno(Body.jobId))
                {
                    MiddleCmd cmd = new MiddleCmd();
                    if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunGetMiddleCmdbyCommandID(Body.jobId, ref cmd))
                        throw new Exception($"Error: Get MiddleCmd fail. jobId : {Body.jobId}.");
                    if (Body.position == cmd.Destination)
                        if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateCmdSts(Body.jobId, clsConstValue.CmdSts_MiddleCmd.strCmd_Finish_Wait, ""))
                            throw new Exception($"Error: Update MiddleCmd fail. jobId : {Body.jobId}.");
                }
                else
                {
                    //非middle控制
                    CmdMstInfo cmd = new CmdMstInfo();
                    if(!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunGetCommand(Body.jobId, ref cmd))
                        throw new Exception($"Error: Get CmdMst fail. jobId : {Body.jobId}.");

                    ConveyorInfo con = new ConveyorInfo();
                    con = ConveyorDef.GetBuffer(Body.position);

                    if (!clsDB_Proc.GetDB_Object().GetCmd_Mst().FunUpdateCurLoc(Body.jobId, con.bufferLocation.DeviceId, Body.position))
                        throw new Exception($"Error: Update CmdMst fail. jobId : {Body.jobId}.");
                }


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
                if(Body.rackId == "unknown")
                {
                    //判斷body.stagePosition是否來自縣邊倉(S0-05)，如果不是:送到這個位置，如果是:call AGV來讀barcode
                }
                else
                {
                    ConveyorInfo con = new ConveyorInfo();
                    con = ConveyorDef.GetBuffer(Body.stagePosition);
                    if(!clsDB_Proc.GetDB_Object().GetProc().FunUpdateCmdMstCurLocOrCarrierReturnNext(Body.jobId, con.bufferLocation.DeviceId,
                        Body.stagePosition, Body.rackId, ref strEM))
                        throw new Exception(strEM);
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
                if (!clsDB_Proc.GetDB_Object().GetProc().FunUpdateCmdMstCurLocOrCarrierReturnNext(Body.jobId, con.bufferLocation.DeviceId,
                    Body.stagePosition, Body.rackId, ref strEM))
                    throw new Exception(strEM);

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
                if (!clsDB_Proc.GetDB_Object().GetProc().FunUpdateCmdMstCurLocOrCarrierReturnNext(Body.jobId, con.bufferLocation.DeviceId,
                    Body.rackLoc, Body.rackId, ref strEM))
                    throw new Exception(strEM);

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
                if (!clsDB_Proc.GetDB_Object().GetProc().FunRackReceivedInfo(Body.jobId, Body.rackId, Body.stagePosition, ref strEM))
                    throw new Exception(strEM);

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
                LotPutawayCheckReply reply = new LotPutawayCheckReply();


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
                string strEM="";
                if(!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateFinishByCommanId(Body.jobId, ref strEM))
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
                string strEM = "";
                if (!clsDB_Proc.GetDB_Object().GetMiddleCmd().FunMiddleCmdUpdateFinishByCommanId(Body.jobId, ref strEM))
                    throw new Exception(strEM);

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

            ReelStockInReply rMsg = new ReelStockInReply
            {
                reelId = Body.reelId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>WRONG_EQU_STOCK_IN_REQUEST start!");
            try
            {



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
