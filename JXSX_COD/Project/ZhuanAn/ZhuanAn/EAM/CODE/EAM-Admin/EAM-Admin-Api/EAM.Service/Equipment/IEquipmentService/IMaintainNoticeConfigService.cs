using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 保养通知配置service接口
    /// </summary>
    public interface IMaintainNoticeConfigService : IBaseService<MaintainNoticeConfig>
    {
        PagedInfo<MaintainNoticeConfigDto> GetList(MaintainNoticeConfigQueryDto parm);

        MaintainNoticeConfig GetInfo(int NoticeConfigId);

        MaintainNoticeConfig AddMaintainNoticeConfig(MaintainNoticeConfig parm);

        int UpdateMaintainNoticeConfig(MaintainNoticeConfig parm);

        int DeleteMaintainNoticeConfig(int[] NoticeConfigIds);
    }
}