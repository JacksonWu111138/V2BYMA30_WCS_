using Mirle.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mirle.Def;
using System.Windows.Forms;

namespace Mirle.MapController.DB_Proc
{
    public class clsProc
    {
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
                        ports = GetAllPort(db);
                        if (ports.Count <= 0) return false;

                        dtRoutDef = new DataTable();
                        return GetRoutdef(ref dtRoutDef, db);
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

        public List<Element_Port> GetAllPort(DataBase.DB db)
        {
            List<Element_Port> lstPorts = new List<Element_Port>();
            Element_Port objPort = null;
            DataTable dtTmp = new DataTable();
            string strEM = "";
            try
            {
                string strSql = $"select * from {DB.Fun.Parameter.clsPortDef.TableName}";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        objPort = new Element_Port(Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsPortDef.Column.DeviceID]),
                            Convert.ToString(dtTmp.Rows[i][DB.Fun.Parameter.clsPortDef.Column.HostPortID]),
                            Convert.ToInt32(dtTmp.Rows[i][DB.Fun.Parameter.clsPortDef.Column.PortType]),
                            Convert.ToInt32(dtTmp.Rows[i][DB.Fun.Parameter.clsPortDef.Column.PortTypeIndex]),
                            Convert.ToInt32(dtTmp.Rows[i][DB.Fun.Parameter.clsPortDef.Column.PLCPortID]));
                        lstPorts.Add(objPort);
                    }

                    return lstPorts;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strSql + " => " + strEM);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return new List<Element_Port>();
            }
            finally
            {
                dtTmp = null;
            }
        }

        public bool GetRoutdef(ref DataTable dtTmp, DataBase.DB db)
        {
            try
            {
                string strSql = $"select * from {DB.Fun.Parameter.clsRoutdef.TableName}";
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet != DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
    }
}
