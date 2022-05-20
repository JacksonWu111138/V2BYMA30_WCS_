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
    public partial class PositionReportForm : Form
    {
        private PositionReport positionReport;
        public PositionReportForm(WebApiConfig config)
        {
            positionReport = new PositionReport(config);
            InitializeComponent();
        }

        private void PositionReportForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            PositionReportInfo info = new PositionReportInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                location = txtLocation.Text
            };

            positionReport.FunReport(info);
            button1.Enabled = true;
        }
    }
}
