using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备保养项目service接口
    /// </summary>
    public interface IMaintainItemService : IBaseService<MaintainItem>
    {
        PagedInfo<MaintainItemDto> GetList(MaintainItemQueryDto parm);

        MaintainItem GetInfo(int Id);

        MaintainItem AddMaintainItem(MaintainItem parm);

        int UpdateMaintainItem(MaintainItem parm);

        int DeleteMaintainItem(int[] Id);

        (string, object, object) ImportMaintainItem(List<MaintainItem> list);

        PagedInfo<MaintainItemDto> ExportList(MaintainItemQueryDto parm);

        int CloneMaintainItem(MaintainItemCloneDto parm);
    }
}