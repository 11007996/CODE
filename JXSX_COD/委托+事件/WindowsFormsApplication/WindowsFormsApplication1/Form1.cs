using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.para += new parameter(txtChange);
            frm.para += new parameter(txtChangea);
            frm.para += new parameter(txtChangeb);
            frm.ShowDialog();
        }
        private void txtChange(string value)
        {
            label1.Text += value;
        }
        private void txtChangea(string value)
        {
            label1.Text += value;
        }
        private void txtChangeb(string value)
        {
            label1.Text += value;
        }
    }
}
