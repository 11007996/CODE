using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;

namespace EAM.Service.Fixture.IFixtureService
{
    /// <summary>
    /// 治具储位信息service接口
    /// </summary>
    public interface IFixtureStorageSpaceService : IBaseService<FixtureStorageSpace>
    {
        PagedInfo<FixtureStorageSpaceDto> GetList(FixtureStorageSpaceQueryDto parm);

        FixtureStorageSpace GetInfo(int StorageId);

        List<FixtureStorageSpace> GetTreeList(FixtureStorageSpaceTreeQueryDto parm);

        FixtureStorageSpace AddStorageSpace(FixtureStorageSpace parm);

        int UpdateStorageSpace(FixtureStorageSpace parm);

        int DeleteStorageSpace(int StorageId);
    }
}