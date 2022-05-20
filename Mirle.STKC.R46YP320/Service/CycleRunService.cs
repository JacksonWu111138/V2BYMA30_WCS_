using Mirle.STKC.R46YP320.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.Service
{
    public class CycleRunService
    {
        private ConcurrentDictionary<int, CurrentCmd> _currentCmds = new ConcurrentDictionary<int, CurrentCmd>();

        private class CurrentCmd
        {
            public SCCommand ScCmd { get; }
            public string TaskNo { get; set; }
            public int Index { get; }

            public CurrentCmd(SCCommand scCmd, int index)
            {
                ScCmd = scCmd;
                Index = index;
            }
        }

        private readonly TaskCommandService _taskCommandService;
        private ThreadWorker _cycleRunWorker;
        private int _index = 0;

        public IEnumerable<SCCommand> CurrentCmds => _currentCmds.Select(r => r.Value.ScCmd);

        public CycleRunService(TaskCommandService taskCommandService)
        {
            _taskCommandService = taskCommandService;
            _cycleRunWorker = new ThreadWorker(CycleRunProc, 1000, false);
        }

        public void Start()
        {
            _cycleRunWorker.Start();
        }

        public void Stop()
        {
            _cycleRunWorker.Pause();
        }

        public void SetCycle(SCCommand scCmd)
        {
            _currentCmds.TryAdd(_index, new CurrentCmd(scCmd, _index++));
        }

        private void CycleRunProc()
        {
            foreach (var currentCmd in _currentCmds.Values.OrderBy(c => c.Index))
            {
                if (string.IsNullOrEmpty(currentCmd.TaskNo))
                {
                    currentCmd.TaskNo = _taskCommandService.CreateNewTaskCommand(currentCmd.ScCmd);
                }
                else
                {
                    var cmds = _taskCommandService.GetByTaskNo(currentCmd.TaskNo);
                    if (cmds.Any() == false)
                    {
                        currentCmd.TaskNo = _taskCommandService.CreateNewTaskCommand(currentCmd.ScCmd);
                    }
                }
            }
        }

        public void ClearCycle()
        {
            _currentCmds.Clear();
        }
        public void ClearTask()
        {
            _taskCommandService.InsertHistory();
        }
    }
}