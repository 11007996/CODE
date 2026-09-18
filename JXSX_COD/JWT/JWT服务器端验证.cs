//验证客户端发过来的JWT
private ReturnInfo CheckToken()
        {
            ReturnInfo info = new ReturnInfo();
            info.msg = "身份验证成功";
            info.status = 0;
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Request.Headers["Authorization"].ToString()))
                {
                    info.status = 102;
                    info.msg = "身份验证失败，请重新登录";
                    return info;
                }
                token = HttpContext.Current.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");   //客户端发送Token需以Bearer 开头
                var key = Encoding.ASCII.GetBytes(config.tokenKey);
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
                info.status = 101;
                info.msg = "身份已过期";
            }
            catch (SecurityTokenException)
            {
                //token错误
                info.status = 102;
                info.msg = "身份验证错误";
            }
            catch (Exception e)
            {
                info.status = 1;
                info.msg = e.Message;
            }
            return info;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
namespace PDA_MESA.Models
{
    public class config
    {
        // 用于签名的密钥
        private static readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"));  //私钥
        public static string tokenKey = "bqsid123k12s0h1d3uhf493fh02hdd102h9s3h38ff";   //公钥
        // 生成Token
        public static string GenerateToken(string username, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim("UserId", userId.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(1),   //有效期
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)    //Sha256加密算法
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // 验证Token
        public static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // 可以根据需要设置更多的验证参数
                }, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}