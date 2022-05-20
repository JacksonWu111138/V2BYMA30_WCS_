using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.DataBlocks.DeviceRange;
using Mirle.MPLC.DataType;
using Mirle.MPLC.SharedMemory;

using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.LCSShareMemory
{
    public class LCSWatchDog
    {
        private readonly WatchDogSignal _signal;
        private readonly Dictionary<int, WatchDogSignal> _watchDogSignals = new Dictionary<int, WatchDogSignal>();

        private readonly SMReadWriter _sm;

        public LCSWatchDog(string stockerId)
        {
            _sm = new SMReadWriter();
            var range = new DDeviceRange("D0", "D100");
            var sharedMemoryName = $@"Global\{stockerId}-WatchDogFlag";
            _sm.AddDataBlock(new SMDataBlock(range, sharedMemoryName));

            _signal = new WatchDogSignal();
            _signal.PLCRComm = new Word(_sm, "D0");
            _signal.SECSComm = new Word(_sm, "D1");
            _signal.STKC = new Word(_sm, "D2");
            _signal.TaskAgent = new Word(_sm, "D3");

            _signal.StopAllService = new Word(_sm, "D9");
            _signal.StopAllService.SetValue(0);

            for (int i = 1; i <= 3; i++)
            {
                var watchDog = new WatchDogSignal();
                watchDog.PLCRComm = new Word(_sm, $"D{i * 10 + 0}");
                watchDog.SECSComm = new Word(_sm, $"D{i * 10 + 1}");
                watchDog.STKC = new Word(_sm, $"D{i * 10 + 2}");
                watchDog.TaskAgent = new Word(_sm, $"D{i * 10 + 3}");
                _watchDogSignals.Add(i, watchDog);
            }
        }

        public enum WatchDogStatus
        {
            Down = 0,
            Run = 1,
            Show = 3,
        }

        public WatchDogStatus PLCRCommStatus
        {
            get => (WatchDogStatus)_signal.PLCRComm.GetValue();
        }

        public WatchDogStatus SECSCommStatus
        {
            get => (WatchDogStatus)_signal.SECSComm.GetValue();
        }

        public WatchDogStatus STKCStatus
        {
            get => (WatchDogStatus)_signal.STKC.GetValue();
        }

        public WatchDogStatus TaskAgentStatus
        {
            get => (WatchDogStatus)_signal.TaskAgent.GetValue();
        }

        public bool IsNeedToStopAllService => _signal.StopAllService.GetValue() != 0;

        public void SetAllDown()
        {
            _signal.PLCRComm.SetValue((int)WatchDogStatus.Down);
            _signal.SECSComm.SetValue((int)WatchDogStatus.Down);
            _signal.STKC.SetValue((int)WatchDogStatus.Down);
            _signal.TaskAgent.SetValue((int)WatchDogStatus.Down);

            foreach (var watchDogSignal in _watchDogSignals.Values)
            {
                watchDogSignal.PLCRComm.SetValue((int)WatchDogStatus.Down);
                watchDogSignal.SECSComm.SetValue((int)WatchDogStatus.Down);
                watchDogSignal.STKC.SetValue((int)WatchDogStatus.Down);
                watchDogSignal.TaskAgent.SetValue((int)WatchDogStatus.Down);
            }
        }

        public void SetPLCRComm(WatchDogStatus status)
        {
            _signal.PLCRComm.SetValue((int)status);
        }

        public void SetPLCRCommById(WatchDogStatus status, int id)
        {
            _watchDogSignals.TryGetValue(id, out var watchDogSignal);
            if (watchDogSignal != null)
            {
                watchDogSignal.PLCRComm.SetValue((int)status);
            }
        }

        public WatchDogStatus GetPLCRCommById(int id)
        {
            _watchDogSignals.TryGetValue(id, out var watchDogSignal);
            if (watchDogSignal != null)
            {
                return (WatchDogStatus)watchDogSignal.PLCRComm.GetValue();
            }
            return WatchDogStatus.Down;
        }

        public void SetSECSComm(WatchDogStatus status)
        {
            _signal.SECSComm.SetValue((int)status);
        }

        public void SetSTKC(WatchDogStatus status)
        {
            _signal.STKC.SetValue((int)status);
        }

        public void SetTaskAgent(WatchDogStatus status)
        {
            _signal.TaskAgent.SetValue((int)status);
        }

        private class WatchDogSignal
        {
            public Word PLCRComm { get; internal set; }
            public Word SECSComm { get; internal set; }
            public Word STKC { get; internal set; }
            public Word TaskAgent { get; internal set; }

            public Word StopAllService { get; internal set; }
        }

        public void StopAllServiceACK()
        {
            _signal.StopAllService.SetValue(0);
        }

        public void StopAllServiceRequest()
        {
            _signal.StopAllService.SetValue(99);
        }
    }
}