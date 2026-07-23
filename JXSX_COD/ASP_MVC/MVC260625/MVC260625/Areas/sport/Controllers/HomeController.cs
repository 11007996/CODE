using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC260625.Areas.sport.Controllers
{
    public class HomeController : Controller
    {
        // GET: sport/Home
        public ActionResult Index()
        {
            return Content("另一个区域");
        }
    }
}