using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class EmptyShelfQuery_WCS
    {
        public string jobId { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        public string transactionId { get; set; } = "EMPTY_SHELF_QUERY";
        public string carrierId { get; set; }
    }
}
