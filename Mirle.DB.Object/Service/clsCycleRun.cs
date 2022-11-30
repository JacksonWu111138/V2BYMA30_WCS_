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
        public static void Initial()
        {
            PCBAcycleRun = false;
        }

        public static bool GetPCBAcycleRun() => PCBAcycleRun;

        public static void ChangePCBACycleRun(bool status)
        {
            PCBAcycleRun = status;
        }
    }
}
