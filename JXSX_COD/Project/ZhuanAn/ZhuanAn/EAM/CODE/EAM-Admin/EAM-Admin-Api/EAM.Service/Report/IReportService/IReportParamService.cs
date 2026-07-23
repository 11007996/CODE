using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Report;

namespace EAM.Service.Report.IReportService
{
    /// <summary>
    /// 报表参数service接口
    /// </summary>
    public interface IReportParamService : IBaseService<ReportParam>
    {
        PagedInfo<ReportParamDto> GetList(ReportParamQueryDto parm);

        ReportParam GetInfo(int ParamId);

        ReportParam AddReportParam(ReportParam parm);

        int UpdateReportParam(ReportParam parm);
    }
}