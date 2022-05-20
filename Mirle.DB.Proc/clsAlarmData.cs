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
    public class clsAlarmData
    {
        private Fun.clsAlarmData alarmData = new Fun.clsAlarmData();
        private clsDbConfig _config = new clsDbConfig();
        public clsAlarmData(clsDbConfig config)
        {
            _config = config;
        }
    }
}
