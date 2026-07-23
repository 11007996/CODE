using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model
{
    public class UserInfoDO
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string UserRight { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime ExpiresTime { get; set; }
    }
}
