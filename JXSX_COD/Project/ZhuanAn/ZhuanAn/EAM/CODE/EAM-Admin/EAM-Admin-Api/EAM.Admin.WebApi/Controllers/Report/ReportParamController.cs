using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Service.Report.IReportService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-03-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 报表参数
    /// </summary>
    [Verify]
    [Route("report/ReportParam")]
    public class ReportParamController : BaseController
    {
        /// <summary>
        /// 报表参数接口
        /// </summary>
        private readonly IReportParamService _ReportParamService;

        public ReportParamController(IReportParamService ReportParamService)
        {
            _ReportParamService = ReportParamService;
        }

        /// <summary>
        /// 查询报表参数列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "rep:report:base:list")]
        public IActionResult QueryReportParam([FromQuery] ReportParamQueryDto parm)
        {
            var response = _ReportParamService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表参数详情
        /// </summary>
        /// <param name="ParamId"></param>
        /// <returns></returns>
        [HttpGet("{ParamId}")]
        [ActionPermissionFilter(Permission = "rep:report:base:query")]
        public IActionResult GetReportParam(int ParamId)
        {
            var response = _ReportParamService.GetInfo(ParamId);

            var info = response.Adapt<ReportParamDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加报表参数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "rep:report:base:add")]
        [Log(Title = "报表参数", BusinessType = BusinessType.INSERT)]
        public IActionResult AddReportParam([FromBody] ReportParamDto parm)
        {
            var modal = parm.Adapt<ReportParam>().ToCreate(HttpContext);

            var response = _ReportParamService.AddReportParam(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新报表参数
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "rep:report:base:edit")]
        [Log(Title = "报表参数", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateReportParam([FromBody] ReportParamDto parm)
        {
            var modal = parm.Adapt<ReportParam>().ToUpdate(HttpContext);
            var response = _ReportParamService.UpdateReportParam(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除报表参数
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "rep:report:base:delete")]
        [Log(Title = "报表参数", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteReportParam([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ReportParamService.Delete(idArr));
        }
    }
}