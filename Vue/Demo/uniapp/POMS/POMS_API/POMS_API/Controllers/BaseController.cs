using Microsoft.AspNetCore.Mvc;
using POMS_API.Helper;
using POMS_API.Helper.Services;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;
using static POMS_API.Models.BaseClass;

namespace POMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
            DBhelper.mesdbhelper.connString = configuration.GetConnectionString("DefaultConnection");
        }

        #region 登陆
        private readonly UserService _userService = new UserService();
        private readonly TokenService _tokenService= new TokenService();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginInput">登陆实体类</param>
        /// <returns></returns>
        [HttpPost("Login", Name = "Login")]
        public APIReturn Login(LoginInput loginInput)
        {
            APIReturn apiReturn = new APIReturn();
            var user = _userService.Authenticate(loginInput.UserNo, loginInput.Password);
            if (user == null)
            {
                apiReturn.status = 101;
                apiReturn.msg = "用户名或密码错误";
                return apiReturn;
            }

            TokenDate tokens = _tokenService.GeTokens(user.UserId, user.Username,loginInput.Appversion);
            if (tokens == null)
            {
                apiReturn.status = 102;
                apiReturn.msg = "获取JWT失败";
                return apiReturn;
            }
            else
            {
                apiReturn.status = 0;
                apiReturn.msg = "登陆成功";
                apiReturn.data.Add(tokens);
                apiReturn.data.Add(new Dictionary<string, int> { { "userId", user.UserId } });
                return apiReturn;
            }
        }
        #endregion

        #region 获取APP程序最新版本号
        /// <summary>
        /// 从后台获取APP程序最新版本号及更新时间  
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAppVersion", Name = "getAppVersion")]
        public APIReturn getAppVersion()
        {
            string sSql = "select PARAM_VALUE,UPDATE_TIME from poms.sys_base_param where PROGRAM='POMS' and PARAM_NAME='app-version' and ENABLED='Y'";
            DataTable dt = DBhelper.mesdbhelper.ExecuteDataTable(sSql);
            if (dt.Rows.Count>0)
            {
                APIReturn apiReturn = new APIReturn();
                apiReturn.status = 0;
                apiReturn.msg = "OK";
                apiReturn.data.Add(dt.Rows[0][0].ToString());
                apiReturn.data.Add(dt.Rows[0][1].ToString());
                return apiReturn;
            }
            else
            {
                APIReturn apiReturn = new APIReturn();
                apiReturn.status = 201;
                apiReturn.msg = "APP程序版本号未维护，联系IT运维人员";
                return apiReturn;
            }


        }
        #endregion

        #region 验证Token
        /// <summary>
        /// 验证Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckToken", Name = "CheckToken")]
        public APIReturn CheckToken()
        {
            TokenService tokenService = new TokenService();
            string jwt = HttpContext.Request.Headers["Authorization"];
            TokenService.ReturnInfo returninfo = tokenService.CheckToken(jwt);
            APIReturn apiReturn = new APIReturn();
            apiReturn.status = returninfo.status;
            apiReturn.msg = returninfo.msg;
            return apiReturn;
        }
        
        #endregion
    }
}
