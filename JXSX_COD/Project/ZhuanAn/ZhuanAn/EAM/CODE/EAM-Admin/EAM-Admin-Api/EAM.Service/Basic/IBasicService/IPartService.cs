using EAM.Model;
using EAM.Model.Basic;

using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 料号service接口
    /// </summary>
    public interface IPartService : IBaseService<Part>
    {
        PagedInfo<PartDto> GetList(PartQueryDto parm);

        Part GetInfo(int PartId);

        Part AddPart(Part parm);

        int UpdatePart(Part parm);

        PagedInfo<DictDataDto> GetDict(PartQueryDto parm);
    }
}