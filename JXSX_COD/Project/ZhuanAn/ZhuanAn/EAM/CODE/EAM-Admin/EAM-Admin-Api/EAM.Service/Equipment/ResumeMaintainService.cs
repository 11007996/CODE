using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 履历保养记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IResumeMaintainService), ServiceLifetime = LifeTime.Transient)]
    public class ResumeMaintainService : BaseService<ResumeMaintain>, IResumeMaintainService
    {
        public ResumeMaintainService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询履历保养记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ResumeMaintainDto> GetList(ResumeMaintainQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Employee>((it, e, emp) => it.ExecuteUser == emp.EmpCode)
                .OrderByIF(parm.Sort?.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("desc") ? OrderByType.Desc : OrderByType.Asc)
                .Select((it, e, emp) => new ResumeMaintainDto
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    ExecuteUserName = emp.EmpName
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResumeMaintain GetInfo(int Id)
        {
            var response = Queryable()
                .Where(it => it.Id == Id)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .LeftJoin<Employee>((it, e, emp) => it.ExecuteUser == emp.EmpCode)
                .Select((it, e, emp) => new ResumeMaintain
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    ExecuteUserName = emp.EmpName
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加履历保养记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResumeMaintain AddResumeMaintain(ResumeMaintain model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改履历保养记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateResumeMaintain(ResumeMaintain model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ResumeMaintain> QueryExp(ResumeMaintainQueryDto parm)
        {
            var predicate = Expressionable.Create<ResumeMaintain>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.BeginExecuteDate != null, it => it.ExecuteDate >= parm.BeginExecuteDate);
            predicate = predicate.AndIF(parm.EndExecuteDate != null, it => it.ExecuteDate <= parm.EndExecuteDate);
            return predicate;
        }
    }
}