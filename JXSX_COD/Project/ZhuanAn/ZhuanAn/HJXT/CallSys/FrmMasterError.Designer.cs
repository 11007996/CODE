namespace CallSys
{
    partial class FrmMasterError
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
            this.components = new System.ComponentModel.Container();
            this.dgvError = new System.Windows.Forms.DataGridView();
            this.dgcErrorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHandler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcHelper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timerRefreshError = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvError
            // 
            this.dgvError.AllowUserToAddRows = false;
            this.dgvError.AllowUserToDeleteRows = false;
            this.dgvError.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvError.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvError.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcErrorId,
            this.dgcMachine,
            this.dgcHandler,
            this.dgcHelper,
            this.dgcStatus});
            this.dgvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvError.Location = new System.Drawing.Point(10, 61);
            this.dgvError.MultiSelect = false;
            this.dgvError.Name = "dgvError";
            this.dgvError.ReadOnly = true;
            this.dgvError.RowHeadersVisible = false;
            this.dgvError.RowTemplate.Height = 27;
            this.dgvError.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvError.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvError.Size = new System.Drawing.Size(459, 277);
            this.dgvError.TabIndex = 0;
            this.dgvError.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvError_CellClick);
            this.dgvError.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvError_CellFormatting);
            // 
            // dgcErrorId
            // 
            this.dgcErrorId.DataPropertyName = "Id";
            this.dgcErrorId.HeaderText = "Id";
            this.dgcErrorId.Name = "dgcErrorId";
            this.dgcErrorId.ReadOnly = true;
            this.dgcErrorId.Visible = false;
            // 
            // dgcMachine
            // 
            this.dgcMachine.DataPropertyName = "Machine";
            this.dgcMachine.HeaderText = "机台";
            this.dgcMachine.Name = "dgcMachine";
            this.dgcMachine.ReadOnly = true;
            // 
            // dgcHandler
            // 
            this.dgcHandler.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHandler.DataPropertyName = "HandlerName";
            this.dgcHandler.HeaderText = "处理人";
            this.dgcHandler.Name = "dgcHandler";
            this.dgcHandler.ReadOnly = true;
            this.dgcHandler.Width = 80;
            // 
            // dgcHelper
            // 
            this.dgcHelper.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcHelper.DataPropertyName = "HelperName";
            this.dgcHelper.HeaderText = "支援人";
            this.dgcHelper.Name = "dgcHelper";
            this.dgcHelper.ReadOnly = true;
            this.dgcHelper.Width = 80;
            // 
            // dgcStatus
            // 
            this.dgcStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcStatus.DataPropertyName = "Status";
            this.dgcStatus.HeaderText = "状态";
            this.dgcStatus.Name = "dgcStatus";
            this.dgcStatus.ReadOnly = true;
            this.dgcStatus.Width = 80;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labTitle);
            this.panelTop.Controls.Add(this.btnCancel);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(10, 10);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(459, 51);
            this.panelTop.TabIndex = 3;
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.DimGray;
            this.labTitle.Location = new System.Drawing.Point(3, 3);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(129, 37);
            this.labTitle.TabIndex = 4;
            this.labTitle.Text = "故障清单";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(344, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 42);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "呼叫重置";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timerRefreshError
            // 
            this.timerRefreshError.Enabled = true;
            this.timerRefreshError.Interval = 60000;
            this.timerRefreshError.Tick += new System.EventHandler(this.timerRefreshError_Tick);
            // 
            // FrmMasterError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(479, 348);
            this.ControlBox = false;
            this.Controls.Add(this.dgvError);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMasterError";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "故障清单";
            this.Load += new System.EventHandler(this.FrmMasterError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvError;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcErrorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMachine;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHandler;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcHelper;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer timerRefreshError;
        private System.Windows.Forms.Label labTitle;
    }
}