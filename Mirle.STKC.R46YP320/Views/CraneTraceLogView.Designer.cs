namespace Mirle.STKC.R46YP320.Views
{
    partial class CraneTraceLogView
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
            this.lsbRM1_CMDTrace = new System.Windows.Forms.ListBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lsbRM1_CMDTrace
            // 
            this.lsbRM1_CMDTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbRM1_CMDTrace.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lsbRM1_CMDTrace.FormattingEnabled = true;
            this.lsbRM1_CMDTrace.ItemHeight = 17;
            this.lsbRM1_CMDTrace.Location = new System.Drawing.Point(0, 0);
            this.lsbRM1_CMDTrace.Name = "lsbRM1_CMDTrace";
            this.lsbRM1_CMDTrace.Size = new System.Drawing.Size(584, 441);
            this.lsbRM1_CMDTrace.TabIndex = 6;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // Crane1CmdTraceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.lsbRM1_CMDTrace);
            this.Name = "Crane1CmdTraceView";
            this.Text = "Crane1CmdTraceView";
            this.Load += new System.EventHandler(this.CraneTraceLogView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbRM1_CMDTrace;
        private System.Windows.Forms.Timer refreshTimer;
    }
}