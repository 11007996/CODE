namespace EAM.Listen
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMqttDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHttpDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTcpDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSerialPort = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.timerInitStatus = new System.Windows.Forms.Timer(this.components);
            this.timerLoadConfig = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "呼叫系统";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.contextMenuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("contextMenuStrip.BackgroundImage")));
            this.contextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMqttDebug,
            this.tsmiHttpDebug,
            this.tsmiTcpDebug,
            this.tsmiSerialPort,
            this.tsmiUserLogin,
            this.tsmiSystemConfig,
            this.tsmiExitApp});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.Size = new System.Drawing.Size(215, 214);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // tsmiMqttDebug
            // 
            this.tsmiMqttDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMqttDebug.Image")));
            this.tsmiMqttDebug.Name = "tsmiMqttDebug";
            this.tsmiMqttDebug.Size = new System.Drawing.Size(214, 26);
            this.tsmiMqttDebug.Text = "Mqtt客户端";
            this.tsmiMqttDebug.Click += new System.EventHandler(this.tsmiMqttClient_Click);
            // 
            // tsmiHttpDebug
            // 
            this.tsmiHttpDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsmiHttpDebug.Image")));
            this.tsmiHttpDebug.Name = "tsmiHttpDebug";
            this.tsmiHttpDebug.Size = new System.Drawing.Size(214, 26);
            this.tsmiHttpDebug.Text = "Http监听";
            this.tsmiHttpDebug.Click += new System.EventHandler(this.tsmiHttpDebug_Click);
            // 
            // tsmiTcpDebug
            // 
            this.tsmiTcpDebug.Image = ((System.Drawing.Image)(resources.GetObject("tsmiTcpDebug.Image")));
            this.tsmiTcpDebug.Name = "tsmiTcpDebug";
            this.tsmiTcpDebug.Size = new System.Drawing.Size(214, 26);
            this.tsmiTcpDebug.Text = "TCP监听";
            this.tsmiTcpDebug.Click += new System.EventHandler(this.tsmiTcpListen_Click);
            // 
            // tsmiSerialPort
            // 
            this.tsmiSerialPort.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSerialPort.Image")));
            this.tsmiSerialPort.Name = "tsmiSerialPort";
            this.tsmiSerialPort.Size = new System.Drawing.Size(214, 26);
            this.tsmiSerialPort.Text = "串口监听";
            this.tsmiSerialPort.Click += new System.EventHandler(this.tsmiSerialPort_Click);
            // 
            // tsmiUserLogin
            // 
            this.tsmiUserLogin.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUserLogin.Image")));
            this.tsmiUserLogin.Name = "tsmiUserLogin";
            this.tsmiUserLogin.Size = new System.Drawing.Size(214, 26);
            this.tsmiUserLogin.Text = "账号登入";
            this.tsmiUserLogin.Click += new System.EventHandler(this.tsmiUserLogin_Click);
            // 
            // tsmiSystemConfig
            // 
            this.tsmiSystemConfig.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSystemConfig.Image")));
            this.tsmiSystemConfig.Name = "tsmiSystemConfig";
            this.tsmiSystemConfig.Size = new System.Drawing.Size(214, 26);
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
            this.tsmiExitApp.Size = new System.Drawing.Size(214, 26);
            this.tsmiExitApp.Text = "退出程序";
            this.tsmiExitApp.Click += new System.EventHandler(this.tsmiExitApp_Click);
            // 
            // timerInitStatus
            // 
            this.timerInitStatus.Enabled = true;
            this.timerInitStatus.Interval = 60000;
            this.timerInitStatus.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerLoadConfig
            // 
            this.timerLoadConfig.Enabled = true;
            this.timerLoadConfig.Interval = 60000;
            this.timerLoadConfig.Tick += new System.EventHandler(this.timerLoadConfig_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(157, 157);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "浮窗";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmiExitApp;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemConfig;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiTcpDebug;
        private System.Windows.Forms.ToolStripMenuItem tsmiSerialPort;
        private System.Windows.Forms.Timer timerInitStatus;
        private System.Windows.Forms.Timer timerLoadConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiMqttDebug;
        private System.Windows.Forms.ToolStripMenuItem tsmiHttpDebug;
    }
}

