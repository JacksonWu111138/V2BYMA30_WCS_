namespace Mirle.STKC.R46YP320.Simulator.CraneCommand
{
    internal class CommandInfo
    {
        public string CommandType { get; set; }
        public int ForkNo { get; set; }
        public int CommandNo { get; set; }
        private int _fromLocation;

        public int FromLocation
        {
            get { return _fromLocation; }
            set
            {
                _fromLocation = value;
                SourceBank = _fromLocation / 10000;
                SourceBay = (_fromLocation % 10000) / 100;
                SourceLevel = _fromLocation % 100;
            }
        }
        private int _toLocation;
        public int ToLocation
        {
            get { return _toLocation; }
            set
            {
                _toLocation = value;
                DestinationBank = _toLocation / 10000;
                DestinationBay = (_toLocation % 10000) / 100;
                DestinationLevel = _toLocation % 100;
            }
        }
        public string CstId { get; set; }

        public int SourceBank { get; private set; }
        public int SourceBay { get; private set; }
        public int SourceLevel { get; private set; }
        public int DestinationBank { get; private set; }
        public int DestinationBay { get; private set; }
        public int DestinationLevel { get; private set; }
    }
}