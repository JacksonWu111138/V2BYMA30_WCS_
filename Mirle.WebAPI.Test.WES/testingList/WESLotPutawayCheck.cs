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
    public partial class WESLotPutawayCheck : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESLotPutawayCheck(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_LotPutawayCheck_Click(object sender, EventArgs e)
        {
            LotPutawayCheckInfo info = new LotPutawayCheckInfo
            {
                jobId = textBox_jobId.Text,
                portId = textBox_portId.Text,
                lotId = textBox_lotId.Text
            };
            if (!api.GetLotPutawayCheck().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot PutAway Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot PutAway Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
