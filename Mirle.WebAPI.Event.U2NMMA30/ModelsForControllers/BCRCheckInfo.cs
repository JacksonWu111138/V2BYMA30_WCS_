﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirle.WebAPI.Event.U2NMMA30.Models
{
    public class BCRCheckInfo : BaseInfo
    {
        public string barcode { get; set; }
        public string ioType { get; set; }
        public string location { get; set; }
    }
}
