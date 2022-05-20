namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal class ScanCommand : ICommand
    {
        private readonly CraneSimulator _crane;
        public CommandInfo Info { get; private set; } = new CommandInfo();

        public ScanCommand(CraneSimulator crane)
        {
            _crane = crane;
            Info.CommandType = "Scan";
        }

        public void Execute()
        {
            _crane.BeginCommand(Info);

            var completeCode = _crane.MoveT1(Info);

            string bcrCstId = string.Empty;
            completeCode = _crane.Scan(Info, out bcrCstId);

            _crane.EndCommand(Info, completeCode);
        }
    }
}