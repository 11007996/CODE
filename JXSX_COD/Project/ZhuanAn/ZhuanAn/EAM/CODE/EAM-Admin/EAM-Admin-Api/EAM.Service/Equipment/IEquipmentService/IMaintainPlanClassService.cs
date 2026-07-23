using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 保养计划班次service接口
    /// </summary>
    public interface IMaintainPlanClassService : IBaseService<MaintainPlanClass>
    {
        PagedInfo<MaintainPlanClassDto> GetList(MaintainPlanClassQueryDto parm);

        MaintainPlanClass GetInfo(int planClassId);

        MaintainPlanClass AddMaintainPlanClass(MaintainPlanClass model);

        int UpdateMaintainPlanClass(MaintainPlanClass model);

        int DeleteMaintainPlanClass(int[] planClassIds);

        PagedInfo<DictDataDto> GetDict(MaintainPlanClassQueryDto parm);
    }
}