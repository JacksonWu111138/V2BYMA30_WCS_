using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Mirle.Def;
using Mirle.WebAPI.U2NMMA30.Function;
using Mirle.WebAPI.U2NMMA30.ReportInfo;
using System.Windows.Forms;

namespace Mirle.WebAPI.U2NMMA30.View
{
    public partial class PickupQueryForm : Form
    {
        private PickupQuery pickupQuery;
        public PickupQueryForm(WebApiConfig config)
        {
            pickupQuery = new PickupQuery(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            for (int i = 1; i < 9; i++)
            {
                CheckBox CheckControl = Controls.Find("checkBox" + i, true).FirstOrDefault() as CheckBox;
                CheckControl.Checked = false;
            }

            PickupQuery_WCS pickupQuery_WCS = new PickupQuery_WCS
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                portId = txtPortID.Text
            };

            PickupQuery_WMS pickupQuery_WMS = new PickupQuery_WMS();
            if (pickupQuery.FunReport(pickupQuery_WCS, ref pickupQuery_WMS))
            {
                if(pickupQuery_WMS.slotList != null)
                {
                    foreach (var lst in pickupQuery_WMS.slotList)
                    {
                        if (!string.IsNullOrWhiteSpace(lst.lotId))
                        {
                            CheckBox CheckControl = Controls.Find("checkBox" + int.Parse(lst.slotId), true).FirstOrDefault() as CheckBox;
                            CheckControl.Checked = true;
                        }
                    }
                }
            }

            button1.Enabled = true;
        }

        private void PickupQueryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
