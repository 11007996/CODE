using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;

namespace EAM.Service.Fixture.IFixtureService
{
    /// <summary>
    /// 治具料号关联表service接口
    /// </summary>
    public interface IFixturePartService : IBaseService<FixturePart>
    {
        PagedInfo<FixturePartDto> GetList(FixturePartQueryDto parm);

        FixturePart GetInfo(FixturePartQueryDto parm);

        FixturePart AddFixturePart(FixturePart parm);

        int UpdateFixturePart(FixturePart parm);

        int DeleteFixturePart(FixturePart parm);
    }
}