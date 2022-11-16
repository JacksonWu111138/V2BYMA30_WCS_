namespace Mirle.ASRS.WCS.View
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tlpMainSts = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.picMirle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDBConn_WMS = new System.Windows.Forms.Label();
            this.chkOnline = new System.Windows.Forms.CheckBox();
            this.lblDBConn_WCS = new System.Windows.Forms.Label();
            this.pnlCraneSts = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbcCmdInfo = new System.Windows.Forms.TabControl();
            this.tbpCmdMst = new System.Windows.Forms.TabPage();
            this.Grid1 = new System.Windows.Forms.DataGridView();
            this.tbpMiddleCmd = new System.Windows.Forms.TabPage();
            this.Grid_MiddleCmd = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_Controller_API_TEST = new System.Windows.Forms.Button();
            this.AGVTaskCancelButten = new System.Windows.Forms.Button();
            this.mnuTransferCmd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTransferCmdComplete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTransferCmdCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateCurLoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiddleCmd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMiddleComplete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMiddleCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertMiddleCmd = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tlpMainSts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMirle)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tbcCmdInfo.SuspendLayout();
            this.tbpCmdMst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).BeginInit();
            this.tbpMiddleCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_MiddleCmd)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuTransferCmd.SuspendLayout();
            this.mnuMiddleCmd.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tlpMainSts);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1756, 898);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // tlpMainSts
            // 
            this.tlpMainSts.ColumnCount = 4;
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tlpMainSts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tlpMainSts.Controls.Add(this.lblTimer, 0, 0);
            this.tlpMainSts.Controls.Add(this.picMirle, 0, 0);
            this.tlpMainSts.Controls.Add(this.tableLayoutPanel2, 3, 0);
            this.tlpMainSts.Controls.Add(this.pnlCraneSts, 2, 0);
            this.tlpMainSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainSts.Location = new System.Drawing.Point(0, 0);
            this.tlpMainSts.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMainSts.Name = "tlpMainSts";
            this.tlpMainSts.RowCount = 1;
            this.tlpMainSts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainSts.Size = new System.Drawing.Size(1756, 118);
            this.tlpMainSts.TabIndex = 0;
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.SystemColors.Control;
            this.lblTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Black;
            this.lblTimer.Location = new System.Drawing.Point(249, 0);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(237, 118);
            this.lblTimer.TabIndex = 268;
            this.lblTimer.Text = "yyyy/MM/dd hh:mm:ss";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picMirle
            // 
            this.picMirle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMirle.Image = ((System.Drawing.Image)(resources.GetObject("picMirle.Image")));
            this.picMirle.Location = new System.Drawing.Point(4, 4);
            this.picMirle.Margin = new System.Windows.Forms.Padding(4);
            this.picMirle.Name = "picMirle";
            this.picMirle.Size = new System.Drawing.Size(237, 110);
            this.picMirle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMirle.TabIndex = 267;
            this.picMirle.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblDBConn_WMS, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkOnline, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDBConn_WCS, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1512, 4);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(240, 110);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblDBConn_WMS
            // 
            this.lblDBConn_WMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDBConn_WMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDBConn_WMS.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDBConn_WMS.Location = new System.Drawing.Point(4, 35);
            this.lblDBConn_WMS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDBConn_WMS.Name = "lblDBConn_WMS";
            this.lblDBConn_WMS.Size = new System.Drawing.Size(232, 35);
            this.lblDBConn_WMS.TabIndex = 3;
            this.lblDBConn_WMS.Text = "WES DB Sts";
            this.lblDBConn_WMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkOnline
            // 
            this.chkOnline.AutoSize = true;
            this.chkOnline.Checked = true;
            this.chkOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOnline.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkOnline.Location = new System.Drawing.Point(4, 74);
            this.chkOnline.Margin = new System.Windows.Forms.Padding(4);
            this.chkOnline.Name = "chkOnline";
            this.chkOnline.Size = new System.Drawing.Size(232, 32);
            this.chkOnline.TabIndex = 2;
            this.chkOnline.Text = "OnLine";
            this.chkOnline.UseVisualStyleBackColor = true;
            this.chkOnline.Visible = false;
            this.chkOnline.CheckedChanged += new System.EventHandler(this.chkOnline_CheckedChanged);
            // 
            // lblDBConn_WCS
            // 
            this.lblDBConn_WCS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDBConn_WCS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDBConn_WCS.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDBConn_WCS.Location = new System.Drawing.Point(4, 0);
            this.lblDBConn_WCS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDBConn_WCS.Name = "lblDBConn_WCS";
            this.lblDBConn_WCS.Size = new System.Drawing.Size(232, 35);
            this.lblDBConn_WCS.TabIndex = 1;
            this.lblDBConn_WCS.Text = "WCS DB Sts";
            this.lblDBConn_WCS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCraneSts
            // 
            this.pnlCraneSts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCraneSts.Location = new System.Drawing.Point(493, 3);
            this.pnlCraneSts.Name = "pnlCraneSts";
            this.pnlCraneSts.Size = new System.Drawing.Size(1012, 112);
            this.pnlCraneSts.TabIndex = 269;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbcCmdInfo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(1756, 774);
            this.splitContainer2.SplitterDistance = 1538;
            this.splitContainer2.TabIndex = 1;
            // 
            // tbcCmdInfo
            // 
            this.tbcCmdInfo.Controls.Add(this.tbpCmdMst);
            this.tbcCmdInfo.Controls.Add(this.tbpMiddleCmd);
            this.tbcCmdInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcCmdInfo.Font = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbcCmdInfo.Location = new System.Drawing.Point(0, 0);
            this.tbcCmdInfo.Name = "tbcCmdInfo";
            this.tbcCmdInfo.SelectedIndex = 0;
            this.tbcCmdInfo.Size = new System.Drawing.Size(1538, 774);
            this.tbcCmdInfo.TabIndex = 0;
            // 
            // tbpCmdMst
            // 
            this.tbpCmdMst.Controls.Add(this.Grid1);
            this.tbpCmdMst.Location = new System.Drawing.Point(4, 34);
            this.tbpCmdMst.Name = "tbpCmdMst";
            this.tbpCmdMst.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCmdMst.Size = new System.Drawing.Size(1530, 736);
            this.tbpCmdMst.TabIndex = 0;
            this.tbpCmdMst.Text = "System Command";
            this.tbpCmdMst.UseVisualStyleBackColor = true;
            // 
            // Grid1
            // 
            this.Grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid1.Location = new System.Drawing.Point(3, 3);
            this.Grid1.Margin = new System.Windows.Forms.Padding(4);
            this.Grid1.Name = "Grid1";
            this.Grid1.RowHeadersWidth = 62;
            this.Grid1.RowTemplate.Height = 24;
            this.Grid1.Size = new System.Drawing.Size(1524, 730);
            this.Grid1.TabIndex = 1;
            // 
            // tbpMiddleCmd
            // 
            this.tbpMiddleCmd.Controls.Add(this.Grid_MiddleCmd);
            this.tbpMiddleCmd.Location = new System.Drawing.Point(4, 34);
            this.tbpMiddleCmd.Name = "tbpMiddleCmd";
            this.tbpMiddleCmd.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMiddleCmd.Size = new System.Drawing.Size(1530, 736);
            this.tbpMiddleCmd.TabIndex = 1;
            this.tbpMiddleCmd.Text = "Middle Command";
            this.tbpMiddleCmd.UseVisualStyleBackColor = true;
            // 
            // Grid_MiddleCmd
            // 
            this.Grid_MiddleCmd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_MiddleCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_MiddleCmd.Location = new System.Drawing.Point(3, 3);
            this.Grid_MiddleCmd.Margin = new System.Windows.Forms.Padding(4);
            this.Grid_MiddleCmd.Name = "Grid_MiddleCmd";
            this.Grid_MiddleCmd.RowHeadersWidth = 62;
            this.Grid_MiddleCmd.RowTemplate.Height = 24;
            this.Grid_MiddleCmd.Size = new System.Drawing.Size(1524, 730);
            this.Grid_MiddleCmd.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.button_Controller_API_TEST, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.AGVTaskCancelButten, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 774);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_Controller_API_TEST
            // 
            this.button_Controller_API_TEST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Controller_API_TEST.Location = new System.Drawing.Point(3, 57);
            this.button_Controller_API_TEST.Name = "button_Controller_API_TEST";
            this.button_Controller_API_TEST.Size = new System.Drawing.Size(208, 34);
            this.button_Controller_API_TEST.TabIndex = 0;
            this.button_Controller_API_TEST.Text = "Controller API test";
            this.button_Controller_API_TEST.UseVisualStyleBackColor = true;
            this.button_Controller_API_TEST.Click += new System.EventHandler(this.button_Controller_API_TEST_Click);
            // 
            // AGVTaskCancelButten
            // 
            this.AGVTaskCancelButten.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AGVTaskCancelButten.Location = new System.Drawing.Point(4, 5);
            this.AGVTaskCancelButten.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AGVTaskCancelButten.Name = "AGVTaskCancelButten";
            this.AGVTaskCancelButten.Size = new System.Drawing.Size(206, 44);
            this.AGVTaskCancelButten.TabIndex = 11;
            this.AGVTaskCancelButten.Text = "WES API Testing";
            this.AGVTaskCancelButten.UseVisualStyleBackColor = true;
            this.AGVTaskCancelButten.Click += new System.EventHandler(this.button1_Click);
            // 
            // mnuTransferCmd
            // 
            this.mnuTransferCmd.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuTransferCmd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTransferCmdComplete,
            this.mnuTransferCmdCancel,
            this.mnuInsertCmd,
            this.mnuUpdateCurLoc});
            this.mnuTransferCmd.Name = "mnuFun";
            this.mnuTransferCmd.Size = new System.Drawing.Size(215, 132);
            // 
            // mnuTransferCmdComplete
            // 
            this.mnuTransferCmdComplete.Name = "mnuTransferCmdComplete";
            this.mnuTransferCmdComplete.Size = new System.Drawing.Size(214, 32);
            this.mnuTransferCmdComplete.Text = "Complete";
            this.mnuTransferCmdComplete.Click += new System.EventHandler(this.mnuTransferCmdComplete_Click);
            // 
            // mnuTransferCmdCancel
            // 
            this.mnuTransferCmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("mnuTransferCmdCancel.Image")));
            this.mnuTransferCmdCancel.Name = "mnuTransferCmdCancel";
            this.mnuTransferCmdCancel.Size = new System.Drawing.Size(214, 32);
            this.mnuTransferCmdCancel.Text = "Cancel";
            this.mnuTransferCmdCancel.Click += new System.EventHandler(this.mnuTransferCmdCancel_Click);
            // 
            // mnuInsertCmd
            // 
            this.mnuInsertCmd.Name = "mnuInsertCmd";
            this.mnuInsertCmd.Size = new System.Drawing.Size(214, 32);
            this.mnuInsertCmd.Text = "Insert";
            this.mnuInsertCmd.Click += new System.EventHandler(this.mnuInsertCmd_Click);
            // 
            // mnuUpdateCurLoc
            // 
            this.mnuUpdateCurLoc.Name = "mnuUpdateCurLoc";
            this.mnuUpdateCurLoc.Size = new System.Drawing.Size(214, 32);
            this.mnuUpdateCurLoc.Text = "Update CurLoc";
            this.mnuUpdateCurLoc.Click += new System.EventHandler(this.mnuUpdateCurLoc_Click);
            // 
            // mnuMiddleCmd
            // 
            this.mnuMiddleCmd.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMiddleCmd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMiddleComplete,
            this.mnuMiddleCancel,
            this.mnuInsertMiddleCmd});
            this.mnuMiddleCmd.Name = "mnuFun";
            this.mnuMiddleCmd.Size = new System.Drawing.Size(172, 100);
            // 
            // mnuMiddleComplete
            // 
            this.mnuMiddleComplete.Name = "mnuMiddleComplete";
            this.mnuMiddleComplete.Size = new System.Drawing.Size(171, 32);
            this.mnuMiddleComplete.Text = "Complete";
            this.mnuMiddleComplete.Click += new System.EventHandler(this.mnuMiddleComplete_Click);
            // 
            // mnuMiddleCancel
            // 
            this.mnuMiddleCancel.Image = ((System.Drawing.Image)(resources.GetObject("mnuMiddleCancel.Image")));
            this.mnuMiddleCancel.Name = "mnuMiddleCancel";
            this.mnuMiddleCancel.Size = new System.Drawing.Size(171, 32);
            this.mnuMiddleCancel.Text = "Cancel";
            this.mnuMiddleCancel.Click += new System.EventHandler(this.mnuMiddleCancel_Click);
            // 
            // mnuInsertMiddleCmd
            // 
            this.mnuInsertMiddleCmd.Name = "mnuInsertMiddleCmd";
            this.mnuInsertMiddleCmd.Size = new System.Drawing.Size(171, 32);
            this.mnuInsertMiddleCmd.Text = "Insert";
            this.mnuInsertMiddleCmd.Click += new System.EventHandler(this.mnuInsertMiddleCmd_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1756, 898);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tlpMainSts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMirle)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tbcCmdInfo.ResumeLayout(false);
            this.tbpCmdMst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).EndInit();
            this.tbpMiddleCmd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_MiddleCmd)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.mnuTransferCmd.ResumeLayout(false);
            this.mnuMiddleCmd.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblDBConn_WCS;
        private System.Windows.Forms.CheckBox chkOnline;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tlpMainSts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox picMirle;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblDBConn_WMS;
        private System.Windows.Forms.Button AGVTaskCancelButten;
        private System.Windows.Forms.TabControl tbcCmdInfo;
        private System.Windows.Forms.TabPage tbpCmdMst;
        private System.Windows.Forms.DataGridView Grid1;
        private System.Windows.Forms.TabPage tbpMiddleCmd;
        private System.Windows.Forms.ContextMenuStrip mnuTransferCmd;
        private System.Windows.Forms.ToolStripMenuItem mnuTransferCmdComplete;
        private System.Windows.Forms.ToolStripMenuItem mnuTransferCmdCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertCmd;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateCurLoc;
        private System.Windows.Forms.ContextMenuStrip mnuMiddleCmd;
        private System.Windows.Forms.ToolStripMenuItem mnuMiddleComplete;
        private System.Windows.Forms.ToolStripMenuItem mnuMiddleCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertMiddleCmd;
        private System.Windows.Forms.DataGridView Grid_MiddleCmd;
        private System.Windows.Forms.Panel pnlCraneSts;
        private System.Windows.Forms.Button button_Controller_API_TEST;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}

