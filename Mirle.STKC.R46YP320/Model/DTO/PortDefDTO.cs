namespace Mirle.STKC.R46YP320.Model
{
    public class PortDefDTO
    {
        public string StockerId { get; set; }
        public int PLCPortId { get; set; }
        public string HostEQPortId { get; set; }
        public string ShelfId { get; set; }
        public PortType PortType { get; set; }
        public int PortTypeIndex { get; set; }
        public int Stage { get; set; }
        public int Vehicles { get; set; }
        public int NetHStnNo { get; set; }
        public int AreaSensorStnNo { get; set; }
        public int AlarmType { get; set; }
    }
}