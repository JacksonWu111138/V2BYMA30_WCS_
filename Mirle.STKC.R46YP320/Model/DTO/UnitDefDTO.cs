namespace Mirle.STKC.R46YP320.Model
{
    public class UnitDefDTO
    {
        public string StockerId { get; set; }
        public string UnitId { get; set; }
        public string HostPortId { get; set; }
        public UnitType UnitType { get; set; }
        public PortType PortType { get; set; }
        public int PortTypeIndex { get; set; }
        public int UnitIndex { get; set; }
        public int MaintenanceValue { get; set; }
    }
}