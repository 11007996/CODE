using Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LxMail.Controllers
{
    [Route("Home")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "LxMail启动成功";
        }
    }
}