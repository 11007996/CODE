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
    /// 设备计划停机时间Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentPlanTimeService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentPlanTimeService : BaseService<EquipmentPlanTime>, IEquipmentPlanTimeService
    {
        public EquipmentPlanTimeService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询设备计划停机时间列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentPlanTimeDto> GetList(EquipmentPlanTimeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("StartTime asc")
                .Where(predicate.ToExpression())
                .ToPage<EquipmentPlanTime, EquipmentPlanTimeDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EquipmentPlanTime GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.PlanId == Id)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备计划停机时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentPlanTime AddEquipmentPlanTime(EquipmentPlanTime model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改设备计划停机时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentPlanTime(EquipmentPlanTime model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentPlanTime> QueryExp(EquipmentPlanTimeQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentPlanTime>();

            return predicate;
        }
    }
}