
namespace Mirle.WebAPI.U2NMMA30.View
{
    partial class ShelfRequestForm
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
            this.txtFromShelfId = new System.Windows.Forms.TextBox();
            this.txtToShelfId = new System.Windows.Forms.TextBox();
            this.txtJobID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFromShelfId
            // 
            this.txtFromShelfId.Location = new System.Drawing.Point(130, 53);
            this.txtFromShelfId.Name = "txtFromShelfId";
            this.txtFromShelfId.Size = new System.Drawing.Size(100, 22);
            this.txtFromShelfId.TabIndex = 36;
            // 
            // txtToShelfId
            // 
            this.txtToShelfId.Location = new System.Drawing.Point(130, 85);
            this.txtToShelfId.Name = "txtToShelfId";
            this.txtToShelfId.Size = new System.Drawing.Size(100, 22);
            this.txtToShelfId.TabIndex = 35;
            // 
            // txtJobID
            // 
            this.txtJobID.Location = new System.Drawing.Point(130, 23);
            this.txtJobID.Name = "txtJobID";
            this.txtJobID.Size = new System.Drawing.Size(100, 22);
            this.txtJobID.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 12);
            this.label3.TabIndex = 33;
            this.label3.Text = "FromShelfId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "ToShelfId";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "JobID";
            // 
            // ShelfRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFromShelfId);
            this.Controls.Add(this.txtToShelfId);
            this.Controls.Add(this.txtJobID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ShelfRequestForm";
            this.Text = "ShelfRequestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFromShelfId;
        private System.Windows.Forms.TextBox txtToShelfId;
        private System.Windows.Forms.TextBox txtJobID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}