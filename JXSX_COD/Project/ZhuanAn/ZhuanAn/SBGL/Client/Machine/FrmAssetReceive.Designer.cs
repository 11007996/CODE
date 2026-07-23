namespace Machine
{
    partial class FrmAssetReceive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAssetReceive));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAssetInfo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRecevie = new DevComponents.DotNetBar.ButtonX();
            this.btnReturn = new DevComponents.DotNetBar.ButtonX();
            this.cmbLine = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dgvLineAsset = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dgcLineAssetNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineAssetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labMessage = new System.Windows.Forms.Label();
            this.txbAsset = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiReceiveRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineAsset)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "产线";
            // 
            // dgvAssetInfo
            // 
            this.dgvAssetInfo.AllowUserToAddRows = false;
            this.dgvAssetInfo.AllowUserToDeleteRows = false;
            this.dgvAssetInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssetInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssetInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAssetNo,
            this.dgcAssetName,
            this.dgcLine});
            this.tableLayoutPanel1.SetColumnSpan(this.dgvAssetInfo, 2);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssetInfo.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAssetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssetInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvAssetInfo.Location = new System.Drawing.Point(3, 43);
            this.dgvAssetInfo.Name = "dgvAssetInfo";
            this.dgvAssetInfo.ReadOnly = true;
            this.dgvAssetInfo.RowHeadersVisible = false;
            this.dgvAssetInfo.RowHeadersWidth = 51;
            this.dgvAssetInfo.RowTemplate.Height = 27;
            this.dgvAssetInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssetInfo.Size = new System.Drawing.Size(604, 641);
            this.dgvAssetInfo.TabIndex = 2;
            // 
            // dgcAssetNo
            // 
            this.dgcAssetNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcAssetNo.DataPropertyName = "AssetNo";
            this.dgcAssetNo.HeaderText = "资产编号";
            this.dgcAssetNo.MinimumWidth = 6;
            this.dgcAssetNo.Name = "dgcAssetNo";
            this.dgcAssetNo.ReadOnly = true;
            this.dgcAssetNo.Width = 150;
            // 
            // dgcAssetName
            // 
            this.dgcAssetName.DataPropertyName = "AssetName";
            this.dgcAssetName.HeaderText = "资产名称";
            this.dgcAssetName.MinimumWidth = 6;
            this.dgcAssetName.Name = "dgcAssetName";
            this.dgcAssetName.ReadOnly = true;
            // 
            // dgcLine
            // 
            this.dgcLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcLine.DataPropertyName = "Line";
            this.dgcLine.HeaderText = "关联产线";
            this.dgcLine.MinimumWidth = 6;
            this.dgcLine.Name = "dgcLine";
            this.dgcLine.ReadOnly = true;
            this.dgcLine.Width = 80;
            // 
            // btnRecevie
            // 
            this.btnRecevie.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRecevie.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRecevie.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRecevie.Image = ((System.Drawing.Image)(resources.GetObject("btnRecevie.Image")));
            this.btnRecevie.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btnRecevie.Location = new System.Drawing.Point(26, 243);
            this.btnRecevie.Name = "btnRecevie";
            this.btnRecevie.Size = new System.Drawing.Size(91, 30);
            this.btnRecevie.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRecevie.TabIndex = 4;
            this.btnRecevie.Text = "领用";
            this.btnRecevie.Click += new System.EventHandler(this.btnRecevie_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReturn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.Location = new System.Drawing.Point(26, 414);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(91, 30);
            this.btnReturn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "归还";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.DisplayMember = "Text";
            this.cmbLine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.ItemHeight = 20;
            this.cmbLine.Location = new System.Drawing.Point(63, 7);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(210, 26);
            this.cmbLine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLine.TabIndex = 6;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // dgvLineAsset
            // 
            this.dgvLineAsset.AllowUserToAddRows = false;
            this.dgvLineAsset.AllowUserToDeleteRows = false;
            this.dgvLineAsset.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineAsset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLineAsset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcLineAssetNo,
            this.dgcLineAssetName,
            this.dgcReceiveTime});
            this.tableLayoutPanel2.SetColumnSpan(this.dgvLineAsset, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineAsset.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLineAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLineAsset.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvLineAsset.Location = new System.Drawing.Point(3, 43);
            this.dgvLineAsset.Name = "dgvLineAsset";
            this.dgvLineAsset.ReadOnly = true;
            this.dgvLineAsset.RowHeadersVisible = false;
            this.dgvLineAsset.RowHeadersWidth = 51;
            this.dgvLineAsset.RowTemplate.Height = 27;
            this.dgvLineAsset.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineAsset.Size = new System.Drawing.Size(604, 641);
            this.dgvLineAsset.TabIndex = 7;
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
            // labMessage
            // 
            this.labMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(0, 728);
            this.labMessage.Name = "labMessage";
            this.labMessage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labMessage.Size = new System.Drawing.Size(1382, 25);
            this.labMessage.TabIndex = 9;
            this.labMessage.Text = "提示";
            // 
            // txbAsset
            // 
            this.txbAsset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.txbAsset.Border.Class = "TextBoxBorder";
            this.txbAsset.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbAsset.Location = new System.Drawing.Point(103, 7);
            this.txbAsset.Name = "txbAsset";
            this.txbAsset.PreventEnterBeep = true;
            this.txbAsset.Size = new System.Drawing.Size(269, 25);
            this.txbAsset.TabIndex = 7;
            this.txbAsset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbAsset_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "名称或编号";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReceiveRecord});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1382, 35);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiReceiveRecord
            // 
            this.tsmiReceiveRecord.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tsmiReceiveRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsmiReceiveRecord.Image")));
            this.tsmiReceiveRecord.Name = "tsmiReceiveRecord";
            this.tsmiReceiveRecord.Size = new System.Drawing.Size(126, 31);
            this.tsmiReceiveRecord.Text = "领用记录";
            this.tsmiReceiveRecord.Click += new System.EventHandler(this.tsmiReceiveRecord_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbAsset, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvAssetInfo, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(610, 687);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dgvLineAsset, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbLine, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(769, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(610, 687);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btnReturn, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btnRecevie, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(619, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(144, 687);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1382, 693);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // FrmAssetReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1382, 753);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmAssetReceive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产线资产领用";
            this.Load += new System.EventHandler(this.FrmAssetReceive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineAsset)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvAssetInfo;
        private DevComponents.DotNetBar.ButtonX btnRecevie;
        private DevComponents.DotNetBar.ButtonX btnReturn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLine;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvLineAsset;
        private DevComponents.DotNetBar.Controls.TextBoxX txbAsset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineAssetNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcLineAssetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcReceiveTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiReceiveRecord;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}