using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using NPOI.XWPF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using System.Text;
using Common.Util;

namespace Machine
{
    //履历报表Word文档相关操作
    public partial class ResumeWord
    {
        #region 根据履历表模板，导出Word文档

        /// <summary>
        /// 根据课程表模板下载Word文档
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void ExportToWordByTemplate(string filePath, Dictionary<string, string> AssetDic, DataTable resumeDT, DataTable repairDT)
        {
            try
            {
                #region 打开文档
                string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "固定资产履历表模板.docx";
                if (!File.Exists(fileName))
                {
                    return;
                }
                XWPFDocument document = null;
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    document = new XWPFDocument(file);
                }
                #endregion

                #region 正文段落
                //替换
                string oldAssetClass = "固定资产种类：□A类检测仪器　□B类生产设备　□C类基础设施";
                foreach (XWPFParagraph paragraph in document.Paragraphs)
                {
                    //判断是否是"固定资产种类"标题
                    if (paragraph.ParagraphText.Contains(oldAssetClass))
                    {
                        paragraph.ReplaceText(oldAssetClass, AssetDic["AssetClassText"]);
                    }
                }
                #endregion

                #region 表格
                int iRow = 0;//表中行的循环索引
                int iCell = 0;//表中列的循环索引
                //1.循环Word文档中的表格
                foreach (XWPFTable table in document.Tables)
                {
                    //2.循环表格行
                    foreach (XWPFTableRow row in table.Rows)
                    {
                        iRow = table.Rows.IndexOf(row);//获取该循环在List集合中的索引

                        //3.循环每行中的列
                        foreach (XWPFTableCell cell in row.GetTableCells())
                        {
                            iCell = row.GetTableCells().IndexOf(cell);//获取该循环在List集合中的索引

                            //4.进行单元格中内容的获取操作
                            //4.1获取单元格中所有的XWPFParagraph(单元格中每行数据都是一个XWPFParagraph对象)
                            //IList<XWPFParagraph> listXWPFParagraph = cell.Paragraphs;

                            //其本信息
                            string cellTxt = cell.GetText();
                            if (cellTxt.Contains("$"))
                            {
                                //替换带有$的关键字
                                cellTxt = cellTxt.Replace("$", "");
                                cell.RemoveParagraph(0);
                                cell.SetText(AssetDic[cellTxt]);
                            }

                            // 保养、校验的记录,行索引[5~20]
                            if (iRow >= 5 && iRow <= 20 && iRow < resumeDT.Rows.Count + 5)
                            {
                                if (iCell > 0)
                                {
                                    cell.RemoveParagraph(0);
                                    cell.SetText(Convert.ToString(resumeDT.Rows[iRow - 5][iCell + 1]));
                                }
                            }

                            //维修记录行索引[22~28]
                            if (iRow >= 22 && iRow <= 28 && iRow < repairDT.Rows.Count + 22)
                            {
                                if (iCell > 0)
                                {
                                    cell.RemoveParagraph(0);
                                    cell.SetText(Convert.ToString(repairDT.Rows[iRow - 22][iCell]));
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 导出文件
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                FileStream fs = File.OpenWrite(filePath);
                document.Write(fs);
                fs.Close();
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(ResumeWord), ex.Message);
                throw ex;
            }
        }
        #endregion



    }
}