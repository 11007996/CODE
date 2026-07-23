using EAM.Model;
using EAM.Model.Consumable;
using EAM.Model.Dto;

namespace EAM.Service.Consumable.IConsumableService
{
    /// <summary>
    /// 耗品通知配置service接口
    /// </summary>
    public interface IConsumableConfigNoticeService : IBaseService<ConsumableConfigNotice>
    {
        PagedInfo<ConsumableConfigNoticeDto> GetList(ConsumableConfigNoticeQueryDto parm);

        ConsumableConfigNotice GetInfo(int NoticeConfigId);

        ConsumableConfigNotice AddConsumableConfigNotice(ConsumableConfigNotice parm);

        int UpdateConsumableConfigNotice(ConsumableConfigNotice parm);
    }
}