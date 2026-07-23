using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 呼叫通知配置service接口
    /// </summary>
    public interface ICallConfigNoticeService : IBaseService<CallConfigNotice>
    {
        PagedInfo<CallConfigNoticeDto> GetList(CallConfigNoticeQueryDto parm);

        CallConfigNotice GetInfo(int NoticeConfigId);

        CallConfigNotice AddCallConfigNotice(CallConfigNotice parm);

        int UpdateCallConfigNotice(CallConfigNotice parm);

        bool DeleteCallConfigNotice(int NoticeConfigId);
    }
}