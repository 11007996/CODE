using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;
using System.Data;

namespace EAM.Service.Report.IReportService
{
    /// <summary>
    /// 报表执行service接口
    /// </summary>
    public interface IReportExecuteService : IBaseService<ReportBase>
    {
        ReportInfoDto GetReportInfo(int ReportId);

        List<DictDataDto> GetReportParamOptions(ReportParamOptionsQueryDto parm);

        PagedInfo<DataTable> GetPageList(ReportExecuteQueryDto parm);

        DataTable GetAllList(ReportExecuteQueryDto parm);
    }
}