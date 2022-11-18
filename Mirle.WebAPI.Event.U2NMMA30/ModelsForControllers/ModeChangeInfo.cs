using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.U2NMMA30.Models
{
    public class ModeChangeInfo : BaseInfo
    {
        public string craneId { get; set; }
        public string outPortMode { get; set; }
        public string inPortMode { get; set; }
    }
}
