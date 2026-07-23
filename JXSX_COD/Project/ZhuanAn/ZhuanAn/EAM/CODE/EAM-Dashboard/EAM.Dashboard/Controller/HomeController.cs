using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    [Route("api/[controller]")]
    public class HomeController : BaseController
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            return SUCCESS("启动成功");
        }
    }
}