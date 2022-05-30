using System;
using System.Collections.Generic;
using Mirle.DB.Proc;
using Mirle.Def;
using System.Threading.Tasks;

namespace Mirle.MapController
{
    public class MapHost
    {
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private clsHost db;
        public MapHost(clsDbConfig config)
        {
            db = new clsHost(config);
            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = true; timRead.Interval = 100;
        }

        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {
                
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                timRead.Enabled = true;
            }
        }
    }
}
