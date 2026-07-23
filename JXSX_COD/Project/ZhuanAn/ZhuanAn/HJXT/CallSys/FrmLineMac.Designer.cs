namespace CallSys
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
            this.gbLineMachine.SuspendLayout();
            this.SuspendLayout();
            // 
            // labLine
            // 
            this.labLine.AutoSize = true;
            this.labLine.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labLine.Location = new System.Drawing.Point(21, 18);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(72, 20);
            this.labLine.TabIndex = 31;
            this.labLine.Text = "线体：";
            // 
            // gbLineMachine
            // 
            this.gbLineMachine.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gbLineMachine.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.gbLineMachine.Controls.Add(this.lbMachine);
            this.gbLineMachine.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbLineMachine.Location = new System.Drawing.Point(12, 64);
            this.gbLineMachine.Name = "gbLineMachine";
            this.gbLineMachine.Size = new System.Drawing.Size(455, 478);
            this.gbLineMachine.TabIndex = 30;
            this.gbLineMachine.TabStop = false;
            this.gbLineMachine.Text = "机台模组";
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
            this.lbMachine.Size = new System.Drawing.Size(449, 441);
            this.lbMachine.TabIndex = 26;
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.btnDelete.Location = new System.Drawing.Point(473, 291);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(128, 44);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.btnAdd.Location = new System.Drawing.Point(473, 221);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 44);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "增加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labMessage
            // 
            this.labMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(476, 85);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(265, 46);
            this.labMessage.TabIndex = 32;
            // 
            // cmbLine
            // 
            this.cmbLine.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(98, 14);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(138, 28);
            this.cmbLine.TabIndex = 33;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labTip.Location = new System.Drawing.Point(280, 9);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(450, 15);
            this.labTip.TabIndex = 34;
            this.labTip.Text = "提示：新增时如果要添加相同类型机台请使用“*”追加机台编号。";
            // 
            // labEnum
            // 
            this.labEnum.AutoSize = true;
            this.labEnum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labEnum.Location = new System.Drawing.Point(280, 31);
            this.labEnum.Name = "labEnum";
            this.labEnum.Size = new System.Drawing.Size(211, 15);
            this.labEnum.TabIndex = 35;
            this.labEnum.Text = "例如：天线全自动打端机*01号";
            // 
            // cmbMachine
            // 
            this.cmbMachine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMachine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMachine.Font = new System.Drawing.Font("宋体", 13F);
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(473, 161);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(268, 30);
            this.cmbMachine.TabIndex = 36;
            // 
            // labMachineSelect
            // 
            this.labMachineSelect.AutoSize = true;
            this.labMachineSelect.Location = new System.Drawing.Point(473, 143);
            this.labMachineSelect.Name = "labMachineSelect";
            this.labMachineSelect.Size = new System.Drawing.Size(105, 15);
            this.labMachineSelect.TabIndex = 37;
            this.labMachineSelect.Text = "选择机台模组:";
            // 
            // FrmLineMac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 554);
            this.Controls.Add(this.labMachineSelect);
            this.Controls.Add(this.cmbMachine);
            this.Controls.Add(this.labTip);
            this.Controls.Add(this.labEnum);
            this.Controls.Add(this.cmbLine);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.labLine);
            this.Controls.Add(this.gbLineMachine);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmLineMac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "线体_机台模组关联";
            this.Load += new System.EventHandler(this.FrmLineMac_Load);
            this.gbLineMachine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}