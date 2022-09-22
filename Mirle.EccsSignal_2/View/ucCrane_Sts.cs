using Mirle.EccsSignal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Mirle.Def;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.EccsSignal_2.View
{
    public partial class ucCrane_Sts : UserControl
    {
        private SignalHost signal;
        public ucCrane_Sts(SignalHost signalHost)
        {
            InitializeComponent();
            signal = signalHost;
            lblEquNo.Text = "Crane" + signal.GetEquNo();
        }

        private void ucCrane_Sts_Load(object sender, EventArgs e)
        {
            timMainProc.Enabled = true;
        }

        private void timMainProc_Tick(object sender, EventArgs e)
        {
            timMainProc.Enabled = false;
            try
            {
                lblCrnMode.Text = FunGetCrnModeName(signal.CrnMode);
                lblCrnStatus.Text = FunGetCrnStsName(signal.CrnSts);

                lblCrnMode.BackColor = signal.CrnMode == clsConstValue.Crane.Mode.Computer ? Color.Lime : Color.Red;
                switch(signal.CrnSts)
                {
                    case clsConstValue.Crane.Status.Busy:
                    case clsConstValue.Crane.Status.Idle:
                    case clsConstValue.Crane.Status.PlcCheck:
                        lblCrnStatus.BackColor = Color.Lime;
                        break;
                    default:
                        lblCrnStatus.BackColor = Color.Red;
                        break;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timMainProc.Enabled = true;
            }
        }

        public string FunGetCrnModeName(string sCrnMode)
        {
            string sMode = "";
            switch (sCrnMode)
            {
                case clsConstValue.Crane.Mode.Computer:
                    sMode = $"{sCrnMode}:電腦模式"; break;
                case clsConstValue.Crane.Mode.Manual:
                    sMode = $"{sCrnMode}:地上盤模式"; break;
                case clsConstValue.Crane.Mode.CarMaintain:
                case clsConstValue.Crane.Mode.MasterMaintain:
                    sMode = $"{sCrnMode}:維護模式"; break;
                case clsConstValue.Crane.Mode.ComputerOffLine:
                    sMode = $"{sCrnMode}:未開啟"; break;
                case clsConstValue.Crane.Mode.Error:
                    sMode = $"{sCrnMode}:未聯機"; break;
            }

            return sMode;
        }

        public string FunGetCrnStsName(string sCrnMode)
        {
            string sMode = "";
            switch (sCrnMode)
            {
                case clsConstValue.Crane.Status.Idle: 
                    sMode = $"{sCrnMode}:待機中"; break;
                case clsConstValue.Crane.Status.Busy: 
                    sMode = $"{sCrnMode}:動作中"; break;
                case clsConstValue.Crane.Status.Alarm: 
                    sMode = $"{sCrnMode}:異常中"; break;
                case clsConstValue.Crane.Status.PlcCheck: 
                    sMode = $"{sCrnMode}:檢查中"; break;
                case clsConstValue.Crane.Status.ComputerOffLine:
                    sMode = $"{sCrnMode}:未開啟"; break;
                case clsConstValue.Crane.Status.Error:
                    sMode = $"{sCrnMode}:未聯機"; break;
            }

            return sMode;
        }
    }
}
