using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace APItest.Controllers
{

    public class HomeController : ApiController
    {
        [HttpGet]
        public string index()
        {
            int a = 3;
            int b = 4;
            string c = (a + b) + "aaaa";
            return c;
        }


        [HttpGet]
        public int intt(int a, int b)
        {
            int c = a + b;
            return c;
        }
    }
}
