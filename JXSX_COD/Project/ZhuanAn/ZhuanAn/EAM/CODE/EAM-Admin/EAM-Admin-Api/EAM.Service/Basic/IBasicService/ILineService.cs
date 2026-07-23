using EAM.Model;
using EAM.Model.Basic;

using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 产线信息service接口
    /// </summary>
    public interface ILineService : IBaseService<Line>
    {
        PagedInfo<LineDto> GetList(LineQueryDto parm);

        Line GetInfo(int LineId);

        Line AddLine(Line parm);

        int UpdateLine(Line parm);

        PagedInfo<DictDataDto> GetDict(LineQueryDto parm);
    }
}