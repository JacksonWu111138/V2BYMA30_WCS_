using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class RetrieveCompleteInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "RETRIEVE_COMPLETE";
        public string carrierId { get; set; }
        public string portId { get; set; }
        public string isComplete { get; set; }
        public string emptyTransfer { get; set; } = clsEnum.WmsApi.IsComplete.N.ToString();
    }
}
