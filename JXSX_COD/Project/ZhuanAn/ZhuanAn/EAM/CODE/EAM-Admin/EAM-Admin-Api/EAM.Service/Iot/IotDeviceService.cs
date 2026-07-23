using EAM.Model;
using EAM.Model.Call;
using EAM.Model.Constant;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Model.Iot;
using EAM.Repository;
using EAM.Service.Iot.IIotService;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace EAM.Service.Iot
{
    /// <summary>
    /// 产品设备表Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IIotDeviceService), ServiceLifetime = LifeTime.Transient)]
    public class IotDeviceService : BaseService<IotDevice>, IIotDeviceService
    {
        public IotDeviceService(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        /// <summary>
        /// 查询产品设备表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<IotDeviceDto> GetList(IotDeviceQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<IotProduct>((it, p) => it.ProductId == p.ProductId)
                .LeftJoin<IotDeviceBind>((it, p, db) => it.DeviceId == db.DeviceId)
                .LeftJoin<EquipmentBase>((it, p, db, e) => db.EquipmentId == e.EquipmentId)
                .LeftJoin<CallBoxBase>((it, p, db, e, b) => db.BoxId == b.BoxId)
                .Where(predicate.ToExpression())
                .Select((it, p, db, e, b) => new IotDeviceDto()
                {
                    ProductName = p.ProductName,
                    EquipmentId = e.EquipmentId,
                    EquipmentName = e.AssetNo + " : " + e.EquipmentName,
                    BoxId = b.BoxId,
                    BoxName = "[" + b.Mac + "]" + b.BoxName,
                }, true)
                .ToPage<IotDeviceDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public IotDeviceDto GetInfo(int deviceId)
        {
            var response = Queryable()
                .LeftJoin<IotProduct>((it, p) => it.ProductId == p.ProductId)
                .Where(it => it.DeviceId == deviceId)
                .Select((it, p) => new IotDeviceDto()
                {
                    ProductName = p.ProductName,
                }, true)
                .First();

            if (response != null)
                response.BindInfo = Context.Queryable<IotDeviceBind>()
                        .Where(it => it.DeviceId == deviceId)
                        .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                        .LeftJoin<CallBoxBase>((it, e, b) => it.BoxId == b.BoxId)
                        .Select((it, e, b) => new IotDeviceBindDto()
                        {
                            DeviceId = it.DeviceId,
                            EquipmentId = e.EquipmentId,
                            EquipmentName = e.EquipmentName,
                            BoxId = b.BoxId,
                            BoxName = "[" + b.Mac + "]" + b.BoxName,
                        }).First();

            return response;
        }

        /// <summary>
        /// 添加产品设备表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotDevice AddIotDevice(IotDevice model)
        {
            CheckData(model);
            model.Status = IotDeviceStatusConstant.未注册;
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改产品设备表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateIotDevice(IotDevice model)
        {
            CheckData(model);

            return Update(model, true);
        }

        /// <summary>
        /// 查询产品设备表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(IotDeviceQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Select(it => new DictDataDto()
                {
                    DictValue = it.DeviceId.ToString(),
                    DictLabel = it.DeviceName + "[" + it.DeviceKey + "]",

                })
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// IOT设备绑定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IotDeviceBind BindIotDevice(IotDeviceBind model)
        {
            IotDeviceBind idb = Context.Queryable<IotDeviceBind>().Where(it => it.DeviceId == model.DeviceId).First();

            bool has = false;
            //检查生产设备是否已绑定
            if (model.EquipmentId > 0)
            {
                has = Context.Queryable<IotDeviceBind>().Where(it => it.EquipmentId == model.EquipmentId && it.DeviceId != model.DeviceId).Count() > 0;
                if (has)
                    throw new CustomException("当前绑定的生产设备已被其他IOT设备绑定");
            }
            //检查呼叫盒是否已绑定
            if (model.BoxId > 0)
            {
                has = Context.Queryable<IotDeviceBind>().Where(it => it.BoxId == model.BoxId && it.DeviceId != model.DeviceId).Count() > 0;
                if (has)
                    throw new CustomException("当前绑定的呼叫盒已被其他IOT设备绑定");
            }

            if (idb == null)
            {//新增
                return Context.Insertable(model).ExecuteReturnEntity();
            }
            else
            {//更新
                idb.EquipmentId = model.EquipmentId;
                idb.BoxId = model.BoxId;
                Context.Updateable(idb).ExecuteCommand();
                return idb;
            }
        }

        /// <summary>
        /// 产品设备解绑
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public int UnBindIotDevice(int deviceId)
        {
            return Context.Deleteable<IotDeviceBind>().Where(it => it.DeviceId == deviceId).ExecuteCommand();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<IotDevice> QueryExp(IotDeviceQueryDto parm)
        {
            var predicate = Expressionable.Create<IotDevice>();

            predicate = predicate.AndIF(parm.ProductId != null, it => it.ProductId == parm.ProductId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.DeviceName), it => it.DeviceName.Contains(parm.DeviceName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.DeviceKey), it => it.DeviceKey == parm.DeviceKey);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.RegisterPacket), it => it.RegisterPacket == parm.RegisterPacket);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Keyword), it => it.DeviceName.Contains(parm.Keyword) || it.DeviceKey.Contains(parm.Keyword) || it.RegisterPacket.Contains(parm.Keyword));
            return predicate;
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GenerateSecureString(int length = 12)
        {
            Random _random = new Random();
            string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(_chars[_random.Next(_chars.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// 数据检查
        /// </summary>
        /// <param name="model"></param>
        private void CheckData(IotDevice model)
        {
            //检查设备key
            if (string.IsNullOrEmpty(model.DeviceKey))
                throw new CustomException("设备key不能为空");
            string[] keyArr = new string[] { "set", "get", "post", "property", "event", "service", "value", "time" };
            if (keyArr.Contains(model.DeviceKey.ToLower()))
                throw new CustomException("设备key不能为特殊值");
            if (!Regex.IsMatch(model.DeviceKey, "^[a-zA-Z0-9_]+$"))
                throw new CustomException("设备key只能是字母数字下划线组成");
            bool hasKey = Queryable().Where(it => it.DeviceKey == model.DeviceKey).WhereIF(model.DeviceId > 0, it => it.DeviceId != model.DeviceId).Any();
            if (hasKey)
                throw new CustomException("当前设备Key已被其他设备使用");

            //检查注册包
            if (!string.IsNullOrEmpty(model.RegisterPacket))
            {
                if (model.RegisterPacket.Length != 12)
                    throw new CustomException("注册包长度必需为12个字符");
                if (!Regex.IsMatch(model.RegisterPacket, "^[A-Z0-9]+$"))
                    throw new CustomException("注册包只能是大写字母数字组成");
                hasKey = Queryable().Where(it => it.RegisterPacket == model.RegisterPacket).WhereIF(model.DeviceId > 0, it => it.DeviceId != model.DeviceId).Any();
                if (hasKey)
                    throw new CustomException("当前注册包已被其他设备使用");
            }
        }
    }
}