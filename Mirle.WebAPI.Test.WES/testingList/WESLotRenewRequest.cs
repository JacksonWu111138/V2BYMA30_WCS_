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
    public partial class WESLotRenewRequest : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESLotRenewRequest(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_LotRenewRequest_Click(object sender, EventArgs e)
        {
            LotRenewRequestInfo info = new LotRenewRequestInfo
            {
                jobId = textBox_jobId.Text,
                lotId = textBox_lotId.Text
            };
            if (textBox_qty.Text != "")
            {
                info.qty = Int32.Parse(textBox_qty.Text);
                if (!api.GetLotRenewRequest().FunReport(info, wesApiconfig.IP))
                {
                    MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot Renew Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot Renew Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show($"qty欄位為空, jobId:{info.jobId}.", "Lot Renew Request", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
    }
}
