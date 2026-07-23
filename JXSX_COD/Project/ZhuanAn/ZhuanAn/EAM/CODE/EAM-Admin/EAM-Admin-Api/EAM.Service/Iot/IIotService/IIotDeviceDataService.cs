using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 设备采集数据service接口
    /// </summary>
    public interface IIotDeviceDataService : IBaseService<IotDeviceData>
    {
        PagedInfo<IotDeviceDataDto> GetList(IotDeviceDataQueryDto parm);

        IotDeviceData GetInfo(int DeviceId);

        IotDeviceData AddIotDeviceData(IotDeviceData parm);
    }
}