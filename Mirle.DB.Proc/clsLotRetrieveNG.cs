using Mirle.DataBase;
using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Proc
{
    public class clsLotRetrieveNG
    {
        private Fun.clsAlarmCVCLog alarmCVCLog = new Fun.clsAlarmCVCLog();
        private clsDbConfig _config = new clsDbConfig();
        public clsLotRetrieveNG(clsDbConfig config)
        {
            _config = config;
        }
        
    }
}
