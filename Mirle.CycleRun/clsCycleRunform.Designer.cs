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
            this.button1 = new System.Windows.Forms.Button();
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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_testlocation = new System.Windows.Forms.TextBox();
            this.label_testlocation = new System.Windows.Forms.Label();
            this.label_testNextlocation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_PCBAStart
            // 
            this.button_PCBAStart.Location = new System.Drawing.Point(596, 34);
            this.button_PCBAStart.Name = "button_PCBAStart";
            this.button_PCBAStart.Size = new System.Drawing.Size(70, 41);
            this.button_PCBAStart.TabIndex = 0;
            this.button_PCBAStart.Text = "Start";
            this.button_PCBAStart.UseVisualStyleBackColor = true;
            this.button_PCBAStart.Click += new System.EventHandler(this.button_PCBAStart_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(672, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 74);
            this.button1.TabIndex = 1;
            this.button1.Text = "Get Status";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_PCBACycleEnd
            // 
            this.button_PCBACycleEnd.Location = new System.Drawing.Point(596, 93);
            this.button_PCBACycleEnd.Name = "button_PCBACycleEnd";
            this.button_PCBACycleEnd.Size = new System.Drawing.Size(70, 41);
            this.button_PCBACycleEnd.TabIndex = 2;
            this.button_PCBACycleEnd.Text = "End";
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(307, 144);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 41);
            this.button2.TabIndex = 7;
            this.button2.Text = "GetNextLocation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // clsCycleRunform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 450);
            this.Controls.Add(this.label_testNextlocation);
            this.Controls.Add(this.label_testlocation);
            this.Controls.Add(this.textBox_testlocation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label_StockInAndOutStartLocation);
            this.Controls.Add(this.label_L2LM802);
            this.Controls.Add(this.label_L2LM801);
            this.Controls.Add(this.label_StockInAndOutCarrierId);
            this.Controls.Add(this.label_L2L);
            this.Controls.Add(this.label_StockInAndOut);
            this.Controls.Add(this.label_PCBACycle);
            this.Controls.Add(this.textBox_L2LM802);
            this.Controls.Add(this.textBox_L2LM801);
            this.Controls.Add(this.textBox_StockInAndOutStartLocation);
            this.Controls.Add(this.textBox_StockInAndOutCarrierId);
            this.Controls.Add(this.button_PCBACycleEnd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_PCBAStart);
            this.Name = "clsCycleRunform";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_PCBAStart;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_testlocation;
        private System.Windows.Forms.Label label_testlocation;
        private System.Windows.Forms.Label label_testNextlocation;
    }
}

