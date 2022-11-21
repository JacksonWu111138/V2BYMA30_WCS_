using System;
using Mirle.Def;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Structure.Info;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mirle.MapController;

namespace Mirle.DB.Object
{
    public class clsMapController
    {
        private static MapHost mapHost;
        public static void Initial(MapHost MapHost)
        {
            mapHost = MapHost;
        }

        public static MapHost GetMapHost() => mapHost;
    }
}
