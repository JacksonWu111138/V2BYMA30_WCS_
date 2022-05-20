using System;

namespace Mirle.ASRS.Conveyor.U2NMMA30.Events
{
    public class BcrResultEventArgs : EventArgs
    {
        //public int BcrResultIndex { get; }
        public string BoxId { get; }

        //public BcrResultEventArgs(int bcrResultIndex, string boxId)
        //{
        //    BcrResultIndex = bcrResultIndex;
        //    BoxId = boxId;
        //}
        public BcrResultEventArgs(string boxId)
        {
            BoxId = boxId;
        }
    }
}
