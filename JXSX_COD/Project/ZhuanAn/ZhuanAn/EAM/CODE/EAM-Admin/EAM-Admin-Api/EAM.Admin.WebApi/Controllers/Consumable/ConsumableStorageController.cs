using EAM.Model.Dto;
using EAM.Service.Consumable.IConsumableService;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;

//创建时间：2024-05-18
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 耗品存储表
    /// </summary>
    [Verify]
    [Route("consumable/ConsumableStorage")]
    public class ConsumableStorageController : BaseController
    {
        /// <summary>
        /// 耗品存储表接口
        /// </summary>
        private readonly IConsumableStorageService _ConsumableStorageService;

        public ConsumableStorageController(IConsumableStorageService ConsumableStorageService)
        {
            _ConsumableStorageService = ConsumableStorageService;
        }

        /// <summary>
        /// 查询耗品存储表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "consumable:storage:list")]
        public IActionResult QueryConsumableStorage([FromQuery] ConsumableStorageQueryDto parm)
        {
            var response = _ConsumableStorageService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询耗品存储表详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("info")]
        [ActionPermissionFilter(Permission = "consumable:storage:query")]
        public IActionResult GetConsumableStorage([FromQuery] ConsumableStorageInfoDto parm)
        {
            var response = _ConsumableStorageService.GetInfo(parm);

            var info = response.Adapt<ConsumableStorageDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 删除耗品存储
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        [ActionPermissionFilter(Permission = "consumable:storage:delete")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteConsumableStorage([FromQuery] ConsumableStorageInfoDto parm)
        {
            return ToResponse(_ConsumableStorageService.DeleteConsumableStorage(parm));
        }

        /// <summary>
        /// 入库
        /// </summary>
        /// <returns></returns>
        [HttpPut("in")]
        [ActionPermissionFilter(Permission = "consumable:storage:in")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.INSERT)]
        public IActionResult InConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.InConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <returns></returns>
        [HttpPut("out")]
        [ActionPermissionFilter(Permission = "consumable:storage:out")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult OutConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.OutConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 报废
        /// </summary>
        /// <returns></returns>
        [HttpPut("scrapped")]
        [ActionPermissionFilter(Permission = "consumable:storage:scrapped")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ScrappedConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.ScrappedConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 领用
        /// </summary>
        /// <returns></returns>
        [HttpPut("receive")]
        [ActionPermissionFilter(Permission = "consumable:storage:receive")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult ReceiveConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.ReceiveConsumableStorage(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 归还
        /// </summary>
        /// <returns></returns>
        [HttpPut("back")]
        [ActionPermissionFilter(Permission = "consumable:storage:back")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult BackConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.BackConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 转移
        /// </summary>
        /// <returns></returns>
        [HttpPut("transfer")]
        [ActionPermissionFilter(Permission = "consumable:storage:transfer")]
        [Log(Title = "耗品存储", BusinessType = BusinessType.UPDATE)]
        public IActionResult TransferConsumableStorage([FromBody] OperateConsumableStorageDto parm)
        {
            var modal = parm.ToCreate(HttpContext);
            var response = _ConsumableStorageService.TransferConsumableStorage(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 导出耗品库存表
        /// </summary>
        /// <returns></returns>
        [Log(Title = "耗品库存", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "consumable:storage:export")]
        public IActionResult Export([FromQuery] ConsumableStorageQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _ConsumableStorageService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "耗品库存表", "耗品库存表");
            return ExportExcel(result.Item2, result.Item1);
        }

        #region 导入
        /// <summary>
        /// 耗品库存变更导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importTemplate")]
        [Log(Title = "耗品库存变更模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<ConsumableStorageDto>() { }, "ConsumableStorage");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 导入检查
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importDataCheck")]
        [ActionPermissionFilter(Permission = "consumable:storage:import")]
        public IActionResult ImportDataCheck([FromForm(Name = "file")] IFormFile formFile)
        {
            List<ConsumableStorageImportDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<ConsumableStorageImportDto>(startCell: "A1").ToList();
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

            return SUCCESS(_ConsumableStorageService.ImportConsumableStorageCheck(list));
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost("importData")]
        [Log(Title = "耗品库存导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "consumable:storage:import")]
        public IActionResult ImportData([FromBody] List<ConsumableStorageImportDto> list)
        {
            return SUCCESS(_ConsumableStorageService.ImportConsumableStorage(list));
        }
        #endregion

        #region 操作导入
        /// <summary>
        /// 【操作导入】导入数据检查
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("importOperateDataCheck")]
        [ActionPermissionFilter(Permission = "consumable:storage:import")]
        public IActionResult ImportOperateDataCheck([FromForm(Name = "file")] IFormFile formFile)
        {
            List<ConsumableStorageOperateImportDto> list = new();
            using (var stream = formFile.OpenReadStream())
            {
                list = stream.Query<ConsumableStorageOperateImportDto>(startCell: "A1").ToList();
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

            return SUCCESS(_ConsumableStorageService.ImportConsumableStorageOperateCheck(list));
        }

        /// <summary>
        /// 【操作导入】导入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost("importOperateData")]
        [Log(Title = "耗品库存操作导入", BusinessType = BusinessType.IMPORT, IsSaveRequestData = false)]
        [ActionPermissionFilter(Permission = "consumable:storage:import")]
        public IActionResult ImportOperateData([FromBody] List<ConsumableStorageOperateImportDto> list)
        {
            return SUCCESS(_ConsumableStorageService.ImportConsumableStorageOperate(list));
        }

        /// <summary>
        /// 【操作导入】耗品库存操作导入模板下载
        /// </summary>
        /// <returns></returns>
        [HttpGet("importOperateTemplate")]
        [Log(Title = "耗品库存操作模板", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ImportOperateTemplateExcel()
        {
            var result = DownloadImportTemplate(new List<ConsumableStorageOperateImportDto>() { }, "ConsumableStorageOperate");
            return ExportExcel(result.Item2, result.Item1);
        }
        #endregion

        #region 耗品记录
        /// <summary>
        /// 查询耗品出入库记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("record/list")]
        [ActionPermissionFilter(Permission = "consumable:storage:list")]
        public IActionResult QueryConsumableStorageRecord([FromQuery] ConsumableStorageRecordQueryDto parm)
        {
            var response = _ConsumableStorageService.GetRecordList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导出耗品操作记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "耗品存储记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("record/export")]
        [ActionPermissionFilter(Permission = "consumable:storage:export")]
        public IActionResult ExportRecord([FromQuery] ConsumableStorageRecordQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _ConsumableStorageService.GetRecordList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }

            var result = ExportExcelMini(list, "耗品存储记录", "耗品存储记录");
            return ExportExcel(result.Item2, result.Item1);
        }
        #endregion
    }
}