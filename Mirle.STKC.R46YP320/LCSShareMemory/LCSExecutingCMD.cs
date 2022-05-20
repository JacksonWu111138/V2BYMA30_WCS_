using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.DataBlocks.DeviceRange;
using Mirle.MPLC.DataType;
using Mirle.MPLC.SharedMemory;

using System.Collections.Generic;

namespace Mirle.STKC.R46YP320.LCSShareMemory
{
    public class LCSExecutingCMD
    {
        private SMReadWriter _sm;
        private readonly Dictionary<string, ExecutingCMD> _cmds = new Dictionary<string, ExecutingCMD>();

        public LCSExecutingCMD(string stockerId)
        {
            _sm = new SMReadWriter();
            var range = new DDeviceRange("D0", "D2000");
            var sharedMemoryName = $@"Global\{stockerId}-EC";
            _sm.AddDataBlock(new SMDataBlock(range, sharedMemoryName));

            InitialLayout();
        }

        private void InitialLayout()
        {
            var index = 0;
            var addr = 0;
            for (int craneNo = 1; craneNo <= 2; craneNo++)
            {
                for (int forkNo = 1; forkNo <= 2; forkNo++)
                {
                    var cmd = new CommandSignal();
                    cmd.AllBlock = new WordBlock(_sm, $"D{addr}", 400);

                    cmd.CommandID = new WordBlock(_sm, $"D{addr}", 32);
                    cmd.TaskNo = new WordBlock(_sm, $"D{addr += 32}", 8);
                    cmd.CSTID = new WordBlock(_sm, $"D{addr + 40}", 32);
                    cmd.TaskState = new Word(_sm, $"D{addr + 72}");
                    cmd.CMDState = new Word(_sm, $"D{addr + 73}");
                    cmd.TransferMode = new Word(_sm, $"D{addr + 74}");
                    cmd.Source = new WordBlock(_sm, $"D{addr + 75}", 4);
                    cmd.SourceBay = new Word(_sm, $"D{addr + 79}");
                    cmd.Destination = new WordBlock(_sm, $"D{addr + 80}", 4);
                    cmd.DestinationBay = new Word(_sm, $"D{addr + 84}");
                    cmd.TravelAxisSpeed = new Word(_sm, $"D{addr + 85}");
                    cmd.LifterAxisSpeed = new Word(_sm, $"D{addr + 86}");
                    cmd.RotateAxisSpeed = new Word(_sm, $"D{addr + 87}");
                    cmd.ForkAxisSpeed = new Word(_sm, $"D{addr + 88}");
                    cmd.UserID = new WordBlock(_sm, $"D{addr + 89}", 10);
                    cmd.CSTType = new WordBlock(_sm, $"D{addr + 99}", 1);
                    cmd.BCRReadFlag = new WordBlock(_sm, $"D{addr + 100}", 1);
                    cmd.BCRReadDT = new WordBlock(_sm, $"D{addr + 101}", 13);
                    cmd.BCRReplyCSTID = new WordBlock(_sm, $"D{addr + 114}", 32);
                    cmd.BCRReadStatus = new Word(_sm, $"D{addr + 146}");
                    cmd.Priority = new Word(_sm, $"D{addr + 147}");
                    cmd.QueueDT = new WordBlock(_sm, $"D{addr + 148}", 13);
                    cmd.InitialDT = new WordBlock(_sm, $"D{addr + 161}", 13);
                    cmd.ActiveDT = new WordBlock(_sm, $"D{addr + 174}", 13);
                    cmd.FinishDT = new WordBlock(_sm, $"D{addr + 187}", 13);
                    cmd.Cycle1StartDT = new WordBlock(_sm, $"D{addr + 200}", 13);
                    cmd.AtSourceDT = new WordBlock(_sm, $"D{addr + 213}", 13);
                    cmd.Fork1StartDT = new WordBlock(_sm, $"D{addr + 226}", 13);
                    cmd.CSTOnCraneDT = new WordBlock(_sm, $"D{addr + 239}", 13);
                    cmd.Cycle2StartDT = new WordBlock(_sm, $"D{addr + 252}", 13);
                    cmd.AtDestinationDT = new WordBlock(_sm, $"D{addr + 265}", 13);
                    cmd.Fork2StartDT = new WordBlock(_sm, $"D{addr + 278}", 13);
                    cmd.CSTTackOffCraneDT = new WordBlock(_sm, $"D{addr + 291}", 13);
                    cmd.FinishLocation = new WordBlock(_sm, $"D{addr + 304}", 13);
                    cmd.EmptyCST = new WordBlock(_sm, $"D{addr + 317}", 1);

                    _cmds.Add($"{craneNo}{forkNo}", new ExecutingCMD(craneNo, forkNo, cmd));
                    index++;
                    addr = index * 400;
                }
            }
        }

        public ExecutingCMD GetExecutingCMD(int craneNo, int forkNo = 1)
        {
            _cmds.TryGetValue($"{craneNo}{forkNo}", out var executingCmd);
            return executingCmd;
        }

        internal class CommandSignal
        {
            public WordBlock AllBlock { get; internal set; }
            public WordBlock CommandID { get; internal set; }
            public WordBlock TaskNo { get; internal set; }
            public WordBlock CSTID { get; internal set; }
            public Word TaskState { get; internal set; }
            public Word CMDState { get; internal set; }
            public Word TransferMode { get; internal set; }
            public WordBlock Source { get; internal set; }
            public Word SourceBay { get; internal set; }
            public WordBlock Destination { get; internal set; }
            public Word DestinationBay { get; internal set; }
            public Word TravelAxisSpeed { get; internal set; }
            public Word LifterAxisSpeed { get; internal set; }
            public Word RotateAxisSpeed { get; internal set; }
            public Word ForkAxisSpeed { get; internal set; }
            public WordBlock UserID { get; internal set; }
            public WordBlock CSTType { get; internal set; }
            public WordBlock BCRReadFlag { get; internal set; }
            public WordBlock BCRReadDT { get; internal set; }
            public WordBlock BCRReplyCSTID { get; internal set; }
            public Word BCRReadStatus { get; internal set; }
            public Word Priority { get; internal set; }
            public WordBlock QueueDT { get; internal set; }
            public WordBlock InitialDT { get; internal set; }
            public WordBlock ActiveDT { get; internal set; }
            public WordBlock FinishDT { get; internal set; }
            public WordBlock Cycle1StartDT { get; internal set; }
            public WordBlock AtSourceDT { get; internal set; }
            public WordBlock Fork1StartDT { get; internal set; }
            public WordBlock CSTOnCraneDT { get; internal set; }
            public WordBlock Cycle2StartDT { get; internal set; }
            public WordBlock AtDestinationDT { get; internal set; }
            public WordBlock Fork2StartDT { get; internal set; }
            public WordBlock CSTTackOffCraneDT { get; internal set; }
            public WordBlock FinishLocation { get; internal set; }
            public WordBlock EmptyCST { get; internal set; }
        }
    }
}