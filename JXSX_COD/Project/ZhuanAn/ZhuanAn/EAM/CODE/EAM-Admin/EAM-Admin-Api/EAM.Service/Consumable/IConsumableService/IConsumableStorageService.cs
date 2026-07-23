using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;

namespace EAM.Service.Consumable.IConsumableService
{
    /// <summary>
    /// 耗品存储表service接口
    /// </summary>
    public interface IConsumableStorageService : IBaseService<ConsumableStorage>
    {
        PagedInfo<ConsumableStorageDto> GetList(ConsumableStorageQueryDto parm);

        ConsumableStorage GetInfo(ConsumableStorageInfoDto parm);

        bool InConsumableStorage(OperateConsumableStorageDto parm);

        bool OutConsumableStorage(OperateConsumableStorageDto parm);

        bool ScrappedConsumableStorage(OperateConsumableStorageDto parm);

        bool ReceiveConsumableStorage(OperateConsumableStorageDto model);

        bool BackConsumableStorage(OperateConsumableStorageDto model);

        bool TransferConsumableStorage(OperateConsumableStorageDto model);

        int DeleteConsumableStorage(ConsumableStorageInfoDto parm);

        PagedInfo<ConsumableStorageDto> ExportList(ConsumableStorageQueryDto parm);

        (bool, object) ImportConsumableStorageCheck(List<ConsumableStorageImportDto> list);

        (bool, object) ImportConsumableStorage(List<ConsumableStorageImportDto> list);

        (bool, object) ImportConsumableStorageOperateCheck(List<ConsumableStorageOperateImportDto> list);

        (bool, object) ImportConsumableStorageOperate(List<ConsumableStorageOperateImportDto> list);

        PagedInfo<ConsumableStorageRecordDto> GetRecordList(ConsumableStorageRecordQueryDto parm);
    }
}