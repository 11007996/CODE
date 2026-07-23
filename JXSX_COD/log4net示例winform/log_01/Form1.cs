using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using log4net;

namespace log_01
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                log4net.Config.DOMConfigurator.Configure();
                 var log = log_01.LogFactory.GetLogger(typeof(Form1));
                 log.Debug("运行错误");
                log.Info("系统启动");
               
             
                log.Info("起始页面载入222");
                log.Error("起始页面载入");
            }
            catch (Exception ex)
            {
                string a = ex.ToString();
            }
        }
    }
}
