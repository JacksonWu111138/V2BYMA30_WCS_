using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.U2NMMA30.Models
{
    public class EmptyBinLoadRequestInfo : BaseInfo
    {
        public string location { get; set; }
        public int reqQty { get; set; }
    }
}
