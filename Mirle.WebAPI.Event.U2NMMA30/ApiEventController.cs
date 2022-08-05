using System;
using System.Data;
using Mirle.DB.Object;
using Mirle.Def;
using Mirle.Def.U2NMMA30;
using Mirle.WebAPI.Event.U2NMMA30.Models;
using System.Web.Http;
using Mirle.Structure;
using Newtonsoft.Json;

namespace Mirle.WebAPI.Event
{
    public class WCSController : ApiController
    {
        public WCSController()
        {
        }

        #region WES-->WCS
        [Route("WCS/CARRIER_TRANSFER")]
        [HttpPost]
        public IHttpActionResult CARRIER_TRANSFER([FromBody] CarrierTransferInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace,$"<CARRIER_TRANSFER> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            CarrierReply rMsg = new CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER start!");
            try
            {



                rMsg.returnCode = clsConstValue.ApiReturnCode.Success;
                rMsg.returnComment = "";

                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>BUFFER_STATUS_QUERY record end!");
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

            CarrierReply rMsg = new CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_PUTAWAY_TRANSFER start!");
            try
            {



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

            CarrierReply rMsg = new CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_RETRIEVE_TRANSFER start!");
            try
            {



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

            CarrierReply rMsg = new CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_SHELF_TRANSFER start!");
            try
            {



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

            CarrierReply rMsg = new CarrierReply
            {
                carrierId = Body.carrierId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>CARRIER_TRANSFER_CANCEL start!");
            try
            {



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

            LotReply rMsg = new LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_PUTAWAY_TRANSFER start!");
            try
            {



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
        public IHttpActionResult LOT_RETRIEVE_TRANSFER([FromBody] LotRetrieveTransferInfo Body)
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

            LotReply rMsg = new LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_SHELF_TRANSFER start!");
            try
            {



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
        public IHttpActionResult LOT_TRANSFER_CANCEL([FromBody] LotTransferCancelInfo Body)
        {
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<LOT_TRANSFER_CANCEL> <WCS Send>\n{JsonConvert.SerializeObject(Body)}");

            LotReply rMsg = new LotReply
            {
                lotId = Body.lotId,
                jobId = Body.jobId,
                transactionId = Body.transactionId
            };
            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, $"<{Body.jobId}>LOT_TRANSFER_CANCEL start!");
            try
            {



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
    }
}
