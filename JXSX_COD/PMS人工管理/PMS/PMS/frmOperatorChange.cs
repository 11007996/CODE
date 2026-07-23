using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS
{
    public partial class frmOperatorChange : Form
    {
        public frmOperatorChange()
        {
            InitializeComponent();
        }

        private void frmOperatorChange_Load(object sender, EventArgs e)
        {
            string sqlstr1 = "select UserID,UserName,Usey from tb_User where Usey <> 'G'";
            using(DataSet ds1 = sqlhelpdb.ExecuteDataset(sqlstr1))
            {
                DataTable dt1 = ds1.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt1;
                }
            }
            string sqlstr2 = "select UserID,UserName from tb_User where Usey = 'G'";
            using (DataSet ds2 = sqlhelpdb.ExecuteDataset(sqlstr2))
            {
                DataTable dt2 = ds2.Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt2;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                label1.Text = "请选择一个用户名";
            }
            else
            {
                DialogResult YOrN = MessageBox.Show("确认是否改为操作员", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (YOrN == DialogResult.OK)
                {
                    string userid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string sqlstr = "update tb_User set Usey='G' where UserID='" + userid + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr);
                    frmOperatorChange_Load(this, null);
                    dataGridView1.ClearSelection();
                    label1.Text = "修改成功";
                }
                else
                {
                    dataGridView1.ClearSelection();
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count < 1)
            {
                label1.Text = "请选择一个用户名";
            }
            else
            {
                DialogResult YOrN = MessageBox.Show("确认取消此操作员", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (YOrN == DialogResult.OK)
                {
                    string userid = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                    string sqlstr = "update tb_User set Usey='Y' where UserID='" + userid + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr);
                    frmOperatorChange_Load(this, null);
                    dataGridView2.ClearSelection();
                    label1.Text = "修改成功";
                }
                else
                {
                    dataGridView2.ClearSelection();
                }
            }
        }
    }
}
