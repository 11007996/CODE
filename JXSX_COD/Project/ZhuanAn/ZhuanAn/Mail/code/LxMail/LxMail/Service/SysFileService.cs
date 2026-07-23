using Infrastructure.Attribute;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Infrastructure.Model;
using LxMail.CoreService;
using LxMail.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace LxMail.Service
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [AppService(ServiceType = typeof(ISysFileService), ServiceLifetime = LifeTime.Transient)]
    public class SysFileService : BaseService<SysFile>, ISysFileService
    {
        private OptionsSetting OptionsSetting;

        public SysFileService(IOptions<OptionsSetting> options)
        {
            OptionsSetting = options.Value;
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
    }
}