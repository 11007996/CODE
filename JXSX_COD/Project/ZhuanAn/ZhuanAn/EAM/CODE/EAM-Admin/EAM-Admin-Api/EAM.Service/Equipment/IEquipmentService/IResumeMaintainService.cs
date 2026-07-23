using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 履历保养记录service接口
    /// </summary>
    public interface IResumeMaintainService : IBaseService<ResumeMaintain>
    {
        PagedInfo<ResumeMaintainDto> GetList(ResumeMaintainQueryDto parm);

        ResumeMaintain GetInfo(int Id);

        ResumeMaintain AddResumeMaintain(ResumeMaintain parm);

        int UpdateResumeMaintain(ResumeMaintain parm);
    }
}