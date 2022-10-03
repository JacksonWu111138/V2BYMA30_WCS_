namespace Mirle.WebAPI.Test.WES.testingList
{
    partial class WESDBTEST
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
            this.button_testDBConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_testDBConnection
            // 
            this.button_testDBConnection.Location = new System.Drawing.Point(158, 88);
            this.button_testDBConnection.Name = "button_testDBConnection";
            this.button_testDBConnection.Size = new System.Drawing.Size(239, 140);
            this.button_testDBConnection.TabIndex = 0;
            this.button_testDBConnection.Text = "TestConnection";
            this.button_testDBConnection.UseVisualStyleBackColor = true;
            this.button_testDBConnection.Click += new System.EventHandler(this.button_testDBConnection_Click);
            // 
            // WESDBTEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_testDBConnection);
            this.Name = "WESDBTEST";
            this.Text = "WES_DB_TEST";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_testDBConnection;
    }
}