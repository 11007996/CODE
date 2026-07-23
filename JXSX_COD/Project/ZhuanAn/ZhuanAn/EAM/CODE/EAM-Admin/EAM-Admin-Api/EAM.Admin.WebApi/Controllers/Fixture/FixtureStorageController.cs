using EAM.Model.Dto;
using EAM.Service.Fixture.IFixtureService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-05-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 治具存储
    /// </summary>
    [Verify]
    [Route("fixture/FixtureStorage")]
    public class FixtureStorageController : BaseController
    {
        /// <summary>
        /// 治具存储接口
        /// </summary>
        private readonly IFixtureStorageService _FixtureStorageService;

        public FixtureStorageController(IFixtureStorageService FixtureStorageService)
        {
            _FixtureStorageService = FixtureStorageService;
        }

        /// <summary>
        /// 查询治具存储列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "fixture:storage:list")]
        public IActionResult QueryFixtureStorage([FromQuery] FixtureStorageQueryDto parm)
        {
            var response = _FixtureStorageService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询储位信息详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("info")]
        [ActionPermissionFilter(Permission = "fixture:storage:query")]
        public IActionResult GetStorageSpace([FromQuery] FixtureStorageInfoDto parm)
        {
            var response = _FixtureStorageService.GetInfo(parm);

            var info = response.Adapt<FixtureStorageDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 删除治具存储
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "fixture:storage:delete")]
        [Log(Title = "治具存储", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFixtureStorage([FromQuery] FixtureStorageInfoDto parm)
        {
            return ToResponse(_FixtureStorageService.DeleteFixtureStorage(parm));
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpPut("in")]
        [ActionPermissionFilter(Permission = "fixture:storage:in")]
        [Log(Title = "治具存储", BusinessType = BusinessType.INSERT)]
        public IActionResult InFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.InFixtureStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        [HttpPut("out")]
        [ActionPermissionFilter(Permission = "fixture:storage:out")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult OutFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.OutFixtureStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 报废
        /// </summary>
        /// <returns></returns>
        [HttpPut("scrapped")]
        [ActionPermissionFilter(Permission = "fixture:storage:scrapped")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ScrappedFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.ScrappedFixtureStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <returns></returns>
        [HttpPut("receive")]
        [ActionPermissionFilter(Permission = "fixture:storage:receive")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ReceiveFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.ReceiveFixtureStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 批量领用
        /// </summary>
        /// <returns></returns>
        [HttpPut("receive/batch")]
        [ActionPermissionFilter(Permission = "fixture:storage:receive")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult BatchReceiveFixtureStorage([FromBody] List<OperateFixtureStorageDto> parm)
        {
            foreach (var item in parm)
            {
                item.ToCreate(HttpContext);
            }
            var response = _FixtureStorageService.BatchReceiveFixtureStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <returns></returns>
        [HttpPut("back")]
        [ActionPermissionFilter(Permission = "fixture:storage:back")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult BackFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.BackFixtureStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 批量归还
        /// </summary>
        /// <returns></returns>
        [HttpPut("back/batch")]
        [ActionPermissionFilter(Permission = "fixture:storage:back")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult BatchBackFixtureStorage([FromBody] List<OperateFixtureStorageDto> parm)
        {
            string username = HttpContext.GetName();
            foreach (var item in parm)
            {
                item.CreateBy = username;
                item.CreateTime = DateTime.Now;
            }
            var response = _FixtureStorageService.BatchBackFixtureStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <returns></returns>
        [HttpPut("transfer")]
        [ActionPermissionFilter(Permission = "fixture:storage:transfer")]
        [Log(Title = "治具存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult TransferFixtureStorage([FromBody] OperateFixtureStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _FixtureStorageService.TransferFixtureStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 查询在使用中的治具列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("using/list")]
        [ActionPermissionFilter(Permission = "fixture:storage:list")]
        public IActionResult QueryFixtureStorageUsing([FromQuery] FixtureStorageUsingQueryDto parm)
        {
            var response = _FixtureStorageService.GetUsingList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询领用信息
        /// </summary>
        /// <param name="fixtureUsingId"></param>
        /// <returns></returns>
        [HttpGet("using/{fixtureUsingId}")]
        [ActionPermissionFilter(Permission = "fixture:storage:list")]
        public IActionResult GetFixtureStorageUsing([FromRoute] int fixtureUsingId)
        {
            var response = _FixtureStorageService.GetUsingInfo(fixtureUsingId);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导出治具库存表
        /// </summary>
        /// <returns></returns>
        [Log(Title = "治具库存", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "fixture:storage:export")]
        public IActionResult Export([FromQuery] FixtureStorageQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _FixtureStorageService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "治具库存表", "治具库存表");
            return ExportExcel(result.Item2, result.Item1);
        }

        #region 导入
        /// <summary>
        /// 导入检查
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importDataCheck")]
        [ActionPermissionFilter(Permission = "fixture:storage:import")]
        public IActionResult ImportDataCheck([FromForm(Name = "file")] IFormFile formFile)
        {
            List<FixtureStorageImportDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<FixtureStorageImportDto>(startCell: "A1").ToList();
            }

            if (list != null && list.Count > 0)
            {
                string createBy = HttpContext.GetName();
                foreach (var item in list)
                {
                    item.CreateBy = createBy;
                    item.CreateTime = item.CreateTime ?? DateTime.Now;
                }
            }

            return SUCCESS(_FixtureStorageService.ImportFixtureStorageCheck(list));
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "治具库存变更导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "fixture:storage:import")]
        public IActionResult ImportData([FromBody] List<FixtureStorageImportDto> list)
        {
            return SUCCESS(_FixtureStorageService.ImportFixtureStorage(list));
        }

        /// <summary>
        /// 治具库存变更导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "治具库存模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<FixtureStorageImportDto>() { }, "FixtureStorage");
            return ExportExcel(result.Item2, result.Item1);
        }
        #endregion

        #region 操作导入
        /// <summary>
        /// 导入检查
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importOperateDataCheck")]
        [ActionPermissionFilter(Permission = "fixture:storage:import")]
        public IActionResult ImportOperateDataCheck([FromForm(Name = "file")] IFormFile formFile)
        {
            List<FixtureStorageOperateImportDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<FixtureStorageOperateImportDto>(startCell: "A1").ToList();
            }

            if (list != null && list.Count > 0)
            {
                string createBy = HttpContext.GetName();
                foreach (var item in list)
                {
                    item.CreateBy = createBy;
                    item.CreateTime = item.CreateTime ?? DateTime.Now;
                }
            }

            return SUCCESS(_FixtureStorageService.ImportFixtureStorageOperateCheck(list));
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost("importOperateData")]
        [Log(Title = "治具库存操作导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "fixture:storage:import")]
        public IActionResult ImportOperateData([FromBody] List<FixtureStorageOperateImportDto> list)
        {
            return SUCCESS(_FixtureStorageService.ImportFixtureStorageOperate(list));
        }

        /// <summary>
        /// 治具库存变更导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importOperateTemplate")]
        [Log(Title = "治具库存操作模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportOperateTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<FixtureStorageOperateImportDto>() { }, "FixtureStorageOperate");
            return ExportExcel(result.Item2, result.Item1);
        }
        #endregion

        #region 治具存储记录
        /// <summary>
        /// 查询治具出入库记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("record/list")]
        [ActionPermissionFilter(Permission = "fixture:storage:list")]
        public IActionResult QueryFixtureStorageRecord([FromQuery] FixtureStorageRecordQueryDto parm)
        {
            var response = _FixtureStorageService.GetRecordList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导出治具存储记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "治具存储记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("record/export")]
        [ActionPermissionFilter(Permission = "fixture:storage:export")]
        public IActionResult ExportRecord([FromQuery] FixtureStorageRecordQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _FixtureStorageService.GetRecordList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "治具存储记录", "治具存储记录");
            return ExportExcel(result.Item2, result.Item1);
        }
        #endregion
    }
}