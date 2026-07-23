namespace LuxMMS
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
            this.labUserNo = new System.Windows.Forms.Label();
            this.labPwd = new System.Windows.Forms.Label();
            this.tbUserNo = new System.Windows.Forms.TextBox();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.labMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labUserNo
            // 
            this.labUserNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserNo.AutoSize = true;
            this.labUserNo.BackColor = System.Drawing.Color.Transparent;
            this.labUserNo.Font = new System.Drawing.Font("宋体", 12F);
            this.labUserNo.Location = new System.Drawing.Point(27, 55);
            this.labUserNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labUserNo.Name = "labUserNo";
            this.labUserNo.Size = new System.Drawing.Size(49, 20);
            this.labUserNo.TabIndex = 0;
            this.labUserNo.Text = "工号";
            // 
            // labPwd
            // 
            this.labPwd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPwd.AutoSize = true;
            this.labPwd.BackColor = System.Drawing.Color.Transparent;
            this.labPwd.Font = new System.Drawing.Font("宋体", 12F);
            this.labPwd.Location = new System.Drawing.Point(27, 124);
            this.labPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(49, 20);
            this.labPwd.TabIndex = 1;
            this.labPwd.Text = "密码";
            // 
            // tbUserNo
            // 
            this.tbUserNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbUserNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbUserNo.Font = new System.Drawing.Font("宋体", 12F);
            this.tbUserNo.Location = new System.Drawing.Point(84, 50);
            this.tbUserNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbUserNo.Name = "tbUserNo";
            this.tbUserNo.Size = new System.Drawing.Size(268, 30);
            this.tbUserNo.TabIndex = 2;
            // 
            // tbPwd
            // 
            this.tbPwd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPwd.Font = new System.Drawing.Font("宋体", 12F);
            this.tbPwd.Location = new System.Drawing.Point(84, 119);
            this.tbPwd.Margin = new System.Windows.Forms.Padding(4);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.PasswordChar = '●';
            this.tbPwd.Size = new System.Drawing.Size(268, 30);
            this.tbPwd.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnOk.Location = new System.Drawing.Point(252, 188);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 42);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.labMessage.BackColor = System.Drawing.Color.Transparent;
            this.labMessage.Location = new System.Drawing.Point(84, 7);
            this.labMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 7;
            this.labMessage.Text = "提示";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labUserNo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbUserNo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnOk, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labPwd, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbPwd, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 250);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // FrmCheckRight
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(396, 290);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmCheckRight";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登入";
            this.Load += new System.EventHandler(this.FrmCheckRight_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labUserNo;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.TextBox tbUserNo;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}