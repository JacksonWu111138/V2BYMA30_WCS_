﻿using Mirle.DataBase;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using Mirle.Def;
using System.Text;
using System.Threading.Tasks;
using Mirle.WebAPI.V2BYMA30.ReportInfo;

namespace Mirle.DB.Fun
{
    public class clsProc
    {
        private clsCmd_Mst Cmd_Mst = new clsCmd_Mst();
        private clsLocMst LocMst = new clsLocMst();
        private clsLocDtl LocDtl = new clsLocDtl();
        private clsTrnLog TrnLog = new clsTrnLog();
        private clsTool tool = new clsTool();
        private clsMiddleCmd MiddleCmd = new clsMiddleCmd();
        public bool FunDoubleStorage_SingleProc(CmdMstInfo cmd, MiddleCmd middleCmd, string sNewLoc, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (string.IsNullOrWhiteSpace(sNewLoc))
                {
                    sRemark = "Error: 二重格找不到新儲位";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                CarrierShelfReportInfo info = new CarrierShelfReportInfo
                {
                    jobId = cmd.JobID,
                    shelfId = sNewLoc,
                    shelfStatus = clsConstValue.LocSts.IN,
                    carrierId = cmd.BoxID,
                    disableLocation = clsConstValue.YesNo.Yes
                };

                int EquNo_New = tool.funGetEquNoByLoc(sNewLoc);
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (cmd.Cmd_Mode == clsConstValue.CmdMode.L2L)
                {
                    if (!Cmd_Mst.FunUpdateNewLocForL2L(cmd.Cmd_Sno, sNewLoc, middleCmd.DeviceID,
                        Location.LocationID.LeftFork.ToString(), db))
                    {
                        db.TransactionCtrl(TransactionTypes.Rollback);
                        sRemark = "Error: 二重格更新新儲位失敗";
                        if (sRemark != cmd.Remark)
                        {
                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                        }

                        return false;
                    }
                }
                else
                {
                    if (!Cmd_Mst.FunUpdateLoc(cmd.Cmd_Sno, sNewLoc, EquNo_New.ToString(),
                        middleCmd.DeviceID, Location.LocationID.LeftFork.ToString(), db))
                    {
                        db.TransactionCtrl(TransactionTypes.Rollback);
                        sRemark = "Error: 二重格更新新儲位失敗";
                        if (sRemark != cmd.Remark)
                        {
                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                        }

                        return false;
                    }
                }

                if (!MiddleCmd.FunInsertHisMiddleCmd(cmd.Cmd_Sno, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: Insert MiddleCmd_His失敗";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!MiddleCmd.FunDelMiddleCmd(cmd.Cmd_Sno, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 刪除MiddleCmd失敗";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmd_Mode_in(CmdMstInfo cmd, List<LocDtlInfo> locDtls,
           List<TrnLogInfo> trnLogs, List<MoldUseLogInfo> moldUseLogs, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if(sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finished, "過帳完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocSts_Mode_In(cmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!LocDtl.FunLocDtl_Mode_In(cmd, locDtls, trnLogs, moldUseLogs, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位明細失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmdCnl_Mode_in(CmdMstInfo cmd, List<LocDtlInfo> locDtls, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancelled, "過帳取消完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocStsCnl_Mode_In(cmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!LocDtl.FunLocDtlCnl_Mode_In(cmd, locDtls, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位明細失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmd_Mode_Out(CmdMstInfo cmd, List<LocDtlInfo> locDtls,
          List<TrnLogInfo> trnLogs, List<MoldUseLogInfo> moldUseLogs, DataBase.DB db)
        {
            try
            {
                string sRemark = ""; string strLocDD = "";
                int iRet = LocMst.funCheckLocDDSts(cmd.Loc, out strLocDD, db);
                if(iRet != DBResult.Success)
                {
                    sRemark = $"Error: 找尋對照儲位失敗 => {cmd.Loc}";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finished, "過帳完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocSts_Mode_Out(cmd, strLocDD, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if(!LocDtl.FunLocDtl_Mode_Out(cmd, locDtls, trnLogs, moldUseLogs, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位明細失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                db.TransactionCtrl(TransactionTypes.Commit);

                string strLocDtl13 = "";
                foreach(var locdtl in locDtls)
                {
                    strLocDtl13 = locdtl.UsedStatus;
                    break;
                }

                LocMst.FunLocSts_S_LocDtl_Null(cmd, strLocDtl13, db);

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

        public bool FunUpdateCmdCnl_Mode_Out(CmdMstInfo cmd, List<LocDtlInfo> locDtls, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancelled, "過帳取消完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocStsCnl_Mode_Out(cmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!LocDtl.FunLocDtlCnl_Mode_Out(cmd, locDtls, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位明細失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmd_Mode_R2R(CmdMstInfo cmd, List<LocDtlInfo> locDtls,
          List<TrnLogInfo> trnLogs, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finished, "過帳完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocSts_Mode_R2R(cmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!LocDtl.FunLocDtl_Mode_R2R(cmd, locDtls, trnLogs, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位明細失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmdCnl_Mode_R2R(CmdMstInfo cmd, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancelled, "過帳取消完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                if (!LocMst.FunUpdLocStsCnl_Mode_R2R(cmd, db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    sRemark = "Error: 更新儲位主檔失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
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

        public bool FunUpdateCmd_Mode_S2S(CmdMstInfo cmd, List<TrnLogInfo> tTrn_Log, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Finished, "過帳完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                foreach (var trnlog in tTrn_Log)
                {
                    if (!TrnLog.FunInsTrnLog(trnlog, db))
                    {
                        db.TransactionCtrl(TransactionTypes.Rollback);
                        return false;
                    }
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

        public bool FunUpdateCmdCnl_Mode_S2S(CmdMstInfo cmd, List<TrnLogInfo> tTrn_Log, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                if (db.TransactionCtrl(TransactionTypes.Begin) != DBResult.Success)
                {
                    sRemark = "Error: Begin失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                if (!Cmd_Mst.FunUpdateCmdSts(cmd.Cmd_Sno, clsConstValue.CmdSts.strCmd_Cancelled, "過帳取消完成", db))
                {
                    db.TransactionCtrl(TransactionTypes.Rollback);
                    return false;
                }

                foreach (var trnlog in tTrn_Log)
                {
                    if (!TrnLog.FunInsTrnLog(trnlog, db))
                    {
                        db.TransactionCtrl(TransactionTypes.Rollback);
                        return false;
                    }
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
