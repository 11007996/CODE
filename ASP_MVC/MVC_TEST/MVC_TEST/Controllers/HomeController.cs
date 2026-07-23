using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_TEST.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Geeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }

        [HttpGet]
        public ActionResult RsvpForm()
        {
            //Response.Redirect("RsvpForm");
            return View();
        }

        [HttpPost]
        public ActionResult RsvpForm(Models.GuestRespon guestrespon)
        {
            if(ModelState.IsValid)     //检查模型数据是否有效
            {
                return View("Thinks", guestrespon);
            }
            else
            {
                //return View("valuecheck");
                return View();
            }
        }
    }
}