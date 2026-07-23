using Asset.Model;
using Asset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asset.Controllers
{
    public class BaseController : Controller
    {
        public UserInfoDO GetCurrUser()
        {
#if DEBUG
            UserInfoDO loginUser = new UserInfoDO();
            loginUser.WorkCode = null;
            loginUser.UserName = "测试账号";
            return loginUser;
#endif
            string token = Request.Headers["Authorization"];
            if (token != null)
            {
                return (UserInfoDO)HttpContext.Cache[token];
            }
            return null;
        }
    }
}