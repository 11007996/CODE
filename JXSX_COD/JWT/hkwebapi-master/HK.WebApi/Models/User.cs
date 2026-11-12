namespace HK.WebApi.Models
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// 密码哈希值
        /// </summary>
        public string PasswordHash { get; set; }
        
        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }
        
        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }
        
        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}    