namespace HZH_Controls.Forms
{
    partial class FrmDefault
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Panel();
            this.btnMax = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Panel();
            this.ucSplitLine_H1 = new HZH_Controls.Controls.UCSplitLine_H();
            this.ucSplitLine_V1 = new HZH_Controls.Controls.UCSplitLine_V();
            this.ucSplitLine_V2 = new HZH_Controls.Controls.UCSplitLine_V();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 17F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(705, 60);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "标题";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnMin.BackgroundImage = global::HZH_Controls.Properties.Resources.最小化;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMin.Location = new System.Drawing.Point(602, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(31, 60);
            this.btnMin.TabIndex = 7;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnMax.BackgroundImage = global::HZH_Controls.Properties.Resources.最大化;
            this.btnMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMax.Location = new System.Drawing.Point(636, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(31, 60);
            this.btnMax.TabIndex = 8;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnClose.BackgroundImage = global::HZH_Controls.Properties.Resources.关闭;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Location = new System.Drawing.Point(670, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(31, 60);
            this.btnClose.TabIndex = 9;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucSplitLine_H1
            // 
            this.ucSplitLine_H1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.ucSplitLine_H1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucSplitLine_H1.Location = new System.Drawing.Point(0, 545);
            this.ucSplitLine_H1.Name = "ucSplitLine_H1";
            this.ucSplitLine_H1.Size = new System.Drawing.Size(705, 2);
            this.ucSplitLine_H1.TabIndex = 10;
            this.ucSplitLine_H1.TabStop = false;
            // 
            // ucSplitLine_V1
            // 
            this.ucSplitLine_V1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.ucSplitLine_V1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucSplitLine_V1.Location = new System.Drawing.Point(0, 60);
            this.ucSplitLine_V1.Name = "ucSplitLine_V1";
            this.ucSplitLine_V1.Size = new System.Drawing.Size(2, 485);
            this.ucSplitLine_V1.TabIndex = 11;
            this.ucSplitLine_V1.TabStop = false;
            // 
            // ucSplitLine_V2
            // 
            this.ucSplitLine_V2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.ucSplitLine_V2.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucSplitLine_V2.Location = new System.Drawing.Point(703, 60);
            this.ucSplitLine_V2.Name = "ucSplitLine_V2";
            this.ucSplitLine_V2.Size = new System.Drawing.Size(2, 485);
            this.ucSplitLine_V2.TabIndex = 12;
            this.ucSplitLine_V2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 485);
            this.panel1.TabIndex = 13;
            // 
            // FrmDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 547);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucSplitLine_V2);
            this.Controls.Add(this.ucSplitLine_V1);
            this.Controls.Add(this.ucSplitLine_H1);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.IsFullSize = false;
            this.Name = "FrmDefault";
            this.Text = "FrmDefault";
            this.Load += new System.EventHandler(this.FrmDefault_Load);
            this.Shown += new System.EventHandler(this.FrmDefault_Shown);
            this.SizeChanged += new System.EventHandler(this.FrmDefault_SizeChanged);
            this.TextChanged += new System.EventHandler(this.FrmDefault_TextChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel btnMin;
        private System.Windows.Forms.Panel btnMax;
        private System.Windows.Forms.Panel btnClose;
        private Controls.UCSplitLine_H ucSplitLine_H1;
        private Controls.UCSplitLine_V ucSplitLine_V1;
        private Controls.UCSplitLine_V ucSplitLine_V2;
        protected System.Windows.Forms.Panel panel1;
    }
}