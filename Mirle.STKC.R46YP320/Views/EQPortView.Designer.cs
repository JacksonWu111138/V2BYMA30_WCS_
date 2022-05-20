namespace Mirle.STKC.R46YP320.Views
{
    partial class EQPortView
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
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpMain_EQPort = new System.Windows.Forms.TableLayoutPanel();
            this.cboEQPort = new System.Windows.Forms.ComboBox();
            this.lblEQCSTID = new System.Windows.Forms.Label();
            this.lblEQ_L_REQ = new System.Windows.Forms.Label();
            this.lblEQ_U_REQ = new System.Windows.Forms.Label();
            this.lblEQ_Ready = new System.Windows.Forms.Label();
            this.lblEQ_Carrier = new System.Windows.Forms.Label();
            this.lblEQ_POnLine = new System.Windows.Forms.Label();
            this.lblEQ_PEStop = new System.Windows.Forms.Label();
            this.lblEQ_Transferring_FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_TR_REQ_FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_BUSY_FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_COMPLETE_FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_PError = new System.Windows.Forms.Label();
            this.lblEQ_Spare = new System.Windows.Forms.Label();
            this.lblEQ_Crane1FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_Crane2FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_AError_FromSTK = new System.Windows.Forms.Label();
            this.lblEQ_ForkNumber_FromSTK = new System.Windows.Forms.Label();
            this.tlpMain_EQPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tlpMain_EQPort
            // 
            this.tlpMain_EQPort.ColumnCount = 4;
            this.tlpMain_EQPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpMain_EQPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpMain_EQPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpMain_EQPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpMain_EQPort.Controls.Add(this.cboEQPort, 1, 1);
            this.tlpMain_EQPort.Controls.Add(this.lblEQCSTID, 1, 2);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_L_REQ, 1, 3);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_U_REQ, 1, 4);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Ready, 1, 5);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Carrier, 1, 6);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_POnLine, 1, 9);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_PEStop, 1, 10);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Transferring_FromSTK, 2, 3);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_TR_REQ_FromSTK, 2, 4);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_BUSY_FromSTK, 2, 5);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_COMPLETE_FromSTK, 2, 6);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_PError, 1, 7);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Spare, 1, 8);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Crane1FromSTK, 2, 7);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_Crane2FromSTK, 2, 8);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_AError_FromSTK, 2, 9);
            this.tlpMain_EQPort.Controls.Add(this.lblEQ_ForkNumber_FromSTK, 2, 10);
            this.tlpMain_EQPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain_EQPort.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tlpMain_EQPort.Location = new System.Drawing.Point(0, 0);
            this.tlpMain_EQPort.Name = "tlpMain_EQPort";
            this.tlpMain_EQPort.RowCount = 12;
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tlpMain_EQPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpMain_EQPort.Size = new System.Drawing.Size(584, 441);
            this.tlpMain_EQPort.TabIndex = 219;
            // 
            // cboEQPort
            // 
            this.tlpMain_EQPort.SetColumnSpan(this.cboEQPort, 2);
            this.cboEQPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboEQPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEQPort.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboEQPort.FormattingEnabled = true;
            this.cboEQPort.Location = new System.Drawing.Point(102, 64);
            this.cboEQPort.Name = "cboEQPort";
            this.cboEQPort.Size = new System.Drawing.Size(378, 28);
            this.cboEQPort.TabIndex = 218;
            this.cboEQPort.SelectedIndexChanged += new System.EventHandler(this.cboEQPort_SelectedIndexChanged);
            // 
            // lblEQCSTID
            // 
            this.lblEQCSTID.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQCSTID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpMain_EQPort.SetColumnSpan(this.lblEQCSTID, 2);
            this.lblEQCSTID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQCSTID.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQCSTID.ForeColor = System.Drawing.Color.Blue;
            this.lblEQCSTID.Location = new System.Drawing.Point(103, 107);
            this.lblEQCSTID.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.lblEQCSTID.Name = "lblEQCSTID";
            this.lblEQCSTID.Size = new System.Drawing.Size(376, 40);
            this.lblEQCSTID.TabIndex = 219;
            this.lblEQCSTID.Text = "CSTID";
            this.lblEQCSTID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEQ_L_REQ
            // 
            this.lblEQ_L_REQ.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_L_REQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_L_REQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_L_REQ.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_L_REQ.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_L_REQ.Location = new System.Drawing.Point(101, 150);
            this.lblEQ_L_REQ.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_L_REQ.Name = "lblEQ_L_REQ";
            this.lblEQ_L_REQ.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_L_REQ.TabIndex = 167;
            this.lblEQ_L_REQ.Text = "L_REQ";
            this.lblEQ_L_REQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_U_REQ
            // 
            this.lblEQ_U_REQ.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_U_REQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_U_REQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_U_REQ.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_U_REQ.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_U_REQ.Location = new System.Drawing.Point(101, 176);
            this.lblEQ_U_REQ.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_U_REQ.Name = "lblEQ_U_REQ";
            this.lblEQ_U_REQ.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_U_REQ.TabIndex = 167;
            this.lblEQ_U_REQ.Text = "U_REQ";
            this.lblEQ_U_REQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Ready
            // 
            this.lblEQ_Ready.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Ready.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Ready.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Ready.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Ready.Location = new System.Drawing.Point(101, 202);
            this.lblEQ_Ready.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Ready.Name = "lblEQ_Ready";
            this.lblEQ_Ready.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Ready.TabIndex = 220;
            this.lblEQ_Ready.Text = "Ready";
            this.lblEQ_Ready.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Carrier
            // 
            this.lblEQ_Carrier.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Carrier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Carrier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Carrier.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Carrier.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Carrier.Location = new System.Drawing.Point(101, 228);
            this.lblEQ_Carrier.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Carrier.Name = "lblEQ_Carrier";
            this.lblEQ_Carrier.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Carrier.TabIndex = 169;
            this.lblEQ_Carrier.Text = "Carrier";
            this.lblEQ_Carrier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_POnLine
            // 
            this.lblEQ_POnLine.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_POnLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_POnLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_POnLine.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_POnLine.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_POnLine.Location = new System.Drawing.Point(101, 306);
            this.lblEQ_POnLine.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_POnLine.Name = "lblEQ_POnLine";
            this.lblEQ_POnLine.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_POnLine.TabIndex = 195;
            this.lblEQ_POnLine.Text = "P.OnLine";
            this.lblEQ_POnLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_PEStop
            // 
            this.lblEQ_PEStop.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_PEStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_PEStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_PEStop.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_PEStop.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_PEStop.Location = new System.Drawing.Point(101, 332);
            this.lblEQ_PEStop.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_PEStop.Name = "lblEQ_PEStop";
            this.lblEQ_PEStop.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_PEStop.TabIndex = 169;
            this.lblEQ_PEStop.Text = "P.EStop";
            this.lblEQ_PEStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Transferring_FromSTK
            // 
            this.lblEQ_Transferring_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Transferring_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Transferring_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Transferring_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Transferring_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Transferring_FromSTK.Location = new System.Drawing.Point(293, 150);
            this.lblEQ_Transferring_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Transferring_FromSTK.Name = "lblEQ_Transferring_FromSTK";
            this.lblEQ_Transferring_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Transferring_FromSTK.TabIndex = 203;
            this.lblEQ_Transferring_FromSTK.Text = "Transferring From STK";
            this.lblEQ_Transferring_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_TR_REQ_FromSTK
            // 
            this.lblEQ_TR_REQ_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_TR_REQ_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_TR_REQ_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_TR_REQ_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_TR_REQ_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_TR_REQ_FromSTK.Location = new System.Drawing.Point(293, 176);
            this.lblEQ_TR_REQ_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_TR_REQ_FromSTK.Name = "lblEQ_TR_REQ_FromSTK";
            this.lblEQ_TR_REQ_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_TR_REQ_FromSTK.TabIndex = 167;
            this.lblEQ_TR_REQ_FromSTK.Text = "TR_REQ From STK";
            this.lblEQ_TR_REQ_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_BUSY_FromSTK
            // 
            this.lblEQ_BUSY_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_BUSY_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_BUSY_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_BUSY_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_BUSY_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_BUSY_FromSTK.Location = new System.Drawing.Point(293, 202);
            this.lblEQ_BUSY_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_BUSY_FromSTK.Name = "lblEQ_BUSY_FromSTK";
            this.lblEQ_BUSY_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_BUSY_FromSTK.TabIndex = 198;
            this.lblEQ_BUSY_FromSTK.Text = "BUSY From STK";
            this.lblEQ_BUSY_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_COMPLETE_FromSTK
            // 
            this.lblEQ_COMPLETE_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_COMPLETE_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_COMPLETE_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_COMPLETE_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_COMPLETE_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_COMPLETE_FromSTK.Location = new System.Drawing.Point(293, 228);
            this.lblEQ_COMPLETE_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_COMPLETE_FromSTK.Name = "lblEQ_COMPLETE_FromSTK";
            this.lblEQ_COMPLETE_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_COMPLETE_FromSTK.TabIndex = 199;
            this.lblEQ_COMPLETE_FromSTK.Text = "COMPLETE From STK";
            this.lblEQ_COMPLETE_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_PError
            // 
            this.lblEQ_PError.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_PError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_PError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_PError.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_PError.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_PError.Location = new System.Drawing.Point(101, 254);
            this.lblEQ_PError.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_PError.Name = "lblEQ_PError";
            this.lblEQ_PError.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_PError.TabIndex = 167;
            this.lblEQ_PError.Text = "P.Error";
            this.lblEQ_PError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Spare
            // 
            this.lblEQ_Spare.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Spare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Spare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Spare.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Spare.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Spare.Location = new System.Drawing.Point(101, 280);
            this.lblEQ_Spare.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Spare.Name = "lblEQ_Spare";
            this.lblEQ_Spare.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Spare.TabIndex = 167;
            this.lblEQ_Spare.Text = "Spare";
            this.lblEQ_Spare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Crane1FromSTK
            // 
            this.lblEQ_Crane1FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Crane1FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Crane1FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Crane1FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Crane1FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Crane1FromSTK.Location = new System.Drawing.Point(293, 254);
            this.lblEQ_Crane1FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Crane1FromSTK.Name = "lblEQ_Crane1FromSTK";
            this.lblEQ_Crane1FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Crane1FromSTK.TabIndex = 199;
            this.lblEQ_Crane1FromSTK.Text = "Crane1 From STK";
            this.lblEQ_Crane1FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_Crane2FromSTK
            // 
            this.lblEQ_Crane2FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_Crane2FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_Crane2FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_Crane2FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_Crane2FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_Crane2FromSTK.Location = new System.Drawing.Point(293, 280);
            this.lblEQ_Crane2FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_Crane2FromSTK.Name = "lblEQ_Crane2FromSTK";
            this.lblEQ_Crane2FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_Crane2FromSTK.TabIndex = 199;
            this.lblEQ_Crane2FromSTK.Text = "Crane2 From STK";
            this.lblEQ_Crane2FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_AError_FromSTK
            // 
            this.lblEQ_AError_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_AError_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_AError_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_AError_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_AError_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_AError_FromSTK.Location = new System.Drawing.Point(293, 306);
            this.lblEQ_AError_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_AError_FromSTK.Name = "lblEQ_AError_FromSTK";
            this.lblEQ_AError_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_AError_FromSTK.TabIndex = 199;
            this.lblEQ_AError_FromSTK.Text = "A.Error From STK";
            this.lblEQ_AError_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEQ_ForkNumber_FromSTK
            // 
            this.lblEQ_ForkNumber_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblEQ_ForkNumber_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEQ_ForkNumber_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEQ_ForkNumber_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblEQ_ForkNumber_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblEQ_ForkNumber_FromSTK.Location = new System.Drawing.Point(293, 332);
            this.lblEQ_ForkNumber_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblEQ_ForkNumber_FromSTK.Name = "lblEQ_ForkNumber_FromSTK";
            this.lblEQ_ForkNumber_FromSTK.Size = new System.Drawing.Size(188, 24);
            this.lblEQ_ForkNumber_FromSTK.TabIndex = 199;
            this.lblEQ_ForkNumber_FromSTK.Text = "ForkNumber From STK";
            this.lblEQ_ForkNumber_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EQPortView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpMain_EQPort);
            this.Name = "EQPortView";
            this.Text = "EQPortView";
            this.Load += new System.EventHandler(this.EQPortView_Load);
            this.VisibleChanged += new System.EventHandler(this.EQPortView_VisibleChanged);
            this.tlpMain_EQPort.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TableLayoutPanel tlpMain_EQPort;
        private System.Windows.Forms.ComboBox cboEQPort;
        internal System.Windows.Forms.Label lblEQCSTID;
        internal System.Windows.Forms.Label lblEQ_L_REQ;
        internal System.Windows.Forms.Label lblEQ_U_REQ;
        internal System.Windows.Forms.Label lblEQ_Ready;
        internal System.Windows.Forms.Label lblEQ_Carrier;
        internal System.Windows.Forms.Label lblEQ_PError;
        internal System.Windows.Forms.Label lblEQ_Spare;
        internal System.Windows.Forms.Label lblEQ_POnLine;
        internal System.Windows.Forms.Label lblEQ_PEStop;
        internal System.Windows.Forms.Label lblEQ_Transferring_FromSTK;
        internal System.Windows.Forms.Label lblEQ_TR_REQ_FromSTK;
        internal System.Windows.Forms.Label lblEQ_BUSY_FromSTK;
        internal System.Windows.Forms.Label lblEQ_COMPLETE_FromSTK;
        internal System.Windows.Forms.Label lblEQ_Crane1FromSTK;
        internal System.Windows.Forms.Label lblEQ_Crane2FromSTK;
        internal System.Windows.Forms.Label lblEQ_AError_FromSTK;
        internal System.Windows.Forms.Label lblEQ_ForkNumber_FromSTK;
    }
}