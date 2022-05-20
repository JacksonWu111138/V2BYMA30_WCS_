using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class WcsCancelInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "WCS_CANCEL";
        public string carrierId { get; set; }
        public string cancelType { get; set; }
    }
}
