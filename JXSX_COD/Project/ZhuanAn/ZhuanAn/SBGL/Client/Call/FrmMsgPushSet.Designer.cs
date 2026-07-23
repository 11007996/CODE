namespace Call
{
    partial class FrmMsgPushSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsgPushSet));
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.tbKeyWord = new System.Windows.Forms.TextBox();
            this.labKeyWord = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.labStage1 = new System.Windows.Forms.Label();
            this.labStage2 = new System.Windows.Forms.Label();
            this.gbContactPerson = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chklsbContactPerson = new System.Windows.Forms.CheckedListBox();
            this.btnCancelChecked = new System.Windows.Forms.Button();
            this.lsbStage1 = new System.Windows.Forms.ListBox();
            this.lsbStage2 = new System.Windows.Forms.ListBox();
            this.btnStage1Add = new System.Windows.Forms.Button();
            this.btnStage1Del = new System.Windows.Forms.Button();
            this.btnStage2Add = new System.Windows.Forms.Button();
            this.btnStage2Del = new System.Windows.Forms.Button();
            this.gbMsgReceiver = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.gbContactPerson.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbMsgReceiver.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbArea
            // 
            this.cmbArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(83, 8);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(95, 23);
            this.cmbArea.TabIndex = 1;
            this.cmbArea.SelectedIndexChanged += new System.EventHandler(this.cmbArea_SelectedIndexChanged);
            // 
            // tbKeyWord
            // 
            this.tbKeyWord.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbKeyWord.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbKeyWord.Location = new System.Drawing.Point(63, 7);
            this.tbKeyWord.Name = "tbKeyWord";
            this.tbKeyWord.Size = new System.Drawing.Size(147, 25);
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
            // labArea
            // 
            this.labArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(3, 12);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(67, 15);
            this.labArea.TabIndex = 9;
            this.labArea.Text = "选择区域";
            // 
            // labStage1
            // 
            this.labStage1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labStage1.AutoSize = true;
            this.labStage1.Location = new System.Drawing.Point(3, 47);
            this.labStage1.Name = "labStage1";
            this.labStage1.Size = new System.Drawing.Size(67, 15);
            this.labStage1.TabIndex = 10;
            this.labStage1.Text = "呼叫支援";
            // 
            // labStage2
            // 
            this.labStage2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labStage2.AutoSize = true;
            this.labStage2.Location = new System.Drawing.Point(3, 212);
            this.labStage2.Name = "labStage2";
            this.labStage2.Size = new System.Drawing.Size(67, 15);
            this.labStage2.TabIndex = 11;
            this.labStage2.Text = "支援超时";
            // 
            // gbContactPerson
            // 
            this.gbContactPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gbContactPerson.Controls.Add(this.tableLayoutPanel1);
            this.gbContactPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContactPerson.Location = new System.Drawing.Point(3, 3);
            this.gbContactPerson.Name = "gbContactPerson";
            this.gbContactPerson.Size = new System.Drawing.Size(319, 395);
            this.gbContactPerson.TabIndex = 12;
            this.gbContactPerson.TabStop = false;
            this.gbContactPerson.Text = "联系人列表";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labKeyWord, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chklsbContactPerson, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnCancelChecked, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbKeyWord, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(313, 371);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // chklsbContactPerson
            // 
            this.chklsbContactPerson.BackColor = System.Drawing.Color.White;
            this.chklsbContactPerson.CheckOnClick = true;
            this.tableLayoutPanel1.SetColumnSpan(this.chklsbContactPerson, 3);
            this.chklsbContactPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chklsbContactPerson.FormattingEnabled = true;
            this.chklsbContactPerson.Location = new System.Drawing.Point(3, 43);
            this.chklsbContactPerson.Name = "chklsbContactPerson";
            this.chklsbContactPerson.Size = new System.Drawing.Size(307, 325);
            this.chklsbContactPerson.TabIndex = 9;
            // 
            // btnCancelChecked
            // 
            this.btnCancelChecked.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancelChecked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelChecked.ForeColor = System.Drawing.Color.White;
            this.btnCancelChecked.Location = new System.Drawing.Point(216, 5);
            this.btnCancelChecked.Name = "btnCancelChecked";
            this.btnCancelChecked.Size = new System.Drawing.Size(94, 30);
            this.btnCancelChecked.TabIndex = 10;
            this.btnCancelChecked.Text = "取消勾选";
            this.btnCancelChecked.UseVisualStyleBackColor = false;
            this.btnCancelChecked.Click += new System.EventHandler(this.btnCancelChecked_Click);
            // 
            // lsbStage1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.lsbStage1, 2);
            this.lsbStage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbStage1.FormattingEnabled = true;
            this.lsbStage1.ItemHeight = 15;
            this.lsbStage1.Location = new System.Drawing.Point(3, 73);
            this.lsbStage1.MultiColumn = true;
            this.lsbStage1.Name = "lsbStage1";
            this.lsbStage1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbStage1.Size = new System.Drawing.Size(307, 129);
            this.lsbStage1.TabIndex = 13;
            // 
            // lsbStage2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.lsbStage2, 2);
            this.lsbStage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbStage2.FormattingEnabled = true;
            this.lsbStage2.ItemHeight = 15;
            this.lsbStage2.Location = new System.Drawing.Point(3, 238);
            this.lsbStage2.MultiColumn = true;
            this.lsbStage2.Name = "lsbStage2";
            this.lsbStage2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbStage2.Size = new System.Drawing.Size(307, 130);
            this.lsbStage2.TabIndex = 14;
            // 
            // btnStage1Add
            // 
            this.btnStage1Add.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStage1Add.BackColor = System.Drawing.Color.Navy;
            this.btnStage1Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage1Add.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage1Add.Image = ((System.Drawing.Image)(resources.GetObject("btnStage1Add.Image")));
            this.btnStage1Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage1Add.Location = new System.Drawing.Point(3, 103);
            this.btnStage1Add.Name = "btnStage1Add";
            this.btnStage1Add.Size = new System.Drawing.Size(88, 34);
            this.btnStage1Add.TabIndex = 16;
            this.btnStage1Add.Text = "添加";
            this.btnStage1Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage1Add.UseVisualStyleBackColor = false;
            this.btnStage1Add.Click += new System.EventHandler(this.btnStage1Add_Click);
            // 
            // btnStage1Del
            // 
            this.btnStage1Del.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStage1Del.BackColor = System.Drawing.Color.Navy;
            this.btnStage1Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage1Del.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage1Del.Image = ((System.Drawing.Image)(resources.GetObject("btnStage1Del.Image")));
            this.btnStage1Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage1Del.Location = new System.Drawing.Point(3, 143);
            this.btnStage1Del.Name = "btnStage1Del";
            this.btnStage1Del.Size = new System.Drawing.Size(88, 34);
            this.btnStage1Del.TabIndex = 17;
            this.btnStage1Del.Text = "删除";
            this.btnStage1Del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage1Del.UseVisualStyleBackColor = false;
            this.btnStage1Del.Click += new System.EventHandler(this.btnStage1Del_Click);
            // 
            // btnStage2Add
            // 
            this.btnStage2Add.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStage2Add.BackColor = System.Drawing.Color.Navy;
            this.btnStage2Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage2Add.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage2Add.Image = ((System.Drawing.Image)(resources.GetObject("btnStage2Add.Image")));
            this.btnStage2Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage2Add.Location = new System.Drawing.Point(3, 264);
            this.btnStage2Add.Name = "btnStage2Add";
            this.btnStage2Add.Size = new System.Drawing.Size(88, 34);
            this.btnStage2Add.TabIndex = 19;
            this.btnStage2Add.Text = "添加";
            this.btnStage2Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage2Add.UseVisualStyleBackColor = false;
            this.btnStage2Add.Click += new System.EventHandler(this.btnStage2Add_Click);
            // 
            // btnStage2Del
            // 
            this.btnStage2Del.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStage2Del.BackColor = System.Drawing.Color.Navy;
            this.btnStage2Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage2Del.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage2Del.Image = ((System.Drawing.Image)(resources.GetObject("btnStage2Del.Image")));
            this.btnStage2Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage2Del.Location = new System.Drawing.Point(3, 304);
            this.btnStage2Del.Name = "btnStage2Del";
            this.btnStage2Del.Size = new System.Drawing.Size(88, 34);
            this.btnStage2Del.TabIndex = 20;
            this.btnStage2Del.Text = "删除";
            this.btnStage2Del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage2Del.UseVisualStyleBackColor = false;
            this.btnStage2Del.Click += new System.EventHandler(this.btnStage2Del_Click);
            // 
            // gbMsgReceiver
            // 
            this.gbMsgReceiver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gbMsgReceiver.Controls.Add(this.tableLayoutPanel2);
            this.gbMsgReceiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMsgReceiver.Location = new System.Drawing.Point(428, 3);
            this.gbMsgReceiver.Name = "gbMsgReceiver";
            this.gbMsgReceiver.Size = new System.Drawing.Size(319, 395);
            this.gbMsgReceiver.TabIndex = 21;
            this.gbMsgReceiver.TabStop = false;
            this.gbMsgReceiver.Text = "消息接收人设置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lsbStage2, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labStage2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lsbStage1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbArea, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labStage1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labArea, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(313, 371);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbMsgReceiver, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.gbContactPerson, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 375F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(750, 401);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.btnStage2Add, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.btnStage2Del, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.btnStage1Add, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnStage1Del, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(328, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(94, 395);
            this.tableLayoutPanel4.TabIndex = 21;
            // 
            // FrmMsgPushSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(750, 401);
            this.Controls.Add(this.tableLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMsgPushSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息通知设置";
            this.Load += new System.EventHandler(this.FrmMsgPushSet_Load);
            this.gbContactPerson.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbMsgReceiver.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.TextBox tbKeyWord;
        private System.Windows.Forms.Label labKeyWord;
        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.Label labStage1;
        private System.Windows.Forms.Label labStage2;
        private System.Windows.Forms.GroupBox gbContactPerson;
        private System.Windows.Forms.ListBox lsbStage1;
        private System.Windows.Forms.ListBox lsbStage2;
        private System.Windows.Forms.CheckedListBox chklsbContactPerson;
        private System.Windows.Forms.Button btnStage1Add;
        private System.Windows.Forms.Button btnStage1Del;
        private System.Windows.Forms.Button btnStage2Add;
        private System.Windows.Forms.Button btnStage2Del;
        private System.Windows.Forms.GroupBox gbMsgReceiver;
        private System.Windows.Forms.Button btnCancelChecked;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}