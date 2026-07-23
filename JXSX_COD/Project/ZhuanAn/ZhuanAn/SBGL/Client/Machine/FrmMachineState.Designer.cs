namespace Machine
{
    partial class FrmMachineState
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
            this.dgvMachineState = new System.Windows.Forms.DataGridView();
            this.dgcStateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbMachineState = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labMessage = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.labStateName = new System.Windows.Forms.Label();
            this.labCount = new System.Windows.Forms.Label();
            this.tbxStateName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineState)).BeginInit();
            this.gbMachineState.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMachineState
            // 
            this.dgvMachineState.AllowUserToAddRows = false;
            this.dgvMachineState.AllowUserToDeleteRows = false;
            this.dgvMachineState.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMachineState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMachineState.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcStateName,
            this.dgcCount});
            this.dgvMachineState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMachineState.Location = new System.Drawing.Point(0, 110);
            this.dgvMachineState.Name = "dgvMachineState";
            this.dgvMachineState.ReadOnly = true;
            this.dgvMachineState.RowHeadersWidth = 51;
            this.dgvMachineState.RowTemplate.Height = 27;
            this.dgvMachineState.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMachineState.Size = new System.Drawing.Size(492, 198);
            this.dgvMachineState.TabIndex = 0;
            this.dgvMachineState.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMachineState_CellClick);
            // 
            // dgcStateName
            // 
            this.dgcStateName.DataPropertyName = "StateName";
            this.dgcStateName.HeaderText = "状态名称";
            this.dgcStateName.MinimumWidth = 6;
            this.dgcStateName.Name = "dgcStateName";
            this.dgcStateName.ReadOnly = true;
            // 
            // dgcCount
            // 
            this.dgcCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcCount.DataPropertyName = "Count";
            this.dgcCount.HeaderText = "设备数量";
            this.dgcCount.MinimumWidth = 6;
            this.dgcCount.Name = "dgcCount";
            this.dgcCount.ReadOnly = true;
            this.dgcCount.Width = 125;
            // 
            // gbMachineState
            // 
            this.gbMachineState.BackColor = System.Drawing.Color.Transparent;
            this.gbMachineState.Controls.Add(this.tableLayoutPanel1);
            this.gbMachineState.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMachineState.Location = new System.Drawing.Point(0, 28);
            this.gbMachineState.Name = "gbMachineState";
            this.gbMachineState.Size = new System.Drawing.Size(492, 82);
            this.gbMachineState.TabIndex = 1;
            this.gbMachineState.TabStop = false;
            this.gbMachineState.Text = "点位信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudCount, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.labStateName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labCount, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbxStateName, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 58);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labMessage, 4);
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(3, 42);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 8;
            this.labMessage.Text = "提示";
            // 
            // nudCount
            // 
            this.nudCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudCount.Location = new System.Drawing.Point(313, 5);
            this.nudCount.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(150, 25);
            this.nudCount.TabIndex = 3;
            // 
            // labStateName
            // 
            this.labStateName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStateName.AutoSize = true;
            this.labStateName.Location = new System.Drawing.Point(10, 10);
            this.labStateName.Name = "labStateName";
            this.labStateName.Size = new System.Drawing.Size(67, 15);
            this.labStateName.TabIndex = 1;
            this.labStateName.Text = "状态名称";
            // 
            // labCount
            // 
            this.labCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCount.AutoSize = true;
            this.labCount.Location = new System.Drawing.Point(240, 10);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(67, 15);
            this.labCount.TabIndex = 0;
            this.labCount.Text = "设备数量";
            // 
            // tbxStateName
            // 
            this.tbxStateName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbxStateName.Location = new System.Drawing.Point(83, 5);
            this.tbxStateName.MaxLength = 10;
            this.tbxStateName.Name = "tbxStateName";
            this.tbxStateName.Size = new System.Drawing.Size(144, 25);
            this.tbxStateName.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSave,
            this.tsmiDel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(492, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(53, 24);
            this.tsmiSave.Text = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiDel
            // 
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(53, 24);
            this.tsmiDel.Text = "删除";
            this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click);
            // 
            // FrmMachineState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(492, 308);
            this.Controls.Add(this.dgvMachineState);
            this.Controls.Add(this.gbMachineState);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMachineState";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备状态维护";
            this.Load += new System.EventHandler(this.FrmMachineDistribute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMachineState)).EndInit();
            this.gbMachineState.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMachineState;
        private System.Windows.Forms.GroupBox gbMachineState;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.TextBox tbxStateName;
        private System.Windows.Forms.Label labStateName;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcStateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}