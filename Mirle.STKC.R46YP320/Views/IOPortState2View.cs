using Mirle.STKC.R46YP320.Extensions;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class IOPortState2View : Form
    {
        private readonly IOPortView _ioPortView;
        private readonly LoggerService _loggerService;

        public IOPortState2View(IOPortView ioPortView, LoggerService loggerService)
        {
            _ioPortView = ioPortView;
            _loggerService = loggerService;
            InitializeComponent();
        }

        private void IOPortState2View_Load(object sender, EventArgs e)
        {
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Width == 0) return;

                if (_ioPortView.CurrentIOPort == null) return;

                var io = _ioPortView.CurrentIOPort.Signal;
                lblIOReady_FromCrane.BackColor = io.Ready_CraneSide.ToColor();
                lblIOBusy_FromCrane.BackColor = io.Busy_CraneSide.ToColor();
                lblIOTRRequest_FromCrane.BackColor = io.TRRequest_CraneSide.ToColor();
                lblIOComplete_FromCrane.BackColor = io.Complete_CraneSide.ToColor();
                lblIOPGlassDetection_MGV.BackColor = io.GlassDetection_MGV.ToColor();
                lblDoorOpenLimit_MGV.BackColor = io.DoorOpenLimit_MGV.ToColor();

                lblIOCSTTransferComplete_Req.BackColor = io.CSTTransferComplete_Req.ToColor();

                lblIO_SRI_AMSwitchofMPLC.BackColor = io.SRI.AutoManualSwitchIsAuto.ToColor();
                lblIO_SRI_SafetyDoorClose.BackColor = io.SRI.SafetyDoorClosed.ToColor();
                lblIO_SRI_EMO.BackColor = io.SRI.EMO.ToColor();
                lblIO_SRI_MainCircuitOnEnable.BackColor = io.SRI.MainCircuitOnEnable.ToColor();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IOPortState2View_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }
    }
}