﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.DB.WMS.Fun.Parameter
{
    public class clsLocMst
    {
        public const string TableName = "r_wms_location";
        public class Column
        {
            public const string Loc = "LOCATION_CODE";
            public const string LocDD = "BROTHER_LOCATION_CODE";
            public const string BoxID = "CARRIER_CODE";
            public const string EquNo = "CRANE";
            public const string BAY = "BAY";
            public const string LEVEL = "LEVEL";
            public const string ROW = "ROW";
        }
    }
}