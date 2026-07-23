using Infrastructure;

namespace EAM.Common.Wechat
{
    internal class WxCorpConfig
    {
        /// <summary>
        /// 企业微信，企业唯一ID
        /// </summary>
        internal static readonly string CORPID = AppSettings.App(new string[] { "WxCorp", "CorpID" });

        /// <summary>
        /// 应用id
        /// </summary>
        internal static readonly string AGENTID = AppSettings.App(new string[] { "WxCorp", "AgentID" });

        /// <summary>
        /// 应用的凭证密钥
        /// </summary>
        internal static readonly string CORPSECRET = AppSettings.App(new string[] { "WxCorp", "CorpSecret" });

        /// <summary>
        /// token缓存文件路径
        /// </summary>
       // internal static string token_file = AppDomain.CurrentDomain.BaseDirectory + "\\Temp Files\\Token.json";

        /// <summary>
        /// ticket缓存文件路径
        /// </summary>
      //  internal static string ticket_file = AppDomain.CurrentDomain.BaseDirectory + "\\Temp Files\\Ticket.json";
    }
}