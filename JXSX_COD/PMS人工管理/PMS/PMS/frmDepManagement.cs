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
    public partial class frmDepManagement : Form
    {
        int ConStatus;
        
        public frmDepManagement()
        {
            InitializeComponent();
            this.toolAdd.Enabled = true;
            this.toolDel.Enabled = true;
            this.toolmod.Enabled = true;
            this.ToolQuery.Enabled = true;
            this.toolSave.Enabled = true;
        }
        
        private void ToolQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select *  from tb_department where 1 = 1");
            if(txtDepID.Text != "")
            {
                sqlstr.Append("and DepID ='"+ txtDepID.Text.Trim() + "'");
            }
            if (txtDepID.Text != "")
            {
                sqlstr.Append("and DepName ='" + txtDepName.Text.Trim() + "'");
            }
            if (txtDepMan.Text != "")
            {
                sqlstr.Append("and DepMan ='" + txtDepMan.Text.Trim() + "'");
            }
            string sqlstr1 = sqlstr.ToString();
            using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr1))
            {
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
            }
            contorlStatus(1);
        }
        private void load()
        {
            string sqlstr = "select *  from tb_department";
            using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string depid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string depname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string depman = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtDepID.Text = depid;
            txtDepName.Text = depname;
            txtDepMan.Text = depman;
        }

        private void toolmod_Click(object sender, EventArgs e)
        {
            if (dataCheck())
            {
                return;
            }
            DialogResult dr = MessageBox.Show("确认修改部门信息", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                 string depid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                 string depname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                 string depman = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                 string sqlstr = "update tb_department set depID='" + txtDepID.Text.Trim() + "',depName='" + txtDepName.Text.Trim() + "',depMan='" + txtDepMan.Text.Trim() + "' where 1=1 and depID='" + depid + "' or depName='" + depname + "' or depMan='" + depman + "'";
                 sqlhelpdb.ExecuteNonQuery(sqlstr);
                 label4.Text = "修改成功";
                 load();
                 contorlStatus(2);
            }
        }
        private bool dataCheck()
        {
            if(txtDepID.Text == "")
            {
                label4.Text = "部门编号不能为空";
                return false;
            }
            else if (txtDepName.Text == "")
            {
                label4.Text = "部门名称不能为空";
                return false;
            }
            else if (txtDepMan.Text =="")
            {
                label4.Text = "部门负责人不能为空";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void toolDel_Click(object sender, EventArgs e)
        {
            if (!dataCheck())
            {
                return;
            }
            DialogResult dr = MessageBox.Show("确认删除部门信息", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    string depid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string sqlstr = "delete from tb_department where depID='" + depid + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                load();
                contorlStatus(3);
            }
        }

        private void toolAdd_Click(object sender, EventArgs e)
        {
            string sqlstr = "select isnull(max(depID),0) from tb_department";
            using(DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
                string depid = dt.Rows[0][0].ToString();
                int newdepid = int.Parse(depid.Substring(1)) + 1 + 100;
                depid = "d" + newdepid.ToString().Substring(1);
                txtDepID.Text = depid;
            }
            ConStatus = 1;
            contorlStatus(4);
            txtDepID.Enabled = false;
            txtDepName.Text = "";
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            switch (ConStatus)
            { 
                case 1:
            if (!dataCheck())
            {
                return;
            }
            try
            {
                string sqlstr1 = "select top 1 1 from tb_department where depName=N'" + txtDepName.Text.Trim() + "'";
                using(DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr1))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count >0)
                    {
                        label4.Text = "此部门已存在";
                        return;
                    }
                }
                string sqlstr = "insert into tb_department values('" + txtDepID.Text.Trim() + "','"+ txtDepName.Text.Trim() + "','" + txtDepMan.Text.Trim() +"')";
                sqlhelpdb.ExecuteNonQuery(sqlstr);
                contorlStatus(5);
                label4.Text = "新增成功";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            break;
        }
            load();
        }
        
        private void chkMan_Click(object sender, EventArgs e)
        {
            chkMan.Checked = false;
            frmSelDepartMan frm = new frmSelDepartMan();
            frm.WinPar += new backParameter(winpar);
            frm.ShowDialog();


        }
        private void winpar(string v)
        {
            txtDepMan.Text = v;
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            txtDepID.Text = "";
            txtDepMan.Text = "";
            txtDepName.Text = "";
            contorlStatus(6);
        }
        private void contorlStatus(int status)
        {
            switch(status)
            {
                case 1:          //查询
                    this.toolAdd.Enabled = false;
                    this.toolCancel.Enabled = true;
                    this.toolDel.Enabled = false;
                    this.toolmod.Enabled = false;
                    this.toolSave.Enabled = false;
                    break;
                case 2:          //修改
                    this.toolAdd.Enabled = false;
                    this.toolCancel.Enabled = true;
                    this.toolDel.Enabled = false;
                    this.toolmod.Enabled = true;
                    this.toolSave.Enabled = false;
                    break;
                case 3:          //删除
                     this.toolAdd.Enabled = false;
                    this.toolCancel.Enabled = true;
                    this.toolDel.Enabled = true;
                    this.toolmod.Enabled = false;
                    this.toolSave.Enabled = true;
                    break;
                case 4:          //增加
                    this.toolAdd.Enabled = true;
                    this.toolCancel.Enabled = true;
                    this.toolDel.Enabled = false;
                    this.toolmod.Enabled = false;
                    this.toolSave.Enabled = true;
                    break;
                case 5:          //保存
                    this.toolAdd.Enabled = true;
                    this.toolCancel.Enabled = true;
                    this.toolDel.Enabled = true;
                    this.toolmod.Enabled = true;
                    this.toolSave.Enabled = true;
                    break;
                case 6:          //取消
                    this.toolAdd.Enabled = true;
                    this.toolDel.Enabled = true;
                    this.toolmod.Enabled = true;
                    this.ToolQuery.Enabled = true;
                    this.toolSave.Enabled = true;
                    break;
            }
        }

        private void frmDepManagement_Load(object sender, EventArgs e)
        {
            string sqlstr = "select *  from tb_department";
            using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
            }
        }
    }
}
