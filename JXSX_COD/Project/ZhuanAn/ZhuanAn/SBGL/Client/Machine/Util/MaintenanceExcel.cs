using Common.Util;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    //保养报表相关的Excel处理
    public class MaintenanceExcel
    {
        /// <summary>
        /// 根据条件生成Excel
        /// </summary>
        /// <param name="filePath">文件生成位置</param>
        /// <param name="dt1">数据</param>
        /// <param name="containColHead">是否包含表头</param>
        /// <param name="autoColumnWidth">是否自动列宽</param>
        public static void WriteToExcel(string filePath, string reprotMark, string assetName, string info, DataTable dt1, DataTable dt2, DataTable dt3, string remark)
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

            int maxColCount = reprotMark == "M" ? 33 : 14;
            //创建一个表单
            ISheet sheet = wb.CreateSheet("Sheet0");
            sheet.DisplayGridlines = false;//是否显示网格线
            ICellStyle titleCS = CreateCellStyle_Title(wb);
            IRow row;
            ICell cell;
            //第一行：标题行
            row = sheet.CreateRow(0);//创建第1行
            row.Height = 34 * 20;
            for (int c = 0; c < maxColCount; c++)
            {
                //创建第c列
                cell = row.CreateCell(c);
                cell.CellStyle = titleCS;//样式
                //调整列宽
                if (c == 0)
                {//第1列
                    sheet.SetColumnWidth(c, (int)(4.5 * 256));
                }
                else if (c == 1)
                {
                    sheet.SetColumnWidth(c, 22 * 256);
                }
                else
                {
                    if (reprotMark == "M")
                        sheet.SetColumnWidth(c, (int)(4.5 * 256));
                    else
                        sheet.SetColumnWidth(c, (int)(11.6 * 256));
                }
            }
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, maxColCount - 1));//合并单元格
            cell = row.Cells[0];
            SetCellValue(cell, assetName + "定期点检保养表");

            //第二行：信息行
            ICellStyle infoCS = CreateCellStyle_Info(wb);
            row = sheet.CreateRow(1);
            row.Height = 15 * 20;
            cell = row.CreateCell(0);
            cell.CellStyle = infoCS;//样式
            SetCellValue(cell, info);

            //第三行：时间值列
            ICellStyle subTitleCS = CreateCellStyle_SubTitle(wb);
            ICellStyle colTitleCS = CreateCellStyle_DateCol(wb);
            row = sheet.CreateRow(2);
            row.Height = 18 * 20;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);//创建第c列
                if (c >= 2)
                {
                    cell.CellStyle = colTitleCS;//样式
                    SetCellValue(cell, c - 1);//单元格赋值
                    sheet.AddMergedRegion(new CellRangeAddress(2, 3, c, c));//合并单元格
                }
                else
                {
                    cell.CellStyle = subTitleCS;//样式
                }
            }

            //第三标题头
            sheet.AddMergedRegion(new CellRangeAddress(2, 3, 0, 1));
            cell = sheet.GetRow(2).Cells[0];
            SetCellValue(cell, "                                               日期\r                           保养记录\r保养项目");

            //第四行
            row = sheet.CreateRow(3);
            row.Height = 18 * 20;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);
                if (c >= 2)
                    cell.CellStyle = colTitleCS;
                else
                    cell.CellStyle = subTitleCS;
            }

            //画线
            PatriarchLine(sheet, 0, 0, (int)(22 * 6), 18, 0, 2, 1, 2);
            PatriarchLine(sheet, 0, 0, (int)(22 * 3), 18, 0, 2, 1, 3);

            //-----------------------保养项目------------------------------
            ICellStyle dateTitleCS = CreateCellStyle_DateTitle(wb);//周期标题样式
            ICellStyle itemNameCS = CreateCellStyle_ItemName(wb);//项目名称样式
            ICellStyle resultMarkCS = CreateCellStyle_ResultMark(wb);//结果标记样式
            ICellStyle signNameCS = CreateCellStyle_SignNameTitle(wb);//‘保养人签名’样式

            int rowStartIndex = 4;
            if (reprotMark == "M")
            {
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 1, dt1, "日", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 7, dt2, "周", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 31, dt3, "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);

            }
            else
            {
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 1, dt1, "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 3, dt2, "季", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                CreateReprotDetail(sheet, ref rowStartIndex, maxColCount, 12, dt3, "年", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
            }

            //最后一行备注
            ICellStyle remarkCS = CreateCellStyle_Remark(wb);
            row = sheet.CreateRow(rowStartIndex);
            row.Height = 25 * 20 + 10;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);//创建第c列
                cell.CellStyle = remarkCS;//样式
            }
            sheet.AddMergedRegion(new CellRangeAddress(rowStartIndex, rowStartIndex, 0, maxColCount - 1));//合并单元格
            cell = row.GetCell(0);
            SetCellValue(cell, remark);

            try
            {
                FileStream fs = File.OpenWrite(filePath);
                wb.Write(fs);//向打开的这个Excel文件中写入表单并保存。  
                fs.Close();
            }
            catch (Exception e)
            {
                LogHelper.Error(typeof(MaintenanceExcel), e.Message.ToString());
            }
        }



        /// <summary>
        /// 创建报表详情数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowStartIndex">起始行索引</param>
        /// <param name="columCount">最大列数量</param>
        /// <param name="colSpan">周期跨单元格数量</param>
        /// <param name="dt">数据表</param>
        /// <param name="timeMark">周期名称</param>
        /// <param name="dateTitleCS">样式:周期标题</param>
        /// <param name="itemNameCS">样式:项目名称</param>
        /// <param name="resultMarkCS">样式:结果数据</param>
        /// <param name="signNameCS">样式:‘保养人签名’</param>
        private static void CreateReprotDetail(ISheet sheet, ref int rowStartIndex, int columCount, int colSpan, DataTable dt, string timeMark, ICellStyle dateTitleCS, ICellStyle itemNameCS, ICellStyle resultMarkCS, ICellStyle signNameCS)
        {
            IRow row;
            ICell cell;
            //-----------------------保养项目:周期X------------------------------
            //r表示行索引,c表示列索引
            for (int r = rowStartIndex; r < dt.Rows.Count + rowStartIndex; r++)
            {
                row = sheet.CreateRow(r);
                for (int c = 0; c < columCount; c++)
                {
                    cell = row.CreateCell(c);
                    if (c == 0)
                    {//周期标题
                        cell.CellStyle = dateTitleCS;
                        if (r == rowStartIndex)
                        {
                            SetCellValue(cell, timeMark + "保养项目");
                            sheet.AddMergedRegion(new CellRangeAddress(rowStartIndex, rowStartIndex + dt.Rows.Count - 1, 0, 0));//合并单元格
                        }
                    }
                    else if (c == 1)
                    {//项目名称
                        SetCellValue(cell, Convert.ToString(dt.Rows[r - rowStartIndex][0]));
                        if ("保养人签名".Equals(Convert.ToString(dt.Rows[r - rowStartIndex][0])))
                            cell.CellStyle = signNameCS;
                        else
                            cell.CellStyle = itemNameCS;
                    }
                    else if (c >= 2)
                    {//保养结果
                        cell.CellStyle = resultMarkCS;
                        //判断是否跨行合并，1表示不跨行
                        if (colSpan == 1)
                        {
                            //如果数据表格有这一列，则用数据填充，否则插入空单元格
                            if (c - 1 < dt.Columns.Count)
                            {
                                SetCellValue(cell, Convert.ToString(dt.Rows[r - rowStartIndex][c - 1]));
                            }
                            else
                            {
                                SetCellValue(cell, "");
                            }
                        }
                        else if (colSpan > 1 && ((c - 2) % colSpan) == 0)
                        {//合并单元格
                            int dtColIndex = (int)((c - 2) / colSpan) + 1;
                            if (dtColIndex < dt.Columns.Count)
                            {
                                SetCellValue(cell, Convert.ToString(dt.Rows[r - rowStartIndex][dtColIndex]));
                            }
                            else
                            {
                                SetCellValue(cell, "");
                            }
                            int colSpanLastIndex = c + colSpan - 1;
                            if (colSpanLastIndex > columCount)
                                colSpanLastIndex = columCount - 1;
                            sheet.AddMergedRegion(new CellRangeAddress(r, r, c, colSpanLastIndex));//合并单元格
                        }

                    }
                }
            }
            rowStartIndex += dt.Rows.Count;
        }

        //周表特殊处理
        private void WeekLayout()
        {


        }


        //单元格样式：主标题
        private static ICellStyle CreateCellStyle_Title(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Center;
            cs.VerticalAlignment = VerticalAlignment.Center;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.IsBold = true;
            font.FontHeightInPoints = 18;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：报表信息
        private static ICellStyle CreateCellStyle_Info(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Left;
            IFont font = wb.CreateFont();
            font.FontName = "楷体";
            font.FontHeightInPoints = 12;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：子标题
        private static ICellStyle CreateCellStyle_SubTitle(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Left;
            cs.VerticalAlignment = VerticalAlignment.Top;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "DFKai-SB";
            font.FontHeightInPoints = 9;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：日期值列
        private static ICellStyle CreateCellStyle_DateCol(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Center;
            cs.VerticalAlignment = VerticalAlignment.Center;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "DFKai-SB";
            font.FontHeightInPoints = 12;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：周期标题
        private static ICellStyle CreateCellStyle_DateTitle(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Center;
            cs.VerticalAlignment = VerticalAlignment.Center;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 14;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：项目名称
        private static ICellStyle CreateCellStyle_ItemName(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.VerticalAlignment = VerticalAlignment.Center;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 9;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：保养人签名
        private static ICellStyle CreateCellStyle_SignNameTitle(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Center;
            cs.VerticalAlignment = VerticalAlignment.Center;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 11;
            cs.SetFont(font);
            return cs;
        }


        //单元格样式：结果标记
        private static ICellStyle CreateCellStyle_ResultMark(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.VerticalAlignment = VerticalAlignment.Center;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "DFKai-SB";
            font.FontHeightInPoints = 14;
            cs.SetFont(font);
            return cs;
        }

        //单元格样式：备注
        private static ICellStyle CreateCellStyle_Remark(IWorkbook wb)
        {
            ICellStyle cs = wb.CreateCellStyle();
            cs.Alignment = HorizontalAlignment.Left;
            cs.VerticalAlignment = VerticalAlignment.Bottom;
            cs.WrapText = true;
            cs.BorderTop = BorderStyle.Hair;
            cs.BorderRight = BorderStyle.Hair;
            cs.BorderBottom = BorderStyle.Hair;
            cs.BorderLeft = BorderStyle.Hair;
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 10;
            cs.SetFont(font);
            return cs;
        }

        //创建样式
        private static ICellStyle CreateCellStyle(IWorkbook wb, HorizontalAlignment ha = HorizontalAlignment.Center, VerticalAlignment va = VerticalAlignment.Center, BorderStyle bs = BorderStyle.None, string fontName = "宋体", bool bold = false, double fontSize = 11, bool wrapText = false, short foreColor = 8, short backColor = 9)
        {
            //----单元格样式------
            ICellStyle cs = wb.CreateCellStyle();
            //水平对齐
            cs.Alignment = ha;
            //垂直对齐
            cs.VerticalAlignment = va;
            //边框
            cs.BorderTop = bs;
            cs.BorderRight = bs;
            cs.BorderBottom = bs;
            cs.BorderLeft = bs;

            //换行
            cs.WrapText = wrapText;
            //前景色,NPOI.HSSF.Util.HSSFColor.?.Index
            cs.FillForegroundColor = foreColor;
            //背景色
            cs.FillBackgroundColor = backColor;
            //填充方式
            //cs.FillPattern = FillPattern.SolidForeground;

            //-----字体样式------
            IFont font = wb.CreateFont();
            //字体名称
            font.FontName = fontName;
            //字体加粗
            font.IsBold = bold;
            //字号
            font.FontHeightInPoints = fontSize;
            cs.SetFont(font);
            return cs;
        }

        //创建线条
        private static void PatriarchLine(ISheet sheet, int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
        {
            //dx1：起始单元格的x偏移量，表示直线起始位置距【开始单元格】左侧的距离；
            //dy1：起始单元格的y偏移量，表示直线起始位置距【开始单元格】上侧的距离；
            //dx2：终止单元格的x偏移量，表示直线起始位置距【结束单元格】左侧的距离；
            //dy2：终止单元格的y偏移量，表示直线起始位置距【结束单元格】上侧的距离；
            //col1：起始单元格列序号，从0开始计算；
            //row1：起始单元格行序号，从0开始计算，
            //col2：终止单元格列序号，从0开始计算；
            //row2：终止单元格行序号，从0开始计算，
            XSSFDrawing linedrawing = (XSSFDrawing)sheet.CreateDrawingPatriarch();
            XSSFClientAnchor lineAnchor = new XSSFClientAnchor(dx1 * XSSFShape.EMU_PER_POINT, dy1 * XSSFShape.EMU_PER_POINT, dx2 * XSSFShape.EMU_PER_POINT, dy2 * XSSFShape.EMU_PER_POINT, col1, row1, col2, row2);
            XSSFSimpleShape lineShape = linedrawing.CreateSimpleShape(lineAnchor);
            lineShape.ShapeType = (int)ShapeTypes.Line;
            lineShape.SetLineStyleColor(0, 0, 0);


            //xls文件，画直线
            //HSSFPatriarch linedrawing = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //HSSFClientAnchor lineAnchor = new HSSFClientAnchor(500, 220, 500, 0, preCol, preRow, picColumn, i + 9);
            //HSSFSimpleShape lineShape = linedrawing.CreateSimpleShape(lineAnchor);
            //lineShape.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
            //lineShape.SetLineStyleColor(0, 0, 0);

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
        public static bool ReadFromExcelFile(string filePath, string reportMark, DataTable dt1, DataTable dt2, DataTable dt3, ref string msg)
        {
            msg = "";
            dt1.Clear();
            dt2.Clear();
            dt3.Clear();
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
                //检查行数与列数
                if (!CheckRowAndColCount(sheet, reportMark, ref msg)) return false;
                IRow row = sheet.GetRow(0);  //读取当前行数据

                //3个表的最后行索引
                List<int> rowIndexs = new List<int>();
                //查找日期标题合并列的最后行索引
                foreach (CellRangeAddress ra in sheet.MergedRegions)
                {
                    if (ra.FirstColumn == 0 && ra.LastColumn == 0 && ra.FirstRow > 3)
                    {
                        rowIndexs.Add(ra.LastRow);
                    }
                }
                if (rowIndexs.Count != 3)
                {
                    msg = "表数量不足3";
                    return false;
                }
                rowIndexs.Sort();//排序

                //读取数据到对应表中
                int maxColIndex = row.Cells.Count - 1;
                if (row.Cells.Count == 33)
                {//月报表
                    ReadDataToDT(sheet, 4, rowIndexs[0], 1, maxColIndex, dt1);
                    ReadDataToDT(sheet, rowIndexs[0] + 1, rowIndexs[1], 7, maxColIndex, dt2);
                    ReadDataToDT(sheet, rowIndexs[1] + 1, rowIndexs[2], 31, maxColIndex, dt3);
                }
                else
                {//年报表
                    ReadDataToDT(sheet, 4, rowIndexs[0], 1, maxColIndex, dt1);
                    ReadDataToDT(sheet, rowIndexs[0] + 1, rowIndexs[1], 3, maxColIndex, dt2);
                    ReadDataToDT(sheet, rowIndexs[1] + 1, rowIndexs[2], 12, maxColIndex, dt3);
                }
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Error(typeof(MaintenanceExcel), e.Message);
                msg = e.Message;
                return false;
            }
            finally
            {
                wk.Close();
            }
        }

        //检查行列数是否符合模版的要求
        private static bool CheckRowAndColCount(ISheet sheet, string reportMark, ref  string msg)
        {
            msg = "";
            if (sheet.PhysicalNumberOfRows < 7)
                msg = "Excel行数不符合模板最小行数";
            IRow row = sheet.GetRow(0);
            if (reportMark == "M" && row.Cells.Count != 33)
            {
                msg = "Excel列数不符合模板指定的33列数";
            }
            else if (reportMark == "Y" && row.Cells.Count != 14)
                msg = "Excel列数不符合模板指定的14列数";
            return msg.Length == 0;
        }

        //
        private static void ReadDataToDT(ISheet sheet, int startRowIndex, int lastRowIndex, int colSpan, int maxColIndx, DataTable dt)
        {
            for (int r = startRowIndex; r <= lastRowIndex; r++)
            {
                DataRow row = dt.NewRow();
                //项目名称
                row[0] = sheet.GetRow(r).Cells[1].ToString();
                for (int c = 2; c <= maxColIndx; c++)
                {
                    if ((c - 2) % colSpan == 0)
                    {
                        int dtColIndex = (c - 2) / colSpan + 1;
                        if (dtColIndex < dt.Columns.Count)
                            row[(c - 2) / colSpan + 1] = sheet.GetRow(r).Cells[c].ToString();
                    }
                }
                //添加行数据到DataTable
                dt.Rows.Add(row);
            }
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
