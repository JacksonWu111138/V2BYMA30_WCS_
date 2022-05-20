using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Mirle.LCS.Models.Info;
using Mirle.MPLC;
using Mirle.STKC.R46YP320.Simulator.CraneCommand;
using Mirle.Stocker.R46YP320;

namespace Mirle.STKC.R46YP320.Simulator
{
    public class StockerSimulator
    {
        private readonly CraneSimulator _crane1;
        private readonly ThreadWorker _crane1CmdWorker;
        private readonly CraneSimulator _crane2;
        private readonly ThreadWorker _crane2CmdWorker;
        private readonly ThreadWorker _heartbeat;
        private readonly object _interferenceLock = new object();
        private readonly ThreadWorker _iosWorker;
        private readonly IMPLCProvider _mplc;
        private readonly SignalMapper4_11 _mapper;
        private Dictionary<int, EQPortSimulator> _eqPorts;
        private Dictionary<int, IOPortSimulator> _ioPorts;
        private bool _isOtherRequestInterference;
        private bool _isRequestedInterference;
        private Dictionary<int, PortInfo> _portLookup = new Dictionary<int, PortInfo>();

        public StockerSimulator(IMPLCProvider mplc, LCSEnums.ControlMode controlMode)
        {
            _mplc = mplc;
            _mapper = new SignalMapper4_11(mplc);

            Stocker = new CSOTStocker(_mapper, controlMode);

            _crane1 = new CraneSimulator(this, _mapper.GetCraneSignalById(1), 1);
            _crane2 = new CraneSimulator(this, _mapper.GetCraneSignalById(2), 2);

            _eqPorts = new Dictionary<int, EQPortSimulator>();
            var eqs = _mapper.EqPorts.ToList();
            for (int i = 0; i < eqs.Count; i++)
            {
                var eqNo = i + 1;
                _eqPorts.Add(eqNo, new EQPortSimulator(eqs[i], eqNo));
            }

            _ioPorts = new Dictionary<int, IOPortSimulator>();
            var ios = _mapper.IoPorts.ToList();
            for (int i = 0; i < ios.Count; i++)
            {
                var ioNo = i + 1;
                _ioPorts.Add(ioNo, new IOPortSimulator(ios[i], ioNo));
            }

            _heartbeat = new ThreadWorker(HeartBeat);
            _crane1CmdWorker = new ThreadWorker(() => CheckAnyCmd(_crane1), 1000, false);
            _crane2CmdWorker = new ThreadWorker(() => CheckAnyCmd(_crane2), 1000, false);
            _iosWorker = new ThreadWorker(IOsHeartBeat, 500);
        }

        public LCSEnums.AvailStatus AvailStatus => Stocker.AvailStatus;
        public CraneSimulator Crane1 => _crane1;
        public CraneSimulator Crane2 => _crane2;
        public IEnumerable<EQPortSimulator> EQs => _eqPorts.Values;
        public int HandoffAreaEnd { get; private set; }
        public int HandoffAreaStart { get; private set; }
        public IEnumerable<IOPortSimulator> IOs => _ioPorts.Values;
        public int SharedAreaEnd { get; private set; }
        public int SharedAreaStart { get; private set; }
        public CSOTStocker Stocker { get; private set; }

        public CraneSimulator GetCraneById(int craneNo)
        {
            return craneNo == 1 ? _crane1 : _crane2;
        }

        public EQPortSimulator GetEqById(int eqNo)
        {
            _eqPorts.TryGetValue(eqNo, out var eq);
            return eq;
        }

        public IOPortSimulator GetIoById(int ioNo)
        {
            _ioPorts.TryGetValue(ioNo, out var io);
            return io;
        }

        public int GetPortLocation(int portNo)
        {
            try
            {
                if (_portLookup.TryGetValue(portNo, out var portInfo))
                {
                    return Convert.ToInt32($"{portInfo.ShelfId.Substring(1, 1)}{portInfo.ShelfId.Substring(3, 2)}{portInfo.ShelfId.Substring(5, 2)}");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}-{e.StackTrace}");
            }

            return 0;
        }

        public void InitialAllEqPort()
        {
            _mplc.WriteWords("D8702", new int[400]);
            foreach (var eq in this.EQs)
            {
                eq.Initial();
            }
        }

        public void InitialAllIoPort()
        {
            _mplc.WriteWords("D6401", new int[2240]);
            _mplc.WriteWords("D5061", new int[80]);

            foreach (var io in this.IOs)
            {
                io.Initial();
            }
        }

        public void InitialCrane1()
        {
            _mplc.WriteWords("D6001", new int[200]);
            _mplc.WriteWords("D5021", new int[20]);
            _crane1.Initial();
        }

        public void InitialCrane2()
        {
            _mplc.WriteWords("D6201", new int[200]);
            _mplc.WriteWords("D5041", new int[20]);
            _crane2.Initial();
        }

        public void SetCrane1Start(bool isStarted)
        {
            if (isStarted)
            {
                _crane1CmdWorker.Start();
            }
            else
            {
                _crane1CmdWorker.Pause();
            }
        }

        public void SetCrane2Start(bool isStarted)
        {
            if (isStarted)
            {
                _crane2CmdWorker.Start();
            }
            else
            {
                _crane2CmdWorker.Pause();
            }
        }

        public void SetPortInfo(IEnumerable<PortInfo> portInfo)
        {
            foreach (var info in portInfo)
            {
                _portLookup.Add(info.PLCPortId, info);
            }
        }

        internal void AlreadyInSharedArea()
        {
            lock (_interferenceLock)
            {
                _isRequestedInterference = false;
            }
        }

        internal bool RequestEnterSharedArea()
        {
            lock (_interferenceLock)
            {
                if (_isRequestedInterference || _crane1.IsInSharedArea || _crane2.IsInSharedArea)
                {
                    _isOtherRequestInterference = true;
                    return false;
                }
                else
                {
                    _isRequestedInterference = true;
                    _isOtherRequestInterference = false;
                    return true;
                }
            }
        }

        private void CheckAnyCmd(CraneSimulator crane)
        {
            if (crane.IsInSharedArea && _isOtherRequestInterference)
            {
                crane.Escape();
            }

            var commands = new List<ICommand>();

            var alreadyWait = false;
            while (crane.AnyCmd())
            {
                crane.StopReceiveNewCommand();
                var newCmd = crane.GetNewCmd();
                if (newCmd != null && IsValid(newCmd.Info))
                {
                    commands.Add(newCmd);
                }

                if (commands.Count < 2 && alreadyWait == false)
                {
                    crane.StartReceiveNewCommand();
                    SpinWait.SpinUntil(crane.AnyCmd, 3000);
                    alreadyWait = true;
                    continue;
                }
            }

            if (commands.Count == 2 && commands.All(cmd => cmd is TransferCommand || cmd is TransferNoIdCommand))
            {
                var cmd1 = commands[0];
                var cmd2 = commands[1];
                if (cmd1.Info.ForkNo != cmd2.Info.ForkNo
                    && cmd1.Info.SourceBank == cmd2.Info.SourceBank
                    && cmd1.Info.DestinationBank == cmd2.Info.DestinationBank
                    && cmd1.Info.SourceLevel == cmd2.Info.SourceLevel
                    && cmd1.Info.DestinationLevel == cmd2.Info.DestinationLevel)
                {
                    var leftInfo = cmd1.Info.ForkNo == 1 ? cmd1.Info : cmd2.Info;
                    var rightInfo = cmd2.Info.ForkNo == 2 ? cmd2.Info : cmd1.Info;

                    var twinCommand = new TwinCommand(crane, leftInfo, rightInfo);
                    twinCommand.Execute();
                    commands.Clear();
                }
            }

            foreach (var command in commands)
            {
                command.Execute();
            }

            crane.StartReceiveNewCommand();
        }

        private void CheckSharedAreaAndHandOffArea()
        {
            var stocker = _mapper.Stocker;
            var sStart = stocker.Controller.ShareArea_StartBay.GetValue();
            var sEnd = stocker.Controller.ShareArea_EndBay.GetValue();
            var hStart = stocker.Controller.HandOff_StartBay.GetValue();
            var hEnd = stocker.Controller.HandOff_EndBay.GetValue();

            if (SharedAreaStart != sStart || SharedAreaEnd != sEnd ||
                HandoffAreaStart != hStart || HandoffAreaEnd != hEnd)
            {
                SharedAreaStart = sStart;
                SharedAreaEnd = sEnd;
                HandoffAreaStart = hStart;
                HandoffAreaEnd = hEnd;

                stocker.ShareArea_StartBay.SetValue(sStart);
                stocker.ShareArea_EndBay.SetValue(sEnd);
                stocker.HandOff_StartBay.SetValue(hStart);
                stocker.HandOff_EndBay.SetValue(hEnd);

                _crane1.SetStartLocation(1, sStart - 1, 1);
                _crane2.SetStartLocation(1, sEnd + 1, 1);
            }
        }

        private void HeartBeat()
        {
            if (_mapper.Stocker.Controller.Heartbeat.IsOn())
            {
                _mapper.Stocker.Controller.Heartbeat.SetOff();
            }

            CheckSharedAreaAndHandOffArea();
        }

        private void IOsHeartBeat()
        {
            try
            {
                foreach (var ioSimulator in _ioPorts.Values)
                {
                    ioSimulator.Heartbeat();
                }
            }
            catch (Exception e)
            {
            }
        }

        private bool IsValid(CommandInfo cmdInfo)
        {
            return true;
            //return (bank >= _minBank && bank <= _maxBank)
            //       && (bay >= _minBay && bay <= _maxBay)
            //       && (level >= _minLevel && level <= _maxLevel);
        }

        public void DoorOpenHP()
        {
            _mapper.Stocker.SafetyDoorClosed_HP.SetOff();
        }

        public void DoorClosedHP()
        {
            _mapper.Stocker.SafetyDoorClosed_HP.SetOn();
        }

        public void DoorOpenOP()
        {
            _mapper.Stocker.SafetyDoorClosed_OP.SetOff();
        }

        public void DoorClosedOP()
        {
            _mapper.Stocker.SafetyDoorClosed_OP.SetOn();
        }

        public void KeySwitchAutoHP()
        {
            _mapper.Stocker.KeySwitch_HP.SetOn();
        }

        public void KeySwitchOffHP()
        {
            _mapper.Stocker.KeySwitch_HP.SetOff();
        }

        public void KeySwitchAutoOP()
        {
            _mapper.Stocker.KeySwitch_OP.SetOn();
        }

        public void KeySwitchOffOP()
        {
            _mapper.Stocker.KeySwitch_OP.SetOff();
        }

        public void AreaSensorSignalOn(int bit)
        {
            _mapper.Stocker.AreaSensorSignal1.SetValue(bit);
        }

        public void AreaSensorSignalOff()
        {
            _mapper.Stocker.AreaSensorSignal1.SetValue(0);
        }
    }
}
