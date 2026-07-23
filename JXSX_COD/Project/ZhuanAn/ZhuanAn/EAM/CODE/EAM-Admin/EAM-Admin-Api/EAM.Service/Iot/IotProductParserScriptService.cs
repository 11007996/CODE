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
    /// 数据解析脚本Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductParserScriptService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductParserScriptService : BaseService<IotProductParserScript>, IIotProductParserScriptService
    {
        public IotProductParserScriptService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询数据解析脚本列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductParserScriptDto> GetList(IotProductParserScriptQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotProductParserScript, IotProductParserScriptDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public IotProductParserScript GetInfo(int ProductId)
        {
            var response = Queryable()
                .Where(x => x.ProductId == ProductId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加数据解析脚本
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductParserScript AddIotProductParserScript(IotProductParserScript model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改数据解析脚本
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductParserScript(IotProductParserScript model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductParserScript> QueryExp(IotProductParserScriptQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductParserScript>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            return predicate;
        }
    }
}