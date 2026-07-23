namespace EAM.Listen.UI
{
    partial class FrmItemCodeConfig
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
            this.dgv_ItemCode = new System.Windows.Forms.DataGridView();
            this.dgc_ItemName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgc_Sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_ByteLen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_fixedCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tlp_CaseData = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ItemCode)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tlp_CaseData.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_ItemCode
            // 
            this.dgv_ItemCode.AllowUserToAddRows = false;
            this.dgv_ItemCode.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SeaShell;
            this.dgv_ItemCode.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ItemCode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ItemCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ItemCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgc_ItemName,
            this.dgc_Sort,
            this.dgc_ByteLen,
            this.dgc_fixedCode});
            this.dgv_ItemCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ItemCode.Location = new System.Drawing.Point(0, 0);
            this.dgv_ItemCode.Name = "dgv_ItemCode";
            this.dgv_ItemCode.ReadOnly = true;
            this.dgv_ItemCode.RowHeadersWidth = 51;
            this.dgv_ItemCode.RowTemplate.Height = 27;
            this.dgv_ItemCode.Size = new System.Drawing.Size(624, 368);
            this.dgv_ItemCode.TabIndex = 28;
            // 
            // dgc_ItemName
            // 
            this.dgc_ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgc_ItemName.DataPropertyName = "ItemName";
            this.dgc_ItemName.HeaderText = "项目名称";
            this.dgc_ItemName.MinimumWidth = 6;
            this.dgc_ItemName.Name = "dgc_ItemName";
            this.dgc_ItemName.ReadOnly = true;
            this.dgc_ItemName.Width = 120;
            // 
            // dgc_Sort
            // 
            this.dgc_Sort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgc_Sort.DataPropertyName = "Sort";
            this.dgc_Sort.HeaderText = "顺序";
            this.dgc_Sort.MinimumWidth = 6;
            this.dgc_Sort.Name = "dgc_Sort";
            this.dgc_Sort.ReadOnly = true;
            this.dgc_Sort.Width = 80;
            // 
            // dgc_ByteLen
            // 
            this.dgc_ByteLen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgc_ByteLen.DataPropertyName = "ByteLen";
            this.dgc_ByteLen.HeaderText = "字节长度";
            this.dgc_ByteLen.MinimumWidth = 6;
            this.dgc_ByteLen.Name = "dgc_ByteLen";
            this.dgc_ByteLen.ReadOnly = true;
            this.dgc_ByteLen.Width = 80;
            // 
            // dgc_fixedCode
            // 
            this.dgc_fixedCode.DataPropertyName = "FixedCode";
            this.dgc_fixedCode.HeaderText = "固定编码";
            this.dgc_fixedCode.MinimumWidth = 6;
            this.dgc_fixedCode.Name = "dgc_fixedCode";
            this.dgc_fixedCode.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 77);
            this.panel1.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.tlp_CaseData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(81, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 77);
            this.panel2.TabIndex = 2;
            // 
            // tlp_CaseData
            // 
            this.tlp_CaseData.AutoSize = true;
            this.tlp_CaseData.BackColor = System.Drawing.Color.Cornsilk;
            this.tlp_CaseData.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tlp_CaseData.ColumnCount = 2;
            this.tlp_CaseData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_CaseData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_CaseData.Controls.Add(this.label3, 0, 0);
            this.tlp_CaseData.Controls.Add(this.label4, 0, 1);
            this.tlp_CaseData.Dock = System.Windows.Forms.DockStyle.Left;
            this.tlp_CaseData.Location = new System.Drawing.Point(0, 0);
            this.tlp_CaseData.Name = "tlp_CaseData";
            this.tlp_CaseData.RowCount = 2;
            this.tlp_CaseData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_CaseData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_CaseData.Size = new System.Drawing.Size(115, 77);
            this.tlp_CaseData.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "前缀";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "EF EF";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCopy);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(81, 77);
            this.panel3.TabIndex = 3;
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCopy.Location = new System.Drawing.Point(0, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(81, 37);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "复制";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "示例数据";
            // 
            // FrmItemCodeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 445);
            this.Controls.Add(this.dgv_ItemCode);
            this.Controls.Add(this.panel1);
            this.Name = "FrmItemCodeConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目编码配置";
            this.Load += new System.EventHandler(this.FrmItemCodeConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ItemCode)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tlp_CaseData.ResumeLayout(false);
            this.tlp_CaseData.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_ItemCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tlp_CaseData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgc_ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Sort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_ByteLen;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_fixedCode;
    }
}