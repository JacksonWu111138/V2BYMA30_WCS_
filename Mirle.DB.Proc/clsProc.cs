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

        public  bool FunMapping_Proc()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        var list = PortDef.GetAllPort(db);
                        if (list.Count <= 0) return false;

                        if (routdef.GetRoutdef(ref dtTmp, db))
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                string DeviceID = Convert.ToString(dtTmp.Rows[i][Fun.Parameter.clsRoutdef.Column.DeviceID]);
                                string HostPortID = Convert.ToString(dtTmp.Rows[i][Fun.Parameter.clsRoutdef.Column.HostPortID]);
                                var data1 = list.Where(r => r.DeviceID == DeviceID && r.HostPortID == HostPortID);
                                Location n1;
                                foreach(var loc in data1)
                                {
                                    n1 = new Location(loc.DeviceID, loc.HostPortID, Location.GetLocationTypesByPortType(loc.PortType));
                                }
                            }

                            return true;
                        }
                        else return false;
                    }
                    else
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, "資料庫開啟失敗！");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }
    }
}
