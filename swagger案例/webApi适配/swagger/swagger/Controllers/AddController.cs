using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace swagger.Controllers
{
    /// <summary>
    /// 计算
    /// </summary>
    public class AddController : ApiController
    {
        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Get(int a,int b)
        {
            return a + b;
        }
    }
}
