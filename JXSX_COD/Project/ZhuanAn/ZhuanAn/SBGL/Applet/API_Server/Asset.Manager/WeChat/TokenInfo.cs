using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.WeChat
{
    class TokenInfo
    {
        public static TokenInfo tokenInfo;
        /// <summary>
        /// 获取到的凭证，最长为512字节
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 凭证的有效时间
        /// </summary>
        public DateTime expires_time { get; set; }
    }
}
