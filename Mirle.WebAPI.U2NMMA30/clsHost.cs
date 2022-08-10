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
        private CVReceiveNewBinCmd RECEIVE_NEW_BIN_CMD = new CVReceiveNewBinCmd();
        private BufferStatusQuery BufferStatusQuery = new BufferStatusQuery();
        private PutawayTransfer PutawayTransfer = new PutawayTransfer();
        private RetrieveTransfer RetrieveTransfer = new RetrieveTransfer();
        private RackMove RackMove = new RackMove();
        private PositionReport PositionReport = new PositionReport();
        private BufferRoll BufferRoll = new BufferRoll();
        private TaskCancel TaskCancel = new TaskCancel();
        public CVReceiveNewBinCmd GetCV_ReceiveNewBinCmd() => RECEIVE_NEW_BIN_CMD;
        public BufferStatusQuery GetBufferStatusQuery() => BufferStatusQuery;
        public PutawayTransfer GetPutawayTransfer() => PutawayTransfer;
        public RetrieveTransfer GetRetrieveTransfer() => RetrieveTransfer;
        public RackMove GetRackMove() => RackMove;
        public PositionReport GetPositionReport() => PositionReport;
        public BufferRoll GetBufferRoll() => BufferRoll;
        public TaskCancel GetTaskCancel() => TaskCancel;    
    }
}
