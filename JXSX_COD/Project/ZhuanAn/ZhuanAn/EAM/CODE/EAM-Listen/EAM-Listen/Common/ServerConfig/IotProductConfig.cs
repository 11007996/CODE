using EAM.Listen.Common.Utils;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EAM.Listen.Common.Config
{
    public class IotProductConfig
    {
        /// <summary>
        /// 产品信息列表
        /// </summary>
        private static List<IotProduct> Products = new List<IotProduct>();

        /// <summary>
        /// 设备key映射到productId
        /// </summary>
        private static List<IotDevice> Devices = new List<IotDevice>();

        /// <summary>
        /// 设备绑定
        /// </summary>
        private static List<IotDeviceBind> DeviceBinds = new List<IotDeviceBind>();

        /// <summary>
        /// 产品的属性,key:产品id
        /// </summary>
        private static Dictionary<int, List<IotProductThingProperty>> ProductPropertyDict = new Dictionary<int, List<IotProductThingProperty>>();

        /// <summary>
        /// 产品的事件，key:产品id
        /// </summary>
        private static Dictionary<int, List<IotProductThingEvent>> ProductEventDict = new Dictionary<int, List<IotProductThingEvent>>();

        /// <summary>
        /// 产品的主题, key: 产品id
        /// </summary>
        private static Dictionary<int, List<IotProductTopic>> ProductTopicDict = new Dictionary<int, List<IotProductTopic>>();

        /// <summary>
        /// 产品的事件动作, key: 产品id
        /// </summary>
        private static Dictionary<int, List<IotProductEventAction>> ProductActionDict = new Dictionary<int, List<IotProductEventAction>>();

        /// <summary>
        /// 产品的解析脚本, key: 产品id
        /// </summary>
        private static Dictionary<int, IotProductParserScript> ProductScriptDict = new Dictionary<int, IotProductParserScript>();

        /// <summary>
        /// 加载配置
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                //初始化所有网关
                List<IotProduct> newProducts = SqlSugarUtil.Conn().Queryable<IotProduct>().ToList();
                List<int> updateProductIds = new List<int>();
                foreach (IotProduct product in newProducts)
                {
                    var tempP = Products.Where(it => it.ProductId == product.ProductId).FirstOrDefault();
                    if (tempP == null || tempP.Version != product.Version)
                    {
                        updateProductIds.Add(product.ProductId);
                    }
                }

                // 删除不需要的产品配置
                List<int> delProductIds = new List<int>();
                foreach (IotProduct product in Products)
                {
                    if (!newProducts.Where(it => it.ProductId == product.ProductId).Any())
                    {
                        delProductIds.Add(product.ProductId);
                    }
                }
                ClearProductConfig(delProductIds);

                // 更新配置
                LoadProductConfig(updateProductIds);

                //更新设备
                List<int> productIds = newProducts.Select(it => it.ProductId).ToList();
                LoadDevices(productIds);

                Products = newProducts == null ? new List<IotProduct>() : newProducts;//防止为null;
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(IotProductConfig), $"初始化产品配置异常", ex);
            }
        }

        /// <summary>
        /// 获取产品的配置
        /// </summary>
        /// <param name="productId"></param>
        public static IotProductConfigDto GetProductConfig(int productId)
        {
            if (productId <= 0)
                return null;

            // 优先从缓存获取
            object val = CacheHelper.GetProductConfig(productId);
            if (val != null)
                return (IotProductConfigDto)val;

            //获取产品的配置
            IotProductConfigDto cfgDto = new IotProductConfigDto()
            {
                Product = Products.Where(it => it.ProductId == productId).FirstOrDefault(),
                Propertys = ProductPropertyDict[productId],
                Events = ProductEventDict[productId],
                Actions = ProductActionDict[productId],
                Topics = ProductTopicDict[productId],
                ParserScript = ProductScriptDict[productId]
            };
            //解析引擎
            if (cfgDto.ParserScript != null)
            {
                try
                {
                    cfgDto.JsEngine = new Jint.Engine().Execute(cfgDto.ParserScript.ScriptCode);
                }
                catch (Exception)
                {
                }
            }

            // 添加到缓存
            CacheHelper.AddProductConfig(productId, cfgDto);

            return cfgDto;
        }

        /// <summary>
        /// 根据设备key获取设备
        /// </summary>
        /// <param name="deviceKey"></param>
        /// <returns></returns>
        public static IotDevice GetIotDeviceByKey(string deviceKey)
        {
            //获取产品的key
            if (Devices == null || Devices.Count <= 0)
                return null;

            //检查是否有对应的设备
            return Devices.Where(it => it.DeviceKey == deviceKey).FirstOrDefault();
        }

        /// <summary>
        /// 通过注册包获取设备
        /// </summary>
        /// <param name="registerPacket"></param>
        /// <returns></returns>
        public static IotDevice GetIotDeviceByRegisterPacket(string registerPacket)
        {
            //获取产品的key
            if (Devices == null || Devices.Count <= 0)
                return null;

            //检查是否有对应的设备
            return Devices.Where(it => it.RegisterPacket == registerPacket).FirstOrDefault();
        }

        /// <summary>
        /// 通过ID获取设备
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static IotDevice GetIotDeviceById(int deviceId)
        {
            //获取产品的key
            if (Devices == null || Devices.Count <= 0)
                return null;

            //检查是否有对应的设备
            return Devices.Where(it => it.DeviceId == deviceId).FirstOrDefault();
        }

        /// <summary>
        /// 获取设备绑定
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public static IotDeviceBind GetIotDeviceBindById(int deviceId)
        {
            //获取产品的key
            if (DeviceBinds == null || DeviceBinds.Count <= 0)
                return null;

            //检查是否有对应的设备
            return DeviceBinds.Where(it => it.DeviceId == deviceId).FirstOrDefault();
        }

        /// <summary>
        /// 清理指定产品的配置
        /// </summary>
        /// <param name="productIds"></param>
        private static void ClearProductConfig(List<int> productIds)
        {
            if (productIds == null || productIds.Count <= 0)
                return;
            foreach (int productId in productIds)
            {
                ProductPropertyDict.Remove(productId);
                ProductEventDict.Remove(productId);
                ProductActionDict.Remove(productId);
                ProductTopicDict.Remove(productId);
                ProductScriptDict.Remove(productId);
                CacheHelper.RemoveProductConfig(productId);
            }
        }

        /// <summary>
        /// 从数据库更新指定产品的配置
        /// </summary>
        /// <param name="productIds"></param>
        private static void LoadProductConfig(List<int> productIds)
        {
            if (productIds == null || productIds.Count <= 0)
                return;
            List<IotProductThingProperty> properties = SqlSugarUtil.Conn().Queryable<IotProductThingProperty>().Where(it => productIds.Contains(it.ProductId) && it.Enabled == true).ToList();
            List<IotProductThingEvent> events = SqlSugarUtil.Conn().Queryable<IotProductThingEvent>().Where(it => productIds.Contains(it.ProductId) && it.Enabled == true).ToList();
            List<int> eventIds = events.Select(it => it.EventId).ToList();
            List<IotProductEventAction> actions = SqlSugarUtil.Conn().Queryable<IotProductEventAction>().Where(it => eventIds.Contains(it.EventId) && it.Enabled == true).ToList();
            List<IotProductTopic> topics = SqlSugarUtil.Conn().Queryable<IotProductTopic>().Where(it => productIds.Contains(it.ProductId) && it.Enabled == true).ToList();
            List<IotProductParserScript> scripts = SqlSugarUtil.Conn().Queryable<IotProductParserScript>().Where(it => productIds.Contains(it.ProductId) && it.Enabled == true).ToList();

            //事件的参数
            List<IotProductParamDefine> eventParms = SqlSugarUtil.Conn().Queryable<IotProductParamDefine>().Where(it => eventIds.Contains(it.OwnerId) && it.OwnerType == "evnet").ToList();
            foreach (IotProductThingEvent ent in events)
            {
                ent.Params = eventParms?.Where(it => it.OwnerId == ent.EventId).ToList();
            }

            //更新到全局变量
            foreach (int productId in productIds)
            {
                //更新产品属性配置
                if (ProductPropertyDict.ContainsKey(productId))
                    ProductPropertyDict[productId] = properties?.Where(it => it.ProductId == productId).ToList();
                else
                    ProductPropertyDict.Add(productId, properties?.Where(it => it.ProductId == productId).ToList());
                // 更新产品事件配置
                if (ProductEventDict.ContainsKey(productId))
                    ProductEventDict[productId] = events?.Where(it => it.ProductId == productId).ToList();
                else
                    ProductEventDict.Add(productId, events?.Where(it => it.ProductId == productId).ToList());
                // 更新事件动作
                eventIds = events.Where(it => it.ProductId == productId).Select(it => it.EventId).ToList();
                if (ProductActionDict.ContainsKey(productId))
                    ProductActionDict[productId] = actions?.Where(it => eventIds.Contains(it.EventId)).ToList();
                else
                    ProductActionDict.Add(productId, actions?.Where(it => eventIds.Contains(it.EventId)).ToList());
                // Topic
                if (ProductTopicDict.ContainsKey(productId))
                    ProductTopicDict[productId] = topics?.Where(it => it.ProductId == productId).ToList();
                else
                    ProductTopicDict.Add(productId, topics?.Where(it => it.ProductId == productId).ToList());
                // 解析脚本
                if (ProductScriptDict.ContainsKey(productId))
                    ProductScriptDict[productId] = scripts?.Where(it => it.ProductId == productId).FirstOrDefault();
                else
                    ProductScriptDict.Add(productId, scripts?.Where(it => it.ProductId == productId).FirstOrDefault());

                //清掉缓存
                CacheHelper.RemoveProductConfig(productId);
            }
        }

        /// <summary>
        /// 加载Iot设备
        /// </summary>
        /// <param name="productIds"></param>
        private static void LoadDevices(List<int> productIds)
        {
            //设备信息
            if (productIds == null || productIds.Count <= 0) return;
            List<IotDevice> devices = SqlSugarUtil.Conn().Queryable<IotDevice>().Where(it => productIds.Contains(it.ProductId) && it.Enabled == true).ToList();
            if (devices == null)
                Devices = new List<IotDevice>();
            else
                Devices = devices;

            //绑定信息
            List<int> ids = devices.Select(it => it.DeviceId).ToList();
            List<IotDeviceBind> iotDeviceBinds = SqlSugarUtil.Conn().Queryable<IotDeviceBind>().Where(it => ids.Contains(it.DeviceId)).ToList();
            if (iotDeviceBinds == null)
                DeviceBinds = new List<IotDeviceBind>();
            else
                DeviceBinds = iotDeviceBinds;
        }
    }
}