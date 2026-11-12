namespace HK.WebApi.Models
{
    /// <summary>
    /// 刷新令牌请求模型
    /// </summary>
    public class RefreshTokenRequest
    {
        /// <summary>
        /// 当前访问令牌
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
    }
}    