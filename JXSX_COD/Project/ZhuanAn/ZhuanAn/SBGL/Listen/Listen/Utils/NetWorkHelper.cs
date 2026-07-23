using Listen.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Utils
{
    public class NetWorkHelper
    {
        //测试IP是否可以连接
        public static string PingIP(string ip)
        {
            try
            {
                Ping pinger = new Ping();
                string testData = "ping test";
                byte[] datas = Encoding.Default.GetBytes(testData);
                int timeout = 500;
                PingReply r = pinger.Send(ip, timeout, datas);
                string pingrst = "";
                if (r.Status != IPStatus.Success)
                {
                    pingrst = Enum.GetName(typeof(IPStatus), r.Status);
                }
                return pingrst;
            }
            catch (Exception e)
            {
                return "异常:" + e.Message;
            }
        }

        public static List<string> GetAllLocalIPv4Address()
        {
            List<string> list = new List<string>();
            //获取本机所有的IP地址列表
            IPAddress[] IpList = Dns.GetHostAddresses(Dns.GetHostName());
            //循环遍历所有IP地址
            foreach (IPAddress IP in IpList)
            {
                //判断是否是IPv4地址
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    list.Add(IP.ToString());
                }
            }
            return list;
        }


        #region 指定类型的端口是否已经被使用了
        /// <summary>
        /// 指定类型的端口是否已经被使用了
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="type">端口类型</param>
        /// <returns></returns>
        public static bool PortInUse(int port, PortType type)
        {
            bool flag = false;
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipendpoints = null;
            if (type == PortType.TCP)
            {
                ipendpoints = properties.GetActiveTcpListeners();
            }
            else
            {
                ipendpoints = properties.GetActiveUdpListeners();
            }
            foreach (IPEndPoint ipendpoint in ipendpoints)
            {
                if (ipendpoint.Port == port)
                {
                    flag = true;
                    break;
                }
            }
            ipendpoints = null;
            properties = null;
            return flag;
        }
        #endregion


        /// <summary>
        /// 获取本地IPv4地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIPv4Address()
        {
            IPAddress localIpv4 = null;
            //获取本机所有的IP地址列表
            IPAddress[] IpList = Dns.GetHostAddresses(Dns.GetHostName());
            //循环遍历所有IP地址
            foreach (IPAddress IP in IpList)
            {
                //判断是否是IPv4地址
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIpv4 = IP;
                }
                else
                {
                    continue;
                }
            }
            return localIpv4;
        }


    }
}
