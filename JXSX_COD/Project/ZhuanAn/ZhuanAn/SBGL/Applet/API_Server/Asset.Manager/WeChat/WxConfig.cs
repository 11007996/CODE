using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.WeChat
{
    class WxConfig
    {
        /// <summary>
        /// 企业微信，企业唯一ID
        /// </summary>
        public static string corpid = "wxaf0083812bd83ebd";

        /// <summary>
        /// 应用id
        /// </summary>
        public static string agentid = "1000136";//应用ID

        /// <summary>
        /// 应用的凭证密钥
        /// </summary>
        public static string corpsecret = "DO_tGofJNhkzRVOkMqq6XRrc-zADoup35m4rsSQg_6U";//测试应用密钥：

        /// <summary>
        /// token缓存文件路径
        /// </summary>
        public static string token_file = System.AppDomain.CurrentDomain.BaseDirectory+"\\Temp Files\\Token.json";

        /// <summary>
        /// ticket缓存文件路径
        /// </summary>
        public static string ticket_file = System.AppDomain.CurrentDomain.BaseDirectory + "\\Temp Files\\Ticket.json";
    }
}
