using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 工站信息service接口
    /// </summary>
    public interface IStationService : IBaseService<Station>
    {
        PagedInfo<StationDto> GetList(StationQueryDto parm);

        Station GetInfo(int StationId);

        Station AddStation(Station parm);

        int UpdateStation(Station parm);

        PagedInfo<DictDataDto> GetDict(StationQueryDto parm);
    }
}