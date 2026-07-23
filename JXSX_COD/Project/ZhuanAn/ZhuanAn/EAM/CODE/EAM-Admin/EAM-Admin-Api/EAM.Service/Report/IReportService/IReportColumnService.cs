using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;

namespace EAM.Service.Report.IReportService
{
    /// <summary>
    /// 报表数据列service接口
    /// </summary>
    public interface IReportColumnService : IBaseService<ReportColumn>
    {
        PagedInfo<ReportColumnDto> GetList(ReportColumnQueryDto parm);

        ReportColumn GetInfo(int ColumnId);

        ReportColumn AddReportColumn(ReportColumn parm);

        int UpdateReportColumn(ReportColumn parm);
    }
}