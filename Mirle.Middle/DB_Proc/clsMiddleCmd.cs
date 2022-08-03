using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.DataBase;
using System.Data;
using Mirle.WebAPI.V2BYMA30.ReportInfo;

namespace Mirle.Middle.DB_Proc
{
    public class clsMiddleCmd
    {
        private clsDbConfig _config = new clsDbConfig();
        private clsTool tool;
        private clsEquCmd EquCmd;
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        private static List<ConveyorInfo> Node_All = new List<ConveyorInfo>();
        public clsMiddleCmd(clsDbConfig config, DeviceInfo[] PCBA, DeviceInfo[] Box, List<ConveyorInfo> conveyors)
        {
            tool = new clsTool(PCBA, Box);
            EquCmd = new clsEquCmd(config);
            _config = config;
            Node_All = conveyors;
        }

        public ConveyorInfo GetCV_ByCmdLoc(MiddleCmd cmd, string Loc, DB db)
        {
            var lst = Node_All.Where(r => r.BufferName == Loc);
            if (lst == null || lst.Count() == 0)
            {
                string sRemark = $"Error: {Loc}不存在在所有節點裡";
                if (sRemark != cmd.Remark)
                {
                    FunUpdateRemark(cmd.CommandID, sRemark, db);
                }

                return new ConveyorInfo();
            }

            ConveyorInfo conveyor = new ConveyorInfo();
            foreach (var cv in lst)
            {
                conveyor = cv;
                break;
            }

            return conveyor;
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
                                    {
                                        bool IsBatch = !string.IsNullOrWhiteSpace(cmd.BatchID);
                                        if (IsBatch)
                                        {
                                            var batch = from myRow in dtTmp.AsEnumerable()
                                                        where myRow.Field<string>(Parameter.clsMiddleCmd.Column.BatchID) == cmd.BatchID
                                                        select myRow;
                                            MiddleCmd[] BatchCmd = new MiddleCmd[2];
                                            int idx = 0;
                                            foreach(var data in batch)
                                            {
                                                if (idx > 1) break;

                                                BatchCmd[idx] = tool.GetMiddleCmd(data);
                                                idx++;
                                            }

                                            if (cmd.CmdSts == clsConstValue.CmdSts_MiddleCmd.strCmd_Initial)
                                            {
                                                switch (cmd.CmdMode)
                                                {
                                                    case clsConstValue.CmdMode.StockIn:
                                                    case clsConstValue.CmdMode.L2L:
                                                        EquCmdInfo equCmd = new EquCmdInfo
                                                        {
                                                            CmdSno = cmd.CommandID,
                                                            CmdMode = cmd.CmdMode,
                                                            CmdSts = clsConstValue.CmdSts.strCmd_Initial,
                                                            CarNo = "1",
                                                            EquNo = cmd.DeviceID,
                                                            LocSize = " ",
                                                            Priority = cmd.Priority.ToString(),
                                                            SpeedLevel = "5",
                                                            Destination = tool.GetEquCmdLoc_BySysCmd(cmd.Destination)
                                                        };

                                                        if (cmd.CmdMode == clsConstValue.CmdMode.StockIn)
                                                        {
                                                            ConveyorInfo[] conveyors = new ConveyorInfo[2];


                                                            var obj = Node_All.Where(r => r.BufferName == cmd.Source);
                                                            if (obj == null || obj.Count() == 0)
                                                            {
                                                                sRemark = "Error: Source站口不存在在所有節點裡";
                                                                if (sRemark != cmd.Remark)
                                                                {
                                                                    FunUpdateRemark(cmd.CommandID, sRemark, db);
                                                                }

                                                                return false;
                                                            }

                                                            foreach (var j in obj)
                                                            {
                                                                equCmd.Source = j.StkPortID.ToString();
                                                            }
                                                        }
                                                        else equCmd.Source = tool.GetEquCmdLoc_BySysCmd(cmd.Source);

                                                        return FunWriEquCmd_Proc(cmd, equCmd, db);

                                                    case clsConstValue.CmdMode.StockOut:
                                                    case clsConstValue.CmdMode.S2S:
                                                        ConveyorInfo conveyor = GetCV_ByCmdLoc(cmd, cmd.Destination, db);
                                                        if (string.IsNullOrWhiteSpace(conveyor.BufferName)) return false;

                                                        CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                                                        {
                                                            bufferId = conveyor.BufferName,
                                                            jobId = cmd.CommandID
                                                        };

                                                        if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                                        {
                                                            sRemark = "Error: Begin失敗！";
                                                            if (sRemark != cmd.Remark)
                                                            {
                                                                FunUpdateRemark(cmd.CommandID, sRemark, db);
                                                            }

                                                            return false;
                                                        }

                                                        sRemark = $"預約{conveyor.BufferName}";
                                                        if (!FunUpdateCmdSts(cmd.CommandID, clsConstValue.CmdSts_MiddleCmd.strCmd_WriteCV, sRemark, db))
                                                        {
                                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                                            return false;
                                                        }

                                                        if (!api.GetCV_ReceiveNewBinCmd().FunReport(info, conveyor.API.IP))
                                                        {
                                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                                            sRemark = $"Error: 預約{conveyor.BufferName}失敗！";
                                                            if (sRemark != cmd.Remark)
                                                            {
                                                                FunUpdateRemark(cmd.CommandID, sRemark, db);
                                                            }

                                                            return false;
                                                        }

                                                        db.TransactionCtrl(TransactionTypes.Commit);
                                                        return true;
                                                    default:
                                                        return false;
                                                }
                                            }
                                            else
                                            {

                                            }
                                        }
                                        else
                                        {
                                            if (FunSingleAsrsCmdProc(cmd, db)) return true;
                                            else continue;
                                        }
                                    }
                                    else
                                    {
                                        if (FunSingleAsrsCmdProc(cmd, db)) return true;
                                        else continue;
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

        public bool FunSingleAsrsCmdProc(MiddleCmd cmd, DB db)
        {
            try
            {
                string sRemark = "";
                if (cmd.CmdSts == clsConstValue.CmdSts_MiddleCmd.strCmd_Initial)
                {
                    switch (cmd.CmdMode)
                    {
                        case clsConstValue.CmdMode.StockIn:
                        case clsConstValue.CmdMode.L2L:
                            EquCmdInfo equCmd = new EquCmdInfo
                            {
                                CmdSno = cmd.CommandID,
                                CmdMode = cmd.CmdMode,
                                CmdSts = clsConstValue.CmdSts.strCmd_Initial,
                                CarNo = "1",
                                EquNo = cmd.DeviceID,
                                LocSize = " ",
                                Priority = cmd.Priority.ToString(),
                                SpeedLevel = "5",
                                Destination = tool.GetEquCmdLoc_BySysCmd(cmd.Destination)
                            };

                            if (cmd.CmdMode == clsConstValue.CmdMode.StockIn)
                            {
                                var obj = Node_All.Where(r => r.BufferName == cmd.Source);
                                if (obj == null || obj.Count() == 0)
                                {
                                    sRemark = "Error: Source站口不存在在所有節點裡";
                                    if (sRemark != cmd.Remark)
                                    {
                                        FunUpdateRemark(cmd.CommandID, sRemark, db);
                                    }

                                    return false;
                                }

                                foreach (var j in obj)
                                {
                                    equCmd.Source = j.StkPortID.ToString();
                                }
                            }
                            else equCmd.Source = tool.GetEquCmdLoc_BySysCmd(cmd.Source);

                            return FunWriEquCmd_Proc(cmd, equCmd, db);

                        case clsConstValue.CmdMode.StockOut:
                        case clsConstValue.CmdMode.S2S:
                            ConveyorInfo conveyor = GetCV_ByCmdLoc(cmd, cmd.Destination, db);
                            if (string.IsNullOrWhiteSpace(conveyor.BufferName)) return false;

                            CVReceiveNewBinCmdInfo info = new CVReceiveNewBinCmdInfo
                            {
                                bufferId = conveyor.BufferName,
                                jobId = cmd.CommandID
                            };

                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                            {
                                sRemark = "Error: Begin失敗！";
                                if (sRemark != cmd.Remark)
                                {
                                    FunUpdateRemark(cmd.CommandID, sRemark, db);
                                }

                                return false;
                            }

                            sRemark = $"預約{conveyor.BufferName}";
                            if (!FunUpdateCmdSts(cmd.CommandID, clsConstValue.CmdSts_MiddleCmd.strCmd_WriteCV, sRemark, db))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                return false;
                            }

                            if (!api.GetCV_ReceiveNewBinCmd().FunReport(info, conveyor.API.IP))
                            {
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                sRemark = $"Error: 預約{conveyor.BufferName}失敗！";
                                if (sRemark != cmd.Remark)
                                {
                                    FunUpdateRemark(cmd.CommandID, sRemark, db);
                                }

                                return false;
                            }

                            db.TransactionCtrl(TransactionTypes.Commit);
                            return true;
                        default:
                            return false;
                    }
                }
                else
                {//CmdSts=1 已預約目的站
                    switch (cmd.CmdMode)
                    {
                        case clsConstValue.CmdMode.L2L:
                        case clsConstValue.CmdMode.StockIn:
                            return FunUpdateCmdSts(cmd.CommandID, clsConstValue.CmdSts_MiddleCmd.strCmd_Initial, "", db);
                    }

                    ConveyorInfo conveyor_From = new ConveyorInfo();
                    if (cmd.CmdMode == clsConstValue.CmdMode.S2S)
                    {
                        conveyor_From = GetCV_ByCmdLoc(cmd, cmd.Source, db);
                        if (string.IsNullOrWhiteSpace(conveyor_From.BufferName)) return false;
                    }

                    ConveyorInfo conveyor_To = GetCV_ByCmdLoc(cmd, cmd.Destination, db);
                    if (string.IsNullOrWhiteSpace(conveyor_To.BufferName)) return false;

                    EquCmdInfo equCmd = new EquCmdInfo
                    {
                        CmdSno = cmd.CommandID,
                        CmdMode = cmd.CmdMode,
                        CmdSts = clsConstValue.CmdSts.strCmd_Initial,
                        CarNo = "1",
                        EquNo = cmd.DeviceID,
                        LocSize = " ",
                        Priority = cmd.Priority.ToString(),
                        SpeedLevel = "5",
                        Destination = conveyor_To.StkPortID.ToString()
                    };

                    if (cmd.CmdMode == clsConstValue.CmdMode.S2S) equCmd.Source = conveyor_From.StkPortID.ToString();
                    else equCmd.Source = tool.GetEquCmdLoc_BySysCmd(cmd.Source);

                    return FunWriEquCmd_Proc(cmd, equCmd, db);
                }
            }
            catch (Exception ex)
            {
                db.TransactionCtrl(TransactionTypes.Rollback);
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public bool FunWriEquCmd_Proc(MiddleCmd cmd, EquCmdInfo equCmd, DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        FunUpdateRemark(cmd.CommandID, sRemark, db);
                    }

                    return false;
                }

                sRemark = "下達EquCmd";
                if (!FunUpdateCmdSts(cmd.CommandID, clsConstValue.CmdSts_MiddleCmd.strCmd_WriteDeviceCmd, sRemark, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!EquCmd.FunInsEquCmd(equCmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 下達EquCmd失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        FunUpdateRemark(cmd.CommandID, sRemark, db);
                    }

                    return false;
                }

                db.TransactionCtrl(TransactionTypes.Commit);
                return true;
            }
            catch (Exception ex)
            {
                db.TransactionCtrl(TransactionTypes.Rollback);
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }
    }
}
