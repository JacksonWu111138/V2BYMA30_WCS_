using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Def.U2NMMA30
{
    public class LocationDef
    {
        public enum Location
        {
            A1_01, A1_02, A1_04, A1_05,
            A1_07, A1_08, A1_10, A1_11,
            A1_13, A1_14, A1_16, A1_17,
            A1_19, A1_20, A1_22, A1_23,
            LeftFork, RightFork, Shelf, Teach
        }

        public enum LocationIn
        {
            A1_04, A1_05, A1_10, A1_11, 
            A1_16, A1_17, A1_22, A1_23
        }
    }
}
