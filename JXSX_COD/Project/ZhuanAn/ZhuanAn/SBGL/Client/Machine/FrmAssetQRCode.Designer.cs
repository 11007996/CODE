namespace Machine
{
    partial class FrmAssetQRCode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAsset = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.pbQRCode = new System.Windows.Forms.PictureBox();
            this.labAssetNo = new System.Windows.Forms.Label();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txbKeyWords = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBatchSave = new DevComponents.DotNetBar.ButtonX();
            this.ChkAll = new System.Windows.Forms.CheckBox();
            this.cmbUrl = new System.Windows.Forms.ComboBox();
            this.txbAssetNo = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.txbAssetName = new System.Windows.Forms.TextBox();
            this.labAssetName = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAsset
            // 
            this.dgvAsset.AllowUserToAddRows = false;
            this.dgvAsset.AllowUserToDeleteRows = false;
            this.dgvAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcCheck,
            this.dgcAssetNo,
            this.dgcAssetName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAsset.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAsset.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvAsset.Location = new System.Drawing.Point(0, 0);
            this.dgvAsset.Name = "dgvAsset";
            this.dgvAsset.RowHeadersVisible = false;
            this.dgvAsset.RowHeadersWidth = 51;
            this.dgvAsset.RowTemplate.Height = 27;
            this.dgvAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsset.Size = new System.Drawing.Size(630, 497);
            this.dgvAsset.TabIndex = 1;
            this.dgvAsset.SelectionChanged += new System.EventHandler(this.dgvAsset_SelectionChanged);
            // 
            // dgcCheck
            // 
            this.dgcCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcCheck.HeaderText = "全选";
            this.dgcCheck.MinimumWidth = 6;
            this.dgcCheck.Name = "dgcCheck";
            this.dgcCheck.Width = 60;
            // 
            // dgcAssetNo
            // 
            this.dgcAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetNo.DataPropertyName = "AssetNo";
            this.dgcAssetNo.HeaderText = "资产编号";
            this.dgcAssetNo.MinimumWidth = 6;
            this.dgcAssetNo.Name = "dgcAssetNo";
            this.dgcAssetNo.Width = 220;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Location = new System.Drawing.Point(389, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pbQRCode
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pbQRCode, 2);
            this.pbQRCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbQRCode.Location = new System.Drawing.Point(3, 3);
            this.pbQRCode.Name = "pbQRCode";
            this.pbQRCode.Size = new System.Drawing.Size(300, 344);
            this.pbQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQRCode.TabIndex = 3;
            this.pbQRCode.TabStop = false;
            // 
            // labAssetNo
            // 
            this.labAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetNo.AutoSize = true;
            this.labAssetNo.Location = new System.Drawing.Point(30, 395);
            this.labAssetNo.Name = "labAssetNo";
            this.labAssetNo.Size = new System.Drawing.Size(37, 15);
            this.labAssetNo.TabIndex = 4;
            this.labAssetNo.Text = "编号";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.tableLayoutPanel1.SetColumnSpan(this.btnSave, 2);
            this.btnSave.Location = new System.Drawing.Point(31, 458);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(244, 43);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txbKeyWords
            // 
            this.txbKeyWords.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txbKeyWords.Location = new System.Drawing.Point(3, 7);
            this.txbKeyWords.Name = "txbKeyWords";
            this.txbKeyWords.Size = new System.Drawing.Size(380, 25);
            this.txbKeyWords.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "路径";
            // 
            // btnBatchSave
            // 
            this.btnBatchSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBatchSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBatchSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBatchSave.Location = new System.Drawing.Point(540, 5);
            this.btnBatchSave.Name = "btnBatchSave";
            this.btnBatchSave.Size = new System.Drawing.Size(93, 30);
            this.btnBatchSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBatchSave.TabIndex = 8;
            this.btnBatchSave.Text = "批量生成";
            this.btnBatchSave.Click += new System.EventHandler(this.btnBatchSave_Click);
            // 
            // ChkAll
            // 
            this.ChkAll.AutoSize = true;
            this.ChkAll.Location = new System.Drawing.Point(43, 5);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(18, 17);
            this.ChkAll.TabIndex = 7;
            this.ChkAll.UseVisualStyleBackColor = true;
            this.ChkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // cmbUrl
            // 
            this.cmbUrl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbUrl.FormattingEnabled = true;
            this.cmbUrl.Items.AddRange(new object[] {
            "",
            "http://sbgl.luxshare-ict.com:8090/h5/#/pages/asset/detail?assetNo="});
            this.cmbUrl.Location = new System.Drawing.Point(73, 426);
            this.cmbUrl.Name = "cmbUrl";
            this.cmbUrl.Size = new System.Drawing.Size(200, 23);
            this.cmbUrl.TabIndex = 10;
            this.cmbUrl.SelectedIndexChanged += new System.EventHandler(this.cmbUrl_SelectedIndexChanged);
            // 
            // txbAssetNo
            // 
            this.txbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetNo.Location = new System.Drawing.Point(73, 390);
            this.txbAssetNo.Name = "txbAssetNo";
            this.txbAssetNo.ReadOnly = true;
            this.txbAssetNo.Size = new System.Drawing.Size(200, 25);
            this.txbAssetNo.TabIndex = 11;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // txbAssetName
            // 
            this.txbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetName.Location = new System.Drawing.Point(73, 355);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.ReadOnly = true;
            this.txbAssetName.Size = new System.Drawing.Size(200, 25);
            this.txbAssetName.TabIndex = 13;
            // 
            // labAssetName
            // 
            this.labAssetName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAssetName.AutoSize = true;
            this.labAssetName.Location = new System.Drawing.Point(30, 360);
            this.labAssetName.Name = "labAssetName";
            this.labAssetName.Size = new System.Drawing.Size(37, 15);
            this.labAssetName.TabIndex = 12;
            this.labAssetName.Text = "名称";
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 2);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(3, 516);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 14;
            this.labMessage.Text = "提示";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbQRCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labAssetName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbUrl, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetNo, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labAssetNo, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(656, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 543);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.ChkAll);
            this.panel1.Controls.Add(this.dgvAsset);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 497);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.Controls.Add(this.txbKeyWords, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBatchSave, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(646, 543);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // FrmAssetQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(972, 563);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmAssetQRCode";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资产二维护码打印";
            this.Load += new System.EventHandler(this.FrmAssetQRCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRCode)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvAsset;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.PictureBox pbQRCode;
        private System.Windows.Forms.Label labAssetNo;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.TextBox txbKeyWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUrl;
        private System.Windows.Forms.TextBox txbAssetNo;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox txbAssetName;
        private System.Windows.Forms.Label labAssetName;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.CheckBox ChkAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgcCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private DevComponents.DotNetBar.ButtonX btnBatchSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}