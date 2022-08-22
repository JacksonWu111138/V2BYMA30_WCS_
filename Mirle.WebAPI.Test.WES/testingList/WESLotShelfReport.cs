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
    public partial class WESLotShelfReport : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESLotShelfReport(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_LotShelfReport_Click(object sender, EventArgs e)
        {
            LotShelfReportInfo info = new LotShelfReportInfo
            {
                jobId = textBox_jobId.Text,
                shelfId = textBox_shelfId.Text,
                shelfStatus = textBox_shelfStatus.Text,
                lotId = textBox_lotId.Text,
                disableLocation = textBox_disableLocation.Text
            };
            if (!api.GetLotShelfReport().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot Shelf Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot Shelf Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
