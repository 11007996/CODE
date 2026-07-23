using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Call;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 广播定时任务service接口
    /// </summary>
    public interface ICallScheduledTaskService : IBaseService<CallScheduledTask>
    {
        PagedInfo<CallScheduledTaskDto> GetList(CallScheduledTaskQueryDto parm);

        CallScheduledTask GetInfo(int CallTaskId);

        CallScheduledTask AddCallScheduledTask(CallScheduledTask parm);

        int UpdateCallScheduledTask(CallScheduledTask parm);
    }
}