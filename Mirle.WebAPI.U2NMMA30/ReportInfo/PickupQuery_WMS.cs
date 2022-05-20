using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Def;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class PickupQuery_WMS
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "PICKUP_QUERY";
        public string carrierId { get; set; }
        public string carrierType { get; set; } = clsEnum.WmsApi.CarrierType.HWS.ToString();
        public string portId { get; set; }
        public string returnCode { get; set; }
        public string returnComment { get; set; }
        public List<SlotListInfo> slotList { get; set; }
    }
}
