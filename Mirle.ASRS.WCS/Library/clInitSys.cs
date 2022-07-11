using System;
using System.Windows.Forms;
using Mirle.Def;
using Config.Net;
using Mirle.Def.U2NMMA30;
using Mirle.DataBase;
using Mirle.Structure;
using System.Text;
using System.Runtime.InteropServices;
using Mirle.ASRS.WCS.View;

namespace Mirle.ASRS.WCS
{
    public class clInitSys
    {
        public static clsDbConfig DbConfig = new clsDbConfig();
        public static clsDbConfig DbConfig_WMS = new clsDbConfig();
        public static WebApiConfig WmsApi_Config = new WebApiConfig();
        public static WebApiConfig WcsApi_Config = new WebApiConfig();
        public static ASRSINI lcsini;
        public static int L2L_MaxCount = 5;
        

        //API
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString
            (string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void FunLoadIniSys()
        {
            try
            {
                lcsini = new ConfigurationBuilder<ASRSINI>()
                   .UseIniFile("Config\\ASRS.ini")
                   .Build();

                FunDbConfig(lcsini);
                FunSysConfig(lcsini);
                FunControllerID_Config(lcsini);
                FunStnNoConfig(lcsini);
                MainForm.AGVBuffer_Initial();
                MainForm.CraneBuffer_Initial();
                FunDeviceConfig(lcsini);
                FunApiConfig(lcsini);
            }
            catch(Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
                MessageBox.Show("找不到.ini資料，請洽系統管理人員 !!", "MIRLE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(0);
            }
        }

        private static void FunDbConfig(ASRSINI lcsini)
        {
            DbConfig.CommandTimeOut = lcsini.Database.CommandTimeOut;
            DbConfig.ConnectTimeOut = lcsini.Database.ConnectTimeOut;
            DbConfig.DbName = lcsini.Database.DataBase;
            DbConfig.DbPassword = lcsini.Database.DbPswd;
            DbConfig.DbServer = lcsini.Database.DbServer;
            DbConfig.DBType = (DBTypes)Enum.Parse(typeof(DBTypes), lcsini.Database.DBMS);
            DbConfig.DbUser = lcsini.Database.DbUser;
            DbConfig.FODBServer = lcsini.Database.FODbServer;
            DbConfig.WriteLog = true;

            DbConfig_WMS.CommandTimeOut = lcsini.Database_WMS.CommandTimeOut;
            DbConfig_WMS.ConnectTimeOut = lcsini.Database_WMS.ConnectTimeOut;
            DbConfig_WMS.DbName = lcsini.Database_WMS.DataBase;
            DbConfig_WMS.DbPassword = lcsini.Database_WMS.DbPswd;
            DbConfig_WMS.DbServer = lcsini.Database_WMS.DbServer;
            DbConfig_WMS.DBType = (DBTypes)Enum.Parse(typeof(DBTypes), lcsini.Database_WMS.DBMS);
            DbConfig_WMS.DbUser = lcsini.Database_WMS.DbUser;
            DbConfig_WMS.FODBServer = lcsini.Database_WMS.FODbServer;
            DbConfig_WMS.WriteLog = true;
        }

        private static void FunSysConfig(ASRSINI lcsini)
        {
            L2L_MaxCount = lcsini.System_Info.L2L_MaxCount;
        }

        private static void FunControllerID_Config(ASRSINI lcsini)
        {
            ConveyorDef.AGV.E1_01.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E1_08.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_35.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_36.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_37.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_38.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_39.ControllerID = lcsini.ControllerID.Tower;
            ConveyorDef.AGV.E2_44.ControllerID = lcsini.ControllerID.Tower;

            ConveyorDef.Box.B1_001.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_004.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_007.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_010.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_013.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_016.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_019.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_022.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_025.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_028.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_031.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_034.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_081.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_084.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_087.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_090.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_093.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_096.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_099.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_102.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_105.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_108.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_111.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_114.ControllerID = lcsini.ControllerID.Box;

            ConveyorDef.Box.B1_062.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_067.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_142.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.Box.B1_147.ControllerID = lcsini.ControllerID.Box;

            ConveyorDef.AGV.B1_070.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.AGV.B1_071.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.AGV.B1_074.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.AGV.B1_075.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.AGV.B1_078.ControllerID = lcsini.ControllerID.Box;
            ConveyorDef.AGV.B1_079.ControllerID = lcsini.ControllerID.Box;

            ConveyorDef.PCBA.M1_01.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.PCBA.M1_06.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.PCBA.M1_11.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.PCBA.M1_16.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.AGV.M1_05.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.AGV.M1_10.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.AGV.M1_15.ControllerID = lcsini.ControllerID.PCBA;
            ConveyorDef.AGV.M1_20.ControllerID = lcsini.ControllerID.PCBA;

            ConveyorDef.AGV.S1_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_07.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_13.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_25.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_31.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_37.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_40.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_41.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_44.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_45.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_48.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_49.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_50.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_52.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_56.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_60.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S1_64.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_07.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_13.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_25.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_31.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S2_49.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_07.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_13.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_19.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_25.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_31.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_37.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_45.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_47.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S3_49.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_07.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_13.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_19.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_25.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_49.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S4_50.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S5_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S5_07.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S5_37.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S5_49.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S6_01.ControllerID = lcsini.ControllerID.SMTC;
            ConveyorDef.AGV.S6_07.ControllerID = lcsini.ControllerID.SMTC;

            ConveyorDef.AGV.A4_01.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_04.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_05.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_08.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_09.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_12.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_13.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_16.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_17.ControllerID = lcsini.ControllerID.Line;
            ConveyorDef.AGV.A4_20.ControllerID = lcsini.ControllerID.Line;

            ConveyorDef.AGV.A1_01.ControllerID = lcsini.ControllerID.SMT3C;
            ConveyorDef.AGV.A1_04.ControllerID = lcsini.ControllerID.SMT3C;
            ConveyorDef.AGV.A1_05.ControllerID = lcsini.ControllerID.SMT3C;
            ConveyorDef.AGV.A1_08.ControllerID = lcsini.ControllerID.SMT3C;
            ConveyorDef.AGV.A1_09.ControllerID = lcsini.ControllerID.SMT3C;
            ConveyorDef.AGV.A1_12.ControllerID = lcsini.ControllerID.SMT3C;

            ConveyorDef.AGV.A2_01.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_04.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_05.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_08.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_09.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_12.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_13.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_16.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_17.ControllerID = lcsini.ControllerID.SMT5C;
            ConveyorDef.AGV.A2_20.ControllerID = lcsini.ControllerID.SMT5C;

            ConveyorDef.AGV.A3_01.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_04.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_05.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_08.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_09.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_12.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_13.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_16.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_17.ControllerID = lcsini.ControllerID.SMT6C;
            ConveyorDef.AGV.A3_20.ControllerID = lcsini.ControllerID.SMT6C;

            ConveyorDef.E04.LO1_02.ControllerID = lcsini.ControllerID.E04;
            ConveyorDef.E04.LO1_07.ControllerID = lcsini.ControllerID.E04;
            ConveyorDef.AGV.LO2_01.ControllerID = lcsini.ControllerID.E04;
            ConveyorDef.AGV.LO2_04.ControllerID = lcsini.ControllerID.E04;

            ConveyorDef.AGV.LO4_01.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO4_04.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO5_01.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO5_04.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO6_01.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO6_04.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO3_01.ControllerID = lcsini.ControllerID.E05;
            ConveyorDef.AGV.LO3_04.ControllerID = lcsini.ControllerID.E05;
        }

        private static void FunStnNoConfig(ASRSINI lcsini)
        {
            ConveyorDef.E04.LO1_02.StnNo = lcsini.StnNo.LO1_02;
            ConveyorDef.E04.LO1_07.StnNo = lcsini.StnNo.LO1_07;
            ConveyorDef.Box.B1_062.StnNo = lcsini.StnNo.B1_062;
            ConveyorDef.Box.B1_067.StnNo = lcsini.StnNo.B1_067;
            ConveyorDef.Box.B1_142.StnNo = lcsini.StnNo.B1_142;
            ConveyorDef.Box.B1_147.StnNo = lcsini.StnNo.B1_147;
        }

        private static void FunDeviceConfig(ASRSINI lcsini)
        {
            string[] adPCBA = lcsini.EquNo.PCBA.Split(',');
            for (int i = 0; i < MainForm.PCBA.Length; i++)
            {
                MainForm.PCBA[i].DeviceID = adPCBA[i];
            }

            string[] adBox = lcsini.EquNo.Box.Split(',');
            for (int i = 0; i < MainForm.Box.Length; i++)
            {
                MainForm.Box[i].DeviceID = adBox[i];
            }

            string[] adAGV = lcsini.EquNo.AGV.Split(',');
            for (int i = 0; i < MainForm.AGV.Length; i++)
            {
                MainForm.AGV[i].DeviceID = adAGV[i];
            }
        }
      
        private static void FunApiConfig(ASRSINI lcsini)
        {
            WmsApi_Config.IP = lcsini.Client_API.IP;
            WcsApi_Config.IP = lcsini.Server_API.IP;
        }

        /// <summary>
        /// 讀取ini檔的單一欄位
        /// </summary>
        /// <param name="sFileName">INI檔檔名</param>
        /// <param name="sAppName">區段名</param>
        /// <param name="sKeyName">KEY名稱</param>
        /// <param name="strDefault">Default</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string funReadParam(string sFileName, string sAppName, string sKeyName, string strDefault = "")
        {
            StringBuilder sResult = new StringBuilder(255);
            int intResult;
            try
            {
                intResult = GetPrivateProfileString(sAppName, sKeyName, strDefault, sResult, sResult.Capacity, sFileName);
                string R = sResult.ToString().Trim();
                return R;
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return strDefault;
            }
        }
    }
}
