﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.WebAPI.V2BYMA30.ReportInfo;
using Mirle.Def;

namespace Mirle.WebAPI.Test.WES.testingList
{
    public partial class WESEmptyESDCarrierUnload : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESEmptyESDCarrierUnload(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_EmptyESDCarrierUnload_Click(object sender, EventArgs e)
        {
            EmptyESDCarrierUnloadInfo info = new EmptyESDCarrierUnloadInfo
            {
                jobId = textBox_jobId.Text,
                carrierId = textBox_carrierId.Text,
                location = textBox_location.Text
            };
            if (!api.GetEmptyESDCarrierUnload().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Empty ESDCarrier Unload", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Empty ESDCarrier Unload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}