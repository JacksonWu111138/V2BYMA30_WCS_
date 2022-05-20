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
    public partial class ShelfRequestForm : Form
    {
        private ShelfRequest shelfRequest;
        public ShelfRequestForm(WebApiConfig config)
        {
            shelfRequest = new ShelfRequest(config);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            ShelfRequestInfo info = new ShelfRequestInfo
            {
                fromShelfId = txtFromShelfId.Text,
                jobId = txtJobID.Text,
                toShelfId = txtToShelfId.Text
            };

            shelfRequest.FunReport(info);
            button1.Enabled = true;
        }
    }
}
