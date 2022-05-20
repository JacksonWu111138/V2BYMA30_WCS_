using Mirle.STKC.R46YP320.Model;

namespace Mirle.LCS.Models.Info
{
    public class AlarmDefInfo
    {
        public AlarmTypes AlarmType { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmId { get; set; }
        public AlarmLevel AlarmLevel { get; set; }
        public bool ReportEnable { get; set; }
    }
}