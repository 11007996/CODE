using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ConsoleApplication2;
using ITJI.WebServic;

class Program
{
    static void Main()
    {
        // 目标 IP 地址和端口
        //string ipAddress = "192.168.0.7";
        //int port = 4001;
        string ipAddress = ConfigurationManager.AppSettings["ipPort"].Split(':')[0];
        int port = int.Parse(ConfigurationManager.AppSettings["ipPort"].Split(':')[1]);
        int sleepInt = int.Parse(ConfigurationManager.AppSettings["sleepInt"]);
        int num = 0;
        saveHelper save = new saveHelper();
        while (true)
        {

            // 创建 TcpClient 实例
            using (TcpClient client = new TcpClient())
            {
                //try
                //{


                // 连接到服务器
                client.Connect(ipAddress, port);
                //Console.WriteLine("Connected to {0}", client.Client.RemoteEndPoint.ToString());
                // 02 03 00 00 00 0A C5 CD 40 15
                // 要发送的数据
                //byte[] sendData = new byte[] { 0x02, 0x03, 0x00, 0x00, 0x00, 0x0a, 0xC5, 0xCD };
                double abnormal = 0;
                string str = ConfigurationManager.AppSettings["jt"].ToString();
                byte[] sendData = HexStringToByteArray(str);  //new byte[] { str.Split(',') };
                                                              // 获取流以发送和接收数据
                NetworkStream stream = client.GetStream();

                // 发送数据
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Sent: " + BitConverter.ToString(sendData));

                // 接收数据
                byte[] receiveBuffer = new byte[256];
                int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                Console.WriteLine("Received: " + BitConverter.ToString(receiveBuffer, 0, bytesRead));
                string ReValue = BitConverter.ToString(receiveBuffer, 0, bytesRead).Replace("-", "");
                //Console.WriteLine(ReValue.Substring(6,4));
                String Message = "";
                Decimal num1 = Convert.ToDecimal(ChangeInt(ReValue.Substring(6, 4)) / 10.0); //温度
                Console.WriteLine(num1);
                Decimal num2 = Convert.ToDecimal(ChangeInt(ReValue.Substring(10, 4)) / 10.0);//湿度
                Console.WriteLine(num2);
                Decimal num3 = Convert.ToDecimal(ChangeInt(ReValue.Substring(14, 4)) / 10.0); //最高温度
                Console.WriteLine(num3);
                Decimal num4 = Convert.ToDecimal(ChangeInt(ReValue.Substring(18, 4)) / 10.0); //最低温度
                Console.WriteLine(num4);
                Decimal num5 = Convert.ToDecimal(ChangeInt(ReValue.Substring(22, 4)) / 10.0); //最高湿度
                Console.WriteLine(num5);
                Decimal num6 = Convert.ToDecimal(ChangeInt(ReValue.Substring(26, 4)) / 10.0); //最低湿度
                Console.WriteLine(num6);
                string ck = ConfigurationManager.AppSettings["ck"].ToString();
                if (num1 > num3)
                {
                    Message = ck + "号仓库温度过高，实时温度" + num1 + "°C,请及时处理！\n";
                    abnormal = 1;
                }
                else if (num1 < num4)
                {
                    Message = Message + ck + "号仓库温度过低，实时温度" + num1 + "°C,请及时处理！\n";
                    abnormal = 2;
                }
                if (num2 > num5)
                {
                    Message = Message + ck + "号仓库湿度过高，实时湿度" + num2 + "%,请及时处理！\n";
                    if(abnormal>0)
                    {
                        abnormal = abnormal*10 + 3;
                    }
                    else
                    {
                        abnormal = 3;
                    }
                }
                else if (num2 < num6)
                {
                    Message = Message + ck + "号仓库湿度过低，实时湿度" + num2 + "%,请及时处理！\n";
                    if (abnormal > 0)
                    {
                        abnormal = abnormal*10 + 4;
                    }
                    else
                    {
                        abnormal = 4;
                    }
                }
                if (Message.Trim() != "")
                {
                    sleepInt += 2000;
                    iDo2060WsHelp obj = new iDo2060WsHelp();
                    obj.SendWeChatTextMessage(ConfigurationManager.AppSettings["emp"].ToString(), Message.ToString());
                }
                else
                {
                    if(sleepInt>5000)
                    {
                        sleepInt = int.Parse(ConfigurationManager.AppSettings["sleepInt"]);
                    }
                }
                Console.WriteLine(num);
                if (num % 120 == 0)
                {
                    if (save.checkHas(ck))
                    {
                        save.saveData(num1, num3, num4, num2, num5, num6, abnormal,ck);
                    }
                }
                Thread.Sleep(sleepInt);
                num = num + 1;
            }
            //    // 显示接收到的数据                             
            //}

            //catch (ArgumentNullException e)
            //{
            //    Console.WriteLine("ArgumentNullException: {0}", e);
            //}
            //catch (SocketException e)
            //{
            //    Console.WriteLine("SocketException: {0}", e);
            //}
            //finally
            //{
            //    // 关闭所有资源
            //    client.Close();
            //}

        }
    }

    static int ChangeInt(string hexString)
    {
        return int.Parse(hexString, NumberStyles.HexNumber);
    }
    // 将16进制字符串转换为字节数组的方法
    static byte[] HexStringToByteArray(string hex)
    {
        // 确保字符串长度是偶数
        if (hex.Length % 2 != 0)
        {
            throw new ArgumentException("输入的16进制字符串长度必须是偶数。");
        }

        // 使用LINQ将字符串每两个字符转换为一个字节
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }
}