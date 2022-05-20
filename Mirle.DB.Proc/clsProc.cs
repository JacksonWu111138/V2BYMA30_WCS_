using Mirle.DataBase;
using Mirle.Def;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.DB.Proc
{
    public class clsProc
    {
        private Fun.clsCmd_Mst Cmd_Mst = new Fun.clsCmd_Mst();
        private Fun.clsCMD_DTL CMD_DTL = new Fun.clsCMD_DTL();
        private Fun.clsLocMst LocMst = new Fun.clsLocMst();
        private Fun.clsProc proc = new Fun.clsProc();
        private clsDbConfig _config = new clsDbConfig();
        public clsProc(clsDbConfig config)
        {
            _config = config;
        }
    }
}
