using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace SerialPort
{
    public partial class Form1 : Form
    {
        string Var_Atten_1;
        int All_S_Bytes = 0;   //发送字符数统计数
        int All_R_Bytes = 0;   //接收字符数统计数
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(mydefault_parameters); //加载默认状态下串口的参数
        }

        //串口自定义参数
        private void mydefault_parameters(object sender, EventArgs e)
        {
            comboBox2.Items.Add("1200");
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("14400");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("56000");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("115200");
            comboBox2.Items.Add("128000");
            comboBox2.Items.Add("230400");
            comboBox2.Items.Add("256000");
            comboBox2.Items.Add("600000");
            comboBox2.Text = "9600";

            comboBox3.Items.Add("5");
            comboBox3.Items.Add("6");
            comboBox3.Items.Add("7");
            comboBox3.Items.Add("8");
            comboBox3.Text = "8";

            comboBox4.Items.Add("1");
            comboBox4.Items.Add("1.5");
            comboBox4.Items.Add("2");
            comboBox4.Text = "1";

            comboBox5.Items.Add("None");
            comboBox5.Items.Add("Odd");
            comboBox5.Items.Add("Even");
            comboBox5.Items.Add("Mark");
            comboBox5.Items.Add("Space");
            comboBox5.Text = "None";

            richTextBox2.ReadOnly = true;
            serialPort1.DataReceived += serialPort1_DataReceived;   //加载串口接收信息
        }

        private void 串口信息_Paint(object sender, PaintEventArgs e)
        {
             
        }

        private void comboBox_Port_Update()
        {
            comboBox1.Items.Clear();
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if(keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                foreach(string sName in sSubKeys )
                {
                    string sValue = (string)keyCom.GetValue(sName );
                    comboBox1.Items.Add(sValue);
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.Text;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox_Port_Update();
        }

        private void button27_Click(object sender, EventArgs e)   //button: 更新串口
        {
            comboBox1.Items.Clear();
            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCom != null)
            {
                string[] sSubKeys = keyCom.GetValueNames();
                foreach (string sName in sSubKeys)
                {
                    string sValue = (string)keyCom.GetValue(sName);
                    comboBox1.Items.Add(sValue);
                }
            }
        }

        private void button26_Click(object sender, EventArgs e)  //button: 打开串口
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    comboBox5.Enabled = false;

                    //ovalShape1.BackColor = Color.Red;
                    serialPort1.Open();
                    打开串口按钮.Text = "关闭串口";
                }
                catch
                {
                    MessageBox.Show("串口" + serialPort1.PortName + "打开失败！\r\n可能是串口不存在或者已被占用。");
                    comboBox1.Enabled = true;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                    comboBox4.Enabled = true;
                    comboBox5.Enabled = true;
                }
            }
            else
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;

                serialPort1.Close();
                //ovalShape1.BackColor = Color.White;
                button2.Text = "打开串口";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("请先打开串口！");
            }
            else
           //if (!Information.IsNumeric(textBox1.Text) | !Information.IsNumeric(textBox2.Text) | !Information.IsNumeric(textBox3.Text) | !Information.IsNumeric(textBox4.Text) | !Information.IsNumeric(textBox5.Text) | !Information.IsNumeric(textBox6.Text) | !Information.IsNumeric(textBox7.Text) | !Information.IsNumeric(textBox8.Text))
           //      MessageBox.Show("请输入完整设置！");
           // else
           if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                Var_Atten_1 = Convert.ToString(richTextBox1.Text);

                serialPort1.Write("\r");
                serialPort1.Write(Var_Atten_1);
                All_S_Bytes += Convert.ToString(Var_Atten_1).Length;
                //flag = 1;
            }
            else
                MessageBox.Show("有数据为空！请完整填写数据");

            /*****
            if (flag == 1 && serialPort1.IsOpen)
            {

                // 十六进制重构 
                HexSerialPort1(0XEE);  //帧头

                HexSerialPort1(0X11);  //衰减控制标志

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_1)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_2)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_3)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_4)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_5)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_6)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_7)));

                HexSerialPort1S(Convert.ToString((int)(4.0 * Var_Atten_8)));

                HexSerialPort1(0XFF);  //帧尾

              ***/


                All_S_Bytes += 1;

                label81.Text = Convert.ToString(All_S_Bytes);
                richTextBox2.Text = richTextBox2.Text + "发送成功 " + DateTime.Now.ToString("HH:mm:ss") + "\r\n"; ;
            //}
        }
       
        /******  串口接收程序 ******/
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            UTF8Encoding unicode = new UTF8Encoding();
            Byte[] readBytes = new Byte[serialPort1.BytesToRead];
            int SDataTemp = this.serialPort1.Read(readBytes, 0, readBytes.Length);
            All_R_Bytes += SDataTemp;

            this.richTextBox2.Invoke
            (
                new MethodInvoker
                (
                    delegate
                    {
                        string returnStr = "";
                        for (int i = 0; i < SDataTemp; i++)
                        {
                            returnStr += readBytes[i].ToString("X2") + " ";
                        }
                        int cs = 1;
                        bool chen = cs > 0;
                        if (chen)
                        {

                            this.richTextBox2.AppendText(Encoding.GetEncoding("gb2312").GetString(readBytes));
                        }
                        else
                        {
                            this.richTextBox2.AppendText(unicode.GetString(readBytes));
                        }

                        richTextBox2.SelectionStart = richTextBox2.TextLength;
                        label86.Text = Convert.ToString(All_R_Bytes);
                    }
                )
            );
        }

        //清空串口接收栏数据
        private void button2_Click_1(object sender, EventArgs e)
        {
            richTextBox2.Text = null;
            label86.Text = "0";
            label81.Text = "0";
        }
    }
}
