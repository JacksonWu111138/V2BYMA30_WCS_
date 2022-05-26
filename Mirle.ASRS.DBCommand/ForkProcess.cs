using Mirle.Def;
using Mirle.Stocker.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.ASRS.DBCommand
{
    public class ForkProcess : IFork
    {
        private IProcess process;
        public ForkProcess(int forkNo, clsPlcConfig plcConfig)
        {
            ForkNo = forkNo;
            LocType = plcConfig.LocType;

            if (plcConfig.CraneType == clsEnum.CmdType.CraneType.Single)
            {
                if (plcConfig.ForkType == clsEnum.CmdType.ForkType.SingleFork)
                {
                    if(LocType == clsEnum.CmdType.LocType.DoubleDeep)
                    {
                        process = new DoubleDeep.SingleCrane.SingleFork.Process();
                        process.Start();
                    }
                }
            }
        }

        public int ForkNo { get; }
        public clsEnum.CmdType.LocType LocType { get; }
        public IProcess GetProcess() => process;
    }
}
