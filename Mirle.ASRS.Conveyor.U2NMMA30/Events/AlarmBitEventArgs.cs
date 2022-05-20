using System;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Events
{
    public class AlarmBitEventArgs : EventArgs
    {
        public int BufferIndex { get; }
        public int AlarmBit { get; }
        public bool Signal { get; }

        public AlarmBitEventArgs(int bufferIndex, int alarmBit, bool signal)
        {
            BufferIndex = bufferIndex;
            AlarmBit = alarmBit;
            Signal = signal;
        }
    }
}
