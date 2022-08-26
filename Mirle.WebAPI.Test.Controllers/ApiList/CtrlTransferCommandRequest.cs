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
    public partial class CtrlTransferCommandRequest : Form
    {
        public static WebApiConfig Apiconfig = new WebApiConfig();
        public static WebApiConfig _TowerApi_Config = new WebApiConfig();
        public static WebApiConfig _E04Api_Config = new WebApiConfig();
        public static WebApiConfig _E05Api_Config = new WebApiConfig();
        private V2BYMA30.clsHost api = new V2BYMA30.clsHost();
        public CtrlTransferCommandRequest(WebApiConfig TowerConfig, WebApiConfig E04Config, WebApiConfig E05Config)
        {
            _TowerApi_Config = TowerConfig;
            _E04Api_Config = E04Config;
            _E05Api_Config = E05Config;
            InitializeComponent();
        }

        private void button_TransferCommandRequest_Click(object sender, EventArgs e)
        {
            bool ctrltype = true;
            switch (comboBox1.SelectedItem)
            {
                case "E800C":
                    Apiconfig = _TowerApi_Config;
                    break;
                case "LIFT4C":
                    Apiconfig = _E04Api_Config;
                    break;
                case "LIFT5C":
                    Apiconfig = _E05Api_Config;
                    break;

                default:
                    ctrltype = false;
                    MessageBox.Show($"未選擇對象controller", "Buffer Roll Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            if(ctrltype)
            {
                TransferCommandRequestInfo info = new TransferCommandRequestInfo
                {
                    jobId = textBox_jobId.Text,
                    binId = textBox_binId.Text,
                    carrierType = textBox_carrierType.Text,
                    source = textBox_source.Text,
                    destination = textBox_destination.Text
                };
                if (!api.GetTransferCommandRequest().FunReport(info, Apiconfig.IP))
                {
                    MessageBox.Show($"失敗, jobId:{info.jobId}.", "Transfer Command Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"成功, jobId:{info.jobId}.", "Transfer Command Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
