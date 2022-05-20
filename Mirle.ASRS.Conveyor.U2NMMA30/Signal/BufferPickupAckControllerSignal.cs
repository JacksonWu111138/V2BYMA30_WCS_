﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Signal
{
    public class BufferPickupAckControllerSignal
    {
        public BitSignal[] PickupBit = new BitSignal[16];
        public BufferPickupAckControllerSignal()
        {
            for (int i = 0; i < PickupBit.Length; i++)
            {
                PickupBit[i] = new BitSignal();
            }
        }
    }
}
