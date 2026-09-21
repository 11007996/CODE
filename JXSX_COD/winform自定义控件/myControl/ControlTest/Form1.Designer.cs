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
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.userControl11 = new myControl.UserControl1();
            this.lTrackBar1 = new myControl.LTrackBar();
            this.myControlA1 = new myControl.myControlA();
            this.nyCotrolB1 = new myControl.nyCotrolB();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.ColumnCount = 2;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.Location = new System.Drawing.Point(318, 159);
            this.panel1.Name = "panel1";
            this.panel1.RowCount = 2;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panel1.Size = new System.Drawing.Size(207, 90);
            this.panel1.TabIndex = 3;
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(450, 96);
            this.userControl11.mWidth = 0;
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(75, 23);
            this.userControl11.TabIndex = 2;
            this.userControl11.Text = "userControl11";
            this.userControl11.UseVisualStyleBackColor = true;
            this.userControl11.Click += new System.EventHandler(this.userControl11_Click);
            // 
            // lTrackBar1
            // 
            this.lTrackBar1.L_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lTrackBar1.L_BarSize = 15;
            this.lTrackBar1.L_IsRound = true;
            this.lTrackBar1.L_Maxmun = 100;
            this.lTrackBar1.L_Mininum = 0;
            this.lTrackBar1.L_Orientation = myControl.Orientation.Horizontal_LR;
            this.lTrackBar1.L_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lTrackBar1.L_Value = 10;
            this.lTrackBar1.Location = new System.Drawing.Point(354, 55);
            this.lTrackBar1.Name = "lTrackBar1";
            this.lTrackBar1.Size = new System.Drawing.Size(404, 15);
            this.lTrackBar1.TabIndex = 1;
            this.lTrackBar1.Text = "lTrackBar1";
            // 
            // myControlA1
            // 
            this.myControlA1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.myControlA1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myControlA1.Location = new System.Drawing.Point(42, 12);
            this.myControlA1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.myControlA1.Name = "myControlA1";
            this.myControlA1.Size = new System.Drawing.Size(236, 188);
            this.myControlA1.TabIndex = 0;
            // 
            // nyCotrolB1
            // 
            this.nyCotrolB1.Location = new System.Drawing.Point(339, 304);
            this.nyCotrolB1.M_Height = 80;
            this.nyCotrolB1.M_Width = 150;
            this.nyCotrolB1.Name = "nyCotrolB1";
            this.nyCotrolB1.Size = new System.Drawing.Size(313, 83);
            this.nyCotrolB1.TabIndex = 4;
            this.nyCotrolB1.Text = "nyCotrolB1";
            this.nyCotrolB1.Click += new System.EventHandler(this.nyCotrolB1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 467);
            this.Controls.Add(this.nyCotrolB1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.lTrackBar1);
            this.Controls.Add(this.myControlA1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private myControl.myControlA myControlA1;
        private myControl.LTrackBar lTrackBar1;
        private myControl.UserControl1 userControl11;
        private System.Windows.Forms.TableLayoutPanel panel1;
        private myControl.nyCotrolB nyCotrolB1;
    }
}

