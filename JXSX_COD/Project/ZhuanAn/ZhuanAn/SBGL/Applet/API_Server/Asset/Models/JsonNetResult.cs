using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asset.Models
{
    /// <summary>
    /// JSON结果返回对象
    /// </summary>
    public class JsonNetResult : JsonResult
    {
        /// <summary>
        /// 实例化JSON结果返回对象
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// <returns>JSON结果对象</returns>
        public JsonNetResult(object data)
            : this(data, JsonRequestBehavior.DenyGet)
        {
        }

        /// <summary>
        /// 实例化JSON结果返回对象
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// <param name="jsonRequestBehavior">设置是否允许GET请求获取JSON结果对象</param>
        /// <returns>JSON结果对象</returns>
        public JsonNetResult(object data, JsonRequestBehavior jsonRequestBehavior)
        {
            this.Data = data;
            this.JsonRequestBehavior = jsonRequestBehavior;
            this.Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //忽略循环引用
                DateFormatString = "yyyy-MM-dd HH:mm:ss", //输出的日期格式
                ContractResolver = new CamelCasePropertyNamesContractResolver() //设置属性的名称为“驼峰命名法”
            };
        }

        /// <summary>
        /// JSON序列化设置对象
        /// </summary>
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>
        /// 向响应流返回结果方法
        /// </summary>
        public override void ExecuteResult(ControllerContext context)
        {
            //判断当前请求是否为GET且判断是否允许GET获取JSON，不允许就报错
            if ("GET".Equals(context.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase) &&
                this.JsonRequestBehavior == JsonRequestBehavior.DenyGet)
                throw new Exception("不允许GET请求返回JSON数据！");

            //判断是否改变响应数据编码
            if (this.ContentEncoding != null)
                context.HttpContext.Response.ContentEncoding = this.ContentEncoding;
            //如果当前需要序列化的数据为NULL就直接返回
            if (this.Data == null)
                return;
            //设置响应数据内容格式为 json
            context.HttpContext.Response.ContentType =
                string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
            //向响应流写入序列化的数据
            JsonSerializer.Create(Settings).Serialize(context.HttpContext.Response.Output, this.Data);
        }
    }
}