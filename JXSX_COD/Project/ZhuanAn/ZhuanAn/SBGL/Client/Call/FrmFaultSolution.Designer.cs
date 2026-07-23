namespace Call
{
    partial class FrmFaultSolution
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
            this.lsbSolutionItems = new System.Windows.Forms.ListBox();
            this.labSolutionItems = new System.Windows.Forms.Label();
            this.lsbFaultItems = new System.Windows.Forms.ListBox();
            this.labHandlerTitle = new System.Windows.Forms.Label();
            this.nudProdCount = new System.Windows.Forms.NumericUpDown();
            this.labProdCount = new System.Windows.Forms.Label();
            this.tbMachineNo = new System.Windows.Forms.TextBox();
            this.labMachineNo = new System.Windows.Forms.Label();
            this.labTotalTimeTitle = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.labTotalTime = new System.Windows.Forms.Label();
            this.labFaultType = new System.Windows.Forms.Label();
            this.cmbFault = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labFaultItems = new System.Windows.Forms.Label();
            this.cmbSolution = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbFaultType = new System.Windows.Forms.ComboBox();
            this.labMachineType = new System.Windows.Forms.Label();
            this.cmbMachineType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labMessage = new System.Windows.Forms.Label();
            this.nudPassCount = new System.Windows.Forms.NumericUpDown();
            this.nudNGCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbSolver = new System.Windows.Forms.TextBox();
            this.txbQCName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNGCount)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbSolutionItems
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lsbSolutionItems, 5);
            this.lsbSolutionItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbSolutionItems.ItemHeight = 15;
            this.lsbSolutionItems.Location = new System.Drawing.Point(104, 377);
            this.lsbSolutionItems.Margin = new System.Windows.Forms.Padding(4);
            this.lsbSolutionItems.Name = "lsbSolutionItems";
            this.lsbSolutionItems.Size = new System.Drawing.Size(722, 115);
            this.lsbSolutionItems.TabIndex = 17;
            // 
            // labSolutionItems
            // 
            this.labSolutionItems.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSolutionItems.AutoSize = true;
            this.labSolutionItems.BackColor = System.Drawing.Color.Transparent;
            this.labSolutionItems.Location = new System.Drawing.Point(29, 340);
            this.labSolutionItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSolutionItems.Name = "labSolutionItems";
            this.labSolutionItems.Size = new System.Drawing.Size(67, 15);
            this.labSolutionItems.TabIndex = 16;
            this.labSolutionItems.Text = "解决方案";
            // 
            // lsbFaultItems
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lsbFaultItems, 5);
            this.lsbFaultItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbFaultItems.ItemHeight = 15;
            this.lsbFaultItems.Location = new System.Drawing.Point(104, 204);
            this.lsbFaultItems.Margin = new System.Windows.Forms.Padding(4);
            this.lsbFaultItems.Name = "lsbFaultItems";
            this.lsbFaultItems.Size = new System.Drawing.Size(722, 115);
            this.lsbFaultItems.TabIndex = 15;
            // 
            // labHandlerTitle
            // 
            this.labHandlerTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labHandlerTitle.AutoSize = true;
            this.labHandlerTitle.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerTitle.Location = new System.Drawing.Point(314, 17);
            this.labHandlerTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerTitle.Name = "labHandlerTitle";
            this.labHandlerTitle.Size = new System.Drawing.Size(52, 15);
            this.labHandlerTitle.TabIndex = 27;
            this.labHandlerTitle.Text = "维护人";
            // 
            // nudProdCount
            // 
            this.nudProdCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudProdCount.Location = new System.Drawing.Point(104, 62);
            this.nudProdCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudProdCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudProdCount.Name = "nudProdCount";
            this.nudProdCount.Size = new System.Drawing.Size(182, 25);
            this.nudProdCount.TabIndex = 25;
            // 
            // labProdCount
            // 
            this.labProdCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labProdCount.AutoSize = true;
            this.labProdCount.BackColor = System.Drawing.Color.Transparent;
            this.labProdCount.Location = new System.Drawing.Point(29, 67);
            this.labProdCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labProdCount.Name = "labProdCount";
            this.labProdCount.Size = new System.Drawing.Size(67, 15);
            this.labProdCount.TabIndex = 24;
            this.labProdCount.Text = "调机品数";
            // 
            // tbMachineNo
            // 
            this.tbMachineNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbMachineNo.Enabled = false;
            this.tbMachineNo.Location = new System.Drawing.Point(104, 12);
            this.tbMachineNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbMachineNo.Name = "tbMachineNo";
            this.tbMachineNo.Size = new System.Drawing.Size(182, 25);
            this.tbMachineNo.TabIndex = 23;
            // 
            // labMachineNo
            // 
            this.labMachineNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineNo.AutoSize = true;
            this.labMachineNo.BackColor = System.Drawing.Color.Transparent;
            this.labMachineNo.Location = new System.Drawing.Point(29, 17);
            this.labMachineNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineNo.Name = "labMachineNo";
            this.labMachineNo.Size = new System.Drawing.Size(67, 15);
            this.labMachineNo.TabIndex = 22;
            this.labMachineNo.Text = "机台编号";
            // 
            // labTotalTimeTitle
            // 
            this.labTotalTimeTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labTotalTimeTitle.AutoSize = true;
            this.labTotalTimeTitle.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTimeTitle.Location = new System.Drawing.Point(44, 513);
            this.labTotalTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTotalTimeTitle.Name = "labTotalTimeTitle";
            this.labTotalTimeTitle.Size = new System.Drawing.Size(52, 15);
            this.labTotalTimeTitle.TabIndex = 30;
            this.labTotalTimeTitle.Text = "总用时";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.BackgroundImage = global::Call.Properties.Resources.menu_bg;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Location = new System.Drawing.Point(739, 505);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 31);
            this.btnConfirm.TabIndex = 31;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labTotalTime
            // 
            this.labTotalTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTotalTime.AutoSize = true;
            this.labTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTime.Location = new System.Drawing.Point(104, 513);
            this.labTotalTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTotalTime.Name = "labTotalTime";
            this.labTotalTime.Size = new System.Drawing.Size(45, 15);
            this.labTotalTime.TabIndex = 33;
            this.labTotalTime.Text = "0分钟";
            // 
            // labFaultType
            // 
            this.labFaultType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labFaultType.AutoSize = true;
            this.labFaultType.BackColor = System.Drawing.Color.Transparent;
            this.labFaultType.Location = new System.Drawing.Point(299, 117);
            this.labFaultType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultType.Name = "labFaultType";
            this.labFaultType.Size = new System.Drawing.Size(67, 15);
            this.labFaultType.TabIndex = 34;
            this.labFaultType.Text = "故障类别";
            // 
            // cmbFault
            // 
            this.cmbFault.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbFault.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFault.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel1.SetColumnSpan(this.cmbFault, 5);
            this.cmbFault.DisplayMember = "Text";
            this.cmbFault.ForeColor = System.Drawing.Color.Black;
            this.cmbFault.FormattingEnabled = true;
            this.cmbFault.ItemHeight = 15;
            this.cmbFault.Location = new System.Drawing.Point(104, 163);
            this.cmbFault.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFault.Name = "cmbFault";
            this.cmbFault.Size = new System.Drawing.Size(722, 23);
            this.cmbFault.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbFault.TabIndex = 36;
            this.cmbFault.SelectedIndexChanged += new System.EventHandler(this.cmbFaultShort_SelectedIndexChanged);
            // 
            // labFaultItems
            // 
            this.labFaultItems.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labFaultItems.AutoSize = true;
            this.labFaultItems.BackColor = System.Drawing.Color.Transparent;
            this.labFaultItems.Location = new System.Drawing.Point(29, 167);
            this.labFaultItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultItems.Name = "labFaultItems";
            this.labFaultItems.Size = new System.Drawing.Size(67, 15);
            this.labFaultItems.TabIndex = 14;
            this.labFaultItems.Text = "故障内容";
            // 
            // cmbSolution
            // 
            this.cmbSolution.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbSolution.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSolution.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel1.SetColumnSpan(this.cmbSolution, 5);
            this.cmbSolution.DisplayMember = "Text";
            this.cmbSolution.ForeColor = System.Drawing.Color.Black;
            this.cmbSolution.FormattingEnabled = true;
            this.cmbSolution.ItemHeight = 15;
            this.cmbSolution.Location = new System.Drawing.Point(104, 336);
            this.cmbSolution.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSolution.Name = "cmbSolution";
            this.cmbSolution.Size = new System.Drawing.Size(722, 23);
            this.cmbSolution.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbSolution.TabIndex = 40;
            this.cmbSolution.SelectedIndexChanged += new System.EventHandler(this.cmbSolutionShort_SelectedIndexChanged);
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbFaultType.Items.AddRange(new object[] {
            "机台故障",
            "换线",
            "操作异常"});
            this.cmbFaultType.Location = new System.Drawing.Point(374, 113);
            this.cmbFaultType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Size = new System.Drawing.Size(182, 23);
            this.cmbFaultType.TabIndex = 46;
            this.cmbFaultType.TextChanged += new System.EventHandler(this.cmbFaultType_TextChanged);
            // 
            // labMachineType
            // 
            this.labMachineType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineType.AutoSize = true;
            this.labMachineType.BackColor = System.Drawing.Color.Transparent;
            this.labMachineType.Location = new System.Drawing.Point(29, 117);
            this.labMachineType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(67, 15);
            this.labMachineType.TabIndex = 48;
            this.labMachineType.Text = "机台类型";
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachineType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMachineType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMachineType.DisplayMember = "MachineType";
            this.cmbMachineType.ForeColor = System.Drawing.Color.Black;
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.ItemHeight = 15;
            this.cmbMachineType.Location = new System.Drawing.Point(104, 113);
            this.cmbMachineType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(182, 23);
            this.cmbMachineType.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbMachineType.TabIndex = 47;
            this.cmbMachineType.ValueMember = "MachineType";
            this.cmbMachineType.TextChanged += new System.EventHandler(this.cmbMachineType_TextChanged);
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 3);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(293, 513);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 52;
            this.labMessage.Text = "提示";
            // 
            // nudPassCount
            // 
            this.nudPassCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPassCount.Location = new System.Drawing.Point(374, 62);
            this.nudPassCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudPassCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudPassCount.Name = "nudPassCount";
            this.nudPassCount.Size = new System.Drawing.Size(182, 25);
            this.nudPassCount.TabIndex = 53;
            // 
            // nudNGCount
            // 
            this.nudNGCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudNGCount.Location = new System.Drawing.Point(644, 62);
            this.nudNGCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudNGCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudNGCount.Name = "nudNGCount";
            this.nudNGCount.Size = new System.Drawing.Size(182, 25);
            this.nudNGCount.TabIndex = 54;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(329, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 55;
            this.label1.Text = "良品";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(584, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 56;
            this.label2.Text = "不良品";
            // 
            // txbSolver
            // 
            this.txbSolver.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbSolver.Enabled = false;
            this.txbSolver.Location = new System.Drawing.Point(374, 12);
            this.txbSolver.Margin = new System.Windows.Forms.Padding(4);
            this.txbSolver.Name = "txbSolver";
            this.txbSolver.ReadOnly = true;
            this.txbSolver.Size = new System.Drawing.Size(182, 25);
            this.txbSolver.TabIndex = 57;
            // 
            // txbQCName
            // 
            this.txbQCName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbQCName.Enabled = false;
            this.txbQCName.Location = new System.Drawing.Point(644, 12);
            this.txbQCName.Margin = new System.Windows.Forms.Padding(4);
            this.txbQCName.Name = "txbQCName";
            this.txbQCName.ReadOnly = true;
            this.txbQCName.Size = new System.Drawing.Size(182, 25);
            this.txbQCName.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(599, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 59;
            this.label3.Text = "品管";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.labMachineNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.nudNGCount, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 6, 7);
            this.tableLayoutPanel1.Controls.Add(this.labTotalTime, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.cmbSolution, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.cmbFaultType, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.labTotalTimeTitle, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cmbFault, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lsbSolutionItems, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachineType, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labMachineType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labSolutionItems, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labFaultType, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lsbFaultItems, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbQCName, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudPassCount, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.labFaultItems, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbMachineNo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labHandlerTitle, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbSolver, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labProdCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudProdCount, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(852, 546);
            this.tableLayoutPanel1.TabIndex = 60;
            // 
            // FrmFaultSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImage = global::Call.Properties.Resources.menu_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(852, 546);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFaultSolution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "解决方案";
            this.Load += new System.EventHandler(this.FrmFaultSolution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNGCount)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbSolutionItems;
        private System.Windows.Forms.Label labSolutionItems;
        private System.Windows.Forms.ListBox lsbFaultItems;
        private System.Windows.Forms.Label labHandlerTitle;
        private System.Windows.Forms.NumericUpDown nudProdCount;
        private System.Windows.Forms.Label labProdCount;
        private System.Windows.Forms.TextBox tbMachineNo;
        private System.Windows.Forms.Label labMachineNo;
        private System.Windows.Forms.Label labTotalTimeTitle;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label labTotalTime;
        private System.Windows.Forms.Label labFaultType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFault;
        private System.Windows.Forms.Label labFaultItems;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSolution;
        private System.Windows.Forms.ComboBox cmbFaultType;
        private System.Windows.Forms.Label labMachineType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMachineType;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.NumericUpDown nudPassCount;
        private System.Windows.Forms.NumericUpDown nudNGCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbSolver;
        private System.Windows.Forms.TextBox txbQCName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}