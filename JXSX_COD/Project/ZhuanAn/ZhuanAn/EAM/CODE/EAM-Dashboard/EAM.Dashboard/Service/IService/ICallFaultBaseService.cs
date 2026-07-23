using EAM.Dashboard.Model.Dto;
using EAM.Model.Call;
using EAM.ServiceCore;

namespace EAM.Dashboard.Service.IService
{
    /// <summary>
    /// 呼叫记录service接口
    /// </summary>
    public interface ICallFaultBaseService : IBaseService<CallFaultBase>
    {
        List<CallArea> GetAreaList();

        List<CallFaultBaseVo> GetUnsolvedCallFaultBase(int Aread);

        List<ChartBaseVo<string, int>> GetMonthCountStat(int AreaId);

        List<ChartBaseVo<string, int>> GetWeekCountStat(int AreaId);

        List<ChartBaseVo<string, int>> GetHourCountStat(int AreaId);

        WeekCallFaultStat GetWeekDataAnalyseStat(int AreaId);

        List<ReadyCallScheduledTaskDto> GetCallScheduledTask(int AreaId, int intervalSeconds);
    }
}