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
    public partial class WESEmptyShelfQuery : Form
    {
        private WebApiConfig wesApiconfig = new WebApiConfig();
        private WebAPI.V2BYMA30.clsHost api = new WebAPI.V2BYMA30.clsHost();
        public WESEmptyShelfQuery(WebApiConfig WESAPIconfig)
        {
            InitializeComponent();
            wesApiconfig = WESAPIconfig;
        }

        private void button_EmptyShelfQuery_Click(object sender, EventArgs e)
        {
            EmptyShelfQueryInfo info = new EmptyShelfQueryInfo
            {
                jobId = textBox_jobId.Text,
                lotIdCarrierId = textBox_lotIdCarrierId.Text,
                craenId = textBox_craneId.Text
            };
            if (!api.GetEmptyShelfQuery().FunReport(info, wesApiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Empty Shelf Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Empty Shelf Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
