using Mirle.Def;
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
        public MidHost()
        {

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
