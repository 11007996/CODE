using Microsoft.AspNetCore.Mvc;

namespace POMS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class homeController
    {
        [HttpGet("add",Name = "add")]
        public int add(int a,int b)
        {
            return a + b;
        }
    }
}
