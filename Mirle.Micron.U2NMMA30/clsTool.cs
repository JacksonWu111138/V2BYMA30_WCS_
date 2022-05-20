using System;
using Mirle.Structure;
using System.Linq;
using Mirle.Def.U2NMMA30;
using Mirle.Def;
using Mirle.ASRS.Conveyor.U2NMMA30;
using System.Data;

namespace Mirle.Micron.U2NMMA30
{
    public class clsTool
    {
        public static string FunTaskLocToWmsLoc(string sTaskLoc, string DeviceID)
        {
            try
            {
                int StockerID = int.Parse(DeviceID);
                string sRow_X = sTaskLoc.Substring(0, 2);
                int iRow = Convert.ToInt32(sRow_X);
                return (iRow + (4 * (StockerID - 1))).ToString().PadLeft(2, '0') + sTaskLoc.Substring(2);
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return string.Empty;
            }
        }

        public static string FunChangeLoc_byTask(string sLoc)
        {
            string sLoc_Task;
            string sRow = sLoc.Substring(0, 2);
            int iRow = int.Parse(sRow);
            switch (iRow)
            {
                case 1:
                case 5:
                case 9:
                case 13:
                    sLoc_Task = "01" + sLoc.Substring(2);
                    break;
                case 2:
                case 6:
                case 10:
                case 14:
                    sLoc_Task = "02" + sLoc.Substring(2);
                    break;
                case 3:
                case 7:
                case 11:
                    sLoc_Task = "03" + sLoc.Substring(2);
                    break;
                case 4:
                case 8:
                case 12:
                    sLoc_Task = "04" + sLoc.Substring(2);
                    break;
                default:
                    sLoc_Task = "";
                    break;
            }

            return sLoc_Task;
        }

        /// <summary>
        /// 依儲位取得Equ_No
        /// </summary>
        /// <param name="sLoc">儲位編號</param>
        /// <returns></returns>
        public static int funGetEquNoByLoc(string sLoc)
        {
            int iCrn = 0;
            string sRow_X = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(sLoc))
                {
                    sRow_X = sLoc.Substring(0, 2);
                    int iRow = Convert.ToInt32(sRow_X);
                    if (iRow % 4 == 0) iCrn = iRow / 4;
                    else iCrn = iRow / 4 + 1;

                    return iCrn;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// 確認該Fork是否可到此儲位
        /// </summary>
        /// <param name="sLoc"></param>
        /// <param name="StockerID"></param>
        /// <param name="fork"></param>
        /// <returns></returns>
        public static bool CheckForkCanDo(string sLoc, int StockerID, int fork)
        {
            try
            {
                string sBay = sLoc.Substring(2, 3);
                int iBay = int.Parse(sBay);
                if (iBay >= clsMicronStocker.GetStockerById(StockerID).GetCraneById(1).GetForkById(fork).GetConfig().Limit.Start &&
                    iBay <= clsMicronStocker.GetStockerById(StockerID).GetCraneById(1).GetForkById(fork).GetConfig().Limit.End)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                return false;
            }
        }

        public static bool IsLimit(string sLoc, ref clsEnum.Fork fork)
        {
            int StockerID = funGetEquNoByLoc(sLoc);
            string sBay = sLoc.Substring(2, 3);
            int iBay = int.Parse(sBay);
            if (iBay == 1)
            {
                fork = clsEnum.Fork.Left;
                return true;
            }
            else
            {
                switch (StockerID)
                {
                    case 1:
                        if (iBay == 71)
                        {
                            fork = clsEnum.Fork.Right;
                            return true;
                        }
                        else return false;
                    case 2:
                    case 3:
                    default:
                        if (iBay == 73)
                        {
                            fork = clsEnum.Fork.Right;
                            return true;
                        }
                        else return false;
                }
            }
        }

        public static ConveyorInfo GetBufferFromLocation(Location location)
        {
            if (location.LocationId == LocationDef.Location.A1_04.ToString())
            {
                return ConveyorDef.A1_04;
            }
            else if (location.LocationId == LocationDef.Location.A1_05.ToString())
            {
                return ConveyorDef.A1_05;
            }
            else if (location.LocationId == LocationDef.Location.A1_10.ToString())
            {
                return ConveyorDef.A1_10;
            }
            else if (location.LocationId == LocationDef.Location.A1_11.ToString())
            {
                return ConveyorDef.A1_11;
            }
            else if (location.LocationId == LocationDef.Location.A1_16.ToString())
            {
                return ConveyorDef.A1_16;
            }
            else if (location.LocationId == LocationDef.Location.A1_17.ToString())
            {
                return ConveyorDef.A1_17;
            }
            else if (location.LocationId == LocationDef.Location.A1_22.ToString())
            {
                return ConveyorDef.A1_22;
            }
            else if (location.LocationId == LocationDef.Location.A1_23.ToString())
            {
                return ConveyorDef.A1_23;
            }
            else return null;
        }

        /// <summary>
        /// 將字串轉成列舉值
        /// </summary>
        /// <typeparam name="TEnum">列舉Type</typeparam>
        /// <param name="EnumAsString">列舉字串</param>
        /// <returns>傳回列舉值</returns>
        public static TEnum funGetEnumValue<TEnum>(string EnumAsString) where TEnum : struct
        {
            TEnum enumType = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            Enum.TryParse<TEnum>(EnumAsString, out enumType);
            return enumType;
        }
    }
}
