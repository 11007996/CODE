using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace ComTools
{
    internal class ConfigUtil
    {
        private static readonly string ConfigFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Settings.json");

        // 加载设置
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
                    Setting.DiskSpeed = loadedSettings.DiskSpeed;
                    Setting.DiskCapacity = loadedSettings.DiskCapacity;
                    Setting.AutomationTest = loadedSettings.AutomationTest;
                    Setting.DPLine = loadedSettings.DPLine;
                    Setting.AgingTest = loadedSettings.AgingTest;
                }
                else
                {//创建一个默认配置文件
                    InitSetting();
                }
            }
            catch (Exception ex)
            {
                InitSetting();
            }
        }

        // 保存设置
        public static void SaveConfig()
        {
            SettingJson setJson = new SettingJson()
            {
                DiskSpeed = Setting.DiskSpeed,
                DiskCapacity = Setting.DiskCapacity,
                AutomationTest = Setting.AutomationTest,
                DPLine = Setting.DPLine,
                AgingTest = Setting.AgingTest,
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

        private static void InitSetting()
        {
            Setting.DiskSpeed.FileSize = 1L * 1024 * 1024 * 1024;//1GB
            Setting.DiskSpeed.BlockSize = 1 * 1024 * 1024;//1MB
            Setting.DiskSpeed.TargetDisk = "C:";
            Setting.DiskSpeed.RunCount = 1;
            Setting.DiskSpeed.AutoFlag = true;
            Setting.DiskSpeed.WriteSpeedRange = 300;
            Setting.DiskSpeed.ReadSpeedRange = 300;
            SaveConfig();
        }
    }
}