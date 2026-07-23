using EAM.Model.Dto;
using EAM.Service.Report.IReportService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-03-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 报表执行
    /// </summary>
    [Verify]
    [Route("report/ReportExecute")]
    public class ReportExecuteController : BaseController
    {
        /// <summary>
        /// 报表基本信息接口
        /// </summary>
        private readonly IReportExecuteService _ReportExecuteService;

        public ReportExecuteController(IReportExecuteService ReportExecuteService)
        {
            _ReportExecuteService = ReportExecuteService;
        }

        /// <summary>
        /// 查询报表基本信息详情
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        [HttpGet("ReportInfo/{ReportId}")]
        [ActionPermissionFilter(Permission = "rep:report:execute:query")]
        public IActionResult GetReportInfo([FromRoute] int ReportId)
        {
            var response = _ReportExecuteService.GetReportInfo(ReportId);

            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表参数选项数据源
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("ReportParamOptions")]
        [ActionPermissionFilter(Permission = "rep:report:execute:query")]
        public IActionResult GetReportParamOptions([FromQuery] ReportParamOptionsQueryDto parm)
        {
            string factoryId = HttpContextExtension.GetFactoryId(HttpContext);
            parm.FactoryId = factoryId;
            var response = _ReportExecuteService.GetReportParamOptions(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表基本信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "rep:report:execute:query")]
        public IActionResult QueryReportExecute([FromQuery] ReportExecuteQueryDto parm)
        {
            string factoryId = HttpContextExtension.GetFactoryId(HttpContext);
            parm.FactoryId = factoryId;
            var response = _ReportExecuteService.GetPageList(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 导出报表数据
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "rep:report:execute:export")]
        [Log(Title = "通用报表执行导出", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        public IActionResult ExportReportExecute([FromQuery] ReportExecuteQueryDto parm)
        {
            string factoryId = HttpContextExtension.GetFactoryId(HttpContext);
            parm.FactoryId = factoryId;
            var response = _ReportExecuteService.GetAllList(parm);
            if (response == null || response.Rows.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(response, "通用报表", "通用报表" + factoryId + "_");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}