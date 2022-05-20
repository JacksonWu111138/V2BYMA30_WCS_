
namespace Mirle.WebAPI.U2NMMA30.View
{
    partial class RetrieveCompleteForm
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
            this.txtPortID = new System.Windows.Forms.TextBox();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIsComplete = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmptyTransfer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 201);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCarrierID
            // 
            this.txtCarrierID.Location = new System.Drawing.Point(183, 80);
            this.txtCarrierID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCarrierID.Name = "txtCarrierID";
            this.txtCarrierID.Size = new System.Drawing.Size(148, 29);
            this.txtCarrierID.TabIndex = 21;
            // 
            // txtPortID
            // 
            this.txtPortID.Location = new System.Drawing.Point(183, 128);
            this.txtPortID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPortID.Name = "txtPortID";
            this.txtPortID.Size = new System.Drawing.Size(148, 29);
            this.txtPortID.TabIndex = 20;
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(183, 34);
            this.txtJobID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(148, 29);
            this.txtJobID.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "CarrierID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 135);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "PortID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "JobID";
            // 
            // txtIsComplete
            // 
            this.txtIsComplete.Location = new System.Drawing.Point(540, 34);
            this.txtIsComplete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIsComplete.Name = "txtIsComplete";
            this.txtIsComplete.Size = new System.Drawing.Size(148, 29);
            this.txtIsComplete.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "IsComplete";
            // 
            // txtEmptyTransfer
            // 
            this.txtEmptyTransfer.Location = new System.Drawing.Point(540, 79);
            this.txtEmptyTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmptyTransfer.Name = "txtEmptyTransfer";
            this.txtEmptyTransfer.Size = new System.Drawing.Size(148, 29);
            this.txtEmptyTransfer.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(406, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 18);
            this.label5.TabIndex = 25;
            this.label5.Text = "emptyTransfer";
            // 
            // RetrieveCompleteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.txtEmptyTransfer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIsComplete);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCarrierID);
            this.Controls.Add(this.txtPortID);
            this.Controls.Add(this.txtJobID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RetrieveCompleteForm";
            this.Text = "RetrieveCompleteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCarrierID;
        private System.Windows.Forms.TextBox txtPortID;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIsComplete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmptyTransfer;
        private System.Windows.Forms.Label label5;
    }
}