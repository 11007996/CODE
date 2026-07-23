using EAM.Model;
using EAM.Repository;
using Microsoft.AspNetCore.Http;

namespace EAM.ServiceCore
{
    /// <summary>
    /// 基础服务定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T> : BaseRepository<T> where T : class, new()
    {
        public BaseService()
        { }

        public BaseService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }
        public BaseService(ISqlSugarClient context) : base(context)
        {
        }

    }
}