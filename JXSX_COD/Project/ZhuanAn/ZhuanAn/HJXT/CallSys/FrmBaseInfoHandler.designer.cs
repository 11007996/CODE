namespace CallSys
{
    partial class FrmBaseInfoHandler
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaseInfoHandler));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHandler = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcHandlerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgcHandlerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerRight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerHelp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerUseFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerUpdateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandlerUpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbHandler = new System.Windows.Forms.GroupBox();
            this.tbPicPath = new System.Windows.Forms.TextBox();
            this.pbHandlerPic = new System.Windows.Forms.PictureBox();
            this.clbHandlerArea = new System.Windows.Forms.CheckedListBox();
            this.labHadlerArea = new System.Windows.Forms.Label();
            this.chkUseFlag = new System.Windows.Forms.CheckBox();
            this.cmbHandlerLevel = new System.Windows.Forms.ComboBox();
            this.labHandlerType = new System.Windows.Forms.Label();
            this.cmbHandlerRight = new System.Windows.Forms.ComboBox();
            this.labHandlerRight = new System.Windows.Forms.Label();
            this.tbHandlerPwd = new System.Windows.Forms.TextBox();
            this.labHandlerPwd = new System.Windows.Forms.Label();
            this.labHandlerMessage = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.cmbHandlerState = new System.Windows.Forms.ComboBox();
            this.labHandlerState = new System.Windows.Forms.Label();
            this.cmbHandlerDept = new System.Windows.Forms.ComboBox();
            this.labHandlerDept = new System.Windows.Forms.Label();
            this.tbHandlerName = new System.Windows.Forms.TextBox();
            this.labHandlerName = new System.Windows.Forms.Label();
            this.tbHandlerNo = new System.Windows.Forms.TextBox();
            this.labHandlerNo = new System.Windows.Forms.Label();
            this.menuStripHandler = new System.Windows.Forms.MenuStrip();
            this.tsmHandlerAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerBack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLineInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHandlerExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBatchUpload = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandler)).BeginInit();
            this.gbHandler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHandlerPic)).BeginInit();
            this.menuStripHandler.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHandler
            // 
            this.dgvHandler.AllowUserToAddRows = false;
            this.dgvHandler.AllowUserToDeleteRows = false;
            this.dgvHandler.AllowUserToResizeRows = false;
            this.dgvHandler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHandler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHandler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcHandlerNo,
            this.dgcHandlerImage,
            this.dgcHandlerName,
            this.dgcHandlerDept,
            this.dgcHandlerState,
            this.dgcHandlerRight,
            this.dgcHandlerHelp,
            this.dgcHandlerArea,
            this.dgcHandlerUseFlag,
            this.dgcHandlerUpdateUser,
            this.dgcHandlerUpdateTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHandler.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHandler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHandler.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvHandler.Location = new System.Drawing.Point(15, 164);
            this.dgvHandler.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHandler.Name = "dgvHandler";
            this.dgvHandler.ReadOnly = true;
            this.dgvHandler.RowTemplate.Height = 65;
            this.dgvHandler.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHandler.Size = new System.Drawing.Size(1570, 621);
            this.dgvHandler.TabIndex = 2;
            this.dgvHandler.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHandler_CellClick);
            this.dgvHandler.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHandler_CellFormatting);
            // 
            // dgcHandlerNo
            // 
            this.dgcHandlerNo.DataPropertyName = "HandlerNo";
            this.dgcHandlerNo.FillWeight = 83.9467F;
            this.dgcHandlerNo.HeaderText = "工号";
            this.dgcHandlerNo.Name = "dgcHandlerNo";
            this.dgcHandlerNo.ReadOnly = true;
            // 
            // dgcHandlerImage
            // 
            this.dgcHandlerImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.dgcHandlerImage.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgcHandlerImage.HeaderText = "头像";
            this.dgcHandlerImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.dgcHandlerImage.Name = "dgcHandlerImage";
            this.dgcHandlerImage.ReadOnly = true;
            this.dgcHandlerImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcHandlerImage.Width = 60;
            // 
            // dgcHandlerName
            // 
            this.dgcHandlerName.DataPropertyName = "HandlerName";
            this.dgcHandlerName.FillWeight = 83.9467F;
            this.dgcHandlerName.HeaderText = "姓名";
            this.dgcHandlerName.Name = "dgcHandlerName";
            this.dgcHandlerName.ReadOnly = true;
            // 
            // dgcHandlerDept
            // 
            this.dgcHandlerDept.DataPropertyName = "HandlerDept";
            this.dgcHandlerDept.FillWeight = 83.9467F;
            this.dgcHandlerDept.HeaderText = "部门";
            this.dgcHandlerDept.Name = "dgcHandlerDept";
            this.dgcHandlerDept.ReadOnly = true;
            // 
            // dgcHandlerState
            // 
            this.dgcHandlerState.DataPropertyName = "HandlerState";
            this.dgcHandlerState.FillWeight = 83.9467F;
            this.dgcHandlerState.HeaderText = "状态";
            this.dgcHandlerState.Name = "dgcHandlerState";
            this.dgcHandlerState.ReadOnly = true;
            // 
            // dgcHandlerRight
            // 
            this.dgcHandlerRight.DataPropertyName = "HandlerRight";
            this.dgcHandlerRight.FillWeight = 83.9467F;
            this.dgcHandlerRight.HeaderText = "权限";
            this.dgcHandlerRight.Name = "dgcHandlerRight";
            this.dgcHandlerRight.ReadOnly = true;
            // 
            // dgcHandlerHelp
            // 
            this.dgcHandlerHelp.DataPropertyName = "HandlerLevel";
            this.dgcHandlerHelp.FillWeight = 83.9467F;
            this.dgcHandlerHelp.HeaderText = "类型";
            this.dgcHandlerHelp.Name = "dgcHandlerHelp";
            this.dgcHandlerHelp.ReadOnly = true;
            // 
            // dgcHandlerArea
            // 
            this.dgcHandlerArea.DataPropertyName = "Area";
            this.dgcHandlerArea.HeaderText = "区域";
            this.dgcHandlerArea.Name = "dgcHandlerArea";
            this.dgcHandlerArea.ReadOnly = true;
            // 
            // dgcHandlerUseFlag
            // 
            this.dgcHandlerUseFlag.DataPropertyName = "UseFlag";
            this.dgcHandlerUseFlag.HeaderText = "是否可用";
            this.dgcHandlerUseFlag.Name = "dgcHandlerUseFlag";
            this.dgcHandlerUseFlag.ReadOnly = true;
            // 
            // dgcHandlerUpdateUser
            // 
            this.dgcHandlerUpdateUser.DataPropertyName = "UpdateUser";
            this.dgcHandlerUpdateUser.FillWeight = 83.9467F;
            this.dgcHandlerUpdateUser.HeaderText = "修改人";
            this.dgcHandlerUpdateUser.Name = "dgcHandlerUpdateUser";
            this.dgcHandlerUpdateUser.ReadOnly = true;
            // 
            // dgcHandlerUpdateTime
            // 
            this.dgcHandlerUpdateTime.DataPropertyName = "UpdateTime";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.dgcHandlerUpdateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcHandlerUpdateTime.FillWeight = 83.9467F;
            this.dgcHandlerUpdateTime.HeaderText = "修改时间";
            this.dgcHandlerUpdateTime.Name = "dgcHandlerUpdateTime";
            this.dgcHandlerUpdateTime.ReadOnly = true;
            // 
            // gbHandler
            // 
            this.gbHandler.BackColor = System.Drawing.Color.White;
            this.gbHandler.Controls.Add(this.tbPicPath);
            this.gbHandler.Controls.Add(this.pbHandlerPic);
            this.gbHandler.Controls.Add(this.clbHandlerArea);
            this.gbHandler.Controls.Add(this.labHadlerArea);
            this.gbHandler.Controls.Add(this.chkUseFlag);
            this.gbHandler.Controls.Add(this.cmbHandlerLevel);
            this.gbHandler.Controls.Add(this.labHandlerType);
            this.gbHandler.Controls.Add(this.cmbHandlerRight);
            this.gbHandler.Controls.Add(this.labHandlerRight);
            this.gbHandler.Controls.Add(this.tbHandlerPwd);
            this.gbHandler.Controls.Add(this.labHandlerPwd);
            this.gbHandler.Controls.Add(this.labHandlerMessage);
            this.gbHandler.Controls.Add(this.btnUpload);
            this.gbHandler.Controls.Add(this.cmbHandlerState);
            this.gbHandler.Controls.Add(this.labHandlerState);
            this.gbHandler.Controls.Add(this.cmbHandlerDept);
            this.gbHandler.Controls.Add(this.labHandlerDept);
            this.gbHandler.Controls.Add(this.tbHandlerName);
            this.gbHandler.Controls.Add(this.labHandlerName);
            this.gbHandler.Controls.Add(this.tbHandlerNo);
            this.gbHandler.Controls.Add(this.labHandlerNo);
            this.gbHandler.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbHandler.Location = new System.Drawing.Point(15, 35);
            this.gbHandler.Margin = new System.Windows.Forms.Padding(4);
            this.gbHandler.Name = "gbHandler";
            this.gbHandler.Padding = new System.Windows.Forms.Padding(4);
            this.gbHandler.Size = new System.Drawing.Size(1570, 129);
            this.gbHandler.TabIndex = 1;
            this.gbHandler.TabStop = false;
            this.gbHandler.Text = "人员信息";
            // 
            // tbPicPath
            // 
            this.tbPicPath.Location = new System.Drawing.Point(1389, 67);
            this.tbPicPath.Name = "tbPicPath";
            this.tbPicPath.Size = new System.Drawing.Size(50, 25);
            this.tbPicPath.TabIndex = 24;
            this.tbPicPath.Visible = false;
            // 
            // pbHandlerPic
            // 
            this.pbHandlerPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbHandlerPic.Location = new System.Drawing.Point(1278, 18);
            this.pbHandlerPic.Name = "pbHandlerPic";
            this.pbHandlerPic.Size = new System.Drawing.Size(104, 104);
            this.pbHandlerPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHandlerPic.TabIndex = 23;
            this.pbHandlerPic.TabStop = false;
            // 
            // clbHandlerArea
            // 
            this.clbHandlerArea.CheckOnClick = true;
            this.clbHandlerArea.FormattingEnabled = true;
            this.clbHandlerArea.Location = new System.Drawing.Point(1156, 18);
            this.clbHandlerArea.Name = "clbHandlerArea";
            this.clbHandlerArea.Size = new System.Drawing.Size(100, 104);
            this.clbHandlerArea.TabIndex = 22;
            // 
            // labHadlerArea
            // 
            this.labHadlerArea.AutoSize = true;
            this.labHadlerArea.Location = new System.Drawing.Point(1105, 22);
            this.labHadlerArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHadlerArea.Name = "labHadlerArea";
            this.labHadlerArea.Size = new System.Drawing.Size(52, 15);
            this.labHadlerArea.TabIndex = 21;
            this.labHadlerArea.Text = "区域：";
            // 
            // chkUseFlag
            // 
            this.chkUseFlag.AutoSize = true;
            this.chkUseFlag.Location = new System.Drawing.Point(901, 32);
            this.chkUseFlag.Name = "chkUseFlag";
            this.chkUseFlag.Size = new System.Drawing.Size(89, 19);
            this.chkUseFlag.TabIndex = 19;
            this.chkUseFlag.Text = "是否可用";
            this.chkUseFlag.UseVisualStyleBackColor = true;
            // 
            // cmbHandlerLevel
            // 
            this.cmbHandlerLevel.FormattingEnabled = true;
            this.cmbHandlerLevel.Items.AddRange(new object[] {
            "",
            "工程师",
            "高级工程师"});
            this.cmbHandlerLevel.Location = new System.Drawing.Point(692, 34);
            this.cmbHandlerLevel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHandlerLevel.Name = "cmbHandlerLevel";
            this.cmbHandlerLevel.Size = new System.Drawing.Size(160, 23);
            this.cmbHandlerLevel.TabIndex = 18;
            // 
            // labHandlerType
            // 
            this.labHandlerType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerType.AutoSize = true;
            this.labHandlerType.Location = new System.Drawing.Point(629, 38);
            this.labHandlerType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerType.Name = "labHandlerType";
            this.labHandlerType.Size = new System.Drawing.Size(52, 15);
            this.labHandlerType.TabIndex = 17;
            this.labHandlerType.Text = "类型：";
            // 
            // cmbHandlerRight
            // 
            this.cmbHandlerRight.FormattingEnabled = true;
            this.cmbHandlerRight.Location = new System.Drawing.Point(949, 89);
            this.cmbHandlerRight.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHandlerRight.Name = "cmbHandlerRight";
            this.cmbHandlerRight.Size = new System.Drawing.Size(160, 23);
            this.cmbHandlerRight.TabIndex = 16;
            // 
            // labHandlerRight
            // 
            this.labHandlerRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerRight.AutoSize = true;
            this.labHandlerRight.Location = new System.Drawing.Point(898, 94);
            this.labHandlerRight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerRight.Name = "labHandlerRight";
            this.labHandlerRight.Size = new System.Drawing.Size(52, 15);
            this.labHandlerRight.TabIndex = 15;
            this.labHandlerRight.Text = "权限：";
            // 
            // tbHandlerPwd
            // 
            this.tbHandlerPwd.Location = new System.Drawing.Point(692, 89);
            this.tbHandlerPwd.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerPwd.Name = "tbHandlerPwd";
            this.tbHandlerPwd.PasswordChar = '*';
            this.tbHandlerPwd.Size = new System.Drawing.Size(160, 25);
            this.tbHandlerPwd.TabIndex = 14;
            // 
            // labHandlerPwd
            // 
            this.labHandlerPwd.AutoSize = true;
            this.labHandlerPwd.Location = new System.Drawing.Point(629, 94);
            this.labHandlerPwd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerPwd.Name = "labHandlerPwd";
            this.labHandlerPwd.Size = new System.Drawing.Size(52, 15);
            this.labHandlerPwd.TabIndex = 13;
            this.labHandlerPwd.Text = "密码：";
            // 
            // labHandlerMessage
            // 
            this.labHandlerMessage.AutoSize = true;
            this.labHandlerMessage.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labHandlerMessage.ForeColor = System.Drawing.Color.Green;
            this.labHandlerMessage.Location = new System.Drawing.Point(1527, 33);
            this.labHandlerMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerMessage.Name = "labHandlerMessage";
            this.labHandlerMessage.Size = new System.Drawing.Size(0, 18);
            this.labHandlerMessage.TabIndex = 12;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(1389, 15);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(50, 29);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnHandlerUpload_Click);
            // 
            // cmbHandlerState
            // 
            this.cmbHandlerState.FormattingEnabled = true;
            this.cmbHandlerState.Items.AddRange(new object[] {
            "",
            "等待中",
            "对应中",
            "其它任务中"});
            this.cmbHandlerState.Location = new System.Drawing.Point(397, 90);
            this.cmbHandlerState.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHandlerState.Name = "cmbHandlerState";
            this.cmbHandlerState.Size = new System.Drawing.Size(160, 23);
            this.cmbHandlerState.TabIndex = 8;
            // 
            // labHandlerState
            // 
            this.labHandlerState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerState.AutoSize = true;
            this.labHandlerState.Location = new System.Drawing.Point(335, 94);
            this.labHandlerState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerState.Name = "labHandlerState";
            this.labHandlerState.Size = new System.Drawing.Size(52, 15);
            this.labHandlerState.TabIndex = 7;
            this.labHandlerState.Text = "状态：";
            // 
            // cmbHandlerDept
            // 
            this.cmbHandlerDept.FormattingEnabled = true;
            this.cmbHandlerDept.Items.AddRange(new object[] {
            "",
            "资讯"});
            this.cmbHandlerDept.Location = new System.Drawing.Point(397, 34);
            this.cmbHandlerDept.Margin = new System.Windows.Forms.Padding(4);
            this.cmbHandlerDept.Name = "cmbHandlerDept";
            this.cmbHandlerDept.Size = new System.Drawing.Size(160, 23);
            this.cmbHandlerDept.TabIndex = 6;
            // 
            // labHandlerDept
            // 
            this.labHandlerDept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerDept.AutoSize = true;
            this.labHandlerDept.Location = new System.Drawing.Point(335, 38);
            this.labHandlerDept.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerDept.Name = "labHandlerDept";
            this.labHandlerDept.Size = new System.Drawing.Size(52, 15);
            this.labHandlerDept.TabIndex = 5;
            this.labHandlerDept.Text = "部门：";
            // 
            // tbHandlerName
            // 
            this.tbHandlerName.Location = new System.Drawing.Point(116, 89);
            this.tbHandlerName.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerName.Name = "tbHandlerName";
            this.tbHandlerName.Size = new System.Drawing.Size(160, 25);
            this.tbHandlerName.TabIndex = 3;
            // 
            // labHandlerName
            // 
            this.labHandlerName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerName.AutoSize = true;
            this.labHandlerName.Location = new System.Drawing.Point(53, 94);
            this.labHandlerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerName.Name = "labHandlerName";
            this.labHandlerName.Size = new System.Drawing.Size(52, 15);
            this.labHandlerName.TabIndex = 2;
            this.labHandlerName.Text = "姓名：";
            // 
            // tbHandlerNo
            // 
            this.tbHandlerNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbHandlerNo.Location = new System.Drawing.Point(116, 33);
            this.tbHandlerNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbHandlerNo.Name = "tbHandlerNo";
            this.tbHandlerNo.Size = new System.Drawing.Size(160, 25);
            this.tbHandlerNo.TabIndex = 1;
            // 
            // labHandlerNo
            // 
            this.labHandlerNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labHandlerNo.AutoSize = true;
            this.labHandlerNo.Location = new System.Drawing.Point(53, 38);
            this.labHandlerNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerNo.Name = "labHandlerNo";
            this.labHandlerNo.Size = new System.Drawing.Size(52, 15);
            this.labHandlerNo.TabIndex = 0;
            this.labHandlerNo.Text = "工号：";
            // 
            // menuStripHandler
            // 
            this.menuStripHandler.BackColor = System.Drawing.Color.Transparent;
            this.menuStripHandler.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripHandler.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHandlerAdd,
            this.tsmHandlerUpdate,
            this.tsmHandlerSave,
            this.tsmHandlerSelect,
            this.tsmHandlerDelete,
            this.tsmHandlerRefresh,
            this.tsmHandlerBack,
            this.tsmHandlerReset,
            this.tsmLineInfo,
            this.tsmHandlerImport,
            this.tsmHandlerExport,
            this.tsmBatchUpload});
            this.menuStripHandler.Location = new System.Drawing.Point(15, 0);
            this.menuStripHandler.Name = "menuStripHandler";
            this.menuStripHandler.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStripHandler.Size = new System.Drawing.Size(1570, 35);
            this.menuStripHandler.TabIndex = 0;
            this.menuStripHandler.Text = "menuStrip1";
            // 
            // tsmHandlerAdd
            // 
            this.tsmHandlerAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerAdd.Image")));
            this.tsmHandlerAdd.Name = "tsmHandlerAdd";
            this.tsmHandlerAdd.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerAdd.Text = "新增";
            this.tsmHandlerAdd.Click += new System.EventHandler(this.tsmHandlerAdd_Click);
            // 
            // tsmHandlerUpdate
            // 
            this.tsmHandlerUpdate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerUpdate.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerUpdate.Image")));
            this.tsmHandlerUpdate.Name = "tsmHandlerUpdate";
            this.tsmHandlerUpdate.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerUpdate.Text = "修改";
            this.tsmHandlerUpdate.Click += new System.EventHandler(this.tsmHandlerUpdate_Click);
            // 
            // tsmHandlerSave
            // 
            this.tsmHandlerSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerSave.Image")));
            this.tsmHandlerSave.Name = "tsmHandlerSave";
            this.tsmHandlerSave.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerSave.Text = "保存";
            this.tsmHandlerSave.Click += new System.EventHandler(this.tsmHandlerSave_Click);
            // 
            // tsmHandlerSelect
            // 
            this.tsmHandlerSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerSelect.Image")));
            this.tsmHandlerSelect.Name = "tsmHandlerSelect";
            this.tsmHandlerSelect.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerSelect.Text = "查询";
            this.tsmHandlerSelect.Click += new System.EventHandler(this.tsmHandlerSelect_Click);
            // 
            // tsmHandlerDelete
            // 
            this.tsmHandlerDelete.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerDelete.Image")));
            this.tsmHandlerDelete.Name = "tsmHandlerDelete";
            this.tsmHandlerDelete.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerDelete.Text = "删除";
            this.tsmHandlerDelete.Click += new System.EventHandler(this.tsmHandlerDelete_Click);
            // 
            // tsmHandlerRefresh
            // 
            this.tsmHandlerRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerRefresh.Image")));
            this.tsmHandlerRefresh.Name = "tsmHandlerRefresh";
            this.tsmHandlerRefresh.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerRefresh.Text = "刷新";
            this.tsmHandlerRefresh.Click += new System.EventHandler(this.tsmHandlerRefresh_Click);
            // 
            // tsmHandlerBack
            // 
            this.tsmHandlerBack.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerBack.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerBack.Image")));
            this.tsmHandlerBack.Name = "tsmHandlerBack";
            this.tsmHandlerBack.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerBack.Text = "返回";
            this.tsmHandlerBack.Click += new System.EventHandler(this.tsmHandlerBack_Click);
            // 
            // tsmHandlerReset
            // 
            this.tsmHandlerReset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerReset.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerReset.Image")));
            this.tsmHandlerReset.Name = "tsmHandlerReset";
            this.tsmHandlerReset.Size = new System.Drawing.Size(124, 31);
            this.tsmHandlerReset.Text = "重置状态";
            this.tsmHandlerReset.Click += new System.EventHandler(this.tsmHandlerReset_Click);
            // 
            // tsmLineInfo
            // 
            this.tsmLineInfo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmLineInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsmLineInfo.Image")));
            this.tsmLineInfo.Name = "tsmLineInfo";
            this.tsmLineInfo.Size = new System.Drawing.Size(124, 31);
            this.tsmLineInfo.Text = "区域线别";
            this.tsmLineInfo.Click += new System.EventHandler(this.tsmLineInfo_Click);
            // 
            // tsmHandlerImport
            // 
            this.tsmHandlerImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmHandlerImport.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerImport.Image")));
            this.tsmHandlerImport.Name = "tsmHandlerImport";
            this.tsmHandlerImport.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerImport.Text = "导入";
            this.tsmHandlerImport.Click += new System.EventHandler(this.tsmHandlerImport_Click);
            // 
            // tsmHandlerExport
            // 
            this.tsmHandlerExport.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsmHandlerExport.Image = ((System.Drawing.Image)(resources.GetObject("tsmHandlerExport.Image")));
            this.tsmHandlerExport.Name = "tsmHandlerExport";
            this.tsmHandlerExport.Size = new System.Drawing.Size(84, 31);
            this.tsmHandlerExport.Text = "导出";
            this.tsmHandlerExport.Click += new System.EventHandler(this.tsmHandlerExport_Click);
            // 
            // tsmBatchUpload
            // 
            this.tsmBatchUpload.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmBatchUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsmBatchUpload.Image")));
            this.tsmBatchUpload.Name = "tsmBatchUpload";
            this.tsmBatchUpload.Size = new System.Drawing.Size(124, 31);
            this.tsmBatchUpload.Text = "批量上传";
            this.tsmBatchUpload.Click += new System.EventHandler(this.tmsBatchUpload_Click);
            // 
            // FrmBaseInfoHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1600, 800);
            this.Controls.Add(this.dgvHandler);
            this.Controls.Add(this.gbHandler);
            this.Controls.Add(this.menuStripHandler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStripHandler;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBaseInfoHandler";
            this.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.Text = "信息维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBaseInfoHandler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHandler)).EndInit();
            this.gbHandler.ResumeLayout(false);
            this.gbHandler.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHandlerPic)).EndInit();
            this.menuStripHandler.ResumeLayout(false);
            this.menuStripHandler.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripHandler;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerExport;
        private System.Windows.Forms.GroupBox gbHandler;
        private System.Windows.Forms.Label labHandlerNo;
        private System.Windows.Forms.Label labHandlerName;
        private System.Windows.Forms.TextBox tbHandlerNo;
        private System.Windows.Forms.ComboBox cmbHandlerDept;
        private System.Windows.Forms.Label labHandlerDept;
        private System.Windows.Forms.TextBox tbHandlerName;
        private System.Windows.Forms.ComboBox cmbHandlerState;
        private System.Windows.Forms.Label labHandlerState;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerSave;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerBack;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvHandler;
        private System.Windows.Forms.Label labHandlerMessage;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerRefresh;
        private System.Windows.Forms.Label labHandlerPwd;
        private System.Windows.Forms.TextBox tbHandlerPwd;
        private System.Windows.Forms.Label labHandlerRight;
        private System.Windows.Forms.ComboBox cmbHandlerRight;
        private System.Windows.Forms.ComboBox cmbHandlerLevel;
        private System.Windows.Forms.Label labHandlerType;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerReset;
        private System.Windows.Forms.CheckBox chkUseFlag;
        private System.Windows.Forms.Label labHadlerArea;
        private System.Windows.Forms.ToolStripMenuItem tsmLineInfo;
        private System.Windows.Forms.CheckedListBox clbHandlerArea;
        private System.Windows.Forms.PictureBox pbHandlerPic;
        private System.Windows.Forms.TextBox tbPicPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerNo;
        private System.Windows.Forms.DataGridViewImageColumn dgcHandlerImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerState;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerRight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerUseFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerUpdateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandlerUpdateTime;
        private System.Windows.Forms.ToolStripMenuItem tsmBatchUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmHandlerImport;
    }
}