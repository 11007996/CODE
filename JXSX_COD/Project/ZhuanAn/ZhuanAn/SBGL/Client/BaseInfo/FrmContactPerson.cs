using Common.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class FrmContactPerson : Form
    {
        public FrmContactPerson()
        {
            InitializeComponent();
        }

        private void FrmContactPerson_Load(object sender, EventArgs e)
        {
            dgvContactPerson.AutoGenerateColumns = false;
            RefreshContactPerson();
        }


        private void RefreshContactPerson()
        {
            string sql = "SELECT WorkCode,RealName FROM S_ContactPerson_T";
            dgvContactPerson.DataSource = DBUtil.GetDataTable(sql);
        }



        //添加联系人
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbWorkCode.Text))
            {
                MessageBox.Show("工号不能为空", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbRealName.Text))
            {
                MessageBox.Show("姓名不能为空", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sql = string.Format("SELECT 1 FROM S_ContactPerson_T WHERE WorkCode='{0}'", tbWorkCode.Text.Trim());
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show("已存在此工号", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sql = string.Format("INSERT INTO S_ContactPerson_T(WorkCode,RealName) VALUES('{0}',N'{1}')", tbWorkCode.Text.Trim(), tbRealName.Text.Trim());
            DBUtil.ExecSQL(sql);
            RefreshContactPerson();
            MessageBox.Show("添加成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }



        //删除
        private void dgvContactPerson_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            if (e.Row.Index < 0)
            {
                MessageBox.Show("请选中要删除的数据", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string workCode = e.Row.Cells["dgcWorkCode"].Value.ToString();
            string realName = e.Row.Cells["dgcRealName"].Value.ToString();
            if (MessageBox.Show("确定要删除【" + workCode + ":" + realName + "】吗?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                string sql = string.Format("DELETE  S_ContactPerson_T WHERE WorkCode = '{0}';", workCode);
                DBUtil.ExecSQL(sql);
                RefreshContactPerson();
                MessageBox.Show("删除成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }


    }
}
