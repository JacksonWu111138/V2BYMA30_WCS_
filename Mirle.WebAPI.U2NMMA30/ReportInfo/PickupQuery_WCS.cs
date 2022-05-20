using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class PickupQuery_WCS
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "PICKUP_QUERY";
        public string carrierId { get; set; }
        public string portId { get; set; }
    }
}
