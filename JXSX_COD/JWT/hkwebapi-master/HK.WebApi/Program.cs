using HK.WebApi.Authorization;
using HK.WebApi.Middleware;
using HK.WebApi.Models;
using HK.WebApi.Services;
using HK.WebApi.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var logger = LogManager.GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

// 配置日志
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    ////第3方日志中间件持久化过滤规则：在持久化时，过滤掉带有"System"命名空间的日志信息实例。
    //logger.AddFilter("System", LogLevel.Error);
    loggingBuilder.AddNLog("nlog.config");
});
// 配置 JWT 设置
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

// 配置 JWT 认证
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,// 默认会延迟5分钟有效期
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };

        options.IncludeErrorDetails = true;

        options.Events = new JwtBearerEvents
        {
            OnForbidden = context =>
            {
                if (context.HttpContext.Items.TryGetValue("AuthorizationFailureReason", out var reason))
                {
                    var message = reason?.ToString();
                    context.Response.StatusCode = 200;
                    var json = JsonSerializer.Serialize(ApiResult.Fail(403, message), JsonOptionsProvider.GlobalJsonOptions);
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(json);
                }
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                // 判断响应是否已经开始，如果已经开始，则不执行任何操作
                if (!context.Response.HasStarted)
                {
                    context.HandleResponse();

                    string errorMessage = "无令牌信息";
                    if (context.AuthenticateFailure != null)
                    {
                        // 根据异常类型定制错误信息
                        if (context.AuthenticateFailure is SecurityTokenExpiredException expiredEx)
                            errorMessage = "令牌已过期";
                        else if (context.AuthenticateFailure is SecurityTokenException)
                            errorMessage = "令牌无效";
                    }
                    var json = JsonSerializer.Serialize(ApiResult.Fail(401, errorMessage), JsonOptionsProvider.GlobalJsonOptions);
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(json);
                }
                return Task.CompletedTask;
            }
        };
    });

// 配置授权策略
builder.Services.AddAuthorizationBuilder().AddPolicy("AdminOnly", policy => policy.Requirements.Add(new AdminOnlyRequirement()));

// 注册自定义授权处理器
builder.Services.AddSingleton<IAuthorizationHandler, AdminOnlyHandler>();

// Microsoft.Extensions.Caching.StackExchangeRedis包
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("Redis:Configuration");
    options.InstanceName = builder.Configuration.GetValue<string>("Redis:InstanceName");
});

// 注册服务
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();

// 配置控制器和JSON序列化
builder.Services.AddControllers(options =>
{
    // 接口默认都需要授权，仅对公开接口添加[AllowAnonymous]
    options.Filters.Add(new AuthorizeFilter());
})
// 应用于控制器返回值和模型绑定
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
    //options.JsonSerializerOptions.WriteIndented = builder.Environment.IsDevelopment();
});

// 配置Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(builder.Configuration.GetValue<string>("Swagger:Version"), new OpenApiInfo { Title = builder.Configuration.GetValue<string>("Swagger:Title"), Version = builder.Configuration.GetValue<string>("Swagger:Version"), Description = builder.Configuration.GetValue<string>("Swagger:Description") });
    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    c.IncludeXmlComments(xmlPath, true);
    c.OrderActionsBy(o => o.RelativePath);
    // 添加 JWT 认证支持
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "使用 Bearer 方案的 JWT Authorization 标头",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
    });
});

var app = builder.Build();

//app.UseHttpsRedirection();

// 可直接访问已验证的用户身份，若在UseAuthentication上启用，则需修改用户信息获取方式
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseMiddleware<IPWhitelistMiddleware>();

// 验证 JWT，填充 HttpContext.User
app.UseAuthentication();
app.UseAuthorization();

// 配置请求管道
app.UseWhen(
    ctx => ctx.Request.Path.StartsWithSegments("/swagger"),
    appBuilder =>
    {
        appBuilder.Use(async (context, next) =>
        {
            // 支持运行时动态判断（可替换为 IOptionsMonitor 或数据库查询）
            var config = context.RequestServices.GetRequiredService<IConfiguration>();
            var isEnabled = config.GetValue<bool>("Swagger:Enable");

            if (!isEnabled)
            {
                context.Response.StatusCode = 200;
                context.Response.Headers["Cache-Control"] = "no-cache";
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync($@"
                    <html>
                        <body style='font-family: Arial, sans-serif; text-align: center; padding: 50px;'>
                            <h1>Swagger 已禁用</h1>
                            <p>API 文档在当前不可用。</p>
                        </body>
                    </html>");
                return;
            }
            await next();
        });
    });
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();