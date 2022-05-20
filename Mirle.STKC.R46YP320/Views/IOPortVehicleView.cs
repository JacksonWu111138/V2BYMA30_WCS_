using Mirle.Extensions;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class IOPortVehicleView : Form
    {
        private readonly IOPortView _ioPortView;
        private IOVehicle _myVehicle;
        private readonly LoggerService _loggerService;
        private readonly LCSInfo _lcsInfo;

        int _lastIOPortNumber = 0;

        public IOPortVehicleView(IOPortView ioPortView, LoggerService loggerService, LCSInfo lcsInfo)
        {
            _ioPortView = ioPortView;
            _loggerService = loggerService;
            _lcsInfo = lcsInfo;
            InitializeComponent();
        }

        private void IOPortVehicleView_Load(object sender, EventArgs e)
        {
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            int intIO = _ioPortView.CurrentIOPortNumber;

            var vehicleCount = _lcsInfo.Stocker.GetIoInfoByIndex(intIO).Vehicles;
            if (intIO != _lastIOPortNumber)
            {
                _lastIOPortNumber = intIO;
                cboVehicle.Items.Clear();
                for (int intVehicle = 0; intVehicle < vehicleCount; intVehicle++)
                {
                    cboVehicle.Items.Add("Vehicle-" + (intVehicle + 1).ToString());
                }

                if (cboVehicle.Items.Count > 0) cboVehicle.SelectedIndex = 0;
            }

            tlpVehicle1.Visible = vehicleCount >= 1;

            tlpVehicle2.Visible = vehicleCount >= 2;

            tlpVehicle3.Visible = vehicleCount >= 3;

            tlpVehicle4.Visible = vehicleCount >= 4;

            tlpVehicle5.Visible = vehicleCount >= 5;

            if (_ioPortView.CurrentIOPort == null) return;

            var io = _ioPortView.CurrentIOPort.Signal;
            lblVehicle1_REP.Text = io.GetVehicleSignalById(1)?.RealTimePosition.GetValue().ToString();
            lblVehicle2_REP.Text = io.GetVehicleSignalById(2)?.RealTimePosition.GetValue().ToString();
            lblVehicle3_REP.Text = io.GetVehicleSignalById(3)?.RealTimePosition.GetValue().ToString();
            lblVehicle4_REP.Text = io.GetVehicleSignalById(4)?.RealTimePosition.GetValue().ToString();
            lblVehicle5_REP.Text = io.GetVehicleSignalById(5)?.RealTimePosition.GetValue().ToString();

            if (cboVehicle.Text.Trim() != string.Empty)
            {
                int.TryParse(cboVehicle.Text.Split("-"[0])[1], out var intVehicle);
                var vehicle = io.GetVehicleSignalById(intVehicle);

                lblVehicleAuto.BackColor = vehicle.Auto.ToColor();
                lblVehicleLoaded.BackColor = vehicle.LoadPresence.ToColor();
                lblVehicleActive.BackColor = vehicle.Active.ToColor();
                lblVehicleError.BackColor = vehicle.Error.ToColor();
                lblVehicleHomePosition.BackColor = vehicle.HomePosition.ToColor();
                lblVehicleHPReturn.BackColor = vehicle.HPReturn.ToColor();

                lblVehiclePosition1.BackColor = vehicle.CurrentLocation_P1.ToColor();
                lblVehiclePosition2.BackColor = vehicle.CurrentLocation_P2.ToColor();
                lblVehiclePosition3.BackColor = vehicle.CurrentLocation_P3.ToColor();
                lblVehiclePosition4.BackColor = vehicle.CurrentLocation_P4.ToColor();
                lblVehiclePosition5.BackColor = vehicle.CurrentLocation_P5.ToColor();

                lblVehicleCSTID.Text = vehicle.CarrierId.GetData().ToASCII();
            }
            else
            {
                lblVehicleAuto.BackColor = Color.White;
                lblVehicleLoaded.BackColor = Color.White;
                lblVehicleActive.BackColor = Color.White;
                lblVehicleError.BackColor = Color.White;
                lblVehicleHomePosition.BackColor = Color.White;
                lblVehicleHPReturn.BackColor = Color.White;

                lblVehiclePosition1.BackColor = Color.White;
                lblVehiclePosition2.BackColor = Color.White;
                lblVehiclePosition3.BackColor = Color.White;
                lblVehiclePosition4.BackColor = Color.White;
                lblVehiclePosition5.BackColor = Color.White;

                lblVehicleCSTID.Text = string.Empty;
            }
        }

        private void cboVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ioPortView.CurrentIOPort == null) return;

            var io = _ioPortView.CurrentIOPort.Signal;
            if (cboVehicle.Text.Trim() != string.Empty)
            {
                int.TryParse(cboVehicle.Text.Split("-"[0])[1], out var intVehicle);
                _myVehicle = _ioPortView.CurrentIOPort.GetVehicleById(intVehicle) as IOVehicle;
            }
        }

        private async void butIOVehicleRun_Click(object sender, EventArgs e)
        {
            butIOVehicleRun.Enabled = false;

            try
            {
                if (_myVehicle == null) return;

                await _myVehicle.RequestRunAsync();

                int intIO = _ioPortView.CurrentIOPortNumber;
                int intVehicle = _myVehicle.Id;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIOVehicleRun_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(intIO).HostEQPortId + "-" + intVehicle.ToString();
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIOVehicleRun.Enabled = true;
        }

        private async void butIOVehicleFaultReset_Click(object sender, EventArgs e)
        {
            butIOVehicleFaultReset.Enabled = false;

            try
            {
                if (_myVehicle == null) return;

                await _myVehicle.RequestFaultResetAsync();

                int intIO = _ioPortView.CurrentIOPortNumber;
                int intVehicle = _myVehicle.Id;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIOVehicleFaultReset_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(intIO).HostEQPortId + "-" + intVehicle.ToString();
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIOVehicleFaultReset.Enabled = true;
        }

        private async void butIOVehicleReturnHome_Click(object sender, EventArgs e)
        {
            butIOVehicleReturnHome.Enabled = false;

            try
            {
                if (_myVehicle == null) return;

                await _myVehicle.RequestReturnHomeAsync();

                int intIO = _ioPortView.CurrentIOPortNumber;
                int intVehicle = _myVehicle.Id;
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIOVehicleReturnHome_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(intIO).HostEQPortId + "-" + intVehicle.ToString();
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIOVehicleReturnHome.Enabled = true;
        }

        private void IOPortVehicleView_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }
    }
}