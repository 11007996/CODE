using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Repository;
using EAM.Service.Report.IReportService;
using Infrastructure.Attribute;

namespace EAM.Service.Report
{
    /// <summary>
    /// 报表数据列Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IReportColumnService), ServiceLifetime = LifeTime.Transient)]
    public class ReportColumnService : BaseService<ReportColumn>, IReportColumnService
    {
        /// <summary>
        /// 查询报表数据列列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ReportColumnDto> GetList(ReportColumnQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<ReportColumn, ReportColumnDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ColumnId"></param>
        /// <returns></returns>
        public ReportColumn GetInfo(int ColumnId)
        {
            var response = Queryable()
                .Where(x => x.ColumnId == ColumnId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加报表数据列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReportColumn AddReportColumn(ReportColumn model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改报表数据列
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateReportColumn(ReportColumn model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ReportColumn> QueryExp(ReportColumnQueryDto parm)
        {
            var predicate = Expressionable.Create<ReportColumn>();

            predicate = predicate.AndIF(parm.ReportId != null, it => it.ReportId == parm.ReportId);
            return predicate;
        }
    }
}