﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.V2BYMA30.ReportInfo
{
    public class WCSCancelInfo
    {
        public string jobId { get; set; }
        public string transactionId { get; set; } = "WCS_Cancel";
        public string lotIdCarrierId { get; set; }
        public string cancelType { get; set; }
    }
}