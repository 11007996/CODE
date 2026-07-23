namespace ComTools.Automation
{
    partial class FrmExtendOperate
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOperateType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbWindowName = new System.Windows.Forms.TextBox();
            this.txbControlType = new System.Windows.Forms.TextBox();
            this.txbAutomationId = new System.Windows.Forms.TextBox();
            this.btnSelectWindow = new System.Windows.Forms.Button();
            this.btnSelectControl = new System.Windows.Forms.Button();
            this.txbAppPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectApp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbOperateType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txbWindowName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbControlType, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAutomationId, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectWindow, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectControl, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txbAppPath, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectApp, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnConfirm, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(425, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "操作类型";
            // 
            // cmbOperateType
            // 
            this.cmbOperateType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbOperateType.FormattingEnabled = true;
            this.cmbOperateType.Location = new System.Drawing.Point(103, 8);
            this.cmbOperateType.Name = "cmbOperateType";
            this.cmbOperateType.Size = new System.Drawing.Size(239, 23);
            this.cmbOperateType.TabIndex = 1;
            this.cmbOperateType.SelectedIndexChanged += new System.EventHandler(this.cmbOperateType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "目标窗口";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "控件类型";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "自动化ID";
            // 
            // txbWindowName
            // 
            this.txbWindowName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbWindowName.Location = new System.Drawing.Point(103, 47);
            this.txbWindowName.Name = "txbWindowName";
            this.txbWindowName.ReadOnly = true;
            this.txbWindowName.Size = new System.Drawing.Size(239, 25);
            this.txbWindowName.TabIndex = 2;
            // 
            // txbControlType
            // 
            this.txbControlType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbControlType.Location = new System.Drawing.Point(103, 87);
            this.txbControlType.Name = "txbControlType";
            this.txbControlType.ReadOnly = true;
            this.txbControlType.Size = new System.Drawing.Size(239, 25);
            this.txbControlType.TabIndex = 2;
            // 
            // txbAutomationId
            // 
            this.txbAutomationId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAutomationId.Location = new System.Drawing.Point(103, 127);
            this.txbAutomationId.Name = "txbAutomationId";
            this.txbAutomationId.ReadOnly = true;
            this.txbAutomationId.Size = new System.Drawing.Size(239, 25);
            this.txbAutomationId.TabIndex = 2;
            // 
            // btnSelectWindow
            // 
            this.btnSelectWindow.Location = new System.Drawing.Point(348, 43);
            this.btnSelectWindow.Name = "btnSelectWindow";
            this.btnSelectWindow.Size = new System.Drawing.Size(74, 34);
            this.btnSelectWindow.TabIndex = 3;
            this.btnSelectWindow.Text = "选择";
            this.btnSelectWindow.UseVisualStyleBackColor = true;
            this.btnSelectWindow.Click += new System.EventHandler(this.btnSelectWindow_Click);
            // 
            // btnSelectControl
            // 
            this.btnSelectControl.Location = new System.Drawing.Point(348, 83);
            this.btnSelectControl.Name = "btnSelectControl";
            this.btnSelectControl.Size = new System.Drawing.Size(74, 34);
            this.btnSelectControl.TabIndex = 3;
            this.btnSelectControl.Text = "选择";
            this.btnSelectControl.UseVisualStyleBackColor = true;
            this.btnSelectControl.Click += new System.EventHandler(this.btnSelectControl_Click);
            // 
            // txbAppPath
            // 
            this.txbAppPath.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txbAppPath.Location = new System.Drawing.Point(103, 167);
            this.txbAppPath.Name = "txbAppPath";
            this.txbAppPath.ReadOnly = true;
            this.txbAppPath.Size = new System.Drawing.Size(239, 25);
            this.txbAppPath.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "软件位置";
            // 
            // btnSelectApp
            // 
            this.btnSelectApp.Location = new System.Drawing.Point(348, 163);
            this.btnSelectApp.Name = "btnSelectApp";
            this.btnSelectApp.Size = new System.Drawing.Size(74, 34);
            this.btnSelectApp.TabIndex = 6;
            this.btnSelectApp.Text = "选择";
            this.btnSelectApp.UseVisualStyleBackColor = true;
            this.btnSelectApp.Click += new System.EventHandler(this.btnSelectApp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDelete.BackColor = System.Drawing.Color.Tomato;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(348, 205);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 34);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(268, 205);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(74, 34);
            this.btnConfirm.TabIndex = 6;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FrmExtendOperate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(425, 245);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmExtendOperate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "扩展操作";
            this.Load += new System.EventHandler(this.FrmExtendOperate_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOperateType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbWindowName;
        private System.Windows.Forms.TextBox txbControlType;
        private System.Windows.Forms.TextBox txbAutomationId;
        private System.Windows.Forms.Button btnSelectWindow;
        private System.Windows.Forms.Button btnSelectControl;
        private System.Windows.Forms.TextBox txbAppPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectApp;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnDelete;
    }
}