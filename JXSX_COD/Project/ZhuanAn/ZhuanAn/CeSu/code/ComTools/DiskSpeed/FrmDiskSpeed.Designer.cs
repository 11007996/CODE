namespace ComTools
{
    partial class FrmDiskSpeed
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiskSpeed));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labReadSpeed = new System.Windows.Forms.Label();
            this.nudReadSet = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudWriteSet = new System.Windows.Forms.NumericUpDown();
            this.labWriteSpeed = new System.Windows.Forms.Label();
            this.readProgressBar = new System.Windows.Forms.ProgressBar();
            this.writeProgressBar = new System.Windows.Forms.ProgressBar();
            this.labWriteResult = new System.Windows.Forms.Label();
            this.labReadResult = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnFlag = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFileSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBlockSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTargetDisk = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRunCount = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAutoRun = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteSet)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(153, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 466);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.labReadSpeed, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.nudReadSet, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.label8, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.nudWriteSet, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.labWriteSpeed, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.readProgressBar, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.writeProgressBar, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labWriteResult, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labReadResult, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label13, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnFlag, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(575, 466);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // labReadSpeed
            // 
            this.labReadSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labReadSpeed.AutoSize = true;
            this.labReadSpeed.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labReadSpeed.ForeColor = System.Drawing.Color.Blue;
            this.labReadSpeed.Location = new System.Drawing.Point(410, 347);
            this.labReadSpeed.Name = "labReadSpeed";
            this.labReadSpeed.Size = new System.Drawing.Size(34, 37);
            this.labReadSpeed.TabIndex = 10;
            this.labReadSpeed.Text = "0";
            // 
            // nudReadSet
            // 
            this.nudReadSet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudReadSet.Location = new System.Drawing.Point(410, 313);
            this.nudReadSet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudReadSet.Name = "nudReadSet";
            this.nudReadSet.Size = new System.Drawing.Size(162, 25);
            this.nudReadSet.TabIndex = 8;
            this.nudReadSet.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(305, 318);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "合格设定(MB)";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 318);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "合格设定(MB)";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "当前写入(MB)";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(305, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 15);
            this.label9.TabIndex = 6;
            this.label9.Text = "当前读取(MB)";
            // 
            // nudWriteSet
            // 
            this.nudWriteSet.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudWriteSet.Location = new System.Drawing.Point(123, 313);
            this.nudWriteSet.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudWriteSet.Name = "nudWriteSet";
            this.nudWriteSet.Size = new System.Drawing.Size(161, 25);
            this.nudWriteSet.TabIndex = 7;
            this.nudWriteSet.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // labWriteSpeed
            // 
            this.labWriteSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labWriteSpeed.AutoSize = true;
            this.labWriteSpeed.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labWriteSpeed.ForeColor = System.Drawing.Color.Blue;
            this.labWriteSpeed.Location = new System.Drawing.Point(123, 347);
            this.labWriteSpeed.Name = "labWriteSpeed";
            this.labWriteSpeed.Size = new System.Drawing.Size(34, 37);
            this.labWriteSpeed.TabIndex = 9;
            this.labWriteSpeed.Text = "0";
            // 
            // readProgressBar
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.readProgressBar, 2);
            this.readProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readProgressBar.Location = new System.Drawing.Point(290, 53);
            this.readProgressBar.Name = "readProgressBar";
            this.readProgressBar.Size = new System.Drawing.Size(282, 34);
            this.readProgressBar.TabIndex = 12;
            // 
            // writeProgressBar
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.writeProgressBar, 2);
            this.writeProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeProgressBar.Location = new System.Drawing.Point(3, 53);
            this.writeProgressBar.Name = "writeProgressBar";
            this.writeProgressBar.Size = new System.Drawing.Size(281, 34);
            this.writeProgressBar.TabIndex = 11;
            // 
            // labWriteResult
            // 
            this.labWriteResult.BackColor = System.Drawing.Color.Blue;
            this.tableLayoutPanel3.SetColumnSpan(this.labWriteResult, 2);
            this.labWriteResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labWriteResult.Font = new System.Drawing.Font("微软雅黑", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labWriteResult.ForeColor = System.Drawing.Color.White;
            this.labWriteResult.Location = new System.Drawing.Point(3, 90);
            this.labWriteResult.Name = "labWriteResult";
            this.labWriteResult.Size = new System.Drawing.Size(281, 216);
            this.labWriteResult.TabIndex = 13;
            this.labWriteResult.Text = "Ready";
            this.labWriteResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labReadResult
            // 
            this.labReadResult.AutoSize = true;
            this.labReadResult.BackColor = System.Drawing.Color.Blue;
            this.tableLayoutPanel3.SetColumnSpan(this.labReadResult, 2);
            this.labReadResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labReadResult.Font = new System.Drawing.Font("微软雅黑", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labReadResult.ForeColor = System.Drawing.Color.White;
            this.labReadResult.Location = new System.Drawing.Point(290, 90);
            this.labReadResult.Name = "labReadResult";
            this.labReadResult.Size = new System.Drawing.Size(282, 216);
            this.labReadResult.TabIndex = 14;
            this.labReadResult.Text = "Ready";
            this.labReadResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel3.SetColumnSpan(this.label12, 2);
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(281, 50);
            this.label12.TabIndex = 15;
            this.label12.Text = "写入测试";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tableLayoutPanel3.SetColumnSpan(this.label13, 2);
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(290, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(282, 50);
            this.label13.TabIndex = 16;
            this.label13.Text = "读取测试";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFlag
            // 
            this.btnFlag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFlag.BackColor = System.Drawing.Color.Green;
            this.tableLayoutPanel3.SetColumnSpan(this.btnFlag, 4);
            this.btnFlag.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFlag.ForeColor = System.Drawing.Color.White;
            this.btnFlag.Location = new System.Drawing.Point(227, 398);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Size = new System.Drawing.Size(120, 55);
            this.btnFlag.TabIndex = 0;
            this.btnFlag.Text = "Start";
            this.btnFlag.UseVisualStyleBackColor = false;
            this.btnFlag.Click += new System.EventHandler(this.btnFlag_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Pink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbFileSize, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbBlockSize, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbTargetDisk, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.cbRunCount, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.cbAutoRun, 0, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(144, 466);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件大小";
            // 
            // cbFileSize
            // 
            this.cbFileSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbFileSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileSize.FormattingEnabled = true;
            this.cbFileSize.Location = new System.Drawing.Point(3, 38);
            this.cbFileSize.Name = "cbFileSize";
            this.cbFileSize.Size = new System.Drawing.Size(138, 23);
            this.cbFileSize.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "块大小";
            // 
            // cbBlockSize
            // 
            this.cbBlockSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBlockSize.FormattingEnabled = true;
            this.cbBlockSize.Location = new System.Drawing.Point(3, 108);
            this.cbBlockSize.Name = "cbBlockSize";
            this.cbBlockSize.Size = new System.Drawing.Size(138, 23);
            this.cbBlockSize.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "目标磁盘";
            // 
            // cbTargetDisk
            // 
            this.cbTargetDisk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbTargetDisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetDisk.FormattingEnabled = true;
            this.cbTargetDisk.Location = new System.Drawing.Point(3, 178);
            this.cbTargetDisk.Name = "cbTargetDisk";
            this.cbTargetDisk.Size = new System.Drawing.Size(138, 23);
            this.cbTargetDisk.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "运行次数";
            // 
            // cbRunCount
            // 
            this.cbRunCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbRunCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRunCount.FormattingEnabled = true;
            this.cbRunCount.Location = new System.Drawing.Point(3, 248);
            this.cbRunCount.Name = "cbRunCount";
            this.cbRunCount.Size = new System.Drawing.Size(138, 23);
            this.cbRunCount.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "自动执行";
            // 
            // cbAutoRun
            // 
            this.cbAutoRun.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbAutoRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoRun.FormattingEnabled = true;
            this.cbAutoRun.Location = new System.Drawing.Point(3, 318);
            this.cbAutoRun.Name = "cbAutoRun";
            this.cbAutoRun.Size = new System.Drawing.Size(138, 23);
            this.cbAutoRun.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbLog, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(981, 472);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rtbLog
            // 
            this.rtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.Location = new System.Drawing.Point(734, 3);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(244, 466);
            this.rtbLog.TabIndex = 2;
            this.rtbLog.Text = "";
            // 
            // progressTimer
            // 
            this.progressTimer.Interval = 300;
            this.progressTimer.Tick += new System.EventHandler(this.progressTimer_Tick);
            // 
            // FrmDiskSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1021, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDiskSpeed";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "磁盘测速";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DiskSpeedTestForm_FormClosing);
            this.Load += new System.EventHandler(this.DiskSpeedTestForm_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteSet)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFlag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFileSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBlockSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTargetDisk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbRunCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbAutoRun;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labReadSpeed;
        private System.Windows.Forms.NumericUpDown nudReadSet;
        private System.Windows.Forms.NumericUpDown nudWriteSet;
        private System.Windows.Forms.Label labWriteSpeed;
        private System.Windows.Forms.ProgressBar writeProgressBar;
        private System.Windows.Forms.ProgressBar readProgressBar;
        private System.Windows.Forms.Label labWriteResult;
        private System.Windows.Forms.Label labReadResult;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

