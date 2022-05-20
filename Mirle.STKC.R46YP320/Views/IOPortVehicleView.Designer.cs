namespace Mirle.STKC.R46YP320.Views
{
    partial class IOPortVehicleView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOPortVehicleView));
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tlpIO_Vehicle = new System.Windows.Forms.TableLayoutPanel();
            this.label183 = new System.Windows.Forms.Label();
            this.cboVehicle = new System.Windows.Forms.ComboBox();
            this.lblVehicleCSTID = new System.Windows.Forms.Label();
            this.lblVehicleLoaded = new System.Windows.Forms.Label();
            this.lblVehicleHPReturn = new System.Windows.Forms.Label();
            this.lblVehicleHomePosition = new System.Windows.Forms.Label();
            this.lblVehicleError = new System.Windows.Forms.Label();
            this.lblVehicleActive = new System.Windows.Forms.Label();
            this.lblVehicleAuto = new System.Windows.Forms.Label();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label197 = new System.Windows.Forms.Label();
            this.tlpVehicle1 = new System.Windows.Forms.TableLayoutPanel();
            this.label200 = new System.Windows.Forms.Label();
            this.lblVehicle1_REP = new System.Windows.Forms.Label();
            this.tlpVehicle5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicle5_REP = new System.Windows.Forms.Label();
            this.label191 = new System.Windows.Forms.Label();
            this.tlpVehicle2 = new System.Windows.Forms.TableLayoutPanel();
            this.label202 = new System.Windows.Forms.Label();
            this.lblVehicle2_REP = new System.Windows.Forms.Label();
            this.tlpVehicle3 = new System.Windows.Forms.TableLayoutPanel();
            this.label203 = new System.Windows.Forms.Label();
            this.lblVehicle3_REP = new System.Windows.Forms.Label();
            this.tlpVehicle4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehicle4_REP = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblVehiclePosition4 = new System.Windows.Forms.Label();
            this.lblVehiclePosition3 = new System.Windows.Forms.Label();
            this.lblVehiclePosition2 = new System.Windows.Forms.Label();
            this.lblVehiclePosition1 = new System.Windows.Forms.Label();
            this.lblVehiclePosition5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.butIOVehicleRun = new System.Windows.Forms.Button();
            this.butIOVehicleReturnHome = new System.Windows.Forms.Button();
            this.butIOVehicleFaultReset = new System.Windows.Forms.Button();
            this.tlpIO_Vehicle.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tlpVehicle1.SuspendLayout();
            this.tlpVehicle5.SuspendLayout();
            this.tlpVehicle2.SuspendLayout();
            this.tlpVehicle3.SuspendLayout();
            this.tlpVehicle4.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tlpIO_Vehicle
            // 
            this.tlpIO_Vehicle.ColumnCount = 6;
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpIO_Vehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tlpIO_Vehicle.Controls.Add(this.label183, 1, 4);
            this.tlpIO_Vehicle.Controls.Add(this.cboVehicle, 1, 2);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleCSTID, 2, 4);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleLoaded, 2, 5);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleHPReturn, 2, 7);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleHomePosition, 1, 7);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleError, 2, 6);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleActive, 1, 6);
            this.tlpIO_Vehicle.Controls.Add(this.lblVehicleAuto, 1, 5);
            this.tlpIO_Vehicle.Controls.Add(this.tableLayoutPanel12, 0, 0);
            this.tlpIO_Vehicle.Controls.Add(this.tableLayoutPanel11, 1, 8);
            this.tlpIO_Vehicle.Controls.Add(this.tableLayoutPanel19, 4, 2);
            this.tlpIO_Vehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIO_Vehicle.Font = new System.Drawing.Font("Microsoft JhengHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tlpIO_Vehicle.Location = new System.Drawing.Point(0, 0);
            this.tlpIO_Vehicle.Name = "tlpIO_Vehicle";
            this.tlpIO_Vehicle.RowCount = 10;
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tlpIO_Vehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tlpIO_Vehicle.Size = new System.Drawing.Size(584, 441);
            this.tlpIO_Vehicle.TabIndex = 221;
            // 
            // label183
            // 
            this.label183.BackColor = System.Drawing.SystemColors.Control;
            this.label183.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label183.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label183.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label183.ForeColor = System.Drawing.Color.Blue;
            this.label183.Location = new System.Drawing.Point(24, 167);
            this.label183.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(173, 35);
            this.label183.TabIndex = 224;
            this.label183.Text = "CSTID On Vehicle";
            this.label183.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboVehicle
            // 
            this.tlpIO_Vehicle.SetColumnSpan(this.cboVehicle, 2);
            this.cboVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehicle.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboVehicle.FormattingEnabled = true;
            this.cboVehicle.Location = new System.Drawing.Point(26, 91);
            this.cboVehicle.Name = "cboVehicle";
            this.cboVehicle.Size = new System.Drawing.Size(344, 28);
            this.cboVehicle.TabIndex = 218;
            this.cboVehicle.SelectedIndexChanged += new System.EventHandler(this.cboVehicle_SelectedIndexChanged);
            // 
            // lblVehicleCSTID
            // 
            this.lblVehicleCSTID.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicleCSTID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleCSTID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleCSTID.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleCSTID.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleCSTID.Location = new System.Drawing.Point(199, 167);
            this.lblVehicleCSTID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblVehicleCSTID.Name = "lblVehicleCSTID";
            this.lblVehicleCSTID.Size = new System.Drawing.Size(173, 35);
            this.lblVehicleCSTID.TabIndex = 226;
            this.lblVehicleCSTID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVehicleLoaded
            // 
            this.lblVehicleLoaded.BackColor = System.Drawing.Color.White;
            this.lblVehicleLoaded.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleLoaded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleLoaded.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleLoaded.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleLoaded.Location = new System.Drawing.Point(200, 203);
            this.lblVehicleLoaded.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleLoaded.Name = "lblVehicleLoaded";
            this.lblVehicleLoaded.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleLoaded.TabIndex = 167;
            this.lblVehicleLoaded.Text = "Load Presence";
            // 
            // lblVehicleHPReturn
            // 
            this.lblVehicleHPReturn.BackColor = System.Drawing.Color.White;
            this.lblVehicleHPReturn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleHPReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleHPReturn.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleHPReturn.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleHPReturn.Location = new System.Drawing.Point(200, 273);
            this.lblVehicleHPReturn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleHPReturn.Name = "lblVehicleHPReturn";
            this.lblVehicleHPReturn.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleHPReturn.TabIndex = 199;
            this.lblVehicleHPReturn.Text = "HP Return";
            // 
            // lblVehicleHomePosition
            // 
            this.lblVehicleHomePosition.BackColor = System.Drawing.Color.White;
            this.lblVehicleHomePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleHomePosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleHomePosition.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleHomePosition.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleHomePosition.Location = new System.Drawing.Point(25, 273);
            this.lblVehicleHomePosition.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleHomePosition.Name = "lblVehicleHomePosition";
            this.lblVehicleHomePosition.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleHomePosition.TabIndex = 195;
            this.lblVehicleHomePosition.Text = "Home Position";
            // 
            // lblVehicleError
            // 
            this.lblVehicleError.BackColor = System.Drawing.Color.White;
            this.lblVehicleError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleError.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleError.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleError.Location = new System.Drawing.Point(200, 238);
            this.lblVehicleError.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleError.Name = "lblVehicleError";
            this.lblVehicleError.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleError.TabIndex = 169;
            this.lblVehicleError.Text = "Error";
            // 
            // lblVehicleActive
            // 
            this.lblVehicleActive.BackColor = System.Drawing.Color.White;
            this.lblVehicleActive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleActive.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleActive.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleActive.Location = new System.Drawing.Point(25, 238);
            this.lblVehicleActive.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleActive.Name = "lblVehicleActive";
            this.lblVehicleActive.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleActive.TabIndex = 167;
            this.lblVehicleActive.Text = "Active";
            // 
            // lblVehicleAuto
            // 
            this.lblVehicleAuto.BackColor = System.Drawing.Color.White;
            this.lblVehicleAuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicleAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicleAuto.Font = new System.Drawing.Font("Microsoft JhengHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicleAuto.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicleAuto.Location = new System.Drawing.Point(25, 203);
            this.lblVehicleAuto.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblVehicleAuto.Name = "lblVehicleAuto";
            this.lblVehicleAuto.Size = new System.Drawing.Size(171, 33);
            this.lblVehicleAuto.TabIndex = 203;
            this.lblVehicleAuto.Text = "Auto";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel12.ColumnCount = 6;
            this.tlpIO_Vehicle.SetColumnSpan(this.tableLayoutPanel12, 6);
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel12.Controls.Add(this.label197, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.tlpVehicle1, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.tlpVehicle5, 5, 0);
            this.tableLayoutPanel12.Controls.Add(this.tlpVehicle2, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.tlpVehicle3, 3, 0);
            this.tableLayoutPanel12.Controls.Add(this.tlpVehicle4, 4, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(584, 66);
            this.tableLayoutPanel12.TabIndex = 219;
            // 
            // label197
            // 
            this.label197.BackColor = System.Drawing.Color.Black;
            this.label197.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label197.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label197.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label197.ForeColor = System.Drawing.Color.White;
            this.label197.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label197.Location = new System.Drawing.Point(4, 3);
            this.label197.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.label197.Name = "label197";
            this.tableLayoutPanel12.SetRowSpan(this.label197, 2);
            this.label197.Size = new System.Drawing.Size(138, 60);
            this.label197.TabIndex = 205;
            this.label197.Text = "Vehicle Real Time Position (cm)";
            this.label197.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVehicle1
            // 
            this.tlpVehicle1.ColumnCount = 1;
            this.tlpVehicle1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicle1.Controls.Add(this.label200, 0, 0);
            this.tlpVehicle1.Controls.Add(this.lblVehicle1_REP, 0, 1);
            this.tlpVehicle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVehicle1.Location = new System.Drawing.Point(146, 2);
            this.tlpVehicle1.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicle1.Name = "tlpVehicle1";
            this.tlpVehicle1.RowCount = 2;
            this.tableLayoutPanel12.SetRowSpan(this.tlpVehicle1, 2);
            this.tlpVehicle1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle1.Size = new System.Drawing.Size(85, 62);
            this.tlpVehicle1.TabIndex = 221;
            // 
            // label200
            // 
            this.label200.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label200.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label200.ForeColor = System.Drawing.Color.Blue;
            this.label200.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label200.Location = new System.Drawing.Point(3, 0);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(79, 31);
            this.label200.TabIndex = 205;
            this.label200.Text = "Vehicle.1";
            this.label200.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehicle1_REP
            // 
            this.lblVehicle1_REP.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicle1_REP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicle1_REP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle1_REP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicle1_REP.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle1_REP.Location = new System.Drawing.Point(2, 31);
            this.lblVehicle1_REP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblVehicle1_REP.Name = "lblVehicle1_REP";
            this.lblVehicle1_REP.Size = new System.Drawing.Size(81, 28);
            this.lblVehicle1_REP.TabIndex = 208;
            this.lblVehicle1_REP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVehicle5
            // 
            this.tlpVehicle5.ColumnCount = 1;
            this.tlpVehicle5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicle5.Controls.Add(this.lblVehicle5_REP, 0, 1);
            this.tlpVehicle5.Controls.Add(this.label191, 0, 0);
            this.tlpVehicle5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVehicle5.Location = new System.Drawing.Point(494, 2);
            this.tlpVehicle5.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicle5.Name = "tlpVehicle5";
            this.tlpVehicle5.RowCount = 2;
            this.tableLayoutPanel12.SetRowSpan(this.tlpVehicle5, 2);
            this.tlpVehicle5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle5.Size = new System.Drawing.Size(88, 62);
            this.tlpVehicle5.TabIndex = 221;
            // 
            // lblVehicle5_REP
            // 
            this.lblVehicle5_REP.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicle5_REP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicle5_REP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle5_REP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicle5_REP.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle5_REP.Location = new System.Drawing.Point(2, 31);
            this.lblVehicle5_REP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblVehicle5_REP.Name = "lblVehicle5_REP";
            this.lblVehicle5_REP.Size = new System.Drawing.Size(84, 28);
            this.lblVehicle5_REP.TabIndex = 208;
            this.lblVehicle5_REP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label191
            // 
            this.label191.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label191.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label191.ForeColor = System.Drawing.Color.Blue;
            this.label191.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label191.Location = new System.Drawing.Point(3, 0);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(82, 31);
            this.label191.TabIndex = 205;
            this.label191.Text = "Vehicle.5";
            this.label191.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVehicle2
            // 
            this.tlpVehicle2.ColumnCount = 1;
            this.tlpVehicle2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicle2.Controls.Add(this.label202, 0, 0);
            this.tlpVehicle2.Controls.Add(this.lblVehicle2_REP, 0, 1);
            this.tlpVehicle2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVehicle2.Location = new System.Drawing.Point(233, 2);
            this.tlpVehicle2.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicle2.Name = "tlpVehicle2";
            this.tlpVehicle2.RowCount = 2;
            this.tableLayoutPanel12.SetRowSpan(this.tlpVehicle2, 2);
            this.tlpVehicle2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle2.Size = new System.Drawing.Size(85, 62);
            this.tlpVehicle2.TabIndex = 221;
            // 
            // label202
            // 
            this.label202.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label202.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label202.ForeColor = System.Drawing.Color.Blue;
            this.label202.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label202.Location = new System.Drawing.Point(3, 0);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(79, 31);
            this.label202.TabIndex = 205;
            this.label202.Text = "Vehicle.2";
            this.label202.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehicle2_REP
            // 
            this.lblVehicle2_REP.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicle2_REP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicle2_REP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle2_REP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicle2_REP.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle2_REP.Location = new System.Drawing.Point(2, 31);
            this.lblVehicle2_REP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblVehicle2_REP.Name = "lblVehicle2_REP";
            this.lblVehicle2_REP.Size = new System.Drawing.Size(81, 28);
            this.lblVehicle2_REP.TabIndex = 208;
            this.lblVehicle2_REP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVehicle3
            // 
            this.tlpVehicle3.ColumnCount = 1;
            this.tlpVehicle3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicle3.Controls.Add(this.label203, 0, 0);
            this.tlpVehicle3.Controls.Add(this.lblVehicle3_REP, 0, 1);
            this.tlpVehicle3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVehicle3.Location = new System.Drawing.Point(320, 2);
            this.tlpVehicle3.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicle3.Name = "tlpVehicle3";
            this.tlpVehicle3.RowCount = 2;
            this.tableLayoutPanel12.SetRowSpan(this.tlpVehicle3, 2);
            this.tlpVehicle3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle3.Size = new System.Drawing.Size(85, 62);
            this.tlpVehicle3.TabIndex = 221;
            // 
            // label203
            // 
            this.label203.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label203.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label203.ForeColor = System.Drawing.Color.Blue;
            this.label203.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label203.Location = new System.Drawing.Point(3, 0);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(79, 31);
            this.label203.TabIndex = 205;
            this.label203.Text = "Vehicle.3";
            this.label203.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehicle3_REP
            // 
            this.lblVehicle3_REP.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicle3_REP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicle3_REP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle3_REP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicle3_REP.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle3_REP.Location = new System.Drawing.Point(2, 31);
            this.lblVehicle3_REP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblVehicle3_REP.Name = "lblVehicle3_REP";
            this.lblVehicle3_REP.Size = new System.Drawing.Size(81, 28);
            this.lblVehicle3_REP.TabIndex = 208;
            this.lblVehicle3_REP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpVehicle4
            // 
            this.tlpVehicle4.ColumnCount = 1;
            this.tlpVehicle4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVehicle4.Controls.Add(this.lblVehicle4_REP, 0, 1);
            this.tlpVehicle4.Controls.Add(this.label195, 0, 0);
            this.tlpVehicle4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVehicle4.Location = new System.Drawing.Point(407, 2);
            this.tlpVehicle4.Margin = new System.Windows.Forms.Padding(0);
            this.tlpVehicle4.Name = "tlpVehicle4";
            this.tlpVehicle4.RowCount = 2;
            this.tableLayoutPanel12.SetRowSpan(this.tlpVehicle4, 2);
            this.tlpVehicle4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpVehicle4.Size = new System.Drawing.Size(85, 62);
            this.tlpVehicle4.TabIndex = 221;
            // 
            // lblVehicle4_REP
            // 
            this.lblVehicle4_REP.BackColor = System.Drawing.SystemColors.Info;
            this.lblVehicle4_REP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehicle4_REP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehicle4_REP.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehicle4_REP.ForeColor = System.Drawing.Color.Blue;
            this.lblVehicle4_REP.Location = new System.Drawing.Point(2, 31);
            this.lblVehicle4_REP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 3);
            this.lblVehicle4_REP.Name = "lblVehicle4_REP";
            this.lblVehicle4_REP.Size = new System.Drawing.Size(81, 28);
            this.lblVehicle4_REP.TabIndex = 208;
            this.lblVehicle4_REP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label195
            // 
            this.label195.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label195.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label195.ForeColor = System.Drawing.Color.Blue;
            this.label195.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label195.Location = new System.Drawing.Point(3, 0);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(79, 31);
            this.label195.TabIndex = 205;
            this.label195.Text = "Vehicle.4";
            this.label195.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel11.ColumnCount = 5;
            this.tlpIO_Vehicle.SetColumnSpan(this.tableLayoutPanel11, 2);
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel11.Controls.Add(this.lblVehiclePosition4, 3, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblVehiclePosition3, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblVehiclePosition2, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblVehiclePosition1, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblVehiclePosition5, 4, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(23, 307);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(350, 35);
            this.tableLayoutPanel11.TabIndex = 219;
            // 
            // lblVehiclePosition4
            // 
            this.lblVehiclePosition4.BackColor = System.Drawing.Color.White;
            this.lblVehiclePosition4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiclePosition4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePosition4.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehiclePosition4.ForeColor = System.Drawing.Color.Blue;
            this.lblVehiclePosition4.Location = new System.Drawing.Point(209, 2);
            this.lblVehiclePosition4.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehiclePosition4.Name = "lblVehiclePosition4";
            this.lblVehiclePosition4.Size = new System.Drawing.Size(67, 31);
            this.lblVehiclePosition4.TabIndex = 208;
            this.lblVehiclePosition4.Text = "Pos 4";
            this.lblVehiclePosition4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehiclePosition3
            // 
            this.lblVehiclePosition3.BackColor = System.Drawing.Color.White;
            this.lblVehiclePosition3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiclePosition3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePosition3.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehiclePosition3.ForeColor = System.Drawing.Color.Blue;
            this.lblVehiclePosition3.Location = new System.Drawing.Point(140, 2);
            this.lblVehiclePosition3.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehiclePosition3.Name = "lblVehiclePosition3";
            this.lblVehiclePosition3.Size = new System.Drawing.Size(67, 31);
            this.lblVehiclePosition3.TabIndex = 208;
            this.lblVehiclePosition3.Text = "Pos 3";
            this.lblVehiclePosition3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehiclePosition2
            // 
            this.lblVehiclePosition2.BackColor = System.Drawing.Color.White;
            this.lblVehiclePosition2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiclePosition2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePosition2.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehiclePosition2.ForeColor = System.Drawing.Color.Blue;
            this.lblVehiclePosition2.Location = new System.Drawing.Point(71, 2);
            this.lblVehiclePosition2.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehiclePosition2.Name = "lblVehiclePosition2";
            this.lblVehiclePosition2.Size = new System.Drawing.Size(67, 31);
            this.lblVehiclePosition2.TabIndex = 198;
            this.lblVehiclePosition2.Text = "Pos 2";
            this.lblVehiclePosition2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehiclePosition1
            // 
            this.lblVehiclePosition1.BackColor = System.Drawing.Color.White;
            this.lblVehiclePosition1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiclePosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePosition1.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehiclePosition1.ForeColor = System.Drawing.Color.Blue;
            this.lblVehiclePosition1.Location = new System.Drawing.Point(2, 2);
            this.lblVehiclePosition1.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehiclePosition1.Name = "lblVehiclePosition1";
            this.lblVehiclePosition1.Size = new System.Drawing.Size(67, 31);
            this.lblVehiclePosition1.TabIndex = 167;
            this.lblVehiclePosition1.Text = "Pos 1";
            this.lblVehiclePosition1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehiclePosition5
            // 
            this.lblVehiclePosition5.BackColor = System.Drawing.Color.White;
            this.lblVehiclePosition5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVehiclePosition5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVehiclePosition5.Font = new System.Drawing.Font("Microsoft JhengHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVehiclePosition5.ForeColor = System.Drawing.Color.Blue;
            this.lblVehiclePosition5.Location = new System.Drawing.Point(278, 2);
            this.lblVehiclePosition5.Margin = new System.Windows.Forms.Padding(0);
            this.lblVehiclePosition5.Name = "lblVehiclePosition5";
            this.lblVehiclePosition5.Size = new System.Drawing.Size(70, 31);
            this.lblVehiclePosition5.TabIndex = 208;
            this.lblVehiclePosition5.Text = "Pos 5";
            this.lblVehiclePosition5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 1;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel19.Controls.Add(this.butIOVehicleRun, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.butIOVehicleReturnHome, 0, 2);
            this.tableLayoutPanel19.Controls.Add(this.butIOVehicleFaultReset, 0, 1);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(399, 91);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 3;
            this.tlpIO_Vehicle.SetRowSpan(this.tableLayoutPanel19, 5);
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(157, 178);
            this.tableLayoutPanel19.TabIndex = 227;
            // 
            // butIOVehicleRun
            // 
            this.butIOVehicleRun.BackColor = System.Drawing.Color.Gainsboro;
            this.butIOVehicleRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIOVehicleRun.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIOVehicleRun.Image = ((System.Drawing.Image)(resources.GetObject("butIOVehicleRun.Image")));
            this.butIOVehicleRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butIOVehicleRun.Location = new System.Drawing.Point(2, 2);
            this.butIOVehicleRun.Margin = new System.Windows.Forms.Padding(2);
            this.butIOVehicleRun.Name = "butIOVehicleRun";
            this.butIOVehicleRun.Size = new System.Drawing.Size(153, 55);
            this.butIOVehicleRun.TabIndex = 8;
            this.butIOVehicleRun.Text = "Run";
            this.butIOVehicleRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIOVehicleRun.UseVisualStyleBackColor = false;
            this.butIOVehicleRun.Click += new System.EventHandler(this.butIOVehicleRun_Click);
            // 
            // butIOVehicleReturnHome
            // 
            this.butIOVehicleReturnHome.BackColor = System.Drawing.Color.Gainsboro;
            this.butIOVehicleReturnHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIOVehicleReturnHome.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIOVehicleReturnHome.Image = ((System.Drawing.Image)(resources.GetObject("butIOVehicleReturnHome.Image")));
            this.butIOVehicleReturnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butIOVehicleReturnHome.Location = new System.Drawing.Point(2, 120);
            this.butIOVehicleReturnHome.Margin = new System.Windows.Forms.Padding(2);
            this.butIOVehicleReturnHome.Name = "butIOVehicleReturnHome";
            this.butIOVehicleReturnHome.Size = new System.Drawing.Size(153, 56);
            this.butIOVehicleReturnHome.TabIndex = 27;
            this.butIOVehicleReturnHome.Text = "Return Home";
            this.butIOVehicleReturnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIOVehicleReturnHome.UseVisualStyleBackColor = false;
            this.butIOVehicleReturnHome.Click += new System.EventHandler(this.butIOVehicleReturnHome_Click);
            // 
            // butIOVehicleFaultReset
            // 
            this.butIOVehicleFaultReset.BackColor = System.Drawing.Color.Gainsboro;
            this.butIOVehicleFaultReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.butIOVehicleFaultReset.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIOVehicleFaultReset.Image = ((System.Drawing.Image)(resources.GetObject("butIOVehicleFaultReset.Image")));
            this.butIOVehicleFaultReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butIOVehicleFaultReset.Location = new System.Drawing.Point(1, 60);
            this.butIOVehicleFaultReset.Margin = new System.Windows.Forms.Padding(1);
            this.butIOVehicleFaultReset.Name = "butIOVehicleFaultReset";
            this.butIOVehicleFaultReset.Size = new System.Drawing.Size(155, 57);
            this.butIOVehicleFaultReset.TabIndex = 28;
            this.butIOVehicleFaultReset.Text = "Error Reset";
            this.butIOVehicleFaultReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.butIOVehicleFaultReset.UseVisualStyleBackColor = false;
            this.butIOVehicleFaultReset.Click += new System.EventHandler(this.butIOVehicleFaultReset_Click);
            // 
            // IOPortVehicleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.tlpIO_Vehicle);
            this.Name = "IOPortVehicleView";
            this.Text = "IOPortVehicleView";
            this.Load += new System.EventHandler(this.IOPortVehicleView_Load);
            this.VisibleChanged += new System.EventHandler(this.IOPortVehicleView_VisibleChanged);
            this.tlpIO_Vehicle.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tlpVehicle1.ResumeLayout(false);
            this.tlpVehicle5.ResumeLayout(false);
            this.tlpVehicle2.ResumeLayout(false);
            this.tlpVehicle3.ResumeLayout(false);
            this.tlpVehicle4.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TableLayoutPanel tlpIO_Vehicle;
        internal System.Windows.Forms.Label label183;
        private System.Windows.Forms.ComboBox cboVehicle;
        internal System.Windows.Forms.Label lblVehicleCSTID;
        internal System.Windows.Forms.Label lblVehicleLoaded;
        internal System.Windows.Forms.Label lblVehicleHPReturn;
        internal System.Windows.Forms.Label lblVehicleHomePosition;
        internal System.Windows.Forms.Label lblVehicleError;
        internal System.Windows.Forms.Label lblVehicleActive;
        internal System.Windows.Forms.Label lblVehicleAuto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        internal System.Windows.Forms.Label label197;
        private System.Windows.Forms.TableLayoutPanel tlpVehicle1;
        internal System.Windows.Forms.Label label200;
        internal System.Windows.Forms.Label lblVehicle1_REP;
        private System.Windows.Forms.TableLayoutPanel tlpVehicle5;
        internal System.Windows.Forms.Label lblVehicle5_REP;
        internal System.Windows.Forms.Label label191;
        private System.Windows.Forms.TableLayoutPanel tlpVehicle2;
        internal System.Windows.Forms.Label label202;
        internal System.Windows.Forms.Label lblVehicle2_REP;
        private System.Windows.Forms.TableLayoutPanel tlpVehicle3;
        internal System.Windows.Forms.Label label203;
        internal System.Windows.Forms.Label lblVehicle3_REP;
        private System.Windows.Forms.TableLayoutPanel tlpVehicle4;
        internal System.Windows.Forms.Label lblVehicle4_REP;
        internal System.Windows.Forms.Label label195;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        internal System.Windows.Forms.Label lblVehiclePosition4;
        internal System.Windows.Forms.Label lblVehiclePosition3;
        internal System.Windows.Forms.Label lblVehiclePosition2;
        internal System.Windows.Forms.Label lblVehiclePosition1;
        internal System.Windows.Forms.Label lblVehiclePosition5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.Button butIOVehicleRun;
        private System.Windows.Forms.Button butIOVehicleReturnHome;
        private System.Windows.Forms.Button butIOVehicleFaultReset;
    }
}