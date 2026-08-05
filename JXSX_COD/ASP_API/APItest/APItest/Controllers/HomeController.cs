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
namespace APItest.Controllers
{

    public class HomeController : ApiController
    {
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


        [HttpGet]
        public int intt(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }
}
