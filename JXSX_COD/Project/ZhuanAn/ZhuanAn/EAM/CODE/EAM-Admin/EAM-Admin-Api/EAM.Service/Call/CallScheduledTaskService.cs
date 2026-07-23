using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.System;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 广播定时任务Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallScheduledTaskService), ServiceLifetime = LifeTime.Transient)]
    public class CallScheduledTaskService : BaseService<CallScheduledTask>, ICallScheduledTaskService
    {
        private ISysFileService _SysFileService;

        public CallScheduledTaskService(IHttpContextAccessor contextAccessor, ISysFileService sysFileService) : base(contextAccessor)
        {
            _SysFileService = sysFileService;
        }

        /// <summary>
        /// 查询广播定时任务列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallScheduledTaskDto> GetList(CallScheduledTaskQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("TaskTime asc")
                .Where(predicate.ToExpression())
                .LeftJoin<CallArea>((it, a) => it.AreaId == a.AreaId)
                .OrderBy((it, a) => it.TaskTime, OrderByType.Asc)
                .Select((it, a) => new CallScheduledTaskDto()
                {
                    AreaName = a.AreaName
                }, true)
                .ToPage<CallScheduledTaskDto>(parm);

            if (response.Result != null && response.Result.Count > 0)
            {//补充文件信息
                List<long?> fileIds = response.Result.Select(it => it.FileId).ToList();

                List<SysFile> files = _SysFileService.Queryable().Where(it => fileIds.Contains(it.Id)).Select(it => new SysFile()
                {
                    Id = it.Id,
                    RealName = it.RealName,
                    AccessUrl = it.AccessUrl
                }).ToList();

                SysFile tempFile = null;
                foreach (var item in response.Result)
                {
                    tempFile = files.Where(it => it.Id == item.FileId).FirstOrDefault();
                    if (tempFile != null)
                    {
                        item.FileAccessUrl = tempFile.AccessUrl;
                        item.FileOriginalName = tempFile.RealName;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="CallTaskId"></param>
        /// <returns></returns>
        public CallScheduledTask GetInfo(int CallTaskId)
        {
            var response = Queryable()
                .Where(x => x.CallTaskId == CallTaskId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加广播定时任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallScheduledTask AddCallScheduledTask(CallScheduledTask model)
        {
            CheckData(model);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改广播定时任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateCallScheduledTask(CallScheduledTask model)
        {
            CheckData(model);
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallScheduledTask> QueryExp(CallScheduledTaskQueryDto parm)
        {
            var predicate = Expressionable.Create<CallScheduledTask>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TaskName), it => it.TaskName.Contains(parm.TaskName));
            return predicate;
        }

        private bool CheckData(CallScheduledTask model)
        {
            bool r = false;
            //检查任务名是否重复
            r = Queryable().Where(it => it.TaskName == model.TaskName)
                  .WhereIF(model.CallTaskId > 0, it => it.CallTaskId != model.CallTaskId)
                  .Count() > 0;

            if (r)
                throw new CustomException("存在相同呼叫任务名称");
            //检查对应播放介质是否有值

            if (model.PlayMedium == CallPlayMediumConstant.文件 && (model.FileId == null || model.FileId <= 0))
                throw new CustomException("未上传需要播放的文件");
            if (model.PlayMedium == CallPlayMediumConstant.文本 && string.IsNullOrEmpty(model.TextContent))
                throw new CustomException("需要播放的文本内容不能为空");

            return true;
        }
    }
}