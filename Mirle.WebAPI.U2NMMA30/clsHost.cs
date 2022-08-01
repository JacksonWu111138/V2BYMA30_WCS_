using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Def;
using Mirle.WebAPI.V2BYMA30.Function;

namespace Mirle.WebAPI.V2BYMA30
{
    public class clsHost
    {
        private CVReceiveNewBinCmd RECEIVE_NEW_BIN_CMD;
        public clsHost()
        {
            RECEIVE_NEW_BIN_CMD = new CVReceiveNewBinCmd();
        }

        public CVReceiveNewBinCmd GetCV_ReceiveNewBinCmd() => RECEIVE_NEW_BIN_CMD;
    }
}
