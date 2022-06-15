using Mirle.DataBase;
using Mirle.DB.Fun.Events;
using Mirle.Def;
using Mirle.MapController;
using Mirle.Middle;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.Fun
{
    public class clsRoutdef
    {
        public delegate void NeedShelfToShelfEventHandler(object sender, NeedShelfToShelfArgs e);
        public event NeedShelfToShelfEventHandler OnNeedShelfToShelfEvent;
        private bool reportedFlag = false;
        protected readonly object _Lock = new object();
        private clsTool tool = new clsTool();
        private clsLocMst LocMst = new clsLocMst();
        private clsCmd_Mst Cmd_Mst = new clsCmd_Mst();
        public Location GetCurLocation(CmdMstInfo cmd, MapHost Router, string DeviceID, string HostLocation, DataBase.DB db)
        {
            try
            {
                Location Start = Router.GetLocation(DeviceID, HostLocation);
                if (Start == null)
                {
                    string sRemark = $"Error: 取得CurLocation失敗 => <DeviceID> {DeviceID} <Location> {HostLocation}";
                    if (sRemark != cmd.Remark)
                    {
                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                    }

                    return null;
                }

                return Start;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return null;
            }
        }

        public Location GetFinialDestination(CmdMstInfo cmd, MapHost Router, List<ConveyorInfo> Stations, DataBase.DB db)
        {
            try
            {
                Location End = null; string sRemark = "";
                string Loc_End = cmd.Cmd_Mode == clsConstValue.CmdMode.L2L ? cmd.New_Loc : cmd.Loc;
                switch (cmd.Cmd_Mode)
                {
                    case clsConstValue.CmdMode.StockIn:
                        End = Router.GetLocation(tool.funGetEquNoByLoc(Loc_End).ToString(), Location.LocationID.Shelf.ToString());
                        break;
                    case clsConstValue.CmdMode.L2L:
                        bool IsTeach = false;
                        int iRet = LocMst.CheckIsTeach(tool.funGetEquNoByLoc(Loc_End).ToString(), Loc_End, ref IsTeach, db);
                        if (iRet == DBResult.Exception)
                        {
                            sRemark = $"Error: 確認是否是校正儲位失敗 => <Loc>{Loc_End}";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return null;
                        }

                        string sLocationID = IsTeach ? Location.LocationID.Teach.ToString() : Location.LocationID.Shelf.ToString();
                        End = Router.GetLocation(tool.funGetEquNoByLoc(Loc_End).ToString(), sLocationID);
                        break;
                    case clsConstValue.CmdMode.S2S:
                    case clsConstValue.CmdMode.StockOut:
                        string stn = cmd.Cmd_Mode == clsConstValue.CmdMode.S2S ? cmd.New_Loc : cmd.Stn_No;
                        var StnList = Stations.Where(r => r.StnNo == stn);
                        foreach(var s in StnList)
                        {
                            End = s.bufferLocation;
                        }

                        break;
                }

                return End;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return null;
            }
        }

        public bool CheckSourceIsOK(CmdMstInfo cmd, Location sLoc_Start, MidHost middle, DeviceInfo Device, WMS.Proc.clsHost wms, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                switch (sLoc_Start.LocationTypes)
                {
                    case LocationTypes.Conveyor:
                    case LocationTypes.EQ:
                    case LocationTypes.IO:
                        if (!middle.CheckIsInReady(Device, sLoc_Start))
                        {
                            sRemark = $"Error: {sLoc_Start.LocationId}沒發入庫Ready";

                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return false;
                        }

                        break;
                    case LocationTypes.Shelf:
                        bool bCheckOutside = false; string sLocDD = ""; bool IsEmpty_DD = false; string BoxID_DD = "";
                        int iRet = wms.GetLocMst().CheckLocIsOutside(cmd.Loc, ref bCheckOutside, ref sLocDD, ref IsEmpty_DD, ref BoxID_DD);
                        if (iRet == DBResult.Success)
                        {
                            if (bCheckOutside)
                            {
                                CmdMstInfo cmd_DD = new CmdMstInfo();
                                iRet = Cmd_Mst.FunCheckHasCommand(sLocDD, ref cmd_DD, db);
                                if (iRet == DBResult.Success)
                                {
                                    bool bCheckCanDo = true;
                                    if (IsEmpty_DD)
                                    {
                                        if (
                                            cmd_DD.Cmd_Sts == clsConstValue.CmdSts.strCmd_Running &&
                                            (
                                              (cmd_DD.Cmd_Mode == clsConstValue.CmdMode.StockIn && cmd_DD.Loc == sLocDD)
                                              ||
                                              (cmd_DD.Cmd_Mode == clsConstValue.CmdMode.L2L && cmd_DD.New_Loc == sLocDD)
                                            )
                                          )
                                        {
                                            bCheckCanDo = false;
                                        }
                                    }
                                    else
                                    {
                                        bCheckCanDo = false;
                                    }

                                    if (!bCheckCanDo)
                                    {
                                        sRemark = $"Error: 等候內儲位{sLocDD}命令完成 => " +
                                            $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{cmd_DD.Cmd_Sno}";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        return false;
                                    }
                                }
                                else if (iRet == DBResult.NoDataSelect && !IsEmpty_DD)
                                {
                                    lock(_Lock)
                                    {
                                        if (!reportedFlag)
                                        {
                                            reportedFlag = true;
                                            OnNeedShelfToShelfEvent?.Invoke(this, new NeedShelfToShelfArgs(Device.DeviceID, sLocDD, BoxID_DD));
                                            reportedFlag = false;
                                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"觸發庫對庫Event => " +
                                                $"<{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD} <{Parameter.clsCmd_Mst.Column.BoxID}>{BoxID_DD}");
                                        }
                                    }

                                    sRemark = $"通知WMS產生內儲位{sLocDD}的庫對庫命令";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                                else if (iRet == DBResult.Exception)
                                {
                                    sRemark = $"Error: 找尋儲位命令失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD}";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                                else { }
                            }
                        }
                        else
                        {
                            sRemark = $"Error: 取得WMS儲位資料失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{cmd.Loc}";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return false;
                        }

                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool CheckSourceIsOK(CmdMstInfo cmd, Location sLoc_Start, MidHost middle, DeviceInfo Device, WMS.Proc.clsHost wms, 
            ref bool IsDoubleCmd, ref CmdMstInfo cmd_DD, DataBase.DB db)
        {
            try
            {
                string sRemark = ""; int iRet = DBResult.Initial;
                switch (sLoc_Start.LocationTypes)
                {
                    case LocationTypes.Conveyor:
                    case LocationTypes.EQ:
                    case LocationTypes.IO:
                        var buffer = tool.GetBuffer(Device, sLoc_Start);
                        var group = tool.GetGroup(Device, buffer);
                        var lst = group.Where(r => r != buffer);
                        ConveyorInfo buffer_another = new ConveyorInfo();
                        foreach(var c in lst)
                        {
                            buffer_another = c;
                        }

                        if(buffer.DoubleType == DoubleType.Right)
                        {
                            #region 左右一定都要有東西
                            if (!middle.CheckIsInReady(buffer))
                            {
                                sRemark = $"Error: {buffer.BufferName}沒發入庫Ready";

                                if (sRemark != cmd.Remark)
                                {
                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                }

                                return false;
                            }
                            else if (!middle.CheckIsInReady(buffer_another))
                            {
                                sRemark = $"Error: {buffer_another.BufferName}沒發入庫Ready";

                                if (sRemark != cmd.Remark)
                                {
                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                }

                                return false;
                            }
                            else
                            {
                                IsDoubleCmd = true;
                                string sCmdSno_DD = middle.GetBufferCmd(buffer_another).ToString().PadLeft(5, '0');
                                bool bFlag = Cmd_Mst.FunGetCommand(sCmdSno_DD, ref cmd_DD, ref iRet, db);
                                if (!bFlag)
                                {
                                    sRemark = $"Error: 取得{buffer_another.BufferName}命令失敗 => " +
                                        $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{sCmdSno_DD}";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                            }
                            #endregion 左右一定都要有東西
                        }
                        else
                        {
                            if (!middle.CheckIsInReady(buffer))
                            {
                                sRemark = $"Error: {buffer.BufferName}沒發入庫Ready";

                                if (sRemark != cmd.Remark)
                                {
                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                }

                                return false;
                            }
                            else
                            {
                                bool IsLoad = false;
                                if(middle.CheckIsLoad(buffer_another, ref IsLoad))
                                {
                                    if(IsLoad)
                                    {
                                        #region 左右都有
                                        IsDoubleCmd = true;
                                        if (!middle.CheckIsInReady(buffer_another))
                                        {
                                            sRemark = $"Error: {buffer_another.BufferName}沒發入庫Ready";

                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            return false;
                                        }

                                        string sCmdSno_DD = middle.GetBufferCmd(buffer_another).ToString().PadLeft(5, '0');
                                        bool bFlag = Cmd_Mst.FunGetCommand(sCmdSno_DD, ref cmd_DD, ref iRet, db);
                                        if (!bFlag)
                                        {
                                            sRemark = $"Error: 取得{buffer_another.BufferName}命令失敗 => " +
                                                $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{sCmdSno_DD}";
                                            if (sRemark != cmd.Remark)
                                            {
                                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                            }

                                            return false;
                                        } 
                                        #endregion 左右都有
                                    }
                                }
                                else
                                {
                                    sRemark = $"Error: 確認{buffer_another.BufferName}荷有狀態失敗";

                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                            }
                        }

                        break;
                    case LocationTypes.Shelf:
                        bool bCheckOutside = false; string sLocDD = ""; bool IsEmpty_DD = false; string BoxID_DD = "";
                        iRet = wms.GetLocMst().CheckLocIsOutside(cmd.Loc, ref bCheckOutside, ref sLocDD, ref IsEmpty_DD, ref BoxID_DD);
                        if (iRet == DBResult.Success)
                        {
                            if (bCheckOutside)
                            {
                                cmd_DD = new CmdMstInfo();
                                iRet = Cmd_Mst.FunCheckHasCommand(sLocDD, ref cmd_DD, db);
                                if (iRet == DBResult.Success)
                                {
                                    bool bCheckCanDo = true;
                                    if (IsEmpty_DD)
                                    {
                                        if (
                                            cmd_DD.Cmd_Sts == clsConstValue.CmdSts.strCmd_Running &&
                                            (
                                              (cmd_DD.Cmd_Mode == clsConstValue.CmdMode.StockIn && cmd_DD.Loc == sLocDD)
                                              ||
                                              (cmd_DD.Cmd_Mode == clsConstValue.CmdMode.L2L && cmd_DD.New_Loc == sLocDD)
                                            )
                                          )
                                        {
                                            bCheckCanDo = false;
                                        }
                                    }
                                    else
                                    {
                                        bCheckCanDo = false;
                                    }

                                    if (!bCheckCanDo)
                                    {
                                        sRemark = $"Error: 等候內儲位{sLocDD}命令完成 => " +
                                            $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{cmd_DD.Cmd_Sno}";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        return false;
                                    }
                                }
                                else if (iRet == DBResult.NoDataSelect && !IsEmpty_DD)
                                {
                                    lock (_Lock)
                                    {
                                        if (!reportedFlag)
                                        {
                                            reportedFlag = true;
                                            OnNeedShelfToShelfEvent?.Invoke(this, new NeedShelfToShelfArgs(Device.DeviceID, sLocDD, BoxID_DD));
                                            reportedFlag = false;
                                            clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"觸發庫對庫Event => " +
                                                $"<{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD} <{Parameter.clsCmd_Mst.Column.BoxID}>{BoxID_DD}");
                                        }
                                    }

                                    sRemark = $"通知WMS產生內儲位{sLocDD}的庫對庫命令";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                                else if (iRet == DBResult.Exception)
                                {
                                    sRemark = $"Error: 找尋儲位命令失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD}";
                                    if (sRemark != cmd.Remark)
                                    {
                                        Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                    }

                                    return false;
                                }
                                else { }
                            }
                        }
                        else
                        {
                            sRemark = $"Error: 取得WMS儲位資料失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{cmd.Loc}";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return false;
                        }

                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return false;
            }
        }

        public bool CheckDestinationIsOK(CmdMstInfo cmd, Location sLoc_End, MidHost middle, DeviceInfo Device, WMS.Proc.clsHost wms, DataBase.DB db)
        {
            try
            {
                string sRemark = "";
                switch (sLoc_End.LocationTypes)
                {
                    case LocationTypes.Conveyor:
                    case LocationTypes.EQ:
                    case LocationTypes.IO:
                        if (!middle.CheckIsOutReady(Device, sLoc_End))
                        {
                            sRemark = $"Error: {sLoc_End.LocationId}沒發出庫Ready";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return false;
                        }

                        break;
                    case LocationTypes.Shelf:
                        string sLoc_Check = cmd.Cmd_Mode == clsConstValue.CmdMode.L2L ? cmd.New_Loc : cmd.Loc;
                        bool bCheckOutside = false; string sLocDD = ""; bool IsEmpty_DD = false; string BoxID_DD = "";
                        int iRet = wms.GetLocMst().CheckLocIsOutside(sLoc_Check, ref bCheckOutside, ref sLocDD, ref IsEmpty_DD, ref BoxID_DD);
                        if (iRet == DBResult.Success)
                        {
                            CmdMstInfo cmd_DD = new CmdMstInfo();
                            iRet = Cmd_Mst.FunCheckHasCommand(sLocDD, ref cmd_DD, db);
                            if (iRet == DBResult.Exception)
                            {
                                sRemark = $"Error: 找尋儲位命令失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD}";
                                if (sRemark != cmd.Remark)
                                {
                                    Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                }

                                return false;
                            }

                            if (bCheckOutside)
                            {
                                if (!IsEmpty_DD)
                                {
                                    if (iRet == DBResult.Success)
                                    {
                                        sRemark = $"Error: 等候內儲位{sLocDD}命令完成 => " +
                                              $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{cmd_DD.Cmd_Sno}";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }
                                    }
                                    else
                                    {
                                        lock (_Lock)
                                        {
                                            if (!reportedFlag)
                                            {
                                                reportedFlag = true;
                                                OnNeedShelfToShelfEvent?.Invoke(this, new NeedShelfToShelfArgs(Device.DeviceID, sLocDD, BoxID_DD));
                                                reportedFlag = false;
                                                clsWriLog.Log.FunWriLog(WriLog.clsLog.Type.Debug, $"觸發庫對庫Event => " +
                                                    $"<{Parameter.clsCmd_Mst.Column.Loc}>{sLocDD} <{Parameter.clsCmd_Mst.Column.BoxID}>{BoxID_DD}");
                                            }
                                        }

                                        sRemark = $"通知WMS產生內儲位{sLocDD}的庫對庫命令";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }
                                    }

                                    return false;
                                }
                            }
                            else
                            {
                                if (!IsEmpty_DD)
                                {
                                    if (iRet == DBResult.Success && cmd_DD.Cmd_Sts == clsConstValue.CmdSts.strCmd_Running)
                                    {
                                        sRemark = $"Error: 等候外儲位{sLocDD}命令完成 => " +
                                             $"<{Parameter.clsCmd_Mst.Column.Cmd_Sno}>{cmd_DD.Cmd_Sno}";
                                        if (sRemark != cmd.Remark)
                                        {
                                            Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                                        }

                                        return false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            sRemark = $"Error: 取得WMS儲位資料失敗 => <{Parameter.clsCmd_Mst.Column.Loc}>{sLoc_Check}";
                            if (sRemark != cmd.Remark)
                            {
                                Cmd_Mst.FunUpdateRemark(cmd.Cmd_Sno, sRemark, db);
                            }

                            return false;
                        }
                        break;
                }

                return true;
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
