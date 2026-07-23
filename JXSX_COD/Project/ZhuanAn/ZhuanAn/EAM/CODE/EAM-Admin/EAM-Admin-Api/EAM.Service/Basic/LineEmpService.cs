using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Basic
{
    /// <summary>
    /// 产线员工关联Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ILineEmpService), ServiceLifetime = LifeTime.Transient)]
    public class LineEmpService : BaseService<LineEmp>, ILineEmpService
    {
        public LineEmpService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产线员工关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<LineEmpDto> GetList(LineEmpQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .LeftJoin<Employee>((it, l, e) => it.EmpCode == e.EmpCode)
                .Select((it, l, e) => new LineEmpDto
                {
                    LineName = l.LineName,
                    EmpName = e.EmpName
                },true)
                .ToPage<LineEmpDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public LineEmp GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.Id == Id)
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .LeftJoin<Employee>((it, l, e) => it.EmpCode == e.EmpCode)
                .Select((it, l, e) => new LineEmp
                {
                    LineName = l.LineName,
                    EmpName = e.EmpName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产线员工关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public LineEmp AddLineEmp(LineEmp model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产线员工关联
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateLineEmp(LineEmp model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<LineEmp> QueryExp(LineEmpQueryDto parm)
        {
            var predicate = Expressionable.Create<LineEmp>();

            predicate = predicate.AndIF(parm.LineId != null, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EmpCode), it => it.EmpCode == parm.EmpCode);
            return predicate;
        }
    }
}