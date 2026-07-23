using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品设备表service接口
    /// </summary>
    public interface IIotDeviceService : IBaseService<IotDevice>
    {
        PagedInfo<IotDeviceDto> GetList(IotDeviceQueryDto parm);

        IotDeviceDto GetInfo(int DeviceId);

        IotDevice AddIotDevice(IotDevice parm);

        int UpdateIotDevice(IotDevice parm);

        PagedInfo<DictDataDto> GetDict(IotDeviceQueryDto parm);

        IotDeviceBind BindIotDevice(IotDeviceBind model);

        int UnBindIotDevice(int deviceId);
    }
}