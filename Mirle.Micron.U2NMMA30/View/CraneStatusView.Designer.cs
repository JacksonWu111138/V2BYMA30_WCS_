
namespace Mirle.Micron.U2NMMA30.View
{
    partial class CraneStatusView
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
            this.tlpMainSts = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tlpMainSts
            // 
            this.tlpMainSts.ColumnCount = 4;
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMainSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainSts.Location = new System.Drawing.Point(0, 0);
            this.tlpMainSts.Name = "tlpMainSts";
            this.tlpMainSts.RowCount = 1;
            this.tlpMainSts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSts.Size = new System.Drawing.Size(980, 116);
            this.tlpMainSts.TabIndex = 0;
            // 
            // CraneStatusView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 116);
            this.Controls.Add(this.tlpMainSts);
            this.Name = "CraneStatusView";
            this.Text = "CraneStatusView";
            this.Load += new System.EventHandler(this.CraneStatusView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainSts;
    }
}