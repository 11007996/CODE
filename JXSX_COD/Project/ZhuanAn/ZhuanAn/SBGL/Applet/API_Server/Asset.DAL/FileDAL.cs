using Asset.DAL.Util;
using Asset.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asset.Model.Enum;

namespace Asset.DAL
{
    public class FileDAL
    {
        #region 源文件相关
        /// <summary>
        /// 搜索匹配的资产，最多10条记录
        /// </summary>
        /// <returns></returns>
        public FileInfoDO GetFileInfo(int fileId)
        {
            string sql = $"Select * FROM S_FileInfo Where FileId='{fileId}'";
            DataTable dt = DBUtil.GetDataTable(sql);
            return DataTableConvert.ToList<FileInfoDO>(dt)[0];
        }

        /// <summary>
        /// 添加文件信息，并返回文件ID
        /// </summary>
        /// <param name="fi"></param>
        /// <returns></returns>
        public int AddFileInfo(FileInfoDO fi)
        {
            string sql = $@"INSERT INTO S_FileInfo(FileName,FileExtension,FileAliasName,FileSize,Enabled,UpdateTime,UpdateUser,FileClass)Values
                                ('{fi.FileName}','{fi.FileExtension}','{fi.FileAliasName}','{fi.FileSize}','Y',GETDATE(),'{fi.UpdateUser}','{(int)fi.FileClass}')
                            SELECT  SCOPE_IDENTITY() ";
            DataTable dt = DBUtil.GetDataTable(sql);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        public bool UpdateFileInfo(FileInfoDO fi)
        {
            string sql = $@"UPDATE S_FileInfo SET FileName='{fi.FileName}',FileExtension='{fi.FileExtension}',FileAliasName='{fi.FileAliasName}',FileSize='{fi.FileSize}',
                            UpdateTime=GETDATE(),UpdateUser='{fi.UpdateUser}'  WHERE FileId='{fi.FileId}'";
            return DBUtil.ExecSQL(sql) > 0;
        }

        #endregion


        #region 预览文件相关
        /// <summary>
        /// 搜索匹配的资产，最多10条记录
        /// </summary>
        /// <returns></returns>
        public PreviewFileInfoDO GetPreviewFileInfo(int fileId)
        {
            string sql = $"Select * FROM S_PreviewFileInfo Where FileId='{fileId}'";
            DataTable dt = DBUtil.GetDataTable(sql);
            return DataTableConvert.ToList<PreviewFileInfoDO>(dt)[0];
        }


        public void DeletePreviewFile(int sourceFileId)
        {
            if (sourceFileId <= 0)
            {
                return;
            }
            string sql = $"DELETE FROM S_PreviewFileInfo WHERE SourceFileId='{sourceFileId}'";
            DBUtil.ExecSQL(sql);
        }

        public bool AddPreviewFile(List<PreviewFileInfoDO> files)
        {
            if (files == null || files.Count == 0)
            {
                return false;
            }
            string sql = "insert into S_PreviewFileInfo(SourceFileId,PageNo,FileName,FileAliasName,FileExtension,FileSize,PreviewType)Values";
            List<string> rowParm = new List<string>();
            for (int i = 0; i < files.Count; i++)
            {
                rowParm.Add($"({files[i].SourceFileId},{i + 1},'{files[i].FileName}','{files[i].FileAliasName}','{files[i].FileExtension}','{files[i].FileSize}','{(int)files[i].PreviewType}')");
            }
            sql += string.Join(",", rowParm);
            return DBUtil.ExecSQL(sql) > 0;
        }


        public List<PreviewFileInfoDO> GetPreviewFileBySourceFile(int sourceFileId, FilePreviewTypeEnum typeEnum)
        {
            if (sourceFileId <= 0 || typeEnum <= 0)
            {
                return null;
            }
            string sql = $"SELECT * FROM S_PreviewFileInfo WHERE SourceFileId={sourceFileId} AND PreviewType='{(int)typeEnum}';";
            DataTable dt = DBUtil.GetDataTable(sql);
            return DataTableConvert.ToList<PreviewFileInfoDO>(dt);
        }

        #endregion
    }
}
