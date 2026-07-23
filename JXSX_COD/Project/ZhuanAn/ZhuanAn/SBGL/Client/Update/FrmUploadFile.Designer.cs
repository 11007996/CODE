namespace Update
{
    partial class FrmUploadFile
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.gbFileInfo = new System.Windows.Forms.GroupBox();
            this.chkIsUpdateApp = new System.Windows.Forms.CheckBox();
            this.labFile = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.tbUploadFilePath = new System.Windows.Forms.TextBox();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.chkUseFlag = new System.Windows.Forms.CheckBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.labFileName_tj = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.dgvFileInfo = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labMessage = new System.Windows.Forms.Label();
            this.labUpdateUser = new System.Windows.Forms.Label();
            this.labUpdateTime = new System.Windows.Forms.Label();
            this.labFileType = new System.Windows.Forms.Label();
            this.labFileVersion = new System.Windows.Forms.Label();
            this.labFileName = new System.Windows.Forms.Label();
            this.labFileSize = new System.Windows.Forms.Label();
            this.labFileId = new System.Windows.Forms.Label();
            this.labUpdateTimeTitle = new System.Windows.Forms.Label();
            this.labUpdateUserTitle = new System.Windows.Forms.Label();
            this.labFileTypeTitle = new System.Windows.Forms.Label();
            this.labFileVersionTitle = new System.Windows.Forms.Label();
            this.labFileSizeTitle = new System.Windows.Forms.Label();
            this.labFileNameTitle = new System.Windows.Forms.Label();
            this.labFileIdTitle = new System.Windows.Forms.Label();
            this.dgcFileId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFileTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcIsUpdateApp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUseFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.gbFileInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelect,
            this.tsmiRefresh,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiSave,
            this.tsmiDelete,
            this.tsmiBack});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1282, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSelect
            // 
            this.tsmiSelect.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSelect.Name = "tsmiSelect";
            this.tsmiSelect.Size = new System.Drawing.Size(64, 31);
            this.tsmiSelect.Text = "查询";
            this.tsmiSelect.Click += new System.EventHandler(this.tsmiSelect_Click);
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(64, 31);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(64, 31);
            this.tsmiAdd.Text = "新增";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(64, 31);
            this.tsmiUpdate.Text = "修改";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(64, 31);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(64, 31);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiBack
            // 
            this.tsmiBack.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(64, 31);
            this.tsmiBack.Text = "返回";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // gbFileInfo
            // 
            this.gbFileInfo.Controls.Add(this.chkIsUpdateApp);
            this.gbFileInfo.Controls.Add(this.labFile);
            this.gbFileInfo.Controls.Add(this.btnUpload);
            this.gbFileInfo.Controls.Add(this.tbUploadFilePath);
            this.gbFileInfo.Controls.Add(this.tbRemark);
            this.gbFileInfo.Controls.Add(this.chkUseFlag);
            this.gbFileInfo.Controls.Add(this.tbFileName);
            this.gbFileInfo.Controls.Add(this.labFileName_tj);
            this.gbFileInfo.Controls.Add(this.labRemark);
            this.gbFileInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFileInfo.Location = new System.Drawing.Point(0, 0);
            this.gbFileInfo.Name = "gbFileInfo";
            this.gbFileInfo.Size = new System.Drawing.Size(954, 148);
            this.gbFileInfo.TabIndex = 1;
            this.gbFileInfo.TabStop = false;
            this.gbFileInfo.Text = "文件上传";
            // 
            // chkIsUpdateApp
            // 
            this.chkIsUpdateApp.AutoSize = true;
            this.chkIsUpdateApp.Location = new System.Drawing.Point(492, 30);
            this.chkIsUpdateApp.Name = "chkIsUpdateApp";
            this.chkIsUpdateApp.Size = new System.Drawing.Size(139, 19);
            this.chkIsUpdateApp.TabIndex = 11;
            this.chkIsUpdateApp.Text = "是否Update.exe";
            this.chkIsUpdateApp.UseVisualStyleBackColor = true;
            // 
            // labFile
            // 
            this.labFile.AutoSize = true;
            this.labFile.Location = new System.Drawing.Point(50, 96);
            this.labFile.Name = "labFile";
            this.labFile.Size = new System.Drawing.Size(45, 15);
            this.labFile.TabIndex = 10;
            this.labFile.Text = "文件:";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(611, 91);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 25);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // tbUploadFilePath
            // 
            this.tbUploadFilePath.Enabled = false;
            this.tbUploadFilePath.Location = new System.Drawing.Point(101, 91);
            this.tbUploadFilePath.Name = "tbUploadFilePath";
            this.tbUploadFilePath.Size = new System.Drawing.Size(504, 25);
            this.tbUploadFilePath.TabIndex = 8;
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(101, 60);
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(504, 25);
            this.tbRemark.TabIndex = 6;
            // 
            // chkUseFlag
            // 
            this.chkUseFlag.AutoSize = true;
            this.chkUseFlag.Checked = true;
            this.chkUseFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseFlag.Location = new System.Drawing.Point(397, 31);
            this.chkUseFlag.Name = "chkUseFlag";
            this.chkUseFlag.Size = new System.Drawing.Size(89, 19);
            this.chkUseFlag.TabIndex = 2;
            this.chkUseFlag.Text = "是否可用";
            this.chkUseFlag.UseVisualStyleBackColor = true;
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(101, 29);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(274, 25);
            this.tbFileName.TabIndex = 1;
            // 
            // labFileName_tj
            // 
            this.labFileName_tj.AutoSize = true;
            this.labFileName_tj.Location = new System.Drawing.Point(36, 34);
            this.labFileName_tj.Name = "labFileName_tj";
            this.labFileName_tj.Size = new System.Drawing.Size(60, 15);
            this.labFileName_tj.TabIndex = 0;
            this.labFileName_tj.Text = "文件名:";
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Location = new System.Drawing.Point(51, 65);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(45, 15);
            this.labRemark.TabIndex = 3;
            this.labRemark.Text = "备注:";
            // 
            // dgvFileInfo
            // 
            this.dgvFileInfo.AllowUserToAddRows = false;
            this.dgvFileInfo.AllowUserToDeleteRows = false;
            this.dgvFileInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFileId,
            this.dgcFileName,
            this.dgcFileSize,
            this.dgcFileVersion,
            this.dgcFileType,
            this.dgcFileTime,
            this.dgcIsUpdateApp,
            this.dgcRemark,
            this.dgcUseFlag,
            this.dgcUpdateUser,
            this.dgcUpdateTime});
            this.dgvFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileInfo.Location = new System.Drawing.Point(0, 148);
            this.dgvFileInfo.Name = "dgvFileInfo";
            this.dgvFileInfo.ReadOnly = true;
            this.dgvFileInfo.RowTemplate.Height = 27;
            this.dgvFileInfo.Size = new System.Drawing.Size(954, 470);
            this.dgvFileInfo.TabIndex = 2;
            this.dgvFileInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFileInfo_CellClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvFileInfo);
            this.splitContainer1.Panel1.Controls.Add(this.gbFileInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.labMessage);
            this.splitContainer1.Panel2.Controls.Add(this.labUpdateUser);
            this.splitContainer1.Panel2.Controls.Add(this.labUpdateTime);
            this.splitContainer1.Panel2.Controls.Add(this.labFileType);
            this.splitContainer1.Panel2.Controls.Add(this.labFileVersion);
            this.splitContainer1.Panel2.Controls.Add(this.labFileName);
            this.splitContainer1.Panel2.Controls.Add(this.labFileSize);
            this.splitContainer1.Panel2.Controls.Add(this.labFileId);
            this.splitContainer1.Panel2.Controls.Add(this.labUpdateTimeTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labUpdateUserTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labFileTypeTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labFileVersionTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labFileSizeTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labFileNameTitle);
            this.splitContainer1.Panel2.Controls.Add(this.labFileIdTitle);
            this.splitContainer1.Size = new System.Drawing.Size(1282, 618);
            this.splitContainer1.SplitterDistance = 954;
            this.splitContainer1.TabIndex = 3;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Green;
            this.labMessage.Location = new System.Drawing.Point(39, 46);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 15);
            this.labMessage.TabIndex = 11;
            // 
            // labUpdateUser
            // 
            this.labUpdateUser.AutoSize = true;
            this.labUpdateUser.ForeColor = System.Drawing.Color.Green;
            this.labUpdateUser.Location = new System.Drawing.Point(127, 318);
            this.labUpdateUser.Name = "labUpdateUser";
            this.labUpdateUser.Size = new System.Drawing.Size(55, 15);
            this.labUpdateUser.TabIndex = 10;
            this.labUpdateUser.Text = "label7";
            // 
            // labUpdateTime
            // 
            this.labUpdateTime.AutoSize = true;
            this.labUpdateTime.ForeColor = System.Drawing.Color.Green;
            this.labUpdateTime.Location = new System.Drawing.Point(127, 352);
            this.labUpdateTime.Name = "labUpdateTime";
            this.labUpdateTime.Size = new System.Drawing.Size(55, 15);
            this.labUpdateTime.TabIndex = 9;
            this.labUpdateTime.Text = "label6";
            // 
            // labFileType
            // 
            this.labFileType.AutoSize = true;
            this.labFileType.ForeColor = System.Drawing.Color.Green;
            this.labFileType.Location = new System.Drawing.Point(127, 284);
            this.labFileType.Name = "labFileType";
            this.labFileType.Size = new System.Drawing.Size(55, 15);
            this.labFileType.TabIndex = 8;
            this.labFileType.Text = "label5";
            // 
            // labFileVersion
            // 
            this.labFileVersion.AutoSize = true;
            this.labFileVersion.ForeColor = System.Drawing.Color.Green;
            this.labFileVersion.Location = new System.Drawing.Point(127, 250);
            this.labFileVersion.Name = "labFileVersion";
            this.labFileVersion.Size = new System.Drawing.Size(55, 15);
            this.labFileVersion.TabIndex = 7;
            this.labFileVersion.Text = "label4";
            // 
            // labFileName
            // 
            this.labFileName.AutoSize = true;
            this.labFileName.ForeColor = System.Drawing.Color.Green;
            this.labFileName.Location = new System.Drawing.Point(127, 182);
            this.labFileName.Name = "labFileName";
            this.labFileName.Size = new System.Drawing.Size(55, 15);
            this.labFileName.TabIndex = 6;
            this.labFileName.Text = "label3";
            // 
            // labFileSize
            // 
            this.labFileSize.AutoSize = true;
            this.labFileSize.ForeColor = System.Drawing.Color.Green;
            this.labFileSize.Location = new System.Drawing.Point(127, 216);
            this.labFileSize.Name = "labFileSize";
            this.labFileSize.Size = new System.Drawing.Size(55, 15);
            this.labFileSize.TabIndex = 5;
            this.labFileSize.Text = "label2";
            // 
            // labFileId
            // 
            this.labFileId.AutoSize = true;
            this.labFileId.ForeColor = System.Drawing.Color.Green;
            this.labFileId.Location = new System.Drawing.Point(127, 148);
            this.labFileId.Name = "labFileId";
            this.labFileId.Size = new System.Drawing.Size(55, 15);
            this.labFileId.TabIndex = 4;
            this.labFileId.Text = "label1";
            // 
            // labUpdateTimeTitle
            // 
            this.labUpdateTimeTitle.AutoSize = true;
            this.labUpdateTimeTitle.Location = new System.Drawing.Point(36, 352);
            this.labUpdateTimeTitle.Name = "labUpdateTimeTitle";
            this.labUpdateTimeTitle.Size = new System.Drawing.Size(75, 15);
            this.labUpdateTimeTitle.TabIndex = 2;
            this.labUpdateTimeTitle.Text = "更新时间:";
            // 
            // labUpdateUserTitle
            // 
            this.labUpdateUserTitle.AutoSize = true;
            this.labUpdateUserTitle.Location = new System.Drawing.Point(51, 318);
            this.labUpdateUserTitle.Name = "labUpdateUserTitle";
            this.labUpdateUserTitle.Size = new System.Drawing.Size(60, 15);
            this.labUpdateUserTitle.TabIndex = 2;
            this.labUpdateUserTitle.Text = "更新人:";
            // 
            // labFileTypeTitle
            // 
            this.labFileTypeTitle.AutoSize = true;
            this.labFileTypeTitle.Location = new System.Drawing.Point(36, 284);
            this.labFileTypeTitle.Name = "labFileTypeTitle";
            this.labFileTypeTitle.Size = new System.Drawing.Size(75, 15);
            this.labFileTypeTitle.TabIndex = 2;
            this.labFileTypeTitle.Text = "文件类型:";
            // 
            // labFileVersionTitle
            // 
            this.labFileVersionTitle.AutoSize = true;
            this.labFileVersionTitle.Location = new System.Drawing.Point(36, 250);
            this.labFileVersionTitle.Name = "labFileVersionTitle";
            this.labFileVersionTitle.Size = new System.Drawing.Size(75, 15);
            this.labFileVersionTitle.TabIndex = 2;
            this.labFileVersionTitle.Text = "文件版本:";
            // 
            // labFileSizeTitle
            // 
            this.labFileSizeTitle.AutoSize = true;
            this.labFileSizeTitle.Location = new System.Drawing.Point(36, 216);
            this.labFileSizeTitle.Name = "labFileSizeTitle";
            this.labFileSizeTitle.Size = new System.Drawing.Size(75, 15);
            this.labFileSizeTitle.TabIndex = 2;
            this.labFileSizeTitle.Text = "文件大小:";
            // 
            // labFileNameTitle
            // 
            this.labFileNameTitle.AutoSize = true;
            this.labFileNameTitle.Location = new System.Drawing.Point(36, 182);
            this.labFileNameTitle.Name = "labFileNameTitle";
            this.labFileNameTitle.Size = new System.Drawing.Size(75, 15);
            this.labFileNameTitle.TabIndex = 1;
            this.labFileNameTitle.Text = "文件名称:";
            // 
            // labFileIdTitle
            // 
            this.labFileIdTitle.AutoSize = true;
            this.labFileIdTitle.Location = new System.Drawing.Point(36, 148);
            this.labFileIdTitle.Name = "labFileIdTitle";
            this.labFileIdTitle.Size = new System.Drawing.Size(75, 15);
            this.labFileIdTitle.TabIndex = 0;
            this.labFileIdTitle.Text = "文件编号:";
            // 
            // dgcFileId
            // 
            this.dgcFileId.DataPropertyName = "FileId";
            this.dgcFileId.HeaderText = "文件编号";
            this.dgcFileId.Name = "dgcFileId";
            this.dgcFileId.ReadOnly = true;
            // 
            // dgcFileName
            // 
            this.dgcFileName.DataPropertyName = "FileName";
            this.dgcFileName.HeaderText = "文件名称";
            this.dgcFileName.Name = "dgcFileName";
            this.dgcFileName.ReadOnly = true;
            // 
            // dgcFileSize
            // 
            this.dgcFileSize.DataPropertyName = "FileSize";
            this.dgcFileSize.HeaderText = "文件大小";
            this.dgcFileSize.Name = "dgcFileSize";
            this.dgcFileSize.ReadOnly = true;
            // 
            // dgcFileVersion
            // 
            this.dgcFileVersion.DataPropertyName = "FileVersion";
            this.dgcFileVersion.HeaderText = "文件版本";
            this.dgcFileVersion.Name = "dgcFileVersion";
            this.dgcFileVersion.ReadOnly = true;
            // 
            // dgcFileType
            // 
            this.dgcFileType.DataPropertyName = "FileType";
            this.dgcFileType.HeaderText = "文件类型";
            this.dgcFileType.Name = "dgcFileType";
            this.dgcFileType.ReadOnly = true;
            // 
            // dgcFileTime
            // 
            this.dgcFileTime.DataPropertyName = "FileTime";
            this.dgcFileTime.HeaderText = "生效时间";
            this.dgcFileTime.Name = "dgcFileTime";
            this.dgcFileTime.ReadOnly = true;
            // 
            // dgcIsUpdateApp
            // 
            this.dgcIsUpdateApp.DataPropertyName = "IsUpdateApp";
            this.dgcIsUpdateApp.HeaderText = "更新应用";
            this.dgcIsUpdateApp.Name = "dgcIsUpdateApp";
            this.dgcIsUpdateApp.ReadOnly = true;
            // 
            // dgcRemark
            // 
            this.dgcRemark.DataPropertyName = "Remark";
            this.dgcRemark.HeaderText = "备注";
            this.dgcRemark.Name = "dgcRemark";
            this.dgcRemark.ReadOnly = true;
            // 
            // dgcUseFlag
            // 
            this.dgcUseFlag.DataPropertyName = "UseFlag";
            this.dgcUseFlag.HeaderText = "是否可用";
            this.dgcUseFlag.Name = "dgcUseFlag";
            this.dgcUseFlag.ReadOnly = true;
            // 
            // dgcUpdateUser
            // 
            this.dgcUpdateUser.DataPropertyName = "UpdateUser";
            this.dgcUpdateUser.HeaderText = "更新用户";
            this.dgcUpdateUser.Name = "dgcUpdateUser";
            this.dgcUpdateUser.ReadOnly = true;
            // 
            // dgcUpdateTime
            // 
            this.dgcUpdateTime.DataPropertyName = "UpdateTime";
            this.dgcUpdateTime.HeaderText = "更新时间";
            this.dgcUpdateTime.Name = "dgcUpdateTime";
            this.dgcUpdateTime.ReadOnly = true;
            // 
            // FrmUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1282, 653);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmUploadFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传文件";
            this.Load += new System.EventHandler(this.FrmUploadFile_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbFileInfo.ResumeLayout(false);
            this.gbFileInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileInfo)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private System.Windows.Forms.GroupBox gbFileInfo;
        private System.Windows.Forms.DataGridView dgvFileInfo;
        private System.Windows.Forms.CheckBox chkUseFlag;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label labFileName_tj;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labUpdateTimeTitle;
        private System.Windows.Forms.Label labUpdateUserTitle;
        private System.Windows.Forms.Label labFileTypeTitle;
        private System.Windows.Forms.Label labFileVersionTitle;
        private System.Windows.Forms.Label labFileSizeTitle;
        private System.Windows.Forms.Label labFileNameTitle;
        private System.Windows.Forms.Label labFileIdTitle;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.Label labUpdateUser;
        private System.Windows.Forms.Label labUpdateTime;
        private System.Windows.Forms.Label labFileType;
        private System.Windows.Forms.Label labFileVersion;
        private System.Windows.Forms.Label labFileName;
        private System.Windows.Forms.Label labFileSize;
        private System.Windows.Forms.Label labFileId;
        private System.Windows.Forms.Label labFile;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox tbUploadFilePath;
        private System.Windows.Forms.TextBox tbRemark;
        private System.Windows.Forms.CheckBox chkIsUpdateApp;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFileTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcIsUpdateApp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUseFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateTime;
    }
}

