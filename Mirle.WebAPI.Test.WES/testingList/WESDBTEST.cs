using Mirle.DB.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.WebAPI.Test.WES.testingList
{
    public partial class WESDBTEST : Form
    {
        public WESDBTEST()
        {
            InitializeComponent();
        }

        private void button_testDBConnection_Click(object sender, EventArgs e)
        {
            if(!clsDB_Proc.GetWmsDB_Object().GetLocMst().FunTestConnection())
            {
                MessageBox.Show($"失敗.", "WES DB Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"成功.", "WES DB Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
