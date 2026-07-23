namespace CallSys
{
    partial class FrmMaster
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaster));
            this.panelCall = new System.Windows.Forms.Panel();
            this.labMessage = new System.Windows.Forms.Label();
            this.labHandler = new System.Windows.Forms.Label();
            this.labHandlerTitle = new System.Windows.Forms.Label();
            this.pbHandlerPic = new System.Windows.Forms.PictureBox();
            this.labErrorReason = new System.Windows.Forms.Label();
            this.gbTime = new System.Windows.Forms.GroupBox();
            this.labFinishTime = new System.Windows.Forms.Label();
            this.labComeTime = new System.Windows.Forms.Label();
            this.labLimitTimes = new System.Windows.Forms.Label();
            this.labRequestTime = new System.Windows.Forms.Label();
            this.labLimitTimesTitle = new System.Windows.Forms.Label();
            this.labRequestTimeTitle = new System.Windows.Forms.Label();
            this.labFinishTimeTitle = new System.Windows.Forms.Label();
            this.labComeTimeTitle = new System.Windows.Forms.Label();
            this.labComeTimes = new System.Windows.Forms.Label();
            this.labComeTimesTitle = new System.Windows.Forms.Label();
            this.labHandleTimes = new System.Windows.Forms.Label();
            this.labHandleTimesTitle = new System.Windows.Forms.Label();
            this.cmbCallReason = new System.Windows.Forms.ComboBox();
            this.btnCallHelper = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCallHandler = new System.Windows.Forms.Button();
            this.labHandlerLeave = new System.Windows.Forms.Label();
            this.gbMachine = new System.Windows.Forms.GroupBox();
            this.lsbMachine = new System.Windows.Forms.ListBox();
            this.timerRefreshErrorInfo = new System.Windows.Forms.Timer(this.components);
            this.panelCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHandlerPic)).BeginInit();
            this.gbTime.SuspendLayout();
            this.gbMachine.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCall
            // 
            this.panelCall.BackColor = System.Drawing.Color.Transparent;
            this.panelCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelCall.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCall.Controls.Add(this.labMessage);
            this.panelCall.Controls.Add(this.labHandler);
            this.panelCall.Controls.Add(this.labHandlerTitle);
            this.panelCall.Controls.Add(this.pbHandlerPic);
            this.panelCall.Controls.Add(this.labErrorReason);
            this.panelCall.Controls.Add(this.gbTime);
            this.panelCall.Controls.Add(this.cmbCallReason);
            this.panelCall.Controls.Add(this.btnCallHelper);
            this.panelCall.Controls.Add(this.btnFinish);
            this.panelCall.Controls.Add(this.btnCallHandler);
            this.panelCall.Controls.Add(this.labHandlerLeave);
            this.panelCall.Location = new System.Drawing.Point(529, 16);
            this.panelCall.Margin = new System.Windows.Forms.Padding(4);
            this.panelCall.Name = "panelCall";
            this.panelCall.Size = new System.Drawing.Size(447, 456);
            this.panelCall.TabIndex = 25;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.BackColor = System.Drawing.Color.Transparent;
            this.labMessage.ForeColor = System.Drawing.Color.Red;
            this.labMessage.Location = new System.Drawing.Point(20, 418);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(45, 15);
            this.labMessage.TabIndex = 32;
            this.labMessage.Text = "提示:";
            // 
            // labHandler
            // 
            this.labHandler.AutoSize = true;
            this.labHandler.BackColor = System.Drawing.Color.Transparent;
            this.labHandler.Location = new System.Drawing.Point(341, 230);
            this.labHandler.Name = "labHandler";
            this.labHandler.Size = new System.Drawing.Size(0, 15);
            this.labHandler.TabIndex = 31;
            // 
            // labHandlerTitle
            // 
            this.labHandlerTitle.AutoSize = true;
            this.labHandlerTitle.BackColor = System.Drawing.Color.Transparent;
            this.labHandlerTitle.Location = new System.Drawing.Point(267, 242);
            this.labHandlerTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerTitle.Name = "labHandlerTitle";
            this.labHandlerTitle.Size = new System.Drawing.Size(67, 15);
            this.labHandlerTitle.TabIndex = 28;
            this.labHandlerTitle.Text = "处理者：";
            // 
            // pbHandlerPic
            // 
            this.pbHandlerPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pbHandlerPic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbHandlerPic.BackgroundImage")));
            this.pbHandlerPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHandlerPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbHandlerPic.Location = new System.Drawing.Point(262, 60);
            this.pbHandlerPic.Margin = new System.Windows.Forms.Padding(4);
            this.pbHandlerPic.Name = "pbHandlerPic";
            this.pbHandlerPic.Size = new System.Drawing.Size(160, 166);
            this.pbHandlerPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHandlerPic.TabIndex = 27;
            this.pbHandlerPic.TabStop = false;
            // 
            // labErrorReason
            // 
            this.labErrorReason.AutoSize = true;
            this.labErrorReason.BackColor = System.Drawing.Color.Transparent;
            this.labErrorReason.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labErrorReason.Location = new System.Drawing.Point(18, 56);
            this.labErrorReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labErrorReason.Name = "labErrorReason";
            this.labErrorReason.Size = new System.Drawing.Size(85, 19);
            this.labErrorReason.TabIndex = 17;
            this.labErrorReason.Text = "呼叫原因";
            // 
            // gbTime
            // 
            this.gbTime.BackColor = System.Drawing.Color.Transparent;
            this.gbTime.Controls.Add(this.labFinishTime);
            this.gbTime.Controls.Add(this.labComeTime);
            this.gbTime.Controls.Add(this.labLimitTimes);
            this.gbTime.Controls.Add(this.labRequestTime);
            this.gbTime.Controls.Add(this.labLimitTimesTitle);
            this.gbTime.Controls.Add(this.labRequestTimeTitle);
            this.gbTime.Controls.Add(this.labFinishTimeTitle);
            this.gbTime.Controls.Add(this.labComeTimeTitle);
            this.gbTime.Controls.Add(this.labComeTimes);
            this.gbTime.Controls.Add(this.labComeTimesTitle);
            this.gbTime.Controls.Add(this.labHandleTimes);
            this.gbTime.Controls.Add(this.labHandleTimesTitle);
            this.gbTime.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbTime.Location = new System.Drawing.Point(11, 133);
            this.gbTime.Margin = new System.Windows.Forms.Padding(4);
            this.gbTime.Name = "gbTime";
            this.gbTime.Padding = new System.Windows.Forms.Padding(4);
            this.gbTime.Size = new System.Drawing.Size(221, 253);
            this.gbTime.TabIndex = 22;
            this.gbTime.TabStop = false;
            this.gbTime.Text = "时间";
            // 
            // labFinishTime
            // 
            this.labFinishTime.AutoSize = true;
            this.labFinishTime.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFinishTime.Location = new System.Drawing.Point(89, 88);
            this.labFinishTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFinishTime.Name = "labFinishTime";
            this.labFinishTime.Size = new System.Drawing.Size(63, 14);
            this.labFinishTime.TabIndex = 13;
            this.labFinishTime.Text = "00:00:00";
            // 
            // labComeTime
            // 
            this.labComeTime.AutoSize = true;
            this.labComeTime.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComeTime.Location = new System.Drawing.Point(89, 54);
            this.labComeTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTime.Name = "labComeTime";
            this.labComeTime.Size = new System.Drawing.Size(63, 14);
            this.labComeTime.TabIndex = 12;
            this.labComeTime.Text = "00:00:00";
            // 
            // labLimitTimes
            // 
            this.labLimitTimes.AutoSize = true;
            this.labLimitTimes.BackColor = System.Drawing.Color.Transparent;
            this.labLimitTimes.Font = new System.Drawing.Font("宋体", 10F);
            this.labLimitTimes.Location = new System.Drawing.Point(99, 219);
            this.labLimitTimes.Name = "labLimitTimes";
            this.labLimitTimes.Size = new System.Drawing.Size(60, 17);
            this.labLimitTimes.TabIndex = 30;
            this.labLimitTimes.Text = "00分钟";
            // 
            // labRequestTime
            // 
            this.labRequestTime.AutoSize = true;
            this.labRequestTime.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRequestTime.Location = new System.Drawing.Point(89, 19);
            this.labRequestTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labRequestTime.Name = "labRequestTime";
            this.labRequestTime.Size = new System.Drawing.Size(63, 14);
            this.labRequestTime.TabIndex = 11;
            this.labRequestTime.Text = "00:00:00";
            // 
            // labLimitTimesTitle
            // 
            this.labLimitTimesTitle.AutoSize = true;
            this.labLimitTimesTitle.BackColor = System.Drawing.Color.Transparent;
            this.labLimitTimesTitle.Location = new System.Drawing.Point(9, 220);
            this.labLimitTimesTitle.Name = "labLimitTimesTitle";
            this.labLimitTimesTitle.Size = new System.Drawing.Size(77, 14);
            this.labLimitTimesTitle.TabIndex = 29;
            this.labLimitTimesTitle.Text = "限定时长：";
            // 
            // labRequestTimeTitle
            // 
            this.labRequestTimeTitle.AutoSize = true;
            this.labRequestTimeTitle.Location = new System.Drawing.Point(9, 21);
            this.labRequestTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labRequestTimeTitle.Name = "labRequestTimeTitle";
            this.labRequestTimeTitle.Size = new System.Drawing.Size(77, 14);
            this.labRequestTimeTitle.TabIndex = 8;
            this.labRequestTimeTitle.Text = "求助时间：";
            // 
            // labFinishTimeTitle
            // 
            this.labFinishTimeTitle.AutoSize = true;
            this.labFinishTimeTitle.Location = new System.Drawing.Point(9, 90);
            this.labFinishTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labFinishTimeTitle.Name = "labFinishTimeTitle";
            this.labFinishTimeTitle.Size = new System.Drawing.Size(77, 14);
            this.labFinishTimeTitle.TabIndex = 10;
            this.labFinishTimeTitle.Text = "完成时间：";
            // 
            // labComeTimeTitle
            // 
            this.labComeTimeTitle.AutoSize = true;
            this.labComeTimeTitle.Location = new System.Drawing.Point(8, 56);
            this.labComeTimeTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimeTitle.Name = "labComeTimeTitle";
            this.labComeTimeTitle.Size = new System.Drawing.Size(77, 14);
            this.labComeTimeTitle.TabIndex = 9;
            this.labComeTimeTitle.Text = "到场时间：";
            // 
            // labComeTimes
            // 
            this.labComeTimes.AutoSize = true;
            this.labComeTimes.BackColor = System.Drawing.Color.Transparent;
            this.labComeTimes.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labComeTimes.Location = new System.Drawing.Point(99, 161);
            this.labComeTimes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimes.Name = "labComeTimes";
            this.labComeTimes.Size = new System.Drawing.Size(60, 17);
            this.labComeTimes.TabIndex = 25;
            this.labComeTimes.Text = "00分钟";
            // 
            // labComeTimesTitle
            // 
            this.labComeTimesTitle.AutoSize = true;
            this.labComeTimesTitle.BackColor = System.Drawing.Color.Transparent;
            this.labComeTimesTitle.Location = new System.Drawing.Point(9, 162);
            this.labComeTimesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labComeTimesTitle.Name = "labComeTimesTitle";
            this.labComeTimesTitle.Size = new System.Drawing.Size(77, 14);
            this.labComeTimesTitle.TabIndex = 23;
            this.labComeTimesTitle.Text = "到场分钟：";
            // 
            // labHandleTimes
            // 
            this.labHandleTimes.AutoSize = true;
            this.labHandleTimes.BackColor = System.Drawing.Color.Transparent;
            this.labHandleTimes.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labHandleTimes.Location = new System.Drawing.Point(99, 190);
            this.labHandleTimes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandleTimes.Name = "labHandleTimes";
            this.labHandleTimes.Size = new System.Drawing.Size(60, 17);
            this.labHandleTimes.TabIndex = 25;
            this.labHandleTimes.Text = "00分钟";
            // 
            // labHandleTimesTitle
            // 
            this.labHandleTimesTitle.AutoSize = true;
            this.labHandleTimesTitle.BackColor = System.Drawing.Color.Transparent;
            this.labHandleTimesTitle.Location = new System.Drawing.Point(9, 191);
            this.labHandleTimesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandleTimesTitle.Name = "labHandleTimesTitle";
            this.labHandleTimesTitle.Size = new System.Drawing.Size(77, 14);
            this.labHandleTimesTitle.TabIndex = 24;
            this.labHandleTimesTitle.Text = "处理分钟：";
            // 
            // cmbCallReason
            // 
            this.cmbCallReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCallReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCallReason.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbCallReason.FormattingEnabled = true;
            this.cmbCallReason.Items.AddRange(new object[] {
            "机台故障",
            "换线"});
            this.cmbCallReason.Location = new System.Drawing.Point(22, 79);
            this.cmbCallReason.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCallReason.Name = "cmbCallReason";
            this.cmbCallReason.Size = new System.Drawing.Size(160, 28);
            this.cmbCallReason.TabIndex = 18;
            this.cmbCallReason.SelectedIndexChanged += new System.EventHandler(this.cmbCallReason_SelectedIndexChanged);
            // 
            // btnCallHelper
            // 
            this.btnCallHelper.BackColor = System.Drawing.Color.Orange;
            this.btnCallHelper.Enabled = false;
            this.btnCallHelper.FlatAppearance.BorderSize = 0;
            this.btnCallHelper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCallHelper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallHelper.ForeColor = System.Drawing.Color.White;
            this.btnCallHelper.Location = new System.Drawing.Point(153, 4);
            this.btnCallHelper.Margin = new System.Windows.Forms.Padding(4);
            this.btnCallHelper.Name = "btnCallHelper";
            this.btnCallHelper.Size = new System.Drawing.Size(135, 34);
            this.btnCallHelper.TabIndex = 21;
            this.btnCallHelper.Text = "支援";
            this.btnCallHelper.UseVisualStyleBackColor = false;
            this.btnCallHelper.Click += new System.EventHandler(this.btnCallHelper_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.Color.Green;
            this.btnFinish.Enabled = false;
            this.btnFinish.FlatAppearance.BorderSize = 0;
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinish.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFinish.ForeColor = System.Drawing.Color.White;
            this.btnFinish.Location = new System.Drawing.Point(295, 4);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(135, 34);
            this.btnFinish.TabIndex = 16;
            this.btnFinish.Text = "完成";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnCallHandler
            // 
            this.btnCallHandler.BackColor = System.Drawing.Color.Red;
            this.btnCallHandler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCallHandler.FlatAppearance.BorderSize = 0;
            this.btnCallHandler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCallHandler.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallHandler.ForeColor = System.Drawing.Color.White;
            this.btnCallHandler.Location = new System.Drawing.Point(11, 4);
            this.btnCallHandler.Margin = new System.Windows.Forms.Padding(4);
            this.btnCallHandler.Name = "btnCallHandler";
            this.btnCallHandler.Size = new System.Drawing.Size(135, 34);
            this.btnCallHandler.TabIndex = 15;
            this.btnCallHandler.Text = "呼叫";
            this.btnCallHandler.UseVisualStyleBackColor = false;
            this.btnCallHandler.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // labHandlerLeave
            // 
            this.labHandlerLeave.BackColor = System.Drawing.Color.White;
            this.labHandlerLeave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labHandlerLeave.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labHandlerLeave.Location = new System.Drawing.Point(265, 275);
            this.labHandlerLeave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labHandlerLeave.Name = "labHandlerLeave";
            this.labHandlerLeave.Size = new System.Drawing.Size(160, 34);
            this.labHandlerLeave.TabIndex = 14;
            this.labHandlerLeave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbMachine
            // 
            this.gbMachine.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gbMachine.BackgroundImage = global::CallSys.Properties.Resources.menu_bg;
            this.gbMachine.Controls.Add(this.lsbMachine);
            this.gbMachine.Font = new System.Drawing.Font("宋体", 12F);
            this.gbMachine.Location = new System.Drawing.Point(26, 16);
            this.gbMachine.Name = "gbMachine";
            this.gbMachine.Size = new System.Drawing.Size(474, 456);
            this.gbMachine.TabIndex = 29;
            this.gbMachine.TabStop = false;
            this.gbMachine.Text = "【】机台模组";
            // 
            // lsbMachine
            // 
            this.lsbMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbMachine.Font = new System.Drawing.Font("宋体", 17F);
            this.lsbMachine.FormattingEnabled = true;
            this.lsbMachine.ItemHeight = 28;
            this.lsbMachine.Location = new System.Drawing.Point(3, 26);
            this.lsbMachine.Name = "lsbMachine";
            this.lsbMachine.Size = new System.Drawing.Size(468, 427);
            this.lsbMachine.TabIndex = 26;
            // 
            // timerRefreshErrorInfo
            // 
            this.timerRefreshErrorInfo.Interval = 30000;
            this.timerRefreshErrorInfo.Tick += new System.EventHandler(this.timerRefreshErrorInfo_Tick);
            // 
            // FrmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(999, 482);
            this.Controls.Add(this.gbMachine);
            this.Controls.Add(this.panelCall);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程求助";
            this.Activated += new System.EventHandler(this.FrmMaster_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMaster_FormClosing);
            this.Load += new System.EventHandler(this.FrmMaster_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmMaster_MouseMove);
            this.panelCall.ResumeLayout(false);
            this.panelCall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHandlerPic)).EndInit();
            this.gbTime.ResumeLayout(false);
            this.gbTime.PerformLayout();
            this.gbMachine.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelCall;
        private System.Windows.Forms.Label labHandleTimesTitle;
        private System.Windows.Forms.Label labComeTimesTitle;
        private System.Windows.Forms.GroupBox gbTime;
        private System.Windows.Forms.Label labFinishTime;
        private System.Windows.Forms.Label labComeTime;
        private System.Windows.Forms.Label labRequestTime;
        private System.Windows.Forms.Label labRequestTimeTitle;
        private System.Windows.Forms.Label labFinishTimeTitle;
        private System.Windows.Forms.Label labComeTimeTitle;
        private System.Windows.Forms.Button btnCallHelper;
        private System.Windows.Forms.ComboBox cmbCallReason;
        private System.Windows.Forms.Label labErrorReason;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnCallHandler;
        private System.Windows.Forms.Label labHandlerLeave;
        private System.Windows.Forms.Label labHandleTimes;
        private System.Windows.Forms.Label labComeTimes;
        private System.Windows.Forms.Label labHandlerTitle;
        private System.Windows.Forms.PictureBox pbHandlerPic;
        private System.Windows.Forms.GroupBox gbMachine;
        private System.Windows.Forms.ListBox lsbMachine;
        private System.Windows.Forms.Label labLimitTimes;
        private System.Windows.Forms.Label labLimitTimesTitle;
        private System.Windows.Forms.Label labHandler;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Timer timerRefreshErrorInfo;
    }
}