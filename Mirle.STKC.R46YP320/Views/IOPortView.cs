using Mirle.LCS;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class IOPortView : Form
    {
        private readonly CSOTStocker _stocker;
        private readonly LoggerService _loggerService;
        private readonly LCSInfo _lcsInfo;
        private readonly STKCHost _stkcHost;
        public int CurrentIOPortNumber { get; private set; }
        public IOPort CurrentIOPort { get; private set; }

        private IOPortState1View _ioPortState1View;
        private IOPortState2View _ioPortState2View;
        private IOPortVehicleView _ioPortVehicleView;
        private IOPortInterfaceView _ioPortInterfaceView;

        public IOPortView(CSOTStocker stocker, STKCHost stkcHost)
        {
            _stocker = stocker;
            _loggerService = stkcHost.GetLoggerService();
            _lcsInfo = stkcHost.GetLCSInfo();
            _stkcHost = stkcHost;
            InitializeComponent();
        }

        private void IOPortView_Load(object sender, EventArgs e)
        {
            cboIOPort.Items.Clear();
            foreach (var ioInfo in _lcsInfo.Stocker.IoInfos)
            {
                cboIOPort.Items.Add($"{ioInfo.PortTypeIndex}-{ioInfo.HostEQPortId}_{ioInfo.PLCPortId}");
            }

            if (cboIOPort.Items.Count > 0) { cboIOPort.SelectedIndex = 0; }

            _ioPortState1View = new IOPortState1View(this, _loggerService, _lcsInfo, _stkcHost.GetAlarmService());
            _ioPortState2View = new IOPortState2View(this, _loggerService);
            _ioPortVehicleView = new IOPortVehicleView(this, _loggerService, _lcsInfo);
            _ioPortInterfaceView = new IOPortInterfaceView(this, _loggerService);

            butIO_State1.PerformClick();
        }

        private void ChangeSubForm(Form subForm)
        {
            butIO_State1.BackColor = subForm == _ioPortState1View ? Color.Aqua : Color.Gainsboro;
            butIO_State2.BackColor = subForm == _ioPortState2View ? Color.Aqua : Color.Gainsboro;
            butIO_Vehicle.BackColor = subForm == _ioPortVehicleView ? Color.Aqua : Color.Gainsboro;
            butIO_InterFace.BackColor = subForm == _ioPortInterfaceView ? Color.Aqua : Color.Gainsboro;

            var children = subFormPanel.Controls;
            foreach (Control c in children)
            {
                if (c is Form)
                {
                    var thisChild = c as Form;
                    //thisChild.Hide();
                    this.subFormPanel.Controls.Remove(thisChild);
                    thisChild.Width = 0;
                }
            }

            Form newChild = subForm;

            if (newChild != null)
            {
                newChild.TopLevel = false;
                newChild.Dock = System.Windows.Forms.DockStyle.Fill;//適應窗體大小
                newChild.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                newChild.Parent = subFormPanel;
                this.subFormPanel.Controls.Add(newChild);
                newChild.Show();
            }
        }

        private void butIO_State1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ioPortState1View == null)
                {
                    _ioPortState1View = new IOPortState1View(this, _loggerService, _lcsInfo, _stkcHost.GetAlarmService());
                }

                ChangeSubForm(_ioPortState1View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butIO_State2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ioPortState2View == null)
                {
                    _ioPortState2View = new IOPortState2View(this, _loggerService);
                }

                ChangeSubForm(_ioPortState2View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butIO_Vehicle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ioPortVehicleView == null)
                {
                    _ioPortVehicleView = new IOPortVehicleView(this, _loggerService, _lcsInfo);
                }

                ChangeSubForm(_ioPortVehicleView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butIO_InterFace_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ioPortInterfaceView == null)
                {
                    _ioPortInterfaceView = new IOPortInterfaceView(this, _loggerService);
                }

                ChangeSubForm(_ioPortInterfaceView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void cboIOPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboIOPort.Text.Trim() == string.Empty) return;

                int.TryParse(cboIOPort.Text.Split("-"[0])[0], out var intIO);
                CurrentIOPortNumber = intIO;
                CurrentIOPort = _stocker.GetIOPortById(intIO) as IOPort;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}