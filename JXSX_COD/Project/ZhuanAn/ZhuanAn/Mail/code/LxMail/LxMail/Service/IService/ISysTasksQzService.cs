using LxMail.CoreService;
using LxMail.Models;
using LxMail.Models.Dto;

namespace LxMail.Service
{
    public interface ISysTasksQzService : IBaseService<SysTasks>
    {
        PagedInfo<SysTasks> SelectTaskList(TasksQueryDto parm);

        //SysTasksQz GetId(object id);
        int AddTasks(SysTasks parm);

        int UpdateTasks(SysTasks parm);
    }
}