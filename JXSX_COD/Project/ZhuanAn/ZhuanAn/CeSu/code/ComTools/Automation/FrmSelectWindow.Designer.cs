namespace ComTools.Automation
{
    partial class FrmSelectWindow
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
            this.dgvWindow = new System.Windows.Forms.DataGridView();
            this.dgcWindowHWnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcWindowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWindow
            // 
            this.dgvWindow.AllowUserToAddRows = false;
            this.dgvWindow.AllowUserToDeleteRows = false;
            this.dgvWindow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWindow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWindow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcWindowHWnd,
            this.dgcWindowName});
            this.dgvWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWindow.Location = new System.Drawing.Point(0, 0);
            this.dgvWindow.Name = "dgvWindow";
            this.dgvWindow.ReadOnly = true;
            this.dgvWindow.RowHeadersVisible = false;
            this.dgvWindow.RowHeadersWidth = 51;
            this.dgvWindow.RowTemplate.Height = 27;
            this.dgvWindow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWindow.Size = new System.Drawing.Size(670, 311);
            this.dgvWindow.TabIndex = 0;
            this.dgvWindow.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWindow_CellContentDoubleClick);
            // 
            // dgcWindowHWnd
            // 
            this.dgcWindowHWnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcWindowHWnd.DataPropertyName = "HWnd";
            this.dgcWindowHWnd.HeaderText = "窗口句柄";
            this.dgcWindowHWnd.MinimumWidth = 6;
            this.dgcWindowHWnd.Name = "dgcWindowHWnd";
            this.dgcWindowHWnd.ReadOnly = true;
            this.dgcWindowHWnd.Width = 80;
            // 
            // dgcWindowName
            // 
            this.dgcWindowName.DataPropertyName = "Title";
            this.dgcWindowName.HeaderText = "窗口名称";
            this.dgcWindowName.MinimumWidth = 6;
            this.dgcWindowName.Name = "dgcWindowName";
            this.dgcWindowName.ReadOnly = true;
            // 
            // FrmSelectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(670, 311);
            this.Controls.Add(this.dgvWindow);
            this.Name = "FrmSelectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "窗口选择";
            this.Load += new System.EventHandler(this.FrmSelectWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWindow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWindow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWindowHWnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcWindowName;
    }
}