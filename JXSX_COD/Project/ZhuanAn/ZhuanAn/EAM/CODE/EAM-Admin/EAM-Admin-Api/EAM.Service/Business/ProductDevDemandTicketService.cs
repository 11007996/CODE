using EAM.Model;
using EAM.Model.Basic;
using EAM.Model.Business;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Enums;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Basic.IBasicService;
using EAM.Service.Business.IBusinessService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Business
{
    /// <summary>
    /// 产品开发需求单Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IProductDevDemandTicketService), ServiceLifetime = LifeTime.Transient)]
    public class ProductDevDemandTicketService : BaseService<ProductDevDemandTicket>, IProductDevDemandTicketService
    {
        private readonly IPartService _partService;

        public ProductDevDemandTicketService(IHttpContextAccessor httpContextAccessor,
            IPartService partService) : base(httpContextAccessor)
        {
            _partService = partService;
        }

        /// <summary>
        /// 查询产品开发需求单列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProductDevDemandTicketDto> GetList(ProductDevDemandTicketQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(it => it.DelFlag == (int)DeleteFlagEnum.存在)
                //.Includes(x => x.ProductDevDemandTicketItemNav) //填充子对象
                //.OrderBy("CreateTime desc")
                .Where(predicate.ToExpression())
                .ToPage<ProductDevDemandTicket, ProductDevDemandTicketDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TicketNo"></param>
        /// <returns></returns>
        public ProductDevDemandTicket GetInfo(string TicketNo)
        {
            var response = Queryable()
                .Includes(x => x.ProductDevDemandTicketItemNav) //填充子对象
                .Where(x => x.TicketNo == TicketNo)
                 .Select(x => new ProductDevDemandTicket()
                 {
                     ProductDevDemandTicketItemNav = x.ProductDevDemandTicketItemNav,
                     EngineerName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerId).Select(e => e.EmpName),
                     EngineerLeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.EngineerLeaderId).Select(e => e.EmpName),
                     LeaderName = SqlFunc.Subqueryable<Employee>().Where(e => e.EmpCode == x.LeaderId).Select(e => e.EmpName),
                 }, true)
                .First();

            return response;
        }

        /// <summary>
        /// 添加产品开发需求单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductDevDemandTicket AddProductDevDemandTicket(ProductDevDemandTicket model)
        {
            model.TicketNo = GetNewId();
            model.DelFlag = (int)DeleteFlagEnum.存在;
            model.ProductDevDemandTicketItemNav.ForEach(it => it.TicketNo = model.TicketNo);
            Context.Insertable<ProductDevDemandTicketItem>(model.ProductDevDemandTicketItemNav).ExecuteCommand();
            return Context.Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品开发需求单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProductDevDemandTicket(ProductDevDemandTicket model)
        {
            return Context.UpdateNav(model).Include(z1 => z1.ProductDevDemandTicketItemNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 删除产品开发需求单
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public int DeleteProductDevDemandTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            ProductDevDemandTicket entity = Queryable().Where(it => it.TicketNo == ticketNo).First();
            entity.DelFlag = (int)DeleteFlagEnum.删除;
            return Update(entity, true);
        }

        /// <summary>
        /// 同步产品开发需求单
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <returns></returns>
        public int AsyncProductDevDemandTicket(string ticketNo)
        {
            if (string.IsNullOrEmpty(ticketNo))
                throw new CustomException("业务编号不能为空");
            ProductDevDemandTicket entity = GetInfo(ticketNo);

            //同步料号
            DbResult<bool> r = UseTran(() =>
            {
                Part part = Context.Queryable<Part>().Where(it => it.PartId == entity.PartId).First();
                if (part == null)
                {
                    part = new Part
                    {
                        PartId = entity.PartId
                    };
                    _partService.AddPart(part);
                }
                //同步载治具料号关联
                foreach (var item in entity.ProductDevDemandTicketItemNav)
                {
                    if (item.EquipmentType == EquipmentItemTypeConstant.治具 || item.EquipmentType == EquipmentItemTypeConstant.载具 && item.DevMode == EquipmentDevModeConstant.延用 && item.ExtendTargetId > 0)
                    {
                        //检查延用目标是否存在
                        FixtureBase fixture = Context.Queryable<FixtureBase>().Where(it => it.FixtureId == item.ExtendTargetId).First();
                        if (fixture == null)
                            throw new CustomException($"延用目标不存在，制程:{item.ProcessName}，目标ID:{item.ExtendTargetId}");

                        //关联延用目标
                        FixturePart fp = Context.Queryable<FixturePart>().Where(it => it.FixtureId == item.ExtendTargetId && it.PartId == part.PartId).First();
                        if (fp == null)
                        {
                            FixturePart fixturePart = new()
                            {
                                FixtureId = item.ExtendTargetId,
                                PartId = part.PartId,
                                DefaultQty = 1
                            };
                            Context.Insertable<FixturePart>(fixturePart).ExecuteCommand();
                        }
                    }
                }
            });

            if (!r.IsSuccess)
                throw new CustomException(r.ErrorMessage);

            return r.IsSuccess ? 1 : 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ProductDevDemandTicket> QueryExp(ProductDevDemandTicketQueryDto parm)
        {
            var predicate = Expressionable.Create<ProductDevDemandTicket>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.TicketNo), it => it.TicketNo == parm.TicketNo);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessInstanceId), it => it.ProcessInstanceId == parm.ProcessInstanceId);
            return predicate;
        }

        private string GetNewId()
        {
            string code = "PD";
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