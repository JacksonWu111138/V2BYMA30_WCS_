using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Def;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class PutawayCheckInfo
    {
        public string jobId { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        public string transactionId { get; set; } = "PUTAWAY_CHECK";
        public string portId { get; set; }
        public string carrierId { get; set; }
        public string onlineMode { get; set; } = clsEnum.WmsApi.IsOnline.Y.ToString();
        public List<SlotListInfo> lotList { get; set; }
        public string checkOnly { get; set; } = clsEnum.WmsApi.IsOnline.N.ToString();
    }
}
