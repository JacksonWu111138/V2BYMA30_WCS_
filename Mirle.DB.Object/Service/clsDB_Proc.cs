using System;
using Mirle.Def;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Structure.Info;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Mirle.DB.Object
{
    public class clsDB_Proc
    {
        private static Proc.clsHost wcs;
        public static void Initial(clsDbConfig dbConfig)
        {
            wcs = new Proc.clsHost(dbConfig);
        }

        public static Proc.clsHost GetDB_Object() => wcs;
        public static bool DBConn => Proc.clsHost.IsConn;
        public static int FunSelectNeedToTeach(int MaxCount, ref DataTable dtTmp) => wcs.GetL2LCount().FunSelectNeedToTeach(MaxCount, ref dtTmp);
    }
}
