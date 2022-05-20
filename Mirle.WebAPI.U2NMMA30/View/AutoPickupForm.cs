using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Mirle.Def;
using System.Linq;
using Mirle.WebAPI.U2NMMA30.Function;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using System.Windows.Forms;

namespace Mirle.WebAPI.U2NMMA30.View
{
    public partial class AutoPickupForm : Form
    {
        private WebApiConfig _config = new WebApiConfig();
        private AutoPickup autoPickup;
        public AutoPickupForm(WebApiConfig Config)
        {
            _config = Config;
            autoPickup = new AutoPickup(_config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            AutoPickupInfo info = new AutoPickupInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                portId = txtPortID.Text
            };

            info.slotList = new List<SlotListInfo_AutoPickup>();
            for (int i = 1; i < 9; i++)
            {
                CheckBox CheckControl = Controls.Find("checkBox" + i, true).FirstOrDefault() as CheckBox;
                if(CheckControl.Checked)
                {
                    info.slotList.Add(new SlotListInfo_AutoPickup() { slotId = i.ToString() });
                }
            }

            autoPickup.FunReport(info);
            button1.Enabled = true;
        }

        private void AutoPickupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
