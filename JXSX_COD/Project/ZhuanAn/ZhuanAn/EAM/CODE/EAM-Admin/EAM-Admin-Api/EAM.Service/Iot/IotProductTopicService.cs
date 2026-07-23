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
    /// 产品主题类表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotProductTopicService), ServiceLifetime = LifeTime.Transient)]
    public class IotProductTopicService : BaseService<IotProductTopic>, IIotProductTopicService
    {
        public IotProductTopicService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品主题类表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotProductTopicDto> GetList(IotProductTopicQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("TopicFormat asc")
                .Where(predicate.ToExpression())
                .ToPage<IotProductTopic, IotProductTopicDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        public IotProductTopic GetInfo(int TopicId)
        {
            var response = Queryable()
                .Where(x => x.TopicId == TopicId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品主题类表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotProductTopic AddIotProductTopic(IotProductTopic model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品主题类表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotProductTopic(IotProductTopic model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotProductTopic> QueryExp(IotProductTopicQueryDto parm)
        {
            var predicate = Expressionable.Create<IotProductTopic>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TopicName), it => it.TopicName.Contains(parm.TopicName));
            return predicate;
        }
    }
}