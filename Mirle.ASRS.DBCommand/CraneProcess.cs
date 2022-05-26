using Mirle.Def;
using Mirle.Stocker.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.ASRS.DBCommand
{
    public class CraneProcess : ICrane
    {
        private readonly List<IFork> _forks = new List<IFork>();
        public CraneProcess(int craneNo, clsPlcConfig plcConfig)
        {
            CraneNo = craneNo;
            ForkType = plcConfig.ForkType;
            _forks.Add(new ForkProcess(1, plcConfig));

            if (ForkType == clsEnum.CmdType.ForkType.TwinFork)
                _forks.Add(new ForkProcess(2, plcConfig));
        }

        public int CraneNo { get; }
        public clsEnum.CmdType.ForkType ForkType { get; }
        public IEnumerable<IFork> GetForks()
        {
            return _forks.AsReadOnly();
        }

        public IFork GetFork(int forkNo)
        {
            return GetForks().FirstOrDefault(x => x.ForkNo == forkNo);
        }
    }
}
