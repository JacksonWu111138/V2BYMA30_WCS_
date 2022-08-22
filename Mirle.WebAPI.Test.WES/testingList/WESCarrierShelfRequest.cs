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
    public partial class WESCarrierShelfRequest : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESCarrierShelfRequest(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_CarrierShelfRequest_Click(object sender, EventArgs e)
        {
            CarrierShelfRequestInfo info = new CarrierShelfRequestInfo
            {
                jobId = textBox_jobId.Text,
                fromShelfId = textBox_fromShelfId.Text,
                toShelfId = textBox_toShelfId.Text,
                disableLocation = textBox_disableLocation.Text
            };
            if (!api.GetCarrierShelfRequest().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Carrier Shelf Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Carrier Shelf Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
