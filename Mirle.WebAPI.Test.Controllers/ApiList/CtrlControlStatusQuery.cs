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
using Mirle.Def.U2NMMA30;

namespace Mirle.WebAPI.Test.Controllers.ApiList
{
    public partial class CtrlControlStatusQuery : Form
    {
        public static WebApiConfig Apiconfig = new WebApiConfig();
        private V2BYMA30.clsHost api = new V2BYMA30.clsHost();
        public CtrlControlStatusQuery(WebApiConfig TowerAPIconfig)
        {
            InitializeComponent();
            Apiconfig = TowerAPIconfig;
        }

        private void button_ControlStatusQuery_Click(object sender, EventArgs e)
        {
            ChipSTKCListInfo list = new ChipSTKCListInfo
            {
                chipSTKCId = textBox_chipSTKCId.Text
            };
            ControlStatusQueryInfo info = new ControlStatusQueryInfo
            {
                jobId = textBox_jobId.Text,
                chipSTKCList = new List<ChipSTKCListInfo>()
            };
            info.chipSTKCList.Add(list);

            if (!api.GetControlStatusQuery().FunReport(info, Apiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Control Status Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Control Status Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
