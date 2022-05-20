using Mirle.LCS;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Windows.Forms;
using Mirle.STKC.R46YP320.LCSShareMemory;
using Mirle.Extensions;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class ModeChangeView : Form
    {
        private readonly LCSInfo _lcsInfo;
        private readonly StockerController _stockerController;
        private readonly CSOTStocker _stocker;
        private readonly LCSParameter _lcsParameter;
        private readonly LoggerService _loggerService;

        public LCSParameter.SCState ScState => _lcsParameter.SCState_Cur;

        private bool disableEvent = true;

        public ModeChangeView(LCSInfo lcsInfo, StockerController stockerController, LCSParameter lcsParameter, LoggerService loggerService)
        {
            _stockerController = stockerController;
            _stocker = stockerController.GetStocker();
            _lcsParameter = lcsParameter;
            _loggerService = loggerService;
            _lcsInfo = lcsInfo;
            InitializeComponent();
        }

        private void ModeChangeView_Load(object sender, EventArgs e)
        {
            disableEvent = false;
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            lblMainTime.Text = _stockerController.MainProcessTime.ToString("F1");
            lblCrane1Time.Text = _stockerController.Crane1ProcessTime.ToString("F1");
            lblCrane2Time.Text = _stockerController.Crane2ProcessTime.ToString("F1");
            lbl_HPKeyIsAuto.BackColor = _stocker.Signal.KeySwitch_HP.ToColor();
            lbl_OPKeyIsAuto.BackColor = _stocker.Signal.KeySwitch_OP.ToColor();
            lbl_HPDoorIsClosed.BackColor = _stocker.Signal.SafetyDoorClosed_HP.ToColor();
            lbl_OPDoorIsClosed.BackColor = _stocker.Signal.SafetyDoorClosed_OP.ToColor();

            if (ScState == LCSParameter.SCState.Auto)
            {
                lblAutoPauseSts.Text = "Auto";
                lblAutoPauseSts.BackColor = Color.Lime;
                butAutoPause.Text = "Pause";
            }
            else if (ScState == LCSParameter.SCState.Pausing)
            {
                lblAutoPauseSts.Text = "Pausing";
                lblAutoPauseSts.BackColor = Color.Yellow;
                butAutoPause.Text = "Auto";
            }
            else if (ScState == LCSParameter.SCState.Paused)
            {
                lblAutoPauseSts.Text = "Pause";
                lblAutoPauseSts.BackColor = Color.Yellow;
                butAutoPause.Text = "Auto";
            }
            else
            {
                lblAutoPauseSts.Text = "None";
                lblAutoPauseSts.BackColor = Color.Red;
                butAutoPause.Text = "Auto";
            }

            var data = _stocker.Signal.Controller.SystemTimeCalibration.GetData();
            lblMPLCDT.Text = data[0].BCDToInt().ToString("D4") + data[1].BCDToInt().ToString("D4") + data[2].BCDToInt().ToString("D4");
        }

        private void butAutoPause_Click(object sender, EventArgs e)
        {
            TraceLogFormat objLog = new TraceLogFormat();

            objLog.Message = "butAutoPause_Click";
            _loggerService.ShowUI(0, objLog);

            if (butAutoPause.Text == "Auto")
            {
                //Send Auto Request to STKC
                _lcsParameter.AutoRequest();
            }
            else
            {
                //Send Pause Request to STKC
                _lcsParameter.PauseRequest();
                _loggerService.Trace(0, "Send Pause Request to SM!");
            }
        }
    }
}