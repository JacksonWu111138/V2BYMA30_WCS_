using Mirle.DataBase;
using Mirle.Def;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun
{
    public class clsLotRetrieveNG
    {
        public bool FunRetrieveNG_Occur(string sCmdSno, string jobId, string lotId, DataBase.DB db, ref string strEM)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsLotRetrieveNG.TableName} where " +
                    $"{Parameter.clsLotRetrieveNG.Column.CmdSno} = '{sCmdSno}' ";
                strSql += $" and {Parameter.clsLotRetrieveNG.Column.CmdSts} = '{Parameter.clsLotRetrieveNG.Status.Occur}' and " +
                    $"{Parameter.clsLotRetrieveNG.Column.lotId} = '{lotId}' and {Parameter.clsLotRetrieveNG.Column.JobID} = '{jobId}' ";

                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success) return true;
                else if (iRet == DBResult.NoDataSelect)
                {
                    strSql = $"insert into {Parameter.clsLotRetrieveNG.TableName}({Parameter.clsLotRetrieveNG.Column.CmdSno}," +
                        $"{Parameter.clsLotRetrieveNG.Column.JobID},{Parameter.clsLotRetrieveNG.Column.lotId}," +
                        $"{Parameter.clsLotRetrieveNG.Column.CmdSts},{Parameter.clsLotRetrieveNG.Column.Start_Date}" +
                        $") values(";
                    strSql += $"'{sCmdSno}', '{jobId}', '{lotId}', '{Parameter.clsLotRetrieveNG.Status.Occur}', '" +
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success) return true;
                    else
                    {
                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                        return false;
                    }
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
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
