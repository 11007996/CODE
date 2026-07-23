using EAM.Listen.Common.Config;
using EAM.Listen.DataProcessing;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace EAM.Listen.Communication
{
    public class SerialPortProxy
    {
        public static IList<byte[]> revData = new List<byte[]>();

        //接收
        public static IList<byte[]> resData = new List<byte[]>();

        //返回
        public static bool OpenListenView = false;

        private static SerialPort SerialPort = null;

        //打开监控视图
        //监听状态
        private static bool _SerialListenState = false;

        public static bool SerialListenState
        { get { return _SerialListenState; } }

        public static void Open()
        {
            try
            {
                SerialPort = new SerialPort();
                SerialPort.PortName = Setting.SerialPortConfig.PortName;
                SerialPort.BaudRate = Setting.SerialPortConfig.BaudRate;
                SerialPort.DataBits = Setting.SerialPortConfig.DataBits;
                SerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Setting.SerialPortConfig.StopBits.ToString());
                SerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Setting.SerialPortConfig.Parity.ToString());
                // SerialPort.ReceivedBytesThreshold = BaseInfo.HexCodeByteSize;
                //添加数据接收处理事件
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.Open();
                Console.WriteLine("启动串口监听服务成功,串口:" + SerialPort.PortName);
                _SerialListenState = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动串口监听服务失败,原因:" + ex.Message);
                Close();
            }
        }

        #region 数据接收事件

        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] buffer = null;
                byte[] data = new byte[2048];
                int receiveCount = 0;
                while (true)
                {
                    if (SerialPort.BytesToRead < 1)
                    {
                        break;
                    }
                    receiveCount += SerialPort.Read(data, receiveCount, SerialPort.BytesToRead);
                }
                buffer = new byte[receiveCount];
                Array.Copy(data, 0, buffer, 0, receiveCount);
                if (receiveCount == 0)
                {
                    return;
                }
                byte[] r = null;
                SendMessage(r);
                if (OpenListenView)
                {
                    revData.Add(buffer);
                }
            }
            catch (Exception)
            {
                Close();
            }
        }

        #endregion 数据接收事件

        #region 发送

        public static void SendMessage(byte[] data)
        {
            try
            {
                if (SerialPort == null || data == null || data.Length <= 0) return;
                SerialPort.Write(data, 0, data.Length);
                if (OpenListenView)
                {
                    resData.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法发送信息！原因:" + ex.Message);
            }
        }

        #endregion 发送

        #region 关闭

        public static void Close()
        {
            if (SerialPort != null)
            {
                SerialPort.Close();
                SerialPort.Dispose();
                SerialPort = null;
            }
            _SerialListenState = false;
        }

        #endregion 关闭
    }
}