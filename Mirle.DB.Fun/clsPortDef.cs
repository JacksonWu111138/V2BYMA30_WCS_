using System;
using System.Collections.Generic;
using Mirle.Def;
using System.Data;
using Mirle.DataBase;

namespace Mirle.DB.Fun
{
    public class clsPortDef
    {
        private List<Element_Port> glstPort = new List<Element_Port>();

        public List<Element_Port> GetLstPort()
        {
            return glstPort;
        }

        public List<Element_Port> GetAllPort(DataBase.DB db)
        {
            List<Element_Port> lstPorts = new List<Element_Port>();
            Element_Port objPort = null;
            DataTable dtTmp = new DataTable();
            string strEM = "";
            try
            {
                string strSql = $"select * from {Parameter.clsPortDef.TableName}";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success)
                {
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        objPort = new Element_Port(Convert.ToString(dtTmp.Rows[i][Parameter.clsPortDef.Column.DeviceID]), 
                            Convert.ToString(dtTmp.Rows[i][Parameter.clsPortDef.Column.HostPortID]),
                            Convert.ToInt32(dtTmp.Rows[i][Parameter.clsPortDef.Column.PortType]), 
                            Convert.ToInt32(dtTmp.Rows[i][Parameter.clsPortDef.Column.PortTypeIndex]), 
                            Convert.ToInt32(dtTmp.Rows[i][Parameter.clsPortDef.Column.PLCPortID]));
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
    }
}
