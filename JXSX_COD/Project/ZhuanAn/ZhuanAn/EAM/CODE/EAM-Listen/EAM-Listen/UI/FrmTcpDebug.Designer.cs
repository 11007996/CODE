using System.Windows.Forms;
namespace EAM.Listen.UI
{
    partial class FrmTcpDebug
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txbLike = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nudClientPort = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.txbClientIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkEnter = new System.Windows.Forms.CheckBox();
            this.tbData = new System.Windows.Forms.TextBox();
            this.chkShowTime = new System.Windows.Forms.CheckBox();
            this.chkShowSend = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txbSendMsg = new System.Windows.Forms.TextBox();
            this.chkHex = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lable5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTimeOut = new System.Windows.Forms.NumericUpDown();
            this.nudListenLimit = new System.Windows.Forms.NumericUpDown();
            this.btnCloseConn = new System.Windows.Forms.Button();
            this.btnOpenConn = new System.Windows.Forms.Button();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.txbIP = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudListenLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txbLike);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.nudClientPort);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txbClientIP);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.chkEnter);
            this.panel2.Controls.Add(this.tbData);
            this.panel2.Controls.Add(this.chkShowTime);
            this.panel2.Controls.Add(this.chkShowSend);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.txbSendMsg);
            this.panel2.Controls.Add(this.chkHex);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(13, 94);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(990, 540);
            this.panel2.TabIndex = 20;
            // 
            // txbLike
            // 
            this.txbLike.Location = new System.Drawing.Point(576, 74);
            this.txbLike.Name = "txbLike";
            this.txbLike.Size = new System.Drawing.Size(291, 27);
            this.txbLike.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(473, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "过滤IP或内容：";
            // 
            // nudClientPort
            // 
            this.nudClientPort.Location = new System.Drawing.Point(762, 7);
            this.nudClientPort.Maximum = new decimal(new int[] {
            65533,
            0,
            0,
            0});
            this.nudClientPort.Name = "nudClientPort";
            this.nudClientPort.Size = new System.Drawing.Size(105, 27);
            this.nudClientPort.TabIndex = 15;
            this.nudClientPort.Value = new decimal(new int[] {
            8888,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(714, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "端口：";
            // 
            // txbClientIP
            // 
            this.txbClientIP.Location = new System.Drawing.Point(576, 7);
            this.txbClientIP.Name = "txbClientIP";
            this.txbClientIP.Size = new System.Drawing.Size(118, 27);
            this.txbClientIP.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "客户端IP：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(875, 71);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 28);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "清空数据";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkEnter
            // 
            this.chkEnter.AutoSize = true;
            this.chkEnter.Location = new System.Drawing.Point(322, 8);
            this.chkEnter.Name = "chkEnter";
            this.chkEnter.Size = new System.Drawing.Size(121, 24);
            this.chkEnter.TabIndex = 24;
            this.chkEnter.Text = "是否带有回车";
            this.chkEnter.UseVisualStyleBackColor = true;
            // 
            // tbData
            // 
            this.tbData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbData.Location = new System.Drawing.Point(0, 145);
            this.tbData.MaxLength = 100;
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(988, 393);
            this.tbData.TabIndex = 21;
            // 
            // chkShowTime
            // 
            this.chkShowTime.AutoSize = true;
            this.chkShowTime.Checked = true;
            this.chkShowTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTime.Location = new System.Drawing.Point(324, 76);
            this.chkShowTime.Name = "chkShowTime";
            this.chkShowTime.Size = new System.Drawing.Size(121, 24);
            this.chkShowTime.TabIndex = 20;
            this.chkShowTime.Text = "是否显示时间";
            this.chkShowTime.UseVisualStyleBackColor = true;
            // 
            // chkShowSend
            // 
            this.chkShowSend.AutoSize = true;
            this.chkShowSend.Checked = true;
            this.chkShowSend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSend.Location = new System.Drawing.Point(151, 76);
            this.chkShowSend.Name = "chkShowSend";
            this.chkShowSend.Size = new System.Drawing.Size(151, 24);
            this.chkShowSend.TabIndex = 19;
            this.chkShowSend.Text = "是否显示发送数据";
            this.chkShowSend.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "数据接收区：";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(875, 40);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 28);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "发送数据";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txbSendMsg
            // 
            this.txbSendMsg.Location = new System.Drawing.Point(12, 43);
            this.txbSendMsg.Name = "txbSendMsg";
            this.txbSendMsg.Size = new System.Drawing.Size(857, 27);
            this.txbSendMsg.TabIndex = 16;
            // 
            // chkHex
            // 
            this.chkHex.AutoSize = true;
            this.chkHex.Checked = true;
            this.chkHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHex.Location = new System.Drawing.Point(151, 8);
            this.chkHex.Name = "chkHex";
            this.chkHex.Size = new System.Drawing.Size(151, 24);
            this.chkHex.TabIndex = 2;
            this.chkHex.Text = "是否十六进制通信";
            this.chkHex.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "数据发送区：";
            // 
            // lable5
            // 
            this.lable5.AutoSize = true;
            this.lable5.ForeColor = System.Drawing.Color.Red;
            this.lable5.Location = new System.Drawing.Point(89, 3);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(56, 20);
            this.lable5.TabIndex = 18;
            this.lable5.Text = "TCP/IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "使用协议：";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudTimeOut);
            this.panel1.Controls.Add(this.nudListenLimit);
            this.panel1.Controls.Add(this.btnCloseConn);
            this.panel1.Controls.Add(this.btnOpenConn);
            this.panel1.Controls.Add(this.nudPort);
            this.panel1.Controls.Add(this.txbIP);
            this.panel1.Location = new System.Drawing.Point(13, 33);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 54);
            this.panel1.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(555, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "接收超时(秒)：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(359, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = " 连接限制：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // nudTimeOut
            // 
            this.nudTimeOut.Location = new System.Drawing.Point(657, 14);
            this.nudTimeOut.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudTimeOut.Name = "nudTimeOut";
            this.nudTimeOut.Size = new System.Drawing.Size(90, 27);
            this.nudTimeOut.TabIndex = 12;
            // 
            // nudListenLimit
            // 
            this.nudListenLimit.Location = new System.Drawing.Point(442, 14);
            this.nudListenLimit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudListenLimit.Name = "nudListenLimit";
            this.nudListenLimit.Size = new System.Drawing.Size(90, 27);
            this.nudListenLimit.TabIndex = 11;
            // 
            // btnCloseConn
            // 
            this.btnCloseConn.Enabled = false;
            this.btnCloseConn.Location = new System.Drawing.Point(871, 13);
            this.btnCloseConn.Name = "btnCloseConn";
            this.btnCloseConn.Size = new System.Drawing.Size(91, 28);
            this.btnCloseConn.TabIndex = 5;
            this.btnCloseConn.Text = "关闭监听";
            this.btnCloseConn.UseVisualStyleBackColor = true;
            this.btnCloseConn.Click += new System.EventHandler(this.btnCloseConn_Click);
            // 
            // btnOpenConn
            // 
            this.btnOpenConn.Location = new System.Drawing.Point(771, 13);
            this.btnOpenConn.Name = "btnOpenConn";
            this.btnOpenConn.Size = new System.Drawing.Size(91, 28);
            this.btnOpenConn.TabIndex = 4;
            this.btnOpenConn.Text = "打开监听";
            this.btnOpenConn.UseVisualStyleBackColor = true;
            this.btnOpenConn.Click += new System.EventHandler(this.btnOpenConn_Click);
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(221, 14);
            this.nudPort.Maximum = new decimal(new int[] {
            65533,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(105, 27);
            this.nudPort.TabIndex = 3;
            this.nudPort.Value = new decimal(new int[] {
            10409,
            0,
            0,
            0});
            // 
            // txbIP
            // 
            this.txbIP.Location = new System.Drawing.Point(34, 14);
            this.txbIP.Name = "txbIP";
            this.txbIP.Size = new System.Drawing.Size(118, 27);
            this.txbIP.TabIndex = 1;
            this.txbIP.Text = "0.0.0.0";
            // 
            // FrmTcpDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 642);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lable5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTcpDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP调试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTcpServer_FormClosing);
            this.Load += new System.EventHandler(this.FrmTcpDebug_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClientPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudListenLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel panel2;
        private TextBox tbData;
        private CheckBox chkShowTime;
        private CheckBox chkShowSend;
        private Label label7;
        private Button btnSend;
        private TextBox txbSendMsg;
        private CheckBox chkHex;
        private Label label6;
        private Label lable5;
        private Label label4;
        private Panel panel1;
        private Button btnCloseConn;
        private Button btnOpenConn;
        private NumericUpDown nudPort;
        private Label label3;
        private TextBox txbIP;
        private Label label1;
        private CheckBox chkEnter;
        private Button btnClear;
        private Label label2;
        private Label label9;
        private Label label8;
        private NumericUpDown nudTimeOut;
        private NumericUpDown nudListenLimit;
        private Label label10;
        private NumericUpDown nudClientPort;
        private TextBox txbClientIP;
        private TextBox txbLike;
        private Label label5;
    }
}