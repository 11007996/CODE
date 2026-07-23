namespace CallSys
{
    partial class FrmSystemConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemConfig));
            this.labArea = new System.Windows.Forms.Label();
            this.labLine = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.labMachineNum = new System.Windows.Forms.Label();
            this.labPreWeekNum = new System.Windows.Forms.Label();
            this.nudMachineNum = new System.Windows.Forms.NumericUpDown();
            this.nudPreWeekNum = new System.Windows.Forms.NumericUpDown();
            this.scSetPanel = new System.Windows.Forms.SplitContainer();
            this.btnCall = new System.Windows.Forms.Button();
            this.btnSystem = new System.Windows.Forms.Button();
            this.btnKanBan = new System.Windows.Forms.Button();
            this.btnArea = new System.Windows.Forms.Button();
            this.flpSetPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSystem = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labAutoUpdate = new System.Windows.Forms.Label();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.btnFolderBrowser = new System.Windows.Forms.Button();
            this.labHadnlerPicDir = new System.Windows.Forms.Label();
            this.tbHandlerPicDir = new System.Windows.Forms.TextBox();
            this.panelCall = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radOne = new System.Windows.Forms.RadioButton();
            this.radMultiple = new System.Windows.Forms.RadioButton();
            this.panelBaseInfo = new System.Windows.Forms.Panel();
            this.labMachine = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.panelKanBan = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudMachineNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreWeekNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scSetPanel)).BeginInit();
            this.scSetPanel.Panel1.SuspendLayout();
            this.scSetPanel.Panel2.SuspendLayout();
            this.scSetPanel.SuspendLayout();
            this.flpSetPanel.SuspendLayout();
            this.panelSystem.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelCall.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelBaseInfo.SuspendLayout();
            this.panelKanBan.SuspendLayout();
            this.SuspendLayout();
            // 
            // labArea
            // 
            this.labArea.AutoSize = true;
            this.labArea.BackColor = System.Drawing.Color.Transparent;
            this.labArea.Location = new System.Drawing.Point(47, 30);
            this.labArea.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labArea.Name = "labArea";
            this.labArea.Size = new System.Drawing.Size(45, 15);
            this.labArea.TabIndex = 0;
            this.labArea.Text = "区域:";
            // 
            // labLine
            // 
            this.labLine.AutoSize = true;
            this.labLine.BackColor = System.Drawing.Color.Transparent;
            this.labLine.Location = new System.Drawing.Point(47, 80);
            this.labLine.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labLine.Name = "labLine";
            this.labLine.Size = new System.Drawing.Size(45, 15);
            this.labLine.TabIndex = 1;
            this.labLine.Text = "线名:";
            // 
            // cmbArea
            // 
            this.cmbArea.Location = new System.Drawing.Point(93, 26);
            this.cmbArea.Margin = new System.Windows.Forms.Padding(4);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(150, 23);
            this.cmbArea.TabIndex = 2;
            this.cmbArea.SelectedIndexChanged += new System.EventHandler(this.cmbArea_SelectedIndexChanged);
            // 
            // cmbLine
            // 
            this.cmbLine.Location = new System.Drawing.Point(93, 76);
            this.cmbLine.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(150, 23);
            this.cmbLine.TabIndex = 3;
            this.cmbLine.SelectedIndexChanged += new System.EventHandler(this.cmbLine_SelectedIndexChanged);
            // 
            // labMachineNum
            // 
            this.labMachineNum.AutoSize = true;
            this.labMachineNum.BackColor = System.Drawing.Color.Transparent;
            this.labMachineNum.Location = new System.Drawing.Point(18, 44);
            this.labMachineNum.Name = "labMachineNum";
            this.labMachineNum.Size = new System.Drawing.Size(60, 15);
            this.labMachineNum.TabIndex = 8;
            this.labMachineNum.Text = "机台数:";
            // 
            // labPreWeekNum
            // 
            this.labPreWeekNum.AutoSize = true;
            this.labPreWeekNum.BackColor = System.Drawing.Color.Transparent;
            this.labPreWeekNum.Location = new System.Drawing.Point(18, 86);
            this.labPreWeekNum.Name = "labPreWeekNum";
            this.labPreWeekNum.Size = new System.Drawing.Size(60, 15);
            this.labPreWeekNum.TabIndex = 9;
            this.labPreWeekNum.Text = "星期数:";
            // 
            // nudMachineNum
            // 
            this.nudMachineNum.Location = new System.Drawing.Point(84, 42);
            this.nudMachineNum.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMachineNum.Name = "nudMachineNum";
            this.nudMachineNum.Size = new System.Drawing.Size(150, 25);
            this.nudMachineNum.TabIndex = 10;
            // 
            // nudPreWeekNum
            // 
            this.nudPreWeekNum.Location = new System.Drawing.Point(84, 84);
            this.nudPreWeekNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPreWeekNum.Name = "nudPreWeekNum";
            this.nudPreWeekNum.Size = new System.Drawing.Size(150, 25);
            this.nudPreWeekNum.TabIndex = 11;
            // 
            // scSetPanel
            // 
            this.scSetPanel.BackColor = System.Drawing.Color.Transparent;
            this.scSetPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scSetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSetPanel.Location = new System.Drawing.Point(0, 0);
            this.scSetPanel.Margin = new System.Windows.Forms.Padding(0);
            this.scSetPanel.Name = "scSetPanel";
            // 
            // scSetPanel.Panel1
            // 
            this.scSetPanel.Panel1.Controls.Add(this.btnCall);
            this.scSetPanel.Panel1.Controls.Add(this.btnSystem);
            this.scSetPanel.Panel1.Controls.Add(this.btnKanBan);
            this.scSetPanel.Panel1.Controls.Add(this.btnArea);
            // 
            // scSetPanel.Panel2
            // 
            this.scSetPanel.Panel2.Controls.Add(this.flpSetPanel);
            this.scSetPanel.Size = new System.Drawing.Size(395, 276);
            this.scSetPanel.SplitterDistance = 78;
            this.scSetPanel.TabIndex = 13;
            // 
            // btnCall
            // 
            this.btnCall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCall.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCall.ForeColor = System.Drawing.Color.White;
            this.btnCall.Location = new System.Drawing.Point(0, 60);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(74, 30);
            this.btnCall.TabIndex = 3;
            this.btnCall.Text = "呼叫";
            this.btnCall.UseVisualStyleBackColor = false;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // btnSystem
            // 
            this.btnSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSystem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSystem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSystem.ForeColor = System.Drawing.Color.White;
            this.btnSystem.Location = new System.Drawing.Point(0, 242);
            this.btnSystem.Name = "btnSystem";
            this.btnSystem.Size = new System.Drawing.Size(74, 30);
            this.btnSystem.TabIndex = 2;
            this.btnSystem.Text = "系统";
            this.btnSystem.UseVisualStyleBackColor = false;
            this.btnSystem.Click += new System.EventHandler(this.btnSystem_Click);
            // 
            // btnKanBan
            // 
            this.btnKanBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnKanBan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKanBan.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnKanBan.ForeColor = System.Drawing.Color.White;
            this.btnKanBan.Location = new System.Drawing.Point(0, 30);
            this.btnKanBan.Name = "btnKanBan";
            this.btnKanBan.Size = new System.Drawing.Size(74, 30);
            this.btnKanBan.TabIndex = 1;
            this.btnKanBan.Text = "看板";
            this.btnKanBan.UseVisualStyleBackColor = false;
            this.btnKanBan.Click += new System.EventHandler(this.btnKanBan_Click);
            // 
            // btnArea
            // 
            this.btnArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnArea.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnArea.ForeColor = System.Drawing.Color.White;
            this.btnArea.Location = new System.Drawing.Point(0, 0);
            this.btnArea.Name = "btnArea";
            this.btnArea.Size = new System.Drawing.Size(74, 30);
            this.btnArea.TabIndex = 0;
            this.btnArea.Text = "基础";
            this.btnArea.UseVisualStyleBackColor = false;
            this.btnArea.Click += new System.EventHandler(this.btnBase_Click);
            // 
            // flpSetPanel
            // 
            this.flpSetPanel.Controls.Add(this.panelSystem);
            this.flpSetPanel.Controls.Add(this.panelCall);
            this.flpSetPanel.Controls.Add(this.panelBaseInfo);
            this.flpSetPanel.Controls.Add(this.panelKanBan);
            this.flpSetPanel.Location = new System.Drawing.Point(0, 0);
            this.flpSetPanel.Name = "flpSetPanel";
            this.flpSetPanel.Size = new System.Drawing.Size(306, 540);
            this.flpSetPanel.TabIndex = 1;
            // 
            // panelSystem
            // 
            this.panelSystem.Controls.Add(this.groupBox2);
            this.panelSystem.Controls.Add(this.labAutoUpdate);
            this.panelSystem.Controls.Add(this.chkAutoUpdate);
            this.panelSystem.Controls.Add(this.btnFolderBrowser);
            this.panelSystem.Controls.Add(this.labHadnlerPicDir);
            this.panelSystem.Controls.Add(this.tbHandlerPicDir);
            this.panelSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSystem.Location = new System.Drawing.Point(3, 3);
            this.panelSystem.Name = "panelSystem";
            this.panelSystem.Size = new System.Drawing.Size(300, 270);
            this.panelSystem.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(4, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 116);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "更新检查机制";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(10, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(265, 36);
            this.label5.TabIndex = 2;
            this.label5.Text = "（3）每日8:30[如打开了看板或呼叫面板,则需要处理完成后才执行更新]";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(10, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "（2）呼叫面板呼叫按钮";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(10, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "（1）应用启动";
            // 
            // labAutoUpdate
            // 
            this.labAutoUpdate.AutoSize = true;
            this.labAutoUpdate.Location = new System.Drawing.Point(18, 59);
            this.labAutoUpdate.Name = "labAutoUpdate";
            this.labAutoUpdate.Size = new System.Drawing.Size(75, 15);
            this.labAutoUpdate.TabIndex = 8;
            this.labAutoUpdate.Text = "自动更新:";
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(95, 57);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(44, 19);
            this.chkAutoUpdate.TabIndex = 7;
            this.chkAutoUpdate.Text = "是";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // btnFolderBrowser
            // 
            this.btnFolderBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnFolderBrowser.Location = new System.Drawing.Point(247, 23);
            this.btnFolderBrowser.Name = "btnFolderBrowser";
            this.btnFolderBrowser.Size = new System.Drawing.Size(45, 25);
            this.btnFolderBrowser.TabIndex = 6;
            this.btnFolderBrowser.Text = "选择";
            this.btnFolderBrowser.UseVisualStyleBackColor = false;
            this.btnFolderBrowser.Click += new System.EventHandler(this.btnFolderBrowser_Click);
            // 
            // labHadnlerPicDir
            // 
            this.labHadnlerPicDir.AutoSize = true;
            this.labHadnlerPicDir.Location = new System.Drawing.Point(18, 28);
            this.labHadnlerPicDir.Name = "labHadnlerPicDir";
            this.labHadnlerPicDir.Size = new System.Drawing.Size(75, 15);
            this.labHadnlerPicDir.TabIndex = 4;
            this.labHadnlerPicDir.Text = "头像缓存:";
            // 
            // tbHandlerPicDir
            // 
            this.tbHandlerPicDir.Enabled = false;
            this.tbHandlerPicDir.Location = new System.Drawing.Point(95, 24);
            this.tbHandlerPicDir.Name = "tbHandlerPicDir";
            this.tbHandlerPicDir.Size = new System.Drawing.Size(150, 25);
            this.tbHandlerPicDir.TabIndex = 5;
            // 
            // panelCall
            // 
            this.panelCall.Controls.Add(this.groupBox1);
            this.panelCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCall.Location = new System.Drawing.Point(3, 279);
            this.panelCall.Name = "panelCall";
            this.panelCall.Size = new System.Drawing.Size(300, 270);
            this.panelCall.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radOne);
            this.groupBox1.Controls.Add(this.radMultiple);
            this.groupBox1.Location = new System.Drawing.Point(3, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 47);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "呼叫限制";
            // 
            // radOne
            // 
            this.radOne.AutoSize = true;
            this.radOne.Location = new System.Drawing.Point(29, 24);
            this.radOne.Name = "radOne";
            this.radOne.Size = new System.Drawing.Size(58, 19);
            this.radOne.TabIndex = 12;
            this.radOne.TabStop = true;
            this.radOne.Text = "单个";
            this.radOne.UseVisualStyleBackColor = true;
            // 
            // radMultiple
            // 
            this.radMultiple.AutoSize = true;
            this.radMultiple.Location = new System.Drawing.Point(134, 24);
            this.radMultiple.Name = "radMultiple";
            this.radMultiple.Size = new System.Drawing.Size(58, 19);
            this.radMultiple.TabIndex = 13;
            this.radMultiple.TabStop = true;
            this.radMultiple.Text = "多个";
            this.radMultiple.UseVisualStyleBackColor = true;
            // 
            // panelBaseInfo
            // 
            this.panelBaseInfo.Controls.Add(this.labMachine);
            this.panelBaseInfo.Controls.Add(this.cmbMachine);
            this.panelBaseInfo.Controls.Add(this.labLine);
            this.panelBaseInfo.Controls.Add(this.cmbLine);
            this.panelBaseInfo.Controls.Add(this.labArea);
            this.panelBaseInfo.Controls.Add(this.cmbArea);
            this.panelBaseInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBaseInfo.Location = new System.Drawing.Point(3, 555);
            this.panelBaseInfo.Name = "panelBaseInfo";
            this.panelBaseInfo.Size = new System.Drawing.Size(300, 270);
            this.panelBaseInfo.TabIndex = 1;
            // 
            // labMachine
            // 
            this.labMachine.AutoSize = true;
            this.labMachine.Location = new System.Drawing.Point(45, 127);
            this.labMachine.Name = "labMachine";
            this.labMachine.Size = new System.Drawing.Size(45, 15);
            this.labMachine.TabIndex = 10;
            this.labMachine.Text = "机台:";
            // 
            // cmbMachine
            // 
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(93, 123);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(150, 23);
            this.cmbMachine.TabIndex = 9;
            // 
            // panelKanBan
            // 
            this.panelKanBan.Controls.Add(this.nudMachineNum);
            this.panelKanBan.Controls.Add(this.labMachineNum);
            this.panelKanBan.Controls.Add(this.nudPreWeekNum);
            this.panelKanBan.Controls.Add(this.labPreWeekNum);
            this.panelKanBan.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKanBan.Location = new System.Drawing.Point(3, 831);
            this.panelKanBan.Name = "panelKanBan";
            this.panelKanBan.Size = new System.Drawing.Size(300, 270);
            this.panelKanBan.TabIndex = 1;
            // 
            // FrmSystemConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(395, 276);
            this.Controls.Add(this.scSetPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSystemConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmSystemConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMachineNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPreWeekNum)).EndInit();
            this.scSetPanel.Panel1.ResumeLayout(false);
            this.scSetPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scSetPanel)).EndInit();
            this.scSetPanel.ResumeLayout(false);
            this.flpSetPanel.ResumeLayout(false);
            this.panelSystem.ResumeLayout(false);
            this.panelSystem.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panelCall.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelBaseInfo.ResumeLayout(false);
            this.panelBaseInfo.PerformLayout();
            this.panelKanBan.ResumeLayout(false);
            this.panelKanBan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labArea;
        private System.Windows.Forms.Label labLine;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.Label labMachineNum;
        private System.Windows.Forms.Label labPreWeekNum;
        private System.Windows.Forms.NumericUpDown nudMachineNum;
        private System.Windows.Forms.NumericUpDown nudPreWeekNum;
        private System.Windows.Forms.SplitContainer scSetPanel;
        private System.Windows.Forms.Panel panelBaseInfo;
        private System.Windows.Forms.Panel panelKanBan;
        private System.Windows.Forms.FlowLayoutPanel flpSetPanel;
        private System.Windows.Forms.Button btnKanBan;
        private System.Windows.Forms.Button btnArea;
        private System.Windows.Forms.Button btnFolderBrowser;
        private System.Windows.Forms.TextBox tbHandlerPicDir;
        private System.Windows.Forms.Label labHadnlerPicDir;
        private System.Windows.Forms.Label labAutoUpdate;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.Label labMachine;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.Button btnSystem;
        private System.Windows.Forms.Panel panelSystem;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Panel panelCall;
        private System.Windows.Forms.RadioButton radMultiple;
        private System.Windows.Forms.RadioButton radOne;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}