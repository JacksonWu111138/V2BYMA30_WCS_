namespace Mirle.STKC.R46YP320.Views
{
    partial class IOPortInterfaceView
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
            this.tlpIO_InterFace = new System.Windows.Forms.TableLayoutPanel();
            this.lblIF_BUSY_FromSTK = new System.Windows.Forms.Label();
            this.lblIF_COMPLETE_FromSTK = new System.Windows.Forms.Label();
            this.lblIF_Transferring_FromSTK = new System.Windows.Forms.Label();
            this.lblIF_TR_REQ_FromSTK = new System.Windows.Forms.Label();
            this.lblIF_Ready = new System.Windows.Forms.Label();
            this.lblIF_CARRIER = new System.Windows.Forms.Label();
            this.lblIF_U_REQ = new System.Windows.Forms.Label();
            this.lblIF_Spare = new System.Windows.Forms.Label();
            this.lblIF_PError = new System.Windows.Forms.Label();
            this.lblIF_Online = new System.Windows.Forms.Label();
            this.lblIF_PEStop = new System.Windows.Forms.Label();
            this.lblIF_Crane1FromSTK = new System.Windows.Forms.Label();
            this.lblIF_Crane2FromSTK = new System.Windows.Forms.Label();
            this.lblIF_L_REQ = new System.Windows.Forms.Label();
            this.lblIF_AStopFromSTK = new System.Windows.Forms.Label();
            this.lblIF_ForkNumberFromSTK = new System.Windows.Forms.Label();
            this.tlpIO_InterFace.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tlpIO_InterFace
            // 
            this.tlpIO_InterFace.ColumnCount = 4;
            this.tlpIO_InterFace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpIO_InterFace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpIO_InterFace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpIO_InterFace.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tlpIO_InterFace.Controls.Add(this.lblIF_BUSY_FromSTK, 2, 4);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_COMPLETE_FromSTK, 2, 5);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Transferring_FromSTK, 2, 2);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_TR_REQ_FromSTK, 2, 3);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Ready, 1, 4);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_CARRIER, 1, 5);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_U_REQ, 1, 3);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Spare, 1, 7);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_PError, 1, 6);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Online, 1, 8);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_PEStop, 1, 9);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_L_REQ, 1, 2);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Crane1FromSTK, 2, 6);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_Crane2FromSTK, 2, 7);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_AStopFromSTK, 2, 8);
            this.tlpIO_InterFace.Controls.Add(this.lblIF_ForkNumberFromSTK, 2, 9);
            this.tlpIO_InterFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIO_InterFace.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tlpIO_InterFace.Location = new System.Drawing.Point(0, 0);
            this.tlpIO_InterFace.Name = "tlpIO_InterFace";
            this.tlpIO_InterFace.RowCount = 12;
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.41667F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.125F));
            this.tlpIO_InterFace.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpIO_InterFace.Size = new System.Drawing.Size(584, 441);
            this.tlpIO_InterFace.TabIndex = 220;
            // 
            // lblIF_BUSY_FromSTK
            // 
            this.lblIF_BUSY_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_BUSY_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_BUSY_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_BUSY_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_BUSY_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_BUSY_FromSTK.Location = new System.Drawing.Point(293, 143);
            this.lblIF_BUSY_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_BUSY_FromSTK.Name = "lblIF_BUSY_FromSTK";
            this.lblIF_BUSY_FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_BUSY_FromSTK.TabIndex = 167;
            this.lblIF_BUSY_FromSTK.Text = "Busy From STK";
            this.lblIF_BUSY_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_COMPLETE_FromSTK
            // 
            this.lblIF_COMPLETE_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_COMPLETE_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_COMPLETE_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_COMPLETE_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_COMPLETE_FromSTK.Location = new System.Drawing.Point(293, 186);
            this.lblIF_COMPLETE_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_COMPLETE_FromSTK.Name = "lblIF_COMPLETE_FromSTK";
            this.lblIF_COMPLETE_FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_COMPLETE_FromSTK.TabIndex = 198;
            this.lblIF_COMPLETE_FromSTK.Text = "Complete From STK";
            this.lblIF_COMPLETE_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Transferring_FromSTK
            // 
            this.lblIF_Transferring_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Transferring_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Transferring_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_Transferring_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Transferring_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Transferring_FromSTK.Location = new System.Drawing.Point(293, 57);
            this.lblIF_Transferring_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Transferring_FromSTK.Name = "lblIF_Transferring_FromSTK";
            this.lblIF_Transferring_FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Transferring_FromSTK.TabIndex = 199;
            this.lblIF_Transferring_FromSTK.Text = "Transferring From STK";
            this.lblIF_Transferring_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_TR_REQ_FromSTK
            // 
            this.lblIF_TR_REQ_FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_TR_REQ_FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_TR_REQ_FromSTK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_TR_REQ_FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_TR_REQ_FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_TR_REQ_FromSTK.Location = new System.Drawing.Point(293, 100);
            this.lblIF_TR_REQ_FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_TR_REQ_FromSTK.Name = "lblIF_TR_REQ_FromSTK";
            this.lblIF_TR_REQ_FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_TR_REQ_FromSTK.TabIndex = 203;
            this.lblIF_TR_REQ_FromSTK.Text = "TR_REQ From STK";
            this.lblIF_TR_REQ_FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Ready
            // 
            this.lblIF_Ready.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Ready.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Ready.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_Ready.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Ready.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Ready.Location = new System.Drawing.Point(101, 143);
            this.lblIF_Ready.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Ready.Name = "lblIF_Ready";
            this.lblIF_Ready.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Ready.TabIndex = 169;
            this.lblIF_Ready.Text = "Ready";
            this.lblIF_Ready.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_CARRIER
            // 
            this.lblIF_CARRIER.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_CARRIER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_CARRIER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_CARRIER.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_CARRIER.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_CARRIER.Location = new System.Drawing.Point(101, 186);
            this.lblIF_CARRIER.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_CARRIER.Name = "lblIF_CARRIER";
            this.lblIF_CARRIER.Size = new System.Drawing.Size(188, 41);
            this.lblIF_CARRIER.TabIndex = 195;
            this.lblIF_CARRIER.Text = "CARRIER";
            this.lblIF_CARRIER.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_U_REQ
            // 
            this.lblIF_U_REQ.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_U_REQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_U_REQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_U_REQ.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_U_REQ.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_U_REQ.Location = new System.Drawing.Point(101, 100);
            this.lblIF_U_REQ.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_U_REQ.Name = "lblIF_U_REQ";
            this.lblIF_U_REQ.Size = new System.Drawing.Size(188, 41);
            this.lblIF_U_REQ.TabIndex = 167;
            this.lblIF_U_REQ.Text = "U_REQ";
            this.lblIF_U_REQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Spare
            // 
            this.lblIF_Spare.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Spare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Spare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_Spare.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Spare.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Spare.Location = new System.Drawing.Point(101, 272);
            this.lblIF_Spare.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Spare.Name = "lblIF_Spare";
            this.lblIF_Spare.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Spare.TabIndex = 169;
            this.lblIF_Spare.Text = "Spare";
            this.lblIF_Spare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_PError
            // 
            this.lblIF_PError.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_PError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_PError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_PError.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_PError.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_PError.Location = new System.Drawing.Point(101, 229);
            this.lblIF_PError.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_PError.Name = "lblIF_PError";
            this.lblIF_PError.Size = new System.Drawing.Size(188, 41);
            this.lblIF_PError.TabIndex = 169;
            this.lblIF_PError.Text = "P.Error";
            this.lblIF_PError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Online
            // 
            this.lblIF_Online.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Online.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Online.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_Online.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Online.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Online.Location = new System.Drawing.Point(101, 315);
            this.lblIF_Online.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Online.Name = "lblIF_Online";
            this.lblIF_Online.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Online.TabIndex = 169;
            this.lblIF_Online.Text = "P.Online";
            this.lblIF_Online.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_EStop
            // 
            this.lblIF_PEStop.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_PEStop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_PEStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_PEStop.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_PEStop.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_PEStop.Location = new System.Drawing.Point(101, 358);
            this.lblIF_PEStop.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_PEStop.Name = "lblIF_PEStop";
            this.lblIF_PEStop.Size = new System.Drawing.Size(188, 41);
            this.lblIF_PEStop.TabIndex = 169;
            this.lblIF_PEStop.Text = "P.E-Stop";
            this.lblIF_PEStop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Crane1FromSTK
            // 
            this.lblIF_Crane1FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Crane1FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Crane1FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Crane1FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Crane1FromSTK.Location = new System.Drawing.Point(293, 229);
            this.lblIF_Crane1FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Crane1FromSTK.Name = "lblIF_Crane1FromSTK";
            this.lblIF_Crane1FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Crane1FromSTK.TabIndex = 208;
            this.lblIF_Crane1FromSTK.Text = "Crane1 From STK";
            this.lblIF_Crane1FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_Crane2FromSTK
            // 
            this.lblIF_Crane2FromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_Crane2FromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_Crane2FromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_Crane2FromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_Crane2FromSTK.Location = new System.Drawing.Point(293, 272);
            this.lblIF_Crane2FromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_Crane2FromSTK.Name = "lblIF_Crane2FromSTK";
            this.lblIF_Crane2FromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_Crane2FromSTK.TabIndex = 208;
            this.lblIF_Crane2FromSTK.Text = "Crane2 From STK";
            this.lblIF_Crane2FromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_L_REQ
            // 
            this.lblIF_L_REQ.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_L_REQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_L_REQ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIF_L_REQ.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_L_REQ.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_L_REQ.Location = new System.Drawing.Point(101, 57);
            this.lblIF_L_REQ.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_L_REQ.Name = "lblIF_L_REQ";
            this.lblIF_L_REQ.Size = new System.Drawing.Size(188, 41);
            this.lblIF_L_REQ.TabIndex = 167;
            this.lblIF_L_REQ.Text = "L_REQ";
            this.lblIF_L_REQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_AStopFromSTK
            // 
            this.lblIF_AStopFromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_AStopFromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_AStopFromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_AStopFromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_AStopFromSTK.Location = new System.Drawing.Point(293, 315);
            this.lblIF_AStopFromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_AStopFromSTK.Name = "lblIF_AStopFromSTK";
            this.lblIF_AStopFromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_AStopFromSTK.TabIndex = 219;
            this.lblIF_AStopFromSTK.Text = "A.Error From STK";
            this.lblIF_AStopFromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIF_ForkNumberFromSTK
            // 
            this.lblIF_ForkNumberFromSTK.BackColor = System.Drawing.SystemColors.Control;
            this.lblIF_ForkNumberFromSTK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIF_ForkNumberFromSTK.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblIF_ForkNumberFromSTK.ForeColor = System.Drawing.Color.Blue;
            this.lblIF_ForkNumberFromSTK.Location = new System.Drawing.Point(293, 358);
            this.lblIF_ForkNumberFromSTK.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblIF_ForkNumberFromSTK.Name = "lblIF_ForkNumberFromSTK";
            this.lblIF_ForkNumberFromSTK.Size = new System.Drawing.Size(188, 41);
            this.lblIF_ForkNumberFromSTK.TabIndex = 198;
            this.lblIF_ForkNumberFromSTK.Text = "ForkNumber From STK";
            this.lblIF_ForkNumberFromSTK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IOPortInterfaceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpIO_InterFace);
            this.Name = "IOPortInterfaceView";
            this.Text = "IOPortInterfaceView";
            this.Load += new System.EventHandler(this.IOPortInterfaceView_Load);
            this.VisibleChanged += new System.EventHandler(this.IOPortInterfaceView_VisibleChanged);
            this.tlpIO_InterFace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TableLayoutPanel tlpIO_InterFace;
        internal System.Windows.Forms.Label lblIF_BUSY_FromSTK;
        internal System.Windows.Forms.Label lblIF_COMPLETE_FromSTK;
        internal System.Windows.Forms.Label lblIF_Transferring_FromSTK;
        internal System.Windows.Forms.Label lblIF_TR_REQ_FromSTK;
        internal System.Windows.Forms.Label lblIF_Ready;
        internal System.Windows.Forms.Label lblIF_CARRIER;
        internal System.Windows.Forms.Label lblIF_L_REQ;
        internal System.Windows.Forms.Label lblIF_U_REQ;
        internal System.Windows.Forms.Label lblIF_Spare;
        internal System.Windows.Forms.Label lblIF_PError;
        internal System.Windows.Forms.Label lblIF_Online;
        internal System.Windows.Forms.Label lblIF_Crane1FromSTK;
        internal System.Windows.Forms.Label lblIF_Crane2FromSTK;
        internal System.Windows.Forms.Label lblIF_PEStop;
        internal System.Windows.Forms.Label lblIF_AStopFromSTK;
        internal System.Windows.Forms.Label lblIF_ForkNumberFromSTK;
    }
}
