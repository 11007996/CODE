using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Workflow.IWorkflowService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 表单模板Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFormTemplateService), ServiceLifetime = LifeTime.Transient)]
    public class FormTemplateService : BaseService<FormTemplate>, IFormTemplateService
    {
        public FormTemplateService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询表单模板列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FormTemplateDto> GetList(FormTemplateQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.FormFieldNav) //填充子对象
                .Where(predicate.ToExpression())
                .ToPage<FormTemplate, FormTemplateDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取所有表单模板字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<DictDataDto> GetDict(FormTemplateQueryDto parm)
        {
            var response = Queryable()
                .Select(it => new DictDataDto
                {
                    DictValue = it.FormId.ToString(),
                    DictLabel = it.FormName
                })
                .ToList();
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public FormTemplate GetInfo(int formId)
        {
            var response = Queryable()
                .Includes(x => x.FormFieldNav) //填充子对象
                .Where(x => x.FormId == formId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加表单模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FormTemplate AddFormTemplate(FormTemplate model)
        {
            return Context.InsertNav(model).Include(s1 => s1.FormFieldNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改表单模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateFormTemplate(FormTemplate model)
        {
            return Context.UpdateNav(model).Include(z1 => z1.FormFieldNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteFormTemplate(int formId)
        {
            return Context.DeleteNav<FormTemplate>(it => it.FormId == formId).Include(s1 => s1.FormFieldNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FormTemplate> QueryExp(FormTemplateQueryDto parm)
        {
            var predicate = Expressionable.Create<FormTemplate>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FormName), it => it.FormName == parm.FormName);
            return predicate;
        }
    }
}