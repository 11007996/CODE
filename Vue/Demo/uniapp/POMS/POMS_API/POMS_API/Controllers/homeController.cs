using Microsoft.AspNetCore.Mvc;

namespace POMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class homeController
    {
        [HttpGet("add",Name = "add")]
        public int add(int a,int b)
        {
            return a + b;
        }
    }
}
