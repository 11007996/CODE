C#实现写入Excel表

using System;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Excel;
  
namespace Test
{
    class Program
    {
        public static bool WriteXls(string filename)
        {
            //启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            _Workbook book = xls.Workbooks.Add(Missing.Value); //创建一张表，一张表可以包含多个sheet
  
            //如果表已经存在，可以用下面的命令打开
            //_Workbook book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
  
            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示
  
            for (int i = 1; i < 4; i++)//循环创建并写入数据到sheet
            {
                try
                {
                    sheet = (_Worksheet)book.Worksheets.get_Item(i);//获得第i个sheet，准备写入
                }
                catch (Exception ex)//不存在就增加一个sheet
                {
                    sheet = (_Worksheet)book.Worksheets.Add(Missing.Value, book.Worksheets[book.Sheets.Count], 1, Missing.Value);
                }
                sheet.Name = "第" + i.ToString() + "页";//设置当前sheet的Name
                for (int row = 1; row < 20; row++)//循环设置每个单元格的值
                {
                    for (int offset = 1; offset < 10; offset++)
                        sheet.Cells[row, offset] = "( " + row.ToString() + "," + offset.ToString() + " )";
                }
            }
            //将表另存为
            book.SaveAs(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
  
            //如果表已经存在，直接用下面的命令保存即可
            //book.Save();
  
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
            //sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return true;
        }
        static void Main(string[] args)
        {
            string Current;
            Current = Directory.GetCurrentDirectory();//获取当前根目录
            WriteXls(Current + "\\test.xls");
        }
    }
}



C#实现读取Excel表

using System;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Excel;
  
namespace Test
{
    class Program
    {
        public static Array ReadXls(string filename,int index)//读取第index个sheet的数据
        {
            // 启动Excel应用程序
            Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
            // 打开filename表
            _Workbook book = xls.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
  
            _Worksheet sheet;//定义sheet变量
            xls.Visible = false;//设置Excel后台运行
            xls.DisplayAlerts = false;//设置不显示确认修改提示
  
            try
            {
                sheet = (_Worksheet)book.Worksheets.get_Item(index);//获得第index个sheet，准备读取
            }
            catch (Exception ex)//不存在就退出
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            Console.WriteLine(sheet.Name);
            int row = sheet.UsedRange.Rows.Count;//获取不为空的行数
            int col = sheet.UsedRange.Columns.Count;//获取不为空的列数
            Array value = (Array)sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[row, col]).Cells.Value2;//获得区域数据赋值给Array数组，方便读取
  
            book.Save();//保存
            book.Close(false, Missing.Value, Missing.Value);//关闭打开的表
            xls.Quit();//Excel程序退出
            // sheet,book,xls设置为null，防止内存泄露
            sheet = null;
            book = null;
            xls = null;
            GC.Collect();//系统回收资源
            return value;
        }
  
        static void Main(string[] args)
        {
            string Current;
            Current = Directory.GetCurrentDirectory();//获取当前根目录
            Array Data = ReadXls(Current + "\\test.xls", 1);//读取test.xlsx的第一个sheet表
            foreach (string temp in Data)
                Console.WriteLine(temp);
            Console.ReadKey();
        }
    }
}


Excel表的一些特性操作

range.NumberFormatLocal = "@";     //设置单元格格式为文本
range = (Range)worksheet.get_Range("A1", "E1");     //获取Excel多个单元格区域
range.Merge(Type.Missing);     //单元格合并动作
worksheet.Cells[1, 1] = "Excel单元格赋值";     //Excel单元格赋值
range.Font.Size = 15;     //设置字体大小
range.Font.Underline=true;     //设置字体是否有下划线
range.Font.Name="黑体";     设置字体的种类
range.HorizontalAlignment=XlHAlign.xlHAlignCenter;     //设置字体在单元格内的对其方式
range.ColumnWidth=15;     //设置单元格的宽度
range.Cells.Interior.Color=System.Drawing.Color.FromArgb(255,204,153).ToArgb();     //设置单元格的背景色
range.Borders.LineStyle=1;     //设置单元格边框的粗细
range.BorderAround(XlLineStyle.xlContinuous,XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic,System.Drawing.Color.Black.ToArgb());     //给单元格加边框
range.EntireColumn.AutoFit();     //自动调整列宽
range.HorizontalAlignment= xlCenter;     // 文本水平居中方式
range.VerticalAlignment= xlCenter     //文本垂直居中方式
range.WrapText=true;     //文本自动换行
range.Interior.ColorIndex=39;     //填充颜色为淡紫色
range.Font.Color=clBlue;     //字体颜色
xlsApp.DisplayAlerts=false;     //保存Excel的时候，不弹出是否保存的窗口直接进行保存
workbook.SaveCopyAs(temp);     //填入完信息之后另存到路径及文件名字