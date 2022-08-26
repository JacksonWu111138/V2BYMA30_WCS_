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

namespace Mirle.WebAPI.Test.Controllers.ApiList
{
    public partial class CtrlLotTransferCancel : Form
    {
        public static WebApiConfig Apiconfig = new WebApiConfig();
        private V2BYMA30.clsHost api = new V2BYMA30.clsHost();
        public CtrlLotTransferCancel(WebApiConfig TowerAPIconfig)
        {
            InitializeComponent();
            Apiconfig = TowerAPIconfig;
        }

        private void label_jobId_Click(object sender, EventArgs e)
        {

        }

        private void textBox_jobId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_LotTransferCancel_Click(object sender, EventArgs e)
        {
            LotTransferCancelInfo info = new LotTransferCancelInfo
            {
                jobId = textBox_jobId.Text,
                lotId = textBox_lotId.Text
            };
            if (!api.GetLotTransferCancel().FunReport(info, Apiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Lot Transfer Cancel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Lot Transfer Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
