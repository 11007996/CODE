using Microsoft.IdentityModel.Tokens;
using System.Data;

//using POMS_API.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static POMS_API.Models.BaseClass;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POMS_API.Helper.Services
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string UserNo { get; set; }
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
                UserId = 1,
                UserNo = "11007996",
                PasswordHash = HashPassword("12qw12qw"),
                Role = "admin"
            },
            new User
            {
                UserId = 2,
                UserNo = "user",
                PasswordHash = HashPassword("user123"),
                Role= "user"
            }
        };

        private User GetUserByName(string userno)
        {
            User user = new User();
            string sSql = "select EMP_ID,EMP_NAME,PASSWD from sys_emp where EMP_NO=:emp";
            var paras = new Dictionary<string, Object>();
            paras.Add("emp", userno);
            DataTable dt = DBhelper.mesdbhelper.ExecuteSQLList(sSql, paras).Tables[0];
            if(dt.Rows.Count > 0)
            {
                user.UserId =Convert.ToInt32( dt.Rows[0][0].ToString());
                user.Username = dt.Rows[0][1].ToString();
                user.PasswordHash = dt.Rows[0][2].ToString();
                return user;
            }
            else
            {
                return user;
            }
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>认证成功返回用户对象，失败返回 null</returns>
        public User Authenticate(string userno, string password,string version)
        {
            var user = _users.SingleOrDefault(u => u.UserNo == userno);
            User raw_uer = GetUserByName(userno);

            // 检查用户名和密码
            if (raw_uer == null || !VerifyPassword(password, raw_uer.PasswordHash))
            {
                return null;
            }
            return raw_uer;
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

    public class TokenDate
    {
        public string token { set; get; }
        public DateTime expiration { set; get; }
        public TokenDate(string token,DateTime expiration)
        {
            this.token = token;
            this.expiration = expiration;
        }

    }
    public class TokenService
    {
        //private static readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("POMS_API_SecretKey"));
        string tokenKey = "yrvL0xXyCruX2ljFqE7y0OhM_J7L9_BYjXE-gttJJes";
        public TokenDate GeTokens(int userId, string userno, string version)
        {
            try
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                        new Claim(ClaimTypes.Name, userno),
                        new Claim(ClaimTypes.Version, version)
                    };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "https://www.luxshare-ict.com/",
                    Audience = "https://www.luxshare-ict.com/",
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new TokenDate(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
                
            }
            catch
            {
                return null;
            }
        }

        public class ReturnInfo
        {
            public string msg { get; set; }
            public int status { get; set; }

        }

        public ReturnInfo CheckToken(string jwtData)
        {
            ReturnInfo info = new ReturnInfo();
            info.msg = "身份验证成功";
            info.status = 0;
            try
            {
                if (string.IsNullOrEmpty(jwtData))
                {
                    info.status = 102;
                    info.msg = "身份验证为空，请重新登录";
                    return info;
                }
                string token = jwtData.Replace("Bearer ", "");
                var key = Encoding.ASCII.GetBytes(tokenKey);
                var vaildateParameter = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "https://www.luxshare-ict.com/",
                    ValidateAudience = true,
                    ValidAudience = "https://www.luxshare-ict.com/",
                    ClockSkew = TimeSpan.Zero
                };

                var jwtToken = new JwtSecurityTokenHandler().ValidateToken(token, vaildateParameter, out SecurityToken validatedtoken);
                var jwaPayload = ((JwtSecurityToken)validatedtoken).Payload.SerializeToJson();
            }
            catch (SecurityTokenExpiredException)
            {
                //token过期
                info.status = 103;
                info.msg = "身份已过期";
            }
            catch (SecurityTokenException)
            {
                //token错误
                info.status = 104;
                info.msg = "身份验证错误";
            }
            catch (Exception e)
            {
                info.status = 1;
                info.msg = "身份验证失败，请重新登录";
            }


            return info;
        }
    }
}   