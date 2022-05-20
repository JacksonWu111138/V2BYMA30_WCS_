using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Def;

namespace Mirle.Micron.U2NMMA30
{
    public class MicronLocationInfo
    {
        public Location[] GetLocations;
        public MicronLocationInfo(int size)
        {
            GetLocations = new Location[size];
        }
    }
}
