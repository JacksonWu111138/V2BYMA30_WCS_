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
    public partial class frmInsertCmd_CmdMst : Form
    {
        public frmInsertCmd_CmdMst()
        {
            InitializeComponent();
        }

        private void butClear_Click(object sender, EventArgs e)
        {
            cbbCmdMode.SelectedIndex = -1;
            cbbPriority.SelectedIndex = 4;
            txtBoxID.Text = "";
            txtCurDeviceID.Text = "";
            txtCurLoc.Text = "";
            txtEquNo.Text = "";
            txtCarrierType.Text = "";
            txtJobID.Text = "";
            txtLoc.Text = "";
            txtNewLoc.Text = "";
            txtStnNo.Text = "";
        }

        private void butSave_Click(object sender, EventArgs e)
        {

        }

        private bool IsOK()
        {
            if (string.IsNullOrWhiteSpace(cbbCmdMode.Text) ||
                string.IsNullOrWhiteSpace(txtBoxID.Text) ||
                string.IsNullOrWhiteSpace(txtCarrierType.Text) ||
                string.IsNullOrWhiteSpace(txtJobID.Text))
                return false;

            switch (cbbCmdMode.Text.Trim().Split(':')[0])
            {
                case clsConstValue.CmdMode.StockIn:

                    break;
            }

            return false;
        }
    }
}
