using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Service.Report;
using EAM.Service.Report.IReportService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2026-03-05
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 报表基本信息
    /// </summary>
    [Verify]
    [Route("report/ReportBase")]
    public class ReportBaseController : BaseController
    {
        /// <summary>
        /// 报表基本信息接口
        /// </summary>
        private readonly IReportBaseService _ReportBaseService;

        public ReportBaseController(IReportBaseService ReportBaseService)
        {
            _ReportBaseService = ReportBaseService;
        }

        /// <summary>
        /// 查询报表基本信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "rep:report:base:list")]
        public IActionResult QueryReportBase([FromQuery] ReportBaseQueryDto parm)
        {
            var response = _ReportBaseService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询报表基本信息详情
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        [HttpGet("{ReportId}")]
        [ActionPermissionFilter(Permission = "rep:report:base:query")]
        public IActionResult GetReportBase(int ReportId)
        {
            var response = _ReportBaseService.GetInfo(ReportId);

            var info = response.Adapt<ReportBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加报表基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "rep:report:base:add")]
        [Log(Title = "报表基本信息", BusinessType = BusinessType.INSERT)]
        public IActionResult AddReportBase([FromBody] ReportBaseDto parm)
        {
            var modal = parm.Adapt<ReportBase>().ToCreate(HttpContext);

            var response = _ReportBaseService.AddReportBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新报表基本信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "rep:report:base:edit")]
        [Log(Title = "报表基本信息", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateReportBase([FromBody] ReportBaseDto parm)
        {
            var modal = parm.Adapt<ReportBase>().ToUpdate(HttpContext);
            var response = _ReportBaseService.UpdateReportBase(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除报表基本信息
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "rep:report:base:delete")]
        [Log(Title = "报表基本信息", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteReportBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ReportBaseService.Delete(idArr));
        }

        /// <summary>
        /// 查询报表信息字典列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult DictReportBase([FromQuery] ReportBaseQueryDto parm)
        {
            var response = _ReportBaseService.GetDict(parm);
            return SUCCESS(response);
        }
    }
}