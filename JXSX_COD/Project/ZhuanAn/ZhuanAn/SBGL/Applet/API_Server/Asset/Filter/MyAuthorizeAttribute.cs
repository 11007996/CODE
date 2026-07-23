using Asset.BLL;
using Asset.Model;
using Asset.Models;
using Asset.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Asset.Filter
{

    public class MyAuthorizeAttribute : AuthorizeAttribute
    {

        private readonly string TokenCacheTimes = ConfigurationManager.AppSettings["TokenCacheTimes"];
        UserService service = new UserService();
        /// <summary>
        /// 验证入口
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 验证核心代码
        /// </summary>
        /// <param name="httpContext">fbc8ZBLd5ZbtCogcY9NUVV4HZbPln1lb</param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
#if DEBUG
            //DEBUG时不检查权限
            return true;
#endif
            //前端请求api时会将token存放在名为"auth"的请求头中
            var token = httpContext.Request.Headers["Authorization"];
            string mothed = httpContext.Request.HttpMethod;
            if (mothed == "OPTIONS") return true;
            if (token == null) return false;

            try
            {
                //解析UserInfo
                UserInfoDO userinfo = JwtHelp.GetJwtDecode(token);

                //读取token缓存
                Cache cache = HttpRuntime.Cache;
                var cacheObj = cache[token];
                if (cacheObj != null)
                {
                    //比对工号是否相周
                    UserInfoDO caInfo = (UserInfoDO)cacheObj;
                    if (userinfo.WorkCode == caInfo.WorkCode && caInfo.ExpiresTime > DateTime.Now) return true;
                }
                else
                {
                    //从数据库中读取
                    UserInfoDO dbInfo = service.GetUserInfoByToken(token);
                    if (dbInfo != null && userinfo.WorkCode == dbInfo.WorkCode && dbInfo.ExpiresTime > DateTime.Now)
                    {
                        cache.Insert(token, dbInfo, null, dbInfo.ExpiresTime, TimeSpan.Zero);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 验证失败处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.HttpContext.Response.Redirect("/Home/Error");
            base.HandleUnauthorizedRequest(filterContext);
            ResultMsg msg = new ResultMsg()
            {
                MsgCode = "401",
                MsgInfo = "请登入"
            };
            filterContext.HttpContext.Response.StatusCode = 401;
            filterContext.HttpContext.Response.ContentType = "application/json";
            filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(msg));
        }


    }

}