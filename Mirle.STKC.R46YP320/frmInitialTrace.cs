using System;
using System.Windows.Forms;

namespace Mirle.STKC.R46YP320
{
    public partial class frmInitialTrace : Form
    {
        public frmInitialTrace()
        {
            InitializeComponent();
        }

        private void frmInitialTrace_Load(object sender, EventArgs e)
        {
        }

        public void Finish()
        {
            this.Invoke(new Action(() =>
            {
                this.Close();
            }));
        }

        public void Trace(string msg)
        {
            this.Invoke(new Action(() =>
            {
                listBox1.Items.Add(msg);
                if (listBox1.Items.Count > 0) listBox1.SelectedIndex = listBox1.Items.Count - 1;
                Application.DoEvents();
            }));
        }
    }
}