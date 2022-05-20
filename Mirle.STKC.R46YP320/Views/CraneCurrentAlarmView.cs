using Mirle.STKC.R46YP320.Service;
using Mirle.Stocker.R46YP320;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320.Views
{
    public partial class CraneCurrentAlarmView : Form
    {
        private readonly int _craneId;
        private readonly LCSInfo _lcsInfo;
        private readonly AlarmService _alarmService;

        public CraneCurrentAlarmView(Crane crane, LCSInfo lcsInfo, AlarmService alarmService)
        {
            _craneId = crane.Id;
            _lcsInfo = lcsInfo;
            _alarmService = alarmService;
            InitializeComponent();
        }

        private void CraneCurrentAlarm_Load(object sender, EventArgs e)
        {
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.Width == 0) return;
            subShowCurrentAlarm();
        }

        private void subShowCurrentAlarm()
        {
            var eqId = _lcsInfo.Stocker.GetCraneInfoByIndex(_craneId).CraneId;
            var alarms = _alarmService.GetCurrentAlarmDescriptionByEqId(eqId);

            lsbRM1_CurrentAlarm.Items.Clear();

            if (alarms.Any())
            {
                foreach (var alarm in alarms)
                {
                    lsbRM1_CurrentAlarm.Items.Add($"{alarm.StrDT} - {alarm.AlarmDesc}");
                }
            }
        }
    }
}