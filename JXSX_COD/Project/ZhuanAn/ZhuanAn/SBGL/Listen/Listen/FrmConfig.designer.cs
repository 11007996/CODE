namespace Listen
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.gbSerailPort = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labSerailListenFlag = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.chkSerailListenFlag = new System.Windows.Forms.CheckBox();
            this.labParity = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.labPortName = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.labStopBits = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.labDataBits = new System.Windows.Forms.Label();
            this.gbTcp = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labTcpListenFlag = new System.Windows.Forms.Label();
            this.labUnit = new System.Windows.Forms.Label();
            this.nudReceiveTimeout = new System.Windows.Forms.NumericUpDown();
            this.cbListenIP = new System.Windows.Forms.ComboBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.btnCheckPort = new System.Windows.Forms.Button();
            this.labLocalIPTitle = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.labReceiveTimeout = new System.Windows.Forms.Label();
            this.chkTcpListenFlag = new System.Windows.Forms.CheckBox();
            this.labTip = new System.Windows.Forms.Label();
            this.gpOperate = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDayMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.chkMonthMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.chkWeekMaintenanceFlag = new System.Windows.Forms.CheckBox();
            this.gpCode = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbGoodSize = new System.Windows.Forms.TextBox();
            this.tbNGSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbWarnCodeSize = new System.Windows.Forms.TextBox();
            this.tbSuffixSize = new System.Windows.Forms.TextBox();
            this.tbWarnDescSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRemarkSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLineSize = new System.Windows.Forms.TextBox();
            this.tbRunStateSize = new System.Windows.Forms.TextBox();
            this.tbOperateSize = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbMachineSize = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbPrefixSize = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbPrefix = new System.Windows.Forms.TextBox();
            this.tbSuffix = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.gbSerailPort.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.gbTcp.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceiveTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.gpOperate.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gpCode.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSerailPort
            // 
            this.gbSerailPort.AutoSize = true;
            this.gbSerailPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gbSerailPort.Controls.Add(this.tableLayoutPanel5);
            this.gbSerailPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSerailPort.Location = new System.Drawing.Point(3, 481);
            this.gbSerailPort.Name = "gbSerailPort";
            this.gbSerailPort.Size = new System.Drawing.Size(301, 140);
            this.gbSerailPort.TabIndex = 16;
            this.gbSerailPort.TabStop = false;
            this.gbSerailPort.Text = "串口通信";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.10169F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.44068F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.35593F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.10169F));
            this.tableLayoutPanel5.Controls.Add(this.labSerailListenFlag, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cmbParity, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.chkSerailListenFlag, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labParity, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.cmbStopBits, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.labPortName, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tbDataBits, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.labStopBits, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.cmbBaudRate, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.cmbPortName, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.labBaudRate, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.labDataBits, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(295, 120);
            this.tableLayoutPanel5.TabIndex = 16;
            // 
            // labSerailListenFlag
            // 
            this.labSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSerailListenFlag.AutoSize = true;
            this.labSerailListenFlag.Location = new System.Drawing.Point(20, 9);
            this.labSerailListenFlag.Name = "labSerailListenFlag";
            this.labSerailListenFlag.Size = new System.Drawing.Size(53, 12);
            this.labSerailListenFlag.TabIndex = 11;
            this.labSerailListenFlag.Text = "串口监听";
            this.labSerailListenFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbParity
            // 
            this.cmbParity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(76, 95);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(0);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(73, 20);
            this.cmbParity.TabIndex = 10;
            // 
            // chkSerailListenFlag
            // 
            this.chkSerailListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSerailListenFlag.AutoSize = true;
            this.chkSerailListenFlag.Location = new System.Drawing.Point(79, 8);
            this.chkSerailListenFlag.Name = "chkSerailListenFlag";
            this.chkSerailListenFlag.Size = new System.Drawing.Size(15, 14);
            this.chkSerailListenFlag.TabIndex = 15;
            // 
            // labParity
            // 
            this.labParity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labParity.AutoSize = true;
            this.labParity.Location = new System.Drawing.Point(20, 99);
            this.labParity.Name = "labParity";
            this.labParity.Size = new System.Drawing.Size(53, 12);
            this.labParity.TabIndex = 4;
            this.labParity.Text = "奇偶校验";
            this.labParity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Location = new System.Drawing.Point(216, 65);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(0);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(75, 20);
            this.cmbStopBits.TabIndex = 9;
            // 
            // labPortName
            // 
            this.labPortName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPortName.AutoSize = true;
            this.labPortName.Location = new System.Drawing.Point(20, 39);
            this.labPortName.Name = "labPortName";
            this.labPortName.Size = new System.Drawing.Size(53, 12);
            this.labPortName.TabIndex = 0;
            this.labPortName.Text = "串口名称";
            this.labPortName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDataBits
            // 
            this.tbDataBits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbDataBits.Location = new System.Drawing.Point(76, 64);
            this.tbDataBits.Margin = new System.Windows.Forms.Padding(0);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(73, 21);
            this.tbDataBits.TabIndex = 5;
            // 
            // labStopBits
            // 
            this.labStopBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStopBits.AutoSize = true;
            this.labStopBits.Location = new System.Drawing.Point(172, 69);
            this.labStopBits.Name = "labStopBits";
            this.labStopBits.Size = new System.Drawing.Size(41, 12);
            this.labStopBits.TabIndex = 3;
            this.labStopBits.Text = "停止位";
            this.labStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(216, 35);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(0);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(75, 20);
            this.cmbBaudRate.TabIndex = 8;
            // 
            // cmbPortName
            // 
            this.cmbPortName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cmbPortName.Location = new System.Drawing.Point(76, 35);
            this.cmbPortName.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(73, 20);
            this.cmbPortName.TabIndex = 6;
            // 
            // labBaudRate
            // 
            this.labBaudRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(172, 39);
            this.labBaudRate.Name = "labBaudRate";
            this.labBaudRate.Size = new System.Drawing.Size(41, 12);
            this.labBaudRate.TabIndex = 1;
            this.labBaudRate.Text = "波特率";
            this.labBaudRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labDataBits
            // 
            this.labDataBits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDataBits.AutoSize = true;
            this.labDataBits.Location = new System.Drawing.Point(32, 69);
            this.labDataBits.Name = "labDataBits";
            this.labDataBits.Size = new System.Drawing.Size(41, 12);
            this.labDataBits.TabIndex = 2;
            this.labDataBits.Text = "数据位";
            this.labDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbTcp
            // 
            this.gbTcp.AutoSize = true;
            this.gbTcp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gbTcp.Controls.Add(this.tableLayoutPanel4);
            this.gbTcp.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTcp.Location = new System.Drawing.Point(3, 335);
            this.gbTcp.Name = "gbTcp";
            this.gbTcp.Size = new System.Drawing.Size(301, 140);
            this.gbTcp.TabIndex = 15;
            this.gbTcp.TabStop = false;
            this.gbTcp.Text = "TCP通信";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.44068F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.22034F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.labTcpListenFlag, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.labUnit, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.nudReceiveTimeout, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.cbListenIP, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.nudPort, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnCheckPort, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.labLocalIPTitle, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labPort, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.labReceiveTimeout, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.chkTcpListenFlag, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(295, 120);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // labTcpListenFlag
            // 
            this.labTcpListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTcpListenFlag.AutoSize = true;
            this.labTcpListenFlag.Location = new System.Drawing.Point(28, 9);
            this.labTcpListenFlag.Name = "labTcpListenFlag";
            this.labTcpListenFlag.Size = new System.Drawing.Size(47, 12);
            this.labTcpListenFlag.TabIndex = 0;
            this.labTcpListenFlag.Text = "TCP监听";
            this.labTcpListenFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labUnit
            // 
            this.labUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labUnit.AutoSize = true;
            this.labUnit.Location = new System.Drawing.Point(238, 99);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(29, 12);
            this.labUnit.TabIndex = 6;
            this.labUnit.Text = "(秒)";
            // 
            // nudReceiveTimeout
            // 
            this.nudReceiveTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudReceiveTimeout.Location = new System.Drawing.Point(78, 94);
            this.nudReceiveTimeout.Margin = new System.Windows.Forms.Padding(0);
            this.nudReceiveTimeout.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudReceiveTimeout.Name = "nudReceiveTimeout";
            this.nudReceiveTimeout.Size = new System.Drawing.Size(147, 21);
            this.nudReceiveTimeout.TabIndex = 5;
            this.nudReceiveTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // cbListenIP
            // 
            this.cbListenIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbListenIP.FormattingEnabled = true;
            this.cbListenIP.Location = new System.Drawing.Point(78, 35);
            this.cbListenIP.Margin = new System.Windows.Forms.Padding(0);
            this.cbListenIP.Name = "cbListenIP";
            this.cbListenIP.Size = new System.Drawing.Size(147, 20);
            this.cbListenIP.TabIndex = 10;
            // 
            // nudPort
            // 
            this.nudPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPort.Location = new System.Drawing.Point(78, 64);
            this.nudPort.Margin = new System.Windows.Forms.Padding(0);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(147, 21);
            this.nudPort.TabIndex = 10;
            this.nudPort.Value = new decimal(new int[] {
            10109,
            0,
            0,
            0});
            // 
            // btnCheckPort
            // 
            this.btnCheckPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCheckPort.Location = new System.Drawing.Point(235, 63);
            this.btnCheckPort.Margin = new System.Windows.Forms.Padding(0);
            this.btnCheckPort.Name = "btnCheckPort";
            this.btnCheckPort.Size = new System.Drawing.Size(51, 23);
            this.btnCheckPort.TabIndex = 13;
            this.btnCheckPort.Text = "检查";
            this.btnCheckPort.UseVisualStyleBackColor = true;
            this.btnCheckPort.Click += new System.EventHandler(this.btnCheckPort_Click);
            // 
            // labLocalIPTitle
            // 
            this.labLocalIPTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLocalIPTitle.AutoSize = true;
            this.labLocalIPTitle.Location = new System.Drawing.Point(34, 39);
            this.labLocalIPTitle.Name = "labLocalIPTitle";
            this.labLocalIPTitle.Size = new System.Drawing.Size(41, 12);
            this.labLocalIPTitle.TabIndex = 11;
            this.labLocalIPTitle.Text = "监听IP";
            this.labLocalIPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labPort
            // 
            this.labPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPort.AutoSize = true;
            this.labPort.Location = new System.Drawing.Point(46, 69);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(29, 12);
            this.labPort.TabIndex = 2;
            this.labPort.Text = "端口";
            this.labPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labReceiveTimeout
            // 
            this.labReceiveTimeout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labReceiveTimeout.AutoSize = true;
            this.labReceiveTimeout.Location = new System.Drawing.Point(22, 99);
            this.labReceiveTimeout.Name = "labReceiveTimeout";
            this.labReceiveTimeout.Size = new System.Drawing.Size(53, 12);
            this.labReceiveTimeout.TabIndex = 4;
            this.labReceiveTimeout.Text = "接收超时";
            this.labReceiveTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkTcpListenFlag
            // 
            this.chkTcpListenFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkTcpListenFlag.AutoSize = true;
            this.chkTcpListenFlag.Location = new System.Drawing.Point(81, 8);
            this.chkTcpListenFlag.Name = "chkTcpListenFlag";
            this.chkTcpListenFlag.Size = new System.Drawing.Size(15, 14);
            this.chkTcpListenFlag.TabIndex = 14;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.ForeColor = System.Drawing.Color.Red;
            this.labTip.Location = new System.Drawing.Point(3, 624);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(167, 12);
            this.labTip.TabIndex = 9;
            this.labTip.Text = "提示:设置后需要重启应用生效";
            // 
            // gpOperate
            // 
            this.gpOperate.AutoSize = true;
            this.gpOperate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gpOperate.Controls.Add(this.tableLayoutPanel2);
            this.gpOperate.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpOperate.Location = new System.Drawing.Point(3, 3);
            this.gpOperate.Name = "gpOperate";
            this.gpOperate.Padding = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.gpOperate.Size = new System.Drawing.Size(301, 110);
            this.gpOperate.TabIndex = 17;
            this.gpOperate.TabStop = false;
            this.gpOperate.Text = "保养操作";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.chkDayMaintenanceFlag, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkMonthMaintenanceFlag, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.chkWeekMaintenanceFlag, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(288, 90);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // chkDayMaintenanceFlag
            // 
            this.chkDayMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkDayMaintenanceFlag.AutoSize = true;
            this.chkDayMaintenanceFlag.Location = new System.Drawing.Point(3, 7);
            this.chkDayMaintenanceFlag.Name = "chkDayMaintenanceFlag";
            this.chkDayMaintenanceFlag.Size = new System.Drawing.Size(132, 16);
            this.chkDayMaintenanceFlag.TabIndex = 5;
            this.chkDayMaintenanceFlag.Text = "【日】保养检查开关";
            this.chkDayMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // chkMonthMaintenanceFlag
            // 
            this.chkMonthMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkMonthMaintenanceFlag.AutoSize = true;
            this.chkMonthMaintenanceFlag.Location = new System.Drawing.Point(3, 67);
            this.chkMonthMaintenanceFlag.Name = "chkMonthMaintenanceFlag";
            this.chkMonthMaintenanceFlag.Size = new System.Drawing.Size(132, 16);
            this.chkMonthMaintenanceFlag.TabIndex = 7;
            this.chkMonthMaintenanceFlag.Text = "【月】保养检查开关";
            this.chkMonthMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // chkWeekMaintenanceFlag
            // 
            this.chkWeekMaintenanceFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkWeekMaintenanceFlag.AutoSize = true;
            this.chkWeekMaintenanceFlag.Location = new System.Drawing.Point(3, 37);
            this.chkWeekMaintenanceFlag.Name = "chkWeekMaintenanceFlag";
            this.chkWeekMaintenanceFlag.Size = new System.Drawing.Size(132, 16);
            this.chkWeekMaintenanceFlag.TabIndex = 6;
            this.chkWeekMaintenanceFlag.Text = "【周】保养检查开关";
            this.chkWeekMaintenanceFlag.UseVisualStyleBackColor = true;
            // 
            // gpCode
            // 
            this.gpCode.AutoSize = true;
            this.gpCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gpCode.Controls.Add(this.tableLayoutPanel1);
            this.gpCode.Controls.Add(this.tableLayoutPanel3);
            this.gpCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpCode.Location = new System.Drawing.Point(3, 119);
            this.gpCode.Name = "gpCode";
            this.gpCode.Size = new System.Drawing.Size(301, 210);
            this.gpCode.TabIndex = 19;
            this.gpCode.TabStop = false;
            this.gpCode.Text = "通信编码(16进制字符)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.tbGoodSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbNGSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbWarnCodeSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbSuffixSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbWarnDescSize, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbRemarkSize, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLineSize, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbRunStateSize, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbOperateSize, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label12, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label13, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineSize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label15, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbPrefixSize, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 77);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(295, 130);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // tbGoodSize
            // 
            this.tbGoodSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbGoodSize.Location = new System.Drawing.Point(9, 105);
            this.tbGoodSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbGoodSize.Name = "tbGoodSize";
            this.tbGoodSize.ReadOnly = true;
            this.tbGoodSize.Size = new System.Drawing.Size(31, 21);
            this.tbGoodSize.TabIndex = 34;
            this.tbGoodSize.Text = "2";
            this.tbGoodSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbNGSize
            // 
            this.tbNGSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbNGSize.Location = new System.Drawing.Point(58, 105);
            this.tbNGSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbNGSize.Name = "tbNGSize";
            this.tbNGSize.ReadOnly = true;
            this.tbNGSize.Size = new System.Drawing.Size(31, 21);
            this.tbNGSize.TabIndex = 33;
            this.tbNGSize.Text = "2";
            this.tbNGSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label8, 6);
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "字节数:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbWarnCodeSize
            // 
            this.tbWarnCodeSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbWarnCodeSize.Location = new System.Drawing.Point(107, 105);
            this.tbWarnCodeSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbWarnCodeSize.Name = "tbWarnCodeSize";
            this.tbWarnCodeSize.ReadOnly = true;
            this.tbWarnCodeSize.Size = new System.Drawing.Size(31, 21);
            this.tbWarnCodeSize.TabIndex = 32;
            this.tbWarnCodeSize.Text = "1";
            this.tbWarnCodeSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSuffixSize
            // 
            this.tbSuffixSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbSuffixSize.Location = new System.Drawing.Point(205, 105);
            this.tbSuffixSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbSuffixSize.Name = "tbSuffixSize";
            this.tbSuffixSize.ReadOnly = true;
            this.tbSuffixSize.Size = new System.Drawing.Size(31, 21);
            this.tbSuffixSize.TabIndex = 31;
            this.tbSuffixSize.Text = "0";
            this.tbSuffixSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbWarnDescSize
            // 
            this.tbWarnDescSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbWarnDescSize.Location = new System.Drawing.Point(156, 105);
            this.tbWarnDescSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbWarnDescSize.Name = "tbWarnDescSize";
            this.tbWarnDescSize.ReadOnly = true;
            this.tbWarnDescSize.Size = new System.Drawing.Size(31, 21);
            this.tbWarnDescSize.TabIndex = 30;
            this.tbWarnDescSize.Text = "2";
            this.tbWarnDescSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.Location = new System.Drawing.Point(153, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 30);
            this.label5.TabIndex = 29;
            this.label5.Text = "报警详情";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "后缀";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "产能";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.Location = new System.Drawing.Point(104, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 30);
            this.label2.TabIndex = 26;
            this.label2.Text = "报警状态";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "不良";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "前缀";
            // 
            // tbRemarkSize
            // 
            this.tbRemarkSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbRemarkSize.Location = new System.Drawing.Point(253, 50);
            this.tbRemarkSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbRemarkSize.Name = "tbRemarkSize";
            this.tbRemarkSize.ReadOnly = true;
            this.tbRemarkSize.Size = new System.Drawing.Size(33, 21);
            this.tbRemarkSize.TabIndex = 24;
            this.tbRemarkSize.Text = "1";
            this.tbRemarkSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label11.Location = new System.Drawing.Point(55, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 24);
            this.label11.TabIndex = 1;
            this.label11.Text = "机台编码";
            this.label11.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbLineSize
            // 
            this.tbLineSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbLineSize.Location = new System.Drawing.Point(205, 50);
            this.tbLineSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbLineSize.Name = "tbLineSize";
            this.tbLineSize.ReadOnly = true;
            this.tbLineSize.Size = new System.Drawing.Size(31, 21);
            this.tbLineSize.TabIndex = 20;
            this.tbLineSize.Text = "1";
            this.tbLineSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRunStateSize
            // 
            this.tbRunStateSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbRunStateSize.Location = new System.Drawing.Point(156, 50);
            this.tbRunStateSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbRunStateSize.Name = "tbRunStateSize";
            this.tbRunStateSize.ReadOnly = true;
            this.tbRunStateSize.Size = new System.Drawing.Size(31, 21);
            this.tbRunStateSize.TabIndex = 21;
            this.tbRunStateSize.Text = "1";
            this.tbRunStateSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbOperateSize
            // 
            this.tbOperateSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbOperateSize.Location = new System.Drawing.Point(107, 50);
            this.tbOperateSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbOperateSize.Name = "tbOperateSize";
            this.tbOperateSize.ReadOnly = true;
            this.tbOperateSize.Size = new System.Drawing.Size(31, 21);
            this.tbOperateSize.TabIndex = 22;
            this.tbOperateSize.Text = "1";
            this.tbOperateSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label12.Location = new System.Drawing.Point(104, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 30);
            this.label12.TabIndex = 2;
            this.label12.Text = "操作指令";
            this.label12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label13.Location = new System.Drawing.Point(153, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 30);
            this.label13.TabIndex = 3;
            this.label13.Text = "运行状态";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbMachineSize
            // 
            this.tbMachineSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbMachineSize.Location = new System.Drawing.Point(58, 50);
            this.tbMachineSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbMachineSize.Name = "tbMachineSize";
            this.tbMachineSize.ReadOnly = true;
            this.tbMachineSize.Size = new System.Drawing.Size(31, 21);
            this.tbMachineSize.TabIndex = 15;
            this.tbMachineSize.Text = "2";
            this.tbMachineSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label14.Location = new System.Drawing.Point(202, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 30);
            this.label14.TabIndex = 4;
            this.label14.Text = "产线编码";
            this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(255, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "备用";
            // 
            // tbPrefixSize
            // 
            this.tbPrefixSize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbPrefixSize.Location = new System.Drawing.Point(9, 50);
            this.tbPrefixSize.Margin = new System.Windows.Forms.Padding(0);
            this.tbPrefixSize.Name = "tbPrefixSize";
            this.tbPrefixSize.ReadOnly = true;
            this.tbPrefixSize.Size = new System.Drawing.Size(31, 21);
            this.tbPrefixSize.TabIndex = 23;
            this.tbPrefixSize.Text = "0";
            this.tbPrefixSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tbPrefix, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbSuffix, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(295, 60);
            this.tableLayoutPanel3.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "前缀";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "后缀";
            // 
            // tbPrefix
            // 
            this.tbPrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPrefix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbPrefix.Location = new System.Drawing.Point(44, 4);
            this.tbPrefix.Margin = new System.Windows.Forms.Padding(0);
            this.tbPrefix.Name = "tbPrefix";
            this.tbPrefix.Size = new System.Drawing.Size(242, 21);
            this.tbPrefix.TabIndex = 10;
            this.tbPrefix.TextChanged += new System.EventHandler(this.tbPrefix_TextChanged);
            this.tbPrefix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPrefix_KeyPress);
            // 
            // tbSuffix
            // 
            this.tbSuffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbSuffix.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbSuffix.Location = new System.Drawing.Point(44, 34);
            this.tbSuffix.Margin = new System.Windows.Forms.Padding(0);
            this.tbSuffix.Name = "tbSuffix";
            this.tbSuffix.Size = new System.Drawing.Size(242, 21);
            this.tbSuffix.TabIndex = 12;
            this.tbSuffix.TextChanged += new System.EventHandler(this.tbSuffix_TextChanged);
            this.tbSuffix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSuffix_KeyPress);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.gpOperate, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.gpCode, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.gbTcp, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.gbSerailPort, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.labTip, 0, 4);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(307, 644);
            this.tableLayoutPanel6.TabIndex = 18;
            // 
            // FrmConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(307, 659);
            this.Controls.Add(this.tableLayoutPanel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.gbSerailPort.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.gbTcp.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceiveTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.gpOperate.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gpCode.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTcpListenFlag;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.NumericUpDown nudReceiveTimeout;
        private System.Windows.Forms.Label labReceiveTimeout;
        private System.Windows.Forms.Label labTip;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label labLocalIPTitle;
        private System.Windows.Forms.Button btnCheckPort;
        private System.Windows.Forms.CheckBox chkTcpListenFlag;
        private System.Windows.Forms.GroupBox gbTcp;
        private System.Windows.Forms.GroupBox gbSerailPort;
        private System.Windows.Forms.Label labStopBits;
        private System.Windows.Forms.Label labDataBits;
        private System.Windows.Forms.Label labBaudRate;
        private System.Windows.Forms.Label labPortName;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label labParity;
        private System.Windows.Forms.CheckBox chkSerailListenFlag;
        private System.Windows.Forms.Label labSerailListenFlag;
        private System.Windows.Forms.GroupBox gpOperate;
        private System.Windows.Forms.ComboBox cbListenIP;
        private System.Windows.Forms.CheckBox chkMonthMaintenanceFlag;
        private System.Windows.Forms.CheckBox chkWeekMaintenanceFlag;
        private System.Windows.Forms.CheckBox chkDayMaintenanceFlag;
        private System.Windows.Forms.GroupBox gpCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRemarkSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLineSize;
        private System.Windows.Forms.TextBox tbRunStateSize;
        private System.Windows.Forms.TextBox tbOperateSize;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbMachineSize;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbPrefixSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSuffix;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbPrefix;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbGoodSize;
        private System.Windows.Forms.TextBox tbNGSize;
        private System.Windows.Forms.TextBox tbWarnCodeSize;
        private System.Windows.Forms.TextBox tbSuffixSize;
        private System.Windows.Forms.TextBox tbWarnDescSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
    }
}