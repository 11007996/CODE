using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Update.Utils;

namespace Update
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //加载配置文件
            ConfigUtil.LoadXmlConfig();

            //同步服务器时间
            DBUtil.SyncServerTime();

            ParseArgs(args);//解析参数
            if (GlobalData.StartType == "0")
            {
                //关掉所有打开了的CallSys.exe程序
                if (Process.GetProcessesByName("CallSys").ToList().Count > 0)
                {
                    foreach (Process p in Process.GetProcessesByName("CallSys"))
                    {
                        p.Kill();
                    }
                }
                Application.Run(new FrmUpdate());
            }
            else
            {
                Application.Run(new FrmUploadFile());
            }
        }

        //解析启动应用的参数
        public static void ParseArgs(string[] args)
        {
            //args说明:
            //         0:启动方式  0或不传值：更新，1：上传
            //         1:用户工号
            //         2:最后打开的Form名称,需要回传给主程序
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (i == 0) GlobalData.StartType = args[0];
                    if (i == 1) GlobalData.UserName = args[1];
                    if (i == 2) GlobalData.LastFormName = args[2];
                }
            }
            else
            {
                GlobalData.StartType = "0";
            }
        }
    }
}
