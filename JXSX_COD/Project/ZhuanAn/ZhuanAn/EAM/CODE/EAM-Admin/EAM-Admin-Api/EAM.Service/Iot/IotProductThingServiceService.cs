using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 产品物模型服务Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductThingServiceService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductThingServiceService : BaseService<IotProductThingService>, IIotProductThingServiceService
    {
        public IotProductThingServiceService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品物模型服务列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductThingServiceDto> GetList(IotProductThingServiceQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProductThingService, IotProductThingServiceDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        public IotProductThingService GetInfo(int ServiceId)
        {
            var response = Queryable()
                .Where(x => x.ServiceId == ServiceId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品物模型服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductThingService AddIotProductThingService(IotProductThingService model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品物模型服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductThingService(IotProductThingService model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductThingService> QueryExp(IotProductThingServiceQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductThingService>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ServiceName), it => it.ServiceName.Contains(parm.ServiceName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Identifier), it => it.Identifier == parm.Identifier);
            return predicate;
        }
    }
}