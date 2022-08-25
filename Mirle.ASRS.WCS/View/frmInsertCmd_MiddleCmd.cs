using Mirle.Def;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.ASRS.WCS.View
{
    public partial class frmInsertCmd_MiddleCmd : Form
    {
        public frmInsertCmd_MiddleCmd()
        {
            InitializeComponent();

            cbbPriority.SelectedIndex = 4;
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            cbbCmdMode.SelectedIndex = -1;
            cbbPriority.SelectedIndex = 4;
            txtCSTID.Text = "";
            txtRackLocation.Text = "";
            txtLargest.Text = "";
            txtDeviceID.Text = "";
            txtCarrierType.Text = "";
            txtLotSize.Text = "";
            txtSource.Text = "";
            txtDestination.Text = "";
            txtBatchID.Text = "";
        }

        private void butSave_Click(object sender, EventArgs e)
        {

        }

        private bool IsOK()
        {
            if (string.IsNullOrWhiteSpace(cbbCmdMode.Text) ||
                string.IsNullOrWhiteSpace(txtCSTID.Text) ||
                string.IsNullOrWhiteSpace(txtCarrierType.Text) ||
                string.IsNullOrWhiteSpace(txtDeviceID.Text))
                return false;

            return true;

            //switch (cbbCmdMode.Text.Trim().Split(':')[0])
            //{
            //    case clsConstValue.CmdMode.StockIn:
            //    case clsConstValue.CmdMode.StockOut:
            //        if (string.IsNullOrWhiteSpace(txtLoc.Text) ||
            //            string.IsNullOrWhiteSpace(txtStnNo.Text))
            //            return false;
            //        else return true;
            //    case clsConstValue.CmdMode.S2S:
            //        if (string.IsNullOrWhiteSpace(txtStnNo.Text) ||
            //            string.IsNullOrWhiteSpace(txtNewLoc.Text))
            //            return false;
            //        else return true;
            //    case clsConstValue.CmdMode.L2L:
            //        if (string.IsNullOrWhiteSpace(txtEquNo.Text) ||
            //            string.IsNullOrWhiteSpace(txtLoc.Text) ||
            //            string.IsNullOrWhiteSpace(txtNewLoc.Text))
            //            return false;
            //        else return true;
            //    default:
            //        return true;
            //}
        }
    }
}
