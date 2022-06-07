using Mirle.DB.Object;
using Mirle.Def;
using Mirle.Stocker.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;
using Mirle.MapController;

namespace Mirle.ASRS.DBCommand
{
    public class ASRSProcess : IStocker
    {
        private clsEnum.CmdType.CraneType craneType;
        private int _stockerId;
        private readonly List<ICrane> _cranes = new List<ICrane>();
        public ASRSProcess(clsPlcConfig plcConfig, DeviceInfo Device, MapHost Router)
        {
            craneType = plcConfig.CraneType;
            _stockerId = int.Parse(Device.DeviceID);
            _cranes.Add(new CraneProcess(1, plcConfig, Device, Router));

            if (craneType == clsEnum.CmdType.CraneType.Daul)
                _cranes.Add(new CraneProcess(2, plcConfig, Device, Router));
        }

        public clsEnum.CmdType.CraneType CraneType => craneType;
        public int Id => _stockerId;
        public IEnumerable<ICrane> GetCranes()
        {
            return _cranes.AsReadOnly();
        }

        public ICrane GetCrane(int craneNo)
        {
            return GetCranes().FirstOrDefault(x => x.CraneNo == craneNo);
        }
    }
}
