namespace Mirle.WebAPI.Test.Controllers.ApiList
{
    partial class CtrlLotRetrieveTransfer
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
            this.button_LotRetrieveTransfer = new System.Windows.Forms.Button();
            this.label_jobId = new System.Windows.Forms.Label();
            this.textBox_jobId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_LotRetrieveTransfer
            // 
            this.button_LotRetrieveTransfer.Location = new System.Drawing.Point(405, 56);
            this.button_LotRetrieveTransfer.Name = "button_LotRetrieveTransfer";
            this.button_LotRetrieveTransfer.Size = new System.Drawing.Size(128, 120);
            this.button_LotRetrieveTransfer.TabIndex = 0;
            this.button_LotRetrieveTransfer.Text = "send";
            this.button_LotRetrieveTransfer.UseVisualStyleBackColor = true;
            // 
            // label_jobId
            // 
            this.label_jobId.AutoSize = true;
            this.label_jobId.Location = new System.Drawing.Point(66, 56);
            this.label_jobId.Name = "label_jobId";
            this.label_jobId.Size = new System.Drawing.Size(43, 18);
            this.label_jobId.TabIndex = 1;
            this.label_jobId.Text = "jobId";
            // 
            // textBox_jobId
            // 
            this.textBox_jobId.Location = new System.Drawing.Point(139, 53);
            this.textBox_jobId.Name = "textBox_jobId";
            this.textBox_jobId.Size = new System.Drawing.Size(135, 29);
            this.textBox_jobId.TabIndex = 2;
            // 
            // CtrlLotRetrieveTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_jobId);
            this.Controls.Add(this.label_jobId);
            this.Controls.Add(this.button_LotRetrieveTransfer);
            this.Name = "CtrlLotRetrieveTransfer";
            this.Text = "LotRetrieveTransfer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_LotRetrieveTransfer;
        private System.Windows.Forms.Label label_jobId;
        private System.Windows.Forms.TextBox textBox_jobId;
    }
}