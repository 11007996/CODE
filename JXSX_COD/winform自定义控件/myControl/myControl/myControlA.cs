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
    public partial class myControlA : UserControl
    {
        public myControlA()
        {
            InitializeComponent();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                label1.Text = "有效";
            }
            else
            {
                label1.Text = "无效";
            }
        }
    }
}
