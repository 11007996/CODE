using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Basic;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 产线员工关联service接口
    /// </summary>
    public interface ILineEmpService : IBaseService<LineEmp>
    {
        PagedInfo<LineEmpDto> GetList(LineEmpQueryDto parm);

        LineEmp GetInfo(int Id);

        LineEmp AddLineEmp(LineEmp parm);

        int UpdateLineEmp(LineEmp parm);
    }
}