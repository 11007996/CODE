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
        #region 测试
        [HttpPost("test", Name = "test")]
        public APIReturn test(int a,int b)
        {
            TokenService.ReturnInfo returnInfo = new TokenService.ReturnInfo();
            string aa = Request.Headers["Authorization"].ToString();
            returnInfo = _tokenService.CheckToken(aa);
            APIReturn apiReturn = new APIReturn();
            //loginReturn.msg = "OK";
            //loginReturn.status = 0;
            apiReturn.msg = returnInfo.msg;
            apiReturn.status = returnInfo.status;
            apiReturn.data.Add("返回数据");
            return apiReturn;
        }
        #endregion

        public BaseController()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory).AddJsonFile("appsettings.json").Build();
            DBhelper.mesdbhelper.connString = configuration.GetConnectionString("DefaultConnection");
        }

        #region 登陆
        private readonly UserService _userService = new UserService();
        private readonly TokenService _tokenService= new TokenService();

        [HttpPost("Login", Name = "Login")]
        public APIReturn Login(LoginInput loginInput)
        {
            APIReturn apiReturn = new APIReturn();
            User user = _userService.Authenticate(loginInput.UserNo, loginInput.Password);
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
                return apiReturn;
            }
        }
        #endregion

        #region 获取APP程序最新版本号
        [HttpGet("getAppVersion", Name = "getAppVersion")]
        public APIReturn getAppVersion()
        {
            string sSql = "select PARAM_VALUE from poms.sys_base_param where PROGRAM='POMS' and PARAM_NAME='app-version' and ENABLED='Y'";
            DataTable dt = DBhelper.mesdbhelper.ExecuteDataTable(sSql);
            if (dt.Rows.Count>0)
            {
                APIReturn apiReturn = new APIReturn();
                apiReturn.status = 0;
                apiReturn.msg = "OK";
                apiReturn.data.Add(dt.Rows[0][0].ToString());
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
    }
}
