using EAM.Model;
using EAM.ServiceCore.Model;
using EAM.ServiceCore.Model.Dto;

namespace EAM.ServiceCore.Monitor.IMonitorService
{
    /// <summary>
    /// 用户在线时长service接口
    /// </summary>
    public interface IUserOnlineLogService : IBaseService<UserOnlineLog>
    {
        PagedInfo<UserOnlineLogDto> GetList(UserOnlineLogQueryDto parm);

        UserOnlineLog AddUserOnlineLog(UserOnlineLog parm);

        PagedInfo<UserOnlineLogDto> ExportList(UserOnlineLogQueryDto parm);
    }
}