namespace ComTools.Automation
{
    partial class FrmModelSelected
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
            this.dgvSelected = new System.Windows.Forms.DataGridView();
            this.dgcControlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcAutomationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcControlValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcRect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSelected
            // 
            this.dgvSelected.AllowUserToAddRows = false;
            this.dgvSelected.AllowUserToDeleteRows = false;
            this.dgvSelected.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelected.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcControlType,
            this.dgcAutomationId,
            this.dgcControlValue,
            this.dgcRect});
            this.dgvSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelected.Location = new System.Drawing.Point(0, 0);
            this.dgvSelected.Name = "dgvSelected";
            this.dgvSelected.ReadOnly = true;
            this.dgvSelected.RowHeadersVisible = false;
            this.dgvSelected.RowHeadersWidth = 51;
            this.dgvSelected.RowTemplate.Height = 27;
            this.dgvSelected.Size = new System.Drawing.Size(800, 450);
            this.dgvSelected.TabIndex = 2;
            this.dgvSelected.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelected_CellClick);
            this.dgvSelected.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelected_CellDoubleClick);
            // 
            // dgcControlType
            // 
            this.dgcControlType.DataPropertyName = "ControlType";
            this.dgcControlType.HeaderText = "控件类型";
            this.dgcControlType.MinimumWidth = 6;
            this.dgcControlType.Name = "dgcControlType";
            this.dgcControlType.ReadOnly = true;
            this.dgcControlType.Width = 96;
            // 
            // dgcAutomationId
            // 
            this.dgcAutomationId.DataPropertyName = "AutomationId";
            this.dgcAutomationId.HeaderText = "自动化ID";
            this.dgcAutomationId.MinimumWidth = 6;
            this.dgcAutomationId.Name = "dgcAutomationId";
            this.dgcAutomationId.ReadOnly = true;
            this.dgcAutomationId.Width = 97;
            // 
            // dgcControlValue
            // 
            this.dgcControlValue.DataPropertyName = "ControlValue";
            this.dgcControlValue.HeaderText = "控件值";
            this.dgcControlValue.MinimumWidth = 6;
            this.dgcControlValue.Name = "dgcControlValue";
            this.dgcControlValue.ReadOnly = true;
            this.dgcControlValue.Width = 81;
            // 
            // dgcRect
            // 
            this.dgcRect.DataPropertyName = "Rect";
            this.dgcRect.HeaderText = "屏幕区域";
            this.dgcRect.MinimumWidth = 6;
            this.dgcRect.Name = "dgcRect";
            this.dgcRect.ReadOnly = true;
            this.dgcRect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgcRect.Width = 96;
            // 
            // FrmModelSelected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvSelected);
            this.Name = "FrmModelSelected";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择控件";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmModelSelected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcControlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAutomationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcControlValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcRect;
    }
}