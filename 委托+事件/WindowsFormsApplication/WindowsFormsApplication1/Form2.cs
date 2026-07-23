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
    public delegate void parameter(string value);
    //public delegate void parameter(string value);
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }
        //public event parameter para;
        private void button1_Click(object sender, EventArgs e)
        {
           
            //para(label1.Text);

            //txtTrans("jiu");
            para("jiu");
        }
        
        public event parameter para;
        //private void txtTrans(string message)
        //{
        //   if(label1.InvokeRequired)
        //   {
        //       parameter par = new parameter(txtTrans);
        //       label1.Invoke(par,message);
        //   }
        //   else
        //   {
        //       label1.Text = message;
        //   }
        //}
    }
}
