
namespace ComTools
{
    partial class FrmPlatform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlatform));
            this.btnDiskSpeed = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAgingTest = new System.Windows.Forms.Button();
            this.btnSerialPortDebug = new System.Windows.Forms.Button();
            this.btnDiskCapacity = new System.Windows.Forms.Button();
            this.btnWindowWatch = new System.Windows.Forms.Button();
            this.btnDPLineTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDiskSpeed
            // 
            this.btnDiskSpeed.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnDiskSpeed.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDiskSpeed.ForeColor = System.Drawing.Color.White;
            this.btnDiskSpeed.Location = new System.Drawing.Point(3, 3);
            this.btnDiskSpeed.Name = "btnDiskSpeed";
            this.btnDiskSpeed.Size = new System.Drawing.Size(239, 44);
            this.btnDiskSpeed.TabIndex = 0;
            this.btnDiskSpeed.Text = "磁盘测速";
            this.btnDiskSpeed.UseVisualStyleBackColor = false;
            this.btnDiskSpeed.Click += new System.EventHandler(this.btnSiskSpeed_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnAgingTest, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnSerialPortDebug, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnDiskCapacity, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDiskSpeed, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnWindowWatch, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDPLineTest, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 326);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnAgingTest
            // 
            this.btnAgingTest.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnAgingTest.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAgingTest.ForeColor = System.Drawing.Color.White;
            this.btnAgingTest.Location = new System.Drawing.Point(3, 253);
            this.btnAgingTest.Name = "btnAgingTest";
            this.btnAgingTest.Size = new System.Drawing.Size(239, 44);
            this.btnAgingTest.TabIndex = 4;
            this.btnAgingTest.Text = "老化测试";
            this.btnAgingTest.UseVisualStyleBackColor = false;
            this.btnAgingTest.Click += new System.EventHandler(this.btnAgingTest_Click);
            // 
            // btnSerialPortDebug
            // 
            this.btnSerialPortDebug.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnSerialPortDebug.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSerialPortDebug.ForeColor = System.Drawing.Color.White;
            this.btnSerialPortDebug.Location = new System.Drawing.Point(3, 203);
            this.btnSerialPortDebug.Name = "btnSerialPortDebug";
            this.btnSerialPortDebug.Size = new System.Drawing.Size(239, 44);
            this.btnSerialPortDebug.TabIndex = 0;
            this.btnSerialPortDebug.Text = "串口调试";
            this.btnSerialPortDebug.UseVisualStyleBackColor = false;
            this.btnSerialPortDebug.Click += new System.EventHandler(this.btnSerialPortDebug_Click);
            // 
            // btnDiskCapacity
            // 
            this.btnDiskCapacity.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnDiskCapacity.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDiskCapacity.ForeColor = System.Drawing.Color.White;
            this.btnDiskCapacity.Location = new System.Drawing.Point(3, 53);
            this.btnDiskCapacity.Name = "btnDiskCapacity";
            this.btnDiskCapacity.Size = new System.Drawing.Size(239, 44);
            this.btnDiskCapacity.TabIndex = 1;
            this.btnDiskCapacity.Text = "磁盘容量";
            this.btnDiskCapacity.UseVisualStyleBackColor = false;
            this.btnDiskCapacity.Click += new System.EventHandler(this.btnDiskCapacity_Click);
            // 
            // btnWindowWatch
            // 
            this.btnWindowWatch.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnWindowWatch.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWindowWatch.ForeColor = System.Drawing.Color.White;
            this.btnWindowWatch.Location = new System.Drawing.Point(3, 103);
            this.btnWindowWatch.Name = "btnWindowWatch";
            this.btnWindowWatch.Size = new System.Drawing.Size(239, 44);
            this.btnWindowWatch.TabIndex = 2;
            this.btnWindowWatch.Text = "窗口自动化";
            this.btnWindowWatch.UseVisualStyleBackColor = false;
            this.btnWindowWatch.Click += new System.EventHandler(this.btnWindowWatch_Click);
            // 
            // btnDPLineTest
            // 
            this.btnDPLineTest.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnDPLineTest.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDPLineTest.ForeColor = System.Drawing.Color.White;
            this.btnDPLineTest.Location = new System.Drawing.Point(3, 153);
            this.btnDPLineTest.Name = "btnDPLineTest";
            this.btnDPLineTest.Size = new System.Drawing.Size(239, 40);
            this.btnDPLineTest.TabIndex = 3;
            this.btnDPLineTest.Text = "DP线测试";
            this.btnDPLineTest.UseVisualStyleBackColor = false;
            this.btnDPLineTest.Click += new System.EventHandler(this.btnDPLineTest_Click);
            // 
            // FrmPlatform
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(266, 346);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPlatform";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "测试工具";
            this.Load += new System.EventHandler(this.FrmPlatform_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDiskSpeed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnDiskCapacity;
        private System.Windows.Forms.Button btnWindowWatch;
        private System.Windows.Forms.Button btnDPLineTest;
        private System.Windows.Forms.Button btnSerialPortDebug;
        private System.Windows.Forms.Button btnAgingTest;
    }
}