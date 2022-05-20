using Mirle.Def.U2NMMA30;
using System.Collections.Generic;
using Mirle.Structure;
using Mirle.Def;
using Mirle.ASRS.Conveyor.U2NMMA30;
using System.Linq;
using System.Data;
using Mirle.ASRS.Conveyor.U2NMMA30.Signal;
using Mirle.ASRS.Conveyor.U2NMMA30.View;
using System.Windows.Forms;

namespace Mirle.Micron.U2NMMA30
{
    public class clsMicronCV
    {
        private static ConveyorController conveyorController;
        public static readonly Dictionary<int, ConveyorInfo[]> CV_Group = new Dictionary<int, ConveyorInfo[]>();
        public static readonly Dictionary<int, string[]> CV_Alarm = new Dictionary<int, string[]>();
        private static MainView _mainView;

        public static void FunInitalCVController(clsPlcConfig CV)
        {
            var Config_CV = new ConveyorConfig("CV", CV.MPLCNo, CV.InMemorySimulator, CV.UseMCProtocol);
            Config_CV.SetIPAddress(CV.MPLCIP);
            Config_CV.SetPort(CV.MPLCPort);
            conveyorController = new ConveyorController(Config_CV);
            conveyorController.Start();
            _mainView = new MainView(conveyorController);
            ConveyorInfo[] cv1 = new ConveyorInfo[] { ConveyorDef.A1_01, ConveyorDef.A1_02, ConveyorDef.A1_04, ConveyorDef.A1_05 };
            CV_Group.Add(1, cv1);

            ConveyorInfo[] cv2 = new ConveyorInfo[] { ConveyorDef.A1_07, ConveyorDef.A1_08, ConveyorDef.A1_10, ConveyorDef.A1_11 };
            CV_Group.Add(2, cv2);

            ConveyorInfo[] cv3 = new ConveyorInfo[] { ConveyorDef.A1_13, ConveyorDef.A1_14, ConveyorDef.A1_16, ConveyorDef.A1_17 };
            CV_Group.Add(3, cv3);

            ConveyorInfo[] cv4 = new ConveyorInfo[] { ConveyorDef.A1_19, ConveyorDef.A1_20, ConveyorDef.A1_22, ConveyorDef.A1_23 };
            CV_Group.Add(4, cv4);

            ConveyorController.CycleCountMax = CV.CycleCount_Max;
        }


        public static string GetCVAlarm(int bufferIndex, int alarmBit)
        {
            string[] alarm = CV_Alarm[bufferIndex];
            return alarm[alarmBit];
        }

        public static int GetBufferCount()
        {
            return SignalMapper.BufferCount;
        }


        public static ConveyorController GetConveyorController()
        {
            return conveyorController;
        }

        public static bool CheckInPortIsStockOutReady_ForLeftFork(int StockerID, ref ConveyorInfo conveyor)
        {
            List<ConveyorInfo> lstConveyor;
            int StartIndex;
            switch (StockerID)
            {
                case 1:
                    StartIndex = 5;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_05, ConveyorDef.A1_04 };
                    break;
                case 2:
                    StartIndex = 11;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_11, ConveyorDef.A1_10 };
                    break;
                case 3:
                    StartIndex = 17;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_17, ConveyorDef.A1_16 };
                    break;
                default:
                    StartIndex = 23;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_23, ConveyorDef.A1_22 };
                    break;
            }

            for (int i = 0; i < 2; i++)
            {
                if(conveyorController.GetBuffer(StartIndex).Ready == (int)clsEnum.Ready.OUT)
                {
                    conveyor = lstConveyor[i];
                    return true;
                }

                StartIndex--;
            }

            return false;
        }

        public static bool CheckInPortIsStockOutReady_ForRightFork(int StockerID, ref ConveyorInfo conveyor)
        {
            switch (StockerID)
            {
                case 1:
                    conveyor = ConveyorDef.A1_04;
                    break;
                case 2:
                    conveyor = ConveyorDef.A1_10;
                    break;
                case 3:
                    conveyor = ConveyorDef.A1_16;
                    break;
                default:
                    conveyor = ConveyorDef.A1_22;
                    break;
            }

            if (conveyorController.GetBuffer(conveyor.Index).Ready == (int)clsEnum.Ready.OUT)
            {
                return true;
            }
            else return false;
        }

        public static bool CheckOutPortIsStockOutReady_ForLeftFork(int StockerID, ref ConveyorInfo conveyor)
        {
            List<ConveyorInfo> lstConveyor;
            int StartIndex;
            switch (StockerID)
            {
                case 1:
                    StartIndex = 2;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_02, ConveyorDef.A1_01 };
                    break;
                case 2:
                    StartIndex = 8;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_08, ConveyorDef.A1_07 };
                    break;
                case 3:
                    StartIndex = 14;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_14, ConveyorDef.A1_13 };
                    break;
                default:
                    StartIndex = 20;
                    lstConveyor = new List<ConveyorInfo> { ConveyorDef.A1_20, ConveyorDef.A1_19 };
                    break;
            }

            for (int i = 0; i < 2; i++)
            {
                if (conveyorController.GetBuffer(StartIndex).Ready == (int)clsEnum.Ready.OUT)
                {
                    conveyor = lstConveyor[i];
                    return true;
                }

                StartIndex--;
            }

            return false;
        }

        public static bool CheckOutPortIsStockOutReady_ForRightFork(int StockerID, ref ConveyorInfo conveyor)
        {
            switch (StockerID)
            {
                case 1:
                    conveyor = ConveyorDef.A1_01;
                    break;
                case 2:
                    conveyor = ConveyorDef.A1_07;
                    break;
                case 3:
                    conveyor = ConveyorDef.A1_13;
                    break;
                default:
                    conveyor = ConveyorDef.A1_19;
                    break;
            }

            if (conveyorController.GetBuffer(conveyor.Index).Ready == (int)clsEnum.Ready.OUT)
            {
                return true;
            }
            else return false;
        }

        public static bool CheckStockInPortIsReady(int StockerID, ref ConveyorInfo conveyor)
        {
            ConveyorInfo buffer1 = new ConveyorInfo();
            ConveyorInfo buffer2 = new ConveyorInfo();
            switch(StockerID)
            {
                case 1:
                    buffer1 = ConveyorDef.A1_04;
                    buffer2 = ConveyorDef.A1_05;
                    break;
                case 2:
                    buffer1 = ConveyorDef.A1_10;
                    buffer2 = ConveyorDef.A1_11;
                    break;
                case 3:
                    buffer1 = ConveyorDef.A1_16;
                    buffer2 = ConveyorDef.A1_17;
                    break;
                default:
                    buffer1 = ConveyorDef.A1_22;
                    buffer2 = ConveyorDef.A1_23;
                    break;
            }

            if (conveyorController.GetBuffer(buffer1.Index).Ready == (int)clsEnum.Ready.IN)
            {
                conveyor = buffer1;
                return true;
            }
            else if (conveyorController.GetBuffer(buffer2.Index).Ready == (int)clsEnum.Ready.IN)
            {
                conveyor = buffer2;
                return true;
            }
            else return false;
        }

        public static bool CheckStockOutPortHasError(int StockerID)
        {
            int StartIndex;
            switch (StockerID)
            {
                case 1:
                    StartIndex = 1; break;
                case 2:
                    StartIndex = 7; break;
                case 3:
                    StartIndex = 13; break;
                default:
                    StartIndex = 19; break;
            }

            if (conveyorController.GetBuffer(StartIndex).EMO || conveyorController.GetBuffer(StartIndex).Error || !conveyorController.GetBuffer(StartIndex).Auto ||
                conveyorController.GetBuffer(StartIndex + 1).EMO || conveyorController.GetBuffer(StartIndex + 1).Error || !conveyorController.GetBuffer(StartIndex + 1).Auto)
                return true;
            else return false;
        }

        public static bool StockOutPortIsAllNoReady(int StockerID)
        {
            int StartIndex;
            switch (StockerID)
            {
                case 1:
                    StartIndex = 1; break;
                case 2:
                    StartIndex = 7; break;
                case 3:
                    StartIndex = 13; break;
                default:
                    StartIndex = 19; break;
            }

            return (conveyorController.GetBuffer(StartIndex).Ready != (int)clsEnum.Ready.OUT &&
                    conveyorController.GetBuffer(StartIndex + 1).Ready != (int)clsEnum.Ready.OUT);
        }

        public static bool StockOutPortIsAllReady(int StockerID)
        {
            int StartIndex;
            switch (StockerID)
            {
                case 1:
                    StartIndex = 1; break;
                case 2:
                    StartIndex = 7; break;
                case 3:
                    StartIndex = 13; break;
                default:
                    StartIndex = 19; break;
            }

            return (conveyorController.GetBuffer(StartIndex).Ready == (int)clsEnum.Ready.OUT &&
                    conveyorController.GetBuffer(StartIndex + 1).Ready == (int)clsEnum.Ready.OUT);
        }

        public static int GetPathByStockerID(int StockerID)
        {
            switch(StockerID)
            {
                case 1:
                    return ConveyorDef.A1_05.Path;
                case 2:
                    return ConveyorDef.A1_11.Path;
                case 3:
                    return ConveyorDef.A1_17.Path;
                case 4:
                    return ConveyorDef.A1_23.Path;
                default:
                    return 0;
            }
        }

        public static int GetPathByStnNo(string sStnNo)
        {
            if(sStnNo == ConveyorDef.A1_41.StnNo)
            {
                return ConveyorDef.A1_41.Path;
            }
            else if (sStnNo == ConveyorDef.A1_42.StnNo)
            {
                return ConveyorDef.A1_42.Path;
            }
            else if (sStnNo == ConveyorDef.A1_43.StnNo)
            {
                return ConveyorDef.A1_43.Path;
            }
            else
            {
                return ConveyorDef.A1_44.Path;
            }
        }

        public static string GetStnNoByPath(int Path)
        {
            if (Path == ConveyorDef.A1_41.Path)
            {
                return ConveyorDef.A1_41.StnNo;
            }
            else if (Path == ConveyorDef.A1_42.Path)
            {
                return ConveyorDef.A1_42.StnNo;
            }
            else if (Path == ConveyorDef.A1_43.Path)
            {
                return ConveyorDef.A1_43.StnNo;
            }
            else
            {
                return ConveyorDef.A1_44.StnNo;
            }
        }

        public static bool GetPathByStockOut(string StnNoList, string BackupPortList, string StnNo_Now,
            ref bool bIsChangeBackupPort, ref string NewStnList, ref int Path)
        {
            Path = 0;
            ConveyorInfo buffer = new ConveyorInfo();
            string[] sStnNo = StnNoList.Split(',');
            if (string.IsNullOrWhiteSpace(StnNo_Now))
            {
                for (int s = 0; s < sStnNo.Length; s++)
                {
                    buffer = GetBufferByStnNo(sStnNo[s]);
                    if (conveyorController.GetBuffer(buffer.Index).Auto &&
                        !conveyorController.GetBuffer(buffer.Index).Error)
                    {
                        Path = buffer.Path; NewStnList = StnNoList;
                        return true;
                    }
                }

                if (string.IsNullOrWhiteSpace(BackupPortList)) return false;
                else
                {
                    string[] backupPort = BackupPortList.Split(',');
                    for (int s = 0; s < backupPort.Length; s++)
                    {
                        buffer = GetBufferByStnNo(backupPort[s]);
                        if (conveyorController.GetBuffer(buffer.Index).Auto &&
                            !conveyorController.GetBuffer(buffer.Index).Error)
                        {
                            Path = buffer.Path; bIsChangeBackupPort = true;
                        }

                        for (int i = 0; i < sStnNo.Length; i++)
                        {
                            if (i == 0) NewStnList = buffer.StnNo;
                            else NewStnList += $",{sStnNo[i]}";
                        }

                        if (Path > 0) return true;
                    }

                    return false;
                }
            }
            else
            {
                for (int s = 0; s < sStnNo.Length; s++)
                {
                    if (sStnNo[s] != StnNo_Now)
                    {
                        buffer = GetBufferByStnNo(sStnNo[s]);
                        if (Path == 0 && conveyorController.GetBuffer(buffer.Index).Auto &&
                            !conveyorController.GetBuffer(buffer.Index).Error)
                        {
                            Path = buffer.Path;
                        }

                        if (string.IsNullOrWhiteSpace(NewStnList)) NewStnList = buffer.StnNo;
                        else NewStnList += $",{buffer.StnNo}";
                    }
                }

                if (Path > 0) return true;

                if (string.IsNullOrWhiteSpace(BackupPortList)) return false;
                else
                {
                    string[] backupPort = BackupPortList.Split(',');
                    for (int s = 0; s < backupPort.Length; s++)
                    {
                        if (backupPort[s] != StnNo_Now)
                        {
                            buffer = GetBufferByStnNo(backupPort[s]);
                            if (conveyorController.GetBuffer(buffer.Index).Auto &&
                                !conveyorController.GetBuffer(buffer.Index).Error)
                            {
                                for (int i = 0; i < sStnNo.Length; i++)
                                {
                                    if (i == 0) NewStnList = buffer.StnNo;
                                    else NewStnList += $",{sStnNo[i]}";
                                }

                                Path = buffer.Path; bIsChangeBackupPort = true;
                                return true;

                            }
                        }
                    }

                    return false;
                }
            }
        }

        public static ConveyorInfo GetBufferByStnNo(string sStnNo)
        {
            if (sStnNo == ConveyorDef.A1_41.StnNo)
            {
                return ConveyorDef.A1_41;
            }
            else if (sStnNo == ConveyorDef.A1_42.StnNo)
            {
                return ConveyorDef.A1_42;
            }
            else if (sStnNo == ConveyorDef.A1_43.StnNo)
            {
                return ConveyorDef.A1_43;
            }
            else if (sStnNo == ConveyorDef.A1_44.StnNo)
            {
                return ConveyorDef.A1_44;
            }
            else return null;
        }

        public static ConveyorInfo GetBufferByStkPortID(int StockerID, int Port)
        {
            switch(StockerID)
            {
                case 1:
                    if (Port == ConveyorDef.A1_01.StkPortID) return ConveyorDef.A1_01;
                    else if (Port == ConveyorDef.A1_02.StkPortID) return ConveyorDef.A1_02;
                    else if (Port == ConveyorDef.A1_04.StkPortID) return ConveyorDef.A1_04;
                    else return ConveyorDef.A1_05;
                case 2:
                    if (Port == ConveyorDef.A1_07.StkPortID) return ConveyorDef.A1_07;
                    else if (Port == ConveyorDef.A1_08.StkPortID) return ConveyorDef.A1_08;
                    else if (Port == ConveyorDef.A1_10.StkPortID) return ConveyorDef.A1_10;
                    else return ConveyorDef.A1_11;
                case 3:
                    if (Port == ConveyorDef.A1_13.StkPortID) return ConveyorDef.A1_13;
                    else if (Port == ConveyorDef.A1_14.StkPortID) return ConveyorDef.A1_14;
                    else if (Port == ConveyorDef.A1_16.StkPortID) return ConveyorDef.A1_16;
                    else return ConveyorDef.A1_17;
                default:
                    if (Port == ConveyorDef.A1_19.StkPortID) return ConveyorDef.A1_19;
                    else if (Port == ConveyorDef.A1_20.StkPortID) return ConveyorDef.A1_20;
                    else if (Port == ConveyorDef.A1_22.StkPortID) return ConveyorDef.A1_22;
                    else return ConveyorDef.A1_23;
            }
        }

        public static ConveyorInfo GetNextBuffer(ConveyorInfo buffer)
        {
            if (buffer == ConveyorDef.A1_05) return ConveyorDef.A1_04;
            else if (buffer == ConveyorDef.A1_11) return ConveyorDef.A1_10;
            else if (buffer == ConveyorDef.A1_17) return ConveyorDef.A1_16;
            else return ConveyorDef.A1_22;
        }

        public static bool GetStockOutBuffer(int StockerID, int fork, ref ConveyorInfo buffer, ref string sRemark)
        {
            if (StockOutPortIsAllNoReady(StockerID))
            {
                sRemark = $"Error: Stocker{StockerID}的CV皆無出庫Ready！";
                return false;
            }
            else
            {
                #region 左側
                if (fork == 1)
                {
                    if (!CheckOutPortIsStockOutReady_ForLeftFork(StockerID, ref buffer))
                    {
                        sRemark = $"Error: Stocker{StockerID}的CV皆無出庫Ready！";
                        return false;
                    }
                    else return true;
                }
                else
                {
                    if (!CheckOutPortIsStockOutReady_ForRightFork(StockerID, ref buffer))
                    {
                        sRemark = $"Error: Stocker{StockerID}的CV皆無出庫Ready！";
                        return false;
                    }
                    else return true;
                }
                #endregion 左側
            }
        }

        public static ConveyorInfo GetBufferByStkPort(int StockerID, int StkPortID)
        {
            switch(StockerID)
            {
                case 1:
                    if (StkPortID == ConveyorDef.A1_01.StkPortID) return ConveyorDef.A1_01;
                    else return ConveyorDef.A1_02;
                case 2:
                    if (StkPortID == ConveyorDef.A1_07.StkPortID) return ConveyorDef.A1_07;
                    else return ConveyorDef.A1_08;
                case 3:
                    if (StkPortID == ConveyorDef.A1_13.StkPortID) return ConveyorDef.A1_13;
                    else return ConveyorDef.A1_14;
                default:
                    if (StkPortID == ConveyorDef.A1_19.StkPortID) return ConveyorDef.A1_19;
                    else return ConveyorDef.A1_20;
            }
        }

        public static Form GetMainView()
        {
            return _mainView;
        }

        public static MainView GetMainView_Object()
        {
            return _mainView;
        }
    }
}
