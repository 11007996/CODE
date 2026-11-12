using HK.WebApi.Models;
using HK.WebApi.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace HK.WebApi.Middleware
{
    /// <summary>
    /// 全局异常处理中间件，捕获所有未处理的异常并返回统一格式的错误响应
    /// </summary>
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 捕获异常
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // 记录错误日志
            _logger.LogError(ex, "全局异常捕获");

            // 设置请求成果响应，在返回结果中包含错误码及信息
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";

            var errorResponse = ApiResult.Fail((int)HttpStatusCode.InternalServerError, "处理请求时出错");

            // 根据不同的异常类型返回不同的HTTP状态码和错误信息
            if (ex is ArgumentNullException || ex is ArgumentException)
            {
                errorResponse = ApiResult.Fail((int)HttpStatusCode.BadRequest, "请求数据无效");
            }
            else if (ex is SecurityTokenException)
            {
                context.Response.Headers.Append("X-Token-Expired", "true");
                errorResponse = ApiResult.Fail((int)HttpStatusCode.Unauthorized, ex.Message);
            }

            //// 在开发环境中包含详细的错误信息
            //if (context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
            //{
            //    errorResponse.Message = ex.Message;
            //    errorResponse.Data = ex.StackTrace;
            //}

            // 将错误信息序列化为JSON并返回
            var json = JsonSerializer.Serialize(errorResponse, JsonOptionsProvider.GlobalJsonOptions);
            return context.Response.WriteAsync(json);
        }
    }
}