using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 设备日志service接口
    /// </summary>
    public interface IIotDeviceLogService : IBaseService<IotDeviceLog>
    {
        PagedInfo<IotDeviceLogDto> GetList(IotDeviceLogQueryDto parm);

        IotDeviceLog GetInfo(long LogId);

        IotDeviceLog AddIotDeviceLog(IotDeviceLog parm);
    }
}