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
    public partial class CtrlRackRequest : Form
    {
        public static WebApiConfig Apiconfig = new WebApiConfig();
        private V2BYMA30.clsHost api = new V2BYMA30.clsHost();
        public CtrlRackRequest(WebApiConfig AGVAPIconfig)
        {
            InitializeComponent();
            Apiconfig = AGVAPIconfig;
        }

        private void button_RackRequest_Click(object sender, EventArgs e)
        {
            RackRequestInfo info = new RackRequestInfo
            {
                jobId = textBox_jobId.Text,
                location = textBox_location.Text
            };
            if (!api.GetRackRequest().FunReport(info, Apiconfig.IP))
            {
                MessageBox.Show($"失敗, jobId:{info.jobId}.", "Rack Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功, jobId:{info.jobId}.", "Rack Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
