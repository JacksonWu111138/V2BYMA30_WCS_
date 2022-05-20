using Mirle.Extensions;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneState1View : Form
    {
        private readonly Crane _crane;

        public CraneState1View(Crane crane)
        {
            _crane = crane;
            InitializeComponent();
        }

        private void CraneState1View_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            _crane.CanAutoSetRun = chkAutoSetRun.Checked;

            var crane = _crane.Signal;

            lblRM1_Escape.BackColor = crane.Escape.ToColor();
            lblRM1_Active.BackColor = crane.Active.ToColor();
            lblRM1_InService.BackColor = crane.InService.ToColor();
            lblRM1_Run.BackColor = crane.Run.ToColor();
            lblRM1_Error.BackColor = crane.Error.ToColor();
            lblRM1_Idle.BackColor = crane.Idle.ToColor();
            lblRM1_TracelHP.BackColor = crane.TravelHomePosition.ToColor();
            lblRM1_Traceling.BackColor = crane.TravelMoving.ToColor();
            lblRM1_LifterHP.BackColor = crane.LifterHomePosition.ToColor();
            lblRM1_Liftering.BackColor = crane.LifterActing.ToColor();
            lblRM1_RotateHP.BackColor = crane.RotateHomePosition.ToColor();
            lblRM1_Rotating.BackColor = crane.Rotating.ToColor();
            lblRM1_HPReturn.BackColor = crane.HPReturn.ToColor();
            lblRM1_RunEnable.BackColor = crane.RunEnable.ToColor();
            lblRM1_CraneLocationUpdated.BackColor = crane.LocationUpdated.ToColor();
            lblRM1_SingleCraneMode.BackColor = crane.SingleCraneMode.ToColor();
            lblRM1_DualCraneComuErr.BackColor = crane.Dual_DualCraneCommunicationErr.ToColor();
            lblRM1_InterferenceWaiting.BackColor = crane.Dual_InterferenceWaiting.ToColor();
            lblRM1_ForkatBank1.BackColor = crane.ForkAtBank1.ToColor();
            lblRM1_ForkatBank2.BackColor = crane.ForkAtBank2.ToColor();
            lblRM1_CurrentBayLevel.Text = crane.CurrentBay.GetValue() + "-" + crane.CurrentLevel.GetValue();
            lblRM1_ReadyReceiveNewCommand.BackColor = crane.ReadyToReceiveNewCommand.ToColor();
            lblRM1_HandoffReserved.BackColor = crane.Dual_HandOffReserved.ToColor();
            lblRM1_TransferCommandReceived.BackColor = crane.TransferCommandReceived.ToColor();
            lblRM1_InterventionEntry.BackColor = crane.Dual_InterventionEntry.ToColor();

            lblRM1_T1.Text = (crane.T1.GetValue() / 10.0).ToString("F1");
            lblRM1_T2.Text = (crane.T2.GetValue() / 10.0).ToString("F1");
            lblRM1_T3.Text = (crane.T3.GetValue() / 10.0).ToString("F1");
            lblRM1_T4.Text = (crane.T4.GetValue() / 10.0).ToString("F1");

            lblRM1FBCRReadReqOn.BackColor = crane.RequestSignal.ScanCompleteReq.ToColor();
            lblRM1_RMLocation.Text = crane.Location.GetValue().ToString("D5");
            lblRM1_CurrentPosition.Text = crane.CurrentPosition.GetValue().ToString();
            lblRM1_EvacuationPosition.Text = crane.EvacuationPositon.GetValue().ToString();
            lblRM1_InterferenceRange.Text = crane.InterferenceRange.GetValue().ToString();

            lblRM1_CommandBuffer1.Text = crane.CommandBuffer1.GetValue().ToString("D5");
            lblRM1_CommandBuffer2.Text = crane.CommandBuffer2.GetValue().ToString("D5");
            lblRM1_CommandBuffer3.Text = crane.CommandBuffer3.GetValue().ToString("D5");

            var leftFork = _crane.Signal.LeftFork;
            lblRM1_ForkIdle_LF.BackColor = leftFork.Idle.ToColor();
            lblRM1_C1_LF.BackColor = leftFork.Cycle1.ToColor();
            lblRM1_C2_LF.BackColor = leftFork.Cycle2.ToColor();
            lblRM1_F1_LF.BackColor = leftFork.Forking1.ToColor();
            lblRM1_ForkRaised_LF.BackColor = leftFork.Rised.ToColor();
            lblRM1_F2_LF.BackColor = leftFork.Forking2.ToColor();
            lblRM1_ForkDowned_LF.BackColor = leftFork.Downed.ToColor();
            lblRM1_ForkHP_LF.BackColor = leftFork.ForkHomePosition.ToColor();
            lblRM1_Forking_LF.BackColor = leftFork.Forking.ToColor();
            lblRM1_LoadSensorOn_LF.BackColor = leftFork.LoadPresenceSensor.ToColor();
            lblRM1_LoadPresenceOn_LF.BackColor = leftFork.CSTPresence.IsOn() ? Color.YellowGreen : Color.White;
            lblRM1_CSTIDTracking_LF.Text = leftFork.TrackingCstId.GetData().ToASCII();
            lblRM1_CSTIDBCRResult_LF.Text = leftFork.BCRResultCstId.GetData().ToASCII();
            lblRM1_CurrentTransferNo_LF.Text = leftFork.CurrentCommand.GetValue().ToString("D5");
            lblRM1_CompletedTransferNo_LF.Text = leftFork.CompletedCommand.GetValue().ToString("D5");
            lblRM1_CompletedCode_LF.Text = leftFork.CompletedCode.GetValue().ToString("X2");

            var rightFork = _crane.Signal.RightFork;
            lblRM1_ForkIdle_RF.BackColor = rightFork.Idle.ToColor();
            lblRM1_C1_RF.BackColor = rightFork.Cycle1.ToColor();
            lblRM1_C2_RF.BackColor = rightFork.Cycle2.ToColor();
            lblRM1_F1_RF.BackColor = rightFork.Forking1.ToColor();
            lblRM1_ForkRaised_RF.BackColor = rightFork.Rised.ToColor();
            lblRM1_F2_RF.BackColor = rightFork.Forking2.ToColor();
            lblRM1_ForkDowned_RF.BackColor = rightFork.Downed.ToColor();
            lblRM1_ForkHP_RF.BackColor = rightFork.ForkHomePosition.ToColor();
            lblRM1_Forking_RF.BackColor = rightFork.Forking.ToColor();
            lblRM1_LoadSensorOn_RF.BackColor = rightFork.LoadPresenceSensor.ToColor();
            lblRM1_LoadPresenceOn_RF.BackColor = rightFork.CSTPresence.IsOn() ? Color.YellowGreen : Color.White;
            lblRM1_CSTIDTracking_RF.Text = rightFork.TrackingCstId.GetData().ToASCII();
            lblRM1_CSTIDBCRResult_RF.Text = rightFork.BCRResultCstId.GetData().ToASCII();
            lblRM1_CurrentTransferNo_RF.Text = rightFork.CurrentCommand.GetValue().ToString("D5");
            lblRM1_CompletedTransferNo_RF.Text = rightFork.CompletedCommand.GetValue().ToString("D5");
            lblRM1_CompletedCode_RF.Text = rightFork.CompletedCode.GetValue().ToString("X2");
        }
    }
}