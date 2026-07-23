using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Repository;
using EAM.Service.Report.IReportService;
using Infrastructure.Attribute;

namespace EAM.Service.Report
{
    /// <summary>
    /// 报表基本信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IReportBaseService), ServiceLifetime = LifeTime.Transient)]
    public class ReportBaseService : BaseService<ReportBase>, IReportBaseService
    {
        /// <summary>
        /// 查询报表基本信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ReportBaseDto> GetList(ReportBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("SortOrder asc")
                .Where(predicate.ToExpression())
                .ToPage<ReportBase, ReportBaseDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ReportId"></param>
        /// <returns></returns>
        public ReportBase GetInfo(int ReportId)
        {
            var response = Queryable()
                .Where(x => x.ReportId == ReportId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加报表基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReportBase AddReportBase(ReportBase model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改报表基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateReportBase(ReportBase model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 获取报表信息字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(ReportBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Where(it => it.Enabled == true)
                .OrderBy(it => it.SortOrder)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.ReportId.ToString(),
                    DictLabel = it.ReportName
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ReportBase> QueryExp(ReportBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<ReportBase>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ReportName), it => it.ReportName == parm.ReportName);
            predicate = predicate.AndIF(parm.GroupId > 0, it => it.GroupId == parm.GroupId);
            return predicate;
        }
    }
}