using EAM.Model.Dto;
using Infrastructure;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using static EAM.Model.Dto.MaintainReportSheetDto;

namespace EAM.Service.Extensions
{
    //保养报表相关的Excel处理
    public class MaintenanceExcel
    {
        public static void WriteToExcel(string filePath, MaintainReportSheetDto report)
        {
            try
            {
                string reportMark = report.Month > 0 ? "M" : "Y";
                DataTable dt = report.SheetPart[0].SheetTable;

                //处理数据
                foreach (ReportSheetPart sheetPart in report.SheetPart)
                {
                    dt = sheetPart.SheetTable;
                    //移除第一行的保养记录ID
                    if (dt.Rows[0]["itemName"].ToString().ToLower() == "id")
                        dt.Rows.RemoveAt(0);
                    //移除第一列的保养项目Id
                    if (dt.Columns[0].ColumnName.ToLower() == "itemid")
                        dt.Columns.RemoveAt(0);
                }

                // WriteToExcel(filePath, reportMark, report.Equipment.AssetName, report.Equipment.AssetNo, report.Equipment.Model, report.Equipment.CostCenter, report.Year, report.Month, report.SheetPart[0].SheetTable, report.SheetPart[1].SheetTable, report.SheetPart[2].SheetTable, remark);
                FlexibleWriteToExcel(filePath, report);
            }
            catch (Exception ex)
            {
                throw new CustomException("生成保养报表文件失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 保养数据写入到Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="report"></param>
        private static void FlexibleWriteToExcel(string filePath, MaintainReportSheetDto report)
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

            string reportMark = report.Month > 0 ? "M" : "Y";
            int maxColCount = report.Month > 0 ? 33 : 14;
            //创建一个表单
            ISheet sheet = wb.CreateSheet("Sheet0");
            sheet.DisplayGridlines = false;//是否显示网格线
            SetExecelColumnWidth(sheet, maxColCount, reportMark);//设置列宽

            //第一行：标题行
            SetReportTitle(wb, sheet, 0, report.Equipment.AssetName + "定期点检保养表", maxColCount);

            //第二行：信息行
            SetReportInfo(wb, sheet, 1, reportMark, report.Equipment.AssetName, report.Equipment.AssetNo, report.Equipment.Model, report.Equipment.CostCenter, report.Year, report.Month);

            //第三、四行：时间值列
            SetReportDateTitle(wb, sheet, 2, maxColCount);

            //-----------------------保养项目------------------------------
            ICellStyle dateTitleCS = CreateCellStyle_DateTitle(wb);//周期标题样式
            ICellStyle itemNameCS = CreateCellStyle_ItemName(wb);//项目名称样式
            ICellStyle resultMarkCS = CreateCellStyle_ResultMark(wb);//结果标记样式
            ICellStyle signNameCS = CreateCellStyle_SignNameTitle(wb);//‘保养人签名’样式

            int rowStartIndex = 4;
            if (reportMark == "M")
            {
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[0], "日", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[1], "周", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[2], "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
            }
            else
            {
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[0], "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[1], "季", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, report.SheetPart[2], "年", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
            }

            //最后一行备注
            SetReportRemark(wb, sheet, rowStartIndex, maxColCount, report.Tip);

            WriteToFile(wb, filePath);
        }

        /// <summary>
        /// 保养数据写入到Excel
        /// </summary>
        private static void WriteToExcel(string filePath, string reportMark, string assetName, string assetNo, string model, string belong, int? year, int? month, DataTable dt1, DataTable dt2, DataTable dt3, string remark)
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

            //创建一个表单
            ISheet sheet = wb.CreateSheet("Sheet0");
            sheet.DisplayGridlines = false;//是否显示网格线
            int maxColCount = reportMark == "M" ? 33 : 14;
            SetExecelColumnWidth(sheet, maxColCount, reportMark);//设置列宽

            //第一行：标题行
            SetReportTitle(wb, sheet, 0, assetName + "定期点检保养表", maxColCount);

            //第二行：信息行
            SetReportInfo(wb, sheet, 1, reportMark, assetName, assetNo, model, belong, year, month);

            //第三、四行：时间值列
            SetReportDateTitle(wb, sheet, 2, maxColCount);

            //-----------------------保养项目------------------------------
            ICellStyle dateTitleCS = CreateCellStyle_DateTitle(wb);//周期标题样式
            ICellStyle itemNameCS = CreateCellStyle_ItemName(wb);//项目名称样式
            ICellStyle resultMarkCS = CreateCellStyle_ResultMark(wb);//结果标记样式
            ICellStyle signNameCS = CreateCellStyle_SignNameTitle(wb);//‘保养人签名’样式

            int rowStartIndex = 4;
            if (reportMark == "M")
            {
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 1, dt1, "日", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 7, dt2, "周", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 31, dt3, "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
            }
            else
            {
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 1, dt1, "月", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 3, dt2, "季", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
                SetReprotDetail(sheet, ref rowStartIndex, maxColCount, 12, dt3, "年", dateTitleCS, itemNameCS, resultMarkCS, signNameCS);
            }

            //最后一行备注
            SetReportRemark(wb, sheet, rowStartIndex, maxColCount, remark);

            //写入到文件
            WriteToFile(wb, filePath);
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="maxColCount"></param>
        /// <param name="reportMark"></param>
        private static void SetExecelColumnWidth(ISheet sheet, int maxColCount, string reportMark)
        {
            for (int c = 0; c < maxColCount; c++)
            {
                //调整列宽
                if (c == 0)
                {//小标题列
                    sheet.SetColumnWidth(c, (int)(4.5 * 256));
                }
                else if (c == 1)
                {//第二列（项目名称）
                    sheet.SetColumnWidth(c, 22 * 256);
                }
                else
                {//日期
                    if (reportMark == "M")//月表
                        sheet.SetColumnWidth(c, (int)(4.5 * 256));
                    else
                        sheet.SetColumnWidth(c, (int)(11.6 * 256));
                }
            }
        }

        /// <summary>
        /// 设置标题(第1行)
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="title"></param>
        /// <param name="maxColCount"></param>
        private static void SetReportTitle(IWorkbook wb, ISheet sheet, int rowIndex, string title, int maxColCount)
        {
            IRow row = sheet.CreateRow(rowIndex);//创建第1行
            row.Height = 34 * 20;
            ICell cell = row.CreateCell(0);
            cell.CellStyle = CreateCellStyle_Title(wb);//样式
            SetCellValue(cell, title);
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, maxColCount - 1));//合并单元格
        }

        /// <summary>
        /// 设置报表的基本信息（第2行）
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="reportMark"></param>
        /// <param name="assetName"></param>
        /// <param name="assetNo"></param>
        /// <param name="model"></param>
        /// <param name="belong"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private static void SetReportInfo(IWorkbook wb, ISheet sheet, int rowIndex, string reportMark, string assetName, string assetNo, string model, string belong, int? year, int? month)
        {
            ICell cell;
            ICellStyle infoCS = CreateCellStyle_Info(wb);
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 15 * 20;
            if (reportMark == "M")
            {
                //资产名称
                cell = row.CreateCell(0);//A
                cell.CellStyle = infoCS;//样式
                SetCellValue(cell, "设备名称:" + assetName);
                //机型
                cell = row.CreateCell(6);//G
                cell.CellStyle = infoCS;
                SetCellValue(cell, "机型:" + model);
                //资产单位
                cell = row.CreateCell(12);//M
                cell.CellStyle = infoCS;
                SetCellValue(cell, "资产单位:" + belong);
                //资产编号
                cell = row.CreateCell(20);//U
                cell.CellStyle = infoCS;
                SetCellValue(cell, "资产编号:" + assetNo);
                //年月
                cell = row.CreateCell(29);//AD
                cell.CellStyle = infoCS;
                SetCellValue(cell, year + "年    " + month + "月");
            }
            else if (reportMark == "Y")
            {
                //资产名称
                cell = row.CreateCell(0);//A
                cell.CellStyle = infoCS;//样式
                SetCellValue(cell, "设备名称:" + assetName);
                //机型
                cell = row.CreateCell(3);
                cell.CellStyle = infoCS;
                SetCellValue(cell, "机型:" + model);
                //资产单位
                cell = row.CreateCell(5);
                cell.CellStyle = infoCS;
                SetCellValue(cell, "资产单位:" + belong);
                //资产编号
                cell = row.CreateCell(8);
                cell.CellStyle = infoCS;
                SetCellValue(cell, "资产编号:" + assetNo);
                //年月
                cell = row.CreateCell(12);
                cell.CellStyle = infoCS;
                SetCellValue(cell, year + "年      月");
            }
        }

        /// <summary>
        /// 设置报表的日期标题（第3，4行）
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="maxColCount"></param>
        private static void SetReportDateTitle(IWorkbook wb, ISheet sheet, int rowIndex, int maxColCount)
        {
            ICellStyle subTitleCS = CreateCellStyle_SubTitle(wb);
            ICellStyle colTitleCS = CreateCellStyle_DateCol(wb);
            IRow row;
            ICell cell;
            row = sheet.CreateRow(rowIndex);
            row.Height = 18 * 20;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);
                if (c >= 2)
                    cell.CellStyle = colTitleCS;
                else
                    cell.CellStyle = subTitleCS;
            }

            //第三行：标题头
            cell = row.GetCell(0);//创建第c列
            SetCellValue(cell, "                                               日期\r                           保养记录\r保养项目");
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex + 1, 0, 1));
            //画线
            PatriarchLine(sheet, 0, 0, (int)(22 * 6), 18, 0, 2, 1, 2);
            PatriarchLine(sheet, 0, 0, (int)(22 * 3), 18, 0, 2, 1, 3);

            //第三行：日期标题
            for (int c = 2; c < maxColCount; c++)
            {
                cell = row.GetCell(c);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex + 1, c, c));//合并单元格
                SetCellValue(cell, c - 1);//单元格赋值
            }

            //第四行样式
            row = sheet.CreateRow(rowIndex + 1);
            row.Height = 18 * 20;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);
                if (c >= 2)
                    cell.CellStyle = colTitleCS;
                else
                    cell.CellStyle = subTitleCS;
            }
        }

        /// <summary>
        /// 设置报表备注（最后一行）
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <param name="maxColCount"></param>
        /// <param name="remark"></param>
        private static void SetReportRemark(IWorkbook wb, ISheet sheet, int rowIndex, int maxColCount, string remark)
        {
            ICellStyle remarkCS = CreateCellStyle_Remark(wb);
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 25 * 20 + 10;
            sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 0, maxColCount - 1));//合并单元格
            ICell cell;
            for (int c = 0; c < maxColCount; c++)
            {
                cell = row.CreateCell(c);
                cell.CellStyle = remarkCS;
            }
            cell = row.GetCell(0);
            SetCellValue(cell, remark);
        }

        //根据数据类型设置不同类型的cell
        private static void SetCellValue(ICell cell, object obj)
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
        /// 设置报表详情数据
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
        private static void SetReprotDetail(ISheet sheet, ref int rowStartIndex, int columCount, int colSpan, DataTable dt, string timeMark, ICellStyle dateTitleCS, ICellStyle itemNameCS, ICellStyle resultMarkCS, ICellStyle signNameCS)
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

        private static void SetReprotDetail(ISheet sheet, ref int rowStartIndex, int maxColCount, ReportSheetPart reportPart, string timeMark, ICellStyle dateTitleCS, ICellStyle itemNameCS, ICellStyle resultMarkCS, ICellStyle signNameCS)
        {
            IRow row;
            ICell cell;
            //-----------------------保养项目:周期X------------------------------
            DataTable dt = reportPart.SheetTable;

            //创建单元格并设置样式
            //r表示行索引,c表示列索引
            for (int r = rowStartIndex; r < dt.Rows.Count + rowStartIndex; r++)
            {
                row = sheet.CreateRow(r);
                for (int c = 0; c < maxColCount; c++)
                {
                    cell = row.CreateCell(c);
                    if (c == 0)
                    {//周期标题
                        cell.CellStyle = dateTitleCS;
                    }
                    else if (c == 1)
                    {//项目名称
                        if (r == (rowStartIndex + dt.Rows.Count))
                            cell.CellStyle = signNameCS;// "保养人签名"样式
                        else
                            cell.CellStyle = itemNameCS;//测试项目的样式
                    }
                    else if (c >= 2)
                    {//保养结果
                        cell.CellStyle = resultMarkCS;
                    }
                }
            }

            //侧面标题(第一列)
            cell = sheet.GetRow(rowStartIndex).GetCell(0);
            SetCellValue(cell, timeMark + "保养项目");
            sheet.AddMergedRegion(new CellRangeAddress(rowStartIndex, rowStartIndex + dt.Rows.Count - 1, 0, 0));//合并单元格

            //项目名称(第二列)
            for (int r = rowStartIndex; r < dt.Rows.Count + rowStartIndex; r++)
            {
                cell = sheet.GetRow(r).GetCell(1);
                SetCellValue(cell, Convert.ToString(dt.Rows[r - rowStartIndex][0]));
            }

            //保养结果(剩余列)
            int spanCol;
            int excelColumnIndex;
            int excelRowIndex;
            for (int a = 0; a < dt.Rows.Count; a++)
            {
                excelColumnIndex = 2;
                excelRowIndex = a + rowStartIndex;
                for (int b = 1; b < dt.Columns.Count; b++)
                {
                    spanCol = reportPart.PartColumn[b - 1].SpanColumn;
                    cell = sheet.GetRow(excelRowIndex).GetCell(excelColumnIndex);
                    SetCellValue(cell, Convert.ToString(dt.Rows[a][b]));

                    if (spanCol > 1)
                    {
                        sheet.AddMergedRegion(new CellRangeAddress(excelRowIndex, excelRowIndex, excelColumnIndex, excelColumnIndex + spanCol - 1));//合并单元格
                    }
                    excelColumnIndex += spanCol;
                }
            }

            rowStartIndex += dt.Rows.Count;
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
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="filePath"></param>
        private static void WriteToFile(IWorkbook wb, string filePath)
        {
            try
            {
                FileStream fs = File.OpenWrite(filePath);
                wb.Write(fs);//向打开的这个Excel文件中写入表单并保存。
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region 样式

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

        #endregion 样式
    }
}