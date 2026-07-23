using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace WebAppi.Controllers
{
    public class PersonController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value" + id;
        }

        public string Get(string name, string age)
        {
            return "name:" + name + "," + "age:" + age;
        }

        //public async Task<string> Gett(int age)
        //{
        //    HttpClient hc = new HttpClient();
        //    string txt =  await hc.GetStringAsync("http://www.xinlang.com");
        //    txt = txt.Substring(0, 30);
        //    Console.WriteLine(age);
        //}

        public string Post([FromBody]string value)
        {
            return "收到Post,value=" + value;
        }
    }
}
