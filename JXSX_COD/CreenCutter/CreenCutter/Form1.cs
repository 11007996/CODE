using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreenCutter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void btn_Scre_Click(object sender, EventArgs e)
        //{
        //    // 新建一个和屏幕大小相同的图片
        //    Bitmap CatchBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);

        //    // 创建一个画板，让我们可以在画板上画图
        //    // 这个画板也就是和屏幕大小一样大的图片
        //    // 我们可以通过Graphics这个类在这个空白图片上画图
        //    Graphics g = Graphics.FromImage(CatchBmp);

        //    // 把屏幕图片拷贝到我们创建的空白图片 CatchBmp中
        //    g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));

        //    // 创建截图窗体
        //    cutter cutter = new cutter();

        //    // 指示窗体的背景图片为屏幕图片
        //    cutter.BackgroundImage = CatchBmp;
        //    // 显示窗体
        //    //cutter.Show();
        //    // 如果Cutter窗体结束，则从剪切板获得截取的图片，并显示在聊天窗体的发送框中
        //    if (cutter.ShowDialog() == DialogResult.OK)
        //    {
        //        IDataObject iData = Clipboard.GetDataObject();

        //        if (iData.GetDataPresent(DataFormats.Bitmap))
        //        {
        //            richTextBox1.Paste();

        //            // 清楚剪贴板的图片
        //            Clipboard.Clear();
        //        }
        //    }
        //}

        private void btn_Scre_Click_1(object sender, EventArgs e)
        {
            // 新建一个和屏幕大小相同的图片
            Bitmap CatchBmp = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);

            // 创建一个画板，让我们可以在画板上画图
            // 这个画板也就是和屏幕大小一样大的图片
            // 我们可以通过Graphics这个类在这个空白图片上画图
            Graphics g = Graphics.FromImage(CatchBmp);

            // 把屏幕图片拷贝到我们创建的空白图片 CatchBmp中
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));

            // 创建截图窗体
            cutter cutter = new cutter();

            // 指示窗体的背景图片为屏幕图片
            cutter.BackgroundImage = CatchBmp;
            // 显示窗体
            //cutter.Show();
            // 如果Cutter窗体结束，则从剪切板获得截取的图片，并显示在聊天窗体的发送框中
            if (cutter.ShowDialog() == DialogResult.OK)
            {
                IDataObject iData = Clipboard.GetDataObject();

                if (iData.GetDataPresent(DataFormats.Bitmap))
                {
                    richTextBox1.Paste();

                    // 清楚剪贴板的图片
                    Clipboard.Clear();
                }
            }
        }
    }
}
