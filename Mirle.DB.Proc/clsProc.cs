using Mirle.DataBase;
using Mirle.DB.Fun;
using Mirle.Def;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.DB.Proc
{
    public class clsProc
    {
        private clsPortDef PortDef = new clsPortDef();
        private clsRoutdef routdef = new clsRoutdef();
        private clsDbConfig _config = new clsDbConfig();
        public clsProc(clsDbConfig config)
        {
            _config = config;
        }

        public  bool FunMapping_Proc(out List<Element_Port> ports, ref DataTable dtRoutDef)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        ports = PortDef.GetAllPort(db);
                        if (ports.Count <= 0) return false;

                        dtRoutDef = new DataTable();
                        return routdef.GetRoutdef(ref dtRoutDef, db);
                    }
                    else
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, "資料庫開啟失敗！");
                        ports = new List<Element_Port>();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                ports = new List<Element_Port>();
                return false;
            }
        }
    }
}
