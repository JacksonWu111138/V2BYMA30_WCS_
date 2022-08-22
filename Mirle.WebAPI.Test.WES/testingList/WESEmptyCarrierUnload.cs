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
    public partial class WESEmptyCarrierUnload : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESEmptyCarrierUnload(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void label_carrierId_Click(object sender, EventArgs e)
        {

        }

        private void textBox_carrierId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_EmptyCarrierUnload_Click(object sender, EventArgs e)
        {
            EmptyCarrierUnloadInfo info = new EmptyCarrierUnloadInfo
            {
                jobId = textBox_jobId.Text,
                carrierId = textBox_carrierId.Text,
                location = textBox_location.Text,
                isEmpty = textBox_isEmpty.Text
            };
            if (!api.GetEmptyCarrierUnload().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Empty Carrier Unload", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Empty Carrier Unload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
