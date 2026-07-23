using EAM.Common;
using EAM.Model;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Fixture;
using EAM.Repository;
using EAM.Service.Fixture.IFixtureService;
using Infrastructure;
using Infrastructure.Attribute;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Fixture
{
    /// <summary>
    /// 治具储位信息Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IFixtureStorageSpaceService), ServiceLifetime = LifeTime.Transient)]
    public class FixtureStorageSpaceService : BaseService<FixtureStorageSpace>, IFixtureStorageSpaceService
    {
        public FixtureStorageSpaceService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询储位信息列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<FixtureStorageSpaceDto> GetList(FixtureStorageSpaceQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("OrderNum asc")
                .Where(predicate.ToExpression())
                .ToPage<FixtureStorageSpace, FixtureStorageSpaceDto>(parm);

            return response;
        }

        /// <summary>
        /// 查询储位信息树列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<FixtureStorageSpace> GetTreeList(FixtureStorageSpaceTreeQueryDto parm)
        {
            List<FixtureStorageSpace> response = new List<FixtureStorageSpace>();

            if (!string.IsNullOrEmpty(parm.StorageName))
            {//条件
                List<FixtureStorageSpace> sp = Queryable().Where(it => it.StorageName.Contains(parm.StorageName)).ToList();
                if (sp != null && sp.Count > 0)
                {
                    var ids = sp.Select(it => it.StorageId).Cast<object>().ToArray();
                    response = Queryable()
                    .OrderBy(it => it.OrderNum)
                    .ToTree(it => it.Children, it => it.ParentId, 0, ids);
                }
            }
            else
            {//全部
                response = Queryable()
               .OrderBy(it => it.OrderNum)
               .ToTree(it => it.Children, it => it.ParentId, 0);
            }

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="StorageId"></param>
        /// <returns></returns>
        public FixtureStorageSpace GetInfo(int StorageId)
        {
            var response = Queryable()
                .Where(x => x.StorageId == StorageId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加储位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public FixtureStorageSpace AddStorageSpace(FixtureStorageSpace model)
        {
            if (model.ParentId == null)
            {
                model.ParentId = 0;
            }
            if (model.StorageName.Contains('/'))
                throw new CustomException("储位名称不可以包含特殊字符'/'");
            if (!CheckStorageType((int)model.ParentId, model.StorageType))
                throw new CustomException("储位类型不合法");
            if (!CheckStorageName((int)model.ParentId, model.StorageId, model.StorageName))
                throw new CustomException("此储位名称在当前层级已存在");

            FixtureStorageSpace parentSS = GetFirst(it => it.StorageId == model.ParentId);
            //如果父节点不为正常状态,则不允许新增子节点
            if (parentSS != null && parentSS.Status != StorageSpaceStatusConstant.正常)
            {
                throw new CustomException("父级储位已停用，不允许新增");
            }

            model.StorageFullName = GetParentFullName((int)model.ParentId) + model.StorageName;
            model.Ancestors = GetAncestorsIds((int)model.ParentId);
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改储位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateStorageSpace(FixtureStorageSpace model)
        {
            if (model.ParentId == null) { model.ParentId = 0; }
            if (model.StorageName.Contains('/'))
                throw new CustomException("储位名称不可以包含特殊字符'/'");
            if (model.ParentId == model.StorageId)
                throw new CustomException("父级储位不能是当前储位本身");
            if (!CheckStorageType((int)model.ParentId, model.StorageType))
                throw new CustomException("储位类型不合法");
            if (!CheckStorageName((int)model.ParentId, model.StorageId, model.StorageName))
                throw new CustomException("此储位名称在当前层级已存在");
            //检查父级储位是否合法
            if (!CheckParentStorage((int)model.ParentId, model.StorageId))
                throw new CustomException("父级储位不能是当前储位的子集");

            model.StorageFullName = null;
            model.Ancestors = null;

            // 更新所有子集的 【祖级信息】、【储位全名】
            FixtureStorageSpace newParentSS = GetFirst(it => it.StorageId == model.ParentId);
            FixtureStorageSpace oldSS = GetFirst(it => it.StorageId == model.StorageId);
            if (newParentSS != null)
            {// 新的有上级
                model.Ancestors = newParentSS.Ancestors + "," + newParentSS.StorageId;
                model.StorageFullName = newParentSS.StorageFullName + " / " + model.StorageName;
            }
            else
            {// 调整为最上级
                model.StorageFullName = model.StorageName;
            }
            string newAncestors = model.Ancestors + "," + model.StorageId;
            string oldAncestors = oldSS.Ancestors + "," + oldSS.StorageId;
            UpdateStorageSpaceChildren(model.StorageId, newAncestors, oldAncestors, model.StorageFullName, oldSS.StorageFullName);

            // 更新所有父级的状态
            if (model.Status == StorageSpaceStatusConstant.正常 && model.Ancestors.IfNotEmpty()
               && !"0".Equals(model.Ancestors))
            {
                // 如果该储位是启用状态，则启用该储位的所有上级储位
                UpdateParentStorageStatusNormal(model);
            }
            return Update(model);
        }

        /// <summary>
        /// 删除储位
        /// </summary>
        /// <param name="storageId"></param>
        /// <returns></returns>
        public int DeleteStorageSpace(int storageId)
        {
            //判断是否存在储位信息
            FixtureStorageSpace model = GetInfo(storageId);
            if (model == null)
                throw new CustomException($"未找到要删除的储位{storageId}信息");
            //判断是否存在子储位
            int count = Queryable().Where(it => it.ParentId == storageId).Count();
            if (count > 0)
                throw new CustomException($"存在下级储位，请先删除下级储位");
            //判断是否有治具与当前储位关联
            count = Context.Queryable<FixtureStorage>().Where(it => it.StorageId == storageId).Count();
            if (count > 0)
                throw new CustomException($"当前储位有治具库存信息，请先删除当前储位关联下的治具库存信息");
            return Delete(model);
        }

        /// <summary>
        /// 修改子元素关系
        /// </summary>
        /// <param name="storageSpaceId">被修改的储位ID</param>
        /// <param name="newAncestors">新的父ID集合</param>
        /// <param name="oldAncestors">旧的父ID集合</param>
        /// <param name="newFullName"></param>
        /// <param name="oldFullName"></param>
        public void UpdateStorageSpaceChildren(int storageSpaceId, string newAncestors, string oldAncestors, string newFullName, string oldFullName)
        {
            // 获取有关联的所有子储位
            List<FixtureStorageSpace> all = Queryable().ToList();
            List<FixtureStorageSpace> children = GetChildrenStorageSpace(all, storageSpaceId);

            //更新所有子储位的 【祖级信息】、【储位全名】
            string ancestors = null;
            string fullName = null;
            foreach (var child in children)
            {
                ancestors = child.Ancestors.ReplaceFirst(oldAncestors, newAncestors);//之前有上级,
                fullName = child.StorageFullName.ReplaceFirst(oldFullName, newFullName);
                child.Ancestors = ancestors;
                child.StorageFullName = fullName;
            }

            //更新所有子孙集的 [祖级信息]\[储位全名]
            if (children.Any())
            {
                Context.Updateable(children).WhereColumns(f => new { f.StorageId })
                .UpdateColumns(it => new { it.Ancestors, it.StorageFullName }).ExecuteCommand();
            }
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<FixtureStorageSpace> QueryExp(FixtureStorageSpaceQueryDto parm)
        {
            var predicate = Expressionable.Create<FixtureStorageSpace>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.StorageName), it => it.StorageName == parm.StorageName);
            return predicate;
        }

        /// <summary>
        /// 修改该储位的父级储位状态
        /// </summary>
        /// <param name="storageSpace">当前储位</param>
        private void UpdateParentStorageStatusNormal(FixtureStorageSpace storageSpace)
        {
            int[] storageIds = Tools.SpitIntArrary(storageSpace.Ancestors);
            storageSpace.Status = StorageSpaceStatusConstant.正常;
            storageSpace.UpdateTime = DateTime.Now;
            // 更新所有父级储位的【更新人】、【更新时间】、【状态】
            Update(storageSpace, it => new { it.UpdateBy, it.UpdateTime, it.Status }, f => storageIds.Contains(f.StorageId));
        }

        /// <summary>
        /// 获取所有子储位
        /// </summary>
        /// <param name="storageSpaces">所有储位信息</param>
        /// <param name="storageId">当前储位ID</param>
        /// <returns></returns>
        private List<FixtureStorageSpace> GetChildrenStorageSpace(List<FixtureStorageSpace> storageSpaces, int storageId)
        {
            return storageSpaces.FindAll(delegate (FixtureStorageSpace item)
            {
                int[] pid = Tools.SpitIntArrary(item.Ancestors);
                return pid.Contains(storageId);
            });
        }

        /// <summary>
        /// 获取父级的全名
        /// </summary>
        /// <param name="storageId"></param>
        /// <returns></returns>
        private string GetParentFullName(int storageId)
        {
            string parentFullName = null;
            var parentList = Queryable().ToParentList(it => it.ParentId, storageId);
            foreach (var item in parentList)
            {
                parentFullName = item.StorageName + " / " + parentFullName;
            }
            return parentFullName;
        }

        /// <summary>
        /// 获取所有的父级的Id（祖级列表）
        /// </summary>
        /// <param name="storageId">当前的储位ID</param>
        /// <returns></returns>
        private string GetAncestorsIds(int storageId)
        {
            string ancestorsIds = null;
            var parentList = Queryable().ToParentList(it => it.ParentId, storageId);
            foreach (var item in parentList)
            {
                ancestorsIds = ancestorsIds + "," + item.StorageId;
            }
            return ancestorsIds;
        }

        /// <summary>
        /// 检查当前储位类型是否是父储位的子类型
        /// </summary>
        /// <param name="parentId">父储位ID</param>
        /// <param name="storageType">当前储位类型</param>
        /// <returns></returns>
        private bool CheckStorageType(int parentId, string storageType)
        {
            if (parentId == 0)
            {
                return true;//不限定最上级的储位类型;storageType == StorageTypeConstant.仓库;
            }
            else
            {
                string parentStorageType = GetInfo(parentId)?.StorageType;
                return int.Parse(parentStorageType) < int.Parse(storageType);
            }
        }

        private bool CheckStorageName(int parentId, int? storageId, string storageName)
        {
            int count = Queryable().Where(it => it.ParentId == parentId && it.StorageName == storageName)
                .WhereIF(storageId != null, it => it.StorageId != storageId).Count();
            return count <= 0;
        }

        /// <summary>
        /// 检查父级储位的是否是当前储位的子集
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="storageId"></param>
        /// <returns></returns>
        private bool CheckParentStorage(int parentId, int storageId)
        {
            var storage = Queryable().Where(it => it.StorageId == parentId).First();
            if (storage != null && storage.Ancestors != null)
            {
                return !storage.Ancestors.Split(',').Contains(storageId.ToString());
            }
            return true;
        }
    }
}