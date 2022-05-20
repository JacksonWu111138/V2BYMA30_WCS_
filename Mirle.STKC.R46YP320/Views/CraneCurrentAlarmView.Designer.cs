namespace Mirle.STKC.R46YP320.Views
{
    partial class CraneCurrentAlarmView
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
            this.components = new System.ComponentModel.Container();
            this.lsbRM1_CurrentAlarm = new System.Windows.Forms.ListBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lsbRM1_CurrentAlarm
            // 
            this.lsbRM1_CurrentAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbRM1_CurrentAlarm.FormattingEnabled = true;
            this.lsbRM1_CurrentAlarm.Location = new System.Drawing.Point(0, 0);
            this.lsbRM1_CurrentAlarm.Name = "lsbRM1_CurrentAlarm";
            this.lsbRM1_CurrentAlarm.Size = new System.Drawing.Size(584, 441);
            this.lsbRM1_CurrentAlarm.TabIndex = 7;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // CraneCurrentAlarmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.lsbRM1_CurrentAlarm);
            this.Name = "CraneCurrentAlarmView";
            this.Text = "Crane1CurrentAlarm";
            this.Load += new System.EventHandler(this.CraneCurrentAlarm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbRM1_CurrentAlarm;
        private System.Windows.Forms.Timer refreshTimer;
    }
}