using EAM.Model;
using EAM.Model.Basic;

using EAM.Model.Business;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Repository;
using EAM.Service.Business.IBusinessService;
using Infrastructure;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace EAM.Service.Business
{
    /// <summary>
    /// 产品测量报告Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IProdMeasureTicketService), ServiceLifetime = LifeTime.Transient)]
    public class ProdMeasureTicketService : BaseService<ProdMeasureTicket>, IProdMeasureTicketService
    {
        public ProdMeasureTicketService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询产品测量报告列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProdMeasureTicketDto> GetList(ProdMeasureTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.Includes(x => x.ProdMeasureTicketItemDefineNav) //填充子对象
                //.OrderBy("CreateTime desc")
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                .Where(predicate.ToExpression())
                .ToPage<ProdMeasureTicket, ProdMeasureTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public ProdMeasureTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                .Includes(x => x.ProdMeasureTicketItemDefineNav) //填充子对象
                .Includes(x => x.ProdMeasureTicketItemNav) //填充子对象
                .Includes(x => x.ProdMeasureTicketItemResultNav) //填充子对象
                .Where(x => x.TicketNo == TicketNo)
                .Select(x => new ProdMeasureTicket()
                {
                    ProdMeasureTicketItemDefineNav = x.ProdMeasureTicketItemDefineNav,
                    ProdMeasureTicketItemNav = x.ProdMeasureTicketItemNav,
                    ProdMeasureTicketItemResultNav = x.ProdMeasureTicketItemResultNav,
                    MakerName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.MakerId).Select(e => e.EmpName),
                    EngineerName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerId).Select(e => e.EmpName),
                    EngineerLeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerLeaderId).Select(e => e.EmpName),
                    QcName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.QcId).Select(e => e.EmpName),
                    QcLeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.QcLeaderId).Select(e => e.EmpName)
                }, true)
                .First();

            //处理数据，方便展示
            DataTable dt = new();
            //列：测试项
            dt.Columns.Add("ProductNo");
            foreach (var item in response.ProdMeasureTicketItemDefineNav)
            {
                dt.Columns.Add(item.OrderNo.ToString());
            }
            dt.Columns.Add("SizeResult");
            dt.Columns.Add("FacadeResult");
            //行：治具编号，判定结果
            foreach (var item in response.ProdMeasureTicketItemResultNav)
            {
                DataRow r = dt.NewRow();
                r["ProductNo"] = item.ProductNo;
                r["SizeResult"] = item.SizeResult;
                r["FacadeResult"] = item.FacadeResult;
                dt.Rows.Add(r);
            }
            //行：测量项目
            foreach (var item in response.ProdMeasureTicketItemNav)
            {
                DataRow r = dt.Select($"ProductNo='{item.ProductNo}'").FirstOrDefault();
                if (r != null)
                {
                    r[item.OrderNo.ToString()] = item.ActualValue;
                }
            }
            response.DynamicStatisticalReport = dt;
            return response;
        }

        /// <summary>
        /// 添加产品测量报告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProdMeasureTicket AddProdMeasureTicket(ProdMeasureTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            return Context.InsertNav(model)
                .Include(s1 => s1.ProdMeasureTicketItemDefineNav)
                .Include(s1 => s1.ProdMeasureTicketItemNav)
                .Include(s1 => s1.ProdMeasureTicketItemResultNav)
                .ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品测量报告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProdMeasureTicket(ProdMeasureTicket model)
        {
            return Context.UpdateNav(model).Include(z1 => z1.ProdMeasureTicketItemDefineNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除产品测量报告
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteProdMeasureTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            ProdMeasureTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ProdMeasureTicket> QueryExp(ProdMeasureTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<ProdMeasureTicket>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            return predicate;
        }

        private string GetNewId()
        {
            string code = "PM";
            string newId = $"{code}{DateTime.Now:yyyyMMdd}";
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