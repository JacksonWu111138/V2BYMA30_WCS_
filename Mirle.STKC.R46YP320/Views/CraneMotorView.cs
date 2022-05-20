using System;
using System.Windows.Forms;
using Mirle.Stocker.R46YP320.Signal;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneMotorView : Form
    {
        private readonly CraneMotorSignal _motorSignal;

        public CraneMotorView(CraneMotorSignal motorSignal)
        {
            _motorSignal = motorSignal;
            InitializeComponent();
        }

        private void CraneMotorView_Load(object sender, EventArgs e)
        {
        }

        private void CraneMotorView_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = this.Visible;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;

            lblMotor01.Text = (_motorSignal.Motor_01_Travel1.GetValue() / 10.0).ToString("F1");
            lblMotor02.Text = (_motorSignal.Motor_02_Travel2.GetValue() / 10.0).ToString("F1");
            lblMotor03.Text = (_motorSignal.Motor_03_Travel3.GetValue() / 10.0).ToString("F1");
            lblMotor04.Text = (_motorSignal.Motor_04_Travel4.GetValue() / 10.0).ToString("F1");
            lblMotor05.Text = (_motorSignal.Motor_05_Lifter1.GetValue() / 10.0).ToString("F1");
            lblMotor06.Text = (_motorSignal.Motor_06_Lifter2.GetValue() / 10.0).ToString("F1");
            lblMotor07.Text = (_motorSignal.Motor_07_Lifter3.GetValue() / 10.0).ToString("F1");
            lblMotor08.Text = (_motorSignal.Motor_08_Lifter4.GetValue() / 10.0).ToString("F1");
            lblMotor09.Text = (_motorSignal.Motor_09_Rotate1.GetValue() / 10.0).ToString("F1");
            lblMotor10.Text = (_motorSignal.Motor_10_Fork1.GetValue() / 10.0).ToString("F1");
            lblMotor11.Text = (_motorSignal.Motor_11_Fork2.GetValue() / 10.0).ToString("F1");
        }
    }
}