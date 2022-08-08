using System.Collections.Generic;
using Mirle.Def;
using Mirle.Structure;

namespace Mirle.Middle.DB_Proc
{
    public class clsHost
    {
        private clsMiddleCmd middleCmd;
        private static object _Lock = new object();
        private static bool _IsConn = false;
        public static bool IsConn
        {
            get { return _IsConn; }
            set
            {
                lock(_Lock)
                {
                    _IsConn = value;
                }
            }
        }

        public clsHost(clsDbConfig config, DeviceInfo[] PCBA, DeviceInfo[] Box, List<ConveyorInfo> conveyors, string DeviceID_AGV, string DeviceID_Tower)
        {
            middleCmd = new clsMiddleCmd(config, PCBA, Box, conveyors, DeviceID_AGV, DeviceID_Tower);
        }

        public clsMiddleCmd GetMiddleCmd() => middleCmd;
    }
}
