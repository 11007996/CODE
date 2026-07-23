namespace PMS
{
    partial class frmDepManagement
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDepID = new System.Windows.Forms.TextBox();
            this.txtDepMan = new System.Windows.Forms.TextBox();
            this.txtDepName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolmod = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.chkMan = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(477, 244);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "部门编号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "部门名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "部门负责人";
            // 
            // txtDepID
            // 
            this.txtDepID.Location = new System.Drawing.Point(82, 36);
            this.txtDepID.Name = "txtDepID";
            this.txtDepID.Size = new System.Drawing.Size(128, 25);
            this.txtDepID.TabIndex = 2;
            // 
            // txtDepMan
            // 
            this.txtDepMan.Location = new System.Drawing.Point(304, 36);
            this.txtDepMan.Name = "txtDepMan";
            this.txtDepMan.ReadOnly = true;
            this.txtDepMan.Size = new System.Drawing.Size(128, 25);
            this.txtDepMan.TabIndex = 2;
            // 
            // txtDepName
            // 
            this.txtDepName.Location = new System.Drawing.Point(82, 67);
            this.txtDepName.Name = "txtDepName";
            this.txtDepName.Size = new System.Drawing.Size(128, 25);
            this.txtDepName.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolQuery,
            this.toolmod,
            this.toolDel,
            this.toolAdd,
            this.toolSave,
            this.toolCancel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(483, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolQuery
            // 
            this.ToolQuery.Name = "ToolQuery";
            this.ToolQuery.Size = new System.Drawing.Size(51, 24);
            this.ToolQuery.Text = "查询";
            this.ToolQuery.Click += new System.EventHandler(this.ToolQuery_Click);
            // 
            // toolmod
            // 
            this.toolmod.Name = "toolmod";
            this.toolmod.Size = new System.Drawing.Size(51, 24);
            this.toolmod.Text = "修改";
            this.toolmod.Click += new System.EventHandler(this.toolmod_Click);
            // 
            // toolDel
            // 
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new System.Drawing.Size(51, 24);
            this.toolDel.Text = "删除";
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // toolAdd
            // 
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.Size = new System.Drawing.Size(51, 24);
            this.toolAdd.Text = "增加";
            this.toolAdd.Click += new System.EventHandler(this.toolAdd_Click);
            // 
            // toolSave
            // 
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(51, 24);
            this.toolSave.Text = "保存";
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolCancel
            // 
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(51, 24);
            this.toolCancel.Text = "取消";
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // chkMan
            // 
            this.chkMan.AutoSize = true;
            this.chkMan.Location = new System.Drawing.Point(434, 41);
            this.chkMan.Name = "chkMan";
            this.chkMan.Size = new System.Drawing.Size(18, 17);
            this.chkMan.TabIndex = 5;
            this.chkMan.UseVisualStyleBackColor = true;
            this.chkMan.Click += new System.EventHandler(this.chkMan_Click);
            // 
            // frmDepManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 343);
            this.Controls.Add(this.chkMan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDepMan);
            this.Controls.Add(this.txtDepName);
            this.Controls.Add(this.txtDepID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmDepManagement";
            this.Text = "depManagement";
            this.Load += new System.EventHandler(this.frmDepManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDepID;
        private System.Windows.Forms.TextBox txtDepMan;
        private System.Windows.Forms.TextBox txtDepName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolQuery;
        private System.Windows.Forms.ToolStripMenuItem toolmod;
        private System.Windows.Forms.ToolStripMenuItem toolDel;
        private System.Windows.Forms.ToolStripMenuItem toolAdd;
        private System.Windows.Forms.ToolStripMenuItem toolSave;
        private System.Windows.Forms.ToolStripMenuItem toolCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkMan;
    }
}