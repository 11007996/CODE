namespace Machine
{
    partial class FrmTipDicData
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
            this.labYearTip = new System.Windows.Forms.Label();
            this.labMonthTip = new System.Windows.Forms.Label();
            this.rtxbYearTip = new System.Windows.Forms.RichTextBox();
            this.rtxbMonthTip = new System.Windows.Forms.RichTextBox();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labYearTip
            // 
            this.labYearTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labYearTip.AutoSize = true;
            this.labYearTip.Location = new System.Drawing.Point(3, 108);
            this.labYearTip.Name = "labYearTip";
            this.labYearTip.Size = new System.Drawing.Size(82, 15);
            this.labYearTip.TabIndex = 0;
            this.labYearTip.Text = "年保养提示";
            this.labYearTip.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labMonthTip
            // 
            this.labMonthTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMonthTip.AutoSize = true;
            this.labMonthTip.Location = new System.Drawing.Point(3, 7);
            this.labMonthTip.Name = "labMonthTip";
            this.labMonthTip.Size = new System.Drawing.Size(97, 15);
            this.labMonthTip.TabIndex = 1;
            this.labMonthTip.Text = "月保养表提示";
            this.labMonthTip.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // rtxbYearTip
            // 
            this.rtxbYearTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbYearTip.ForeColor = System.Drawing.Color.Green;
            this.rtxbYearTip.Location = new System.Drawing.Point(3, 134);
            this.rtxbYearTip.Name = "rtxbYearTip";
            this.rtxbYearTip.Size = new System.Drawing.Size(1576, 65);
            this.rtxbYearTip.TabIndex = 2;
            this.rtxbYearTip.Text = "";
            // 
            // rtxbMonthTip
            // 
            this.rtxbMonthTip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbMonthTip.ForeColor = System.Drawing.Color.Green;
            this.rtxbMonthTip.Location = new System.Drawing.Point(3, 33);
            this.rtxbMonthTip.Name = "rtxbMonthTip";
            this.rtxbMonthTip.Size = new System.Drawing.Size(1576, 65);
            this.rtxbMonthTip.TabIndex = 3;
            this.rtxbMonthTip.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(1409, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(170, 45);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.rtxbYearTip, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labMonthTip, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labYearTip, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.rtxbMonthTip, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 253);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FrmTipDicData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1582, 253);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmTipDicData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保养报表提示";
            this.Load += new System.EventHandler(this.FrmTipDicData_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labYearTip;
        private System.Windows.Forms.Label labMonthTip;
        private System.Windows.Forms.RichTextBox rtxbYearTip;
        private System.Windows.Forms.RichTextBox rtxbMonthTip;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}