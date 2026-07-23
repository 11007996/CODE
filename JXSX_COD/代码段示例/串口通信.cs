using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace SerialPort
{
    public partial class SerialPort : Form
    {
        String serialPortName;
        public SerialPort()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();//获取电脑上可用串口号
            comboBox1.Items.AddRange(ports);//给comboBox1添加数据
            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;//如果里面有数据,显示第0个

            comboBox2.Text = "115200";/*默认波特率:115200*/
            comboBox3.Text = "1";/*默认停止位:1*/
            comboBox4.Text = "8";/*默认数据位:8*/
            comboBox5.Text = "无";/*默认奇偶校验位:无*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "打开串口"){//如果按钮显示的是打开
                try{//防止意外错误
                    serialPort1.PortName = comboBox1.Text;//获取comboBox1要打开的串口号
                    serialPortName = comboBox1.Text;
                    serialPort1.BaudRate = int.Parse(comboBox2.Text);//获取comboBox2选择的波特率
                    serialPort1.DataBits = int.Parse(comboBox4.Text);//设置数据位
                    /*设置停止位*/
                    if (comboBox3.Text == "1") { serialPort1.StopBits = StopBits.One; }
                    else if (comboBox3.Text == "1.5") { serialPort1.StopBits = StopBits.OnePointFive; }
                    else if (comboBox3.Text == "2") { serialPort1.StopBits = StopBits.Two; }
                    /*设置奇偶校验*/
                    if (comboBox5.Text == "无") { serialPort1.Parity = Parity.None; }
                    else if (comboBox5.Text == "奇校验") { serialPort1.Parity = Parity.Odd; }
                    else if (comboBox5.Text == "偶校验") { serialPort1.Parity = Parity.Even; }

                    serialPort1.Open();//打开串口
                    button1.Text = "关闭串口";//按钮显示关闭串口
                }
                catch (Exception err)
                {
                    MessageBox.Show("打开失败"+ err.ToString(), "提示!");//对话框显示打开失败
                }
            }
            else{//要关闭串口
                try{//防止意外错误
                    serialPort1.Close();//关闭串口
                }
                catch (Exception){}
                button1.Text = "打开串口";//按钮显示打开
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0219){//设备改变
                if (m.WParam.ToInt32() == 0x8004){//usb串口拔出
                    string[] ports = System.IO.Ports.SerialPort.GetPortNames();//重新获取串口
                    comboBox1.Items.Clear();//清除comboBox里面的数据
                    comboBox1.Items.AddRange(ports);//给comboBox1添加数据
                    if (button1.Text == "关闭串口"){//用户打开过串口
                        if (!serialPort1.IsOpen){//用户打开的串口被关闭:说明热插拔是用户打开的串口
                            button1.Text = "打开串口";
                            serialPort1.Dispose();//释放掉原先的串口资源
                            comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;//显示获取的第一个串口号
                        }
                        else{
                            comboBox1.Text = serialPortName;//显示用户打开的那个串口号
                        }
                    }
                    else{//用户没有打开过串口
                        comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;//显示获取的第一个串口号
                    }
                }
                else if (m.WParam.ToInt32() == 0x8000){//usb串口连接上
                    string[] ports = System.IO.Ports.SerialPort.GetPortNames();//重新获取串口
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(ports);
                    if (button1.Text == "关闭串口"){//用户打开过一个串口
                        comboBox1.Text = serialPortName;//显示用户打开的那个串口号
                    }
                    else{
                        comboBox1.SelectedIndex = comboBox1.Items.Count > 0 ? 0 : -1;//显示获取的第一个串口号
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int len = serialPort1.BytesToRead;//获取可以读取的字节数
            byte[] buff = new byte[len];//创建缓存数据数组
            serialPort1.Read(buff, 0, len);//把数据读取到buff数组
            
            Invoke((new Action(() =>{//C# 3.0以后代替委托的新方法
                
                    textBox1.AppendText(Encoding.Default.GetString(buff));//对话框追加显示数据
            })));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();//清除接收对话框显示的数据
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String Str = textBox2.Text.ToString();//获取发送文本框里面的数据
            try
            {
                if (Str.Length > 0)
                {
                   
                        serialPort1.Write(Str);//串口发送数据
                    
                }
            }
            catch (Exception){ }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();//清除发送文本框里面的内容
        }
    }
}