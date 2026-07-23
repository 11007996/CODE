using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //动态增加控件，先实例化的个控件，设置控件的初始属性，然后调用组件的Controls.Add方法添加进组件
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 实例化一个控件
            CheckBox cb = new CheckBox();
            Point p = new Point(20,50); 
            cb.Checked = true;
            cb.Location = p;
            cb.Text = "新增控件";
            #endregion

            #region 向panel组件中添加CheckBox控件
            panel1.Controls.Add(cb);
            #endregion
        }
    }
}
