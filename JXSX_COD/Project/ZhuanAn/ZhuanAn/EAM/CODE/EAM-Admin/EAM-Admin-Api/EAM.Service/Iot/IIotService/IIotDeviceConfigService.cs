using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 设备配置service接口
    /// </summary>
    public interface IIotDeviceConfigService : IBaseService<IotDeviceConfig>
    {
        PagedInfo<IotDeviceConfigDto> GetList(IotDeviceConfigQueryDto parm);

        IotDeviceConfig GetInfo(int DeviceId);

        IotDeviceConfig AddIotDeviceConfig(IotDeviceConfig parm);

        int UpdateIotDeviceConfig(IotDeviceConfig parm);


    }
}