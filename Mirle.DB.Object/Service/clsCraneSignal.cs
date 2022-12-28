using System;
using Mirle.Def;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Structure.Info;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mirle.Middle;
using Mirle.EccsSignal;

namespace Mirle.DB.Object
{
    public class clsCraneSignal
    {
        private static SignalHost PCBA1CraneSignal;
        private static SignalHost PCBA2CraneSignal;
        private static SignalHost Box1CraneSignal;
        private static SignalHost Box2CraneSignal;
        private static SignalHost Box3CraneSignal;
        public static void Initial(SignalHost pCBA1CraneSignal, SignalHost pCBA2CraneSignal, SignalHost box1CraneSignal, SignalHost box2CraneSignal, SignalHost box3CraneSignal)
        {
            PCBA1CraneSignal = pCBA1CraneSignal;
            PCBA2CraneSignal = pCBA2CraneSignal;
            Box1CraneSignal = box1CraneSignal;
            Box2CraneSignal = box2CraneSignal;
            Box3CraneSignal = box3CraneSignal;
        }

        public static SignalHost GetPCBA1CraneSingnal() => PCBA1CraneSignal;
        public static SignalHost GetPCBA2CraneSingnal() => PCBA2CraneSignal;
        public static SignalHost GetBox1CraneSingnal() => Box1CraneSignal;
        public static SignalHost GetBox2CraneSingnal() => Box2CraneSignal;
        public static SignalHost GetBox3CraneSingnal() => Box3CraneSignal;

    }
}
