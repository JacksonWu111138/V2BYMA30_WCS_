
namespace Mirle.EccsSignal_2.View
{
    partial class ucCrane_Sts
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEquNo = new System.Windows.Forms.Label();
            this.lblCrnMode = new System.Windows.Forms.Label();
            this.lblCrnStatus = new System.Windows.Forms.Label();
            this.timMainProc = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblCrnStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCrnMode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblEquNo, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(151, 139);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblEquNo
            // 
            this.lblEquNo.AutoSize = true;
            this.lblEquNo.BackColor = System.Drawing.Color.Yellow;
            this.lblEquNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEquNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEquNo.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEquNo.Location = new System.Drawing.Point(3, 0);
            this.lblEquNo.Name = "lblEquNo";
            this.lblEquNo.Size = new System.Drawing.Size(145, 46);
            this.lblEquNo.TabIndex = 0;
            this.lblEquNo.Text = "label1";
            this.lblEquNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCrnMode
            // 
            this.lblCrnMode.BackColor = System.Drawing.Color.Lime;
            this.lblCrnMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCrnMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCrnMode.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrnMode.ForeColor = System.Drawing.Color.Black;
            this.lblCrnMode.Location = new System.Drawing.Point(4, 46);
            this.lblCrnMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCrnMode.Name = "lblCrnMode";
            this.lblCrnMode.Size = new System.Drawing.Size(143, 46);
            this.lblCrnMode.TabIndex = 229;
            this.lblCrnMode.Text = "R:地上盤模式";
            this.lblCrnMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCrnStatus
            // 
            this.lblCrnStatus.BackColor = System.Drawing.Color.Lime;
            this.lblCrnStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCrnStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCrnStatus.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCrnStatus.ForeColor = System.Drawing.Color.Black;
            this.lblCrnStatus.Location = new System.Drawing.Point(4, 92);
            this.lblCrnStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCrnStatus.Name = "lblCrnStatus";
            this.lblCrnStatus.Size = new System.Drawing.Size(143, 47);
            this.lblCrnStatus.TabIndex = 230;
            this.lblCrnStatus.Text = "X";
            this.lblCrnStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timMainProc
            // 
            this.timMainProc.Interval = 500;
            this.timMainProc.Tick += new System.EventHandler(this.timMainProc_Tick);
            // 
            // ucCrane_Sts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucCrane_Sts";
            this.Size = new System.Drawing.Size(151, 139);
            this.Load += new System.EventHandler(this.ucCrane_Sts_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCrnMode;
        private System.Windows.Forms.Label lblEquNo;
        private System.Windows.Forms.Label lblCrnStatus;
        private System.Windows.Forms.Timer timMainProc;
    }
}
