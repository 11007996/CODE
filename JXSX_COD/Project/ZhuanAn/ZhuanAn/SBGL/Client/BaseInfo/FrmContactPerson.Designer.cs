namespace Basic
{
    partial class FrmContactPerson
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
            this.tbWorkCode = new System.Windows.Forms.TextBox();
            this.tbRealName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.labWorkCode = new System.Windows.Forms.Label();
            this.labRealName = new System.Windows.Forms.Label();
            this.gbContactPerson = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvContactPerson = new System.Windows.Forms.DataGridView();
            this.dgcWorkCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbContactPerson.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContactPerson)).BeginInit();
            this.SuspendLayout();
            // 
            // tbWorkCode
            // 
            this.tbWorkCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbWorkCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbWorkCode.Location = new System.Drawing.Point(103, 9);
            this.tbWorkCode.Name = "tbWorkCode";
            this.tbWorkCode.Size = new System.Drawing.Size(114, 25);
            this.tbWorkCode.TabIndex = 3;
            // 
            // tbRealName
            // 
            this.tbRealName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbRealName.Location = new System.Drawing.Point(323, 9);
            this.tbRealName.Name = "tbRealName";
            this.tbRealName.Size = new System.Drawing.Size(114, 25);
            this.tbRealName.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(443, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labWorkCode
            // 
            this.labWorkCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labWorkCode.AutoSize = true;
            this.labWorkCode.Location = new System.Drawing.Point(52, 14);
            this.labWorkCode.Name = "labWorkCode";
            this.labWorkCode.Size = new System.Drawing.Size(45, 15);
            this.labWorkCode.TabIndex = 7;
            this.labWorkCode.Text = "工号:";
            // 
            // labRealName
            // 
            this.labRealName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labRealName.AutoSize = true;
            this.labRealName.Location = new System.Drawing.Point(272, 14);
            this.labRealName.Name = "labRealName";
            this.labRealName.Size = new System.Drawing.Size(45, 15);
            this.labRealName.TabIndex = 8;
            this.labRealName.Text = "姓名:";
            // 
            // gbContactPerson
            // 
            this.gbContactPerson.BackColor = System.Drawing.Color.Transparent;
            this.gbContactPerson.Controls.Add(this.tableLayoutPanel1);
            this.gbContactPerson.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbContactPerson.Location = new System.Drawing.Point(0, 0);
            this.gbContactPerson.Margin = new System.Windows.Forms.Padding(10);
            this.gbContactPerson.Name = "gbContactPerson";
            this.gbContactPerson.Padding = new System.Windows.Forms.Padding(10);
            this.gbContactPerson.Size = new System.Drawing.Size(816, 82);
            this.gbContactPerson.TabIndex = 12;
            this.gbContactPerson.TabStop = false;
            this.gbContactPerson.Text = "添加联系人";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labWorkCode, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbRealName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labRealName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbWorkCode, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 44);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // dgvContactPerson
            // 
            this.dgvContactPerson.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.GhostWhite;
            this.dgvContactPerson.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvContactPerson.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContactPerson.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContactPerson.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcWorkCode,
            this.dgcRealName});
            this.dgvContactPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContactPerson.Location = new System.Drawing.Point(0, 82);
            this.dgvContactPerson.Name = "dgvContactPerson";
            this.dgvContactPerson.ReadOnly = true;
            this.dgvContactPerson.RowHeadersVisible = false;
            this.dgvContactPerson.RowHeadersWidth = 51;
            this.dgvContactPerson.RowTemplate.Height = 27;
            this.dgvContactPerson.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvContactPerson.Size = new System.Drawing.Size(816, 349);
            this.dgvContactPerson.TabIndex = 13;
            this.dgvContactPerson.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvContactPerson_UserDeletingRow);
            // 
            // dgcWorkCode
            // 
            this.dgcWorkCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWorkCode.DataPropertyName = "WorkCode";
            this.dgcWorkCode.HeaderText = "工号";
            this.dgcWorkCode.MinimumWidth = 6;
            this.dgcWorkCode.Name = "dgcWorkCode";
            this.dgcWorkCode.ReadOnly = true;
            this.dgcWorkCode.Width = 120;
            // 
            // dgcRealName
            // 
            this.dgcRealName.DataPropertyName = "RealName";
            this.dgcRealName.HeaderText = "姓名";
            this.dgcRealName.MinimumWidth = 6;
            this.dgcRealName.Name = "dgcRealName";
            this.dgcRealName.ReadOnly = true;
            // 
            // FrmContactPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(816, 431);
            this.Controls.Add(this.dgvContactPerson);
            this.Controls.Add(this.gbContactPerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmContactPerson";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "联系人设置";
            this.Load += new System.EventHandler(this.FrmContactPerson_Load);
            this.gbContactPerson.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContactPerson)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbWorkCode;
        private System.Windows.Forms.TextBox tbRealName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labWorkCode;
        private System.Windows.Forms.Label labRealName;
        private System.Windows.Forms.GroupBox gbContactPerson;
        private System.Windows.Forms.DataGridView dgvContactPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWorkCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRealName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}