namespace Basic
{
    partial class FrmFilePreview
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
            this.pbCurrImg = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.tabPanelImage = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrImg)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.tabPanelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCurrImg
            // 
            this.pbCurrImg.Location = new System.Drawing.Point(138, 33);
            this.pbCurrImg.Name = "pbCurrImg";
            this.pbCurrImg.Padding = new System.Windows.Forms.Padding(5);
            this.pbCurrImg.Size = new System.Drawing.Size(546, 672);
            this.pbCurrImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCurrImg.TabIndex = 0;
            this.pbCurrImg.TabStop = false;
            this.pbCurrImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCurrImg_MouseDown);
            this.pbCurrImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCurrImg_MouseMove);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelRight, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1382, 753);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.AutoScroll = true;
            this.panelLeft.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelLeft.Controls.Add(this.tabPanelImage);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(3, 3);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(194, 747);
            this.panelLeft.TabIndex = 1;
            // 
            // tabPanelImage
            // 
            this.tabPanelImage.AutoSize = true;
            this.tabPanelImage.BackColor = System.Drawing.Color.Silver;
            this.tabPanelImage.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tabPanelImage.ColumnCount = 1;
            this.tabPanelImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanelImage.Controls.Add(this.pictureBox1, 0, 0);
            this.tabPanelImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabPanelImage.Location = new System.Drawing.Point(0, 0);
            this.tabPanelImage.Name = "tabPanelImage";
            this.tabPanelImage.RowCount = 1;
            this.tabPanelImage.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanelImage.Size = new System.Drawing.Size(194, 128);
            this.tabPanelImage.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Location = new System.Drawing.Point(22, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.pbCurrImg);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(203, 3);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(1176, 747);
            this.panelRight.TabIndex = 1;
            // 
            // FrmFilePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmFilePreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件预览";
            this.Load += new System.EventHandler(this.FrmFilePreview_Load);
            this.Shown += new System.EventHandler(this.FrmFilePreview_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrImg)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.tabPanelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbCurrImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TableLayoutPanel tabPanelImage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelRight;
    }
}