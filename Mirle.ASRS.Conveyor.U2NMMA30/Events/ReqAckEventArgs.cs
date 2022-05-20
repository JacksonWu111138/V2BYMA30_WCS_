﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Events
{
    public class ReqAckEventArgs : EventArgs
    {
        public int BufferIndex { get; }

        public ReqAckEventArgs(int bufferIndex)
        {
            BufferIndex = bufferIndex;
        }
    }
}
