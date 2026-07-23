using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 履历维修记录service接口
    /// </summary>
    public interface IResumeRepairService : IBaseService<ResumeRepair>
    {
        PagedInfo<ResumeRepairDto> GetList(ResumeRepairQueryDto parm);

        ResumeRepair GetInfo(int Id);

        ResumeRepair AddResumeRepair(ResumeRepair parm);

        int UpdateResumeRepair(ResumeRepair parm);
    }
}