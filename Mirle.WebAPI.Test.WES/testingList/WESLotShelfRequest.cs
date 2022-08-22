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
    public partial class WESLotShelfRequest : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESLotShelfRequest(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void label_jobId_Click(object sender, EventArgs e)
        {

        }

        private void textBox_jobId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_LotShelfComplete_Click(object sender, EventArgs e)
        {
            
        }

        private void button_LotShelfRequest_Click(object sender, EventArgs e)
        {
            LotShelfRequestInfo info = new LotShelfRequestInfo
            {
                jobId = textBox_jobId.Text,
                fromShelfId = textBox_fromShelfId.Text,
                toShelfId = textBox_toShelfId.Text
            };
            if (!api.GetLotShelfRequest().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot Shelf Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot Shelf Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
