namespace Mirle.STKC.R46YP320.Views
{
    partial class SystraceView
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
            this.lstSysTrace = new System.Windows.Forms.ListBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lstSysTrace
            // 
            this.lstSysTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSysTrace.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lstSysTrace.FormattingEnabled = true;
            this.lstSysTrace.ItemHeight = 16;
            this.lstSysTrace.Location = new System.Drawing.Point(0, 0);
            this.lstSysTrace.Name = "lstSysTrace";
            this.lstSysTrace.Size = new System.Drawing.Size(584, 441);
            this.lstSysTrace.TabIndex = 11;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // SystraceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.lstSysTrace);
            this.Name = "SystraceView";
            this.Text = "SystraceView";
            this.Load += new System.EventHandler(this.SystraceView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstSysTrace;
        private System.Windows.Forms.Timer refreshTimer;
    }
}