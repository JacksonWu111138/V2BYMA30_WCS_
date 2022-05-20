using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class AutoPickupInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "AUTO_PICKUP";
        public string carrierId { get; set; }
        public string portId { get; set; }
        public List<SlotListInfo_AutoPickup> slotList { get; set; }
    }
}
