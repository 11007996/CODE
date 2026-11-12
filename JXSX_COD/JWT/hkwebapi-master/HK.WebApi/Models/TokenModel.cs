namespace HK.WebApi.Models
{
    /// <summary>
    /// 令牌模型，包含访问令牌和刷新令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// 访问令牌过期时间
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}    