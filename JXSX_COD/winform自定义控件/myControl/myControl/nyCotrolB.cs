using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace myControl
{
    public class nyCotrolB:Control
    {
        private int _mWidth = 150;
        [Description("宽度"), Category("自定义")]
        public int M_Width 
        {
            get { return _mWidth; }
            set
            {
                _mWidth = value;
                this.Width = _mWidth;
                Invalidate();
            }
        }

        private int _mHeight = 80;
        [Description("高度"),Category("自定义")]
        public int M_Height
        {
            get { return _mHeight; }
            set
            {
                    _mHeight = value;
                    this.Height = _mHeight;
                    Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            Pen p = new Pen(Color.Red, 2);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);    //控件定位，控件的左上角为(0,0)
            gh.DrawRectangle(p, rect);

            p = new Pen(Color.Red,20);
            Point p1 = new Point(10, this.Height / 2);
            Point p2 = new Point(this.Width - 10, this.Height / 2);
            gh.DrawLine(p,p1,p2);
        }

        public nyCotrolB()
        {
            /*//自定义事件，在构造函数中订阅
            aaaa += onClick;
            this.Click += aaaa;    //可以在任意地方触发aaaa事件，如某个属性改变，在set中触发，此处绑定在Click事件上，
             * */

            this.Click += onClick;   //直接绑定到Click事件上
        }

        private void onClick(object sender, EventArgs e)
        {
            MessageBox.Show("控件弹窗");
        }
        public event EventHandler aaaa;



    }
}
