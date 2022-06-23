using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;
using Mirle.Def;
using Mirle.DataBase;
using System.Data;
using Mirle.Def.U2NMMA30;
using Mirle.MapController;

namespace Mirle.DB.Fun
{
    public class clsMiddleCmd
    {
        private readonly clsSno SNO = new clsSno();
        private readonly clsCmd_Mst CMD_MST = new clsCmd_Mst();
        private readonly clsTool tool = new clsTool();
        private readonly clsRoutdef routdef = new clsRoutdef();
        public int GetFinishCommand(string DeviceID, ref DataTable dtTmp, DataBase.DB db)
        {
            try
            {
                string strSql = $"select * from {Parameter.clsMiddleCmd.TableName} where {Parameter.clsMiddleCmd.Column.DeviceID} = '{DeviceID}' and " +
                    $"{Parameter.clsMiddleCmd.Column.CmdSts} in ('{clsConstValue.CmdSts.strCmd_Cancel_Wait}','{clsConstValue.CmdSts.strCmd_Finish_Wait}')";
                dtTmp = new DataTable();
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet != DBResult.Success && iRet != DBResult.NoDataSelect)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
        }

        public bool FunGetMiddleCmd(CmdMstInfo cmd, Location sLoc_Start, Location sLoc_End, ref MiddleCmd middleCmd, DataBase.DB db, string BatchID = "")
        {
            try
            {
                middleCmd = new MiddleCmd();
                middleCmd.TaskNo = SNO.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO, db);
                if (string.IsNullOrWhiteSpace(middleCmd.TaskNo))
                {
                    string sRemark = "Error: 取得TaskNo失敗！";
                    if (sRemark != cmd.Remark)
                    {
                        CMD_MST.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return false;
                }

                middleCmd.CommandID = cmd.Cmd_Sno;
                middleCmd.DeviceID = sLoc_Start.DeviceId;
                middleCmd.CSTID = cmd.Loc_ID;
                string ToLoc = cmd.Cmd_Mode == clsConstValue.CmdMode.L2L ? cmd.New_Loc : cmd.Loc;
                middleCmd.Source = tool.GetLocation(cmd.Loc, sLoc_Start);
                middleCmd.Destination = tool.GetLocation(ToLoc, sLoc_End);
                string sCmdMode = "";
                if (sLoc_Start.LocationTypes == LocationTypes.Shelf)
                {
                    switch (sLoc_End.LocationTypes)
                    {
                        case LocationTypes.Shelf:
                            sCmdMode = clsConstValue.CmdMode.L2L;
                            break;
                        default:
                            sCmdMode = clsConstValue.CmdMode.StockOut;
                            break;
                    }
                }
                else
                {
                    switch (sLoc_End.LocationTypes)
                    {
                        case LocationTypes.Shelf:
                            sCmdMode = clsConstValue.CmdMode.StockIn;
                            break;
                        default:
                            sCmdMode = clsConstValue.CmdMode.S2S;
                            break;
                    }
                }

                middleCmd.CmdMode = sCmdMode;
                middleCmd.Priority = Convert.ToInt32(cmd.Prty);

                string sStnNo = cmd.Cmd_Mode == clsConstValue.CmdMode.S2S ? cmd.New_Loc : cmd.Stn_No;
                middleCmd.Path = ConveyorDef.GetPathByStn(sStnNo);
                middleCmd.BatchID = BatchID;

                return true;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool FunGetMiddleCmd(CmdMstInfo cmd, Location sLoc_Start, Location sLoc_End, ref MiddleCmd middleCmd, ref MiddleCmd middleCmd_DD,
            bool IsDoubleCmd, CmdMstInfo cmd_DD, MapHost Router, DataBase.DB db)
        {
            try
            {
                middleCmd = new MiddleCmd();
                middleCmd_DD = new MiddleCmd();
                string sRemark = "";
                if (IsDoubleCmd)
                {
                    string sBatchID = SNO.FunGetSeqNo(clsEnum.enuSnoType.CMDSUO, db);
                    if (string.IsNullOrWhiteSpace(sBatchID))
                    {
                        sRemark = $"Error: 取得{Parameter.clsMiddleCmd.Column.BatchID}失敗！";
                        if (sRemark != cmd.Remark)
                        {
                            CMD_MST.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                        }

                        return false;
                    }

                    if (FunGetMiddleCmd(cmd, sLoc_Start, sLoc_End, ref middleCmd, db, sBatchID))
                    {
                        Location sLoc_Start_DD = null; Location sLoc_End_DD = null;
                        if (routdef.FunGetLocation(cmd_DD, Router, ref sLoc_Start_DD, ref sLoc_End_DD, db))
                        {
                            return FunGetMiddleCmd(cmd_DD, sLoc_Start_DD, sLoc_End_DD, ref middleCmd_DD, db, sBatchID);
                        }
                        else return false;
                    }
                    else return false;
                }
                else
                {
                    return FunGetMiddleCmd(cmd, sLoc_Start, sLoc_End, ref middleCmd, db);
                }
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public int CheckHasMiddleCmd(string DeviceID, DataBase.DB db)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string strSql = $"select * from {Parameter.clsMiddleCmd.TableName} where " +
                    $"{Parameter.clsMiddleCmd.Column.DeviceID} = '{DeviceID}'";
                string strEM = "";
                int iRet = db.GetDataTable(strSql, ref dtTmp, ref strEM);
                if (iRet != DBResult.Success && iRet != DBResult.NoDataSelect)
                {
                    clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Error, $"{strSql} => {strEM}");
                }

                return iRet;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return DBResult.Exception;
            }
            finally
            {
                dtTmp.Dispose();
            }
        }

        public bool FunInsMiddleCmd(MiddleCmd middleCmd, DataBase.DB db)
        {
            try
            {
                string strSql = $"insert into {Parameter.clsMiddleCmd.TableName}({Parameter.clsMiddleCmd.Column.CmdMode}," +
                    $"{Parameter.clsMiddleCmd.Column.CmdSts},{Parameter.clsMiddleCmd.Column.CommandID}," +
                    $"{Parameter.clsMiddleCmd.Column.Create_Date},{Parameter.clsMiddleCmd.Column.CSTID}," +
                    $"{Parameter.clsMiddleCmd.Column.Destination},{Parameter.clsMiddleCmd.Column.DeviceID}," +
                    $"{Parameter.clsMiddleCmd.Column.EndDate},{Parameter.clsMiddleCmd.Column.Expose_Date},{Parameter.clsMiddleCmd.Column.CompleteCode}," +
                    $"{Parameter.clsMiddleCmd.Column.Path},{Parameter.clsMiddleCmd.Column.Priority},{Parameter.clsMiddleCmd.Column.Remark}," +
                    $"{Parameter.clsMiddleCmd.Column.Source},{Parameter.clsMiddleCmd.Column.TaskNo},{Parameter.clsMiddleCmd.Column.BatchID}) values (" +
                    $"'{middleCmd.CmdMode}','{middleCmd.CmdSts}','{middleCmd.CommandID}','{middleCmd.CrtDate}'," +
                    $"'{middleCmd.CSTID}','{middleCmd.Destination}','{middleCmd.DeviceID}','{middleCmd.EndDate}'," +
                    $"'{middleCmd.ExpDate}','{middleCmd.CompleteCode}',{middleCmd.Path},{middleCmd.Priority}," +
                    $"'{middleCmd.Remark}','{middleCmd.Source}','{middleCmd.TaskNo}','{middleCmd.BatchID}')";
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
    }
}
