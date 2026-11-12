using HK.WebApi.Authorization;
using HK.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HK.WebApi.Controllers
{
    /// <summary>
    /// 用户控制器，演示授权访问
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 获取所有用户（管理员权限）
        /// </summary>
        /// <returns>用户列表</returns>
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public ApiResult GetAll()
        {
            var users = new[]
            {
                new { Id = 1, Username = "admin", Role = "Admin" },
                new { Id = 2, Username = "user", Role = "User" }
            };

            return ApiResult.Success(users, users.Length);
        }

        /// <summary>
        /// 不认证的公共接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("public-all")]
        [AllowAnonymous]
        public ApiResult PublicAll()
        {
            return ApiResult.Success("这是一个不需要认证的公共接口");
        }


        /// <summary>
        /// 不认证的公共接口,模拟获取OpenId
        /// </summary>
        /// <returns></returns>
        [HttpGet("simulate-get-openid")]
        [AllowAnonymous]
        public string SimulateGetOpenId([FromQuery] string appid, [FromQuery] string secret)
        {
            return $@"{{
                    ""openid"": ""{appid}"",
                    ""session_key"": ""{secret}"",
                    ""unionid"": ""unionid_123456""
                }}";
        }

        /// <summary>
        /// 需要认证的公共接口
        /// </summary>
        /// <returns></returns>
        [HttpPost("public")]
        public ApiResult Public()
        {
            return ApiResult.Success("这是一个需要认证的公共接口");
        }

        /// <summary>
        /// IP白名单测试接口
        /// </summary>
        /// <returns></returns>
        [IPWhitelist]
        [AllowAnonymous]
        [HttpPost("CheckIp")]
        public ApiResult CheckIp()
        {
            return ApiResult.Success("测试成功");
        }

        /// <summary>
        /// 测试表单参数（application/x-www-form-urlencoded 或 multipart/form-data）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("form")]
        public IActionResult TestFormParams([FromForm] TestFormModel model)
        {
            return Ok(new
            {
                Message = "表单参数接收成功",
                Data = model
            });
        }

        /// <summary>
        /// 测试查询参数（URL参数）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public IActionResult TestQueryParams([FromQuery] TestQueryModel model)
        {
            return Ok(new
            {
                Message = "查询参数接收成功",
                Data = model
            });
        }

        /// <summary>
        /// 测试JSON Body参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("body")]
        public IActionResult TestBodyParams([FromBody] TestBodyModel model)
        {
            return Ok(new
            {
                Message = "JSON Body参数接收成功",
                Data = model
            });
        }
    }
    /// <summary>
    /// 表单参数模型
    /// </summary>
    public class TestFormModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public List<IFormFile> Avatar { get; set; }
    }

    /// <summary>
    /// 查询参数模型
    /// </summary>
    public class TestQueryModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// JSON Body参数模型
    /// </summary>
    public class TestBodyModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Tags { get; set; }
    }
}