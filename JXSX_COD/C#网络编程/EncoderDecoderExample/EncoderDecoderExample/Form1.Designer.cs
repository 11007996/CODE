namespace EncoderDecoderExample
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOldText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEncoder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDecoder = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码类型";
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(85, 24);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(303, 23);
            this.comboBoxType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "编码前";
            // 
            // textBoxOldText
            // 
            this.textBoxOldText.Location = new System.Drawing.Point(85, 64);
            this.textBoxOldText.Multiline = true;
            this.textBoxOldText.Name = "textBoxOldText";
            this.textBoxOldText.Size = new System.Drawing.Size(303, 90);
            this.textBoxOldText.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "编码后";
            // 
            // textBoxEncoder
            // 
            this.textBoxEncoder.Location = new System.Drawing.Point(85, 160);
            this.textBoxEncoder.Multiline = true;
            this.textBoxEncoder.Name = "textBoxEncoder";
            this.textBoxEncoder.Size = new System.Drawing.Size(303, 90);
            this.textBoxEncoder.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "解码后";
            // 
            // textBoxDecoder
            // 
            this.textBoxDecoder.Location = new System.Drawing.Point(85, 280);
            this.textBoxDecoder.Multiline = true;
            this.textBoxDecoder.Name = "textBoxDecoder";
            this.textBoxDecoder.Size = new System.Drawing.Size(303, 129);
            this.textBoxDecoder.TabIndex = 2;
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(151, 433);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(136, 39);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "编码\\解码";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 495);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.textBoxDecoder);
            this.Controls.Add(this.textBoxEncoder);
            this.Controls.Add(this.textBoxOldText);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOldText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEncoder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDecoder;
        private System.Windows.Forms.Button buttonRun;
    }
}

