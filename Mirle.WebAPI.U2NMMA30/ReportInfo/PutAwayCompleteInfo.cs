using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class PutAwayCompleteInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "PUTAWAY_COMPLETE";
        public string carrierId { get; set; }
        public string shelfId { get; set; }
        public string isComplete { get; set; } = "Y";
    }
}
