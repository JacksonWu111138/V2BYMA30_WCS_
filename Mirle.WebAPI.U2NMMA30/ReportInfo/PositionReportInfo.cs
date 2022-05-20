﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.U2NMMA30.ReportInfo
{
    public class PositionReportInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "POSITION_REPORT";
        public string carrierId { get; set; }
        public string location { get; set; }
    }
}
