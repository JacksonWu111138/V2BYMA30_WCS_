using Mirle.STKC.R46YP320.Simulator;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class SimMPLCSettingView : Form
    {
        private readonly LCSInfo _lcsInfo;
        private StockerSimulator _simulator;

        public SimMPLCSettingView(LCSInfo lcsInfo, StockerSimulator simulator)
        {
            _lcsInfo = lcsInfo;
            _simulator = simulator;
            InitializeComponent();
        }

        private void butMPLCInitial_C1_Click(object sender, EventArgs e)
        {
            try
            {
                _simulator.InitialCrane1();
            }
            catch (Exception ex)
            {
            }
        }

        private void butMPLCInitial_C2_Click(object sender, EventArgs e)
        {
            try
            {
                _simulator.InitialCrane2();
            }
            catch (Exception ex)
            {
            }
        }

        private void butToggleSingleCraneMode_C1_Click(object sender, EventArgs e)
        {
            var crane1 = _simulator.GetCraneById(1);
            crane1.ToggleSingleCraneMode();
        }

        private void butToggleSingleCraneMode_C2_Click(object sender, EventArgs e)
        {
            var crane2 = _simulator.GetCraneById(2);
            crane2.ToggleSingleCraneMode();
        }

        private void timerUIController_Tick(object sender, EventArgs e)
        {
            _simulator.SetCrane1Start(ckbStart_C1.Checked);
            _simulator.SetCrane2Start(ckbStart_C2.Checked);

            var crane1 = _simulator.GetCraneById(1);
            lblMPLCC1ExecuteTaskNo.Text = crane1.ExcutingCommandNo;
            crane1.SetBCRReplyCstid(chkSetFBCRReplay_C1.Checked ? cbbFBCRReplayCSTID_C1.Text : string.Empty);
            crane1.SetDoubleStorageSimulation(rdbDSSim_C1.Checked);
            crane1.SetEmptyRetrieveSimulation(rdbERSim_C1.Checked);
            crane1.SetIdMismatchSimulation(rdbIDMismatch_C1.Checked);
            crane1.SetTypeMismatchSimulation(rdbTypeMismatch_C1.Checked);
            crane1.SetIdReadFailSimulation(rdbReadFail_C1.Checked);
            crane1.SetCycle1InterlockError(rdbC1InterLockError_C1.Checked);
            crane1.SetCycle2InterlockError(rdbC2InterLockError_C1.Checked);
            crane1.SetInterlockCstOnFork(rdbInterLock_CstOnFork_C1.Checked);
            crane1.SetE1WrongCommandSimulation(rdbE1WrongCmd_C1.Checked);
            crane1.SetCustomerCompleteCode(comboBox_CompleteCode_C1.Text);

            var crane2 = _simulator.GetCraneById(2);
            lblMPLCC2ExecuteTaskNo.Text = crane2.ExcutingCommandNo;
            crane2.SetBCRReplyCstid(chkSetFBCRReplay_C2.Checked ? cbbFBCRReplayCSTID_C2.Text : string.Empty);
            crane2.SetDoubleStorageSimulation(rdbDSSim_C2.Checked);
            crane2.SetEmptyRetrieveSimulation(rdbERSim_C2.Checked);
            crane2.SetIdMismatchSimulation(rdbIDMismatch_C2.Checked);
            crane2.SetTypeMismatchSimulation(rdbTypeMismatch_C2.Checked);
            crane2.SetIdReadFailSimulation(rdbReadFail_C2.Checked);
            crane2.SetCycle1InterlockError(rdbC1InterLockError_C2.Checked);
            crane2.SetCycle2InterlockError(rdbC2InterLockError_C2.Checked);
            crane2.SetInterlockCstOnFork(rdbInterLock_CstOnFork_C2.Checked);
            crane2.SetE1WrongCommandSimulation(rdbE1WrongCmd_C2.Checked);
            crane2.SetCustomerCompleteCode(comboBox_CompleteCode_C2.Text);

            if (ckbAutoWaitIn.Checked)
            {
                if (cbbIOPort.Text.Trim() == string.Empty) { return; }
                int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

                var io = _simulator.GetIoById(intIOIndex);
                if (io.IsReadyForWaitIn())
                {
                    txtIOPortCSTID.Text = txtIOPortCSTID.Text.Substring(0, 1) + (Convert.ToInt32(txtIOPortCSTID.Text.Substring(1, 5)) + 1).ToString("00000");
                    butIOPortWaitIn_Click(sender, e);
                }
            }
        }

        private void rdbNormal_C1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNormal_C1.Checked)
            {
                chkSetFBCRReplay_C1.Checked = false;
                cbbFBCRReplayCSTID_C1.Text = string.Empty;
            }
        }

        private void rdbIDMismatch_C1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbIDMismatch_C1.Checked)
            {
                chkSetFBCRReplay_C1.Checked = true;
                cbbFBCRReplayCSTID_C1.Text = "ABCDEF";
            }
        }

        private void rdbTypeMismatch_C1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTypeMismatch_C1.Checked)
            {
                
            }
        }

        private void rdbReadFail_C1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReadFail_C1.Checked)
            {
                chkSetFBCRReplay_C1.Checked = true;
                cbbFBCRReplayCSTID_C1.SelectedItem = "ERROR1";
            }
        }

        private void rdbNormal_C2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNormal_C2.Checked)
            {
                chkSetFBCRReplay_C2.Checked = false;
                cbbFBCRReplayCSTID_C2.Text = string.Empty;
            }
        }

        private void rdbIDMismatch_C2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbIDMismatch_C2.Checked)
            {
                chkSetFBCRReplay_C2.Checked = true;
                cbbFBCRReplayCSTID_C2.Text = "ABCDEF";
            }
        }

        private void rdbTypeMismatch_C2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTypeMismatch_C2.Checked)
            {

            }
        }

        private void rdbReadFail_C2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbReadFail_C2.Checked)
            {
                chkSetFBCRReplay_C2.Checked = true;
                cbbFBCRReplayCSTID_C2.SelectedItem = "ERROR1";
            }
        }

        private async void butMPLCInitial_IOPort_Click(object sender, EventArgs e)
        {
            butMPLCInitial_IOPort.Enabled = false;

            await Task.Run(() =>
            {
                try
                {
                    _simulator.InitialAllIoPort();
                }
                catch (Exception ex)
                {
                }
            });

            butMPLCInitial_IOPort.Enabled = true;
        }

        private void butInitial_SingleIOPort_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

            var io = _simulator.GetIoById(intIOIndex);
            io?.Initial();
        }

        private void butIOPortWaitIn_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

            var io = _simulator.GetIoById(intIOIndex);
            io?.WaitIn(txtIOPortCSTID.Text);
        }

        private void butIOPortWaitOut_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

            var io = _simulator.GetIoById(intIOIndex);
            io?.WaitOut(txtIOPortCSTID.Text);
        }

        private void butIORemove_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

            var io = _simulator.GetIoById(intIOIndex);
            io?.Remove();
        }

        private void butIOPresentOn_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);
            int intStage = Convert.ToInt32(cbbIOPortStage.Text);

            var io = _simulator.GetIoById(intIOIndex);
            io.PresentOn(intStage, txtIOPortCSTID.Text);
        }

        private void butIOPresentOff_Click(object sender, EventArgs e)
        {
            if (cbbIOPort.Text.Trim() == string.Empty) { return; }
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);
            int intStage = Convert.ToInt32(cbbIOPortStage.Text);

            var io = _simulator.GetIoById(intIOIndex);
            io.PresentOff(intStage);
        }

        private void cbbIOPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intIOIndex = Convert.ToInt32(cbbIOPort.Text.Split('-')[0]);

            cbbIOPortStage.Items.Clear();
            var io = _lcsInfo.Stocker.GetIoInfoByIndex(intIOIndex);
            for (int i = 1; i <= io.Stage; i++)
            {
                cbbIOPortStage.Items.Add(i);
            }
            cbbIOPortStage.SelectedIndex = 0;
        }

        private async void butMPLCInitial_EQPort_Click(object sender, EventArgs e)
        {
            butMPLCInitial_EQPort.Enabled = false;
            await Task.Run(() =>
            {
                try
                {
                    _simulator.InitialAllEqPort();
                }
                catch (Exception ex)
                {
                }
            });

            butMPLCInitial_EQPort.Enabled = true;
        }

        private void butInitial_SingleIEQort_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }
            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.Initial();
        }

        private void butSetID_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }
            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetCSTID(txtEQCSTID.Text);
        }

        private void butEQCSTPresentOn_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }
            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetPresentOn();
        }

        private void butEQCSTPresentOff_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }
            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetPresentOff();
        }

        private void butEQOnlineOn_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq?.SetOnline();
        }

        private void butEQOnlineOff_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq?.SetOffline();
        }

        private void butEQLoadReq_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetLoadRequest();
        }

        private void butEQUnloadReq_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetUnloadRequest();
        }

        private void butEQNoReq_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetNoRequest();
        }

        private void butEQPriorityUpOn_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }

            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetPriorityUpdateOn();
        }

        private void butEQPriorityUpOff_Click(object sender, EventArgs e)
        {
            if (cbbEQPort.Text.Trim() == string.Empty) { return; }
            int intEQIndex = Convert.ToInt32(cbbEQPort.Text.Split('-')[0]);

            var eq = _simulator.GetEqById(intEQIndex);
            eq.SetPriorityUpdateOff();
        }

        private void MPLCSettingView_Load(object sender, EventArgs e)
        {
            //Initiall cbbIOPort
            cbbIOPort.Items.Clear();
            foreach (var ioInfo in _lcsInfo.Stocker.IoInfos)
            {
                cbbIOPort.Items.Add($"{ioInfo.PortTypeIndex}-{ioInfo.HostEQPortId}");
            }

            //Initiall cbbEQPort
            cbbEQPort.Items.Clear();
            foreach (var eqInfo in _lcsInfo.Stocker.EqInfos)
            {
                cbbEQPort.Items.Add($"{eqInfo.PortTypeIndex}-{eqInfo.HostEQPortId}");
            }

            _simulator.SetPortInfo(_lcsInfo.Stocker.PortInfos);

            switch (_lcsInfo.Stocker.CraneCount)
            {
                case 1:
                    gpbProcess_C1.Visible = true;
                    gpbProcess_C2.Visible = false;
                    break;

                case 2:
                    gpbProcess_C1.Visible = true;
                    gpbProcess_C2.Visible = true;
                    break;

                default:
                    gpbProcess_C1.Visible = false;
                    gpbProcess_C2.Visible = false;
                    break;
            }

            timerUIController.Enabled = true;
        }
    }
}
