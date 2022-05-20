using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class ShelfRequestInfo
    {
        public string jobId { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        public string transactionId { get; set; } = "SHELF_REQUEST";
        public string fromShelfId { get; set; }
        public string toShelfId { get; set; }
    }
}
