using Mirle.Extensions;
using Mirle.Stocker.R46YP320.Signal;

using System.Linq;
using System.Threading.Tasks;

namespace Mirle.STKC.R46YP320.Simulator
{
    public class IOPortSimulator
    {
        private readonly IOPortSignal _signal;

        private readonly int _ioNo;

        public IOPortSimulator(IOPortSignal signal, int ioNo)
        {
            _signal = signal;
            _ioNo = ioNo;
        }

        public void Initial()
        {
            _signal.Run.SetOff();
            _signal.Down.SetOn();

            Task.Delay(400).Wait();
            _signal.Down.SetOff();

            _signal.AutoManualMode.SetOn();
            _signal.OutMode.SetOn();
            _signal.LoadOK.SetOn();
            _signal.PortModeChangeable.SetOn();

            _signal.SRI.AutoManualSwitchIsAuto.SetOn();
            _signal.SRI.SafetyDoorClosed.SetOn();

            _signal.RunEnable.SetOff();
            _signal.Run.SetOn();
        }

        public void Heartbeat()
        {
            if (_signal.RunEnable.IsOn() && _signal.Controller.Run.IsOn())
            {
                _signal.Controller.Run.SetOff();
                _signal.RunEnable.SetOff();
                _signal.Run.SetOn();
            }

            if (_signal.RunEnable.IsOff() && _signal.Controller.Stop.IsOn())
            {
                _signal.Controller.Stop.SetOff();
                _signal.Run.SetOff();
                _signal.RunEnable.SetOn();
            }

            if (_signal.PortModeChangeable.IsOn() && _signal.Controller.RequestInputMode.IsOn() && _signal.Run.IsOn())
            {
                _signal.PortModeChangeable.SetOff();
                _signal.LoadOK.SetOff();
                _signal.OutMode.SetOff();
                Task.Delay(2000).Wait();
                _signal.InMode.SetOn();
                _signal.UnloadOK.SetOn();
                _signal.PortModeChangeable.SetOn();
            }

            if (_signal.PortModeChangeable.IsOn() && _signal.Controller.RequestOutputMode.IsOn() && _signal.Run.IsOn())
            {
                _signal.PortModeChangeable.SetOff();
                _signal.UnloadOK.SetOff();
                _signal.InMode.SetOff();
                Task.Delay(2000).Wait();
                _signal.OutMode.SetOn();
                _signal.LoadOK.SetOn();
                _signal.PortModeChangeable.SetOn();
            }

            var stage1 = _signal.Stages.ToList()[0];
            if (_signal.Controller.MoveBack.IsOn() && _signal.InMode.IsOn() && stage1.LoadPresence.IsOn() && _signal.Run.IsOn())
            {
                _signal.InMode.SetOff();
                _signal.OutMode.SetOn();
                stage1.LoadPresence.SetOff();
                stage1.CarrierId.Clear();
                _signal.CSTRemoveCheck_Req.SetOn();
                Task.Delay(500).Wait();
                _signal.CSTRemoveCheck_Req.SetOff();
            }
        }

        public void PresentOn(int stageIndex, string cstId)
        {
            if (stageIndex < 1 && stageIndex > 5) return;
            _signal.Stages.ToList()[stageIndex - 1].CarrierId.SetData(cstId.ToIntArray(10));
            Task.Delay(500).Wait();
            _signal.Stages.ToList()[stageIndex - 1].LoadPresence.SetOn();
        }

        public void PresentOff(int stageIndex)
        {
            if (stageIndex < 1 && stageIndex > 5) return;
            _signal.Stages.ToList()[stageIndex - 1].LoadPresence.SetOff();
        }

        public void Remove()
        {
            if (_signal.OutMode.IsOn() && _signal.Run.IsOn())
            {
                Task.Run(() =>
                {
                    _signal.CSTRemoveCheck_Req.SetOn();
                    Task.Delay(3000).Wait();
                    _signal.CSTRemoveCheck_Req.SetOff();
                    Task.Delay(500).Wait();
                    _signal.Stages.ToList()[0].CarrierId.Clear();
                });
            }
        }

        public void WaitIn(string cstId)
        {
            if (_signal.WaitIn.IsOn())
            {
                _signal.WaitIn.SetOff();
                Task.Delay(100).Wait();
                _signal.Stages.ToList()[0].LoadPresence.SetOff();
            }

            if (_signal.InMode.IsOn() && _signal.Run.IsOn())
            {
                Task.Run(() =>
                {
                    _signal.BCRReadResult.SetData(cstId.ToIntArray(10));
                    Task.Delay(100).Wait();
                    _signal.BCRReadDone.SetOn();
                    Task.Delay(200).Wait();
                    _signal.BCRReadResult.Clear();
                    Task.Delay(150).Wait();
                    _signal.Stages.ToList()[0].LoadPresence.SetOn();
                    Task.Delay(150).Wait();
                    _signal.Stages.ToList()[0].CarrierId.SetData(cstId.ToIntArray(10));
                    Task.Delay(200).Wait();
                    _signal.WaitIn.SetOn();
                    Task.Delay(300).Wait();
                    _signal.BCRReadDone.SetOff();
                });
            }
        }

        public void WaitOut(string cstId)
        {
            if (_signal.OutMode.IsOn() && _signal.Run.IsOn())
            {
                Task.Run(() =>
                {
                    _signal.Stages.ToList()[0].CarrierId.SetData(cstId.ToIntArray(10));
                    Task.Delay(500).Wait();
                    _signal.WaitOut.Set(_signal.WaitOut.IsOff());
                });
            }
        }

        public bool IsReadyForWaitIn()
        {
            return _signal.WaitIn.IsOff() && _signal.Stages.ToList()[0].LoadPresence.IsOff() && _signal.Stages.ToList()[0].CarrierId.GetData().ToASCII() == "";
        }

        public void ClearAlarm()
        {
            _signal.ErrorCode.SetValue(0);
            _signal.Fault.SetOff();
        }

        public void SetAlarm(int alarmCode)
        {
            _signal.Fault.SetOn();
            _signal.ErrorCode.SetValue(alarmCode);
            var pcErrorIndex = _signal.Controller.PcErrorIndex.GetValue();
            _signal.ErrorIndex.SetValue(++pcErrorIndex);
        }
    }
}
