using System;
using System.Collections.Generic;
using Mirle.Def.U2NMMA30;
using Mirle.Stocker.R46YP320;
using Mirle.STKC.R46YP320;
using Mirle.Def;
using System.Linq;
using Mirle.STKC.R46YP320.Views;
using System.Windows.Forms;

namespace Mirle.Micron.U2NMMA30
{
    public class clsMicronStocker
    {
        private static STKCHost[] _stkcHost = new STKCHost[4];
        private static IStocker[] _stocker = new IStocker[4];
        private static StockerController[] _stockerController = new StockerController[4];
        private static View.CraneStatusView stockerStateView;
        public static void StockerInitial(ASRSINI lcsini)
        {
            string[] sSpeed = lcsini.Device.Speed.Split(',');
            for (int i = 0; i < 4; i++)
            {
                _stkcHost[i] = new STKCHost(new InitialTrace(), i + 1);
                _stockerController[i] = _stkcHost[i].GetSTKCManager().GetStockerController();
                _stocker[i] = _stockerController[i].GetStocker();
                _stocker[i].GetCraneById(1).CanAutoSetRun = true;
                _stocker[i].GetCraneById(1).Speed = int.Parse(sSpeed[i]);
                SetForkLimit(i + 1);
                string[] sforkEnable;
                switch (i)
                {
                    case 0:
                        sforkEnable = lcsini.Fork.S1.Split(',');
                        break;
                    case 1:
                        sforkEnable = lcsini.Fork.S2.Split(',');
                        break;
                    case 2:
                        sforkEnable = lcsini.Fork.S3.Split(',');
                        break;
                    default:
                        sforkEnable = lcsini.Fork.S4.Split(',');
                        break;
                }

                for (int fork = 0; fork < 2; fork++)
                {
                    _stocker[i].GetCraneById(1).GetForkById(fork + 1).GetConfig().Enable = 
                        sforkEnable[fork] == "1" ? true : false;
                }
            }

            stockerStateView = new View.CraneStatusView();
        }

        public static Form GetStockerStsView()
        {
            return stockerStateView;
        }

        public static IStocker GetStockerById(int StockerID)
        {
            return _stocker[StockerID - 1];
        }

        public static STKCHost GetSTKCHostById(int StockerID)
        {
            return _stkcHost[StockerID - 1];
        }

        public static Structure.Info.TransferBatch GetCommand(int StockerID, int ForkID)
        {
            return GetStockerById(StockerID).GetCraneById(1).GetForkById(ForkID).GetCommand();
        }

        private static void SetForkLimit(int StockerID)
        {
            GetStockerById(StockerID).GetCraneById(1).GetForkById(1).GetConfig().Limit.Start = 1;
            GetStockerById(StockerID).GetCraneById(1).GetForkById(2).GetConfig().Limit.Start = 2;
            switch (StockerID)
            {
                case 1:
                    GetStockerById(StockerID).GetCraneById(1).GetForkById(1).GetConfig().Limit.End = 70;
                    GetStockerById(StockerID).GetCraneById(1).GetForkById(2).GetConfig().Limit.End = 71;
                    break;
                case 2:
                case 3:
                default:
                    GetStockerById(StockerID).GetCraneById(1).GetForkById(1).GetConfig().Limit.End = 72;
                    GetStockerById(StockerID).GetCraneById(1).GetForkById(2).GetConfig().Limit.End = 73;
                    break;
            }
        }

        public static string GetShelfIdByFork(int Fork)
        {
            return Fork == 1 ? clsConstValue.STKC_FinishLoc.LeftFork : clsConstValue.STKC_FinishLoc.RightFork;
        }

        public static Location GetForkLocation(Location Start, string sLoc)
        {
            var StockerID = int.Parse(Start.DeviceId);
            var crane = GetStockerById(StockerID).GetCraneById(1);
            var LeftHasCarrier = (crane.GetForkById(1).HasCarrier || !crane.GetForkById(1).GetConfig().Enable);
            var RightHasCarrier = (crane.GetForkById(2).HasCarrier || !crane.GetForkById(2).GetConfig().Enable);
            var stk = MicronLocation.GetMicronLocationById(StockerID);
            clsEnum.Fork fork = clsEnum.Fork.None;
            if (Start.LocationId == LocationDef.Location.A1_04.ToString() ||
                Start.LocationId == LocationDef.Location.A1_10.ToString() ||
                Start.LocationId == LocationDef.Location.A1_16.ToString() ||
                Start.LocationId == LocationDef.Location.A1_22.ToString())
            {
                if (clsTool.IsLimit(sLoc, ref fork))
                {
                    if(fork == clsEnum.Fork.Left)
                        return stk.GetLocations.FirstOrDefault(loc => loc.LocationId == LocationDef.Location.LeftFork.ToString());
                    else
                        return stk.GetLocations.FirstOrDefault(loc => loc.LocationId == LocationDef.Location.RightFork.ToString());
                }
                else
                {
                    if (!RightHasCarrier)
                        return stk.GetLocations.FirstOrDefault(loc => loc.LocationId == LocationDef.Location.RightFork.ToString());
                    else return stk.GetLocations.FirstOrDefault(loc => loc.LocationId == LocationDef.Location.LeftFork.ToString());
                }
            }
            else return stk.GetLocations.FirstOrDefault(loc => loc.LocationId == LocationDef.Location.LeftFork.ToString());
        }

        public static bool CheckTwinForkIsOK(int StockerID, out clsEnum.Fork CanUsefork)
        {
            bool bFlag = true; CanUsefork = clsEnum.Fork.None;
            for (int i = 1; i <= 2; i++)
            {
                if (GetStockerById(StockerID).GetCraneById(1).GetForkById(i).GetConfig().Enable)
                    CanUsefork = i == 1 ? clsEnum.Fork.Left : clsEnum.Fork.Right;
                else bFlag = false;
            }

            return bFlag;
        }

        public static bool CheckCraneIsIdle(int StockerID)
        {
            if (!GetStockerById(StockerID).GetCraneById(1).IsIdle)
            {
                return false;
            }
            else return true;
        }

        public static bool CheckSTKAllGood(ref int[] NotGoodID)
        {
            bool bFlag = true;
            for (int i = 1; i <= 4; i++)
            {
                if (!GetSTKCHostById(i).GetSTKCManager().IsPlcConn)
                {
                    bFlag = false;
                    Array.Resize(ref NotGoodID, NotGoodID.Length + 1);
                    NotGoodID[NotGoodID.Length - 1] = i;
                    continue;
                }

                if (!GetStockerById(i).GetCraneById(1).IsInService)
                {
                    bFlag = false;
                    Array.Resize(ref NotGoodID, NotGoodID.Length + 1);
                    NotGoodID[NotGoodID.Length - 1] = i;
                    continue;
                }

                if (GetStockerById(i).GetCraneById(1).IsAlarm)
                {
                    bFlag = false;
                    Array.Resize(ref NotGoodID, NotGoodID.Length + 1);
                    NotGoodID[NotGoodID.Length - 1] = i;
                    continue;
                }
            }
            return bFlag;
        }
    }
}
