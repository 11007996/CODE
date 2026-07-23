using EAM.Common;
using EAM.Common.Wechat;
using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Repository;
using Infrastructure;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 企业微信发送记录表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IWxMessageService), ServiceLifetime = LifeTime.Transient)]
    public class WxMessageService : BaseService<WxMessage>, IWxMessageService
    {
        /// <summary>
        /// 发送联天群消息（文本）
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="content"></param>
        public WxMessage SendWxChatMessage(string chatId, string content)
        {
            //参数检查
            if (string.IsNullOrEmpty(chatId))
                throw new CustomException("聊天群ID不能为空");
            if (string.IsNullOrEmpty(content))
                throw new CustomException("消息内容不能为空");

            //检查聊天群id
            WxChatGroup chatGroup = Context.Queryable<WxChatGroup>().Where(it => it.ChatId == chatId).First();
            if (chatGroup == null)
                throw new CustomException($"聊天群【{chatId}】不存在");

            //发送消息
            WxMessage model = new WxMessage()
            {
                ChatId = chatId,
                MsgType = WxMessageHelper.MSGTYPE_TEXT,
                Content = content,
                SendTime = DateTime.Now,
            };
            try
            {
                string result = LuxshareHelper.SendGroupTextMessage(chatId, content);
                model.ResultMsg = result;
            }
            catch (Exception ex)
            {
                model.ResultMsg = ex.Message;
            }
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 发送用户消息（文本）
        /// </summary>
        /// <param name="empCodes"></param>
        /// <param name="content"></param>
        public WxMessage SendWxEmpMessage(string empCodes, string content)
        {
            //参数检查
            if (string.IsNullOrEmpty(empCodes))
                throw new CustomException("员工工号不能为空");
            if (string.IsNullOrEmpty(content))
                throw new CustomException("消息内容不能为空");

            //过滤、检查员工工号
            List<string> emps = empCodes.Split(',').Distinct().ToList();
            emps = Context.Queryable<SysUser>()
                .Where(it => empCodes.Contains(it.UserName) && it.DelFlag == 0 && it.UserType == UserTypeConstant.OA用户)
                .Select(it => it.UserName)
                .ToList();
            if (emps == null || emps.Count == 0)
                throw new CustomException("没有有效的员工工号");
            empCodes = string.Join(',', emps);

            //发送消息
            WxMessage model = new WxMessage()
            {
                EmpCodes = empCodes,
                MsgType = WxMessageHelper.MSGTYPE_TEXT,
                Content = content,
                SendTime = DateTime.Now
            };
            try
            {
                string result = LuxshareHelper.SendEmpTextMessage(empCodes, content);
                model.ResultMsg = result;
            }
            catch (Exception ex)
            {
                model.ResultMsg = ex.Message;
            }
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 发送卡片消息
        /// </summary>
        /// <param name="empCodes"></param>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="linkUrl"></param>
        /// <exception cref="CustomException"></exception>
        public WxMessage SendTextCardMessage(string empCodes, string content, string title, string linkUrl)
        {
            //参数检查
            if (string.IsNullOrEmpty(empCodes))
                throw new CustomException("员工工号不能为空");
            if (string.IsNullOrEmpty(title))
                throw new CustomException("消息标题不能为空");
            if (string.IsNullOrEmpty(content))
                throw new CustomException("消息内容不能为空");
            if (string.IsNullOrEmpty(linkUrl))
                throw new CustomException("链接不能为空");

            WxMessage model = new()
            {
                Title = title,
                Content = content,
                EmpCodes = empCodes,
                MsgType = WxMessageHelper.MSGTYPE_TEXTCARD,
                LinkUrl = linkUrl,
                SendTime = DateTime.Now
            };

            try
            {
                string result = WxMessageHelper.SendTextCardMessage(empCodes, title, content, linkUrl);
                model.ResultMsg = result;
            }
            catch (Exception ex)
            {
                model.ResultMsg = ex.Message;
            }
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 查询企业微信发送记录表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<WxMessageDto> GetList(WxMessageQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<WxMessage, WxMessageDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public WxMessage GetInfo(int Id)
        {
            var response = Queryable()
                .Where(x => x.Id == Id)
                .First();

            return response;
        }

        /// <summary>
        /// 添加企业微信发送记录表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WxMessage AddWxMessage(WxMessage model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改企业微信发送记录表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateWxMessage(WxMessage model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<WxMessage> QueryExp(WxMessageQueryDto parm)
        {
            var predicate = Expressionable.Create<WxMessage>();

            predicate = predicate.AndIF(parm.BeginSendTime == null, it => it.SendTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate = predicate.AndIF(parm.BeginSendTime != null, it => it.SendTime >= parm.BeginSendTime);
            predicate = predicate.AndIF(parm.EndSendTime != null, it => it.SendTime <= parm.EndSendTime);
            return predicate;
        }
    }
}