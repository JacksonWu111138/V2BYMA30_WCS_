using Mirle.STKC.R46YP320.Service;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class SystraceView : Form
    {
        private readonly LoggerService _loggerService;
        public readonly int MaxMessageCount;

        public SystraceView(LoggerService loggerService)
        {
            _loggerService = loggerService;
            MaxMessageCount = _loggerService.MaxMessageCount;
            InitializeComponent();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            while (lstSysTrace.Items.Count >= MaxMessageCount)
            {
                lstSysTrace.Items.RemoveAt(0);
            }

            var msgs = _loggerService.GetNewMessages();
            foreach (var msg in msgs)
            {
                lstSysTrace.Items.Add(msg);
                lstSysTrace.SelectedIndex = lstSysTrace.Items.Count - 1;
            }
        }

        private void SystraceView_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }
    }
}