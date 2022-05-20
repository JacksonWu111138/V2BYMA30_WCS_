using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.Micron.U2NMMA30.View
{
    public partial class CraneStatusView : Form
    {
        public CraneStatusView()
        {
            InitializeComponent();
        }

        private void CraneStatusView_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 5; i++)
            {
                var subForm = clsMicronStocker.GetSTKCHostById(i).GetStockerStsView();
                subForm.TopLevel = false;
                subForm.Dock = DockStyle.Fill;//適應窗體大小
                subForm.FormBorderStyle = FormBorderStyle.None;//隱藏右上角的按鈕
                subForm.Parent = tlpMainSts;
                tlpMainSts.Controls.Add(subForm, i - 1, 0);
                subForm.Show();
            }
        }
    }
}
