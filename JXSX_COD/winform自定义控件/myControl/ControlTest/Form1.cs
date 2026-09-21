using myControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    }
}
