using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mirle.Structure;
using Mirle.Structure.Info;

namespace Mirle.Def.U2NMMA30
{
    public class ConveyorDef
    {
        public class TeachLoc
        {
            public static string M801Left = "";
            public static string M801Right = "";
            public static string M802Left = "";
            public static string M802Right = "";
            public static string B801Left = "";
            public static string B801Right = "";
            public static string B802Left = "";
            public static string B802Right = "";
            public static string B803Left = "";
            public static string B803Right = "";
        }
        public class E04
        {
            /// <summary>
            /// 站口
            /// </summary>
            public static ConveyorInfo LO1_02 = new ConveyorInfo { Index = 2, BufferName = "LO1-02" };
            /// <summary>
            /// 站口
            /// </summary>
            public static ConveyorInfo LO1_07 = new ConveyorInfo { Index = 7, BufferName = "LO1-07" };
        }
        public class E05
        {
            /// <summary>
            /// 八樓離開電梯第一節
            /// </summary>
            public static ConveyorInfo LO3_02 = new ConveyorInfo { Index = 2, BufferName = "LO3-02" };
        }

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

            public static ConveyorInfo B1_037 = new ConveyorInfo { Index = 37, BufferName = "B1-037" };
            public static ConveyorInfo B1_041 = new ConveyorInfo { Index = 41, BufferName = "B1-041" };
            public static ConveyorInfo B1_045 = new ConveyorInfo { Index = 45, BufferName = "B1-045" };
            public static ConveyorInfo B1_054 = new ConveyorInfo { Index = 54, BufferName = "B1-054" };
            public static ConveyorInfo B1_117 = new ConveyorInfo { Index = 117, BufferName = "B1-117" };
            public static ConveyorInfo B1_121 = new ConveyorInfo { Index = 121, BufferName = "B1-121" };
            public static ConveyorInfo B1_125 = new ConveyorInfo { Index = 125, BufferName = "B1-125" };
            public static ConveyorInfo B1_134 = new ConveyorInfo { Index = 134, BufferName = "B1-134" };

            public static ConveyorInfo B1_009 = new ConveyorInfo { Index = 9, BufferName = "B1-009" };
            public static ConveyorInfo B1_012 = new ConveyorInfo { Index = 12, BufferName = "B1-012" };
            public static ConveyorInfo B1_021 = new ConveyorInfo { Index = 21, BufferName = "B1-021" };
            public static ConveyorInfo B1_024 = new ConveyorInfo { Index = 24, BufferName = "B1-024" };
            public static ConveyorInfo B1_033 = new ConveyorInfo { Index = 33, BufferName = "B1-033" };
            public static ConveyorInfo B1_036 = new ConveyorInfo { Index = 36, BufferName = "B1-036" };
            public static ConveyorInfo B1_089 = new ConveyorInfo { Index = 89, BufferName = "B1-089" };
            public static ConveyorInfo B1_092 = new ConveyorInfo { Index = 92, BufferName = "B1-092" };
            public static ConveyorInfo B1_101 = new ConveyorInfo { Index = 101, BufferName = "B1-101" };
            public static ConveyorInfo B1_104 = new ConveyorInfo { Index = 104, BufferName = "B1-104" };
            public static ConveyorInfo B1_113 = new ConveyorInfo { Index = 113, BufferName = "B1-113" };
            public static ConveyorInfo B1_116 = new ConveyorInfo { Index = 116, BufferName = "B1-116" };
            /// <summary>
            /// 下左撿料口
            /// </summary>
            public static ConveyorInfo B1_062 = new ConveyorInfo { Index = 62, BufferName = "B1-062", Path = 10 };
            /// <summary>
            /// 下右撿料口
            /// </summary>
            public static ConveyorInfo B1_067 = new ConveyorInfo { Index = 67, BufferName = "B1-067", Path = 20 };
            /// <summary>
            /// 上左撿料口
            /// </summary>
            public static ConveyorInfo B1_142 = new ConveyorInfo { Index = 142, BufferName = "B1-142", Path = 30 };
            /// <summary>
            /// 上右撿料口
            /// </summary>
            public static ConveyorInfo B1_147 = new ConveyorInfo { Index = 147, BufferName = "B1-147", Path = 40 };
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

            public static ConveyorInfo A4_01 = new ConveyorInfo { Index = 1, BufferName = "A4-01" };
            public static ConveyorInfo A4_04 = new ConveyorInfo { Index = 4, BufferName = "A4-04" };
            public static ConveyorInfo A4_05 = new ConveyorInfo { Index = 5, BufferName = "A4-05" };
            public static ConveyorInfo A4_08 = new ConveyorInfo { Index = 8, BufferName = "A4-08" };
            public static ConveyorInfo A4_09 = new ConveyorInfo { Index = 9, BufferName = "A4-09" };
            public static ConveyorInfo A4_12 = new ConveyorInfo { Index = 12, BufferName = "A4-12" };
            public static ConveyorInfo A4_13 = new ConveyorInfo { Index = 13, BufferName = "A4-13" };
            public static ConveyorInfo A4_16 = new ConveyorInfo { Index = 16, BufferName = "A4-16" };
            public static ConveyorInfo A4_17 = new ConveyorInfo { Index = 17, BufferName = "A4-17" };
            public static ConveyorInfo A4_20 = new ConveyorInfo { Index = 20, BufferName = "A4-20" };

            public static ConveyorInfo B1_070 = new ConveyorInfo { Index = 70, BufferName = "B1-070", Path = 41 };
            public static ConveyorInfo B1_071 = new ConveyorInfo { Index = 71, BufferName = "B1-071", Path = 42 };
            public static ConveyorInfo B1_074 = new ConveyorInfo { Index = 74, BufferName = "B1-074", Path = 43 };
            public static ConveyorInfo B1_075 = new ConveyorInfo { Index = 75, BufferName = "B1-075", Path = 44 };
            public static ConveyorInfo B1_078 = new ConveyorInfo { Index = 78, BufferName = "B1-078", Path = 45 };
            public static ConveyorInfo B1_079 = new ConveyorInfo { Index = 79, BufferName = "B1-079", Path = 46 };

            public static ConveyorInfo E1_01 = new ConveyorInfo { Index = 1, BufferName = "E1-01" };
            public static ConveyorInfo E1_08 = new ConveyorInfo { Index = 8, BufferName = "E1-08" };
            public static ConveyorInfo E2_35 = new ConveyorInfo { Index = 35, BufferName = "E2-35" };
            public static ConveyorInfo E2_36 = new ConveyorInfo { Index = 36, BufferName = "E2-36" };
            public static ConveyorInfo E2_37 = new ConveyorInfo { Index = 37, BufferName = "E2-37" };
            public static ConveyorInfo E2_38 = new ConveyorInfo { Index = 38, BufferName = "E2-38" };
            public static ConveyorInfo E2_39 = new ConveyorInfo { Index = 39, BufferName = "E2-39" };
            public static ConveyorInfo E2_44 = new ConveyorInfo { Index = 44, BufferName = "E2-44" };

            public static ConveyorInfo LO2_01 = new ConveyorInfo { Index = 1, BufferName = "LO2-01" };
            public static ConveyorInfo LO2_04 = new ConveyorInfo { Index = 4, BufferName = "LO2-04" };
            public static ConveyorInfo LO3_01 = new ConveyorInfo { Index = 1, BufferName = "LO3-01" };
            public static ConveyorInfo LO3_04 = new ConveyorInfo { Index = 4, BufferName = "LO3-04" };
            public static ConveyorInfo LO4_01 = new ConveyorInfo { Index = 1, BufferName = "LO4-01" };
            public static ConveyorInfo LO4_04 = new ConveyorInfo { Index = 4, BufferName = "LO4-04" };
            public static ConveyorInfo LO5_01 = new ConveyorInfo { Index = 1, BufferName = "LO5-01" };
            public static ConveyorInfo LO5_04 = new ConveyorInfo { Index = 4, BufferName = "LO5-04" };
            public static ConveyorInfo LO6_01 = new ConveyorInfo { Index = 1, BufferName = "LO6-01" };
            public static ConveyorInfo LO6_04 = new ConveyorInfo { Index = 4, BufferName = "LO6-04" };

            public static ConveyorInfo M1_05 = new ConveyorInfo { Index = 5, BufferName = "M1-05" };
            public static ConveyorInfo M1_10 = new ConveyorInfo { Index = 10, BufferName = "M1-10" };
            public static ConveyorInfo M1_15 = new ConveyorInfo { Index = 15, BufferName = "M1-15" };
            public static ConveyorInfo M1_20 = new ConveyorInfo { Index = 20, BufferName = "M1-20" };

            public static ConveyorInfo S1_01 = new ConveyorInfo { Index = 1, BufferName = "S1-01" };
            public static ConveyorInfo S1_07 = new ConveyorInfo { Index = 7, BufferName = "S1-07" };
            public static ConveyorInfo S1_13 = new ConveyorInfo { Index = 13, BufferName = "S1-13" };
            public static ConveyorInfo S1_25 = new ConveyorInfo { Index = 25, BufferName = "S1-25" };
            public static ConveyorInfo S1_31 = new ConveyorInfo { Index = 31, BufferName = "S1-31" };
            public static ConveyorInfo S1_37 = new ConveyorInfo { Index = 37, BufferName = "S1-37" };
            public static ConveyorInfo S1_40 = new ConveyorInfo { Index = 40, BufferName = "S1-40" };
            public static ConveyorInfo S1_41 = new ConveyorInfo { Index = 41, BufferName = "S1-41" };
            public static ConveyorInfo S1_44 = new ConveyorInfo { Index = 44, BufferName = "S1-44" };
            public static ConveyorInfo S1_45 = new ConveyorInfo { Index = 45, BufferName = "S1-45" };
            public static ConveyorInfo S1_48 = new ConveyorInfo { Index = 48, BufferName = "S1-48" };
            public static ConveyorInfo S1_49 = new ConveyorInfo { Index = 49, BufferName = "S1-49" };
            public static ConveyorInfo S1_50 = new ConveyorInfo { Index = 50, BufferName = "S1-50" };
            //public static ConveyorInfo S1_52 = new ConveyorInfo { Index = 52, BufferName = "S1-52" };
            //public static ConveyorInfo S1_56 = new ConveyorInfo { Index = 56, BufferName = "S1-56" };
            //public static ConveyorInfo S1_60 = new ConveyorInfo { Index = 60, BufferName = "S1-60" };
            //public static ConveyorInfo S1_64 = new ConveyorInfo { Index = 64, BufferName = "S1-64" };

            public static ConveyorInfo S2_01 = new ConveyorInfo { Index = 1, BufferName = "S2-01" };
            public static ConveyorInfo S2_07 = new ConveyorInfo { Index = 7, BufferName = "S2-07" };
            public static ConveyorInfo S2_13 = new ConveyorInfo { Index = 13, BufferName = "S2-13" };
            public static ConveyorInfo S2_25 = new ConveyorInfo { Index = 25, BufferName = "S2-25" };
            public static ConveyorInfo S2_31 = new ConveyorInfo { Index = 31, BufferName = "S2-31" };
            public static ConveyorInfo S2_49 = new ConveyorInfo { Index = 49, BufferName = "S2-49" };

            public static ConveyorInfo S3_01 = new ConveyorInfo { Index = 1, BufferName = "S3-01" };
            public static ConveyorInfo S3_07 = new ConveyorInfo { Index = 7, BufferName = "S3-07" };
            public static ConveyorInfo S3_13 = new ConveyorInfo { Index = 13, BufferName = "S3-13" };
            public static ConveyorInfo S3_19 = new ConveyorInfo { Index = 19, BufferName = "S3-19" };
            public static ConveyorInfo S3_25 = new ConveyorInfo { Index = 25, BufferName = "S3-25" };
            public static ConveyorInfo S3_31 = new ConveyorInfo { Index = 31, BufferName = "S3-31" };
            public static ConveyorInfo S3_37 = new ConveyorInfo { Index = 37, BufferName = "S3-37" };
            public static ConveyorInfo S3_40 = new ConveyorInfo { Index = 40, BufferName = "S3-40" };
            public static ConveyorInfo S3_41 = new ConveyorInfo { Index = 41, BufferName = "S3-41" };
            public static ConveyorInfo S3_44 = new ConveyorInfo { Index = 44, BufferName = "S3-44" };
            public static ConveyorInfo S3_45 = new ConveyorInfo { Index = 45, BufferName = "S3-45" };
            public static ConveyorInfo S3_48 = new ConveyorInfo { Index = 48, BufferName = "S3-48" };
            public static ConveyorInfo S3_49 = new ConveyorInfo { Index = 49, BufferName = "S3-49" };

            public static ConveyorInfo S4_01 = new ConveyorInfo { Index = 1, BufferName = "S4-01" };
            public static ConveyorInfo S4_07 = new ConveyorInfo { Index = 7, BufferName = "S4-07" };
            public static ConveyorInfo S4_13 = new ConveyorInfo { Index = 13, BufferName = "S4-13" };
            public static ConveyorInfo S4_19 = new ConveyorInfo { Index = 19, BufferName = "S4-19" };
            public static ConveyorInfo S4_25 = new ConveyorInfo { Index = 25, BufferName = "S4-25" };
            public static ConveyorInfo S4_49 = new ConveyorInfo { Index = 49, BufferName = "S4-49" };
            public static ConveyorInfo S4_50 = new ConveyorInfo { Index = 50, BufferName = "S4-50" };

            public static ConveyorInfo S5_01 = new ConveyorInfo { Index = 1, BufferName = "S5-01" };
            public static ConveyorInfo S5_07 = new ConveyorInfo { Index = 7, BufferName = "S5-07" };
            public static ConveyorInfo S5_37 = new ConveyorInfo { Index = 37, BufferName = "S5-37" };
            public static ConveyorInfo S5_40 = new ConveyorInfo { Index = 40, BufferName = "S5-40" };
            public static ConveyorInfo S5_49 = new ConveyorInfo { Index = 49, BufferName = "S5-49" };

            public static ConveyorInfo S6_01 = new ConveyorInfo { Index = 1, BufferName = "S6-01" };
            public static ConveyorInfo S6_07 = new ConveyorInfo { Index = 7, BufferName = "S6-07" };

            public static ConveyorInfo S0_01 = new ConveyorInfo { Index = 1, BufferName = "S0-01" };
            public static ConveyorInfo S0_04 = new ConveyorInfo { Index = 4, BufferName = "S0-04" };
            public static ConveyorInfo S0_05 = new ConveyorInfo { Index = 5, BufferName = "S0-05" };
        }
        /// <summary>
        /// 3F產線
        /// </summary>
        public class SMT3C
        {
            public static ConveyorInfo A1_02 = new ConveyorInfo { Index = 2, BufferName = "A1-02" };
            public static ConveyorInfo A1_03 = new ConveyorInfo { Index = 3, BufferName = "A1-03" };
            public static ConveyorInfo A1_06 = new ConveyorInfo { Index = 6, BufferName = "A1-06" };
            public static ConveyorInfo A1_07 = new ConveyorInfo { Index = 7, BufferName = "A1-07" };
            public static ConveyorInfo A1_10 = new ConveyorInfo { Index = 10, BufferName = "A1-10" };
            public static ConveyorInfo A1_11 = new ConveyorInfo { Index = 11, BufferName = "A1-11" };
        }
        /// <summary>
        /// 5F產線
        /// </summary>
        public class SMT5C
        {
            public static ConveyorInfo A2_02 = new ConveyorInfo { Index = 2, BufferName = "A2-02" };
            public static ConveyorInfo A2_03 = new ConveyorInfo { Index = 3, BufferName = "A2-03" };
            public static ConveyorInfo A2_06 = new ConveyorInfo { Index = 6, BufferName = "A2-06" };
            public static ConveyorInfo A2_07 = new ConveyorInfo { Index = 7, BufferName = "A2-07" };
            public static ConveyorInfo A2_10 = new ConveyorInfo { Index = 10, BufferName = "A2-10" };
            public static ConveyorInfo A2_11 = new ConveyorInfo { Index = 11, BufferName = "A2-11" };
            public static ConveyorInfo A2_14 = new ConveyorInfo { Index = 14, BufferName = "A2-14" };
            public static ConveyorInfo A2_15 = new ConveyorInfo { Index = 15, BufferName = "A2-15" };
            public static ConveyorInfo A2_18 = new ConveyorInfo { Index = 18, BufferName = "A2-18" };
            public static ConveyorInfo A2_19 = new ConveyorInfo { Index = 19, BufferName = "A2-19" };
        }
        /// <summary>
        /// 6F產線
        /// </summary>
        public class SMT6C
        {
            public static ConveyorInfo A3_02 = new ConveyorInfo { Index = 2, BufferName = "A3-02" };
            public static ConveyorInfo A3_03 = new ConveyorInfo { Index = 3, BufferName = "A3-03" };
            public static ConveyorInfo A3_06 = new ConveyorInfo { Index = 6, BufferName = "A3-06" };
            public static ConveyorInfo A3_07 = new ConveyorInfo { Index = 7, BufferName = "A3-07" };
            public static ConveyorInfo A3_10 = new ConveyorInfo { Index = 10, BufferName = "A3-10" };
            public static ConveyorInfo A3_11 = new ConveyorInfo { Index = 11, BufferName = "A3-11" };
            public static ConveyorInfo A3_14 = new ConveyorInfo { Index = 14, BufferName = "A3-14" };
            public static ConveyorInfo A3_15 = new ConveyorInfo { Index = 15, BufferName = "A3-15" };
            public static ConveyorInfo A3_18 = new ConveyorInfo { Index = 18, BufferName = "A3-18" };
            public static ConveyorInfo A3_19 = new ConveyorInfo { Index = 19, BufferName = "A3-19" };
        }
        /// <summary>
        /// 產線
        /// </summary>
        public class SMTC
        {
            public static ConveyorInfo S1_38 = new ConveyorInfo { Index = 38, BufferName = "S1-38" };
            public static ConveyorInfo S1_39 = new ConveyorInfo { Index = 39, BufferName = "S1-39" };
            public static ConveyorInfo S1_42 = new ConveyorInfo { Index = 42, BufferName = "S1-42" };
            public static ConveyorInfo S1_43 = new ConveyorInfo { Index = 43, BufferName = "S1-43" };
            public static ConveyorInfo S1_46 = new ConveyorInfo { Index = 46, BufferName = "S1-46" };
            public static ConveyorInfo S1_47 = new ConveyorInfo { Index = 47, BufferName = "S1-47" };
            public static ConveyorInfo S3_38 = new ConveyorInfo { Index = 38, BufferName = "S3-38" };
            public static ConveyorInfo S3_39 = new ConveyorInfo { Index = 39, BufferName = "S3-39" };
            public static ConveyorInfo S3_42 = new ConveyorInfo { Index = 42, BufferName = "S3-42" };
            public static ConveyorInfo S3_43 = new ConveyorInfo { Index = 43, BufferName = "S3-43" };
            public static ConveyorInfo S3_46 = new ConveyorInfo { Index = 46, BufferName = "S3-46" };
            public static ConveyorInfo S3_47 = new ConveyorInfo { Index = 47, BufferName = "S3-47" };
            public static ConveyorInfo S5_38 = new ConveyorInfo { Index = 38, BufferName = "S5-38" };
            public static ConveyorInfo S5_39 = new ConveyorInfo { Index = 39, BufferName = "S5-39" };
            public static ConveyorInfo S0_02 = new ConveyorInfo { Index = 2, BufferName = "S0-02" };
            public static ConveyorInfo S0_03 = new ConveyorInfo { Index = 3, BufferName = "S0-03" };

            public static ConveyorInfo S3_02 = new ConveyorInfo { Index = 2, BufferName = "S3-02" };
            public static ConveyorInfo S3_08 = new ConveyorInfo { Index = 8, BufferName = "S3-08" };
            public static ConveyorInfo S3_14 = new ConveyorInfo { Index = 14, BufferName = "S3-14" };
            public static ConveyorInfo S3_20 = new ConveyorInfo { Index = 20, BufferName = "S3-20" };
            public static ConveyorInfo S3_26 = new ConveyorInfo { Index = 26, BufferName = "S3-26" };
            public static ConveyorInfo S3_32 = new ConveyorInfo { Index = 32, BufferName = "S3-32" };

            public static ConveyorInfo S1_02 = new ConveyorInfo { Index = 2, BufferName = "S1-02" };
            public static ConveyorInfo S1_08 = new ConveyorInfo { Index = 8, BufferName = "S1-08" };
            public static ConveyorInfo S1_14 = new ConveyorInfo { Index = 14, BufferName = "S1-14" };
            public static ConveyorInfo S1_26 = new ConveyorInfo { Index = 26, BufferName = "S1-26" };
            public static ConveyorInfo S1_32 = new ConveyorInfo { Index = 32, BufferName = "S1-32" };

            public static ConveyorInfo S2_06 = new ConveyorInfo { Index = 6, BufferName = "S2-06" };
            public static ConveyorInfo S2_12 = new ConveyorInfo { Index = 12, BufferName = "S2-12" };
            public static ConveyorInfo S2_18 = new ConveyorInfo { Index = 18, BufferName = "S2-18" };
            public static ConveyorInfo S2_30 = new ConveyorInfo { Index = 30, BufferName = "S2-30" };
            public static ConveyorInfo S2_36 = new ConveyorInfo { Index = 36, BufferName = "S2-36" };

            public static ConveyorInfo S4_06 = new ConveyorInfo { Index = 6, BufferName = "S4-06" };
            public static ConveyorInfo S4_12 = new ConveyorInfo { Index = 12, BufferName = "S4-12" };
            public static ConveyorInfo S4_18 = new ConveyorInfo { Index = 18, BufferName = "S4-18" };
            public static ConveyorInfo S4_24 = new ConveyorInfo { Index = 24, BufferName = "S4-24" };
            public static ConveyorInfo S4_30 = new ConveyorInfo { Index = 30, BufferName = "S4-30" };
        }
        /// <summary>
        /// 線邊倉
        /// </summary>
        public class Line
        {
            public static ConveyorInfo A4_02 = new ConveyorInfo { Index = 2, BufferName = "A4-02" };
            public static ConveyorInfo A4_03 = new ConveyorInfo { Index = 3, BufferName = "A4-03" };
            public static ConveyorInfo A4_06 = new ConveyorInfo { Index = 6, BufferName = "A4-06" };
            public static ConveyorInfo A4_07 = new ConveyorInfo { Index = 7, BufferName = "A4-07" };
            public static ConveyorInfo A4_10 = new ConveyorInfo { Index = 10, BufferName = "A4-10" };
            public static ConveyorInfo A4_11 = new ConveyorInfo { Index = 11, BufferName = "A4-11" };
            public static ConveyorInfo A4_14 = new ConveyorInfo { Index = 14, BufferName = "A4-14" };
            public static ConveyorInfo A4_15 = new ConveyorInfo { Index = 15, BufferName = "A4-15" };
            public static ConveyorInfo A4_18 = new ConveyorInfo { Index = 18, BufferName = "A4-18" };
            public static ConveyorInfo A4_19 = new ConveyorInfo { Index = 19, BufferName = "A4-19" };
        }
        /// <summary>
        /// 電子料塔
        /// </summary>
        public class Tower
        {
            public static ConveyorInfo E1_04 = new ConveyorInfo { Index = 4, BufferName = "E1-04" };
            public static ConveyorInfo E1_10 = new ConveyorInfo { Index = 10, BufferName = "E1-10" };
        }

        public class SharingNode
        {
            public static TwoNodeOneStnnoInfo S800_12 = new TwoNodeOneStnnoInfo { Stn_No = "S800-12", end = SMTC.S0_02, start = SMTC.S0_03 };
            public static TwoNodeOneStnnoInfo S800_M1 = new TwoNodeOneStnnoInfo { Stn_No = "S800-M1", end = Line.A4_10, start = Line.A4_11 };
            public static TwoNodeOneStnnoInfo S800_M2 = new TwoNodeOneStnnoInfo { Stn_No = "S800-M2", end = Line.A4_14, start = Line.A4_15 };
            public static TwoNodeOneStnnoInfo S800_B1 = new TwoNodeOneStnnoInfo { Stn_No = "S800-B1", end = Line.A4_02, start = Line.A4_03 };
            public static TwoNodeOneStnnoInfo S800_B2 = new TwoNodeOneStnnoInfo { Stn_No = "S800-B2", end = Line.A4_06, start = Line.A4_07 };
            
            public static TwoNodeOneStnnoInfo S811_4 = new TwoNodeOneStnnoInfo { Stn_No = "S811-4", end = SMTC.S1_38, start = SMTC.S1_39 };
            public static TwoNodeOneStnnoInfo S811_1 = new TwoNodeOneStnnoInfo { Stn_No = "S811-1", end = AGV.S1_01, start = SMTC.S1_02 };
            public static TwoNodeOneStnnoInfo S811_2 = new TwoNodeOneStnnoInfo { Stn_No = "S811-2", end = AGV.S1_07, start = SMTC.S1_08 };
            public static TwoNodeOneStnnoInfo S811_6 = new TwoNodeOneStnnoInfo { Stn_No = "S811-6", end = AGV.S2_07, start = SMTC.S2_12 };
            public static TwoNodeOneStnnoInfo S811_5 = new TwoNodeOneStnnoInfo { Stn_No = "S811-5", end = AGV.S2_01, start = SMTC.S2_06 };

            public static TwoNodeOneStnnoInfo S812_4 = new TwoNodeOneStnnoInfo { Stn_No = "S812-4", end = SMTC.S1_42, start = SMTC.S1_43 };
            public static TwoNodeOneStnnoInfo S812_1 = new TwoNodeOneStnnoInfo { Stn_No = "S812-1", end = AGV.S1_13, start = SMTC.S1_14 };
            public static TwoNodeOneStnnoInfo S812_5 = new TwoNodeOneStnnoInfo { Stn_No = "S812-5", end = AGV.S2_13, start = SMTC.S2_18 };

            public static TwoNodeOneStnnoInfo S813_4 = new TwoNodeOneStnnoInfo { Stn_No = "S813-4", end = SMTC.S1_46, start = SMTC.S1_47 };
            public static TwoNodeOneStnnoInfo S813_1 = new TwoNodeOneStnnoInfo { Stn_No = "S813-1", end = AGV.S1_25, start = SMTC.S1_26 };
            public static TwoNodeOneStnnoInfo S813_2 = new TwoNodeOneStnnoInfo { Stn_No = "S813-2", end = AGV.S1_31, start = SMTC.S1_32 };
            public static TwoNodeOneStnnoInfo S813_6 = new TwoNodeOneStnnoInfo { Stn_No = "S813-6", end = AGV.S2_31, start = SMTC.S2_36 };
            public static TwoNodeOneStnnoInfo S813_5 = new TwoNodeOneStnnoInfo { Stn_No = "S813-5", end = AGV.S2_25, start = SMTC.S2_30 };

            public static TwoNodeOneStnnoInfo S814_4 = new TwoNodeOneStnnoInfo { Stn_No = "S814-4", end = SMTC.S3_38, start = SMTC.S3_39 };
            public static TwoNodeOneStnnoInfo S814_1 = new TwoNodeOneStnnoInfo { Stn_No = "S814-1", end = AGV.S3_01, start = SMTC.S3_02 };
            public static TwoNodeOneStnnoInfo S814_2 = new TwoNodeOneStnnoInfo { Stn_No = "S814-2", end = AGV.S3_07, start = SMTC.S3_08 };
            public static TwoNodeOneStnnoInfo S814_6 = new TwoNodeOneStnnoInfo { Stn_No = "S814-6", end = AGV.S4_07, start = SMTC.S4_12 };
            public static TwoNodeOneStnnoInfo S814_5 = new TwoNodeOneStnnoInfo { Stn_No = "S814-5", end = AGV.S4_01, start = SMTC.S4_06 };

            public static TwoNodeOneStnnoInfo S815_4 = new TwoNodeOneStnnoInfo { Stn_No = "S815-4", end = SMTC.S3_42, start = SMTC.S3_43 };
            public static TwoNodeOneStnnoInfo S815_1 = new TwoNodeOneStnnoInfo { Stn_No = "S815-1", end = AGV.S3_13, start = SMTC.S3_14 };
            public static TwoNodeOneStnnoInfo S815_2 = new TwoNodeOneStnnoInfo { Stn_No = "S815-2", end = AGV.S3_19, start = SMTC.S3_20 };
            public static TwoNodeOneStnnoInfo S815_6 = new TwoNodeOneStnnoInfo { Stn_No = "S815-6", end = AGV.S4_19, start = SMTC.S4_24 };
            public static TwoNodeOneStnnoInfo S815_5 = new TwoNodeOneStnnoInfo { Stn_No = "S815-5", end = AGV.S4_13, start = SMTC.S4_18 };

            public static TwoNodeOneStnnoInfo S816_4 = new TwoNodeOneStnnoInfo { Stn_No = "S816-4", end = SMTC.S3_46, start = SMTC.S3_47 };
            public static TwoNodeOneStnnoInfo S816_1 = new TwoNodeOneStnnoInfo { Stn_No = "S816-1", end = AGV.S3_25, start = SMTC.S3_26 };
            public static TwoNodeOneStnnoInfo S816_2 = new TwoNodeOneStnnoInfo { Stn_No = "S816-2", end = AGV.S3_31, start = SMTC.S3_32 };
            public static TwoNodeOneStnnoInfo S816_5 = new TwoNodeOneStnnoInfo { Stn_No = "S816-5", end = AGV.S4_25, start = SMTC.S4_30 };

            public static TwoNodeOneStnnoInfo SF301 = new TwoNodeOneStnnoInfo { Stn_No = "SF301", end = SMT3C.A1_02, start = SMT3C.A1_03 };
            public static TwoNodeOneStnnoInfo SF302 = new TwoNodeOneStnnoInfo { Stn_No = "SF302", end = SMT3C.A1_06, start = SMT3C.A1_07 };
            public static TwoNodeOneStnnoInfo SF303 = new TwoNodeOneStnnoInfo { Stn_No = "SF303", end = SMT3C.A1_10, start = SMT3C.A1_11 };

            public static TwoNodeOneStnnoInfo SF501 = new TwoNodeOneStnnoInfo { Stn_No = "SF501", end = SMT5C.A2_02, start = SMT5C.A2_03 };
            public static TwoNodeOneStnnoInfo SF502 = new TwoNodeOneStnnoInfo { Stn_No = "SF502", end = SMT5C.A2_06, start = SMT5C.A2_07 };
            public static TwoNodeOneStnnoInfo SF503 = new TwoNodeOneStnnoInfo { Stn_No = "SF503", end = SMT5C.A2_10, start = SMT5C.A2_11 };
            public static TwoNodeOneStnnoInfo SF504 = new TwoNodeOneStnnoInfo { Stn_No = "SF504", end = SMT5C.A2_14, start = SMT5C.A2_15 };
            public static TwoNodeOneStnnoInfo SF505 = new TwoNodeOneStnnoInfo { Stn_No = "SF505", end = SMT5C.A2_18, start = SMT5C.A2_19 };

            public static TwoNodeOneStnnoInfo SF601 = new TwoNodeOneStnnoInfo { Stn_No = "SF601", end = SMT6C.A3_02, start = SMT6C.A3_03 };
            public static TwoNodeOneStnnoInfo SF602 = new TwoNodeOneStnnoInfo { Stn_No = "SF602", end = SMT6C.A3_06, start = SMT6C.A3_07 };
            public static TwoNodeOneStnnoInfo SF603 = new TwoNodeOneStnnoInfo { Stn_No = "SF603", end = SMT6C.A3_10, start = SMT6C.A3_11 };
            public static TwoNodeOneStnnoInfo SF604 = new TwoNodeOneStnnoInfo { Stn_No = "SF604", end = SMT6C.A3_14, start = SMT6C.A3_15 };
            public static TwoNodeOneStnnoInfo SF605 = new TwoNodeOneStnnoInfo { Stn_No = "SF605", end = SMT6C.A3_18, start = SMT6C.A3_19 };

        }

        public static string DeviceID_AGV = "";
        public static string[] DeviceID_AGV_Router = new string[] { "63", "65", "66", "68" };
        public static string DeviceID_Tower = "";
        public static string WES_B800CV = "B800CV";

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

            Node_All.Add(Box.B1_037);
            Node_All.Add(Box.B1_041);
            Node_All.Add(Box.B1_045);
            Node_All.Add(Box.B1_054);
            Node_All.Add(Box.B1_117);
            Node_All.Add(Box.B1_121);
            Node_All.Add(Box.B1_125);
            Node_All.Add(Box.B1_134);

            Node_All.Add(Box.B1_009);
            Node_All.Add(Box.B1_012);
            Node_All.Add(Box.B1_021);
            Node_All.Add(Box.B1_024);
            Node_All.Add(Box.B1_033);
            Node_All.Add(Box.B1_036);
            Node_All.Add(Box.B1_089);
            Node_All.Add(Box.B1_092);
            Node_All.Add(Box.B1_101);
            Node_All.Add(Box.B1_104);
            Node_All.Add(Box.B1_113);
            Node_All.Add(Box.B1_116);

            Node_All.Add(Box.B1_062);
            Node_All.Add(Box.B1_067);
            Node_All.Add(Box.B1_142);
            Node_All.Add(Box.B1_147);

            Node_All.Add(PCBA.M1_01);
            Node_All.Add(PCBA.M1_06);
            Node_All.Add(PCBA.M1_11);
            Node_All.Add(PCBA.M1_16);

            Node_All.Add(AGV.A1_01);
            Node_All.Add(SMT3C.A1_02);
            Node_All.Add(SMT3C.A1_03);
            Node_All.Add(AGV.A1_04);
            Node_All.Add(AGV.A1_05);
            Node_All.Add(SMT3C.A1_06);
            Node_All.Add(SMT3C.A1_07);
            Node_All.Add(AGV.A1_08);
            Node_All.Add(AGV.A1_09);
            Node_All.Add(SMT3C.A1_10);
            Node_All.Add(SMT3C.A1_11);
            Node_All.Add(AGV.A1_12);
            Node_All.Add(AGV.A2_01);
            Node_All.Add(SMT5C.A2_02);
            Node_All.Add(SMT5C.A2_03);
            Node_All.Add(AGV.A2_04);
            Node_All.Add(AGV.A2_05);
            Node_All.Add(SMT5C.A2_06);
            Node_All.Add(SMT5C.A2_07);
            Node_All.Add(AGV.A2_08);
            Node_All.Add(AGV.A2_09);
            Node_All.Add(SMT5C.A2_10);
            Node_All.Add(SMT5C.A2_11);
            Node_All.Add(AGV.A2_12);
            Node_All.Add(AGV.A2_13);
            Node_All.Add(SMT5C.A2_14);
            Node_All.Add(SMT5C.A2_15);
            Node_All.Add(AGV.A2_16);
            Node_All.Add(AGV.A2_17);
            Node_All.Add(SMT5C.A2_18);
            Node_All.Add(SMT5C.A2_19);
            Node_All.Add(AGV.A2_20);
            Node_All.Add(AGV.A3_01);
            Node_All.Add(SMT6C.A3_02);
            Node_All.Add(SMT6C.A3_03);
            Node_All.Add(AGV.A3_04);
            Node_All.Add(AGV.A3_05);
            Node_All.Add(SMT6C.A3_06);
            Node_All.Add(SMT6C.A3_07);
            Node_All.Add(AGV.A3_08);
            Node_All.Add(AGV.A3_09);
            Node_All.Add(SMT6C.A3_10);
            Node_All.Add(SMT6C.A3_11);
            Node_All.Add(AGV.A3_12);
            Node_All.Add(AGV.A3_13);
            Node_All.Add(SMT6C.A3_14);
            Node_All.Add(SMT6C.A3_15);
            Node_All.Add(AGV.A3_16);
            Node_All.Add(AGV.A3_17);
            Node_All.Add(SMT6C.A3_18);
            Node_All.Add(SMT6C.A3_19);
            Node_All.Add(AGV.A3_20);
            Node_All.Add(AGV.A4_01);
            Node_All.Add(Line.A4_02);
            Node_All.Add(Line.A4_03);
            Node_All.Add(Line.A4_06);
            Node_All.Add(Line.A4_07);
            Node_All.Add(Line.A4_10);
            Node_All.Add(Line.A4_11);
            Node_All.Add(Line.A4_14);
            Node_All.Add(Line.A4_15);
            Node_All.Add(Line.A4_18);
            Node_All.Add(Line.A4_19);
            Node_All.Add(AGV.A4_04);
            Node_All.Add(AGV.A4_05);
            Node_All.Add(AGV.A4_08);
            Node_All.Add(AGV.A4_09);
            Node_All.Add(AGV.A4_12);
            Node_All.Add(AGV.A4_13);
            Node_All.Add(AGV.A4_16);
            Node_All.Add(AGV.A4_17);
            Node_All.Add(AGV.A4_20);
            Node_All.Add(AGV.B1_070);
            Node_All.Add(AGV.B1_071);
            Node_All.Add(AGV.B1_074);
            Node_All.Add(AGV.B1_075);
            Node_All.Add(AGV.B1_078);
            Node_All.Add(AGV.B1_079);
            Node_All.Add(AGV.E1_01);
            Node_All.Add(Tower.E1_04);
            Node_All.Add(Tower.E1_10);
            Node_All.Add(AGV.E1_08);
            Node_All.Add(AGV.E2_35);
            Node_All.Add(AGV.E2_36);
            Node_All.Add(AGV.E2_37);
            Node_All.Add(AGV.E2_38);
            Node_All.Add(AGV.E2_39);
            Node_All.Add(AGV.E2_44);
            Node_All.Add(E04.LO1_02);
            Node_All.Add(E04.LO1_07);
            Node_All.Add(AGV.LO2_01);
            Node_All.Add(AGV.LO2_04);
            Node_All.Add(AGV.LO3_01);
            Node_All.Add(AGV.LO3_04);
            Node_All.Add(E05.LO3_02);
            Node_All.Add(AGV.LO4_01);
            Node_All.Add(AGV.LO4_04);
            Node_All.Add(AGV.LO5_01);
            Node_All.Add(AGV.LO5_04);
            Node_All.Add(AGV.LO6_01);
            Node_All.Add(AGV.LO6_04);
            Node_All.Add(AGV.M1_05);
            Node_All.Add(AGV.M1_10);
            Node_All.Add(AGV.M1_15);
            Node_All.Add(AGV.M1_20);
            Node_All.Add(AGV.S1_01);
            Node_All.Add(AGV.S1_07);
            Node_All.Add(AGV.S1_13);
            Node_All.Add(AGV.S1_25);
            Node_All.Add(AGV.S1_31);
            Node_All.Add(AGV.S1_37);
            Node_All.Add(SMTC.S1_38);
            Node_All.Add(SMTC.S1_39);
            Node_All.Add(AGV.S1_40);
            Node_All.Add(AGV.S1_41);
            Node_All.Add(SMTC.S1_42);
            Node_All.Add(SMTC.S1_43);
            Node_All.Add(AGV.S1_44);
            Node_All.Add(AGV.S1_45);
            Node_All.Add(SMTC.S1_46);
            Node_All.Add(SMTC.S1_47);
            Node_All.Add(AGV.S1_48);
            Node_All.Add(AGV.S1_49);
            Node_All.Add(AGV.S1_50);
            Node_All.Add(AGV.S2_01);
            Node_All.Add(AGV.S2_07);
            Node_All.Add(AGV.S2_13);
            Node_All.Add(AGV.S2_25);
            Node_All.Add(AGV.S2_31);
            Node_All.Add(AGV.S2_49);
            Node_All.Add(AGV.S3_01);
            Node_All.Add(AGV.S3_07);
            Node_All.Add(AGV.S3_13);
            Node_All.Add(AGV.S3_19);
            Node_All.Add(AGV.S3_25);
            Node_All.Add(AGV.S3_31);
            Node_All.Add(AGV.S3_37);
            Node_All.Add(SMTC.S3_38);
            Node_All.Add(SMTC.S3_39);
            Node_All.Add(AGV.S3_40);
            Node_All.Add(AGV.S3_41);
            Node_All.Add(SMTC.S3_42);
            Node_All.Add(SMTC.S3_43);
            Node_All.Add(AGV.S3_44);
            Node_All.Add(AGV.S3_45);
            Node_All.Add(SMTC.S3_46);
            Node_All.Add(SMTC.S3_47);
            Node_All.Add(AGV.S3_48);
            Node_All.Add(AGV.S3_49);
            Node_All.Add(AGV.S4_01);
            Node_All.Add(AGV.S4_07);
            Node_All.Add(AGV.S4_13);
            Node_All.Add(AGV.S4_19);
            Node_All.Add(AGV.S4_25);
            Node_All.Add(AGV.S4_49);
            Node_All.Add(AGV.S4_50);
            Node_All.Add(AGV.S5_01);
            Node_All.Add(AGV.S5_07);
            Node_All.Add(AGV.S5_37);
            Node_All.Add(SMTC.S5_38);
            Node_All.Add(SMTC.S5_39);
            Node_All.Add(AGV.S5_40);
            Node_All.Add(AGV.S5_49);
            Node_All.Add(AGV.S6_01);
            Node_All.Add(AGV.S6_07);
            Node_All.Add(AGV.S0_01);
            Node_All.Add(SMTC.S0_02);
            Node_All.Add(SMTC.S0_03);
            Node_All.Add(AGV.S0_04);
            Node_All.Add(AGV.S0_05);
            Node_All.Add(SMTC.S3_02);
            Node_All.Add(SMTC.S3_08);
            Node_All.Add(SMTC.S3_14);
            Node_All.Add(SMTC.S3_20);
            Node_All.Add(SMTC.S3_26);
            Node_All.Add(SMTC.S3_32);
            Node_All.Add(SMTC.S1_02);
            Node_All.Add(SMTC.S1_08);
            Node_All.Add(SMTC.S1_14);
            Node_All.Add(SMTC.S1_26);
            Node_All.Add(SMTC.S1_32);
            Node_All.Add(SMTC.S2_06);
            Node_All.Add(SMTC.S2_12);
            Node_All.Add(SMTC.S2_18);
            Node_All.Add(SMTC.S2_30);
            Node_All.Add(SMTC.S2_36);
            Node_All.Add(SMTC.S4_06);
            Node_All.Add(SMTC.S4_12);
            Node_All.Add(SMTC.S4_18);
            Node_All.Add(SMTC.S4_24);
            Node_All.Add(SMTC.S4_30);
        }

        private static List<ConveyorInfo> AGV_All = new List<ConveyorInfo>();

        private static List<TwoNodeOneStnnoInfo> Sharing_Node = new List<TwoNodeOneStnnoInfo>();
        public static List<TwoNodeOneStnnoInfo> GetSharingNode() => Sharing_Node;
        public static void FunSharingNode_ListInit()
        {
            Sharing_Node.Add(SharingNode.S800_12);
            Sharing_Node.Add(SharingNode.S800_M1);
            Sharing_Node.Add(SharingNode.S800_M2);
            Sharing_Node.Add(SharingNode.S800_B1);
            Sharing_Node.Add(SharingNode.S800_B2);

            Sharing_Node.Add(SharingNode.S811_4);
            Sharing_Node.Add(SharingNode.S811_1);
            Sharing_Node.Add(SharingNode.S811_2);
            Sharing_Node.Add(SharingNode.S811_6);
            Sharing_Node.Add(SharingNode.S811_5);

            Sharing_Node.Add(SharingNode.S812_4);
            Sharing_Node.Add(SharingNode.S812_1);
            Sharing_Node.Add(SharingNode.S812_5);

            Sharing_Node.Add(SharingNode.S813_4);
            Sharing_Node.Add(SharingNode.S813_1);
            Sharing_Node.Add(SharingNode.S813_2);
            Sharing_Node.Add(SharingNode.S813_6);
            Sharing_Node.Add(SharingNode.S813_5);

            Sharing_Node.Add(SharingNode.S814_4);
            Sharing_Node.Add(SharingNode.S814_1);
            Sharing_Node.Add(SharingNode.S814_2);
            Sharing_Node.Add(SharingNode.S814_6);
            Sharing_Node.Add(SharingNode.S814_5);

            Sharing_Node.Add(SharingNode.S815_4);
            Sharing_Node.Add(SharingNode.S815_1);
            Sharing_Node.Add(SharingNode.S815_2);
            Sharing_Node.Add(SharingNode.S815_6);
            Sharing_Node.Add(SharingNode.S815_5);

            Sharing_Node.Add(SharingNode.S816_4);
            Sharing_Node.Add(SharingNode.S816_1);
            Sharing_Node.Add(SharingNode.S816_2);
            Sharing_Node.Add(SharingNode.S816_5);

            Sharing_Node.Add(SharingNode.SF301);
            Sharing_Node.Add(SharingNode.SF302);
            Sharing_Node.Add(SharingNode.SF303);

            Sharing_Node.Add(SharingNode.SF501);
            Sharing_Node.Add(SharingNode.SF502);
            Sharing_Node.Add(SharingNode.SF503);
            Sharing_Node.Add(SharingNode.SF504);
            Sharing_Node.Add(SharingNode.SF505);

            Sharing_Node.Add(SharingNode.SF601);
            Sharing_Node.Add(SharingNode.SF602);
            Sharing_Node.Add(SharingNode.SF603);
            Sharing_Node.Add(SharingNode.SF604);
            Sharing_Node.Add(SharingNode.SF605);

            foreach(var c in Sharing_Node)
            {
                c.Stn_No = c.end.StnNo == "" ? c.Stn_No : c.end.StnNo;
            }
            FunSharingNode_3FListInit();
            FunSharingNode_5FListInit();
            FunSharingNode_6FListInit();
        }

        private static List<TwoNodeOneStnnoInfo> Sharing_Node_3F = new List<TwoNodeOneStnnoInfo>();
        public static List<TwoNodeOneStnnoInfo> GetSharingNode3F() => Sharing_Node_3F;
        public static void FunSharingNode_3FListInit()
        {
            Sharing_Node_3F.Add(SharingNode.SF301);
            Sharing_Node_3F.Add(SharingNode.SF302);
            Sharing_Node_3F.Add(SharingNode.SF303);
        }

        private static List<TwoNodeOneStnnoInfo> Sharing_Node_5F = new List<TwoNodeOneStnnoInfo>();
        public static List<TwoNodeOneStnnoInfo> GetSharingNode5F() => Sharing_Node_5F;
        public static void FunSharingNode_5FListInit()
        {
            Sharing_Node_5F.Add(SharingNode.SF501);
            Sharing_Node_5F.Add(SharingNode.SF502);
            Sharing_Node_5F.Add(SharingNode.SF503);
            Sharing_Node_5F.Add(SharingNode.SF504);
            Sharing_Node_5F.Add(SharingNode.SF505);
        }

        private static List<TwoNodeOneStnnoInfo> Sharing_Node_6F = new List<TwoNodeOneStnnoInfo>();
        public static List<TwoNodeOneStnnoInfo> GetSharingNode6F() => Sharing_Node_6F;
        public static void FunSharingNode_6FListInit()
        {
            Sharing_Node_6F.Add(SharingNode.SF601);
            Sharing_Node_6F.Add(SharingNode.SF602);
            Sharing_Node_6F.Add(SharingNode.SF603);
            Sharing_Node_6F.Add(SharingNode.SF604);
            Sharing_Node_6F.Add(SharingNode.SF605);
        }

        public static void FunAGVListInit()
        {
            FunAGVSendToCVend();
            FunAGV_8FPort();
            FunAGV_6Fport();
            FunAGV_5Fport();
            FunAGV_3Fport();
            FunNode_6F();
            FunNode_5F();
            FunNode_3F();
            foreach (var s in Node_3F)
            {
                s.DeviceId = "SMT3C";
            }
            foreach (var s in Node_5F)
            {
                s.DeviceId = "SMT5C";
            }
            foreach (var s in Node_6F)
            {
                s.DeviceId = "SMT6C";
            }
            foreach (var s in AGV_3FPort)
            {
                s.DeviceId = "63";
            }
            foreach (var s in AGV_5FPort)
            {
                s.DeviceId = "65";
            }
            foreach (var s in AGV_6FPort)
            {
                s.DeviceId = "66";
            }
            foreach (var s in AGV_8FPort)
            {
                s.DeviceId = "68";
            }
        }

        private static List<ConveyorInfo> AGV_SendToCVend = new List<ConveyorInfo>();
        public static void FunAGVSendToCVend()
        {
            //八樓線邊倉
            AGV_SendToCVend.Add(AGV.A4_01);
            AGV_SendToCVend.Add(AGV.A4_05);
            AGV_SendToCVend.Add(AGV.A4_09);
            AGV_SendToCVend.Add(AGV.A4_13);
            AGV_SendToCVend.Add(AGV.A4_17);

            //八樓SMTC產線
            AGV_SendToCVend.Add(AGV.S1_37);
            AGV_SendToCVend.Add(AGV.S1_41);
            AGV_SendToCVend.Add(AGV.S1_45);
            AGV_SendToCVend.Add(AGV.S3_37);
            AGV_SendToCVend.Add(AGV.S3_41);
            AGV_SendToCVend.Add(AGV.S3_45);
            AGV_SendToCVend.Add(AGV.S5_37);
            AGV_SendToCVend.Add(AGV.S0_01);

            //六樓SMT6C
            AGV_SendToCVend.Add(AGV.A3_01);
            AGV_SendToCVend.Add(AGV.A3_05);
            AGV_SendToCVend.Add(AGV.A3_09);
            AGV_SendToCVend.Add(AGV.A3_13);
            AGV_SendToCVend.Add(AGV.A3_17);

            //五樓SMT5C
            AGV_SendToCVend.Add(AGV.A2_01);
            AGV_SendToCVend.Add(AGV.A2_05);
            AGV_SendToCVend.Add(AGV.A2_09);
            AGV_SendToCVend.Add(AGV.A2_13);
            AGV_SendToCVend.Add(AGV.A2_17);

            //三樓SMT3C
            AGV_SendToCVend.Add(AGV.A1_01);
            AGV_SendToCVend.Add(AGV.A1_05);
            AGV_SendToCVend.Add(AGV.A1_09);

        }

        public static List<ConveyorInfo> GetAGV_SendToCVend() => AGV_SendToCVend;

        private static List<ConveyorInfo> AGV_8FPort = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetAGV_8FPort() => AGV_8FPort;
        public static void FunAGV_8FPort()
        {
            //八樓線邊倉
            AGV_8FPort.Add(AGV.A4_01);
            AGV_8FPort.Add(AGV.A4_04);
            AGV_8FPort.Add(AGV.A4_05);
            AGV_8FPort.Add(AGV.A4_08);
            AGV_8FPort.Add(AGV.A4_09);
            AGV_8FPort.Add(AGV.A4_12);
            AGV_8FPort.Add(AGV.A4_13);
            AGV_8FPort.Add(AGV.A4_16);
            AGV_8FPort.Add(AGV.A4_17);
            AGV_8FPort.Add(AGV.A4_20);

            //八樓SMTC產線
            AGV_8FPort.Add(AGV.S1_01);
            AGV_8FPort.Add(AGV.S1_07);
            AGV_8FPort.Add(AGV.S1_13);
            AGV_8FPort.Add(AGV.S1_25);
            AGV_8FPort.Add(AGV.S1_31);
            AGV_8FPort.Add(AGV.S1_37);
            AGV_8FPort.Add(AGV.S1_40);
            AGV_8FPort.Add(AGV.S1_41);
            AGV_8FPort.Add(AGV.S1_44);
            AGV_8FPort.Add(AGV.S1_45);
            AGV_8FPort.Add(AGV.S1_48);
            AGV_8FPort.Add(AGV.S1_49);
            AGV_8FPort.Add(AGV.S1_50);

            AGV_8FPort.Add(AGV.S2_01);
            AGV_8FPort.Add(AGV.S2_07);
            AGV_8FPort.Add(AGV.S2_13);
            AGV_8FPort.Add(AGV.S2_25);
            AGV_8FPort.Add(AGV.S2_31);
            AGV_8FPort.Add(AGV.S2_49);

            AGV_8FPort.Add(AGV.S3_01);
            AGV_8FPort.Add(AGV.S3_07);
            AGV_8FPort.Add(AGV.S3_13);
            AGV_8FPort.Add(AGV.S3_19);
            AGV_8FPort.Add(AGV.S3_25);
            AGV_8FPort.Add(AGV.S3_31);
            AGV_8FPort.Add(AGV.S3_37);
            AGV_8FPort.Add(AGV.S3_40);
            AGV_8FPort.Add(AGV.S3_41);
            AGV_8FPort.Add(AGV.S3_44);
            AGV_8FPort.Add(AGV.S3_45);
            AGV_8FPort.Add(AGV.S3_48);
            AGV_8FPort.Add(AGV.S3_49);

            AGV_8FPort.Add(AGV.S4_01);
            AGV_8FPort.Add(AGV.S4_07);
            AGV_8FPort.Add(AGV.S4_13);
            AGV_8FPort.Add(AGV.S4_19);
            AGV_8FPort.Add(AGV.S4_25);
            AGV_8FPort.Add(AGV.S4_49);
            AGV_8FPort.Add(AGV.S4_50);


            AGV_8FPort.Add(AGV.S5_01);
            AGV_8FPort.Add(AGV.S5_07);
            AGV_8FPort.Add(AGV.S5_37);
            AGV_8FPort.Add(AGV.S5_40);
            AGV_8FPort.Add(AGV.S5_49);

            AGV_8FPort.Add(AGV.S6_01);
            AGV_8FPort.Add(AGV.S6_07);

            AGV_8FPort.Add(AGV.S0_01);
            AGV_8FPort.Add(AGV.S0_04);
            AGV_8FPort.Add(AGV.S0_05);

            //箱式倉
            AGV_8FPort.Add(AGV.B1_070);
            AGV_8FPort.Add(AGV.B1_074);
            AGV_8FPort.Add(AGV.B1_078);
            AGV_8FPort.Add(AGV.B1_071);
            AGV_8FPort.Add(AGV.B1_075);
            AGV_8FPort.Add(AGV.B1_079);

            //PCBA
            AGV_8FPort.Add(AGV.M1_05);
            AGV_8FPort.Add(AGV.M1_10);
            AGV_8FPort.Add(AGV.M1_15);
            AGV_8FPort.Add(AGV.M1_20);


            //電子料塔
            AGV_8FPort.Add(AGV.E1_01);
            AGV_8FPort.Add(AGV.E1_08);
            AGV_8FPort.Add(AGV.E2_35);
            AGV_8FPort.Add(AGV.E2_36);
            AGV_8FPort.Add(AGV.E2_37);
            AGV_8FPort.Add(AGV.E2_38);
            AGV_8FPort.Add(AGV.E2_39);

            //4號電梯
            AGV_8FPort.Add(AGV.LO2_01);
            AGV_8FPort.Add(AGV.LO2_04);

            //5號電梯
            AGV_8FPort.Add(AGV.LO3_01);
            AGV_8FPort.Add(AGV.LO3_04);
        }

        private static List<ConveyorInfo> AGV_6FPort = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetAGV_6FPort() => AGV_6FPort;
        public static void FunAGV_6Fport()
        {
            //線邊倉
            
            AGV_6FPort.Add(AGV.A3_01);
            AGV_6FPort.Add(AGV.A3_04);
            AGV_6FPort.Add(AGV.A3_05);
            AGV_6FPort.Add(AGV.A3_08);
            AGV_6FPort.Add(AGV.A3_09);
            AGV_6FPort.Add(AGV.A3_12);
            AGV_6FPort.Add(AGV.A3_13);
            AGV_6FPort.Add(AGV.A3_16);
            AGV_6FPort.Add(AGV.A3_17);
            AGV_6FPort.Add(AGV.A3_20);
            
            /*
            AGV_6FPort.Add(SMT6C.A3_02);
            AGV_6FPort.Add(SMT6C.A3_03);
            AGV_6FPort.Add(SMT6C.A3_06);
            AGV_6FPort.Add(SMT6C.A3_07);
            AGV_6FPort.Add(SMT6C.A3_10);
            AGV_6FPort.Add(SMT6C.A3_11);
            AGV_6FPort.Add(SMT6C.A3_14);
            AGV_6FPort.Add(SMT6C.A3_15);
            AGV_6FPort.Add(SMT6C.A3_18);
            AGV_6FPort.Add(SMT6C.A3_19);
            */

            //5號電梯
            AGV_6FPort.Add(AGV.LO6_01);
            AGV_6FPort.Add(AGV.LO6_04);

        }

        private static List<ConveyorInfo> Node_6F = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetNode_6F() => Node_6F;
        public static void FunNode_6F()
        {
            //線邊倉

            Node_6F.Add(AGV.A3_01);
            Node_6F.Add(AGV.A3_04);
            Node_6F.Add(AGV.A3_05);
            Node_6F.Add(AGV.A3_08);
            Node_6F.Add(AGV.A3_09);
            Node_6F.Add(AGV.A3_12);
            Node_6F.Add(AGV.A3_13);
            Node_6F.Add(AGV.A3_16);
            Node_6F.Add(AGV.A3_17);
            Node_6F.Add(AGV.A3_20);


            Node_6F.Add(SMT6C.A3_02);
            Node_6F.Add(SMT6C.A3_03);
            Node_6F.Add(SMT6C.A3_06);
            Node_6F.Add(SMT6C.A3_07);
            Node_6F.Add(SMT6C.A3_10);
            Node_6F.Add(SMT6C.A3_11);
            Node_6F.Add(SMT6C.A3_14);
            Node_6F.Add(SMT6C.A3_15);
            Node_6F.Add(SMT6C.A3_18);
            Node_6F.Add(SMT6C.A3_19);


            //5號電梯
            Node_6F.Add(AGV.LO6_01);
            Node_6F.Add(AGV.LO6_04);

        }

        private static List<ConveyorInfo> AGV_5FPort = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetAGV_5FPort() => AGV_5FPort;
        public static void FunAGV_5Fport()
        {
            //線邊倉
            
            AGV_5FPort.Add(AGV.A2_01);
            AGV_5FPort.Add(AGV.A2_04);
            AGV_5FPort.Add(AGV.A2_05);
            AGV_5FPort.Add(AGV.A2_08);
            AGV_5FPort.Add(AGV.A2_09);
            AGV_5FPort.Add(AGV.A2_12);
            AGV_5FPort.Add(AGV.A2_13);
            AGV_5FPort.Add(AGV.A2_16);
            AGV_5FPort.Add(AGV.A2_17);
            AGV_5FPort.Add(AGV.A2_20);
            /*
            AGV_5FPort.Add(SMT5C.A2_02);
            AGV_5FPort.Add(SMT5C.A2_03);
            AGV_5FPort.Add(SMT5C.A2_06);
            AGV_5FPort.Add(SMT5C.A2_07);
            AGV_5FPort.Add(SMT5C.A2_10);
            AGV_5FPort.Add(SMT5C.A2_11);
            AGV_5FPort.Add(SMT5C.A2_14);
            AGV_5FPort.Add(SMT5C.A2_15);
            AGV_5FPort.Add(SMT5C.A2_18);
            AGV_5FPort.Add(SMT5C.A2_19);
            */
            //5號電梯
            AGV_5FPort.Add(AGV.LO5_01);
            AGV_5FPort.Add(AGV.LO5_04);

        }
        private static List<ConveyorInfo> Node_5F = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetNode_5F() => Node_5F;
        public static void FunNode_5F()
        {
            //線邊倉

            Node_5F.Add(AGV.A2_01);
            Node_5F.Add(AGV.A2_04);
            Node_5F.Add(AGV.A2_05);
            Node_5F.Add(AGV.A2_08);
            Node_5F.Add(AGV.A2_09);
            Node_5F.Add(AGV.A2_12);
            Node_5F.Add(AGV.A2_13);
            Node_5F.Add(AGV.A2_16);
            Node_5F.Add(AGV.A2_17);
            Node_5F.Add(AGV.A2_20);

            Node_5F.Add(SMT5C.A2_02);
            Node_5F.Add(SMT5C.A2_03);
            Node_5F.Add(SMT5C.A2_06);
            Node_5F.Add(SMT5C.A2_07);
            Node_5F.Add(SMT5C.A2_10);
            Node_5F.Add(SMT5C.A2_11);
            Node_5F.Add(SMT5C.A2_14);
            Node_5F.Add(SMT5C.A2_15);
            Node_5F.Add(SMT5C.A2_18);
            Node_5F.Add(SMT5C.A2_19);

            //5號電梯
            Node_5F.Add(AGV.LO5_01);
            Node_5F.Add(AGV.LO5_04);

        }

        private static List<ConveyorInfo> AGV_3FPort = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetAGV_3FPort() => AGV_3FPort;
        public static void FunAGV_3Fport()
        {
            //線邊倉
            AGV_3FPort.Add(AGV.A1_01);
            AGV_3FPort.Add(AGV.A1_04);
            AGV_3FPort.Add(AGV.A1_05);
            AGV_3FPort.Add(AGV.A1_08);
            AGV_3FPort.Add(AGV.A1_09);
            AGV_3FPort.Add(AGV.A1_12);

            /*
            AGV_3FPort.Add(SMT3C.A1_02);
            AGV_3FPort.Add(SMT3C.A1_03);
            AGV_3FPort.Add(SMT3C.A1_06);
            AGV_3FPort.Add(SMT3C.A1_07);
            AGV_3FPort.Add(SMT3C.A1_10);
            AGV_3FPort.Add(SMT3C.A1_11);
            */

            //5號電梯
            AGV_3FPort.Add(AGV.LO4_01);
            AGV_3FPort.Add(AGV.LO4_04);

        }
        private static List<ConveyorInfo> Node_3F = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetNode_3F() => Node_3F;
        public static void FunNode_3F()
        {
            //線邊倉
            Node_3F.Add(AGV.A1_01);
            Node_3F.Add(AGV.A1_04);
            Node_3F.Add(AGV.A1_05);
            Node_3F.Add(AGV.A1_08);
            Node_3F.Add(AGV.A1_09);
            Node_3F.Add(AGV.A1_12);


            Node_3F.Add(SMT3C.A1_02);
            Node_3F.Add(SMT3C.A1_03);
            Node_3F.Add(SMT3C.A1_06);
            Node_3F.Add(SMT3C.A1_07);
            Node_3F.Add(SMT3C.A1_10);
            Node_3F.Add(SMT3C.A1_11);


            //5號電梯
            Node_3F.Add(AGV.LO4_01);
            Node_3F.Add(AGV.LO4_04);

        }

        private static List<ConveyorInfo> B800CV = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetB800CV_List() => B800CV;
        private static int Current = 1;
        public static ConveyorInfo GetB800CV()
        {
            int count = 1;
            foreach(var s in B800CV)
            {
                if(count == Current)
                {
                    Current++;
                    if (Current > B800CV.Count()) Current = 1;

                    return s;
                }

                count++;
            }

            return new ConveyorInfo();
        }

        private static void FunB800CVAddInit()
        {
            B800CV.Add(AGV.B1_070);
            B800CV.Add(AGV.B1_074);
            B800CV.Add(AGV.B1_078);
        }
        private static List<ConveyorInfo> B800CVOut = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetB800CVOut_List() => B800CVOut;
        private static int Current_Out = 1;
        public static ConveyorInfo GetB800CVOut()
        {
            int count = 1;
            foreach (var s in B800CVOut)
            {
                if (count == Current_Out)
                {
                    Current_Out++;
                    if (Current_Out > B800CVOut.Count()) Current_Out = 1;

                    return s;
                }

                count++;
            }

            return new ConveyorInfo();
        }

        private static void FunB800CVOutAddInit()
        {
            B800CVOut.Add(AGV.B1_071);
            B800CVOut.Add(AGV.B1_075);
            B800CVOut.Add(AGV.B1_079);
        }
        public static List<ConveyorInfo> Node_Lift = new List<ConveyorInfo>();
        public static List<ConveyorInfo> GetLifetNode_List() => Node_Lift;
        public static void FunLiftNodeInit()
        {
            Node_Lift.Add(E04.LO1_02);
            Node_Lift.Add(E04.LO1_07);
            Node_Lift.Add(AGV.LO2_01);
            Node_Lift.Add(AGV.LO2_04);
            Node_Lift.Add(AGV.LO3_01);
            Node_Lift.Add(E05.LO3_02);
            Node_Lift.Add(AGV.LO3_04);
            Node_Lift.Add(AGV.LO4_01);
            Node_Lift.Add(AGV.LO4_04);
            Node_Lift.Add(AGV.LO5_01);
            Node_Lift.Add(AGV.LO5_04);
            Node_Lift.Add(AGV.LO6_01);
            Node_Lift.Add(AGV.LO6_04);
        }
        private static List<ConveyorInfo> Stations = new List<ConveyorInfo>();
        /// <summary>
        /// 人員工作站List
        /// </summary>
        /// <returns></returns>
        public static List<ConveyorInfo> GetStations() => Stations;
        public static void FunStnListAddInit()
        {
            FunB800CVAddInit();
            FunB800CVOutAddInit();
            FunLiftNodeInit();

            Stations.Add(E04.LO1_02);
            Stations.Add(E04.LO1_07);
            Stations.Add(AGV.LO3_01);
            Stations.Add(Box.B1_062);
            Stations.Add(Box.B1_067);
            Stations.Add(AGV.B1_070);
            //Stations.Add(AGV.B1_071);
            Stations.Add(AGV.B1_074);
            Stations.Add(AGV.B1_078);
            //Stations.Add(AGV.B1_079);
            Stations.Add(Box.B1_142);
            Stations.Add(Box.B1_147);
            Stations.Add(SMT3C.A1_02);
            Stations.Add(SMT3C.A1_03);
            Stations.Add(SMT3C.A1_06);
            Stations.Add(SMT3C.A1_07);
            Stations.Add(SMT3C.A1_10);
            Stations.Add(SMT3C.A1_11);
            Stations.Add(SMT5C.A2_02);
            Stations.Add(SMT5C.A2_03);
            Stations.Add(SMT5C.A2_06);
            Stations.Add(SMT5C.A2_07);
            Stations.Add(SMT5C.A2_10);
            Stations.Add(SMT5C.A2_11);
            Stations.Add(SMT5C.A2_14);
            Stations.Add(SMT5C.A2_15);
            Stations.Add(SMT5C.A2_18);
            Stations.Add(SMT5C.A2_19);
            Stations.Add(SMT6C.A3_02);
            Stations.Add(SMT6C.A3_03);
            Stations.Add(SMT6C.A3_06);
            Stations.Add(SMT6C.A3_07);
            Stations.Add(SMT6C.A3_10);
            Stations.Add(SMT6C.A3_11);
            Stations.Add(SMT6C.A3_14);
            Stations.Add(SMT6C.A3_15);
            Stations.Add(SMT6C.A3_18);
            Stations.Add(SMT6C.A3_19);
            Stations.Add(AGV.S0_05);
            Stations.Add(SMTC.S0_02);
            Stations.Add(SMTC.S0_03);
            Stations.Add(SMTC.S3_02);
            Stations.Add(SMTC.S3_08);
            Stations.Add(SMTC.S3_14);
            Stations.Add(SMTC.S3_20);
            Stations.Add(SMTC.S3_26);
            Stations.Add(SMTC.S3_32);
            Stations.Add(SMTC.S1_02);
            Stations.Add(SMTC.S1_08);
            Stations.Add(SMTC.S1_14);
            Stations.Add(SMTC.S1_26);
            Stations.Add(SMTC.S1_32);
            Stations.Add(SMTC.S2_06);
            Stations.Add(SMTC.S2_12);
            Stations.Add(SMTC.S2_18);
            Stations.Add(SMTC.S2_30);
            Stations.Add(SMTC.S2_36);
            Stations.Add(SMTC.S4_06);
            Stations.Add(SMTC.S4_12);
            Stations.Add(SMTC.S4_18);
            Stations.Add(SMTC.S4_24);
            Stations.Add(SMTC.S4_30);

            Stations.Add(AGV.S1_01);
            Stations.Add(AGV.S1_07);
            Stations.Add(AGV.S1_13);
            Stations.Add(AGV.S1_25);
            Stations.Add(AGV.S1_31);
            Stations.Add(SMTC.S1_38);
            Stations.Add(SMTC.S1_39);
            Stations.Add(SMTC.S1_42);
            Stations.Add(SMTC.S1_43);
            Stations.Add(SMTC.S1_46);
            Stations.Add(SMTC.S1_47);
            Stations.Add(AGV.S1_49);
            Stations.Add(AGV.S1_50);
            Stations.Add(AGV.S2_01);
            Stations.Add(AGV.S2_07);
            Stations.Add(AGV.S2_13);
            Stations.Add(AGV.S2_25);
            Stations.Add(AGV.S2_31);
            Stations.Add(AGV.S2_49);
            Stations.Add(AGV.S3_01);
            Stations.Add(AGV.S3_07);
            Stations.Add(AGV.S3_13);
            Stations.Add(AGV.S3_19);
            Stations.Add(AGV.S3_25);
            Stations.Add(AGV.S3_31);
            Stations.Add(SMTC.S3_38);
            Stations.Add(SMTC.S3_39);
            Stations.Add(SMTC.S3_42);
            Stations.Add(SMTC.S3_43);
            Stations.Add(SMTC.S3_46);
            Stations.Add(SMTC.S3_47);
            Stations.Add(AGV.S3_49);
            Stations.Add(AGV.S4_01);
            Stations.Add(AGV.S4_07);
            Stations.Add(AGV.S4_13);
            Stations.Add(AGV.S4_19);
            Stations.Add(AGV.S4_25);
            Stations.Add(AGV.S4_49);
            Stations.Add(AGV.S4_50);
            Stations.Add(SMTC.S5_38);
            Stations.Add(SMTC.S5_39);
            Stations.Add(Line.A4_02);
            Stations.Add(Line.A4_03);
            Stations.Add(Line.A4_06);
            Stations.Add(Line.A4_07);
            Stations.Add(Line.A4_10);
            Stations.Add(Line.A4_11);
            Stations.Add(Line.A4_14);
            Stations.Add(Line.A4_15);
            Stations.Add(Line.A4_18);
            Stations.Add(Line.A4_19);
            Stations.Add(Tower.E1_04);
            Stations.Add(Tower.E1_10);
            Stations.Add(AGV.E2_35);
            Stations.Add(AGV.E2_36);
            Stations.Add(AGV.E2_37);
            Stations.Add(AGV.E2_38);
            Stations.Add(AGV.E2_39);
            Stations.Add(AGV.E2_44);
            Stations.Add(AGV.M1_10);
            Stations.Add(AGV.M1_20);
            Stations.Add(AGV.M1_05);
            Stations.Add(AGV.M1_15);
        }

        public static ConveyorInfo GetBuffer_ByStnNo(string StnNo)
        {
            var StnList = Stations.Where(r => r.StnNo == StnNo);
            foreach (var s in StnList)
            {
                return s;
            }

            return new ConveyorInfo();
        }

        public static bool FunCheckInAGVSendToCVEnd(string sBufferName)
        {
            if(AGV_SendToCVend.Where(r => r.BufferName == sBufferName).Any())
            {
                return true;
            }
            return false;
        }

        public static ConveyorInfo GetBuffer(string BufferName)
        {
            var lst = GetAllNode().Where(r => r.BufferName == BufferName);
            foreach (var con in lst)
            {
                return con;
            }

            return new ConveyorInfo();
        }

        public static ConveyorInfo GetBufferByDevice(string DeviceId)
        {
            var lst = GetAllNode().Where(r => r.bufferLocation.DeviceId == DeviceId);
            foreach (var con in lst)
            {
                return con;
            }

            return new ConveyorInfo();
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

        public static TwoNodeOneStnnoInfo GetTwoNodeOneStnnoByStnNo(string sStnNo)
        {
            var v = GetSharingNode().Where(r => r.Stn_No == sStnNo);
            foreach (var s in v)
            {
                return s;
            }
            return new TwoNodeOneStnnoInfo();
        }
        public static TwoNodeOneStnnoInfo GetTwoNodeOneStnnoByBufferName(string sBufferName)
        {
            var v1 = GetSharingNode().Where(r => r.end.BufferName == sBufferName);
            foreach (var s in v1)
            {
                return s;
            }
            var v2 = GetSharingNode().Where(r => r.start.BufferName == sBufferName);
            foreach (var s in v2)
            {
                return s;
            }
            return new TwoNodeOneStnnoInfo();
        }
    }
}
