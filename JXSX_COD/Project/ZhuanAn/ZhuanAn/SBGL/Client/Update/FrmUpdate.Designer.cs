namespace Update
{
    partial class FrmUpdate
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
            this.proBar = new System.Windows.Forms.ProgressBar();
            this.labProgress = new System.Windows.Forms.Label();
            this.labDetail = new System.Windows.Forms.Label();
            this.labFileName = new System.Windows.Forms.Label();
            this.labScale = new System.Windows.Forms.Label();
            this.lsbUpdateDetail = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // proBar
            // 
            this.proBar.Location = new System.Drawing.Point(81, 38);
            this.proBar.MarqueeAnimationSpeed = 10;
            this.proBar.Name = "proBar";
            this.proBar.Size = new System.Drawing.Size(477, 23);
            this.proBar.TabIndex = 0;
            // 
            // labProgress
            // 
            this.labProgress.AutoSize = true;
            this.labProgress.Location = new System.Drawing.Point(28, 38);
            this.labProgress.Name = "labProgress";
            this.labProgress.Size = new System.Drawing.Size(37, 15);
            this.labProgress.TabIndex = 1;
            this.labProgress.Text = "进度";
            // 
            // labDetail
            // 
            this.labDetail.AutoSize = true;
            this.labDetail.Location = new System.Drawing.Point(28, 76);
            this.labDetail.Name = "labDetail";
            this.labDetail.Size = new System.Drawing.Size(37, 15);
            this.labDetail.TabIndex = 4;
            this.labDetail.Text = "详情";
            // 
            // labFileName
            // 
            this.labFileName.AutoSize = true;
            this.labFileName.Location = new System.Drawing.Point(81, 17);
            this.labFileName.Name = "labFileName";
            this.labFileName.Size = new System.Drawing.Size(37, 15);
            this.labFileName.TabIndex = 5;
            this.labFileName.Text = "文件";
            // 
            // labScale
            // 
            this.labScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labScale.AutoSize = true;
            this.labScale.Location = new System.Drawing.Point(535, 17);
            this.labScale.Name = "labScale";
            this.labScale.Size = new System.Drawing.Size(23, 15);
            this.labScale.TabIndex = 6;
            this.labScale.Text = "0%";
            // 
            // lsbUpdateDetail
            // 
            this.lsbUpdateDetail.FormattingEnabled = true;
            this.lsbUpdateDetail.ItemHeight = 15;
            this.lsbUpdateDetail.Location = new System.Drawing.Point(81, 76);
            this.lsbUpdateDetail.Name = "lsbUpdateDetail";
            this.lsbUpdateDetail.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lsbUpdateDetail.Size = new System.Drawing.Size(477, 109);
            this.lsbUpdateDetail.TabIndex = 7;
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(609, 200);
            this.Controls.Add(this.lsbUpdateDetail);
            this.Controls.Add(this.labScale);
            this.Controls.Add(this.labFileName);
            this.Controls.Add(this.labDetail);
            this.Controls.Add(this.labProgress);
            this.Controls.Add(this.proBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "应用更新";
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar proBar;
        private System.Windows.Forms.Label labProgress;
        private System.Windows.Forms.Label labDetail;
        private System.Windows.Forms.Label labFileName;
        private System.Windows.Forms.Label labScale;
        private System.Windows.Forms.ListBox lsbUpdateDetail;
    }
}