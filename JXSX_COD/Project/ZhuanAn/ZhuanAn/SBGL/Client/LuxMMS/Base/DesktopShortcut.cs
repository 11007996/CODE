using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.IO;
using System.Windows.Forms;
namespace LuxMMS.Base
{
    class DesktopShortcut
    {

        // 创建桌面快捷方式
        public static void CreateDesktopShortcut(string targetPath, string shortcutName)
        {
            // 创建WshShell对象
            WshShell shell = new WshShell();

            // 获取桌面文件夹路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // 创建快捷方式对象
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(desktopPath, shortcutName + ".lnk"));

            // 设置快捷方式属性
            shortcut.TargetPath = targetPath; // 目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath); // 工作目录
            shortcut.Description = "Shortcut to " + shortcutName; // 快捷方式描述
            shortcut.IconLocation = targetPath; // 快捷方式图标路径
            shortcut.Save();
        }


        // 判断桌面上是否存在指定的快捷方式
        public static bool IsDesktopShortcutExists()
        {
            // 获取桌面文件夹路径
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // 获取桌面上的所有快捷方式文件
            string[] shortcutFiles = Directory.GetFiles(desktopPath, "*.lnk");

            // 检查文件是否存在
            foreach (string shortcutFile in shortcutFiles)
            {
                // 创建WshShell对象
                WshShell shell = new WshShell();

                // 读取快捷方式文件
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutFile);

                // 检查快捷方式的目标路径是否指向你的应用程序
                if (shortcut.TargetPath.Equals(Application.ExecutablePath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
