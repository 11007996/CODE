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
    /// 机台类型Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentTypeService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentTypeService : BaseService<EquipmentType>, IEquipmentTypeService
    {
        public EquipmentTypeService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询机台类型列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentTypeDto> GetList(EquipmentTypeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<EquipmentType, EquipmentTypeDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="EquipmentTypeName"></param>
        /// <returns></returns>
        public EquipmentType GetInfo(string EquipmentTypeName)
        {
            var response = Queryable()
                .Where(x => x.EquipmentTypeName == EquipmentTypeName)
                .First();

            return response;
        }

        /// <summary>
        /// 添加机台类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentType AddEquipmentType(EquipmentType model)
        {
            if (GetInfo(model.EquipmentTypeName) != null)
                throw new CustomException("存在相同的类型名称");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改机台类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateEquipmentType(EquipmentType model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询机台类型列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(EquipmentTypeQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictLabel = it.EquipmentTypeName,
                    DictValue = it.EquipmentTypeName
                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<EquipmentType> QueryExp(EquipmentTypeQueryDto parm)
        {
            var predicate = Expressionable.Create<EquipmentType>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.EquipmentTypeName), it => it.EquipmentTypeName.Contains(parm.EquipmentTypeName));
            return predicate;
        }
    }
}