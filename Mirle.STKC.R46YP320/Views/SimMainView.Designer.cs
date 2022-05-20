namespace Mirle.STKC.R46YP320.Views
{
    partial class SimMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpMPLCSetting = new System.Windows.Forms.TabPage();
            this.tbpAlarm = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpMPLCSetting);
            this.tabControl1.Controls.Add(this.tbpAlarm);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 631);
            this.tabControl1.TabIndex = 0;
            // 
            // tbpMPLCSetting
            // 
            this.tbpMPLCSetting.Location = new System.Drawing.Point(4, 22);
            this.tbpMPLCSetting.Name = "tbpMPLCSetting";
            this.tbpMPLCSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMPLCSetting.Size = new System.Drawing.Size(676, 605);
            this.tbpMPLCSetting.TabIndex = 0;
            this.tbpMPLCSetting.Text = "MPLCSetting";
            this.tbpMPLCSetting.UseVisualStyleBackColor = true;
            // 
            // tbpAlarm
            // 
            this.tbpAlarm.Location = new System.Drawing.Point(4, 22);
            this.tbpAlarm.Name = "tbpAlarm";
            this.tbpAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAlarm.Size = new System.Drawing.Size(525, 332);
            this.tbpAlarm.TabIndex = 1;
            this.tbpAlarm.Text = "Alarm";
            this.tbpAlarm.UseVisualStyleBackColor = true;
            // 
            // SimMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 631);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "SimMainView";
            this.Text = "SimMainView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimMainView_FormClosing);
            this.Load += new System.EventHandler(this.SimMainView_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpMPLCSetting;
        private System.Windows.Forms.TabPage tbpAlarm;
    }
}