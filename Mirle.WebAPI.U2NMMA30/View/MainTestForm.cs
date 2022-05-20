using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Mirle.Def;
using System.Windows.Forms;

namespace Mirle.WebAPI.U2NMMA30.View
{
    public partial class MainTestForm : Form
    {
        private WebApiConfig _config = new WebApiConfig();
        public MainTestForm(WebApiConfig config)
        {
            _config = config;
            InitializeComponent();
        }

        private AutoPickupForm autoPickup;
        private void btnAutoPickup_Click(object sender, EventArgs e)
        {
            if (autoPickup == null)
            {
                autoPickup = new AutoPickupForm(_config);
                autoPickup.TopMost = true;
                autoPickup.FormClosed += new FormClosedEventHandler(funAutoPickup_FormClosed);
                autoPickup.Show();
            }
            else
            {
                autoPickup.BringToFront();
            }
        }

        private void funAutoPickup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (autoPickup != null)
                autoPickup = null;
        }

        private PickupQueryForm pickupQuery;
        private void btnPickupQuery_Click(object sender, EventArgs e)
        {
            if (pickupQuery == null)
            {
                pickupQuery = new PickupQueryForm(_config);
                pickupQuery.TopMost = true;
                pickupQuery.FormClosed += new FormClosedEventHandler(funPickupQuery_FormClosed);
                pickupQuery.Show();
            }
            else
            {
                pickupQuery.BringToFront();
            }
        }

        private void funPickupQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (pickupQuery != null)
                pickupQuery = null;
        }

        private PositionReportForm positionReport;
        private void btnPositionReport_Click(object sender, EventArgs e)
        {
            if (positionReport == null)
            {
                positionReport = new PositionReportForm(_config);
                positionReport.TopMost = true;
                positionReport.FormClosed += new FormClosedEventHandler(funPositionReport_FormClosed);
                positionReport.Show();
            }
            else
            {
                positionReport.BringToFront();
            }
        }

        private void funPositionReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (positionReport != null)
                positionReport = null;
        }

        private PutawayCheckForm putawayCheck;
        private void btnPutawayCheck_Click(object sender, EventArgs e)
        {
            if (putawayCheck == null)
            {
                putawayCheck = new PutawayCheckForm(_config);
                putawayCheck.TopMost = true;
                putawayCheck.FormClosed += new FormClosedEventHandler(funPutawayCheck_FormClosed);
                putawayCheck.Show();
            }
            else
            {
                putawayCheck.BringToFront();
            }
        }

        private void funPutawayCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (putawayCheck != null)
                putawayCheck = null;
        }

        private PutAwayCompleteForm putAwayCompleteForm;
        private void btnPutAwayComplete_Click(object sender, EventArgs e)
        {
            if (putAwayCompleteForm == null)
            {
                putAwayCompleteForm = new PutAwayCompleteForm(_config);
                putAwayCompleteForm.TopMost = true;
                putAwayCompleteForm.FormClosed += new FormClosedEventHandler(funPutawayComplete_FormClosed);
                putAwayCompleteForm.Show();
            }
            else
            {
                putAwayCompleteForm.BringToFront();
            }
        }

        private void funPutawayComplete_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (putAwayCompleteForm != null)
                putAwayCompleteForm = null;
        }

        private RetrieveCompleteForm retrieveCompleteForm;
        private void btnRetrieveComplete_Click(object sender, EventArgs e)
        {
            if (retrieveCompleteForm == null)
            {
                retrieveCompleteForm = new RetrieveCompleteForm(_config);
                retrieveCompleteForm.TopMost = true;
                retrieveCompleteForm.FormClosed += new FormClosedEventHandler(funRetrieveComplete_FormClosed);
                retrieveCompleteForm.Show();
            }
            else
            {
                retrieveCompleteForm.BringToFront();
            }
        }

        private void funRetrieveComplete_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (retrieveCompleteForm != null)
                retrieveCompleteForm = null;
        }

        private ShelfCompleteForm shelfComplete;
        private void btnShelfComplete_Click(object sender, EventArgs e)
        {
            if (shelfComplete == null)
            {
                shelfComplete = new ShelfCompleteForm(_config);
                shelfComplete.TopMost = true;
                shelfComplete.FormClosed += new FormClosedEventHandler(funShelfComplete_FormClosed);
                shelfComplete.Show();
            }
            else
            {
                shelfComplete.BringToFront();
            }
        }

        private void funShelfComplete_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (shelfComplete != null)
                shelfComplete = null;
        }

        private ShelfReportForm shelfReport;
        private void btnShelfReport_Click(object sender, EventArgs e)
        {
            if (shelfReport == null)
            {
                shelfReport = new ShelfReportForm(_config);
                shelfReport.TopMost = true;
                shelfReport.FormClosed += new FormClosedEventHandler(funShelfReport_FormClosed);
                shelfReport.Show();
            }
            else
            {
                shelfReport.BringToFront();
            }
        }

        private void funShelfReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (shelfReport != null)
                shelfReport = null;
        }

        private ShelfRequestForm shelfRequest;
        private void btnShelfRequest_Click(object sender, EventArgs e)
        {
            if (shelfRequest == null)
            {
                shelfRequest = new ShelfRequestForm(_config);
                shelfRequest.TopMost = true;
                shelfRequest.FormClosed += new FormClosedEventHandler(funShelfRequest_FormClosed);
                shelfRequest.Show();
            }
            else
            {
                shelfRequest.BringToFront();
            }
        }

        private void funShelfRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (shelfRequest != null)
                shelfRequest = null;
        }
    }
}
