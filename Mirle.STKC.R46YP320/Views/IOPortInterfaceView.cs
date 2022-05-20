using Mirle.STKC.R46YP320.Extensions;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class IOPortInterfaceView : Form
    {
        private readonly IOPortView _ioPortView;
        private readonly LoggerService _loggerService;

        public IOPortInterfaceView(IOPortView ioPortView, LoggerService loggerService)
        {
            _ioPortView = ioPortView;
            _loggerService = loggerService;
            InitializeComponent();
        }

        private void IOPortInterfaceView_Load(object sender, EventArgs e)
        {
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Width == 0) return;

                if (_ioPortView.CurrentIOPort == null) return;

                var io = _ioPortView.CurrentIOPort.Signal;

                lblIF_L_REQ.BackColor = io.L_REQ.ToColor();
                lblIF_U_REQ.BackColor = io.U_REQ.ToColor();
                lblIF_Ready.BackColor = io.Ready.ToColor();
                lblIF_CARRIER.BackColor = io.CARRIER.ToColor();
                lblIF_PError.BackColor = io.PError.ToColor();
                lblIF_Spare.BackColor = io.Spare.ToColor();
                lblIF_Online.BackColor = io.POnline.ToColor();
                lblIF_PEStop.BackColor = io.PEStop.ToColor();
                lblIF_Transferring_FromSTK.BackColor = io.Transferring_FromSTK.ToColor();
                lblIF_TR_REQ_FromSTK.BackColor = io.TR_REQ_FromSTK.ToColor();
                lblIF_BUSY_FromSTK.BackColor = io.BUSY_FromSTK.ToColor();
                lblIF_COMPLETE_FromSTK.BackColor = io.COMPLETE_FromSTK.ToColor();
                lblIF_Crane1FromSTK.BackColor = io.CRANE_1_FromSTK.ToColor();
                lblIF_Crane2FromSTK.BackColor = io.CRANE_2_FromSTK.ToColor();
                lblIF_AStopFromSTK.BackColor = io.AError_FromSTK.ToColor();
                lblIF_ForkNumberFromSTK.BackColor = io.ForkNumber_FromSTK.ToColor();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void IOPortInterfaceView_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }
    }
}
