using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myControl
{
    public partial class UserControl1 : Button
    {
        private int _mWidth = 0;
        [Description("大小"), Category("自定义")]
        public int mWidth
        {
            get { return _mWidth; }
            set { 
                _mWidth = value;
                this.Width = _mWidth;
            }
        }
        
        public void mClick(object sender,EventArgs arg)
        {
            MessageBox.Show("hello");
        }
        public UserControl1()
        {
            this.Click += mClick;

        }
    }
}
