using System;
using System.Windows.Forms;
using Mirle.Stocker.R46YP320;
using Mirle.STKC.R46YP320.Manager;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneTaskCmdView : Form
    {
        private readonly int _craneId;
        private readonly STKCHost _stkcHost;
        private readonly STKCManager _stkcManager;
        private readonly CurrentExeCmdManager _currentExeCmdManager;

        public CraneTaskCmdView(Crane crane, STKCHost stkcHost)
        {
            _craneId = crane.Id;
            _stkcHost = stkcHost;
            _currentExeCmdManager = stkcHost.GetSTKCManager().GetTaskCmdManager();
            InitializeComponent();
        }

        private void CraneTaskCmdView_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            int intForkNumber = 0;

            if (rdbRM1_LeftFork.Checked)
            {
                intForkNumber = 1;
            }
            else if (rdbRM1_RightFork.Checked)
            {
                intForkNumber = 2;
            }

            ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByFork(_craneId, intForkNumber);
            if (exeCmd != null)
            {
                lblRM1SM_CommandID.Text = exeCmd.CommandID;
                lblRM1SM_CSTID.Text = exeCmd.CSTID;
                lblRM1SM_TaskNo.Text = exeCmd.TaskNo;
                lblRM1SM_TransferState.Text = exeCmd.TaskState.ToString();
                lblRM1SM_CMDState.Text = exeCmd.CMDState.ToString();
                lblRM1SM_UserID.Text = exeCmd.UserID;
                lblRM1SM_BCRReadFlag.Text = exeCmd.BCRReadFlag;
                lblRM1SM_BCRReadDT.Text = exeCmd.BCRReadDT;
                lblRM1SM_BCRReadStatus.Text = exeCmd.BCRReadStatus.ToString();
                lblRM1SM_BCRReplyCSTID.Text = exeCmd.BCRReplyCSTID;
                lblRM1SM_QueueDT.Text = exeCmd.QueueDT;
                lblRM1SM_InitialDT.Text = exeCmd.InitialDT;
                lblRM1SM_ActiveDT.Text = exeCmd.ActiveDT;
                lblRM1SM_CSTOnCraneDT.Text = exeCmd.CSTOnDT;
                lblRM1SM_CSTTakeOffCraneDT.Text = exeCmd.CSTTakeOffDT;
                lblRM1SM_Speed.Text =
                    $"{exeCmd.TravelAxisSpeed}-{exeCmd.LifterAxisSpeed}-{exeCmd.RotateAxisSpeed}-{exeCmd.ForkAxisSpeed}";
                lblRM1SM_TransferMode.Text = exeCmd.TransferMode.ToString();
                lblRM1SM_Priority.Text = exeCmd.Priority.ToString();
                lblRM1SM_Source.Text = exeCmd.Source;
                lblRM1SM_SourceBay.Text = exeCmd.SourceBay.ToString();
                lblRM1SM_Destination.Text = exeCmd.Destination;
                lblRM1SM_DestinationBay.Text = exeCmd.DestinationBay.ToString();
                lblRM1SM_CSTType.Text = exeCmd.CSTType;
                lblRM1SM_C1StartDT.Text = exeCmd.C1StartDT;
                lblRM1SM_AtSourceDT.Text = exeCmd.AtSourceDT;
                lblRM1SM_Fork1StartDT.Text = exeCmd.F1StartDT;
                lblRM1SM_C2StartDT.Text = exeCmd.C2StartDT;
                lblRM1SM_AtDestinationDT.Text = exeCmd.AtDestinationDT;
                lblRM1SM_Fork2StartDT.Text = exeCmd.F2StartDT;
                //lblRM1SM_DuplicateShelfID.Text = tmpCmd.DuplicateShelfID;
                //lblRM1_IdleTime.Text = _lcsConfig.gdblCmdBufTOStart_ms[_craneId].ToString();

                //V1.13.1510.15-3
                lblRM1SM_EmptyCST.Text = exeCmd.EmptyCST;
            }
            else
            {
                lblRM1SM_CommandID.Text = string.Empty;
                lblRM1SM_CSTID.Text = string.Empty;
                lblRM1SM_TaskNo.Text = string.Empty;
                lblRM1SM_TransferState.Text = string.Empty;
                lblRM1SM_CMDState.Text = string.Empty;
                lblRM1SM_UserID.Text = string.Empty;
                lblRM1SM_BCRReadFlag.Text = string.Empty;
                lblRM1SM_BCRReadDT.Text = string.Empty;
                lblRM1SM_BCRReadStatus.Text = string.Empty;
                lblRM1SM_BCRReplyCSTID.Text = string.Empty;
                lblRM1SM_QueueDT.Text = string.Empty;
                lblRM1SM_InitialDT.Text = string.Empty;
                lblRM1SM_ActiveDT.Text = string.Empty;
                lblRM1SM_CSTOnCraneDT.Text = string.Empty;
                lblRM1SM_CSTTakeOffCraneDT.Text = string.Empty;
                lblRM1SM_Speed.Text = string.Empty;
                lblRM1SM_TransferMode.Text = string.Empty;
                lblRM1SM_Priority.Text = string.Empty;
                lblRM1SM_Source.Text = string.Empty;
                lblRM1SM_SourceBay.Text = string.Empty;
                lblRM1SM_Destination.Text = string.Empty;
                lblRM1SM_DestinationBay.Text = string.Empty;
                //lblRM1SM_CSTAttribute.Text = string.Empty;
                lblRM1SM_CSTType.Text = string.Empty;
                lblRM1SM_C1StartDT.Text = string.Empty;
                lblRM1SM_AtSourceDT.Text = string.Empty;
                lblRM1SM_Fork1StartDT.Text = string.Empty;
                lblRM1SM_C2StartDT.Text = string.Empty;
                lblRM1SM_AtDestinationDT.Text = string.Empty;
                lblRM1SM_Fork2StartDT.Text = string.Empty;
                //lblRM1SM_DuplicateShelfID.Text = string.Empty;
                //lblRM1_IdleTime.Text = _lcsConfig.gdblCmdBufTOStart_ms[_craneId].ToString();

                //V1.13.1510.15-3
                lblRM1SM_EmptyCST.Text = string.Empty;
            }
        }

        private void buttonRM1_AbortCmd_Click(object sender, EventArgs e)
        {
            int intForkNumber = 0;

            if (rdbRM1_LeftFork.Checked) { intForkNumber = 1; }
            else if (rdbRM1_RightFork.Checked) { intForkNumber = 2; }

            ExeCmd exeCmd = _currentExeCmdManager.GetCurrentCommandByFork(_craneId, intForkNumber);
            if (exeCmd != null)
            {
                exeCmd.ForceAbortCommand();
            }
        }
    }
}