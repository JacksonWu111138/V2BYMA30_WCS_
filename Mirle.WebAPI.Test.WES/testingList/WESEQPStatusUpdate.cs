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
    public partial class WESEQPStatusUpdate : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESEQPStatusUpdate(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void label_craneStatus_Click(object sender, EventArgs e)
        {

        }

        private void textBox_craneStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_EQPStatusUpdate_Click(object sender, EventArgs e)
        {
            EQPStatusUpdateInfo info = new EQPStatusUpdateInfo
            {
                jobId = textBox_jobId.Text,
                craneId = textBox_craneId.Text,
                craneStatus = textBox_craneStatus.Text,
                portId = textBox_portId.Text,
                portStatus = textBox_portStatus.Text
            };
            if (!api.GetEQPStatusUpdate().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "EQP Status Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "EQP Status Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
