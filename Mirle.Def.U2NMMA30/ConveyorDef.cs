using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Structure;

namespace Mirle.Def.U2NMMA30
{
    public class ConveyorDef
    {
        /// <summary>
        /// 箱式倉
        /// </summary>
        public class Box
        {
            public static ConveyorInfo B1_001 = new ConveyorInfo { Index = 1, BufferName = "B1-001", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_004 = new ConveyorInfo { Index = 4, BufferName = "B1-004", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_007 = new ConveyorInfo { Index = 7, BufferName = "B1-007", DoubleType = DoubleType.Left, Path = 11 };
            public static ConveyorInfo B1_010 = new ConveyorInfo { Index = 10, BufferName = "B1-010", DoubleType = DoubleType.Right, Path = 12 };
            public static ConveyorInfo B1_081 = new ConveyorInfo { Index = 81, BufferName = "B1-081", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_084 = new ConveyorInfo { Index = 84, BufferName = "B1-084", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_087 = new ConveyorInfo { Index = 87, BufferName = "B1-087", DoubleType = DoubleType.Left, Path = 13 };
            public static ConveyorInfo B1_090 = new ConveyorInfo { Index = 90, BufferName = "B1-090", DoubleType = DoubleType.Right, Path = 14 };

            public static ConveyorInfo B1_013 = new ConveyorInfo { Index = 13, BufferName = "B1-013", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_016 = new ConveyorInfo { Index = 16, BufferName = "B1-016", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_019 = new ConveyorInfo { Index = 19, BufferName = "B1-019", DoubleType = DoubleType.Left, Path = 21 };
            public static ConveyorInfo B1_022 = new ConveyorInfo { Index = 22, BufferName = "B1-022", DoubleType = DoubleType.Right, Path = 22 };
            public static ConveyorInfo B1_093 = new ConveyorInfo { Index = 93, BufferName = "B1-093", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_096 = new ConveyorInfo { Index = 96, BufferName = "B1-096", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_099 = new ConveyorInfo { Index = 99, BufferName = "B1-099", DoubleType = DoubleType.Left, Path = 23 };
            public static ConveyorInfo B1_102 = new ConveyorInfo { Index = 102, BufferName = "B1-102", DoubleType = DoubleType.Right, Path = 24 };

            public static ConveyorInfo B1_025 = new ConveyorInfo { Index = 25, BufferName = "B1-025", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_028 = new ConveyorInfo { Index = 28, BufferName = "B1-028", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_031 = new ConveyorInfo { Index = 31, BufferName = "B1-031", DoubleType = DoubleType.Left, Path = 31 };
            public static ConveyorInfo B1_034 = new ConveyorInfo { Index = 34, BufferName = "B1-034", DoubleType = DoubleType.Right, Path = 32 };
            public static ConveyorInfo B1_105 = new ConveyorInfo { Index = 105, BufferName = "B1-105", DoubleType = DoubleType.Left };
            public static ConveyorInfo B1_108 = new ConveyorInfo { Index = 108, BufferName = "B1-108", DoubleType = DoubleType.Right };
            public static ConveyorInfo B1_111 = new ConveyorInfo { Index = 111, BufferName = "B1-111", DoubleType = DoubleType.Left, Path = 33 };
            public static ConveyorInfo B1_114 = new ConveyorInfo { Index = 114, BufferName = "B1-114", DoubleType = DoubleType.Right, Path = 34 };
        }

        public class PCBA
        {
            public static ConveyorInfo M1_01 = new ConveyorInfo { Index = 1, BufferName = "M1-01" };
            public static ConveyorInfo M1_06 = new ConveyorInfo { Index = 6, BufferName = "M1-06" };

            public static ConveyorInfo M1_11 = new ConveyorInfo { Index = 11, BufferName = "M1-11" };
            public static ConveyorInfo M1_16 = new ConveyorInfo { Index = 16, BufferName = "M1-16" };
        }

        public class AGV
        {
            public static ConveyorInfo A1_01 = new ConveyorInfo { Index = 1, BufferName = "A1-01" };
            public static ConveyorInfo A1_04 = new ConveyorInfo { Index = 4, BufferName = "A1-04" };
            public static ConveyorInfo A1_05 = new ConveyorInfo { Index = 5, BufferName = "A1-05" };
            public static ConveyorInfo A1_08 = new ConveyorInfo { Index = 8, BufferName = "A1-08" };
            public static ConveyorInfo A1_09 = new ConveyorInfo { Index = 9, BufferName = "A1-09" };
            public static ConveyorInfo A1_12 = new ConveyorInfo { Index = 12, BufferName = "A1-12" };

            public static ConveyorInfo A2_01 = new ConveyorInfo { Index = 1, BufferName = "A2-01" };
            public static ConveyorInfo A2_04 = new ConveyorInfo { Index = 4, BufferName = "A2-04" };
            public static ConveyorInfo A2_05 = new ConveyorInfo { Index = 5, BufferName = "A2-05" };
            public static ConveyorInfo A2_08 = new ConveyorInfo { Index = 8, BufferName = "A2-08" };
            public static ConveyorInfo A2_09 = new ConveyorInfo { Index = 9, BufferName = "A2-09" };
            public static ConveyorInfo A2_12 = new ConveyorInfo { Index = 12, BufferName = "A2-12" };
            public static ConveyorInfo A2_13 = new ConveyorInfo { Index = 13, BufferName = "A2-13" };
            public static ConveyorInfo A2_16 = new ConveyorInfo { Index = 16, BufferName = "A2-16" };
            public static ConveyorInfo A2_17 = new ConveyorInfo { Index = 17, BufferName = "A2-17" };
            public static ConveyorInfo A2_20 = new ConveyorInfo { Index = 20, BufferName = "A2-20" };

            public static ConveyorInfo A3_01 = new ConveyorInfo { Index = 1, BufferName = "A3-01" };
            public static ConveyorInfo A3_04 = new ConveyorInfo { Index = 4, BufferName = "A3-04" };
            public static ConveyorInfo A3_05 = new ConveyorInfo { Index = 5, BufferName = "A3-05" };
            public static ConveyorInfo A3_08 = new ConveyorInfo { Index = 8, BufferName = "A3-08" };
            public static ConveyorInfo A3_09 = new ConveyorInfo { Index = 9, BufferName = "A3-09" };
            public static ConveyorInfo A3_12 = new ConveyorInfo { Index = 12, BufferName = "A3-12" };
            public static ConveyorInfo A3_13 = new ConveyorInfo { Index = 13, BufferName = "A3-13" };
            public static ConveyorInfo A3_16 = new ConveyorInfo { Index = 16, BufferName = "A3-16" };
            public static ConveyorInfo A3_17 = new ConveyorInfo { Index = 17, BufferName = "A3-17" };
            public static ConveyorInfo A3_20 = new ConveyorInfo { Index = 20, BufferName = "A3-20" };

            public static ConveyorInfo B1_070 = new ConveyorInfo { Index = 70, BufferName = "B1-070", Path = 41 };
            public static ConveyorInfo B1_071 = new ConveyorInfo { Index = 71, BufferName = "B1-071", Path = 42 };
            public static ConveyorInfo B1_074 = new ConveyorInfo { Index = 74, BufferName = "B1-074", Path = 43 };
            public static ConveyorInfo B1_075 = new ConveyorInfo { Index = 75, BufferName = "B1-075", Path = 44 };
            public static ConveyorInfo B1_078 = new ConveyorInfo { Index = 78, BufferName = "B1-078", Path = 45 };
            public static ConveyorInfo B1_079 = new ConveyorInfo { Index = 79, BufferName = "B1-079", Path = 46 };

            public static ConveyorInfo S1_01 = new ConveyorInfo { Index = 1, BufferName = "S1-01" };
            public static ConveyorInfo S1_07 = new ConveyorInfo { Index = 7, BufferName = "S1-07" };
            public static ConveyorInfo S1_13 = new ConveyorInfo { Index = 13, BufferName = "S1-13" };
            public static ConveyorInfo S1_25 = new ConveyorInfo { Index = 25, BufferName = "S1-25" };

        }

        private static List<ConveyorInfo> Node_All = new List<ConveyorInfo>();
        /// <summary>
        /// 取得所有節點的ConveyerInfo
        /// </summary>
        /// <returns></returns>
        public static List<ConveyorInfo> GetAllNode() => Node_All;
        public static void FunNodeListAddInit()
        {
            Node_All.Add(Box.B1_001);
            Node_All.Add(Box.B1_004);
            Node_All.Add(Box.B1_007);
            Node_All.Add(Box.B1_010);
            Node_All.Add(Box.B1_013);
            Node_All.Add(Box.B1_016);
            Node_All.Add(Box.B1_019);
            Node_All.Add(Box.B1_022);
            Node_All.Add(Box.B1_025);
            Node_All.Add(Box.B1_028);
            Node_All.Add(Box.B1_031);
            Node_All.Add(Box.B1_034);
            Node_All.Add(Box.B1_081);
            Node_All.Add(Box.B1_084);
            Node_All.Add(Box.B1_087);
            Node_All.Add(Box.B1_090);
            Node_All.Add(Box.B1_093);
            Node_All.Add(Box.B1_096);
            Node_All.Add(Box.B1_099);
            Node_All.Add(Box.B1_102);
            Node_All.Add(Box.B1_105);
            Node_All.Add(Box.B1_108);
            Node_All.Add(Box.B1_111);
            Node_All.Add(Box.B1_114);

            Node_All.Add(PCBA.M1_01);
            Node_All.Add(PCBA.M1_06);
            Node_All.Add(PCBA.M1_11);
            Node_All.Add(PCBA.M1_16);


        }

        private static List<ConveyorInfo> Stations = new List<ConveyorInfo>();
        /// <summary>
        /// 人員工作站List
        /// </summary>
        /// <returns></returns>
        public static List<ConveyorInfo> GetStations() => Stations;
        public static void FunStnListAddInit()
        {

        }

        public static int GetPathByStn(string StnNo)
        {
            var lst = Stations.Where(r => r.StnNo == StnNo);
            foreach(var s in lst)
            {
                return s.Path;
            }

            return 0;
        }
    }
}
