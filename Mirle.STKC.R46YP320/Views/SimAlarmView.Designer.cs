namespace Mirle.STKC.R46YP320.Views
{
    partial class SimAlarmView
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
            this.buttonDoorOpenHP = new System.Windows.Forms.Button();
            this.buttonDoorOpenOP = new System.Windows.Forms.Button();
            this.buttonDoorClosedHP = new System.Windows.Forms.Button();
            this.buttonDoorClosedOP = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCraneID = new System.Windows.Forms.ComboBox();
            this.buttonAlarmClear = new System.Windows.Forms.Button();
            this.comboBoxAlarmCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAlarmSet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAlarmCode = new System.Windows.Forms.TextBox();
            this.textBoxPLCIndex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPCIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.buttonKeySwitchAutoHP = new System.Windows.Forms.Button();
            this.buttonKeySwitchAutoOP = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxIOPortID = new System.Windows.Forms.ComboBox();
            this.buttonAlarmClearIO = new System.Windows.Forms.Button();
            this.comboBoxAlarmCodeIO = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAlarmSetIO = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAlarmCodeIO = new System.Windows.Forms.TextBox();
            this.textBoxPLCIndexIO = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPCIndexIO = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonKeySwitchOffHP = new System.Windows.Forms.Button();
            this.buttonKeySwitchOffOP = new System.Windows.Forms.Button();
            this.buttonAreaSensorSignal1Off = new System.Windows.Forms.Button();
            this.lblGroup = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.buttonAreaSensorSignal1On = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonPLCWriter_WriteBitOff = new System.Windows.Forms.Button();
            this.buttonPLCWriter_WriteBitOn = new System.Windows.Forms.Button();
            this.buttonPLCWriter_WriteWord = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPLCWriter_Bit = new System.Windows.Forms.TextBox();
            this.textBoxPLCWriter_Word_Value = new System.Windows.Forms.TextBox();
            this.textBoxPLCWriter_Word = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDoorOpenHP
            // 
            this.buttonDoorOpenHP.Location = new System.Drawing.Point(12, 11);
            this.buttonDoorOpenHP.Name = "buttonDoorOpenHP";
            this.buttonDoorOpenHP.Size = new System.Drawing.Size(101, 21);
            this.buttonDoorOpenHP.TabIndex = 0;
            this.buttonDoorOpenHP.Text = "DoorOpenHP";
            this.buttonDoorOpenHP.UseVisualStyleBackColor = true;
            this.buttonDoorOpenHP.Click += new System.EventHandler(this.buttonDoorOpenHP_Click);
            // 
            // buttonDoorOpenOP
            // 
            this.buttonDoorOpenOP.Location = new System.Drawing.Point(124, 11);
            this.buttonDoorOpenOP.Name = "buttonDoorOpenOP";
            this.buttonDoorOpenOP.Size = new System.Drawing.Size(101, 21);
            this.buttonDoorOpenOP.TabIndex = 1;
            this.buttonDoorOpenOP.Text = "DoorOpenOP";
            this.buttonDoorOpenOP.UseVisualStyleBackColor = true;
            this.buttonDoorOpenOP.Click += new System.EventHandler(this.buttonDoorOpenOP_Click);
            // 
            // buttonDoorClosedHP
            // 
            this.buttonDoorClosedHP.Location = new System.Drawing.Point(12, 54);
            this.buttonDoorClosedHP.Name = "buttonDoorClosedHP";
            this.buttonDoorClosedHP.Size = new System.Drawing.Size(101, 21);
            this.buttonDoorClosedHP.TabIndex = 2;
            this.buttonDoorClosedHP.Text = "DoorClosedHP";
            this.buttonDoorClosedHP.UseVisualStyleBackColor = true;
            this.buttonDoorClosedHP.Click += new System.EventHandler(this.buttonDoorClosedHP_Click);
            // 
            // buttonDoorClosedOP
            // 
            this.buttonDoorClosedOP.Location = new System.Drawing.Point(124, 54);
            this.buttonDoorClosedOP.Name = "buttonDoorClosedOP";
            this.buttonDoorClosedOP.Size = new System.Drawing.Size(101, 21);
            this.buttonDoorClosedOP.TabIndex = 3;
            this.buttonDoorClosedOP.Text = "DoorClosedOP";
            this.buttonDoorClosedOP.UseVisualStyleBackColor = true;
            this.buttonDoorClosedOP.Click += new System.EventHandler(this.buttonDoorClosedOP_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCraneID);
            this.groupBox1.Controls.Add(this.buttonAlarmClear);
            this.groupBox1.Controls.Add(this.comboBoxAlarmCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonAlarmSet);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxAlarmCode);
            this.groupBox1.Controls.Add(this.textBoxPLCIndex);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPCIndex);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 164);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crane1 Alarm";
            // 
            // comboBoxCraneID
            // 
            this.comboBoxCraneID.FormattingEnabled = true;
            this.comboBoxCraneID.Location = new System.Drawing.Point(12, 27);
            this.comboBoxCraneID.Name = "comboBoxCraneID";
            this.comboBoxCraneID.Size = new System.Drawing.Size(121, 20);
            this.comboBoxCraneID.TabIndex = 10;
            this.comboBoxCraneID.SelectedIndexChanged += new System.EventHandler(this.comboBoxCraneID_SelectedIndexChanged);
            // 
            // buttonAlarmClear
            // 
            this.buttonAlarmClear.Location = new System.Drawing.Point(227, 126);
            this.buttonAlarmClear.Name = "buttonAlarmClear";
            this.buttonAlarmClear.Size = new System.Drawing.Size(75, 21);
            this.buttonAlarmClear.TabIndex = 9;
            this.buttonAlarmClear.Text = "Alarm Clear";
            this.buttonAlarmClear.UseVisualStyleBackColor = true;
            this.buttonAlarmClear.Click += new System.EventHandler(this.buttonAlarmClear_Click);
            // 
            // comboBoxAlarmCode
            // 
            this.comboBoxAlarmCode.FormattingEnabled = true;
            this.comboBoxAlarmCode.Items.AddRange(new object[] {
            "A502",
            "A508"});
            this.comboBoxAlarmCode.Location = new System.Drawing.Point(12, 127);
            this.comboBoxAlarmCode.Name = "comboBoxAlarmCode";
            this.comboBoxAlarmCode.Size = new System.Drawing.Size(121, 20);
            this.comboBoxAlarmCode.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "New Alarm:";
            // 
            // buttonAlarmSet
            // 
            this.buttonAlarmSet.Location = new System.Drawing.Point(146, 126);
            this.buttonAlarmSet.Name = "buttonAlarmSet";
            this.buttonAlarmSet.Size = new System.Drawing.Size(75, 21);
            this.buttonAlarmSet.TabIndex = 6;
            this.buttonAlarmSet.Text = "Alarm Set";
            this.buttonAlarmSet.UseVisualStyleBackColor = true;
            this.buttonAlarmSet.Click += new System.EventHandler(this.buttonAlarmSet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "AlarmCode:";
            // 
            // textBoxAlarmCode
            // 
            this.textBoxAlarmCode.Location = new System.Drawing.Point(76, 76);
            this.textBoxAlarmCode.Name = "textBoxAlarmCode";
            this.textBoxAlarmCode.Size = new System.Drawing.Size(69, 22);
            this.textBoxAlarmCode.TabIndex = 4;
            // 
            // textBoxPLCIndex
            // 
            this.textBoxPLCIndex.Location = new System.Drawing.Point(208, 52);
            this.textBoxPLCIndex.Name = "textBoxPLCIndex";
            this.textBoxPLCIndex.Size = new System.Drawing.Size(69, 22);
            this.textBoxPLCIndex.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "PLC Index:";
            // 
            // textBoxPCIndex
            // 
            this.textBoxPCIndex.Location = new System.Drawing.Point(68, 52);
            this.textBoxPCIndex.Name = "textBoxPCIndex";
            this.textBoxPCIndex.Size = new System.Drawing.Size(69, 22);
            this.textBoxPCIndex.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "PC Index:";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 300;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // buttonKeySwitchAutoHP
            // 
            this.buttonKeySwitchAutoHP.Location = new System.Drawing.Point(271, 11);
            this.buttonKeySwitchAutoHP.Name = "buttonKeySwitchAutoHP";
            this.buttonKeySwitchAutoHP.Size = new System.Drawing.Size(119, 21);
            this.buttonKeySwitchAutoHP.TabIndex = 5;
            this.buttonKeySwitchAutoHP.Text = "KeySwitchAutoHP";
            this.buttonKeySwitchAutoHP.UseVisualStyleBackColor = true;
            this.buttonKeySwitchAutoHP.Click += new System.EventHandler(this.buttonKeySwitchAutoHP_Click);
            // 
            // buttonKeySwitchAutoOP
            // 
            this.buttonKeySwitchAutoOP.Location = new System.Drawing.Point(418, 11);
            this.buttonKeySwitchAutoOP.Name = "buttonKeySwitchAutoOP";
            this.buttonKeySwitchAutoOP.Size = new System.Drawing.Size(119, 21);
            this.buttonKeySwitchAutoOP.TabIndex = 6;
            this.buttonKeySwitchAutoOP.Text = "KeySwitchAutoOP";
            this.buttonKeySwitchAutoOP.UseVisualStyleBackColor = true;
            this.buttonKeySwitchAutoOP.Click += new System.EventHandler(this.buttonKeySwitchAutoOP_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxIOPortID);
            this.groupBox2.Controls.Add(this.buttonAlarmClearIO);
            this.groupBox2.Controls.Add(this.comboBoxAlarmCodeIO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonAlarmSetIO);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxAlarmCodeIO);
            this.groupBox2.Controls.Add(this.textBoxPLCIndexIO);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxPCIndexIO);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(361, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(315, 164);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IO Port1 Alarm";
            // 
            // comboBoxIOPortID
            // 
            this.comboBoxIOPortID.FormattingEnabled = true;
            this.comboBoxIOPortID.Location = new System.Drawing.Point(10, 27);
            this.comboBoxIOPortID.Name = "comboBoxIOPortID";
            this.comboBoxIOPortID.Size = new System.Drawing.Size(121, 20);
            this.comboBoxIOPortID.TabIndex = 10;
            this.comboBoxIOPortID.SelectedIndexChanged += new System.EventHandler(this.comboBoxIOPortID_SelectedIndexChanged);
            // 
            // buttonAlarmClearIO
            // 
            this.buttonAlarmClearIO.Location = new System.Drawing.Point(221, 126);
            this.buttonAlarmClearIO.Name = "buttonAlarmClearIO";
            this.buttonAlarmClearIO.Size = new System.Drawing.Size(75, 21);
            this.buttonAlarmClearIO.TabIndex = 9;
            this.buttonAlarmClearIO.Text = "Alarm Clear";
            this.buttonAlarmClearIO.UseVisualStyleBackColor = true;
            this.buttonAlarmClearIO.Click += new System.EventHandler(this.buttonAlarmClearIO_Click);
            // 
            // comboBoxAlarmCodeIO
            // 
            this.comboBoxAlarmCodeIO.FormattingEnabled = true;
            this.comboBoxAlarmCodeIO.Items.AddRange(new object[] {
            "BE17",
            "BE18",
            "BE20"});
            this.comboBoxAlarmCodeIO.Location = new System.Drawing.Point(6, 127);
            this.comboBoxAlarmCodeIO.Name = "comboBoxAlarmCodeIO";
            this.comboBoxAlarmCodeIO.Size = new System.Drawing.Size(121, 20);
            this.comboBoxAlarmCodeIO.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "New Alarm:";
            // 
            // buttonAlarmSetIO
            // 
            this.buttonAlarmSetIO.Location = new System.Drawing.Point(140, 126);
            this.buttonAlarmSetIO.Name = "buttonAlarmSetIO";
            this.buttonAlarmSetIO.Size = new System.Drawing.Size(75, 21);
            this.buttonAlarmSetIO.TabIndex = 6;
            this.buttonAlarmSetIO.Text = "Alarm Set";
            this.buttonAlarmSetIO.UseVisualStyleBackColor = true;
            this.buttonAlarmSetIO.Click += new System.EventHandler(this.buttonAlarmSetIO_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "AlarmCode:";
            // 
            // textBoxAlarmCodeIO
            // 
            this.textBoxAlarmCodeIO.Location = new System.Drawing.Point(70, 76);
            this.textBoxAlarmCodeIO.Name = "textBoxAlarmCodeIO";
            this.textBoxAlarmCodeIO.Size = new System.Drawing.Size(69, 22);
            this.textBoxAlarmCodeIO.TabIndex = 4;
            // 
            // textBoxPLCIndexIO
            // 
            this.textBoxPLCIndexIO.Location = new System.Drawing.Point(202, 52);
            this.textBoxPLCIndexIO.Name = "textBoxPLCIndexIO";
            this.textBoxPLCIndexIO.Size = new System.Drawing.Size(69, 22);
            this.textBoxPLCIndexIO.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(137, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "PLC Index:";
            // 
            // textBoxPCIndexIO
            // 
            this.textBoxPCIndexIO.Location = new System.Drawing.Point(62, 52);
            this.textBoxPCIndexIO.Name = "textBoxPCIndexIO";
            this.textBoxPCIndexIO.Size = new System.Drawing.Size(69, 22);
            this.textBoxPCIndexIO.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "PC Index:";
            // 
            // buttonKeySwitchOffHP
            // 
            this.buttonKeySwitchOffHP.Location = new System.Drawing.Point(271, 54);
            this.buttonKeySwitchOffHP.Name = "buttonKeySwitchOffHP";
            this.buttonKeySwitchOffHP.Size = new System.Drawing.Size(119, 21);
            this.buttonKeySwitchOffHP.TabIndex = 11;
            this.buttonKeySwitchOffHP.Text = "KeySwitchOffHP";
            this.buttonKeySwitchOffHP.UseVisualStyleBackColor = true;
            this.buttonKeySwitchOffHP.Click += new System.EventHandler(this.buttonKeySwitchOffHP_Click);
            // 
            // buttonKeySwitchOffOP
            // 
            this.buttonKeySwitchOffOP.Location = new System.Drawing.Point(418, 54);
            this.buttonKeySwitchOffOP.Name = "buttonKeySwitchOffOP";
            this.buttonKeySwitchOffOP.Size = new System.Drawing.Size(119, 21);
            this.buttonKeySwitchOffOP.TabIndex = 12;
            this.buttonKeySwitchOffOP.Text = "KeySwitchOffOP";
            this.buttonKeySwitchOffOP.UseVisualStyleBackColor = true;
            this.buttonKeySwitchOffOP.Click += new System.EventHandler(this.buttonKeySwitchOffOP_Click);
            // 
            // buttonAreaSensorSignal1Off
            // 
            this.buttonAreaSensorSignal1Off.Location = new System.Drawing.Point(227, 17);
            this.buttonAreaSensorSignal1Off.Name = "buttonAreaSensorSignal1Off";
            this.buttonAreaSensorSignal1Off.Size = new System.Drawing.Size(85, 21);
            this.buttonAreaSensorSignal1Off.TabIndex = 14;
            this.buttonAreaSensorSignal1Off.Text = "AreaSensorOff";
            this.buttonAreaSensorSignal1Off.UseVisualStyleBackColor = true;
            this.buttonAreaSensorSignal1Off.Click += new System.EventHandler(this.buttonAreaSensorSignal1Off_Click);
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(10, 21);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(65, 12);
            this.lblGroup.TabIndex = 0;
            this.lblGroup.Text = "WordValue :";
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(76, 16);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(69, 22);
            this.txtGroup.TabIndex = 1;
            // 
            // buttonAreaSensorSignal1On
            // 
            this.buttonAreaSensorSignal1On.Location = new System.Drawing.Point(146, 17);
            this.buttonAreaSensorSignal1On.Name = "buttonAreaSensorSignal1On";
            this.buttonAreaSensorSignal1On.Size = new System.Drawing.Size(80, 21);
            this.buttonAreaSensorSignal1On.TabIndex = 13;
            this.buttonAreaSensorSignal1On.Text = "AreaSensorOn";
            this.buttonAreaSensorSignal1On.UseVisualStyleBackColor = true;
            this.buttonAreaSensorSignal1On.Click += new System.EventHandler(this.buttonAreaSensorSignal1On_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAreaSensorSignal1On);
            this.groupBox3.Controls.Add(this.buttonAreaSensorSignal1Off);
            this.groupBox3.Controls.Add(this.txtGroup);
            this.groupBox3.Controls.Add(this.lblGroup);
            this.groupBox3.Location = new System.Drawing.Point(26, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(315, 46);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "AreaSensor1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonPLCWriter_WriteBitOff);
            this.groupBox4.Controls.Add(this.buttonPLCWriter_WriteBitOn);
            this.groupBox4.Controls.Add(this.buttonPLCWriter_WriteWord);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.textBoxPLCWriter_Bit);
            this.groupBox4.Controls.Add(this.textBoxPLCWriter_Word_Value);
            this.groupBox4.Controls.Add(this.textBoxPLCWriter_Word);
            this.groupBox4.Location = new System.Drawing.Point(360, 290);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(315, 159);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PLC Writer";
            // 
            // buttonPLCWriter_WriteBitOff
            // 
            this.buttonPLCWriter_WriteBitOff.Location = new System.Drawing.Point(196, 120);
            this.buttonPLCWriter_WriteBitOff.Name = "buttonPLCWriter_WriteBitOff";
            this.buttonPLCWriter_WriteBitOff.Size = new System.Drawing.Size(45, 21);
            this.buttonPLCWriter_WriteBitOff.TabIndex = 2;
            this.buttonPLCWriter_WriteBitOff.Text = "Off";
            this.buttonPLCWriter_WriteBitOff.UseVisualStyleBackColor = true;
            this.buttonPLCWriter_WriteBitOff.Click += new System.EventHandler(this.buttonPLCWriter_WriteBitOff_Click);
            // 
            // buttonPLCWriter_WriteBitOn
            // 
            this.buttonPLCWriter_WriteBitOn.Location = new System.Drawing.Point(145, 120);
            this.buttonPLCWriter_WriteBitOn.Name = "buttonPLCWriter_WriteBitOn";
            this.buttonPLCWriter_WriteBitOn.Size = new System.Drawing.Size(45, 21);
            this.buttonPLCWriter_WriteBitOn.TabIndex = 2;
            this.buttonPLCWriter_WriteBitOn.Text = "On";
            this.buttonPLCWriter_WriteBitOn.UseVisualStyleBackColor = true;
            this.buttonPLCWriter_WriteBitOn.Click += new System.EventHandler(this.buttonPLCWriter_WriteBitOn_Click);
            // 
            // buttonPLCWriter_WriteWord
            // 
            this.buttonPLCWriter_WriteWord.Location = new System.Drawing.Point(179, 49);
            this.buttonPLCWriter_WriteWord.Name = "buttonPLCWriter_WriteWord";
            this.buttonPLCWriter_WriteWord.Size = new System.Drawing.Size(75, 21);
            this.buttonPLCWriter_WriteWord.TabIndex = 2;
            this.buttonPLCWriter_WriteWord.Text = "Write";
            this.buttonPLCWriter_WriteWord.UseVisualStyleBackColor = true;
            this.buttonPLCWriter_WriteWord.Click += new System.EventHandler(this.buttonPLCWriter_WriteWord_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "Bit";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "Word";
            // 
            // textBoxPLCWriter_Bit
            // 
            this.textBoxPLCWriter_Bit.Location = new System.Drawing.Point(32, 123);
            this.textBoxPLCWriter_Bit.Name = "textBoxPLCWriter_Bit";
            this.textBoxPLCWriter_Bit.Size = new System.Drawing.Size(100, 22);
            this.textBoxPLCWriter_Bit.TabIndex = 0;
            this.textBoxPLCWriter_Bit.Text = "D5000.1";
            // 
            // textBoxPLCWriter_Word_Value
            // 
            this.textBoxPLCWriter_Word_Value.Location = new System.Drawing.Point(73, 51);
            this.textBoxPLCWriter_Word_Value.Name = "textBoxPLCWriter_Word_Value";
            this.textBoxPLCWriter_Word_Value.Size = new System.Drawing.Size(100, 22);
            this.textBoxPLCWriter_Word_Value.TabIndex = 0;
            this.textBoxPLCWriter_Word_Value.Text = "123";
            // 
            // textBoxPLCWriter_Word
            // 
            this.textBoxPLCWriter_Word.Location = new System.Drawing.Point(73, 25);
            this.textBoxPLCWriter_Word.Name = "textBoxPLCWriter_Word";
            this.textBoxPLCWriter_Word.Size = new System.Drawing.Size(100, 22);
            this.textBoxPLCWriter_Word.TabIndex = 0;
            this.textBoxPLCWriter_Word.Text = "D5000";
            // 
            // SimAlarmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 460);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonKeySwitchOffOP);
            this.Controls.Add(this.buttonKeySwitchOffHP);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonKeySwitchAutoOP);
            this.Controls.Add(this.buttonKeySwitchAutoHP);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDoorClosedOP);
            this.Controls.Add(this.buttonDoorClosedHP);
            this.Controls.Add(this.buttonDoorOpenOP);
            this.Controls.Add(this.buttonDoorOpenHP);
            this.Name = "SimAlarmView";
            this.Text = "StockerSimView";
            this.Load += new System.EventHandler(this.StockerSimView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDoorOpenHP;
        private System.Windows.Forms.Button buttonDoorOpenOP;
        private System.Windows.Forms.Button buttonDoorClosedHP;
        private System.Windows.Forms.Button buttonDoorClosedOP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAlarmClear;
        private System.Windows.Forms.ComboBox comboBoxAlarmCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAlarmSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAlarmCode;
        private System.Windows.Forms.TextBox textBoxPLCIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPCIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Button buttonKeySwitchAutoHP;
        private System.Windows.Forms.Button buttonKeySwitchAutoOP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAlarmClearIO;
        private System.Windows.Forms.ComboBox comboBoxAlarmCodeIO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAlarmSetIO;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAlarmCodeIO;
        private System.Windows.Forms.TextBox textBoxPLCIndexIO;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPCIndexIO;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonKeySwitchOffHP;
        private System.Windows.Forms.Button buttonKeySwitchOffOP;
        private System.Windows.Forms.ComboBox comboBoxCraneID;
        private System.Windows.Forms.ComboBox comboBoxIOPortID;
        private System.Windows.Forms.Button buttonAreaSensorSignal1Off;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Button buttonAreaSensorSignal1On;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonPLCWriter_WriteBitOff;
        private System.Windows.Forms.Button buttonPLCWriter_WriteBitOn;
        private System.Windows.Forms.Button buttonPLCWriter_WriteWord;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPLCWriter_Bit;
        private System.Windows.Forms.TextBox textBoxPLCWriter_Word_Value;
        private System.Windows.Forms.TextBox textBoxPLCWriter_Word;
    }
}
