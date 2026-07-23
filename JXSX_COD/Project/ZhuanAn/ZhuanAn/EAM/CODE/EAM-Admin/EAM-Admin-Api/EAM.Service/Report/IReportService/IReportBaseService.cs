using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;

namespace EAM.Service.Report.IReportService
{
    /// <summary>
    /// 报表基本信息service接口
    /// </summary>
    public interface IReportBaseService : IBaseService<ReportBase>
    {
        PagedInfo<ReportBaseDto> GetList(ReportBaseQueryDto parm);

        ReportBase GetInfo(int ReportId);

        ReportBase AddReportBase(ReportBase parm);

        int UpdateReportBase(ReportBase parm);

        PagedInfo<DictDataDto> GetDict(ReportBaseQueryDto parm);
    }
}