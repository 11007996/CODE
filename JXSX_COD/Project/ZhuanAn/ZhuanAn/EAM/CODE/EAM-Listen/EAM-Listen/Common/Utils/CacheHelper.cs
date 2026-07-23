using EAM.Listen.Common.Config;
using System;
using System.Runtime.Caching;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EAM.Listen.Common.Utils
{
    public class CacheHelper
    {
        public static readonly string EAMTokenCacheKey = "EAM-Token-Cache-";
        private static readonly string IotProductConfigKey = "Iot-Config-Product-";

        /// <summary>
        /// 缓存管理
        /// </summary>
        private static MemoryCache MCache = MemoryCache.Default;

        private static MemoryCache TokenCache = MemoryCache.Default;

        #region 产品配置缓存

        public static void AddProductConfig(int productId, IotProductConfigDto val)
        {
            MCache.Add(IotProductConfigKey + productId, val, DateTimeOffset.Now.AddDays(1));
        }

        public static IotProductConfigDto GetProductConfig(int productId)
        {
            object val = MCache.Get(IotProductConfigKey + productId);
            return val != null ? (IotProductConfigDto)val : null;
        }

        public static void RemoveProductConfig(int productId)
        {
            MCache.Remove(IotProductConfigKey + productId);
        }

        #endregion 产品配置缓存

        #region EAM Token缓存

        public static void AddEAMToken(string factoryId, string userName, string Token, int cacheMinutes)
        {
            TokenCache.Add(EAMTokenCacheKey + factoryId + "-" + userName, Token, DateTimeOffset.Now.AddMinutes(cacheMinutes));
        }

        public static string GetEAMToken(string factoryId, string userName)
        {
            object val = TokenCache.Get(EAMTokenCacheKey + factoryId + "-" + userName);
            return val != null ? val.ToString() : null;
        }

        public static void RemoveEAMToken(string factoryId, string userName)
        {
            TokenCache.Remove(EAMTokenCacheKey + factoryId + "-" + userName);
        }

        #endregion EAM Token缓存
    }
}