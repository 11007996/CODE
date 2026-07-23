namespace Call
{
    partial class FrmLineQC
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
            this.dgvLineQC = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcQCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcQCWorkCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.cmbQC = new System.Windows.Forms.ComboBox();
            this.labLine = new System.Windows.Forms.Label();
            this.labQCName = new System.Windows.Forms.Label();
            this.labMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineQC)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLineQC
            // 
            this.dgvLineQC.AllowUserToAddRows = false;
            this.dgvLineQC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineQC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineQC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcLine,
            this.dgcQCName,
            this.dgcQCWorkCode});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineQC.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLineQC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineQC.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLineQC.Location = new System.Drawing.Point(0, 65);
            this.dgvLineQC.Name = "dgvLineQC";
            this.dgvLineQC.ReadOnly = true;
            this.dgvLineQC.RowHeadersVisible = false;
            this.dgvLineQC.RowHeadersWidth = 51;
            this.dgvLineQC.RowTemplate.Height = 27;
            this.dgvLineQC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineQC.Size = new System.Drawing.Size(532, 387);
            this.dgvLineQC.TabIndex = 0;
            this.dgvLineQC.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvLineQC_UserDeletingRow);
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
            // dgcQCName
            // 
            this.dgcQCName.DataPropertyName = "QCName";
            this.dgcQCName.HeaderText = "品管名称";
            this.dgcQCName.MinimumWidth = 6;
            this.dgcQCName.Name = "dgcQCName";
            this.dgcQCName.ReadOnly = true;
            // 
            // dgcQCWorkCode
            // 
            this.dgcQCWorkCode.DataPropertyName = "QCWorkCode";
            this.dgcQCWorkCode.HeaderText = "品管工号";
            this.dgcQCWorkCode.MinimumWidth = 6;
            this.dgcQCWorkCode.Name = "dgcQCWorkCode";
            this.dgcQCWorkCode.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAdd.Location = new System.Drawing.Point(443, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 29);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.Location = new System.Drawing.Point(73, 6);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(144, 23);
            this.cmbLine.TabIndex = 7;
            // 
            // cmbQC
            // 
            this.cmbQC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbQC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbQC.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbQC.Location = new System.Drawing.Point(293, 6);
            this.cmbQC.Name = "cmbQC";
            this.cmbQC.Size = new System.Drawing.Size(144, 23);
            this.cmbQC.TabIndex = 6;
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLine.AutoSize = true;
            this.labLine.Location = new System.Drawing.Point(30, 10);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 5;
            this.labLine.Text = "线别";
            // 
            // labQCName
            // 
            this.labQCName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labQCName.AutoSize = true;
            this.labQCName.Location = new System.Drawing.Point(250, 10);
            this.labQCName.Name = "labQCName";
            this.labQCName.Size = new System.Drawing.Size(37, 15);
            this.labQCName.TabIndex = 4;
            this.labQCName.Text = "品管";
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 5);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(492, 42);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 3;
            this.labMessage.Text = "提示";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labLine, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbQC, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labQCName, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(532, 65);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // FrmLineQC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(532, 452);
            this.Controls.Add(this.dgvLineQC);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLineQC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "产线品管设置";
            this.Load += new System.EventHandler(this.FrmLineQC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineQC)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLineQC;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.ComboBox cmbQC;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label labQCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcQCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcQCWorkCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}