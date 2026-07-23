using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Repository;
using EAM.Service.Call.ICallService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Call
{
    /// <summary>
    /// 故障记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ICallFaultBaseService), ServiceLifetime = LifeTime.Transient)]
    public class CallFaultBaseService : BaseService<CallFaultBase>, ICallFaultBaseService
    {
        private readonly IWxMessageService _wxMessageService;

        public CallFaultBaseService(IHttpContextAccessor contextAccessor, IWxMessageService wxMessageService) : base(contextAccessor)
        {
            _wxMessageService = wxMessageService;
        }

        //定时任务使用
        public CallFaultBaseService(ISqlSugarClient sqlSugarClient, IWxMessageService wxMessageService) : base(sqlSugarClient)
        {
            _wxMessageService = wxMessageService;
        }

        /// <summary>
        /// 查询故障记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallFaultBaseDto> GetList(CallFaultBaseQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("StartTime desc")
                .Where(predicate.ToExpression())
                .LeftJoin<CallAreaLine>((it, al) => it.LineId == al.LineId)
                .LeftJoin<CallArea>((it, al, a) => al.AreaId == a.AreaId)
                .LeftJoin<Line>((it, al, a, l) => it.LineId == l.LineId)
                .LeftJoin<Station>((it, al, a, l, s) => it.StationId == s.StationId)
                .LeftJoin<Employee>((it, al, a, l, s, e1) => it.HandlerNo == e1.EmpCode)
                .LeftJoin<Employee>((it, al, a, l, s, e1, e2) => it.HelperNo == e2.EmpCode)
                .LeftJoin<Employee>((it, al, a, l, s, e1, e2, e3) => it.SolverNo == e3.EmpCode)
                .LeftJoin<Employee>((it, al, a, l, s, e1, e2, e3, e4) => it.QcNo == e4.EmpCode)
                .WhereIF(parm.AreaId > 0, (it, al, a, l, s, e1, e2, e3, e4) => al.AreaId == parm.AreaId)
                .OrderByIF(!string.IsNullOrEmpty(parm.Sort) && parm.Sort.ToLower() == "createtime", it => it.CreateTime, parm.SortType.ToLower().StartsWith("asc") ? OrderByType.Asc : OrderByType.Desc)
                .Select((it, al, a, l, s, e1, e2, e3, e4) => new CallFaultBaseDto()
                {
                    AreaId = a.AreaId,
                    AreaName = a.AreaName,
                    LineName = l.LineName,
                    StationName = s.StationName,
                    HandlerName = e1.EmpName,
                    HelperName = e2.EmpName,
                    SolverName = e3.EmpName,
                    QcName = e4.EmpName,
                    CreateTime = it.CreateTime
                }, true)
                .ToPageNoSort<CallFaultBaseDto>(parm);

            //补全部门名称信息
            //if (response.Result.Count > 0)
            //{
            //    List<int?> deptIds = response.Result.Select(it => it.DeptId).ToList();
            //    var dbContext = DbScoped.SugarScope.GetConnectionScope(0);
            //    List<SysDept> depts = dbContext.Queryable<SysDept>().Where(it => deptIds.Contains((int)it.DeptId)).ToList();
            //    foreach (var callRecord in response.Result)
            //    {
            //        if (callRecord.DeptId > 0)
            //        {
            //            callRecord.DeptName = depts.Where(it => it.DeptId == callRecord.DeptId.Value).FirstOrDefault().DeptName;
            //        }
            //    }
            //    dbContext.Close();
            //    dbContext.Dispose();
            //}

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="CallId"></param>
        /// <returns></returns>
        public CallFaultBase GetInfo(int CallId)
        {
            var response = Queryable()
                .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                .LeftJoin<Station>((it, l, s) => it.StationId == s.StationId)
                .LeftJoin<Employee>((it, l, s, e1) => it.HandlerNo == e1.EmpCode)
                .LeftJoin<Employee>((it, l, s, e1, e2) => it.HelperNo == e2.EmpCode)
                .LeftJoin<Employee>((it, l, s, e1, e2, e3) => it.SolverNo == e3.EmpCode)
                .LeftJoin<Employee>((it, l, s, e1, e2, e3, e4) => it.QcNo == e4.EmpCode)
                .Where(it => it.CallId == CallId)
                .Select((it, l, s, e1, e2, e3, e4) => new CallFaultBase()
                {
                    LineName = l.LineName,
                    StationName = s.StationName,
                    HandlerName = e1.EmpName,
                    HelperName = e2.EmpName,
                    SolverName = e3.EmpName,
                    QcName = e4.EmpName,
                    CreateTime = it.CreateTime
                }, true)
                .First();

            var handles = Context.Queryable<CallFaultOperate>()
                .LeftJoin<Employee>((it, e) => it.OperaterNo == e.EmpCode)
                .Where(it => it.CallId == CallId)
                .OrderBy(it => it.CreateTime, OrderByType.Asc)
                .Select((it, e) => new CallFaultOperate()
                {
                    OperaterName = e.EmpName,
                }, true)
                .ToList();

            response.CallFaultOperateNav = handles;

            return response;
        }

        /// <summary>
        /// 查询产线未完结的呼叫
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns></returns>
        public List<CallFaultBaseDto> GetUnsolvedCallFaultBase(int LineId)
        {
            //故障信息
            var response = Queryable()
                 .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在 && it.LineId == LineId && it.FaultStatus != CallFaultStatusConstant.已完成 && it.FaultStatus != CallFaultStatusConstant.已中止)
                 .LeftJoin<Line>((it, l) => it.LineId == l.LineId)
                 .LeftJoin<Station>((it, l, s) => it.StationId == s.StationId)
                 .LeftJoin<Employee>((it, l, s, e1) => it.HandlerNo == e1.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2) => it.HelperNo == e2.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2, e3) => it.SolverNo == e3.EmpCode)
                 .LeftJoin<Employee>((it, l, s, e1, e2, e3, e4) => it.QcNo == e4.EmpCode)
                 .OrderBy(it => it.CreateTime, OrderByType.Desc)
                 .Select((it, l, s, e1, e2, e3, e4) => new CallFaultBaseDto()
                 {
                     LineName = l.LineName,
                     StationName = s.StationName,
                     HandlerName = e1.EmpName,
                     HelperName = e2.EmpName,
                     SolverName = e3.EmpName,
                     QcName = e4.EmpName,
                     CreateTime = it.CreateTime
                 }, true)
                 .ToList();

            return response;
        }

        /// <summary>
        /// 获取产线的呼叫摘要信息(关联的区域、设备、工站)
        /// </summary>
        /// <param name="LineId"></param>
        /// <returns></returns>
        public LineCallSummaryDto GetCallSummaryByLine(int LineId)
        {
            //产线设备
            var lineEquipment = Context.Queryable<CallLineEquipment>()
                .Where(it => it.LineId == LineId)
                .Select(it => new CallLineEquipmentDto()
                {
                }, true)
                .ToList();

            //产线工站
            var lineStation = Context.Queryable<Station>()
                .Where(it => it.LineId == LineId && it.Enabled == true)
                .Select(it => new StationDto() { }, true).ToList();

            //产线所属区域信息
            var areaLine = Context.Queryable<CallAreaLine>()
                .LeftJoin<CallArea>((it, ca) => it.AreaId == ca.AreaId)
                .Where(it => it.LineId == LineId)
                .Select((it, ca) => new CallAreaLineDto()
                {
                    AreaName = ca.AreaName,
                }, true).First();

            LineCallSummaryDto result = new LineCallSummaryDto();
            result.LineId = LineId;
            result.LineEquipmentList = lineEquipment;
            result.LineStationList = lineStation;
            result.CallArea = areaLine;

            return result;
        }

        /// <summary>
        /// 导出呼叫信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<CallFaultBaseDto> ExportList(CallFaultBaseQueryDto parm)
        {
            return GetList(parm);
        }

        /// <summary>
        /// 添加呼叫
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public CallFaultBase AddCallFaultBase(CallFaultBase model)
        {
            //检查呼叫原因
            if (string.IsNullOrEmpty(model.CallReason))
                throw new CustomException("呼叫原因不能为空");
            //点位类型对应值检查
            if (model.CallPointType == CallPointTypeConstant.设备 && string.IsNullOrEmpty(model.EquipmentType))
                throw new CustomException($"点位类型为设备时，设备不能为空");
            else if (model.CallPointType == CallPointTypeConstant.工站 && (model.StationId == null || model.StationId <= 0))
                throw new CustomException($"点位类型为工站时， 工站不能为空");

            //产线检查
            if (model.LineId == null || model.LineId <= 0)
                throw new CustomException($"未指定产线");
            //检查是否存在相同未处理的设备
            if (model.CallPointType == CallPointTypeConstant.设备)
            {
                int count = Context.Queryable<CallFaultBase>().Where(it => it.LineId == model.LineId && it.EquipmentType == model.EquipmentType && it.EquipmentNo == model.EquipmentNo && it.FaultStatus != CallFaultStatusConstant.已中止 && it.FaultStatus != CallFaultStatusConstant.已完成 && it.DelFlag == (int)DeleteFlagEnum.存在).Count();
                if (count > 0)
                    throw new CustomException($"当前产线存在相同设备的呼叫记录没有处理");
            }
            //检查是否存在相同未处理的工站
            if (model.CallPointType == CallPointTypeConstant.工站)
            {
                int count = Context.Queryable<CallFaultBase>().Where(it => it.LineId == model.LineId && it.StationId == model.StationId && it.FaultStatus != CallFaultStatusConstant.已中止 && it.FaultStatus != CallFaultStatusConstant.已完成 && it.DelFlag == (int)DeleteFlagEnum.存在).Count();
                if (count > 0)
                    throw new CustomException($"当前产线存在相同工站的呼叫记录没有处理");
            }

            //初始化部分数据
            model.FaultStatus = CallFaultStatusConstant.待处理;
            model.DelFlag = (int)DeleteFlagEnum.存在;

            if (model.CallPointType == CallPointTypeConstant.设备)
            {
                model.StationId = null;
            }
            else if (model.CallPointType == CallPointTypeConstant.工站)
            {
                model.EquipmentType = null;
                model.EquipmentNo = null;
            }
            if (!string.IsNullOrEmpty(model.EquipmentType))
            {
                CallConfigFault config = Context.Queryable<CallConfigFault>().Where(it => it.EquipmentType == model.EquipmentType).First();
                model.MaxHandleTimes = config?.MaxHandleTimes;
                model.MaxHelpTimes = config?.MaxHelpTimes;
            }

            //事物处理
            CallFaultBase entity = null;
            var r = UseTran(() =>
            {
                entity = Context.Insertable(model).ExecuteReturnEntity();
                CallFaultOperate cfh = new CallFaultOperate()
                {
                    CallId = entity.CallId,
                    OperaterNo = model.CreateBy,
                    CreateTime = model.CreateTime,
                    FaultStatus = CallFaultStatusConstant.待处理
                };
                Context.Insertable(cfh).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            entity = GetInfo(entity.CallId);

            //如果配置了阶段通知（呼叫通知），发送通知
            if (entity != null)
            {
                Employee employee = Context.Queryable<Employee>().Where(e => e.EmpCode == model.CreateBy).First();
                SendMsg(CallStageTypeEnum.呼叫通知, entity.Adapt<CallFaultBaseDto>(), employee);
            }

            return entity;
        }

        /// <summary>
        /// 处理故障
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool HandleCallFaultBase(CallOperateDto model)
        {
            CallFaultBase cfb = GetInfo(model.CallId);
            if (cfb.FaultStatus != CallFaultStatusConstant.待处理)
                throw new CustomException($"当前故障状态为【{cfb.FaultStatus}】,无法执行【处理】操作");

            //检查员工是否存在
            if (string.IsNullOrEmpty(model.OperatorNo))
                throw new CustomException($"未传递处理人工号参数");
            var emp = Context.Queryable<Employee>().Where(e => e.EmpCode == model.OperatorNo).First();
            if (emp == null)
                throw new CustomException($"未找到工号为【{model.OperatorNo}】的员工信息");

            cfb.HandlerNo = model.OperatorNo;
            cfb.HandleTime = DateTime.Now;
            cfb.FaultStatus = CallFaultStatusConstant.处理中;
            cfb.UpdateBy = model.UpdateBy;
            cfb.UpdateTime = model.UpdateTime;

            var r = UseTran(() =>
            {
                Context.Updateable(cfb).UpdateColumns(it => new { it.HandlerNo, it.HandleTime, it.FaultStatus, it.UpdateBy, it.UpdateTime }).ExecuteCommand();
                CallFaultOperate cfh = new CallFaultOperate()
                {
                    CallId = model.CallId,
                    OperaterNo = model.OperatorNo,
                    CreateTime = model.UpdateTime,
                    FaultStatus = CallFaultStatusConstant.处理中
                };
                Context.Insertable(cfh).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            //如果配置了阶段通知（签到处理），发送通知
            if (cfb != null)
            {
                SendMsg(CallStageTypeEnum.处理签到, cfb.Adapt<CallFaultBaseDto>(), emp);
            }

            return r.IsSuccess;
        }

        /// <summary>
        /// 请求支援
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool RequestHelpCallFaultBase(CallOperateDto model)
        {
            CallFaultBase cfb = GetById(model.CallId);
            if (cfb.FaultStatus != CallFaultStatusConstant.处理中)
                throw new CustomException($"当前故障状态为【{cfb.FaultStatus}】,无法执行【请求支援】操作");

            //检查员工是否存在
            if (string.IsNullOrEmpty(model.OperatorNo))
                throw new CustomException($"未传递处理人工号参数");
            var emp = Context.Queryable<Employee>().Where(e => e.EmpCode == model.OperatorNo).First();
            if (emp == null)
                throw new CustomException($"未找到工号为【{model.OperatorNo}】的员工信息");

            cfb.CallHelpTime = DateTime.Now;
            cfb.CallHelpWay = model.CallHelpWay;
            cfb.FaultStatus = CallFaultStatusConstant.待支援;
            cfb.UpdateBy = model.UpdateBy;
            cfb.UpdateTime = model.UpdateTime;

            //事务处理
            var r = UseTran(() =>
            {
                Context.Updateable(cfb).UpdateColumns(it => new { it.CallHelpTime, it.CallHelpWay, it.FaultStatus, it.UpdateBy, it.UpdateTime }).ExecuteCommand();
                CallFaultOperate cfh = new CallFaultOperate()
                {
                    CallId = model.CallId,
                    OperaterNo = model.OperatorNo,
                    CreateTime = model.UpdateTime,
                    FaultStatus = CallFaultStatusConstant.待支援
                };
                Context.Insertable(cfh).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 支援签到
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool HelpCallFaultBase(CallOperateDto model)
        {
            CallFaultBase cfb = GetById(model.CallId);
            if (cfb.FaultStatus != CallFaultStatusConstant.待支援)
                throw new CustomException($"当前故障状态为【{cfb.FaultStatus}】,无法执行【支援签到】操作");

            //检查员工是否存在
            if (string.IsNullOrEmpty(model.OperatorNo))
                throw new CustomException($"未传递处理人工号参数");
            var emp = Context.Queryable<Employee>().Where(e => e.EmpCode == model.OperatorNo).First();
            if (emp == null)
                throw new CustomException($"未找到工号为【{model.OperatorNo}】的员工信息");

            //检查支援人是否合法
            if (model.OperatorNo == cfb.HandlerNo)
                throw new CustomException("支援人不能与处理人相同");

            cfb.HelperNo = model.OperatorNo;
            cfb.HelpTime = model.UpdateTime;
            cfb.FaultStatus = CallFaultStatusConstant.支援中;
            cfb.UpdateBy = model.UpdateBy;
            cfb.UpdateTime = model.UpdateTime;

            var r = UseTran(() =>
            {
                Context.Updateable(cfb).UpdateColumns(it => new { it.HelperNo, it.HelpTime, it.FaultStatus, it.UpdateBy, it.UpdateTime }).ExecuteCommand();
                CallFaultOperate cfh = new CallFaultOperate()
                {
                    CallId = model.CallId,
                    OperaterNo = model.OperatorNo,
                    CreateTime = model.UpdateTime,
                    FaultStatus = CallFaultStatusConstant.支援中
                };
                Context.Insertable(cfh).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 解决完成
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SolveCallFaultBase(CallOperateDto model)
        {
            CallFaultBase cfb = GetById(model.CallId);
            if (cfb.FaultStatus == CallFaultStatusConstant.已中止 || cfb.FaultStatus == CallFaultStatusConstant.已完成)
                throw new CustomException($"当前故障状态为【{cfb.FaultStatus}】,无法执行【完成】操作");

            //检查员工是否存在
            if (string.IsNullOrEmpty(model.OperatorNo))
                throw new CustomException($"未传递处理人工号参数");
            var emp = Context.Queryable<Employee>().Where(e => e.EmpCode == model.OperatorNo).First();
            if (emp == null)
                throw new CustomException($"未找到工号为【{model.OperatorNo}】的员工信息");

            //检查操作人是否合法
            if (model.OperatorNo != cfb.HandlerNo && model.OperatorNo != cfb.HelperNo)
                throw new CustomException("解决人只能是处理人或支援人");

            //检查数量
            if (model.ProdCount > 0 && model.ProdCount.Value != (model.PassCount.Value + model.NgCount.Value))
                throw new CustomException("制品跟踪数 不等于 不良数据+良品数量");

            cfb.SolverNo = model.OperatorNo;
            cfb.EndTime = model.UpdateTime;
            cfb.FaultType = model.FaultType;
            cfb.FaultContent = model.FaultContent;
            cfb.SolutionContent = model.SolutionContent;
            cfb.ProdCount = model.ProdCount;
            cfb.PassCount = model.PassCount;
            cfb.NgCount = model.NgCount;
            cfb.QcNo = model.QcNo;
            cfb.FaultStatus = CallFaultStatusConstant.已完成;
            cfb.UpdateBy = model.UpdateBy;
            cfb.UpdateTime = model.UpdateTime;

            //事务处理
            var r = UseTran(() =>
            {
                Context.Updateable(cfb).ExecuteCommand();
                CallFaultOperate cfh = new CallFaultOperate()
                {
                    CallId = model.CallId,
                    OperaterNo = model.OperatorNo,
                    CreateTime = model.UpdateTime,
                    FaultStatus = CallFaultStatusConstant.已完成
                };
                Context.Insertable(cfh).ExecuteCommand();
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 停止呼叫
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="operatorNo"></param>
        /// <returns></returns>
        public bool StopCallFaultBase(int[] ids, string operatorNo)
        {
            if (ids == null || ids.Length <= 0)
                throw new CustomException($"未选择要停止的呼叫记录");

            var r = UseTran(() =>
            {
                foreach (int id in ids)
                {
                    CallFaultBase cfb = GetById(id);
                    if (cfb.FaultStatus == CallFaultStatusConstant.已中止 || cfb.FaultStatus == CallFaultStatusConstant.已完成)
                        throw new CustomException($"ID【{id}】当前故障状态为【{cfb.FaultStatus}】,无法执行【停止】操作");

                    cfb.SolverNo = operatorNo;
                    cfb.EndTime = DateTime.Now;
                    cfb.FaultStatus = CallFaultStatusConstant.已中止;
                    cfb.UpdateBy = operatorNo;
                    cfb.UpdateTime = DateTime.Now;
                    Context.Updateable(cfb).UpdateColumns(it => new { it.SolverNo, it.EndTime, it.FaultStatus, it.UpdateBy, it.UpdateTime }).ExecuteCommand();

                    CallFaultOperate cfh = new CallFaultOperate()
                    {
                        CallId = id,
                        OperaterNo = operatorNo,
                        CreateTime = cfb.UpdateTime,
                        FaultStatus = CallFaultStatusConstant.已中止
                    };
                    Context.Insertable(cfh).ExecuteCommand();
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess;
        }

        /// <summary>
        /// 删除故障记录
        /// </summary>
        /// <param name="idArr"></param>
        /// <returns></returns>
        public int DeleteCallFaultBase(int[] idArr)
        {
            return Context.Updateable<CallFaultBase>().SetColumns(it => it.DelFlag == (int)DeleteFlagEnum.删除).Where(it => idArr.Contains((int)it.CallId)).ExecuteCommand();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<CallFaultBase> QueryExp(CallFaultBaseQueryDto parm)
        {
            var predicate = Expressionable.Create<CallFaultBase>();

            predicate = predicate.And(it => it.DelFlag == (int)DeleteFlagEnum.存在);
            predicate = predicate.AndIF(parm.LineId != null, it => it.LineId == parm.LineId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CallTargetType), it => it.CallTargetType == parm.CallTargetType);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.CallPointType), it => it.CallPointType == parm.CallPointType);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EquipmentType), it => it.EquipmentType == parm.EquipmentType);
            predicate = predicate.AndIF(parm.BeginCreateTime == null, it => it.CreateTime >= DateTime.Now.ToShortDateString().ParseToDateTime());
            predicate = predicate.AndIF(parm.BeginCreateTime != null, it => it.CreateTime >= parm.BeginCreateTime);
            predicate = predicate.AndIF(parm.EndCreateTime != null, it => it.CreateTime <= parm.EndCreateTime);
            if (!string.IsNullOrEmpty(parm.FaultStatus))
            {
                if (parm.FaultStatus.IndexOf(',') > 0)
                {
                    string[] faultStatus = parm.FaultStatus.Split(",");
                    predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FaultStatus), it => faultStatus.Contains(it.FaultStatus));
                }
                else
                    predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.FaultStatus), it => it.FaultStatus == parm.FaultStatus);
            }
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.HandlerNo), it => it.HandlerNo == parm.HandlerNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.HelperNo), it => it.HelperNo == parm.HelperNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.SolverNo), it => it.SolverNo == parm.SolverNo);

            return predicate;
        }

        /// <summary>
        /// 发送通知消息
        /// </summary>
        /// <param name="stageType"></param>
        /// <param name="callFaultBase"></param>
        /// <param name="employee"></param>
        private void SendMsg(CallStageTypeEnum stageType, CallFaultBaseDto callFaultBase, Employee employee)
        {
            //检查是否有配置通知
            CallConfigNotice noticeConfig = Context.Queryable<CallConfigNotice>()
                 .LeftJoin<CallAreaLine>((it, al) => it.AreaId == al.AreaId)
                 .Where(it => it.CallStageType == Convert.ToInt32(stageType).ToString())
                 .Where((it, al) => al.LineId == callFaultBase.LineId || it.AreaId == null)
                 .Where((it, al) => it.CallTargetType == callFaultBase.CallTargetType || it.CallTargetType == null)
                 .OrderBy(it => it.CallTargetType, OrderByType.Desc) //先以【呼叫目标】排序
                 .OrderBy(it => it.AreaId, OrderByType.Desc) //再以【区域】排序
                 .First();

            if (noticeConfig == null)
                return;

            string targetLabel = string.Empty;

            //消息模板
            string content = string.Empty;
            if (callFaultBase.CallPointType == CallPointTypeConstant.设备)
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n机台：{callFaultBase.EquipmentName}\n人员：{employee?.EmpName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";
            else if (callFaultBase.CallPointType == CallPointTypeConstant.工站)
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n工站：{callFaultBase.StationName}\n人员：{employee?.EmpName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";
            else
                content = $"EAM 【呼叫提醒：{stageType}】\n线别：{callFaultBase.LineName}\n原因：{callFaultBase.CallReason}\n目标：{callFaultBase.CallTargetTypeLabel}\n人员：{employee?.EmpName}\n时间：{callFaultBase.CreateTime}\n备注：{callFaultBase.Remark}";

            WxMessage wxMessage = null;
            if (!string.IsNullOrEmpty(noticeConfig.WxChatId))
            {//群消息
                wxMessage = _wxMessageService.SendWxChatMessage(noticeConfig.WxChatId, content);
            }
            else
            {//人员消息
                wxMessage = _wxMessageService.SendWxEmpMessage(noticeConfig.EmpCodes, content);
            }

            CallFaultNotice model = new CallFaultNotice()
            {
                CallId = callFaultBase.CallId,
                CallStageType = ((int)stageType).ToString(),
                WxNoticeId = wxMessage.Id,
                CreateTime = DateTime.Now
            };
            Context.Insertable<CallFaultNotice>(model).ExecuteCommand();
        }
    }
}