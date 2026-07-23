using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class ExcelUtil
    {

        public static void WriteToExcel(string filePath, DataTable data)
        {
            WriteToExcel(filePath, data, false);
        }

        public static void WriteToExcel(string filePath, DataTable data, bool containColHead)
        {
            WriteToExcel(filePath, data, containColHead, false, false);
        }

        public static void WriteToExcel(string filePath, DataTable data, bool containColHead, bool autoColumnWidth)
        {
            WriteToExcel(filePath, data, containColHead, autoColumnWidth, false);
        }

        /// <summary>
        /// 根据条件生成Excel
        /// </summary>
        /// <param name="filePath">文件生成位置</param>
        /// <param name="data">数据</param>
        /// <param name="containColHead">是否包含表头</param>
        /// <param name="autoColumnWidth">是否自动列宽</param>
        /// <param name="autoFilter">是否在第一行开启过滤器</param>
        public static void WriteToExcel(string filePath, DataTable data, bool containColHead, bool autoColumnWidth, bool autoFilter)
        {
            //创建工作薄  
            IWorkbook wb;
            string extension = System.IO.Path.GetExtension(filePath);
            //根据指定的文件格式创建对应的类
            if (extension.Equals(".xls"))
            {
                wb = new HSSFWorkbook();
            }
            else
            {
                wb = new XSSFWorkbook();
            }
            //列名样式
            ICellStyle styleColHead = wb.CreateCellStyle();//样式
            styleColHead.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//文字水平对齐方式
            styleColHead.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//文字垂直对齐方式
            //边框
            styleColHead.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styleColHead.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styleColHead.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            styleColHead.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            styleColHead.WrapText = false;//自动换行
            //设置背景色
            styleColHead.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Aqua.Index;
            styleColHead.FillPattern = FillPattern.SolidForeground;
            styleColHead.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;

            //创建一个表单
            ISheet sheet = wb.CreateSheet("Sheet0");

            IRow row;
            ICell cell;
            //是否插入列头名称
            if (containColHead)
            {
                //列名称,第一行数据
                row = sheet.CreateRow(0);//创建第0行
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    cell = row.CreateCell(c);//创建第c列
                    cell.CellStyle = styleColHead;
                    //根据数据类型设置不同类型的cell
                    SetCellValue(cell, data.Columns[c].Caption);
                    //设置自动调整列宽
                    sheet.AutoSizeColumn(c);
                }
                if (autoFilter)
                {
                    string colEndIndex = ConvertColumnIndexToColumnName(data.Columns.Count - 1);
                    //添加过滤器。
                    CellRangeAddress cr = CellRangeAddress.ValueOf("A1:" + colEndIndex + "1");
                    sheet.SetAutoFilter(cr);
                }
            }

            //行数据
            for (int r = 0; r < data.Rows.Count; r++)
            {
                if (containColHead)
                {
                    row = sheet.CreateRow(r + 1);//创建第r+1行
                }
                else
                {
                    row = sheet.CreateRow(r);//创建第r行
                }
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    cell = row.CreateCell(c);//创建第c列
                    //根据数据类型设置不同类型的cell
                    object obj = data.Rows[r][c];
                    SetCellValue(cell, data.Rows[r][c]);
                    //如果要根据第一行内容自动调整列宽，需要先setCellValue再调用；不要每一行都去设置，否则性能严重下降。
                    if (r == 0 && autoColumnWidth)
                    {
                        if (data.Rows[r][c].ToString().Length <= 20)
                        {
                            sheet.AutoSizeColumn(c);
                        }
                        else
                        {
                            sheet.SetColumnWidth(c, 256 * 20);
                        }
                    }
                }
            }

            try
            {
                FileStream fs = File.OpenWrite(filePath);
                wb.Write(fs);//向打开的这个Excel文件中写入表单并保存。  
                fs.Close();
            }
            catch (Exception e)
            {
                LogHelper.Error(typeof(ExcelUtil), e.Message.ToString());
            }
        }


        //根据数据类型设置不同类型的cell
        public static void SetCellValue(ICell cell, object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                cell.SetCellValue((int)obj);
            }
            else if (obj.GetType() == typeof(double))
            {
                cell.SetCellValue((double)obj);
            }
            else if (obj.GetType() == typeof(IRichTextString))
            {
                cell.SetCellValue((IRichTextString)obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                cell.SetCellValue((DateTime)obj);
            }
            else if (obj.GetType() == typeof(bool))
            {
                cell.SetCellValue((bool)obj);
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }
        }

        /// <summary>
        /// 读取Excel返回datatable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ReadFromExcelFile(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook wk = null;
            string extension = Path.GetExtension(filePath);
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (extension.Equals(".xls"))
                    {
                        //把xls文件中的数据写入wk中
                        wk = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        //把xlsx文件中的数据写入wk中
                        wk = new XSSFWorkbook(fs);
                    }
                    fs.Close();
                }
                //读取当前表数据
                ISheet sheet = wk.GetSheetAt(0);
                IRow row = sheet.GetRow(0);  //读取当前行数据
                for (int i = 0; i <= sheet.LastRowNum; i++)  //LastRowNum 是当前表的总行数-1（注意）
                {
                    row = sheet.GetRow(i);  //读取当前行数据
                    if (row != null)
                    {
                        if (i == 0)
                        {
                            //第一行当作列名称行。
                            for (int j = 0; j < row.LastCellNum; j++)
                            {
                                //读取该行的第j列数据
                                dt.Columns.Add(row.GetCell(j).ToString());
                            }
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            //LastCellNum 是当前行的总列数
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                //读取该行的第j列数据
                                dr[j] = row.GetCell(j) == null ? null : row.GetCell(j).ToString();
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                LogHelper.Error(typeof(ExcelUtil), e.Message);
            }
            finally
            {
                wk.Close();
            }
            return null;
        }


        /// <summary>
        /// 将Excel的列索引转换为列名，列索引从0开始，列名从A开始。如第0列为A，第1列为B...
        /// </summary>
        /// <param name="index">列索引</param>
        /// <returns>列名，如第0列为A，第1列为B...</returns>
        public static string ConvertColumnIndexToColumnName(int index)
        {
            index = index + 1;
            int system = 26;
            char[] digArray = new char[100];
            int i = 0;
            while (index > 0)
            {
                int mod = index % system;
                if (mod == 0) mod = system;
                digArray[i++] = (char)(mod - 1 + 'A');
                index = (index - 1) / 26;
            }
            StringBuilder sb = new StringBuilder(i);
            for (int j = i - 1; j >= 0; j--)
            {
                sb.Append(digArray[j]);
            }
            return sb.ToString();
        }
    }
}
