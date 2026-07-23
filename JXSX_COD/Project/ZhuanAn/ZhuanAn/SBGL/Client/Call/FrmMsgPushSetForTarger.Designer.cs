namespace Call
{
    partial class FrmMsgPushSetForTarger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsgPushSetForTarger));
            this.cmbDept = new System.Windows.Forms.ComboBox();
            this.tbKeyWord = new System.Windows.Forms.TextBox();
            this.labKeyWord = new System.Windows.Forms.Label();
            this.labDept = new System.Windows.Forms.Label();
            this.labStage3 = new System.Windows.Forms.Label();
            this.gbContactPerson = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chklsbContactPerson = new System.Windows.Forms.CheckedListBox();
            this.btnCancelChecked = new System.Windows.Forms.Button();
            this.lsbStage3 = new System.Windows.Forms.ListBox();
            this.btnStage3Add = new System.Windows.Forms.Button();
            this.btnStage3Del = new System.Windows.Forms.Button();
            this.gbMsgReceiver = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbContactPerson.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbMsgReceiver.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDept
            // 
            this.cmbDept.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Location = new System.Drawing.Point(83, 8);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(137, 23);
            this.cmbDept.TabIndex = 1;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // tbKeyWord
            // 
            this.tbKeyWord.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbKeyWord.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbKeyWord.Location = new System.Drawing.Point(63, 7);
            this.tbKeyWord.Name = "tbKeyWord";
            this.tbKeyWord.Size = new System.Drawing.Size(180, 25);
            this.tbKeyWord.TabIndex = 3;
            this.tbKeyWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKeyWord_KeyPress);
            // 
            // labKeyWord
            // 
            this.labKeyWord.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labKeyWord.AutoSize = true;
            this.labKeyWord.Location = new System.Drawing.Point(20, 12);
            this.labKeyWord.Name = "labKeyWord";
            this.labKeyWord.Size = new System.Drawing.Size(37, 15);
            this.labKeyWord.TabIndex = 7;
            this.labKeyWord.Text = "过滤";
            // 
            // labDept
            // 
            this.labDept.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labDept.AutoSize = true;
            this.labDept.Location = new System.Drawing.Point(10, 12);
            this.labDept.Name = "labDept";
            this.labDept.Size = new System.Drawing.Size(67, 15);
            this.labDept.TabIndex = 9;
            this.labDept.Text = "部门选择";
            // 
            // labStage3
            // 
            this.labStage3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labStage3.AutoSize = true;
            this.labStage3.Location = new System.Drawing.Point(10, 47);
            this.labStage3.Name = "labStage3";
            this.labStage3.Size = new System.Drawing.Size(67, 15);
            this.labStage3.TabIndex = 10;
            this.labStage3.Text = "接单超时";
            // 
            // gbContactPerson
            // 
            this.gbContactPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gbContactPerson.Controls.Add(this.tableLayoutPanel3);
            this.gbContactPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContactPerson.Location = new System.Drawing.Point(3, 3);
            this.gbContactPerson.Name = "gbContactPerson";
            this.gbContactPerson.Size = new System.Drawing.Size(352, 425);
            this.gbContactPerson.TabIndex = 12;
            this.gbContactPerson.TabStop = false;
            this.gbContactPerson.Text = "联系人列表";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.labKeyWord, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chklsbContactPerson, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnCancelChecked, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tbKeyWord, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(346, 401);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // chklsbContactPerson
            // 
            this.chklsbContactPerson.BackColor = System.Drawing.Color.White;
            this.chklsbContactPerson.CheckOnClick = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chklsbContactPerson, 3);
            this.chklsbContactPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklsbContactPerson.FormattingEnabled = true;
            this.chklsbContactPerson.Location = new System.Drawing.Point(3, 43);
            this.chklsbContactPerson.Name = "chklsbContactPerson";
            this.chklsbContactPerson.Size = new System.Drawing.Size(340, 355);
            this.chklsbContactPerson.TabIndex = 9;
            // 
            // btnCancelChecked
            // 
            this.btnCancelChecked.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancelChecked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelChecked.ForeColor = System.Drawing.Color.White;
            this.btnCancelChecked.Location = new System.Drawing.Point(249, 5);
            this.btnCancelChecked.Name = "btnCancelChecked";
            this.btnCancelChecked.Size = new System.Drawing.Size(94, 30);
            this.btnCancelChecked.TabIndex = 10;
            this.btnCancelChecked.Text = "取消勾选";
            this.btnCancelChecked.UseVisualStyleBackColor = false;
            this.btnCancelChecked.Click += new System.EventHandler(this.btnCancelChecked_Click);
            // 
            // lsbStage3
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.lsbStage3, 2);
            this.lsbStage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbStage3.FormattingEnabled = true;
            this.lsbStage3.ItemHeight = 15;
            this.lsbStage3.Location = new System.Drawing.Point(3, 73);
            this.lsbStage3.MultiColumn = true;
            this.lsbStage3.Name = "lsbStage3";
            this.lsbStage3.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbStage3.Size = new System.Drawing.Size(340, 325);
            this.lsbStage3.TabIndex = 13;
            // 
            // btnStage3Add
            // 
            this.btnStage3Add.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStage3Add.BackColor = System.Drawing.Color.Navy;
            this.btnStage3Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage3Add.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage3Add.Image = ((System.Drawing.Image)(resources.GetObject("btnStage3Add.Image")));
            this.btnStage3Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage3Add.Location = new System.Drawing.Point(3, 177);
            this.btnStage3Add.Name = "btnStage3Add";
            this.btnStage3Add.Size = new System.Drawing.Size(88, 30);
            this.btnStage3Add.TabIndex = 16;
            this.btnStage3Add.Text = "添加";
            this.btnStage3Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage3Add.UseVisualStyleBackColor = false;
            this.btnStage3Add.Click += new System.EventHandler(this.btnStage3Add_Click);
            // 
            // btnStage3Del
            // 
            this.btnStage3Del.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStage3Del.BackColor = System.Drawing.Color.Navy;
            this.btnStage3Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage3Del.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage3Del.Image = ((System.Drawing.Image)(resources.GetObject("btnStage3Del.Image")));
            this.btnStage3Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage3Del.Location = new System.Drawing.Point(3, 217);
            this.btnStage3Del.Name = "btnStage3Del";
            this.btnStage3Del.Size = new System.Drawing.Size(88, 30);
            this.btnStage3Del.TabIndex = 17;
            this.btnStage3Del.Text = "删除";
            this.btnStage3Del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage3Del.UseVisualStyleBackColor = false;
            this.btnStage3Del.Click += new System.EventHandler(this.btnStage3Del_Click);
            // 
            // gbMsgReceiver
            // 
            this.gbMsgReceiver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gbMsgReceiver.Controls.Add(this.tableLayoutPanel4);
            this.gbMsgReceiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMsgReceiver.Location = new System.Drawing.Point(461, 3);
            this.gbMsgReceiver.Name = "gbMsgReceiver";
            this.gbMsgReceiver.Size = new System.Drawing.Size(352, 425);
            this.gbMsgReceiver.TabIndex = 21;
            this.gbMsgReceiver.TabStop = false;
            this.gbMsgReceiver.Text = "消息接收人设置";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.labDept, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lsbStage3, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.cmbDept, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.labStage3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(346, 401);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbContactPerson, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbMsgReceiver, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 431);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnStage3Del, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnStage3Add, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(361, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 425);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // FrmMsgPushSetForTarger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(816, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMsgPushSetForTarger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息通知设置(指定人员)";
            this.Load += new System.EventHandler(this.FrmMsgPushSetForTarger_Load);
            this.gbContactPerson.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.gbMsgReceiver.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDept;
        private System.Windows.Forms.TextBox tbKeyWord;
        private System.Windows.Forms.Label labKeyWord;
        private System.Windows.Forms.Label labDept;
        private System.Windows.Forms.Label labStage3;
        private System.Windows.Forms.GroupBox gbContactPerson;
        private System.Windows.Forms.ListBox lsbStage3;
        private System.Windows.Forms.CheckedListBox chklsbContactPerson;
        private System.Windows.Forms.Button btnStage3Add;
        private System.Windows.Forms.Button btnStage3Del;
        private System.Windows.Forms.GroupBox gbMsgReceiver;
        private System.Windows.Forms.Button btnCancelChecked;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}