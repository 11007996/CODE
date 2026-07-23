namespace COMServer
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtBoxMSG = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxTrimSpace = new System.Windows.Forms.CheckBox();
            this.chBoxIsHex = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBoxSend = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.isHexShow = new System.Windows.Forms.CheckBox();
            this.CLSTxtBoxAllReceive = new System.Windows.Forms.Button();
            this.txtBoxAllReceive = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.btnOpenSerialPort = new System.Windows.Forms.Button();
            this.ComboBoxPortNum = new System.Windows.Forms.ComboBox();
            this.comboBoxBondRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LBPortState = new System.Windows.Forms.Label();
            this.comboBoxStopBit = new System.Windows.Forms.ComboBox();
            this.btnCloseSerialPort = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxDateBits = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtBoxMSG);
            this.groupBox3.Location = new System.Drawing.Point(12, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(880, 129);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message";
            // 
            // txtBoxMSG
            // 
            this.txtBoxMSG.Location = new System.Drawing.Point(14, 20);
            this.txtBoxMSG.Multiline = true;
            this.txtBoxMSG.Name = "txtBoxMSG";
            this.txtBoxMSG.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxMSG.Size = new System.Drawing.Size(860, 103);
            this.txtBoxMSG.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxTrimSpace);
            this.groupBox1.Controls.Add(this.chBoxIsHex);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtBoxSend);
            this.groupBox1.Location = new System.Drawing.Point(12, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 142);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send";
            // 
            // checkBoxTrimSpace
            // 
            this.checkBoxTrimSpace.AutoSize = true;
            this.checkBoxTrimSpace.Location = new System.Drawing.Point(151, 120);
            this.checkBoxTrimSpace.Name = "checkBoxTrimSpace";
            this.checkBoxTrimSpace.Size = new System.Drawing.Size(96, 16);
            this.checkBoxTrimSpace.TabIndex = 7;
            this.checkBoxTrimSpace.Text = "去掉所有空格";
            this.checkBoxTrimSpace.UseVisualStyleBackColor = true;
            // 
            // chBoxIsHex
            // 
            this.chBoxIsHex.AutoSize = true;
            this.chBoxIsHex.Location = new System.Drawing.Point(102, 118);
            this.chBoxIsHex.Name = "chBoxIsHex";
            this.chBoxIsHex.Size = new System.Drawing.Size(42, 16);
            this.chBoxIsHex.TabIndex = 6;
            this.chBoxIsHex.Text = "HEX";
            this.chBoxIsHex.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(6, 114);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(151, 199);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "去掉所有空格";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(102, 197);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(42, 16);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "HEX";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtBoxSend
            // 
            this.txtBoxSend.Location = new System.Drawing.Point(6, 20);
            this.txtBoxSend.Multiline = true;
            this.txtBoxSend.Name = "txtBoxSend";
            this.txtBoxSend.Size = new System.Drawing.Size(868, 88);
            this.txtBoxSend.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.isHexShow);
            this.groupBox5.Controls.Add(this.CLSTxtBoxAllReceive);
            this.groupBox5.Controls.Add(this.txtBoxAllReceive);
            this.groupBox5.Location = new System.Drawing.Point(17, 351);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(874, 191);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Receive";
            // 
            // isHexShow
            // 
            this.isHexShow.AutoSize = true;
            this.isHexShow.Location = new System.Drawing.Point(2, 134);
            this.isHexShow.Name = "isHexShow";
            this.isHexShow.Size = new System.Drawing.Size(42, 16);
            this.isHexShow.TabIndex = 2;
            this.isHexShow.Text = "HEX";
            this.isHexShow.UseVisualStyleBackColor = true;
            // 
            // CLSTxtBoxAllReceive
            // 
            this.CLSTxtBoxAllReceive.Location = new System.Drawing.Point(8, 20);
            this.CLSTxtBoxAllReceive.Name = "CLSTxtBoxAllReceive";
            this.CLSTxtBoxAllReceive.Size = new System.Drawing.Size(29, 98);
            this.CLSTxtBoxAllReceive.TabIndex = 1;
            this.CLSTxtBoxAllReceive.Text = "C L S";
            this.CLSTxtBoxAllReceive.UseVisualStyleBackColor = true;
            this.CLSTxtBoxAllReceive.Click += new System.EventHandler(this.CLSTxtBoxAllReceive_Click);
            // 
            // txtBoxAllReceive
            // 
            this.txtBoxAllReceive.Location = new System.Drawing.Point(43, 20);
            this.txtBoxAllReceive.Multiline = true;
            this.txtBoxAllReceive.Name = "txtBoxAllReceive";
            this.txtBoxAllReceive.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxAllReceive.Size = new System.Drawing.Size(825, 165);
            this.txtBoxAllReceive.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxParity);
            this.groupBox2.Controls.Add(this.btnOpenSerialPort);
            this.groupBox2.Controls.Add(this.ComboBoxPortNum);
            this.groupBox2.Controls.Add(this.comboBoxBondRate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.LBPortState);
            this.groupBox2.Controls.Add(this.comboBoxStopBit);
            this.groupBox2.Controls.Add(this.btnCloseSerialPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBoxDateBits);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(888, 49);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "Even",
            "Mark",
            "None",
            "Odd",
            "Space"});
            this.comboBoxParity.Location = new System.Drawing.Point(458, 18);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(59, 20);
            this.comboBoxParity.TabIndex = 11;
            // 
            // btnOpenSerialPort
            // 
            this.btnOpenSerialPort.Location = new System.Drawing.Point(536, 17);
            this.btnOpenSerialPort.Name = "btnOpenSerialPort";
            this.btnOpenSerialPort.Size = new System.Drawing.Size(51, 23);
            this.btnOpenSerialPort.TabIndex = 2;
            this.btnOpenSerialPort.Text = "打开";
            this.btnOpenSerialPort.UseVisualStyleBackColor = true;
            this.btnOpenSerialPort.Click += new System.EventHandler(this.btnOpenSerialPort_Click);
            // 
            // ComboBoxPortNum
            // 
            this.ComboBoxPortNum.FormattingEnabled = true;
            this.ComboBoxPortNum.Location = new System.Drawing.Point(60, 15);
            this.ComboBoxPortNum.Name = "ComboBoxPortNum";
            this.ComboBoxPortNum.Size = new System.Drawing.Size(49, 20);
            this.ComboBoxPortNum.TabIndex = 3;
            // 
            // comboBoxBondRate
            // 
            this.comboBoxBondRate.FormattingEnabled = true;
            this.comboBoxBondRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200"});
            this.comboBoxBondRate.Location = new System.Drawing.Point(257, 17);
            this.comboBoxBondRate.Name = "comboBoxBondRate";
            this.comboBoxBondRate.Size = new System.Drawing.Size(58, 20);
            this.comboBoxBondRate.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "串口号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "波特率:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "状态:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "停止位:";
            // 
            // LBPortState
            // 
            this.LBPortState.AutoSize = true;
            this.LBPortState.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.LBPortState.Location = new System.Drawing.Point(697, 23);
            this.LBPortState.Name = "LBPortState";
            this.LBPortState.Size = new System.Drawing.Size(41, 12);
            this.LBPortState.TabIndex = 6;
            this.LBPortState.Text = "NotNow";
            // 
            // comboBoxStopBit
            // 
            this.comboBoxStopBit.FormattingEnabled = true;
            this.comboBoxStopBit.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.comboBoxStopBit.Location = new System.Drawing.Point(374, 17);
            this.comboBoxStopBit.Name = "comboBoxStopBit";
            this.comboBoxStopBit.Size = new System.Drawing.Size(38, 20);
            this.comboBoxStopBit.TabIndex = 12;
            // 
            // btnCloseSerialPort
            // 
            this.btnCloseSerialPort.Enabled = false;
            this.btnCloseSerialPort.Location = new System.Drawing.Point(593, 17);
            this.btnCloseSerialPort.Name = "btnCloseSerialPort";
            this.btnCloseSerialPort.Size = new System.Drawing.Size(54, 23);
            this.btnCloseSerialPort.TabIndex = 7;
            this.btnCloseSerialPort.Text = "关闭";
            this.btnCloseSerialPort.UseVisualStyleBackColor = true;
            this.btnCloseSerialPort.Click += new System.EventHandler(this.btnCloseSerialPort_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "数据位:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "校验:";
            // 
            // comboBoxDateBits
            // 
            this.comboBoxDateBits.FormattingEnabled = true;
            this.comboBoxDateBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxDateBits.Location = new System.Drawing.Point(160, 17);
            this.comboBoxDateBits.Name = "comboBoxDateBits";
            this.comboBoxDateBits.Size = new System.Drawing.Size(39, 20);
            this.comboBoxDateBits.TabIndex = 9;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 646);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "通用串口测试程序 欢迎你分享知识和我们共同进步，请加入QQ群：5814395  MSN群：group24578@msnzone.cn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxMSG;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBoxSend;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button CLSTxtBoxAllReceive;
        private System.Windows.Forms.TextBox txtBoxAllReceive;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.Button btnOpenSerialPort;
        private System.Windows.Forms.ComboBox ComboBoxPortNum;
        private System.Windows.Forms.ComboBox comboBoxBondRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LBPortState;
        private System.Windows.Forms.ComboBox comboBoxStopBit;
        private System.Windows.Forms.Button btnCloseSerialPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxDateBits;
        private System.Windows.Forms.CheckBox checkBoxTrimSpace;
        private System.Windows.Forms.CheckBox chBoxIsHex;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox isHexShow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
       
    }
}

