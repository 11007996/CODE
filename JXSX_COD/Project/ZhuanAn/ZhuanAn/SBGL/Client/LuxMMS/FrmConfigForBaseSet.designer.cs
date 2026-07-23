namespace LuxMMS
{
    partial class FrmConfigForBaseSet
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
            this.labArea = new System.Windows.Forms.Label();
            this.labLine = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.radOne = new System.Windows.Forms.RadioButton();
            this.radMultiple = new System.Windows.Forms.RadioButton();
            this.labMachine = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labArea
            // 
            this.labArea.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labArea.AutoSize = true;
            this.labArea.BackColor = System.Drawing.Color.Transparent;
            this.labArea.Location = new System.Drawing.Point(36, 12);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(37, 15);
            this.labArea.TabIndex = 0;
            this.labArea.Text = "区域";
            this.labArea.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labLine.AutoSize = true;
            this.labLine.BackColor = System.Drawing.Color.Transparent;
            this.labLine.Location = new System.Drawing.Point(36, 52);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(37, 15);
            this.labLine.TabIndex = 1;
            this.labLine.Text = "线名";
            this.labLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbArea
            // 
            this.cmbArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbArea.Location = new System.Drawing.Point(80, 8);
            this.cmbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(179, 23);
            this.cmbArea.TabIndex = 2;
            this.cmbArea.SelectedIndexChanged += new System.EventHandler(this.cmbArea_SelectedIndexChanged);
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.Location = new System.Drawing.Point(80, 48);
            this.cmbLine.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(179, 23);
            this.cmbLine.TabIndex = 3;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // radOne
            // 
            this.radOne.AutoSize = true;
            this.radOne.Dock = System.Windows.Forms.DockStyle.Left;
            this.radOne.Location = new System.Drawing.Point(0, 0);
            this.radOne.Name = "radOne";
            this.radOne.Size = new System.Drawing.Size(58, 34);
            this.radOne.TabIndex = 12;
            this.radOne.TabStop = true;
            this.radOne.Text = "单个";
            this.radOne.UseVisualStyleBackColor = true;
            // 
            // radMultiple
            // 
            this.radMultiple.AutoSize = true;
            this.radMultiple.Dock = System.Windows.Forms.DockStyle.Left;
            this.radMultiple.Location = new System.Drawing.Point(58, 0);
            this.radMultiple.Name = "radMultiple";
            this.radMultiple.Size = new System.Drawing.Size(58, 34);
            this.radMultiple.TabIndex = 13;
            this.radMultiple.TabStop = true;
            this.radMultiple.Text = "多个";
            this.radMultiple.UseVisualStyleBackColor = true;
            // 
            // labMachine
            // 
            this.labMachine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labMachine.AutoSize = true;
            this.labMachine.Location = new System.Drawing.Point(36, 92);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(37, 15);
            this.labMachine.TabIndex = 10;
            this.labMachine.Text = "机台";
            this.labMachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMachine
            // 
            this.cmbMachine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(79, 88);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(179, 23);
            this.cmbMachine.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.labArea, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachine, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labMachine, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbArea, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labLine, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 160);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "呼叫限制";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.Controls.Add(this.radMultiple);
            this.panel1.Controls.Add(this.radOne);
            this.panel1.Location = new System.Drawing.Point(79, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 34);
            this.panel1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 223);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基础设置";
            // 
            // FrmConfigForBaseSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(312, 223);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfigForBaseSet";
            this.Text = "基本设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfigForBaseSet_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.Label labMachine;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.RadioButton radMultiple;
        private System.Windows.Forms.RadioButton radOne;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}