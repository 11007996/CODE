using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallSys.Utils
{
    class UpdateHelper
    {
        public const string START_TYPE_UPDATE = "0";//更新启动
        public const string START_TYPE_UPLOAD = "1";//上传启动

        //下次检查更新的时间, 初始为次日早上8:30，随机增加秒数，防止同时大量更新。
        public static DateTime CHECK_TIME = DateTime.Now.Date.AddDays(1).AddHours(8).AddMinutes(30).AddSeconds(new Random().Next(0, 60));

        //重置次日检查时间
        public static void ResetCheckTime()
        {
            CHECK_TIME = DateTime.Now.Date.AddDays(1).AddHours(8).AddMinutes(30).AddSeconds(new Random().Next(0, 60));
        }

        /// <summary>
        /// 启动更新程序
        /// </summary>
        /// <param name="startType">打开方式: 0更新，1上传</param>
        /// <param name="userName">用户名</param>
        /// <param name="lastForm"></param>
        public static void StartUpdateApp(string startType)
        {
            string username = BaseInfo.LoginHandlerNo == null ? "\"\"" : BaseInfo.LoginHandlerNo;
            string currFrmType = GlobalData.CurrFrmType == null ? "\"\"" : GlobalData.CurrFrmType.ToString();
            string args = string.Format("{0} {1} {2}", startType, username, currFrmType);
            //启动update.exe，更新。
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Update.exe", args.Trim());
            if (startType == START_TYPE_UPDATE)
                System.Environment.Exit(0);
        }

        //检查并自动更新呼叫系统
        public static void AutoUpdateCallSysApp()
        {
            //数据库所有可用文件
            if (CheckCallSysApp())
            {
                StartUpdateApp(START_TYPE_UPDATE);
            }
        }

        //检查呼叫系统是否有更新
        public static bool CheckCallSysApp()
        {
            //数据库所有可用文件
            string sql = "SELECT FileId,FileName,FileSize,FileTime,FileVersion,UpdateMode,Remark FROM S_SystemFile_T WHERE UseFlag='Y' AND UpdateStatus='Y' ";
            DataTable sysFiles = DBUtil.GetDataTable(sql);
            if (sysFiles == null || sysFiles.Rows.Count <= 0) return false;
            //比对当前文件夹下文件
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = sysFiles.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = sysFiles.Rows[i];
                string filePath = dirPath + row["FileName"].ToString();
                FileInfo fi = new FileInfo(filePath);
                string fileVersion = "";
                if (fi.Exists)
                {
                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(filePath);
                    fileVersion = info.FileVersion == null ? "" : info.FileVersion;
                }
                //清除不需要更新的文件(条件： 文件存在 and 文件创建时间大于上传时间 and 文件版本一致)
                if (fi.Exists && fi.CreationTime >= Convert.ToDateTime(row["FileTime"].ToString()) && fileVersion == row["FileVersion"].ToString())
                {
                    sysFiles.Rows.Remove(row);
                }
            }
            return sysFiles.Rows.Count > 0;
        }


        //检查update.exe是否有更新
        public static void CheckUpdateApp()
        {
            string sql = "SELECT FileId,FileName,FileSize,FileTime,FileContent,FileVersion FROM S_SystemFile_T WHERE UseFlag='Y'  AND FileName=N'Update.exe' ";
            DataTable dt = DBUtil.GetDataTable(sql);
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string filePath = dirPath + row["FileName"].ToString();

                //本地文件信息
                FileInfo fi = new FileInfo(filePath);
                string fileVersion = "";
                if (fi.Exists)
                {
                    //本地文件版本信息
                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(filePath);
                    fileVersion = info.FileVersion == null ? "" : info.FileVersion;
                }

                //需要更新(条件:文件不存在 or 当前文件创建时间小于上传时间  or  文件版本不匹配)
                if (!fi.Exists || fi.CreationTime <= Convert.ToDateTime(row["FileTime"].ToString()) || fileVersion != row["FileVersion"].ToString())
                {
                    if (fi.Exists) fi.Delete();//删除原文件
                    byte[] bt = (byte[])dt.Rows[0]["FileContent"];
                    FileStream fs = new FileStream(dirPath + row["FileName"].ToString(), FileMode.Create, FileAccess.Write);
                    fs.Write(bt, 0, bt.Length);
                    fs.Close();
                    FileInfo f2 = new FileInfo(dirPath + row["FileName"].ToString());
                    f2.CreationTime = TimeUtil.Now;
                }
            }
        }
    }
}
