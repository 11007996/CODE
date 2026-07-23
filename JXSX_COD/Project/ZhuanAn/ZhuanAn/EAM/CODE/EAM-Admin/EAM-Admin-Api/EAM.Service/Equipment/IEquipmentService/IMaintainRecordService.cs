using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备保养记录service接口
    /// </summary>
    public interface IMaintainRecordService : IBaseService<MaintainRecord>
    {
        PagedInfo<MaintainRecordDto> GetList(MaintainRecordQueryDto parm);

        MaintainRecord GetInfo(int Id);

        MaintainRecord AddMaintainRecord(MaintainRecord parm);

        int UpdateMaintainRecord(MaintainRecord parm);

        int DeleteMaintainRecord(int[] Id);

        PagedInfo<MaintainRecordDto> ExportList(MaintainRecordQueryDto parm);

        MaintainRecord GetDetail(MaintainRecordQueryDetailDto param);

        int GlobalMaintainRecord(GlobalMaintainRecordDto param);
    }
}