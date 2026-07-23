using EAM.Model;
using EAM.Model.System;
using EAM.Model.System.Dto;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 数据差异日志service接口
    /// </summary>
    public interface ISqlDiffLogService : IBaseService<SqlDiffLog>
    {
        PagedInfo<SqlDiffLogDto> GetList(SqlDiffLogQueryDto parm);

        SqlDiffLog GetInfo(long PId);

        SqlDiffLog AddSqlDiffLog(SqlDiffLog parm);

        int UpdateSqlDiffLog(SqlDiffLog parm);
    }
}