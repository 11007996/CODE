using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallSys.Utils
{
    class FileUtil
    {
        public static Image GetCacheFile(string handlerNo, DateTime? updateTime)
        {
            try
            {
                if (handlerNo == null || updateTime == null) return global::CallSys.Properties.Resources.defualt_face;
                string url = null;
                string imageName = handlerNo + ".jpg";
                string filePath = BaseInfo.PicCachePath + "\\" + imageName;

                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists || fi.LastWriteTime < updateTime)
                {
                    url = GetNewCacheFile(handlerNo);
                }
                else if (fi.Exists && fi.LastWriteTime >= updateTime)
                {
                    url = filePath;
                }
                if (fi.Exists && url == null)
                { //删除原来的文件。
                    fi.Delete();
                }
                if (url != null)
                {
                    FileStream fs = new FileStream(url, FileMode.Open);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                    fs.Close();
                    return img;
                }
                else
                {
                    return global::CallSys.Properties.Resources.defualt_face;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FileUtil), ex.Message);
            }
            return global::CallSys.Properties.Resources.defualt_face;
        }


        public static string GetNewCacheFile(string handlerNo)
        {
            string url = null;
            string sql = string.Format("SELECT HandlerImage FROM M_HandlerInfo_T WHERE HandlerNo='{0}';", handlerNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["HandlerImage"] != DBNull.Value)
            {
                byte[] Image_img = (byte[])dt.Rows[0]["HandlerImage"];
                if (Image_img.Length > 0)
                {
                    int filelength = Image_img.Length;
                    string imageName = handlerNo + ".jpg";
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

        public static string CreateNewCacheFile(string handlerNo, byte[] Image_img)
        {
            string url = null;
            if (Image_img != null && Image_img.Length > 0)
            {
                int filelength = Image_img.Length;
                string imageName = handlerNo + ".jpg";
                url = BaseInfo.PicCachePath + "\\" + imageName;
                FileStream fs = new FileStream(url, FileMode.Create, FileAccess.Write);
                BinaryWriter BW = new BinaryWriter(fs);
                BW.BaseStream.Write(Image_img, 0, filelength);
                BW.Flush();
                BW.Close();
                fs.Close();
            }
            return url;
        }
    }
}
