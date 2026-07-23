namespace Basic
{
    partial class FrmFileManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileManage));
            this.gbUpload = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.labFileCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbFileClass = new System.Windows.Forms.ComboBox();
            this.labMessage = new System.Windows.Forms.TextBox();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.dgcFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileAliasName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dgcFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileExtension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcBindCount = new System.Windows.Forms.DataGridViewLinkColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.gbSelect = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbConditionFileClass = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tbConditionFileName = new System.Windows.Forms.TextBox();
            this.gbUpload.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.gbSelect.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUpload
            // 
            this.gbUpload.Controls.Add(this.tableLayoutPanel1);
            this.gbUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbUpload.Location = new System.Drawing.Point(0, 102);
            this.gbUpload.Name = "gbUpload";
            this.gbUpload.Size = new System.Drawing.Size(1382, 145);
            this.gbUpload.TabIndex = 0;
            this.gbUpload.TabStop = false;
            this.gbUpload.Text = "上传";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbFiles, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpload, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnChange, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectFile, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbFileClass, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1376, 121);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbFiles
            // 
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.ItemHeight = 15;
            this.lbFiles.Location = new System.Drawing.Point(273, 3);
            this.lbFiles.Name = "lbFiles";
            this.tableLayoutPanel1.SetRowSpan(this.lbFiles, 3);
            this.lbFiles.Size = new System.Drawing.Size(244, 115);
            this.lbFiles.TabIndex = 5;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUpload.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(523, 3);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(110, 34);
            this.btnUpload.TabIndex = 10;
            this.btnUpload.Text = "上传[新增]";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnChange.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnChange.ForeColor = System.Drawing.Color.White;
            this.btnChange.Location = new System.Drawing.Point(523, 43);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(110, 34);
            this.btnChange.TabIndex = 11;
            this.btnChange.Text = "上传[替换]";
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.labFileCount, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(153, 83);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(114, 35);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "个";
            // 
            // labFileCount
            // 
            this.labFileCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labFileCount.AutoSize = true;
            this.labFileCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFileCount.ForeColor = System.Drawing.Color.Blue;
            this.labFileCount.Location = new System.Drawing.Point(78, 10);
            this.labFileCount.Margin = new System.Windows.Forms.Padding(0);
            this.labFileCount.Name = "labFileCount";
            this.labFileCount.Size = new System.Drawing.Size(16, 15);
            this.labFileCount.TabIndex = 10;
            this.labFileCount.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "共";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSelectFile.Location = new System.Drawing.Point(173, 5);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(94, 29);
            this.btnSelectFile.TabIndex = 1;
            this.btnSelectFile.Text = "选择文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // cbFileClass
            // 
            this.cbFileClass.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbFileClass.FormattingEnabled = true;
            this.cbFileClass.Location = new System.Drawing.Point(26, 8);
            this.cbFileClass.Name = "cbFileClass";
            this.cbFileClass.Size = new System.Drawing.Size(121, 23);
            this.cbFileClass.TabIndex = 14;
            this.cbFileClass.Text = "选择文件分类";
            this.cbFileClass.SelectedIndexChanged += new System.EventHandler(this.cbFileClass_SelectedIndexChanged);
            // 
            // labMessage
            // 
            this.labMessage.BackColor = System.Drawing.Color.AliceBlue;
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(673, 3);
            this.labMessage.Multiline = true;
            this.labMessage.Name = "labMessage";
            this.labMessage.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.labMessage, 3);
            this.labMessage.Size = new System.Drawing.Size(700, 115);
            this.labMessage.TabIndex = 15;
            this.labMessage.Text = "提示";
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvFile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFileId,
            this.dgcFileAliasName,
            this.dgcFileName,
            this.dgcFileExtension,
            this.dgcFileSize,
            this.dgcFileClass,
            this.dgcUpdateTime,
            this.dgcUpdateUser,
            this.dgcBindCount});
            this.dgvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFile.Location = new System.Drawing.Point(0, 247);
            this.dgvFile.MultiSelect = false;
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersWidth = 51;
            this.dgvFile.RowTemplate.Height = 27;
            this.dgvFile.Size = new System.Drawing.Size(1382, 506);
            this.dgvFile.TabIndex = 1;
            this.dgvFile.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFile_CellContentClick);
            this.dgvFile.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFile_CellFormatting);
            this.dgvFile.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvFile_UserDeletingRow);
            // 
            // dgcFileId
            // 
            this.dgcFileId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileId.DataPropertyName = "FileId";
            this.dgcFileId.HeaderText = "文件ID";
            this.dgcFileId.MinimumWidth = 6;
            this.dgcFileId.Name = "dgcFileId";
            this.dgcFileId.ReadOnly = true;
            this.dgcFileId.Width = 80;
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
            // dgcFileName
            // 
            this.dgcFileName.DataPropertyName = "FileName";
            this.dgcFileName.HeaderText = "文件名称(唯一)";
            this.dgcFileName.MinimumWidth = 6;
            this.dgcFileName.Name = "dgcFileName";
            this.dgcFileName.ReadOnly = true;
            this.dgcFileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgcFileExtension
            // 
            this.dgcFileExtension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileExtension.DataPropertyName = "FileExtension";
            this.dgcFileExtension.HeaderText = "文件后缀";
            this.dgcFileExtension.MinimumWidth = 6;
            this.dgcFileExtension.Name = "dgcFileExtension";
            this.dgcFileExtension.ReadOnly = true;
            this.dgcFileExtension.Width = 80;
            // 
            // dgcFileSize
            // 
            this.dgcFileSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFileSize.DataPropertyName = "FileSize";
            this.dgcFileSize.HeaderText = "文件大小";
            this.dgcFileSize.MinimumWidth = 6;
            this.dgcFileSize.Name = "dgcFileSize";
            this.dgcFileSize.ReadOnly = true;
            this.dgcFileSize.Width = 80;
            // 
            // dgcFileClass
            // 
            this.dgcFileClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgcFileClass.DataPropertyName = "FileClass";
            this.dgcFileClass.HeaderText = "文件分类";
            this.dgcFileClass.MinimumWidth = 6;
            this.dgcFileClass.Name = "dgcFileClass";
            this.dgcFileClass.ReadOnly = true;
            this.dgcFileClass.Width = 75;
            // 
            // dgcUpdateTime
            // 
            this.dgcUpdateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcUpdateTime.DataPropertyName = "UpdateTime";
            this.dgcUpdateTime.HeaderText = "更新时间";
            this.dgcUpdateTime.MinimumWidth = 6;
            this.dgcUpdateTime.Name = "dgcUpdateTime";
            this.dgcUpdateTime.ReadOnly = true;
            this.dgcUpdateTime.Width = 125;
            // 
            // dgcUpdateUser
            // 
            this.dgcUpdateUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcUpdateUser.DataPropertyName = "UserName";
            this.dgcUpdateUser.HeaderText = "更新人员";
            this.dgcUpdateUser.MinimumWidth = 6;
            this.dgcUpdateUser.Name = "dgcUpdateUser";
            this.dgcUpdateUser.ReadOnly = true;
            this.dgcUpdateUser.Width = 80;
            // 
            // dgcBindCount
            // 
            this.dgcBindCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcBindCount.DataPropertyName = "BindCount";
            this.dgcBindCount.HeaderText = "关联个数";
            this.dgcBindCount.MinimumWidth = 6;
            this.dgcBindCount.Name = "dgcBindCount";
            this.dgcBindCount.ReadOnly = true;
            this.dgcBindCount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcBindCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgcBindCount.Width = 80;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiQuery,
            this.tsmiUpload});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1382, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiQuery
            // 
            this.tsmiQuery.BackColor = System.Drawing.Color.Turquoise;
            this.tsmiQuery.ForeColor = System.Drawing.Color.White;
            this.tsmiQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsmiQuery.Image")));
            this.tsmiQuery.Name = "tsmiQuery";
            this.tsmiQuery.Size = new System.Drawing.Size(73, 26);
            this.tsmiQuery.Text = "查询";
            this.tsmiQuery.Click += new System.EventHandler(this.tsmiQuery_Click);
            // 
            // tsmiUpload
            // 
            this.tsmiUpload.BackColor = System.Drawing.Color.Turquoise;
            this.tsmiUpload.ForeColor = System.Drawing.Color.White;
            this.tsmiUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUpload.Image")));
            this.tsmiUpload.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsmiUpload.Name = "tsmiUpload";
            this.tsmiUpload.Size = new System.Drawing.Size(73, 26);
            this.tsmiUpload.Text = "上传";
            this.tsmiUpload.Click += new System.EventHandler(this.tsmiUpload_Click);
            // 
            // gbSelect
            // 
            this.gbSelect.Controls.Add(this.tableLayoutPanel3);
            this.gbSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSelect.Location = new System.Drawing.Point(0, 30);
            this.gbSelect.Name = "gbSelect";
            this.gbSelect.Size = new System.Drawing.Size(1382, 72);
            this.gbSelect.TabIndex = 1;
            this.gbSelect.TabStop = false;
            this.gbSelect.Text = "查询";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cbConditionFileClass, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSelect, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnReset, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbConditionFileName, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1376, 48);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "文件名称";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "文件分类";
            // 
            // cbConditionFileClass
            // 
            this.cbConditionFileClass.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbConditionFileClass.FormattingEnabled = true;
            this.cbConditionFileClass.Location = new System.Drawing.Point(123, 12);
            this.cbConditionFileClass.Name = "cbConditionFileClass";
            this.cbConditionFileClass.Size = new System.Drawing.Size(144, 23);
            this.cbConditionFileClass.TabIndex = 15;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSelect.ForeColor = System.Drawing.Color.White;
            this.btnSelect.Location = new System.Drawing.Point(523, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(90, 40);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "查询";
            this.btnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(623, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(90, 40);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tbConditionFileName
            // 
            this.tbConditionFileName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbConditionFileName.Location = new System.Drawing.Point(373, 11);
            this.tbConditionFileName.Name = "tbConditionFileName";
            this.tbConditionFileName.Size = new System.Drawing.Size(144, 25);
            this.tbConditionFileName.TabIndex = 18;
            // 
            // FrmFileManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.gbUpload);
            this.Controls.Add(this.gbSelect);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmFileManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件管理";
            this.Load += new System.EventHandler(this.FrmFileManage_Load);
            this.gbUpload.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbSelect.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUpload;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labFileCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFileClass;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpload;
        private System.Windows.Forms.GroupBox gbSelect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbConditionFileClass;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox tbConditionFileName;
        private System.Windows.Forms.TextBox labMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileId;
        private System.Windows.Forms.DataGridViewLinkColumn dgcFileAliasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileExtension;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateUser;
        private System.Windows.Forms.DataGridViewLinkColumn dgcBindCount;
    }
}