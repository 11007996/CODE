using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace swagger
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var allowOrigins = "*";//最好来自配置文件夹
            var allowHeaders = "*";//最好来自配置文件夹
            var allowMethods = "*";//最好来自配置文件夹
            var globalCors = new System.Web.Http.Cors.EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods)
            {
                SupportsCredentials = true
            };
            config.EnableCors(globalCors);
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
