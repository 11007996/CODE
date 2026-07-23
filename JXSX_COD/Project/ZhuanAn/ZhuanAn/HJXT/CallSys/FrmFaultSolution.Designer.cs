namespace CallSys
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
            this.labHandler = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lsbSolutionItems
            // 
            this.lsbSolutionItems.ItemHeight = 15;
            this.lsbSolutionItems.Location = new System.Drawing.Point(116, 396);
            this.lsbSolutionItems.Margin = new System.Windows.Forms.Padding(4);
            this.lsbSolutionItems.Name = "lsbSolutionItems";
            this.lsbSolutionItems.Size = new System.Drawing.Size(725, 139);
            this.lsbSolutionItems.TabIndex = 17;
            // 
            // labSolutionItems
            // 
            this.labSolutionItems.AutoSize = true;
            this.labSolutionItems.BackColor = System.Drawing.Color.Transparent;
            this.labSolutionItems.Location = new System.Drawing.Point(32, 360);
            this.labSolutionItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labSolutionItems.Name = "labSolutionItems";
            this.labSolutionItems.Size = new System.Drawing.Size(82, 15);
            this.labSolutionItems.TabIndex = 16;
            this.labSolutionItems.Text = "解决方案：";
            // 
            // lsbFaultItems
            // 
            this.lsbFaultItems.ItemHeight = 15;
            this.lsbFaultItems.Location = new System.Drawing.Point(116, 202);
            this.lsbFaultItems.Margin = new System.Windows.Forms.Padding(4);
            this.lsbFaultItems.Name = "lsbFaultItems";
            this.lsbFaultItems.Size = new System.Drawing.Size(725, 124);
            this.lsbFaultItems.TabIndex = 15;
            // 
            // labHandlerTitle
            // 
            this.labHandlerTitle.AutoSize = true;
            this.labHandlerTitle.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerTitle.Location = new System.Drawing.Point(442, 22);
            this.labHandlerTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerTitle.Name = "labHandlerTitle";
            this.labHandlerTitle.Size = new System.Drawing.Size(67, 15);
            this.labHandlerTitle.TabIndex = 27;
            this.labHandlerTitle.Text = "维护人：";
            // 
            // nudProdCount
            // 
            this.nudProdCount.Location = new System.Drawing.Point(116, 66);
            this.nudProdCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudProdCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudProdCount.Name = "nudProdCount";
            this.nudProdCount.Size = new System.Drawing.Size(253, 25);
            this.nudProdCount.TabIndex = 25;
            // 
            // labProdCount
            // 
            this.labProdCount.AutoSize = true;
            this.labProdCount.BackColor = System.Drawing.Color.Transparent;
            this.labProdCount.Location = new System.Drawing.Point(17, 71);
            this.labProdCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labProdCount.Name = "labProdCount";
            this.labProdCount.Size = new System.Drawing.Size(97, 15);
            this.labProdCount.TabIndex = 24;
            this.labProdCount.Text = "制品跟踪数：";
            // 
            // tbMachineNo
            // 
            this.tbMachineNo.Enabled = false;
            this.tbMachineNo.Location = new System.Drawing.Point(116, 17);
            this.tbMachineNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbMachineNo.Name = "tbMachineNo";
            this.tbMachineNo.Size = new System.Drawing.Size(253, 25);
            this.tbMachineNo.TabIndex = 23;
            // 
            // labMachineNo
            // 
            this.labMachineNo.AutoSize = true;
            this.labMachineNo.BackColor = System.Drawing.Color.Transparent;
            this.labMachineNo.Location = new System.Drawing.Point(32, 22);
            this.labMachineNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineNo.Name = "labMachineNo";
            this.labMachineNo.Size = new System.Drawing.Size(82, 15);
            this.labMachineNo.TabIndex = 22;
            this.labMachineNo.Text = "机台编号：";
            // 
            // labTotalTimeTitle
            // 
            this.labTotalTimeTitle.AutoSize = true;
            this.labTotalTimeTitle.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTimeTitle.Location = new System.Drawing.Point(47, 565);
            this.labTotalTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTotalTimeTitle.Name = "labTotalTimeTitle";
            this.labTotalTimeTitle.Size = new System.Drawing.Size(67, 15);
            this.labTotalTimeTitle.TabIndex = 30;
            this.labTotalTimeTitle.Text = "总用时：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirm.Location = new System.Drawing.Point(754, 562);
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
            this.labTotalTime.AutoSize = true;
            this.labTotalTime.BackColor = System.Drawing.Color.Transparent;
            this.labTotalTime.Location = new System.Drawing.Point(122, 565);
            this.labTotalTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTotalTime.Name = "labTotalTime";
            this.labTotalTime.Size = new System.Drawing.Size(45, 15);
            this.labTotalTime.TabIndex = 33;
            this.labTotalTime.Text = "0分钟";
            // 
            // labFaultType
            // 
            this.labFaultType.AutoSize = true;
            this.labFaultType.BackColor = System.Drawing.Color.Transparent;
            this.labFaultType.Location = new System.Drawing.Point(427, 71);
            this.labFaultType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultType.Name = "labFaultType";
            this.labFaultType.Size = new System.Drawing.Size(82, 15);
            this.labFaultType.TabIndex = 34;
            this.labFaultType.Text = "故障类别：";
            // 
            // cmbFault
            // 
            this.cmbFault.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFault.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFault.DisplayMember = "Text";
            this.cmbFault.ForeColor = System.Drawing.Color.Black;
            this.cmbFault.FormattingEnabled = true;
            this.cmbFault.ItemHeight = 15;
            this.cmbFault.Location = new System.Drawing.Point(116, 165);
            this.cmbFault.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFault.Name = "cmbFault";
            this.cmbFault.Size = new System.Drawing.Size(725, 23);
            this.cmbFault.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbFault.TabIndex = 36;
            this.cmbFault.SelectedIndexChanged += new System.EventHandler(this.cmbFaultShort_SelectedIndexChanged);
            // 
            // labFaultItems
            // 
            this.labFaultItems.AutoSize = true;
            this.labFaultItems.BackColor = System.Drawing.Color.Transparent;
            this.labFaultItems.Location = new System.Drawing.Point(31, 169);
            this.labFaultItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFaultItems.Name = "labFaultItems";
            this.labFaultItems.Size = new System.Drawing.Size(82, 15);
            this.labFaultItems.TabIndex = 14;
            this.labFaultItems.Text = "故障内容：";
            // 
            // cmbSolution
            // 
            this.cmbSolution.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSolution.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSolution.DisplayMember = "Text";
            this.cmbSolution.ForeColor = System.Drawing.Color.Black;
            this.cmbSolution.FormattingEnabled = true;
            this.cmbSolution.ItemHeight = 15;
            this.cmbSolution.Location = new System.Drawing.Point(116, 356);
            this.cmbSolution.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSolution.Name = "cmbSolution";
            this.cmbSolution.Size = new System.Drawing.Size(725, 23);
            this.cmbSolution.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbSolution.TabIndex = 40;
            this.cmbSolution.SelectedIndexChanged += new System.EventHandler(this.cmbSolutionShort_SelectedIndexChanged);
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.Items.AddRange(new object[] {
            "机台故障",
            "换线",
            "操作异常"});
            this.cmbFaultType.Location = new System.Drawing.Point(517, 67);
            this.cmbFaultType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Size = new System.Drawing.Size(244, 23);
            this.cmbFaultType.TabIndex = 46;
            this.cmbFaultType.TextChanged += new System.EventHandler(this.cmbFaultType_TextChanged);
            // 
            // labMachineType
            // 
            this.labMachineType.AutoSize = true;
            this.labMachineType.BackColor = System.Drawing.Color.Transparent;
            this.labMachineType.Location = new System.Drawing.Point(32, 120);
            this.labMachineType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMachineType.Name = "labMachineType";
            this.labMachineType.Size = new System.Drawing.Size(82, 15);
            this.labMachineType.TabIndex = 48;
            this.labMachineType.Text = "机台类型：";
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMachineType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMachineType.DisplayMember = "MachineType";
            this.cmbMachineType.ForeColor = System.Drawing.Color.Black;
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.ItemHeight = 15;
            this.cmbMachineType.Location = new System.Drawing.Point(116, 116);
            this.cmbMachineType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(253, 23);
            this.cmbMachineType.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.cmbMachineType.TabIndex = 47;
            this.cmbMachineType.ValueMember = "MachineType";
            this.cmbMachineType.TextChanged += new System.EventHandler(this.cmbMachineType_TextChanged);
            // 
            // labHandler
            // 
            this.labHandler.AutoSize = true;
            this.labHandler.BackColor = System.Drawing.Color.Transparent;
            this.labHandler.Location = new System.Drawing.Point(517, 22);
            this.labHandler.Name = "labHandler";
            this.labHandler.Size = new System.Drawing.Size(0, 15);
            this.labHandler.TabIndex = 51;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(213, 565);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 15);
            this.labMessage.TabIndex = 52;
            // 
            // FrmFaultSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(894, 620);
            this.ControlBox = false;
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.labHandler);
            this.Controls.Add(this.labMachineType);
            this.Controls.Add(this.cmbMachineType);
            this.Controls.Add(this.cmbFaultType);
            this.Controls.Add(this.cmbSolution);
            this.Controls.Add(this.cmbFault);
            this.Controls.Add(this.labFaultType);
            this.Controls.Add(this.labTotalTime);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.labTotalTimeTitle);
            this.Controls.Add(this.labHandlerTitle);
            this.Controls.Add(this.nudProdCount);
            this.Controls.Add(this.labProdCount);
            this.Controls.Add(this.tbMachineNo);
            this.Controls.Add(this.labMachineNo);
            this.Controls.Add(this.lsbSolutionItems);
            this.Controls.Add(this.labSolutionItems);
            this.Controls.Add(this.lsbFaultItems);
            this.Controls.Add(this.labFaultItems);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFaultSolution";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "解决方案";
            this.Load += new System.EventHandler(this.FrmFaultSolution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudProdCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label labHandler;
        private System.Windows.Forms.Label labMessage;
    }
}