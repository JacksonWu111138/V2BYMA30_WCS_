using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Mirle.ASRS.Conveyor.U2NMMA30
{
    public class clsTool
    {
        public static int GetOffset_TrayID(int BufferIndex)
        {
            switch (BufferIndex)
            {
                case 5:
                    return 0;
                case 11:
                    return 5;
                case 17:
                    return 10;
                case 23:
                    return 15;
                case 41:
                    return 20;
                case 42:
                    return 25;
                case 43:
                    return 30;
                case 44:
                    return 35;
                case 4:
                    return 40;
                case 10:
                    return 45;
                case 16:
                    return 50;
                case 22:
                    return 55;
                default:
                    return 60;
            }
        }

        public static int GetOffset_LittleThings(int BufferIndex)
        {
            switch (BufferIndex)
            {
                case 41:
                    return 0;
                case 42:
                    return 48;
                case 43:
                    return 96;
                case 44:
                    return 144;
                default:
                    return 150;
            }
        }

        public static int GetOffset_FOB(int BufferIndex)
        {
            switch (BufferIndex)
            {
                case 41:
                    return 0;
                case 42:
                    return 12;
                case 43:
                    return 24;
                case 44:
                    return 36;
                default:
                    return 40;
            }
        }

        /// <summary>
        /// 將數值轉成列舉值
        /// </summary>
        /// <typeparam name="TEnum">列舉Type</typeparam>
        /// <param name="EnumAsString">列舉數值</param>
        /// <returns>傳回列舉值</returns>
        public static TEnum funGetEnumValue<TEnum>(int EnumAsInt) where TEnum : struct
        {
            TEnum enumType = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            Enum.TryParse<TEnum>(EnumAsInt.ToString(), out enumType);
            return enumType;
        }

        public static void Signal_Show(bool bFlag, ref Label label)
        {
            if (bFlag == true) label.BackColor = Color.Yellow;
            else label.BackColor = Color.Transparent;
        }
    }
}
