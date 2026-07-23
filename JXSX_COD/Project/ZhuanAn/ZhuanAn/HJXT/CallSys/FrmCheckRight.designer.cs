namespace CallSys
{
    partial class FrmCheckRight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckRight));
            this.labHandlerNo = new System.Windows.Forms.Label();
            this.labHandlerPwd = new System.Windows.Forms.Label();
            this.tbHandlerNo = new System.Windows.Forms.TextBox();
            this.tbHandlerPwd = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.labMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labHandlerNo
            // 
            this.labHandlerNo.AutoSize = true;
            this.labHandlerNo.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerNo.Location = new System.Drawing.Point(67, 48);
            this.labHandlerNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerNo.Name = "labHandlerNo";
            this.labHandlerNo.Size = new System.Drawing.Size(52, 15);
            this.labHandlerNo.TabIndex = 0;
            this.labHandlerNo.Text = "工号：";
            // 
            // labHandlerPwd
            // 
            this.labHandlerPwd.AutoSize = true;
            this.labHandlerPwd.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerPwd.Location = new System.Drawing.Point(67, 111);
            this.labHandlerPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerPwd.Name = "labHandlerPwd";
            this.labHandlerPwd.Size = new System.Drawing.Size(52, 15);
            this.labHandlerPwd.TabIndex = 1;
            this.labHandlerPwd.Text = "密码：";
            // 
            // tbHandlerNo
            // 
            this.tbHandlerNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbHandlerNo.Location = new System.Drawing.Point(129, 45);
            this.tbHandlerNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerNo.Name = "tbHandlerNo";
            this.tbHandlerNo.Size = new System.Drawing.Size(193, 25);
            this.tbHandlerNo.TabIndex = 2;
            // 
            // tbHandlerPwd
            // 
            this.tbHandlerPwd.Location = new System.Drawing.Point(129, 108);
            this.tbHandlerPwd.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerPwd.Name = "tbHandlerPwd";
            this.tbHandlerPwd.PasswordChar = '●';
            this.tbHandlerPwd.Size = new System.Drawing.Size(193, 25);
            this.tbHandlerPwd.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(264, 231);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 29);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.BackColor = System.Drawing.Color.Transparent;
            this.labMessage.Location = new System.Drawing.Point(127, 11);
            this.labMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 15);
            this.labMessage.TabIndex = 7;
            // 
            // FrmCheckRight
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(396, 294);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbHandlerPwd);
            this.Controls.Add(this.tbHandlerNo);
            this.Controls.Add(this.labHandlerPwd);
            this.Controls.Add(this.labHandlerNo);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCheckRight";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登入";
            this.Load += new System.EventHandler(this.FrmCheckRight_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labHandlerNo;
        private System.Windows.Forms.Label labHandlerPwd;
        private System.Windows.Forms.TextBox tbHandlerNo;
        private System.Windows.Forms.TextBox tbHandlerPwd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label labMessage;
    }
}