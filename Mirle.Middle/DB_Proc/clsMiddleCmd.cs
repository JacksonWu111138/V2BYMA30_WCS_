using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.DataBase;
using System.Data;

namespace Mirle.Middle.DB_Proc
{
    public class clsMiddleCmd
    {
        private clsDbConfig _config = new clsDbConfig();
        private clsTool tool;
        private clsEquCmd EquCmd;
        public clsMiddleCmd(clsDbConfig config, DeviceInfo[] PCBA, DeviceInfo[] Box)
        {
            tool = new clsTool(PCBA, Box);
            EquCmd = new clsEquCmd(config);
            _config = config;
        }

        public bool FunUpdateRemark(string sCmdSno, string sRemark, DB db)
        {
            try
            {
                string strSql = $"update {Parameter.clsMiddleCmd.TableName} set " +
                    $"{Parameter.clsMiddleCmd.Column.Remark} = N'" + sRemark +
                    $"' where {Parameter.clsMiddleCmd.Column.CommandID} = '{sCmdSno}'";

                string strEM = "";
                if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strSql);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strSql + " => " + strEM);
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

        public bool FunUpdateCmdSts(string sCmdSno, string sCmdSts, string sRemark, DB db)
        {
            try
            {
                string strSql = $"update {Parameter.clsMiddleCmd.TableName} set" +
                    $" {Parameter.clsMiddleCmd.Column.Remark} = N'" + sRemark +
                    $"', {Parameter.clsMiddleCmd.Column.CmdSts} = '{sCmdSts}' ";

                if (sCmdSts == clsConstValue.CmdSts_MiddleCmd.strCmd_Cancel_Wait || sCmdSts == clsConstValue.CmdSts_MiddleCmd.strCmd_Finish_Wait)
                {
                    strSql += $", {Parameter.clsMiddleCmd.Column.EndDate} = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                }
                else
                {
                    strSql += $", {Parameter.clsMiddleCmd.Column.Expose_Date} = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                }

                strSql += $" where {Parameter.clsMiddleCmd.Column.CommandID} = '{sCmdSno}' ";

                string strEM = "";
                if (db.ExecuteSQL(strSql, ref strEM) == DBResult.Success)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Trace, strSql);
                    return true;
                }
                else
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, strSql + " => " + strEM);
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

        public bool MiddleCmd_Proc()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        string strSql = $"select * from {Parameter.clsMiddleCmd.TableName} where " +
                            $"{Parameter.clsMiddleCmd.Column.CmdSts} < '{clsConstValue.CmdSts_MiddleCmd.strCmd_WriteDeviceCmd}' " +
                            $"ORDER BY {Parameter.clsMiddleCmd.Column.Priority}, {Parameter.clsMiddleCmd.Column.Create_Date}, " +
                            $"{Parameter.clsMiddleCmd.Column.CommandID}";
                        string strEM = "";
                        iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                        if (iRet == DBResult.Success)
                        {
                            string sRemark = "";
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                MiddleCmd cmd = tool.GetMiddleCmd(dtTmp.Rows[i]);
                                DeviceInfo device = new DeviceInfo(); clsEnum.AsrsType type = clsEnum.AsrsType.None;
                                if (tool.CheckDeviceMatchCraneDevice(cmd.DeviceID, ref device, ref type))
                                {
                                    if (!EquCmd.funCheckCanInsertEquCmd(cmd.DeviceID, db))
                                    {
                                        sRemark = $"Error: 等候Line{cmd.DeviceID}命令完成";
                                        if (sRemark != cmd.Remark)
                                        {
                                            FunUpdateRemark(cmd.CommandID, sRemark, db);
                                        }

                                        continue;
                                    }

                                    if(type == clsEnum.AsrsType.Box)
                                    {//需要考慮左右

                                    }
                                    else
                                    {
                                        if(cmd.CmdSts == clsConstValue.CmdSts_MiddleCmd.strCmd_Initial)
                                        {
                                            switch(cmd.CmdMode)
                                            {
                                                case clsConstValue.CmdMode.StockIn:
                                                case clsConstValue.CmdMode.L2L:
                                                    EquCmdInfo equCmd = new EquCmdInfo
                                                    {
                                                        CmdSno = cmd.CommandID,
                                                        CmdMode = cmd.CmdMode,
                                                        CmdSts = cmd.CmdSts,
                                                        CarNo = "1",
                                                        EquNo = cmd.DeviceID,
                                                        LocSize=" ",
                                                        Priority=cmd.Priority.ToString(),
                                                        SpeedLevel = "5",
                                                        
                                                    };

                                                    if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                    {
                                                        sRemark = "Error: Begin失敗！";
                                                        if (sRemark != cmd.Remark)
                                                        {
                                                            FunUpdateRemark(cmd.CommandID, sRemark, db);
                                                        }

                                                        continue;
                                                    }

                                                    if (!FunUpdateCmdSts(cmd.CommandID, clsConstValue.CmdSts_MiddleCmd.strCmd_WriteDeviceCmd, sRemark, db))
                                                    {
                                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                                        continue;
                                                    }

                                                    break;
                                                case clsConstValue.CmdMode.StockOut:
                                                case clsConstValue.CmdMode.S2S:
                                                    break;
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        else if (iRet == DBResult.Exception) clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                        else { }

                        return false;
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
