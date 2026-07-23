using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Call.Base
{
    public class UserImageUtil
    {
        //查检头像是否有更新，如果有则删除当前的删除。
        public static void UpdateAllUserImage()
        {
            try
            {
                string sql = "SELECT  UserNo,ImageUpdateTime FROM S_User_T";
                DataTable userDT = DBUtil.GetDataTable(sql);
                if (userDT == null) return;

                string filePath = "";
                foreach (DataRow user in userDT.Rows)
                {
                    filePath = BaseInfo.PicCachePath + "\\" + user["UserNo"].ToString() + ".jpg";
                    FileInfo fi = new FileInfo(filePath);
                    if (user["ImageUpdateTime"] == DBNull.Value && fi.Exists)
                        fi.Delete();
                    else if (user["ImageUpdateTime"] != DBNull.Value && fi.LastWriteTime < Convert.ToDateTime(user["ImageUpdateTime"]))
                        fi.Delete();
                }

            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(UserImageUtil), ex.Message);
            }
        }


        public static Image GetCacheFile(string userNo)
        {
            try
            {
                if (userNo == null) return global::Call.Properties.Resources.defualt_face;
                string filePath = BaseInfo.PicCachePath + "\\" + userNo + ".jpg";

                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists)
                {
                    return GetNewCacheImage(userNo);
                }

                FileStream fs = new FileStream(filePath, FileMode.Open);
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                fs.Close();
                return img;
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(UserImageUtil), ex.Message);
                return global::Call.Properties.Resources.defualt_face;
            }
        }

        public static Image GetNewCacheImage(string userNo)
        {
            string filePath = GetNewCachePath(userNo);
            if (string.IsNullOrEmpty(filePath))
                return global::Call.Properties.Resources.defualt_face;
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                fs.Close();
                return img;
            }
        }


        private static string GetNewCachePath(string userNo)
        {
            string url = null;
            string sql = string.Format("SELECT UserImage FROM S_User_T WHERE UserNo='{0}';", userNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["UserImage"] != DBNull.Value)
            {
                byte[] Image_img = (byte[])dt.Rows[0]["UserImage"];
                if (Image_img.Length > 0)
                {
                    int filelength = Image_img.Length;
                    string imageName = userNo + ".jpg";
                    url = BaseInfo.PicCachePath + "\\" + imageName;
                    FileStream fs = new FileStream(url, FileMode.Create, FileAccess.Write);
                    BinaryWriter BW = new BinaryWriter(fs);
                    BW.BaseStream.Write(Image_img, 0, filelength);
                    BW.Flush();
                    BW.Close();
                    fs.Close();
                }
            }
            return url;
        }
    }
}
