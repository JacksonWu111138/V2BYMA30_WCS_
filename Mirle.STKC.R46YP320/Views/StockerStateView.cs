using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mirle.STKC.R46YP320.Model.Define;
using Mirle.STKC.R46YP320.Model;
using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System.Windows.Forms;
using Mirle.STKC.R46YP320.LCSShareMemory;
using Mirle.Def;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class StockerStateView : Form
    {
        private STKCHost _stkcHost;
        private LCSInfo _lcsInfo;
        private StockerController _stockerController;
        private LoggerService _loggerService;

        public StockerStateView(STKCHost stkcHost)
        {
            InitializeComponent();

            _stkcHost = stkcHost;
            _lcsInfo = _stkcHost.GetLCSInfo();
            _stockerController = _stkcHost.GetSTKCManager().GetStockerController();
            _loggerService = _stkcHost.GetLoggerService();
        }

        private void StockerStateView_Load(object sender, EventArgs e)
        {
            //Initial UI
            lblStockerID.Text = _lcsInfo.Stocker.StockerId;
            timMainProc.Enabled = true;
        }

        private void timMainProc_Tick(object sender, EventArgs e)
        {
            timMainProc.Enabled = false;
            try
            {
                var stocker = _stockerController.GetStocker();
                var crane = stocker.GetCraneById(1) as Crane;
                //Check PLC
                if (_stockerController.PLCIsConnected)
                {
                    if(crane.IsInService)
                    {
                        if(crane.Signal.Run.IsOn())
                        {
                            lblMode.Text = "C: Run"; lblMode.BackColor = Color.Lime;
                        }
                        else
                        {
                            lblMode.Text = "R: Not Run"; lblMode.BackColor = Color.Yellow;
                        }

                        if(crane.IsAlarm)
                        {
                            lblSts.Text = "E: Error"; lblSts.BackColor = Color.Red;
                        }
                        else if(crane.IsIdle)
                        {
                            lblSts.Text = "W: Idle"; lblSts.BackColor = Color.Lime;
                        }
                        else if(crane.Signal.Active.IsOn())
                        {
                            lblSts.Text = "A: Busy"; lblSts.BackColor = Color.Lime;
                        }
                        else
                        {
                            lblSts.Text = "X: No Status"; lblSts.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        lblMode.Text = "M: Maintenance"; lblSts.Text = "M: Maintenance";
                        lblMode.BackColor = lblSts.BackColor = Color.Red;
                    }
                }
                else
                {
                    lblMode.Text = lblSts.Text = "X: No Connect";
                    lblMode.BackColor = lblSts.BackColor = Color.Red;
                }

                for (int fork = 1; fork < 3; fork++)
                {
                    Label LabelControl = Controls.Find("lblFork" + fork, true).FirstOrDefault() as Label;
                    if(crane.GetForkById(fork).GetConfig().Enable)
                    {
                        LabelControl.Text = "Enable"; LabelControl.BackColor = Color.Lime;
                    }
                    else
                    {
                        LabelControl.Text = "Disable"; LabelControl.BackColor = Color.Red;
                    }
                }
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

        private void lblFork1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var stocker = _stockerController.GetStocker();
                var crane = stocker.GetCraneById(1) as Crane;
                if(crane.GetForkById(1).GetConfig().Enable)
                {
                    _loggerService.Trace(1, "已手動禁用左Fork！");
                    crane.GetForkById(1).GetConfig().Enable = false;
                }
                else
                {
                    _loggerService.Trace(1, "已手動啟用左Fork！");
                    crane.GetForkById(1).GetConfig().Enable = true;
                }

                string final = "";
                for (int i = 1; i < 3; i++)
                {
                    string value = crane.GetForkById(i).GetConfig().Enable ? "1" : "0";
                    if (i == 1)
                    {
                        final = value;
                    }
                    else
                    {
                        final += "," + value;
                    }
                }

                clsTool.funWriteValue("Fork Setup", $"S{_lcsInfo.Stocker.StockerId}", final);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private void lblFork2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var stocker = _stockerController.GetStocker();
                var crane = stocker.GetCraneById(1) as Crane;
                if (crane.GetForkById(2).GetConfig().Enable)
                {
                    _loggerService.Trace(1, "已手動禁用右Fork！");
                    crane.GetForkById(2).GetConfig().Enable = false;
                }
                else
                {
                    _loggerService.Trace(1, "已手動啟用右Fork！");
                    crane.GetForkById(2).GetConfig().Enable = true;
                }

                string final = "";
                for (int i = 1; i < 3; i++)
                {
                    string value = crane.GetForkById(i).GetConfig().Enable ? "1" : "0";
                    if (i == 1)
                    {
                        final = value;
                    }
                    else
                    {
                        final += "," + value;
                    }
                }

                clsTool.funWriteValue("Fork Setup", $"S{_lcsInfo.Stocker.StockerId}", final);
            }
            catch (Exception ex)
            {
                _loggerService.Error(System.Reflection.MethodBase.GetCurrentMethod().Name, $"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
