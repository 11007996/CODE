using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;
using EAM.Repository;
using EAM.Service.Report.IReportService;
using Infrastructure.Attribute;

namespace EAM.Service.Report
{
    /// <summary>
    /// 报表参数Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IReportParamService), ServiceLifetime = LifeTime.Transient)]
    public class ReportParamService : BaseService<ReportParam>, IReportParamService
    {
        /// <summary>
        /// 查询报表参数列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ReportParamDto> GetList(ReportParamQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<ReportParam, ReportParamDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ParamId"></param>
        /// <returns></returns>
        public ReportParam GetInfo(int ParamId)
        {
            var response = Queryable()
                .Where(x => x.ParamId == ParamId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加报表参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReportParam AddReportParam(ReportParam model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改报表参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateReportParam(ReportParam model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ReportParam> QueryExp(ReportParamQueryDto parm)
        {
            var predicate = Expressionable.Create<ReportParam>();

            predicate = predicate.AndIF(parm.ReportId != null, it => it.ReportId == parm.ReportId);
            return predicate;
        }
    }
}