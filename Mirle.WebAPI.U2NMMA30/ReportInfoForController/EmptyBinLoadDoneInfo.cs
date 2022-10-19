using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.V2BYMA30.ReportInfo
{
    public class EmptyBinLoadDoneInfo
    {
        /// <summary>
        /// 命令序號
        /// </summary>
        public string jobId { get; set; } = "";
        public string transactionId { get; set; } = "EMPTY_BIN_LOAD_DONE";
        public string location { get; set; }
    }
}
