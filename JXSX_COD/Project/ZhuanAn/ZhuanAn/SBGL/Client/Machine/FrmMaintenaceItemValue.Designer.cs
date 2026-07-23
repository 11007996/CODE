namespace Machine
{
    partial class FrmMaintenaceItemValue
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbAssetNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dtiDay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.dgcItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcItemValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txbAssetName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dtiDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "资产编号";
            // 
            // txbAssetNo
            // 
            this.txbAssetNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.txbAssetNo.Border.Class = "TextBoxBorder";
            this.txbAssetNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbAssetNo.Location = new System.Drawing.Point(83, 5);
            this.txbAssetNo.Name = "txbAssetNo";
            this.txbAssetNo.PreventEnterBeep = true;
            this.txbAssetNo.ReadOnly = true;
            this.txbAssetNo.Size = new System.Drawing.Size(278, 25);
            this.txbAssetNo.TabIndex = 1;
            // 
            // dtiDay
            // 
            this.dtiDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.dtiDay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiDay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiDay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiDay.ButtonDropDown.Visible = true;
            this.dtiDay.Enabled = false;
            this.dtiDay.IsPopupCalendarOpen = false;
            this.dtiDay.Location = new System.Drawing.Point(83, 75);
            // 
            // 
            // 
            // 
            // 
            // 
            this.dtiDay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiDay.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiDay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiDay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiDay.MonthCalendar.DisplayMonth = new System.DateTime(2023, 7, 1, 0, 0, 0, 0);
            this.dtiDay.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            // 
            // 
            // 
            this.dtiDay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiDay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiDay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiDay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiDay.MonthCalendar.TodayButtonVisible = true;
            this.dtiDay.Name = "dtiDay";
            this.dtiDay.Size = new System.Drawing.Size(141, 25);
            this.dtiDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiDay.TabIndex = 2;
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcItemName,
            this.dgcItemValue});
            this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItem.Location = new System.Drawing.Point(10, 115);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.ReadOnly = true;
            this.dgvItem.RowHeadersVisible = false;
            this.dgvItem.RowHeadersWidth = 51;
            this.dgvItem.RowTemplate.Height = 27;
            this.dgvItem.Size = new System.Drawing.Size(380, 336);
            this.dgvItem.TabIndex = 3;
            // 
            // dgcItemName
            // 
            this.dgcItemName.DataPropertyName = "ItemName";
            this.dgcItemName.HeaderText = "保养项目";
            this.dgcItemName.MinimumWidth = 6;
            this.dgcItemName.Name = "dgcItemName";
            this.dgcItemName.ReadOnly = true;
            // 
            // dgcItemValue
            // 
            this.dgcItemValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgcItemValue.DataPropertyName = "ItemValue";
            this.dgcItemValue.HeaderText = "维护结果";
            this.dgcItemValue.MinimumWidth = 6;
            this.dgcItemValue.Name = "dgcItemValue";
            this.dgcItemValue.ReadOnly = true;
            this.dgcItemValue.Width = 125;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "日期";
            // 
            // txbAssetName
            // 
            this.txbAssetName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.txbAssetName.Border.Class = "TextBoxBorder";
            this.txbAssetName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txbAssetName.Location = new System.Drawing.Point(83, 40);
            this.txbAssetName.Name = "txbAssetName";
            this.txbAssetName.PreventEnterBeep = true;
            this.txbAssetName.ReadOnly = true;
            this.txbAssetName.Size = new System.Drawing.Size(278, 25);
            this.txbAssetName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "资产名称";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtiDay, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAssetNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 105);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FrmMaintenaceItemValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(400, 461);
            this.Controls.Add(this.dgvItem);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmMaintenaceItemValue";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保养项目详情";
            this.Load += new System.EventHandler(this.FrmMaintenaceItemValue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtiDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX txbAssetNo;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiDay;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.TextBoxX txbAssetName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcItemValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}