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
    public partial class CtrlNewCarrierToStage : Form
    {
        public static WebApiConfig Apiconfig = new WebApiConfig();
        private V2BYMA30.clsHost api = new V2BYMA30.clsHost();
        public CtrlNewCarrierToStage(WebApiConfig TowerAPIconfig)
        {
            InitializeComponent();
            Apiconfig = TowerAPIconfig;
        }

        private void button_NewCarrierToStage_Click(object sender, EventArgs e)
        {
            NewCarrierToStageInfo info = new NewCarrierToStageInfo
            {
                jobId = textBox_jobId.Text,
                carrierId = textBox_carrierId.Text,
                stagePosition = textBox_stagePosition.Text
            };
            if (!api.GetNewCarrierToStage().FunReport(info, Apiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "New Carrier To Stage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "New Carrier To Stage", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
