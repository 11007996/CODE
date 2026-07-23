namespace TCPclient
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.butconnect = new System.Windows.Forms.Button();
            this.butdiscon = new System.Windows.Forms.Button();
            this.ritxtsend = new System.Windows.Forms.RichTextBox();
            this.butsend = new System.Windows.Forms.Button();
            this.riTxt = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(12, 12);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(173, 25);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "172.17.144.1:51888";
            // 
            // butconnect
            // 
            this.butconnect.Location = new System.Drawing.Point(204, 12);
            this.butconnect.Name = "butconnect";
            this.butconnect.Size = new System.Drawing.Size(69, 25);
            this.butconnect.TabIndex = 1;
            this.butconnect.Text = "连接";
            this.butconnect.UseVisualStyleBackColor = true;
            this.butconnect.Click += new System.EventHandler(this.butconnect_Click);
            // 
            // butdiscon
            // 
            this.butdiscon.Location = new System.Drawing.Point(291, 12);
            this.butdiscon.Name = "butdiscon";
            this.butdiscon.Size = new System.Drawing.Size(69, 25);
            this.butdiscon.TabIndex = 1;
            this.butdiscon.Text = "离开";
            this.butdiscon.UseVisualStyleBackColor = true;
            this.butdiscon.Click += new System.EventHandler(this.butdiscon_Click);
            // 
            // ritxtsend
            // 
            this.ritxtsend.Location = new System.Drawing.Point(12, 43);
            this.ritxtsend.Name = "ritxtsend";
            this.ritxtsend.Size = new System.Drawing.Size(437, 63);
            this.ritxtsend.TabIndex = 2;
            this.ritxtsend.Text = "";
            // 
            // butsend
            // 
            this.butsend.Location = new System.Drawing.Point(380, 12);
            this.butsend.Name = "butsend";
            this.butsend.Size = new System.Drawing.Size(69, 25);
            this.butsend.TabIndex = 1;
            this.butsend.Text = "发送";
            this.butsend.UseVisualStyleBackColor = true;
            this.butsend.Click += new System.EventHandler(this.butsend_Click);
            // 
            // riTxt
            // 
            this.riTxt.Location = new System.Drawing.Point(12, 112);
            this.riTxt.Name = "riTxt";
            this.riTxt.Size = new System.Drawing.Size(437, 186);
            this.riTxt.TabIndex = 2;
            this.riTxt.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 327);
            this.Controls.Add(this.riTxt);
            this.Controls.Add(this.ritxtsend);
            this.Controls.Add(this.butsend);
            this.Controls.Add(this.butdiscon);
            this.Controls.Add(this.butconnect);
            this.Controls.Add(this.txtIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button butconnect;
        private System.Windows.Forms.Button butdiscon;
        private System.Windows.Forms.RichTextBox ritxtsend;
        private System.Windows.Forms.Button butsend;
        private System.Windows.Forms.RichTextBox riTxt;
    }
}

