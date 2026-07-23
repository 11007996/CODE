namespace CallSys
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLineInfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcFactory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAreaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tbLine = new System.Windows.Forms.TextBox();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.labLine = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLineInfo
            // 
            this.dgvLineInfo.AllowUserToAddRows = false;
            this.dgvLineInfo.AllowUserToDeleteRows = false;
            this.dgvLineInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcFactory,
            this.dgcAreaName,
            this.dgcLineName});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineInfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLineInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLineInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvLineInfo.Name = "dgvLineInfo";
            this.dgvLineInfo.ReadOnly = true;
            this.dgvLineInfo.RowTemplate.Height = 27;
            this.dgvLineInfo.Size = new System.Drawing.Size(368, 452);
            this.dgvLineInfo.TabIndex = 0;
            // 
            // dgcFactory
            // 
            this.dgcFactory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcFactory.DataPropertyName = "Factory";
            this.dgcFactory.HeaderText = "工厂名称";
            this.dgcFactory.Name = "dgcFactory";
            this.dgcFactory.ReadOnly = true;
            this.dgcFactory.Visible = false;
            this.dgcFactory.Width = 150;
            // 
            // dgcAreaName
            // 
            this.dgcAreaName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAreaName.DataPropertyName = "Area";
            this.dgcAreaName.HeaderText = "区域名称";
            this.dgcAreaName.Name = "dgcAreaName";
            this.dgcAreaName.ReadOnly = true;
            // 
            // dgcLineName
            // 
            this.dgcLineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcLineName.DataPropertyName = "Line";
            this.dgcLineName.HeaderText = "线别名称";
            this.dgcLineName.Name = "dgcLineName";
            this.dgcLineName.ReadOnly = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvLineInfo);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnDel);
            this.splitContainer.Panel2.Controls.Add(this.btnAdd);
            this.splitContainer.Panel2.Controls.Add(this.tbLine);
            this.splitContainer.Panel2.Controls.Add(this.tbArea);
            this.splitContainer.Panel2.Controls.Add(this.labLine);
            this.splitContainer.Panel2.Controls.Add(this.labArea);
            this.splitContainer.Panel2.Controls.Add(this.labMessage);
            this.splitContainer.Size = new System.Drawing.Size(636, 452);
            this.splitContainer.SplitterDistance = 368;
            this.splitContainer.TabIndex = 1;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDel.Location = new System.Drawing.Point(147, 245);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 30);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.Location = new System.Drawing.Point(39, 245);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tbLine
            // 
            this.tbLine.Location = new System.Drawing.Point(79, 187);
            this.tbLine.Name = "tbLine";
            this.tbLine.Size = new System.Drawing.Size(143, 25);
            this.tbLine.TabIndex = 7;
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(79, 141);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(143, 25);
            this.tbArea.TabIndex = 6;
            // 
            // labLine
            // 
            this.labLine.AutoSize = true;
            this.labLine.Location = new System.Drawing.Point(36, 190);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 5;
            this.labLine.Text = "线别";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(36, 144);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(37, 15);
            this.labArea.TabIndex = 4;
            this.labArea.Text = "区域";
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(12, 11);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(0, 15);
            this.labMessage.TabIndex = 3;
            // 
            // FrmLineInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 452);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLineInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "产线设置";
            this.Load += new System.EventHandler(this.FrmLineInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineInfo)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLineInfo;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAreaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineName;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox tbLine;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label labArea;
    }
}