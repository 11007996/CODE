using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC260625.Models;
using System.Net;

namespace MVC260625.Controllers
{
    public class HomeController : Controller
    {
        #region 内置对象
        #region Get请求字符串
        public ActionResult Index()
        {
            //return Content(Request.MapPath("~/"+Request.QueryString["path"]));  //虚拟路径，项目文件夹为当前路径，即Startup.cs所在文件夹,Request.MapPath可以导航到本项目文件路径，Server.MapPath可以导航到站外的路径
            //return Content(Request["name"]);
            //return View();
            return Content("OK");
        }

        public ActionResult stringGet(string name)
        {
            return Content(name);
        }
        public ActionResult txtData()
        {
            ViewBag.path = "fileText.txt";
            return View();
        }
        

        
        #endregion

        #region POST请求字符串
        public ActionResult PostData()
        {
            return Content(Request.Form["name"]);
        }
        public ActionResult FIleData()  //post上传文件
        {
            //Request.MapPath()，相对路径转换为绝对路径
            Request.Files["file"].SaveAs(Request.MapPath("../filepath/" + Request.Files["file"].FileName));
            return Content(Request.MapPath("../filepath"));
        }
        #endregion

        #region 响应
        public ActionResult ReponseData()
        {
            //Response.Write("hello world");   //服务器向客户端发送数据
            //Response.Redirect("https://www.doubao.com/chat/38432249859359490?channel=bing_sem");   //重定向，可跨域重定向
            //return Content(Response.StatusCode.ToString());   //返回状态码
            return Content(Response.StatusCode.ToString());
        }
        #endregion

        #region 请求头
        public ActionResult RequestHeader()
        {
            return Content(Request.Headers["token"]);  //浏览器向服务器发送请求头
        }
        #endregion

        #region Cookies
        public ActionResult CookiesSet()
        {
            Response.Cookies.Add(new HttpCookie("token")   //服务器向浏览器发送Cookies
            {
                Value = "123456",
                Expires = DateTime.Now.AddDays(1)
            }) ;
            return Content("OK");
        }

        public ActionResult CookiesGet()
        {
            return Content(Request.Cookies["token"].Value);
        }
        public ActionResult CookiesClear()
        {
            Response.Cookies.Add(new HttpCookie("token")
            {
                Expires = DateTime.Now.AddDays(-1)
            });
            return Content("OK");
        }
        #endregion

        #region Application
        public ActionResult ApplicationData()
        {
            HttpContext.Application["user"] = "jack";
            return Content("ok");
        }
        public ActionResult ApplicationDataGet()
        {
            return Content(HttpContext.Application["user"].ToString());
        }
        #endregion

        #region Server
        public ActionResult ServerDemo()
        {
            Server.Transfer("/transferPage.aspx");  //转发，只能转发到同一个项目，地址不变但内容转发
            return Content("");
        }
        #endregion

        #region Session
        //Session 存储于服务器，只存储账号等少量重要数据，每个会话保存单独的Session
        public ActionResult SessionData()
        {
            Session["user"] = "session string";
            return Content(Session["user"].ToString());
        }
        public ActionResult SessionGet()
        {
            return Content(Session["user"].ToString());
        }
        public ActionResult SessionClear()
        {
            Session.Abandon();
            return Content(Session["user"].ToString());
        }
        #endregion
        #endregion

        #region 模型传参视图
        //[HttpPost]
        //[ValidateAntiForgeryToken]  //强制验证身份令牌
        public ActionResult viewData(Models.fileTextModels file)
        {
            if(ModelState.IsValid)   //验证模型数据是否符合要求
            {
                return View(file);
            }
            else
            {
                //return Content("NG");
                ModelState.AddModelError("error", "数据状态错误");      //向视图@Html.ValidationSummary() 传递错误信息
                return View(file);
            }
        }
        #endregion

        #region 视图语法
        public ActionResult showView()
        {
            //return Redirect("https://www.doubao.com/chat/38419958087689218");   //重定向，跳到对应网站,Response.Redirect的封装
            //Response.Redirect("https://www.doubao.com/chat/38419958087689218");   //重定向，跳到对应网站
            //return File(@"C:\Users\mh.guo\Desktop\6月份排班表.png","image/png");    //在浏览器中打开某个文件
            //return File(@"C:\Users\mh.guo\Desktop\新建文本文档.txt", "text/plain");
            //return RedirectToAction("Index","Home");   //跳转到本项目的某个方法
            //return new HttpStatusCodeResult(HttpStatusCode.NotFound);    //发送状态码
            //return Json(new { name = "aaa", age = 12, shell = "hignt" }, JsonRequestBehavior.AllowGet);   //生成格式文本
            return Json(new fileTextModels { name = "aaa", age = 12, pwd = "hignt" }, JsonRequestBehavior.AllowGet);     //生成格式文本

        }

        public PartialViewResult partView()
        {
            return PartialView();
        }

        public ActionResult fileSave(HttpPostedFileBase file)
        {
            string fileName = DateTime.Now.Ticks + file.FileName;
            string path = "";
            file.SaveAs(Request.MapPath(fileName));
            return Content(fileName);
        }

        #endregion
    }

    #region Pages帮助器
    /*
     * ASP.NET 帮助器
     * WebGrid 帮助器:简化了显示数据的方式,制作HTML表格 ；var grid = new WebGrid(data);
     * Chart 帮助器：显示不同类型的带有多种格式化选项和标签的图表图像 ；var myChart = new Chart(width: 600, height: 400)
     * WebMail 帮助器：供了使用SMTP（Simple Mail Transfer Protocol 简单邮件传输协议）发送电子邮件的功能。  WebSecurity.InitializeDatabaseConnection("Users", "UserProfile", "UserId", "Email", true);WebMail.SmtpServer = "smtp.example.com";
     * WebImage 帮助器：提供了管理网页中图像的功能。
     * 第三方帮助器
     * */
    #endregion

    #region HTML 帮助器
    /*
     * @Html.ActionLink("About this Website", "About")
     * BeginForm()
     **   EndForm()
     **   TextArea()
     **   TextBox()
     **   CheckBox()
     **   RadioButton()
     **   ListBox()
     **   DropDownList()
     **   Hidden()
     **   Password()
     * */
    #endregion
}