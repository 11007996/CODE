namespace SerialPort
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.串口信息 = new DevExpress.XtraEditors.GroupControl();
            this.label81 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.打开串口按钮 = new System.Windows.Forms.Button();
            this.label85 = new System.Windows.Forms.Label();
            this.更新串口按钮 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.button1 = new System.Windows.Forms.Button();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.串口信息)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // 串口信息
            // 
            this.串口信息.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.串口信息.Appearance.Options.UseBackColor = true;
            this.串口信息.AppearanceCaption.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.串口信息.AppearanceCaption.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.串口信息.AppearanceCaption.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.串口信息.AppearanceCaption.Options.UseBackColor = true;
            this.串口信息.AppearanceCaption.Options.UseBorderColor = true;
            this.串口信息.Controls.Add(this.label81);
            this.串口信息.Controls.Add(this.label79);
            this.串口信息.Controls.Add(this.label84);
            this.串口信息.Controls.Add(this.label80);
            this.串口信息.Controls.Add(this.label86);
            this.串口信息.Controls.Add(this.label82);
            this.串口信息.Controls.Add(this.label87);
            this.串口信息.Controls.Add(this.label83);
            this.串口信息.Controls.Add(this.打开串口按钮);
            this.串口信息.Controls.Add(this.label85);
            this.串口信息.Controls.Add(this.更新串口按钮);
            this.串口信息.Controls.Add(this.comboBox1);
            this.串口信息.Controls.Add(this.comboBox2);
            this.串口信息.Controls.Add(this.comboBox3);
            this.串口信息.Controls.Add(this.comboBox4);
            this.串口信息.Controls.Add(this.comboBox5);
            this.串口信息.Location = new System.Drawing.Point(16, 15);
            this.串口信息.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.串口信息.Name = "串口信息";
            this.串口信息.Size = new System.Drawing.Size(261, 330);
            this.串口信息.TabIndex = 1;
            this.串口信息.Text = "串口信息";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.BackColor = System.Drawing.SystemColors.Control;
            this.label81.Location = new System.Drawing.Point(135, 296);
            this.label81.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(26, 18);
            this.label81.TabIndex = 22;
            this.label81.Text = "Tx";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BackColor = System.Drawing.SystemColors.Control;
            this.label79.Location = new System.Drawing.Point(37, 48);
            this.label79.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(68, 18);
            this.label79.TabIndex = 11;
            this.label79.Text = "串口号：";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.BackColor = System.Drawing.SystemColors.Control;
            this.label84.Location = new System.Drawing.Point(61, 296);
            this.label84.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(53, 18);
            this.label84.TabIndex = 21;
            this.label84.Text = "发送：";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.BackColor = System.Drawing.SystemColors.Control;
            this.label80.Location = new System.Drawing.Point(37, 118);
            this.label80.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(68, 18);
            this.label80.TabIndex = 13;
            this.label80.Text = "数据位：";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.BackColor = System.Drawing.SystemColors.Control;
            this.label86.Location = new System.Drawing.Point(135, 275);
            this.label86.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(25, 18);
            this.label86.TabIndex = 20;
            this.label86.Text = "Rx";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.BackColor = System.Drawing.SystemColors.Control;
            this.label82.Location = new System.Drawing.Point(37, 152);
            this.label82.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(68, 18);
            this.label82.TabIndex = 14;
            this.label82.Text = "停止位：";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.BackColor = System.Drawing.SystemColors.Control;
            this.label87.Location = new System.Drawing.Point(61, 275);
            this.label87.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(53, 18);
            this.label87.TabIndex = 19;
            this.label87.Text = "接收：";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.BackColor = System.Drawing.SystemColors.Control;
            this.label83.Location = new System.Drawing.Point(37, 82);
            this.label83.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(68, 18);
            this.label83.TabIndex = 12;
            this.label83.Text = "波特率：";
            // 
            // 打开串口按钮
            // 
            this.打开串口按钮.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打开串口按钮.Location = new System.Drawing.Point(139, 230);
            this.打开串口按钮.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.打开串口按钮.Name = "打开串口按钮";
            this.打开串口按钮.Size = new System.Drawing.Size(100, 29);
            this.打开串口按钮.TabIndex = 18;
            this.打开串口按钮.Text = "打开串口";
            this.打开串口按钮.UseVisualStyleBackColor = true;
            this.打开串口按钮.Click += new System.EventHandler(this.button26_Click);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.BackColor = System.Drawing.SystemColors.Control;
            this.label85.Location = new System.Drawing.Point(37, 188);
            this.label85.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(68, 18);
            this.label85.TabIndex = 15;
            this.label85.Text = "校验位：";
            // 
            // 更新串口按钮
            // 
            this.更新串口按钮.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.更新串口按钮.Location = new System.Drawing.Point(25, 230);
            this.更新串口按钮.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.更新串口按钮.Name = "更新串口按钮";
            this.更新串口按钮.Size = new System.Drawing.Size(100, 29);
            this.更新串口按钮.TabIndex = 17;
            this.更新串口按钮.Text = "更新串口";
            this.更新串口按钮.UseVisualStyleBackColor = true;
            this.更新串口按钮.Click += new System.EventHandler(this.button27_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(115, 44);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 26);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(115, 79);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(101, 26);
            this.comboBox2.TabIndex = 17;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(115, 114);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(101, 26);
            this.comboBox3.TabIndex = 18;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(115, 149);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(101, 26);
            this.comboBox4.TabIndex = 19;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(115, 184);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(101, 26);
            this.comboBox5.TabIndex = 20;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(19, 44);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(445, 76);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(333, 222);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(445, 105);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupControl1.Appearance.Options.UseBorderColor = true;
            this.groupControl1.Controls.Add(this.button1);
            this.groupControl1.Controls.Add(this.richTextBox1);
            this.groupControl1.Location = new System.Drawing.Point(315, 15);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(597, 141);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "发送信息栏";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 71);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupControl2.Appearance.Options.UseBorderColor = true;
            this.groupControl2.Controls.Add(this.button2);
            this.groupControl2.Location = new System.Drawing.Point(315, 182);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(597, 162);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "信息栏";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(485, 78);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 29);
            this.button2.TabIndex = 4;
            this.button2.Text = "清除";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 368);
            this.Controls.Add(this.串口信息);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.groupControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "串口助手（By WX estrom22）";
            ((System.ComponentModel.ISupportInitialize)(this.串口信息)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl 串口信息;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Button 打开串口按钮;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Button 更新串口按钮;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
    }
}

