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
        private clsTool tool = new clsTool();
        public clsEquCmd(clsDbConfig config)
        {
            _config = config;
        }

        public int GetMiddleCmd(string sCmdSno, ref MiddleCmd middleCmd, DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsMiddleCmd.TableName} where " +
                            $"{Parameter.clsMiddleCmd.Column.CommandID} = '{sCmdSno}' ";
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet == DBResult.Success) middleCmd = tool.GetMiddleCmd(dtTmp.Rows[0]);
                else { }

                return iRet;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp.Dispose();
            }
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

        public bool FunEquCmdFinish_Proc()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        string strSql = $"select * from {Parameter.clsEquCmd.TableName} where " +
                            $"{Parameter.clsEquCmd.Column.CmdSts} in ('{clsConstValue.CmdSts.strCmd_Cancel_Wait}', '{clsConstValue.CmdSts.strCmd_Finished}')";
                        string strEM = "";
                        iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                        switch(iRet)
                        {
                            case DBResult.Success:
                                for (int i = 0; i < dtTmp.Rows.Count; i++)
                                {
                                    string sDeviceID = Convert.ToString(dtTmp.Rows[i][Parameter.clsEquCmd.Column.EquNo]);
                                    string sCmdSno = Convert.ToString(dtTmp.Rows[i][Parameter.clsEquCmd.Column.CmdSno]);
                                    MiddleCmd middleCmd = new MiddleCmd();
                                    if(GetMiddleCmd(sCmdSno, ref middleCmd, db) != DBResult.Success)
                                    {
                                        clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, "NG: 取得MiddleCmd資料失敗 => " +
                                            $"<{Parameter.clsMiddleCmd.Column.CommandID}>{sCmdSno}");
                                        continue;
                                    }

                                    if (string.IsNullOrWhiteSpace(middleCmd.BatchID))
                                    {//單板命令

                                    }
                                    else
                                    {//雙板命令

                                    }
                                }

                                return false;
                            case DBResult.NoDataSelect:
                                return true;
                            default:
                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                                return false;
                        }
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
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }
    }
}
