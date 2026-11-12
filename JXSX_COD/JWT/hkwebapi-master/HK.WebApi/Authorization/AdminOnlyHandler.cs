using Microsoft.AspNetCore.Authorization;

namespace HK.WebApi.Authorization
{
    /// <summary>
    /// 管理员权限
    /// </summary>
    public class AdminOnlyHandler : AuthorizationHandler<AdminOnlyRequirement>
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOnlyRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            else
            {
                var httpContext = context.Resource as HttpContext;
                httpContext.Items["AuthorizationFailureReason"] = "需要管理员权限";
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 管理员权限
    /// </summary>
    public class AdminOnlyRequirement : IAuthorizationRequirement { }
}
