namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 接口调用时使用的登入配置
    /// </summary>
    public class EAMLoginConfigDto
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登入凭证过期时间，分钟
        /// </summary>
        public int Expire { get; set; } = 720;
    }
}