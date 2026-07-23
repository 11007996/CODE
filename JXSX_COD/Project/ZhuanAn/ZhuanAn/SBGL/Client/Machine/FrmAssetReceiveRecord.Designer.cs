namespace Machine
{
    partial class FrmAssetReceiveRecord
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
            this.dgvLineAsset = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkNotReturn = new System.Windows.Forms.CheckBox();
            this.txbAsset = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmbLine = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labAsset = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineAsset)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLineAsset
            // 
            this.dgvLineAsset.AllowUserToAddRows = false;
            this.dgvLineAsset.AllowUserToDeleteRows = false;
            this.dgvLineAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcLine,
            this.dgcLineAssetNo,
            this.dgcLineAssetName,
            this.dgcReceiveTime,
            this.dgcEndTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineAsset.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLineAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineAsset.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLineAsset.Location = new System.Drawing.Point(0, 40);
            this.dgvLineAsset.Name = "dgvLineAsset";
            this.dgvLineAsset.ReadOnly = true;
            this.dgvLineAsset.RowHeadersVisible = false;
            this.dgvLineAsset.RowHeadersWidth = 51;
            this.dgvLineAsset.RowTemplate.Height = 27;
            this.dgvLineAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineAsset.Size = new System.Drawing.Size(761, 390);
            this.dgvLineAsset.TabIndex = 8;
            // 
            // dgcLine
            // 
            this.dgcLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcLine.DataPropertyName = "Line";
            this.dgcLine.HeaderText = "产线";
            this.dgcLine.MinimumWidth = 6;
            this.dgcLine.Name = "dgcLine";
            this.dgcLine.ReadOnly = true;
            this.dgcLine.Width = 125;
            // 
            // dgcLineAssetNo
            // 
            this.dgcLineAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcLineAssetNo.DataPropertyName = "AssetNo";
            this.dgcLineAssetNo.HeaderText = "资产编号";
            this.dgcLineAssetNo.MinimumWidth = 6;
            this.dgcLineAssetNo.Name = "dgcLineAssetNo";
            this.dgcLineAssetNo.ReadOnly = true;
            this.dgcLineAssetNo.Width = 150;
            // 
            // dgcLineAssetName
            // 
            this.dgcLineAssetName.DataPropertyName = "AssetName";
            this.dgcLineAssetName.HeaderText = "资产名称";
            this.dgcLineAssetName.MinimumWidth = 6;
            this.dgcLineAssetName.Name = "dgcLineAssetName";
            this.dgcLineAssetName.ReadOnly = true;
            // 
            // dgcReceiveTime
            // 
            this.dgcReceiveTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcReceiveTime.DataPropertyName = "StartTime";
            this.dgcReceiveTime.HeaderText = "领用时间";
            this.dgcReceiveTime.MinimumWidth = 6;
            this.dgcReceiveTime.Name = "dgcReceiveTime";
            this.dgcReceiveTime.ReadOnly = true;
            this.dgcReceiveTime.Width = 125;
            // 
            // dgcEndTime
            // 
            this.dgcEndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcEndTime.DataPropertyName = "EndTime";
            this.dgcEndTime.HeaderText = "归还时间";
            this.dgcEndTime.MinimumWidth = 6;
            this.dgcEndTime.Name = "dgcEndTime";
            this.dgcEndTime.ReadOnly = true;
            this.dgcEndTime.Width = 125;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkNotReturn, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbAsset, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labAsset, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(761, 40);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // chkNotReturn
            // 
            this.chkNotReturn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkNotReturn.AutoSize = true;
            this.chkNotReturn.Location = new System.Drawing.Point(493, 10);
            this.chkNotReturn.Name = "chkNotReturn";
            this.chkNotReturn.Size = new System.Drawing.Size(74, 19);
            this.chkNotReturn.TabIndex = 4;
            this.chkNotReturn.Text = "未归还";
            this.chkNotReturn.UseVisualStyleBackColor = true;
            this.chkNotReturn.CheckedChanged += new System.EventHandler(this.chkNotReturn_CheckedChanged);
            // 
            // txbAsset
            // 
            this.txbAsset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.txbAsset.Border.Class = "TextBoxBorder";
            this.txbAsset.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbAsset.Location = new System.Drawing.Point(243, 7);
            this.txbAsset.Name = "txbAsset";
            this.txbAsset.PreventEnterBeep = true;
            this.txbAsset.Size = new System.Drawing.Size(239, 25);
            this.txbAsset.TabIndex = 3;
            this.txbAsset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbAsset_KeyPress);
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.DisplayMember = "Text";
            this.cmbLine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.ItemHeight = 19;
            this.cmbLine.Location = new System.Drawing.Point(63, 7);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(114, 25);
            this.cmbLine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLine.TabIndex = 2;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // labAsset
            // 
            this.labAsset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labAsset.AutoSize = true;
            this.labAsset.Location = new System.Drawing.Point(200, 12);
            this.labAsset.Name = "labAsset";
            this.labAsset.Size = new System.Drawing.Size(37, 15);
            this.labAsset.TabIndex = 1;
            this.labAsset.Text = "资产";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "产线";
            // 
            // FrmAssetReceiveRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(761, 430);
            this.Controls.Add(this.dgvLineAsset);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmAssetReceiveRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "领用归还记录";
            this.Load += new System.EventHandler(this.FrmAssetReceiveRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineAsset)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLineAsset;
        private DevComponents.DotNetBar.Controls.TextBoxX txbAsset;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLine;
        private System.Windows.Forms.Label labAsset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkNotReturn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcReceiveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEndTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}