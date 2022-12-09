using System;
using Mirle.Def;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Structure.Info;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Mirle.Middle;

namespace Mirle.DB.Object
{
    public class clsCycleRun
    {
        private static bool PCBAcycleRun;
        private static bool BOXcycleRun;
        public static void Initial()
        {
            PCBAcycleRun = false;
            BOXcycleRun = false;
        }

        public static bool GetPCBAcycleRun() => PCBAcycleRun;
        public static bool GetBOXcycleRun() => BOXcycleRun;

        public static void ChangePCBACycleRun(bool status)
        {
            PCBAcycleRun = status;
        }
        public static void ChangeBOXCycleRun(bool status)
        {
            BOXcycleRun = status;
        }
    }
}
