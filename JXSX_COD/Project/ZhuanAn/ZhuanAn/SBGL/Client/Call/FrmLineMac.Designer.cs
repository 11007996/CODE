namespace Call
{
    partial class FrmLineMac
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
            this.labLine = new System.Windows.Forms.Label();
            this.gbLineMachine = new System.Windows.Forms.GroupBox();
            this.lbMachine = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.labMessage = new System.Windows.Forms.Label();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.labTip = new System.Windows.Forms.Label();
            this.labEnum = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.labMachineSelect = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbLineMachine.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labLine
            // 
            this.labLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labLine.AutoSize = true;
            this.labLine.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLine.Location = new System.Drawing.Point(23, 6);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(46, 18);
            this.labLine.TabIndex = 31;
            this.labLine.Text = "线体";
            // 
            // gbLineMachine
            // 
            this.gbLineMachine.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.gbLineMachine, 2);
            this.gbLineMachine.Controls.Add(this.lbMachine);
            this.gbLineMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLineMachine.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbLineMachine.Location = new System.Drawing.Point(23, 73);
            this.gbLineMachine.Name = "gbLineMachine";
            this.gbLineMachine.Size = new System.Drawing.Size(469, 372);
            this.gbLineMachine.TabIndex = 30;
            this.gbLineMachine.TabStop = false;
            this.gbLineMachine.Text = "已关联模组";
            // 
            // lbMachine
            // 
            this.lbMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMachine.FormattingEnabled = true;
            this.lbMachine.ItemHeight = 27;
            this.lbMachine.Items.AddRange(new object[] {
            "1号"});
            this.lbMachine.Location = new System.Drawing.Point(3, 34);
            this.lbMachine.Name = "lbMachine";
            this.lbMachine.Size = new System.Drawing.Size(463, 335);
            this.lbMachine.TabIndex = 26;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(498, 73);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 34);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.Location = new System.Drawing.Point(498, 33);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 34);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "增加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labMessage
            // 
            this.labMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(23, 448);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(37, 15);
            this.labMessage.TabIndex = 32;
            this.labMessage.Text = "提示";
            // 
            // cmbLine
            // 
            this.cmbLine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbLine.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(23, 36);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(136, 28);
            this.cmbLine.TabIndex = 33;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // labTip
            // 
            this.labTip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labTip.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labTip, 3);
            this.labTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labTip.Location = new System.Drawing.Point(23, 470);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(450, 15);
            this.labTip.TabIndex = 34;
            this.labTip.Text = "提示：新增时如果要添加相同类型机台请使用“*”追加机台编号。";
            // 
            // labEnum
            // 
            this.labEnum.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labEnum.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labEnum, 3);
            this.labEnum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labEnum.Location = new System.Drawing.Point(23, 500);
            this.labEnum.Name = "labEnum";
            this.labEnum.Size = new System.Drawing.Size(211, 15);
            this.labEnum.TabIndex = 35;
            this.labEnum.Text = "例如：天线全自动打端机*01号";
            // 
            // cmbMachine
            // 
            this.cmbMachine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMachine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMachine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMachine.Font = new System.Drawing.Font("宋体", 13F);
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(165, 35);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(327, 30);
            this.cmbMachine.TabIndex = 36;
            // 
            // labMachineSelect
            // 
            this.labMachineSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labMachineSelect.AutoSize = true;
            this.labMachineSelect.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMachineSelect.Location = new System.Drawing.Point(165, 6);
            this.labMachineSelect.Name = "labMachineSelect";
            this.labMachineSelect.Size = new System.Drawing.Size(84, 18);
            this.labMachineSelect.TabIndex = 37;
            this.labMachineSelect.Text = "机台模组";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.labMachineSelect, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.labEnum, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labTip, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmbMachine, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labLine, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labMessage, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbLine, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbLineMachine, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 523);
            this.tableLayoutPanel1.TabIndex = 38;
            // 
            // FrmLineMac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(618, 523);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLineMac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "线体_机台模组关联";
            this.Load += new System.EventHandler(this.FrmLineMac_Load);
            this.gbLineMachine.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox gbLineMachine;
        private System.Windows.Forms.ListBox lbMachine;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.Label labTip;
        private System.Windows.Forms.Label labEnum;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.Label labMachineSelect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}