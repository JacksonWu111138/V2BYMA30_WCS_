using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.V2BYMA30.ReportInfo
{
    public class RollTaskInfo
    {
        /// <summary>
        /// 命令序號
        /// </summary>
        public string jobId { get; set; } = "";
        public string transactionId { get; set; } = "ROLL_TASK";
        public string fromLoc { get; set; }
        public string toLoc { get; set; }
        public string carrierType { get; set; }
    }
}
