using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Call;
using EAM.Model.Dto;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 呼叫通知配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallConfigNoticeService), ServiceLifetime = LifeTime.Transient)]
    public class CallConfigNoticeService : BaseService<CallConfigNotice>, ICallConfigNoticeService
    {
        private readonly IWxChatGroupService _wxChatGroupService;

        public CallConfigNoticeService(IHttpContextAccessor contextAccessor, IWxChatGroupService wxChatGroupService) : base(contextAccessor)
        {
            _wxChatGroupService = wxChatGroupService;
        }

        /// <summary>
        /// 查询呼叫通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallConfigNoticeDto> GetList(CallConfigNoticeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<CallArea>((it, ca) => it.AreaId == ca.AreaId)
                .Where(predicate.ToExpression())
                .Select((it, ca) => new CallConfigNotice()
                {
                    AreaName = ca.AreaName,
                }, true)
                .ToPage<CallConfigNotice, CallConfigNoticeDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        public CallConfigNotice GetInfo(int NoticeConfigId)
        {
            var response = Queryable()
                .Where(x => x.NoticeConfigId == NoticeConfigId)
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
        /// 添加呼叫通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallConfigNotice AddCallConfigNotice(CallConfigNotice model)
        {
            CheckData(model);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改呼叫通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallConfigNotice(CallConfigNotice model)
        {
            CheckData(model);
            return Update(model);
        }

        /// <summary>
        /// 删除通知配置
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public bool DeleteCallConfigNotice(int NoticeConfigId)
        {
            return Delete(NoticeConfigId) > 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallConfigNotice> QueryExp(CallConfigNoticeQueryDto parm)
        {
            var predicate = Expressionable.Create<CallConfigNotice>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CallStageType), it => it.CallStageType == parm.CallStageType);

            return predicate;
        }

        /// <summary>
        /// 检查表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool CheckData(CallConfigNotice model)
        {
            //检查【呼叫阶段】
            if (string.IsNullOrEmpty(model.CallStageType))
                throw new CustomException("呼叫阶段不能为空");

            //检查通知范围限制
            var config = Context.Queryable<CallConfigNotice>()
                  .Where(it => it.CallStageType == model.CallStageType && it.AreaId == model.AreaId && it.CallTargetType == model.CallTargetType)
                  .WhereIF(model.NoticeConfigId > 0, it => it.NoticeConfigId != model.NoticeConfigId)
                  .First();

            //存在重复配置
            if (config != null)
            {
                if (string.IsNullOrEmpty(model.CallTargetType) && (model.AreaId == null || model.AreaId < 0))
                {//同时为空
                    throw new CustomException("呼叫阶段存在重复配置");
                }
                else if (!string.IsNullOrEmpty(model.CallTargetType) && model.AreaId > 0)
                {//同时不为空(精准通知配置)
                    throw new CustomException("呼叫阶段存在重复的【区域】、【呼叫目标】配置");
                }
                else if (string.IsNullOrEmpty(model.CallTargetType) && model.AreaId > 0)
                {//特定区域所有呼叫目标通知配置
                    throw new CustomException("呼叫阶段存在重复的【区域】配置");
                }
                else if (!string.IsNullOrEmpty(model.CallTargetType) && (model.AreaId == null || model.AreaId <= 0))
                {//所有区域特定呼叫目标通知配置
                    throw new CustomException("呼叫阶段存在重复的【呼叫目标】配置");
                }
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