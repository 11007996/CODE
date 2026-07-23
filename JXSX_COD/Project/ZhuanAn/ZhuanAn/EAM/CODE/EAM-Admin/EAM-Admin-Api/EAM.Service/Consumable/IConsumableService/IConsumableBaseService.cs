using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;

namespace EAM.Service.Consumable.IConsumableService
{
    /// <summary>
    /// 耗品表service接口
    /// </summary>
    public interface IConsumableBaseService : IBaseService<ConsumableBase>
    {
        PagedInfo<ConsumableBaseDto> GetList(ConsumableBaseQueryDto parm);

        PagedInfo<ConsumableDetailDto> GetDetailList(ConsumableBaseQueryDto parm);

        ConsumableBase GetInfo(int ConsumableId);

        ConsumableBase AddConsumableBase(ConsumableBase parm);

        int UpdateConsumableBase(ConsumableBase parm);

        int DeleteConsumableBase(int[] idArr);

        (string, object, object) ImportConsumableBase(List<ConsumableBase> list);

        PagedInfo<ConsumableDetailDto> ExportList(ConsumableBaseQueryDto parm);

        PagedInfo<DictDataDto> GetDict(ConsumableBaseQueryDto parm);

        PagedInfo<DictDataDto> GetCategoryDict(ConsumableBaseQueryDto parm);
    }
}