using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class EmptyShelfQuery_WMS
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "EMPTY_SHELF_QUERY";
        public string carrierId { get; set; }
        public string shelfId { get; set; }
        public string returnCode { get; set; }
        public string returnComment { get; set; }
    }
}
