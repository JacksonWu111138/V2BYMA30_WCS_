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
    public partial class RetrieveCompleteForm : Form
    {
        private RetrieveComplete retrieveComplete;
        public RetrieveCompleteForm(WebApiConfig config)
        {
            retrieveComplete = new RetrieveComplete(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            RetrieveCompleteInfo info = new RetrieveCompleteInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                portId = txtPortID.Text,
                isComplete = txtIsComplete.Text,
                emptyTransfer = txtEmptyTransfer.Text
            };

            retrieveComplete.FunReport(info);
            button1.Enabled = true;
        }
    }
}
