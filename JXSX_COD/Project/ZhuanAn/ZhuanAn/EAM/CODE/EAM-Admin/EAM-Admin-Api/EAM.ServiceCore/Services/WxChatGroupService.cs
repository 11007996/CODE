using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.Repository;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 微信聊天群Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IWxChatGroupService), ServiceLifetime = LifeTime.Transient)]
    public class WxChatGroupService : BaseService<WxChatGroup>, IWxChatGroupService
    {
        private readonly IHttpContextAccessor _ContextAccessor;
        public WxChatGroupService(IHttpContextAccessor contextAccessor)
        {
            _ContextAccessor = contextAccessor;
        }
        /// <summary>
        /// 查询微信聊天群列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<WxChatGroupDto> GetList(WxChatGroupQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<WxChatGroup, WxChatGroupDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ChatId"></param>
        /// <returns></returns>
        public WxChatGroup GetInfo(string ChatId)
        {
            var response = Queryable()
                .Where(x => x.ChatId == ChatId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加微信聊天群
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WxChatGroup AddWxChatGroup(WxChatGroup model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改微信聊天群
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateWxChatGroup(WxChatGroup model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询微信聊天群字典
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(WxChatGroupQueryDto parm)
        {
            //限制厂区只能选择自己的群
            if (string.IsNullOrEmpty(parm.FactoryId))
                parm.FactoryId = HttpContextExtension.GetFactoryId(_ContextAccessor.HttpContext);

            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictValue = it.ChatId,
                    DictLabel = it.ChatName,
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<WxChatGroup> QueryExp(WxChatGroupQueryDto parm)
        {
            var predicate = Expressionable.Create<WxChatGroup>();

            predicate.AndIF(!string.IsNullOrEmpty(parm.FactoryId), it => it.FactoryId == parm.FactoryId);

            return predicate;
        }
    }
}