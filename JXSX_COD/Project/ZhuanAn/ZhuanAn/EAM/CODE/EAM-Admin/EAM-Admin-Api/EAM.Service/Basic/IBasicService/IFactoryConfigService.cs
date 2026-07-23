using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 厂区配置service接口
    /// </summary>
    public interface IFactoryConfigService : IBaseService<FactoryConfig>
    {
        PagedInfo<FactoryConfigDto> GetList(FactoryConfigQueryDto parm);

        FactoryConfig GetInfo(int Id);

        FactoryConfig GetInfoByKey(string key);

        FactoryConfig AddFactoryConfig(FactoryConfig parm);

        int UpdateFactoryConfig(FactoryConfig parm);
    }
}