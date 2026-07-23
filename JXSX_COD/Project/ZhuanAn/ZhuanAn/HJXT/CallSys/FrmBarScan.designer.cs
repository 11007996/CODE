namespace CallSys
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
            this.tbHandlerNo = new System.Windows.Forms.TextBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.pbScan = new System.Windows.Forms.PictureBox();
            this.labHandlerNo = new System.Windows.Forms.Label();
            this.labMachine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbScan)).BeginInit();
            this.SuspendLayout();
            // 
            // tbHandlerNo
            // 
            this.tbHandlerNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbHandlerNo.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbHandlerNo.Location = new System.Drawing.Point(163, 2);
            this.tbHandlerNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerNo.Name = "tbHandlerNo";
            this.tbHandlerNo.Size = new System.Drawing.Size(216, 34);
            this.tbHandlerNo.TabIndex = 0;
            this.tbHandlerNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbHandlerNo_KeyPress);
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.BackColor = System.Drawing.Color.White;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(78, 388);
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
            this.pbScan.Size = new System.Drawing.Size(376, 374);
            this.pbScan.TabIndex = 1;
            this.pbScan.TabStop = false;
            this.pbScan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pbScan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // labHandlerNo
            // 
            this.labHandlerNo.AutoSize = true;
            this.labHandlerNo.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labHandlerNo.ForeColor = System.Drawing.Color.White;
            this.labHandlerNo.Location = new System.Drawing.Point(12, 5);
            this.labHandlerNo.Name = "labHandlerNo";
            this.labHandlerNo.Size = new System.Drawing.Size(135, 24);
            this.labHandlerNo.TabIndex = 5;
            this.labHandlerNo.Text = "工号输入：";
            // 
            // labMachine
            // 
            this.labMachine.AutoSize = true;
            this.labMachine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMachine.ForeColor = System.Drawing.Color.White;
            this.labMachine.Location = new System.Drawing.Point(9, 425);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(69, 19);
            this.labMachine.TabIndex = 6;
            this.labMachine.Text = "机台：";
            // 
            // FrmBarScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(391, 453);
            this.Controls.Add(this.labMachine);
            this.Controls.Add(this.labHandlerNo);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.pbScan);
            this.Controls.Add(this.tbHandlerNo);
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

        private System.Windows.Forms.TextBox tbHandlerNo;
        private System.Windows.Forms.PictureBox pbScan;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Label labHandlerNo;
        private System.Windows.Forms.Label labMachine;
    }
}