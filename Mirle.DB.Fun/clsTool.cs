﻿using System;
using Mirle.Def;
using Mirle.Structure;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Mirle.Def.U2NMMA30;
using static Mirle.Def.U2NMMA30.ConveyorDef;

namespace Mirle.DB.Fun
{
    public class clsTool
    {
        public clsTool() { }

        private static DeviceInfo[] pcba;
        private static DeviceInfo[] box;
        public clsTool(DeviceInfo[] PCBA, DeviceInfo[] Box)
        {
            pcba = PCBA;
            box = Box;
        }

        public CmdMstInfo GetCommand(DataRow drTmp)
        {
            CmdMstInfo cmd = new CmdMstInfo
            {
                BoxID = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.BoxID]),
                Cmd_Mode = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Cmd_Mode]),
                Cmd_Sno = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Cmd_Sno]),
                Cmd_Sts = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Cmd_Sts]),
                Crt_Date = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Create_Date]),
                CurDeviceID = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.CurDeviceID]),
                CurLoc = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.CurLoc]),
                End_Date = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.End_Date]),
                Equ_No = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Equ_No]),
                EXP_Date = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Expose_Date]),
                IO_Type = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.IO_Type]),
                Loc = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Loc]),
                New_Loc = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.New_Loc]),
                Prty = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Prty]),
                Remark = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Remark]),
                Stn_No = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Stn_No]),
                Trn_User = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Trn_User]),
                Zone_ID = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Zone]),
                BatchID = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.BatchID]),
                JobID = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.JobID]),
                backupPortId = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.backupPortId]),
                NeedShelfToShelf = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.NeedShelfToShelf]),
                rackLocation = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.rackLocation]),
                largest = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.largest]),
                Cmd_Abnormal = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.Cmd_Abnormal]),
                carrierType= Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.carrierType]),
                writeToMiddle = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.writeToMiddle]),
                boxStockOutAgv = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.boxStockOutAgv]),
                updateFailTime = Convert.ToString(drTmp[Parameter.clsCmd_Mst.Column.updateFailTime])

            };

            return cmd;
        }

        public LocDtlInfo GetLocDtl_FromCmdDtl(CmdMstInfo cmd, DataRow drCmdDtl)
        {
            LocDtlInfo locDtl = new LocDtlInfo();
            if (cmd.Cmd_Mode == clsConstValue.CmdMode.L2L)
            {
                locDtl.Loc = cmd.New_Loc;
            }
            else
            {
                locDtl.Loc = cmd.Loc;
            }
            locDtl.LocTxno = drCmdDtl[Parameter.clsCmd_Dtl.Column.Cmd_Txno].ToString();
            locDtl.WhId = cmd.WH_ID;
            locDtl.Loc = drCmdDtl[Parameter.clsCmd_Dtl.Column.Loc].ToString();
            locDtl.InDate = drCmdDtl[Parameter.clsCmd_Dtl.Column.IN_Date].ToString();
            locDtl.PltId = drCmdDtl[Parameter.clsCmd_Dtl.Column.Plt_Id].ToString();
            locDtl.LotNo = drCmdDtl[Parameter.clsCmd_Dtl.Column.Lot_No].ToString();
            locDtl.PltQty = double.Parse(drCmdDtl[Parameter.clsCmd_Dtl.Column.Plt_Qty].ToString());
            locDtl.AloQty = double.Parse(drCmdDtl[Parameter.clsCmd_Dtl.Column.Trn_Qty].ToString());
            locDtl.TktNo = drCmdDtl[Parameter.clsCmd_Dtl.Column.IN_Tkt_No].ToString();
            locDtl.TktSeq = drCmdDtl[Parameter.clsCmd_Dtl.Column.IN_Tkt_Seq].ToString();
            locDtl.TrnTktNo = drCmdDtl[Parameter.clsCmd_Dtl.Column.Trn_Tkt_No].ToString();
            locDtl.TrnTktSeq = drCmdDtl[Parameter.clsCmd_Dtl.Column.Trn_Tkt_Seq].ToString();
            locDtl.TktIO = drCmdDtl[Parameter.clsCmd_Dtl.Column.Tkt_IO].ToString();
            locDtl.TktType = drCmdDtl[Parameter.clsCmd_Dtl.Column.Tkt_Type].ToString();
            locDtl.CycNo = drCmdDtl[Parameter.clsCmd_Dtl.Column.Cyc_no].ToString();
            locDtl.ValidDate = drCmdDtl[Parameter.clsCmd_Dtl.Column.ShelfLife].ToString();
            locDtl.ItemNo = drCmdDtl[Parameter.clsCmd_Dtl.Column.ItemNo].ToString();
            locDtl.Factory = drCmdDtl[Parameter.clsCmd_Dtl.Column.Factory].ToString();
            locDtl.CustomerId = drCmdDtl[Parameter.clsCmd_Dtl.Column.ClientNo].ToString();
            locDtl.CustomerName = drCmdDtl[Parameter.clsCmd_Dtl.Column.ClientName].ToString();
            locDtl.ProviderId = drCmdDtl[Parameter.clsCmd_Dtl.Column.SupplierNo].ToString();
            locDtl.ProviderName = drCmdDtl[Parameter.clsCmd_Dtl.Column.SupplierName].ToString();
            locDtl.ItemUnit = drCmdDtl[Parameter.clsCmd_Dtl.Column.Unit].ToString();
            locDtl.BoxQty = double.Parse(drCmdDtl[Parameter.clsCmd_Dtl.Column.BoxCount].ToString());
            locDtl.Remarks = drCmdDtl[Parameter.clsCmd_Dtl.Column.Remark].ToString();
            locDtl.StoreCode = drCmdDtl[Parameter.clsCmd_Dtl.Column.WH_Type].ToString();
            locDtl.ProdDate = drCmdDtl[Parameter.clsCmd_Dtl.Column.BeginDate].ToString();

            return locDtl;
        }

        public TrnLogInfo GetTrnLogInfo(CmdMstInfo cmd, LocDtlInfo locDtl)
        {
            TrnLogInfo trnLog = new TrnLogInfo
            {
                LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                CmdSno = cmd.Cmd_Sno,
                CmdTxno = locDtl.LocTxno,
                CmdSts = cmd.Cmd_Sts,
                CmdAbnormal = cmd.Cmd_Abnormal,
                Prty = cmd.Prty,
                StnNo = cmd.Stn_No,
                CmdMode = cmd.Cmd_Mode,
                IoType = cmd.IO_Type,
                WhId = cmd.WH_ID,
                Loc = cmd.Loc,
                NewLoc = cmd.New_Loc,
                MixQty = cmd.Mix_Qty,
                Avail = cmd.Avail,
                ZoneId = cmd.Zone_ID,
                LocId = cmd.BoxID,
                CrtDate = cmd.Crt_Date,
                ExpDate = cmd.EXP_Date,
                EndDate = cmd.End_Date,
                TrnUser = cmd.Trn_User,
                HostName = cmd.Host_Name,
                Trace = cmd.Trace,
                PltCount = cmd.Plt_Count,
                EquNo = cmd.Equ_No,
                PltQty = locDtl.PltQty,
                TrnQty = locDtl.AloQty,
                PltId = locDtl.PltId,
                LotNo = locDtl.LotNo,
                InDate = locDtl.InDate,

                //需判斷入出庫單
                InTktNo = locDtl.TktNo,
                InTktSeq = locDtl.TktSeq,
                TrnTktNo = locDtl.TktNo,
                TrnTktSeq = locDtl.TktSeq,

                ValidDate = locDtl.ValidDate,
                ItemNo = locDtl.ItemNo,
                Factory = locDtl.Factory,
                CustomerId = locDtl.CustomerId,
                CustomerName = locDtl.CustomerName,
                ProviderId = locDtl.ProviderId,
                ProviderName = locDtl.ProviderName,
                ItemUnit = locDtl.ItemUnit,
                BoxQty = locDtl.BoxQty,
                Remarks = locDtl.Remarks,
                StoreCode = locDtl.StoreCode,
                ProdDate = locDtl.ProdDate
            };

            return trnLog;
        }

        /// <summary>
        /// 依儲位取得Equ_No
        /// </summary>
        /// <param name="sLoc">儲位編號</param>
        /// <returns></returns>
        public int funGetEquNoByLoc(string sLoc)
        {
            int iCrn = 0;
            string sRow_X = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(sLoc))
                {
                    sRow_X = sLoc.Substring(0, 2);
                    int iRow = Convert.ToInt32(sRow_X);
                    if (iRow % 4 == 0) iCrn = iRow / 4;
                    else iCrn = iRow / 4 + 1;

                    return iCrn;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return 0;
            }
        }

        public string GetLocation(string Loc, Location location)
        {
            switch(location.LocationTypes)
            {
                case LocationTypes.Shelf:
                    return Loc;
                default:
                    return location.LocationId;
            }
        }

        public ConveyorInfo GetBuffer(DeviceInfo Device, Location location)
        {
            foreach (var floor in Device.Floors)
            {
                foreach (var con in floor.Group_IN)
                {
                    if (con.BufferName == location.LocationId)
                    {
                        return con;
                    }
                }

                foreach (var con in floor.Group_OUT)
                {
                    if (con.BufferName == location.LocationId)
                    {
                        return con;
                    }
                }
            }

            return new ConveyorInfo();
        }

        public List<ConveyorInfo> GetGroup(DeviceInfo Device, ConveyorInfo conveyor)
        {
            foreach (var floor in Device.Floors)
            {
                foreach (var con in floor.Group_IN)
                {
                    if (con == conveyor)
                    {
                        return floor.Group_IN;
                    }
                }

                foreach (var con in floor.Group_OUT)
                {
                    if (con == conveyor)
                    {
                        return floor.Group_OUT;
                    }
                }
            }

            return new List<ConveyorInfo>();
        }

        public bool IsAGV(string DeviceID_Router, ref string sDeviceID)
        {
            if (ConveyorDef.DeviceID_AGV_Router.Where(r => r == DeviceID_Router).Any())
            {
                sDeviceID = ConveyorDef.DeviceID_AGV;
                return true;
            }
            else return false;
        }
        public string GetDeviceId(string bufferName)
        {
            //PCBA
            if (bufferName == PCBA.M1_01.BufferName || bufferName == PCBA.M1_06.BufferName)
                return "2";
            else if (bufferName == PCBA.M1_11.BufferName || bufferName == PCBA.M1_16.BufferName)
                return "1";
            //BOX
            else if (bufferName == Box.B1_001.BufferName || bufferName == Box.B1_004.BufferName ||
                    bufferName == Box.B1_007.BufferName || bufferName == Box.B1_010.BufferName ||
                    bufferName == Box.B1_081.BufferName || bufferName == Box.B1_084.BufferName ||
                    bufferName == Box.B1_087.BufferName || bufferName == Box.B1_090.BufferName)
                return "5";
            else if (bufferName == Box.B1_013.BufferName || bufferName == Box.B1_016.BufferName ||
                    bufferName == Box.B1_019.BufferName || bufferName == Box.B1_022.BufferName ||
                    bufferName == Box.B1_093.BufferName || bufferName == Box.B1_096.BufferName ||
                    bufferName == Box.B1_099.BufferName || bufferName == Box.B1_102.BufferName)
                return "4";
            else if (bufferName == Box.B1_025.BufferName || bufferName == Box.B1_028.BufferName ||
                    bufferName == Box.B1_031.BufferName || bufferName == Box.B1_034.BufferName ||
                    bufferName == Box.B1_105.BufferName || bufferName == Box.B1_108.BufferName ||
                    bufferName == Box.B1_111.BufferName || bufferName == Box.B1_114.BufferName)
                return "3";
            //Tower
            else if (bufferName == Tower.E1_04.BufferName)
                return "7";
            //AGV
            else if (ConveyorDef.GetAGV_3FPort().Where(r => r.BufferName == bufferName).Any())
                return "63";
            else if (ConveyorDef.GetAGV_5FPort().Where(r => r.BufferName == bufferName).Any())
                return "65";
            else if (ConveyorDef.GetAGV_6FPort().Where(r => r.BufferName == bufferName).Any())
                return "66";
            else if (ConveyorDef.GetAGV_8FPort().Where(r => r.BufferName == bufferName).Any())
                return "68";
            //356線邊倉
            else if (ConveyorDef.GetNode_3F().Where(r => r.BufferName == bufferName).Any())
                return "SMT3C";
            else if (ConveyorDef.GetNode_5F().Where(r => r.BufferName == bufferName).Any())
                return "SMT5C";
            else if (ConveyorDef.GetNode_6F().Where(r => r.BufferName == bufferName).Any())
                return "SMT6C";
            //整理區
            else if (bufferName.Contains("A4"))
                return "Line";
            //線邊倉
            else if (bufferName.Contains("A3"))
                return "SMT6C";
            else if (bufferName.Contains("A2"))
                return "SMT5C";
            else if (bufferName.Contains("A1"))
                return "SMT3C";
            //產線
            else if (bufferName.Contains("S"))
                return "SMTC";
            else
                return "";
        }

        public bool CheckWhId_ASRS(string sDeviceID, ref clsEnum.AsrsType type)
        {
            bool check = pcba.Where(r => r.DeviceID == sDeviceID).Any();
            if (check)
            {
                type = clsEnum.AsrsType.PCBA;
                return true;
            }
            else
            {
                check = box.Where(r => r.DeviceID == sDeviceID).Any();
                if (check)
                {
                    type = clsEnum.AsrsType.Box;
                    return true;
                }
                else
                {
                    type = clsEnum.AsrsType.None;
                    return false;
                }
            }
        }

        public clsEnum.Shelf_LocationSide GetSide(string sLoc)
        {
            try
            {
                int iRow = Convert.ToInt32(sLoc.Substring(0, 2));
                if (iRow == 0) return clsEnum.Shelf_LocationSide.Fail;

                switch(iRow % 4)
                {
                    case 1:
                    case 3:
                        return clsEnum.Shelf_LocationSide.Left;
                    default:
                        return clsEnum.Shelf_LocationSide.Right;
                }
            }
            catch
            {
                return clsEnum.Shelf_LocationSide.Fail;
            }
        }

        public clsEnum.Shelf GetShelfLocation(string sLoc)
        {
            if (string.IsNullOrWhiteSpace(sLoc) || sLoc.Length < 7) return clsEnum.Shelf.None;

            int iRow = Convert.ToInt32(sLoc.Substring(0, 2));
            switch (iRow % 4)
            {
                case 1:
                case 2:
                    return clsEnum.Shelf.InSide;
                case 3:
                default:
                    return clsEnum.Shelf.OutSide;
            }
        }

        public MiddleCmd GetMiddleCmd(DataRow drTmp)
        {
            MiddleCmd cmd = new MiddleCmd
            {
                BatchID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.BatchID]),
                Destination = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Destination]),
                DeviceID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.DeviceID]),
                CrtDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Create_Date]),
                CSTID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CSTID]),
                EndDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.EndDate]),
                ExpDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Expose_Date]),
                CmdMode = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CmdMode]),
                CmdSts = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CmdSts]),
                CommandID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CommandID]),
                CompleteCode = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CompleteCode]),
                Path = Convert.ToInt32(drTmp[Parameter.clsMiddleCmd.Column.Path]),
                Priority = Convert.ToInt32(drTmp[Parameter.clsMiddleCmd.Column.Priority]),
                Remark = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Remark]),
                Source = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Source]),
                TaskNo = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.TaskNo]),
                Iotype = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.IoType]),
                largest = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.largest]),
                rackLocation = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.rackLocation]),
                carrierType = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.carrierType])
            };

            return cmd;
        }
    }
}
