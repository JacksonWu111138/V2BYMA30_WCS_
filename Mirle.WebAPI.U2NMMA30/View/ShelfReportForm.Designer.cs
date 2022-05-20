
namespace Mirle.WebAPI.U2NMMA30.View
{
    partial class ShelfReportForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtCarrierID = new System.Windows.Forms.TextBox();
            this.txtShelfId = new System.Windows.Forms.TextBox();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtShelfStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(65, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCarrierID
            // 
            this.txtCarrierID.Location = new System.Drawing.Point(128, 50);
            this.txtCarrierID.Name = "txtCarrierID";
            this.txtCarrierID.Size = new System.Drawing.Size(100, 22);
            this.txtCarrierID.TabIndex = 28;
            // 
            // txtShelfId
            // 
            this.txtShelfId.Location = new System.Drawing.Point(128, 82);
            this.txtShelfId.Name = "txtShelfId";
            this.txtShelfId.Size = new System.Drawing.Size(100, 22);
            this.txtShelfId.TabIndex = 27;
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(128, 20);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(100, 22);
            this.txtJobID.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "CarrierID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "ShelfId";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "JobID";
            // 
            // txtShelfStatus
            // 
            this.txtShelfStatus.Location = new System.Drawing.Point(128, 118);
            this.txtShelfStatus.Name = "txtShelfStatus";
            this.txtShelfStatus.Size = new System.Drawing.Size(100, 22);
            this.txtShelfStatus.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "ShelfStatus";
            // 
            // ShelfReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtShelfStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCarrierID);
            this.Controls.Add(this.txtShelfId);
            this.Controls.Add(this.txtJobID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ShelfReportForm";
            this.Text = "ShelfReportForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCarrierID;
        private System.Windows.Forms.TextBox txtShelfId;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShelfStatus;
        private System.Windows.Forms.Label label4;
    }
}