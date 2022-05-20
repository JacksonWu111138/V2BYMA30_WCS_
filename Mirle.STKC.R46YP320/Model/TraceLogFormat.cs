using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.STKC.R46YP320.Model
{
    public class TraceLogFormat
    {
        public string CommandID { get; set; } = string.Empty;
        public string CarrierID { get; set; } = string.Empty;
        public string BoxID { get; set; } = string.Empty;
        public string TaskNo { get; set; } = string.Empty;
        public string FunctionName { get; set; } = "STKC";
        public string Message { get; set; } = string.Empty;
    }
}
