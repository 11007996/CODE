using ComTools.AgingTest;
using ComTools.Automation;
using ComTools.DPLineTest;
using ComTools.SerialPortService;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ComTools
{
    public partial class FrmPlatform : Form
    {
        public FrmPlatform()
        {
            InitializeComponent();
        }

        private void FrmPlatform_Load(object sender, EventArgs e)
        {
            // 设置启动位置在屏幕右下边缘
            this.StartPosition = FormStartPosition.Manual;
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width, workingArea.Bottom - this.Height);
            this.Text = $"测试工具 - {Application.ProductVersion}";
        }

        private void btnSiskSpeed_Click(object sender, EventArgs e)
        {
            FrmDiskSpeed frm = new FrmDiskSpeed();
            frm.Show();
        }

        private void btnDiskCapacity_Click(object sender, EventArgs e)
        {
            FrmDiskCapacity frm = new FrmDiskCapacity();
            frm.Show();
        }

        private void btnWindowWatch_Click(object sender, EventArgs e)
        {
            FrmAutomationTest frm = new FrmAutomationTest();
            frm.Show();
        }

        private void btnDPLineTest_Click(object sender, EventArgs e)
        {
            FrmDPLineTest frm = new FrmDPLineTest();
            frm.Show();
        }

        private void btnSerialPortDebug_Click(object sender, EventArgs e)
        {
            FrmSerialDebug frm = new FrmSerialDebug();
            frm.Show();
        }

        private void btnAgingTest_Click(object sender, EventArgs e)
        {
            FrmAgingTest frm = new FrmAgingTest();
            frm.Show();
        }
    }
}