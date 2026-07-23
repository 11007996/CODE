using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.System;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using EAM.Service.Business.IBusinessService;
using EAM.Service.Workflow.IWorkflowService;
using EAM.ServiceCore.Services;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Data;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 流程实例Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IProcessInstanceService), ServiceLifetime = LifeTime.Transient)]
    public class ProcessInstanceService : BaseService<ProcessInstance>, IProcessInstanceService
    {
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly INodeFieldControlService _nodeFieldControlService;
        private readonly ISysUserService _userService;
        private readonly ISysDeptService _deptService;
        private readonly IOnlineNoticeTicketService _onlineNoticeTicketService;
        private readonly ISimpleOnlineNoticeTicketService _simpleOnlineNoticeTicketService;
        private readonly IConsumableReceiveTicketService _consumableReceiveTicketService;
        private readonly IProductDevDemandTicketService _productDevDemandTicketService;
        private readonly ISizeMeasureTicketService _sizeMeasureTicketService;
        private readonly IProdMeasureTicketService _prodMeasureTicketService;
        private readonly IWxMessageService _wxMessageService;
        private readonly IEmployeeService _employeeService;

        public ProcessInstanceService(
            IHttpContextAccessor httpContextAccessor,
            INodeFieldControlService nodeFieldControlService,
            ISysUserService userService,
            ISysDeptService deptService,
            IOnlineNoticeTicketService onlineNoticeTicketService,
            ISimpleOnlineNoticeTicketService simpleOnlineNoticeTicketService,
            IConsumableReceiveTicketService consumableReceiveTicketService,
            IProductDevDemandTicketService productDevDemandTicketService,
            ISizeMeasureTicketService sizeMeasureTicketService,
            IProdMeasureTicketService prodMeasureTicketService,
            IWxMessageService wxMessageService,
            IEmployeeService employeeService)
            : base(httpContextAccessor)
        {
            _nodeFieldControlService = nodeFieldControlService;
            _userService = userService;
            _deptService = deptService;
            _onlineNoticeTicketService = onlineNoticeTicketService;
            _simpleOnlineNoticeTicketService = simpleOnlineNoticeTicketService;
            _consumableReceiveTicketService = consumableReceiveTicketService;
            _productDevDemandTicketService = productDevDemandTicketService;
            _sizeMeasureTicketService = sizeMeasureTicketService;
            _prodMeasureTicketService = prodMeasureTicketService;
            _wxMessageService = wxMessageService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// 查询流程实例列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProcessInstanceDto> GetList(ProcessInstanceQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.InstanceApprovalNav) //填充子对象
                .Where(predicate.ToExpression())
                .ToPage<ProcessInstance, ProcessInstanceDto>(parm);

            return response;
        }

        /// <summary>
        /// 根据不同状态分页查询流程实例
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProcessInstanceDto> GetListByStatus(ProcessInstanceQueryDto parm)
        {
            var predicate = QueryExp(parm);
            PagedInfo<ProcessInstanceDto> response = null;
            // Pending 待处理，Processed 已处理， Completed 已办结 ，Created 我的请求
            switch (parm.Status)
            {
                case "Pending"://待处理
                    response = Queryable()
                    .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                    .LeftJoin<NodeDefine>((it, p, n) => it.CurrentNodeId == n.NodeId)
                    .LeftJoin<Employee>((it, p, n, e) => it.InitiatorId == e.EmpCode)
                    .InnerJoin<InstanceTask>((it, p, n, e, t) => it.ProcessInstanceId == t.ProcessInstanceId && t.AssigneeId == parm.UserName && (t.Status == TaskStatusConstant.待处理 || t.Status == TaskStatusConstant.处理中))
                    .Where(predicate.ToExpression())
                    .Where(it => it.Status == ProcessStatusConstant.新建 || it.Status == ProcessStatusConstant.暂停 || it.Status == ProcessStatusConstant.进行中)
                    .OrderBy(it => it.CreateTime, OrderByType.Desc)
                    .Select((it, p, n, e, t) => new ProcessInstanceDto
                    {
                        ProcessName = p.ProcessName,
                        CurrentNodeName = n.NodeName,
                        InitiatorName = e.EmpName
                    }, true)
                    .Distinct()
                    .ToPageNoSort<ProcessInstanceDto>(parm);
                    break;

                case "Processed"://已处理
                    response = Queryable()
                    .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                    .LeftJoin<NodeDefine>((it, p, n) => it.CurrentNodeId == n.NodeId)
                    .LeftJoin<Employee>((it, p, n, e) => it.InitiatorId == e.EmpCode)
                    .InnerJoin<InstanceTask>((it, p, n, e, t) => it.ProcessInstanceId == t.ProcessInstanceId && t.AssigneeId == parm.UserName && (t.Status == TaskStatusConstant.已完成 || t.Status == TaskStatusConstant.已取消))
                    //.Where(it => it.Status != ProcessStatusConstant.已完成)
                    .Where(predicate.ToExpression())
                    .OrderBy(it => it.CreateTime, OrderByType.Desc)
                    .Select((it, p, n, e, t) => new ProcessInstanceDto
                    {
                        ProcessName = p.ProcessName,
                        CurrentNodeName = n.NodeName,
                        InitiatorName = e.EmpName
                    }, true)
                    .Distinct()
                    .ToPageNoSort<ProcessInstanceDto>(parm);
                    break;

                case "Completed"://已办结
                    response = Queryable()
                    .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                    .LeftJoin<NodeDefine>((it, p, n) => it.CurrentNodeId == n.NodeId)
                    .LeftJoin<Employee>((it, p, n, e) => it.InitiatorId == e.EmpCode)
                    .InnerJoin<InstanceTask>((it, p, n, e, t) => it.ProcessInstanceId == t.ProcessInstanceId && t.AssigneeId == parm.UserName && t.Status == TaskStatusConstant.已完成)
                    .Where(it => it.Status == ProcessStatusConstant.已完成 || it.Status == ProcessStatusConstant.已终止)
                    .Where(predicate.ToExpression())
                    .OrderBy(it => it.CreateTime, OrderByType.Desc)
                    .Select((it, p, n, e, t) => new ProcessInstanceDto
                    {
                        ProcessName = p.ProcessName,
                        CurrentNodeName = n.NodeName,
                        InitiatorName = e.EmpName
                    }, true)
                    .Distinct()
                    .ToPageNoSort<ProcessInstanceDto>(parm);
                    break;

                case "Created"://我的创建
                    response = Queryable()
                        .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                        .LeftJoin<NodeDefine>((it, p, n) => it.CurrentNodeId == n.NodeId)
                        .LeftJoin<Employee>((it, p, n, e) => it.InitiatorId == e.EmpCode)
                        .Where(it => it.InitiatorId == parm.UserName)
                        .Where(predicate.ToExpression())
                        .OrderBy(it => it.CreateTime, OrderByType.Desc)
                        .Select((it, p, n, e) => new ProcessInstanceDto
                        {
                            ProcessName = p.ProcessName,
                            CurrentNodeName = n.NodeName,
                            InitiatorName = e.EmpName
                        }, true)
                        .ToPageNoSort<ProcessInstanceDto>(parm);
                    break;
            }
            return response;
        }

        /// <summary>
        /// 获取当前用户待处理流程个数
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserPendingCount(string userName)
        {
            return Queryable()
                   .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                   .LeftJoin<NodeDefine>((it, p, n) => it.CurrentNodeId == n.NodeId)
                   .LeftJoin<Employee>((it, p, n, e) => it.InitiatorId == e.EmpCode)
                   .InnerJoin<InstanceTask>((it, p, n, e, t) => it.ProcessInstanceId == t.ProcessInstanceId && t.AssigneeId == userName && (t.Status == TaskStatusConstant.待处理 || t.Status == TaskStatusConstant.处理中))
                   .Where(it => it.Status == ProcessStatusConstant.新建 || it.Status == ProcessStatusConstant.暂停 || it.Status == ProcessStatusConstant.进行中)
                   .Select((it, p, n, e, t) => new ProcessInstanceDto
                   {
                       ProcessName = p.ProcessName,
                       CurrentNodeName = n.NodeName,
                       InitiatorName = e.EmpName
                   }, true)
                   .Distinct()
                   .Count();
        }

        /// <summary>
        /// 获取默认的实例信息(初始化)
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public ProcessInstance GetInitInfo(ProcessInstanceInitDto parm)
        {
            ProcessDefine process = Context.Queryable<ProcessDefine>().Where(it => it.ProcessId == parm.ProcessId).First();
            SysUser user = _userService.Queryable().ClearFilter().Where(it => it.UserName == parm.InitiatorId).First();
            SysDept dept = _deptService.Queryable().ClearFilter().Where(it => it.DeptId == user.DeptId).First();
            NodeDefine node = Context.Queryable<NodeDefine>().Where(it => it.ProcessId == parm.ProcessId && it.NodeType == NodeTypeConstant.开始).First();
            if (node == null)
                throw new CustomException($"流程【{process.ProcessName}】未配置开始节点");

            ProcessInstance instance = new()
            {
                ProcessId = process.ProcessId,
                ProcessName = process.ProcessName,
                ProcessTemplate = process.ProcessTemplate,
                Title = $"{process.ProcessName}-{process.ProcessId}-{user.NickName}-{DateTime.Now:yyyyMMddHHmm}",
                Status = ProcessStatusConstant.新建,
                DeptId = dept?.DeptId,
                DeptName = dept?.DeptName,
                InitiatorId = user.UserName,
                InitiatorName = user.NickName,
                CurrentNodeId = node.NodeId,
                CurrentNodeName = node.NodeName
            };
            return instance;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ProcessInstance GetInfo(string processInstanceId, string userName)
        {
            //更新任务状态
            List<InstanceTask> myTasks = Context.Queryable<InstanceTask>().Where(it => it.AssigneeId == userName && it.ProcessInstanceId == processInstanceId && it.Status == TaskStatusConstant.待处理).ToList();
            foreach (InstanceTask task in myTasks)
            {
                if (task.TaskType == ProcessTaskTypeConstant.查看)
                    task.Status = TaskStatusConstant.已完成;
                else
                    task.Status = TaskStatusConstant.处理中;
                task.StartTime = DateTime.Now;
            }
            Context.Updateable<InstanceTask>(myTasks).ExecuteCommand();

            var response = Queryable()
                // .Includes(x => x.InstanceApprovalNav) //填充子对象
                .Includes(x => x.InstanceAttachmentNav)
                .Includes(x => x.InstanceFormDataNav)
                //  .Includes(x => x.InstanceTaskNav)
                .Where(x => x.ProcessInstanceId == processInstanceId)
                .First();
            ProcessDefine process = Context.Queryable<ProcessDefine>().Where(it => it.ProcessId == response.ProcessId).First();
            SysUser user = _userService.Queryable().ClearFilter().Where(it => it.UserName == response.InitiatorId).First();
            NodeDefine currNode = Context.Queryable<NodeDefine>().Where(it => it.NodeId == response.CurrentNodeId).First();
            response.InitiatorName = user.NickName;
            response.ProcessName = process.ProcessName;
            response.CurrentNodeName = currNode.NodeName;

            //审批记录
            List<InstanceApproval> approvals = Context.Queryable<InstanceApproval>()
                .LeftJoin<Employee>((ia, e) => ia.ApproverId == e.EmpCode)
                .LeftJoin<NodeDefine>((ia, e, n) => ia.NodeId == n.NodeId)
                .Where((ia, e) => ia.ProcessInstanceId == processInstanceId)
                .Select((ia, e, n) => new InstanceApproval
                {
                    ApproverName = e.EmpName,
                    NodeName = n.NodeName
                }, true)
                .OrderBy(ia => ia.ActionTime, OrderByType.Desc)
                .ToList();
            response.InstanceApprovalNav = approvals;
            //任务状态
            List<InstanceTask> tasks = Context.Queryable<InstanceTask>()
               .LeftJoin<Employee>((it, e) => it.AssigneeId == e.EmpCode)
               .LeftJoin<NodeDefine>((it, e, n) => it.NodeId == n.NodeId)
               .Where((it, e) => it.ProcessInstanceId == processInstanceId)
               .Select((it, e, n) => new InstanceTask
               {
                   AssigneeName = e.EmpName,
                   NodeName = n.NodeName
               }, true)
               .OrderBy(it => it.CreateTime, OrderByType.Asc)
               .ToList();
            response.InstanceTaskNav = tasks;

            return response;
        }

        /// <summary>
        /// 根据用户获取流程可操作的节点信息
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public NodeActionAndFieldDto QueryUserActionForProcess(QueryUserActionInProcessDto parm)
        {
            NodeActionAndFieldDto response = new();
            List<string> actions = new();
            InstanceTask task = null;
            ProcessInstance instance = null;

            //*************获取当前用户在流程中的节点信息************
            if (string.IsNullOrEmpty(parm.ProcessInstanceId))
            {//新增流程,查询流程[开始]的节点
                if (string.IsNullOrEmpty(parm.ProcessId))
                    throw new CustomException("未传递有效的流程定义编号");
                response.NodeInfo = Context.Queryable<NodeDefine>().Where(it => it.ProcessId == parm.ProcessId && it.NodeType == NodeTypeConstant.开始).First();
            }
            else
            {//已创建流程实例
                instance = Queryable().Where(it => it.ProcessInstanceId == parm.ProcessInstanceId).First();
                //判断当前用户是否未处理的任务
                task = Context.Queryable<InstanceTask>()
                    .Where(it => it.ProcessInstanceId == parm.ProcessInstanceId
                    && it.AssigneeId == parm.UserId
                    && (it.Status == TaskStatusConstant.待处理 || it.Status == TaskStatusConstant.处理中)
                    ).First();
                if (task != null)
                {//有任务可操作，初始化当前节点为任务节点
                    response.NodeInfo = Context.Queryable<NodeDefine>().Where(it => it.NodeId == task.NodeId).First();
                    if (task.TaskType == ProcessTaskTypeConstant.查看)
                        response.NodeInfo.AllowedActions = "";
                }
                else
                {//没有任务,初始化当前节点为流程的当前节点,清除节点默认可用操作
                    response.NodeInfo = Context.Queryable<NodeDefine>().Where(it => it.NodeId == instance.CurrentNodeId).First();
                    response.NodeInfo.AllowedActions = "";
                }
            }
            if (response.NodeInfo == null)
                throw new CustomException("未找到任何可用节点");

            //**********获取节点的字段控制信息**********
            if (task != null || response.NodeInfo.NodeType == NodeTypeConstant.开始)
            {
                response.FieldControls = _nodeFieldControlService.GetDetailList(response.NodeInfo.NodeId);
            }

            //**********获取用户在当前节点的可用动作操作**********
            //用户在当前节点任务中的可用操作
            if (!string.IsNullOrEmpty(response.NodeInfo.AllowedActions))
            {
                actions.AddRange(response.NodeInfo.AllowedActions.Split(","));
                if (instance != null && (instance.Status == ProcessStatusConstant.已终止 || instance.Status == ProcessStatusConstant.已完成))
                {
                    actions.Clear();
                }
            }
            //追加操作
            if (instance != null)
            {
                //如果是发起人或管理,追加【终止】操作
                if (instance.Status != ProcessStatusConstant.已终止 && instance.Status != ProcessStatusConstant.已完成 && (instance.InitiatorId == parm.UserId || parm.UserId == GlobalConstant.AdminRole))
                    actions.Add(NodeActionConstant.终止);

                //有任务且当前流程不为新建状态，追加【转发】操作
                if (task != null && task.TaskType == ProcessTaskTypeConstant.审批 && instance.Status != ProcessStatusConstant.新建)
                    actions.Add(NodeActionConstant.转发);

                //已创建，所有节点，追加【传阅】操作
                actions.Add(NodeActionConstant.传阅);
            }
            //去重并设置当前节点的可用操作
            response.AllowedActions = actions.Distinct().ToList();

            //*******************获取当前节点【批准】后的下个节点审批人定义*************************
            NodeApprover nextNodeApprover = GetNextNodeApprover(response.NodeInfo.NodeId);
            //判断是否使用的表单字段
            if (nextNodeApprover != null && nextNodeApprover.ApproverType == ApproverTypeConstant.表单字段)
            {
                response.NextNodeFormApprover = nextNodeApprover.ApproverValue;
            }

            return response;
        }

        /// <summary>
        /// 添加流程实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProcessInstance AddProcessInstance(ProcessInstanceActionDto model)
        {
            ProcessInstance instance = null;
            //新创建的任务
            List<InstanceTask> newTasks = new();

            ProcessDefine process = Context.Queryable<ProcessDefine>().Where(it => it.ProcessId == model.ProcessId).First();

            //开启事务更新
            DbResult<bool> r = UseTran(() =>
            {
                //更新流程实例
                instance = new ProcessInstance
                {
                    ProcessInstanceId = GetNewId(model.ProcessId),
                    Title = model.Title,
                    ProcessId = model.ProcessId,
                    ProcessTemplate = process.ProcessTemplate,
                    CurrentNodeId = model.ActionNodeId,
                    InitiatorId = model.InitiatorId,
                    DeptId = model.DeptId,
                    DeptName = model.DeptName,
                    Status = ProcessStatusConstant.新建,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                    DelFlag = ((int)DeleteFlagEnum.存在).ToString()
                };
                Insert(instance);

                //更新表单数据
                model.ProcessInstanceId = instance.ProcessInstanceId;
                UpdateFormData(model.ProcessInstanceId, model.ActionNodeId, model.UpdateBy, model.FormData);

                //更新当前任务记录
                InstanceTask currTask = new()
                {
                    ProcessInstanceId = instance.ProcessInstanceId,
                    NodeId = model.ActionNodeId,
                    TaskType = ProcessTaskTypeConstant.创建,
                    AssigneeId = model.CreateBy,
                    Status = TaskStatusConstant.待处理,
                    CreateTime = DateTime.Now,
                };
                currTask = Context.Insertable<InstanceTask>(currTask).ExecuteReturnEntity();

                //当操作为【保存】时，不进行后续操作
                if (model.ActionType == NodeActionConstant.保存)
                    return;

                //更新任务状态
                UpdateTaskStatus(currTask);

                //【批准】操作后续任务创建
                if (model.ActionType == NodeActionConstant.批准)
                {
                    //检查是否有未完成的节点任务
                    newTasks.AddRange(GenerateTaskByApprove(model.ProcessId, model.ProcessInstanceId, model.ActionNodeId, model.ActionType, model.InitiatorId, model.FormData));
                }

                //插入新任务
                if (newTasks != null && newTasks.Count > 0)
                    Context.Insertable(newTasks).ExecuteCommand();

                //更新审批记录
                InsertApprovalRecord(model.ProcessInstanceId, model.ActionNodeId, model.ActionType, model.Opinion, model.UpdateBy, newTasks);

                //更新流程实例
                UpdateInstanceStatus(model.ProcessInstanceId, model.ActionType, newTasks);
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            //通知任务执行人
            SendMessage(instance, newTasks, model.UpdateBy);

            return instance;
        }

        /// <summary>
        /// 修改流程实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProcessInstance(ProcessInstanceActionDto model)
        {
            if (string.IsNullOrEmpty(model.ProcessInstanceId))
                throw new CustomException("流程编号不能为空");

            //当前操作的节点
            ProcessInstance instance = Context.Queryable<ProcessInstance>().Where(it => it.ProcessInstanceId == model.ProcessInstanceId).First();
            model.InitiatorId = instance.InitiatorId;
            model.ProcessId = instance.ProcessId;
            //检查流程状态,允许流程结束后可以执行【传阅】
            if ((instance.Status == ProcessStatusConstant.已终止 || instance.Status == ProcessStatusConstant.已完成) && model.ActionType != NodeActionConstant.传阅)
                throw new CustomException("当前流程已终止或完成，无法继续操作");

            //新创建的任务
            List<InstanceTask> newTasks = new();

            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //检查当前任务
                InstanceTask currTask = GetTaskForUserAction(model.ProcessInstanceId, model.ActionNodeId, model.ActionType, model.InitiatorId, model.UpdateBy);

                //更新表单数据,  传阅、转发不保存数据
                if (model.ActionType != NodeActionConstant.传阅 && model.ActionType != NodeActionConstant.转发)
                    UpdateFormData(model.ProcessInstanceId, model.ActionNodeId, model.UpdateBy, model.FormData);

                //************************不同动作创建不同任务 STAET *************************************
                //【保存】时，不进行后续操作
                if (model.ActionType == NodeActionConstant.保存)
                    return;

                //更新当前任务状态，非【保存】或【传阅】
                if (model.ActionType != NodeActionConstant.保存 && model.ActionType != NodeActionConstant.传阅)
                    UpdateTaskStatus(currTask);

                //【传阅】操作
                if (model.ActionType == NodeActionConstant.传阅)
                {
                    newTasks.AddRange(GenerateTaskByCirculate(model.ProcessInstanceId, model.ActionNodeId, model.AcceptorId, model.UpdateBy));
                }
                else if (model.ActionType == NodeActionConstant.批准)
                {//【批准】:根据流向创建任务
                    newTasks.AddRange(GenerateTaskByApprove(model.ProcessId, model.ProcessInstanceId, model.ActionNodeId, model.ActionType, model.InitiatorId, model.FormData));
                }
                else if (model.ActionType == NodeActionConstant.转发)
                {//【转发】:将当前任务转给他人处理
                    newTasks.Add(GenerateTaskByForward(model.ProcessInstanceId, model.ActionNodeId, model.AcceptorId, model.UpdateBy));
                }
                else if (model.ActionType == NodeActionConstant.拒绝 || model.ActionType == NodeActionConstant.终止)
                {//【拒绝】、【终止】:终止未完成的任务，创建任务通知发起人
                    newTasks.Add(GenerateTaskByTerminated(model.ProcessInstanceId, model.ActionNodeId, model.InitiatorId));
                }

                //插入新任务
                if (newTasks != null && newTasks.Count > 0)
                    Context.Insertable(newTasks).ExecuteCommand();

                //************************不同动作创建不同任务 END *************************************

                //更新审批记录
                InsertApprovalRecord(model.ProcessInstanceId, model.ActionNodeId, model.ActionType, model.Opinion, model.UpdateBy, newTasks);

                //检查是否可以归档
                if (newTasks != null && newTasks.Count == 1 && newTasks[0].TaskType == ProcessTaskTypeConstant.归档)
                {
                    ProcessFiling(model.ProcessInstanceId);
                }

                //更新流程实例
                UpdateInstanceStatus(model.ProcessInstanceId, model.ActionType, newTasks);
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            //通知任务执行人
            SendMessage(instance, newTasks, model.UpdateBy);

            //通知任务执行人
            return r.IsSuccess ? 1 : 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ProcessInstance> QueryExp(ProcessInstanceQueryDto parm)
        {
            var predicate = Expressionable.Create<ProcessInstance>();
            predicate.And(it => it.DelFlag == ((int)DeleteFlagEnum.存在).ToString());
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            return predicate;
        }

        /// <summary>
        /// 流程归档处理
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        private bool ProcessFiling(string processInstanceId)
        {
            ProcessInstance instance = Queryable()
              // .Includes(x => x.InstanceApprovalNav) //填充子对象
              .Includes(x => x.InstanceAttachmentNav)
              .Includes(x => x.InstanceFormDataNav)
              //  .Includes(x => x.InstanceTaskNav)
              .Where(x => x.ProcessInstanceId == processInstanceId)
              .First();

            if (instance == null)
                throw new CustomException($"流程编号{processInstanceId}未找到");
            Employee employee = Context.Queryable<Employee>().Where(it => it.EmpCode == instance.InitiatorId).First();
            instance.InitiatorName = employee.EmpName;

            //防止重复归档操作
            if (instance.Status == ProcessStatusConstant.已终止 || instance.Status == ProcessStatusConstant.已完成)
                return false;

            NodeDefine endNode = Context.Queryable<NodeDefine>().Where(it => it.ProcessId == instance.ProcessId && it.NodeType == NodeTypeConstant.结束).First();
            if (endNode == null)
                throw new CustomException($"归档失败,当前流程模板[{instance.ProcessId}]没有设置[结束]节点");
            InstanceTask filingTask = null;
            List<InstanceTask> newTasks = new();
            //开启事务
            DbResult<bool> r = UseTran(() =>
            {
                //更新任务状态(创建或更新归档任务)
                filingTask = Context.Queryable<InstanceTask>()
                                                 .Where(it =>
                                                     it.ProcessInstanceId == instance.ProcessInstanceId &&
                                                     it.NodeId == endNode.NodeId &&
                                                     it.TaskType == ProcessTaskTypeConstant.归档 &&
                                                     (it.Status == TaskStatusConstant.待处理 || it.Status == TaskStatusConstant.处理中))
                                                 .First();
                if (filingTask != null)
                {
                    UpdateTaskStatus(filingTask);
                }
                else
                    throw new CustomException("未找到归档任务");

                //更新表单数据
                BusinessFiling(instance, filingTask.AssigneeId);

                //插入归档后通知发起人查看的任务
                newTasks.Add(new InstanceTask()
                {
                    ProcessInstanceId = processInstanceId,
                    NodeId = endNode.NodeId,
                    TaskType = ProcessTaskTypeConstant.查看,
                    AssigneeId = instance.InitiatorId,
                    CreateTime = DateTime.Now,
                    Status = TaskStatusConstant.待处理
                });
                Context.Insertable(newTasks).ExecuteCommand();

                //更新审批记录
                string receiverName = Context.Queryable<Employee>().Where(it => it.EmpCode == instance.InitiatorId).Select(it => it.EmpName).First();
                InstanceApproval approval = new()
                {
                    ProcessInstanceId = instance.ProcessInstanceId,
                    NodeId = endNode.NodeId,
                    ApproverId = filingTask.AssigneeId,
                    DeptName = "系统",
                    ActionTime = DateTime.Now,
                    ActionType = NodeActionConstant.批准,
                    Opinion = "系统自动归档",
                    Receiver = receiverName
                };
                Context.Insertable(approval).ExecuteCommand();

                //更新流程实例
                instance.CurrentNodeId = endNode.NodeId;
                instance.Status = ProcessStatusConstant.已完成;
                Update(instance);
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            //通知任务执行人
            SendMessage(instance, newTasks, filingTask?.AssigneeId);

            return r.IsSuccess;
        }

        #region 私有方法:流程创建、更新业务方法

        /// <summary>
        /// 获取指定节点的所有上级节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flows"></param>
        /// <param name="nodeId"></param>
        /// <param name="result"></param>
        private static void GetAllParentNodes(List<NodeDefine> nodes, List<NodeFlow> flows, int nodeId, List<NodeDefine> result)
        {
            //查询所有能够流向当前节点的父节点
            List<NodeFlow> parentFlows = flows.Where(it => it.ToNodeId == nodeId && it.ActionType == NodeActionConstant.批准).ToList();

            if (parentFlows != null && parentFlows.Count > 0)
            {
                //检查父节点的下所有子节点是否有未完成的任务
                foreach (NodeFlow flow in parentFlows)
                {
                    result.Add(nodes.Where(it => it.NodeId == flow.FromNodeId).First());
                    GetAllParentNodes(nodes, flows, flow.FromNodeId, result);
                }
            }
        }

        /// <summary>
        /// 检查是否符合指定条件表达式
        /// </summary>
        /// <param name="formData">表单数据</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        private static bool CheckCondForJson(List<FormItemData> formData, string conditions)
        {
            if (string.IsNullOrEmpty(conditions)) return true;
            List<ConditionExpression> condition = JsonConvert.DeserializeObject<List<ConditionExpression>>(conditions);
            string itemValue = "";
            string condValue = "";
            bool r = true;
            foreach (ConditionExpression cond in condition)
            {
                itemValue = formData.Where(it => it.Key == cond.Name).Select(it => it.Value).FirstOrDefault();
                condValue = cond.Value;
                if (r)
                {
                    switch (cond.Operator)
                    {
                        case Operator.等于: // 添加等于操作符的支持
                            r = condValue.Equals(itemValue);
                            break;

                        case Operator.不等于:
                            r = !condValue.Equals(itemValue);
                            break;

                        case Operator.大于:
                            r = Convert.ToDecimal(itemValue) > Convert.ToDecimal(condValue);
                            break;

                        case Operator.大于等于:
                            r = Convert.ToDecimal(itemValue) >= Convert.ToDecimal(condValue);
                            break;

                        case Operator.小于:
                            r = Convert.ToDecimal(itemValue) < Convert.ToDecimal(condValue);
                            break;

                        case Operator.小于等于:
                            r = Convert.ToDecimal(itemValue) <= Convert.ToDecimal(condValue);
                            break;

                        default:
                            throw new NotSupportedException($"Operator '{cond.Operator}' is not supported.");
                    }
                }
            }
            return r;
        }

        /// <summary>
        /// 获取一个新的流程编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        private string GetNewId(string processId)
        {
            if (string.IsNullOrEmpty(processId))
                throw new CustomException("参数流程ID不能为空");
            string newId = $"{processId}-{DateTime.Now:yyyyMMdd}-";
            var max = Queryable().Where(it => it.ProcessInstanceId.StartsWith(newId)).Max(it => it.ProcessInstanceId);
            if (max == null)
            {
                newId += "0001";
            }
            else
            {
                int num = Convert.ToInt32(max.Split('-')[2]);
                newId += (num + 1).ToString().PadLeft(4, '0');
            }
            return newId;
        }

        /// <summary>
        /// 获取当前用户可执行任务
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="actionType">操作动作</param>
        /// <param name="initiatorId">流程发起人</param>
        /// <param name="operaterId">操作人</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private InstanceTask GetTaskForUserAction(string processInstanceId, int actionNodeId, string actionType, string initiatorId, string operaterId)
        {
            //获取当前用户在操作节点上的任务
            InstanceTask currTask = Context.Queryable<InstanceTask>()
               .Where(it =>
                   it.ProcessInstanceId == processInstanceId
                   && it.NodeId == actionNodeId
                   && it.AssigneeId == operaterId
                   && it.TaskType != ProcessTaskTypeConstant.查看
                   && (it.Status == TaskStatusConstant.待处理 || it.Status == TaskStatusConstant.处理中))
               .First();

            if (currTask == null)
            {//无任务
                if (actionType == NodeActionConstant.终止 && (initiatorId == operaterId || operaterId == GlobalConstant.AdminRole))
                {//当前用户为管理员或发起人执行【终止】流程
                }
                else if (actionType == NodeActionConstant.传阅)
                { //【传阅】操作

                }
                else
                {//无任务
                    throw new CustomException($"当有用户在流程中没有可执行的任务");
                }
            }
            return currTask;
        }

        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="operaterId">操作人</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        private void UpdateFormData(string processInstanceId, int actionNodeId, string operaterId, List<FormItemData> formData)
        {
            string errorMsg = "";
            //获取节点域控制
            List<NodeFieldControlDetailDto> fields = Context.Queryable<NodeFieldControl>()
                .LeftJoin<FormField>((it, ff) => it.FormId == ff.FormId && it.FieldName == ff.FieldName)
                .Where(it => it.NodeId == actionNodeId)
                .Select((it, ff) => new NodeFieldControlDetailDto()
                {
                    FieldDesc = ff.FieldDesc,
                }, true)
                .ToList();
            if (fields == null || fields.Count == 0) return;

            //遍历节点字段
            FormItemData item;
            List<InstanceFormData> instanceFormData = new();
            foreach (NodeFieldControlDetailDto field in fields)
            {
                item = formData.Where(it => it.Key == field.FieldName).FirstOrDefault();
                //检查必填
                if (field.Required != null && field.Required == true && item == null)
                    errorMsg += $"表单数据【{field.FieldName}:{field.FieldDesc}】不能为空 |";
                //可编辑
                if (item != null && field.Editable != null && field.Editable == true)
                {
                    InstanceFormData itemData = new()
                    {
                        ProcessInstanceId = processInstanceId,
                        NodeId = actionNodeId,
                        FieldName = item.Key,
                        FieldValue = item.Value,
                        UpdateBy = operaterId,
                        UpdateTime = DateTime.Now
                    };
                    instanceFormData.Add(itemData);
                }
            }

            //检查是否有错误
            if (!string.IsNullOrEmpty(errorMsg))
                throw new CustomException(errorMsg);

            if (instanceFormData.Count > 0)
            {
                //插入或更新，根据主键
                var x = Context.Storageable(instanceFormData).ToStorage();
                x.AsInsertable.ExecuteCommand();//不存在插入
                x.AsUpdateable.ExecuteCommand();//存在更新
            }
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="currTask">当前任务</param>
        private void UpdateTaskStatus(InstanceTask currTask)
        {
            if (currTask != null)
            {
                currTask.Status = TaskStatusConstant.已完成;
                currTask.StartTime = currTask.StartTime == null ? DateTime.Now : currTask.StartTime;
                currTask.FinishTime = DateTime.Now;
                Context.Updateable<InstanceTask>(currTask).ExecuteCommand();
            }
        }

        /// <summary>
        /// 增加审批记录
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="actionType">操作动作</param>
        /// <param name="opinion">意见</param>
        /// <param name="operaterId">操作人</param>
        /// <param name="newTasks">新创建的任务</param>
        private void InsertApprovalRecord(string processInstanceId, int actionNodeId, string actionType, string opinion, string operaterId, List<InstanceTask> newTasks)
        {
            Employee emp = Context.Queryable<Employee>().Where(it => it.EmpCode == operaterId).First();
            if (emp == null)
                emp = _employeeService.AddEmployeeFromUser(operaterId);

            SysDept dept = _deptService.Queryable().ClearFilter().Where(it => it.DeptId == emp.DeptId).First();

            List<string> taskEmp = newTasks?.Select(it => it.AssigneeId).ToList();
            List<string> taskEmpName = null;
            if (taskEmp != null && taskEmp.Count > 0)
            {
                taskEmpName = Context.Queryable<Employee>().Where(it => taskEmp.Contains(it.EmpCode)).Select(it => it.EmpName).ToList();
            }
            InstanceApproval approval = new()
            {
                ProcessInstanceId = processInstanceId,
                NodeId = actionNodeId,
                ApproverId = operaterId,
                DeptName = dept?.DeptName,
                ActionTime = DateTime.Now,
                ActionType = actionType,
                Opinion = opinion,
                Receiver = taskEmpName == null || taskEmpName.Count <= 0 ? null : string.Join(",", taskEmpName.ToArray())
            };
            Context.Insertable<InstanceApproval>(approval).ExecuteCommand();
        }

        /// <summary>
        /// 更新实例状态
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionType">操作动作</param>
        /// <param name="newTasks">新创建的任务</param>
        private void UpdateInstanceStatus(string processInstanceId, string actionType, List<InstanceTask> newTasks)
        {
            //有创建新的任务，且创建的任务为1,更新流程实例的当前节点
            if (newTasks.Count == 1)
            {
                string status = ProcessStatusConstant.进行中;
                if (actionType == NodeActionConstant.拒绝 || actionType == NodeActionConstant.终止)
                {
                    status = ProcessStatusConstant.已终止;
                }
                else
                {
                    NodeDefine node = Context.Queryable<NodeDefine>().Where(it => it.NodeId == newTasks[0].NodeId).First();
                    status = node.NodeType == NodeTypeConstant.开始 ? ProcessStatusConstant.新建 : node.NodeType == NodeTypeConstant.结束 ? ProcessStatusConstant.已完成 : ProcessStatusConstant.进行中;
                }

                Context.Updateable<ProcessInstance>()
                      .SetColumns(it => it.CurrentNodeId == newTasks[0].NodeId)
                      .SetColumns(it => it.Status == status)
                      .Where(it => it.ProcessInstanceId == processInstanceId && it.Status != ProcessStatusConstant.已终止 && it.Status != ProcessStatusConstant.已完成)
                      .ExecuteCommand();
            }
        }

        /// <summary>
        /// 获取节点流向
        /// </summary>
        /// <param name="fromNodeId">表单</param>
        /// <param name="action">动作</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        private List<NodeFlow> GetCurrNodeFlow(int fromNodeId, string action, List<FormItemData> formData)
        {
            //检查
            List<NodeFlow> flows = Context.Queryable<NodeFlow>().Where(it => it.FromNodeId == fromNodeId && it.ActionType == action).ToList();

            List<NodeFlow> r = new();
            //根据条件筛选
            if (flows != null && flows.Count > 0)
            {
                foreach (NodeFlow flow in flows)
                {
                    if (CheckCondForJson(formData, flow.ConditionExpression))
                        r.Add(flow);
                }
            }
            return r;
        }

        /// <summary>
        /// 检查前置(并行)任务是否完成,用于判断是否可以流向下个节点
        /// </summary>
        /// <param name="processId">流程</param>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="nextNodeIds">下个节点Id集合</param>
        /// <returns></returns>
        private bool CheckPreTaskStatus(string processId, string processInstanceId, List<int> nextNodeIds)
        {
            //流程所有节点
            List<NodeDefine> nodes = Context.Queryable<NodeDefine>().Where(it => it.ProcessId == processId).ToList();
            List<int> nodesId = nodes.Select(it => it.NodeId).Distinct().ToList();
            //所有节点的流向
            List<NodeFlow> flows = Context.Queryable<NodeFlow>().Where(it => nodesId.Contains(it.FromNodeId) && it.ActionType == NodeActionConstant.批准).ToList();
            //获取下个流向的
            List<NodeDefine> parentNodes = new();
            foreach (int nodeId in nextNodeIds)
            {
                GetAllParentNodes(nodes, flows, nodeId, parentNodes);
            }
            //检查是否存在未完成的父节点任务
            List<int> parentNodeIds = parentNodes.Select(it => it.NodeId).Distinct().ToList();
            int count = Context.Queryable<InstanceTask>().Where(it => it.ProcessInstanceId == processInstanceId && parentNodeIds.Contains((int)it.NodeId) && it.TaskType != ProcessTaskTypeConstant.查看 && (it.Status == TaskStatusConstant.待处理 || it.Status == TaskStatusConstant.处理中)).Count();
            return count <= 0;
        }

        /// <summary>
        /// 根据节点流向生成任务
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="initiator">发起人</param>
        /// <param name="flows">节点流向</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        private List<InstanceTask> GenerateTaskByNodeFlow(string processInstanceId, string initiator, List<NodeFlow> flows, List<FormItemData> formData)
        {
            if (flows == null || flows.Count == 0) throw new CustomException("当前节点操作没有设置流向目标");
            List<InstanceTask> tasks = new();

            foreach (NodeFlow flow in flows)
            {
                NodeDefine node = Context.Queryable<NodeDefine>().Where(it => it.NodeId == flow.ToNodeId).First();
                NodeApprover approver = Context.Queryable<NodeApprover>().Where(it => it.NodeId == flow.ToNodeId).First();
                if (approver == null)
                    throw new CustomException($"流向节点【{node.NodeName}】未定义审批人");

                List<SysUser> users = GetApproverByType(approver.ApproverType, approver.ApproverValue, initiator, formData);
                if (users == null || users.Count == 0)
                    throw new CustomException($"流向节点【{node.NodeName}】定义的审批人类型【{approver.ApproverType}】下没有用户");
                if (users.Count > 1)
                    throw new CustomException($"流向节点【{node.NodeName}】定义的审批人类型【{approver.ApproverType}】下的用户大于1人以上");
                //检查是否有员工信息
                Employee employee = _employeeService.GetInfo(users[0].UserName);
                if (employee == null)
                    _employeeService.AddEmployeeFromUser(users[0].UserName);
                tasks.Add(new InstanceTask()
                {
                    ProcessInstanceId = processInstanceId,
                    NodeId = flow.ToNodeId,
                    TaskType = node.NodeType == NodeTypeConstant.结束 ? ProcessTaskTypeConstant.归档 : ProcessTaskTypeConstant.审批,
                    AssigneeId = users[0].UserName,
                    CreateTime = DateTime.Now,
                    Status = TaskStatusConstant.待处理
                });
            }
            return tasks;
        }

        /// <summary>
        /// 根据审批人类型，获取到审批人
        /// </summary>
        /// <param name="approverType">审批人类型</param>
        /// <param name="approverValue">审批人值</param>
        /// <param name="initiator">发起人</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private List<SysUser> GetApproverByType(string approverType, string approverValue, string initiator, List<FormItemData> formData)
        {
            if (string.IsNullOrEmpty(approverType))
            {
                throw new CustomException("审批人类型不能为空");
            }
            List<SysUser> users;
            switch (approverType)
            {
                case ApproverTypeConstant.用户:
                    users = _userService.Queryable().ClearFilter()
                        .Where(it => it.UserName == approverValue).ToList();
                    break;

                case ApproverTypeConstant.角色:
                    users = _userService.Queryable().ClearFilter().LeftJoin<SysUserRole>((u, ur) => u.UserId == ur.UserId)
                        .Where((u, ur) => ur.RoleId == Convert.ToInt64(approverValue))
                        .Select<SysUser>().ToList();
                    break;

                case ApproverTypeConstant.部门领导:
                    users = _userService.Queryable().ClearFilter().InnerJoin<SysDept>((u, d) => u.UserName == d.Leader)
                        .Where((u, d) => d.DeptId == Convert.ToInt64(approverValue))
                    .Select<SysUser>().ToList();
                    break;

                case ApproverTypeConstant.职位:
                    users = _userService.Queryable().ClearFilter().LeftJoin<SysUserPost>((u, up) => u.UserId == up.UserId)
                        .Where((u, up) => up.PostId == Convert.ToInt32(approverValue))
                        .Select<SysUser>().ToList();
                    break;

                case ApproverTypeConstant.直属主管:
                    long deptId = _userService.Queryable().ClearFilter().Where(it => it.UserName == initiator).Select(it => it.DeptId).First();
                    users = _userService.Queryable().ClearFilter().InnerJoin<SysDept>((u, d) => u.UserName == d.Leader)
                        .Where((u, d) => d.DeptId == deptId)
                    .Select<SysUser>().ToList();
                    break;

                case ApproverTypeConstant.表单字段:
                    string empCode = formData.Where(it => it.Key == approverValue).Select(it => it.Value).First();
                    users = _userService.Queryable().ClearFilter().Where(u => u.UserName == empCode)
                        .Select<SysUser>().ToList();
                    break;

                default:
                    throw new CustomException($"审批人类型[{approverType}]无效");
            }
            return users;
        }

        #endregion 私有方法:流程创建、更新业务方法

        #region 节点操作任务创建
        /// <summary>
        /// 【传阅】操作
        /// </summary>
        /// <param name="processInstanceId"></param>
        /// <param name="actionNodeId"></param>
        /// <param name="acceptorIds"></param>
        /// <param name="operaterId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<InstanceTask> GenerateTaskByCirculate(string processInstanceId, int actionNodeId, string acceptorIds, string operaterId)
        {
            if (string.IsNullOrEmpty(acceptorIds))
                throw new CustomException($"当前操作未选择【传阅人】");
            string[] acceptorIdArr = acceptorIds.Split(',');
            if (acceptorIdArr.Contains(operaterId))
                throw new CustomException($"当前操作选择的【传阅人】包含操作人自己");

            List<SysUser> users = _userService.Queryable().ClearFilter().Where(it => acceptorIdArr.Contains(it.UserName)).ToList();
            //if (users == null || users.Count != acceptorIdArr.Length)
            //    throw new CustomException($"存在未在系统里账号信息");
            //创建一个任务
            List<InstanceTask> newTasks = new List<InstanceTask>();
            foreach (SysUser user in users)
            {
                newTasks.Add(new InstanceTask()
                {
                    ProcessInstanceId = processInstanceId,
                    NodeId = actionNodeId,
                    TaskType = ProcessTaskTypeConstant.查看,
                    AssigneeId = user.UserName,
                    Status = TaskStatusConstant.待处理,
                    CreateTime = DateTime.Now
                });
            }
            return newTasks;
        }

        /// <summary>
        /// 【批准】操作
        /// </summary>
        /// <param name="processId">流程</param>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="actionType">操作动作</param>
        /// <param name="initiatorId">发起人</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<InstanceTask> GenerateTaskByApprove(string processId, string processInstanceId, int actionNodeId, string actionType, string initiatorId, List<FormItemData> formData)
        {
            List<InstanceTask> tasks = new();
            //检查当前节点操作流向设置

            List<NodeFlow> flows = GetCurrNodeFlow(actionNodeId, actionType, formData);
            if (flows == null || flows.Count == 0)
                throw new CustomException($"当前节点的操作【{actionType}】没有设置流向");

            //检查是否完成前置任务
            List<int> nodeIds = flows.Select(it => it.ToNodeId).ToList();
            if (CheckPreTaskStatus(processId, processInstanceId, nodeIds))
            {//前置任务都已完成
                //生成流向节点的任务
                return GenerateTaskByNodeFlow(processInstanceId, initiatorId, flows, formData);
            }
            return tasks;
        }

        /// <summary>
        /// 【转发】操作
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="acceptorId">受理人</param>
        /// <param name="operaterId">操作人</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private InstanceTask GenerateTaskByForward(string processInstanceId, int actionNodeId, string acceptorId, string operaterId)
        {
            if (string.IsNullOrEmpty(acceptorId))
                throw new CustomException($"当前操作未选择【转发人】");
            if (acceptorId == operaterId)
                throw new CustomException($"当前操作选择的【转发人】不可以是操作人自己");
            SysUser acceptor = _userService.Queryable().ClearFilter().Where(it => it.UserName == acceptorId).First();
            if (acceptor == null)
                throw new CustomException($"未找到转发受理人【{acceptorId}】账号信息");
            //创建一个任务
            InstanceTask newTask = new()
            {
                ProcessInstanceId = processInstanceId,
                NodeId = actionNodeId,
                TaskType = ProcessTaskTypeConstant.审批,
                AssigneeId = acceptorId,
                Status = TaskStatusConstant.待处理,
                CreateTime = DateTime.Now
            };
            return newTask;
        }

        /// <summary>
        /// 【终止】、【拒绝】操作
        /// </summary>
        /// <param name="processInstanceId">流程实例</param>
        /// <param name="actionNodeId">操作节点</param>
        /// <param name="initiatorId">发起人</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private InstanceTask GenerateTaskByTerminated(string processInstanceId, int actionNodeId, string initiatorId)
        {
            //更新任务状态（未完成任务全部取消）
            Context.Updateable<InstanceTask>()
                    .SetColumns(it => it.StartTime == DateTime.Now)
                    .Where(it => it.ProcessInstanceId == processInstanceId && it.Status == TaskStatusConstant.待处理)
                    .ExecuteCommand();
            Context.Updateable<InstanceTask>()
                   .SetColumns(it => it.Status == TaskStatusConstant.已取消)
                   .SetColumns(it => it.FinishTime == DateTime.Now)
                   .Where(it => it.ProcessInstanceId == processInstanceId && (it.Status == TaskStatusConstant.待处理 || it.Status == TaskStatusConstant.处理中))
                   .ExecuteCommand();

            //创建发起人查看的任务
            InstanceTask task = new()
            {
                ProcessInstanceId = processInstanceId,
                NodeId = actionNodeId,
                TaskType = ProcessTaskTypeConstant.查看,
                AssigneeId = initiatorId,
                CreateTime = DateTime.Now,
                Status = TaskStatusConstant.待处理
            };
            return task;
        }

        #endregion 节点操作任务创建

        #region 业务归档

        /// <summary>
        /// 获取表单数据并转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fieldName"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        private static T GetFormData<T>(List<InstanceFormData> data, string fieldName, T nullValue)
        {
            try
            {
                InstanceFormData field = data.Where(it => it.FieldName.ToLower() == fieldName.ToLower()).FirstOrDefault();
                if (field == null || string.IsNullOrEmpty(field.FieldValue))
                    return nullValue;

                if (typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    // 处理可空类型
                    var underlyingType = Nullable.GetUnderlyingType(typeof(T));
                    var convertedValue = Convert.ChangeType(field.FieldValue, underlyingType);
                    return (T)convertedValue;
                }
                else
                {
                    // 处理非可空类型
                    return (T)Convert.ChangeType(field.FieldValue, typeof(T));
                }
            }
            catch (Exception)
            {
                throw new CustomException($"表单字段{fieldName}的值无法转换为类型{typeof(T).Name}.");
            }
        }

        /// <summary>
        /// 业务归档入口
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="operaterId">操作人</param>
        /// <exception cref="CustomException"></exception>
        private void BusinessFiling(ProcessInstance instance, string operaterId)
        {
            switch (instance.ProcessTemplate)
            {
                case TicketTypeConstant.上线通知单:
                    OnlineNoticeTicketFiling(instance, operaterId);
                    break;

                case TicketTypeConstant.上线通知单_简单:
                    SimpleOnlineNoticeTicketFiling(instance, operaterId);
                    break;

                case TicketTypeConstant.耗品领用单:
                    ConsumableReceiveTicketFiling(instance, operaterId);
                    break;

                case TicketTypeConstant.新产品开发治具需求单:
                    ProductDevDemandTicketFiling(instance, operaterId);
                    break;

                case TicketTypeConstant.治具尺寸量测单:
                    SizeMeasureTicketFiling(instance, operaterId);
                    break;

                case TicketTypeConstant.产品量测单:
                    ProdMeasureTicketFiling(instance, operaterId);
                    break;

                default:
                    throw new CustomException("此归档没有匹配的业务表单处理");
            }
        }

        /// <summary>
        /// 上线通知单归档
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="operaterId"></param>
        private void OnlineNoticeTicketFiling(ProcessInstance instance, string operaterId)
        {
            OnlineNoticeTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                PartId = GetFormData<int?>(instance.InstanceFormDataNav, "PartId", null),
                ProductQty = GetFormData<int?>(instance.InstanceFormDataNav, "ProductQty", null),
                LineId = GetFormData<int?>(instance.InstanceFormDataNav, "LineId", null),
                NeedTime = GetFormData<DateTime?>(instance.InstanceFormDataNav, "NeedTime", null),
                ProcessInstanceId = instance.ProcessInstanceId,
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };
            //设备清单
            string equipmentJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "equipmentList", null);
            List<OnlineNoticeTicketEquipment> equipments = JsonConvert.DeserializeObject<List<OnlineNoticeTicketEquipment>>(equipmentJsonStr);
            ticket.EquipmentNav = equipments;
            //治具清单
            string fixtureJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "fixtureList", null);
            List<OnlineNoticeTicketFixture> fixtures = JsonConvert.DeserializeObject<List<OnlineNoticeTicketFixture>>(fixtureJsonStr);
            ticket.FixtureNav = fixtures;
            _onlineNoticeTicketService.AddOnlineNoticeTicket(ticket);
        }

        /// <summary>
        /// 上线通知单（简单）归档
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="operaterId"></param>
        private void SimpleOnlineNoticeTicketFiling(ProcessInstance instance, string operaterId)
        {
            SimpleOnlineNoticeTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                NewPartName = GetFormData<string>(instance.InstanceFormDataNav, "NewPartName", null),
                OldPartName = GetFormData<string>(instance.InstanceFormDataNav, "OldPartName", null),
                ProductQty = GetFormData<int?>(instance.InstanceFormDataNav, "ProductQty", null),
                LineId = GetFormData<int?>(instance.InstanceFormDataNav, "LineId", null),
                NeedTime = GetFormData<DateTime?>(instance.InstanceFormDataNav, "NeedTime", null),
                ProcessInstanceId = instance.ProcessInstanceId,
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };
            //设备清单
            string itemJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemList", null);
            List<SimpleOnlineNoticeTicketItem> items = JsonConvert.DeserializeObject<List<SimpleOnlineNoticeTicketItem>>(itemJsonStr);
            ticket.ItemNav = items;
            _simpleOnlineNoticeTicketService.AddSimpleOnlineNoticeTicket(ticket);
        }

        /// <summary>
        /// 耗品领用单归档
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="operaterId">操作人</param>
        private void ConsumableReceiveTicketFiling(ProcessInstance instance, string operaterId)
        {
            ConsumableReceiveTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                LineId = GetFormData<int?>(instance.InstanceFormDataNav, nameof(ConsumableReceiveTicket.LineId), null),
                NeedDate = GetFormData<DateTime?>(instance.InstanceFormDataNav, nameof(ConsumableReceiveTicket.NeedDate), null),
                Purpose = GetFormData<string>(instance.InstanceFormDataNav, nameof(ConsumableReceiveTicket.Purpose), null),
                ProcessInstanceId = instance.ProcessInstanceId,
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };
            string consumableJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ConsumableList", null);
            List<ConsumableReceiveTicketItem> consumables = JsonConvert.DeserializeObject<List<ConsumableReceiveTicketItem>>(consumableJsonStr);
            ticket.ConsumableNav = consumables;
            _consumableReceiveTicketService.AddConsumableReceiveTicket(ticket);
        }

        /// <summary>
        /// 新产品开发治具需求单归档
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="operaterId">操作人</param>
        private void ProductDevDemandTicketFiling(ProcessInstance instance, string operaterId)
        {
            ProductDevDemandTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                CustomId = GetFormData<string>(instance.InstanceFormDataNav, "CustomId", null),
                PartId = GetFormData<int?>(instance.InstanceFormDataNav, "PartId", null),
                NeedDate = GetFormData<DateTime?>(instance.InstanceFormDataNav, "NeedDate", null),
                OrderQty = GetFormData<int>(instance.InstanceFormDataNav, "OrderQty", 0),
                ProcessInstanceId = instance.ProcessInstanceId,
                EngineerId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerId", null),
                EngineerLeaderId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerLeaderId", null),
                LeaderId = GetFormData<string>(instance.InstanceFormDataNav, "LeaderId", null),
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };
            string itemJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemList", null);
            List<ProductDevDemandTicketItem> item = JsonConvert.DeserializeObject<List<ProductDevDemandTicketItem>>(itemJsonStr);
            ticket.ProductDevDemandTicketItemNav = item;
            _productDevDemandTicketService.AddProductDevDemandTicket(ticket);
        }

        /// <summary>
        /// 治具尺寸测量单归档
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="operaterId">操作人</param>
        private void SizeMeasureTicketFiling(ProcessInstance instance, string operaterId)
        {
            SizeMeasureTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                CreateMode = GetFormData<string>(instance.InstanceFormDataNav, "CreateMode", null),
                PartId = GetFormData<int?>(instance.InstanceFormDataNav, "PartId", null),
                FixtureId = GetFormData<int?>(instance.InstanceFormDataNav, "FixtureId", null),
                FixtureName = GetFormData<string>(instance.InstanceFormDataNav, "FixtureName", null),
                DrawingNo = GetFormData<string>(instance.InstanceFormDataNav, "DrawingNo", null),
                FixtureNoDesc = GetFormData<string>(instance.InstanceFormDataNav, "FixtureNoDesc", null),
                Version = GetFormData<string>(instance.InstanceFormDataNav, "Version", null),
                ProcessInstanceId = instance.ProcessInstanceId,
                EngineerId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerId", null),
                EngineerLeaderId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerLeaderId", null),
                QcId = GetFormData<string>(instance.InstanceFormDataNav, "QCId", null),
                QcLeaderId = GetFormData<string>(instance.InstanceFormDataNav, "QCLeaderId", null),
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };

            //测试项目定义
            string itemDefineJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemDefine", null);
            List<SizeMeasureTicketItemDefine> itemDefine = JsonConvert.DeserializeObject<List<SizeMeasureTicketItemDefine>>(itemDefineJsonStr);
            ticket.SizeMeasureTicketItemDefineNav = itemDefine;

            //尺寸判定评估及结果
            string itemResultJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemResult", null);
            List<SizeMeasureTicketItemResult> itemResult = new();
            List<SizeMeasureTicketItem> items = new();
            // 反序列化为 JArray
            JArray jsonArray = JArray.Parse(itemResultJsonStr);
            // 遍历 JArray
            foreach (JObject jsonObject in jsonArray.Children<JObject>())
            {
                itemResult.Add(new SizeMeasureTicketItemResult()
                {
                    FixtureNo = jsonObject["fixtureNo"].ToString(),
                    SizeResult = jsonObject["result"].ToString(),
                    InStorage = "N"
                });
                foreach (JProperty property in jsonObject.Properties())
                {
                    Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                    if (property.Name != "fixtureNo" && property.Name != "result" && property.Name != "index")
                    {
                        if (int.TryParse(property.Name, out int orderNo))
                        {
                            items.Add(new SizeMeasureTicketItem()
                            {
                                FixtureNo = jsonObject["fixtureNo"].ToString(),
                                OrderNo = orderNo,
                                ActualValue = property.Value == null ? null : Convert.ToDecimal(property.Value),
                            });
                        }
                    }
                }
            }
            ticket.SizeMeasureTicketItemNav = items;
            ticket.SizeMeasureTicketItemResultNav = itemResult;

            //其他判定评估
            string itemOtherJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemOther", null);
            List<SizeMeasureTicketItemOther> itemOther = JsonConvert.DeserializeObject<List<SizeMeasureTicketItemOther>>(itemOtherJsonStr);
            ticket.SizeMeasureTicketItemOtherNav = itemOther;

            _sizeMeasureTicketService.AddSizeMeasureTicket(ticket);
        }

        /// <summary>
        /// 产品尺寸测量单归档
        /// </summary>
        /// <param name="instance">实例对象</param>
        /// <param name="operaterId">操作人</param>
        private void ProdMeasureTicketFiling(ProcessInstance instance, string operaterId)
        {
            ProdMeasureTicket ticket = new()
            {
                InitiatorId = instance.InitiatorId,
                InitiatorName = instance.InitiatorName,
                CreateMode = GetFormData<string>(instance.InstanceFormDataNav, "CreateMode", null),
                PartId = GetFormData<int?>(instance.InstanceFormDataNav, "PartId", null),
                FixtureName = GetFormData<string>(instance.InstanceFormDataNav, "FixtureName", null),
                DrawingNo = GetFormData<string>(instance.InstanceFormDataNav, "DrawingNo", null),
                FixtureNoDesc = GetFormData<string>(instance.InstanceFormDataNav, "FixtureNoDesc", null),
                Version = GetFormData<string>(instance.InstanceFormDataNav, "Version", null),
                ProcessInstanceId = instance.ProcessInstanceId,
                MakerId = GetFormData<string>(instance.InstanceFormDataNav, "MakerId", null),
                EngineerId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerId", null),
                EngineerLeaderId = GetFormData<string>(instance.InstanceFormDataNav, "EngineerLeaderId", null),
                QcId = GetFormData<string>(instance.InstanceFormDataNav, "QCId", null),
                QcLeaderId = GetFormData<string>(instance.InstanceFormDataNav, "QCLeaderId", null),
                CreateTime = DateTime.Now,
                CreateBy = operaterId
            };

            //测试项目定义
            string itemDefineJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemDefine", null);
            List<ProdMeasureTicketItemDefine> itemDefine = JsonConvert.DeserializeObject<List<ProdMeasureTicketItemDefine>>(itemDefineJsonStr);
            ticket.ProdMeasureTicketItemDefineNav = itemDefine;

            //尺寸判定评估及结果
            string itemResultJsonStr = GetFormData<string>(instance.InstanceFormDataNav, "ItemResult", null);
            List<ProdMeasureTicketItemResult> itemResult = new();
            List<ProdMeasureTicketItem> items = new();
            // 反序列化为 JArray
            JArray jsonArray = JArray.Parse(itemResultJsonStr);
            // 遍历 JArray
            foreach (JObject jsonObject in jsonArray.Children<JObject>())
            {
                itemResult.Add(new ProdMeasureTicketItemResult()
                {
                    ProductNo = jsonObject["productNo"].ToString(),
                    SizeResult = jsonObject["sizeResult"].ToString(),
                    FacadeResult = jsonObject["facadeResult"].ToString()
                });
                foreach (JProperty property in jsonObject.Properties())
                {
                    Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                    if (property.Name != "fixtureNo" && property.Name != "sizeResult" && property.Name != "facadeResult" && property.Name != "index")
                    {
                        if (int.TryParse(property.Name, out int orderNo))
                        {
                            items.Add(new ProdMeasureTicketItem()
                            {
                                ProductNo = jsonObject["productNo"].ToString(),
                                OrderNo = orderNo,
                                ActualValue = property.Value == null ? null : Convert.ToDecimal(property.Value),
                            });
                        }
                    }
                }
            }
            ticket.ProdMeasureTicketItemNav = items;
            ticket.ProdMeasureTicketItemResultNav = itemResult;

            _prodMeasureTicketService.AddProdMeasureTicket(ticket);
        }

        #endregion 业务归档

        #region 消息通知

        private void SendMessage(ProcessInstance instance, List<InstanceTask> newTasks, string operaterId)
        {
            try
            {
                //提取企业OA用户
                List<string> assigneeIds = newTasks.Select(it => it.AssigneeId).ToList();
                List<string> empCodes = _userService.ExtractUsersByUserType(assigneeIds, UserTypeConstant.OA用户)?.Distinct().ToList();
                if (empCodes == null || empCodes.Count <= 0)
                    return;
                // 创建消息内容
                SysUser currUser = _userService.Queryable().ClearFilter().Where(it => it.UserName == operaterId).First();
                string frontUrl = AppSettings.Get<string>("frontUrl");

                string title = "【EAM设备系统】" + instance.Title;
                string content = $"你有一个新的待办任务，来自【{currUser.NickName}】,流程编号【{instance.ProcessInstanceId}】";
                string linkUrl = $"{frontUrl}/process/{instance.ProcessId}?instanceId={instance.ProcessInstanceId}";

                _wxMessageService.SendTextCardMessage(string.Join(",", empCodes), content, title, linkUrl);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        #endregion 消息通知

        #region 获取下个节点的审批人定义信息

        /// <summary>
        /// 获取下个节点的审批人定义信息
        /// </summary>
        /// <param name="currNodeId"></param>
        /// <returns></returns>
        private NodeApprover GetNextNodeApprover(int currNodeId)
        {
            // 当前节点的【批准】动作的流向
            NodeFlow nodeflow = Context.Queryable<NodeFlow>().Where(it => it.FromNodeId == currNodeId && it.ActionType == NodeActionConstant.批准).First();
            if (nodeflow != null && nodeflow.ToNodeId > 0)
            {
                return Context.Queryable<NodeApprover>().Where(it => it.NodeId == nodeflow.ToNodeId).First();
            }
            return null;
        }

        #endregion 获取下个节点的审批人定义信息

        #region 删除流程实例

        public int DeleteProcessInstance(string processInstanceId, string username)
        {
            ProcessInstance model = Context.Queryable<ProcessInstance>().Where(it => it.ProcessInstanceId == processInstanceId && it.InitiatorId == username).First();

            model.DelFlag = ((int)DeleteFlagEnum.删除).ToString();
            return Context.Updateable<ProcessInstance>(model).UpdateColumns(it => it.DelFlag).ExecuteCommand();
        }

        #endregion 删除流程实例
    }

    /// <summary>
    /// 动态过滤
    /// </summary>
    public class ConditionExpression
    {
        public string Name { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }

    public class Operator
    {
        public const string 等于 = "=";
        public const string 不等于 = "!=";
        public const string 大于 = ">";
        public const string 大于等于 = ">=";
        public const string 小于 = "<";
        public const string 小于等于 = "<=";
    }
}