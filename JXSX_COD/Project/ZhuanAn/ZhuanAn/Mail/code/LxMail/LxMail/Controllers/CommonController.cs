using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Controllers;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Infrastructure.Model;
using LxMail.Models;
using LxMail.Models.Dto;
using LxMail.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LxMail.Controllers
{
    /// <summary>
    /// 公共模块
    /// </summary>
    [Route("[controller]/[action]")]
    public class CommonController : BaseController
    {
        private readonly OptionsSetting OptionsSetting;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly ISysFileService SysFileService;

        public CommonController(
            IOptions<OptionsSetting> options,
            IWebHostEnvironment webHostEnvironment,
            ISysFileService fileService)
        {
            WebHostEnvironment = webHostEnvironment;
            SysFileService = fileService;
            OptionsSetting = options.Value;
        }

        #region 上传

        /// <summary>
        /// 存储文件
        /// </summary>
        /// <param name="uploadDto">自定义文件名</param>
        /// <param name="storeType">上传类型1、保存到本地 2、保存到阿里云</param>
        /// <returns></returns>
        [HttpPost()]
        [Log(Title = "文件上传", BusinessType = BusinessType.INSERT, IsSaveRequestData = false, IsSaveResponseData = true)]
        public async Task<IActionResult> UploadFile([FromForm] UploadDto uploadDto, StoreType storeType = StoreType.LOCAL)
        {
            if (uploadDto?.File == null) throw new CustomException(ResultCode.PARAM_ERROR, "上传文件不能为空");
            SysFile file = new();
            IFormFile formFile = uploadDto.File;
            string fileExt = Path.GetExtension(formFile.FileName);//文件后缀
            long fileSize = formFile.Length;//文件大小
            if (fileSize > OptionsSetting.Upload.MaxSize * 1024 * 1024)
            {
                return ToResponse(ResultCode.CUSTOM_ERROR, "上传失败，文件过大");
            }
            if (!OptionsSetting.Upload.AllowedExt.Contains(fileExt))
            {
                return ToResponse(ResultCode.CUSTOM_ERROR, "上传失败，未经允许上传类型");
            }
            uploadDto.FileNameType = 3;//强制为自动生成随机文件名
            if (uploadDto.FileNameType == 1)
            {
                uploadDto.FileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            }
            else if (uploadDto.FileNameType == 3)
            {
                uploadDto.FileName = SysFileService.HashFileName();
            }
            switch (storeType)
            {
                case StoreType.LOCAL:
                    string savePath = Path.Combine(WebHostEnvironment.WebRootPath);
                    //if (uploadDto.FileDir.IsEmpty())
                    //{
                    //    uploadDto.FileDir = OptionsSetting.Upload.LocalSavePath;
                    //}
                    uploadDto.FileDir = OptionsSetting.Upload.LocalSavePath;//强制为配置的目录
                    file = await SysFileService.SaveFileToLocal(savePath, uploadDto.FileName, uploadDto.FileDir, HttpContext.GetName(), formFile);
                    break;

                case StoreType.REMOTE:
                    break;

                default:
                    break;
            }
            return SUCCESS(new
            {
                url = file.AccessUrl,
                fileName = file.FileName,
                realName = file.RealName,
                fileId = file.Id.ToString()
            });
        }

        #endregion 上传
    }
}