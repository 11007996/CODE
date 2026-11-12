using myControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        UserControl1 uc;
        private void userControl11_Click(object sender, EventArgs e)
        {
            this.userControl11.BackColor = Color.Cyan;
            if(uc != null)
            {
                uc.Dispose();
                uc = null;
                this.panel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            }
            else
            {
                uc = new UserControl1();
                uc.Width = 80;
                uc.Height = 30;
                uc.Text = "动态控件";
                uc.Name = "duc";
                Point p = new Point(450, 150);
                uc.Location = p;
                //this.Controls.Add(uc);
                this.panel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                this.panel1.Controls.Add(uc, 0, 1);
            }
        }

        private void nyCotrolB1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("事务弹窗");   //先触发控件click事件，再触发此处主事务事件
        }





        #region 拖动窗体
        /// <summary>
        /// The wm syscommand
        /// </summary>
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary>
        /// The sc move
        /// </summary>
        public const int SC_MOVE = 0xF010;
        /// <summary>
        /// The htcaption
        /// </summary>
        public const int HTCAPTION = 0x0002;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
      
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        //[DllImport("user32.dll")]
        //public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            //MessageBox(this.Handle, "提示", "提示内容", 0);
        }
    }
}
