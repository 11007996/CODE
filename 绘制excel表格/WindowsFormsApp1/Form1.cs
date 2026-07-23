using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void reoGridControl1_Click(object sender, EventArgs e)
        {

        }
        private void sheetLoad()
        {
            reoGrid.Reset();
            var sheet = reoGrid.CurrentWorksheet;
            sheet["A1"] = "Waterfall";
            sheet["B1"] = "Quantity";
            sheet["C1"] = "Sampling Rate";
            var range = sheet.Ranges[new RangePosition(0, 3, 1, 5)];
            range.Merge();
            //range.Style.VerticalAlign = ReoGridVerAlign.Middle;
            range.Style.HorizontalAlign = ReoGridHorAlign.Center;
            sheet[range.StartPos.ToAddress()] = "Test 1";
            sheet["D2"] = "Test 1 Name";
            sheet["E2"] = "Test 1 ORT Spec";
            sheet["F2"] = "Test 1Check Method";
            sheet["G2"] = "Test1   Schedule";
            sheet["H2"] = "Test Result";
            sheet.SetRangeStyles(new unvell.ReoGrid.RangePosition(0, 0, 2, 8), new unvell.ReoGrid.WorksheetRangeStyle
            {
                Flag = unvell.ReoGrid.PlainStyleFlag.BackAll | PlainStyleFlag.VerticalAlign | PlainStyleFlag.HorizontalAlign | PlainStyleFlag.BackColor | PlainStyleFlag.FillPatternColor,
                HAlign = ReoGridHorAlign.Center,
                VAlign = ReoGridVerAlign.Middle,
                //FillPatternColor = Color.Transparent,
                BackColor = Color.FromArgb(240, 255, 240),

            }) ;
            for(int i=0;i<sheet.UsedRange.Cols;i++)
            {
                sheet.ColumnHeaders[i].FitWidthToCells();
            }
            sheet.SetRangeBorders(new RangePosition(0, 0, 2, 8), BorderPositions.All, new RangeBorderStyle
            {
                Style = BorderLineStyle.Solid,
                Color = Color.Black,
            }
            );
            sheet.FreezeToCell(2, 0, FreezeArea.Top);
            sheet.FreezeToCell(2,0, FreezeArea.Top);
            
            //for(int i =0;i < sheet.UsedRange.Cols;i++)
            //{
            //    sheet.ColumnHeaders[i].IsAutoWidth = true;
            //    sheet.RowHeaders[i].IsAutoHeight = true;
            //}
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            reoGrid.SetSettings(unvell.ReoGrid.WorkbookSettings.View_ShowScrolls, false);
            //FitWidthToCells
            var sheet = reoGrid.CurrentWorksheet;
            
            sheetLoad();
            //sheet.AutoFitColumnWidth(10);
            sheet.SetColumnsWidth(0, 1, 800);

            #region
            /*
            //ReoGrid初始化
            reoGrid.Reset();
            //隐藏横向、纵向滚动条
            reoGrid.SetSettings(unvell.ReoGrid.WorkbookSettings.View_ShowScrolls, false);
            //隐藏横向滚动条
            //rightReoGrid.SetSettings(unvell.ReoGrid.WorkbookSettings.View_ShowHorScroll, false);
            //隐藏纵向滚动条
            //rightReoGrid.SetSettings(unvell.ReoGrid.WorkbookSettings.View_ShowVerScroll, false);
            //隐藏行头(A,B,C...)和列头(1,2,3...)
            reoGrid.CurrentWorksheet.SetSettings(unvell.ReoGrid.WorksheetSettings.View_ShowHeaders, false);
            reoGrid.SheetTabNewButtonVisible = false;  //新建表格按钮不可见
            reoGrid.SheetTabVisible = false;  //工作簿状态样不可见

            //设置控件当前工作页总的行数
            var rightReoGridRowNum = 5;
            //设置控件工作页总的列数
            var rightReoGridColNum = 3;
            //获取控件当前工作页
            var rightWorkSheet = reoGrid.CurrentWorksheet;
            rightWorkSheet.Resize(rightReoGridRowNum, rightReoGridColNum);

            //设定Name列列宽：50
            rightWorkSheet.SetColumnsWidth(0, 1, 50);
            //设定Address列列宽：30
            rightWorkSheet.SetColumnsWidth(1, 2, 30);
            //设定行高：16
            rightWorkSheet.SetRowsHeight(0, rightReoGridRowNum, 16);

            //设置标题行
            rightWorkSheet[0, 0] = "Name";
            rightWorkSheet[0, 1] = "Address1";
            rightWorkSheet[0, 2] = "Address2";

            //填充Name列
            for (int i = 1; i < rightReoGridRowNum; i++)
            {
                rightWorkSheet[i, 0] = "Hello" + i.ToString();
            }
            */
            #endregion
        }

        private void reoGrid_Click(object sender, EventArgs e)
        {
            /*
            var workbook= reoGrid.CreateWorksheet("nwesheets");  //新建工作簿
            workbook.Save(".\tablename",unvell.ReoGrid.IO.FileFormat.Excel2007);  //保存工作簿
            workbook.Load(".\tablename");   //加载EXCEL表
            var sheet = reoGrid.CurrentWorksheet;  //获取当前工作表

            sheet = reoGrid.Worksheets[0];
            sheet = reoGrid.Worksheets["Sheet1"];   //获取指定工作表

            reoGrid.Worksheets.Add(sheet);  //插入工作表
            reoGrid.Worksheets.Insert(1, sheet);
            int index = reoGrid.GetWorksheetIndex("Sheet1");  //获取工作表索引位置

            //单元格样式设置
            sheet.SetRangeStyles(new unvell.ReoGrid.RangePosition(0, 0, sheet.RowCount, 1), new unvell.ReoGrid.WorksheetRangeStyle
            {
                Flag = unvell.ReoGrid.PlainStyleFlag.BackAll,
                BackColor = Color.CornflowerBlue,
            });

            var rowheader = reoGrid.Worksheets[0].RowHeaders[1];   //获取标头
            rowheader.Text = "simple";
            sheet.RowHeaderWidth = 100;  //标头宽度

            sheet.SetRowsHeight(0, 20, 100);  //设置行高
            sheet.RowHeaders[1].IsAutoHeight = true;  //自适应标头高
            sheet.AutoFitRowHeight(0, false);  //自适应行高

            var rowheader1 = sheet.RowHeaders[1];
            rowheader1.IsVisible = false;  //隐藏指定行

            sheet.SetSettings(unvell.ReoGrid.WorksheetSettings.View_ShowRowHeader, false);   //隐藏行标头

            var colheader = sheet.ColumnHeaders[1];  //获取列标头
            colheader.Text = "Numbers";  //列标头名称
            colheader.Width = 100;  //列标头宽
            colheader.IsAutoWidth = true;  //标头自适应宽
            colheader.FitWidthToCells(false);  //列自适应宽

            var cellA1 = new unvell.ReoGrid.CellPosition(0, 0);  //通过坐标创建一个单元格地址
            string AddressName = cellA1.ToAddress(); //获取单元格地址
            cellA1= new unvell.ReoGrid.CellPosition("A5"); //通过单元格地址创建一个单元格地址

            //检查单元格是否有效
            CellPosition.IsValidAddress("D3");
            CellPosition.IsValidAddress("D3:D5");
            CellPosition.IsValidAddress("myRange");

            //单元格数据设置
            sheet["A1"] = 10;
            sheet[0,0] = 10;
            sheet[new CellPosition("A1")] = 10;
            sheet[new CellPosition(0,0)] = 10;

            //批量设置单元格数据
            sheet.DefineNamedRange("mycell",new RangePosition("A1"));
            sheet["mycell"] = 10;
            //批量导入数据
            sheet.SetRangeData(new RangePosition(0,0,1,3),new DataTable());
            sheet.SetRangeData(new RangePosition(0,0, 1,3),new object[] { "a","b"});

            //获取指定范围的单元格数据集合
            var sheet1 = reoGrid.CurrentWorksheet;
            var tt = sheet1.Ranges["B2:C3"];
            var ll = tt.Cells.ToList();
            ll.ForEach (Cell => { string a = Cell.DisplayText; });
            ll = sheet.Ranges[new RangePosition(1, 1, 2, 3)].Cells.ToList();
            ll.ForEach(Cell => { string a = Cell.DisplayText; });

            var sheet2 = reoGrid.Worksheets[0];
            var tt1 = sheet2.Ranges[new RangePosition(1, 1, 3, 4)];
            var st = tt1.StartPos;  //第一个单元格
            var et = tt1.EndPos;  //最后一个单元格
            var rt = tt1.Row;   //从0计数的行数
            var ct = tt1.Column;   //从0计数的列数
            var rts = tt1.Rows;  //表的总行数
            var cts = tt1.Cols;  //表的总列数
            var er = tt1.EndRow;  //最后一行
            var cr = tt1.EndColumn;  //最后一列

            //合并单元格
            tt = sheet.Ranges[new RangePosition(0, 0, 2, 2)];
            tt.Merge();  //合并单元格
            tt.Unmerge();  //取消合并
            sheet.MergeRange("A1:D3");
            sheet.Ranges["A1:D3"].Merge();
            sheet.IsMergedCell(0,2);  //检查合并后的单元格是否为有效单元格，合并后只有左上角那一个单元格是有效的

            var range = sheet.UsedRange;  //获取有效范围
            string ss = range.ToAddress();  //获取范围的地址
            int startrow = sheet.UsedRange.Row; //范围开始的行
            int startcol = sheet.UsedRange.Col;  //范围开始的列
            */
        }
    }
}
