using Mirle.STKC.R46YP320.Service;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneTraceLogView : Form
    {
        private readonly int _craneId;
        private readonly LoggerService _loggerService;

        public CraneTraceLogView(int craneId, LoggerService loggerService)
        {
            _craneId = craneId;
            _loggerService = loggerService;
            InitializeComponent();
        }

        private void CraneTraceLogView_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            while (lsbRM1_CMDTrace.Items.Count >= _loggerService.MaxMessageCount)
            {
                lsbRM1_CMDTrace.Items.RemoveAt(0);
            }

            var msgs = _loggerService.GetNewMessagesByCraneId(_craneId);
            foreach (var msg in msgs)
            {
                lsbRM1_CMDTrace.Items.Add(msg);
                lsbRM1_CMDTrace.SelectedIndex = lsbRM1_CMDTrace.Items.Count - 1;
            }
        }
    }
}