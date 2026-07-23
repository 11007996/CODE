using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxMMS.Base
{
    public class UpdateHelper
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
            string username = BaseInfo.LoginUserNo == null ? "\"\"" : BaseInfo.LoginUserNo;
            string currFrmType = BaseInfo.CurrFrmType == null ? "\"\"" : BaseInfo.CurrFrmType.ToString();
            string args = string.Format("{0} {1} {2}", startType, username, currFrmType);
            //启动update.exe，更新。
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Update.exe", args.Trim());
            if (startType == START_TYPE_UPDATE)
                System.Environment.Exit(0);
        }

        //检查并自动更新呼叫系统
        public static void AutoUpdateApp()
        {
            //数据库所有可用文件
            if (CheckMainApp())
            {
                StartUpdateApp(START_TYPE_UPDATE);
            }
        }

        //检查主系统是否有更新
        public static bool CheckMainApp()
        {
            //数据库所有可用文件
            string sql = "SELECT FileId,FileName,FileSize,FileTime,FileVersion,Remark FROM S_SystemFile_T WHERE UseFlag='Y' AND IsUpdateApp<>'Y' ";
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
            string sql = "SELECT FileId,FileName,FileSize,FileTime,FileContent,FileVersion FROM S_SystemFile_T WHERE UseFlag='Y'  AND IsUpdateApp='Y' ";
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

        /// <summary>
        /// 清理过时无效的系统文件
        /// </summary>
        public static void ClearObsoleteFiles()
        {
            string sql = string.Format("SELECT FileName FROM S_SystemFile_T WHERE UseFlag='Y'");
            DataTable dt = DBUtil.GetDataTable(sql);
            List<string> dbFileNames = dt.AsEnumerable().Select(t => t.Field<string>("FileName")).ToList();
            // 获取程序目录
            string bootPath = AppDomain.CurrentDomain.BaseDirectory;

            // 获取桌面上的所有快捷方式文件
            List<string> files = Directory.GetFiles(bootPath, "*.dll").ToList();
            files.AddRange(Directory.GetFiles(bootPath, "*.exe").ToList());
            files.Distinct();
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                if (!dbFileNames.Contains(fileName))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
