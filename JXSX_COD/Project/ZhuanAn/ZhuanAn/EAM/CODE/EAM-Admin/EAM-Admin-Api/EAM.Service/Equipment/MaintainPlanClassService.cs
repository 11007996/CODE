using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 保养计划班次Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IMaintainPlanClassService), ServiceLifetime = LifeTime.Transient)]
    public class MaintainPlanClassService : BaseService<MaintainPlanClass>, IMaintainPlanClassService
    {
        public MaintainPlanClassService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询保养计划班次列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<MaintainPlanClassDto> GetList(MaintainPlanClassQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("PlanYear desc")
                .Where(predicate.ToExpression())
                .ToPage<MaintainPlanClass, MaintainPlanClassDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="planClassId"></param>
        /// <returns></returns>
        public MaintainPlanClass GetInfo(int planClassId)
        {
            var response = Queryable()
                .Includes(x => x.MaintainPlanClassItemNav)
                .Where(x => x.PlanClassId == planClassId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加保养计划班次
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MaintainPlanClass AddMaintainPlanClass(MaintainPlanClass model)
        {
            CheckData(model);

            return Context.InsertNav(model).Include(s1 => s1.MaintainPlanClassItemNav).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改保养计划班次
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateMaintainPlanClass(MaintainPlanClass model)
        {
            CheckData(model);

            return Context.UpdateNav(model).Include(z1 => z1.MaintainPlanClassItemNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除计划班次
        /// </summary>
        /// <param name="planClassIds"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteMaintainPlanClass(int[] planClassIds)
        {
            //检查是否被使用
            bool has = Context.Queryable<MaintainPlan>().Where(it => planClassIds.Contains(it.PlanClassId)).Any();
            if (has)
                throw new CustomException($"当前班次存在被使用，无法删除");

            return Context.DeleteNav<MaintainPlanClass>(it => planClassIds.Contains(it.PlanClassId)).Include(s1 => s1.MaintainPlanClassItemNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 班次字典查询
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(MaintainPlanClassQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("PlanYear desc")
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictValue = it.PlanClassId.ToString(),
                    DictLabel = "[" + it.PlanYear.ToString() + "]" + it.PlanClass
                })
                .ToPage<DictDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<MaintainPlanClass> QueryExp(MaintainPlanClassQueryDto parm)
        {
            var predicate = Expressionable.Create<MaintainPlanClass>();

            predicate = predicate.AndIF(parm.PlanYear != null, it => it.PlanYear == parm.PlanYear);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.PlanClass), it => it.PlanClass == parm.PlanClass);
            return predicate;
        }

        private bool CheckData(MaintainPlanClass model)
        {
            if (model.PlanYear < DateTime.Now.Year)
                throw new CustomException($"计划年份不能低于当年");

            bool has = Queryable().Where(it => it.PlanYear == model.PlanYear && it.PlanClass == model.PlanClass).WhereIF(model.PlanClassId > 0, it => it.PlanClassId != model.PlanClassId).Any();
            if (has)
                throw new CustomException($"已存在年份：{model.PlanYear},班次：{model.PlanClass}的保养计划班次信息");

            bool hasgroup = model.MaintainPlanClassItemNav.GroupBy(it => new { it.DateMark, it.DateMarkStamp }).Where(g => g.Count() > 1).Any();
            if (hasgroup)
                throw new CustomException($"存在相同的日期标记与值的数据行，请检查");

            List<MaintainPlanClass> models = new List<MaintainPlanClass>();
            foreach (var item in model.MaintainPlanClassItemNav)
            {
                if (model.PlanYear != item.StartDate.Value.Year || model.PlanYear != item.EndDate.Value.Year)
                    throw new CustomException($"日期标记[{item.DateMark}],值[{item.DateMarkStamp}],存在非法的日期");
            }

            return true;
        }
    }
}