using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;

namespace EAM.Service.Report.IReportService
{
    /// <summary>
    /// 报表分组service接口
    /// </summary>
    public interface IReportGroupService : IBaseService<ReportGroup>
    {
        PagedInfo<ReportGroupDto> GetList(ReportGroupQueryDto parm);

        ReportGroup GetInfo(int GroupId);

        ReportGroup AddReportGroup(ReportGroup parm);

        int UpdateReportGroup(ReportGroup parm);

        PagedInfo<DictDataDto> GetDict(ReportGroupQueryDto parm);
    }
}