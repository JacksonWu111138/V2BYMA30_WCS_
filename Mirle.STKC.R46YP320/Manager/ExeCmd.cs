using Mirle.LCS.Models;
using Mirle.STKC.R46YP320.Model;
using Mirle.Def;
using System;

namespace Mirle.STKC.R46YP320.Manager
{
    public class ExeCmd
    {
        private readonly Structure.TaskDTO _dto;
        private readonly object _finishLock = new object();

        public ExeCmd(Structure.TaskDTO dto)
        {
            _dto = dto;

            this.CreateTime = DateTime.Now;
            //this.ExpectedFinishTime = DateTime.Now.AddSeconds(120);
            this.ExpectedFinishTime = DateTime.Now.AddMinutes(5);
            this.OverDueTime = DateTime.MinValue;
        }

        public DateTime CreateTime { get; private set; }
        public DateTime ExpectedFinishTime { get; private set; }
        public DateTime OverDueTime { get; private set; }

        //public bool IsActive { get; private set; }
        public bool IsCstOnTheCrane_IntoCycle2 { get; private set; }

        #region TaskDTO Data

        //public string StockerID { get; set; }
        public string CommandID => _dto.CommandID;

        public string TaskNo => _dto.TaskNo;
        public int CraneNo => _dto.CraneNo;
        public int ForkNumber => _dto.ForkNo;
        public string CSTID => _dto.CSTID;
        public clsEnum.TaskState TaskState => _dto.TaskState;
        public string CompleteCode => _dto.CompleteCode;
        public clsEnum.TaskCmdState CMDState => _dto.CMDState;
        public clsEnum.TaskMode TransferMode => _dto.TransferMode;

        //public string TransferModeType { get; set; }
        public string Source => _dto.Source;

        public int SourceBay => _dto.SourceBay;
        public string Destination => _dto.Destination;
        public int DestinationBay => _dto.DestinationBay;
        public string AtSourceDT => _dto.AtSourceDT;
        public string AtDestinationDT => _dto.AtDestinationDT;
        public int TravelAxisSpeed => _dto.TravelAxisSpeed;
        public int LifterAxisSpeed => _dto.LifterAxisSpeed;
        public int RotateAxisSpeed => _dto.RotateAxisSpeed;
        public int ForkAxisSpeed => _dto.ForkAxisSpeed;
        public string CMDInfo => _dto.CMDInfo;

        //public string LotID { get; set; }
        public string UserID => _dto.UserID;

        public string EmptyCST => _dto.EmptyCST;
        public string CSTType => _dto.CSTType;
        public string BCRReadFlag => _dto.BCRReadFlag;
        public string BCRReadDT => _dto.BCRReadDT;
        public string BCRReplyCSTID => _dto.BCRReplyCSTID;
        public clsEnum.BCRReadStatus BCRReadStatus => _dto.BCRReadStatus;
        public int Priority => _dto.Priority;
        public string QueueDT => _dto.QueueDT;
        public string InitialDT => _dto.InitialDT;

        //public string WaitingDT { get; set; }
        public string ActiveDT => _dto.ActiveDT;

        public string FinishDT => _dto.FinishDT;
        public string FinishLocation => _dto.FinishLocation;
        public string C1StartDT => _dto.C1StartDT;
        public string CSTOnDT => _dto.CSTOnDT;
        public string CSTTakeOffDT => _dto.CSTTakeOffDT;
        public string C2StartDT { get; set; }

        //public int T1 { get; set; }
        //public int T2 { get; set; }
        //public int T3 { get; set; }
        //public int T4 { get; set; }
        public string F1StartDT => _dto.F1StartDT;

        public string F2StartDT => _dto.F2StartDT;
        //public string RenewFlag { get; set; }
        //public string AccStep { get; set; }
        //public string UpdateDT { get; set; }
        //public int TravelDistance { get; set; }
        //public string ReplyCstId { get; set; }

        #endregion TaskDTO Data

        public void ForceAbortCommand()
        {
            lock (_finishLock)
            {
                if (string.IsNullOrEmpty(CompleteCode))
                {
                    //強制結束命令
                    _dto.CMDState = clsEnum.TaskCmdState.AbnormalFinish;
                    _dto.TaskState = clsEnum.TaskState.Complete;
                    _dto.CompleteCode = Model.Define.CompleteCode.CannotExcuteFromSTKC; // "PD";
                    _dto.FinishDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
            }
        }

        public void ForceCommandFinishWithRetry(string craneShelfId, bool isCstOnFork)
        {
            lock (_finishLock)
            {
                var CmdStatus = new ExeCmdFinishState()
                {
                    CommandType = TransferMode,
                    CompleteCode = Model.Define.CompleteCode.CommandTimeoutFromSTKC,
                    CstOnFork = isCstOnFork,
                    BcrResult = "",
                    IsInCycle2 = IsCstOnTheCrane_IntoCycle2,
                };

                var cmdResult = CmdStatus.GetResult();
                string strFinishLocation = string.Empty;
                switch (cmdResult.FinishLocation)
                {
                    case FinishLocationState.OnSource:
                        strFinishLocation = Source;
                        break;

                    case FinishLocationState.OnDestination:
                        strFinishLocation = Destination;
                        break;

                    case FinishLocationState.OnCrane:
                        strFinishLocation = craneShelfId;
                        break;

                    default:
                        strFinishLocation = string.Empty;
                        break;
                }

                if (string.IsNullOrEmpty(CompleteCode))
                {
                    //強制結束命令
                    _dto.CMDState = clsEnum.TaskCmdState.AbnormalFinish;
                    _dto.TaskState = clsEnum.TaskState.Complete;
                    _dto.CompleteCode = Model.Define.CompleteCode.CommandTimeoutFromSTKC; // "PE";//需要Task重下命令
                    _dto.FinishDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.FinishLocation = strFinishLocation;
                }
            }
        }

        public Structure.TaskDTO GetTaskDTO()
        {
            return _dto;
        }

        public bool IsCommandFinish()
        {
            lock (_finishLock)
            {
                return string.IsNullOrWhiteSpace(CompleteCode) == false;
            }
        }

        public void ResetFinishTime()
        {
            ExpectedFinishTime = DateTime.Now.AddSeconds(15);
        }

        public void SetActive()
        {
            lock (_finishLock)
            {
                if (string.IsNullOrEmpty(ActiveDT))
                {
                    _dto.ActiveDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }

                if (_dto.TaskState == clsEnum.TaskState.Initialize)
                {
                    _dto.TaskState = clsEnum.TaskState.Transferring;
                }
            }
        }

        public void SetAtDestination()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.AtDestination > (int)_dto.CMDState)
                {
                    _dto.AtDestinationDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.AtDestination;
                }
            }
        }

        public void SetAtSource()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.AtSource > (int)_dto.CMDState)
                {
                    _dto.AtSourceDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.AtSource;
                }
            }
        }

        public void SetBCRRead(string bcrCstid)
        {
            // 定義MPLC當RBCR發生異常時，所傳回的CSTID 用以判別FBCR狀態
            const string NOCST1 = "NOCST1";
            const string NORD01 = "NORD01";
            const string ERROR1 = "ERROR1";

            lock (_finishLock)
            {
                if (string.IsNullOrWhiteSpace(_dto.BCRReadDT) || string.IsNullOrWhiteSpace(_dto.BCRReplyCSTID))
                {
                    _dto.BCRReadDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.BCRReplyCSTID = bcrCstid;

                    if (_dto.CSTID == bcrCstid)
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.Success; }
                    else if (bcrCstid == NOCST1)
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.NoCST; }
                    else if (bcrCstid == NORD01)
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.Failure; }
                    else if (bcrCstid == ERROR1)
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.Failure; }
                    else if (_dto.CSTID != bcrCstid)
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.Mismatch; }
                    else
                    { _dto.BCRReadStatus = clsEnum.BCRReadStatus.Failure; }
                }
            }
        }

        public void SetCommandFinish(clsEnum.TaskCmdState cmdState, string completedCode, string finishLocation,
            int t1, int t2, int t3, int t4)
        {
            lock (_finishLock)
            {
                _dto.CMDState = cmdState;
                _dto.CompleteCode = completedCode;
                _dto.FinishDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                _dto.FinishLocation = finishLocation;

                _dto.TaskState = clsEnum.TaskState.Complete;
                _dto.T1 = t1;
                _dto.T2 = t2;
                _dto.T3 = t3;
                _dto.T4 = t4;
            }
        }

        public void SetCstOffCrane()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.CSTPresentOffCrane > (int)_dto.CMDState)
                {
                    _dto.CSTTakeOffDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.CSTPresentOffCrane;
                }
            }
        }

        public void SetCstOnCrane()
        {
            lock (_finishLock)
            {
                IsCstOnTheCrane_IntoCycle2 = true;
                if ((int)clsEnum.TaskCmdState.CSTPresentOnCrane > (int)_dto.CMDState)
                {
                    _dto.CSTOnDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.CSTPresentOnCrane;
                }
            }
        }

        public void SetCycle1()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.Cycle1Start > (int)_dto.CMDState)
                {
                    _dto.C1StartDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.Cycle1Start;
                }
            }
        }

        public void SetCycle2()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.Cycle2Start > (int)_dto.CMDState)
                {
                    _dto.C2StartDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.Cycle2Start;
                }
            }
        }

        public void SetForking1()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.Fork1Start > (int)_dto.CMDState)
                {
                    _dto.F1StartDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.Fork1Start;
                }
            }
        }

        public void SetForking2()
        {
            lock (_finishLock)
            {
                if ((int)clsEnum.TaskCmdState.Fork2Start > (int)_dto.CMDState)
                {
                    _dto.F2StartDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    _dto.CMDState = clsEnum.TaskCmdState.Fork2Start;
                }
            }
        }

        public void SetInitial()
        {
            _dto.InitialDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (_dto.TaskState < clsEnum.TaskState.Transferring)
            {
                _dto.TaskState = clsEnum.TaskState.Initialize;
                _dto.CMDState = clsEnum.TaskCmdState.WriteCMD2PLC;
            }
        }

        public void SetOverTime()
        {
            OverDueTime = DateTime.Now.AddSeconds(5);
        }

        public void SetWaiting()
        {
            lock (_finishLock)
            {
                _dto.WaitingDT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
        }
    }
}
