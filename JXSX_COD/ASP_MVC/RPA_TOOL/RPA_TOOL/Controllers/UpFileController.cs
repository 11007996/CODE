using RPA_TOOL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RPA_TOOL.Controllers
{
    public class UpFileController : Controller
    {
        // 导航页
        public ActionResult Index()
        {
            return View();
        }

        //上传下载页
        public ActionResult UpFile(uploadfileModel m)
        {
            ViewBag.Msg = m.msg;
            return View(m);
        }

        //上传功能
        public ActionResult SaveFile(HttpPostedFileBase uploadFile)
        {
            uploadfileModel um = new uploadfileModel();
            if (uploadFile == null || uploadFile.ContentLength <= 0)
            {
                um.msg = "请选择文件";
                um.flag = "NG";
                return RedirectToAction("UpFile", "UpFile", um);
            }
            if (!(uploadFile.FileName.ToUpper()=="SOP.XLSX") && !(uploadFile.FileName.ToUpper()=="BOM.XLSX"))
            {
                um.msg = "文件名无法识别";
                um.flag = "NG";
                return RedirectToAction("UpFile", "UpFile", um);
            }
            try
            {
            string fileName = Path.GetFileName(uploadFile.FileName);

            // 保存到服务器物理路径
            string saveDir = Server.MapPath("~/UploadFiles");


            // 如果文件夹不存在，创建文件夹
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            string fullPath = Path.Combine(saveDir, fileName);

            // 保存文件
            uploadFile.SaveAs(fullPath);
            
            um.msg = "上传成功";
            um.flag = "OK";
            return RedirectToAction("UpFile", "UpFile", um);
            }
            catch(Exception e)
            {
                um.msg = "上传异常";
                um.flag = "NG";
                return RedirectToAction("UpFile", "UpFile", um);
            }
        }

        public ActionResult transFileServeView(uploadfileModel m)
        {
            return View(m);
        }

        public ActionResult transFileServe(HttpPostedFileBase uploadFile)
        {
            uploadfileModel um = new uploadfileModel();
            if (uploadFile == null || uploadFile.ContentLength <= 0)
            {
                um.msg = "请选择文件";
                um.flag = "NG";
                return RedirectToAction("transFileServeView", "UpFile", um);
            }
            try
            {
                string fileName = Path.GetFileName(uploadFile.FileName);

                // 保存到服务器物理路径
                string saveDir = Server.MapPath("~/UploadFiles/ServeFile");


                // 如果文件夹不存在，创建文件夹
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }

                string fullPath = Path.Combine(saveDir, fileName);

                // 保存文件
                uploadFile.SaveAs(fullPath);

                um.msg = "上传成功";
                um.flag = "OK";
                return RedirectToAction("transFileServeView", "UpFile", um);
            }
            catch (Exception e)
            {
                um.msg = "上传异常";
                um.flag = "NG";
                return RedirectToAction("transFileServeView", "UpFile", um);
            }
        }

        //下载页
        public ActionResult downloadFile()
        {
            uploadfileModel um = new uploadfileModel();
            return View();
        }

        //下载功能
        public ActionResult DownloadInner(string fileName)
        {
            string safeName = Path.GetFileName(fileName);
            string saveDir = Request.MapPath("~/UploadFiles/download");
            string fullPath = Path.Combine(saveDir, safeName);
            return File(fullPath, "multipart/form-data", safeName);    //文件下载

        }

        //删除功能
        public ActionResult DeleteInner(string fileName)
        {
            string safeName = Path.GetFileName(fileName);
            string saveDir = Request.MapPath("~/UploadFiles/download");
            string fullPath = Path.Combine(saveDir, safeName);
            //return File(fullPath, "multipart/form-data", safeName);    //文件下载
            System.IO.File.Delete(fullPath);

            return RedirectToAction("UpFile", "UpFile", new uploadfileModel());
        }
    }
}