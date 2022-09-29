namespace Mirle.WebAPI.Test.Controllers.ApiList
{
    partial class CtrlBufferInitialInfo
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label_jobId = new System.Windows.Forms.Label();
            this.textBox_jobId = new System.Windows.Forms.TextBox();
            this.button_BufferInitital = new System.Windows.Forms.Button();
            this.label_bufferId = new System.Windows.Forms.Label();
            this.textBox_bufferId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "E800C",
            "SMTC",
            "LIFT4C",
            "LIFT5C",
            "B800C",
            "M800C",
            "OSMTC"});
            this.comboBox1.Location = new System.Drawing.Point(112, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(270, 26);
            this.comboBox1.TabIndex = 0;
            // 
            // label_jobId
            // 
            this.label_jobId.AutoSize = true;
            this.label_jobId.Location = new System.Drawing.Point(109, 133);
            this.label_jobId.Name = "label_jobId";
            this.label_jobId.Size = new System.Drawing.Size(43, 18);
            this.label_jobId.TabIndex = 1;
            this.label_jobId.Text = "jobId";
            // 
            // textBox_jobId
            // 
            this.textBox_jobId.Location = new System.Drawing.Point(207, 130);
            this.textBox_jobId.Name = "textBox_jobId";
            this.textBox_jobId.Size = new System.Drawing.Size(100, 29);
            this.textBox_jobId.TabIndex = 2;
            // 
            // button_BufferInitital
            // 
            this.button_BufferInitital.Location = new System.Drawing.Point(355, 135);
            this.button_BufferInitital.Name = "button_BufferInitital";
            this.button_BufferInitital.Size = new System.Drawing.Size(123, 97);
            this.button_BufferInitital.TabIndex = 3;
            this.button_BufferInitital.Text = "send";
            this.button_BufferInitital.UseVisualStyleBackColor = true;
            this.button_BufferInitital.Click += new System.EventHandler(this.button_BufferInitital_Click);
            // 
            // label_bufferId
            // 
            this.label_bufferId.AutoSize = true;
            this.label_bufferId.Location = new System.Drawing.Point(109, 206);
            this.label_bufferId.Name = "label_bufferId";
            this.label_bufferId.Size = new System.Drawing.Size(64, 18);
            this.label_bufferId.TabIndex = 1;
            this.label_bufferId.Text = "bufferId";
            // 
            // textBox_bufferId
            // 
            this.textBox_bufferId.Location = new System.Drawing.Point(207, 203);
            this.textBox_bufferId.Name = "textBox_bufferId";
            this.textBox_bufferId.Size = new System.Drawing.Size(100, 29);
            this.textBox_bufferId.TabIndex = 2;
            // 
            // CtrlBufferInitialInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 301);
            this.Controls.Add(this.button_BufferInitital);
            this.Controls.Add(this.textBox_bufferId);
            this.Controls.Add(this.label_bufferId);
            this.Controls.Add(this.textBox_jobId);
            this.Controls.Add(this.label_jobId);
            this.Controls.Add(this.comboBox1);
            this.Name = "CtrlBufferInitialInfo";
            this.Text = "CtrlBufferInitial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label_jobId;
        private System.Windows.Forms.TextBox textBox_jobId;
        private System.Windows.Forms.Button button_BufferInitital;
        private System.Windows.Forms.Label label_bufferId;
        private System.Windows.Forms.TextBox textBox_bufferId;
    }
}