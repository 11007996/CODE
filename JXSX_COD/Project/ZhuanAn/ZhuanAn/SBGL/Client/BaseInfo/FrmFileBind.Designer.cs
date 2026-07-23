namespace Basic
{
    partial class FrmFileBind
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txbFileAliasName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbFileId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labCount = new System.Windows.Forms.Label();
            this.dgvFileBind = new System.Windows.Forms.DataGridView();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileBind)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txbFileAliasName, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbFileId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.labCount, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txbFileAliasName
            // 
            this.txbFileAliasName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbFileAliasName.Location = new System.Drawing.Point(263, 7);
            this.txbFileAliasName.Name = "txbFileAliasName";
            this.txbFileAliasName.Size = new System.Drawing.Size(244, 25);
            this.txbFileAliasName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件名";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件ID";
            // 
            // txbFileId
            // 
            this.txbFileId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbFileId.Location = new System.Drawing.Point(83, 7);
            this.txbFileId.Name = "txbFileId";
            this.txbFileId.Size = new System.Drawing.Size(94, 25);
            this.txbFileId.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(537, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "已关联总计:";
            // 
            // labCount
            // 
            this.labCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCount.AutoSize = true;
            this.labCount.ForeColor = System.Drawing.Color.Blue;
            this.labCount.Location = new System.Drawing.Point(633, 12);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(15, 15);
            this.labCount.TabIndex = 5;
            this.labCount.Text = "0";
            // 
            // dgvFileBind
            // 
            this.dgvFileBind.AllowUserToAddRows = false;
            this.dgvFileBind.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Honeydew;
            this.dgvFileBind.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFileBind.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFileBind.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFileBind.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAssetNo,
            this.dgcAssetName});
            this.dgvFileBind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFileBind.Location = new System.Drawing.Point(0, 40);
            this.dgvFileBind.Name = "dgvFileBind";
            this.dgvFileBind.ReadOnly = true;
            this.dgvFileBind.RowHeadersVisible = false;
            this.dgvFileBind.RowHeadersWidth = 51;
            this.dgvFileBind.RowTemplate.Height = 27;
            this.dgvFileBind.Size = new System.Drawing.Size(800, 410);
            this.dgvFileBind.TabIndex = 1;
            // 
            // dgcAssetNo
            // 
            this.dgcAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetNo.DataPropertyName = "AssetNo";
            this.dgcAssetNo.HeaderText = "资产编号";
            this.dgcAssetNo.MinimumWidth = 6;
            this.dgcAssetNo.Name = "dgcAssetNo";
            this.dgcAssetNo.ReadOnly = true;
            this.dgcAssetNo.Width = 180;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.ReadOnly = true;
            // 
            // FrmFileBind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvFileBind);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmFileBind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件绑定资产";
            this.Load += new System.EventHandler(this.FrmFileBind_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFileBind)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFileBind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbFileId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbFileAliasName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
    }
}