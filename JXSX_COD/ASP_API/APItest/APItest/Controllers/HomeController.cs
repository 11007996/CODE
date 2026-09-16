using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http.Headers;
using APItest.Models;
using System.Threading.Tasks;
namespace APItest.Controllers
{

    public class HomeController : ApiController
    {
        #region 后端设置跨域请求
        [HttpGet]
        public HttpResponseMessage index()
        {
            int a = 3;
            int b = 4;
            string c = (a + b) + "aaaa";

            var body = new { msg = c };
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, body);

            // 设置自定义头
            //response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:5173");   //表示只允许http://localhost:5173跨域访问
            response.Headers.Add("Access-Control-Allow-Origin", "*");   //表示允许所有人跨域访问

            // content头部
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;

            //return c;
        }
        #endregion


        #region HTTP请求
        [HttpGet]
        public HttpResponseMessage strShow(HttpRequestMessage request)
        {
            requClass req = new requClass();
            HttpResponseMessage response;

            IEnumerable<string> li;
            bool b =request.Headers.TryGetValues("token", out  li);
            if (!b)
            {
                req.msg = "token null";
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, req);
                return response;
            }
            else
            {
                string token = li.FirstOrDefault();
                if (token != "aaa")
                {
                    req.msg = "token error";
                    response = Request.CreateResponse(HttpStatusCode.Unauthorized, req);
                    return response;
                }
            }
            req.msg = "request OK";
            response = Request.CreateResponse(HttpStatusCode.OK, req);
            response.Headers.Add("header-quto", "qwertyuio");
            return response;
        }
        #endregion


        #region  异步接口
        [HttpGet]
        public async Task<string> str(string msg)
        {
            return await strA(msg);
        }
        private async Task<string> strA(string msg)
        {
            return msg + " async";
        }
        #endregion

        #region 返回图片地址给前端
        [HttpGet]
        public string getPic(string path)
        {
            return Url.Content("~/pic/" + path);
        }
        #endregion

    }
}
