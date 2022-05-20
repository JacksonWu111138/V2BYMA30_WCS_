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
    public partial class PutAwayCompleteForm : Form
    {
        private PutAwayComplete putAwayComplete;
        public PutAwayCompleteForm(WebApiConfig config)
        {
            putAwayComplete = new PutAwayComplete(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            PutAwayCompleteInfo info = new PutAwayCompleteInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                shelfId = txtShelfId.Text
            };

            putAwayComplete.FunReport(info);
            button1.Enabled = true;
        }
    }
}
