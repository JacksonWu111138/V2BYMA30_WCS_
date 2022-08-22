using Mirle.Def;
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

namespace Mirle.WebAPI.Test.WES.testingList
{
    public partial class WESLotPositionReport : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESLotPositionReport(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }


        private void button_LotPositionReport_Click(object sender, EventArgs e)
        {
            LotPositionReportInfo info = new LotPositionReportInfo
            {
                jobId = textBox_jobId.Text,
                lotId = textBox_lotId.Text,
                location = textBox_location.Text
            };
            if (!api.GetLotPositionReport().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot Position Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot Position Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
