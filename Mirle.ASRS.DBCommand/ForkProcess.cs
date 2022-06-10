﻿using Mirle.Def;
using Mirle.Stocker.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using Mirle.Structure;
using System.Threading.Tasks;
using Mirle.MapController;
using Mirle.Middle;
using Mirle.EccsSignal;

namespace Mirle.ASRS.DBCommand
{
    public class ForkProcess : IFork
    {
        private IProcess process;
        public ForkProcess(int forkNo, clsPlcConfig plcConfig, DeviceInfo Device, MapHost Router, MidHost middle, SignalHost CrnSignal)
        {
            ForkNo = forkNo;
            LocType = plcConfig.LocType;

            if (plcConfig.CraneType == clsEnum.CmdType.CraneType.Single)
            {
                if (plcConfig.ForkType == clsEnum.CmdType.ForkType.SingleFork)
                {
                    if(LocType == clsEnum.CmdType.LocType.DoubleDeep)
                    {
                        switch(plcConfig.CV_Type)
                        {
                            case clsEnum.CmdType.CV_Type.Double:
                                process = new DoubleDeep.SingleCrane.SingleFork.DoubleCV.Process(Device, Router);
                                break;
                            default:
                                process = new DoubleDeep.SingleCrane.SingleFork.Process(Device, Router, middle, CrnSignal);
                                break;
                        }
                    }
                }
            }

            process.Start();
        }

        public int ForkNo { get; }
        public clsEnum.CmdType.LocType LocType { get; }
        public IProcess GetProcess() => process;
    }
}
