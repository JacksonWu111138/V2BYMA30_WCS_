using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Manager
{
    public class CurrentExeCmdManager
    {
        //key: craneNo+forkNo 11,12,21,22
        private readonly ConcurrentDictionary<string, ExeCmd> _currentForkCmd = new ConcurrentDictionary<string, ExeCmd>();

        private readonly ConcurrentQueue<ExeCmd> _finishCmdQueue = new ConcurrentQueue<ExeCmd>();

        public CurrentExeCmdManager()
        {
        }

        public void AddCommand(int CraneNo, int ForkNo, ExeCmd exeCmd)
        {
            _currentForkCmd[$"{CraneNo.ToString()}{ForkNo.ToString()}"] = exeCmd;
        }

        public IEnumerable<ExeCmd> GetCurrentCommandByCrane(int CraneNo)
        {
            foreach (var cmdKeyValuePair in _currentForkCmd)
            {
                if (cmdKeyValuePair.Key.StartsWith(CraneNo.ToString()) && cmdKeyValuePair.Value != null)
                {
                    yield return cmdKeyValuePair.Value;
                }
            }
        }

        public ExeCmd GetCurrentCommandByFork(int craneNo, int forkNo)
        {
            _currentForkCmd.TryGetValue($"{craneNo.ToString()}{forkNo.ToString()}", out var currentCmd);
            return currentCmd;
        }

        public ExeCmd GetCurrentCommandByMplcCmdNo(string last5CmdId)
        {
            foreach (var cmdKeyValuePair in _currentForkCmd)
            {
                if (cmdKeyValuePair.Value.TaskNo.EndsWith(last5CmdId))
                    return cmdKeyValuePair.Value;
            }

            return null;
        }

        public void RemoveForkCommand(int craneNo, int forkNo)
        {
            _currentForkCmd.TryRemove($"{craneNo.ToString()}{forkNo.ToString()}", out var cmd);
        }

        public void EnqueueFinishCommand(ExeCmd exeCmd)
        {
            _finishCmdQueue.Enqueue(exeCmd);
        }

        public ExeCmd DequeueFinishCommand()
        {
            if (_finishCmdQueue.IsEmpty) return null;
            _finishCmdQueue.TryDequeue(out var cmd);
            return cmd;
        }

        public bool AllForkHasCommandByCrane(int craneNo)
        {
            return _currentForkCmd.TryGetValue($"{craneNo.ToString()}{1}", out var currentCmd)
            || _currentForkCmd.TryGetValue($"{craneNo.ToString()}{2}", out currentCmd);
        }
    }
}
