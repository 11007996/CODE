using HK.WebApi.Utilities;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HK.WebApi.Middleware
{
    /// <summary>
    /// 请求日志记录中间件
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly PathString[] _excludedPaths = new[]
        {
            new PathString("/swagger"),
        };

        /// <summary>
        /// 请求日志记录中间件构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 请求处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // 检查是否为排除的路径
            if (_excludedPaths.Any(p => context.Request.Path.StartsWithSegments(p)))
            {
                await _next(context);
                return;
            }
            var stopwatch = Stopwatch.StartNew();

            var flag = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            // 记录请求信息
            var request = context.Request;
            var requestLog = new
            {
                //Time = DateTime.Now,
                Flag = flag,
                Path = request.Path.Value,
                request.Method,
                QueryString = WebUtility.UrlDecode(request.QueryString.Value),
                //Headers = FilterSensitiveHeaders(request.Headers),
                Body = await ReadRequestBodyAsync(request)
            };

            _logger.LogDebug("Request: {Request}", JsonSerializer.Serialize(requestLog, JsonOptionsProvider.GlobalJsonOptions));

            // 保存原始响应流
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                // 执行请求
                await _next(context);

                // 记录响应信息
                var response = context.Response;
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);

                var responseLog = new
                {
                    //Time = DateTime.Now,
                    Flag = flag,
                    //response.StatusCode,
                    Duration = stopwatch.ElapsedMilliseconds,
                    //Body = SanitizeSensitiveData(responseBodyText)
                    Body = responseBodyText
                };

                _logger.LogDebug("Response: {Response}", JsonSerializer.Serialize(responseLog, JsonOptionsProvider.GlobalJsonOptions));

                // 将响应内容复制回原始流
                await responseBody.CopyToAsync(originalBodyStream);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        /// <summary>
        /// 过滤掉敏感头
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private Dictionary<string, string> FilterSensitiveHeaders(IHeaderDictionary headers)
        {
            // 不需要记录的敏感头
            var sensitiveHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Accept","Authorization", "Cookie", "Set-Cookie","User-Agent", "Accept-Encoding", "Accept-Language", "Cache-Control", "Pragma", "Referer", "sec-ch-ua-platform", "sec-ch-ua", "sec-ch-ua-mobile", "sec-fetch-site", "sec-fetch-mode", "sec-fetch-dest", "priority"
            };
            return headers.Where(h => !sensitiveHeaders.Contains(h.Key, StringComparer.OrdinalIgnoreCase)).ToDictionary(h => h.Key, h => string.Join("; ", h.Value));
        }

        /// <summary>
        /// 读取请求体
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            string bodyText = string.Empty;
            // 仅处理表单数据
            if (request.HasFormContentType)
            {
                var form = await request.ReadFormAsync();
                // 获取文件数量
                int fileCount = form.Files.Count;
                var parameters = form.Keys.Where(key => !form.Files.Any(file => file.Name == key)).ToDictionary(key => key, key => form[key].ToString());
                var paramText = string.Join(", ", parameters.Select(p => $"{p.Key}={p.Value}"));
                bodyText = $"FormParas:{paramText}, Files: {fileCount}";
            }
            else
            {
                var buffer = new byte[Math.Min(20 * 1024, request.ContentLength ?? 0)]; // 限制最大读取20kb
                var bytesRead = await request.Body.ReadAsync(buffer, 0, buffer.Length);
                bodyText = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            // 重置请求流位置
            request.Body.Position = 0;

            //return SanitizeSensitiveData(bodyText);
            return bodyText;
        }

        /// <summary>
        /// 清理敏感数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string SanitizeSensitiveData(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // 替换敏感字段（如密码、信用卡号）
            var sensitivePatterns = new Dictionary<string, string>
            {
                { @"""password""\s*:\s*""[^""]*""", @"""password"":""***""" },
                { @"""token""\s*:\s*""[^""]*""", @"""token"":""***""" }
            };

            foreach (var pattern in sensitivePatterns)
            {
                input = Regex.Replace(input, pattern.Key, pattern.Value, RegexOptions.IgnoreCase);
            }

            return input;
        }
    }
}
