namespace LuxMMS
{
    partial class FrmConfigForSpeechSet
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
            this.swBtnCallHSpeechFlag = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labCallHandlerSpeechFlag = new System.Windows.Forms.Label();
            this.labSpeechSpanMinuteUnit = new System.Windows.Forms.Label();
            this.labSpeechSpanMinute = new System.Windows.Forms.Label();
            this.labSpeechRate = new System.Windows.Forms.Label();
            this.nudSpeechSpanMinute = new System.Windows.Forms.NumericUpDown();
            this.trackBarSpeechRate = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeechSpanMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeechRate)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // swBtnCallHSpeechFlag
            // 
            this.swBtnCallHSpeechFlag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            // 
            // 
            // 
            this.swBtnCallHSpeechFlag.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.swBtnCallHSpeechFlag.Location = new System.Drawing.Point(79, 89);
            this.swBtnCallHSpeechFlag.Name = "swBtnCallHSpeechFlag";
            this.swBtnCallHSpeechFlag.OffText = "关闭";
            this.swBtnCallHSpeechFlag.OnText = "开启";
            this.swBtnCallHSpeechFlag.Size = new System.Drawing.Size(66, 22);
            this.swBtnCallHSpeechFlag.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.swBtnCallHSpeechFlag.TabIndex = 9;
            // 
            // labCallHandlerSpeechFlag
            // 
            this.labCallHandlerSpeechFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labCallHandlerSpeechFlag.AutoSize = true;
            this.labCallHandlerSpeechFlag.Location = new System.Drawing.Point(6, 92);
            this.labCallHandlerSpeechFlag.Name = "labCallHandlerSpeechFlag";
            this.labCallHandlerSpeechFlag.Size = new System.Drawing.Size(67, 15);
            this.labCallHandlerSpeechFlag.TabIndex = 8;
            this.labCallHandlerSpeechFlag.Text = "人员广播";
            this.labCallHandlerSpeechFlag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSpeechSpanMinuteUnit
            // 
            this.labSpeechSpanMinuteUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labSpeechSpanMinuteUnit.AutoSize = true;
            this.labSpeechSpanMinuteUnit.Location = new System.Drawing.Point(232, 52);
            this.labSpeechSpanMinuteUnit.Name = "labSpeechSpanMinuteUnit";
            this.labSpeechSpanMinuteUnit.Size = new System.Drawing.Size(53, 15);
            this.labSpeechSpanMinuteUnit.TabIndex = 7;
            this.labSpeechSpanMinuteUnit.Text = "(分钟)";
            // 
            // labSpeechSpanMinute
            // 
            this.labSpeechSpanMinute.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSpeechSpanMinute.AutoSize = true;
            this.labSpeechSpanMinute.Location = new System.Drawing.Point(6, 52);
            this.labSpeechSpanMinute.Name = "labSpeechSpanMinute";
            this.labSpeechSpanMinute.Size = new System.Drawing.Size(67, 15);
            this.labSpeechSpanMinute.TabIndex = 6;
            this.labSpeechSpanMinute.Text = "广播频率";
            this.labSpeechSpanMinute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labSpeechRate
            // 
            this.labSpeechRate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labSpeechRate.AutoSize = true;
            this.labSpeechRate.Location = new System.Drawing.Point(6, 12);
            this.labSpeechRate.Name = "labSpeechRate";
            this.labSpeechRate.Size = new System.Drawing.Size(67, 15);
            this.labSpeechRate.TabIndex = 5;
            this.labSpeechRate.Text = "广播语速";
            this.labSpeechRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudSpeechSpanMinute
            // 
            this.nudSpeechSpanMinute.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudSpeechSpanMinute.Location = new System.Drawing.Point(79, 47);
            this.nudSpeechSpanMinute.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSpeechSpanMinute.Name = "nudSpeechSpanMinute";
            this.nudSpeechSpanMinute.Size = new System.Drawing.Size(147, 25);
            this.nudSpeechSpanMinute.TabIndex = 2;
            this.nudSpeechSpanMinute.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // trackBarSpeechRate
            // 
            this.trackBarSpeechRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trackBarSpeechRate.AutoSize = false;
            this.tableLayoutPanel1.SetColumnSpan(this.trackBarSpeechRate, 2);
            this.trackBarSpeechRate.Location = new System.Drawing.Point(79, 7);
            this.trackBarSpeechRate.Maximum = 5;
            this.trackBarSpeechRate.Minimum = -5;
            this.trackBarSpeechRate.Name = "trackBarSpeechRate";
            this.trackBarSpeechRate.Size = new System.Drawing.Size(150, 25);
            this.trackBarSpeechRate.TabIndex = 4;
            this.trackBarSpeechRate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.labSpeechRate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labSpeechSpanMinuteUnit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.swBtnCallHSpeechFlag, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labSpeechSpanMinute, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labCallHandlerSpeechFlag, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.trackBarSpeechRate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nudSpeechSpanMinute, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 120);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 223);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "广播设置";
            // 
            // FrmConfigForSpeechSet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(312, 223);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConfigForSpeechSet";
            this.Text = "广播设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSystemConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfigForSpeechSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeechSpanMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeechRate)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NumericUpDown nudSpeechSpanMinute;
        private System.Windows.Forms.TrackBar trackBarSpeechRate;
        private System.Windows.Forms.Label labSpeechRate;
        private System.Windows.Forms.Label labSpeechSpanMinute;
        private System.Windows.Forms.Label labSpeechSpanMinuteUnit;
        private System.Windows.Forms.Label labCallHandlerSpeechFlag;
        private DevComponents.DotNetBar.Controls.SwitchButton swBtnCallHSpeechFlag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}