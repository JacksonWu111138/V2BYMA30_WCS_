using Mirle.Def;
using Mirle.Middle.DB_Proc;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Middle
{
    public class MidHost
    {
        private static List<ConveyorInfo> Node_All = new List<ConveyorInfo>();
        private WebApiConfig AgvApi_Config = new WebApiConfig();
        private DeviceInfo[] _PCBA = new DeviceInfo[2];
        private DeviceInfo[] _Box = new DeviceInfo[3];
        private string sDeviceID_AGV = "";
        private readonly clsHost db;
        private System.Timers.Timer timRead = new System.Timers.Timer();
        private bool bOnline = true;
        public MidHost(List<ConveyorInfo> conveyors, WebApiConfig AgvApiConfig, DeviceInfo[] PCBA, DeviceInfo[] Box, string DeviceID_AGV, clsDbConfig config)
        {
            db = new clsHost(config, PCBA, Box);
            Node_All = conveyors;
            AgvApi_Config = AgvApiConfig;
            _PCBA = PCBA;
            _Box = Box;
            sDeviceID_AGV = DeviceID_AGV;

            timRead.Elapsed += new System.Timers.ElapsedEventHandler(timRead_Elapsed);
            timRead.Enabled = true; timRead.Interval = 500;
        }

        public bool Online
        {
            get { return bOnline; }
            set { bOnline = value; }
        }

        private void timRead_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timRead.Enabled = false;
            try
            {
                if (bOnline)
                {

                }
            }
            catch (Exception ex)
            {
                int errorLine = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                clsWriLog.Log.subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, errorLine.ToString() + ":" + ex.Message);
            }
            finally
            {
                timRead.Enabled = true;
            }
        }

        /// <summary>
        /// 取得Buffer PLC上的命令序號
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public int GetBufferCmd(ConveyorInfo buffer)
        {
            return 0;
        }

        /// <summary>
        /// 確認是否是入庫Ready
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool CheckIsInReady(DeviceInfo Device, Location location)
        {
            ConveyorInfo conveyor = new ConveyorInfo();
            foreach(var floor in Device.Floors)
            {
                bool bGet = false;
                foreach(var con in floor.Group_IN)
                {
                    if(con.BufferName == location.LocationId)
                    {
                        conveyor = con;
                        bGet = true;
                        break;
                    }
                }

                if (bGet) break;
            }

            return true;
        }

        /// <summary>
        /// 確認是否是入庫Ready
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool CheckIsInReady(ConveyorInfo buffer)
        {
            return true;
        }

        /// <summary>
        /// 確認是否是出庫Ready
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool CheckIsOutReady(DeviceInfo Device, Location location)
        {
            ConveyorInfo conveyor = new ConveyorInfo();
            foreach (var floor in Device.Floors)
            {
                bool bGet = false;
                foreach (var con in floor.Group_OUT)
                {
                    if (con.BufferName == location.LocationId)
                    {
                        conveyor = con;
                        bGet = true;
                        break;
                    }
                }

                if (bGet) break;
            }

            return true;
        }

        /// <summary>
        /// 確認是否是出庫Ready
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool CheckIsOutReady(ConveyorInfo buffer)
        {
            return true;
        }

        /// <summary>
        /// 確認該Buffer是否荷有
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="IsLoad">是否荷有</param>
        /// <returns></returns>
        public bool CheckIsLoad(ConveyorInfo buffer, ref bool IsLoad)
        {
            return true;
        }
    }
}
