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
    public partial class WESRemoveRackShow : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESRemoveRackShow(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button_RemoveRackShow_Click(object sender, EventArgs e)
        {
            RemoveRackShowInfo info = new RemoveRackShowInfo
            {
                jobId = textBox_jobId.Text,
                location = textBox_location.Text,
                carrierId = textBox_carrierId.Text
            };
            if (!api.GetRemoveRackShow().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Remove Rack Show", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Remove Rack Show", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
