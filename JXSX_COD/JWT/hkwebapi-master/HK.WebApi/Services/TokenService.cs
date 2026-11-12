using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using HK.WebApi.Models;
using System.Security.Cryptography;
using HK.WebApi.Utilities;

namespace HK.WebApi.Services
{
    /// <summary>
    /// 令牌服务实现，负责生成和验证 JWT 令牌
    /// </summary>
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jwtSettings"></param>
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// 生成访问令牌和刷新令牌
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="username">用户名</param>
        /// <param name="role">用户角色</param>
        /// <returns>令牌模型</returns>
        public TokenModel GenerateTokens(int userId, string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var accessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
            var refreshToken = GenerateRefreshToken();

            CacheHelper.Set_AbsoluteExpire(userId.ToString(), refreshToken, TimeSpan.FromDays(_jwtSettings.RefreshTokenExpirationDays));

            return new TokenModel
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken,
                Expiration = accessToken.ValidTo
            };
        }

        /// <summary>
        /// 使用刷新令牌刷新访问令牌
        /// </summary>
        /// <param name="accessToken">当前访问令牌</param>
        /// <param name="refreshToken">刷新令牌</param>
        /// <returns>新的令牌模型</returns>
        public TokenModel RefreshTokens(string accessToken, string refreshToken)
        {
            //try
            //{
            var user = GetUserFromExpiredToken(accessToken);
            var refreshTokenCache = CacheHelper.Get<string>(user.Id.ToString());
            if (refreshTokenCache != refreshToken)
            {
                throw new SecurityTokenException("刷新令牌过期");
            }
            // 生成新的刷新令牌
            var newRefreshToken = GenerateRefreshToken();
            // 更新刷新令牌
            CacheHelper.Set_AbsoluteExpire(user.Id.ToString(), newRefreshToken, TimeSpan.FromDays(_jwtSettings.RefreshTokenExpirationDays));
            // 生成新的访问令牌
            return GenerateTokens(user.Id, user.Username, user.Role);
            //}
            //catch (Exception ex)
            //{
            //    throw new SecurityTokenException("Token refresh failed: " + ex.Message);
            //}
        }

        /// <summary>
        /// 从过期的访问令牌中获取用户名
        /// </summary>
        /// <param name="token">访问令牌</param>
        /// <returns>用户名</returns>
        public User GetUserFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return new User { Id = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)), Username = principal.Identity.Name, Role = principal.FindFirstValue(ClaimTypes.Role) };
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        /// <returns>刷新令牌字符串</returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}