using System.Security.Cryptography;
using System.Text;
using HK.WebApi.Models;

namespace HK.WebApi.Services
{
    /// <summary>
    /// 用户服务实现，负责用户管理和认证
    /// </summary>
    public class UserService
    {
        // 模拟数据库中的用户数据
        private List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = HashPassword("admin123"),
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "user",
                PasswordHash = HashPassword("user123"),
                Role = "User"
            }
        };

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>认证成功返回用户对象，失败返回 null</returns>
        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username);

            // 检查用户名和密码
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// 根据刷新令牌获取用户
        /// </summary>
        /// <param name="refreshToken">刷新令牌</param>
        /// <returns>用户对象</returns>
        public User GetByRefreshToken(string refreshToken)
        {
            return _users.SingleOrDefault(u => u.RefreshToken == refreshToken);
        }

        /// <summary>
        /// 哈希密码
        /// </summary>
        /// <param name="password">明文密码</param>
        /// <returns>哈希后的密码</returns>
        private static string HashPassword(string password)
        {
            using var hmac = new HMACSHA256();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordSalt = hmac.Key;

            return $"{Convert.ToBase64String(passwordSalt)}:{Convert.ToBase64String(passwordHash)}";
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">明文密码</param>
        /// <param name="storedHash">存储的哈希密码</param>
        /// <returns>密码匹配返回 true，否则返回 false</returns>
        private static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            using var hmac = new HMACSHA256(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(hash);
        }
    }
}    