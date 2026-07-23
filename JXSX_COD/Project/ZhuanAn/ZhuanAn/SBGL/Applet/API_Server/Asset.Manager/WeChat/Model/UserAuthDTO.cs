using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Manager.WeChat.Model
{
    public class UserAuthDTO : BaseDTO
    {

        #region 企业成员返回参数
        /// <summary>
        /// 成员UserID。若需要获得用户详情信息，可调用通讯录接口：读取成员。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员票据，最大为512字节，有效期为1800s。
        /// scope为snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。
        /// 后续利用该参数可以获取用户信息或敏感信息，
        /// </summary>
        public string user_ticket { get; set; }
        #endregion

        #region 非企业成员返回参数
        /// <summary>
        /// 非企业成员的标识，对当前企业唯一。不超过64字节
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 外部联系人id，当且仅当用户是企业的客户，且跟进人在应用的可见范围内时返回。如果是第三方应用调用，针对同一个客户，同一个服务商不同应用获取到的id相同
        /// </summary>
        public string external_userid { get; set; }
        #endregion

    }
}
