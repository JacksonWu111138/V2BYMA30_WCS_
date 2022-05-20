namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal class MoveCommand : ICommand
    {
        private readonly CraneSimulator _crane;
        public CommandInfo Info { get; private set; } = new CommandInfo();

        public MoveCommand(CraneSimulator crane)
        {
            _crane = crane;
            Info.CommandType = "Move";
        }

        public void Execute()
        {
            _crane.BeginCommand(Info);

            var completeCode = _crane.MoveT3(Info);

            _crane.EndCommand(Info, completeCode);
        }
    }
}