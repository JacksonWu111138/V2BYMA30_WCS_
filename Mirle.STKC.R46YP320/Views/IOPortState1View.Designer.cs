namespace Mirle.STKC.R46YP320.Views
{
    partial class IOPortState1View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOPortState1View));
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpIO_Sts1 = new System.Windows.Forms.TableLayoutPanel();
            this.butResetControl = new System.Windows.Forms.Button();
            this.lblIORun = new System.Windows.Forms.Label();
            this.lblIORunEnable = new System.Windows.Forms.Label();
            this.lblIODown = new System.Windows.Forms.Label();
            this.lblIOFault = new System.Windows.Forms.Label();
            this.lblIOAMMode = new System.Windows.Forms.Label();
            this.lblIOLoadOK = new System.Windows.Forms.Label();
            this.lblIOUnloadOK = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.butIOAutoManual = new System.Windows.Forms.Button();
            this.butIO_FaultReset = new System.Windows.Forms.Button();
            this.butIO_BuzzerStop = new System.Windows.Forms.Button();
            this.butIO_PortModeChangeReq = new System.Windows.Forms.Button();
            this.butIO_EnableDisableP1FBCR = new System.Windows.Forms.Button();
            this.butIO_ManualTriggerFBCRRescan = new System.Windows.Forms.Button();
            this.butIO_MoveBackForMGV = new System.Windows.Forms.Button();
            this.lblIOPortModeChangeable = new System.Windows.Forms.Label();
            this.tlpPositionCSTID = new System.Windows.Forms.TableLayoutPanel();
            this.lblPos5 = new System.Windows.Forms.Label();
            this.lblIOLoadPosition5 = new System.Windows.Forms.Label();
            this.lblIOLoadPosition4 = new System.Windows.Forms.Label();
            this.lblIOLoadPosition3 = new System.Windows.Forms.Label();
            this.lblPos4 = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.lblIOLoadPosition2 = new System.Windows.Forms.Label();
            this.lblIOLoadPosition1 = new System.Windows.Forms.Label();
            this.lblPos1 = new System.Windows.Forms.Label();
            this.lblPos2 = new System.Windows.Forms.Label();
            this.lblPos3 = new System.Windows.Forms.Label();
            this.lblIOInMode = new System.Windows.Forms.Label();
            this.lblIOOutMode = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIOBCRReadDone_Req = new System.Windows.Forms.Label();
            this.lblIOFBCRResultCSTID = new System.Windows.Forms.Label();
            this.lblIOCSTRemoveCheck_Req = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIOPLCBatteryLow = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.lblIOAlarmCode = new System.Windows.Forms.Label();
            this.label243 = new System.Windows.Forms.Label();
            this.label244 = new System.Windows.Forms.Label();
            this.lblIOPLCAlarmIndex = new System.Windows.Forms.Label();
            this.label245 = new System.Windows.Forms.Label();
            this.lblIOPCAlarmIndex = new System.Windows.Forms.Label();
            this.lblIOWaitIn = new System.Windows.Forms.Label();
            this.lblIOWaitOut = new System.Windows.Forms.Label();
            this.chkSendRequest2SM = new System.Windows.Forms.CheckBox();
            this.lblP1_FBCREnable = new System.Windows.Forms.Label();
            this.chkAutoSetRun = new System.Windows.Forms.CheckBox();
            this.tlpIO_Sts1.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tlpPositionCSTID.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tlpIO_Sts1
            // 
            this.tlpIO_Sts1.ColumnCount = 4;
            this.tlpIO_Sts1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIO_Sts1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIO_Sts1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIO_Sts1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIO_Sts1.Controls.Add(this.chkAutoSetRun, 0, 0);
            this.tlpIO_Sts1.Controls.Add(this.butResetControl, 2, 0);
            this.tlpIO_Sts1.Controls.Add(this.lblIORun, 0, 1);
            this.tlpIO_Sts1.Controls.Add(this.lblIORunEnable, 1, 0);
            this.tlpIO_Sts1.Controls.Add(this.lblIODown, 0, 2);
            this.tlpIO_Sts1.Controls.Add(this.lblIOFault, 1, 1);
            this.tlpIO_Sts1.Controls.Add(this.lblIOAMMode, 2, 1);
            this.tlpIO_Sts1.Controls.Add(this.lblIOLoadOK, 1, 2);
            this.tlpIO_Sts1.Controls.Add(this.lblIOUnloadOK, 1, 3);
            this.tlpIO_Sts1.Controls.Add(this.tableLayoutPanel13, 3, 0);
            this.tlpIO_Sts1.Controls.Add(this.lblIOPortModeChangeable, 2, 2);
            this.tlpIO_Sts1.Controls.Add(this.tlpPositionCSTID, 0, 11);
            this.tlpIO_Sts1.Controls.Add(this.lblIOInMode, 0, 5);
            this.tlpIO_Sts1.Controls.Add(this.lblIOOutMode, 1, 5);
            this.tlpIO_Sts1.Controls.Add(this.groupBox1, 2, 8);
            this.tlpIO_Sts1.Controls.Add(this.lblIOCSTRemoveCheck_Req, 2, 6);
            this.tlpIO_Sts1.Controls.Add(this.groupBox2, 1, 8);
            this.tlpIO_Sts1.Controls.Add(this.groupBox3, 0, 7);
            this.tlpIO_Sts1.Controls.Add(this.lblIOWaitIn, 0, 6);
            this.tlpIO_Sts1.Controls.Add(this.lblIOWaitOut, 1, 6);
            this.tlpIO_Sts1.Controls.Add(this.chkSendRequest2SM, 1, 7);
            this.tlpIO_Sts1.Controls.Add(this.lblP1_FBCREnable, 2, 3);
            this.tlpIO_Sts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIO_Sts1.Location = new System.Drawing.Point(0, 0);
            this.tlpIO_Sts1.Name = "tlpIO_Sts1";
            this.tlpIO_Sts1.RowCount = 13;
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tlpIO_Sts1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpIO_Sts1.Size = new System.Drawing.Size(584, 441);
            this.tlpIO_Sts1.TabIndex = 20;
            // 
            // butResetControl
            // 
            this.butResetControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butResetControl.Location = new System.Drawing.Point(295, 3);
            this.butResetControl.Name = "butResetControl";
            this.butResetControl.Size = new System.Drawing.Size(140, 27);
            this.butResetControl.TabIndex = 231;
            this.butResetControl.Text = "Reset Control";
            this.butResetControl.UseVisualStyleBackColor = true;
            this.butResetControl.Click += new System.EventHandler(this.butResetControl_Click);
            // 
            // lblIORun
            // 
            this.lblIORun.BackColor = System.Drawing.SystemColors.Control;
            this.lblIORun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIORun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIORun.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIORun.ForeColor = System.Drawing.Color.Blue;
            this.lblIORun.Location = new System.Drawing.Point(2, 34);
            this.lblIORun.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIORun.Name = "lblIORun";
            this.lblIORun.Size = new System.Drawing.Size(142, 31);
            this.lblIORun.TabIndex = 167;
            this.lblIORun.Text = "Auto (Run)";
            this.lblIORun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIORunEnable
            // 
            this.lblIORunEnable.BackColor = System.Drawing.SystemColors.Control;
            this.lblIORunEnable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIORunEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIORunEnable.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIORunEnable.ForeColor = System.Drawing.Color.Blue;
            this.lblIORunEnable.Location = new System.Drawing.Point(148, 1);
            this.lblIORunEnable.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIORunEnable.Name = "lblIORunEnable";
            this.lblIORunEnable.Size = new System.Drawing.Size(142, 31);
            this.lblIORunEnable.TabIndex = 167;
            this.lblIORunEnable.Text = "Run Enable";
            this.lblIORunEnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIODown
            // 
            this.lblIODown.BackColor = System.Drawing.SystemColors.Control;
            this.lblIODown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIODown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIODown.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIODown.ForeColor = System.Drawing.Color.Blue;
            this.lblIODown.Location = new System.Drawing.Point(2, 67);
            this.lblIODown.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIODown.Name = "lblIODown";
            this.lblIODown.Size = new System.Drawing.Size(142, 31);
            this.lblIODown.TabIndex = 169;
            this.lblIODown.Text = "Manual (Down)";
            this.lblIODown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOFault
            // 
            this.lblIOFault.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOFault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOFault.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOFault.ForeColor = System.Drawing.Color.Blue;
            this.lblIOFault.Location = new System.Drawing.Point(148, 34);
            this.lblIOFault.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOFault.Name = "lblIOFault";
            this.lblIOFault.Size = new System.Drawing.Size(142, 31);
            this.lblIOFault.TabIndex = 195;
            this.lblIOFault.Text = "Fault";
            this.lblIOFault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOAMMode
            // 
            this.lblIOAMMode.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOAMMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOAMMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOAMMode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOAMMode.ForeColor = System.Drawing.Color.Blue;
            this.lblIOAMMode.Location = new System.Drawing.Point(294, 34);
            this.lblIOAMMode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOAMMode.Name = "lblIOAMMode";
            this.lblIOAMMode.Size = new System.Drawing.Size(142, 31);
            this.lblIOAMMode.TabIndex = 209;
            this.lblIOAMMode.Text = "Auto/Manual Mode";
            this.lblIOAMMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOLoadOK
            // 
            this.lblIOLoadOK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOLoadOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadOK.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadOK.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadOK.Location = new System.Drawing.Point(148, 67);
            this.lblIOLoadOK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOLoadOK.Name = "lblIOLoadOK";
            this.lblIOLoadOK.Size = new System.Drawing.Size(142, 31);
            this.lblIOLoadOK.TabIndex = 198;
            this.lblIOLoadOK.Text = "Load OK";
            this.lblIOLoadOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOUnloadOK
            // 
            this.lblIOUnloadOK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOUnloadOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOUnloadOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOUnloadOK.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOUnloadOK.ForeColor = System.Drawing.Color.Blue;
            this.lblIOUnloadOK.Location = new System.Drawing.Point(148, 100);
            this.lblIOUnloadOK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOUnloadOK.Name = "lblIOUnloadOK";
            this.lblIOUnloadOK.Size = new System.Drawing.Size(142, 31);
            this.lblIOUnloadOK.TabIndex = 204;
            this.lblIOUnloadOK.Text = "Unload OK";
            this.lblIOUnloadOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.butIOAutoManual, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.butIO_FaultReset, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.butIO_BuzzerStop, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.butIO_PortModeChangeReq, 0, 3);
            this.tableLayoutPanel13.Controls.Add(this.butIO_EnableDisableP1FBCR, 0, 4);
            this.tableLayoutPanel13.Controls.Add(this.butIO_ManualTriggerFBCRRescan, 0, 5);
            this.tableLayoutPanel13.Controls.Add(this.butIO_MoveBackForMGV, 0, 6);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(441, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 7;
            this.tlpIO_Sts1.SetRowSpan(this.tableLayoutPanel13, 11);
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(140, 357);
            this.tableLayoutPanel13.TabIndex = 221;
            // 
            // butIOAutoManual
            // 
            this.butIOAutoManual.BackColor = System.Drawing.Color.Gainsboro;
            this.butIOAutoManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIOAutoManual.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIOAutoManual.ForeColor = System.Drawing.Color.Black;
            this.butIOAutoManual.Image = ((System.Drawing.Image)(resources.GetObject("butIOAutoManual.Image")));
            this.butIOAutoManual.Location = new System.Drawing.Point(1, 1);
            this.butIOAutoManual.Margin = new System.Windows.Forms.Padding(1);
            this.butIOAutoManual.Name = "butIOAutoManual";
            this.butIOAutoManual.Size = new System.Drawing.Size(138, 49);
            this.butIOAutoManual.TabIndex = 217;
            this.butIOAutoManual.Text = "Manual";
            this.butIOAutoManual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIOAutoManual.UseVisualStyleBackColor = false;
            this.butIOAutoManual.Click += new System.EventHandler(this.butIOAutoManual_Click);
            // 
            // butIO_FaultReset
            // 
            this.butIO_FaultReset.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_FaultReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_FaultReset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_FaultReset.Image = ((System.Drawing.Image)(resources.GetObject("butIO_FaultReset.Image")));
            this.butIO_FaultReset.Location = new System.Drawing.Point(1, 52);
            this.butIO_FaultReset.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_FaultReset.Name = "butIO_FaultReset";
            this.butIO_FaultReset.Size = new System.Drawing.Size(138, 49);
            this.butIO_FaultReset.TabIndex = 28;
            this.butIO_FaultReset.Text = "Fault Reset";
            this.butIO_FaultReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_FaultReset.UseVisualStyleBackColor = false;
            this.butIO_FaultReset.Click += new System.EventHandler(this.butIO_FaultReset_Click);
            // 
            // butIO_BuzzerStop
            // 
            this.butIO_BuzzerStop.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_BuzzerStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_BuzzerStop.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_BuzzerStop.Image = ((System.Drawing.Image)(resources.GetObject("butIO_BuzzerStop.Image")));
            this.butIO_BuzzerStop.Location = new System.Drawing.Point(1, 103);
            this.butIO_BuzzerStop.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_BuzzerStop.Name = "butIO_BuzzerStop";
            this.butIO_BuzzerStop.Size = new System.Drawing.Size(138, 49);
            this.butIO_BuzzerStop.TabIndex = 25;
            this.butIO_BuzzerStop.Text = "Buzzer Stop";
            this.butIO_BuzzerStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_BuzzerStop.UseVisualStyleBackColor = false;
            this.butIO_BuzzerStop.Click += new System.EventHandler(this.butIO_BuzzerStop_Click);
            // 
            // butIO_PortModeChangeReq
            // 
            this.butIO_PortModeChangeReq.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_PortModeChangeReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_PortModeChangeReq.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_PortModeChangeReq.Location = new System.Drawing.Point(1, 154);
            this.butIO_PortModeChangeReq.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_PortModeChangeReq.Name = "butIO_PortModeChangeReq";
            this.butIO_PortModeChangeReq.Size = new System.Drawing.Size(138, 49);
            this.butIO_PortModeChangeReq.TabIndex = 25;
            this.butIO_PortModeChangeReq.Text = "PortMode Change Request";
            this.butIO_PortModeChangeReq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_PortModeChangeReq.UseVisualStyleBackColor = false;
            this.butIO_PortModeChangeReq.Click += new System.EventHandler(this.butIO_PortModeChangeReq_Click);
            // 
            // butIO_EnableDisableP1FBCR
            // 
            this.butIO_EnableDisableP1FBCR.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_EnableDisableP1FBCR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_EnableDisableP1FBCR.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_EnableDisableP1FBCR.Location = new System.Drawing.Point(1, 205);
            this.butIO_EnableDisableP1FBCR.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_EnableDisableP1FBCR.Name = "butIO_EnableDisableP1FBCR";
            this.butIO_EnableDisableP1FBCR.Size = new System.Drawing.Size(138, 49);
            this.butIO_EnableDisableP1FBCR.TabIndex = 25;
            this.butIO_EnableDisableP1FBCR.Tag = "";
            this.butIO_EnableDisableP1FBCR.Text = "Disable Load Port FBCR";
            this.butIO_EnableDisableP1FBCR.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_EnableDisableP1FBCR.UseVisualStyleBackColor = false;
            this.butIO_EnableDisableP1FBCR.Click += new System.EventHandler(this.butIO_EnableDisableP1FBCR_Click);
            // 
            // butIO_ManualTriggerFBCRRescan
            // 
            this.butIO_ManualTriggerFBCRRescan.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_ManualTriggerFBCRRescan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_ManualTriggerFBCRRescan.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_ManualTriggerFBCRRescan.Location = new System.Drawing.Point(1, 256);
            this.butIO_ManualTriggerFBCRRescan.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_ManualTriggerFBCRRescan.Name = "butIO_ManualTriggerFBCRRescan";
            this.butIO_ManualTriggerFBCRRescan.Size = new System.Drawing.Size(138, 49);
            this.butIO_ManualTriggerFBCRRescan.TabIndex = 25;
            this.butIO_ManualTriggerFBCRRescan.Text = "Manual Trigger FBCR Rescan";
            this.butIO_ManualTriggerFBCRRescan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_ManualTriggerFBCRRescan.UseVisualStyleBackColor = false;
            this.butIO_ManualTriggerFBCRRescan.Click += new System.EventHandler(this.butIO_ManualTriggerFBCRRescan_Click);
            // 
            // butIO_MoveBackForMGV
            // 
            this.butIO_MoveBackForMGV.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_MoveBackForMGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_MoveBackForMGV.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIO_MoveBackForMGV.Location = new System.Drawing.Point(1, 307);
            this.butIO_MoveBackForMGV.Margin = new System.Windows.Forms.Padding(1);
            this.butIO_MoveBackForMGV.Name = "butIO_MoveBackForMGV";
            this.butIO_MoveBackForMGV.Size = new System.Drawing.Size(138, 49);
            this.butIO_MoveBackForMGV.TabIndex = 25;
            this.butIO_MoveBackForMGV.Text = "MoveBack for MGV Port";
            this.butIO_MoveBackForMGV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIO_MoveBackForMGV.UseVisualStyleBackColor = false;
            this.butIO_MoveBackForMGV.Click += new System.EventHandler(this.butIO_MoveBackForMGV_Click);
            // 
            // lblIOPortModeChangeable
            // 
            this.lblIOPortModeChangeable.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOPortModeChangeable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOPortModeChangeable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOPortModeChangeable.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOPortModeChangeable.ForeColor = System.Drawing.Color.Blue;
            this.lblIOPortModeChangeable.Location = new System.Drawing.Point(294, 67);
            this.lblIOPortModeChangeable.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOPortModeChangeable.Name = "lblIOPortModeChangeable";
            this.lblIOPortModeChangeable.Size = new System.Drawing.Size(142, 31);
            this.lblIOPortModeChangeable.TabIndex = 215;
            this.lblIOPortModeChangeable.Text = "PortModeChangeable";
            this.lblIOPortModeChangeable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpPositionCSTID
            // 
            this.tlpPositionCSTID.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpPositionCSTID.ColumnCount = 6;
            this.tlpIO_Sts1.SetColumnSpan(this.tlpPositionCSTID, 4);
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8F));
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8F));
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8F));
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8F));
            this.tlpPositionCSTID.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.8F));
            this.tlpPositionCSTID.Controls.Add(this.lblPos5, 5, 0);
            this.tlpPositionCSTID.Controls.Add(this.lblIOLoadPosition5, 5, 1);
            this.tlpPositionCSTID.Controls.Add(this.lblIOLoadPosition4, 4, 1);
            this.tlpPositionCSTID.Controls.Add(this.lblIOLoadPosition3, 3, 1);
            this.tlpPositionCSTID.Controls.Add(this.lblPos4, 4, 0);
            this.tlpPositionCSTID.Controls.Add(this.label196, 0, 0);
            this.tlpPositionCSTID.Controls.Add(this.lblIOLoadPosition2, 2, 1);
            this.tlpPositionCSTID.Controls.Add(this.lblIOLoadPosition1, 1, 1);
            this.tlpPositionCSTID.Controls.Add(this.lblPos1, 1, 0);
            this.tlpPositionCSTID.Controls.Add(this.lblPos2, 2, 0);
            this.tlpPositionCSTID.Controls.Add(this.lblPos3, 3, 0);
            this.tlpPositionCSTID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPositionCSTID.Location = new System.Drawing.Point(0, 363);
            this.tlpPositionCSTID.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPositionCSTID.Name = "tlpPositionCSTID";
            this.tlpPositionCSTID.Padding = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.tlpPositionCSTID.RowCount = 1;
            this.tlpIO_Sts1.SetRowSpan(this.tlpPositionCSTID, 2);
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlpPositionCSTID.Size = new System.Drawing.Size(584, 78);
            this.tlpPositionCSTID.TabIndex = 227;
            // 
            // lblPos5
            // 
            this.lblPos5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPos5.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPos5.ForeColor = System.Drawing.Color.Blue;
            this.lblPos5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos5.Location = new System.Drawing.Point(478, 2);
            this.lblPos5.Name = "lblPos5";
            this.lblPos5.Size = new System.Drawing.Size(98, 36);
            this.lblPos5.TabIndex = 205;
            this.lblPos5.Text = "Pos.5";
            this.lblPos5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPos5.Visible = false;
            // 
            // lblIOLoadPosition5
            // 
            this.lblIOLoadPosition5.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOLoadPosition5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadPosition5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadPosition5.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadPosition5.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadPosition5.Location = new System.Drawing.Point(477, 40);
            this.lblIOLoadPosition5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblIOLoadPosition5.Name = "lblIOLoadPosition5";
            this.lblIOLoadPosition5.Size = new System.Drawing.Size(100, 33);
            this.lblIOLoadPosition5.TabIndex = 208;
            this.lblIOLoadPosition5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIOLoadPosition5.Visible = false;
            // 
            // lblIOLoadPosition4
            // 
            this.lblIOLoadPosition4.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOLoadPosition4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadPosition4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadPosition4.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadPosition4.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadPosition4.Location = new System.Drawing.Point(375, 40);
            this.lblIOLoadPosition4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblIOLoadPosition4.Name = "lblIOLoadPosition4";
            this.lblIOLoadPosition4.Size = new System.Drawing.Size(96, 33);
            this.lblIOLoadPosition4.TabIndex = 208;
            this.lblIOLoadPosition4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIOLoadPosition4.Visible = false;
            // 
            // lblIOLoadPosition3
            // 
            this.lblIOLoadPosition3.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOLoadPosition3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadPosition3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadPosition3.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadPosition3.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadPosition3.Location = new System.Drawing.Point(273, 40);
            this.lblIOLoadPosition3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblIOLoadPosition3.Name = "lblIOLoadPosition3";
            this.lblIOLoadPosition3.Size = new System.Drawing.Size(96, 33);
            this.lblIOLoadPosition3.TabIndex = 208;
            this.lblIOLoadPosition3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIOLoadPosition3.Visible = false;
            // 
            // lblPos4
            // 
            this.lblPos4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPos4.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPos4.ForeColor = System.Drawing.Color.Blue;
            this.lblPos4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos4.Location = new System.Drawing.Point(376, 2);
            this.lblPos4.Name = "lblPos4";
            this.lblPos4.Size = new System.Drawing.Size(94, 36);
            this.lblPos4.TabIndex = 205;
            this.lblPos4.Text = "Pos.4";
            this.lblPos4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPos4.Visible = false;
            // 
            // label196
            // 
            this.label196.BackColor = System.Drawing.Color.Black;
            this.label196.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label196.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label196.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label196.ForeColor = System.Drawing.Color.White;
            this.label196.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label196.Location = new System.Drawing.Point(5, 3);
            this.label196.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label196.Name = "label196";
            this.tlpPositionCSTID.SetRowSpan(this.label196, 2);
            this.label196.Size = new System.Drawing.Size(58, 72);
            this.label196.TabIndex = 205;
            this.label196.Text = "Stocker Side";
            this.label196.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIOLoadPosition2
            // 
            this.lblIOLoadPosition2.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOLoadPosition2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadPosition2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadPosition2.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadPosition2.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadPosition2.Location = new System.Drawing.Point(171, 40);
            this.lblIOLoadPosition2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblIOLoadPosition2.Name = "lblIOLoadPosition2";
            this.lblIOLoadPosition2.Size = new System.Drawing.Size(96, 33);
            this.lblIOLoadPosition2.TabIndex = 208;
            this.lblIOLoadPosition2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIOLoadPosition2.Visible = false;
            // 
            // lblIOLoadPosition1
            // 
            this.lblIOLoadPosition1.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOLoadPosition1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOLoadPosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOLoadPosition1.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOLoadPosition1.ForeColor = System.Drawing.Color.Blue;
            this.lblIOLoadPosition1.Location = new System.Drawing.Point(69, 40);
            this.lblIOLoadPosition1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblIOLoadPosition1.Name = "lblIOLoadPosition1";
            this.lblIOLoadPosition1.Size = new System.Drawing.Size(96, 33);
            this.lblIOLoadPosition1.TabIndex = 208;
            this.lblIOLoadPosition1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPos1
            // 
            this.lblPos1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPos1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPos1.ForeColor = System.Drawing.Color.Blue;
            this.lblPos1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos1.Location = new System.Drawing.Point(70, 2);
            this.lblPos1.Name = "lblPos1";
            this.lblPos1.Size = new System.Drawing.Size(94, 36);
            this.lblPos1.TabIndex = 205;
            this.lblPos1.Text = "Pos.1";
            this.lblPos1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPos2
            // 
            this.lblPos2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPos2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPos2.ForeColor = System.Drawing.Color.Blue;
            this.lblPos2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos2.Location = new System.Drawing.Point(172, 2);
            this.lblPos2.Name = "lblPos2";
            this.lblPos2.Size = new System.Drawing.Size(94, 36);
            this.lblPos2.TabIndex = 205;
            this.lblPos2.Text = "Pos.2";
            this.lblPos2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPos2.Visible = false;
            // 
            // lblPos3
            // 
            this.lblPos3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPos3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPos3.ForeColor = System.Drawing.Color.Blue;
            this.lblPos3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPos3.Location = new System.Drawing.Point(274, 2);
            this.lblPos3.Name = "lblPos3";
            this.lblPos3.Size = new System.Drawing.Size(94, 36);
            this.lblPos3.TabIndex = 205;
            this.lblPos3.Text = "Pos.3";
            this.lblPos3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPos3.Visible = false;
            // 
            // lblIOInMode
            // 
            this.lblIOInMode.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOInMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOInMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOInMode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOInMode.ForeColor = System.Drawing.Color.Blue;
            this.lblIOInMode.Location = new System.Drawing.Point(2, 166);
            this.lblIOInMode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOInMode.Name = "lblIOInMode";
            this.lblIOInMode.Size = new System.Drawing.Size(142, 31);
            this.lblIOInMode.TabIndex = 199;
            this.lblIOInMode.Text = "Input Mode";
            this.lblIOInMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOOutMode
            // 
            this.lblIOOutMode.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOOutMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOOutMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOOutMode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOOutMode.ForeColor = System.Drawing.Color.Blue;
            this.lblIOOutMode.Location = new System.Drawing.Point(148, 166);
            this.lblIOOutMode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOOutMode.Name = "lblIOOutMode";
            this.lblIOOutMode.Size = new System.Drawing.Size(142, 31);
            this.lblIOOutMode.TabIndex = 203;
            this.lblIOOutMode.Text = "Output Mode";
            this.lblIOOutMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel16);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(295, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.tlpIO_Sts1.SetRowSpan(this.groupBox1, 3);
            this.groupBox1.Size = new System.Drawing.Size(140, 93);
            this.groupBox1.TabIndex = 229;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fixed BCR";
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 1;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Controls.Add(this.lblIOBCRReadDone_Req, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.lblIOFBCRResultCSTID, 0, 1);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 13);
            this.tableLayoutPanel16.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 2;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(140, 80);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // lblIOBCRReadDone_Req
            // 
            this.lblIOBCRReadDone_Req.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOBCRReadDone_Req.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOBCRReadDone_Req.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOBCRReadDone_Req.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOBCRReadDone_Req.ForeColor = System.Drawing.Color.Blue;
            this.lblIOBCRReadDone_Req.Location = new System.Drawing.Point(1, 0);
            this.lblIOBCRReadDone_Req.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblIOBCRReadDone_Req.Name = "lblIOBCRReadDone_Req";
            this.lblIOBCRReadDone_Req.Size = new System.Drawing.Size(138, 40);
            this.lblIOBCRReadDone_Req.TabIndex = 224;
            this.lblIOBCRReadDone_Req.Text = "BCR Read Done";
            this.lblIOBCRReadDone_Req.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOFBCRResultCSTID
            // 
            this.lblIOFBCRResultCSTID.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOFBCRResultCSTID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOFBCRResultCSTID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOFBCRResultCSTID.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOFBCRResultCSTID.ForeColor = System.Drawing.Color.Blue;
            this.lblIOFBCRResultCSTID.Location = new System.Drawing.Point(1, 40);
            this.lblIOFBCRResultCSTID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblIOFBCRResultCSTID.Name = "lblIOFBCRResultCSTID";
            this.lblIOFBCRResultCSTID.Size = new System.Drawing.Size(138, 40);
            this.lblIOFBCRResultCSTID.TabIndex = 226;
            this.lblIOFBCRResultCSTID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIOCSTRemoveCheck_Req
            // 
            this.lblIOCSTRemoveCheck_Req.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOCSTRemoveCheck_Req.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOCSTRemoveCheck_Req.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOCSTRemoveCheck_Req.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOCSTRemoveCheck_Req.ForeColor = System.Drawing.Color.Blue;
            this.lblIOCSTRemoveCheck_Req.Location = new System.Drawing.Point(294, 199);
            this.lblIOCSTRemoveCheck_Req.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOCSTRemoveCheck_Req.Name = "lblIOCSTRemoveCheck_Req";
            this.lblIOCSTRemoveCheck_Req.Size = new System.Drawing.Size(142, 31);
            this.lblIOCSTRemoveCheck_Req.TabIndex = 228;
            this.lblIOCSTRemoveCheck_Req.Text = "CST Remove Check";
            this.lblIOCSTRemoveCheck_Req.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel17);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.groupBox2.Location = new System.Drawing.Point(149, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.tlpIO_Sts1.SetRowSpan(this.groupBox2, 3);
            this.groupBox2.Size = new System.Drawing.Size(140, 93);
            this.groupBox2.TabIndex = 229;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Battery Low";
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.lblIOPLCBatteryLow, 0, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(0, 18);
            this.tableLayoutPanel17.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 2;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(140, 75);
            this.tableLayoutPanel17.TabIndex = 0;
            // 
            // lblIOPLCBatteryLow
            // 
            this.lblIOPLCBatteryLow.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOPLCBatteryLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOPLCBatteryLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOPLCBatteryLow.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOPLCBatteryLow.ForeColor = System.Drawing.Color.Blue;
            this.lblIOPLCBatteryLow.Location = new System.Drawing.Point(2, 1);
            this.lblIOPLCBatteryLow.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOPLCBatteryLow.Name = "lblIOPLCBatteryLow";
            this.lblIOPLCBatteryLow.Size = new System.Drawing.Size(136, 35);
            this.lblIOPLCBatteryLow.TabIndex = 167;
            this.lblIOPLCBatteryLow.Text = "PLC CPU Battery";
            this.lblIOPLCBatteryLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel18);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(3, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.tlpIO_Sts1.SetRowSpan(this.groupBox3, 4);
            this.groupBox3.Size = new System.Drawing.Size(140, 126);
            this.groupBox3.TabIndex = 229;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AlarmData";
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.Controls.Add(this.lblIOAlarmCode, 1, 2);
            this.tableLayoutPanel18.Controls.Add(this.label243, 0, 2);
            this.tableLayoutPanel18.Controls.Add(this.label244, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.lblIOPLCAlarmIndex, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.label245, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.lblIOPCAlarmIndex, 1, 1);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(0, 18);
            this.tableLayoutPanel18.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 3;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(140, 108);
            this.tableLayoutPanel18.TabIndex = 0;
            // 
            // lblIOAlarmCode
            // 
            this.lblIOAlarmCode.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOAlarmCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOAlarmCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOAlarmCode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOAlarmCode.ForeColor = System.Drawing.Color.Blue;
            this.lblIOAlarmCode.Location = new System.Drawing.Point(72, 73);
            this.lblIOAlarmCode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOAlarmCode.Name = "lblIOAlarmCode";
            this.lblIOAlarmCode.Size = new System.Drawing.Size(66, 34);
            this.lblIOAlarmCode.TabIndex = 184;
            this.lblIOAlarmCode.Text = "0000";
            this.lblIOAlarmCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label243
            // 
            this.label243.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label243.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label243.ForeColor = System.Drawing.Color.Blue;
            this.label243.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label243.Location = new System.Drawing.Point(3, 72);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(64, 36);
            this.label243.TabIndex = 183;
            this.label243.Text = "ErrCode";
            this.label243.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label244
            // 
            this.label244.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label244.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label244.ForeColor = System.Drawing.Color.Blue;
            this.label244.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label244.Location = new System.Drawing.Point(3, 0);
            this.label244.Name = "label244";
            this.label244.Size = new System.Drawing.Size(64, 36);
            this.label244.TabIndex = 175;
            this.label244.Text = "PLC Idx";
            this.label244.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIOPLCAlarmIndex
            // 
            this.lblIOPLCAlarmIndex.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOPLCAlarmIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOPLCAlarmIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOPLCAlarmIndex.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOPLCAlarmIndex.ForeColor = System.Drawing.Color.Blue;
            this.lblIOPLCAlarmIndex.Location = new System.Drawing.Point(72, 1);
            this.lblIOPLCAlarmIndex.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOPLCAlarmIndex.Name = "lblIOPLCAlarmIndex";
            this.lblIOPLCAlarmIndex.Size = new System.Drawing.Size(66, 34);
            this.lblIOPLCAlarmIndex.TabIndex = 180;
            this.lblIOPLCAlarmIndex.Text = "0";
            this.lblIOPLCAlarmIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label245
            // 
            this.label245.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label245.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label245.ForeColor = System.Drawing.Color.Blue;
            this.label245.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label245.Location = new System.Drawing.Point(3, 36);
            this.label245.Name = "label245";
            this.label245.Size = new System.Drawing.Size(64, 36);
            this.label245.TabIndex = 181;
            this.label245.Text = "PC Idx";
            this.label245.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIOPCAlarmIndex
            // 
            this.lblIOPCAlarmIndex.BackColor = System.Drawing.SystemColors.Info;
            this.lblIOPCAlarmIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOPCAlarmIndex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOPCAlarmIndex.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOPCAlarmIndex.ForeColor = System.Drawing.Color.Blue;
            this.lblIOPCAlarmIndex.Location = new System.Drawing.Point(72, 37);
            this.lblIOPCAlarmIndex.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOPCAlarmIndex.Name = "lblIOPCAlarmIndex";
            this.lblIOPCAlarmIndex.Size = new System.Drawing.Size(66, 34);
            this.lblIOPCAlarmIndex.TabIndex = 182;
            this.lblIOPCAlarmIndex.Text = "0";
            this.lblIOPCAlarmIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIOWaitIn
            // 
            this.lblIOWaitIn.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOWaitIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOWaitIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOWaitIn.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOWaitIn.ForeColor = System.Drawing.Color.Blue;
            this.lblIOWaitIn.Location = new System.Drawing.Point(2, 199);
            this.lblIOWaitIn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOWaitIn.Name = "lblIOWaitIn";
            this.lblIOWaitIn.Size = new System.Drawing.Size(142, 31);
            this.lblIOWaitIn.TabIndex = 222;
            this.lblIOWaitIn.Text = "Wait In";
            this.lblIOWaitIn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIOWaitOut
            // 
            this.lblIOWaitOut.BackColor = System.Drawing.SystemColors.Control;
            this.lblIOWaitOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIOWaitOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIOWaitOut.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIOWaitOut.ForeColor = System.Drawing.Color.Blue;
            this.lblIOWaitOut.Location = new System.Drawing.Point(148, 199);
            this.lblIOWaitOut.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIOWaitOut.Name = "lblIOWaitOut";
            this.lblIOWaitOut.Size = new System.Drawing.Size(142, 31);
            this.lblIOWaitOut.TabIndex = 223;
            this.lblIOWaitOut.Text = "Wait Out";
            this.lblIOWaitOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSendRequest2SM
            // 
            this.chkSendRequest2SM.AutoSize = true;
            this.tlpIO_Sts1.SetColumnSpan(this.chkSendRequest2SM, 2);
            this.chkSendRequest2SM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSendRequest2SM.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkSendRequest2SM.ForeColor = System.Drawing.Color.Blue;
            this.chkSendRequest2SM.Location = new System.Drawing.Point(148, 236);
            this.chkSendRequest2SM.Margin = new System.Windows.Forms.Padding(2, 5, 2, 0);
            this.chkSendRequest2SM.Name = "chkSendRequest2SM";
            this.chkSendRequest2SM.Size = new System.Drawing.Size(288, 28);
            this.chkSendRequest2SM.TabIndex = 220;
            this.chkSendRequest2SM.Text = "Direction Change Request to MCS";
            this.chkSendRequest2SM.UseVisualStyleBackColor = true;
            this.chkSendRequest2SM.Visible = false;
            this.chkSendRequest2SM.CheckedChanged += new System.EventHandler(this.chkSendRequest2SM_CheckedChanged);
            // 
            // lblP1_FBCREnable
            // 
            this.lblP1_FBCREnable.BackColor = System.Drawing.SystemColors.Control;
            this.lblP1_FBCREnable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblP1_FBCREnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblP1_FBCREnable.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblP1_FBCREnable.ForeColor = System.Drawing.Color.Blue;
            this.lblP1_FBCREnable.Location = new System.Drawing.Point(294, 100);
            this.lblP1_FBCREnable.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblP1_FBCREnable.Name = "lblP1_FBCREnable";
            this.lblP1_FBCREnable.Size = new System.Drawing.Size(142, 31);
            this.lblP1_FBCREnable.TabIndex = 203;
            this.lblP1_FBCREnable.Text = "Disable Load Port FBCR";
            this.lblP1_FBCREnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoSetRun
            // 
            this.chkAutoSetRun.AutoSize = true;
            this.chkAutoSetRun.Checked = true;
            this.chkAutoSetRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoSetRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAutoSetRun.Location = new System.Drawing.Point(3, 3);
            this.chkAutoSetRun.Name = "chkAutoSetRun";
            this.chkAutoSetRun.Size = new System.Drawing.Size(140, 27);
            this.chkAutoSetRun.TabIndex = 232;
            this.chkAutoSetRun.Text = "Auto Set Run";
            this.chkAutoSetRun.UseVisualStyleBackColor = true;
            // 
            // IOPortState1View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpIO_Sts1);
            this.Name = "IOPortState1View";
            this.Text = "IOState1View";
            this.Load += new System.EventHandler(this.IOPortState1View_Load);
            this.VisibleChanged += new System.EventHandler(this.IOPortState1View_VisibleChanged);
            this.tlpIO_Sts1.ResumeLayout(false);
            this.tlpIO_Sts1.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tlpPositionCSTID.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TableLayoutPanel tlpIO_Sts1;
        internal System.Windows.Forms.Label lblIORun;
        internal System.Windows.Forms.Label lblIORunEnable;
        internal System.Windows.Forms.Label lblIODown;
        internal System.Windows.Forms.Label lblIOFault;
        internal System.Windows.Forms.Label lblIOAMMode;
        internal System.Windows.Forms.Label lblIOLoadOK;
        internal System.Windows.Forms.Label lblIOUnloadOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Button butIOAutoManual;
        private System.Windows.Forms.Button butIO_FaultReset;
        private System.Windows.Forms.Button butIO_BuzzerStop;
        private System.Windows.Forms.Button butIO_PortModeChangeReq;
        private System.Windows.Forms.Button butIO_EnableDisableP1FBCR;
        private System.Windows.Forms.Button butIO_ManualTriggerFBCRRescan;
        private System.Windows.Forms.Button butIO_MoveBackForMGV;
        internal System.Windows.Forms.Label lblIOPortModeChangeable;
        private System.Windows.Forms.TableLayoutPanel tlpPositionCSTID;
        internal System.Windows.Forms.Label lblPos5;
        internal System.Windows.Forms.Label lblIOLoadPosition5;
        internal System.Windows.Forms.Label lblIOLoadPosition4;
        internal System.Windows.Forms.Label lblIOLoadPosition3;
        internal System.Windows.Forms.Label lblPos4;
        internal System.Windows.Forms.Label label196;
        internal System.Windows.Forms.Label lblIOLoadPosition2;
        internal System.Windows.Forms.Label lblIOLoadPosition1;
        internal System.Windows.Forms.Label lblPos1;
        internal System.Windows.Forms.Label lblPos2;
        internal System.Windows.Forms.Label lblPos3;
        internal System.Windows.Forms.Label lblIOInMode;
        internal System.Windows.Forms.Label lblIOOutMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        internal System.Windows.Forms.Label lblIOBCRReadDone_Req;
        internal System.Windows.Forms.Label lblIOFBCRResultCSTID;
        internal System.Windows.Forms.Label lblIOCSTRemoveCheck_Req;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        internal System.Windows.Forms.Label lblIOPLCBatteryLow;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        internal System.Windows.Forms.Label lblIOAlarmCode;
        internal System.Windows.Forms.Label label243;
        internal System.Windows.Forms.Label label244;
        internal System.Windows.Forms.Label lblIOPLCAlarmIndex;
        internal System.Windows.Forms.Label label245;
        internal System.Windows.Forms.Label lblIOPCAlarmIndex;
        internal System.Windows.Forms.Label lblIOWaitIn;
        internal System.Windows.Forms.Label lblIOWaitOut;
        private System.Windows.Forms.CheckBox chkSendRequest2SM;
        internal System.Windows.Forms.Label lblP1_FBCREnable;
        private System.Windows.Forms.Button butResetControl;
        private System.Windows.Forms.CheckBox chkAutoSetRun;
    }
}