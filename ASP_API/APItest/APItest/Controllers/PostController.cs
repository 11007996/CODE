using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using APItest.Models;
using System.Text;

namespace APItest.Controllers
{
    public class PostController : ApiController
    {
        /*
        [HttpPost]
        //接收表单参数
        public HttpResponseMessage postform(postForm pf)
        {
            postForms pfs = new postForms();
            pfs.a = pf.a;
            pfs.b = pf.b;
            pfs.c = pfs.a + pfs.b;
            string json = JsonConvert.SerializeObject(pfs);

            //返回json数
            return new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };
        }
        */
        
        [HttpPost]
        //接收json参数
        public int postJson([FromBody]object json)
        {
            JObject jobject = JObject.Parse(json.ToString());
            //JObject jo = (JObject)JsonConvert.DeserializeObject(obj);
            string a = jobject["a"].ToString();
            string b = jobject["b"].ToString();
            int c = int.Parse(a) + int.Parse(b);
            return c;
        }
         
    }
}
