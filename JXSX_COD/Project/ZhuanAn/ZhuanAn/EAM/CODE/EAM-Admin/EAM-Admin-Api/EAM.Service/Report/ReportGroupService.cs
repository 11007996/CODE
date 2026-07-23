using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Repository;
using EAM.Service.Report.IReportService;
using Infrastructure.Attribute;

namespace EAM.Service.Report
{
    /// <summary>
    /// 报表分组Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IReportGroupService), ServiceLifetime = LifeTime.Transient)]
    public class ReportGroupService : BaseService<ReportGroup>, IReportGroupService
    {
        /// <summary>
        /// 查询报表分组列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ReportGroupDto> GetList(ReportGroupQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<ReportGroup, ReportGroupDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public ReportGroup GetInfo(int GroupId)
        {
            var response = Queryable()
                .Where(x => x.GroupId == GroupId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加报表分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReportGroup AddReportGroup(ReportGroup model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改报表分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateReportGroup(ReportGroup model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询报表分组字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(ReportGroupQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictValue = it.GroupId.ToString(),
                    DictLabel = it.GroupName
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ReportGroup> QueryExp(ReportGroupQueryDto parm)
        {
            var predicate = Expressionable.Create<ReportGroup>();

            predicate.AndIF(!string.IsNullOrEmpty(parm.GroupName), it => it.GroupName.Contains(parm.GroupName));

            return predicate;
        }
    }
}