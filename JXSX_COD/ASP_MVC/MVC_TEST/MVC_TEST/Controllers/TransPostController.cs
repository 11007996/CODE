using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_TEST.Controllers
{
    public class TransPostController : Controller
    {
        // GET: TransPost
        public ActionResult Index()
        {

            return Content("hi");
        }
        public ActionResult UpIndex()
        {
            return View("Upload");
        }
        public ActionResult Upload(HttpPostedFileBase uploadFile)
        {
            // 1 判断是否有文件
        if (uploadFile == null || uploadFile.ContentLength <= 0)
        {
            ViewBag.Msg = "请选择文件";
            return View("UpIndex");
        }

        // 获取原始文件名
        string fileName = Path.GetFileName(uploadFile.FileName);

        // 保存到服务器物理路径
        // Server.MapPath 将虚拟路径转为服务器磁盘真实路径
        string saveDir = Server.MapPath("~/UploadFiles");

        // 如果文件夹不存在，创建文件夹
        if (!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }

        // 避免重名：可以拼接时间
        //string newFileName = "{DateTime.Now:yyyyMMddHHmmss}_{fileName}";
        string fullPath = Path.Combine(saveDir, fileName);

        // 保存文件
        uploadFile.SaveAs(fullPath);

        ViewBag.Msg = "上传成功，保存文件名："+fileName;
        return View();
        }
    }
}