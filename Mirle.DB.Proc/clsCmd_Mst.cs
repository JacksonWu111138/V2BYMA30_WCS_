using System;
using System.Data;
using Mirle.Def;
using Mirle.Structure;
using Mirle.DataBase;
using Mirle.WebAPI.V2BYMA30.ReportInfo;

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


        public bool FunMoveFinishCmdToHistory_Proc(bool PCBACycleRun)
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
                                string sJobID = Convert.ToString(dtTmp.Rows[i]["jobID"]);
                                CmdMstInfo nextCycleCmd = new CmdMstInfo();
                                CmdMstInfo lastCycleCmd = new CmdMstInfo();
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

                                if (PCBACycleRun && sJobID.Contains("CYCLERUN"))
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
                                            nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockOut;
                                            nextCycleCmd.Stn_No = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockIn, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            break;
                                        case clsConstValue.CmdMode.StockOut:
                                            nextCycleCmd.Cmd_Mode = clsConstValue.CmdMode.StockIn;
                                            nextCycleCmd.Loc = tool.FunGetCycleRunNextLocation(clsConstValue.CmdMode.StockOut, lastCycleCmd.Stn_No, lastCycleCmd.Loc);
                                            switch (lastCycleCmd.Equ_No)
                                            {
                                                case "1":
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
