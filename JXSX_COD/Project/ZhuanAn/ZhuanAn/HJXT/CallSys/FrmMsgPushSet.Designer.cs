namespace CallSys
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
            this.tbWorkCode = new System.Windows.Forms.TextBox();
            this.tbRealName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.labWorkCode = new System.Windows.Forms.Label();
            this.labRealName = new System.Windows.Forms.Label();
            this.labArea = new System.Windows.Forms.Label();
            this.labStage1 = new System.Windows.Forms.Label();
            this.labStage2 = new System.Windows.Forms.Label();
            this.gbContactPerson = new System.Windows.Forms.GroupBox();
            this.btnCancelChecked = new System.Windows.Forms.Button();
            this.chklsbContactPerson = new System.Windows.Forms.CheckedListBox();
            this.lsbStage1 = new System.Windows.Forms.ListBox();
            this.lsbStage2 = new System.Windows.Forms.ListBox();
            this.btnStage1Add = new System.Windows.Forms.Button();
            this.btnStage1Del = new System.Windows.Forms.Button();
            this.btnStage2Add = new System.Windows.Forms.Button();
            this.btnStage2Del = new System.Windows.Forms.Button();
            this.gbMsgReceiver = new System.Windows.Forms.GroupBox();
            this.gbContactPerson.SuspendLayout();
            this.gbMsgReceiver.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(125, 27);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(95, 23);
            this.cmbArea.TabIndex = 1;
            this.cmbArea.SelectedIndexChanged += new System.EventHandler(this.cmbArea_SelectedIndexChanged);
            // 
            // tbWorkCode
            // 
            this.tbWorkCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbWorkCode.Location = new System.Drawing.Point(7, 56);
            this.tbWorkCode.Name = "tbWorkCode";
            this.tbWorkCode.Size = new System.Drawing.Size(114, 25);
            this.tbWorkCode.TabIndex = 3;
            // 
            // tbRealName
            // 
            this.tbRealName.Location = new System.Drawing.Point(127, 56);
            this.tbRealName.Name = "tbRealName";
            this.tbRealName.Size = new System.Drawing.Size(120, 25);
            this.tbRealName.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(253, 53);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDel.ForeColor = System.Drawing.Color.White;
            this.btnDel.Location = new System.Drawing.Point(253, 89);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 30);
            this.btnDel.TabIndex = 6;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // labWorkCode
            // 
            this.labWorkCode.AutoSize = true;
            this.labWorkCode.Location = new System.Drawing.Point(13, 38);
            this.labWorkCode.Name = "labWorkCode";
            this.labWorkCode.Size = new System.Drawing.Size(45, 15);
            this.labWorkCode.TabIndex = 7;
            this.labWorkCode.Text = "工号:";
            // 
            // labRealName
            // 
            this.labRealName.AutoSize = true;
            this.labRealName.Location = new System.Drawing.Point(124, 39);
            this.labRealName.Name = "labRealName";
            this.labRealName.Size = new System.Drawing.Size(45, 15);
            this.labRealName.TabIndex = 8;
            this.labRealName.Text = "姓名:";
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.Location = new System.Drawing.Point(74, 30);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(45, 15);
            this.labArea.TabIndex = 9;
            this.labArea.Text = "区域:";
            // 
            // labStage1
            // 
            this.labStage1.AutoSize = true;
            this.labStage1.Location = new System.Drawing.Point(74, 68);
            this.labStage1.Name = "labStage1";
            this.labStage1.Size = new System.Drawing.Size(67, 15);
            this.labStage1.TabIndex = 10;
            this.labStage1.Text = "呼叫支援";
            // 
            // labStage2
            // 
            this.labStage2.AutoSize = true;
            this.labStage2.Location = new System.Drawing.Point(74, 254);
            this.labStage2.Name = "labStage2";
            this.labStage2.Size = new System.Drawing.Size(67, 15);
            this.labStage2.TabIndex = 11;
            this.labStage2.Text = "支援超时";
            // 
            // gbContactPerson
            // 
            this.gbContactPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.gbContactPerson.Controls.Add(this.btnCancelChecked);
            this.gbContactPerson.Controls.Add(this.chklsbContactPerson);
            this.gbContactPerson.Controls.Add(this.labWorkCode);
            this.gbContactPerson.Controls.Add(this.labRealName);
            this.gbContactPerson.Controls.Add(this.tbWorkCode);
            this.gbContactPerson.Controls.Add(this.tbRealName);
            this.gbContactPerson.Controls.Add(this.btnDel);
            this.gbContactPerson.Controls.Add(this.btnAdd);
            this.gbContactPerson.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbContactPerson.Location = new System.Drawing.Point(0, 0);
            this.gbContactPerson.Margin = new System.Windows.Forms.Padding(10);
            this.gbContactPerson.Name = "gbContactPerson";
            this.gbContactPerson.Padding = new System.Windows.Forms.Padding(10);
            this.gbContactPerson.Size = new System.Drawing.Size(398, 431);
            this.gbContactPerson.TabIndex = 12;
            this.gbContactPerson.TabStop = false;
            this.gbContactPerson.Text = "联系人列表";
            // 
            // btnCancelChecked
            // 
            this.btnCancelChecked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancelChecked.ForeColor = System.Drawing.Color.White;
            this.btnCancelChecked.Location = new System.Drawing.Point(253, 383);
            this.btnCancelChecked.Name = "btnCancelChecked";
            this.btnCancelChecked.Size = new System.Drawing.Size(97, 30);
            this.btnCancelChecked.TabIndex = 10;
            this.btnCancelChecked.Text = "取消勾选";
            this.btnCancelChecked.UseVisualStyleBackColor = false;
            this.btnCancelChecked.Click += new System.EventHandler(this.btnCancelChecked_Click);
            // 
            // chklsbContactPerson
            // 
            this.chklsbContactPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.chklsbContactPerson.CheckOnClick = true;
            this.chklsbContactPerson.FormattingEnabled = true;
            this.chklsbContactPerson.Location = new System.Drawing.Point(7, 89);
            this.chklsbContactPerson.Name = "chklsbContactPerson";
            this.chklsbContactPerson.Size = new System.Drawing.Size(240, 324);
            this.chklsbContactPerson.TabIndex = 9;
            // 
            // lsbStage1
            // 
            this.lsbStage1.FormattingEnabled = true;
            this.lsbStage1.ItemHeight = 15;
            this.lsbStage1.Location = new System.Drawing.Point(77, 94);
            this.lsbStage1.MultiColumn = true;
            this.lsbStage1.Name = "lsbStage1";
            this.lsbStage1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbStage1.Size = new System.Drawing.Size(329, 139);
            this.lsbStage1.TabIndex = 13;
            // 
            // lsbStage2
            // 
            this.lsbStage2.FormattingEnabled = true;
            this.lsbStage2.ItemHeight = 15;
            this.lsbStage2.Location = new System.Drawing.Point(77, 272);
            this.lsbStage2.MultiColumn = true;
            this.lsbStage2.Name = "lsbStage2";
            this.lsbStage2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbStage2.Size = new System.Drawing.Size(329, 139);
            this.lsbStage2.TabIndex = 14;
            // 
            // btnStage1Add
            // 
            this.btnStage1Add.BackColor = System.Drawing.Color.Navy;
            this.btnStage1Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage1Add.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage1Add.Image = ((System.Drawing.Image)(resources.GetObject("btnStage1Add.Image")));
            this.btnStage1Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage1Add.Location = new System.Drawing.Point(357, 130);
            this.btnStage1Add.Name = "btnStage1Add";
            this.btnStage1Add.Size = new System.Drawing.Size(80, 30);
            this.btnStage1Add.TabIndex = 16;
            this.btnStage1Add.Text = "添加";
            this.btnStage1Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage1Add.UseVisualStyleBackColor = false;
            this.btnStage1Add.Click += new System.EventHandler(this.btnStage1Add_Click);
            // 
            // btnStage1Del
            // 
            this.btnStage1Del.BackColor = System.Drawing.Color.Navy;
            this.btnStage1Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage1Del.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage1Del.Image = ((System.Drawing.Image)(resources.GetObject("btnStage1Del.Image")));
            this.btnStage1Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage1Del.Location = new System.Drawing.Point(357, 165);
            this.btnStage1Del.Name = "btnStage1Del";
            this.btnStage1Del.Size = new System.Drawing.Size(80, 30);
            this.btnStage1Del.TabIndex = 17;
            this.btnStage1Del.Text = "删除";
            this.btnStage1Del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage1Del.UseVisualStyleBackColor = false;
            this.btnStage1Del.Click += new System.EventHandler(this.btnStage1Del_Click);
            // 
            // btnStage2Add
            // 
            this.btnStage2Add.BackColor = System.Drawing.Color.Navy;
            this.btnStage2Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage2Add.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage2Add.Image = ((System.Drawing.Image)(resources.GetObject("btnStage2Add.Image")));
            this.btnStage2Add.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage2Add.Location = new System.Drawing.Point(357, 312);
            this.btnStage2Add.Name = "btnStage2Add";
            this.btnStage2Add.Size = new System.Drawing.Size(80, 30);
            this.btnStage2Add.TabIndex = 19;
            this.btnStage2Add.Text = "添加";
            this.btnStage2Add.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage2Add.UseVisualStyleBackColor = false;
            this.btnStage2Add.Click += new System.EventHandler(this.btnStage2Add_Click);
            // 
            // btnStage2Del
            // 
            this.btnStage2Del.BackColor = System.Drawing.Color.Navy;
            this.btnStage2Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStage2Del.ForeColor = System.Drawing.Color.Turquoise;
            this.btnStage2Del.Image = ((System.Drawing.Image)(resources.GetObject("btnStage2Del.Image")));
            this.btnStage2Del.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStage2Del.Location = new System.Drawing.Point(357, 347);
            this.btnStage2Del.Name = "btnStage2Del";
            this.btnStage2Del.Size = new System.Drawing.Size(80, 30);
            this.btnStage2Del.TabIndex = 20;
            this.btnStage2Del.Text = "删除";
            this.btnStage2Del.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStage2Del.UseVisualStyleBackColor = false;
            this.btnStage2Del.Click += new System.EventHandler(this.btnStage2Del_Click);
            // 
            // gbMsgReceiver
            // 
            this.gbMsgReceiver.Controls.Add(this.lsbStage1);
            this.gbMsgReceiver.Controls.Add(this.labStage2);
            this.gbMsgReceiver.Controls.Add(this.cmbArea);
            this.gbMsgReceiver.Controls.Add(this.lsbStage2);
            this.gbMsgReceiver.Controls.Add(this.labStage1);
            this.gbMsgReceiver.Controls.Add(this.labArea);
            this.gbMsgReceiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMsgReceiver.Location = new System.Drawing.Point(398, 0);
            this.gbMsgReceiver.Name = "gbMsgReceiver";
            this.gbMsgReceiver.Size = new System.Drawing.Size(418, 431);
            this.gbMsgReceiver.TabIndex = 21;
            this.gbMsgReceiver.TabStop = false;
            this.gbMsgReceiver.Text = "消息接收人设置";
            // 
            // FrmMsgPushSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(816, 431);
            this.Controls.Add(this.btnStage1Add);
            this.Controls.Add(this.btnStage1Del);
            this.Controls.Add(this.btnStage2Add);
            this.Controls.Add(this.btnStage2Del);
            this.Controls.Add(this.gbMsgReceiver);
            this.Controls.Add(this.gbContactPerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMsgPushSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "消息通知设置";
            this.Load += new System.EventHandler(this.FrmMsgPushSet_Load);
            this.gbContactPerson.ResumeLayout(false);
            this.gbContactPerson.PerformLayout();
            this.gbMsgReceiver.ResumeLayout(false);
            this.gbMsgReceiver.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.TextBox tbWorkCode;
        private System.Windows.Forms.TextBox tbRealName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label labWorkCode;
        private System.Windows.Forms.Label labRealName;
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
    }
}