namespace PMS
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolHuman = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUserFileManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRewardPunishManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUserReansferManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEvaluateManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSalaryManagemeng = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAttendanceManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSalarySummarize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSysManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.depManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDataBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.rightManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUser = new System.Windows.Forms.ToolStripMenuItem();
            this.OperatorManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.OperatorChange = new System.Windows.Forms.ToolStripMenuItem();
            this.userManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolHuman,
            this.toolSalaryManagemeng,
            this.toolSysManagement,
            this.toolUser,
            this.ToolManagement,
            this.toolExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(786, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolHuman
            // 
            this.toolHuman.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolUserFileManagement,
            this.toolRewardPunishManagement,
            this.toolUserReansferManagement,
            this.toolEvaluateManagement});
            this.toolHuman.Name = "toolHuman";
            this.toolHuman.Size = new System.Drawing.Size(81, 24);
            this.toolHuman.Text = "人事管理";
            this.toolHuman.Click += new System.EventHandler(this.toolHuman_Click);
            // 
            // toolUserFileManagement
            // 
            this.toolUserFileManagement.Name = "toolUserFileManagement";
            this.toolUserFileManagement.Size = new System.Drawing.Size(144, 26);
            this.toolUserFileManagement.Text = "档案管理";
            // 
            // toolRewardPunishManagement
            // 
            this.toolRewardPunishManagement.Name = "toolRewardPunishManagement";
            this.toolRewardPunishManagement.Size = new System.Drawing.Size(144, 26);
            this.toolRewardPunishManagement.Text = "奖罚管理";
            // 
            // toolUserReansferManagement
            // 
            this.toolUserReansferManagement.Name = "toolUserReansferManagement";
            this.toolUserReansferManagement.Size = new System.Drawing.Size(144, 26);
            this.toolUserReansferManagement.Text = "调动管理";
            // 
            // toolEvaluateManagement
            // 
            this.toolEvaluateManagement.Name = "toolEvaluateManagement";
            this.toolEvaluateManagement.Size = new System.Drawing.Size(144, 26);
            this.toolEvaluateManagement.Text = "考评管理";
            // 
            // toolSalaryManagemeng
            // 
            this.toolSalaryManagemeng.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAttendanceManagement,
            this.toolSalarySummarize});
            this.toolSalaryManagemeng.Name = "toolSalaryManagemeng";
            this.toolSalaryManagemeng.Size = new System.Drawing.Size(81, 24);
            this.toolSalaryManagemeng.Text = "工资管理";
            this.toolSalaryManagemeng.Click += new System.EventHandler(this.toolSalaryManagemeng_Click);
            // 
            // toolAttendanceManagement
            // 
            this.toolAttendanceManagement.Name = "toolAttendanceManagement";
            this.toolAttendanceManagement.Size = new System.Drawing.Size(144, 26);
            this.toolAttendanceManagement.Text = "考勤管理";
            // 
            // toolSalarySummarize
            // 
            this.toolSalarySummarize.Name = "toolSalarySummarize";
            this.toolSalarySummarize.Size = new System.Drawing.Size(144, 26);
            this.toolSalarySummarize.Text = "工资总结";
            // 
            // toolSysManagement
            // 
            this.toolSysManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.depManagement,
            this.toolDataBackup,
            this.rightManagement});
            this.toolSysManagement.Name = "toolSysManagement";
            this.toolSysManagement.Size = new System.Drawing.Size(81, 24);
            this.toolSysManagement.Text = "系统管理";
            this.toolSysManagement.Click += new System.EventHandler(this.toolSysManagement_Click);
            // 
            // depManagement
            // 
            this.depManagement.Name = "depManagement";
            this.depManagement.Size = new System.Drawing.Size(181, 26);
            this.depManagement.Text = "部门管理";
            this.depManagement.Click += new System.EventHandler(this.depManagement_Click);
            // 
            // toolDataBackup
            // 
            this.toolDataBackup.Name = "toolDataBackup";
            this.toolDataBackup.Size = new System.Drawing.Size(181, 26);
            this.toolDataBackup.Text = "数据备份";
            // 
            // rightManagement
            // 
            this.rightManagement.Name = "rightManagement";
            this.rightManagement.Size = new System.Drawing.Size(181, 26);
            this.rightManagement.Text = "权限管理";
            this.rightManagement.Click += new System.EventHandler(this.rightManagement_Click);
            // 
            // toolUser
            // 
            this.toolUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OperatorManagement,
            this.OperatorChange,
            this.userManagement});
            this.toolUser.Name = "toolUser";
            this.toolUser.Size = new System.Drawing.Size(81, 24);
            this.toolUser.Text = "用户管理";
            this.toolUser.Click += new System.EventHandler(this.toolUser_Click);
            // 
            // OperatorManagement
            // 
            this.OperatorManagement.Name = "OperatorManagement";
            this.OperatorManagement.Size = new System.Drawing.Size(181, 26);
            this.OperatorManagement.Text = "操作员管理";
            // 
            // OperatorChange
            // 
            this.OperatorChange.Name = "OperatorChange";
            this.OperatorChange.Size = new System.Drawing.Size(181, 26);
            this.OperatorChange.Text = "更改操作员";
            this.OperatorChange.Click += new System.EventHandler(this.OperatorChange_Click);
            // 
            // userManagement
            // 
            this.userManagement.Name = "userManagement";
            this.userManagement.Size = new System.Drawing.Size(181, 26);
            this.userManagement.Text = "普通用户管理";
            this.userManagement.Click += new System.EventHandler(this.userManagement_Click);
            // 
            // ToolManagement
            // 
            this.ToolManagement.Name = "ToolManagement";
            this.ToolManagement.Size = new System.Drawing.Size(81, 24);
            this.ToolManagement.Text = "模块管理";
            this.ToolManagement.Click += new System.EventHandler(this.ToolManagement_Click);
            // 
            // toolExit
            // 
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(51, 24);
            this.toolExit.Text = "退出";
            this.toolExit.Click += new System.EventHandler(this.TSMenuItem5_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 399);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(786, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(99, 20);
            this.StatusLabel1.Text = "登录用户名：";
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(84, 20);
            this.StatusLabel2.Text = "登录时间：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "计算器";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 424);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "人事工资管理系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolHuman;
        private System.Windows.Forms.ToolStripMenuItem toolSalaryManagemeng;
        private System.Windows.Forms.ToolStripMenuItem toolSysManagement;
        private System.Windows.Forms.ToolStripMenuItem toolUser;
        private System.Windows.Forms.ToolStripMenuItem toolExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem toolUserFileManagement;
        private System.Windows.Forms.ToolStripMenuItem toolRewardPunishManagement;
        private System.Windows.Forms.ToolStripMenuItem toolUserReansferManagement;
        private System.Windows.Forms.ToolStripMenuItem toolEvaluateManagement;
        private System.Windows.Forms.ToolStripMenuItem toolAttendanceManagement;
        private System.Windows.Forms.ToolStripMenuItem toolSalarySummarize;
        private System.Windows.Forms.ToolStripMenuItem depManagement;
        private System.Windows.Forms.ToolStripMenuItem toolDataBackup;
        private System.Windows.Forms.ToolStripMenuItem OperatorManagement;
        private System.Windows.Forms.ToolStripMenuItem OperatorChange;
        private System.Windows.Forms.ToolStripMenuItem userManagement;
        private System.Windows.Forms.ToolStripMenuItem ToolManagement;
        private System.Windows.Forms.ToolStripMenuItem rightManagement;
        private System.Windows.Forms.Button button1;
    }
}