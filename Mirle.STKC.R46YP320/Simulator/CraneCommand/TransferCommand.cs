using System.Threading.Tasks;

namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal class TransferCommand : ICommand
    {
        private readonly CraneSimulator _crane;
        public CommandInfo Info { get; private set; } = new CommandInfo();

        public TransferCommand(CraneSimulator crane)
        {
            _crane = crane;
            Info.CommandType = "Transfer";
        }

        public void Execute()
        {
            _crane.BeginCommand(Info);

            var completeCode = string.Empty;
            string bcrCstId = string.Empty;

            if (Info.FromLocation != 0 && Info.ToLocation != 0)
            {
                completeCode = _crane.MoveT1(Info);
                completeCode = _crane.Retrieve(Info, out bcrCstId);
                if (completeCode != CompleteCode.Success_91_Retrieve)
                {
                    _crane.EndCommand(Info, completeCode);
                    return;
                }

                Task.Delay(100).Wait();

                completeCode = _crane.MoveT3(Info);
                completeCode = _crane.Deposit(Info);
                _crane.EndCommand(Info, completeCode);
            }
            else if (Info.FromLocation != 0)
            {
                completeCode = _crane.MoveT1(Info);
                completeCode = _crane.Retrieve(Info, out bcrCstId);
                _crane.EndCommand(Info, completeCode);
            }
            else if (Info.ToLocation != 0)
            {
                completeCode = _crane.MoveT3(Info);
                completeCode = _crane.Deposit(Info);
                _crane.EndCommand(Info, completeCode);
            }
            else
            {
                _crane.EndCommand(Info, CompleteCode.Error_E1_WrongCommand);
            }
        }
    }
}
