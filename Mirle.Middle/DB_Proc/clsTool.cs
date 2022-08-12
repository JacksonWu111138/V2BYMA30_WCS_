using Mirle.Def;
using Mirle.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.Middle.DB_Proc
{
    public class clsTool
    {
        private DeviceInfo[] pcba;
        private DeviceInfo[] box;
        public clsTool(DeviceInfo[] PCBA, DeviceInfo[] Box)
        {
            pcba = PCBA;
            box = Box;
        }

        public clsEnum.Shelf GetShelfLocation(string sLoc)
        {
            if (string.IsNullOrWhiteSpace(sLoc) || sLoc.Length < 7) return clsEnum.Shelf.None;

            int iRow = Convert.ToInt32(sLoc.Substring(0, 2));
            switch (iRow % 4)
            {
                case 1:
                case 2:
                    return clsEnum.Shelf.InSide;
                case 3:
                default:
                    return clsEnum.Shelf.OutSide;
            }
        }

        public string GetEquCmdLoc_BySysCmd(string sLoc)
        {
            if (string.IsNullOrWhiteSpace(sLoc)) return string.Empty;
            else
            {
                int iRow = Convert.ToInt32(sLoc.Substring(0, 2));
                if (iRow <= 4) return sLoc;
                else
                {
                    if (iRow % 4 == 0) return "04" + sLoc.Substring(2);
                    else return (iRow % 4).ToString().PadLeft(2, '0') + sLoc.Substring(2);
                }
            }
        }

        public bool CheckDeviceMatchCraneDevice(string sDeviceID, ref DeviceInfo device, ref clsEnum.AsrsType type)
        {
            bool check = pcba.Where(r => r.DeviceID == sDeviceID).Any();
            if(check)
            {
                type = clsEnum.AsrsType.PCBA;
                var devices = pcba.Where(r => r.DeviceID == sDeviceID);
                foreach(var d in devices)
                {
                    device = d;
                    return true;
                }
            }
            else
            {
                check = box.Where(r => r.DeviceID == sDeviceID).Any();
                if (check)
                {
                    type = clsEnum.AsrsType.Box;
                    var devices = box.Where(r => r.DeviceID == sDeviceID);
                    foreach (var d in devices)
                    {
                        device = d;
                        return true;
                    }
                }
                else
                {
                    type = clsEnum.AsrsType.None;
                    return false;
                }
            }

            return false;
        }

        public MiddleCmd GetMiddleCmd(DataRow drTmp)
        {
            MiddleCmd cmd = new MiddleCmd
            {
                BatchID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.BatchID]),
                Destination = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Destination]),
                DeviceID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.DeviceID]),
                CrtDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Create_Date]),
                CSTID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CSTID]),
                EndDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.EndDate]),
                ExpDate = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Expose_Date]),
                CmdMode = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CmdMode]),
                CmdSts = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CmdSts]),
                CommandID = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CommandID]),
                CompleteCode = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.CompleteCode]),
                Path = Convert.ToInt32(drTmp[Parameter.clsMiddleCmd.Column.Path]),
                Priority = Convert.ToInt32(drTmp[Parameter.clsMiddleCmd.Column.Priority]),
                Remark = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Remark]),
                Source = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.Source]),
                TaskNo = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.TaskNo]),
                Iotype = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.IoType]),
                largest = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.largest]),
                rackLocation = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.rackLocation]),
                carrierType = Convert.ToString(drTmp[Parameter.clsMiddleCmd.Column.carrierType])
            };

            return cmd;
        }
    }
}
