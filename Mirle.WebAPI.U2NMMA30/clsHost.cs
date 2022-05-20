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
        private WebApiConfig _config = new WebApiConfig();
        private ShelfReport shelfReport;
        private PositionReport positionReport;
        private RetrieveComplete retrieveComplete;
        private PutAwayComplete putAwayComplete;
        private PickupQuery pickupQuery;
        private AutoPickup autoPickup;
        private PutawayCheck putawayCheck;
        private ShelfRequest shelfRequest;
        private ShelfComplete shelfComplete;
        private EmptyShelfQuery emptyShelfQuery;
        private WcsCancel wcsCancel;
        private EQPStatusUpdate statusUpdate;
        public clsHost(WebApiConfig Config)
        {
            _config = Config;
            shelfReport = new ShelfReport(_config);
            positionReport = new PositionReport(_config);
            retrieveComplete = new RetrieveComplete(_config);
            putAwayComplete = new PutAwayComplete(_config);
            pickupQuery = new PickupQuery(_config);
            autoPickup = new AutoPickup(_config);
            putawayCheck = new PutawayCheck(_config);
            shelfRequest = new ShelfRequest(_config);
            shelfComplete = new ShelfComplete(_config);
            emptyShelfQuery = new EmptyShelfQuery(_config);
            wcsCancel = new WcsCancel(_config);
            statusUpdate = new EQPStatusUpdate(_config);
        }

        public ShelfReport GetShelfReport()
        {
            return shelfReport;
        }

        public PositionReport GetPositionReport()
        {
            return positionReport;
        }

        public RetrieveComplete GetRetrieveComplete()
        {
            return retrieveComplete;
        }

        public PutAwayComplete GetPutAwayComplete()
        {
            return putAwayComplete;
        }

        public PickupQuery GetPickupQuery()
        {
            return pickupQuery;
        }

        public AutoPickup GetAutoPickup()
        {
            return autoPickup;
        }

        public PutawayCheck GetPutawayCheck()
        {
            return putawayCheck;
        }

        public ShelfRequest GetShelfRequest()
        {
            return shelfRequest;
        }

        public ShelfComplete GetShelfComplete()
        {
            return shelfComplete;
        }

        public EmptyShelfQuery GetEmptyShelfQuery()
        {
            return emptyShelfQuery;
        }

        public WcsCancel GetWcsCancel()
        {
            return wcsCancel;
        }

        public EQPStatusUpdate GetEQPStatusUpdate()
        {
            return statusUpdate;
        }
    }
}
