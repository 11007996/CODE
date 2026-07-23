namespace IPExample
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxLocalInfo = new System.Windows.Forms.ListBox();
            this.listBoxRemoteInfo = new System.Windows.Forms.ListBox();
            this.buttonLocalIP = new System.Windows.Forms.Button();
            this.buttonRemoteIP = new System.Windows.Forms.Button();
            this.testBoxRmoteIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBoxLocalInfo
            // 
            this.listBoxLocalInfo.FormattingEnabled = true;
            this.listBoxLocalInfo.ItemHeight = 15;
            this.listBoxLocalInfo.Location = new System.Drawing.Point(12, 12);
            this.listBoxLocalInfo.Name = "listBoxLocalInfo";
            this.listBoxLocalInfo.Size = new System.Drawing.Size(443, 424);
            this.listBoxLocalInfo.TabIndex = 0;
            // 
            // listBoxRemoteInfo
            // 
            this.listBoxRemoteInfo.FormattingEnabled = true;
            this.listBoxRemoteInfo.ItemHeight = 15;
            this.listBoxRemoteInfo.Location = new System.Drawing.Point(461, 12);
            this.listBoxRemoteInfo.Name = "listBoxRemoteInfo";
            this.listBoxRemoteInfo.Size = new System.Drawing.Size(511, 424);
            this.listBoxRemoteInfo.TabIndex = 0;
            // 
            // buttonLocalIP
            // 
            this.buttonLocalIP.Location = new System.Drawing.Point(37, 463);
            this.buttonLocalIP.Name = "buttonLocalIP";
            this.buttonLocalIP.Size = new System.Drawing.Size(139, 55);
            this.buttonLocalIP.TabIndex = 1;
            this.buttonLocalIP.Text = "显示本机IP信息";
            this.buttonLocalIP.UseVisualStyleBackColor = true;
            this.buttonLocalIP.Click += new System.EventHandler(this.buttonLocalIP_Click);
            // 
            // buttonRemoteIP
            // 
            this.buttonRemoteIP.Location = new System.Drawing.Point(795, 463);
            this.buttonRemoteIP.Name = "buttonRemoteIP";
            this.buttonRemoteIP.Size = new System.Drawing.Size(138, 55);
            this.buttonRemoteIP.TabIndex = 1;
            this.buttonRemoteIP.Text = "显示服务器信息";
            this.buttonRemoteIP.UseVisualStyleBackColor = true;
            this.buttonRemoteIP.Click += new System.EventHandler(this.buttonRemoteIP_Click);
            // 
            // testBoxRmoteIP
            // 
            this.testBoxRmoteIP.Location = new System.Drawing.Point(546, 480);
            this.testBoxRmoteIP.Name = "testBoxRmoteIP";
            this.testBoxRmoteIP.Size = new System.Drawing.Size(214, 25);
            this.testBoxRmoteIP.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(474, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "服务器";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 530);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.testBoxRmoteIP);
            this.Controls.Add(this.buttonRemoteIP);
            this.Controls.Add(this.buttonLocalIP);
            this.Controls.Add(this.listBoxRemoteInfo);
            this.Controls.Add(this.listBoxLocalInfo);
            this.Name = "Form1";
            this.Text = "IPExample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLocalInfo;
        private System.Windows.Forms.ListBox listBoxRemoteInfo;
        private System.Windows.Forms.Button buttonLocalIP;
        private System.Windows.Forms.Button buttonRemoteIP;
        private System.Windows.Forms.TextBox testBoxRmoteIP;
        private System.Windows.Forms.Label label1;
    }
}

