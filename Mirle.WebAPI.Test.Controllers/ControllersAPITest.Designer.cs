namespace Mirle.WebAPI.Test.Controllers
{
    partial class ControllersAPITest
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CtrlCVReceiveNewBinCmd = new System.Windows.Forms.Button();
            this.CtrlControlStatusQuery = new System.Windows.Forms.Button();
            this.CtrlBufferStatusQuery = new System.Windows.Forms.Button();
            this.CtrlBlockStatusQuery = new System.Windows.Forms.Button();
            this.CtrlBufferRollInfo = new System.Windows.Forms.Button();
            this.CtrlHealthCheck = new System.Windows.Forms.Button();
            this.CtrlLotRetrieveTransfer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.CtrlLotRetrieveTransfer, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.CtrlHealthCheck, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CtrlCVReceiveNewBinCmd, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.CtrlControlStatusQuery, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.CtrlBufferStatusQuery, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CtrlBlockStatusQuery, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CtrlBufferRollInfo, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(79, 93);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 354);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // CtrlCVReceiveNewBinCmd
            // 
            this.CtrlCVReceiveNewBinCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlCVReceiveNewBinCmd.Location = new System.Drawing.Point(3, 283);
            this.CtrlCVReceiveNewBinCmd.Name = "CtrlCVReceiveNewBinCmd";
            this.CtrlCVReceiveNewBinCmd.Size = new System.Drawing.Size(210, 68);
            this.CtrlCVReceiveNewBinCmd.TabIndex = 1;
            this.CtrlCVReceiveNewBinCmd.Text = "CVReceiveNewBinCmd";
            this.CtrlCVReceiveNewBinCmd.UseVisualStyleBackColor = true;
            this.CtrlCVReceiveNewBinCmd.Click += new System.EventHandler(this.CtrlCVReceiveNewBinCmd_Click);
            // 
            // CtrlControlStatusQuery
            // 
            this.CtrlControlStatusQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlControlStatusQuery.Location = new System.Drawing.Point(3, 213);
            this.CtrlControlStatusQuery.Name = "CtrlControlStatusQuery";
            this.CtrlControlStatusQuery.Size = new System.Drawing.Size(210, 64);
            this.CtrlControlStatusQuery.TabIndex = 1;
            this.CtrlControlStatusQuery.Text = "ControlStatusQuery";
            this.CtrlControlStatusQuery.UseVisualStyleBackColor = true;
            this.CtrlControlStatusQuery.Click += new System.EventHandler(this.CtrlControlStatusQuery_Click);
            // 
            // CtrlBufferStatusQuery
            // 
            this.CtrlBufferStatusQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlBufferStatusQuery.Location = new System.Drawing.Point(3, 143);
            this.CtrlBufferStatusQuery.Name = "CtrlBufferStatusQuery";
            this.CtrlBufferStatusQuery.Size = new System.Drawing.Size(210, 64);
            this.CtrlBufferStatusQuery.TabIndex = 1;
            this.CtrlBufferStatusQuery.Text = "BufferStatusQuery";
            this.CtrlBufferStatusQuery.UseVisualStyleBackColor = true;
            this.CtrlBufferStatusQuery.Click += new System.EventHandler(this.CtrlBufferStatusQuery_Click);
            // 
            // CtrlBlockStatusQuery
            // 
            this.CtrlBlockStatusQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlBlockStatusQuery.Location = new System.Drawing.Point(3, 3);
            this.CtrlBlockStatusQuery.Name = "CtrlBlockStatusQuery";
            this.CtrlBlockStatusQuery.Size = new System.Drawing.Size(210, 64);
            this.CtrlBlockStatusQuery.TabIndex = 1;
            this.CtrlBlockStatusQuery.Text = "BlockStatusQuery";
            this.CtrlBlockStatusQuery.UseVisualStyleBackColor = true;
            this.CtrlBlockStatusQuery.Click += new System.EventHandler(this.CtrlBlockStatusQuery_Click);
            // 
            // CtrlBufferRollInfo
            // 
            this.CtrlBufferRollInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlBufferRollInfo.Location = new System.Drawing.Point(3, 73);
            this.CtrlBufferRollInfo.Name = "CtrlBufferRollInfo";
            this.CtrlBufferRollInfo.Size = new System.Drawing.Size(210, 64);
            this.CtrlBufferRollInfo.TabIndex = 2;
            this.CtrlBufferRollInfo.Text = "BufferRollInfo";
            this.CtrlBufferRollInfo.UseVisualStyleBackColor = true;
            this.CtrlBufferRollInfo.Click += new System.EventHandler(this.CtrlBufferRollInfo_Click);
            // 
            // CtrlHealthCheck
            // 
            this.CtrlHealthCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlHealthCheck.Location = new System.Drawing.Point(219, 3);
            this.CtrlHealthCheck.Name = "CtrlHealthCheck";
            this.CtrlHealthCheck.Size = new System.Drawing.Size(210, 64);
            this.CtrlHealthCheck.TabIndex = 1;
            this.CtrlHealthCheck.Text = "HealthCheck";
            this.CtrlHealthCheck.UseVisualStyleBackColor = true;
            this.CtrlHealthCheck.Click += new System.EventHandler(this.CtrlHealthCheck_Click);
            // 
            // CtrlLotRetrieveTransfer
            // 
            this.CtrlLotRetrieveTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CtrlLotRetrieveTransfer.Location = new System.Drawing.Point(219, 73);
            this.CtrlLotRetrieveTransfer.Name = "CtrlLotRetrieveTransfer";
            this.CtrlLotRetrieveTransfer.Size = new System.Drawing.Size(210, 64);
            this.CtrlLotRetrieveTransfer.TabIndex = 1;
            this.CtrlLotRetrieveTransfer.Text = "LotRetrieveTransfer";
            this.CtrlLotRetrieveTransfer.UseVisualStyleBackColor = true;
            // 
            // ControllersAPITest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 540);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControllersAPITest";
            this.Text = "Controllers API Test";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CtrlBlockStatusQuery;
        private System.Windows.Forms.Button CtrlBufferRollInfo;
        private System.Windows.Forms.Button CtrlBufferStatusQuery;
        private System.Windows.Forms.Button CtrlControlStatusQuery;
        private System.Windows.Forms.Button CtrlCVReceiveNewBinCmd;
        private System.Windows.Forms.Button CtrlHealthCheck;
        private System.Windows.Forms.Button CtrlLotRetrieveTransfer;
    }
}

