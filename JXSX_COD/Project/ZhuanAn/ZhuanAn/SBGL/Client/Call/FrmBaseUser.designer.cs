namespace Call
{
    partial class FrmBaseUser
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseUser));
            this.dgvUser = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcUserNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUserState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUserRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUserLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUseFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHasUserImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbUser = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labUserNo = new System.Windows.Forms.Label();
            this.tbPicPath = new System.Windows.Forms.TextBox();
            this.tbUserNo = new System.Windows.Forms.TextBox();
            this.pbUserImage = new System.Windows.Forms.PictureBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.labDept = new System.Windows.Forms.Label();
            this.clbArea = new System.Windows.Forms.CheckedListBox();
            this.cmbDept = new System.Windows.Forms.ComboBox();
            this.labArea = new System.Windows.Forms.Label();
            this.labUserLevel = new System.Windows.Forms.Label();
            this.cmbUserRight = new System.Windows.Forms.ComboBox();
            this.chkUseFlag = new System.Windows.Forms.CheckBox();
            this.labUserRight = new System.Windows.Forms.Label();
            this.cmbUserLevel = new System.Windows.Forms.ComboBox();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.labUserName = new System.Windows.Forms.Label();
            this.labPwd = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.labUserState = new System.Windows.Forms.Label();
            this.cmbUserState = new System.Windows.Forms.ComboBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.menuStripHandler = new System.Windows.Forms.MenuStrip();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBatchUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSyncaContact = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.gbUser.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImage)).BeginInit();
            this.menuStripHandler.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcUserNo,
            this.dgcUserName,
            this.dgcDept,
            this.dgcUserState,
            this.dgcUserRight,
            this.dgcUserLevel,
            this.dgcArea,
            this.dgcUseFlag,
            this.dgcHasUserImage,
            this.dgcUpdateUser,
            this.dgcUpdateTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUser.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvUser.Location = new System.Drawing.Point(15, 199);
            this.dgvUser.Margin = new System.Windows.Forms.Padding(4);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersWidth = 51;
            this.dgvUser.Size = new System.Drawing.Size(1570, 586);
            this.dgvUser.TabIndex = 2;
            this.dgvUser.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUser_CellClick);
            this.dgvUser.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUser_CellFormatting);
            // 
            // dgcUserNo
            // 
            this.dgcUserNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcUserNo.DataPropertyName = "UserNo";
            this.dgcUserNo.FillWeight = 83.9467F;
            this.dgcUserNo.HeaderText = "工号";
            this.dgcUserNo.MinimumWidth = 6;
            this.dgcUserNo.Name = "dgcUserNo";
            this.dgcUserNo.ReadOnly = true;
            this.dgcUserNo.Width = 125;
            // 
            // dgcUserName
            // 
            this.dgcUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcUserName.DataPropertyName = "UserName";
            this.dgcUserName.FillWeight = 83.9467F;
            this.dgcUserName.HeaderText = "姓名";
            this.dgcUserName.MinimumWidth = 6;
            this.dgcUserName.Name = "dgcUserName";
            this.dgcUserName.ReadOnly = true;
            this.dgcUserName.Width = 125;
            // 
            // dgcDept
            // 
            this.dgcDept.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcDept.DataPropertyName = "Dept";
            this.dgcDept.FillWeight = 83.9467F;
            this.dgcDept.HeaderText = "部门";
            this.dgcDept.MinimumWidth = 6;
            this.dgcDept.Name = "dgcDept";
            this.dgcDept.ReadOnly = true;
            this.dgcDept.Width = 140;
            // 
            // dgcUserState
            // 
            this.dgcUserState.DataPropertyName = "UserState";
            this.dgcUserState.FillWeight = 83.9467F;
            this.dgcUserState.HeaderText = "状态";
            this.dgcUserState.MinimumWidth = 6;
            this.dgcUserState.Name = "dgcUserState";
            this.dgcUserState.ReadOnly = true;
            // 
            // dgcUserRight
            // 
            this.dgcUserRight.DataPropertyName = "UserRight";
            this.dgcUserRight.FillWeight = 83.9467F;
            this.dgcUserRight.HeaderText = "权限";
            this.dgcUserRight.MinimumWidth = 6;
            this.dgcUserRight.Name = "dgcUserRight";
            this.dgcUserRight.ReadOnly = true;
            // 
            // dgcUserLevel
            // 
            this.dgcUserLevel.DataPropertyName = "UserLevel";
            this.dgcUserLevel.FillWeight = 83.9467F;
            this.dgcUserLevel.HeaderText = "类型";
            this.dgcUserLevel.MinimumWidth = 6;
            this.dgcUserLevel.Name = "dgcUserLevel";
            this.dgcUserLevel.ReadOnly = true;
            // 
            // dgcArea
            // 
            this.dgcArea.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcArea.DataPropertyName = "Area";
            this.dgcArea.HeaderText = "区域";
            this.dgcArea.MinimumWidth = 6;
            this.dgcArea.Name = "dgcArea";
            this.dgcArea.ReadOnly = true;
            this.dgcArea.Width = 125;
            // 
            // dgcUseFlag
            // 
            this.dgcUseFlag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcUseFlag.DataPropertyName = "UseFlag";
            this.dgcUseFlag.HeaderText = "是否可用";
            this.dgcUseFlag.MinimumWidth = 6;
            this.dgcUseFlag.Name = "dgcUseFlag";
            this.dgcUseFlag.ReadOnly = true;
            this.dgcUseFlag.Width = 80;
            // 
            // dgcHasUserImage
            // 
            this.dgcHasUserImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHasUserImage.DataPropertyName = "HasUserImage";
            this.dgcHasUserImage.HeaderText = "头像";
            this.dgcHasUserImage.MinimumWidth = 6;
            this.dgcHasUserImage.Name = "dgcHasUserImage";
            this.dgcHasUserImage.ReadOnly = true;
            this.dgcHasUserImage.Width = 80;
            // 
            // dgcUpdateUser
            // 
            this.dgcUpdateUser.DataPropertyName = "UpdateUser";
            this.dgcUpdateUser.FillWeight = 83.9467F;
            this.dgcUpdateUser.HeaderText = "修改人";
            this.dgcUpdateUser.MinimumWidth = 6;
            this.dgcUpdateUser.Name = "dgcUpdateUser";
            this.dgcUpdateUser.ReadOnly = true;
            // 
            // dgcUpdateTime
            // 
            this.dgcUpdateTime.DataPropertyName = "UpdateTime";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.dgcUpdateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcUpdateTime.FillWeight = 83.9467F;
            this.dgcUpdateTime.HeaderText = "修改时间";
            this.dgcUpdateTime.MinimumWidth = 6;
            this.dgcUpdateTime.Name = "dgcUpdateTime";
            this.dgcUpdateTime.ReadOnly = true;
            // 
            // gbUser
            // 
            this.gbUser.AutoSize = true;
            this.gbUser.BackColor = System.Drawing.Color.Transparent;
            this.gbUser.Controls.Add(this.tableLayoutPanel1);
            this.gbUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbUser.Location = new System.Drawing.Point(15, 35);
            this.gbUser.Name = "gbUser";
            this.gbUser.Size = new System.Drawing.Size(1570, 129);
            this.gbUser.TabIndex = 1;
            this.gbUser.TabStop = false;
            this.gbUser.Text = "人员信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 14;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labUserNo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbPicPath, 11, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbUserNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbUserImage, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUpload, 11, 0);
            this.tableLayoutPanel1.Controls.Add(this.labDept, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbArea, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDept, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labArea, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.labUserLevel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbUserRight, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkUseFlag, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.labUserRight, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbUserLevel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbPwd, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labUserName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labPwd, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbUserName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labUserState, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbUserState, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1564, 105);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // labUserNo
            // 
            this.labUserNo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserNo.AutoSize = true;
            this.labUserNo.Location = new System.Drawing.Point(40, 10);
            this.labUserNo.Name = "labUserNo";
            this.labUserNo.Size = new System.Drawing.Size(37, 15);
            this.labUserNo.TabIndex = 0;
            this.labUserNo.Text = "工号";
            // 
            // tbPicPath
            // 
            this.tbPicPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.tbPicPath, 2);
            this.tbPicPath.Location = new System.Drawing.Point(1223, 75);
            this.tbPicPath.Name = "tbPicPath";
            this.tbPicPath.ReadOnly = true;
            this.tbPicPath.Size = new System.Drawing.Size(318, 25);
            this.tbPicPath.TabIndex = 24;
            // 
            // tbUserNo
            // 
            this.tbUserNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbUserNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbUserNo.Location = new System.Drawing.Point(83, 5);
            this.tbUserNo.Name = "tbUserNo";
            this.tbUserNo.Size = new System.Drawing.Size(150, 25);
            this.tbUserNo.TabIndex = 1;
            // 
            // pbUserImage
            // 
            this.pbUserImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbUserImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbUserImage.Location = new System.Drawing.Point(1123, 0);
            this.pbUserImage.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.pbUserImage.Name = "pbUserImage";
            this.tableLayoutPanel1.SetRowSpan(this.pbUserImage, 3);
            this.pbUserImage.Size = new System.Drawing.Size(97, 105);
            this.pbUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUserImage.TabIndex = 23;
            this.pbUserImage.TabStop = false;
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnUpload.Location = new System.Drawing.Point(1223, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(50, 27);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // labDept
            // 
            this.labDept.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDept.AutoSize = true;
            this.labDept.Location = new System.Drawing.Point(280, 10);
            this.labDept.Name = "labDept";
            this.labDept.Size = new System.Drawing.Size(37, 15);
            this.labDept.TabIndex = 5;
            this.labDept.Text = "部门";
            // 
            // clbArea
            // 
            this.clbArea.CheckOnClick = true;
            this.clbArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbArea.FormattingEnabled = true;
            this.clbArea.Location = new System.Drawing.Point(1040, 0);
            this.clbArea.Margin = new System.Windows.Forms.Padding(0);
            this.clbArea.Name = "clbArea";
            this.tableLayoutPanel1.SetRowSpan(this.clbArea, 3);
            this.clbArea.Size = new System.Drawing.Size(80, 105);
            this.clbArea.TabIndex = 22;
            // 
            // cmbDept
            // 
            this.cmbDept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Items.AddRange(new object[] {
            "",
            "资讯"});
            this.cmbDept.Location = new System.Drawing.Point(323, 6);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(150, 23);
            this.cmbDept.TabIndex = 6;
            // 
            // labArea
            // 
            this.labArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(1000, 10);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(37, 15);
            this.labArea.TabIndex = 21;
            this.labArea.Text = "区域";
            // 
            // labUserLevel
            // 
            this.labUserLevel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserLevel.AutoSize = true;
            this.labUserLevel.Location = new System.Drawing.Point(520, 10);
            this.labUserLevel.Name = "labUserLevel";
            this.labUserLevel.Size = new System.Drawing.Size(37, 15);
            this.labUserLevel.TabIndex = 17;
            this.labUserLevel.Text = "类型";
            // 
            // cmbUserRight
            // 
            this.cmbUserRight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbUserRight.FormattingEnabled = true;
            this.cmbUserRight.Location = new System.Drawing.Point(803, 41);
            this.cmbUserRight.Name = "cmbUserRight";
            this.cmbUserRight.Size = new System.Drawing.Size(150, 23);
            this.cmbUserRight.TabIndex = 16;
            // 
            // chkUseFlag
            // 
            this.chkUseFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkUseFlag.AutoSize = true;
            this.chkUseFlag.Location = new System.Drawing.Point(803, 8);
            this.chkUseFlag.Name = "chkUseFlag";
            this.chkUseFlag.Size = new System.Drawing.Size(89, 19);
            this.chkUseFlag.TabIndex = 19;
            this.chkUseFlag.Text = "是否可用";
            this.chkUseFlag.UseVisualStyleBackColor = true;
            // 
            // labUserRight
            // 
            this.labUserRight.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserRight.AutoSize = true;
            this.labUserRight.Location = new System.Drawing.Point(760, 45);
            this.labUserRight.Name = "labUserRight";
            this.labUserRight.Size = new System.Drawing.Size(37, 15);
            this.labUserRight.TabIndex = 15;
            this.labUserRight.Text = "权限";
            // 
            // cmbUserLevel
            // 
            this.cmbUserLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbUserLevel.FormattingEnabled = true;
            this.cmbUserLevel.Items.AddRange(new object[] {
            "",
            "工程师",
            "高级工程师"});
            this.cmbUserLevel.Location = new System.Drawing.Point(563, 6);
            this.cmbUserLevel.Name = "cmbUserLevel";
            this.cmbUserLevel.Size = new System.Drawing.Size(150, 23);
            this.cmbUserLevel.TabIndex = 18;
            // 
            // tbPwd
            // 
            this.tbPwd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbPwd.Location = new System.Drawing.Point(563, 40);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.PasswordChar = '*';
            this.tbPwd.Size = new System.Drawing.Size(150, 25);
            this.tbPwd.TabIndex = 14;
            // 
            // labUserName
            // 
            this.labUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserName.AutoSize = true;
            this.labUserName.Location = new System.Drawing.Point(40, 45);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(37, 15);
            this.labUserName.TabIndex = 2;
            this.labUserName.Text = "姓名";
            // 
            // labPwd
            // 
            this.labPwd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labPwd.AutoSize = true;
            this.labPwd.Location = new System.Drawing.Point(520, 45);
            this.labPwd.Name = "labPwd";
            this.labPwd.Size = new System.Drawing.Size(37, 15);
            this.labPwd.TabIndex = 13;
            this.labPwd.Text = "密码";
            // 
            // tbUserName
            // 
            this.tbUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbUserName.Location = new System.Drawing.Point(83, 40);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(150, 25);
            this.tbUserName.TabIndex = 3;
            // 
            // labUserState
            // 
            this.labUserState.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labUserState.AutoSize = true;
            this.labUserState.Location = new System.Drawing.Point(280, 45);
            this.labUserState.Name = "labUserState";
            this.labUserState.Size = new System.Drawing.Size(37, 15);
            this.labUserState.TabIndex = 7;
            this.labUserState.Text = "状态";
            // 
            // cmbUserState
            // 
            this.cmbUserState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbUserState.FormattingEnabled = true;
            this.cmbUserState.Items.AddRange(new object[] {
            "",
            "等待中",
            "对应中",
            "其它任务中"});
            this.cmbUserState.Location = new System.Drawing.Point(323, 41);
            this.cmbUserState.Name = "cmbUserState";
            this.cmbUserState.Size = new System.Drawing.Size(150, 23);
            this.cmbUserState.TabIndex = 8;
            // 
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(15, 164);
            this.labMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(1570, 35);
            this.labMessage.TabIndex = 12;
            this.labMessage.Text = "提示";
            // 
            // menuStripHandler
            // 
            this.menuStripHandler.BackColor = System.Drawing.Color.Transparent;
            this.menuStripHandler.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.menuStripHandler.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripHandler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRefresh,
            this.tsmiSelect,
            this.tsmiAdd,
            this.tsmiUpdate,
            this.tsmiDelete,
            this.tsmiSave,
            this.tsmiBack,
            this.tsmiReset,
            this.tsmiImport,
            this.tsmiExport,
            this.tsmBatchUpload,
            this.tsmiSyncaContact});
            this.menuStripHandler.Location = new System.Drawing.Point(15, 0);
            this.menuStripHandler.Name = "menuStripHandler";
            this.menuStripHandler.Size = new System.Drawing.Size(1570, 35);
            this.menuStripHandler.TabIndex = 0;
            this.menuStripHandler.Text = "menuStrip1";
            // 
            // tsmiRefresh
            // 
            this.tsmiRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiRefresh.Image")));
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(86, 31);
            this.tsmiRefresh.Text = "刷新";
            this.tsmiRefresh.Click += new System.EventHandler(this.tsmiRefresh_Click);
            // 
            // tsmiSelect
            // 
            this.tsmiSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSelect.Image")));
            this.tsmiSelect.Name = "tsmiSelect";
            this.tsmiSelect.Size = new System.Drawing.Size(86, 31);
            this.tsmiSelect.Text = "查询";
            this.tsmiSelect.Click += new System.EventHandler(this.tsmiSelect_Click);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAdd.Image")));
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(86, 31);
            this.tsmiAdd.Text = "新增";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUpdate.Image")));
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(86, 31);
            this.tsmiUpdate.Text = "修改";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDelete.Image")));
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(86, 31);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(86, 31);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiBack
            // 
            this.tsmiBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmiBack.Image")));
            this.tsmiBack.Name = "tsmiBack";
            this.tsmiBack.Size = new System.Drawing.Size(86, 31);
            this.tsmiBack.Text = "返回";
            this.tsmiBack.Click += new System.EventHandler(this.tsmiBack_Click);
            // 
            // tsmiReset
            // 
            this.tsmiReset.Image = ((System.Drawing.Image)(resources.GetObject("tsmiReset.Image")));
            this.tsmiReset.Name = "tsmiReset";
            this.tsmiReset.Size = new System.Drawing.Size(126, 31);
            this.tsmiReset.Text = "重置状态";
            this.tsmiReset.Click += new System.EventHandler(this.tsmiReset_Click);
            // 
            // tsmiImport
            // 
            this.tsmiImport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiImport.Image")));
            this.tsmiImport.Name = "tsmiImport";
            this.tsmiImport.Size = new System.Drawing.Size(86, 31);
            this.tsmiImport.Text = "导入";
            this.tsmiImport.Click += new System.EventHandler(this.tsmiImport_Click);
            // 
            // tsmiExport
            // 
            this.tsmiExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmiExport.Image")));
            this.tsmiExport.Name = "tsmiExport";
            this.tsmiExport.Size = new System.Drawing.Size(86, 31);
            this.tsmiExport.Text = "导出";
            this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
            // 
            // tsmBatchUpload
            // 
            this.tsmBatchUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsmBatchUpload.Image")));
            this.tsmBatchUpload.Name = "tsmBatchUpload";
            this.tsmBatchUpload.Size = new System.Drawing.Size(126, 31);
            this.tsmBatchUpload.Text = "批量上传";
            this.tsmBatchUpload.Click += new System.EventHandler(this.tmsBatchUpload_Click);
            // 
            // tsmiSyncaContact
            // 
            this.tsmiSyncaContact.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSyncaContact.Image")));
            this.tsmiSyncaContact.Name = "tsmiSyncaContact";
            this.tsmiSyncaContact.Size = new System.Drawing.Size(146, 31);
            this.tsmiSyncaContact.Text = "同步联系人";
            this.tsmiSyncaContact.Click += new System.EventHandler(this.tsmiSyncaContact_Click);
            // 
            // FrmBaseUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.gbUser);
            this.Controls.Add(this.menuStripHandler);
            this.MainMenuStrip = this.menuStripHandler;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseUser";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "用户信息";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.gbUser.ResumeLayout(false);
            this.gbUser.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserImage)).EndInit();
            this.menuStripHandler.ResumeLayout(false);
            this.menuStripHandler.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripHandler;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiExport;
        private System.Windows.Forms.GroupBox gbUser;
        private System.Windows.Forms.Label labUserNo;
        private System.Windows.Forms.Label labUserName;
        private System.Windows.Forms.TextBox tbUserNo;
        private System.Windows.Forms.ComboBox cmbDept;
        private System.Windows.Forms.Label labDept;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.ComboBox cmbUserState;
        private System.Windows.Forms.Label labUserState;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiBack;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvUser;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
        private System.Windows.Forms.Label labPwd;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Label labUserRight;
        private System.Windows.Forms.ComboBox cmbUserRight;
        private System.Windows.Forms.ComboBox cmbUserLevel;
        private System.Windows.Forms.Label labUserLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmiReset;
        private System.Windows.Forms.CheckBox chkUseFlag;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.CheckedListBox clbArea;
        private System.Windows.Forms.PictureBox pbUserImage;
        private System.Windows.Forms.TextBox tbPicPath;
        private System.Windows.Forms.ToolStripMenuItem tsmBatchUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiImport;
        private System.Windows.Forms.ToolStripMenuItem tsmiSyncaContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUserNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUserState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUserRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUserLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUseFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHasUserImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcUpdateTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}