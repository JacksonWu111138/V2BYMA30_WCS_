using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Mirle.STKC.R46YP320.LCSShareMemory;
using Mirle.Extensions;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class MainView : Form
    {
        private STKCHost _stkcHost;
        private LCSInfo _lcsInfo;
        private StockerController _stockerController;
        private LCSParameter _lcsParameter;
        private AlarmService _alarmService;
        private LoggerService _loggerService;

        //UI
        private ModeChangeView _modeChangeView;

        private SCCommandView _scCommandView;
        private SystraceView _systraceView;
        private CraneMainView _crane1View;
        private CraneMainView _crane2View;
        private HandoffAreaSetView _handoffAreaSetView;
        private IOPortView _ioPortView;
        private EQPortView _eqPortView;

        public MainView(STKCHost stkcHost)
        {
            InitializeComponent();

            _stkcHost = stkcHost;
            _lcsInfo = _stkcHost.GetLCSInfo();
            _stockerController = _stkcHost.GetSTKCManager().GetStockerController();
            _lcsParameter = _stkcHost.GetLCSParameter();
            _alarmService = _stkcHost.GetAlarmService();
            _loggerService = _stkcHost.GetLoggerService();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            try
            {
                //Initial MPLC_C
                //var strEM = string.Empty;
                //Mirle.LCS.MPLC_TF.STKCConfig gMPLC_Config = new Mirle.LCS.MPLC_TF.STKCConfig();
                //gMPLC_Config.STK_ID = _lcsInfo.LCSHostId;
                //gMPLC_Config.RM_QTY = _lcsInfo.Stocker.CraneCount;
                //gMPLC_Config.EQPort_QTY = _lcsInfo.Stocker.EqInfos.Count();
                //gMPLC_Config.IOPort_QTY = _lcsInfo.Stocker.IoInfos.Count();
                //var MPLC_TF1 = new MPLC_TF();
                //MPLC_TF1.subInitial(gMPLC_Config, "STKC", false, ref strEM);
                //MPLC_TF1.subStart(new string[0]);

                //Initial UI
                lblSTKID.Text = _lcsInfo.Stocker.StockerId;
                //Initial Layout by ControlMode
                subInitialLayout();

                lblNowDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                this.Text = "Mirle Stocker Controller (V." + Application.ProductVersion + ")";

                _scCommandView = new SCCommandView(_loggerService, _stkcHost.GetTaskCommandService());
                _systraceView = new SystraceView(_loggerService);

                var t2Stocker = _stockerController.GetStocker();
                _modeChangeView = new ModeChangeView(_lcsInfo, _stockerController, _lcsParameter, _loggerService);
                _crane1View = new CraneMainView(t2Stocker.GetCraneById(1) as Crane, _stkcHost);
                _crane2View = new CraneMainView(t2Stocker.GetCraneById(2) as Crane, _stkcHost);
                _handoffAreaSetView = new HandoffAreaSetView(t2Stocker, _stkcHost, _loggerService);
                _ioPortView = new IOPortView(t2Stocker, _stkcHost);
                _eqPortView = new EQPortView(_lcsInfo.Stocker.EqInfos, t2Stocker);               

                butMain_ModeChange.PerformClick();

                //Start Timer
                timMainProc.Interval = 300;
                timMainProc.Enabled = true;
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                MessageBox.Show("Initial STKC Fail!!!");
                this.Close();
                this.Dispose();
            }
        }

        private void subInitialLayout()
        {
            butMain_ModeChange.BackColor = Color.Aqua;

            //Enable IOPort
            butMain_IOPort.Enabled = _lcsInfo.Stocker.IoInfos.Any();
            //Enable EQPort
            butMain_EQPort.Enabled = _lcsInfo.Stocker.EqInfos.Any();

            switch (_lcsInfo.Stocker.ControlMode)
            {
                case LCSEnums.ControlMode.TwinFork:
                case LCSEnums.ControlMode.Single:               //Single

                    lblRM2Sts.Visible = false;

                    tlpStatus.ColumnStyles[5].SizeType = SizeType.Percent;
                    tlpStatus.ColumnStyles[5].Width = 0;

                    //Disable Crane2
                    tlpMainPage.ColumnStyles[4].SizeType = SizeType.Percent;
                    tlpMainPage.ColumnStyles[4].Width = 0;
                    //Disable HasndoffAreaSet
                    tlpMainPage.ColumnStyles[5].SizeType = SizeType.Percent;
                    tlpMainPage.ColumnStyles[5].Width = 0;

                    butRM1Function.Visible = true;
                    butRM2Function.Visible = false;
                    tlpCrane1Controller.Visible = true;
                    tlpCrane2Controller.Visible = false;

                    butRM1CMD.Visible = true;
                    butRM2CMD.Visible = false;
                    tlpRM1Command.Visible = true;
                    tlpRM2Command.Visible = false;

                    butRM1Function.Visible = true;
                    tlpCrane1Controller.Visible = true;
                    tlpCrane1Controller.Dock = DockStyle.Fill;

                    butRM1CMD.Visible = true;
                    tlpRM1Command.Visible = true;
                    tlpRM1Command.Dock = DockStyle.Fill;

                    break;

                case LCSEnums.ControlMode.Dual:                 //Dual (By Share-Area and Handoff-Area)
                case LCSEnums.ControlMode.DoubleSingle:         //Double Single (By OHCV)
                case LCSEnums.ControlMode.TwinForkDual:
                default:

                    butRM1Function.Visible = true;
                    butRM2Function.Visible = true;
                    tlpCrane1Controller.Visible = true;
                    tlpCrane2Controller.Visible = true;
                    tlpCrane1Controller.Dock = DockStyle.Fill;
                    tlpCrane2Controller.Dock = DockStyle.None; tlpCrane2Controller.Width = 0;

                    butRM1CMD.Visible = true;
                    butRM2CMD.Visible = true;
                    tlpRM1Command.Visible = true;
                    tlpRM2Command.Visible = true;
                    tlpRM1Command.Dock = DockStyle.Fill;
                    tlpRM2Command.Dock = DockStyle.None; tlpRM2Command.Width = 0;

                    break;
            }

            butRM1_Run.Enabled = false;
            butRM2_Run.Enabled = false;

            butRM1_Stop.Enabled = false;
            butRM2_Stop.Enabled = false;
            butRM1_Abort.Enabled = false;
            butRM2_Abort.Enabled = false;

            butMain_ModeChange.BackColor = Color.Aqua;
            butMain_SCCMD.BackColor = Color.Gainsboro;
            butMain_SysTrace.BackColor = Color.Gainsboro;
            butMain_Crane1.BackColor = Color.Gainsboro;
            butMain_Crane2.BackColor = Color.Gainsboro;
            butMain_HandOffAreaSet.BackColor = Color.Gainsboro;
            butMain_IOPort.BackColor = Color.Gainsboro;
            butMain_EQPort.BackColor = Color.Gainsboro;
        }

        #region Timer

        private void timMainProc_Tick(object sender, EventArgs e)
        {
            string strEM = string.Empty;

            try
            {
                lblNowDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                timMainProc.Enabled = false;

                //Check DB
                if (_stkcHost.IsDBConnected())
                {
                    lblDBConnSts.BackColor = Color.Lime;
                }
                else
                {
                    lblDBConnSts.BackColor = Color.Red;
                }

                //Check PLC
                if (_stockerController.PLCIsConnected)
                {
                    lblPLCConnSts.BackColor = Color.Lime;
                }
                else
                {
                    lblPLCConnSts.BackColor = Color.Red;
                }             

                //Reflash Form
                subShowProc(ref strEM);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                timMainProc.Enabled = true;
            }
        }

        #endregion Timer

        private void subShowProc(ref string strEM)
        {
            int intTT = DateTime.Now.Millisecond;

            int intRMIndex = 0;

            try
            {
                var stocker = _stockerController.GetStocker();
                var crane1 = stocker.GetCraneById(1) as Crane;
                var crane2 = stocker.GetCraneById(2) as Crane;

                lblSTKStatus.Text = stocker.AvailStatus.ToString();
                lblSTKStatus.BackColor = stocker.AvailStatus == LCSEnums.AvailStatus.Avail ? Color.Lime : Color.Red;

                switch (crane1.Status)
                {
                    case StockerEnums.CraneStatus.BUSY:
                        lblRM1Sts.Text = StatusString.BUSY;
                        lblRM1Sts.BackColor = Color.Yellow;
                        break;

                    case StockerEnums.CraneStatus.HOMEACTION:
                        lblRM1Sts.Text = StatusString.HOMEACTION;
                        lblRM1Sts.BackColor = Color.SkyBlue;
                        break;

                    case StockerEnums.CraneStatus.IDLE:
                        lblRM1Sts.Text = StatusString.IDLE;
                        lblRM1Sts.BackColor = Color.Lime;
                        break;

                    case StockerEnums.CraneStatus.MAINTAIN:
                        lblRM1Sts.Text = StatusString.MAINTEN;
                        lblRM1Sts.BackColor = Color.Orange;
                        break;

                    case StockerEnums.CraneStatus.STOP:
                        lblRM1Sts.Text = StatusString.STOP;
                        lblRM1Sts.BackColor = Color.Red;
                        break;

                    case StockerEnums.CraneStatus.WAITINGHOMEACTION:
                        lblRM1Sts.Text = StatusString.WAITHOME;
                        lblRM1Sts.BackColor = Color.Fuchsia;
                        break;

                    case StockerEnums.CraneStatus.ESCAPE:
                        lblRM1Sts.Text = StatusString.ESCAPE;
                        lblRM1Sts.BackColor = Color.DeepSkyBlue;
                        break;

                    case StockerEnums.CraneStatus.Waiting:
                        lblRM1Sts.Text = StatusString.BUSY;
                        lblRM1Sts.BackColor = Color.DarkSeaGreen;
                        break;

                    default:
                        lblRM1Sts.Text = StatusString.None;
                        lblRM1Sts.BackColor = Color.Red;
                        break;
                }

                if (lblRM2Sts.Visible)
                {
                    switch (crane2.Status)
                    {
                        case StockerEnums.CraneStatus.BUSY:
                            lblRM2Sts.Text = StatusString.BUSY;
                            lblRM2Sts.BackColor = Color.Yellow;
                            break;

                        case StockerEnums.CraneStatus.HOMEACTION:
                            lblRM2Sts.Text = StatusString.HOMEACTION;
                            lblRM2Sts.BackColor = Color.SkyBlue;
                            break;

                        case StockerEnums.CraneStatus.IDLE:
                            lblRM2Sts.Text = StatusString.IDLE;
                            lblRM2Sts.BackColor = Color.Lime;
                            break;

                        case StockerEnums.CraneStatus.MAINTAIN:
                            lblRM2Sts.Text = StatusString.MAINTEN;
                            lblRM2Sts.BackColor = Color.Orange;
                            break;

                        case StockerEnums.CraneStatus.STOP:
                            lblRM2Sts.Text = StatusString.STOP;
                            lblRM2Sts.BackColor = Color.Red;
                            break;

                        case StockerEnums.CraneStatus.WAITINGHOMEACTION:
                            lblRM2Sts.Text = StatusString.WAITHOME;
                            lblRM2Sts.BackColor = Color.Fuchsia;
                            break;

                        case StockerEnums.CraneStatus.ESCAPE:
                            lblRM2Sts.Text = StatusString.ESCAPE;
                            lblRM2Sts.BackColor = Color.DeepSkyBlue;
                            break;

                        case StockerEnums.CraneStatus.Waiting:
                            lblRM2Sts.Text = StatusString.BUSY;
                            lblRM2Sts.BackColor = Color.DarkSeaGreen;
                            break;

                        default:
                            lblRM2Sts.Text = StatusString.None;
                            lblRM2Sts.BackColor = Color.Red;
                            break;
                    }
                }

                butRM1_Run.BackColor = crane1.Signal.Controller.Run.ToButtonColor();

                butRM1_Run.Enabled = crane1.Signal.RunEnable.IsOn();

                butRM1_Stop.BackColor = crane1.Signal.Controller.Stop.ToButtonColor();

                butRM1_Stop.Enabled = crane1.Signal.Run.IsOn();

                butRM1_Abort.BackColor = crane1.Signal.Controller.CommandAbort.ToButtonColor();
                butRM1_HPReturn.BackColor = crane1.Signal.Controller.HomeReturn.ToButtonColor();
                butRM1_BuzzerStop.BackColor = crane1.Signal.Controller.BuzzerStop.ToButtonColor();
                butRM1_FaultReset.BackColor = crane1.Signal.Controller.FaultReset.ToButtonColor();

                lblRM1_CSTOnCrane_LF.BackColor = (crane1.GetLeftFork() as Fork).Signal.CSTPresence.ToButtonColor();
                lblRM1_CSTOnCrane_RF.BackColor = (crane1.GetRightFork() as Fork).Signal.CSTPresence.ToButtonColor();

                if (tlpCrane2Controller.Visible)
                {
                    //Crane 2
                    butRM2_Run.BackColor = crane2.Signal.Controller.Run.ToButtonColor();

                    butRM2_Run.Enabled = crane2.Signal.RunEnable.IsOn();

                    butRM2_Stop.BackColor = crane2.Signal.Controller.Stop.ToButtonColor();

                    butRM2_Stop.Enabled = crane2.Signal.Run.IsOn();

                    butRM2_Abort.BackColor = crane2.Signal.Controller.CommandAbort.ToButtonColor();
                    butRM2_HPReturn.BackColor = crane2.Signal.Controller.HomeReturn.ToButtonColor();
                    butRM2_BuzzerStop.BackColor = crane2.Signal.Controller.BuzzerStop.ToButtonColor();
                    butRM2_FaultReset.BackColor = crane2.Signal.Controller.FaultReset.ToButtonColor();

                    lblRM2_CSTOnCrane_LF.BackColor = (crane2.GetLeftFork() as Fork).Signal.CSTPresence.ToButtonColor();
                    lblRM2_CSTOnCrane_RF.BackColor = (crane2.GetRightFork() as Fork).Signal.CSTPresence.ToButtonColor();
                }

                //Crane 1
                lblRM1_CMDRequest1.BackColor = crane1.Signal.Controller.CmdType_TransferWithoutIDRead.ToCmdButtonColor();
                lblRM1_CMDRequest2.BackColor = crane1.Signal.Controller.CmdType_Transfer.ToCmdButtonColor();
                lblRM1_CMDRequest3.BackColor = crane1.Signal.Controller.CmdType_Move.ToCmdButtonColor();
                lblRM1_CMDRequest4.BackColor = crane1.Signal.Controller.CmdType_Scan.ToCmdButtonColor();
                lblRM1_TaskNo.Text = crane1.Signal.Controller.TransferNo.GetValue().ToString("D5");
                lblRM1_Source.Text = crane1.Signal.Controller.FromLocation.GetValue().ToString("D5");
                lblRM1_Destination.Text = crane1.Signal.Controller.ToLocation.GetValue().ToString("D5");
                lblRM1_CSTID.Text = crane1.Signal.Controller.CmdCstId.GetData().ToASCII();
                lblRM1_ForkNumber.Text = crane1.Signal.Controller.UseLeftFork.IsOn() ? "1-Left" : (crane1.Signal.Controller.UseRightFork.IsOn() ? "2-Right" : "0-None");

                lblRM1_Speed.Text = $"{crane1.Signal.TravelAxisSpeed.GetValue()}-{crane1.Signal.LifterAxisSpeed.GetValue()}-{crane1.Signal.RotateAxisSpeed.GetValue()}-{crane1.Signal.ForkAxisSpeed.GetValue()}";

                if (tlpRM2Command.Visible)
                {
                    //Crane 2
                    lblRM2_CMDRequest1.BackColor = crane2.Signal.Controller.CmdType_TransferWithoutIDRead.ToCmdButtonColor();
                    lblRM2_CMDRequest2.BackColor = crane2.Signal.Controller.CmdType_Transfer.ToCmdButtonColor();
                    lblRM2_CMDRequest3.BackColor = crane2.Signal.Controller.CmdType_Move.ToCmdButtonColor();
                    lblRM2_CMDRequest4.BackColor = crane2.Signal.Controller.CmdType_Scan.ToCmdButtonColor();
                    lblRM2_TaskNo.Text = crane2.Signal.Controller.TransferNo.GetValue().ToString("D5");
                    lblRM2_Source.Text = crane2.Signal.Controller.FromLocation.GetValue().ToString("D5");
                    lblRM2_Destination.Text = crane2.Signal.Controller.ToLocation.GetValue().ToString("D5");
                    lblRM2_CSTID.Text = crane2.Signal.Controller.CmdCstId.GetData().ToASCII();

                    lblRM2_ForkNumber.Text = crane2.Signal.Controller.UseLeftFork.IsOn() ? "1-Left" : (crane2.Signal.Controller.UseRightFork.IsOn() ? "2-Right" : "0-None");

                    lblRM2_Speed.Text = $"{crane2.Signal.TravelAxisSpeed.GetValue()}-{crane2.Signal.LifterAxisSpeed.GetValue()}-{crane2.Signal.RotateAxisSpeed.GetValue()}-{crane2.Signal.ForkAxisSpeed.GetValue()}";
                }

                if (_lcsParameter.SCState_Cur == LCSParameter.SCState.Auto)
                {
                    lblSCSts.Text = "Auto";
                    lblSCSts.BackColor = Color.Lime;
                }
                else if (_lcsParameter.SCState_Cur == LCSParameter.SCState.Pausing)
                {
                    lblSCSts.Text = "Pausing";
                    lblSCSts.BackColor = Color.Yellow;
                }
                else if (_lcsParameter.SCState_Cur == LCSParameter.SCState.Paused)
                {
                    lblSCSts.Text = "Pause";
                    lblSCSts.BackColor = Color.Yellow;
                }
                else
                {
                    lblSCSts.Text = "None";
                    lblSCSts.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        #region Controller

        private async void butRM1_Run_Click(object sender, EventArgs e)
        {
            butRM1_Run.Enabled = false;
            try
            {
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_Run_Click";
                _loggerService.ShowUI(0, objLog);

                string strEM = string.Empty;

                var stocker = _stockerController.GetStocker();
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                if (stocker.KeySwitchIsAuto)
                {
                    await crane1.RequestRunAsync();
                }
                else
                {
                    objLog.Message = "butRM1_Run_Click-Fail(MPLCKeyHP+MPLCKeyOP+RunEnable+Run/SetRun+SetStop->" +
                                     (stocker.Signal.KeySwitch_HP.IsOn() ? "1" : "0") +
                                     (stocker.Signal.KeySwitch_OP.IsOn() ? "1" : "0") +
                                     (crane1.Signal.RunEnable.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Run.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Controller.Run.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Controller.Stop.IsOn() ? "1" : "0") + ")";
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM1_Run.Enabled = true;
        }

        private async void butRM2_Run_Click(object sender, EventArgs e)
        {
            butRM2_Run.Enabled = false;
            try
            {
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_Run_Click";
                _loggerService.ShowUI(0, objLog);

                string strEM = string.Empty;

                var stocker = _stockerController.GetStocker();
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                if (stocker.KeySwitchIsAuto)
                {
                    await crane2.RequestRunAsync();
                }
                else
                {
                    objLog.Message = "butRM2_Run_Click-Fail(MPLCKeyHP+MPLCKeyOP+RunEnable+Run/SetRun+SetStop->" +
                                     (stocker.Signal.KeySwitch_HP.IsOn() ? "1" : "0") +
                                     (stocker.Signal.KeySwitch_OP.IsOn() ? "1" : "0") +
                                     (crane2.Signal.RunEnable.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Run.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Controller.Run.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Controller.Stop.IsOn() ? "1" : "0") + ")";
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butRM2_Run.Enabled = true;
        }

        private async void butRM1_Stop_Click(object sender, EventArgs e)
        {
            butRM1_Stop.Enabled = false;
            try
            {
                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_Stop_Click";
                _loggerService.ShowUI(0, objLog);

                string strEM = string.Empty;

                var stocker = _stockerController.GetStocker();
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                if (stocker.KeySwitchIsAuto)
                {
                    await crane1.RequestStopAsync();
                }
                else
                {
                    objLog.Message = "butRM1_Stop_Click-Fail(MPLCKeyHP+MPLCKeyOP+RunEnable+Run/SetRun+SetStop->" +
                                     (stocker.Signal.KeySwitch_HP.IsOn() ? "1" : "0") +
                                     (stocker.Signal.KeySwitch_OP.IsOn() ? "1" : "0") +
                                     (crane1.Signal.RunEnable.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Run.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Controller.Run.IsOn() ? "1" : "0") +
                                     (crane1.Signal.Controller.Stop.IsOn() ? "1" : "0") + ")";
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM1_Stop.Enabled = true;
        }

        private async void butRM2_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                butRM2_Stop.Enabled = false;

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_Stop_Click";
                _loggerService.ShowUI(0, objLog);

                string strEM = string.Empty;

                var stocker = _stockerController.GetStocker();
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                if (stocker.KeySwitchIsAuto)
                {
                    await crane2.RequestStopAsync();
                }
                else
                {
                    objLog.Message = "butRM2_Stop_Click-Fail(MPLCKeyHP+MPLCKeyOP+RunEnable+Run/SetRun+SetStop->" +
                                     (stocker.Signal.KeySwitch_HP.IsOn() ? "1" : "0") +
                                     (stocker.Signal.KeySwitch_OP.IsOn() ? "1" : "0") +
                                     (crane2.Signal.RunEnable.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Run.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Controller.Run.IsOn() ? "1" : "0") +
                                     (crane2.Signal.Controller.Stop.IsOn() ? "1" : "0") + ")";
                    _loggerService.ShowUI(0, objLog);
                }
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                butRM2_Stop.Enabled = true;
            }
        }

        private async void butRM1_BuzzerStop_Click(object sender, EventArgs e)
        {
            butRM1_BuzzerStop.Enabled = false;

            try
            {
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                await crane1.RequestBuzzzerStopAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_BuzzerStop_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM1_BuzzerStop.Enabled = true;
        }

        private async void butRM2_BuzzerStop_Click(object sender, EventArgs e)
        {
            butRM2_BuzzerStop.Enabled = false;

            try
            {
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                await crane2.RequestBuzzzerStopAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_BuzzerStop_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM2_BuzzerStop.Enabled = true;
        }

        private async void butRM1_FaultReset_Click(object sender, EventArgs e)
        {
            butRM1_FaultReset.Enabled = false;

            try
            {
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                await crane1.RequestFaultResetAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_FaultReset_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
            butRM1_FaultReset.Enabled = true;
        }

        private async void butRM2_FaultReset_Click(object sender, EventArgs e)
        {
            butRM2_FaultReset.Enabled = false;

            try
            {
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                await crane2.RequestFaultResetAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_FaultReset_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM2_FaultReset.Enabled = true;
        }

        private async void butRM1_HPReturn_Click(object sender, EventArgs e)
        {
            butRM1_HPReturn.Enabled = false;

            try
            {
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                await crane1.RequestReturnHomeAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_HPReturn_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM1_HPReturn.Enabled = true;
        }

        private async void butRM2_HPReturn_Click(object sender, EventArgs e)
        {
            butRM2_HPReturn.Enabled = false;

            try
            {
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                await crane2.RequestReturnHomeAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_HPReturn_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM2_HPReturn.Enabled = true;
        }

        private async void butRM1_Abort_Click(object sender, EventArgs e)
        {
            butRM1_Abort.Enabled = false;

            try
            {
                var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;

                await crane1.RequestCommandAbortAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM1_Abort_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM1_Abort.Enabled = true;
        }

        private async void butRM2_Abort_Click(object sender, EventArgs e)
        {
            butRM2_Abort.Enabled = false;

            try
            {
                var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;

                await crane2.RequestCommandAbortAsync();

                TraceLogFormat objLog = new TraceLogFormat();
                objLog.Message = "butRM2_Abort_Click";
                _loggerService.ShowUI(0, objLog);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }

            butRM2_Abort.Enabled = true;
        }

        #endregion Controller

        private void butRM2Function_Click(object sender, EventArgs e)
        {
            tlpCrane1Controller.Dock = DockStyle.None; tlpCrane1Controller.Width = 0;
            tlpCrane2Controller.Dock = DockStyle.Fill;

            butRM1Function.BackColor = Color.Gainsboro;
            butRM2Function.BackColor = Color.Aqua;
        }

        private void butRM1Function_Click(object sender, EventArgs e)
        {
            tlpCrane1Controller.Dock = DockStyle.Fill;
            tlpCrane2Controller.Dock = DockStyle.None; tlpCrane2Controller.Width = 0;

            butRM1Function.BackColor = Color.Aqua;
            butRM2Function.BackColor = Color.Gainsboro;
        }

        private void butRM1CMD_Click(object sender, EventArgs e)
        {
            tlpRM1Command.Dock = DockStyle.Fill;
            tlpRM2Command.Dock = DockStyle.None; tlpRM2Command.Width = 0;

            butRM1CMD.BackColor = Color.Aqua;
            butRM2CMD.BackColor = Color.Gainsboro;
        }

        private async void butRM1CMD_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                butRM1CMD.Enabled = false;

                try
                {
                    var crane1 = _stockerController.GetStocker().GetCraneById(1) as Crane;
                    //Clear PLC Command Data
                    await crane1.ClearCommandWriteZoneAsync();
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }

                butRM1CMD.Enabled = true;
            }
        }

        private void butRM2CMD_Click(object sender, EventArgs e)
        {
            tlpRM1Command.Dock = DockStyle.None; tlpRM1Command.Width = 0;
            tlpRM2Command.Dock = DockStyle.Fill;

            butRM1CMD.BackColor = Color.Gainsboro;
            butRM2CMD.BackColor = Color.Aqua;
        }

        private async void butRM2CMD_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                butRM2CMD.Enabled = false;

                try
                {
                    var crane2 = _stockerController.GetStocker().GetCraneById(2) as Crane;
                    //Clear PLC Command Data
                    await crane2.ClearCommandWriteZoneAsync();
                }
                catch (Exception ex)
                {
                    _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
                }

                butRM2CMD.Enabled = true;
            }
        }

        private void ChangeSubForm(Form subForm)
        {
            butMain_ModeChange.BackColor = subForm == _modeChangeView ? Color.Aqua : Color.Gainsboro;
            butMain_SCCMD.BackColor = subForm == _scCommandView ? Color.Aqua : Color.Gainsboro;
            butMain_SysTrace.BackColor = subForm == _systraceView ? Color.Aqua : Color.Gainsboro;
            butMain_Crane1.BackColor = subForm == _crane1View ? Color.Aqua : Color.Gainsboro;
            butMain_Crane2.BackColor = subForm == _crane2View ? Color.Aqua : Color.Gainsboro;
            butMain_HandOffAreaSet.BackColor = subForm == _handoffAreaSetView ? Color.Aqua : Color.Gainsboro;
            butMain_IOPort.BackColor = subForm == _ioPortView ? Color.Aqua : Color.Gainsboro;
            butMain_EQPort.BackColor = subForm == _eqPortView ? Color.Aqua : Color.Gainsboro;

            var children = spcMain.Panel2.Controls;
            foreach (Control c in children)
            {
                if (c is Form)
                {
                    var thisChild = c as Form;
                    //thisChild.Hide();
                    this.spcMain.Panel2.Controls.Remove(thisChild);
                    thisChild.Width = 0;
                }
            }

            Form newChild = subForm;

            if (newChild != null)
            {
                newChild.TopLevel = false;
                newChild.Dock = System.Windows.Forms.DockStyle.Fill;//適應窗體大小
                newChild.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                newChild.Parent = this.spcMain.Panel2;
                this.spcMain.Panel2.Controls.Add(newChild);
                newChild.Show();
            }
        }

        private void butMain_ModeChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (_modeChangeView == null)
                {
                    _modeChangeView = new ModeChangeView(_lcsInfo, _stockerController, _lcsParameter, _loggerService);
                }

                ChangeSubForm(_modeChangeView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_SCCMD_Click(object sender, EventArgs e)
        {
            try
            {
                if (_scCommandView == null)
                {
                    _scCommandView = new SCCommandView(_loggerService, _stkcHost.GetTaskCommandService());
                }

                ChangeSubForm(_scCommandView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_SysTrace_Click(object sender, EventArgs e)
        {
            try
            {
                if (_systraceView == null)
                {
                    _systraceView = new SystraceView(_loggerService);
                }
                ChangeSubForm(_systraceView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_Crane1_Click(object sender, EventArgs e)
        {
            try
            {
                var t2Stocker = _stockerController.GetStocker();
                if (_crane1View == null)
                {
                    _crane1View = new CraneMainView(t2Stocker.GetCraneById(1) as Crane, _stkcHost);
                }

                ChangeSubForm(_crane1View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_Crane2_Click(object sender, EventArgs e)
        {
            try
            {
                var t2Stocker = _stockerController.GetStocker();
                if (_crane2View == null)
                {
                    _crane2View = new CraneMainView(t2Stocker.GetCraneById(2) as Crane, _stkcHost);
                }

                ChangeSubForm(_crane2View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_HandOffAreaSet_Click(object sender, EventArgs e)
        {
            try
            {
                var t2Stocker = _stockerController.GetStocker();
                if (_handoffAreaSetView == null)
                {
                    _handoffAreaSetView = new HandoffAreaSetView(t2Stocker, _stkcHost, _loggerService);
                }

                ChangeSubForm(_handoffAreaSetView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_IOPort_Click(object sender, EventArgs e)
        {
            try
            {
                var t2Stocker = _stockerController.GetStocker();
                if (_ioPortView == null)
                {
                    _ioPortView = new IOPortView(t2Stocker, _stkcHost);
                }

                ChangeSubForm(_ioPortView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butMain_EQPort_Click(object sender, EventArgs e)
        {
            try
            {
                var t2Stocker = _stockerController.GetStocker();
                if (_eqPortView == null)
                {
                    _eqPortView = new EQPortView(_lcsInfo.Stocker.EqInfos, t2Stocker);
                }

                ChangeSubForm(_eqPortView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
            }
        }
    }
}
