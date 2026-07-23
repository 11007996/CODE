using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 保养计划service接口
    /// </summary>
    public interface IMaintainPlanService : IBaseService<MaintainPlan>
    {
        PagedInfo<MaintainPlanDto> GetList(MaintainPlanQueryDto parm);

        MaintainPlan GetInfo(int PlanId);

        int AddMaintainPlan(MaintainPlanBatchAddDto parm);

        int UpdateMaintainPlan(MaintainPlan parm);

        PagedInfo<EquipmentSimpleDto> GetExcludeEquipment(ExcludeEquipmentQueryDto parm);
    }
}