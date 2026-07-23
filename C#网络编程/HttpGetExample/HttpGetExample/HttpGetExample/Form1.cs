using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpGetExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Encoding utf = Encoding.GetEncoding("UTF-8");
            string uri = "http://www.baidu.com/s?wd=" + HttpUtility.UrlEncode(textBox1.Text, utf);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                richTextBox1.Text = sr.ReadToEnd();
                stream.Dispose();
                sr.Dispose();
                stream.Dispose();
            }
            webBrowser1.DocumentText = richTextBox1.Text;
        }
    }
}
