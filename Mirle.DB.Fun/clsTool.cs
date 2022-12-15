using System;
using Mirle.Def;
using Mirle.Structure;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Mirle.Def.U2NMMA30;
using static Mirle.Def.U2NMMA30.ConveyorDef;
using Mirle.Structure.Info;

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

        public L2LCountInfo Get2LCountInfo(DataRow drTmp)
        {
            L2LCountInfo CountInfo = new L2LCountInfo
            {
                BoxId = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.BoxID]),
                EquNo = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.EquNo]),
                RoundSts = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.RoundSts]),
                L2LTimes = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.Count]),
                HisLoc = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.HisLoc]),
                CrtDate = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.Create_Date]),
                ExpDate = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.Update_Date]),
                TeachLoc = Convert.ToString(drTmp[Parameter.clsL2LCount.Column.TeachLoc]),
            };
            return CountInfo;
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

        public string FunGetB800L2LCycleRunInitialDestiantion(string startShelf)
        {
            string nextlocation = "";
            int lastRow = 0;
            lastRow = Convert.ToInt32(startShelf.Substring(2, 3));


            int equNo = funGetEquNoByLoc(startShelf);
            if (equNo < 6 && equNo > 2)
            {
                if(lastRow > 10 && lastRow < 20)
                {
                    nextlocation = startShelf.Substring(0, 2) + (lastRow - 9).ToString().PadLeft(3, '0') + startShelf.Substring(5, 2);
                }
                else if (lastRow > 19 && lastRow < 27)
                {
                    nextlocation = startShelf.Substring(0, 2) + (lastRow - 18).ToString().PadLeft(3, '0') + startShelf.Substring(5, 2);
                }
                else
                {
                    return $"Error: LastRow B800庫對庫命令取得初始終點失敗， LastRow = {lastRow}.";
                }
            }
            else
            {
                return $"Error: B800庫對庫命令取得初始終點失敗(EquNo有誤)， startShelf = {startShelf} and EquNo = {equNo}.";
            }

            return nextlocation;
        }

        public string FunGetCycleRunNextLocation(string lastCmdMode, string location, string lastShelf)
        {
            string nextlocation = "";
            ConveyorInfo nowConveyor = ConveyorDef.GetBuffer(location);
            int lastColumn = 0; int lastRow = 0; int lastLevel = 0;
            lastColumn = Convert.ToInt32(lastShelf.Substring(0, 2));
            lastRow = Convert.ToInt32(lastShelf.Substring(2, 3));
            lastLevel = Convert.ToInt32(lastShelf.Substring(5, 2));
            int nextColumn = 0, nextRow = 0, nextLevel = 0;

            if(lastCmdMode == clsConstValue.CmdMode.StockOut)
            {
                if(nowConveyor.ControllerID == "PCBA")
                {
                    //只使用前兩個Row
                    //路徑 01/01/01 -> 05/01/01 -> 02/01/01 -> 06/01/01... -> 08/01/01 -> 01/01/02 ->...->08/01/03 -> 01/02/01
                    //追加條件：M801庫對庫命令需六筆出庫入庫一次，因此需另外判斷
                    if(nowConveyor.BufferName == ConveyorDef.AGV.M1_15.BufferName || nowConveyor.BufferName == ConveyorDef.AGV.M1_20.BufferName)
                    {
                        //出庫到M801 AGV口
                        if (lastColumn < 5) //for M801庫對庫使用
                        {
                            //追加條件：M801庫對庫命令需六筆出庫入庫一次，因此需另外判斷
                            if (lastColumn == 0)
                            {
                                //錯誤，未抓取Column成功
                                return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                            }
                            else if (lastColumn < 4)
                            {
                                nextColumn = lastColumn + 1;
                                nextRow = lastRow - 14;
                                nextLevel = 1;
                            }
                            else if (lastColumn == 4)
                            {
                                nextColumn = 1;
                                nextRow = lastRow - 13;
                                nextLevel = 1;
                                if (lastRow == 30)
                                    nextRow = 3;
                            }
                            else
                            {
                                //錯誤，未抓取Column成功
                                return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                            }
                        }
                        else if (lastColumn < 8)
                        {
                            nextColumn = lastColumn - 3;
                            nextRow = lastRow;
                            nextLevel = lastLevel;
                        }
                        else if (lastColumn == 8)
                        {
                            nextColumn = 1;

                            if (lastLevel == 0)
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel get fail, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                            else if(lastLevel < 3)
                            {
                                nextRow = lastRow;
                                nextLevel = lastLevel + 1;
                            }
                            else if (lastLevel == 3)
                            {
                                nextLevel = 1;

                                if(lastRow == 0)
                                {
                                    //錯誤，未抓取Row成功
                                    return $"Error: LastRow get fail, string ver = {lastShelf.Substring(2, 3)} and int ver = {lastRow}.";
                                }
                                else if (lastRow > 3)
                                {
                                    //錯誤，未抓取Row成功
                                    return $"Error: 入出庫 LastRow 取得庫對庫使用儲位, string ver = {lastShelf.Substring(2, 3)} and int ver = {lastRow}.";
                                }
                                else if (lastRow == 1)
                                {
                                    nextRow = lastRow + 1;
                                }
                                else if (lastRow == 2)
                                {
                                    nextRow = lastRow - 1;
                                }
                                else
                                {
                                    //錯誤，未抓取Row成功
                                    return $"Error: LastRow get fail, string ver = {lastShelf.Substring(2, 3)} and int ver = {lastRow}.";
                                }
                            }
                            else
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel get fail, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                        }
                        else
                        {
                            //錯誤，未抓取Column成功
                            return $"Error: LastColumn get fail, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                        }
                    }
                    else if(nowConveyor.BufferName == ConveyorDef.AGV.M1_05.BufferName || nowConveyor.BufferName == ConveyorDef.AGV.M1_10.BufferName)
                    {
                        //出庫到M802 AGV口
                        if (lastColumn == 0)
                        {
                            //錯誤，未抓取Column成功
                            return $"Error: LastColumn get fail, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                        }
                        else if (lastColumn <= 4)
                        {
                            nextColumn = lastColumn + 4;
                            nextRow = lastRow;
                            nextLevel = lastLevel;
                        }
                        else
                        {
                            //錯誤，未抓取Column成功
                            return $"Error: LastColumn get fail, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                        }

                    }
                }
                else if (nowConveyor.ControllerID.Contains("Box"))
                {
                    //上撿料口->內儲位->下撿料口->外儲位，上撿料口
                    //Ex. B1-142 -> 0900101 -> B1-062 -> 1100101 -> B1-142 -> 0900102 -> B1-062 -> 1100102
                    if(nowConveyor == ConveyorDef.Box.B1_062 || nowConveyor == ConveyorDef.Box.B1_067)
                    {
                        if(lastColumn % 4 != 1 && lastColumn % 4 != 2)
                            return $"Error: LastColumn get fail, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";

                        nextColumn = lastColumn + 2;
                        nextRow = lastRow;
                        nextLevel = lastLevel;
                    }
                    else if(nowConveyor == ConveyorDef.Box.B1_142 || nowConveyor == ConveyorDef.Box.B1_147)
                    {
                        if (lastColumn % 4 != 3 && lastColumn % 4 != 0)
                            return $"Error: LastColumn get fail, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";

                        if (lastLevel == 0 || lastLevel > 5)
                        {
                            //錯誤，未抓取Level成功
                            return $"Error: LastLevel get fail, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                        }
                        else if(lastLevel < 5)
                        {
                            nextColumn = lastColumn - 2;
                            nextRow = lastRow;
                            nextLevel = lastLevel + 1;
                        }
                        else //Case: lastLevel == 5
                        {
                            nextColumn = lastColumn - 2;
                            nextRow = lastRow;
                            nextLevel = 1;
                        }
                    }
                }

                nextlocation = nextColumn.ToString().PadLeft(2, '0') + nextRow.ToString().PadLeft(3, '0') + nextLevel.ToString().PadLeft(2, '0');
            }
            else if(lastCmdMode == clsConstValue.CmdMode.StockIn)
            {
                int equNo = funGetEquNoByLoc(lastShelf);

                switch(equNo)
                {
                    case 1:
                        //PCBA倉命令
                        nextlocation = ConveyorDef.GetBuffer_ByStnNo("M800-2").BufferName;
                        break;
                    case 2:
                        //PCBA倉命令
                        nextlocation = ConveyorDef.GetBuffer_ByStnNo("M800-1").BufferName;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        //箱式倉命令
                        //上撿料口->內儲位->下撿料口->外儲位，上撿料口
                        switch(lastColumn % 4)
                        {
                            case 1:
                                nextlocation = ConveyorDef.Box.B1_062.BufferName;
                                break;
                            case 2:
                                nextlocation = ConveyorDef.Box.B1_067.BufferName;
                                break;
                            case 3:
                                nextlocation = ConveyorDef.Box.B1_142.BufferName;
                                break;
                            case 0:
                                nextlocation = ConveyorDef.Box.B1_147.BufferName;
                                break;
                            default:
                                return $"Error: LastColumn 判定入庫後Cycle Run尋找下個位置有誤, lastColumn = {lastColumn}.";
                        }
                        break;
                    default:
                        //錯誤，取得EquNo失敗
                        return $"Error: LastShelf 入庫位置取得EquNo錯誤, lastShelf = {lastShelf} and EquNo = {equNo}.";
                }
            }
            else if(lastCmdMode == clsConstValue.CmdMode.L2L)
            {
                int equNo = funGetEquNoByLoc(lastShelf);
                if(equNo > 0 && equNo < 3)
                {
                    //PCBA倉命令
                    if(equNo == 1)
                    {
                        //M801 庫對庫 Row 從 03 <-> 30
                        //分兩區塊 03<->16, 17<->30
                        //路徑01/03/01 -> 01/17/01 -> 01/03/02 -> 01/17/02 -> 01/03/03 -> 01/17/03 -> M1-20 -> 02/03/01 -> 02/17/01 ->...-> 04/17/03 -> M1-20 -> 01/04/01 -> 01/18/01 ->...
                        if (lastRow > 2 && lastRow < 17)
                        {
                            nextColumn = lastColumn;
                            nextRow = lastRow + 14;
                            nextLevel = lastLevel;
                        }
                        else if(lastRow > 16 && lastRow < 31)
                        {
                            if(lastLevel == 0)
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel M801庫對庫位置錯誤, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                            else if(lastLevel < 3)
                            {
                                nextColumn = lastColumn;
                                nextRow = lastRow - 14;
                                nextLevel = lastLevel + 1;
                            }
                            else if(lastLevel == 3)
                            {
                                if (equNo == 1)
                                    return ConveyorDef.AGV.M1_20.BufferName; //M801庫對庫Cycle六筆入出庫一次

                                if(lastColumn == 0)
                                {
                                    //錯誤，未抓取Column成功
                                    return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                                }
                                else if(lastColumn < 4)
                                {
                                    nextColumn = lastColumn + 1;
                                    nextRow = lastRow - 14;
                                    nextLevel = 1;
                                }
                                else if(lastColumn == 4)
                                {
                                    nextColumn = 1;
                                    nextRow = lastRow - 13;
                                    nextLevel = 1;
                                    if (lastRow == 30)
                                        nextRow = 3;
                                }
                                else
                                {
                                    //錯誤，未抓取Column成功
                                    return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                                }
                            }
                            else
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel M801庫對庫位置錯誤, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                        }
                        else
                        {
                            //錯誤，取得Row失敗
                            return $"Error: LastRow M801庫對庫位置錯誤, string ver = {lastShelf.Substring(2, 3)} and int ver = {lastRow}.";
                        }
                    }
                    else if (equNo == 2)
                    {
                        //M802 庫對庫 Row 從 03 <-> 38
                        //分兩區塊 03<->20, 21<->38
                        //路徑05/03/01 -> 05/21/01 -> 05/03/02 -> 05/21/02 -> 05/03/03 -> 05/21/03 -> 06/03/01 -> 06/21/01 ->...-> 08/21/03 -> 05/04/01 -> 05/22/01 ->...
                        if (lastRow > 2 && lastRow < 21)
                        {
                            nextColumn = lastColumn;
                            nextRow = lastRow + 18;
                            nextLevel = lastLevel;
                        }
                        else if (lastRow > 20 && lastRow < 39)
                        {
                            if (lastLevel == 0)
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel M802庫對庫位置錯誤, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                            else if (lastLevel < 3)
                            {
                                nextColumn = lastColumn;
                                nextRow = lastRow - 18;
                                nextLevel = lastLevel + 1;
                            }
                            else if (lastLevel == 3)
                            {
                                if (lastColumn < 5)
                                {
                                    //錯誤，未抓取Column成功
                                    return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                                }
                                else if (lastColumn < 8)
                                {
                                    nextColumn = lastColumn + 1;
                                    nextRow = lastRow - 18;
                                    nextLevel = 1;
                                }
                                else if (lastColumn == 8)
                                {
                                    nextColumn = 5;
                                    nextRow = lastRow - 17;
                                    nextLevel = 1;
                                    if (lastRow == 38)
                                        nextRow = 3;
                                }
                                else
                                {
                                    //錯誤，未抓取Column成功
                                    return $"Error: LastColumn M801庫對庫位置錯誤, string ver = {lastShelf.Substring(0, 2)} and int ver = {lastColumn}.";
                                }
                            }
                            else
                            {
                                //錯誤，未抓取Level成功
                                return $"Error: LastLevel M802庫對庫位置錯誤, string ver = {lastShelf.Substring(5, 2)} and int ver = {lastLevel}.";
                            }
                        }
                        else
                        {
                            //錯誤，取得Row失敗
                            return $"Error: LastRow M802庫對庫位置錯誤, string ver = {lastShelf.Substring(2, 3)} and int ver = {lastRow}.";
                        }
                    }
                }
                else if(equNo > 2 && equNo < 6)
                {
                    //箱式倉命令
                    //需要使用location (判定為上筆命令之起點)，lastShelf(判定為上筆命令之終點)
                    //row ： 後面16排(26~11) 為雙版滿載儲位； 前9排(02~10)為目標空儲位
                    //順序 03/11/01 <-> 03/02/01, 04/11/01 <-> 04/02/01, 03/11/02 <-> 03/02/02 ... <-> 04/02/05, 03/12/01 <-> ... 04/19/05 <-> 04/10/05, (延續下行)
                    //03/20/01 <-> 03/02/01 ... 04/26/05 <-> 04/08/05, 03/11/01 <-> 03/02/01
                    //跳過 20/{14, 15, 16}/{01,..., 05}
                    int lastStartColumn = 0, lastStartRow = 0, lastStartLevel = 0;
                    if (!string.IsNullOrEmpty(location))
                    {
                        lastStartColumn = Convert.ToInt32(location.Substring(0, 2));
                        lastStartRow = Convert.ToInt32(location.Substring(2, 3));
                        lastStartLevel = Convert.ToInt32(location.Substring(5, 2));
                    }

                    if (lastRow < 11 && lastRow > 1)
                    {
                        nextColumn = lastStartColumn;
                        nextRow = lastStartRow;
                        nextLevel = lastStartLevel;
                    }
                    else if(lastRow > 10 && lastRow < 20)
                    {
                        //雙版滿載儲位前半段
                        //輸出兩個位置，以','分隔
                        if(lastColumn % 4 == 3)
                        {
                            if(equNo == 5 && lastRow > 13 && lastRow < 17)
                            {
                                //跳過 20/{14, 15, 16}/{01,..., 05}
                                if(lastLevel < 5)
                                {
                                    lastColumn = lastColumn + 1;
                                }
                                else if(lastLevel == 5)
                                {
                                    lastColumn = lastColumn + 1;
                                }
                                else
                                {
                                    return $"Error: LastLevel B800庫對庫命令取得錯誤，跳過禁用儲位失敗， lastLevel = {lastLevel}.";
                                }

                                lastShelf = lastColumn.ToString().PadLeft(2, '0') + lastRow.ToString().PadLeft(3, '0') + lastLevel.ToString().PadLeft(2, '0');
                                return FunGetCycleRunNextLocation(lastCmdMode, "", lastShelf);
                            }
                            nextlocation = (equNo * 4).ToString().PadLeft(2, '0') + lastRow.ToString().PadLeft(3, '0') + lastLevel.ToString().PadLeft(2, '0');
                            nextlocation +="," + (equNo * 4).ToString().PadLeft(2, '0') + (lastRow - 9).ToString().PadLeft(3, '0') + lastLevel.ToString().PadLeft(2, '0');
                        }
                        else if(lastColumn % 4 == 0)
                        {
                            if(lastLevel < 5)
                            {
                                nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + lastRow.ToString().PadLeft(3, '0') + (lastLevel + 1).ToString().PadLeft(2, '0');
                                nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow - 9).ToString().PadLeft(3, '0') + (lastLevel + 1).ToString().PadLeft(2, '0');
                            }
                            else if (lastLevel == 5)
                            {
                                if(lastRow < 19)
                                {
                                    nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1).ToString().PadLeft(3, '0') + "01";
                                    nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1 - 9).ToString().PadLeft(3, '0') + "01";
                                }
                                else if(lastRow == 19)
                                {
                                    nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1).ToString().PadLeft(3, '0') + "01";
                                    nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1 - 18).ToString().PadLeft(3, '0') + "01";
                                }
                                else
                                {
                                    return $"Error: LastRow B800庫對庫命令取得失敗，lastRow = {lastRow}.";
                                }
                            }
                        }
                        else
                        {
                            return $"Error: LastColumn B800庫對庫命令取得錯誤，lastColumn = {lastColumn}.";
                        }
                        return nextlocation;
                    }
                    else if (lastRow > 19 && lastRow < 27)
                    {
                        //雙版滿載儲位後半段
                        //輸出兩個位置，以','分隔
                        if (lastColumn % 4 == 3)
                        {
                            nextlocation = (equNo * 4).ToString().PadLeft(2, '0') + lastRow.ToString().PadLeft(3, '0') + lastLevel.ToString().PadLeft(2, '0');
                            nextlocation += "," + (equNo * 4).ToString().PadLeft(2, '0') + (lastRow - 18).ToString().PadLeft(3, '0') + lastLevel.ToString().PadLeft(2, '0');
                        }
                        else if (lastColumn % 4 == 0)
                        {
                            if (lastLevel < 5)
                            {
                                nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + lastRow.ToString().PadLeft(3, '0') + (lastLevel + 1).ToString().PadLeft(2, '0');
                                nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow - 18).ToString().PadLeft(3, '0') + (lastLevel + 1).ToString().PadLeft(2, '0');
                            }
                            else if (lastLevel == 5)
                            {
                                if (lastRow < 26)
                                {
                                    nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1).ToString().PadLeft(3, '0') + "01";
                                    nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + (lastRow + 1 - 18).ToString().PadLeft(3, '0') + "01";
                                }
                                else if (lastRow == 26)
                                {
                                    nextlocation = ((equNo * 4) - 1).ToString().PadLeft(2, '0') + "01101";
                                    nextlocation += "," + ((equNo * 4) - 1).ToString().PadLeft(2, '0') + "00201";
                                }
                                else
                                {
                                    return $"Error: LastRow B800庫對庫命令取得失敗，lastRow = {lastRow}.";
                                }
                            }
                        }
                        else
                        {
                            return $"Error: LastColumn B800庫對庫命令取得錯誤，lastColumn = {lastColumn}.";
                        }
                        return nextlocation;
                    }
                    else
                    {
                        return $"Error: LastRow B800庫對庫位置取得錯誤, lastRow = {lastRow}.";
                    }    

                }
                else
                {
                    return $"Error: LastShelf 庫對庫位置取得EquNo錯誤, lastShelf = {lastShelf} and EquNo = {equNo}.";
                }

                nextlocation = nextColumn.ToString().PadLeft(2, '0') + nextRow.ToString().PadLeft(3, '0') + nextLevel.ToString().PadLeft(2, '0');
            }


            return nextlocation;
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
