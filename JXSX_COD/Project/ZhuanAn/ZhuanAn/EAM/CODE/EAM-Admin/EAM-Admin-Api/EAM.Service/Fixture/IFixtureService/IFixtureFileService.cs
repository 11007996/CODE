using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Model.System;

namespace EAM.Service.Fixture.IFixtureService
{
    /// <summary>
    /// 治具文件关联service接口
    /// </summary>
    public interface IFixtureFileService : IBaseService<FixtureFile>
    {
        PagedInfo<FixtureFileDto> GetList(FixtureFileQueryDto parm);

        PagedInfo<SysFile> GetFileList(FixtureFileQueryDto parm);

        FixtureFile GetInfo(int FixtureId);

        FixtureFile AddFixtureFile(FixtureFile parm);

        int BatchAddFixtureFile(List<FixtureFile> parm);

        int UpdateFixtureFile(FixtureFile parm);

        int DeleteFixtureFile(FixtureFileDto parm);
    }
}