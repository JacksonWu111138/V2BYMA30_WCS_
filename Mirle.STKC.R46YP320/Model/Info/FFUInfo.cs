using Mirle.STKC.R46YP320.Model;

namespace Mirle.LCS.Models.Info
{
    public class FFUInfo
    {
        public bool Enable { get; set; }

        public string EqId { get; set; }

        public int GroupId { get; set; }
        public int FFUId { get; set; }
        public string ShelfId { get; set; }
        public bool Run { get; set; }
        public FFUModeType ModeType { get; set; }
        public FFURemoteType MPURemote { get; set; }
        public int SpeedValue { get; set; }
        public int Mode1Speed { get; set; }
        public int Mode2Speed { get; set; }
        public int Mode3Speed { get; set; }
        public int SpeedMin { get; set; }
        public int SpeedMax { get; set; }
    }
}