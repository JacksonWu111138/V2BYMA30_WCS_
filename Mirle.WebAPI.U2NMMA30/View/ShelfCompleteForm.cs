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
    public partial class ShelfCompleteForm : Form
    {
        private ShelfComplete shelfComplete;
        public ShelfCompleteForm(WebApiConfig config)
        {
            shelfComplete = new ShelfComplete(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            ShelfCompleteInfo info = new ShelfCompleteInfo
            {
                carrierId = txtCarrierID.Text,
                jobId = txtJobID.Text,
                shelfId = txtShelfId.Text
            };

            shelfComplete.FunReport(info);
            button1.Enabled = true;
        }
    }
}
