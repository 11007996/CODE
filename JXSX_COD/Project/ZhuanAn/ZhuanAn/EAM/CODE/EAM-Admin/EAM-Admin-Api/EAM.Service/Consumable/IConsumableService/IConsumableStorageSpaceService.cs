using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;

namespace EAM.Service.Consumable.IConsumableService
{
    /// <summary>
    /// 耗品储位信息service接口
    /// </summary>
    public interface IConsumableStorageSpaceService : IBaseService<ConsumableStorageSpace>
    {
        PagedInfo<ConsumableStorageSpaceDto> GetList(ConsumableStorageSpaceQueryDto parm);

        ConsumableStorageSpace GetInfo(int StorageId);

        List<ConsumableStorageSpace> GetTreeList(ConsumableStorageSpaceTreeQueryDto parm);

        ConsumableStorageSpace AddStorageSpace(ConsumableStorageSpace parm);

        int UpdateStorageSpace(ConsumableStorageSpace parm);

        int DeleteStorageSpace(int StorageId);
    }
}