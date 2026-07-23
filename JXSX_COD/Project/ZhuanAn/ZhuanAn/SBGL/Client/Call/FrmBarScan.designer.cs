namespace Call
{
    partial class FrmBarScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarScan));
            this.tbUserNo = new System.Windows.Forms.TextBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.pbScan = new System.Windows.Forms.PictureBox();
            this.labUserNo = new System.Windows.Forms.Label();
            this.labMachine = new System.Windows.Forms.Label();
            this.labState = new System.Windows.Forms.Label();
            this.labUser = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbScan)).BeginInit();
            this.SuspendLayout();
            // 
            // tbUserNo
            // 
            this.tbUserNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbUserNo.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUserNo.Location = new System.Drawing.Point(163, 2);
            this.tbUserNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbUserNo.Name = "tbUserNo";
            this.tbUserNo.Size = new System.Drawing.Size(216, 34);
            this.tbUserNo.TabIndex = 0;
            this.tbUserNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserNo_KeyPress);
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.BackColor = System.Drawing.Color.White;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(59, 51);
            this.labMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 15);
            this.labMessage.TabIndex = 2;
            // 
            // pbScan
            // 
            this.pbScan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbScan.BackgroundImage")));
            this.pbScan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbScan.Location = new System.Drawing.Point(6, 38);
            this.pbScan.Margin = new System.Windows.Forms.Padding(4);
            this.pbScan.Name = "pbScan";
            this.pbScan.Size = new System.Drawing.Size(376, 371);
            this.pbScan.TabIndex = 1;
            this.pbScan.TabStop = false;
            this.pbScan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pbScan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // labUserNo
            // 
            this.labUserNo.AutoSize = true;
            this.labUserNo.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUserNo.ForeColor = System.Drawing.Color.White;
            this.labUserNo.Location = new System.Drawing.Point(12, 5);
            this.labUserNo.Name = "labUserNo";
            this.labUserNo.Size = new System.Drawing.Size(130, 23);
            this.labUserNo.TabIndex = 5;
            this.labUserNo.Text = "工号输入：";
            // 
            // labMachine
            // 
            this.labMachine.AutoSize = true;
            this.labMachine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMachine.ForeColor = System.Drawing.Color.White;
            this.labMachine.Location = new System.Drawing.Point(9, 425);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(65, 18);
            this.labMachine.TabIndex = 6;
            this.labMachine.Text = "机台：";
            // 
            // labState
            // 
            this.labState.AutoSize = true;
            this.labState.BackColor = System.Drawing.Color.White;
            this.labState.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labState.ForeColor = System.Drawing.Color.Black;
            this.labState.Location = new System.Drawing.Point(9, 384);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(65, 18);
            this.labState.TabIndex = 7;
            this.labState.Text = "阶段：";
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUser.ForeColor = System.Drawing.Color.White;
            this.labUser.Location = new System.Drawing.Point(223, 425);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(65, 18);
            this.labUser.TabIndex = 8;
            this.labUser.Text = "人员：";
            // 
            // FrmBarScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(391, 456);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.labState);
            this.Controls.Add(this.labMachine);
            this.Controls.Add(this.labUserNo);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.pbScan);
            this.Controls.Add(this.tbUserNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBarScan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请扫描条形码";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FrmBarScan_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBarScan_FormClosed);
            this.Load += new System.EventHandler(this.FrmBarScan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbScan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUserNo;
        private System.Windows.Forms.PictureBox pbScan;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Label labUserNo;
        private System.Windows.Forms.Label labMachine;
        private System.Windows.Forms.Label labState;
        private System.Windows.Forms.Label labUser;
    }
}