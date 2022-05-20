using Mirle.Def;
using Mirle.STKC.R46YP320.Model;

namespace Mirle.STKC.R46YP320.Manager
{
    public class ExeCmdFinishState
    {
        public clsEnum.TaskMode CommandType { get; set; }

        public string CompleteCode { get; set; }
        public bool CstOnFork { get; set; }
        public string BcrResult { get; set; }
        public bool IsInCycle2 { get; set; }
    }

    public class ExeCmdResult
    {
        public string CompleteCode { get; set; }
        public clsEnum.TaskCmdState CmdState { get; set; }
        public FinishLocationState FinishLocation { get; set; }
    }

    public static class ExeCmdFinishAnalyzer
    {
        public static ExeCmdResult GetResult(this ExeCmdFinishState status)
        {
            var result = new ExeCmdResult()
            {
                CompleteCode = status.CompleteCode,
                CmdState = clsEnum.TaskCmdState.Initialize,
                FinishLocation = FinishLocationState.OnCrane,
            };

            switch (status.CommandType)
            {
                case clsEnum.TaskMode.Move:
                    result.CmdState = status.CompleteCode == "92" ? clsEnum.TaskCmdState.Finish : clsEnum.TaskCmdState.AbnormalFinish;
                    result.FinishLocation = FinishLocationState.OnDestination;
                    break;

                case clsEnum.TaskMode.Pickup:
                    if (status.CompleteCode == "91")
                    {
                        result.CmdState = clsEnum.TaskCmdState.Finish;
                        result.FinishLocation = FinishLocationState.OnCrane;
                    }
                    else
                    {
                        result.CmdState = clsEnum.TaskCmdState.AbnormalFinish;
                        result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnSource;
                    }
                    break;

                case clsEnum.TaskMode.Deposit:
                    if (status.CompleteCode == "92")
                    {
                        result.CmdState = clsEnum.TaskCmdState.Finish;
                        result.FinishLocation = FinishLocationState.OnDestination;
                    }
                    else
                    {
                        if (status.CompleteCode == "EC")
                        {
                            result.FinishLocation = FinishLocationState.OnCrane;
                        }
                        else
                        {
                            result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnDestination;
                        }
                        result.CmdState = clsEnum.TaskCmdState.AbnormalFinish;
                    }
                    break;

                case clsEnum.TaskMode.Scan:
                    if (status.CompleteCode == "97")
                    {
                        result.CmdState = clsEnum.TaskCmdState.Finish;
                        result.FinishLocation = FinishLocationState.OnSource;
                    }
                    else
                    {
                        result.CmdState = clsEnum.TaskCmdState.AbnormalFinish;
                        result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnSource;
                    }
                    break;

                case clsEnum.TaskMode.Transfer:
                    if (status.CompleteCode == "92")
                    {
                        result.CmdState = clsEnum.TaskCmdState.Finish;
                        result.FinishLocation = FinishLocationState.OnDestination;
                    }
                    else
                    {
                        switch (status.CompleteCode)
                        {
                            case "F1":
                                result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnSource;
                                break;

                            case "F2":
                                result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnDestination;
                                break;

                            case "E2"://EmptyRetrieval
                                result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane : FinishLocationState.OnSource;
                                break;

                            case "EC"://DoubleStorage
                                result.FinishLocation = FinishLocationState.OnCrane;
                                break;

                            case "E0"://Interlock
                            case "ED"://Interlock
                            case "EE"://Interlock
                                if (status.CstOnFork)
                                { result.FinishLocation = FinishLocationState.OnCrane; }
                                else
                                {
                                    result.FinishLocation = status.IsInCycle2 ? FinishLocationState.OnDestination : FinishLocationState.OnSource;
                                }
                                break;

                            case "E1":
                                result.FinishLocation = FinishLocationState.OnSource;
                                break;

                            default:
                                result.FinishLocation = status.CstOnFork ? FinishLocationState.OnCrane
                                    : status.IsInCycle2 ? FinishLocationState.OnDestination : FinishLocationState.OnSource;
                                break;
                        }
                        result.CmdState = clsEnum.TaskCmdState.AbnormalFinish;
                    }
                    break;
            }

            return result;
        }
    }
}