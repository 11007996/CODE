using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OracleClient;
using System.Data;

namespace MVC_REPORT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string report(Models.request s)
        {
            string ser = s.sn;
            string wo = s.work_order;
            string pdline = s.pdline_name;
            string sqlRequest = "select work_order from sajet.g_sn_status where serial_number='" + s.sn + "'";
            DataTable dt = ClientUtils.ExecuteSQL(sqlRequest).Tables[0];
            wo = dt.Rows[0][0].ToString();
            return wo;
        }
    }
}