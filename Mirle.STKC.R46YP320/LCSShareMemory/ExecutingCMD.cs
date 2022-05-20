using Mirle.Def;
using Mirle.Extensions;
using Mirle.STKC.R46YP320.Model;
using System;

namespace Mirle.STKC.R46YP320.LCSShareMemory
{
    public class ExecutingCMD
    {
        public int CraneNo { get; }
        public int ForkNo { get; }
        private readonly LCSExecutingCMD.CommandSignal _cmd;

        #region Field

        public string CommandID
        {
            get => _cmd.CommandID.GetData().ToASCII();
            set => _cmd.CommandID.SetData(value.ToIntArray(_cmd.CommandID.Length));
        }

        public string TaskNo
        {
            get => _cmd.TaskNo.GetData().ToASCII();
            set => _cmd.TaskNo.SetData(value.ToIntArray(_cmd.TaskNo.Length));
        }

        public string CSTID
        {
            get => _cmd.CSTID.GetData().ToASCII();
            set => _cmd.CSTID.SetData(value.ToIntArray(_cmd.CSTID.Length));
        }

        public clsEnum.TaskState TaskState
        {
            get => (clsEnum.TaskState)_cmd.TaskState.GetValue();
            set => _cmd.TaskState.SetValue((int)value);
        }

        public clsEnum.TaskCmdState CMDState
        {
            get => (clsEnum.TaskCmdState)_cmd.CMDState.GetValue();
            set => _cmd.CMDState.SetValue((int)value);
        }

        public clsEnum.TaskMode TransferMode
        {
            get => (clsEnum.TaskMode)_cmd.TransferMode.GetValue();
            set => _cmd.TransferMode.SetValue((int)value);
        }

        public string Source
        {
            get => _cmd.Source.GetData().ToASCII();
            set => _cmd.Source.SetData(value.ToIntArray(_cmd.Source.Length));
        }

        public int SourceBay
        {
            get => _cmd.SourceBay.GetValue();
            set => _cmd.SourceBay.SetValue(value);
        }

        public string Destination
        {
            get => _cmd.Destination.GetData().ToASCII();
            set => _cmd.Destination.SetData(value.ToIntArray(_cmd.Destination.Length));
        }

        public int TravelAxisSpeed
        {
            get => _cmd.TravelAxisSpeed.GetValue();
            set => _cmd.TravelAxisSpeed.SetValue(value);
        }

        public int LifterAxisSpeed
        {
            get => _cmd.LifterAxisSpeed.GetValue();
            set => _cmd.LifterAxisSpeed.SetValue(value);
        }

        public int RotateAxisSpeed
        {
            get => _cmd.RotateAxisSpeed.GetValue();
            set => _cmd.RotateAxisSpeed.SetValue(value);
        }

        public int ForkAxisSpeed
        {
            get => _cmd.ForkAxisSpeed.GetValue();
            set => _cmd.ForkAxisSpeed.SetValue(value);
        }

        public int DestinationBay
        {
            get => _cmd.DestinationBay.GetValue();
            set => _cmd.DestinationBay.SetValue(value);
        }

        public string UserID
        {
            get => _cmd.UserID.GetData().ToASCII();
            set => _cmd.UserID.SetData(value.ToIntArray(_cmd.UserID.Length));
        }

        public string CSTType
        {
            get => _cmd.CSTType.GetData().ToASCII();
            set => _cmd.CSTType.SetData(value.ToIntArray(_cmd.CSTType.Length));
        }

        public string BCRReadFlag
        {
            get => _cmd.BCRReadFlag.GetData().ToASCII();
            set => _cmd.BCRReadFlag.SetData(value.ToIntArray(_cmd.BCRReadFlag.Length));
        }

        public string BCRReadDT
        {
            get => _cmd.BCRReadDT.GetData().ToASCII();
            set => _cmd.BCRReadDT.SetData(value.ToIntArray(_cmd.BCRReadDT.Length));
        }

        public string BCRReplyCSTID
        {
            get => _cmd.BCRReplyCSTID.GetData().ToASCII();
            set => _cmd.BCRReplyCSTID.SetData(value.ToIntArray(_cmd.BCRReplyCSTID.Length));
        }

        public clsEnum.BCRReadStatus BCRReadStatus
        {
            get => (clsEnum.BCRReadStatus)_cmd.BCRReadStatus.GetValue();
            set => _cmd.BCRReadStatus.SetValue((int)value);
        }

        public int Priority
        {
            get => _cmd.Priority.GetValue();
            set => _cmd.Priority.SetValue(value);
        }

        public string QueueDT
        {
            get => _cmd.QueueDT.GetData().ToASCII();
            set => _cmd.QueueDT.SetData(value.ToIntArray(_cmd.QueueDT.Length));
        }

        public string InitialDT
        {
            get => _cmd.InitialDT.GetData().ToASCII();
            set => _cmd.InitialDT.SetData(value.ToIntArray(_cmd.InitialDT.Length));
        }

        public string ActiveDT
        {
            get => _cmd.ActiveDT.GetData().ToASCII();
            set => _cmd.ActiveDT.SetData(value.ToIntArray(_cmd.ActiveDT.Length));
        }

        public string FinishDT
        {
            get => _cmd.FinishDT.GetData().ToASCII();
            set => _cmd.FinishDT.SetData(value.ToIntArray(_cmd.FinishDT.Length));
        }

        public string Cycle1StartDT
        {
            get => _cmd.Cycle1StartDT.GetData().ToASCII();
            set => _cmd.Cycle1StartDT.SetData(value.ToIntArray(_cmd.Cycle1StartDT.Length));
        }

        public string AtSourceDT
        {
            get => _cmd.AtSourceDT.GetData().ToASCII();
            set => _cmd.AtSourceDT.SetData(value.ToIntArray(_cmd.AtSourceDT.Length));
        }

        public string Fork1StartDT
        {
            get => _cmd.Fork1StartDT.GetData().ToASCII();
            set => _cmd.Fork1StartDT.SetData(value.ToIntArray(_cmd.Fork1StartDT.Length));
        }

        public string CSTOnCraneDT
        {
            get => _cmd.CSTOnCraneDT.GetData().ToASCII();
            set => _cmd.CSTOnCraneDT.SetData(value.ToIntArray(_cmd.CSTOnCraneDT.Length));
        }

        public string Cycle2StartDT
        {
            get => _cmd.Cycle2StartDT.GetData().ToASCII();
            set => _cmd.Cycle2StartDT.SetData(value.ToIntArray(_cmd.Cycle2StartDT.Length));
        }

        public string AtDestinationDT
        {
            get => _cmd.AtDestinationDT.GetData().ToASCII();
            set => _cmd.AtDestinationDT.SetData(value.ToIntArray(_cmd.AtDestinationDT.Length));
        }

        public string Fork2StartDT
        {
            get => _cmd.Fork2StartDT.GetData().ToASCII();
            set => _cmd.Fork2StartDT.SetData(value.ToIntArray(_cmd.Fork2StartDT.Length));
        }

        public string CSTTackOffCraneDT
        {
            get => _cmd.CSTTackOffCraneDT.GetData().ToASCII();
            set => _cmd.CSTTackOffCraneDT.SetData(value.ToIntArray(_cmd.CSTTackOffCraneDT.Length));
        }

        public string FinishLocation
        {
            get => _cmd.FinishLocation.GetData().ToASCII();
            set => _cmd.FinishLocation.SetData(value.ToIntArray(_cmd.FinishLocation.Length));
        }

        public string EmptyCST
        {
            get => _cmd.EmptyCST.GetData().ToASCII();
            set => _cmd.EmptyCST.SetData(value.ToIntArray(_cmd.EmptyCST.Length));
        }

        #endregion Field

        internal ExecutingCMD(int craneNo, int forkNo, LCSExecutingCMD.CommandSignal cmd)
        {
            _cmd = cmd;
            CraneNo = craneNo;
            ForkNo = forkNo;
        }

        public void Clear()
        {
            _cmd.AllBlock.Clear();
        }

        public void OnActive(clsEnum.TaskState taskState, string activeDt)
        {
            TaskState = taskState;
            ActiveDT = activeDt ?? string.Empty;
        }

        public void OnAtSource(clsEnum.TaskCmdState cmdState, string atSourceDt)
        {
            CMDState = cmdState;
            AtSourceDT = atSourceDt ?? string.Empty;
        }

        public void OnAtDestinationDT(clsEnum.TaskCmdState cmdState, string atDestinationDt)
        {
            CMDState = cmdState;
            AtDestinationDT = atDestinationDt ?? string.Empty;
        }

        public void OnBCRRead(string bcrReadDt, string bcrReplyCstid, clsEnum.BCRReadStatus bcrReadStatus)
        {
            BCRReadDT = bcrReadDt ?? string.Empty;
            BCRReplyCSTID = bcrReplyCstid ?? string.Empty;
            BCRReadStatus = bcrReadStatus;
        }

        public void OnForkTransferRequestWrong(clsEnum.TaskCmdState cmdState, string finishDt, string finishLocation)
        {
            CMDState = cmdState;
            FinishDT = finishDt ?? string.Empty;
            FinishLocation = finishLocation ?? String.Empty;
        }

        public void OnForkCycle1(clsEnum.TaskCmdState cmdState, string cycle1StartDt)
        {
            CMDState = cmdState;
            Cycle1StartDT = cycle1StartDt ?? string.Empty;
        }

        public void OnForkCycle2(clsEnum.TaskCmdState cmdState, string cycle2StartDt)
        {
            CMDState = cmdState;
            Cycle2StartDT = cycle2StartDt ?? string.Empty;
        }

        public void OnForkForking1(clsEnum.TaskCmdState cmdState, string fork1StartDt)
        {
            CMDState = cmdState;
            Fork1StartDT = fork1StartDt ?? string.Empty;
        }

        public void OnForkForking2(clsEnum.TaskCmdState cmdState, string fork2StartDt)
        {
            CMDState = cmdState;
            Fork2StartDT = fork2StartDt ?? string.Empty;
        }

        public void OnCSTOnCrane(clsEnum.TaskCmdState cmdState, string cstOnCraneDt)
        {
            CMDState = cmdState;
            CSTOnCraneDT = cstOnCraneDt ?? string.Empty;
        }

        public void OnCSTOffCrane(clsEnum.TaskCmdState cmdState, string cstTackOffCraneDt)
        {
            CMDState = cmdState;
            CSTTackOffCraneDT = cstTackOffCraneDt ?? string.Empty;
        }

        public void OnForkCurrentCommandChanged(clsEnum.TaskState taskState, string activeDt)
        {
            TaskState = taskState;
            ActiveDT = activeDt ?? string.Empty;
        }

        public void OnCommandFinished(clsEnum.TaskCmdState cmdState, string finishDt, string finishLocation)
        {
            CMDState = cmdState;
            FinishDT = finishDt ?? string.Empty;
            FinishLocation = finishLocation ?? String.Empty;
        }
    }
}