using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class ReturnPutawayCheckInfo : ReturnMsgInfo
    {
        public string portId { get; set; }
        public string carrierId { get; set; }
        public List<ReturnSlotListInfo> lotList { get; set; }
    }
}
