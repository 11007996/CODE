using Microsoft.AspNetCore.Mvc;

namespace EAM.Dashboard.Controller
{
    /// <summary>
    /// 公共模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "sys")]
    [Verify]
    public class CommonController : BaseController
    {
        private readonly ISysFileService SysFileService;

        public CommonController(
            ISysFileService fileService)
        {
            SysFileService = fileService;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionPermissionFilter(Permission = "common")]
        [Log(Title = "下载文件", IsSaveResponseData = false)]
        public IActionResult DownloadFile(long fileId)
        {
            string fullPath = null;
            string fileName = null;
            if (fileId > 0)
            {
                var fileInfo = SysFileService.GetById(fileId);
                if (fileInfo != null)
                {
                    fullPath = fileInfo.FileUrl;
                    fileName = fileInfo.RealName;
                }
            }
            else
            {
                throw new CustomException("未传递文件ID");
            }

            if (fileName == null)
                fileName = Path.GetFileName(fullPath);

            //使用原始名称
            return DownFile(fullPath, fileName);
        }
    }
}