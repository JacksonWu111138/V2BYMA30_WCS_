﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.V2BYMA30.ReportInfo
{
    public class BufferStatusReply : BufferReply
    {
        public string ready { get; set; }
        public string isLoad { get; set; }
        public string isEmpty { get; set; }
        public string stbSts { get; set; }
    }
}
