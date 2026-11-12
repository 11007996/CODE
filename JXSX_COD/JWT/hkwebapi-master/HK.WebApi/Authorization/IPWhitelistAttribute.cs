using HK.WebApi.Models;
using HK.WebApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace HK.WebApi.Authorization
{
    /// <summary>
    /// IP白名单过滤器，JWT认证之后使用，便于控制仅内网访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class IPWhitelistAttribute : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// IP白名单过滤器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var remoteIp = context.HttpContext.Request.GetClientIp();
            var allowedIPs = config.GetSection("AllowedIPs:AfterJWT").Get<string[]>();

            if (!allowedIPs.Any(ip => remoteIp.StartsWith(ip)) && allowedIPs?[0] != "*")
            {
                context.Result = new ObjectResult(JsonSerializer.Serialize(ApiResult.Fail(401, "IP未授权"), JsonOptionsProvider.GlobalJsonOptions))
                {
                    StatusCode = 200
                };
                return;
            }

            await next();
        }
    }
}
