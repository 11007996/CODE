using EAM.Model.System;
using EAM.Model.System.Dto;
using EAM.ServiceCore.Model;
using Infrastructure.Attribute;

namespace EAM.ServiceCore.Services
{
    /// <summary>
    /// 系统部门扩展Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ISysDeptExpandService), ServiceLifetime = LifeTime.Transient)]
    public class SysDeptExpandService : BaseService<SysDeptExpand>, ISysDeptExpandService
    {
        /// <summary>
        /// 查询系统部门扩展列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<SysDeptExpandDto> GetList(SysDeptExpandQueryDto parm)
        {
            var response = Context.Queryable<SysDept>()
                .LeftJoin<SysDeptExpand>((d, it) => it.SysDeptId == d.DeptId)
                .WhereIF(!string.IsNullOrEmpty(parm.DeptName), (d, it) => d.DeptName.Contains(parm.DeptName))
                .WhereIF(!string.IsNullOrEmpty(parm.FactoryId), (d, it) => it.DefaultFactoryId == parm.FactoryId)
                .Select((d, it) => new SysDeptExpandDto()
                {
                    SysDeptId = d.DeptId,
                    LuxDeptId = it.LuxDeptId,
                    WxDeptId = it.WxDeptId,
                    DefaultFactoryId = it.DefaultFactoryId,
                    DeptName = d.DeptName,
                    ParentId = d.ParentId
                })
                .ToList();

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="SysDeptId"></param>
        /// <returns></returns>
        public SysDeptExpand GetInfo(long SysDeptId)
        {
            var response = Queryable()
                .Where(x => x.SysDeptId == SysDeptId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加系统部门扩展
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysDeptExpand AddSysDeptExpand(SysDeptExpand model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改系统部门扩展
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSysDeptExpand(SysDeptExpand model)
        {
            return Update(model);
        }

        /// <summary>
        /// 从同步系统部门名称
        /// </summary>
        /// <returns></returns>
        public bool SyncSysDeptExpand()
        {
            //获取部门扩展配置
            List<SysDeptExpand> deptExpands = Queryable().Where(it => it.LuxDeptId != null).ToList();
            if (deptExpands == null || deptExpands.Count <= 0) return true;
            List<string> luxDeptIds = deptExpands.Select(it => it.LuxDeptId).ToList();
            List<long> sysDeptIds = deptExpands.Select(it => it.SysDeptId).ToList();
            //获取集团部门信息
            LuxEmpService luxEmpService = new LuxEmpService();
            List<LuxEmp> luxDeptlist = luxEmpService.Queryable().GroupBy(it => new { it.DeptCode, it.DeptName }).Select(it => new LuxEmp()
            {
                DeptCode = it.DeptCode,
                DeptName = it.DeptName
            }).Where(it => luxDeptIds.Contains(it.DeptCode)).ToList();

            //获取需要更新的系统部门
            List<SysDept> depts = new List<SysDept>();
            string tempDeptName = string.Empty;
            foreach (var item in deptExpands)
            {
                tempDeptName = luxDeptlist.Where(it => it.DeptCode == item.LuxDeptId).Select(it => it.DeptName).FirstOrDefault();
                if (!string.IsNullOrEmpty(tempDeptName))
                    depts.Add(new SysDept() { DeptId = item.SysDeptId, DeptName = tempDeptName });
            }

            return Context.Updateable(depts).UpdateColumns(it => it.DeptName).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<SysDeptExpand> QueryExp(SysDeptExpandQueryDto parm)
        {
            var predicate = Expressionable.Create<SysDeptExpand>();

            predicate.AndIF(parm.SysDeptId > 0, it => it.SysDeptId == parm.SysDeptId);
            predicate.AndIF(!string.IsNullOrEmpty(parm.FactoryId), it => it.DefaultFactoryId == parm.FactoryId);

            return predicate;
        }
    }
}