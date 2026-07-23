namespace LuxMMS
{
    partial class FrmConfigForSysSet
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labHadnlerPicDir = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.swBtnAutoUpdate = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.tbUserPicDir = new System.Windows.Forms.TextBox();
            this.btnFolderBrowser = new System.Windows.Forms.Button();
            this.labAutoUpdate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.labHadnlerPicDir, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.swBtnAutoUpdate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbUserPicDir, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFolderBrowser, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labAutoUpdate, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 190);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // labHadnlerPicDir
            // 
            this.labHadnlerPicDir.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labHadnlerPicDir.AutoSize = true;
            this.labHadnlerPicDir.Location = new System.Drawing.Point(6, 12);
            this.labHadnlerPicDir.Name = "labHadnlerPicDir";
            this.labHadnlerPicDir.Size = new System.Drawing.Size(67, 15);
            this.labHadnlerPicDir.TabIndex = 4;
            this.labHadnlerPicDir.Text = "头像缓存";
            this.labHadnlerPicDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 106);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "更新检查机制";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(3, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(294, 36);
            this.label5.TabIndex = 2;
            this.label5.Text = "（3）每日8:30[如打开了看板或呼叫面板,则需要处理完成后才执行更新]";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(3, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(294, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "（2）呼叫面板呼叫按钮";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(3, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "（1）应用启动";
            // 
            // swBtnAutoUpdate
            // 
            this.swBtnAutoUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.swBtnAutoUpdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.swBtnAutoUpdate.Location = new System.Drawing.Point(79, 49);
            this.swBtnAutoUpdate.Name = "swBtnAutoUpdate";
            this.swBtnAutoUpdate.OffText = "关闭";
            this.swBtnAutoUpdate.OnText = "开启";
            this.swBtnAutoUpdate.Size = new System.Drawing.Size(66, 22);
            this.swBtnAutoUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.swBtnAutoUpdate.TabIndex = 11;
            // 
            // tbUserPicDir
            // 
            this.tbUserPicDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbUserPicDir.Enabled = false;
            this.tbUserPicDir.Location = new System.Drawing.Point(79, 7);
            this.tbUserPicDir.Name = "tbUserPicDir";
            this.tbUserPicDir.Size = new System.Drawing.Size(147, 25);
            this.tbUserPicDir.TabIndex = 5;
            // 
            // btnFolderBrowser
            // 
            this.btnFolderBrowser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFolderBrowser.AutoSize = true;
            this.btnFolderBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnFolderBrowser.Location = new System.Drawing.Point(232, 5);
            this.btnFolderBrowser.Name = "btnFolderBrowser";
            this.btnFolderBrowser.Size = new System.Drawing.Size(47, 30);
            this.btnFolderBrowser.TabIndex = 6;
            this.btnFolderBrowser.Text = "选择";
            this.btnFolderBrowser.UseVisualStyleBackColor = false;
            this.btnFolderBrowser.Click += new System.EventHandler(this.btnFolderBrowser_Click);
            // 
            // labAutoUpdate
            // 
            this.labAutoUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAutoUpdate.AutoSize = true;
            this.labAutoUpdate.Location = new System.Drawing.Point(6, 52);
            this.labAutoUpdate.Name = "labAutoUpdate";
            this.labAutoUpdate.Size = new System.Drawing.Size(67, 15);
            this.labAutoUpdate.TabIndex = 8;
            this.labAutoUpdate.Text = "自动更新";
            this.labAutoUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 223);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统设置";
            // 
            // FrmConfigForSysSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(312, 223);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfigForSysSet";
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfigForSysSet_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFolderBrowser;
        private System.Windows.Forms.TextBox tbUserPicDir;
        private System.Windows.Forms.Label labHadnlerPicDir;
        private System.Windows.Forms.Label labAutoUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.SwitchButton swBtnAutoUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}