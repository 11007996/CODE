using HK.WebApi.Models;
using HK.WebApi.Utilities;
using System.Text.Json;

namespace HK.WebApi.Middleware
{
    /// <summary>
    /// IP白名单中间件，JWT认证之前使用，用于限制外部访问的IP地址
    /// </summary>
    public class IPWhitelistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _allowedIPs;

        /// <summary>
        /// IP白名单中间件
        /// </summary>
        /// <param name="next"></param>
        /// <param name="config"></param>
        public IPWhitelistMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _allowedIPs = config.GetSection("AllowedIPs:BeforeJWT").Get<string[]>();
        }

        /// <summary>
        /// IP白名单中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var remoteIp = context.Request.GetClientIp();
            if (!_allowedIPs.Any(ip => remoteIp.StartsWith(ip)) && _allowedIPs?[0] != "*")
            {
                var json = JsonSerializer.Serialize(ApiResult.Fail(401, "IP未授权"), JsonOptionsProvider.GlobalJsonOptions);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
                return;
            }

            await _next(context);
        }
    }
}
