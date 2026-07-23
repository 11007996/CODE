using EAM.Model;
using EAM.Model.Basic;

using EAM.Model.Dto;

namespace EAM.Service.Basic.IBasicService
{
    /// <summary>
    /// 员工信息service接口
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee>
    {
        PagedInfo<EmployeeDto> GetList(EmployeeQueryDto parm);

        Employee GetInfo(string empCode);

        Employee AddEmployee(Employee parm);

        int UpdateEmployee(Employee parm);

        int DeleteEmployee(string empCode);

        PagedInfo<DictDataDto> GetDict(EmployeeQueryDto parm);

        Employee AddEmployeeFromUser(string username);

        (string, object, object) ImportEmployee(List<Employee> models);
    }
}