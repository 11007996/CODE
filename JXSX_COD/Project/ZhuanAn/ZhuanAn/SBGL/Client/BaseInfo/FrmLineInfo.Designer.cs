namespace Basic
{
    partial class FrmLineInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLineInfo = new System.Windows.Forms.DataGridView();
            this.dgcId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.tbLine = new System.Windows.Forms.TextBox();
            this.labLine = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLineInfo
            // 
            this.dgvLineInfo.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvLineInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLineInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcId,
            this.dgcFactory,
            this.dgcArea,
            this.dgcLine});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineInfo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLineInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLineInfo.Location = new System.Drawing.Point(0, 63);
            this.dgvLineInfo.Name = "dgvLineInfo";
            this.dgvLineInfo.ReadOnly = true;
            this.dgvLineInfo.RowHeadersVisible = false;
            this.dgvLineInfo.RowHeadersWidth = 51;
            this.dgvLineInfo.RowTemplate.Height = 27;
            this.dgvLineInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineInfo.Size = new System.Drawing.Size(884, 349);
            this.dgvLineInfo.TabIndex = 0;
            this.dgvLineInfo.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvLineInfo_UserDeletingRow);
            // 
            // dgcId
            // 
            this.dgcId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcId.DataPropertyName = "Id";
            this.dgcId.HeaderText = "产线Id(编码)";
            this.dgcId.MinimumWidth = 6;
            this.dgcId.Name = "dgcId";
            this.dgcId.ReadOnly = true;
            this.dgcId.Width = 125;
            // 
            // dgcFactory
            // 
            this.dgcFactory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFactory.DataPropertyName = "Factory";
            this.dgcFactory.HeaderText = "工厂名称";
            this.dgcFactory.MinimumWidth = 6;
            this.dgcFactory.Name = "dgcFactory";
            this.dgcFactory.ReadOnly = true;
            this.dgcFactory.Visible = false;
            this.dgcFactory.Width = 150;
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
            // dgcLine
            // 
            this.dgcLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcLine.DataPropertyName = "Line";
            this.dgcLine.HeaderText = "线别名称";
            this.dgcLine.MinimumWidth = 6;
            this.dgcLine.Name = "dgcLine";
            this.dgcLine.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(443, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbArea
            // 
            this.tbArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbArea.Location = new System.Drawing.Point(103, 7);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(114, 25);
            this.tbArea.TabIndex = 6;
            // 
            // tbLine
            // 
            this.tbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbLine.Location = new System.Drawing.Point(323, 7);
            this.tbLine.Name = "tbLine";
            this.tbLine.Size = new System.Drawing.Size(114, 25);
            this.tbLine.TabIndex = 7;
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLine.AutoSize = true;
            this.labLine.Location = new System.Drawing.Point(280, 12);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 5;
            this.labLine.Text = "线别";
            // 
            // labArea
            // 
            this.labArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(60, 12);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(37, 15);
            this.labArea.TabIndex = 4;
            this.labArea.Text = "区域";
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(543, 12);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 3;
            this.labMessage.Text = "提示";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(884, 63);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加产线";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbArea, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labLine, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbLine, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(878, 39);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // FrmLineInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 412);
            this.Controls.Add(this.dgvLineInfo);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLineInfo";
            this.Text = "产线区域设置";
            this.Load += new System.EventHandler(this.FrmLineInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLineInfo;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbLine;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}