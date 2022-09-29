using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mirle.Structure;
using Mirle.Def.U2NMMA30;

namespace Mirle.WebAPI.Test.WES.testingList
{
    public partial class WESGetBufferNameaAndStnNo : Form
    {
        public WESGetBufferNameaAndStnNo()
        {
            InitializeComponent();
        }

        private void button_GetBuffer_Click(object sender, EventArgs e)
        {
            ConveyorInfo con = new ConveyorInfo();
            ConveyorInfo con2 = new ConveyorInfo();
            con = ConveyorDef.GetBuffer(textBox_bufferName.Text);
            con2 = ConveyorDef.GetBuffer_ByStnNo(textBox_StnNo.Text);

            MessageBox.Show($"Result:   " +
                $"BufferName = {textBox_bufferName.Text} => StnNo = " + con.StnNo + ";  " +
                $"StnNo = {textBox_StnNo.Text} => BufferName = " + con2.BufferName + "."
                , "GetStnNo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
