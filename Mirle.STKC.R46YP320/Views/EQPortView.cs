using Mirle.LCS.Models.Info;
using Mirle.Stocker.R46YP320;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class EQPortView : Form
    {
        private readonly IEnumerable<EqInfo> _eqInfos;
        private readonly CSOTStocker _stocker;
        private EQPort _myEqPort;

        public EQPortView(IEnumerable<EqInfo> eqInfos, CSOTStocker stocker)
        {
            _eqInfos = eqInfos;
            _stocker = stocker;
            InitializeComponent();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            if (_myEqPort == null) return;

            lblEQCSTID.Text = _myEqPort.CSTID;
            lblEQ_L_REQ.BackColor = _myEqPort.Signal.L_REQ.IsOn() ? Color.Yellow : Color.White;
            lblEQ_U_REQ.BackColor = _myEqPort.Signal.U_REQ.IsOn() ? Color.Yellow : Color.White;
            lblEQ_Ready.BackColor = _myEqPort.Signal.Ready.IsOn() ? Color.Yellow : Color.White;
            lblEQ_Carrier.BackColor = _myEqPort.Signal.Carrier.IsOn() ? Color.Yellow : Color.White;
            lblEQ_PError.BackColor = _myEqPort.Signal.PError.IsOn() ? Color.Yellow : Color.White;
            lblEQ_Spare.BackColor = _myEqPort.Signal.Spare.IsOn() ? Color.Yellow : Color.White;
            lblEQ_POnLine.BackColor = _myEqPort.Signal.POnline.IsOn() ? Color.Yellow : Color.White;
            lblEQ_PEStop.BackColor = _myEqPort.Signal.PEStop.IsOn() ? Color.Yellow : Color.White;

            lblEQ_Transferring_FromSTK.BackColor = _myEqPort.Signal.Transferring_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_TR_REQ_FromSTK.BackColor = _myEqPort.Signal.TR_REQ_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_BUSY_FromSTK.BackColor = _myEqPort.Signal.BUSY_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_COMPLETE_FromSTK.BackColor = _myEqPort.Signal.COMPLETE_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_Crane1FromSTK.BackColor = _myEqPort.Signal.CRANE_1_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_Crane2FromSTK.BackColor = _myEqPort.Signal.CRANE_2_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_AError_FromSTK.BackColor = _myEqPort.Signal.AError_FromSTK.IsOn() ? Color.Yellow : Color.White;
            lblEQ_ForkNumber_FromSTK.BackColor = _myEqPort.Signal.ForkNumber_FromSTK.IsOn() ? Color.Yellow : Color.White;
        }

        private void EQPortView_Load(object sender, EventArgs e)
        {
            cboEQPort.Items.Clear();
            foreach (var eqInfo in _eqInfos)
            {
                cboEQPort.Items.Add(eqInfo.PortTypeIndex.ToString() + "-" + eqInfo.HostEQPortId + "_" + eqInfo.PLCPortId);
            }
            if (cboEQPort.Items.Count > 0) { cboEQPort.SelectedIndex = 0; }
        }

        private void cboEQPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEQPort.Text.Trim() == string.Empty) return;

            int.TryParse(cboEQPort.Text.Split("-"[0])[0], out var intEQ);
            _myEqPort = _stocker.GetEQPortById(intEQ) as EQPort;
        }

        private void EQPortView_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }
    }
}
