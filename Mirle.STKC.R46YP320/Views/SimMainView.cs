using Mirle.MPLC.DataBlocks;
using Mirle.MPLC.SharedMemory;
using Mirle.STKC.R46YP320.Simulator;
using Mirle.Stocker.R46YP320;
using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class SimMainView : Form
    {
        private readonly LCSInfo _lcsInfo;
        private StockerSimulator _stockerSimulator;

        public SimMainView(LCSInfo lcsInfo)
        {
            _lcsInfo = lcsInfo;
            InitializeComponent();
        }

        private void SimMainView_Load(object sender, EventArgs e)
        {
            var smMPLC = new SMReadWriter();
            foreach (var block in SignalMapper4_11.SignalBlocks)
            {
                smMPLC.AddDataBlock(new SMDataBlockInt32(block.DeviceRange, $@"Global\{_lcsInfo.Stocker.StockerId}-{block.SharedMemoryName}"));
            }

            _stockerSimulator = new StockerSimulator(smMPLC, _lcsInfo.Stocker.ControlMode);

            ShowFormToTabPage(new SimMPLCSettingView(_lcsInfo, _stockerSimulator), tbpMPLCSetting);
            ShowFormToTabPage(new SimAlarmView(_stockerSimulator, smMPLC), tbpAlarm);

            this.Text = this.Text + $"{_lcsInfo.Stocker.StockerId}  v " + ProductVersion;
        }

        private void SimMainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void ShowFormToTabPage(Form form, TabPage tabPage)
        {
            if (form == null) return;
            form.TopLevel = false;
            form.Dock = System.Windows.Forms.DockStyle.Fill; //適應窗體大小
            form.FormBorderStyle = FormBorderStyle.None; //隱藏右上角的按鈕
            form.Parent = tabPage;
            tabPage.Controls.Add(form);
            form.Show();
        }
    }
}
