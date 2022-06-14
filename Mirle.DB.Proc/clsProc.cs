using Mirle.DataBase;
using Mirle.Def;
using Mirle.EccsSignal;
using Mirle.MapController;
using Mirle.Middle;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.DB.Proc
{
    public class clsProc
    {
        private Fun.clsCmd_Mst Cmd_Mst = new Fun.clsCmd_Mst();
        private Fun.clsLocMst LocMst = new Fun.clsLocMst();
        private Fun.clsTool tool = new Fun.clsTool();
        private Fun.clsRoutdef Routdef = new Fun.clsRoutdef();
        private clsDbConfig _config = new clsDbConfig();
        public clsProc(clsDbConfig config)
        {
            _config = config;
        }

        public Fun.clsRoutdef GetFun_Routdef() => Routdef;

        public bool FunAsrsCmd_Proc(DeviceInfo Device, string StockInLoc_Sql, MapHost Router, 
            WMS.Proc.clsHost wms, MidHost middle, SignalHost CrnSignal)
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (var db = clsGetDB.GetDB(_config))
                {
                    int iRet = clsGetDB.FunDbOpen(db);
                    if (iRet == DBResult.Success)
                    {
                        iRet = Cmd_Mst.FunGetCommand(Device.DeviceID, StockInLoc_Sql, ref dtTmp, db);
                        if (iRet == DBResult.Success)
                        {
                            for (int i = 0; i < dtTmp.Rows.Count; i++)
                            {
                                CmdMstInfo cmd = tool.GetCommand(dtTmp.Rows[i]);
                                if (!Cmd_Mst.CheckCraneStatus(cmd, Device, CrnSignal, db)) continue;

                                string sRemark = "";
                                Location Start = null; Location End = null;
                                if (!string.IsNullOrWhiteSpace(cmd.CurLoc))
                                {
                                    #region 判斷當前位置
                                    Start = Routdef.GetCurLocation(cmd, Router, cmd.CurDeviceID, cmd.CurLoc, db);
                                    if (Start == null) continue;
                                    #endregion 判斷當前位置
                                }
                                else
                                {
                                    if(cmd.Cmd_Sts == clsConstValue.CmdSts.strCmd_Initial)
                                    {
                                        #region 判斷當前位置
                                        bool IsTeach = false;
                                        iRet = LocMst.CheckIsTeach(cmd.Equ_No, cmd.Loc, ref IsTeach, db);
                                        if (iRet == DBResult.Exception)
                                        {
                                            sRemark = $"Error: 確認是否是校正儲位失敗 => <{Fun.Parameter.clsCmd_Mst.Column.Loc}>{cmd.Loc}";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            continue;
                                        }

                                        string sLocationID = IsTeach ? Location.LocationID.Teach.ToString() : Location.LocationID.Shelf.ToString();
                                        Start = Routdef.GetCurLocation(cmd, Router, cmd.Equ_No, sLocationID, db);
                                        if (Start == null) continue;
                                        #endregion 判斷當前位置
                                    }
                                    else
                                    {
                                        sRemark = $"Error: 執行中的命令{Fun.Parameter.clsCmd_Mst.Column.CurLoc}不該為空" +
                                            $" => <{Fun.Parameter.clsCmd_Mst.Column.Cmd_Sno}>{cmd.Cmd_Sno}";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }
                                }

                                #region 判斷最終目的位置
                                End = Routdef.GetFinialDestination(cmd, Router, db);
                                if (End == null)
                                {
                                    sRemark = "Error: 取得最終位置失敗！";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }
                                    continue;
                                }
                                #endregion 判斷最終目的位置
                                if (Start != End)
                                {
                                    Location sLoc_Start = null; Location sLoc_End = null;
                                    bool bCheck = Router.GetPath(Start, End, ref sLoc_Start, ref sLoc_End);
                                    if (bCheck == false)
                                    {
                                        sRemark = "Error: Route給出的路徑為Null，WCS給的Location => Start: <Device>" + Start.DeviceId + " <Location>" + Start.LocationId +
                                           "，End: <Device>" + End.DeviceId + " <Location>" + End.LocationId;
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        continue;
                                    }
                                    else
                                    {
                                        if (!Routdef.CheckSourceIsOK(cmd, sLoc_Start, middle, Device, wms, db)) continue;
                                        if (!Routdef.CheckDestinationIsOK(cmd, sLoc_End, middle, Device, wms, db)) continue;

                                        

                                    }
                                }
                                else continue;
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
                dtTmp.Dispose();
            }
        }
    }
}
