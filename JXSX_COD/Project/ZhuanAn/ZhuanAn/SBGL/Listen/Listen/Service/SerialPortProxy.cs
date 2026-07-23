using Listen;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Listen.Service
{
    public class SerialPortProxy
    {
        private static SerialPort SerialPort = null;
        public static IList<byte[]> revData = new List<byte[]>();//接收
        public static IList<byte[]> resData = new List<byte[]>();//返回
        public static bool OpenListenView = false; //打开监控视图
        //监听状态
        private static bool _SerialListenState = false;
        public static bool SerialListenState { get { return _SerialListenState; } }

        public static void Open()
        {
            try
            {
                SerialPort = new SerialPort();
                SerialPort.PortName = BaseInfo.PortName;
                SerialPort.BaudRate = BaseInfo.BaudRate;
                SerialPort.DataBits = BaseInfo.DataBits;
                SerialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), BaseInfo.StopBits.ToString());
                SerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), BaseInfo.Parity.ToString());
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
                byte[] r = ReceiveHandle.SerialPort_HandleReceiveMsg(buffer, e);
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
        #endregion


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
        #endregion

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
        #endregion
    }
}
