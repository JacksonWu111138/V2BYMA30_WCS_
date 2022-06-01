using Mirle.DB.Object;
using Mirle.Def;
using Mirle.Stocker.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;

namespace Mirle.ASRS.DBCommand.DoubleDeep.SingleCrane.SingleFork
{
    public class Process : IProcess
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private DeviceInfo device;
        public Process(clsPlcConfig plcConfig, DeviceInfo Device)
        {
            EquNo = int.Parse(plcConfig.DeviceNo);
            device = Device;
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = false; timRead.Interval = 500;
        }

        private int EquNo;
        public void Start() => timRead.Enabled = true;
        public void Stop() => timRead.Enabled = false;

        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {
                if (clsDB_Proc.DBConn)
                {
                    
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }
    }
}
