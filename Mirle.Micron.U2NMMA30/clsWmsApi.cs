using System;
using Mirle.Structure;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30;

namespace Mirle.Micron.U2NMMA30
{
    public class clsWmsApi
    {
        private static clsHost report;

        public static void FunInit(WebApiConfig config)
        {
            report = new clsHost(config);

            for (int i = 0; i < StockerSts.Length; i++)
            {
                StockerSts[i] = clsEnum.WmsApi.EqSts.StockOutOnly;
            }
        }

        public static bool FunPutAwayCheck_Proc(ConveyorInfo buffer)
        {
            try
            {
                PutawayCheckInfo info = GetPutawayCheckInfo(buffer);

                if(report.GetPutawayCheck().FunReport(info))
                {
                    if (clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).SetReadReq().Result)
                    {
                        clsWriLog.Log.FunWriTraceLog_CV($"<Buffer> {buffer.BufferName} <Carrier> {info.carrierId} => 告知CV已收到條碼訊息");
                        return true;
                    }
                    else
                    {
                        clsWriLog.Log.FunWriTraceLog_CV($"NG: <Buffer> {buffer.BufferName} <Carrier> {info.carrierId} => 告知CV已收到條碼訊息失敗！");
                        return false;
                    }
                }
                else
                {
                    clsWriLog.Log.FunWriTraceLog_CV($"NG: <Buffer> {buffer.BufferName} <Carrier> {info.carrierId} => 上報Putaway Check失敗！");
                    return false;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public static PutawayCheckInfo GetPutawayCheckInfo(ConveyorInfo buffer, bool bCheckOnly = false)
        {
            PutawayCheckInfo info = new PutawayCheckInfo
            {
                carrierId = clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetTrayID,
                portId = buffer.StnNo,
                checkOnly = bCheckOnly ? clsEnum.WmsApi.IsOnline.Y.ToString() : clsEnum.WmsApi.IsOnline.N.ToString()
            };

            var cv = clsMicronCV.GetConveyorController().GetBuffer(buffer.Index);
            info.onlineMode = cv.Online ? clsEnum.WmsApi.IsOnline.Y.ToString() : clsEnum.WmsApi.IsOnline.N.ToString();
            info.lotList = new System.Collections.Generic.List<SlotListInfo>();
            for (int i = 1; i <= 8; i++)
            {
                if (i < 3)
                {
                    if (!string.IsNullOrWhiteSpace(clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetFobIdByLocation(i)) ||
                        !string.IsNullOrWhiteSpace(clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetLittleThingIdByLocation(i)))
                    {
                        if (!string.IsNullOrWhiteSpace(clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetFobIdByLocation(i)))
                        {
                            info.lotList.Add(new SlotListInfo()
                            {
                                lotId = clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetFobIdByLocation(i),
                                slotId = i.ToString()
                            });
                        }
                        else
                        {
                            info.lotList.Add(new SlotListInfo()
                            {
                                lotId = clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetLittleThingIdByLocation(i),
                                slotId = i.ToString()
                            });
                        }
                    }
                    else
                    {
                        info.lotList.Add(new SlotListInfo()
                        {
                            lotId = "",
                            slotId = i.ToString()
                        });
                    }
                }
                else
                {
                    info.lotList.Add(new SlotListInfo()
                    {
                        lotId = clsMicronCV.GetConveyorController().GetBuffer(buffer.Index).GetLittleThingIdByLocation(i),
                        slotId = i.ToString()
                    });
                }
            }

            return info;
        }

        public static RetrieveCompleteInfo GetRetrieveCompleteInfo(CmdMstInfo cmd, ConveyorInfo buffer)
        {
            RetrieveCompleteInfo info = new RetrieveCompleteInfo
            {
                carrierId = cmd.BoxID,
                isComplete = clsEnum.WmsApi.IsComplete.Y.ToString(),
                jobId = cmd.JobID,
                portId = buffer.StnNo
            };

            return info;
        }

        private static clsEnum.WmsApi.EqSts[] StockerSts = new clsEnum.WmsApi.EqSts[4];
        public static void FunReportStkStatusChanged(int StockerID, Stocker.R46YP320.StockerEnums.CraneStatus status)
        {
            clsEnum.WmsApi.EqSts sts_api;
            switch(status)
            {
                case Stocker.R46YP320.StockerEnums.CraneStatus.BUSY:
                case Stocker.R46YP320.StockerEnums.CraneStatus.IDLE:
                    sts_api = clsEnum.WmsApi.EqSts.Run; break;
                default:
                    sts_api = clsEnum.WmsApi.EqSts.Down; break;
            }

            if (StockerSts[StockerID - 1] != sts_api)
            {
                StockerSts[StockerID - 1] = sts_api;
                EQPStatusUpdateInfo info = new EQPStatusUpdateInfo
                {
                    craneId = StockerID.ToString(),
                    craneStatus = ((int)sts_api).ToString()
                };

                report.GetEQPStatusUpdate().FunReport(info);
            }
        }

        public static clsHost GetApiProcess()
        {
            return report;
        }
    }
}
