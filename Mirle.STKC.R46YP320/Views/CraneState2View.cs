using Mirle.STKC.R46YP320.Extensions;
using Mirle.Stocker.R46YP320;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneState2View : Form
    {
        private readonly Crane _crane;

        public CraneState2View(Crane _crane)
        {
            this._crane = _crane;
            InitializeComponent();
        }

        private void CraneState2View_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            var crane = _crane.Signal;
            var ctrl = _crane.Signal.Controller;
            var leftFork = _crane.Signal.LeftFork;
            var rightFork = _crane.Signal.RightFork;
            var sri = _crane.Signal.SRI;

            lblRM1_ErrorIndex1_PC.Text = ctrl.PcErrorIndex.GetValue().ToString();
            lblRM1_ErrorIndex1_PLC.Text = crane.ErrorIndex.GetValue().ToString();

            lblRM1_ErrorCode.Text = crane.ErrorCode.GetValue().ToString("X4");
            lblRM1_RotatingCounter.Text = crane.RotatingCounter.GetValue().ToString();
            lblRM1_ForkCounter_LF.Text = leftFork.ForkCounter.GetValue().ToString();
            lblRM1_ForkCounter_RF.Text = rightFork.ForkCounter.GetValue().ToString();
            lblRM1_MileageOfTravel.Text = crane.MileageOfTravel.GetValue().ToString();
            lblRM1_MileageOfLifter.Text = crane.MiileageOfLifter.GetValue().ToString();

            lblRM1_PLCCPUBatteryLow.BackColor = crane.PLCBatteryLow_CPU.ToColor();
            lblRM1_DriverBatteryLow.BackColor = crane.DriverBatteryLow.ToColor();
            lblRM1_AnyOneFFUisErr.BackColor = crane.AnyFFUofCraneIsError.ToColor();
            lblRM1_SRI_AMSwitchofRMPLCisAutoHP.BackColor = sri.TheAMSwitchIsAuto_RM.ToColor();
            lblRM1_SRI_MainCircuitOnEnable.BackColor = sri.MainCircuitOnEnable.ToColor();
            lblRM1_SRI_EMO.BackColor = sri.EMO.ToColor();
            lblRM1_SRI_RM1HIDPowerOn.BackColor = sri.HIDPowerOn.ToColor();
            lblRM1_SRI_NoError.BackColor = sri.NoError.ToColor();

            lblRM1_WrongCommandReasonCode.Text = crane.WrongCommandReasonCode.GetValue().ToString("X4");
            lblRM1_TravelAxisSpeed.Text = crane.TravelAxisSpeed.GetValue().ToString();
            lblRM1_LifterAxisSpeed.Text = crane.LifterAxisSpeed.GetValue().ToString();
            lblRM1_RotateAxisSpeed.Text = crane.RotateAxisSpeed.GetValue().ToString();
            lblRM1_ForkAxisSpeed.Text = crane.ForkAxisSpeed.GetValue().ToString();
        }
    }
}