using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;

namespace EAM.Service.Fixture.IFixtureService
{
    /// <summary>
    /// 治具信息service接口
    /// </summary>
    public interface IFixtureBaseService : IBaseService<FixtureBase>
    {
        PagedInfo<FixtureBaseDto> GetList(FixtureBaseQueryDto parm);

        PagedInfo<FixtureDetailDto> GetFixtureDetailList(FixtureBaseQueryDto parm);

        PagedInfo<IdleFixtureDto> IdleFixtureList(FixtureBaseQueryDto parm);

        FixtureBase GetInfo(int FixtureId);

        FixtureBase AddFixtureBase(FixtureBase parm);

        int UpdateFixtureBase(FixtureBase parm);

        int DeleteFixtureBase(int[] idArr);

        PagedInfo<FixtureDetailDto> ExportList(FixtureBaseQueryDto parm);

        PagedInfo<DictDataDto> GetDict(FixtureBaseQueryDto parm);

        (string, object, object) ImportFixtureBase(List<FixtureBase> list);
    }
}