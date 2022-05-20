using Mirle.MPLC.DataType;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Signal
{
    public class ConveyorControllerSignal
    {
        public Word Heartbeat { get; internal set; }
        public WordBlock SystemTimeCalibration { get; internal set; }
    }
}
