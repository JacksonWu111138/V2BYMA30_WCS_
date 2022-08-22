using System;
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
    public partial class WESWCSCancel : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESWCSCancel(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_WCSCancel_Click(object sender, EventArgs e)
        {
            WCSCancelInfo info = new WCSCancelInfo
            {
                jobId = textBox_jobId.Text,
                lotIdCarrierId = textBox_lotIdCarrierId.Text,
                cancelType = textBox_cancelType.Text
            };
            if (!api.GetWCSCancel().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "WCS Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "WCS Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
