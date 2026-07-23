using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Service.Report.IReportService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-03-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 报表数据列
    /// </summary>
    [Verify]
    [Route("report/ReportColumn")]
    public class ReportColumnController : BaseController
    {
        /// <summary>
        /// 报表数据列接口
        /// </summary>
        private readonly IReportColumnService _ReportColumnService;

        public ReportColumnController(IReportColumnService ReportColumnService)
        {
            _ReportColumnService = ReportColumnService;
        }

        /// <summary>
        /// 查询报表数据列列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "rep:report:base:list")]
        public IActionResult QueryReportColumn([FromQuery] ReportColumnQueryDto parm)
        {
            var response = _ReportColumnService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表数据列详情
        /// </summary>
        /// <param name="ColumnId"></param>
        /// <returns></returns>
        [HttpGet("{ColumnId}")]
        [ActionPermissionFilter(Permission = "rep:report:base:query")]
        public IActionResult GetReportColumn(int ColumnId)
        {
            var response = _ReportColumnService.GetInfo(ColumnId);

            var info = response.Adapt<ReportColumnDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加报表数据列
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "rep:report:base:add")]
        [Log(Title = "报表数据列", BusinessType = BusinessType.INSERT)]
        public IActionResult AddReportColumn([FromBody] ReportColumnDto parm)
        {
            var modal = parm.Adapt<ReportColumn>().ToCreate(HttpContext);

            var response = _ReportColumnService.AddReportColumn(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新报表数据列
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "rep:report:base:edit")]
        [Log(Title = "报表数据列", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateReportColumn([FromBody] ReportColumnDto parm)
        {
            var modal = parm.Adapt<ReportColumn>().ToUpdate(HttpContext);
            var response = _ReportColumnService.UpdateReportColumn(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除报表数据列
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "rep:report:base:delete")]
        [Log(Title = "报表数据列", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteReportColumn([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ReportColumnService.Delete(idArr));
        }
    }
}