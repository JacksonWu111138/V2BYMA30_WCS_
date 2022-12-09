namespace Mirle.CycleRun
{
    partial class clsCycleRunform
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
            this.button_PCBAStart = new System.Windows.Forms.Button();
            this.button_PCBAGetStatus = new System.Windows.Forms.Button();
            this.button_PCBACycleEnd = new System.Windows.Forms.Button();
            this.textBox_StockInAndOutCarrierId = new System.Windows.Forms.TextBox();
            this.label_PCBACycle = new System.Windows.Forms.Label();
            this.label_StockInAndOut = new System.Windows.Forms.Label();
            this.label_StockInAndOutCarrierId = new System.Windows.Forms.Label();
            this.textBox_StockInAndOutStartLocation = new System.Windows.Forms.TextBox();
            this.label_StockInAndOutStartLocation = new System.Windows.Forms.Label();
            this.textBox_L2LM801 = new System.Windows.Forms.TextBox();
            this.textBox_L2LM802 = new System.Windows.Forms.TextBox();
            this.label_L2L = new System.Windows.Forms.Label();
            this.label_L2LM801 = new System.Windows.Forms.Label();
            this.label_L2LM802 = new System.Windows.Forms.Label();
            this.button_CycleGetNextLocation = new System.Windows.Forms.Button();
            this.textBox_testlocation = new System.Windows.Forms.TextBox();
            this.label_testlocation = new System.Windows.Forms.Label();
            this.label_testNextlocation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_B801StockInAndOut_carrierId = new System.Windows.Forms.TextBox();
            this.textBox_B801StockInAndOut_location = new System.Windows.Forms.TextBox();
            this.label_B801StockInAndOut = new System.Windows.Forms.Label();
            this.label_B801StockInAndOut_carrierId = new System.Windows.Forms.Label();
            this.label_B801StockInAndOut_location = new System.Windows.Forms.Label();
            this.textBox_B802StockInAndOut_carrierId = new System.Windows.Forms.TextBox();
            this.textBox_B802StockInAndOut_location = new System.Windows.Forms.TextBox();
            this.label_B802StockInAndOut_carrierId = new System.Windows.Forms.Label();
            this.label_B802StockInAndOut_location = new System.Windows.Forms.Label();
            this.label_infomation = new System.Windows.Forms.Label();
            this.label_B802StockInAndOut = new System.Windows.Forms.Label();
            this.textBox_B803StockInAndOut_carrierId = new System.Windows.Forms.TextBox();
            this.textBox_B803StockInAndOut_location = new System.Windows.Forms.TextBox();
            this.label_B803StockInAndOut = new System.Windows.Forms.Label();
            this.label_B803StockInAndOut_carrierId = new System.Windows.Forms.Label();
            this.label_B803StockInAndOut_location = new System.Windows.Forms.Label();
            this.button_BoxCycleRunStart = new System.Windows.Forms.Button();
            this.button_BoxGetStatus = new System.Windows.Forms.Button();
            this.button_BoxCycleRunEnd = new System.Windows.Forms.Button();
            this.textBox_L2LB801 = new System.Windows.Forms.TextBox();
            this.textBox_L2LB802 = new System.Windows.Forms.TextBox();
            this.label2__BoxL2L = new System.Windows.Forms.Label();
            this.label_B801L2L_location = new System.Windows.Forms.Label();
            this.label__B802L2L_location = new System.Windows.Forms.Label();
            this.textBox_L2LB803 = new System.Windows.Forms.TextBox();
            this.label_B803L2L_location = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_PCBAStart
            // 
            this.button_PCBAStart.Location = new System.Drawing.Point(596, 34);
            this.button_PCBAStart.Name = "button_PCBAStart";
            this.button_PCBAStart.Size = new System.Drawing.Size(115, 41);
            this.button_PCBAStart.TabIndex = 0;
            this.button_PCBAStart.Text = "PCBA_Start";
            this.button_PCBAStart.UseVisualStyleBackColor = true;
            this.button_PCBAStart.Click += new System.EventHandler(this.button_PCBAStart_Click);
            // 
            // button_PCBAGetStatus
            // 
            this.button_PCBAGetStatus.Location = new System.Drawing.Point(717, 48);
            this.button_PCBAGetStatus.Name = "button_PCBAGetStatus";
            this.button_PCBAGetStatus.Size = new System.Drawing.Size(158, 74);
            this.button_PCBAGetStatus.TabIndex = 1;
            this.button_PCBAGetStatus.Text = "Get PCBA Status";
            this.button_PCBAGetStatus.UseVisualStyleBackColor = true;
            this.button_PCBAGetStatus.Click += new System.EventHandler(this.button_PCBAGetStatus_Click);
            // 
            // button_PCBACycleEnd
            // 
            this.button_PCBACycleEnd.Location = new System.Drawing.Point(596, 93);
            this.button_PCBACycleEnd.Name = "button_PCBACycleEnd";
            this.button_PCBACycleEnd.Size = new System.Drawing.Size(115, 41);
            this.button_PCBACycleEnd.TabIndex = 2;
            this.button_PCBACycleEnd.Text = "PCBA_End";
            this.button_PCBACycleEnd.UseVisualStyleBackColor = true;
            this.button_PCBACycleEnd.Click += new System.EventHandler(this.button_PCBACycleEnd_Click);
            // 
            // textBox_StockInAndOutCarrierId
            // 
            this.textBox_StockInAndOutCarrierId.Location = new System.Drawing.Point(247, 54);
            this.textBox_StockInAndOutCarrierId.Name = "textBox_StockInAndOutCarrierId";
            this.textBox_StockInAndOutCarrierId.Size = new System.Drawing.Size(100, 29);
            this.textBox_StockInAndOutCarrierId.TabIndex = 3;
            // 
            // label_PCBACycle
            // 
            this.label_PCBACycle.AutoSize = true;
            this.label_PCBACycle.Font = new System.Drawing.Font("新細明體", 18F);
            this.label_PCBACycle.Location = new System.Drawing.Point(12, 9);
            this.label_PCBACycle.Name = "label_PCBACycle";
            this.label_PCBACycle.Size = new System.Drawing.Size(104, 36);
            this.label_PCBACycle.TabIndex = 4;
            this.label_PCBACycle.Text = "PCBA";
            // 
            // label_StockInAndOut
            // 
            this.label_StockInAndOut.AutoSize = true;
            this.label_StockInAndOut.Location = new System.Drawing.Point(50, 57);
            this.label_StockInAndOut.Name = "label_StockInAndOut";
            this.label_StockInAndOut.Size = new System.Drawing.Size(103, 18);
            this.label_StockInAndOut.TabIndex = 5;
            this.label_StockInAndOut.Text = "出庫/入庫：";
            // 
            // label_StockInAndOutCarrierId
            // 
            this.label_StockInAndOutCarrierId.AutoSize = true;
            this.label_StockInAndOutCarrierId.Location = new System.Drawing.Point(159, 57);
            this.label_StockInAndOutCarrierId.Name = "label_StockInAndOutCarrierId";
            this.label_StockInAndOutCarrierId.Size = new System.Drawing.Size(82, 18);
            this.label_StockInAndOutCarrierId.TabIndex = 6;
            this.label_StockInAndOutCarrierId.Text = "CarrierId=";
            // 
            // textBox_StockInAndOutStartLocation
            // 
            this.textBox_StockInAndOutStartLocation.Location = new System.Drawing.Point(474, 54);
            this.textBox_StockInAndOutStartLocation.Name = "textBox_StockInAndOutStartLocation";
            this.textBox_StockInAndOutStartLocation.Size = new System.Drawing.Size(100, 29);
            this.textBox_StockInAndOutStartLocation.TabIndex = 3;
            // 
            // label_StockInAndOutStartLocation
            // 
            this.label_StockInAndOutStartLocation.AutoSize = true;
            this.label_StockInAndOutStartLocation.Location = new System.Drawing.Point(357, 57);
            this.label_StockInAndOutStartLocation.Name = "label_StockInAndOutStartLocation";
            this.label_StockInAndOutStartLocation.Size = new System.Drawing.Size(111, 18);
            this.label_StockInAndOutStartLocation.TabIndex = 6;
            this.label_StockInAndOutStartLocation.Text = "Start location=";
            // 
            // textBox_L2LM801
            // 
            this.textBox_L2LM801.Location = new System.Drawing.Point(247, 93);
            this.textBox_L2LM801.Name = "textBox_L2LM801";
            this.textBox_L2LM801.Size = new System.Drawing.Size(100, 29);
            this.textBox_L2LM801.TabIndex = 3;
            // 
            // textBox_L2LM802
            // 
            this.textBox_L2LM802.Location = new System.Drawing.Point(474, 93);
            this.textBox_L2LM802.Name = "textBox_L2LM802";
            this.textBox_L2LM802.Size = new System.Drawing.Size(100, 29);
            this.textBox_L2LM802.TabIndex = 3;
            // 
            // label_L2L
            // 
            this.label_L2L.AutoSize = true;
            this.label_L2L.Location = new System.Drawing.Point(73, 96);
            this.label_L2L.Name = "label_L2L";
            this.label_L2L.Size = new System.Drawing.Size(80, 18);
            this.label_L2L.TabIndex = 5;
            this.label_L2L.Text = "庫對庫：";
            // 
            // label_L2LM801
            // 
            this.label_L2LM801.AutoSize = true;
            this.label_L2LM801.Location = new System.Drawing.Point(151, 96);
            this.label_L2LM801.Name = "label_L2LM801";
            this.label_L2LM801.Size = new System.Drawing.Size(90, 18);
            this.label_L2LM801.TabIndex = 6;
            this.label_L2LM801.Text = "M801Start=";
            this.label_L2LM801.Click += new System.EventHandler(this.label_L2LM801_Click);
            // 
            // label_L2LM802
            // 
            this.label_L2LM802.AutoSize = true;
            this.label_L2LM802.Location = new System.Drawing.Point(378, 96);
            this.label_L2LM802.Name = "label_L2LM802";
            this.label_L2LM802.Size = new System.Drawing.Size(90, 18);
            this.label_L2LM802.TabIndex = 6;
            this.label_L2LM802.Text = "M802Start=";
            this.label_L2LM802.Click += new System.EventHandler(this.label_L2LM801_Click);
            // 
            // button_CycleGetNextLocation
            // 
            this.button_CycleGetNextLocation.Location = new System.Drawing.Point(307, 144);
            this.button_CycleGetNextLocation.Name = "button_CycleGetNextLocation";
            this.button_CycleGetNextLocation.Size = new System.Drawing.Size(161, 41);
            this.button_CycleGetNextLocation.TabIndex = 7;
            this.button_CycleGetNextLocation.Text = "GetNextLocation";
            this.button_CycleGetNextLocation.UseVisualStyleBackColor = true;
            this.button_CycleGetNextLocation.Click += new System.EventHandler(this.button_CycleGetNextLocation_Click);
            // 
            // textBox_testlocation
            // 
            this.textBox_testlocation.Location = new System.Drawing.Point(185, 152);
            this.textBox_testlocation.Name = "textBox_testlocation";
            this.textBox_testlocation.Size = new System.Drawing.Size(100, 29);
            this.textBox_testlocation.TabIndex = 8;
            // 
            // label_testlocation
            // 
            this.label_testlocation.AutoSize = true;
            this.label_testlocation.Location = new System.Drawing.Point(116, 155);
            this.label_testlocation.Name = "label_testlocation";
            this.label_testlocation.Size = new System.Drawing.Size(63, 18);
            this.label_testlocation.TabIndex = 9;
            this.label_testlocation.Text = "location";
            // 
            // label_testNextlocation
            // 
            this.label_testNextlocation.AutoSize = true;
            this.label_testNextlocation.Location = new System.Drawing.Point(483, 155);
            this.label_testNextlocation.Name = "label_testNextlocation";
            this.label_testNextlocation.Size = new System.Drawing.Size(116, 18);
            this.label_testNextlocation.TabIndex = 10;
            this.label_testNextlocation.Text = "Next location =";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 18F);
            this.label1.Location = new System.Drawing.Point(12, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "箱式倉";
            // 
            // textBox_B801StockInAndOut_carrierId
            // 
            this.textBox_B801StockInAndOut_carrierId.Location = new System.Drawing.Point(239, 269);
            this.textBox_B801StockInAndOut_carrierId.Name = "textBox_B801StockInAndOut_carrierId";
            this.textBox_B801StockInAndOut_carrierId.Size = new System.Drawing.Size(100, 29);
            this.textBox_B801StockInAndOut_carrierId.TabIndex = 3;
            // 
            // textBox_B801StockInAndOut_location
            // 
            this.textBox_B801StockInAndOut_location.Location = new System.Drawing.Point(462, 269);
            this.textBox_B801StockInAndOut_location.Name = "textBox_B801StockInAndOut_location";
            this.textBox_B801StockInAndOut_location.Size = new System.Drawing.Size(100, 29);
            this.textBox_B801StockInAndOut_location.TabIndex = 3;
            // 
            // label_B801StockInAndOut
            // 
            this.label_B801StockInAndOut.AutoSize = true;
            this.label_B801StockInAndOut.Location = new System.Drawing.Point(12, 272);
            this.label_B801StockInAndOut.Name = "label_B801StockInAndOut";
            this.label_B801StockInAndOut.Size = new System.Drawing.Size(138, 18);
            this.label_B801StockInAndOut.TabIndex = 5;
            this.label_B801StockInAndOut.Text = "B801出庫/入庫：";
            // 
            // label_B801StockInAndOut_carrierId
            // 
            this.label_B801StockInAndOut_carrierId.AutoSize = true;
            this.label_B801StockInAndOut_carrierId.Location = new System.Drawing.Point(151, 272);
            this.label_B801StockInAndOut_carrierId.Name = "label_B801StockInAndOut_carrierId";
            this.label_B801StockInAndOut_carrierId.Size = new System.Drawing.Size(82, 18);
            this.label_B801StockInAndOut_carrierId.TabIndex = 6;
            this.label_B801StockInAndOut_carrierId.Text = "CarrierId=";
            // 
            // label_B801StockInAndOut_location
            // 
            this.label_B801StockInAndOut_location.AutoSize = true;
            this.label_B801StockInAndOut_location.Location = new System.Drawing.Point(345, 272);
            this.label_B801StockInAndOut_location.Name = "label_B801StockInAndOut_location";
            this.label_B801StockInAndOut_location.Size = new System.Drawing.Size(111, 18);
            this.label_B801StockInAndOut_location.TabIndex = 6;
            this.label_B801StockInAndOut_location.Text = "Start location=";
            // 
            // textBox_B802StockInAndOut_carrierId
            // 
            this.textBox_B802StockInAndOut_carrierId.Location = new System.Drawing.Point(239, 304);
            this.textBox_B802StockInAndOut_carrierId.Name = "textBox_B802StockInAndOut_carrierId";
            this.textBox_B802StockInAndOut_carrierId.Size = new System.Drawing.Size(100, 29);
            this.textBox_B802StockInAndOut_carrierId.TabIndex = 3;
            // 
            // textBox_B802StockInAndOut_location
            // 
            this.textBox_B802StockInAndOut_location.Location = new System.Drawing.Point(462, 304);
            this.textBox_B802StockInAndOut_location.Name = "textBox_B802StockInAndOut_location";
            this.textBox_B802StockInAndOut_location.Size = new System.Drawing.Size(100, 29);
            this.textBox_B802StockInAndOut_location.TabIndex = 3;
            // 
            // label_B802StockInAndOut_carrierId
            // 
            this.label_B802StockInAndOut_carrierId.AutoSize = true;
            this.label_B802StockInAndOut_carrierId.Location = new System.Drawing.Point(151, 307);
            this.label_B802StockInAndOut_carrierId.Name = "label_B802StockInAndOut_carrierId";
            this.label_B802StockInAndOut_carrierId.Size = new System.Drawing.Size(82, 18);
            this.label_B802StockInAndOut_carrierId.TabIndex = 6;
            this.label_B802StockInAndOut_carrierId.Text = "CarrierId=";
            // 
            // label_B802StockInAndOut_location
            // 
            this.label_B802StockInAndOut_location.AutoSize = true;
            this.label_B802StockInAndOut_location.Location = new System.Drawing.Point(345, 307);
            this.label_B802StockInAndOut_location.Name = "label_B802StockInAndOut_location";
            this.label_B802StockInAndOut_location.Size = new System.Drawing.Size(111, 18);
            this.label_B802StockInAndOut_location.TabIndex = 6;
            this.label_B802StockInAndOut_location.Text = "Start location=";
            // 
            // label_infomation
            // 
            this.label_infomation.AutoSize = true;
            this.label_infomation.Location = new System.Drawing.Point(159, 242);
            this.label_infomation.Name = "label_infomation";
            this.label_infomation.Size = new System.Drawing.Size(668, 18);
            this.label_infomation.TabIndex = 5;
            this.label_infomation.Text = "輸入用 \',\' 隔開，前面走左撿料，後面去右撿料；庫對庫: 後儲位搬前儲位再直接回去";
            // 
            // label_B802StockInAndOut
            // 
            this.label_B802StockInAndOut.AutoSize = true;
            this.label_B802StockInAndOut.Location = new System.Drawing.Point(12, 307);
            this.label_B802StockInAndOut.Name = "label_B802StockInAndOut";
            this.label_B802StockInAndOut.Size = new System.Drawing.Size(138, 18);
            this.label_B802StockInAndOut.TabIndex = 5;
            this.label_B802StockInAndOut.Text = "B802出庫/入庫：";
            // 
            // textBox_B803StockInAndOut_carrierId
            // 
            this.textBox_B803StockInAndOut_carrierId.Location = new System.Drawing.Point(239, 339);
            this.textBox_B803StockInAndOut_carrierId.Name = "textBox_B803StockInAndOut_carrierId";
            this.textBox_B803StockInAndOut_carrierId.Size = new System.Drawing.Size(100, 29);
            this.textBox_B803StockInAndOut_carrierId.TabIndex = 3;
            // 
            // textBox_B803StockInAndOut_location
            // 
            this.textBox_B803StockInAndOut_location.Location = new System.Drawing.Point(462, 339);
            this.textBox_B803StockInAndOut_location.Name = "textBox_B803StockInAndOut_location";
            this.textBox_B803StockInAndOut_location.Size = new System.Drawing.Size(100, 29);
            this.textBox_B803StockInAndOut_location.TabIndex = 3;
            // 
            // label_B803StockInAndOut
            // 
            this.label_B803StockInAndOut.AutoSize = true;
            this.label_B803StockInAndOut.Location = new System.Drawing.Point(12, 342);
            this.label_B803StockInAndOut.Name = "label_B803StockInAndOut";
            this.label_B803StockInAndOut.Size = new System.Drawing.Size(138, 18);
            this.label_B803StockInAndOut.TabIndex = 5;
            this.label_B803StockInAndOut.Text = "B802出庫/入庫：";
            // 
            // label_B803StockInAndOut_carrierId
            // 
            this.label_B803StockInAndOut_carrierId.AutoSize = true;
            this.label_B803StockInAndOut_carrierId.Location = new System.Drawing.Point(151, 342);
            this.label_B803StockInAndOut_carrierId.Name = "label_B803StockInAndOut_carrierId";
            this.label_B803StockInAndOut_carrierId.Size = new System.Drawing.Size(82, 18);
            this.label_B803StockInAndOut_carrierId.TabIndex = 6;
            this.label_B803StockInAndOut_carrierId.Text = "CarrierId=";
            // 
            // label_B803StockInAndOut_location
            // 
            this.label_B803StockInAndOut_location.AutoSize = true;
            this.label_B803StockInAndOut_location.Location = new System.Drawing.Point(345, 342);
            this.label_B803StockInAndOut_location.Name = "label_B803StockInAndOut_location";
            this.label_B803StockInAndOut_location.Size = new System.Drawing.Size(111, 18);
            this.label_B803StockInAndOut_location.TabIndex = 6;
            this.label_B803StockInAndOut_location.Text = "Start location=";
            // 
            // button_BoxCycleRunStart
            // 
            this.button_BoxCycleRunStart.Location = new System.Drawing.Point(596, 269);
            this.button_BoxCycleRunStart.Name = "button_BoxCycleRunStart";
            this.button_BoxCycleRunStart.Size = new System.Drawing.Size(104, 41);
            this.button_BoxCycleRunStart.TabIndex = 0;
            this.button_BoxCycleRunStart.Text = "Box_Start";
            this.button_BoxCycleRunStart.UseVisualStyleBackColor = true;
            this.button_BoxCycleRunStart.Click += new System.EventHandler(this.button_BOXStart_Click);
            // 
            // button_BoxGetStatus
            // 
            this.button_BoxGetStatus.Location = new System.Drawing.Point(717, 279);
            this.button_BoxGetStatus.Name = "button_BoxGetStatus";
            this.button_BoxGetStatus.Size = new System.Drawing.Size(158, 74);
            this.button_BoxGetStatus.TabIndex = 1;
            this.button_BoxGetStatus.Text = "Get Box Status";
            this.button_BoxGetStatus.UseVisualStyleBackColor = true;
            this.button_BoxGetStatus.Click += new System.EventHandler(this.button_BoxGetStatus_Click);
            // 
            // button_BoxCycleRunEnd
            // 
            this.button_BoxCycleRunEnd.Location = new System.Drawing.Point(596, 328);
            this.button_BoxCycleRunEnd.Name = "button_BoxCycleRunEnd";
            this.button_BoxCycleRunEnd.Size = new System.Drawing.Size(104, 41);
            this.button_BoxCycleRunEnd.TabIndex = 2;
            this.button_BoxCycleRunEnd.Text = "Box_End";
            this.button_BoxCycleRunEnd.UseVisualStyleBackColor = true;
            this.button_BoxCycleRunEnd.Click += new System.EventHandler(this.button_BOXCycleEnd_Click);
            // 
            // textBox_L2LB801
            // 
            this.textBox_L2LB801.Location = new System.Drawing.Point(239, 374);
            this.textBox_L2LB801.Name = "textBox_L2LB801";
            this.textBox_L2LB801.Size = new System.Drawing.Size(100, 29);
            this.textBox_L2LB801.TabIndex = 3;
            // 
            // textBox_L2LB802
            // 
            this.textBox_L2LB802.Location = new System.Drawing.Point(462, 374);
            this.textBox_L2LB802.Name = "textBox_L2LB802";
            this.textBox_L2LB802.Size = new System.Drawing.Size(100, 29);
            this.textBox_L2LB802.TabIndex = 3;
            // 
            // label2__BoxL2L
            // 
            this.label2__BoxL2L.AutoSize = true;
            this.label2__BoxL2L.Location = new System.Drawing.Point(70, 377);
            this.label2__BoxL2L.Name = "label2__BoxL2L";
            this.label2__BoxL2L.Size = new System.Drawing.Size(80, 18);
            this.label2__BoxL2L.TabIndex = 5;
            this.label2__BoxL2L.Text = "庫對庫：";
            // 
            // label_B801L2L_location
            // 
            this.label_B801L2L_location.AutoSize = true;
            this.label_B801L2L_location.Location = new System.Drawing.Point(147, 377);
            this.label_B801L2L_location.Name = "label_B801L2L_location";
            this.label_B801L2L_location.Size = new System.Drawing.Size(86, 18);
            this.label_B801L2L_location.TabIndex = 6;
            this.label_B801L2L_location.Text = "B801Start=";
            this.label_B801L2L_location.Click += new System.EventHandler(this.label_L2LM801_Click);
            // 
            // label__B802L2L_location
            // 
            this.label__B802L2L_location.AutoSize = true;
            this.label__B802L2L_location.Location = new System.Drawing.Point(370, 377);
            this.label__B802L2L_location.Name = "label__B802L2L_location";
            this.label__B802L2L_location.Size = new System.Drawing.Size(86, 18);
            this.label__B802L2L_location.TabIndex = 6;
            this.label__B802L2L_location.Text = "B802Start=";
            this.label__B802L2L_location.Click += new System.EventHandler(this.label_L2LM801_Click);
            // 
            // textBox_L2LB803
            // 
            this.textBox_L2LB803.Location = new System.Drawing.Point(693, 375);
            this.textBox_L2LB803.Name = "textBox_L2LB803";
            this.textBox_L2LB803.Size = new System.Drawing.Size(100, 29);
            this.textBox_L2LB803.TabIndex = 3;
            // 
            // label_B803L2L_location
            // 
            this.label_B803L2L_location.AutoSize = true;
            this.label_B803L2L_location.Location = new System.Drawing.Point(597, 378);
            this.label_B803L2L_location.Name = "label_B803L2L_location";
            this.label_B803L2L_location.Size = new System.Drawing.Size(86, 18);
            this.label_B803L2L_location.TabIndex = 6;
            this.label_B803L2L_location.Text = "B803Start=";
            this.label_B803L2L_location.Click += new System.EventHandler(this.label_L2LM801_Click);
            // 
            // clsCycleRunform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 450);
            this.Controls.Add(this.label_testNextlocation);
            this.Controls.Add(this.label_testlocation);
            this.Controls.Add(this.textBox_testlocation);
            this.Controls.Add(this.button_CycleGetNextLocation);
            this.Controls.Add(this.label_B803StockInAndOut_location);
            this.Controls.Add(this.label_B802StockInAndOut_location);
            this.Controls.Add(this.label_B801StockInAndOut_location);
            this.Controls.Add(this.label_StockInAndOutStartLocation);
            this.Controls.Add(this.label_B803L2L_location);
            this.Controls.Add(this.label__B802L2L_location);
            this.Controls.Add(this.label_L2LM802);
            this.Controls.Add(this.label_B801L2L_location);
            this.Controls.Add(this.label_L2LM801);
            this.Controls.Add(this.label_B803StockInAndOut_carrierId);
            this.Controls.Add(this.label_B802StockInAndOut_carrierId);
            this.Controls.Add(this.label_B801StockInAndOut_carrierId);
            this.Controls.Add(this.label_StockInAndOutCarrierId);
            this.Controls.Add(this.label_infomation);
            this.Controls.Add(this.label_B803StockInAndOut);
            this.Controls.Add(this.label_B802StockInAndOut);
            this.Controls.Add(this.label_B801StockInAndOut);
            this.Controls.Add(this.label2__BoxL2L);
            this.Controls.Add(this.label_L2L);
            this.Controls.Add(this.label_StockInAndOut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_PCBACycle);
            this.Controls.Add(this.textBox_B803StockInAndOut_location);
            this.Controls.Add(this.textBox_L2LB803);
            this.Controls.Add(this.textBox_L2LB802);
            this.Controls.Add(this.textBox_L2LM802);
            this.Controls.Add(this.textBox_B802StockInAndOut_location);
            this.Controls.Add(this.textBox_B803StockInAndOut_carrierId);
            this.Controls.Add(this.textBox_B801StockInAndOut_location);
            this.Controls.Add(this.textBox_B802StockInAndOut_carrierId);
            this.Controls.Add(this.textBox_L2LB801);
            this.Controls.Add(this.textBox_L2LM801);
            this.Controls.Add(this.textBox_B801StockInAndOut_carrierId);
            this.Controls.Add(this.textBox_StockInAndOutStartLocation);
            this.Controls.Add(this.textBox_StockInAndOutCarrierId);
            this.Controls.Add(this.button_BoxCycleRunEnd);
            this.Controls.Add(this.button_PCBACycleEnd);
            this.Controls.Add(this.button_BoxGetStatus);
            this.Controls.Add(this.button_PCBAGetStatus);
            this.Controls.Add(this.button_BoxCycleRunStart);
            this.Controls.Add(this.button_PCBAStart);
            this.Name = "clsCycleRunform";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_PCBAStart;
        private System.Windows.Forms.Button button_PCBAGetStatus;
        private System.Windows.Forms.Button button_PCBACycleEnd;
        private System.Windows.Forms.TextBox textBox_StockInAndOutCarrierId;
        private System.Windows.Forms.Label label_PCBACycle;
        private System.Windows.Forms.Label label_StockInAndOut;
        private System.Windows.Forms.Label label_StockInAndOutCarrierId;
        private System.Windows.Forms.TextBox textBox_StockInAndOutStartLocation;
        private System.Windows.Forms.Label label_StockInAndOutStartLocation;
        private System.Windows.Forms.TextBox textBox_L2LM801;
        private System.Windows.Forms.TextBox textBox_L2LM802;
        private System.Windows.Forms.Label label_L2L;
        private System.Windows.Forms.Label label_L2LM801;
        private System.Windows.Forms.Label label_L2LM802;
        private System.Windows.Forms.Button button_CycleGetNextLocation;
        private System.Windows.Forms.TextBox textBox_testlocation;
        private System.Windows.Forms.Label label_testlocation;
        private System.Windows.Forms.Label label_testNextlocation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_B801StockInAndOut_carrierId;
        private System.Windows.Forms.TextBox textBox_B801StockInAndOut_location;
        private System.Windows.Forms.Label label_B801StockInAndOut;
        private System.Windows.Forms.Label label_B801StockInAndOut_carrierId;
        private System.Windows.Forms.Label label_B801StockInAndOut_location;
        private System.Windows.Forms.TextBox textBox_B802StockInAndOut_carrierId;
        private System.Windows.Forms.TextBox textBox_B802StockInAndOut_location;
        private System.Windows.Forms.Label label_B802StockInAndOut_carrierId;
        private System.Windows.Forms.Label label_B802StockInAndOut_location;
        private System.Windows.Forms.Label label_infomation;
        private System.Windows.Forms.Label label_B802StockInAndOut;
        private System.Windows.Forms.TextBox textBox_B803StockInAndOut_carrierId;
        private System.Windows.Forms.TextBox textBox_B803StockInAndOut_location;
        private System.Windows.Forms.Label label_B803StockInAndOut;
        private System.Windows.Forms.Label label_B803StockInAndOut_carrierId;
        private System.Windows.Forms.Label label_B803StockInAndOut_location;
        private System.Windows.Forms.Button button_BoxCycleRunStart;
        private System.Windows.Forms.Button button_BoxGetStatus;
        private System.Windows.Forms.Button button_BoxCycleRunEnd;
        private System.Windows.Forms.TextBox textBox_L2LB801;
        private System.Windows.Forms.TextBox textBox_L2LB802;
        private System.Windows.Forms.Label label2__BoxL2L;
        private System.Windows.Forms.Label label_B801L2L_location;
        private System.Windows.Forms.Label label__B802L2L_location;
        private System.Windows.Forms.TextBox textBox_L2LB803;
        private System.Windows.Forms.Label label_B803L2L_location;
    }
}

