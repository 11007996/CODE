using Microsoft.IdentityModel.Tokens;
using POMS_API.Helper.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace POMS_API.Models
{
    public class BaseClass
    {
        /// <summary>
        /// 登陆账号密码
        /// </summary>
        public class LoginInput
        {
            public required string UserNo { get; set; }
            public required string Password { get; set; }
            public required string Appversion {  get; set; }
        }
        /// <summary>
        /// 接口的返回信息
        /// </summary>
        public class APIReturn
        {
            public string msg { get; set; }
            public int status { get; set; }
            public List<object> data { get; set; } = new List<object>();

        }

    }
}
