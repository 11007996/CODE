using Listen.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listen
{
    public partial class FrmMsgDialog : Form
    {
        public FrmMsgDialog(string msg)
        {
            InitializeComponent();
            this.labMsg.Text = msg;
        }

        public FrmMsgDialog(string title, string msg)
        {
            InitializeComponent();
            this.labTitle.Text = title;
            this.labMsg.Text = msg;
        }

        private void FrmMsgDialog_Load(object sender, EventArgs e)
        {
            //设置窗体在屏幕右下角显示
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);
        }


        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
