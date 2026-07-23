using System;
using System.IO.Ports;

namespace ComTools.SerialPortService
{
    public class SerialPortProxy
    {
        public HandleReceivedDataDelegate HandleReceivedDelegate;
        private SerialPort SerialPort = null;

        //委托：接收数据的处理
        public delegate byte[] HandleReceivedDataDelegate(SerialDataReceivedArgs e);

        //监听状态
        public bool SerialListenState
        {
            get
            {
                if (SerialPort != null && SerialPort.IsOpen)
                    return true;
                return false;
            }
        }

        public SerialPort GetSerialPort()
        {
            return SerialPort;
        }

        /// <summary>
        /// 开启串口监听
        /// </summary>
        /// <param name="config"></param>
        /// <param name="handleAction">接收消息处理方法</param>
        public void Open(SerialPortConfig config, HandleReceivedDataDelegate handleAction)
        {
            try
            {
                SerialPort = new SerialPort();
                SerialPort.PortName = config.PortName;
                SerialPort.BaudRate = config.BaudRate;
                SerialPort.DataBits = config.DataBits;
                SerialPort.StopBits = config.StopBits;
                SerialPort.Parity = config.Parity;
                if (handleAction != null)
                    HandleReceivedDelegate = handleAction;
                //添加数据接收处理事件
                SerialPort.DataReceived += SerialPort_DataReceived;
                SerialPort.Open();
                Console.WriteLine("启动串口监听服务成功,串口:" + SerialPort.PortName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动串口监听服务失败,原因:" + ex.Message);
                Close();
            }
        }

        #region 数据接收事件

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] buffer = null;
                byte[] data = new byte[1024];
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
                if (HandleReceivedDelegate != null)
                {
                    SerialDataReceivedArgs args = new SerialDataReceivedArgs();
                    args.SerialPort = SerialPort;
                    args.Data = buffer;
                    r = HandleReceivedDelegate(args);
                }
                SendMessage(r);
            }
            catch (Exception)
            {
                Close();
            }
        }

        #endregion 数据接收事件

        #region 发送

        public bool SendMessage(byte[] data)
        {
            try
            {
                if (!SerialListenState || data == null || data.Length <= 0) return false;
                SerialPort.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法发送信息！原因:" + ex.Message);
                return false;
            }
        }

        #endregion 发送

        /// <summary>
        ///再次开启
        /// </summary>
        public bool TryReOpen()
        {
            try
            {
                if (!SerialListenState)
                    SerialPort.Open();
                return SerialListenState;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 关闭

        public void Close()
        {
            if (SerialPort != null)
            {
                SerialPort.Close();
                SerialPort.Dispose();
            }
        }

        #endregion 关闭
    }
}