using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Business.IBusinessService;
using EAM.Service.Fixture.IFixtureService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace EAM.Service.Business
{
    /// <summary>
    /// 治具尺寸量测验收单Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(ISizeMeasureTicketService), ServiceLifetime = LifeTime.Transient)]
    public class SizeMeasureTicketService : BaseService<SizeMeasureTicket>, ISizeMeasureTicketService
    {
        private IFixtureBaseService _fixtureService;
        private IFixtureStorageService _fixtureStorageService;

        public SizeMeasureTicketService(
            IHttpContextAccessor httpContextAccessor,
            IFixtureStorageService fixtureStorageService,
            IFixtureBaseService fixtureService) : base(httpContextAccessor)
        {
            _fixtureService = fixtureService;
            _fixtureStorageService = fixtureStorageService;
        }

        /// <summary>
        /// 查询治具尺寸量测验收单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SizeMeasureTicketDto> GetList(SizeMeasureTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()

                //.Includes(x => x.SizeMeasureTicketItemNav) //填充子对象
                //.OrderBy("CreateTime desc")
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .Where(predicate.ToExpression())
                .ToPage<SizeMeasureTicket, SizeMeasureTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public SizeMeasureTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                .Includes(x => x.SizeMeasureTicketItemDefineNav)
                .Includes(x => x.SizeMeasureTicketItemNav) //填充子对象
                .Includes(x => x.SizeMeasureTicketItemResultNav)
                .Includes(x => x.SizeMeasureTicketItemOtherNav)
                .Where(x => x.TicketNo == TicketNo)
                .Select(x => new SizeMeasureTicket()
                {
                    SizeMeasureTicketItemDefineNav = x.SizeMeasureTicketItemDefineNav,
                    SizeMeasureTicketItemNav = x.SizeMeasureTicketItemNav,
                    SizeMeasureTicketItemResultNav = x.SizeMeasureTicketItemResultNav,
                    SizeMeasureTicketItemOtherNav = x.SizeMeasureTicketItemOtherNav,
                    EngineerName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerId).Select(e => e.EmpName),
                    EngineerLeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerLeaderId).Select(e => e.EmpName),
                    QcName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.QcId).Select(e => e.EmpName),
                    QcLeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.QcLeaderId).Select(e => e.EmpName)
                }, true)
                .First();
            if (response == null)
                throw new CustomException($"未找到业务编号{TicketNo}");
            //处理数据，方便展示
            DataTable dt = new DataTable();
            //列：测试项
            dt.Columns.Add("FixtureNo");
            foreach (var item in response.SizeMeasureTicketItemDefineNav)
            {
                dt.Columns.Add(item.OrderNo.ToString());
            }
            dt.Columns.Add("SizeResult");
            dt.Columns.Add("InStorage");
            //行：治具编号，判定结果
            foreach (var item in response.SizeMeasureTicketItemResultNav)
            {
                DataRow r = dt.NewRow();
                r["FixtureNo"] = item.FixtureNo;
                r["SizeResult"] = item.SizeResult;
                r["InStorage"] = item.InStorage;
                dt.Rows.Add(r);
            }
            //行：测量项目
            foreach (var item in response.SizeMeasureTicketItemNav)
            {
                DataRow r = dt.Select($"FixtureNo='{item.FixtureNo}'").FirstOrDefault();
                if (r != null)
                {
                    r[item.OrderNo.ToString()] = item.ActualValue;
                }
            }
            response.DynamicStatisticalReport = dt;
            return response;
        }

        /// <summary>
        /// 添加治具尺寸量测验收单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SizeMeasureTicket AddSizeMeasureTicket(SizeMeasureTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            return Context.InsertNav(model)
                .Include(s1 => s1.SizeMeasureTicketItemDefineNav)
                .Include(s1 => s1.SizeMeasureTicketItemNav)
                .Include(s1 => s1.SizeMeasureTicketItemResultNav)
                .Include(s1 => s1.SizeMeasureTicketItemOtherNav)
                .ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改治具尺寸量测验收单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateSizeMeasureTicket(SizeMeasureTicket model)
        {
            return Context.UpdateNav(model).Include(z1 => z1.SizeMeasureTicketItemNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除治具尺寸量测验收单
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteSizeMeasureTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            SizeMeasureTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 批量入库
        /// </summary>
        /// <param name="modal"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int SizeMeasureTicketInStorage(SizeMeasureTicketInStorageDto modal)
        {
            SizeMeasureTicket smt = Queryable()
                .Includes(x => x.SizeMeasureTicketItemResultNav)
                .Where(x => x.TicketNo == modal.TicketNo)
                .First();
            //检查
            if (smt == null)
                throw new CustomException("未找到业务信息");
            if (smt.SizeMeasureTicketItemResultNav == null || smt.SizeMeasureTicketItemResultNav.Count <= 0)
                throw new CustomException("业务信息无测量治具");
            if (modal.CheckedFixtureList == null || modal.CheckedFixtureList.Count <= 0)
                throw new CustomException("未选择治具");

            //检查是否有已入库治具
            string inStorage = null;
            string erroMsg = "";
            foreach (var item in modal.CheckedFixtureList)
            {
                inStorage = smt.SizeMeasureTicketItemResultNav.Where(it => it.FixtureNo == item.FixtureNo).Select(it => it.InStorage).FirstOrDefault();
                if (inStorage == "Y")
                    erroMsg += $"治具编号{item.FixtureNo}已入库 | ";
            }
            if (!string.IsNullOrEmpty(erroMsg))
                throw new CustomException(erroMsg);

            DbResult<bool> r = UseTran(() =>
            {
                //更新治具信息
                FixtureBase fixture = Context.Queryable<FixtureBase>().Where(it => it.FixtureId == smt.FixtureId).First();
                if (fixture == null)
                    throw new CustomException("未找到治具信息");

                //入库
                List<OperateFixtureStorageDto> li = modal.CheckedFixtureList.GroupBy(it => new { it.StorageId }).Select(a => new OperateFixtureStorageDto
                {
                    FixtureId = (int)fixture.FixtureId,
                    StorageId = Convert.ToInt32(a.Key.StorageId),
                    ChangeQty = a.Count(),
                    CreateBy = modal.CreateBy,
                    CreateTime = modal.CreateTime,
                    TicketNo = modal.TicketNo,
                    TicketType = TicketTypeConstant.治具尺寸量测单,
                }).ToList();
                _fixtureStorageService.BatchInFixtureStorage(li);

                //更新业务清单治具结果状态
                List<string> list = modal.CheckedFixtureList.Select(it => it.FixtureNo).ToList();
                Context.Updateable<SizeMeasureTicketItemResult>().SetColumns(it => it.InStorage == "Y").Where(it => list.Contains(it.FixtureNo)).ExecuteCommand();
            });

            //处理结果
            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return modal.CheckedFixtureList.Count;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<SizeMeasureTicket> QueryExp(SizeMeasureTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<SizeMeasureTicket>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            return predicate;
        }

        private string GetNewId()
        {
            string code = "SM";
            string newId = $"{code}{DateTime.Now.ToString("yyyyMMdd")}";
            var max = Queryable().Where(it => it.TicketNo.StartsWith(newId)).Max(it => it.TicketNo);
            if (max == null)
            {
                newId += "0001";
            }
            else
            {
                int num = Convert.ToInt32(max.Replace(newId, ""));
                newId += (num + 1).ToString().PadLeft(4, '0');
            }
            return newId;
        }
    }
}