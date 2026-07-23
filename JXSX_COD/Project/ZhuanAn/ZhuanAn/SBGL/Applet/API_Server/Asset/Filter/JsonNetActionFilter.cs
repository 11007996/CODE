using Asset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asset.Filter
{
    /// <summary>
    /// 将Action返回的JsonResult转换成JsonNetAction
    /// </summary>
    public class JsonNetActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //跨域的预请求处理，阻止其访问控制层的Action
            if (filterContext.HttpContext.Request.HttpMethod == "OPTIONS")
            {
                 var jsonNetResult = new JsonNetResult(null)
                {
                    ContentEncoding =null,
                    ContentType = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = null,
                    RecursionLimit =null
                };
                filterContext.Result = jsonNetResult;
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //判断是JsonResult对象
            if (filterContext.Result is JsonResult && !(filterContext.Result is JsonNetResult))
            {
                //得到原JsonResult对象
                var jsonResult = (JsonResult)filterContext.Result;
                //创建新的JsonNetResult对象
                var jsonNetResult = new JsonNetResult(jsonResult.Data)
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior,
                    MaxJsonLength = jsonResult.MaxJsonLength,
                    RecursionLimit = jsonResult.RecursionLimit
                };
                //将新的JsonNetResult对象赋值给返回结果
                filterContext.Result = jsonNetResult;
            }
        }
    }
}