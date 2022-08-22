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
    public partial class WESCarrierPutawayCheck : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESCarrierPutawayCheck(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_CarrierPutawayCheck_Click(object sender, EventArgs e)
        {
            CarrierPutawayCheckInfo info = new CarrierPutawayCheckInfo
            {
                jobId = textBox_jobId.Text,
                portId = textBox_portId.Text,
                carrierId = textBox_carrierId.Text,
                storageType = textBox_storageType.Text
            };
            if (!api.GetCarrierPutawayCheck().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Carrier Transfer Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Carrier Transfer Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
