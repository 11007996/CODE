using EAM.Listen.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EAM.Listen.Common.Config
{
    public class FactoryBaseConfig
    {
        //数据库获取的厂区配置
        public static MaintainConfigDto MaintainConfig = new MaintainConfigDto();

        // 自定义TCP协议
        public static SignalConfigDto SignalConfig = new SignalConfigDto();

        private const string 保养检查开关 = "MaintainCheckFlag";
        private const string 设备运行数据上传开关 = "EquipmentRunDataUploadFlag";
        private const string 设备接收数据编码配置 = "EquipmentReceiveCodeSetting";
        private const string 设备发送数据编码配置 = "EquipmentSendCodeSetting";

        /// <summary>
        /// 从服务器加载厂区配置
        /// </summary>
        public static void LoadConfig()
        {
            string[] configKeys = { 保养检查开关, 设备运行数据上传开关, 设备接收数据编码配置, 设备发送数据编码配置 };
            List<FactoryConfig> fcs = SqlSugarUtil.Conn().Queryable<FactoryConfig>().Where(it => configKeys.Contains(it.ConfigKey) && it.EnableFlag == "Y").ToList();

            string configVal;

            configVal = GetConfigVal(fcs, 保养检查开关);
            MaintainConfig.DayMaintainFlag = string.IsNullOrEmpty(configVal) ? false : configVal.Contains("D");
            MaintainConfig.WeekMaintainFlag = string.IsNullOrEmpty(configVal) ? false : configVal.Contains("W");
            MaintainConfig.MonthMaintainFlag = string.IsNullOrEmpty(configVal) ? false : configVal.Contains("M");

            configVal = GetConfigVal(fcs, 设备运行数据上传开关);
            SignalConfig.UploadDataFlag = string.IsNullOrEmpty(configVal) ? false : configVal == "Y" ? true : false;

            configVal = GetConfigVal(fcs, 设备接收数据编码配置);
            SignalConfig.ReceiveCodeItems = string.IsNullOrEmpty(configVal) ? null : JsonConvert.DeserializeObject<List<ItemCode>>(configVal);

            configVal = GetConfigVal(fcs, 设备发送数据编码配置);
            SignalConfig.SendCodeItems = string.IsNullOrEmpty(configVal) ? null : JsonConvert.DeserializeObject<List<ItemCode>>(configVal);
        }

        private static string GetConfigVal(List<FactoryConfig> dt, string configKey)
        {
            string configVal = null;
            if (dt != null)
            {
                configVal = dt.Where(it => it.ConfigKey == configKey).Select(it => it.ConfigValue).FirstOrDefault();
            }
            return configVal;
        }
    }
}