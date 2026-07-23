using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Service.Report.IReportService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-03-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 报表分组
    /// </summary>
    [Verify]
    [Route("report/ReportGroup")]
    public class ReportGroupController : BaseController
    {
        /// <summary>
        /// 报表分组接口
        /// </summary>
        private readonly IReportGroupService _ReportGroupService;

        public ReportGroupController(IReportGroupService ReportGroupService)
        {
            _ReportGroupService = ReportGroupService;
        }

        /// <summary>
        /// 查询报表分组列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "rep:report:base:list")]
        public IActionResult QueryReportGroup([FromQuery] ReportGroupQueryDto parm)
        {
            var response = _ReportGroupService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表分组详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet("{GroupId}")]
        [ActionPermissionFilter(Permission = "rep:report:base:query")]
        public IActionResult GetReportGroup(int GroupId)
        {
            var response = _ReportGroupService.GetInfo(GroupId);

            var info = response.Adapt<ReportGroupDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加报表分组
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "rep:report:base:add")]
        [Log(Title = "报表分组", BusinessType = BusinessType.INSERT)]
        public IActionResult AddReportGroup([FromBody] ReportGroupDto parm)
        {
            var modal = parm.Adapt<ReportGroup>().ToCreate(HttpContext);

            var response = _ReportGroupService.AddReportGroup(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新报表分组
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "rep:report:base:edit")]
        [Log(Title = "报表分组", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateReportGroup([FromBody] ReportGroupDto parm)
        {
            var modal = parm.Adapt<ReportGroup>().ToUpdate(HttpContext);
            var response = _ReportGroupService.UpdateReportGroup(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除报表分组
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "rep:report:base:delete")]
        [Log(Title = "报表分组", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteReportGroup([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ReportGroupService.Delete(idArr));
        }

        /// <summary>
        /// 查询报表分组字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictReportGroup([FromQuery] ReportGroupQueryDto parm)
        {
            var response = _ReportGroupService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}