using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Structure;

namespace Mirle.Def.U2NMMA30
{
    public class ConveyorDef
    {
        public static ConveyorInfo B1_001 = new ConveyorInfo { Index = 1, BufferName = "B1-001", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_004 = new ConveyorInfo { Index = 4, BufferName = "B1-004", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_007 = new ConveyorInfo { Index = 7, BufferName = "B1-007", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_010 = new ConveyorInfo { Index = 10, BufferName = "B1-010", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_081 = new ConveyorInfo { Index = 81, BufferName = "B1-081", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_084 = new ConveyorInfo { Index = 84, BufferName = "B1-084", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_087 = new ConveyorInfo { Index = 87, BufferName = "B1-087", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_090 = new ConveyorInfo { Index = 90, BufferName = "B1-090", DoubleType = DoubleType.Right };

        public static ConveyorInfo B1_013 = new ConveyorInfo { Index = 13, BufferName = "B1-013", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_016 = new ConveyorInfo { Index = 16, BufferName = "B1-016", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_019 = new ConveyorInfo { Index = 19, BufferName = "B1-019", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_022 = new ConveyorInfo { Index = 22, BufferName = "B1-022", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_093 = new ConveyorInfo { Index = 93, BufferName = "B1-093", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_096 = new ConveyorInfo { Index = 96, BufferName = "B1-096", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_099 = new ConveyorInfo { Index = 99, BufferName = "B1-099", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_102 = new ConveyorInfo { Index = 102, BufferName = "B1-102", DoubleType = DoubleType.Right };

        public static ConveyorInfo B1_025 = new ConveyorInfo { Index = 25, BufferName = "B1-025", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_028 = new ConveyorInfo { Index = 28, BufferName = "B1-028", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_031 = new ConveyorInfo { Index = 31, BufferName = "B1-031", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_034 = new ConveyorInfo { Index = 34, BufferName = "B1-034", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_105 = new ConveyorInfo { Index = 105, BufferName = "B1-105", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_108 = new ConveyorInfo { Index = 108, BufferName = "B1-108", DoubleType = DoubleType.Right };
        public static ConveyorInfo B1_111 = new ConveyorInfo { Index = 111, BufferName = "B1-111", DoubleType = DoubleType.Left };
        public static ConveyorInfo B1_114 = new ConveyorInfo { Index = 114, BufferName = "B1-114", DoubleType = DoubleType.Right };

        public static ConveyorInfo M1_01 = new ConveyorInfo { Index = 1, BufferName = "M1-01" };
        public static ConveyorInfo M1_06 = new ConveyorInfo { Index = 6, BufferName = "M1-06" };

        public static ConveyorInfo M1_11 = new ConveyorInfo { Index = 11, BufferName = "M1-11" };
        public static ConveyorInfo M1_16 = new ConveyorInfo { Index = 16, BufferName = "M1-16" };

        private static List<ConveyorInfo> Stations = new List<ConveyorInfo>();
        /// <summary>
        /// 人員工作站List
        /// </summary>
        /// <returns></returns>
        public static List<ConveyorInfo> GetStations() => Stations;
        public static void FunStnListAddInit()
        {

        }
    }
}
