using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.Def;
using Mirle.WebAPI.Test.WES.testingList;

namespace Mirle.WebAPI.Test.WES
{
    public partial class WESAPITest : Form
    {
        private WebApiConfig WESApi_config = new WebApiConfig();
        public WESAPITest()
        {

        }
        public WESAPITest(WebApiConfig WesAPI_config)
        {
            InitializeComponent();
            WESApi_config = WesAPI_config;
        }

        private void WESPosititonReport_Click(object sender, EventArgs e)
        {
            WESPositionReport form = new WESPositionReport(WESApi_config);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WESLotPositionReport form = new WESLotPositionReport(WESApi_config);
        }

        private void WESLotPositionReport_Click(object sender, EventArgs e)
        {
            WESLotPositionReport form = new WESLotPositionReport(WESApi_config);
            form.Show();
        }

        private void WESNGPositionReport_Click(object sender, EventArgs e)
        {
            WESNGPositionReport form = new WESNGPositionReport(WESApi_config);
            form.Show();
        }

        private void WESCarrierReturnNext_Click(object sender, EventArgs e)
        {
            WESCarrierReturnNext form = new WESCarrierReturnNext(WESApi_config);
            form.Show();
        }

        private void WESCarrierTransferComplete_Click(object sender, EventArgs e)
        {
            WESCarrierTransferComplete form = new WESCarrierTransferComplete(WESApi_config);
            form.Show();
        }

        private void WESCarrierPutawayCheck_Click(object sender, EventArgs e)
        {
            WESCarrierPutawayCheck form = new WESCarrierPutawayCheck(WESApi_config);
            form.Show();
        }

        private void WESCarrierPutawayComplete_Click(object sender, EventArgs e)
        {
            WESCarrierPutawayComplete form = new WESCarrierPutawayComplete(WESApi_config);
            form.Show();
        }

        private void WESLotPutawayCheck_Click(object sender, EventArgs e)
        {
            WESLotPutawayCheck form = new WESLotPutawayCheck(WESApi_config);
            form.Show();
        }

        private void WESLotPutawayComplete_Click(object sender, EventArgs e)
        {
            WESLotPutawayComplete form = new WESLotPutawayComplete(WESApi_config);
            form.Show();
        }

        private void WESLotShelfReport_Click(object sender, EventArgs e)
        {
            WESLotShelfReport form = new WESLotShelfReport(WESApi_config);
            form.Show();
        }

        private void WESCarrierShelfReport_Click(object sender, EventArgs e)
        {
            WESCarrierShelfReport form = new WESCarrierShelfReport(WESApi_config);
            form.Show();
        }

        private void WESCarrierShelfRequest_Click(object sender, EventArgs e)
        {
            WESCarrierShelfRequest form = new WESCarrierShelfRequest(WESApi_config);
            form.Show();
        }

        private void WESLotShelfComplete_Click(object sender, EventArgs e)
        {
            
        }

        private void WESLotShelfRequest_Click(object sender, EventArgs e)
        {
            WESLotShelfRequest form = new WESLotShelfRequest(WESApi_config);
            form.Show();
        }

        private void WESCarrierShelfComplete_Click(object sender, EventArgs e)
        {
            WESCarrierShelfComplete form = new WESCarrierShelfComplete(WESApi_config);
            form.Show();
        }

        private void WESLotShelfComplete_Click_1(object sender, EventArgs e)
        {
            WESLotShelfComplete form = new WESLotShelfComplete(WESApi_config);
            form.Show();
        }

        private void WESEmptyShelfQuery_Click(object sender, EventArgs e)
        {
            WESEmptyShelfQuery form = new WESEmptyShelfQuery(WESApi_config);
            form.Show();
        }

        private void WESWCSCancel_Click(object sender, EventArgs e)
        {
            WESWCSCancel form = new WESWCSCancel(WESApi_config);
            form.Show();
        }

        private void WESEmptyCarrierUnload_Click(object sender, EventArgs e)
        {
            WESEmptyCarrierUnload form = new WESEmptyCarrierUnload(WESApi_config);
            form.Show();
        }

        private void WESPortStatusUpload_Click(object sender, EventArgs e)
        {
            WESPortStatusUpdate form = new WESPortStatusUpdate(WESApi_config);
            form.Show();
        }

        private void WESLotRetrieveComplete_Click(object sender, EventArgs e)
        {
            WESLotRetrieveComplete form = new WESLotRetrieveComplete(WESApi_config);
            form.Show();
        }

        private void WESCarrierRetrieveComplete_Click(object sender, EventArgs e)
        {
            WESCarrierRetrieveComplete form = new WESCarrierRetrieveComplete(WESApi_config);
            form.Show();
        }

        private void WESEmptyMagazineUnload_Click(object sender, EventArgs e)
        {
            WESEmptyMagazineUnload form = new WESEmptyMagazineUnload(WESApi_config);
            form.Show();
        }

        private void WESEmptyMagazineLoadRequest_Click(object sender, EventArgs e)
        {
            WESEmptyMagazineLoadRequest form = new WESEmptyMagazineLoadRequest(WESApi_config);
            form.Show();
        }

        private void WESMagazineLoadRequest_Click(object sender, EventArgs e)
        {
            WESMagazineLoadRequest form = new WESMagazineLoadRequest(WESApi_config);
            form.Show();
        }

        private void WESLotRenewRequest_Click(object sender, EventArgs e)
        {
            WESLotRenewRequest form = new WESLotRenewRequest(WESApi_config);
            form.Show();
        }

        private void WESEmptyESDCarrierUnload_Click(object sender, EventArgs e)
        {
            WESEmptyESDCarrierUnload form = new WESEmptyESDCarrierUnload(WESApi_config);
            form.Show();
        }

        private void WESEmptyESDCarrierLoadRequest_Click(object sender, EventArgs e)
        {
            WESEmptyESDCarrierLoadRequest form = new WESEmptyESDCarrierLoadRequest(WESApi_config);
            form.Show();
        }

        private void WESEQPStatusUpdate_Click(object sender, EventArgs e)
        {
            WESEQPStatusUpdate form = new WESEQPStatusUpdate(WESApi_config);
            form.Show();
        }

        private void WESRemoveRackShow_Click(object sender, EventArgs e)
        {
            WESRemoveRackShow form = new WESRemoveRackShow(WESApi_config);
            form.Show();
        }

        private void WESRemoveRackDown_Click(object sender, EventArgs e)
        {
            WESRemoveRackDown form = new WESRemoveRackDown(WESApi_config);
            form.Show();
        }
    }
}
