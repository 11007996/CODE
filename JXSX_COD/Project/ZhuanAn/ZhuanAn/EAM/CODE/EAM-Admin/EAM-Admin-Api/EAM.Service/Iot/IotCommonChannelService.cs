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
    /// 传输通道Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotCommonChannelService), ServiceLifetime = LifeTime.Transient)]
    public class IotCommonChannelService : BaseService<IotCommonChannel>, IIotCommonChannelService
    {
        public IotCommonChannelService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询传输通道列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotCommonChannelDto> GetList(IotCommonChannelQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<IotCommonChannel, IotCommonChannelDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        public IotCommonChannel GetInfo(int ChannelId)
        {
            var response = Queryable()
                .Where(x => x.ChannelId == ChannelId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加传输通道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotCommonChannel AddIotCommonChannel(IotCommonChannel model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改传输通道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotCommonChannel(IotCommonChannel model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 传输通道字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(IotCommonChannelQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictLabel = it.ChannelName,
                    DictValue = it.ChannelId.ToString(),
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotCommonChannel> QueryExp(IotCommonChannelQueryDto parm)
        {
            var predicate = Expressionable.Create<IotCommonChannel>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ChannelName), it => it.ChannelName.Contains(parm.ChannelName));
            return predicate;
        }
    }
}