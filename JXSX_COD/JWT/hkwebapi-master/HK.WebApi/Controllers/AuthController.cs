using Microsoft.AspNetCore.Mvc;
using HK.WebApi.Models;
using HK.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using HK.WebApi.Utilities;
using System.Security.Claims;

namespace HK.WebApi.Controllers
{
    /// <summary>
    /// 认证控制器，处理用户登录、刷新令牌和注销
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="tokenService"></param>
        /// <param name="logger"></param>
        public AuthController(UserService userService, TokenService tokenService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel">登录模型</param>
        /// <returns>包含访问令牌和刷新令牌的响应</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public ApiResult Login([FromBody] LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                return ApiResult.Fail(401, "用户名或密码错误");
            }

            var tokens = _tokenService.GenerateTokens(user.Id, user.Username, user.Role);
            return ApiResult.Success(tokens);
        }

        /// <summary>
        /// 刷新访问令牌
        /// </summary>
        /// <param name="request">刷新令牌请求</param>
        /// <returns>新的访问令牌和刷新令牌</returns>
        [HttpPost("refresh-token")]
        public ApiResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokens = _tokenService.RefreshTokens(request.AccessToken, request.RefreshToken);
            return ApiResult.Success(tokens);
        }

        /// <summary>
        /// 注销令牌
        /// </summary>
        /// <returns>操作结果</returns>
        [HttpPost("revoke-token")]
        public ApiResult RevokeToken()
        {
            _logger.LogInformation($"用户 {User.Identity.Name} 正在注销刷新令牌{CacheHelper.Get<string>(User.FindFirstValue(ClaimTypes.NameIdentifier))}");
            // 移除刷新令牌
            CacheHelper.Remove(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return ApiResult.Success(null, "令牌已成功撤销");
        }
    }
}