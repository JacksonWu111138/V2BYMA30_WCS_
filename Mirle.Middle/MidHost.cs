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
    }
}
