using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 传输通道service接口
    /// </summary>
    public interface IIotCommonChannelService : IBaseService<IotCommonChannel>
    {
        PagedInfo<IotCommonChannelDto> GetList(IotCommonChannelQueryDto parm);

        IotCommonChannel GetInfo(int ChannelId);

        IotCommonChannel AddIotCommonChannel(IotCommonChannel parm);

        int UpdateIotCommonChannel(IotCommonChannel parm);

        PagedInfo<DictDataDto> GetDict(IotCommonChannelQueryDto parm);
    }
}