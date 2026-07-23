using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ComTools.Util
{
    public class DiskHelper
    {
        /// <summary>
        /// 获取当前所有磁盘设备名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDisks()
        {
            // 获取当前计算机的所有逻辑磁盘
            List<string> disks = new List<string>();
            string diskName;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                diskName = drive.Name.Split('\\')[0];
                disks.Add(diskName);
            }
            return disks;
        }

        /// <summary>
        /// 获取触发消息事件的设备名称
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string GetDeviceName(Message m)
        {
            try
            {
                DEV_BROADCAST_VOLUME vol;
                vol = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));

                string driveLetter = GetDriveLetter(vol.dbcv_unitmask);
                return driveLetter;
                //Console.WriteLine("driveLetter:"+ driveLetter);
                //DriveInfo drive = new DriveInfo(driveLetter);

                //if (drive.IsReady)
                //{
                //    return drive.Name;
                //}
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static string GetDriveLetter(int mask)
        {
            string letter = "";
            for (char c = 'A'; c <= 'Z'; ++c)
            {
                if ((mask & 1) == 1)
                {
                    letter = c.ToString();
                    break;
                }
                mask >>= 1;
            }
            return letter + ":\\";
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_VOLUME
        {
            public int dbcv_size;
            public int dbcv_devicetype;
            public int dbcv_reserved;
            public int dbcv_unitmask;
        }
    }
}