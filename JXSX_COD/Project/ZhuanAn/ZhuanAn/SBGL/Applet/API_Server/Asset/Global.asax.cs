using Asset.BLL;
using Asset.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Asset
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitCacheToken();

            //日志文件 
            string isWriteLog = ConfigurationManager.AppSettings["IsWriteLog"];
            //判断是否开启日志记录
            if (isWriteLog == "1")
            {
                var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + ConfigurationManager.AppSettings["log4net"];
                var fi = new FileInfo(path);
                log4net.Config.XmlConfigurator.Configure(fi);
            }

        }

        /// <summary>
        /// 初始加载token缓存
        /// </summary>
        private void InitCacheToken()
        {
            UserService service = new UserService();
            DataTable usersDT = service.GetAllCacheTokenUser();

            if (usersDT != null)
            {
                Cache cache = HttpRuntime.Cache;
                foreach (DataRow row in usersDT.Rows)
                {
                    UserInfoDO u = new UserInfoDO();
                    u.WorkCode = Convert.ToString(row["WorkCode"]);
                    u.UserName = Convert.ToString(row["UserName"]);
                    u.UserRight = Convert.ToString(row["UserRight"]);
                    u.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                    u.ExpiresTime = Convert.ToDateTime(row["ExpiresTime"]);
                    string token = Convert.ToString(row["Token"]);
                    if (cache.Get(token) == null)
                        cache.Insert(token, u, null, u.ExpiresTime, TimeSpan.Zero);//缓存token
                }
            }
        }
    }
}
