using EAM.Model;
using EAM.Model.Call;

using EAM.Model.Dto;

namespace EAM.Service.Call.ICallService
{
    /// <summary>
    /// 呼叫记录service接口
    /// </summary>
    public interface ICallFaultBaseService : IBaseService<CallFaultBase>
    {
        PagedInfo<CallFaultBaseDto> GetList(CallFaultBaseQueryDto parm);

        CallFaultBase GetInfo(int CallId);

        List<CallFaultBaseDto> GetUnsolvedCallFaultBase(int LineId);

        LineCallSummaryDto GetCallSummaryByLine(int LineId);

        PagedInfo<CallFaultBaseDto> ExportList(CallFaultBaseQueryDto parm);

        CallFaultBase AddCallFaultBase(CallFaultBase parm);

        bool HandleCallFaultBase(CallOperateDto parm);

        bool RequestHelpCallFaultBase(CallOperateDto parm);

        bool HelpCallFaultBase(CallOperateDto parm);

        bool SolveCallFaultBase(CallOperateDto parm);

        bool StopCallFaultBase(int[] ids, string operatorNo);

        int DeleteCallFaultBase(int[] CallId);
    }
}