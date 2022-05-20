using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneMainView : Form
    {
        private readonly int _craneId;
        private readonly STKCHost _stkcHost;
        private readonly Crane _crane;

        private readonly LoggerService _loggerService;

        private CraneReqAckView _reqAckView;
        private CraneTaskCmdView _taskCmdView;
        private CraneTraceLogView _cmdTraceView;
        private CraneCurrentAlarmView _currentAlarmView;
        private CraneState1View _state1View;
        private CraneState2View _state2View;
        private CraneMotorView _motorView;

        public CraneMainView(Crane crane, STKCHost stkcHost)
        {
            _craneId = crane.Id;
            _stkcHost = stkcHost;
            _loggerService = stkcHost.GetLoggerService();
            _crane = crane;
            InitializeComponent();
        }

        private void CraneMainView_Load(object sender, EventArgs e)
        {
            _reqAckView = new CraneReqAckView(_crane);
            _taskCmdView = new CraneTaskCmdView(_crane, _stkcHost);
            _cmdTraceView = new CraneTraceLogView(_craneId, _stkcHost.GetLoggerService());
            _currentAlarmView = new CraneCurrentAlarmView(_crane, _stkcHost.GetLCSInfo(), _stkcHost.GetAlarmService());
            _state1View = new CraneState1View(_crane);
            _state2View = new CraneState2View(_crane);
            _motorView = new CraneMotorView(_crane.Signal.Motor);

            butRM1_State1.PerformClick();
        }

        private void ChangeSubForm(Form subForm)
        {
            butRM1_ReqAck.BackColor = subForm == _reqAckView ? Color.Aqua : Color.Gainsboro;
            butRM1_CMD.BackColor = subForm == _taskCmdView ? Color.Aqua : Color.Gainsboro;
            butRM1_CMDTrace.BackColor = subForm == _cmdTraceView ? Color.Aqua : Color.Gainsboro;
            butRM1_CurrentAlarm.BackColor = subForm == _currentAlarmView ? Color.Aqua : Color.Gainsboro;
            butRM1_State1.BackColor = subForm == _state1View ? Color.Aqua : Color.Gainsboro;
            butRM1_State2.BackColor = subForm == _state2View ? Color.Aqua : Color.Gainsboro;
            butRM1_Motor.BackColor = subForm == _motorView ? Color.Aqua : Color.Gainsboro;

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

        private void butRM1_ReqAck_Click(object sender, EventArgs e)
        {
            try
            {
                if (_reqAckView == null)
                {
                    _reqAckView = new CraneReqAckView(_crane);
                }

                ChangeSubForm(_reqAckView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_CMD_Click(object sender, EventArgs e)
        {
            try
            {
                if (_taskCmdView == null)
                {
                    _taskCmdView = new CraneTaskCmdView(_crane, _stkcHost);
                }

                ChangeSubForm(_taskCmdView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_CMDTrace_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cmdTraceView == null)
                {
                    _cmdTraceView = new CraneTraceLogView(_craneId, _stkcHost.GetLoggerService());
                }

                ChangeSubForm(_cmdTraceView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_CurrentAlarm_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentAlarmView == null)
                {
                    _currentAlarmView = new CraneCurrentAlarmView(_crane, _stkcHost.GetLCSInfo(), _stkcHost.GetAlarmService());
                }

                ChangeSubForm(_currentAlarmView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_State1_Click(object sender, EventArgs e)
        {
            try
            {
                if (_state1View == null)
                {
                    _state1View = new CraneState1View(_crane);
                }

                ChangeSubForm(_state1View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_State2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_state2View == null)
                {
                    _state2View = new CraneState2View(_crane);
                }

                ChangeSubForm(_state2View);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void butRM1_Motor_Click(object sender, EventArgs e)
        {
            try
            {
                if (_motorView == null)
                {
                    _motorView = new CraneMotorView(_crane.Signal.Motor);
                }

                ChangeSubForm(_motorView);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}