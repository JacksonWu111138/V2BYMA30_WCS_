namespace Mirle.LCS.Models.Info
{
    public class IoInfo
    {
        public int PLCPortId { get; set; }
        public string HostEQPortId { get; set; }

        public int PortTypeIndex { get; set; }
        public int Stage { get; set; }//StageCount
        public int Vehicles { get; set; }//VehicleCount
        public int AlarmType { get; set; }
    }
}