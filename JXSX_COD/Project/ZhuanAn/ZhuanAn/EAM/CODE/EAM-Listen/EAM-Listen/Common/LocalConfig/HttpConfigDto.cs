using System.Collections.Generic;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// Http连接配置
    /// </summary>
    public class HttpConfigDto
    {
        /// <summary>
        /// 是否开启Http监听服务
        /// </summary>
        public bool HttpListenFlag { get; set; } = false;

        public int Port { get; set; } = 8409;

        /// <summary>
        /// http路径前缀集合
        /// </summary>
        //public List<string> Prefixes { get; set; } = new List<string>();
    }
}