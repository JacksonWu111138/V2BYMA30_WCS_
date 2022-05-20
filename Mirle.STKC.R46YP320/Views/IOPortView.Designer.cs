namespace Mirle.STKC.R46YP320.Views
{
    partial class IOPortView
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
            this.tlpMain_IOPort = new System.Windows.Forms.TableLayoutPanel();
            this.subFormPanel = new System.Windows.Forms.Panel();
            this.cboIOPort = new System.Windows.Forms.ComboBox();
            this.butIO_State1 = new System.Windows.Forms.Button();
            this.butIO_State2 = new System.Windows.Forms.Button();
            this.butIO_Vehicle = new System.Windows.Forms.Button();
            this.butIO_InterFace = new System.Windows.Forms.Button();
            this.tlpMain_IOPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain_IOPort
            // 
            this.tlpMain_IOPort.ColumnCount = 4;
            this.tlpMain_IOPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain_IOPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain_IOPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain_IOPort.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain_IOPort.Controls.Add(this.subFormPanel, 0, 2);
            this.tlpMain_IOPort.Controls.Add(this.cboIOPort, 0, 0);
            this.tlpMain_IOPort.Controls.Add(this.butIO_State1, 0, 1);
            this.tlpMain_IOPort.Controls.Add(this.butIO_State2, 1, 1);
            this.tlpMain_IOPort.Controls.Add(this.butIO_Vehicle, 2, 1);
            this.tlpMain_IOPort.Controls.Add(this.butIO_InterFace, 3, 1);
            this.tlpMain_IOPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain_IOPort.Location = new System.Drawing.Point(0, 0);
            this.tlpMain_IOPort.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain_IOPort.Name = "tlpMain_IOPort";
            this.tlpMain_IOPort.RowCount = 3;
            this.tlpMain_IOPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpMain_IOPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlpMain_IOPort.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlpMain_IOPort.Size = new System.Drawing.Size(584, 441);
            this.tlpMain_IOPort.TabIndex = 5;
            // 
            // subFormPanel
            // 
            this.tlpMain_IOPort.SetColumnSpan(this.subFormPanel, 4);
            this.subFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subFormPanel.Location = new System.Drawing.Point(3, 68);
            this.subFormPanel.Name = "subFormPanel";
            this.subFormPanel.Size = new System.Drawing.Size(578, 370);
            this.subFormPanel.TabIndex = 222;
            // 
            // cboIOPort
            // 
            this.tlpMain_IOPort.SetColumnSpan(this.cboIOPort, 4);
            this.cboIOPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboIOPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIOPort.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboIOPort.ForeColor = System.Drawing.Color.Blue;
            this.cboIOPort.FormattingEnabled = true;
            this.cboIOPort.Location = new System.Drawing.Point(3, 3);
            this.cboIOPort.Name = "cboIOPort";
            this.cboIOPort.Size = new System.Drawing.Size(578, 28);
            this.cboIOPort.TabIndex = 220;
            this.cboIOPort.SelectedIndexChanged += new System.EventHandler(this.cboIOPort_SelectedIndexChanged);
            // 
            // butIO_State1
            // 
            this.butIO_State1.BackColor = System.Drawing.Color.Aqua;
            this.butIO_State1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_State1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butIO_State1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.butIO_State1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butIO_State1.Location = new System.Drawing.Point(0, 35);
            this.butIO_State1.Margin = new System.Windows.Forms.Padding(0);
            this.butIO_State1.Name = "butIO_State1";
            this.butIO_State1.Size = new System.Drawing.Size(146, 30);
            this.butIO_State1.TabIndex = 36;
            this.butIO_State1.Text = "State 1/2";
            this.butIO_State1.UseVisualStyleBackColor = false;
            this.butIO_State1.Click += new System.EventHandler(this.butIO_State1_Click);
            // 
            // butIO_State2
            // 
            this.butIO_State2.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_State2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_State2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butIO_State2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.butIO_State2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butIO_State2.Location = new System.Drawing.Point(146, 35);
            this.butIO_State2.Margin = new System.Windows.Forms.Padding(0);
            this.butIO_State2.Name = "butIO_State2";
            this.butIO_State2.Size = new System.Drawing.Size(146, 30);
            this.butIO_State2.TabIndex = 36;
            this.butIO_State2.Text = "State 2/2";
            this.butIO_State2.UseVisualStyleBackColor = false;
            this.butIO_State2.Click += new System.EventHandler(this.butIO_State2_Click);
            // 
            // butIO_Vehicle
            // 
            this.butIO_Vehicle.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_Vehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_Vehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butIO_Vehicle.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.butIO_Vehicle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butIO_Vehicle.Location = new System.Drawing.Point(292, 35);
            this.butIO_Vehicle.Margin = new System.Windows.Forms.Padding(0);
            this.butIO_Vehicle.Name = "butIO_Vehicle";
            this.butIO_Vehicle.Size = new System.Drawing.Size(146, 30);
            this.butIO_Vehicle.TabIndex = 36;
            this.butIO_Vehicle.Text = "Vehicle";
            this.butIO_Vehicle.UseVisualStyleBackColor = false;
            this.butIO_Vehicle.Click += new System.EventHandler(this.butIO_Vehicle_Click);
            // 
            // butIO_InterFace
            // 
            this.butIO_InterFace.BackColor = System.Drawing.Color.Gainsboro;
            this.butIO_InterFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIO_InterFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butIO_InterFace.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.butIO_InterFace.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.butIO_InterFace.Location = new System.Drawing.Point(438, 35);
            this.butIO_InterFace.Margin = new System.Windows.Forms.Padding(0);
            this.butIO_InterFace.Name = "butIO_InterFace";
            this.butIO_InterFace.Size = new System.Drawing.Size(146, 30);
            this.butIO_InterFace.TabIndex = 36;
            this.butIO_InterFace.Text = "InterFace";
            this.butIO_InterFace.UseVisualStyleBackColor = false;
            this.butIO_InterFace.Click += new System.EventHandler(this.butIO_InterFace_Click);
            // 
            // IOPortView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpMain_IOPort);
            this.Name = "IOPortView";
            this.Text = "IOPortView";
            this.Load += new System.EventHandler(this.IOPortView_Load);
            this.tlpMain_IOPort.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain_IOPort;
        private System.Windows.Forms.Panel subFormPanel;
        private System.Windows.Forms.ComboBox cboIOPort;
        private System.Windows.Forms.Button butIO_State1;
        private System.Windows.Forms.Button butIO_State2;
        private System.Windows.Forms.Button butIO_Vehicle;
        private System.Windows.Forms.Button butIO_InterFace;
    }
}