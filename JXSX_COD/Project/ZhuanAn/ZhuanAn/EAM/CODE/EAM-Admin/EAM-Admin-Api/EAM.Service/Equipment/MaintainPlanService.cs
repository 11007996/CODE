using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Equipment;
using EAM.Model.System;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using SqlSugar.IOC;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 保养计划Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainPlanService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainPlanService : BaseService<MaintainPlan>, IMaintainPlanService
    {
        public MaintainPlanService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询保养计划列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainPlanDto> GetList(MaintainPlanQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.MaintainPlanClassNav) //填充子对象
                //.OrderBy("CreateTime desc")
                .Where(predicate.ToExpression())
                .LeftJoin<MaintainPlanClass>((it, pc) => it.PlanClassId == pc.PlanClassId)
                .LeftJoin<EquipmentBase>((it, pc, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, pc, e) => new MaintainPlanDto()
                {
                    AssetNo = e.AssetNo,
                    AssetName = e.AssetName,
                    EquipmentName = e.EquipmentName,
                    PlanClass = pc.PlanClass,
                }, true)
                .ToPage<MaintainPlanDto>(parm);

            if (response != null)
            {
                List<long> deptIds = response.Result.Select(it => it.ExecuteDeptId).ToList();
                var dbContext = DbScoped.SugarScope.GetConnectionScope(0);
                List<SysDept> depts = dbContext.Queryable<SysDept>().Where(it => deptIds.Contains((int)it.DeptId)).ToList();
                foreach (var plan in response.Result)
                {
                    if (plan.ExecuteDeptId > 0)
                    {
                        plan.ExecuteDeptName = depts.Where(it => it.DeptId == plan.ExecuteDeptId).FirstOrDefault().DeptName;
                    }
                }
                dbContext.Close();
                dbContext.Dispose();
            }

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="PlanId"></param>
        /// <returns></returns>
        public MaintainPlan GetInfo(int PlanId)
        {
            var response = Queryable()
                .Includes(x => x.MaintainPlanClassItemNav) //填充子对象
                .Where(x => x.PlanId == PlanId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加保养计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddMaintainPlan(MaintainPlanBatchAddDto model)
        {
            //检查班次
            MaintainPlanClass planClass = Context.Queryable<MaintainPlanClass>().Where(it => it.PlanClassId == model.PlanClassId).First();
            if (planClass == null)
                throw new CustomException("未找到班次信息");
            if (planClass.PlanYear < DateTime.Now.Year)
                throw new CustomException("班次的年份不能低于当前年份");
            //检查保养计划
            bool hasPlan = Queryable().Where(it => it.PlanYear == planClass.PlanYear && model.EquipmentIds.Contains(it.EquipmentId)).Any();
            if (hasPlan)
                throw new CustomException("当前有设备已创建过保养计划");

            List<MaintainPlan> models = new List<MaintainPlan>();
            foreach (int equId in model.EquipmentIds)
            {
                models.Add(new MaintainPlan()
                {
                    EquipmentId = equId,
                    PlanYear = planClass.PlanYear,
                    PlanClassId = model.PlanClassId,
                    ExecuteDeptId = model.ExecuteDeptId,
                    CreateBy = model.CreateBy,
                    CreateTime = model.CreateTime,
                });
            }

            return Context.Insertable(models).ExecuteCommand();
        }

        /// <summary>
        /// 修改保养计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainPlan(MaintainPlan model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 获取未做保养计划的设备
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentSimpleDto> GetExcludeEquipment(ExcludeEquipmentQueryDto parm)
        {
            var response = Context.Queryable<EquipmentBase>()
                .WhereIF(!string.IsNullOrEmpty(parm.Keyword),
                        it => it.AssetNo.Contains(parm.Keyword) || it.AssetName.Contains(parm.Keyword)
                        || it.EquipmentName.Contains(parm.Keyword) || it.CustomModel.Contains(parm.Keyword))
                .Where(it => SqlFunc.Subqueryable<MaintainPlan>().Where(p => p.EquipmentId == it.EquipmentId && p.PlanYear == parm.PlanYear).NotAny())
                .Select(it => new EquipmentSimpleDto()
                {
                    EquipmentId = it.EquipmentId,
                    EquipmentName = it.EquipmentName,
                    AssetNo = it.AssetNo,
                    AssetName = it.AssetName,
                    CustomModel = it.CustomModel,
                    Status = it.Status,
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<MaintainPlan> QueryExp(MaintainPlanQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainPlan>();

            predicate = predicate.AndIF(parm.EquipmentId != null, it => it.EquipmentId == parm.EquipmentId);
            predicate = predicate.AndIF(parm.PlanYear != null, it => it.PlanYear == parm.PlanYear);
            predicate = predicate.AndIF(parm.PlanClassId != null, it => it.PlanClassId == parm.PlanClassId);
            predicate = predicate.AndIF(parm.ExecuteDeptId != null, it => it.ExecuteDeptId == parm.ExecuteDeptId);
            return predicate;
        }
    }
}