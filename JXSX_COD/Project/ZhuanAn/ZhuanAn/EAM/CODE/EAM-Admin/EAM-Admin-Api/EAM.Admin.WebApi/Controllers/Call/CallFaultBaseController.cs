using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Service.Call.ICallService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2025-07-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 呼叫记录
    /// </summary>
    [Verify]
    [Route("call/CallFaultBase")]
    public class CallFaultBaseController : BaseController
    {
        /// <summary>
        /// 呼叫接口
        /// </summary>
        private readonly ICallFaultBaseService _CallFaultBaseService;

        public CallFaultBaseController(ICallFaultBaseService CallFaultBaseService)
        {
            _CallFaultBaseService = CallFaultBaseService;
        }

        /// <summary>
        /// 查询呼叫记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "call:fault:base:list")]
        public IActionResult QueryCallFaultBase([FromQuery] CallFaultBaseQueryDto parm)
        {
            var response = _CallFaultBaseService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询呼叫记录详情
        /// </summary>
        /// <param name="CallId"></param>
        /// <returns></returns>
        [HttpGet("{CallId}")]
        [ActionPermissionFilter(Permission = "call:fault:base:query")]
        public IActionResult GetCallFaultBase(int CallId)
        {
            var response = _CallFaultBaseService.GetInfo(CallId);

            var info = response.Adapt<CallFaultBaseDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 删除呼叫记录
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "call:fault:base:delete")]
        [Log(Title = "呼叫记录", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteCallFaultBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_CallFaultBaseService.DeleteCallFaultBase(idArr));
        }

        /// <summary>
        /// 导出呼叫记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "呼叫记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "call:fault:base:export")]
        public IActionResult Export([FromQuery] CallFaultBaseQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _CallFaultBaseService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "呼叫记录", "呼叫记录");
            return ExportExcel(result.Item2, result.Item1);
        }

        #region 呼叫操作

        /// <summary>
        /// 根据产线获取产线相关设备、区域、工站 摘要信息
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        [HttpGet("line/{lineId}/summary")]
        [ActionPermissionFilter(Permission = "call:fault:operate")]
        public IActionResult QueryCallSummaryByLine([FromRoute] int lineId)
        {
            //获取产线的故障与设备、区域等信息
            var response = _CallFaultBaseService.GetCallSummaryByLine(lineId);

            return SUCCESS(response);
        }

        /// <summary>
        /// 根据产线获取未解决故障列表
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        [HttpGet("line/unsolved/{lineId}")]
        [ActionPermissionFilter(Permission = "call:fault:operate")]
        public IActionResult QueryUnsolvedCallFaultBase([FromRoute] int lineId)
        {
            //获取产线的故障与设备、区域等信息
            var response = _CallFaultBaseService.GetUnsolvedCallFaultBase(lineId);

            return SUCCESS(response);
        }

        /// <summary>
        /// 添加呼叫
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "call:fault:operate:call")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.INSERT)]
        public IActionResult AddCallFaultBase([FromBody] AddCallFaultBaseDto parm)
        {
            var modal = parm.Adapt<CallFaultBase>().ToCreate(HttpContext);
            modal.PcIp = HttpContext.GetClientUserIp();
            var response = _CallFaultBaseService.AddCallFaultBase(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 签到处理
        /// </summary>
        /// <returns></returns>
        [HttpPut("handle")]
        [ActionPermissionFilter(Permission = "call:fault:operate:handle")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.UPDATE)]
        public IActionResult HandleCallFaultBase([FromBody] CallOperateDto parm)
        {
            var model = parm.ToUpdate(HttpContext);
            var response = _CallFaultBaseService.HandleCallFaultBase(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 请求支援
        /// </summary>
        /// <returns></returns>
        [HttpPut("requestHelp")]
        [ActionPermissionFilter(Permission = "call:fault:operate:handle")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.UPDATE)]
        public IActionResult RequestHelpCallFaultBase([FromBody] CallOperateDto parm)
        {
            var model = parm.ToUpdate(HttpContext);
            var response = _CallFaultBaseService.RequestHelpCallFaultBase(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 支援签到
        /// </summary>
        /// <returns></returns>
        [HttpPut("help")]
        [ActionPermissionFilter(Permission = "call:fault:operate:help")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.UPDATE)]
        public IActionResult HelpCallFaultBase([FromBody] CallOperateDto parm)
        {
            var model = parm.ToUpdate(HttpContext);
            var response = _CallFaultBaseService.HelpCallFaultBase(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 解决完成
        /// </summary>
        /// <returns></returns>
        [HttpPut("solve")]
        [ActionPermissionFilter(Permission = "call:fault:operate:solve")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.UPDATE)]
        public IActionResult SolveCallFaultBase([FromBody] CallOperateDto parm)
        {
            var model = parm.ToUpdate(HttpContext);
            var response = _CallFaultBaseService.SolveCallFaultBase(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 停止呼叫
        /// </summary>
        /// <returns></returns>
        [HttpPut("stop/{ids}")]
        [ActionPermissionFilter(Permission = "call:fault:operate:stop")]
        [Log(Title = "呼叫操作", BusinessType = BusinessType.UPDATE)]
        public IActionResult StopCallFaultBase([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);
            var response = _CallFaultBaseService.StopCallFaultBase(idArr, HttpContext.GetName());

            return SUCCESS(response);
        }

        #endregion 呼叫操作
    }
}