namespace Mirle.WebAPI.U2NMMA30.View
{
    partial class MainTestForm
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
            this.btnAutoPickup = new System.Windows.Forms.Button();
            this.btnPickupQuery = new System.Windows.Forms.Button();
            this.btnPositionReport = new System.Windows.Forms.Button();
            this.btnPutawayCheck = new System.Windows.Forms.Button();
            this.btnPutAwayComplete = new System.Windows.Forms.Button();
            this.btnRetrieveComplete = new System.Windows.Forms.Button();
            this.btnShelfComplete = new System.Windows.Forms.Button();
            this.btnShelfReport = new System.Windows.Forms.Button();
            this.btnShelfRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAutoPickup
            // 
            this.btnAutoPickup.Location = new System.Drawing.Point(40, 24);
            this.btnAutoPickup.Name = "btnAutoPickup";
            this.btnAutoPickup.Size = new System.Drawing.Size(100, 23);
            this.btnAutoPickup.TabIndex = 0;
            this.btnAutoPickup.Text = "AutoPickup";
            this.btnAutoPickup.UseVisualStyleBackColor = true;
            this.btnAutoPickup.Click += new System.EventHandler(this.btnAutoPickup_Click);
            // 
            // btnPickupQuery
            // 
            this.btnPickupQuery.Location = new System.Drawing.Point(40, 72);
            this.btnPickupQuery.Name = "btnPickupQuery";
            this.btnPickupQuery.Size = new System.Drawing.Size(100, 23);
            this.btnPickupQuery.TabIndex = 1;
            this.btnPickupQuery.Text = "PickupQuery";
            this.btnPickupQuery.UseVisualStyleBackColor = true;
            this.btnPickupQuery.Click += new System.EventHandler(this.btnPickupQuery_Click);
            // 
            // btnPositionReport
            // 
            this.btnPositionReport.Location = new System.Drawing.Point(40, 122);
            this.btnPositionReport.Name = "btnPositionReport";
            this.btnPositionReport.Size = new System.Drawing.Size(100, 23);
            this.btnPositionReport.TabIndex = 2;
            this.btnPositionReport.Text = "PositionReport";
            this.btnPositionReport.UseVisualStyleBackColor = true;
            this.btnPositionReport.Click += new System.EventHandler(this.btnPositionReport_Click);
            // 
            // btnPutawayCheck
            // 
            this.btnPutawayCheck.Location = new System.Drawing.Point(40, 169);
            this.btnPutawayCheck.Name = "btnPutawayCheck";
            this.btnPutawayCheck.Size = new System.Drawing.Size(100, 23);
            this.btnPutawayCheck.TabIndex = 3;
            this.btnPutawayCheck.Text = "PutawayCheck";
            this.btnPutawayCheck.UseVisualStyleBackColor = true;
            this.btnPutawayCheck.Click += new System.EventHandler(this.btnPutawayCheck_Click);
            // 
            // btnPutAwayComplete
            // 
            this.btnPutAwayComplete.Location = new System.Drawing.Point(163, 24);
            this.btnPutAwayComplete.Name = "btnPutAwayComplete";
            this.btnPutAwayComplete.Size = new System.Drawing.Size(100, 23);
            this.btnPutAwayComplete.TabIndex = 4;
            this.btnPutAwayComplete.Text = "PutAwayComplete";
            this.btnPutAwayComplete.UseVisualStyleBackColor = true;
            this.btnPutAwayComplete.Click += new System.EventHandler(this.btnPutAwayComplete_Click);
            // 
            // btnRetrieveComplete
            // 
            this.btnRetrieveComplete.Location = new System.Drawing.Point(163, 72);
            this.btnRetrieveComplete.Name = "btnRetrieveComplete";
            this.btnRetrieveComplete.Size = new System.Drawing.Size(100, 23);
            this.btnRetrieveComplete.TabIndex = 5;
            this.btnRetrieveComplete.Text = "RetrieveComplete";
            this.btnRetrieveComplete.UseVisualStyleBackColor = true;
            this.btnRetrieveComplete.Click += new System.EventHandler(this.btnRetrieveComplete_Click);
            // 
            // btnShelfComplete
            // 
            this.btnShelfComplete.Location = new System.Drawing.Point(163, 122);
            this.btnShelfComplete.Name = "btnShelfComplete";
            this.btnShelfComplete.Size = new System.Drawing.Size(100, 23);
            this.btnShelfComplete.TabIndex = 6;
            this.btnShelfComplete.Text = "ShelfComplete";
            this.btnShelfComplete.UseVisualStyleBackColor = true;
            this.btnShelfComplete.Click += new System.EventHandler(this.btnShelfComplete_Click);
            // 
            // btnShelfReport
            // 
            this.btnShelfReport.Location = new System.Drawing.Point(163, 169);
            this.btnShelfReport.Name = "btnShelfReport";
            this.btnShelfReport.Size = new System.Drawing.Size(100, 23);
            this.btnShelfReport.TabIndex = 7;
            this.btnShelfReport.Text = "ShelfReport";
            this.btnShelfReport.UseVisualStyleBackColor = true;
            this.btnShelfReport.Click += new System.EventHandler(this.btnShelfReport_Click);
            // 
            // btnShelfRequest
            // 
            this.btnShelfRequest.Location = new System.Drawing.Point(303, 24);
            this.btnShelfRequest.Name = "btnShelfRequest";
            this.btnShelfRequest.Size = new System.Drawing.Size(100, 23);
            this.btnShelfRequest.TabIndex = 8;
            this.btnShelfRequest.Text = "ShelfRequest";
            this.btnShelfRequest.UseVisualStyleBackColor = true;
            this.btnShelfRequest.Click += new System.EventHandler(this.btnShelfRequest_Click);
            // 
            // MainTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnShelfRequest);
            this.Controls.Add(this.btnShelfReport);
            this.Controls.Add(this.btnShelfComplete);
            this.Controls.Add(this.btnRetrieveComplete);
            this.Controls.Add(this.btnPutAwayComplete);
            this.Controls.Add(this.btnPutawayCheck);
            this.Controls.Add(this.btnPositionReport);
            this.Controls.Add(this.btnPickupQuery);
            this.Controls.Add(this.btnAutoPickup);
            this.Name = "MainTestForm";
            this.Text = "MainTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAutoPickup;
        private System.Windows.Forms.Button btnPickupQuery;
        private System.Windows.Forms.Button btnPositionReport;
        private System.Windows.Forms.Button btnPutawayCheck;
        private System.Windows.Forms.Button btnPutAwayComplete;
        private System.Windows.Forms.Button btnRetrieveComplete;
        private System.Windows.Forms.Button btnShelfComplete;
        private System.Windows.Forms.Button btnShelfReport;
        private System.Windows.Forms.Button btnShelfRequest;
    }
}