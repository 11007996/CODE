using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsClient
{
    public partial class Form1 : Form
    {
        WebSocketClient ws;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ws = new WebSocketClient("ws://localhost:8080");
            await ws.ConnectAsync();
            ws.OnTextMessage += showMes;
            ws.OnClosed += ClientClose;
            ws.OnError += showError;
            button1.Enabled = false;
            button3.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await ws.SendTextAsync(textBox1.Text);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await ws.CloseAsync();
            button3.Enabled = false;
            button1.Enabled = true;
        }
        private void showMes(string message)
        {
            if(!message.Equals("PING"))
                textBox2.Text ="服务器消息："+ message;
        }
        private void ClientClose(string message)
        {
            textBox2.Text = "服务器关闭：" + message;
            ws.OnClosed -= ClientClose;
        }
        private void showError(Exception ex)
        {
            textBox2.Text = "服务器异常"+ ex.Message;
        }
    }
}
