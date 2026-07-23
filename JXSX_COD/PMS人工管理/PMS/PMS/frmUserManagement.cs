using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace PMS
{
    public partial class frmUserManagement : Form
    {
        int controlStatus =0;
        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            txtUserName.Enabled = false;
            txtUserId.Enabled = false;
            txtOldPassword.Enabled = false;
            txtNewPassword.Enabled = false;
            dataShow();
        }

        //刷新数据
        private void dataShow()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select UserID,UserName,Usey,Intime from tb_User where 1 = 1");
            if (txtUserId.Text.Trim() != "")
            {
                sqlstr.Append("and UserID like '%" + txtUserId.Text.Trim().ToString() + "%'");
            }
            if (txtUserName.Text.Trim() != "")
            {
                sqlstr.Append("and UserName like '%" + txtUserName.Text.Trim().ToString() + "%'");
            }
            //if (txtNewPassword.Text.Trim() != "")
            //{
            //    sqlstr.Append("and PassWord = %" + txtNewPassword.Text.Trim().ToString() + "%");
            //}
            //if(txtOldPassword.Text.Trim() != "")
            //{
            //    sqlstr.Append("and PassWord = %" + txtOldPassword.Text.Trim().ToString() + "%");
            //}
            DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr.ToString());
            DataTable dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            string texthander = "用户编号|用户名|有效|时间";
            string[] hander =texthander.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            for (int i= 0;i <dataGridView1.Columns.Count-1;i++)
            {
                dataGridView1.Columns[i].HeaderText = hander[i];
            }

        }

        private void toolQuery_Click(object sender, EventArgs e)
        {
            controlStatus = 1;
            conStatus();
        }
        private void conStatus()
        {
            switch (controlStatus)
            {
                case 1:
                    txtUserId.Enabled = true;
                    txtUserName.Enabled = true;
                    txtNewPassword.Enabled = false;
                    txtOldPassword.Enabled = false;
                    break;
                case 2:
                    txtUserId.Enabled = true;
                    txtUserName.Enabled = true;
                    txtNewPassword.Enabled = true;
                    txtOldPassword.Enabled = true;
                    label3.Text = "密码1";
                    label4.Text = "密码2";
                    break;
                case 3:
                    txtUserId.Enabled = false;
                    txtUserName.Enabled = false;
                    txtNewPassword.Enabled = true;
                    txtOldPassword.Enabled = true;
                    label3.Text = "密码1";
                    label4.Text = "密码2";
                    break;

            }
           
        }

        private void ToolRefresh_Click(object sender, EventArgs e)
        {
            switch(controlStatus)
            {
                case 1:
                     dataShow();
                    break;
                case 2:
                     if (txtNewPassword.Text.Trim() == string.Empty) 
                {
                    string err = sysInformation.msgshow("m003");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Focus();
                    return;
                }
                if (txtOldPassword.Text.Trim() == string.Empty)
                {
                    string err = sysInformation.msgshow("m004");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOldPassword.Focus();
                    return;
                }
                if (txtUserId.Text.Trim() == string.Empty)
                {
                    string err = sysInformation.msgshow("m005");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserId.Focus();
                    return;
                }
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    string err = sysInformation.msgshow("m006");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }
                if (txtNewPassword.Text.Trim() != txtOldPassword.Text.Trim())
                {
                    string err = sysInformation.msgshow("m007");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPassword.Clear();
                    txtOldPassword.Clear();
                    txtOldPassword.Focus();
                    return;
                }
                string PWord = sysInformation.GetMD5(txtNewPassword.Text.Trim().ToString());
                string sqlstr = "INSERT INTO [dbo].[tb_User]([UserID] ,[UserName] ,[PassWord] ,[Usey] ,[Intime]) VALUES ('" + txtUserId.Text.Trim() + "','" + txtUserName.Text.Trim() + "','" + PWord + "'," +"'Y'" + ",'" + DateTime.Now.ToString() + "')";
                sqlhelpdb.ExecuteNonQuery(sqlstr);
                dataShow();
                txtOldPassword.Clear();
                txtNewPassword.Clear();
                    break;
                case 3:
                     if (txtNewPassword.Text.Trim() == string.Empty)
            {
                string err = sysInformation.msgshow("m003");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }
            if (txtOldPassword.Text.Trim() == string.Empty)
            {
                string err = sysInformation.msgshow("m004");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOldPassword.Focus();
                return;
            }
            if (txtNewPassword.Text.Trim() != txtOldPassword.Text.Trim())
            {
                string err = sysInformation.msgshow("m007");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Clear();
                txtOldPassword.Clear();
                txtOldPassword.Focus();
                return;
            }
            if (sysInformation.sysUserid != txtUserId.Text.Trim().ToString())
            {
                string err = sysInformation.msgshow("m010");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string PassWord = sysInformation.GetMD5(txtNewPassword.Text.Trim().ToString());
            string sqlstr1 = "update tb_User set PassWord='" + PassWord + "' where UserID ='" + txtUserId.Text.Trim().ToString() + "'and UserName='" + txtUserName.Text.Trim().ToString() + "'";
            sqlhelpdb.ExecuteNonQuery(sqlstr1);
            dataShow();
                    break;
            }
        }
   
        private void ToolAdd_Click(object sender, EventArgs e)
        {
            controlStatus = 2;
            conStatus();
        }

        private void ToolCancel_Click(object sender, EventArgs e)
        {
             if (txtUserId.Text.Trim() == string.Empty)
                {
                    string err = sysInformation.msgshow("m005");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserId.Focus();
                    return;
                }
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    string err = sysInformation.msgshow("m006");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserName.Focus();
                    return;
                }
                string sqlstr = "select top 1 1 from tb_User where UserID ='" + txtUserId.Text.Trim().ToString() + "'and  UserName =N'" + txtUserName.Text.Trim().ToString() + "'and Usey = 'N'";
                DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string err = sysInformation.msgshow("m008");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sqlstr1 = "update tb_User set Usey='N' where UserID ='" + txtUserId.Text.Trim().ToString() + "'and  UserName =N'" + txtUserName.Text.Trim().ToString() + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr1);
                    dataShow();
                }
        }

        private void ToolRecover_Click(object sender, EventArgs e)
        {
            string sqlstr = "select top 1 1 from tb_User where UserID ='" + txtUserId.Text.Trim().ToString() + "'and  UserName =N'" + txtUserName.Text.Trim().ToString() + "'and Usey = 'Y'";
            DataSet ds =sqlhelpdb.ExecuteDataset(sqlstr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string err = sysInformation.msgshow("m009");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string sqlstr1 = "update tb_User set Usey='Y' where UserID ='" + txtUserId.Text.Trim().ToString() + "'and  UserName =N'" + txtUserName.Text.Trim().ToString() + "'";
                sqlhelpdb.ExecuteNonQuery(sqlstr1);
                dataShow();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtUserName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void ToolDel_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text.Trim() == string.Empty)
            {
                string err = sysInformation.msgshow("m005");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserId.Focus();
                return;
            }
            if (txtUserName.Text.Trim() == string.Empty)
            {
                string err = sysInformation.msgshow("m006");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            string sqlstr = "delete from tb_User where UserID='" + txtUserId.Text.Trim() + "'and UserName='" + txtUserName.Text.Trim() +" '";
            sqlhelpdb.ExecuteNonQuery(sqlstr);
            dataShow();
        }

        private void ToolMod_Click(object sender, EventArgs e)
        {
            controlStatus = 3;
            conStatus();
           

        }

        private void ToolExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                string err = sysInformation.msgshow("m011");
                MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DataTable dt = (dataGridView1.DataSource as DataTable);
                //通过IO流导出Excel文件
                //ExportExcel(dt);
                //通过NPOI.DLL导出Excel文件
                ExportExcel_B(dt);
            }
        }
        public void ExportExcel_B(DataTable dt)
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
                //string path = HttpContext.Current.Server.MapPath("Export/");

                string path = Application.StartupPath + "\\export";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //设置新建文件路径及名称
                string savePath = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

                //设置新建文件路径及名称
                //string savePath = path + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);

                //创建一个 IO 流
                MemoryStream ms = new MemoryStream();

                //写入到流
                workbook.Write(ms);

                //转换为字节数组
                byte[] bytes = ms.ToArray();

                file.Write(bytes, 0, bytes.Length);
                file.Flush();

                //还可以调用下面的方法，把流输出到浏览器下载
                //OutputClient(bytes);

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
            catch (Exception ex)
            {

            }
        }
        
        public void ExportExcel(DataTable dt)
        {
            //设置导出文件路径
            //string path = HttpContext.Current.Server.MapPath("Export/");
            string path = Application.StartupPath + "\\export";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //设置新建文件路径及名称
            string savePath = path + "\\"+DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xls";

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
        private void ToolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolCan_Click(object sender, EventArgs e)
        {

            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtNewPassword.Enabled = false;
            txtOldPassword.Enabled = false;
            txtUserId.Enabled = false;
            txtUserName.Enabled = false;
            dataShow();
        }



    }
}
