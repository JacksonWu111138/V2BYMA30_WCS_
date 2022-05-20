using Mirle.MPLC.DataType;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Signal
{
    public class BufferAckSignal
    {
        public Word InitalAck { get; internal set; }
        public Word ReadBcrAck { get; internal set; }
    }
}
