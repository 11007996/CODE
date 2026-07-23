namespace EAM.Common.Wechat.Model
{
    /// <summary>
    /// 获取token接口返回对象
    /// </summary>
    public class WxTokenDto : BaseResultDto
    {
        /// <summary>
        /// 获取到的凭证，最长为512字节
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }
    }
}