namespace HK.WebApi.Models
{
    /// <summary>
    /// JWT 配置信息
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 用于签名 JWT 的密钥
        /// </summary>
        public string SecretKey { get; set; }
        
        /// <summary>
        /// JWT 发行人
        /// </summary>
        public string Issuer { get; set; }
        
        /// <summary>
        /// JWT 接收人
        /// </summary>
        public string Audience { get; set; }
        
        /// <summary>
        /// 访问令牌过期时间（分钟）
        /// </summary>
        public int AccessTokenExpirationMinutes { get; set; }
        
        /// <summary>
        /// 刷新令牌过期时间（天）
        /// </summary>
        public int RefreshTokenExpirationDays { get; set; }
    }
}    