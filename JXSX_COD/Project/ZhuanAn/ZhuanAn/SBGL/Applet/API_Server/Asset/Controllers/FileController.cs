using Asset.BLL;
using Asset.Filter;
using Asset.Model;
using Asset.Model.Enum;
using Asset.Models;
using Asset.Util;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Asset.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileController : BaseController
    {
        FileManager fileManager = new FileManager();
        private static readonly ILog Logger = log4net.LogManager.GetLogger(typeof(FileController));

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="replaceFileId"></param>
        /// <param name="fileClass"></param>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult UploadFile(int? replaceFileId, FileClassEnum? fileClass)
        {
            ResultMsg r = new ResultMsg();
            r.MsgCode = "0";
            string tempMsg = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    //解码，防止文件名称乱码导
                    string fileName = Uri.UnescapeDataString(Request.Files[0].FileName);
                    Stream stream = Request.Files[0].InputStream;
                    string userId = GetCurrUser().WorkCode;
                    if (fileManager.SaveOrReplaceFile(replaceFileId, fileClass, stream, fileName, userId, ref tempMsg))
                    {
                        r.MsgCode = "0";
                        r.MsgInfo = "上传文件成功";
                    }
                    else
                    {
                        r.MsgCode = "1";
                        r.MsgInfo = tempMsg;
                    }
                }
                else
                {
                    r.MsgCode = "1";
                    r.MsgInfo = "未获取到上传文件";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.MsgInfo = "上传失败，异常原因：" + ex.Message;
            }
            Logger.Info("上传文件，返回结果：" + JsonConvert.SerializeObject(r));
            return Json(r, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [MyAuthorize]
        public void DownloadFile(int? fileId)
        {
            try
            {
                string fileAlisName = string.Empty;
                FileInfo file = fileManager.GetFilePath((int)fileId, ref fileAlisName);
                if (file != null)
                {
                    if (file.Exists)
                    {
                        //filename不是真实文件名，需要做百分号编码处理
                        //添加响应头 
                        Response.AddHeader("Access-Control-Expose-Headers", "Content-Disposition,download-filename");
                        //添加响应头内容
                        Response.AddHeader("Content-disposition", $"attachment;filename=\"{fileAlisName}\";");
                        Response.AddHeader("download-filename", Path.GetFileName(file.Name));
                        Response.TransmitFile(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// 下载预览文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="previewType"></param>
        /// <returns></returns>
        [MyAuthorize]
        public void DownloadPreview(int? fileId)
        {
            try
            {
                string fileAlisName = string.Empty;
                FileInfo file = fileManager.GetPreviewFilePath((int)fileId, ref fileAlisName);
                if (file != null)
                {
                    if (file.Exists)
                    {
                        //filename不是真实文件名，需要做百分号编码处理
                        //添加响应头 
                        Response.AddHeader("Access-Control-Expose-Headers", "Content-Disposition,download-filename");
                        //添加响应头内容
                        Response.AddHeader("Content-disposition", $"attachment;filename=\"{fileAlisName}\";");
                        Response.AddHeader("download-filename", Path.GetFileName(file.Name));
                        Response.TransmitFile(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        /// <summary>
        /// 根据源文件ID,获取预览文件信息
        /// </summary>
        /// <param name="replaceFileId"></param>
        /// <param name="fileClass"></param>
        /// <returns></returns>
        [MyAuthorize]
        public JsonResult GetPreviewFiles(int? sourceFileId, FilePreviewTypeEnum? previewType)
        {
            ResultMsg r = new ResultMsg();
            r.MsgCode = "0";
            try
            {
                if (sourceFileId != null || previewType == null)
                {
                    //解码，防止文件名称乱码导
                    List<PreviewFileInfoDO> files = fileManager.GetPreviewFiles((int)sourceFileId, (FilePreviewTypeEnum)previewType);
                    if (files != null)
                    {
                        r.MsgCode = "0";
                        r.MsgInfo = "获取预览文件信息成功";
                        r.Data = files;
                    }
                    else
                    {
                        r.MsgCode = "1";
                        r.MsgInfo = "未获取到预览文件信息";
                    }
                }
                else
                {
                    r.MsgCode = "1";
                    r.MsgInfo = "未传参数[sourceFileId]或[previewType]";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                r.MsgCode = "1";
                r.MsgInfo = "获取预览文件失败，异常原因：" + ex.Message;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }



    }
}