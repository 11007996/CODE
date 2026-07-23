namespace PMS
{
    partial class frmUserManagement
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
            this.toolQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolRecover = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolDel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMod = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolCan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolQuery,
            this.ToolAdd,
            this.ToolCancel,
            this.ToolRecover,
            this.ToolDel,
            this.ToolMod,
            this.ToolRefresh,
            this.toolCan,
            this.ToolExport,
            this.ToolExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(609, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolQuery
            // 
            this.toolQuery.Name = "toolQuery";
            this.toolQuery.Size = new System.Drawing.Size(51, 24);
            this.toolQuery.Text = "查询";
            this.toolQuery.Click += new System.EventHandler(this.toolQuery_Click);
            // 
            // ToolAdd
            // 
            this.ToolAdd.Name = "ToolAdd";
            this.ToolAdd.Size = new System.Drawing.Size(51, 24);
            this.ToolAdd.Text = "新增";
            this.ToolAdd.Click += new System.EventHandler(this.ToolAdd_Click);
            // 
            // ToolCancel
            // 
            this.ToolCancel.Name = "ToolCancel";
            this.ToolCancel.Size = new System.Drawing.Size(51, 24);
            this.ToolCancel.Text = "作废";
            this.ToolCancel.Click += new System.EventHandler(this.ToolCancel_Click);
            // 
            // ToolRecover
            // 
            this.ToolRecover.Name = "ToolRecover";
            this.ToolRecover.Size = new System.Drawing.Size(51, 24);
            this.ToolRecover.Text = "恢复";
            this.ToolRecover.Click += new System.EventHandler(this.ToolRecover_Click);
            // 
            // ToolDel
            // 
            this.ToolDel.Name = "ToolDel";
            this.ToolDel.Size = new System.Drawing.Size(51, 24);
            this.ToolDel.Text = "删除";
            this.ToolDel.Click += new System.EventHandler(this.ToolDel_Click);
            // 
            // ToolMod
            // 
            this.ToolMod.Name = "ToolMod";
            this.ToolMod.Size = new System.Drawing.Size(81, 24);
            this.ToolMod.Text = "修改密码";
            this.ToolMod.Click += new System.EventHandler(this.ToolMod_Click);
            // 
            // ToolRefresh
            // 
            this.ToolRefresh.Name = "ToolRefresh";
            this.ToolRefresh.Size = new System.Drawing.Size(51, 24);
            this.ToolRefresh.Text = "刷新";
            this.ToolRefresh.Click += new System.EventHandler(this.ToolRefresh_Click);
            // 
            // ToolExport
            // 
            this.ToolExport.Name = "ToolExport";
            this.ToolExport.Size = new System.Drawing.Size(51, 24);
            this.ToolExport.Text = "导出";
            this.ToolExport.Click += new System.EventHandler(this.ToolExport_Click);
            // 
            // ToolExit
            // 
            this.ToolExit.Name = "ToolExit";
            this.ToolExit.Size = new System.Drawing.Size(51, 24);
            this.ToolExit.Text = "退出";
            this.ToolExit.Click += new System.EventHandler(this.ToolExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "原密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "新密码";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(99, 43);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(166, 25);
            this.txtUserId.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(359, 43);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(166, 25);
            this.txtUserName.TabIndex = 2;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(99, 77);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(166, 25);
            this.txtOldPassword.TabIndex = 2;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(359, 77);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(166, 25);
            this.txtNewPassword.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 119);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(585, 260);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // toolCan
            // 
            this.toolCan.Name = "toolCan";
            this.toolCan.Size = new System.Drawing.Size(51, 24);
            this.toolCan.Text = "取消";
            this.toolCan.Click += new System.EventHandler(this.toolCan_Click);
            // 
            // frmUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 391);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmUserManagement";
            this.Text = "frmUserManagement";
            this.Load += new System.EventHandler(this.frmUserManagement_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolQuery;
        private System.Windows.Forms.ToolStripMenuItem ToolAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolCancel;
        private System.Windows.Forms.ToolStripMenuItem ToolRecover;
        private System.Windows.Forms.ToolStripMenuItem ToolDel;
        private System.Windows.Forms.ToolStripMenuItem ToolMod;
        private System.Windows.Forms.ToolStripMenuItem ToolExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem ToolRefresh;
        private System.Windows.Forms.ToolStripMenuItem ToolExport;
        private System.Windows.Forms.ToolStripMenuItem toolCan;
    }
}