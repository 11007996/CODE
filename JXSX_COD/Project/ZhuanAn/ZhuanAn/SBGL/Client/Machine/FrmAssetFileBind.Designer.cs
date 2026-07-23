namespace Machine
{
    partial class FrmAssetFileBind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAssetFileBind));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAssetInfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcAssetCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetMSOP = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dgcAssetMMI = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dgcAssetMMOS = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnBind = new DevComponents.DotNetBar.ButtonX();
            this.dgvFileInfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcFileCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgcFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileAliasName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dgcFileClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.txbAssetKeyword = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chkFileMMOS = new System.Windows.Forms.CheckBox();
            this.chkFileMMI = new System.Windows.Forms.CheckBox();
            this.chkFileMSOP = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbFileKeyword = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnUnBind = new DevComponents.DotNetBar.ButtonX();
            this.labMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件别名或ID";
            // 
            // dgvAssetInfo
            // 
            this.dgvAssetInfo.AllowUserToAddRows = false;
            this.dgvAssetInfo.AllowUserToDeleteRows = false;
            this.dgvAssetInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssetInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssetInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAssetCheck,
            this.dgcAssetNo,
            this.dgcAssetName,
            this.dgcAssetMSOP,
            this.dgcAssetMMI,
            this.dgcAssetMMOS});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssetInfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAssetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssetInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvAssetInfo.Location = new System.Drawing.Point(3, 73);
            this.dgvAssetInfo.Name = "dgvAssetInfo";
            this.dgvAssetInfo.RowHeadersVisible = false;
            this.dgvAssetInfo.RowHeadersWidth = 51;
            this.dgvAssetInfo.RowTemplate.Height = 27;
            this.dgvAssetInfo.Size = new System.Drawing.Size(798, 677);
            this.dgvAssetInfo.TabIndex = 2;
            this.dgvAssetInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAssetInfo_CellContentClick);
            // 
            // dgcAssetCheck
            // 
            this.dgcAssetCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetCheck.HeaderText = "选择";
            this.dgcAssetCheck.MinimumWidth = 6;
            this.dgcAssetCheck.Name = "dgcAssetCheck";
            this.dgcAssetCheck.Width = 40;
            // 
            // dgcAssetNo
            // 
            this.dgcAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetNo.DataPropertyName = "AssetNo";
            this.dgcAssetNo.HeaderText = "资产编号";
            this.dgcAssetNo.MinimumWidth = 6;
            this.dgcAssetNo.Name = "dgcAssetNo";
            this.dgcAssetNo.ReadOnly = true;
            this.dgcAssetNo.Width = 140;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.ReadOnly = true;
            // 
            // dgcAssetMSOP
            // 
            this.dgcAssetMSOP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetMSOP.DataPropertyName = "MSOP";
            this.dgcAssetMSOP.HeaderText = "SOP";
            this.dgcAssetMSOP.MinimumWidth = 6;
            this.dgcAssetMSOP.Name = "dgcAssetMSOP";
            this.dgcAssetMSOP.ReadOnly = true;
            this.dgcAssetMSOP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcAssetMSOP.VisitedLinkColor = System.Drawing.Color.Purple;
            this.dgcAssetMSOP.Width = 60;
            // 
            // dgcAssetMMI
            // 
            this.dgcAssetMMI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetMMI.DataPropertyName = "MMI";
            this.dgcAssetMMI.HeaderText = "周期表";
            this.dgcAssetMMI.MinimumWidth = 6;
            this.dgcAssetMMI.Name = "dgcAssetMMI";
            this.dgcAssetMMI.ReadOnly = true;
            this.dgcAssetMMI.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcAssetMMI.Width = 60;
            // 
            // dgcAssetMMOS
            // 
            this.dgcAssetMMOS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetMMOS.DataPropertyName = "MMOS";
            this.dgcAssetMMOS.HeaderText = "作业书";
            this.dgcAssetMMOS.MinimumWidth = 6;
            this.dgcAssetMMOS.Name = "dgcAssetMMOS";
            this.dgcAssetMMOS.ReadOnly = true;
            this.dgcAssetMMOS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcAssetMMOS.Width = 60;
            // 
            // btnBind
            // 
            this.btnBind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBind.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnBind.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBind.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBind.Image = ((System.Drawing.Image)(resources.GetObject("btnBind.Image")));
            this.btnBind.Location = new System.Drawing.Point(14, 138);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(85, 40);
            this.btnBind.TabIndex = 4;
            this.btnBind.Text = "绑定";
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // dgvFileInfo
            // 
            this.dgvFileInfo.AllowUserToAddRows = false;
            this.dgvFileInfo.AllowUserToDeleteRows = false;
            this.dgvFileInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFileInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFileCheck,
            this.dgcFileId,
            this.dgcFileAliasName,
            this.dgcFileClass});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFileInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvFileInfo.Location = new System.Drawing.Point(927, 73);
            this.dgvFileInfo.Name = "dgvFileInfo";
            this.dgvFileInfo.RowHeadersVisible = false;
            this.dgvFileInfo.RowHeadersWidth = 51;
            this.dgvFileInfo.RowTemplate.Height = 27;
            this.dgvFileInfo.Size = new System.Drawing.Size(652, 677);
            this.dgvFileInfo.TabIndex = 7;
            this.dgvFileInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFileInfo_CellContentClick);
            // 
            // dgcFileCheck
            // 
            this.dgcFileCheck.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileCheck.DataPropertyName = "FileCheck";
            this.dgcFileCheck.HeaderText = "选择";
            this.dgcFileCheck.MinimumWidth = 6;
            this.dgcFileCheck.Name = "dgcFileCheck";
            this.dgcFileCheck.Width = 40;
            // 
            // dgcFileId
            // 
            this.dgcFileId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileId.DataPropertyName = "FileId";
            this.dgcFileId.HeaderText = "文件Id";
            this.dgcFileId.MinimumWidth = 6;
            this.dgcFileId.Name = "dgcFileId";
            this.dgcFileId.ReadOnly = true;
            this.dgcFileId.Width = 70;
            // 
            // dgcFileAliasName
            // 
            this.dgcFileAliasName.DataPropertyName = "FileAliasName";
            this.dgcFileAliasName.HeaderText = "文件名";
            this.dgcFileAliasName.MinimumWidth = 6;
            this.dgcFileAliasName.Name = "dgcFileAliasName";
            this.dgcFileAliasName.ReadOnly = true;
            this.dgcFileAliasName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcFileAliasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dgcFileClass
            // 
            this.dgcFileClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileClass.DataPropertyName = "FileClassText";
            this.dgcFileClass.HeaderText = "文件类别";
            this.dgcFileClass.MinimumWidth = 6;
            this.dgcFileClass.Name = "dgcFileClass";
            this.dgcFileClass.ReadOnly = true;
            this.dgcFileClass.Width = 125;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvFileInfo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvAssetInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1582, 753);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.txbAssetKeyword, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(798, 64);
            this.tableLayoutPanel6.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "资产编号或名称";
            // 
            // txbAssetKeyword
            // 
            this.txbAssetKeyword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAssetKeyword.Location = new System.Drawing.Point(121, 35);
            this.txbAssetKeyword.Name = "txbAssetKeyword";
            this.txbAssetKeyword.Size = new System.Drawing.Size(250, 25);
            this.txbAssetKeyword.TabIndex = 14;
            this.txbAssetKeyword.TextChanged += new System.EventHandler(this.txbAssetKeyword_TextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.chkFileMMOS, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkFileMMI, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkFileMSOP, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txbFileKeyword, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(927, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(652, 64);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // chkFileMMOS
            // 
            this.chkFileMMOS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkFileMMOS.AutoSize = true;
            this.chkFileMMOS.Checked = true;
            this.chkFileMMOS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileMMOS.Location = new System.Drawing.Point(366, 6);
            this.chkFileMMOS.Name = "chkFileMMOS";
            this.chkFileMMOS.Size = new System.Drawing.Size(164, 19);
            this.chkFileMMOS.TabIndex = 13;
            this.chkFileMMOS.Tag = "3";
            this.chkFileMMOS.Text = "设备保养作业标准书";
            this.chkFileMMOS.UseVisualStyleBackColor = true;
            this.chkFileMMOS.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkFileMMI
            // 
            this.chkFileMMI.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkFileMMI.AutoSize = true;
            this.chkFileMMI.Checked = true;
            this.chkFileMMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileMMI.Location = new System.Drawing.Point(226, 6);
            this.chkFileMMI.Name = "chkFileMMI";
            this.chkFileMMI.Size = new System.Drawing.Size(134, 19);
            this.chkFileMMI.TabIndex = 12;
            this.chkFileMMI.Tag = "2";
            this.chkFileMMI.Text = "设备保养周期表";
            this.chkFileMMI.UseVisualStyleBackColor = true;
            this.chkFileMMI.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkFileMSOP
            // 
            this.chkFileMSOP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkFileMSOP.AutoSize = true;
            this.chkFileMSOP.Checked = true;
            this.chkFileMSOP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFileMSOP.Location = new System.Drawing.Point(107, 6);
            this.chkFileMSOP.Name = "chkFileMSOP";
            this.chkFileMSOP.Size = new System.Drawing.Size(113, 19);
            this.chkFileMSOP.TabIndex = 11;
            this.chkFileMSOP.Tag = "1";
            this.chkFileMSOP.Text = "设备操作SOP";
            this.chkFileMSOP.UseVisualStyleBackColor = true;
            this.chkFileMSOP.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "文件类别";
            // 
            // txbFileKeyword
            // 
            this.txbFileKeyword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.txbFileKeyword, 3);
            this.txbFileKeyword.Location = new System.Drawing.Point(107, 35);
            this.txbFileKeyword.Name = "txbFileKeyword";
            this.txbFileKeyword.Size = new System.Drawing.Size(250, 25);
            this.txbFileKeyword.TabIndex = 14;
            this.txbFileKeyword.TextChanged += new System.EventHandler(this.txbFileKeyword_TextChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.btnUnBind, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.btnBind, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labMessage, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(807, 73);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(114, 677);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnUnBind
            // 
            this.btnUnBind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnBind.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUnBind.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnBind.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnBind.Image = ((System.Drawing.Image)(resources.GetObject("btnUnBind.Image")));
            this.btnUnBind.Location = new System.Drawing.Point(14, 273);
            this.btnUnBind.Name = "btnUnBind";
            this.btnUnBind.Size = new System.Drawing.Size(85, 40);
            this.btnUnBind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnBind.TabIndex = 5;
            this.btnUnBind.Text = "解绑";
            this.btnUnBind.Click += new System.EventHandler(this.btnUnBind_Click);
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Location = new System.Drawing.Point(3, 405);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(52, 15);
            this.labMessage.TabIndex = 6;
            this.labMessage.Text = "提示：";
            // 
            // FrmAssetFileBind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1582, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmAssetFileBind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资产文件绑定";
            this.Load += new System.EventHandler(this.FrmAssetFileBind_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvAssetInfo;
        private DevComponents.DotNetBar.ButtonX btnBind;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvFileInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox chkFileMMOS;
        private System.Windows.Forms.CheckBox chkFileMSOP;
        private System.Windows.Forms.CheckBox chkFileMMI;
        private System.Windows.Forms.TextBox txbFileKeyword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbAssetKeyword;
        private DevComponents.DotNetBar.ButtonX btnUnBind;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgcAssetCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private System.Windows.Forms.DataGridViewLinkColumn dgcAssetMSOP;
        private System.Windows.Forms.DataGridViewLinkColumn dgcAssetMMI;
        private System.Windows.Forms.DataGridViewLinkColumn dgcAssetMMOS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgcFileCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileId;
        private System.Windows.Forms.DataGridViewLinkColumn dgcFileAliasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileClass;
    }
}