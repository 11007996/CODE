using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Service.Workflow.IWorkflowService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-08
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 表单模板
    /// </summary>
    [Verify]
    [Route("workflow/FormTemplate")]
    public class FormTemplateController : BaseController
    {
        /// <summary>
        /// 表单模板接口
        /// </summary>
        private readonly IFormTemplateService _FormTemplateService;

        public FormTemplateController(IFormTemplateService FormTemplateService)
        {
            _FormTemplateService = FormTemplateService;
        }

        /// <summary>
        /// 查询表单模板列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryFormTemplate([FromQuery] FormTemplateQueryDto parm)
        {
            var response = _FormTemplateService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询表单模板列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryFormTemplateDict([FromQuery] FormTemplateQueryDto parm)
        {
            var response = _FormTemplateService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询表单模板详情
        /// </summary>
        /// <param name="FormId"></param>
        /// <returns></returns>
        [HttpGet("{FormId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetFormTemplate(int FormId)
        {
            var response = _FormTemplateService.GetInfo(FormId);

            var info = response.Adapt<FormTemplateDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加表单模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "表单模板", BusinessType = BusinessType.INSERT)]
        public IActionResult AddFormTemplate([FromBody] FormTemplateDto parm)
        {
            var modal = parm.Adapt<FormTemplate>().ToCreate(HttpContext);

            var response = _FormTemplateService.AddFormTemplate(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新表单模板
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "表单模板", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateFormTemplate([FromBody] FormTemplateDto parm)
        {
            var modal = parm.Adapt<FormTemplate>().ToUpdate(HttpContext);
            var response = _FormTemplateService.UpdateFormTemplate(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除表单模板
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{formId}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "表单模板", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteFormTemplate([FromRoute] int formId)
        {
            return ToResponse(_FormTemplateService.DeleteFormTemplate(formId));
        }
    }
}