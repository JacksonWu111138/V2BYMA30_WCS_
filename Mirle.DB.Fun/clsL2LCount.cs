using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mirle.DataBase;
using Mirle.Def;
using Mirle.Structure.Info;

namespace Mirle.DB.Fun
{
    public class clsL2LCount
    {
        private clsTool tool = new clsTool();
        public int FunSelectNeedToTeach(int MaxCount, ref DataTable dtTmp, DataBase.DB db)
        {
            try
            {
                string strSql = $"select * from {Parameter.clsL2LCount.TableName} where " +
                    $"{Parameter.clsL2LCount.Column.Count} >= {MaxCount}";
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if(iRet == DBResult.Exception)
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");

                return iRet;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return DBResult.Exception;
            }
        }

        public int CheckHasL2LRecord(string BoxID, ref L2LCountInfo info, ref string strEM, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsL2LCount.TableName} where " +
                    $"{Parameter.clsL2LCount.Column.BoxID} = '{BoxID}' and {Parameter.clsL2LCount.Column.RoundSts} = '{clsConstValue.RoundSts.Happen}' ";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);

                if (iRet == DBResult.Exception)
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                else if (iRet == DBResult.Success)
                {
                    info = new L2LCountInfo();
                    info = tool.Get2LCountInfo(dtTmp.Rows[0]);
                }
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
                dtTmp = null;
            }
        }

        public bool FunUpdL2LCount(string BoxID, string count, string HisLoc, ref string strEM, DataBase.DB db)
        {
            try
            {
                string strSql = $"update {Parameter.clsL2LCount.TableName} set {Parameter.clsL2LCount.Column.Count} = {count}, " +
                    $"{Parameter.clsL2LCount.Column.HisLoc} = {Parameter.clsL2LCount.Column.HisLoc} + ',' + '{HisLoc}', " +
                    $"{Parameter.clsL2LCount.Column.Update_Date} = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' where " +
                    $"{Parameter.clsL2LCount.Column.BoxID} = '{BoxID}' and {Parameter.clsL2LCount.Column.RoundSts} = '{clsConstValue.RoundSts.Happen}'";
                if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strSql);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public bool FunInsL2LCount(string BoxID, string EquNo, string oldShelf, string newShelf, ref string strEM, DataBase.DB db)
        {
            try
            {
                string strSql = $"insert into {Parameter.clsL2LCount.TableName} ({Parameter.clsL2LCount.Column.BoxID}, {Parameter.clsL2LCount.Column.Count}, " +
                    $"{Parameter.clsL2LCount.Column.EquNo}, {Parameter.clsL2LCount.Column.Create_Date}, " +
                    $"{Parameter.clsL2LCount.Column.RoundSts}, {Parameter.clsL2LCount.Column.HisLoc}) " +
                    $"values('{BoxID}', '1', '{EquNo}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{clsConstValue.RoundSts.Happen}', '{oldShelf},{newShelf}')";
                if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strSql);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public bool FunFinishL2LCount(string BoxID, string roundSts, string teachLoc, ref string strEM, DataBase.DB db)
        {
            try
            {
                string strSql = $"update {Parameter.clsL2LCount.TableName} set {Parameter.clsL2LCount.Column.RoundSts} = '{roundSts}', " +
                    $"{Parameter.clsL2LCount.Column.TeachLoc} = '{teachLoc}', {Parameter.clsL2LCount.Column.Update_Date} = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'  where " +
                    $"{Parameter.clsL2LCount.Column.BoxID} = '{BoxID}' and {Parameter.clsL2LCount.Column.RoundSts} = '{clsConstValue.RoundSts.Happen}' ";
                if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strSql);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }
    }
}
