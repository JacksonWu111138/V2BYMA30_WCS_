using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Mirle.Stocker.R46YP320.Signal;
using Mirle.STKC.R46YP320.Simulator.CraneCommand;
using Mirle.Extensions;

namespace Mirle.STKC.R46YP320.Simulator
{
    public class CraneSimulator
    {
        private readonly StockerSimulator _stocker;
        private readonly CraneSignal _signal;
        private readonly int _craneNo;
        private readonly CraneControllerSignal _ctrl;
        private readonly ForkSignal _leftFork;
        private readonly ForkSignal _rightFork;

        private CommandInfo _leftForkCmd;
        private CommandInfo _rightForkCmd;
        private readonly ConcurrentQueue<CommandInfo> _cmdQueue = new ConcurrentQueue<CommandInfo>();
        private int _currentBank;
        private int _currentBay;
        private int _currentLevel;
        private bool _craneMoving;
        private bool _fork1Moving;
        private bool _fork2Moving;

        private bool _t1Enable;
        private bool _t2Enable;
        private bool _t3Enable;
        private bool _t4Enable;
        private string _bcrReplyCstid;
        private bool _enableDoubleStorageSimulation;
        private bool _enableEmptyRetrieveSimulation;
        private bool _enableIdMismatchSimulation;
        private bool _enableTypeMismatchSimulation;
        private bool _enableIdReadFailSimulation;
        private bool _enableE1WrongCommandSimulation;
        private bool _enableCycle1InterlockError;
        private bool _enableCycle2InterlockError;
        private bool _enableInterlockCstOnFork;
        private bool _ReqAckFlag = true;
        private int _customerCompleteCode;

        public string ExcutingCommandNo { get; private set; }

        public bool IsInSharedArea
        {
            get
            {
                if (_currentBay >= _stocker.SharedAreaStart && _currentBay <= _stocker.SharedAreaEnd)
                {
                    return true;
                }
                return false;
            }
        }

        public CraneSimulator(StockerSimulator stocker, CraneSignal signal, int craneNo)
        {
            _stocker = stocker;
            _signal = signal;
            _craneNo = craneNo;
            _leftFork = signal.LeftFork;
            _rightFork = signal.RightFork;

            _ctrl = _signal.Controller;
        }

        public void Initial()
        {
            _ReqAckFlag = false;
            Task.Delay(100).Wait();
            _ReqAckFlag = true;

            _enableE1WrongCommandSimulation = false;
            _enableDoubleStorageSimulation = false;
            _enableEmptyRetrieveSimulation = false;
            _enableCycle1InterlockError = false;
            _enableCycle2InterlockError = false;

            _signal.InService.SetOn();
            _signal.Run.SetOn();
            _signal.Idle.SetOn();

            //_crane.SRI.TheAMSwitchIsAuto_MPLC_HP.SetOn();
            //_crane.SRI.TheAMSwitchIsAuto_MPLC_OP.SetOn();
            //_crane.SRI.TheAMSwitchIsAuto_RMPLC_HP.SetOn();
            //_crane.SRI.TheAMSwitchIsAuto_RMPLC_OP.SetOn();

            _signal.SRI.MainCircuitOnEnable.SetOn();
            _signal.SRI.HIDPowerOn.SetOn();
            //_crane.SRI.HIDPowerOn_Crane2.SetOn();

            _leftFork.Idle.SetOn();
            _leftFork.ForkHomePosition.SetOn();

            _rightFork.Idle.SetOn();
            _rightFork.ForkHomePosition.SetOn();
        }

        public void SetStartLocation(int bank, int bay, int level)
        {
            _currentBank = bank;
            _currentBay = bay;
            _currentLevel = level;
            if (bank == 1)
            {
                _signal.ForkAtBank1.SetOn();
                _signal.ForkAtBank2.SetOff();
            }
            else
            {
                _signal.ForkAtBank1.SetOff();
                _signal.ForkAtBank2.SetOn();
            }
            _signal.CurrentBay.SetValue(bay);
            _signal.CurrentLevel.SetValue(level);
            _signal.Location.SetValue(bank * 10000 + bay * 100 + level);
            _signal.LocationUpdated.SetOn();
        }

        public bool AnyCmd()
        {
            if (_ctrl.CmdType_TransferWithoutIDRead.IsOn()) return true;
            if (_ctrl.CmdType_Transfer.IsOn()) return true;
            if (_ctrl.CmdType_Move.IsOn()) return true;
            if (_ctrl.CmdType_Scan.IsOn()) return true;
            return false;
        }

        public void StartReceiveNewCommand()
        {
            _signal.ReadyToReceiveNewCommand.SetOn();
        }

        public void StopReceiveNewCommand()
        {
            _signal.ReadyToReceiveNewCommand.SetOff();
        }

        internal ICommand GetNewCmd()
        {
            ICommand newCmd = null;
            if (_ctrl.CmdType_TransferWithoutIDRead.IsOn()) newCmd = new TransferNoIdCommand(this);
            if (_ctrl.CmdType_Transfer.IsOn()) newCmd = new TransferCommand(this);
            if (_ctrl.CmdType_Move.IsOn()) newCmd = new MoveCommand(this);
            if (_ctrl.CmdType_Scan.IsOn()) newCmd = new ScanCommand(this);

            if (newCmd != null)
            {
                newCmd.Info.ForkNo = _ctrl.UseLeftFork.IsOn() ? 1 : 2;
                var cmdData = _ctrl.CommandData.GetData();
                newCmd.Info.CommandNo = cmdData[0];
                var fromLocation = cmdData[1];
                if (fromLocation > 200)
                {
                    newCmd.Info.FromLocation = fromLocation;
                }
                else
                {
                    newCmd.Info.FromLocation = _stocker.GetPortLocation(fromLocation);
                }

                var toLocation = cmdData[2];
                if (toLocation > 200)
                {
                    newCmd.Info.ToLocation = toLocation;
                }
                else
                {
                    newCmd.Info.ToLocation = _stocker.GetPortLocation(toLocation);
                }
                var tmpCstId = new int[10];
                Array.Copy(cmdData, 4, tmpCstId, 0, 10);
                newCmd.Info.CstId = tmpCstId.ToASCII();

                _ctrl.CmdType_TransferWithoutIDRead.SetOff();
                _ctrl.CmdType_Transfer.SetOff();
                _ctrl.CmdType_Move.SetOff();
                _ctrl.CmdType_Scan.SetOff();
                _ctrl.UseLeftFork.SetOff();
                _ctrl.UseRightFork.SetOff();

                _ctrl.CommandData.Clear();

                _signal.TransferCommandReceived.SetOn();
                Task.Delay(2000).Wait();
                _signal.TransferCommandReceived.SetOff();

                //test WrongCmd
                if (_enableE1WrongCommandSimulation)
                {
                    SimulateE1WrongCommand(newCmd);
                    return null;
                }

                return newCmd;
            }
            return null;
        }

        private void SimulateE1WrongCommand(ICommand newCmd)
        {
            //_signal.T3.SetValue(71);
            Task.Delay(800).Wait();

            _signal.CommandBuffer1.SetValue(newCmd.Info.CommandNo);
            _signal.Idle.SetOff();
            _signal.Active.SetOn();
            Task.Delay(700).Wait();

            _signal.RequestSignal.TransferRequestWrongReq_LF.SetOn();
            while (_ReqAckFlag)
            {
                Task.Delay(50).Wait();
                if (_ctrl.AckSignal.TransferRequestWrongAck_LF.IsOn())
                {
                    Task.Delay(500).Wait();
                    _signal.RequestSignal.TransferRequestWrongReq_LF.SetOff();
                    _signal.Active.SetOff();
                    _signal.CommandBuffer1.SetValue(0);
                    Task.Delay(500).Wait();
                    _signal.Idle.SetOn();
                    break;
                }
            }
        }

        private void SetIdle()
        {
            _signal.Active.SetOff();
            _signal.Idle.SetOn();
        }

        private void SetBusy()
        {
            _signal.Idle.SetOff();
            _signal.Active.SetOn();
        }

        public void Escape()
        {
            var escBay = _craneNo == 1 ? _stocker.SharedAreaStart - 1 : _stocker.SharedAreaEnd + 1;
            var cmdInfo = new CommandInfo();
            cmdInfo.CommandNo = _craneNo == 1 ? 30001 : 30002;
            cmdInfo.ForkNo = 1;
            cmdInfo.ToLocation = 10000 + escBay * 100 + 1;

            BeginCommand(cmdInfo);
            _signal.Escape.SetOn();
            var completeCode = MoveT3(cmdInfo);
            _signal.Escape.SetOff();
            EndCommand(cmdInfo, completeCode);
        }

        internal string MoveT1(CommandInfo info)
        {
            StartT1(info.ForkNo);
            var completeCode = Move(info.SourceBank, info.SourceBay, info.SourceLevel);
            StopT1(info.ForkNo);



            if (_customerCompleteCode !=0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCode;
        }

        internal string MoveT3(CommandInfo info)
        {
            StartT3(info.ForkNo);
            var completeCode = Move(info.DestinationBank, info.DestinationBay, info.DestinationLevel);
            StopT3(info.ForkNo);

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCode;
        }

        private string Move(int bank, int bay, int level)
        {
            var noticeCraneInSharedArea = false;
            if (this.IsInSharedArea == false && bay >= _stocker.SharedAreaStart && bay <= _stocker.SharedAreaEnd)
            {
                while (_stocker.RequestEnterSharedArea() == false)
                {
                    Task.Delay(1000).Wait();
                }
                noticeCraneInSharedArea = true;
            }

            _signal.Location.SetValue(0);
            _signal.LocationUpdated.SetOff();
            _craneMoving = true;
            var bankTask = Task.Run(() =>
            {
                if (_currentBank != bank)
                {
                    _currentBank = bank;

                    _signal.Rotating.SetOn();
                    Task.Delay(1000).Wait();

                    if (_currentBank == 1)
                    {
                        _signal.ForkAtBank1.SetOn();
                        _signal.ForkAtBank2.SetOff();
                    }
                    else
                    {
                        _signal.ForkAtBank1.SetOff();
                        _signal.ForkAtBank2.SetOn();
                    }
                }

                _signal.Rotating.SetOff();
            });

            var bayTask = Task.Run(() =>
            {
                if (_currentBay != bay)
                {
                    _signal.TravelMoving.SetOn();
                    Task.Delay(1000).Wait();
                    while (_currentBay != bay)
                    {
                        _signal.TravelMoving.SetOn();
                        _currentBay += _currentBay < bay ? 1 : -1;
                        _signal.CurrentPosition.SetValue(_currentBay * 100);
                        _signal.CurrentBay.SetValue(_currentBay);
                        if (_currentBay >= _stocker.SharedAreaStart && _currentBay <= _stocker.SharedAreaEnd)
                        {
                            _signal.Dual_HandOffReserved.SetOn();
                            if (noticeCraneInSharedArea)
                            {
                                _stocker.AlreadyInSharedArea();
                                noticeCraneInSharedArea = false;
                            }
                        }
                        else
                        {
                            _signal.Dual_HandOffReserved.SetOff();
                        }
                        Task.Delay(100).Wait();
                    }
                    Task.Delay(1000).Wait();
                }

                _signal.TravelMoving.SetOff();
            });

            var levelTask = Task.Run(() =>
            {
                if (_currentLevel != level)
                {
                    _signal.LifterActing.SetOn();
                    Task.Delay(1000).Wait();
                    while (_currentLevel != level)
                    {
                        _currentLevel += _currentLevel < level ? 1 : -1;
                        _signal.CurrentLevel.SetValue(_currentLevel);
                        Task.Delay(200).Wait();
                    }
                    Task.Delay(1000).Wait();
                }
                _signal.LifterActing.SetOff();
            });

            Task.WaitAll(bankTask, bayTask, levelTask);
            _signal.Location.SetValue(bank * 10000 + bay * 100 + level);
            _craneMoving = false;
            _signal.LocationUpdated.SetOn();

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return CompleteCode.Success_92_MoveAndDeposit;
        }

        internal string RetrieveNoId(CommandInfo info)
        {
            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;
            StartT2(info.ForkNo);
            Task.Delay(2000).Wait();
            var completeCodeResult = CompleteCode.Success_91_Retrieve;
            if (_enableEmptyRetrieveSimulation)
            {
                completeCodeResult = CompleteCode.Error_E2_EmptyRetrieve;
            }
            else if (_customerCompleteCode == 241)
            {
                if (_enableInterlockCstOnFork)
                {
                    fork.LoadPresenceSensor.SetOn();
                    fork.CSTPresence.SetOn();
                    fork.Rised.SetOn();
                    Task.Delay(1000).Wait();
                    fork.Rised.SetOff();
                }
                return _customerCompleteCode.ToString("X2");
            }
            else if (_enableTypeMismatchSimulation)
            {
                //fork.CstTypeIsSmall.Set(fork.CstTypeIsSmall.IsOn());
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
                completeCodeResult = CompleteCode.Error_EB_TypeMismatch;
            }
            else if (_enableCycle1InterlockError)
            {
                if (_enableInterlockCstOnFork)
                {
                    fork.LoadPresenceSensor.SetOn();
                    fork.CSTPresence.SetOn();
                    fork.Rised.SetOn();
                    Task.Delay(1000).Wait();
                    fork.Rised.SetOff();
                }
                completeCodeResult = CompleteCode.Error_ED_Interlock;
            }
            else
            {
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
            }

            StopT2(info.ForkNo);

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCodeResult;
        }

        internal string Retrieve(CommandInfo info, out string bcrCstId)
        {
            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;
            StartT2(info.ForkNo);
            Task.Delay(2000).Wait();

            bcrCstId = string.IsNullOrWhiteSpace(_bcrReplyCstid) ? info.CstId : _bcrReplyCstid;
            var completeCodeResult = CompleteCode.Success_91_Retrieve;
            if (_enableEmptyRetrieveSimulation)
            {
                bcrCstId = "NOCST1";
                completeCodeResult = CompleteCode.Error_E2_EmptyRetrieve;
            }
            else if (_customerCompleteCode == 241)
            {
                if (_enableInterlockCstOnFork)
                {
                    fork.LoadPresenceSensor.SetOn();
                    fork.CSTPresence.SetOn();
                    fork.Rised.SetOn();
                    Task.Delay(1000).Wait();
                    fork.Rised.SetOff();
                }
                return _customerCompleteCode.ToString("X2");
            }
            else if (_enableTypeMismatchSimulation)
            {
                //fork.CstTypeIsSmall.Set(fork.CstTypeIsSmall.IsOn());
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
                completeCodeResult = CompleteCode.Error_EB_TypeMismatch;
            }
            else if (_enableCycle1InterlockError)
            {
                if (_enableInterlockCstOnFork)
                {
                    fork.LoadPresenceSensor.SetOn();
                    fork.CSTPresence.SetOn();
                    fork.Rised.SetOn();
                    Task.Delay(1000).Wait();
                    fork.Rised.SetOff();
                }
                completeCodeResult = CompleteCode.Error_ED_Interlock;
            }
            else if (_enableIdReadFailSimulation)
            {
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
                completeCodeResult = CompleteCode.Error_E3_ScanError;
            }
            else if (_enableIdMismatchSimulation)
            {
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
                completeCodeResult = CompleteCode.Error_E4_IdMismatch;
            }
            else
            {
                fork.LoadPresenceSensor.SetOn();
                fork.CSTPresence.SetOn();
                fork.Rised.SetOn();
                Task.Delay(1000).Wait();
                fork.Rised.SetOff();
            }

            switch (bcrCstId)
            {
                case "ERROR1":
                case "NORD01":
                    completeCodeResult = CompleteCode.Error_E3_ScanError;
                    break;

                case "NOCST1":
                    completeCodeResult = CompleteCode.Error_E2_EmptyRetrieve;
                    break;

                default:
                    if (bcrCstId != info.CstId)
                        completeCodeResult = CompleteCode.Error_E4_IdMismatch;
                    break;
            }

            Task.Delay(100).Wait();
            fork.BCRResultCstId.SetData(bcrCstId.ToIntArray(10));
            StopT2(info.ForkNo);

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCodeResult;
        }

        internal string Deposit(CommandInfo info)
        {
            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;
            StartT4(info.ForkNo);

            var completeCodeResult = CompleteCode.Success_92_MoveAndDeposit;
            if (_enableDoubleStorageSimulation)
            {
                completeCodeResult = CompleteCode.Error_EC_DoubleStorage;
            }
            else if (_enableTypeMismatchSimulation)
            {
                //fork.CstTypeIsSmall.Set(!fork.CstTypeIsSmall.IsOn());
                completeCodeResult = CompleteCode.Error_EB_TypeMismatch;
            }
            else if (_enableCycle2InterlockError)
            {
                if (_enableInterlockCstOnFork == false)
                {
                    fork.LoadPresenceSensor.SetOff();
                    fork.CSTPresence.SetOff();
                    fork.Downed.SetOn();
                    Task.Delay(1000).Wait();
                    fork.Downed.SetOff();
                }
                completeCodeResult = CompleteCode.Error_ED_Interlock;
            }
            else
            {
                fork.LoadPresenceSensor.SetOff();
                Task.Delay(2000).Wait();
                fork.CSTPresence.SetOff();
                fork.Downed.SetOn();
                Task.Delay(1000).Wait();
                fork.Downed.SetOff();
            }

            StopT4(info.ForkNo);

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCodeResult;
        }

        internal string Scan(CommandInfo info, out string bcrCstId)
        {
            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;
            StartT2(info.ForkNo);
            Task.Delay(2000).Wait();

            fork.LoadPresenceSensor.SetOn();
            fork.CSTPresence.SetOn();

            var completeCodeResult = CompleteCode.ScanFinish_97;
            if (_enableEmptyRetrieveSimulation)
            {
                bcrCstId = "NOCST1";
                fork.BCRResultCstId.SetData(bcrCstId.ToIntArray(10));
                Task.Delay(1000).Wait();
                fork.LoadPresenceSensor.SetOff();
                Task.Delay(1000).Wait();
                fork.CSTPresence.SetOff();
                StopT2(info.ForkNo);
            }
            else if (_enableIdMismatchSimulation)
            {
                completeCodeResult = CompleteCode.Error_E4_IdMismatch;
            }
            else if (_enableIdReadFailSimulation)
            {
                completeCodeResult = CompleteCode.Error_E3_ScanError;
            }

            bcrCstId = string.IsNullOrWhiteSpace(_bcrReplyCstid) ? info.CstId : _bcrReplyCstid;
            fork.BCRResultCstId.SetData(bcrCstId.ToIntArray(10));
            Task.Delay(1000).Wait();
            fork.LoadPresenceSensor.SetOff();
            Task.Delay(1000).Wait();
            fork.CSTPresence.SetOff();
            StopT2(info.ForkNo);

            if (_customerCompleteCode != 0)
            {
                return _customerCompleteCode.ToString("X2");
            }
            return completeCodeResult;
        }

        internal void BeginCommand(CommandInfo info)
        {
            ExcutingCommandNo = info.CommandNo.ToString("D5");
            SetBusy();
            _signal.T1.SetValue(0);
            _signal.T2.SetValue(0);
            _signal.T3.SetValue(0);
            _signal.T4.SetValue(0);

            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;

            fork.BCRResultCstId.Clear();
            fork.TrackingCstId.Clear();
            if (!string.IsNullOrWhiteSpace(info.CstId))
            {
                fork.TrackingCstId.SetData(info.CstId.ToIntArray(10));
            }
            fork.CurrentCommand.SetValue(info.CommandNo);
            fork.Idle.SetOff();
        }

        public void StartT1(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Cycle1.SetOn();

            if (_t1Enable) return;
            Task.Run(() =>
            {
                _t1Enable = true;
                var timer = new Stopwatch();
                timer.Start();
                while (_t1Enable)
                {
                    var span = timer.ElapsedMilliseconds;
                    _signal.T1.SetValue(Convert.ToInt32(span / 100));
                    Task.Delay(200).Wait();
                }
            });
        }

        public void StopT1(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Cycle1.SetOff();

            _t1Enable = false;
        }

        public void StartT2(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.ForkHomePosition.SetOff();
            fork.Forking1.SetOn();
            fork.Forking.SetOn();

            if (_t2Enable) return;
            Task.Run(() =>
            {
                _t2Enable = true;
                var timer = new Stopwatch();
                timer.Start();
                while (_t2Enable)
                {
                    var span = timer.ElapsedMilliseconds;
                    _signal.T2.SetValue(Convert.ToInt32(span / 100));
                    Task.Delay(200).Wait();
                }
            });
        }

        public void StopT2(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Forking1.SetOff();
            fork.Forking.SetOff();
            fork.ForkHomePosition.SetOn();

            _t2Enable = false;
        }

        public void StartT3(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Cycle2.SetOn();

            if (_t3Enable) return;
            Task.Run(() =>
            {
                _t3Enable = true;
                var timer = new Stopwatch();
                timer.Start();
                while (_t3Enable)
                {
                    var span = timer.ElapsedMilliseconds;
                    _signal.T3.SetValue(Convert.ToInt32(span / 100));
                    Task.Delay(200).Wait();
                }
            });
        }

        public void StopT3(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Cycle2.SetOff();

            _t3Enable = false;
        }

        public void StartT4(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.ForkHomePosition.SetOff();
            fork.Forking2.SetOn();
            fork.Forking.SetOn();

            if (_t4Enable) return;
            Task.Run(() =>
            {
                _t4Enable = true;
                var timer = new Stopwatch();
                timer.Start();
                while (_t4Enable)
                {
                    var span = timer.ElapsedMilliseconds;
                    _signal.T4.SetValue(Convert.ToInt32(span / 100));
                    Task.Delay(200).Wait();
                }
            });
        }

        public void StopT4(int forkNo)
        {
            var fork = forkNo == 1 ? _leftFork : _rightFork;
            fork.Forking2.SetOff();
            fork.Forking.SetOff();
            fork.ForkHomePosition.SetOn();

            _t4Enable = false;
        }

        internal void EndCommand(CommandInfo info, string completeCode)
        {
            ExcutingCommandNo = string.Empty;
            SetIdle();
            var fork = info.ForkNo == 1 ? _leftFork : _rightFork;
            fork.Idle.SetOn();
            fork.Cycle1.SetOff();
            fork.Forking1.SetOff();
            fork.Cycle2.SetOff();
            fork.Forking2.SetOff();
            fork.Forking.SetOff();
            fork.ForkHomePosition.SetOn();
            fork.CurrentCommand.SetValue(0);
            fork.CompletedCode.SetValue(completeCode.HexToInt());
            fork.CompletedCommand.SetValue(info.CommandNo);
        }

        public void SetBCRReplyCstid(string cstid)
        {
            _bcrReplyCstid = cstid;
        }

        public void SetDoubleStorageSimulation(bool isEnable)
        {
            _enableDoubleStorageSimulation = isEnable;
        }

        public void SetEmptyRetrieveSimulation(bool isEnable)
        {
            _enableEmptyRetrieveSimulation = isEnable;
        }

        public void SetIdMismatchSimulation(bool isEnable)
        {
            _enableIdMismatchSimulation = isEnable;
        }

        public void SetTypeMismatchSimulation(bool isEnable)
        {
            if (isEnable==false)
            {
                //_leftFork.CstTypeIsSmall.SetOff();
                //_rightFork.CstTypeIsSmall.SetOff();
            }
            _enableTypeMismatchSimulation = isEnable;
        }

        public void SetIdReadFailSimulation(bool isEnable)
        {
            _enableIdReadFailSimulation = isEnable;
        }

        public void SetE1WrongCommandSimulation(bool isEnable)
        {
            _enableE1WrongCommandSimulation = isEnable;
        }

        public void ClearAlarm()
        {
            _signal.Error.SetOff();
            _signal.ErrorCode.SetValue(0);
        }

        public void SetAlarm(int alarmCode)
        {
            _signal.Error.SetOn();
            _signal.ErrorCode.SetValue(alarmCode);
            var pcErrorIndex = _signal.Controller.PcErrorIndex.GetValue();
            _signal.ErrorIndex.SetValue(++pcErrorIndex);
        }

        public void SetCycle1InterlockError(bool isEnable)
        {
            _enableCycle1InterlockError = isEnable;
        }

        public void SetCycle2InterlockError(bool isEnable)
        {
            _enableCycle2InterlockError = isEnable;
        }

        public void SetInterlockCstOnFork(bool isEnable)
        {
            _enableInterlockCstOnFork = isEnable;
        }

        public void ToggleSingleCraneMode()
        {
            if (_signal.SingleCraneMode.IsOn())
            {
                _signal.SingleCraneMode.SetOff();
            }
            else
            {
                _signal.SingleCraneMode.SetOn();
            }
        }

        public void SetCustomerCompleteCode(string completeCode)
        {
            try
            {
                _customerCompleteCode = completeCode.HexToInt();
            }
            catch
            {
                _customerCompleteCode = 0;
            }
        }
    }
}
