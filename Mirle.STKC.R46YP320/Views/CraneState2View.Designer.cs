namespace Mirle.STKC.R46YP320.Views
{
    partial class CraneState2View
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
            this.tlpRM1_State2 = new System.Windows.Forms.TableLayoutPanel();
            this.label90 = new System.Windows.Forms.Label();
            this.lblRM1_ErrorCode = new System.Windows.Forms.Label();
            this.lblRM1_ErrorIndex1_PC = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.lblRM1_ErrorIndex1_PLC = new System.Windows.Forms.Label();
            this.lblRM1_RotatingCounter = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.lblRM1_SRI_RM1HIDPowerOn = new System.Windows.Forms.Label();
            this.lblRM1_SRI_EMO = new System.Windows.Forms.Label();
            this.lblRM1_SRI_NoError = new System.Windows.Forms.Label();
            this.lblRM1_SRI_MainCircuitOnEnable = new System.Windows.Forms.Label();
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.lblRM1_MileageOfTravel = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.lblRM1_MileageOfLifter = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label170 = new System.Windows.Forms.Label();
            this.lblRM1_TravelAxisSpeed = new System.Windows.Forms.Label();
            this.lblRM1_LifterAxisSpeed = new System.Windows.Forms.Label();
            this.lblRM1_RotateAxisSpeed = new System.Windows.Forms.Label();
            this.lblRM1_ForkAxisSpeed = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.lblRM1_ForkCounter_LF = new System.Windows.Forms.Label();
            this.lblRM1_ForkCounter_RF = new System.Windows.Forms.Label();
            this.lblRM1_WrongCommandReasonCode = new System.Windows.Forms.Label();
            this.lblRM1_PLCCPUBatteryLow = new System.Windows.Forms.Label();
            this.lblRM1_AnyOneFFUisErr = new System.Windows.Forms.Label();
            this.lblRM1_DriverBatteryLow = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.tlpRM1_State2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tlpRM1_State2
            // 
            this.tlpRM1_State2.ColumnCount = 4;
            this.tlpRM1_State2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tlpRM1_State2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tlpRM1_State2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tlpRM1_State2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tlpRM1_State2.Controls.Add(this.label90, 2, 0);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_ErrorCode, 3, 0);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_ErrorIndex1_PC, 1, 1);
            this.tlpRM1_State2.Controls.Add(this.label102, 0, 0);
            this.tlpRM1_State2.Controls.Add(this.label109, 0, 2);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_ErrorIndex1_PLC, 1, 0);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_RotatingCounter, 1, 2);
            this.tlpRM1_State2.Controls.Add(this.label110, 2, 3);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_SRI_RM1HIDPowerOn, 2, 12);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_SRI_EMO, 2, 11);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_SRI_NoError, 3, 11);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_SRI_MainCircuitOnEnable, 0, 12);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP, 0, 11);
            this.tlpRM1_State2.Controls.Add(this.label107, 0, 4);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_MileageOfTravel, 1, 4);
            this.tlpRM1_State2.Controls.Add(this.label108, 2, 4);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_MileageOfLifter, 3, 4);
            this.tlpRM1_State2.Controls.Add(this.tableLayoutPanel2, 0, 6);
            this.tlpRM1_State2.Controls.Add(this.label103, 2, 2);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_ForkCounter_LF, 3, 2);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_ForkCounter_RF, 3, 3);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_WrongCommandReasonCode, 1, 5);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_PLCCPUBatteryLow, 0, 8);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_AnyOneFFUisErr, 2, 9);
            this.tlpRM1_State2.Controls.Add(this.lblRM1_DriverBatteryLow, 2, 8);
            this.tlpRM1_State2.Controls.Add(this.label172, 0, 5);
            this.tlpRM1_State2.Controls.Add(this.label104, 0, 1);
            this.tlpRM1_State2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRM1_State2.Location = new System.Drawing.Point(0, 0);
            this.tlpRM1_State2.Name = "tlpRM1_State2";
            this.tlpRM1_State2.RowCount = 15;
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRM1_State2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpRM1_State2.Size = new System.Drawing.Size(584, 441);
            this.tlpRM1_State2.TabIndex = 19;
            // 
            // label90
            // 
            this.label90.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label90.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.ForeColor = System.Drawing.Color.Blue;
            this.label90.Location = new System.Drawing.Point(293, 1);
            this.label90.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(153, 27);
            this.label90.TabIndex = 67;
            this.label90.Text = "Error Code";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_ErrorCode
            // 
            this.lblRM1_ErrorCode.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ErrorCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ErrorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ErrorCode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ErrorCode.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_ErrorCode.Location = new System.Drawing.Point(450, 1);
            this.lblRM1_ErrorCode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_ErrorCode.Name = "lblRM1_ErrorCode";
            this.lblRM1_ErrorCode.Size = new System.Drawing.Size(132, 27);
            this.lblRM1_ErrorCode.TabIndex = 67;
            this.lblRM1_ErrorCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_ErrorIndex1_PC
            // 
            this.lblRM1_ErrorIndex1_PC.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ErrorIndex1_PC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ErrorIndex1_PC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ErrorIndex1_PC.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ErrorIndex1_PC.ForeColor = System.Drawing.Color.Red;
            this.lblRM1_ErrorIndex1_PC.Location = new System.Drawing.Point(160, 29);
            this.lblRM1_ErrorIndex1_PC.Name = "lblRM1_ErrorIndex1_PC";
            this.lblRM1_ErrorIndex1_PC.Size = new System.Drawing.Size(128, 29);
            this.lblRM1_ErrorIndex1_PC.TabIndex = 67;
            this.lblRM1_ErrorIndex1_PC.Text = "0";
            this.lblRM1_ErrorIndex1_PC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label102
            // 
            this.label102.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label102.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.ForeColor = System.Drawing.Color.Blue;
            this.label102.Location = new System.Drawing.Point(2, 1);
            this.label102.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(153, 27);
            this.label102.TabIndex = 67;
            this.label102.Text = "MPLC Error Index";
            this.label102.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label109
            // 
            this.label109.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label109.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label109.ForeColor = System.Drawing.Color.Blue;
            this.label109.Location = new System.Drawing.Point(2, 59);
            this.label109.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(153, 27);
            this.label109.TabIndex = 67;
            this.label109.Text = "Rotating Counter";
            this.label109.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_ErrorIndex1_PLC
            // 
            this.lblRM1_ErrorIndex1_PLC.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ErrorIndex1_PLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ErrorIndex1_PLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ErrorIndex1_PLC.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ErrorIndex1_PLC.ForeColor = System.Drawing.Color.Red;
            this.lblRM1_ErrorIndex1_PLC.Location = new System.Drawing.Point(160, 0);
            this.lblRM1_ErrorIndex1_PLC.Name = "lblRM1_ErrorIndex1_PLC";
            this.lblRM1_ErrorIndex1_PLC.Size = new System.Drawing.Size(128, 29);
            this.lblRM1_ErrorIndex1_PLC.TabIndex = 67;
            this.lblRM1_ErrorIndex1_PLC.Text = "0";
            this.lblRM1_ErrorIndex1_PLC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_RotatingCounter
            // 
            this.lblRM1_RotatingCounter.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_RotatingCounter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_RotatingCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_RotatingCounter.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_RotatingCounter.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_RotatingCounter.Location = new System.Drawing.Point(159, 59);
            this.lblRM1_RotatingCounter.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_RotatingCounter.Name = "lblRM1_RotatingCounter";
            this.lblRM1_RotatingCounter.Size = new System.Drawing.Size(130, 27);
            this.lblRM1_RotatingCounter.TabIndex = 67;
            this.lblRM1_RotatingCounter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label110
            // 
            this.label110.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label110.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label110.ForeColor = System.Drawing.Color.Blue;
            this.label110.Location = new System.Drawing.Point(293, 88);
            this.label110.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(153, 27);
            this.label110.TabIndex = 67;
            this.label110.Text = "Fork Counter_RF";
            this.label110.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_SRI_RM1HIDPowerOn
            // 
            this.lblRM1_SRI_RM1HIDPowerOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_SRI_RM1HIDPowerOn, 2);
            this.lblRM1_SRI_RM1HIDPowerOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_SRI_RM1HIDPowerOn.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_SRI_RM1HIDPowerOn.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_SRI_RM1HIDPowerOn.Location = new System.Drawing.Point(293, 349);
            this.lblRM1_SRI_RM1HIDPowerOn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_SRI_RM1HIDPowerOn.Name = "lblRM1_SRI_RM1HIDPowerOn";
            this.lblRM1_SRI_RM1HIDPowerOn.Size = new System.Drawing.Size(289, 27);
            this.lblRM1_SRI_RM1HIDPowerOn.TabIndex = 78;
            this.lblRM1_SRI_RM1HIDPowerOn.Text = "SRI:HID Power On";
            this.lblRM1_SRI_RM1HIDPowerOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_SRI_EMO
            // 
            this.lblRM1_SRI_EMO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRM1_SRI_EMO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_SRI_EMO.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_SRI_EMO.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_SRI_EMO.Location = new System.Drawing.Point(293, 320);
            this.lblRM1_SRI_EMO.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_SRI_EMO.Name = "lblRM1_SRI_EMO";
            this.lblRM1_SRI_EMO.Size = new System.Drawing.Size(153, 27);
            this.lblRM1_SRI_EMO.TabIndex = 81;
            this.lblRM1_SRI_EMO.Text = "SRI:EMO";
            this.lblRM1_SRI_EMO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_SRI_NoError
            // 
            this.lblRM1_SRI_NoError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRM1_SRI_NoError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_SRI_NoError.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_SRI_NoError.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_SRI_NoError.Location = new System.Drawing.Point(450, 320);
            this.lblRM1_SRI_NoError.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_SRI_NoError.Name = "lblRM1_SRI_NoError";
            this.lblRM1_SRI_NoError.Size = new System.Drawing.Size(132, 27);
            this.lblRM1_SRI_NoError.TabIndex = 74;
            this.lblRM1_SRI_NoError.Text = "SRI:No Error";
            this.lblRM1_SRI_NoError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_SRI_MainCircuitOnEnable
            // 
            this.lblRM1_SRI_MainCircuitOnEnable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_SRI_MainCircuitOnEnable, 2);
            this.lblRM1_SRI_MainCircuitOnEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_SRI_MainCircuitOnEnable.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_SRI_MainCircuitOnEnable.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_SRI_MainCircuitOnEnable.Location = new System.Drawing.Point(2, 349);
            this.lblRM1_SRI_MainCircuitOnEnable.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_SRI_MainCircuitOnEnable.Name = "lblRM1_SRI_MainCircuitOnEnable";
            this.lblRM1_SRI_MainCircuitOnEnable.Size = new System.Drawing.Size(287, 27);
            this.lblRM1_SRI_MainCircuitOnEnable.TabIndex = 79;
            this.lblRM1_SRI_MainCircuitOnEnable.Text = "SRI:Main Circuit On Enable";
            this.lblRM1_SRI_MainCircuitOnEnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_SRI_AMSwitchofRMPLCisAutoHP
            // 
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP, 2);
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Location = new System.Drawing.Point(2, 320);
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Name = "lblRM1_SRI_AMSwitchofRMPLCisAutoHP";
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Size = new System.Drawing.Size(287, 27);
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.TabIndex = 76;
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.Text = "SRI:Auto/Manual Key of RM-PLC is Auto";
            this.lblRM1_SRI_AMSwitchofRMPLCisAutoHP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label107
            // 
            this.label107.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label107.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.ForeColor = System.Drawing.Color.Blue;
            this.label107.Location = new System.Drawing.Point(2, 117);
            this.label107.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(153, 27);
            this.label107.TabIndex = 67;
            this.label107.Text = "Mileage of travel";
            this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_MileageOfTravel
            // 
            this.lblRM1_MileageOfTravel.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_MileageOfTravel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_MileageOfTravel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_MileageOfTravel.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_MileageOfTravel.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_MileageOfTravel.Location = new System.Drawing.Point(159, 117);
            this.lblRM1_MileageOfTravel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_MileageOfTravel.Name = "lblRM1_MileageOfTravel";
            this.lblRM1_MileageOfTravel.Size = new System.Drawing.Size(130, 27);
            this.lblRM1_MileageOfTravel.TabIndex = 67;
            this.lblRM1_MileageOfTravel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label108
            // 
            this.label108.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label108.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label108.ForeColor = System.Drawing.Color.Blue;
            this.label108.Location = new System.Drawing.Point(293, 117);
            this.label108.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(153, 27);
            this.label108.TabIndex = 67;
            this.label108.Text = "Miileage of lifter";
            this.label108.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_MileageOfLifter
            // 
            this.lblRM1_MileageOfLifter.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_MileageOfLifter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_MileageOfLifter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_MileageOfLifter.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_MileageOfLifter.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_MileageOfLifter.Location = new System.Drawing.Point(450, 117);
            this.lblRM1_MileageOfLifter.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_MileageOfLifter.Name = "lblRM1_MileageOfLifter";
            this.lblRM1_MileageOfLifter.Size = new System.Drawing.Size(132, 27);
            this.lblRM1_MileageOfLifter.TabIndex = 67;
            this.lblRM1_MileageOfLifter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tlpRM1_State2.SetColumnSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label170, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRM1_TravelAxisSpeed, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRM1_LifterAxisSpeed, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRM1_RotateAxisSpeed, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRM1_ForkAxisSpeed, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 174);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(584, 29);
            this.tableLayoutPanel2.TabIndex = 84;
            // 
            // label170
            // 
            this.label170.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label170.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label170.ForeColor = System.Drawing.Color.Blue;
            this.label170.Location = new System.Drawing.Point(2, 1);
            this.label170.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(112, 27);
            this.label170.TabIndex = 67;
            this.label170.Text = "AxisSpeed > ";
            this.label170.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_TravelAxisSpeed
            // 
            this.lblRM1_TravelAxisSpeed.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_TravelAxisSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_TravelAxisSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_TravelAxisSpeed.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_TravelAxisSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_TravelAxisSpeed.Location = new System.Drawing.Point(118, 1);
            this.lblRM1_TravelAxisSpeed.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_TravelAxisSpeed.Name = "lblRM1_TravelAxisSpeed";
            this.lblRM1_TravelAxisSpeed.Size = new System.Drawing.Size(112, 27);
            this.lblRM1_TravelAxisSpeed.TabIndex = 67;
            this.lblRM1_TravelAxisSpeed.Text = "0";
            this.lblRM1_TravelAxisSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRM1_LifterAxisSpeed
            // 
            this.lblRM1_LifterAxisSpeed.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_LifterAxisSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_LifterAxisSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_LifterAxisSpeed.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_LifterAxisSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_LifterAxisSpeed.Location = new System.Drawing.Point(234, 1);
            this.lblRM1_LifterAxisSpeed.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_LifterAxisSpeed.Name = "lblRM1_LifterAxisSpeed";
            this.lblRM1_LifterAxisSpeed.Size = new System.Drawing.Size(112, 27);
            this.lblRM1_LifterAxisSpeed.TabIndex = 67;
            this.lblRM1_LifterAxisSpeed.Text = "0";
            this.lblRM1_LifterAxisSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRM1_RotateAxisSpeed
            // 
            this.lblRM1_RotateAxisSpeed.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_RotateAxisSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_RotateAxisSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_RotateAxisSpeed.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_RotateAxisSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_RotateAxisSpeed.Location = new System.Drawing.Point(350, 1);
            this.lblRM1_RotateAxisSpeed.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_RotateAxisSpeed.Name = "lblRM1_RotateAxisSpeed";
            this.lblRM1_RotateAxisSpeed.Size = new System.Drawing.Size(112, 27);
            this.lblRM1_RotateAxisSpeed.TabIndex = 67;
            this.lblRM1_RotateAxisSpeed.Text = "0";
            this.lblRM1_RotateAxisSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRM1_ForkAxisSpeed
            // 
            this.lblRM1_ForkAxisSpeed.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ForkAxisSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ForkAxisSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ForkAxisSpeed.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ForkAxisSpeed.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_ForkAxisSpeed.Location = new System.Drawing.Point(466, 1);
            this.lblRM1_ForkAxisSpeed.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_ForkAxisSpeed.Name = "lblRM1_ForkAxisSpeed";
            this.lblRM1_ForkAxisSpeed.Size = new System.Drawing.Size(116, 27);
            this.lblRM1_ForkAxisSpeed.TabIndex = 67;
            this.lblRM1_ForkAxisSpeed.Text = "0";
            this.lblRM1_ForkAxisSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label103
            // 
            this.label103.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label103.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.ForeColor = System.Drawing.Color.Blue;
            this.label103.Location = new System.Drawing.Point(293, 59);
            this.label103.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(153, 27);
            this.label103.TabIndex = 67;
            this.label103.Text = "Fork Counter_LF";
            this.label103.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRM1_ForkCounter_LF
            // 
            this.lblRM1_ForkCounter_LF.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ForkCounter_LF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ForkCounter_LF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ForkCounter_LF.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ForkCounter_LF.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_ForkCounter_LF.Location = new System.Drawing.Point(450, 59);
            this.lblRM1_ForkCounter_LF.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_ForkCounter_LF.Name = "lblRM1_ForkCounter_LF";
            this.lblRM1_ForkCounter_LF.Size = new System.Drawing.Size(132, 27);
            this.lblRM1_ForkCounter_LF.TabIndex = 67;
            this.lblRM1_ForkCounter_LF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_ForkCounter_RF
            // 
            this.lblRM1_ForkCounter_RF.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_ForkCounter_RF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_ForkCounter_RF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_ForkCounter_RF.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_ForkCounter_RF.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_ForkCounter_RF.Location = new System.Drawing.Point(450, 88);
            this.lblRM1_ForkCounter_RF.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_ForkCounter_RF.Name = "lblRM1_ForkCounter_RF";
            this.lblRM1_ForkCounter_RF.Size = new System.Drawing.Size(132, 27);
            this.lblRM1_ForkCounter_RF.TabIndex = 67;
            this.lblRM1_ForkCounter_RF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_WrongCommandReasonCode
            // 
            this.lblRM1_WrongCommandReasonCode.BackColor = System.Drawing.SystemColors.Info;
            this.lblRM1_WrongCommandReasonCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRM1_WrongCommandReasonCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_WrongCommandReasonCode.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_WrongCommandReasonCode.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_WrongCommandReasonCode.Location = new System.Drawing.Point(159, 146);
            this.lblRM1_WrongCommandReasonCode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_WrongCommandReasonCode.Name = "lblRM1_WrongCommandReasonCode";
            this.lblRM1_WrongCommandReasonCode.Size = new System.Drawing.Size(130, 27);
            this.lblRM1_WrongCommandReasonCode.TabIndex = 67;
            this.lblRM1_WrongCommandReasonCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_PLCCPUBatteryLow
            // 
            this.lblRM1_PLCCPUBatteryLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_PLCCPUBatteryLow, 2);
            this.lblRM1_PLCCPUBatteryLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_PLCCPUBatteryLow.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_PLCCPUBatteryLow.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_PLCCPUBatteryLow.Location = new System.Drawing.Point(2, 233);
            this.lblRM1_PLCCPUBatteryLow.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_PLCCPUBatteryLow.Name = "lblRM1_PLCCPUBatteryLow";
            this.lblRM1_PLCCPUBatteryLow.Size = new System.Drawing.Size(287, 27);
            this.lblRM1_PLCCPUBatteryLow.TabIndex = 68;
            this.lblRM1_PLCCPUBatteryLow.Text = "The PLC CPU battery low";
            this.lblRM1_PLCCPUBatteryLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_AnyOneFFUisErr
            // 
            this.lblRM1_AnyOneFFUisErr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_AnyOneFFUisErr, 2);
            this.lblRM1_AnyOneFFUisErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_AnyOneFFUisErr.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_AnyOneFFUisErr.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_AnyOneFFUisErr.Location = new System.Drawing.Point(293, 262);
            this.lblRM1_AnyOneFFUisErr.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_AnyOneFFUisErr.Name = "lblRM1_AnyOneFFUisErr";
            this.lblRM1_AnyOneFFUisErr.Size = new System.Drawing.Size(289, 27);
            this.lblRM1_AnyOneFFUisErr.TabIndex = 73;
            this.lblRM1_AnyOneFFUisErr.Text = "Any one FFU is Err";
            this.lblRM1_AnyOneFFUisErr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRM1_DriverBatteryLow
            // 
            this.lblRM1_DriverBatteryLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpRM1_State2.SetColumnSpan(this.lblRM1_DriverBatteryLow, 2);
            this.lblRM1_DriverBatteryLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRM1_DriverBatteryLow.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRM1_DriverBatteryLow.ForeColor = System.Drawing.Color.Blue;
            this.lblRM1_DriverBatteryLow.Location = new System.Drawing.Point(293, 233);
            this.lblRM1_DriverBatteryLow.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblRM1_DriverBatteryLow.Name = "lblRM1_DriverBatteryLow";
            this.lblRM1_DriverBatteryLow.Size = new System.Drawing.Size(289, 27);
            this.lblRM1_DriverBatteryLow.TabIndex = 74;
            this.lblRM1_DriverBatteryLow.Text = "Driver battery low";
            this.lblRM1_DriverBatteryLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label172
            // 
            this.label172.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label172.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label172.ForeColor = System.Drawing.Color.Blue;
            this.label172.Location = new System.Drawing.Point(2, 146);
            this.label172.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(153, 27);
            this.label172.TabIndex = 67;
            this.label172.Text = "Reason Code";
            this.label172.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label104
            // 
            this.label104.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label104.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label104.ForeColor = System.Drawing.Color.Blue;
            this.label104.Location = new System.Drawing.Point(2, 30);
            this.label104.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(153, 27);
            this.label104.TabIndex = 67;
            this.label104.Text = "PC Error Index";
            this.label104.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CraneState2View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpRM1_State2);
            this.Name = "CraneState2View";
            this.Text = "Crane1State2View";
            this.Load += new System.EventHandler(this.CraneState2View_Load);
            this.tlpRM1_State2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TableLayoutPanel tlpRM1_State2;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label lblRM1_ErrorCode;
        private System.Windows.Forms.Label lblRM1_ErrorIndex1_PC;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label lblRM1_ErrorIndex1_PLC;
        private System.Windows.Forms.Label lblRM1_RotatingCounter;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label lblRM1_SRI_RM1HIDPowerOn;
        private System.Windows.Forms.Label lblRM1_SRI_EMO;
        private System.Windows.Forms.Label lblRM1_SRI_NoError;
        private System.Windows.Forms.Label lblRM1_SRI_MainCircuitOnEnable;
        private System.Windows.Forms.Label lblRM1_SRI_AMSwitchofRMPLCisAutoHP;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label lblRM1_MileageOfTravel;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label lblRM1_MileageOfLifter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label170;
        private System.Windows.Forms.Label lblRM1_TravelAxisSpeed;
        private System.Windows.Forms.Label lblRM1_LifterAxisSpeed;
        private System.Windows.Forms.Label lblRM1_RotateAxisSpeed;
        private System.Windows.Forms.Label lblRM1_ForkAxisSpeed;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label lblRM1_ForkCounter_LF;
        private System.Windows.Forms.Label lblRM1_ForkCounter_RF;
        private System.Windows.Forms.Label lblRM1_WrongCommandReasonCode;
        private System.Windows.Forms.Label lblRM1_PLCCPUBatteryLow;
        private System.Windows.Forms.Label lblRM1_AnyOneFFUisErr;
        private System.Windows.Forms.Label lblRM1_DriverBatteryLow;
        private System.Windows.Forms.Label label172;
        private System.Windows.Forms.Label label104;
    }
}