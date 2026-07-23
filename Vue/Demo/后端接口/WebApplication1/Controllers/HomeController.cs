using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage add(int a, int b)
        {
            res ress = new res();
            ress.resInt = a + b;

            HttpResponseMessage result = new HttpResponseMessage();
            result.Headers.Add("Access-Control-Allow-Origin", "*");   //解决跨域问题
            result.Content = new StringContent(ress.resInt.ToString());
            return result;
        }

        [HttpPost]
        //public HttpResponseMessage addd(res aa)
        //{
        //    res ress = new res();
        //    ress.resInt = aa.resInt * 2;
        //    HttpResponseMessage result = new HttpResponseMessage();
        //    result.Headers.Add("Access-Control-Allow-Origin", "*");   //解决跨域问题
        //    result.Content = new StringContent(ress.resInt.ToString());
        //    return result;
        //}

        public HttpResponseMessage addd(res res)
        {
            //res ress = new res();
            res.resInt = res.resInt * 2;
            HttpResponseMessage result = new HttpResponseMessage();
            result.Headers.Add("Access-Control-Allow-Origin", "*");   //解决跨域问题
            result.Content = new StringContent(JsonConvert.SerializeObject(res));
            return result;
        }
    }
}
