using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Spire.Xls;
using System.Drawing.Imaging;
using System.Configuration;
using Asset.Model.Enum;
using System.Data;
using Asset.DAL;
using Asset.Model;
using System.Runtime.InteropServices.ComTypes;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using Aspose.Cells.Rendering;
using Aspose.Cells;
using System.Diagnostics;
//using static System.Net.WebRequestMethods;

namespace Asset.BLL
{
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileManager
    {
        private FileDAL dao = new FileDAL();
        //资产相关文件的部分目录
        private static readonly string ASSET_FILE_FOLDER = "AssetFile";

        /// <summary>
        /// 获取预览文件信息
        /// </summary>
        /// <param name="sourceFileId"></param>
        /// <returns></returns>
        public List<PreviewFileInfoDO> GetPreviewFiles(int sourceFileId, FilePreviewTypeEnum previewTyp)
        {
            string path = string.Empty;
            return dao.GetPreviewFileBySourceFile(sourceFileId, previewTyp);
        }



        /// <summary>
        /// 根据文件id，获取文件在服务器上的路径
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public FileInfo GetFilePath(int fileId, ref string fileAliasName)
        {
            string path = string.Empty;
            FileInfoDO fi = dao.GetFileInfo(fileId);
            if (fi != null && !string.IsNullOrEmpty(fi.FileName))
            {
                //上传根目录
                path = GetFileSavePath(fi.FileClass);
                fileAliasName = fi.FileAliasName;
                return new FileInfo(path + fi.FileName);
            }
            return null;
        }

        /// <summary>
        /// 获取预览文件路径
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public FileInfo GetPreviewFilePath(int previewFileId, ref string fileAliasName)
        {
            string path = string.Empty;
            PreviewFileInfoDO fi = dao.GetPreviewFileInfo(previewFileId);
            if (fi != null && !string.IsNullOrEmpty(fi.FileName))
            {
                FileInfoDO sourcefi = dao.GetFileInfo((int)fi.SourceFileId);
                //上传根目录
                path = GetFileSavePath(sourcefi.FileClass);
                fileAliasName = fi.FileAliasName;
                return new FileInfo(path + fi.FileName);
            }
            return null;
        }


        /// <summary>
        /// 根据不同分类，保存或替换指定文件
        /// </summary>
        /// <param name="fileId">需要替换的文件Id,为空表示新增</param>
        /// <param name="fileClass"></param>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SaveOrReplaceFile(int? fileId, FileClassEnum? fileClass, Stream stream, string fileName, string userId, ref string msg)
        {
            msg = "";
            //文件后缀
            string fileExtension = Path.GetExtension(fileName);
            //判断要替换的文件是否存在
            if (fileId != null)
            {
                FileInfoDO oldfi = dao.GetFileInfo((int)fileId);
                if (oldfi == null || oldfi.FileId == null)
                {
                    msg = "未找到要替换的文件";
                    return false;
                }
                fileClass = oldfi.FileClass;
            }

            //文件检查
            if (!CheckFile(stream.Length, fileClass, fileExtension, ref msg))
                return false;
            //文件保存的目录
            string uploadFolder = GetFileSavePath(fileClass);

            //生成一个新的随机文件名(防止重名覆盖)
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_") + Guid.NewGuid().ToString().Replace("-", "").ToUpper() + fileExtension;

            //保存文件
            if (SaveFile(stream, newFileName, uploadFolder))
            {
                FileInfo fi = new FileInfo(uploadFolder + newFileName);
                FileInfoDO fiDo = new FileInfoDO()
                {
                    FileId = fileId,
                    FileName = fi.Name,
                    FileAliasName = fileName,
                    FileExtension = fi.Extension,
                    FileSize = fi.Length,
                    UpdateUser = userId,
                    FileClass = fileClass,
                };

                if (fileId == null)
                {//新增
                    fileId = dao.AddFileInfo(fiDo);
                }
                else
                {//修改
                    dao.UpdateFileInfo(fiDo);
                    dao.DeletePreviewFile((int)fileId);
                }
                if (fileId != null)
                {
                    CreatePreviewFile((int)fileId, fi.FullName, FilePreviewTypeEnum.BLUR);
                    CreatePreviewFile((int)fileId, fi.FullName, FilePreviewTypeEnum.HIGH);
                }
                msg = "文件保存成功";
                return true;
            }
            else
            {
                msg = "上传文件保存失败";
                return false;
            }
        }


        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="saveFolder"></param>
        /// <returns></returns>
        private static bool SaveFile(Stream stream, string fileName, string saveFolder)
        {
            string savePath = saveFolder + fileName;
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            FileStream fsWrite = new FileStream(savePath, FileMode.Create);

            byte[] bytes = new byte[1024 * 4];
            int total = 0;
            int size = 0;
            do
            {
                //注意第二个参数是在buffer中的偏移量，不是在文件中的偏移量
                size = stream.Read(bytes, 0, bytes.Length);
                fsWrite.Write(bytes, 0, size);
                total += size;
            } while (size > 0);
            fsWrite.Close();
            return true;
        }

        /// <summary>
        /// 上传文件检查
        ///     检查大小
        ///     检查文件类型
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="fileClass"></param>
        /// <param name="fileExtension"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static bool CheckFile(long fileSize, FileClassEnum? fileClass, string fileExtension, ref string msg)
        {
            //检查文件大小
            msg = "";
            long uploadMaxSize = long.Parse(ConfigurationManager.AppSettings["uploadMaxSize"].ToString());
            if (fileSize > uploadMaxSize * 1024 * 1024)
            {
                msg = $"文件过大,最大限制:{uploadMaxSize}MB";
                return false;
            }
            if (fileClass == null)
            {
                msg = $"文件分类不确定";
                return false;
            }

            //根据不同的上传分类，检查文件是否合规则，再保存到不同文件夹
            switch (fileClass)
            {
                case FileClassEnum.MSOP:
                case FileClassEnum.MMI:
                case FileClassEnum.MMOS:
                    if (fileExtension != ".xlsx" && fileExtension != ".xls")
                    {
                        msg = "文件类型不合规则（只能上传xlsx或xls文件）";
                        return false;
                    }
                    break;
                default:
                    msg = "文件分类未匹配合规的文件类型，请联系管理员";
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 获取文件保存位置
        ///     不同FileClassEnum文件存放位置不同
        /// </summary>
        /// <param name="fileClass"></param>
        /// <returns></returns>
        private static string GetFileSavePath(FileClassEnum? fileClass)
        {
            //上传文件保存目录=(应用根目录+文件上传根存放位置+不同分类文件夹)
            string uploadFolder = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["uploadPath"].ToString() + "\\";
            //根据不同的上传分类，检查文件是否合规则，再保存到不同文件夹
            switch (fileClass)
            {
                case FileClassEnum.MSOP:
                case FileClassEnum.MMI:
                case FileClassEnum.MMOS:
                    uploadFolder += FileManager.ASSET_FILE_FOLDER + "\\";
                    break;
                default:
                    uploadFolder += "Temp\\";
                    break;
            }
            return uploadFolder;
        }


        private void CreatePreviewFile(int sourceFileId, string sourceFile, FilePreviewTypeEnum previewType)
        {
            List<PreviewFileInfoDO> pfiDO = null;
            if (!File.Exists(sourceFile))
                return;
            string extension = Path.GetExtension(sourceFile);
            switch (extension)
            {
                case ".xlsx":
                case ".xls":
                    if (previewType == FilePreviewTypeEnum.HIGH)
                    {
                        pfiDO = ConvertExcelToImgReturnDO(sourceFile, 120);
                    }
                    else if (previewType == FilePreviewTypeEnum.BLUR)
                    {
                        pfiDO = ConvertExcelToImgReturnDO(sourceFile, 30);
                    }
                    break;
                default:
                    break;
            }
            //保存预览文件数据
            if (pfiDO != null)
            {
                foreach (PreviewFileInfoDO fi in pfiDO)
                {
                    fi.SourceFileId = sourceFileId;
                    fi.PreviewType = previewType;
                }
                dao.AddPreviewFile(pfiDO);
            }
        }

        /// <summary>
        /// excel文件别存为图片
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="dpi">对比度</param>
        /// <returns>返回生成的图片信息集合</returns>
        //private static List<FileInfo> ConvertExcelToImg(string excelFilePath, int dpi)
        //{
        //    FileInfo f = new FileInfo(excelFilePath);
        //    //判断文件是否存在
        //    if (!f.Exists)
        //    {
        //        return null;
        //    }
        //    //判断文件是否是指定格式
        //    if (f.Extension != ".xlsx" && f.Extension != ".xls")
        //    {
        //        return null;
        //    }
        //    Workbook workbook = new Workbook();
        //    workbook.LoadFromFile(excelFilePath);
        //    //调整转换的清析度
        //    workbook.ConverterSetting.XDpi = dpi;
        //    workbook.ConverterSetting.YDpi = dpi;
        //    //转换工作表为图片
        //    List<FileInfo> result = new List<FileInfo>();
        //    string imageFilePath;
        //    foreach (Worksheet sheet in workbook.Worksheets)
        //    {
        //        //只保存可见的工作表
        //        if (sheet.Visibility == WorksheetVisibility.Visible)
        //        {
        //            imageFilePath = excelFilePath + sheet.Index + ".png";
        //            //保存到图片
        //            sheet.SaveToImage(imageFilePath, ImageFormat.Png);
        //            if (File.Exists(imageFilePath))
        //                result.Add(new FileInfo(imageFilePath));
        //        }
        //    }
        //    return result;
        //}


        /// <summary>
        /// excel文件别存为图片
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="dpi">对比度</param>
        /// <returns>返回生成的图片信息集合</returns>
        //private static List<PreviewFileInfoDO> ConvertExcelToImgReturnDO(string excelFilePath, int dpi)
        //{
        //    FileInfo f = new FileInfo(excelFilePath);
        //    //判断文件是否存在
        //    if (!f.Exists)
        //    {
        //        return null;
        //    }
        //    //判断文件是否是指定格式
        //    if (f.Extension != ".xlsx" && f.Extension != ".xls")
        //    {
        //        return null;
        //    }
        //    Workbook workbook = new Workbook();
        //    workbook.LoadFromFile(excelFilePath);
        //    //调整转换的清析度
        //    workbook.ConverterSetting.XDpi = dpi;
        //    workbook.ConverterSetting.YDpi = dpi;
        //    //转换工作表为图片
        //    List<PreviewFileInfoDO> result = new List<PreviewFileInfoDO>();
        //    string imageFilePath;
        //    foreach (Spire.Xls.Worksheet sheet in workbook.Worksheets)
        //    {
        //        //只保存可见的工作表
        //        if (sheet.Visibility == WorksheetVisibility.Visible)
        //        {
        //            imageFilePath = excelFilePath + "_" + sheet.Index + "_" + dpi + ".png";
        //            //保存到图片
        //            sheet.SaveToImage(imageFilePath, ImageFormat.Png);
        //            if (File.Exists(imageFilePath))
        //            {
        //                FileInfo fi = new FileInfo(imageFilePath);
        //                result.Add(new PreviewFileInfoDO()
        //                {
        //                    FileName = fi.Name,
        //                    FileExtension = fi.Extension,
        //                    FileSize = fi.Length,
        //                    FileAliasName = sheet.Name
        //                });
        //                return result;
        //            }
        //        }
        //    }
        //    return null;
        //}


        private static List<PreviewFileInfoDO> ConvertExcelToImgReturnDO(string excelFilePath, int dpi)
        {
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(excelFilePath);

            int cnt = workbook.Worksheets.Count;

            ImageOrPrintOptions imgOptions = new ImageOrPrintOptions();
            // Set the format type of the image
            imgOptions.ImageFormat = ImageFormat.Png;
            imgOptions.VerticalResolution = dpi;
            imgOptions.HorizontalResolution = dpi;
            //**********Add this line************//
            // CellsHelper.setFontDir("c:\\windows\\fonts");
            List<PreviewFileInfoDO> result = new List<PreviewFileInfoDO>();
            for (int i = 0; i < workbook.Worksheets.Count; i++)
            {
                // Get the worksheet.
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[i]; 

                // Create a SheetRender object with respect to your desired sheet
                SheetRender sr = new SheetRender(sheet, imgOptions);

                for (int j = 0; j < sr.PageCount; j++)
                {
                    // Generate image(s) for the worksheet
                    string imageFilePath = excelFilePath + "_" + sheet.Index + "_" + j + "_" + dpi + ".png";
                    sr.ToImage(j, imageFilePath);
                    FileInfo fi = new FileInfo(imageFilePath);
                    result.Add(new PreviewFileInfoDO()
                    {
                        FileName = fi.Name,
                        FileExtension = fi.Extension,
                        FileSize = fi.Length,
                        FileAliasName = sheet.Name
                    });
                }

            }
            return result;

        }


        /// <summary>
        /// excel文件另存为PDF文件
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="pdfFilePath"></param>
        /// <returns></returns>
        //private static bool ConvertExcelToPDF(string excelFilePath, string pdfFilePath)
        //{
        //    FileInfo f = new FileInfo(excelFilePath);
        //    //判断文件是否存在
        //    if (!f.Exists)
        //    {
        //        return false;
        //    }
        //    //判断文件是否是指定格式
        //    if (f.Extension != ".xlsx" && f.Extension != ".xls")
        //    {
        //        return false;
        //    }
        //    Workbook workbook = new Workbook();
        //    workbook.LoadFromFile(excelFilePath);
        //    workbook.SaveToFile(pdfFilePath, FileFormat.PDF);
        //    return true;
        //}
    }
}
