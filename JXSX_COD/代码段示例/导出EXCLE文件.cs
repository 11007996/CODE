/*引用Microsoft.Office.Interop.Excel.dll
public void ExportExcel(DataTable dt)
        {
            if (dt != null)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                if (excel == null)
                {
                    return;
                }

                //设置为不可见，操作在后台执行，为 true 的话会打开 Excel
                excel.Visible = false;

                //打开时设置为全屏显式
                //excel.DisplayFullScreen = true;

                //初始化工作簿
                Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;

                //新增加一个工作簿，Add（）方法也可以直接传入参数 true
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                //同样是新增一个工作簿，但是会弹出保存对话框
                //Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

                //新增加一个 Excel 表(sheet)
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                //设置表的名称
                worksheet.Name = dt.TableName;
                try
                {
                    //创建一个单元格
                    Microsoft.Office.Interop.Excel.Range range;

                    int rowIndex = 1;       //行的起始下标为 1
                    int colIndex = 1;       //列的起始下标为 1

                    //设置列名
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //设置第一行，即列名
                        worksheet.Cells[rowIndex, colIndex + i] = dt.Columns[i].ColumnName;

                        //获取第一行的每个单元格
                        range = worksheet.Cells[rowIndex, colIndex + i];

                        //设置单元格的内部颜色
                        range.Interior.ColorIndex = 33;

                        //字体加粗
                        range.Font.Bold = true;

                        //设置为黑色
                        range.Font.Color = 0;

                        //设置为宋体
                        range.Font.Name = "Arial";

                        //设置字体大小
                        range.Font.Size = 12;

                        //水平居中
                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                        //垂直居中
                        range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }

                    //跳过第一行，第一行写入了列名
                    rowIndex++;

                    //写入数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[rowIndex + i, colIndex + j] = dt.Rows[i][j].ToString();
                        }
                    }

                    //设置所有列宽为自动列宽
                    //worksheet.Columns.AutoFit();

                    //设置所有单元格列宽为自动列宽
                    worksheet.Cells.Columns.AutoFit();
                    //worksheet.Cells.EntireColumn.AutoFit();

                    //是否提示，如果想删除某个sheet页，首先要将此项设为fasle。
                    excel.DisplayAlerts = false;

                    //保存写入的数据，这里还没有保存到磁盘
                    workbook.Saved = true;

                    //设置导出文件路径
                    string path = HttpContext.Current.Server.MapPath("Export/");

                    //设置新建文件路径及名称
                    string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";

                    //创建文件
                    FileStream file = new FileStream(savePath, FileMode.CreateNew);

                    //关闭释放流，不然没办法写入数据
                    file.Close();
                    file.Dispose();

                    //保存到指定的路径
                    workbook.SaveCopyAs(savePath);

                    //还可以加入以下方法输出到浏览器下载
                    FileInfo fileInfo = new FileInfo(savePath);
                    OutputClient(fileInfo);
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    workbook.Close(false, Type.Missing, Type.Missing);
                    workbooks.Close();

                    //关闭退出
                    excel.Quit();

                    //释放 COM 对象
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(excel);

                    worksheet = null;
                    workbook = null;
                    workbooks = null;
                    excel = null;

                    GC.Collect();
                }
            }
        }
		
public void OutputClient(FileInfo file)
        {
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            //导出到 .xlsx 格式不能用时，可以试试这个
            //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd-HH-mm")));

            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");

            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());

            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.Close();
        }
*/





/*
使用 Aspose.Cells.dll
public void ExportExcel(DataTable dt)
        {
            try
            {
                //获取指定虚拟路径的物理路径
                string path = HttpContext.Current.Server.MapPath("DLL/") + "License.lic";

                //读取 License 文件
                Stream stream = (Stream)File.OpenRead(path);

                //注册 License
                Aspose.Cells.License li = new Aspose.Cells.License();
                li.SetLicense(stream);

                //创建一个工作簿
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();

                //创建一个 sheet 表
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

                //设置 sheet 表名称
                worksheet.Name = dt.TableName;

                Aspose.Cells.Cell cell;

                int rowIndex = 0;   //行的起始下标为 0
                int colIndex = 0;   //列的起始下标为 0

                //设置列名
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //获取第一行的每个单元格
                    cell = worksheet.Cells[rowIndex, colIndex + i];

                    //设置列名
                    cell.PutValue(dt.Columns[i].ColumnName);

                    //设置字体
                    cell.Style.Font.Name = "Arial";

                    //设置字体加粗
                    cell.Style.Font.IsBold = true;

                    //设置字体大小
                    cell.Style.Font.Size = 12;

                    //设置字体颜色
                    cell.Style.Font.Color = System.Drawing.Color.Black;

                    //设置背景色
                    cell.Style.BackgroundColor = System.Drawing.Color.LightGreen;
                }

                //跳过第一行，第一行写入了列名
                rowIndex++;

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cell = worksheet.Cells[rowIndex + i, colIndex + j];

                        cell.PutValue(dt.Rows[i][j]);
                    }
                }

                //自动列宽
                worksheet.AutoFitColumns();

                //设置导出文件路径
                path = HttpContext.Current.Server.MapPath("Export/");

                //设置新建文件路径及名称
                string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xlsx";

                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew);

                //关闭释放流，不然没办法写入数据
                file.Close();
                file.Dispose();

                //保存至指定路径
                workbook.Save(savePath);


                //或者使用下面的方法，输出到浏览器下载。
                //byte[] bytes = workbook.SaveToStream().ToArray();
                //OutputClient(bytes);

                worksheet = null;
                workbook = null;
            }
            catch(Exception ex)
            {

            }
        }
		
public void OutputClient(byte[] bytes)
        {
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm")));

            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");

            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.Close();
        }
*/





/*直接使用 GridView 把 DataTable 的数据转换为字符串流，然后输出到浏览器下载
public void ExportExcel(DataTable dt)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Buffer = true;

            response.Clear();
            response.ClearHeaders();
            response.ClearContent();

            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));

            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");

            //实例化一个流
            StringWriter stringWrite = new StringWriter();

            //指定文本输出到流
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //设置每个单元格的格式
                    gv.Rows[i].Cells[j].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }

            //把 GridView 的内容输出到 HtmlTextWriter
            gv.RenderControl(htmlWrite);
            
            response.Write(stringWrite.ToString());
            response.Flush();

            response.Close();
        }
*/


/*直接使用 DataGrid
public void ExportExcel(DataTable dt)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Buffer = true;

            response.Clear();
            response.ClearHeaders();
            response.ClearContent();

            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));

            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");

            //实例化一个流
            StringWriter stringWrite = new StringWriter();

            //指定文本输出到流
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            DataGrid dg = new DataGrid();
            dg.DataSource = dt;
            dg.DataBind();

            dg.Attributes.Add("style", "vnd.ms-excel.numberformat:@");

            //把 DataGrid 的内容输出到 HtmlTextWriter
            dg.RenderControl(htmlWrite);

            response.Write(stringWrite.ToString());
            response.Flush();

            response.Close();
        }
*/


/*直接使用IO流
public void ExportExcel(DataTable dt)
        {
            //设置导出文件路径
            string path = HttpContext.Current.Server.MapPath("Export/");

            //设置新建文件路径及名称
            string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

            //创建文件
            FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
            
            //以指定的字符编码向指定的流写入字符
            StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding("GB2312"));

            StringBuilder strbu = new StringBuilder();

            //写入标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strbu.Append(dt.Columns[i].ColumnName.ToString() + "\t");
            }
            //加入换行字符串
            strbu.Append(Environment.NewLine);

            //写入内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strbu.Append(dt.Rows[i][j].ToString() + "\t");
                }
                strbu.Append(Environment.NewLine);
            }

            sw.Write(strbu.ToString());
            sw.Flush();
            file.Flush();
            
            sw.Close();
            sw.Dispose();

            file.Close();
            file.Dispose();
        }
*/


/*使用NPIO导出
public void ExportExcel(DataTable dt)
        {
            try
            {
                //创建一个工作簿
                IWorkbook workbook = new HSSFWorkbook();

                //创建一个 sheet 表
                ISheet sheet = workbook.CreateSheet(dt.TableName);

                //创建一行
                IRow rowH = sheet.CreateRow(0);

                //创建一个单元格
                ICell cell = null;

                //创建单元格样式
                ICellStyle cellStyle = workbook.CreateCellStyle();

                //创建格式
                IDataFormat dataFormat = workbook.CreateDataFormat();

                //设置为文本格式，也可以为 text，即 dataFormat.GetFormat("text");
                cellStyle.DataFormat = dataFormat.GetFormat("@");

                //设置列名
                foreach (DataColumn col in dt.Columns)
                {
                    //创建单元格并设置单元格内容
                    rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);

                    //设置单元格格式
                    rowH.Cells[col.Ordinal].CellStyle = cellStyle;
                }

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //跳过第一行，第一行为列名
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                        cell.CellStyle = cellStyle;
                    }
                }

                //设置导出服务器文件路径
                string path = HttpContext.Current.Server.MapPath("Export/");

                //设置新建文件路径及名称
                string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew,FileAccess.Write);

                //创建一个 IO 流
                MemoryStream ms = new MemoryStream();

                //写入到流
                workbook.Write(ms);

                //转换为字节数组
                byte[] bytes = ms.ToArray();

                file.Write(bytes, 0, bytes.Length);
                file.Flush();

                //还可以调用下面的方法，把流输出到浏览器下载
                OutputClient(bytes);

                //释放资源
                bytes = null;

                ms.Close();
                ms.Dispose();

                file.Close();
                file.Dispose();

                workbook.Close();
                sheet = null;
                workbook = null;
            }
            catch(Exception ex)
            {

            }
        }
		
public void OutputClient(byte[] bytes)
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Buffer = true;

            response.Clear();
            response.ClearHeaders();
            response.ClearContent();

            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));

            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");

            response.BinaryWrite(bytes);
            response.Flush();

            response.Close();
        }
*/