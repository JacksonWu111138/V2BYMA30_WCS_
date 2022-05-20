using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class ShelfReportInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "SHELF_REPORT";
        public string shelfId { get; set; }
        public string shelfStatus { get; set; }
        public string carrierId { get; set; }
    }
}
