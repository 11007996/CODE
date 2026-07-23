using Listen.Base;
using Listen.Model;
using Listen.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace Listen
{

    public class BaseInfo
    {
        #region 保养操作开关
        /// <summary>
        /// 【日】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public static bool DayMaintenanceCheckFlag = false;
        /// <summary>
        /// 【周】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public static bool WeekMaintenanceCheckFlag = false;
        /// <summary>
        /// 【月】保养检查开关，false 表示返回的保养状态都是正常，true表示需要返回实际保养状态
        /// </summary>
        public static bool MonthMaintenanceCheckFlag = false;
        #endregion

        #region 通信编码
        /// <summary>
        /// 前缀16进制字符编码
        /// </summary>
        public static string PrefixHexCode;
        /// <summary>
        /// 后缀16进制字符编码
        /// </summary>
        public static string SuffixHexCode;
        /// <summary>
        /// 通信编码16进制字节码总长度
        /// </summary>
        public static int HexCodeByteSize;
        /// <summary>
        /// 通信响应的16进制字符编码
        /// </summary>
        public static string ResponseHexCode;
        #endregion

        #region TCP配置
        /// <summary>
        /// 是否开启TCP监听服务
        /// </summary>
        public static bool TCPListenFlag = false;
        /// <summary>
        /// 开启服务的IP
        /// </summary>
        public static IPAddress ListenIP;
        /// <summary>
        /// 服务端口号
        /// </summary>
        public static int Port = 10409;
        /// <summary>
        /// 接收超时(秒)
        /// </summary>
        public static int ReceiveTimeout = 5;
        /// <summary>
        /// 监听限制
        /// </summary>
        public static int ListenLimit = 100;
        #endregion

        #region 串口设置
        /// <summary>
        /// 是否开启串口监听服务
        /// </summary>
        public static bool SerialListenFlag = false;
        /// <summary>
        /// 串口名称
        /// </summary>
        public static string PortName = "COM3";
        /// <summary>
        /// 比特率
        /// </summary>
        public static int BaudRate = 9600;
        /// <summary>
        /// 数据位
        /// </summary>
        public static int DataBits = 8;
        /// <summary>
        /// 停止位(枚举StopBits的值)
        /// </summary>
        public static StopBits StopBits = StopBits.One;
        /// <summary>
        /// 奇偶校验(枚举Parity的值)
        /// </summary>
        public static Parity Parity = Parity.Odd;
        #endregion

        #region 登入人
        /// <summary>
        /// 当前登入人工号(为空，未登入)
        /// </summary>
        public static string LoginUserNo;
        /// <summary>
        /// 当前登入人名称
        /// </summary>
        public static string LoginUserName;
        /// <summary>
        /// 当前登入人权限（A:管理员，B:产线人员）
        /// </summary>
        public static string LoginUserRight;
        /// <summary>
        /// 当前登入人部门
        /// </summary>
        public static string LoginDept;
        #endregion
       
    }
}

