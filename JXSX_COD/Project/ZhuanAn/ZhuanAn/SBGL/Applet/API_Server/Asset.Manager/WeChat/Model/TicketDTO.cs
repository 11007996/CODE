using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.WeChat.Model
{
    /// <summary>
    /// 获取ticket接口返回对象
    /// </summary>
    class TicketDTO : BaseDTO
    {
        /// <summary>
        /// 获取到的凭证，最长为512字节
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}
