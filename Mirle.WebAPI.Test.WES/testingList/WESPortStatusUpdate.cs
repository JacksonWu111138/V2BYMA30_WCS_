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
    public partial class WESPortStatusUpdate : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESPortStatusUpdate(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_PortStatusUpload_Click(object sender, EventArgs e)
        {
            PortStatusUpdateInfo info = new PortStatusUpdateInfo
            {
                jobId = textBox_jobId.Text,
                portId = textBox_portId.Text,
                portStatus = textBox_portStatus.Text
            };
            if (!api.GetPortStatusUpdate().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Port Status Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Port Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
