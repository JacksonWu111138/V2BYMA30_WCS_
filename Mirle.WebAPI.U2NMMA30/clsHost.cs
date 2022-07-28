using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.Function;

namespace Mirle.WebAPI.U2NMMA30
{
    public class clsHost
    {
        private CV_RECEIVE_NEW_BIN_CMD RECEIVE_NEW_BIN_CMD;
        public clsHost()
        {
            RECEIVE_NEW_BIN_CMD = new CV_RECEIVE_NEW_BIN_CMD();
        }

        public CV_RECEIVE_NEW_BIN_CMD GetCV_ReceiveNewBinCmd() => RECEIVE_NEW_BIN_CMD;
    }
}
