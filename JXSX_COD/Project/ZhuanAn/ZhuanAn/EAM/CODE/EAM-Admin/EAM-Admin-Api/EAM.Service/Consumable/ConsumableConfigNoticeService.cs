using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Consumable;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Consumable.IConsumableService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Consumable
{
    /// <summary>
    /// 耗品通知配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IConsumableConfigNoticeService), ServiceLifetime = LifeTime.Transient)]
    public class ConsumableConfigNoticeService : BaseService<ConsumableConfigNotice>, IConsumableConfigNoticeService
    {
        private readonly IWxChatGroupService _wxChatGroupService;

        public ConsumableConfigNoticeService(IHttpContextAccessor contextAccessor, IWxChatGroupService wxChatGroupService) : base(contextAccessor)
        {
            _wxChatGroupService = wxChatGroupService;
        }

        /// <summary>
        /// 查询耗品通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ConsumableConfigNoticeDto> GetList(ConsumableConfigNoticeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<ConsumableConfigNotice, ConsumableConfigNoticeDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        public ConsumableConfigNotice GetInfo(int NoticeConfigId)
        {
            var response = Queryable()
                .Where(it => it.NoticeConfigId == NoticeConfigId)
                .First();

            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.EmpCodes))
                {
                    string[] empcodes = response.EmpCodes.Split(',');
                    response.EmpNav = Context.Queryable<Employee>().Where(it => empcodes.Contains(it.EmpCode))
                        .Select(it => new EmpSimpleDto()
                        {
                            EmpCode = it.EmpCode,
                            EmpName = it.EmpName,
                        })
                        .ToList();
                }
                if (!string.IsNullOrEmpty(response.WxChatId))
                {
                    response.WxChatName = _wxChatGroupService.GetById(response.WxChatId)?.ChatName;
                }
            }

            return response;
        }

        /// <summary>
        /// 添加耗品通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ConsumableConfigNotice AddConsumableConfigNotice(ConsumableConfigNotice model)
        {
            CheckData(model);

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改耗品通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateConsumableConfigNotice(ConsumableConfigNotice model)
        {
            CheckData(model);

            return Update(model);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ConsumableConfigNotice> QueryExp(ConsumableConfigNoticeQueryDto parm)
        {
            var predicate = Expressionable.Create<ConsumableConfigNotice>();

            return predicate;
        }

        /// <summary>
        /// 检查表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool CheckData(ConsumableConfigNotice model)
        {
            if (model.NoticeConfigId <= 0)
            {
                int count = Queryable().Count();
                if (count > 0)
                    throw new CustomException("已存在配置，请匆重复添加");
            }

            //检查通知对象
            if (string.IsNullOrEmpty(model.WxChatId) && string.IsNullOrEmpty(model.EmpCodes))
                throw new CustomException("通知对象【微信群】与【员工】必需填写一项");
            if (!string.IsNullOrEmpty(model.WxChatId) && !string.IsNullOrEmpty(model.EmpCodes))
                throw new CustomException("通知对象【微信群】与【员工】只需要填写一项");

            if (!string.IsNullOrEmpty(model.WxChatId))
            {//检查微信群
                string factoryId = Context.CurrentConnectionConfig.ConfigId.ToString();
                var wx = _wxChatGroupService.Queryable().Where(it => it.ChatId == model.WxChatId && it.FactoryId == factoryId).First();
                if (wx == null)
                    throw new CustomException("微信群ID不存在");
            }
            else
            {//检查员工
                List<string> empcodes = model.EmpCodes.Split(',').ToList();
                if (empcodes.Count != empcodes.Distinct().Count())
                    throw new CustomException("通知员工存在相同的工号");
                int count = Context.Queryable<Employee>().Where(it => empcodes.Contains(it.EmpCode)).Count();
                if (count != empcodes.Count())
                    throw new CustomException("存在无效的员工工号");
            }

            return true;
        }
    }
}