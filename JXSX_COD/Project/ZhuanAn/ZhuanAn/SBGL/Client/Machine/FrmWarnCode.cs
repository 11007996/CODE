using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmWarnCode : Form
    {
        public FrmWarnCode()
        {
            InitializeComponent();
        }

        public FrmWarnCode(string machineCode)
        {
            InitializeComponent();
            txbMachineCode.Text = machineCode;
        }

        private void FrmWarnCode_Load(object sender, EventArgs e)
        {
            dgvWarnCode.AutoGenerateColumns = false;
            RefreshDataGrid();
            labMessage.Text = "";
        }


        //增加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            string machineCode = txbMachineCode.Text;
            string warnCode = txbWarnCode.Text.Trim();
            string warnDesc = txbWarnDesc.Text.Trim();
            //数据检查
            if (string.IsNullOrWhiteSpace(machineCode))
            {
                labMessage.Text = "设备编码不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(warnCode))
            {
                labMessage.Text = "报警代码不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(warnDesc))
            {
                labMessage.Text = "报警详情不能为空";
                return;
            }

            //检查是否重复添加
            string sqlStr = string.Format("SELECT 1  FROM M_MachineWarnCode_T WHERE  MachineCode='{0}'  AND  WarnCode='{1}'", machineCode, warnCode);
            DataTable dt = DBUtil.GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                labMessage.Text = "报警代码已经存在，请不要重复添加";
                return;
            }
            else
            {
                //添加
                sqlStr = string.Format("INSERT INTO M_MachineWarnCode_T (MachineCode,WarnCode,WarnDesc) VALUES ('{0}','{1}',N'{2}')", machineCode, warnCode, warnDesc);
                DBUtil.ExecSQL(sqlStr);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "添加成功";
                RefreshDataGrid();
            }
        }

        //删除
        private void dgvWarnCode_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            if (dgvWarnCode.SelectedRows.Count <= 0)
            {
                labMessage.Text = "请选择一条数据";
                return;
            }
            string warnCode = dgvWarnCode.SelectedRows[0].Cells["dgcWarnCode"].Value.ToString();
            string machineCode = txbMachineCode.Text;
            if (MessageBox.Show("确定要删除【设备编码:" + machineCode + ", 报警代码:" + warnCode + "】吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_delete = string.Format("DELETE M_MachineWarnCode_T WHERE MachineCode='{0}' AND  WarnCode='{1}'", machineCode, warnCode);
                DBUtil.ExecSQL(sql_delete);

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
                RefreshDataGrid();
            }
        }

        // 刷新
        public void RefreshDataGrid()
        {
            string sqlstr = string.Format("SELECT WarnCode,WarnDesc  FROM M_MachineWarnCode_T WHERE  MachineCode='{0}' ", txbMachineCode.Text);
            dgvWarnCode.DataSource = DBUtil.GetDataTable(sqlstr);
            dgvWarnCode.ClearSelection();
        }

        //导入
        private void tmisImport_Click(object sender, EventArgs e)
        {
            try
            {
                labMessage.Text = "";
                //打开文件选择
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请上传报警信息";
                ofd.Filter = "Excel 文件|*.xls;*.xlsx";
                ofd.ShowDialog();
                string SelectedFilePath = ofd.FileName;
                string SelectedFileName = ofd.SafeFileName;
                if (SelectedFilePath == "" || SelectedFileName == "")
                {
                    return;
                }
                if (!File.Exists(SelectedFilePath))
                {
                    labMessage.Text = "文件不存在！";
                    return;
                }
                //文件数据导入到DataTable数据类型中
                DataTable dt = ExcelUtil.ReadFromExcelFile(SelectedFilePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //判断字段是否合规
                    if (!dt.Columns.Contains("报警代码") || !dt.Columns.Contains("报警详情"))
                    {
                        labMessage.ForeColor = Color.Red;
                        labMessage.Text = "字段名称异常";
                        return;
                    }

                    //机台信息（开启自增插入）
                    string sql = string.Format(@"DELETE M_MachineWarnCode_T WHERE MachineCode='{0}' ;
                                   INSERT INTO M_MachineWarnCode_T(MachineCode,WarnCode,WarnDesc)  VALUES", txbMachineCode.Text);
                    List<string> rowData = new List<string>();

                    //遍历数据
                    foreach (DataRow row in dt.Rows)
                    {
                        string warnCode = row["报警代码"].ToString();
                        string warnDesc = row["报警详情"].ToString();
                        rowData.Add(string.Format("('{0}','{1}',N'{2}')", txbMachineCode.Text, warnCode, warnDesc));
                    }
                    sql += string.Join(",", rowData);
                    int r = DBUtil.ExecSQL(sql);
                    //判断导入结果
                    if (r > 0)
                    {
                        labMessage.ForeColor = Color.Green;
                        labMessage.Text = "导入成功";
                        RefreshDataGrid();
                    }
                    else
                    {
                        labMessage.ForeColor = Color.Red;
                        labMessage.Text = "导入失败";
                    }
                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmWarnCode), null, ex);
            }
        }

        //导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // 列名称强制转换
                for (int c = 0; c < dgvWarnCode.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvWarnCode.Columns[c].Name.ToString());
                    dc.Caption = dgvWarnCode.Columns[c].HeaderText.ToString();
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvWarnCode.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvWarnCode.Columns.Count; c++)
                    {
                        dr[c] = Convert.ToString(dgvWarnCode.Rows[r].Cells[c].EditedFormattedValue);
                    }
                    dt.Rows.Add(dr);
                }
                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "设备报警代码";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmWarnCode), null, ex);
            }
        }

       


    }
}
