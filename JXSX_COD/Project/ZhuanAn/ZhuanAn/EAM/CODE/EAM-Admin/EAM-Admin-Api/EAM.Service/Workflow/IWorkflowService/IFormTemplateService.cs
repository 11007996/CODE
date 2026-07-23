using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 表单模板service接口
    /// </summary>
    public interface IFormTemplateService : IBaseService<FormTemplate>
    {
        PagedInfo<FormTemplateDto> GetList(FormTemplateQueryDto parm);

        List<DictDataDto> GetDict(FormTemplateQueryDto parm);

        FormTemplate GetInfo(int formId);

        FormTemplate AddFormTemplate(FormTemplate parm);

        int UpdateFormTemplate(FormTemplate parm);

        int DeleteFormTemplate(int formId);
    }
}