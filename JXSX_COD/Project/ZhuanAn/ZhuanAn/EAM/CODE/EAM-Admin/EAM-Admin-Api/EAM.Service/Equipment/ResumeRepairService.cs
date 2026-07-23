using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 履历维修记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IResumeRepairService), ServiceLifetime = LifeTime.Transient)]
    public class ResumeRepairService : BaseService<ResumeRepair>, IResumeRepairService
    {
        public ResumeRepairService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询履历维修记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ResumeRepairDto> GetList(ResumeRepairQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .OrderByIF(parm.Sort?.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("desc") ? OrderByType.Desc : OrderByType.Asc)
                .Select((it, e) => new ResumeRepairDto()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                }, true)
                .ToPageNoSort(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResumeRepair GetInfo(int Id)
        {
            var response = Queryable()
                .Where(it => it.Id == Id)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new ResumeRepair
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加履历维修记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResumeRepair AddResumeRepair(ResumeRepair model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改履历维修记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateResumeRepair(ResumeRepair model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ResumeRepair> QueryExp(ResumeRepairQueryDto parm)
        {
            var predicate = Expressionable.Create<ResumeRepair>();

            predicate = predicate.AndIF(parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.BeginRepairDate != null, it => it.RepairDate >= parm.BeginRepairDate);
            predicate = predicate.AndIF(parm.EndRepairDate != null, it => it.RepairDate <= parm.EndRepairDate);
            return predicate;
        }
    }
}