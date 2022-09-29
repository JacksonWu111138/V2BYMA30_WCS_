namespace Mirle.WebAPI.Test.WES.testingList
{
    partial class WESGetBufferNameaAndStnNo
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
            this.button_GetBuffer = new System.Windows.Forms.Button();
            this.label_bufferName = new System.Windows.Forms.Label();
            this.textBox_bufferName = new System.Windows.Forms.TextBox();
            this.label_StnNo = new System.Windows.Forms.Label();
            this.textBox_StnNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_GetBuffer
            // 
            this.button_GetBuffer.Location = new System.Drawing.Point(387, 106);
            this.button_GetBuffer.Name = "button_GetBuffer";
            this.button_GetBuffer.Size = new System.Drawing.Size(110, 104);
            this.button_GetBuffer.TabIndex = 0;
            this.button_GetBuffer.Text = "Get";
            this.button_GetBuffer.UseVisualStyleBackColor = true;
            this.button_GetBuffer.Click += new System.EventHandler(this.button_GetBuffer_Click);
            // 
            // label_bufferName
            // 
            this.label_bufferName.AutoSize = true;
            this.label_bufferName.Location = new System.Drawing.Point(59, 89);
            this.label_bufferName.Name = "label_bufferName";
            this.label_bufferName.Size = new System.Drawing.Size(91, 18);
            this.label_bufferName.TabIndex = 1;
            this.label_bufferName.Text = "bufferName";
            // 
            // textBox_bufferName
            // 
            this.textBox_bufferName.Location = new System.Drawing.Point(178, 86);
            this.textBox_bufferName.Name = "textBox_bufferName";
            this.textBox_bufferName.Size = new System.Drawing.Size(100, 29);
            this.textBox_bufferName.TabIndex = 2;
            // 
            // label_StnNo
            // 
            this.label_StnNo.AutoSize = true;
            this.label_StnNo.Location = new System.Drawing.Point(59, 149);
            this.label_StnNo.Name = "label_StnNo";
            this.label_StnNo.Size = new System.Drawing.Size(50, 18);
            this.label_StnNo.TabIndex = 1;
            this.label_StnNo.Text = "StnNo";
            // 
            // textBox_StnNo
            // 
            this.textBox_StnNo.Location = new System.Drawing.Point(178, 146);
            this.textBox_StnNo.Name = "textBox_StnNo";
            this.textBox_StnNo.Size = new System.Drawing.Size(100, 29);
            this.textBox_StnNo.TabIndex = 2;
            // 
            // WESGetBufferNameaAndStnNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_StnNo);
            this.Controls.Add(this.label_StnNo);
            this.Controls.Add(this.textBox_bufferName);
            this.Controls.Add(this.label_bufferName);
            this.Controls.Add(this.button_GetBuffer);
            this.Name = "WESGetBufferNameaAndStnNo";
            this.Text = "GetBufferName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_GetBuffer;
        private System.Windows.Forms.Label label_bufferName;
        private System.Windows.Forms.TextBox textBox_bufferName;
        private System.Windows.Forms.Label label_StnNo;
        private System.Windows.Forms.TextBox textBox_StnNo;
    }
}