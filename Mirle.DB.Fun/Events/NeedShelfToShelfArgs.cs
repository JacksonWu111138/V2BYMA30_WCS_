using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun.Events
{
    public class NeedShelfToShelfArgs : EventArgs
    {
        public string Loc { get; }
        public string BoxID { get; }

        public NeedShelfToShelfArgs(string sLoc, string sBoxID)
        {
            Loc = sLoc;
            BoxID = sBoxID;
        }
    }
}
