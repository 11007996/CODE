using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 保养通知配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainNoticeConfigService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainNoticeConfigService : BaseService<MaintainNoticeConfig>, IMaintainNoticeConfigService
    {
        private readonly IWxChatGroupService _wxChatGroupService;

        public MaintainNoticeConfigService(IHttpContextAccessor contextAccessor, IWxChatGroupService wxChatGroupService) : base(contextAccessor)
        {
            _wxChatGroupService = wxChatGroupService;
        }

        /// <summary>
        /// 查询保养通知配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainNoticeConfigDto> GetList(MaintainNoticeConfigQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.EquMaintainNoticeConfigEmpNav) //填充子对象
                .Where(predicate.ToExpression())
                .ToPage<MaintainNoticeConfig, MaintainNoticeConfigDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NoticeConfigId"></param>
        /// <returns></returns>
        public MaintainNoticeConfig GetInfo(int NoticeConfigId)
        {
            var response = Queryable()
                // .Includes(x => x.MaintainNoticeConfigEmpNav) //填充子对象
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
        /// 添加保养通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaintainNoticeConfig AddMaintainNoticeConfig(MaintainNoticeConfig model)
        {
            CheckData(model);

            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改保养通知配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainNoticeConfig(MaintainNoticeConfig model)
        {
            CheckData(model);

            return Update(model);
        }

        public int DeleteMaintainNoticeConfig(int[] NoticeConfigIds)
        {
            return Delete(NoticeConfigIds);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<MaintainNoticeConfig> QueryExp(MaintainNoticeConfigQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainNoticeConfig>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.DateMark), it => it.DateMark == parm.DateMark);
            return predicate;
        }

        /// <summary>
        /// 检查表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private bool CheckData(MaintainNoticeConfig model)
        {
            //检查配置是否有重复
            int count = Queryable().Where(it => it.DateMark == model.DateMark)
                .WhereIF(model.NoticeConfigId > 0, it => it.NoticeConfigId != model.NoticeConfigId).Count();
            if (count > 0)
                throw new CustomException($"已存在日期标记[{model.DateMark}]的配置");

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
                count = Context.Queryable<Employee>().Where(it => empcodes.Contains(it.EmpCode)).Count();
                if (count != empcodes.Count())
                    throw new CustomException("存在无效的员工工号");
            }

            return true;
        }
    }
}