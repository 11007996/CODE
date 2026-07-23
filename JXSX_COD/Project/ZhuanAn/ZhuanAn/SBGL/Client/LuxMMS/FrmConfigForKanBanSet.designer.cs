namespace LuxMMS
{
    partial class FrmConfigForKanBanSet
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
            this.labMachineNum = new System.Windows.Forms.Label();
            this.labPreWeekNum = new System.Windows.Forms.Label();
            this.nudMachineNum = new System.Windows.Forms.NumericUpDown();
            this.nudPreWeekNum = new System.Windows.Forms.NumericUpDown();
            this.labCallHandlerShowFlag = new System.Windows.Forms.Label();
            this.swBtnCallHandlerShowFlag = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labPreWeekNumTip = new System.Windows.Forms.Label();
            this.labMachineNumTip = new System.Windows.Forms.Label();
            this.labDept = new System.Windows.Forms.Label();
            this.cmbDept = new System.Windows.Forms.ComboBox();
            this.cmbKanBanShow = new System.Windows.Forms.ComboBox();
            this.labKanBanShow = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMachineNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreWeekNum)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labMachineNum
            // 
            this.labMachineNum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachineNum.AutoSize = true;
            this.labMachineNum.BackColor = System.Drawing.Color.Transparent;
            this.labMachineNum.Location = new System.Drawing.Point(21, 52);
            this.labMachineNum.Name = "labMachineNum";
            this.labMachineNum.Size = new System.Drawing.Size(52, 15);
            this.labMachineNum.TabIndex = 8;
            this.labMachineNum.Text = "机台数";
            this.labMachineNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labPreWeekNum
            // 
            this.labPreWeekNum.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPreWeekNum.AutoSize = true;
            this.labPreWeekNum.BackColor = System.Drawing.Color.Transparent;
            this.labPreWeekNum.Location = new System.Drawing.Point(21, 92);
            this.labPreWeekNum.Name = "labPreWeekNum";
            this.labPreWeekNum.Size = new System.Drawing.Size(52, 15);
            this.labPreWeekNum.TabIndex = 9;
            this.labPreWeekNum.Text = "星期数";
            this.labPreWeekNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudMachineNum
            // 
            this.nudMachineNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudMachineNum.Location = new System.Drawing.Point(79, 47);
            this.nudMachineNum.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMachineNum.Name = "nudMachineNum";
            this.nudMachineNum.Size = new System.Drawing.Size(147, 25);
            this.nudMachineNum.TabIndex = 10;
            // 
            // nudPreWeekNum
            // 
            this.nudPreWeekNum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudPreWeekNum.Location = new System.Drawing.Point(79, 87);
            this.nudPreWeekNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPreWeekNum.Name = "nudPreWeekNum";
            this.nudPreWeekNum.Size = new System.Drawing.Size(147, 25);
            this.nudPreWeekNum.TabIndex = 11;
            // 
            // labCallHandlerShowFlag
            // 
            this.labCallHandlerShowFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCallHandlerShowFlag.AutoSize = true;
            this.labCallHandlerShowFlag.BackColor = System.Drawing.Color.Transparent;
            this.labCallHandlerShowFlag.Location = new System.Drawing.Point(6, 132);
            this.labCallHandlerShowFlag.Name = "labCallHandlerShowFlag";
            this.labCallHandlerShowFlag.Size = new System.Drawing.Size(67, 15);
            this.labCallHandlerShowFlag.TabIndex = 16;
            this.labCallHandlerShowFlag.Text = "指定人员";
            this.labCallHandlerShowFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // swBtnCallHandlerShowFlag
            // 
            this.swBtnCallHandlerShowFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.swBtnCallHandlerShowFlag.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.swBtnCallHandlerShowFlag.Location = new System.Drawing.Point(79, 129);
            this.swBtnCallHandlerShowFlag.Name = "swBtnCallHandlerShowFlag";
            this.swBtnCallHandlerShowFlag.OffText = "关闭";
            this.swBtnCallHandlerShowFlag.OnText = "开启";
            this.swBtnCallHandlerShowFlag.Size = new System.Drawing.Size(66, 22);
            this.swBtnCallHandlerShowFlag.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.swBtnCallHandlerShowFlag.TabIndex = 17;
            // 
            // labPreWeekNumTip
            // 
            this.labPreWeekNumTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labPreWeekNumTip.AutoSize = true;
            this.labPreWeekNumTip.Location = new System.Drawing.Point(232, 92);
            this.labPreWeekNumTip.Name = "labPreWeekNumTip";
            this.labPreWeekNumTip.Size = new System.Drawing.Size(61, 15);
            this.labPreWeekNumTip.TabIndex = 15;
            this.labPreWeekNumTip.Text = "(最大5)";
            // 
            // labMachineNumTip
            // 
            this.labMachineNumTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMachineNumTip.AutoSize = true;
            this.labMachineNumTip.Location = new System.Drawing.Point(232, 52);
            this.labMachineNumTip.Name = "labMachineNumTip";
            this.labMachineNumTip.Size = new System.Drawing.Size(69, 15);
            this.labMachineNumTip.TabIndex = 14;
            this.labMachineNumTip.Text = "(最大30)";
            // 
            // labDept
            // 
            this.labDept.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDept.AutoSize = true;
            this.labDept.Location = new System.Drawing.Point(6, 12);
            this.labDept.Name = "labDept";
            this.labDept.Size = new System.Drawing.Size(67, 15);
            this.labDept.TabIndex = 12;
            this.labDept.Text = "人员部门";
            this.labDept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDept
            // 
            this.cmbDept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Location = new System.Drawing.Point(79, 8);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(147, 23);
            this.cmbDept.TabIndex = 13;
            // 
            // cmbKanBanShow
            // 
            this.cmbKanBanShow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbKanBanShow.FormattingEnabled = true;
            this.cmbKanBanShow.Items.AddRange(new object[] {
            "呼叫看板",
            "设备看板",
            "兼容切换"});
            this.cmbKanBanShow.Location = new System.Drawing.Point(79, 168);
            this.cmbKanBanShow.Name = "cmbKanBanShow";
            this.cmbKanBanShow.Size = new System.Drawing.Size(147, 23);
            this.cmbKanBanShow.TabIndex = 15;
            // 
            // labKanBanShow
            // 
            this.labKanBanShow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labKanBanShow.AutoSize = true;
            this.labKanBanShow.Location = new System.Drawing.Point(6, 172);
            this.labKanBanShow.Name = "labKanBanShow";
            this.labKanBanShow.Size = new System.Drawing.Size(67, 15);
            this.labKanBanShow.TabIndex = 14;
            this.labKanBanShow.Text = "看板显示";
            this.labKanBanShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.swBtnCallHandlerShowFlag, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labCallHandlerShowFlag, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labKanBanShow, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmbKanBanShow, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.labDept, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDept, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labPreWeekNumTip, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labMachineNum, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudPreWeekNum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labMachineNumTip, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labPreWeekNum, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nudMachineNum, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 200);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 223);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "看板设置";
            // 
            // FrmConfigForKanBanSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(312, 223);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfigForKanBanSet";
            this.Text = "看板设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfigForKanBanSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMachineNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreWeekNum)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labMachineNum;
        private System.Windows.Forms.Label labPreWeekNum;
        private System.Windows.Forms.NumericUpDown nudMachineNum;
        private System.Windows.Forms.NumericUpDown nudPreWeekNum;
        private System.Windows.Forms.Label labDept;
        private System.Windows.Forms.ComboBox cmbDept;
        private System.Windows.Forms.Label labKanBanShow;
        private System.Windows.Forms.Label labPreWeekNumTip;
        private System.Windows.Forms.Label labMachineNumTip;
        private System.Windows.Forms.ComboBox cmbKanBanShow;
        private System.Windows.Forms.Label labCallHandlerShowFlag;
        private DevComponents.DotNetBar.Controls.SwitchButton swBtnCallHandlerShowFlag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}