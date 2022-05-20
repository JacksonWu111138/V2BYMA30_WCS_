using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Mirle.Def;
using System.Linq;
using Mirle.WebAPI.U2NMMA30.Function;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using System.Windows.Forms;

namespace Mirle.WebAPI.U2NMMA30.View
{
    public partial class PutawayCheckForm : Form
    {
        private WebApiConfig _config = new WebApiConfig();
        private PutawayCheck putawayCheck;
        public PutawayCheckForm(WebApiConfig config)
        {
            putawayCheck = new PutawayCheck(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            PutawayCheckInfo info = new PutawayCheckInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                portId = txtPortID.Text,
                onlineMode = txtonlineMode.Text
            };

            info.lotList = new List<SlotListInfo>();
            for (int i = 1; i < 9; i++)
            {
                CheckBox CheckControl = Controls.Find("checkBox" + i, true).FirstOrDefault() as CheckBox;
                TextBox TextControl = Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;
                
                if (CheckControl.Checked)
                {
                    info.lotList.Add(new SlotListInfo()
                    {
                        slotId = i.ToString(),
                        lotId = TextControl.Text
                    });
                }
                else
                {
                    info.lotList.Add(new SlotListInfo()
                    {
                        slotId = i.ToString(),
                        lotId = ""
                    });
                }
            }

            putawayCheck.FunReport(info);
            button1.Enabled = true;
        }

        private void txtCarrierID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPortID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJobID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
