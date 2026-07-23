namespace LuxMMS
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiBase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiKanban = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpeech = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBase,
            this.tsmiKanban,
            this.tsmiSpeech,
            this.tsmiSystem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(88, 276);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiBase
            // 
            this.tsmiBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tsmiBase.ForeColor = System.Drawing.Color.White;
            this.tsmiBase.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tsmiBase.Name = "tsmiBase";
            this.tsmiBase.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.tsmiBase.Size = new System.Drawing.Size(71, 28);
            this.tsmiBase.Text = "基础";
            this.tsmiBase.Click += new System.EventHandler(this.tsmiBase_Click);
            // 
            // tsmiKanban
            // 
            this.tsmiKanban.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tsmiKanban.ForeColor = System.Drawing.Color.White;
            this.tsmiKanban.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tsmiKanban.Name = "tsmiKanban";
            this.tsmiKanban.Size = new System.Drawing.Size(71, 28);
            this.tsmiKanban.Text = "看板";
            this.tsmiKanban.Click += new System.EventHandler(this.tsmiKanban_Click);
            // 
            // tsmiSpeech
            // 
            this.tsmiSpeech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tsmiSpeech.ForeColor = System.Drawing.Color.White;
            this.tsmiSpeech.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tsmiSpeech.Name = "tsmiSpeech";
            this.tsmiSpeech.Size = new System.Drawing.Size(71, 28);
            this.tsmiSpeech.Text = "广播";
            this.tsmiSpeech.Click += new System.EventHandler(this.tsmiSpeech_Click);
            // 
            // tsmiSystem
            // 
            this.tsmiSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tsmiSystem.ForeColor = System.Drawing.Color.White;
            this.tsmiSystem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tsmiSystem.Name = "tsmiSystem";
            this.tsmiSystem.Size = new System.Drawing.Size(71, 28);
            this.tsmiSystem.Text = "系统";
            this.tsmiSystem.Click += new System.EventHandler(this.tsmiSystem_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(88, 0);
            this.panelContent.MinimumSize = new System.Drawing.Size(80, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(15);
            this.panelContent.Size = new System.Drawing.Size(322, 276);
            this.panelContent.TabIndex = 1;
            // 
            // FrmConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(410, 276);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiBase;
        private System.Windows.Forms.ToolStripMenuItem tsmiKanban;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpeech;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystem;
        private System.Windows.Forms.Panel panelContent;
    }
}