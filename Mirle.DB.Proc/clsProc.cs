using Mirle.DataBase;
using Mirle.DB.Fun;
using Mirle.Def;
using Mirle.MapController;
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
        private Fun.clsCmd_Mst Cmd_Mst = new Fun.clsCmd_Mst();
        private clsDbConfig _config = new clsDbConfig();
        public clsProc(clsDbConfig config)
        {
            _config = config;
        }

        public bool FunAsrsCmd_Proc(DeviceInfo Device, string StockInLoc_Sql, MapHost Router, WMS.Proc.clsHost wms)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetCommand(Device.DeviceID, StockInLoc_Sql, ref dtTmp, db);

                        return true;
                    }
                    else return false;
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
