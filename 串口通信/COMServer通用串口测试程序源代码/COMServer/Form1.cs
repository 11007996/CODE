using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;


namespace COMServer
{
    public partial class Form1 : Form
    {
        protected bool isInTimerFun = false;//是否正处在timer的事件中
        string ReceiveData = "";//接收到的数据
        //从串口中读取数据的委托
        public delegate void _SafeAddtrTextCall(string text);
        /// <summary>
        /// 把信息显示在textbox上并计入日志的委托
        /// </summary>
        /// <param name="msg">要显示的信息</param>
        private delegate void ShowMSgDelegate(string msg);//把信息显示在textbox上并计入日志的委托
        /// <summary>
        /// 把异常信息显示在textbox上并计入日志的委托
        /// </summary>
        /// <param name="msg">要显示的异常信息</param>
        private delegate void ShowExceptionMSgDelegate(Exception msg);//把异常信息显示在textbox上并计入日志的委托
        /// <summary>
        /// 互斥元，保证些日志的时候，日志文件共享读写不出错
        /// </summary>
        Mutex mu = new Mutex(false);//互斥元，保证些日志的时候，日志文件共享读写不出错
        /// <summary>
        /// 互斥元，保证写异常日志的时候，日志文件共享读写不出错
        /// </summary>
        Mutex muExceptionLog = new Mutex(false);//互斥元，保证写异常日志的时候，日志文件共享读写不出错

        public Form1()
        {
            InitializeComponent();
        }

        #region 窗体事件
        private void Form1_Load(object sender, EventArgs e)
        {
            //SetMSG("服务程序开启……");
            string portNum = "SerialPort Count:";

            try
            {
                //设置串口的默认参数
                portNum += SerialPort.GetPortNames().Length.ToString();
                for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
                {
                    portNum += "\r\n" + SerialPort.GetPortNames().GetValue(i).ToString() + "\r\n";

                    ComboBoxPortNum.Items.Add(SerialPort.GetPortNames().GetValue(i).ToString());

                }
                SetMSG(portNum);
                ComboBoxPortNum.SelectedIndex = 0;
                comboBoxDateBits.SelectedIndex = 3;
                comboBoxParity.SelectedIndex = 2;
                comboBoxStopBit.SelectedIndex = 0;
                comboBoxBondRate.SelectedIndex = 3;
                //作为服务程序运行，需要自行打开串口
                OpenSerialPort();
              
            }
            catch
            {               
                portNum += "0\r\nTheir is no any SerialPort on your computer!";
                SetMSG(portNum);
            }
        }

        /// <summary>
        /// 窗体关闭释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1 != null)
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                    }
                    serialPort1.Dispose();
                }
            }
            catch (Exception ex)
            {
                SetMSG("关闭时发生错误！" + ex.Message);
            }
        }

        #endregion

        #region 打开和关闭串口
        /// <summary>
        /// 打开串口
        /// </summary>
        protected void OpenSerialPort()
        {
            if (ComboBoxPortNum.Text.ToString() != "")
            {
                serialPort1.PortName = ComboBoxPortNum.Text.ToString().Trim();
                try
                {
                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.Open();
                    }

                    LBPortState.Text = serialPort1.PortName + "已打开！";
                    if (comboBoxBondRate.Text.ToString() != "")
                    {
                        serialPort1.BaudRate = int.Parse(comboBoxBondRate.Text.ToString());
                    }
                    if (comboBoxDateBits.Text.ToString() != "")
                    {
                        serialPort1.DataBits = int.Parse(comboBoxDateBits.Text.ToString());
                    }
                    if (comboBoxStopBit.Text.ToString() != "")
                    {
                        switch (comboBoxStopBit.Text.ToString())
                        {
                            case "1":
                                serialPort1.StopBits = StopBits.One;
                                break;
                            case "1.5":
                                serialPort1.StopBits = StopBits.OnePointFive;
                                break;
                            case "2":
                                serialPort1.StopBits = StopBits.Two;
                                break;
                        }
                    }
                    if (comboBoxParity.Text.ToString() != "")
                    {
                        switch (comboBoxParity.Text.ToString())
                        {/*
                          * 
                          * Even
                          * Mark
                          * None
                          * Odd
                          * Space
                          */
                            case "Even":
                                serialPort1.Parity = Parity.Even;
                                break;
                            case "Mark":
                                serialPort1.Parity = Parity.Mark;
                                break;
                            case "None":
                                serialPort1.Parity = Parity.None;
                                break;
                            case "Odd":
                                serialPort1.Parity = Parity.Odd;
                                break;
                            case "Space":
                                serialPort1.Parity = Parity.Space;
                                break;
                        }
                    }
            
                    btnSend.Enabled = true;
                    btnCloseSerialPort.Enabled = true;
                    btnOpenSerialPort.Enabled = false;
                   
                }
                catch (Exception e2)
                {
                    LBPortState.Text = e2.Message;
                    //toolStripStatusLabel1.Text = e2.Message;
                    serialPort1.Dispose();
                }
            }
            else
            {
                LBPortState.Text = "请选择串口！";
            }
        }
        //打开串口
        private void btnOpenSerialPort_Click(object sender, EventArgs e)
        {
            OpenSerialPort();
        }

        //关闭串口
        private void btnCloseSerialPort_Click(object sender, EventArgs e)
        {
           
            if (ComboBoxPortNum.Text.ToString() != "")
            {

                try
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();

                    }

                    LBPortState.Text = serialPort1.PortName + "已关闭！";
                    btnSend.Enabled = false;
                    btnOpenSerialPort.Enabled = true;
                    btnCloseSerialPort.Enabled = false;
                   
                }
                catch (Exception e2)
                {
                    LBPortState.Text = e2.Message;
                    serialPort1.Dispose();
                }

            }
            else
            {
                LBPortState.Text = "请选择串口！";
            }
        }

        #endregion

        #region 通用串口测试
        //通用串口测试
        private void btnSend_Click(object sender, EventArgs e)
        {
            string sResult = "";
            sResult = WriteData(txtBoxSend.Text);
            if (sResult != "")
            {
                SetMSG(sResult);
            }

        }
        #endregion

        #region 处理接收到的数据的委托

        //委托的实现
        private void safeAddtrText(string text)
        {

            if (this.InvokeRequired)
            {
                _SafeAddtrTextCall callALL =
                   delegate(string s)
                   {
                       //if (txtBoxAllReceive.Text.Length > 2048)
                       //    txtBoxAllReceive.Text = "";
                       //txtBoxAllReceive.Text += s + "  ";
                       txtBoxAllReceive.AppendText(s+"   ");
                       SetMSG("收到数据:" + s);
                   };
                this.Invoke(callALL, text);
            }
            else
            {
                txtBoxAllReceive.AppendText(text);
                SetMSG("收到数据:" + text);
            }

        }




        #endregion

        #region 串口接收数据事件响应
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //bool flag = false;//是否读完整包数据
            //int PackgeLength = 0;//下位机返回数据的总长度。
            while (serialPort1.BytesToRead > 0)
            {

                if (isHexShow.Checked)
                {
                    int data = 0x00;
                    try
                    {
                        data = serialPort1.ReadByte();
                    }
                    catch (Exception e2)
                    {
                        // MessageBox.Show(e2.Message);
                        SetMSG(e2.Message);
                        break;
                    }

                    ReceiveData += data.ToString("X").PadLeft(2, '0');
                    ReceiveData = ReceiveData.ToUpper();
                }
                else
                {
                    ReceiveData += serialPort1.ReadExisting();
                }
            }
            safeAddtrText(ReceiveData);//显示数据的委托
            ReceiveData = "";

        }
        #endregion

        #region 杂项
        ////设置程序运行中产生的消息并计入日志
        //protected void SetMSG(string msg)
        //{
        //    txtBoxMSG.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg + "\r\n";
        //    NomalFunction.WriteNomalLog(msg);
        //}

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CLSTxtBoxAllReceive_Click(object sender, EventArgs e)
        {
            txtBoxAllReceive.Text = "";
        }

        //private void txtBoxAllReceive_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtBoxAllReceive.Text.Length > 4096)
        //        txtBoxAllReceive.Text = "";
        //    txtBoxAllReceive.SelectionStart = txtBoxAllReceive.Text.Length + 10;//设置选中文字的开始位置为文本框的文字的长度
        //    txtBoxAllReceive.SelectionLength = 0;//设置被选中文字的长度为0（将光标移动到文字最后）
        //    txtBoxAllReceive.ScrollToCaret();//将滚动条移动到光标位置
        //}

      
        #endregion

        #region 向串口发送数据
        /// <summary>
        /// 向串口发送数据
        /// </summary>       
        /// <param name="cmd">则为命令码</param>
        /// <returns>为空则执行正常，否则为异常信息</returns>
        protected string WriteData(string cmd)
        {
            string sResult = "";
            if (serialPort1 == null)
            {
              
                sResult = "串口未初始化！";
                return sResult;

            }
            if (!serialPort1.IsOpen)
            {
              
                sResult = "串口未打开，串口号为：" + serialPort1.PortName;
                return sResult;
            }
            try
            {
                string sendvalue = "";
                sendvalue = cmd;

                if (checkBoxTrimSpace.Checked)
                {
                    sendvalue = sendvalue.Replace(" ", "");
                }


                if (chBoxIsHex.Checked)
                {
                    int sendLength = sendvalue.Length / 2;
                    byte[] StrBuffer = new byte[sendLength];
                    string hexstring = "";
                    int k = 0;
                    for (int i = 0; i < sendvalue.Length; )
                    {
                        try
                        {
                            hexstring = sendvalue.Substring(i, 2);
                        }
                        catch (Exception ex)
                        {
                            sResult += "使用hex发送数据出错，请检查发送的数据是否是16进制格式！详细信息："+ex.Message;
                            return sResult;
                        }
                        int j;
                        j = int.Parse(hexstring, System.Globalization.NumberStyles.HexNumber);
                        StrBuffer[k] = (byte)j;
                        i += 2;
                        k++;
                    }

                    serialPort1.Write(StrBuffer, 0, StrBuffer.Length);

                }
                else
                {
                    serialPort1.Write(sendvalue);
                    System.Threading.Thread.Sleep(50);//每次发送完毕，休眠50ms
                }
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
            }
            return sResult;
        }
        #endregion



        #region 消息和日志

        /// <summary>
        /// //设置程序运行中产生的消息并计入日志
        /// </summary>
        /// <param name="msg">消息</param>
        protected void SetMSG(string msg)
        {
            ShowMSgDelegate showmsgDelegate = new ShowMSgDelegate(SetmsgDelegateTargetFun);
            if (txtBoxMSG.InvokeRequired)
            {
                txtBoxMSG.BeginInvoke(showmsgDelegate, msg);
            }
            else
            {
                if (txtBoxMSG.Text.Length > 1024 * 512)
                {
                    txtBoxMSG.Text = string.Empty;
                }
                txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg + "\r\n");
            }
            mu.WaitOne();
            SiXi.Logs.Log.WriteDebugLog(msg);
            mu.ReleaseMutex();
        }
        /// <summary>
        /// 显示消息的委托的目标函数
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        protected void SetmsgDelegateTargetFun(string msg)
        {
            if (txtBoxMSG.Text.Length > 1024 * 512)
            {
                txtBoxMSG.Text = string.Empty;
            }
            txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg + "\r\n");
        }
        /// <summary>
        /// //显示程序运行中产生的异常消息并计入日志
        /// </summary>
        /// <param name="msg">消息</param>
        protected void SetExceptionMSG(Exception msg)
        {
            ShowExceptionMSgDelegate showmsgDelegate = new ShowExceptionMSgDelegate(SetExceptionmsgDelegateTargetFun);
            if (txtBoxMSG.InvokeRequired)
            {
                txtBoxMSG.BeginInvoke(showmsgDelegate, msg);
            }
            else
            {
                if (txtBoxMSG.Text.Length > 1024 * 512)
                {
                    txtBoxMSG.Text = string.Empty;
                }
                txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg.ToString() + "\r\n");
            }
            muExceptionLog.WaitOne();
            SiXi.Logs.Log.WriteDebugLog(msg.ToString());
            muExceptionLog.ReleaseMutex();
        }
        /// <summary>
        /// 显示异常消息的委托的目标函数
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        protected void SetExceptionmsgDelegateTargetFun(Exception msg)
        {
            if (txtBoxMSG.Text.Length > 1024 * 512)
            {
                txtBoxMSG.Text = string.Empty;
            }
            txtBoxMSG.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "：" + msg.ToString() + "\r\n");
        }
        #endregion

        #region 右键菜单
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 abForm = new AboutBox1();
            abForm.ShowDialog();
        }
        #endregion



    }
}