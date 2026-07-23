using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Service.Workflow.IWorkflowService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-04
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 流程定义
    /// </summary>
    [Verify]
    [Route("workflow/processDefine")]
    public class ProcessDefineController : BaseController
    {
        /// <summary>
        /// 流程定义接口
        /// </summary>
        private readonly IProcessDefineService _ProcessDefineService;

        public ProcessDefineController(IProcessDefineService ProcessDefineService)
        {
            _ProcessDefineService = ProcessDefineService;
        }

        /// <summary>
        /// 查询流程定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "processInstance:list")]
        public IActionResult QueryProcessDefine([FromQuery] ProcessDefineQueryDto parm)
        {
            var response = _ProcessDefineService.GetList(parm);
            return SUCCESS(response);
        }

        [HttpGet("dict")]
        public IActionResult QueryProcessDefineDict([FromQuery] ProcessDefineQueryDto parm)
        {
            var response = _ProcessDefineService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询流程定义详情
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [HttpGet("{ProcessId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetProcessDefine(string ProcessId)
        {
            var response = _ProcessDefineService.GetInfo(ProcessId);

            var info = response.Adapt<ProcessDefineDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加流程定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "流程定义", BusinessType = BusinessType.INSERT)]
        public IActionResult AddProcessDefine([FromBody] ProcessDefineDto parm)
        {
            var modal = parm.Adapt<ProcessDefine>().ToCreate(HttpContext);

            var response = _ProcessDefineService.AddProcessDefine(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新流程定义
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "流程定义", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateProcessDefine([FromBody] ProcessDefineDto parm)
        {
            var modal = parm.Adapt<ProcessDefine>().ToUpdate(HttpContext);
            var response = _ProcessDefineService.UpdateProcessDefine(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除流程定义
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "流程定义", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteProcessDefine([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ProcessDefineService.Delete(idArr));
        }

        /// <summary>
        /// 查询流程所有节点信息
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        [HttpGet("{ProcessId}/nodeApprover")]
        public IActionResult GetProcessNodeInfo(string ProcessId)
        {
            var response = _ProcessDefineService.GetProcessNodeApprover(ProcessId);

            var info = response.Adapt<List<NodeApproverDto>>();
            return SUCCESS(info);
        }
    }
}