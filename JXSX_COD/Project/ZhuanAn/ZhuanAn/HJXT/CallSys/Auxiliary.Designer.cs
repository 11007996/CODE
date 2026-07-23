namespace CallSys
{
    partial class Auxiliary
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auxiliary));
            this.timerFrmTopMost = new System.Windows.Forms.Timer(this.components);
            this.timerClockTime = new System.Windows.Forms.Timer(this.components);
            this.panelClock = new System.Windows.Forms.Panel();
            this.panelOperate = new System.Windows.Forms.Panel();
            this.pbCall = new System.Windows.Forms.PictureBox();
            this.btnBaseInfo = new System.Windows.Forms.Button();
            this.btnKanBan = new System.Windows.Forms.Button();
            this.labTimeVal = new System.Windows.Forms.Label();
            this.labTimeUnit = new System.Windows.Forms.Label();
            this.pbTimer = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCloseCurrFrm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSwitchCurrFrmShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmsSwitchFloatFrmShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUploadSysFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerCheckDBConn = new System.Windows.Forms.Timer(this.components);
            this.panelClock.SuspendLayout();
            this.panelOperate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTimer)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerFrmTopMost
            // 
            this.timerFrmTopMost.Tick += new System.EventHandler(this.timerFrmTopMost_Tick);
            // 
            // timerClockTime
            // 
            this.timerClockTime.Enabled = true;
            this.timerClockTime.Interval = 1000;
            this.timerClockTime.Tick += new System.EventHandler(this.timerClockTime_Tick);
            // 
            // panelClock
            // 
            this.panelClock.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelClock.BackgroundImage")));
            this.panelClock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelClock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelClock.Controls.Add(this.panelOperate);
            this.panelClock.Controls.Add(this.labTimeVal);
            this.panelClock.Controls.Add(this.labTimeUnit);
            this.panelClock.Controls.Add(this.pbTimer);
            this.panelClock.Location = new System.Drawing.Point(-17, -12);
            this.panelClock.Margin = new System.Windows.Forms.Padding(4);
            this.panelClock.Name = "panelClock";
            this.panelClock.Size = new System.Drawing.Size(196, 183);
            this.panelClock.TabIndex = 3;
            this.panelClock.Click += new System.EventHandler(this.contextMenuStrip_Show);
            this.panelClock.DoubleClick += new System.EventHandler(this.panelClock_DoubleClick);
            this.panelClock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // panelOperate
            // 
            this.panelOperate.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelOperate.Controls.Add(this.pbCall);
            this.panelOperate.Controls.Add(this.btnBaseInfo);
            this.panelOperate.Controls.Add(this.btnKanBan);
            this.panelOperate.Location = new System.Drawing.Point(7, 12);
            this.panelOperate.Margin = new System.Windows.Forms.Padding(4);
            this.panelOperate.Name = "panelOperate";
            this.panelOperate.Size = new System.Drawing.Size(181, 154);
            this.panelOperate.TabIndex = 3;
            this.panelOperate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // pbCall
            // 
            this.pbCall.Image = ((System.Drawing.Image)(resources.GetObject("pbCall.Image")));
            this.pbCall.Location = new System.Drawing.Point(44, 31);
            this.pbCall.Margin = new System.Windows.Forms.Padding(4);
            this.pbCall.Name = "pbCall";
            this.pbCall.Size = new System.Drawing.Size(93, 88);
            this.pbCall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCall.TabIndex = 18;
            this.pbCall.TabStop = false;
            this.toolTip1.SetToolTip(this.pbCall, "救助");
            this.pbCall.Click += new System.EventHandler(this.pbCall_Click);
            this.pbCall.MouseEnter += new System.EventHandler(this.pbCall_MouseEnter);
            this.pbCall.MouseLeave += new System.EventHandler(this.pbCall_MouseLeave);
            this.pbCall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // btnBaseInfo
            // 
            this.btnBaseInfo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBaseInfo.Location = new System.Drawing.Point(96, -9);
            this.btnBaseInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnBaseInfo.Name = "btnBaseInfo";
            this.btnBaseInfo.Size = new System.Drawing.Size(80, 171);
            this.btnBaseInfo.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnBaseInfo, "基本信息");
            this.btnBaseInfo.UseVisualStyleBackColor = false;
            this.btnBaseInfo.Click += new System.EventHandler(this.btnBaseInfo_Click);
            this.btnBaseInfo.MouseEnter += new System.EventHandler(this.btnBaseInfo_MouseEnter);
            this.btnBaseInfo.MouseLeave += new System.EventHandler(this.btnBaseInfo_MouseLeave);
            this.btnBaseInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // btnKanBan
            // 
            this.btnKanBan.BackColor = System.Drawing.Color.IndianRed;
            this.btnKanBan.Location = new System.Drawing.Point(9, -9);
            this.btnKanBan.Margin = new System.Windows.Forms.Padding(4);
            this.btnKanBan.Name = "btnKanBan";
            this.btnKanBan.Size = new System.Drawing.Size(80, 171);
            this.btnKanBan.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnKanBan, "看板");
            this.btnKanBan.UseVisualStyleBackColor = false;
            this.btnKanBan.Click += new System.EventHandler(this.btnKanBan_Click);
            this.btnKanBan.MouseEnter += new System.EventHandler(this.btnKanBan_MouseEnter);
            this.btnKanBan.MouseLeave += new System.EventHandler(this.btnKanBan_MouseLeave);
            this.btnKanBan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // labTimeVal
            // 
            this.labTimeVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labTimeVal.BackColor = System.Drawing.Color.Transparent;
            this.labTimeVal.Font = new System.Drawing.Font("宋体", 23.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTimeVal.ForeColor = System.Drawing.Color.Blue;
            this.labTimeVal.Location = new System.Drawing.Point(53, 55);
            this.labTimeVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTimeVal.Name = "labTimeVal";
            this.labTimeVal.Size = new System.Drawing.Size(85, 39);
            this.labTimeVal.TabIndex = 2;
            this.labTimeVal.Text = "1";
            this.labTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTimeVal.Click += new System.EventHandler(this.contextMenuStrip_Show);
            this.labTimeVal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // labTimeUnit
            // 
            this.labTimeUnit.AutoSize = true;
            this.labTimeUnit.BackColor = System.Drawing.Color.Transparent;
            this.labTimeUnit.ForeColor = System.Drawing.Color.Fuchsia;
            this.labTimeUnit.Location = new System.Drawing.Point(80, 104);
            this.labTimeUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTimeUnit.Name = "labTimeUnit";
            this.labTimeUnit.Size = new System.Drawing.Size(37, 15);
            this.labTimeUnit.TabIndex = 1;
            this.labTimeUnit.Text = "秒钟";
            this.labTimeUnit.Click += new System.EventHandler(this.contextMenuStrip_Show);
            this.labTimeUnit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // pbTimer
            // 
            this.pbTimer.BackgroundImage = global::CallSys.Properties.Resources.clock_01;
            this.pbTimer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbTimer.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pbTimer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbTimer.Location = new System.Drawing.Point(0, 0);
            this.pbTimer.Margin = new System.Windows.Forms.Padding(4);
            this.pbTimer.Name = "pbTimer";
            this.pbTimer.Size = new System.Drawing.Size(192, 179);
            this.pbTimer.TabIndex = 4;
            this.pbTimer.TabStop = false;
            this.pbTimer.Click += new System.EventHandler(this.contextMenuStrip_Show);
            this.pbTimer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.contextMenuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("contextMenuStrip.BackgroundImage")));
            this.contextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCloseCurrFrm,
            this.tsmiSwitchCurrFrmShow,
            this.tsmiUserLogin,
            this.tsmsSwitchFloatFrmShow,
            this.tsmiUploadSysFile,
            this.tsmiSystemConfig,
            this.tsmiExitApp});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.Size = new System.Drawing.Size(188, 214);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // tsmiCloseCurrFrm
            // 
            this.tsmiCloseCurrFrm.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCloseCurrFrm.Image")));
            this.tsmiCloseCurrFrm.Name = "tsmiCloseCurrFrm";
            this.tsmiCloseCurrFrm.Size = new System.Drawing.Size(187, 26);
            this.tsmiCloseCurrFrm.Text = "关闭当前窗体";
            this.tsmiCloseCurrFrm.Click += new System.EventHandler(this.tsmiCloseCurrFrm_Click);
            // 
            // tsmiSwitchCurrFrmShow
            // 
            this.tsmiSwitchCurrFrmShow.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSwitchCurrFrmShow.Image")));
            this.tsmiSwitchCurrFrmShow.Name = "tsmiSwitchCurrFrmShow";
            this.tsmiSwitchCurrFrmShow.Size = new System.Drawing.Size(187, 26);
            this.tsmiSwitchCurrFrmShow.Text = "最小化当前窗体";
            this.tsmiSwitchCurrFrmShow.Click += new System.EventHandler(this.tsmiSwitchCurrFrmShow_Click);
            // 
            // tsmiUserLogin
            // 
            this.tsmiUserLogin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUserLogin.Image")));
            this.tsmiUserLogin.Name = "tsmiUserLogin";
            this.tsmiUserLogin.Size = new System.Drawing.Size(187, 26);
            this.tsmiUserLogin.Text = "账号登入";
            this.tsmiUserLogin.Click += new System.EventHandler(this.tsmiUserLogin_Click);
            // 
            // tsmsSwitchFloatFrmShow
            // 
            this.tsmsSwitchFloatFrmShow.Image = ((System.Drawing.Image)(resources.GetObject("tsmsSwitchFloatFrmShow.Image")));
            this.tsmsSwitchFloatFrmShow.Name = "tsmsSwitchFloatFrmShow";
            this.tsmsSwitchFloatFrmShow.Size = new System.Drawing.Size(187, 26);
            this.tsmsSwitchFloatFrmShow.Text = "隐藏浮窗";
            this.tsmsSwitchFloatFrmShow.Click += new System.EventHandler(this.tsmsSwitchFloatFrmShow_Click);
            // 
            // tsmiUploadSysFile
            // 
            this.tsmiUploadSysFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUploadSysFile.Image")));
            this.tsmiUploadSysFile.Name = "tsmiUploadSysFile";
            this.tsmiUploadSysFile.Size = new System.Drawing.Size(187, 26);
            this.tsmiUploadSysFile.Text = "上传更新";
            this.tsmiUploadSysFile.Visible = false;
            this.tsmiUploadSysFile.Click += new System.EventHandler(this.tsmiUploadSysFile_Click);
            // 
            // tsmiSystemConfig
            // 
            this.tsmiSystemConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSystemConfig.Image")));
            this.tsmiSystemConfig.Name = "tsmiSystemConfig";
            this.tsmiSystemConfig.Size = new System.Drawing.Size(187, 26);
            this.tsmiSystemConfig.Text = "系统设置";
            this.tsmiSystemConfig.Click += new System.EventHandler(this.tsmiSystemConfig_Click);
            // 
            // tsmiExitApp
            // 
            this.tsmiExitApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmiExitApp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsmiExitApp.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExitApp.Image")));
            this.tsmiExitApp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiExitApp.Name = "tsmiExitApp";
            this.tsmiExitApp.Size = new System.Drawing.Size(187, 26);
            this.tsmiExitApp.Text = "退出程序";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "呼叫系统";
            this.notifyIcon.Visible = true;
            // 
            // timerCheckDBConn
            // 
            this.timerCheckDBConn.Enabled = true;
            this.timerCheckDBConn.Interval = 60000;
            this.timerCheckDBConn.Tick += new System.EventHandler(this.timerCheckDBConn_Tick);
            // 
            // Auxiliary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 158);
            this.Controls.Add(this.panelClock);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Auxiliary";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "浮窗";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Auxiliary_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Auxiliary_MouseMove);
            this.panelClock.ResumeLayout(false);
            this.panelClock.PerformLayout();
            this.panelOperate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTimer)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerFrmTopMost;
        private System.Windows.Forms.Timer timerClockTime;
        private System.Windows.Forms.Panel panelClock;
        private System.Windows.Forms.Label labTimeUnit;
        private System.Windows.Forms.Label labTimeVal;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseCurrFrm;
        private System.Windows.Forms.ToolStripMenuItem tsmiSwitchCurrFrmShow;
        private System.Windows.Forms.Panel panelOperate;
        private System.Windows.Forms.PictureBox pbCall;
        private System.Windows.Forms.Button btnBaseInfo;
        private System.Windows.Forms.Button btnKanBan;
        private System.Windows.Forms.ToolStripMenuItem tsmsSwitchFloatFrmShow;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.PictureBox pbTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemConfig;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadSysFile;
        private System.Windows.Forms.Timer timerCheckDBConn;
    }
}

