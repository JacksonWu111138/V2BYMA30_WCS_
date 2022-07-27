using Mirle.DataBase;
using Mirle.Def;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Middle.DB_Proc
{
    public class clsEquCmd
    {
        private clsDbConfig _config = new clsDbConfig();
        public clsEquCmd(clsDbConfig config)
        {
            _config = config;
        }

        public bool funCheckCanInsertEquCmd(string EquNo, DB db)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                string strSQL = $"SELECT COUNT (*) AS ICOUNT FROM {Parameter.clsEquCmd.TableName} WHERE " +
                    $"{Parameter.clsEquCmd.Column.EquNo}='" + EquNo + "' ";

                objDataTable = new DataTable();
                if (db.GetDataTable(strSQL, ref objDataTable) == DBResult.Success)
                {
                    int intCraneNo = int.Parse(objDataTable.Rows[0]["ICOUNT"].ToString());
                    return intCraneNo == 0 ? true : false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
            finally
            {
                if (objDataTable != null)
                {
                    objDataTable.Clear();
                    objDataTable.Dispose();
                    objDataTable = null;
                }
            }
        }

        public bool FunInsEquCmd(EquCmdInfo cmd, DB db)
        {
            try
            {
                string SQL = $"INSERT INTO {Parameter.clsEquCmd.TableName}({Parameter.clsEquCmd.Column.CmdSno}," +
                    $"{Parameter.clsEquCmd.Column.EquNo},{Parameter.clsEquCmd.Column.CmdMode},{Parameter.clsEquCmd.Column.CmdSts}," +
                    $"{Parameter.clsEquCmd.Column.Source},{Parameter.clsEquCmd.Column.Destination},{Parameter.clsEquCmd.Column.LocSize}," +
                    $"{Parameter.clsEquCmd.Column.Priority},{Parameter.clsEquCmd.Column.CreateDate},{Parameter.clsEquCmd.Column.ReNewFlag}," +
                    $"{Parameter.clsEquCmd.Column.SpeedLevel}) values (";
                SQL += $"'{cmd.CmdSno}', '{cmd.EquNo}', '{cmd.CmdMode}', '{clsConstValue.CmdSts.strCmd_Initial}', ";
                SQL += $"'{cmd.Source}', '{cmd.Destination}', '{cmd.LocSize}', '{cmd.Priority}', " +
                    $"'{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{cmd.ReNewFlag}', '{cmd.SpeedLevel}')";
                string strEM = "";
                int iRet = db.ExecuteSQL(SQL, ref strEM);
                if (iRet == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, SQL);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{SQL} => {strEM}");
                    return false;
                }
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
