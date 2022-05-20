using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class HandoffAreaSetView : Form
    {
        private readonly CSOTStocker _stocker;
        private readonly STKCHost _stkcHost;
        private readonly LoggerService _loggerService;

        public HandoffAreaSetView(CSOTStocker stocker, STKCHost stkcHost, LoggerService loggerService)
        {
            _stocker = stocker;
            _stkcHost = stkcHost;
            _loggerService = loggerService;
            InitializeComponent();
        }

        private async void butSetHandoffSetting_Click(object sender, EventArgs e)
        {
            int.TryParse(txtShareAreaStart.Text, out var sharedAreaStart);
            int.TryParse(txtShareAreaEnd.Text, out var sharedAreaEnd);
            int.TryParse(txtHandoffAreaStart.Text, out var handoffAreaStart);
            int.TryParse(txtHandoffAreaEnd.Text, out var handoffAreaEnd);

            TraceLogFormat objLog = new TraceLogFormat();

            //Log Process
            string strMsg = "Handoff Area and Shared Area Setting(SS:" + sharedAreaStart.ToString() +
                            ",SE:" + sharedAreaEnd.ToString() +
                            ",HS:" + handoffAreaStart.ToString() +
                            ",HE:" + handoffAreaEnd.ToString() + ")";

            try
            {
                var result = await _stocker.SetShareAreaAndHandoffAsync(
                    sharedAreaStart,
                    sharedAreaEnd,
                    handoffAreaStart,
                    handoffAreaEnd);

                objLog.Message = result ? strMsg : strMsg + " Fail!";
                if (result == false)
                {
                    MessageBox.Show(
@"ShareAreaStart <= HandoffAreaStart
HandoffAreaStart <= HandoffAreaEnd
HandoffAreaEnd <= ShareAreaEnd", "Setting Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                objLog.Message = strMsg + " Fail!";
            }
            _loggerService.ShowUI(0, objLog);
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            var ctrl = _stocker.Signal.Controller;
            lblShareAreaStart.Text = ctrl.ShareArea_StartBay.GetValue().ToString();
            lblShareAreaEnd.Text = ctrl.ShareArea_EndBay.GetValue().ToString();
            lblHandoffAreaStart.Text = ctrl.HandOff_StartBay.GetValue().ToString();
            lblHandoffAreaEnd.Text = ctrl.HandOff_EndBay.GetValue().ToString();

            var status = _stocker.Signal;
            lblShareAreaStart_Dynamic.Text = status.ShareArea_StartBay.GetValue().ToString();
            lblShareAreaEnd_Dynamic.Text = status.ShareArea_EndBay.GetValue().ToString();
            lblHandoffAreaStart_Dynamic.Text = status.HandOff_StartBay.GetValue().ToString();
            lblHandoffAreaEnd_Dynamic.Text = status.HandOff_EndBay.GetValue().ToString();
        }

        private void HandoffAreaSetView_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }
    }
}