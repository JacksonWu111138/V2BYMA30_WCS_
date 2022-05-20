using Mirle.MPLC.DataType;
using System.Collections;
using System.Collections.Generic;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Signal
{
    public class ConveyorSignal
    {
        public Word Heartbeat { get; internal set; }
        public BufferAlarmBitSignal AlarmBit { get; internal set; }
        public ConveyorControllerSignal Controller { get; internal set; }
    }
}
