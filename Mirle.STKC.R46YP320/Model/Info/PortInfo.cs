using Mirle.STKC.R46YP320.Model;

namespace Mirle.LCS.Models.Info
{
    public class PortInfo
    {
        public int PLCPortId { get; set; }
        public string HostEQPortId { get; set; }
        public string ShelfId { get; set; }
        public PortType PortType { get; set; }
        public int PortTypeIndex { get; set; }
        public int Stage { get; set; }//StageCount
        public int Vehicles { get; set; }//VehicleCount
        public int NetHStnNo { get; set; }
        public int AreaSensorStnNo { get; set; }
    }
}