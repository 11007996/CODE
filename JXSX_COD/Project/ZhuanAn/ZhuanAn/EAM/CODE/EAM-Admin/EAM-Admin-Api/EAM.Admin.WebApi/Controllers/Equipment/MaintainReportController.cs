using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;
using System.Data;

//创建时间：2024-10-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 资产保养报表
    /// </summary>
    [Verify]
    [Route("equipment/MaintainReport")]
    public class MaintainReportController : BaseController
    {
        /// <summary>
        /// 资产保养报表接口
        /// </summary>
        private readonly IMaintainReportService _MaintainReportService;

        public MaintainReportController(IMaintainReportService MaintainReportService)
        {
            _MaintainReportService = MaintainReportService;
        }

        /// <summary>
        /// 查询资产保养报表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:report:list")]
        public IActionResult QueryMaintainReport([FromQuery] MaintainReportQueryDto parm)
        {
            var response = _MaintainReportService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询资产保养报表详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "maintain:report:query")]
        public IActionResult GetMaintainReport(int Id)
        {
            var response = _MaintainReportService.GetInfo(Id);

            var info = response.Adapt<MaintainReportDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加资产保养报表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:report:add")]
        [Log(Title = "资产保养报表", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainReport([FromBody] MaintainReportDto parm)
        {
            var modal = parm.Adapt<MaintainReport>().ToCreate(HttpContext);

            var response = _MaintainReportService.AddMaintainReport(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 批量添加资产保养报表
        /// </summary>
        /// <returns></returns>
        [HttpPost("batch")]
        [ActionPermissionFilter(Permission = "maintain:report:add")]
        [Log(Title = "资产保养报表", BusinessType = BusinessType.INSERT)]
        public IActionResult BatchAddMaintainReport([FromBody] MaintainReportBatchAddDto parm)
        {
            var response = _MaintainReportService.BatchAddMaintainReport(parm.Year);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新资产保养报表
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:report:edit")]
        [Log(Title = "资产保养报表", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainReport([FromBody] MaintainReportDto parm)
        {
            var modal = parm.Adapt<MaintainReport>().ToUpdate(HttpContext);
            var response = _MaintainReportService.UpdateMaintainReport(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除资产保养报表
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:report:delete")]
        [Log(Title = "资产保养报表", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainReport([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainReportService.Delete(idArr));
        }

        /// <summary>
        /// 查询资产保养报表单页
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("sheet")]
        [ActionPermissionFilter(Permission = "maintain:report:list")]
        public IActionResult QueryMaintainReport([FromQuery] MaintainReportSheetQueryDto parm)
        {
            var response = _MaintainReportService.GetReportSheet(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导出设备保养报表
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备保养报表", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "maintain:report:export")]
        public IActionResult Export([FromQuery] MaintainReportSheetQueryDto parm)
        {
            var filePath = _MaintainReportService.ExportReportExcel(parm);

            return ExportExcel(filePath, Path.GetFileName(filePath));
        }

        /// <summary>
        /// 查询资产保养报表概况
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("overview")]
        [ActionPermissionFilter(Permission = "maintain:overview:list")]
        public IActionResult OverviewMaintainReport([FromQuery] MaintainReportOverviewQueryDto parm)
        {
            var response = _MaintainReportService.GetReportOverview(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 导出设备保养概况
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备保养概况导出", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("overview/export")]
        [ActionPermissionFilter(Permission = "maintain:overview:export")]
        public IActionResult ExportOverview([FromQuery] MaintainReportOverviewQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _MaintainReportService.GetReportOverview(parm).Item2;
            if (list == null || list.Rows.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            //将保养记录Id转为字符 'V'
            foreach(DataRow row in list.Rows)
            {
                foreach(DataColumn c in list.Columns)
                {
                    if(int.TryParse(c.ColumnName,out int i))
                    {//列为为 int类型的 日期标记值
                        if (row[c] != DBNull.Value)
                            row[c] = 'V';
                    }
                }
            }
            var result = ExportExcelMini(list, "设备保养概况", "设备保养概况");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}