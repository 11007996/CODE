namespace ControlTest
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
            this.lTrackBar1 = new myControl.LTrackBar();
            this.SuspendLayout();
            // 
            // lTrackBar1
            // 
            this.lTrackBar1.L_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lTrackBar1.L_BarSize = 10;
            this.lTrackBar1.L_IsRound = true;
            this.lTrackBar1.L_Maxmun = 100;
            this.lTrackBar1.L_Mininum = 0;
            this.lTrackBar1.L_Orientation = myControl.Orientation.Horizontal_LR;
            this.lTrackBar1.L_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lTrackBar1.L_Value = 0;
            this.lTrackBar1.Location = new System.Drawing.Point(93, 121);
            this.lTrackBar1.Name = "lTrackBar1";
            this.lTrackBar1.Size = new System.Drawing.Size(359, 10);
            this.lTrackBar1.TabIndex = 0;
            this.lTrackBar1.Text = "lTrackBar1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 388);
            this.Controls.Add(this.lTrackBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private myControl.LTrackBar lTrackBar1;

    }
}

