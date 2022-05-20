using System;
using System.Windows.Forms;
using Mirle.MPLC;
using Mirle.STKC.R46YP320.Simulator;
using Mirle.Stocker.R46YP320;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class SimAlarmView : Form
    {
        private readonly StockerSimulator _stockerSimulator;
        private readonly IMPLCProvider _mplc;
        private CraneSimulator _crane1;
        private IOPortSimulator _io;
        private CSOTStocker _stocker;

        public SimAlarmView(StockerSimulator stockerSimulator, IMPLCProvider mplc)
        {
            _stockerSimulator = stockerSimulator;
            _mplc = mplc;
            _crane1 = stockerSimulator.Crane1;
            _io = stockerSimulator.GetIoById(1);
            _stocker = stockerSimulator.Stocker;
            InitializeComponent();
        }

        private void buttonDoorOpenHP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.DoorOpenHP();
        }

        private void buttonDoorClosedHP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.DoorClosedHP();
        }

        private void buttonDoorOpenOP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.DoorOpenOP();
        }

        private void buttonDoorClosedOP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.DoorClosedOP();
        }

        private void StockerSimView_Load(object sender, EventArgs e)
        {
            comboBoxCraneID.Items.Add(1);
            comboBoxCraneID.Items.Add(2);
            comboBoxCraneID.SelectedIndex = 0;

            for (int i = 0; i < 25; i++)
            {
                comboBoxIOPortID.Items.Add(i + 1);
            }
            comboBoxIOPortID.SelectedIndex = 0;

            comboBoxAlarmCode.SelectedIndex = 1;
            comboBoxAlarmCodeIO.SelectedIndex = 1;
            timerRefresh.Enabled = true;
        }

        private void buttonAlarmSet_Click(object sender, EventArgs e)
        {
            var alarmCode = Convert.ToInt32(comboBoxAlarmCode.Text, 16);
            _crane1.SetAlarm(alarmCode);
        }

        private void buttonAlarmClear_Click(object sender, EventArgs e)
        {
            _crane1.ClearAlarm();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            var crane = _stocker.GetCraneById(comboBoxCraneID.SelectedIndex + 1) as Crane;
            textBoxPCIndex.Text = crane.Signal.Controller.PcErrorIndex.GetValue().ToString();
            textBoxPLCIndex.Text = crane.Signal.ErrorIndex.GetValue().ToString();
            textBoxAlarmCode.Text = crane.Signal.ErrorCode.GetValue().ToString("X4");

            var io = _stocker.GetIOPortById(comboBoxIOPortID.SelectedIndex + 1) as IOPort;
            textBoxPCIndexIO.Text = io.Signal.Controller.PcErrorIndex.GetValue().ToString();
            textBoxPLCIndexIO.Text = io.Signal.ErrorIndex.GetValue().ToString();
            textBoxAlarmCodeIO.Text = io.Signal.ErrorCode.GetValue().ToString("X4");
        }

        private void buttonAlarmSetIO_Click(object sender, EventArgs e)
        {
            var alarmCode = Convert.ToInt32(comboBoxAlarmCodeIO.Text, 16);
            _io.SetAlarm(alarmCode);
        }

        private void buttonAlarmClearIO_Click(object sender, EventArgs e)
        {
            _io.ClearAlarm();
        }

        private void buttonKeySwitchAutoHP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.KeySwitchAutoHP();
        }

        private void buttonKeySwitchOffHP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.KeySwitchOffHP();
        }

        private void buttonKeySwitchAutoOP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.KeySwitchAutoOP();
        }

        private void buttonKeySwitchOffOP_Click(object sender, EventArgs e)
        {
            _stockerSimulator.KeySwitchOffOP();
        }

        private void comboBoxCraneID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _crane1 = comboBoxCraneID.SelectedIndex == 0
                ? _stockerSimulator.Crane1 : _stockerSimulator.Crane2;
        }

        private void comboBoxIOPortID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _io = _stockerSimulator.GetIoById(comboBoxIOPortID.SelectedIndex + 1);
        }

        private void buttonAreaSensorSignal1On_Click(object sender, EventArgs e)
        {
            try
            {
                _stockerSimulator.AreaSensorSignalOn(Convert.ToInt32(txtGroup.Text));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void buttonAreaSensorSignal1Off_Click(object sender, EventArgs e)
        {
            try
            {
                _stockerSimulator.AreaSensorSignalOff();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void buttonPLCWriter_WriteWord_Click(object sender, EventArgs e)
        {
            try
            {
                var address = textBoxPLCWriter_Word.Text.ToUpper();
                var value = Convert.ToInt32(textBoxPLCWriter_Word_Value.Text);
                _mplc.WriteWord(address, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}-{ex.StackTrace}");
            }
        }

        private void buttonPLCWriter_WriteBitOn_Click(object sender, EventArgs e)
        {
            try
            {
                var address = textBoxPLCWriter_Bit.Text.ToUpper();
                _mplc.SetBitOn(address);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}-{ex.StackTrace}");
            }
        }

        private void buttonPLCWriter_WriteBitOff_Click(object sender, EventArgs e)
        {
            try
            {
                var address = textBoxPLCWriter_Bit.Text.ToUpper();
                _mplc.SetBitOff(address);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}-{ex.StackTrace}");
            }
        }
    }
}
