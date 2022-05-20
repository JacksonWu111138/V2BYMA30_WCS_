using System.Threading.Tasks;

namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal class TwinCommand : ICommand
    {
        private readonly CraneSimulator _crane;
        private readonly CommandInfo _leftInfo;
        private readonly CommandInfo _rightInfo;
        public CommandInfo Info { get; private set; } = new CommandInfo();

        public TwinCommand(CraneSimulator crane, CommandInfo leftInfo, CommandInfo rightInfo)
        {
            _crane = crane;
            Info = leftInfo;

            _leftInfo = leftInfo;
            _rightInfo = rightInfo;

            Info.CommandType = "TwinTransfer";
        }

        public void Execute()
        {
            _crane.BeginCommand(_leftInfo);
            _crane.BeginCommand(_rightInfo);

            var completeCode = string.Empty;
            var bcrCstId_LF = string.Empty;
            var bcrCstId_RF = string.Empty;
            var completeCode_LF = string.Empty;
            var completeCode_RF = string.Empty;

            if (_leftInfo.FromLocation != 0 && _leftInfo.ToLocation != 0)
            {
                completeCode = _crane.MoveT1(_leftInfo);
                var leftTask = Task.Run(() => _crane.Retrieve(_leftInfo, out bcrCstId_LF));
                var rightTask = Task.Run(() => _crane.Retrieve(_rightInfo, out bcrCstId_RF));

                completeCode_LF = leftTask.Result;
                completeCode_RF = rightTask.Result;

                if (completeCode_LF != CompleteCode.Success_91_Retrieve)
                {
                    _crane.EndCommand(_leftInfo, completeCode_LF);
                }
                if (completeCode_RF != CompleteCode.Success_91_Retrieve)
                {
                    _crane.EndCommand(_rightInfo, completeCode_RF);
                }
                if (completeCode_LF != CompleteCode.Success_91_Retrieve
                    && completeCode_RF != CompleteCode.Success_91_Retrieve)
                {
                    return;
                }

                Task.Delay(100).Wait();

                completeCode = _crane.MoveT3(_leftInfo);
                leftTask = null;
                rightTask = null;
                if (completeCode_LF == CompleteCode.Success_91_Retrieve)
                {
                    leftTask = Task.Run(() => _crane.Deposit(_leftInfo));
                }
                if (completeCode_RF == CompleteCode.Success_91_Retrieve)
                {
                    rightTask = Task.Run(() => _crane.Deposit(_rightInfo));
                }

                if (leftTask != null)
                {
                    completeCode_LF = leftTask.Result;
                    _crane.EndCommand(_leftInfo, completeCode_LF);
                }
                if (rightTask != null)
                {
                    completeCode_RF = rightTask.Result;
                    _crane.EndCommand(_rightInfo, completeCode_RF);
                }
            }
            else if (_leftInfo.FromLocation != 0)
            {
                completeCode = _crane.MoveT1(_leftInfo);
                var leftTask = Task.Run(() => _crane.Retrieve(_leftInfo, out bcrCstId_LF));
                var rightTask = Task.Run(() => _crane.Retrieve(_rightInfo, out bcrCstId_RF));

                completeCode_LF = leftTask.Result;
                completeCode_RF = rightTask.Result;
                _crane.EndCommand(_leftInfo, completeCode_LF);
                _crane.EndCommand(_rightInfo, completeCode_RF);
            }
            else if (_leftInfo.ToLocation != 0)
            {
                completeCode = _crane.MoveT3(_leftInfo);
                var leftTask = Task.Run(() => _crane.Deposit(_leftInfo));
                var rightTask = Task.Run(() => _crane.Deposit(_rightInfo));

                completeCode_LF = leftTask.Result;
                completeCode_RF = rightTask.Result;
                _crane.EndCommand(_leftInfo, completeCode_LF);
                _crane.EndCommand(_rightInfo, completeCode_RF);
            }
            else
            {
                _crane.EndCommand(_leftInfo, CompleteCode.Error_E1_WrongCommand);
                _crane.EndCommand(_rightInfo, CompleteCode.Error_E1_WrongCommand);
            }
        }
    }
}
