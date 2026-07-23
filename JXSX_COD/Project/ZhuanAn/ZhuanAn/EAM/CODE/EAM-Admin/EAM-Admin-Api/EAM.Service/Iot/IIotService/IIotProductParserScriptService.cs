using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 数据解析脚本service接口
    /// </summary>
    public interface IIotProductParserScriptService : IBaseService<IotProductParserScript>
    {
        PagedInfo<IotProductParserScriptDto> GetList(IotProductParserScriptQueryDto parm);

        IotProductParserScript GetInfo(int ProductId);

        IotProductParserScript AddIotProductParserScript(IotProductParserScript parm);

        int UpdateIotProductParserScript(IotProductParserScript parm);
    }
}