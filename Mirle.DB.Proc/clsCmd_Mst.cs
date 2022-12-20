using System;
using System.Data;
using Mirle.Def;
using Mirle.Structure;
using Mirle.DataBase;
using Mirle.WebAPI.V2BYMA30.ReportInfo;
using Mirle.Def.U2NMMA30;
using Mirle.Structure.Info;
using static Mirle.Def.U2NMMA30.ConveyorDef;

namespace Mirle.DB.Proc
{
    public class clsCmd_Mst
    {
        private Fun.clsCmd_Mst CMD_MST = new Fun.clsCmd_Mst();
        private Fun.clsCMD_DTL CMD_DTL = new Fun.clsCMD_DTL();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        private clsSno sno;
        private clsDbConfig _config = new clsDbConfig();
        private WebApiConfig WesAPIConfig = new WebApiConfig();
        private DB.Fun.clsTool tool = new DB.Fun.clsTool();
        private Fun.clsL2LCount L2LCount = new Fun.clsL2LCount();
        public clsCmd_Mst(clsDbConfig config, WebApiConfig wesApi)
        {
            _config = config;
            sno = new clsSno(_config);
            WesAPIConfig = wesApi;
        }

        public bool FunGetCommand(string sCmdSno, ref CmdMstInfo cmd)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunGetCommand(sCmdSno, ref cmd, ref iRet, db);
                    }
                    else return false;
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

        public int FunGetCommand_byBoxID(string sBoxID, ref CmdMstInfo cmd)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunGetCommand_byBoxID(sBoxID, ref cmd, db);
                    }

                    return iRet;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return DBResult.Exception;
            }
        }

        public int FunGetCmdMst_Grid_AutoUpFile(ref DataTable dtTmp)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunGetCmdMst_Grid_AutoUpFile(ref dtTmp, db);
                    }
                    else return iRet;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        public int FunGetCmdMst_Grid(ref DataTable dtTmp)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunGetCmdMst_Grid(ref dtTmp, db);
                    }
                    else return iRet;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        public int FunCheckHasCommand(string sLoc, ref CmdMstInfo cmd)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunCheckHasCommand(sLoc, ref cmd, db);
                    }
                    else return iRet;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        public int FunCheckHasCommand(string sLoc, string sCmdSts, ref DataTable dtTmp)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunCheckHasCommand(sLoc, sCmdSts, ref dtTmp, db);
                    }
                    else return iRet;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        public bool FunCheckHasCommand_ByBoxID(string BoxId, ref CmdMstInfo cmd)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunCheckHasCommand_ByBoxID(BoxId, ref cmd, db);
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
        }

        public bool FunUpdateStnNo(string sCmdSno, string sStnNo, string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success) return CMD_MST.FunUpdateStnNo(sCmdSno, sStnNo, sRemark, db);
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunUpdatePry(string sBoxID, string Pry, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                        return CMD_MST.FunUpdatePry(sBoxID, Pry, ref strEM, db);
                    else
                    {
                        strEM = "開啟資料庫失敗！";
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
        }
        public bool FunUpdateboxStockOutAgv(string sCmdSno, string AgvPort)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                        return CMD_MST.FunUpdateboxStockOutAgv(sCmdSno, AgvPort, db);
                    else
                    {
                        throw new Exception("開啟資料庫失敗！");
                    }
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }
        
        public bool FunUpdateCmdSts(string sCmdSno, string sCmdSts, string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success) return CMD_MST.FunUpdateCmdSts(sCmdSno, sCmdSts, sRemark, db);
                    else return false;
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunUpdateCmdSts(string sCmdSno, string sCmdSts, clsEnum.Cmd_Abnormal abnormal, string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunUpdateCmdSts(sCmdSno, sCmdSts, abnormal, sRemark, db);
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
        }

        public bool FunUpdateRemark(string sCmdSno, string sRemark)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
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
        }

        public bool FunUpdateCurLoc(string sCmdSno, string sCurDeviceID, string sCurLoc)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunUpdateCurLoc(sCmdSno, sCurDeviceID, sCurLoc, db);
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
        }

        public bool FunInsCmdMst(CmdMstInfo stuCmdMst, ref string strErrMsg)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunInsCmdMst(stuCmdMst, ref strErrMsg, db);
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
        }

        public bool FunShelfReportToWes(string sCmdSno, LotShelfReportInfo info, ref string strEM)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        CmdMstInfo cmd = new CmdMstInfo();
                        string sRemark = "";
                        if (CMD_MST.FunGetCommand(sCmdSno, ref cmd, ref iRet, db))
                        {
                            string sRemark_Pre = cmd.Remark;
                            if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                            {
                                sRemark = "Error: Begin失敗！";
                                if (sRemark != sRemark_Pre)
                                {
                                    CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                }
                            }

                            if (!CMD_MST.FunUpdateLoc(sCmdSno, info.shelfId, db))
                            {
                                strEM = $"Error: 更新updateLoc失敗, jobId = {cmd.Cmd_Sno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                throw new Exception(strEM);
                            }

                            if (!api.GetLotShelfReport().FunReport(info, WesAPIConfig.IP))
                            {
                                strEM = $"Error: 向WES預約shelf失敗, jobId = {cmd.Cmd_Sno}.";
                                db.TransactionCtrl(TransactionTypes.Rollback);
                                throw new Exception(strEM);
                            }


                            db.TransactionCtrl(TransactionTypes.Commit);
                            return true;

                        }
                        strEM = $"Error: 取得命令失敗, jobId = {sCmdSno}.";
                        throw new Exception(strEM);

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
        }


        public bool FunMoveFinishCmdToHistory_Proc(bool PCBACycleRun, bool B800CycleRun)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        if (CMD_MST.FunGetFinishCommand(ref dtTmp, db) == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                string sCmdSno = Convert.ToString(dtTmp.Rows[i]["CmdSno"]);
                                string sRemark_Pre = Convert.ToString(dtTmp.Rows[i]["Remark"]);
                                string sRemark = "";
                                string sJobID = Convert.ToString(dtTmp.Rows[i]["JobID"]);
                                string sCmdMode = Convert.ToString(dtTmp.Rows[i]["CmdMode"]);
                                string sBoxID = Convert.ToString(dtTmp.Rows[i]["BoxId"]);
                                string sEquNo = Convert.ToString(dtTmp.Rows[i]["EquNO"]);
                                string sLoc = Convert.ToString(dtTmp.Rows[i]["Loc"]);
                                string sNewLoc = Convert.ToString(dtTmp.Rows[i]["NewLoc"]);
                                int sShelfRow = 0 ;
                                if (string.IsNullOrEmpty(sNewLoc) && sCmdMode == clsConstValue.CmdMode.L2L)
                                    sShelfRow = Convert.ToInt32(sNewLoc.Substring(0, 2));
                                CmdMstInfo nextCycleCmd = new CmdMstInfo();
                                CmdMstInfo lastCycleCmd = new CmdMstInfo();
                                CmdMstInfo TeachLocCmd = new CmdMstInfo();
                                L2LCountInfo L2LCountInfo = new L2LCountInfo();
                                if (!FunGetCommand(sCmdSno, ref lastCycleCmd))
                                {
                                    sRemark = "Error: 取得CycleRun先前命令失敗.";
                                    if (FunUpdateRemark(sCmdSno, sRemark))
                                        continue;
                                    else
                                        continue;
                                }

                                /*iRet = CMD_DTL.FunGetCmdDtl(sCmdSno, db);
                                if(iRet == DBResult.Exception)
                                {
                                    sRemark = "Error: 取得命令明細資料失敗";
                                    if (sRemark != sRemark_Pre)
                                    {
                                        CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                    }

                                    continue;
                                }*/

                                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                                {
                                    sRemark = "Error: Begin失敗！";
                                    if (sRemark != sRemark_Pre)
                                    {
                                        CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                    }

                                    continue;
                                }

                                if (!CMD_MST.FunInsertCMD_MST_His(sCmdSno, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    continue;
                                }

                                if (!CMD_MST.FunDelCmdMst(sCmdSno, db))
                                {
                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                    continue;
                                }

                                if (PCBACycleRun && sJobID.Contains("CYCLERUN") && !sJobID.Contains("_B80"))
                                {
                                    nextCycleCmd = lastCycleCmd;
                                    //nextCycleCmd.Cmd_Sno = sno.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                                    nextCycleCmd.CurLoc = "";
                                    nextCycleCmd.CurDeviceID = "";
                                    nextCycleCmd.Cmd_Sts = clsConstValue.CmdSts.strCmd_Initial;
                                    nextCycleCmd.Remark = "";
                                    nextCycleCmd.End_Date = "";
                                    if (string.IsNullOrWhiteSpace(nextCycleCmd.Cmd_Sno))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        sRemark = $"Cycle Run 取得下筆命令序號失敗！";
                                        if(sRemark != sRemark_Pre)
                                            FunUpdateRemark(sCmdSno, sRemark);
                                        continue;
                                    }

                                    switch (lastCycleCmd.Cmd_Mode)
                                    {
                                        case clsConstValue.CmdMode.StockIn:
                                            if(lastCycleCmd.JobID.Contains("L2L"))
                                            {
                                                nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.L2L;
                                                nextCycleCmd.New_Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                                nextCycleCmd.Prty = "9";
                                                nextCycleCmd.Stn_No = "";
                                            }
                                            else
                                            {
                                                nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                                                nextCycleCmd.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            }
                                            break;
                                        case clsConstValue.CmdMode.StockOut:
                                            nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                                            nextCycleCmd.Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockOut, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            switch (lastCycleCmd.Equ_No)
                                            {
                                                case "1":
                                                    if(!lastCycleCmd.JobID.Contains("L2L"))
                                                        nextCycleCmd.Equ_No = "2";
                                                    break;
                                                case "2":
                                                    nextCycleCmd.Equ_No = "1";
                                                    break;
                                                case "3":
                                                case "4":
                                                case "5":
                                                    break;
                                                default:
                                                    db.TransactionCtrl(TransactionTypes.Rollback);
                                                    sRemark = $"Error: CycleRun先前EquNo有誤, EquNo = {lastCycleCmd.Equ_No}";
                                                    FunUpdateRemark(sCmdSno, sRemark);
                                                    continue;
                                            }
                                            
                                            break;
                                        case clsConstValue.CmdMode.L2L:
                                            nextCycleCmd.Loc = lastCycleCmd.New_Loc;
                                            nextCycleCmd.New_Loc = tool.FunGetCycleRunNextLocation(lastCycleCmd.Cmd_Mode, lastCycleCmd.Stn_No, lastCycleCmd.New_Loc);
                                            if(nextCycleCmd.New_Loc == ConveyorDef.AGV.M1_20.BufferName) //六筆需出庫
                                            {
                                                nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                                                nextCycleCmd.Stn_No = ConveyorDef.AGV.M1_20.BufferName;
                                                nextCycleCmd.Prty = "5";
                                                nextCycleCmd.New_Loc = "";
                                            }
                                            break;
                                        default:
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: CycleRun先前命令模式有誤, cmdMode = {lastCycleCmd.Cmd_Mode}.";
                                            if (FunUpdateRemark(sCmdSno, sRemark))
                                                continue;
                                            else
                                                continue;
                                    }

                                    if (!CMD_MST.FunInsCmdMst(nextCycleCmd, ref sRemark, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        continue;
                                    }
                                }
                                else if ((B800CycleRun && sJobID.Contains("CYCLERUN") && sJobID.Contains("_B80")))
                                {
                                    nextCycleCmd = lastCycleCmd;

                                    nextCycleCmd.CurLoc = "";
                                    nextCycleCmd.CurDeviceID = "";
                                    nextCycleCmd.Cmd_Sts = clsConstValue.CmdSts.strCmd_Initial;
                                    nextCycleCmd.Remark = "";
                                    nextCycleCmd.End_Date = "";

                                    switch (lastCycleCmd.Cmd_Mode)
                                    {
                                        case clsConstValue.CmdMode.StockIn:
                                            nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                                            nextCycleCmd.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            break;
                                        case clsConstValue.CmdMode.StockOut:
                                            nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                                            nextCycleCmd.Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockOut, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            break;
                                        case clsConstValue.CmdMode.L2L:
                                            string[] nextLocation = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.L2L, lastCycleCmd.Loc, lastCycleCmd.New_Loc).Split(',');
                                            if(nextLocation.Length == 2)
                                            {
                                                nextCycleCmd.Loc = nextLocation[0];
                                                nextCycleCmd.New_Loc = nextLocation[1];
                                            }
                                            else if(nextLocation.Length == 1)
                                            {
                                                nextCycleCmd.Loc = lastCycleCmd.New_Loc;
                                                nextCycleCmd.New_Loc = nextLocation[0];
                                            }
                                            else
                                            {
                                                throw new Exception($"Error: 取得B800 庫對庫命令下個位置有誤. nextLocation = {nextLocation}");
                                            }
                                            break;
                                        default:
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: CycleRun先前命令模式有誤, cmdMode = {lastCycleCmd.Cmd_Mode}.";
                                            if (FunUpdateRemark(sCmdSno, sRemark))
                                                continue;
                                            else
                                                continue;
                                    }

                                    if (!CMD_MST.FunInsCmdMst(nextCycleCmd, ref sRemark, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        continue;
                                    }
                                }
                                else if (sJobID == "TEACH_LOC_MOVING_BACK")
                                {
                                    if (!L2LCount.FunFinishL2LCount(sBoxID, clsConstValue.RoundSts.Teach, sLoc, ref sRemark, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        sRemark = $"Error: 完成L2LCount命令失敗, BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                        if (sRemark != sRemark_Pre)
                                            CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                        continue;
                                    }
                                }
                                else if (sJobID == "TEACH_LOC_MOVING")
                                {
                                    //尚未撰寫
                                    //建立從校正儲位回原儲位命令
                                    TeachLocCmd.Cmd_Sno = sno.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                                    if (string.IsNullOrWhiteSpace(TeachLocCmd.Cmd_Sno))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        sRemark = $"送往校正儲位命令 取得序號失敗！ BoxId = {sBoxID} and jobId = {sCmdSno}.";
                                        if (sRemark != sRemark_Pre)
                                            CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                        continue;
                                    }
                                    TeachLocCmd.BoxID = sBoxID;
                                    TeachLocCmd.Cmd_Mode = clsConstValue.CmdMode.L2L;
                                    TeachLocCmd.CurDeviceID = "";
                                    TeachLocCmd.CurLoc = "";
                                    TeachLocCmd.End_Date = "";
                                    TeachLocCmd.Loc = sNewLoc;
                                    TeachLocCmd.Equ_No = sEquNo;
                                    TeachLocCmd.EXP_Date = "";
                                    TeachLocCmd.JobID = "TEACH_LOC_MOVING_BACK";
                                    TeachLocCmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();

                                    TeachLocCmd.New_Loc = sLoc;

                                    TeachLocCmd.Prty = "3";
                                    TeachLocCmd.Remark = "";
                                    TeachLocCmd.Stn_No = "";
                                    TeachLocCmd.Host_Name = "WCS";
                                    TeachLocCmd.Zone_ID = "";
                                    TeachLocCmd.carrierType = sShelfRow < 9 ? "MAG" : "BIN";
                                    TeachLocCmd.lotSize = "";

                                    if (!CMD_MST.FunInsCmdMst(TeachLocCmd, ref sRemark, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        if (sRemark != sRemark_Pre)
                                            CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                        continue;
                                    }
                                }
                                else if (sCmdMode == clsConstValue.CmdMode.StockOut)
                                {
                                    iRet = L2LCount.CheckHasL2LRecord(sBoxID, ref L2LCountInfo, ref sRemark, db);
                                    if (iRet == DBResult.Success)
                                    {
                                        if(!L2LCount.FunFinishL2LCount(sBoxID, clsConstValue.RoundSts.StockOut, "", ref sRemark, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: 完成L2LCount中命令失敗，BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                            if(sRemark != sRemark_Pre)
                                                CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                            continue;
                                        }
                                    }
                                    else if (iRet == DBResult.NoDataSelect)
                                    {
                                        //不做事
                                    }
                                    else
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        sRemark = $"Error: 取得L2LCount中命令失敗，BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                        if (sRemark != sRemark_Pre)
                                            CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                        continue;
                                    }
                                }
                                else if (sCmdMode == clsConstValue.CmdMode.L2L)
                                {
                                    iRet = L2LCount.CheckHasL2LRecord(sBoxID, ref L2LCountInfo, ref sRemark, db);
                                    if (iRet == DBResult.NoDataSelect)
                                    {
                                        if (!L2LCount.FunInsL2LCount(sBoxID, sEquNo, sLoc, sNewLoc, ref sRemark, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: 輸入新L2LCount中命令失敗，BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                            if (sRemark != sRemark_Pre)
                                                CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                            continue;
                                        }
                                    }
                                    else if (iRet == DBResult.Success)
                                    {

                                        if (!L2LCount.FunUpdL2LCount(sBoxID, (Convert.ToInt32(L2LCountInfo.Count) + 1).ToString(), sNewLoc, ref sRemark, db))
                                        {
                                            db.TransactionCtrl(TransactionTypes.Rollback);
                                            sRemark = $"Error: 更新L2LCount中命令失敗，BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                            if (sRemark != sRemark_Pre)
                                                CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                            continue;
                                        }
                                        if (Convert.ToInt32(L2LCountInfo.Count) >= 4)
                                        {
                                            //L2LCountInfo.Count == 4，因為L2LCountInfo.Count > 4的L2LCountInfo.RoundSts不該被選為「未完成紀錄」
                                            //放入去校正儲位之庫對庫命令
                                            //JobID == TEACH_LOC_MOVING
                                            string sTeachLoc;

                                            //選擇校正儲位
                                            switch(sShelfRow)
                                            {
                                                case 1:
                                                case 3:
                                                    sTeachLoc = ConveyorDef.TeachLoc.M801Left;
                                                    break;
                                                case 2:
                                                case 4:
                                                    sTeachLoc = ConveyorDef.TeachLoc.M801Right;
                                                    break;
                                                case 5:
                                                case 7:
                                                    sTeachLoc = ConveyorDef.TeachLoc.M802Left;
                                                    break;
                                                case 6:
                                                case 8:
                                                    sTeachLoc = ConveyorDef.TeachLoc.M802Right;
                                                    break;
                                                case 9:
                                                case 11:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B801Left;
                                                    break;
                                                case 10:
                                                case 12:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B801Right;
                                                    break;
                                                case 13:
                                                case 15:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B802Left;
                                                    break;
                                                case 14:
                                                case 16:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B802Right;
                                                    break;
                                                case 17:
                                                case 19:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B803Left;
                                                    break;
                                                case 18:
                                                case 20:
                                                    sTeachLoc = ConveyorDef.TeachLoc.B803Right;
                                                    break;
                                                default:
                                                    sRemark = $"Error: 取得對應校正儲位失敗，ShelfId = {sNewLoc} and cmdSno = {sCmdSno}.";
                                                    if (sRemark != sRemark_Pre)
                                                        CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                                    continue;
                                            }

                                            //建立前往校正儲位命令
                                            TeachLocCmd.Cmd_Sno = sno.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO);
                                            if (string.IsNullOrWhiteSpace(TeachLocCmd.Cmd_Sno))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                sRemark = $"送往校正儲位命令 取得序號失敗！ BoxId = {sBoxID} and jobId = {sCmdSno}.";
                                                if (sRemark != sRemark_Pre)
                                                    CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                                continue;
                                            }
                                            TeachLocCmd.BoxID = sBoxID;
                                            TeachLocCmd.Cmd_Sts = clsConstValue.CmdSts.strCmd_Initial;
                                            TeachLocCmd.Cmd_Mode = clsConstValue.CmdMode.L2L;
                                            TeachLocCmd.CurDeviceID = "";
                                            TeachLocCmd.CurLoc = "";
                                            TeachLocCmd.End_Date = "";
                                            TeachLocCmd.Loc = sNewLoc;
                                            TeachLocCmd.Equ_No = sEquNo;
                                            TeachLocCmd.EXP_Date = "";
                                            TeachLocCmd.JobID = "TEACH_LOC_MOVING";
                                            TeachLocCmd.NeedShelfToShelf = clsEnum.NeedL2L.N.ToString();
                                            TeachLocCmd.Crt_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                            TeachLocCmd.New_Loc = sTeachLoc;

                                            TeachLocCmd.Prty = "4";
                                            TeachLocCmd.Remark = "";
                                            TeachLocCmd.Stn_No = "";
                                            TeachLocCmd.Host_Name = "WCS";
                                            TeachLocCmd.Zone_ID = "";
                                            TeachLocCmd.carrierType = sShelfRow < 9? "MAG" : "BIN";
                                            TeachLocCmd.lotSize = "";

                                            if(!CMD_MST.FunInsCmdMst(TeachLocCmd, ref sRemark, db))
                                            {
                                                db.TransactionCtrl(TransactionTypes.Rollback);
                                                if (sRemark != sRemark_Pre)
                                                    CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                                continue;
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        sRemark = $"Error: 取得L2LCount中命令失敗，BoxId = {sBoxID} and cmdSno = {sCmdSno}.";
                                        if (sRemark != sRemark_Pre)
                                            CMD_MST.FunUpdateRemark(sCmdSno, sRemark, db);
                                        continue;
                                    }
                                }
                                /*if (iRet == DBResult.Success)
                                {
                                    if (!CMD_DTL.FunInsertCmd_Dtl_His(sCmdSno, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        continue;
                                    }

                                    if (!CMD_DTL.FunDelCmd_Dtl(sCmdSno, db))
                                    {
                                        db.TransactionCtrl(TransactionTypes.Rollback);
                                        continue;
                                    }
                                }*/

                                db.TransactionCtrl(TransactionTypes.Commit);
                                return true;
                            }

                            return false;
                        }
                        else return false;
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
                dtTmp = null;
            }
        }
        

        public bool FunDelCMD_MST_His(double dblDay)
        {
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        return CMD_MST.FunDelCMD_MST_His(dblDay, db);
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
        }

        
    }
}
