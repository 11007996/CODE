using System.Windows.Forms;
namespace ComTools.SerialPortService
{
     public partial class FrmSerialDebug : Form
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
            this.labSerialProtocol = new System.Windows.Forms.Label();
            this.labUseProtocol = new System.Windows.Forms.Label();
            this.panelConfig = new System.Windows.Forms.Panel();
            this.cmbCom = new System.Windows.Forms.ComboBox();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.labParity = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.labStopBits = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.labDataBits = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.labBaudRate = new System.Windows.Forms.Label();
            this.labCom = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbData = new System.Windows.Forms.TextBox();
            this.chkShowTime = new System.Windows.Forms.CheckBox();
            this.chkShowSend = new System.Windows.Forms.CheckBox();
            this.labReceivedData = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSendData = new System.Windows.Forms.TextBox();
            this.ckbHex = new System.Windows.Forms.CheckBox();
            this.labSendArea = new System.Windows.Forms.Label();
            this.panelConfig.SuspendLayout();
            this.panelData.SuspendLayout();
            this.SuspendLayout();
            // 
            // labSerialProtocol
            // 
            this.labSerialProtocol.AutoSize = true;
            this.labSerialProtocol.ForeColor = System.Drawing.Color.Red;
            this.labSerialProtocol.Location = new System.Drawing.Point(89, 8);
            this.labSerialProtocol.Name = "labSerialProtocol";
            this.labSerialProtocol.Size = new System.Drawing.Size(99, 20);
            this.labSerialProtocol.TabIndex = 11;
            this.labSerialProtocol.Text = "串口，无协议";
            // 
            // labUseProtocol
            // 
            this.labUseProtocol.AutoSize = true;
            this.labUseProtocol.Location = new System.Drawing.Point(15, 8);
            this.labUseProtocol.Name = "labUseProtocol";
            this.labUseProtocol.Size = new System.Drawing.Size(84, 20);
            this.labUseProtocol.TabIndex = 10;
            this.labUseProtocol.Text = "使用协议：";
            // 
            // panelConfig
            // 
            this.panelConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConfig.Controls.Add(this.cmbCom);
            this.panelConfig.Controls.Add(this.cmbParity);
            this.panelConfig.Controls.Add(this.labParity);
            this.panelConfig.Controls.Add(this.cmbStopBits);
            this.panelConfig.Controls.Add(this.labStopBits);
            this.panelConfig.Controls.Add(this.tbDataBits);
            this.panelConfig.Controls.Add(this.labDataBits);
            this.panelConfig.Controls.Add(this.btnClose);
            this.panelConfig.Controls.Add(this.btnOpen);
            this.panelConfig.Controls.Add(this.tbBaudRate);
            this.panelConfig.Controls.Add(this.labBaudRate);
            this.panelConfig.Controls.Add(this.labCom);
            this.panelConfig.Location = new System.Drawing.Point(15, 32);
            this.panelConfig.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.Size = new System.Drawing.Size(978, 54);
            this.panelConfig.TabIndex = 7;
            // 
            // cmbCom
            // 
            this.cmbCom.FormattingEnabled = true;
            this.cmbCom.Location = new System.Drawing.Point(62, 12);
            this.cmbCom.Name = "cmbCom";
            this.cmbCom.Size = new System.Drawing.Size(84, 28);
            this.cmbCom.TabIndex = 16;
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Location = new System.Drawing.Point(546, 13);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(59, 28);
            this.cmbParity.TabIndex = 15;
            // 
            // labParity
            // 
            this.labParity.AutoSize = true;
            this.labParity.Location = new System.Drawing.Point(498, 17);
            this.labParity.Name = "labParity";
            this.labParity.Size = new System.Drawing.Size(54, 20);
            this.labParity.TabIndex = 14;
            this.labParity.Text = "奇偶：";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.Location = new System.Drawing.Point(430, 14);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(49, 28);
            this.cmbStopBits.TabIndex = 13;
            // 
            // labStopBits
            // 
            this.labStopBits.AutoSize = true;
            this.labStopBits.Location = new System.Drawing.Point(371, 17);
            this.labStopBits.Name = "labStopBits";
            this.labStopBits.Size = new System.Drawing.Size(69, 20);
            this.labStopBits.TabIndex = 12;
            this.labStopBits.Text = "停止位：";
            // 
            // tbDataBits
            // 
            this.tbDataBits.Location = new System.Drawing.Point(334, 14);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(24, 27);
            this.tbDataBits.TabIndex = 11;
            this.tbDataBits.Text = "8";
            // 
            // labDataBits
            // 
            this.labDataBits.AutoSize = true;
            this.labDataBits.Location = new System.Drawing.Point(272, 17);
            this.labDataBits.Name = "labDataBits";
            this.labDataBits.Size = new System.Drawing.Size(69, 20);
            this.labDataBits.TabIndex = 10;
            this.labDataBits.Text = "数据位：";
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(875, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭串口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(778, 11);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(91, 28);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(219, 14);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(47, 27);
            this.tbBaudRate.TabIndex = 3;
            this.tbBaudRate.Text = "9600";
            // 
            // labBaudRate
            // 
            this.labBaudRate.AutoSize = true;
            this.labBaudRate.Location = new System.Drawing.Point(152, 17);
            this.labBaudRate.Name = "labBaudRate";
            this.labBaudRate.Size = new System.Drawing.Size(69, 20);
            this.labBaudRate.TabIndex = 2;
            this.labBaudRate.Text = "波特率：";
            // 
            // labCom
            // 
            this.labCom.AutoSize = true;
            this.labCom.Location = new System.Drawing.Point(8, 17);
            this.labCom.Name = "labCom";
            this.labCom.Size = new System.Drawing.Size(73, 20);
            this.labCom.TabIndex = 0;
            this.labCom.Text = "Com口：";
            // 
            // panelData
            // 
            this.panelData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelData.Controls.Add(this.btnClear);
            this.panelData.Controls.Add(this.tbData);
            this.panelData.Controls.Add(this.chkShowTime);
            this.panelData.Controls.Add(this.chkShowSend);
            this.panelData.Controls.Add(this.labReceivedData);
            this.panelData.Controls.Add(this.btnSend);
            this.panelData.Controls.Add(this.tbSendData);
            this.panelData.Controls.Add(this.ckbHex);
            this.panelData.Controls.Add(this.labSendArea);
            this.panelData.Location = new System.Drawing.Point(15, 93);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(978, 540);
            this.panelData.TabIndex = 13;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(875, 68);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 28);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "清空数据";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbData
            // 
            this.tbData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbData.Location = new System.Drawing.Point(0, 105);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbData.Size = new System.Drawing.Size(976, 433);
            this.tbData.TabIndex = 21;
            // 
            // chkShowTime
            // 
            this.chkShowTime.AutoSize = true;
            this.chkShowTime.Checked = true;
            this.chkShowTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowTime.Location = new System.Drawing.Point(366, 70);
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
            this.chkShowSend.Location = new System.Drawing.Point(146, 70);
            this.chkShowSend.Name = "chkShowSend";
            this.chkShowSend.Size = new System.Drawing.Size(151, 24);
            this.chkShowSend.TabIndex = 19;
            this.chkShowSend.Text = "是否显示发送数据";
            this.chkShowSend.UseVisualStyleBackColor = true;
            // 
            // labReceivedData
            // 
            this.labReceivedData.AutoSize = true;
            this.labReceivedData.Location = new System.Drawing.Point(8, 72);
            this.labReceivedData.Name = "labReceivedData";
            this.labReceivedData.Size = new System.Drawing.Size(99, 20);
            this.labReceivedData.TabIndex = 18;
            this.labReceivedData.Text = "数据接收区：";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(875, 35);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(91, 28);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "发送数据";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbSendData
            // 
            this.tbSendData.Location = new System.Drawing.Point(11, 36);
            this.tbSendData.Name = "tbSendData";
            this.tbSendData.Size = new System.Drawing.Size(858, 27);
            this.tbSendData.TabIndex = 16;
            this.tbSendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSendData_KeyDown);
            // 
            // ckbHex
            // 
            this.ckbHex.AutoSize = true;
            this.ckbHex.Checked = true;
            this.ckbHex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHex.Location = new System.Drawing.Point(145, 8);
            this.ckbHex.Name = "ckbHex";
            this.ckbHex.Size = new System.Drawing.Size(151, 24);
            this.ckbHex.TabIndex = 2;
            this.ckbHex.Text = "是否十六进制通信";
            this.ckbHex.UseVisualStyleBackColor = true;
            // 
            // labSendArea
            // 
            this.labSendArea.AutoSize = true;
            this.labSendArea.Location = new System.Drawing.Point(8, 9);
            this.labSendArea.Name = "labSendArea";
            this.labSendArea.Size = new System.Drawing.Size(99, 20);
            this.labSendArea.TabIndex = 1;
            this.labSendArea.Text = "数据发送区：";
            // 
            // FrmSerialDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1004, 645);
            this.Controls.Add(this.labSerialProtocol);
            this.Controls.Add(this.labUseProtocol);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelConfig);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmSerialDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串口调试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSerialDebug_FormClosed);
            this.Load += new System.EventHandler(this.FrmSerialDebug_Load);
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Label labSerialProtocol;

        private Label labUseProtocol;

        private Panel panelConfig;

        private ComboBox cmbParity;

        private Label labParity;

        private ComboBox cmbStopBits;

        private Label labStopBits;

        private TextBox tbDataBits;

        private Label labDataBits;

        private Button btnClose;

        private Button btnOpen;

        private TextBox tbBaudRate;

        private Label labBaudRate;

        private Label labCom;

        private Panel panelData;

        private TextBox tbData;

        private CheckBox chkShowTime;

        private CheckBox chkShowSend;

        private Label labReceivedData;

        private Button btnSend;

        private TextBox tbSendData;

        private CheckBox ckbHex;

        private Label labSendArea;

        private ComboBox cmbCom;
        private Button btnClear;
    }
}