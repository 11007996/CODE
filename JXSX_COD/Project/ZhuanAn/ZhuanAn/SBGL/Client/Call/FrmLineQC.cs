using Common;
using Common.Util;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Call
{
    public partial class FrmLineQC : Form
    {
        //编辑前的数据
        public FrmLineQC()
        {
            InitializeComponent();
        }

        private void FrmLineQC_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            RefreshDataGrid();
            string strSql = "SELECT Line FROM S_LineInfo_T";
            DataTable lineDT = DBUtil.GetDataTable(strSql);
            DataRow row0 = lineDT.NewRow();
            lineDT.Rows.InsertAt(row0, 0);
            cmbLine.DataSource = lineDT;
            cmbLine.ValueMember = "Line";

            strSql = "SELECT WorkCode,RealName FROM S_ContactPerson_T";
            DataTable qcDT = DBUtil.GetDataTable(strSql);
            DataRow row = qcDT.NewRow();
            qcDT.Rows.InsertAt(row, 0);
            cmbQC.DataSource = qcDT;
            cmbQC.ValueMember = "WorkCode";
            cmbQC.DisplayMember = "RealName";
        }

        private void RefreshDataGrid()
        {
            dgvLineQC.DataSource = DBUtil.GetDataTable("SELECT * FROM M_LineQC_T Order By Line ASC");
        }


        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            //数据检查
            if (cmbQC.SelectedValue == null)
            {
                labMessage.Text = "品管未选择";
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbLine.Text))
            {
                labMessage.Text = "线别不能为空";
                return;
            }
            try
            {
                string sql = string.Format("SELECT 1 FROM M_LineQC_T WHERE  Line=N'{0}'", cmbLine.Text.Trim());
                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    labMessage.Text = "该线已设置品管";
                    return;
                }
                //新增
                sql = string.Format("INSERT INTO M_LineQC_T(Line,QCName,QCWorkCode) VALUES(N'{0}',N'{1}','{2}')", cmbLine.Text.Trim(), cmbQC.Text.Trim(), cmbQC.SelectedValue);
                DBUtil.ExecSQL(sql);
                RefreshDataGrid();

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "【提示】操作成功";
            }
            catch (Exception)
            {
                labMessage.Text = "【警告】操作失败";
            }
        }

        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {


        }

        private void dgvLineQC_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            labMessage.Text = "";
            if (dgvLineQC.SelectedRows.Count != 1)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "请选中一行数据进行删除。";
                return;
            }
            string line = e.Row.Cells["dgcLine"].Value.ToString();
            if (MessageBox.Show("确定要删除" + line + "吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sqlEdit = string.Format("DELETE M_LineQC_T  WHERE  Line=N'{0}'", line);
                int execCount = DBUtil.ExecSQL(sqlEdit);
                //显示操作结果
                if (execCount > 0)
                {
                    RefreshDataGrid();
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "【提示】删除成功";
                }
                else
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "【警告】删除失败";
                }
            }
        }
    }
}
