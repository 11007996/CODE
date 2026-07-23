namespace Machine
{
    partial class FrmHolidayDicData
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
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.swBtnHoliday = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.dgvHolidayDic = new System.Windows.Forms.DataGridView();
            this.dgcDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcIsHoliday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHolidayDic)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(373, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 28);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpDate.Location = new System.Drawing.Point(63, 7);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(143, 25);
            this.dtpDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "日期";
            // 
            // swBtnHoliday
            // 
            this.swBtnHoliday.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.swBtnHoliday.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.swBtnHoliday.Location = new System.Drawing.Point(273, 9);
            this.swBtnHoliday.Name = "swBtnHoliday";
            this.swBtnHoliday.OffText = "否";
            this.swBtnHoliday.OnText = "是";
            this.swBtnHoliday.Size = new System.Drawing.Size(66, 22);
            this.swBtnHoliday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.swBtnHoliday.TabIndex = 12;
            // 
            // dgvHolidayDic
            // 
            this.dgvHolidayDic.AllowUserToAddRows = false;
            this.dgvHolidayDic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHolidayDic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHolidayDic.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcDate,
            this.dgcIsHoliday});
            this.dgvHolidayDic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHolidayDic.Location = new System.Drawing.Point(0, 98);
            this.dgvHolidayDic.Name = "dgvHolidayDic";
            this.dgvHolidayDic.ReadOnly = true;
            this.dgvHolidayDic.RowHeadersWidth = 51;
            this.dgvHolidayDic.RowTemplate.Height = 27;
            this.dgvHolidayDic.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHolidayDic.Size = new System.Drawing.Size(483, 304);
            this.dgvHolidayDic.TabIndex = 13;
            this.dgvHolidayDic.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvHolidayDic_UserDeletingRow);
            // 
            // dgcDate
            // 
            this.dgcDate.DataPropertyName = "DataKey";
            this.dgcDate.HeaderText = "日期";
            this.dgcDate.MinimumWidth = 6;
            this.dgcDate.Name = "dgcDate";
            this.dgcDate.ReadOnly = true;
            // 
            // dgcIsHoliday
            // 
            this.dgcIsHoliday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcIsHoliday.DataPropertyName = "DataValue";
            this.dgcIsHoliday.HeaderText = "是否节日";
            this.dgcIsHoliday.MinimumWidth = 6;
            this.dgcIsHoliday.Name = "dgcIsHoliday";
            this.dgcIsHoliday.ReadOnly = true;
            this.dgcIsHoliday.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 98);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "节日设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.swBtnHoliday, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(477, 74);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 5);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(3, 49);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 14;
            this.labMessage.Text = "提示";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "假期";
            // 
            // FrmHolidayDicData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(483, 402);
            this.Controls.Add(this.dgvHolidayDic);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmHolidayDicData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节假日维护";
            this.Load += new System.EventHandler(this.FrmHolidayDicData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHolidayDic)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.SwitchButton swBtnHoliday;
        private System.Windows.Forms.DataGridView dgvHolidayDic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcIsHoliday;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}