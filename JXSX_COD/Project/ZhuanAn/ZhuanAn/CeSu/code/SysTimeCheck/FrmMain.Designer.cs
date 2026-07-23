namespace SysTimeCheck
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvErrorRecord = new System.Windows.Forms.DataGridView();
            this.dgcCurrTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPreTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcErrorMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Time1_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统时间";
            // 
            // labTime
            // 
            this.labTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTime.AutoSize = true;
            this.labTime.ForeColor = System.Drawing.Color.Blue;
            this.labTime.Location = new System.Drawing.Point(123, 7);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(159, 15);
            this.labTime.TabIndex = 1;
            this.labTime.Text = "yyyy-MM-dd HH:mm:ss";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "问题次数";
            // 
            // labCount
            // 
            this.labCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labCount.AutoSize = true;
            this.labCount.ForeColor = System.Drawing.Color.Red;
            this.labCount.Location = new System.Drawing.Point(123, 36);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(15, 15);
            this.labCount.TabIndex = 0;
            this.labCount.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(483, 58);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // dgvErrorRecord
            // 
            this.dgvErrorRecord.AllowUserToAddRows = false;
            this.dgvErrorRecord.AllowUserToDeleteRows = false;
            this.dgvErrorRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvErrorRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrorRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcCurrTime,
            this.dgcPreTime,
            this.dgcErrorMsg});
            this.dgvErrorRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvErrorRecord.Location = new System.Drawing.Point(0, 58);
            this.dgvErrorRecord.Name = "dgvErrorRecord";
            this.dgvErrorRecord.ReadOnly = true;
            this.dgvErrorRecord.RowHeadersVisible = false;
            this.dgvErrorRecord.RowHeadersWidth = 51;
            this.dgvErrorRecord.RowTemplate.Height = 27;
            this.dgvErrorRecord.Size = new System.Drawing.Size(483, 228);
            this.dgvErrorRecord.TabIndex = 4;
            // 
            // dgcCurrTime
            // 
            this.dgcCurrTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcCurrTime.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgcCurrTime.HeaderText = "当前时间";
            this.dgcCurrTime.MinimumWidth = 6;
            this.dgcCurrTime.Name = "dgcCurrTime";
            this.dgcCurrTime.ReadOnly = true;
            this.dgcCurrTime.Width = 130;
            // 
            // dgcPreTime
            // 
            this.dgcPreTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgcPreTime.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgcPreTime.HeaderText = "上个时间";
            this.dgcPreTime.MinimumWidth = 6;
            this.dgcPreTime.Name = "dgcPreTime";
            this.dgcPreTime.ReadOnly = true;
            this.dgcPreTime.Width = 130;
            // 
            // dgcErrorMsg
            // 
            this.dgcErrorMsg.HeaderText = "错误说明";
            this.dgcErrorMsg.MinimumWidth = 6;
            this.dgcErrorMsg.Name = "dgcErrorMsg";
            this.dgcErrorMsg.ReadOnly = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 286);
            this.Controls.Add(this.dgvErrorRecord);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统时间检查";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvErrorRecord;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCurrTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPreTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorMsg;
    }
}

