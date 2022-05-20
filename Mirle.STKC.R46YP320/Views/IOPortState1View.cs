using Mirle.STKC.R46YP320.Extensions;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using System;
using System.Drawing;
using Mirle.Stocker.R46YP320;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class IOPortState1View : Form
    {
        private readonly IOPortView _ioPortView;
        private readonly LoggerService _loggerService;
        private readonly LCSInfo _lcsInfo;
        private readonly AlarmService _alarmService;

        public IOPortState1View(IOPortView ioPortView, LoggerService loggerService, LCSInfo lcsInfo, AlarmService alarmService)
        {
            _ioPortView = ioPortView;
            _loggerService = loggerService;
            _lcsInfo = lcsInfo;
            _alarmService = alarmService;
            InitializeComponent();
        }

        private void IOPortState1View_Load(object sender, EventArgs e)
        {
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Width == 0) return;

                if (_ioPortView.CurrentIOPort == null) return;

                var showSecondString = "N/A";
                if (_ioPortView.CurrentIOPort.NextCanAutoSetRunTime != DateTime.MaxValue)
                {
                    var totalSeconds = (_ioPortView.CurrentIOPort.NextCanAutoSetRunTime - DateTime.Now).TotalSeconds;
                    showSecondString = $"{(totalSeconds < 0 ? 0 : totalSeconds):0}s";
                }
                chkAutoSetRun.Text = $"Auto Set Run ({showSecondString})";

                _ioPortView.CurrentIOPort.CanAutoSetRun = chkAutoSetRun.Checked;

                var io = _ioPortView.CurrentIOPort.Signal;

                int intIO = _ioPortView.CurrentIOPortNumber;

                butIOAutoManual.Text = io.Run.IsOn() ? "Manual" : "Auto";
                lblIORun.BackColor = io.Run.ToColor();
                lblIODown.BackColor = io.Down.ToColor();
                lblIOFault.BackColor = io.Fault.ToColor();
                lblIOInMode.BackColor = io.InMode.ToColor();
                lblIOOutMode.BackColor = io.OutMode.ToColor();
                lblIOAMMode.BackColor = io.AutoManualMode.ToColor();
                lblIOLoadOK.BackColor = io.LoadOK.ToColor();
                lblIOUnloadOK.BackColor = io.UnloadOK.ToColor();
                lblIOPLCBatteryLow.BackColor = io.PLCBatteryLow_CPU.ToColor();
                lblIORunEnable.BackColor = io.RunEnable.ToColor();
                lblIOPortModeChangeable.BackColor = _ioPortView.CurrentIOPort.IsPortModeChangeable.ToColor();

                var ctrl = io.Controller;
                butIO_EnableDisableP1FBCR.Text =
                    ctrl.BCRDisable_P1.IsOn() ? "Enable Load Port FBCR" : "Disable Load Port FBCR";
                lblP1_FBCREnable.BackColor = ctrl.BCRDisable_P1.ToColor();
                butIO_FaultReset.BackColor = ctrl.FaultReset.ToButtonColor();
                butIO_BuzzerStop.BackColor = ctrl.BuzzerStop.ToButtonColor();
                butIO_ManualTriggerFBCRRescan.BackColor = ctrl.IDReadCommand.ToButtonColor();
                butIO_MoveBackForMGV.BackColor = ctrl.MoveBack.ToButtonColor();
                butIO_PortModeChangeReq.BackColor = ctrl.RequestInputMode.IsOn()
                    ? Color.Lime
                    : (ctrl.RequestOutputMode.IsOn() ? Color.DodgerBlue : Color.Gainsboro);

                lblIOBCRReadDone_Req.BackColor = io.BCRReadDone.ToColor();
                lblIOWaitIn.BackColor = io.WaitIn.ToColor();
                lblIOWaitOut.BackColor = io.WaitOut.ToColor();

                lblIOLoadPosition1.BackColor = io.GetStageSignalById(1)?.LoadPresence.ToColor() ?? Color.White;
                lblIOLoadPosition2.BackColor = io.GetStageSignalById(2)?.LoadPresence.ToColor() ?? Color.White;
                lblIOLoadPosition3.BackColor = io.GetStageSignalById(3)?.LoadPresence.ToColor() ?? Color.White;
                lblIOLoadPosition4.BackColor = io.GetStageSignalById(4)?.LoadPresence.ToColor() ?? Color.White;
                lblIOLoadPosition5.BackColor = io.GetStageSignalById(5)?.LoadPresence.ToColor() ?? Color.White;

                lblIOFBCRResultCSTID.Text = io.CSTID_BarcodeResultOnP1;

                lblIOLoadPosition1.Text = io.GetStageSignalById(1)?.CSTID ?? string.Empty;
                lblIOLoadPosition2.Text = io.GetStageSignalById(2)?.CSTID ?? string.Empty;
                lblIOLoadPosition3.Text = io.GetStageSignalById(3)?.CSTID ?? string.Empty;
                lblIOLoadPosition4.Text = io.GetStageSignalById(4)?.CSTID ?? string.Empty;
                lblIOLoadPosition5.Text = io.GetStageSignalById(5)?.CSTID ?? string.Empty;

                lblIOPLCAlarmIndex.Text = io.ErrorIndex.GetValue().ToString();
                lblIOAlarmCode.Text = io.ErrorCode.GetValue().ToString("X4");
                lblIOPCAlarmIndex.Text = io.Controller.PcErrorIndex.GetValue().ToString();
                lblIOCSTRemoveCheck_Req.BackColor = io.CSTRemoveCheck_Req.ToColor();

                int intStage = _lcsInfo.Stocker.GetIoInfoByIndex(intIO)?.Stage ?? 1;
                if (intStage >= 2)
                {
                    lblPos2.Visible = true;
                    lblIOLoadPosition2.Visible = true;
                }
                else
                {
                    lblPos2.Visible = false;
                    lblIOLoadPosition2.Visible = false;
                }

                if (intStage >= 3)
                {
                    lblPos3.Visible = true;
                    lblIOLoadPosition3.Visible = true;
                }
                else
                {
                    lblPos3.Visible = false;
                    lblIOLoadPosition3.Visible = false;
                }

                if (intStage >= 4)
                {
                    lblPos4.Visible = true;
                    lblIOLoadPosition4.Visible = true;
                }
                else
                {
                    lblPos4.Visible = false;
                    lblIOLoadPosition4.Visible = false;
                }

                if (intStage >= 5)
                {
                    lblPos5.Visible = true;
                    lblIOLoadPosition5.Visible = true;
                }
                else
                {
                    lblPos5.Visible = false;
                    lblIOLoadPosition5.Visible = false;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private async void butIO_FaultReset_Click(object sender, EventArgs e)
        {
            butIO_FaultReset.Enabled = false;
            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                if (io.Signal.Fault.IsOn())
                {
                    await io.RequestFaultResetAsync();

                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = "butIO_FaultReset_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butIO_FaultReset.Enabled = true;
        }

        private async void butIO_BuzzerStop_Click(object sender, EventArgs e)
        {
            butIO_BuzzerStop.Enabled = false;
            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                await io.RequestBuzzerStopAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIO_BuzzerStop_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIO_BuzzerStop.Enabled = true;
        }

        private async void butIO_PortModeChangeReq_Click(object sender, EventArgs e)
        {
            butIO_PortModeChangeReq.Enabled = false;

            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                if (io.Direction == StockerEnums.IOPortDirection.InMode)
                {
                    if (chkSendRequest2SM.Checked)
                    {
                        //Change to Outmode by Send Req to SM (主動權在Host)

                        TraceLogFormat objLog = new TraceLogFormat();
                        objLog.Message = "butIO_PortModeChangeReq2Out_Click(By MCS)! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                        _loggerService.ShowUI(0, objLog);
                    }
                    else
                    {
                        //Change to OutMode by Write PLC
                        await io.RequestOutModeAsync();

                        TraceLogFormat objLog = new TraceLogFormat();
                        objLog.Message = "butIO_PortModeChangeReq2Out_Click(DirectWritePLC)! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                        _loggerService.ShowUI(0, objLog);
                    }
                }
                else if (io.Direction == StockerEnums.IOPortDirection.OutMode)
                {
                    if (chkSendRequest2SM.Checked)
                    {
                        //Change to InMode by Send Req to SM (主動權在Host)

                        TraceLogFormat objLog = new TraceLogFormat();
                        objLog.Message = "butIO_PortModeChangeReq2In_Click(By MCS)! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                        _loggerService.ShowUI(0, objLog);
                    }
                    else
                    {
                        //Change to InMode by Write PLC
                        await io.RequestInModeAsync();

                        TraceLogFormat objLog = new TraceLogFormat();
                        objLog.Message = "butIO_PortModeChangeReq2In_Click(DirectWritePLC)! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                        _loggerService.ShowUI(0, objLog);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIO_PortModeChangeReq.Enabled = true;
        }

        private async void butIO_EnableDisableP1FBCR_Click(object sender, EventArgs e)
        {
            butIO_EnableDisableP1FBCR.Enabled = false;
            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                if (io.Signal.Controller.BCRDisable_P1.IsOff())
                {
                    //Enable
                    butIO_EnableDisableP1FBCR.Text = "Enable Load Port FBCR";

                    await io.RequestDisableFBCRAsync();
                }
                else
                {
                    //Disable
                    butIO_EnableDisableP1FBCR.Text = "Disable Load Port FBCR";

                    await io.RequestEnableFBCRAsync();
                }

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIO_EnableDisableP1FBCR_Click(" + butIO_EnableDisableP1FBCR.Text + ")! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butIO_EnableDisableP1FBCR.Enabled = true;
        }

        private async void butIO_ManualTriggerFBCRRescan_Click(object sender, EventArgs e)
        {
            butIO_ManualTriggerFBCRRescan.Enabled = false;
            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                await io.RequestBCRReadAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIO_ManualTriggerFBCRRescan_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butIO_ManualTriggerFBCRRescan.Enabled = true;
        }

        private async void butIO_MoveBackForMGV_Click(object sender, EventArgs e)
        {
            butIO_MoveBackForMGV.Enabled = false;
            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                await io.RequestMoveBackMGVAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butIO_MoveBackForMGV_Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butIO_MoveBackForMGV.Enabled = true;
        }

        private void chkSendRequest2SM_CheckedChanged(object sender, EventArgs e)
        {
            TraceLogFormat objLog = new TraceLogFormat();
            objLog.Message = "chkSendRequest2SM_CheckedChanged Checked is " + ((chkSendRequest2SM.Checked) ? "1" : "0");
            _loggerService.ShowUI(0, objLog);
        }

        private async void butIOAutoManual_Click(object sender, EventArgs e)
        {
            butIOAutoManual.Enabled = false;

            string strAddress = string.Empty;
            string strEM = string.Empty;

            try
            {
                var io = _ioPortView.CurrentIOPort;
                if (io == null) return;

                if (io.Signal.Run.IsOn())
                {
                    //Set Manual > Write Stop
                    await io.RequestStopAsync();

                    TraceLogFormat objLog = new TraceLogFormat();
                    objLog.Message = "Manual(Stop) Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                    _loggerService.ShowUI(0, objLog);
                }
                else
                {
                    if (io.Signal.RunEnable.IsOn())
                    {
                        //Set Auto > Write Run
                        await io.RequestRunAsync();

                        TraceLogFormat objLog = new TraceLogFormat();
                        objLog.Message = "Auto(Run) Click! for " + _lcsInfo.Stocker.GetIoInfoByIndex(io.Id).HostEQPortId;
                        _loggerService.ShowUI(0, objLog);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butIOAutoManual.Enabled = true;
        }

        private void IOPortState1View_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }

        private async void butResetControl_Click(object sender, EventArgs e)
        {
            var io = _ioPortView.CurrentIOPort;
            if (io == null) return;
            try
            {
                chkAutoSetRun.Enabled = true;
                await io.ClearControlBitsAsync();
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
