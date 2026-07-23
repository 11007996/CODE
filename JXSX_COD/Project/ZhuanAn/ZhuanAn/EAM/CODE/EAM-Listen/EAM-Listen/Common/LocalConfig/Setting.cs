using EAM.Listen.Common.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 全局的配置入口
    /// </summary>
    public class Setting
    {
        public static CommonConfigDto CommonConfig = new CommonConfigDto();
        public static DbConfigDto DbConfig = new DbConfigDto();
        public static MqttConfigDto MqttConfig = new MqttConfigDto();
        public static HttpConfigDto HttpConfig = new HttpConfigDto();
        public static TCPConfigDto TCPConfig = new TCPConfigDto();
        public static SerialPortConfigDto SerialPortConfig = new SerialPortConfigDto();
        public static EAMLoginConfigDto EAMLoginConfig = new EAMLoginConfigDto();

        private static readonly string ConfigFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

        /// <summary>
        /// 保存到配置文件
        /// </summary>
        public static void SaveConfig()
        {
            SettingJson setJson = new SettingJson()
            {
                CommonConfig = Setting.CommonConfig,
                DbConfig = Setting.DbConfig,
                MqttConfig = Setting.MqttConfig,
                HttpConfig = Setting.HttpConfig,
                TCPConfig = Setting.TCPConfig,
                SerialPortConfig = Setting.SerialPortConfig,
                EAMLoginConfig = Setting.EAMLoginConfig
            };
            string json = JsonConvert.SerializeObject(setJson, Formatting.Indented);
            if (!Directory.Exists(Path.GetDirectoryName(ConfigFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
            if (File.Exists(ConfigFilePath))
            {
                File.Delete(ConfigFilePath);
            }
            FileStream fs = File.Open(ConfigFilePath, FileMode.OpenOrCreate);
            byte[] data = Encoding.UTF8.GetBytes(json);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                //判断是否有配置文件
                if (File.Exists(ConfigFilePath))
                {//从配置文件读取
                    string jsonStr = File.ReadAllText(ConfigFilePath);
                    //返序列化
                    SettingJson loadedSettings = JsonConvert.DeserializeObject<SettingJson>(jsonStr);
                    Setting.CommonConfig = loadedSettings.CommonConfig;
                    Setting.DbConfig = loadedSettings.DbConfig;
                    Setting.MqttConfig = loadedSettings.MqttConfig;
                    Setting.HttpConfig = loadedSettings.HttpConfig;
                    Setting.TCPConfig = loadedSettings.TCPConfig;
                    Setting.SerialPortConfig = loadedSettings.SerialPortConfig;
                    Setting.EAMLoginConfig = loadedSettings.EAMLoginConfig;
                }
                else
                {//创建一个默认配置文件
                    SaveConfig();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(Setting), ex.Message);
                SaveConfig();
            }
        }
    }

    /// <summary>
    /// 用于序列化保存测试的设置
    /// </summary>
    public class SettingJson
    {
        public CommonConfigDto CommonConfig = new CommonConfigDto();
        public DbConfigDto DbConfig = new DbConfigDto();
        public MqttConfigDto MqttConfig = new MqttConfigDto();
        public HttpConfigDto HttpConfig = new HttpConfigDto();
        public TCPConfigDto TCPConfig = new TCPConfigDto();
        public SerialPortConfigDto SerialPortConfig = new SerialPortConfigDto();
        public EAMLoginConfigDto EAMLoginConfig = new EAMLoginConfigDto();
    }
}