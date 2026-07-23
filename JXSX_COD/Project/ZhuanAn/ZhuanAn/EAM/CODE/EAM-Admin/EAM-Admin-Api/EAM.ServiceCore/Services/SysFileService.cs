using EAM.Common;
using EAM.Common.Model;
using EAM.Model.System;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [AppService(ServiceType = typeof(ISysFileService), ServiceLifetime = LifeTime.Transient)]
    public class SysFileService : BaseService<SysFile>, ISysFileService
    {
        private readonly ISysConfigService SysConfigService;
        private string domainUrl = AppSettings.GetConfig("ALIYUN_OSS:domainUrl");
        private OptionsSetting OptionsSetting;
        private IHttpContextAccessor _httpContextAccessor;

        public SysFileService(IHttpContextAccessor httpContextAccessor, ISysConfigService sysConfigService, IOptions<OptionsSetting> options)
        {
            SysConfigService = sysConfigService;
            OptionsSetting = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 存储本地
        /// </summary>
        /// <param name="fileDir">存储文件夹</param>
        /// <param name="rootPath">存储根目录</param>
        /// <param name="fileName">自定文件名</param>
        /// <param name="formFile">上传的文件流</param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<SysFile> SaveFileToLocal(string rootPath, string fileName, string fileDir, string userName, IFormFile formFile)
        {
            string fileExt = Path.GetExtension(formFile.FileName);
            fileName = (fileName.IsEmpty() ? HashFileName() : fileName) + fileExt;

            string filePath = GetDirPath(fileDir);
            string finalFilePath = Path.Combine(rootPath, filePath, fileName);
            long fileSize = formFile.Length;

            if (!Directory.Exists(Path.GetDirectoryName(finalFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(finalFilePath));
            }

            using (var stream = new FileStream(finalFilePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            string uploadUrl = OptionsSetting.Upload.UploadUrl;
            string accessPath = string.Concat(filePath.Replace("\\", "/"), "/", fileName);
            Uri baseUri = new(uploadUrl);
            Uri fullUrl = new(baseUri, accessPath);
            SysFile file = new(formFile.FileName, fileName, fileExt, fileSize, filePath.Replace("\\", "/"), userName)
            {
                StoreType = (int)StoreType.LOCAL,
                FileType = formFile.ContentType,
                FileUrl = finalFilePath.Replace("\\", "/"),
                AccessUrl = fullUrl.AbsoluteUri
            };
            file.Id = await InsertFile(file);
            return file;
        }

        /// <summary>
        /// 上传文件到阿里云
        /// </summary>
        /// <param name="file"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<SysFile> SaveFileToAliyun(SysFile file, IFormFile formFile)
        {
            file.FileName = (file.FileName.IsEmpty() ? HashFileName() : file.FileName) + file.FileExt;
            file.StorePath = GetDirPath(file.StorePath);
            string finalPath = Path.Combine(file.StorePath, file.FileName);
            HttpStatusCode statusCode = AliyunOssHelper.PutObjectFromFile(formFile.OpenReadStream(), finalPath, "");
            if (statusCode != HttpStatusCode.OK) return file;

            file.StorePath = file.StorePath;
            file.FileUrl = finalPath;
            file.AccessUrl = string.Concat(domainUrl, "/", file.StorePath.Replace("\\", "/"), "/", file.FileName);
            file.Id = await InsertFile(file);

            return file;
        }

        /// <summary>
        /// 获取文件存储目录
        /// </summary>
        /// <param name="storePath"></param>
        /// <param name="byTimeStore">是否按年月日存储</param>
        /// <returns></returns>
        public string GetDirPath(string storePath = "", bool byTimeStore = true)
        {
            DateTime date = DateTime.Now;
            string timeDir = date.ToString("yyyy/MMdd");

            if (!string.IsNullOrEmpty(storePath))
            {
                timeDir = Path.Combine(storePath, timeDir);
            }
            return timeDir;
        }

        public string HashFileName(string str = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = Guid.NewGuid().ToString().ToLower();
            }
            return BitConverter.ToString(MD5.HashData(Encoding.Default.GetBytes(str)), 4, 8).Replace("-", "");
        }

        public Task<long> InsertFile(SysFile file)
        {
            return Insertable(file).ExecuteReturnSnowflakeIdAsync();//单条插入返回雪花ID;
        }

        /// <summary>
        /// 获取文件预览
        /// </summary>
        /// <param name="sourceFileId"></param>
        /// <returns></returns>
        public List<SysFilePreview> GetFilePreview(long sourceFileId)
        {
            SysFile file = Queryable().Where(it => it.Id == sourceFileId).First();
            List<SysFilePreview> previews = Context.Queryable<SysFilePreview>().Where(it => it.SourceFileId == sourceFileId).ToList();
            //如果没有找到可用的预览文件，则创建预览文件
            if (previews.Count <= 0)
            {
                List<PreviewFileInfo> fileInfos = new List<PreviewFileInfo>();
                string saveFilePath = _httpContextAccessor.HttpContext.GetFactoryId() + "/" + GetDirPath("preview", false);

                switch (file.FileExt)
                {
                    case ".xls":
                    case ".xlsx":
                        fileInfos = ExcelHelper<object>.ConvertToImg(file.FileUrl, saveFilePath, 96);
                        break;

                    default:
                        throw new CustomException($"文件类型暂不支持预览");
                }

                string fileName = "";
                string filePath = _httpContextAccessor.HttpContext.GetFactoryId() + "/" + GetDirPath("preview");
                string uploadUrl = OptionsSetting.Upload.UploadUrl;
                string accessPath = string.Concat(filePath.Replace("\\", "/"), "/", fileName);
                Uri baseUri = new(uploadUrl);
                Uri fullUrl = new(baseUri, accessPath);
                //string finalFilePath = Path.Combine(rootPath, filePath, fileName);

                //FileUrl = finalFilePath.Replace("\\", "/"),
                //AccessUrl = fullUrl.AbsoluteUri
                foreach (PreviewFileInfo item in fileInfos)
                {
                    SysFilePreview sfp = new()
                    {
                        SourceFileId = sourceFileId,
                        FileName = item.FileName,
                        FileSize = item.FileSize,
                        PageNo = item.PageNo,
                        FileExtension = item.FileExtension,
                        FileAliasName = item.FileAliasName,
                    };
                    previews.Add(sfp);
                }
                Context.Insertable<SysFilePreview>(previews).ExecuteCommand();
                previews = Context.Queryable<SysFilePreview>().Where(it => it.SourceFileId == sourceFileId).ToList();
            }
            return previews;
        }
    }
}