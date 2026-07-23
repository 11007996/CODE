using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Fixture;

namespace EAM.Service.Fixture.IFixtureService
{
    /// <summary>
    /// 治具存储service接口
    /// </summary>
    public interface IFixtureStorageService : IBaseService<FixtureStorage>
    {
        PagedInfo<FixtureStorageDto> GetList(FixtureStorageQueryDto parm);

        FixtureStorage GetInfo(FixtureStorageInfoDto parm);

        FixtureStorageRecord AddFixtureStorageRecord(FixtureStorageRecord parm);

        int DeleteFixtureStorage(FixtureStorageInfoDto parm);

        bool InFixtureStorage(OperateFixtureStorageDto parm);

        bool BatchInFixtureStorage(List<OperateFixtureStorageDto> parm);

        bool OutFixtureStorage(OperateFixtureStorageDto parm);

        bool ScrappedFixtureStorage(OperateFixtureStorageDto parm);

        bool ReceiveFixtureStorage(OperateFixtureStorageDto model);

        int BatchReceiveFixtureStorage(List<OperateFixtureStorageDto> models);

        bool BackFixtureStorage(OperateFixtureStorageDto model);

        int BatchBackFixtureStorage(List<OperateFixtureStorageDto> model);

        bool TransferFixtureStorage(OperateFixtureStorageDto model);

        PagedInfo<FixtureStorageDto> ExportList(FixtureStorageQueryDto parm);

        (bool, object) ImportFixtureStorageCheck(List<FixtureStorageImportDto> list);

        (bool, object) ImportFixtureStorage(List<FixtureStorageImportDto> list);

        (bool, object) ImportFixtureStorageOperateCheck(List<FixtureStorageOperateImportDto> list);

        (bool, object) ImportFixtureStorageOperate(List<FixtureStorageOperateImportDto> list);

        PagedInfo<FixtureStorageUsingDto> GetUsingList(FixtureStorageUsingQueryDto parm);

        FixtureStorageUsing GetUsingInfo(int fixtureUsingId);

        PagedInfo<FixtureStorageRecordDto> GetRecordList(FixtureStorageRecordQueryDto parm);
    }
}