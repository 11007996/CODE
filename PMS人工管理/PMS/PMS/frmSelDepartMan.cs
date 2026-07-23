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
    public delegate void backParameter(string value);
    public partial class frmSelDepartMan : Form
    {
        public frmSelDepartMan()
        {
            InitializeComponent();
        }
        private void frmSelDepartMan_Load(object sender, EventArgs e)
        {
            string sqlstr = "select UserID,UserName from tb_User where Usey <> 'N'";
            using(DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                string hendertext = "员工编号|员工姓名";
                string[] hender = hendertext.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderText = hender[i];
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select UserID,UserName from tb_User where Usey <> 'N'");
               if(txtID.Text.Trim() != "")
               {
                   sqlstr.Append(" and UserID='" + txtID.Text.Trim() + "'");
               }
            if (txtName.Text.Trim() != "")
            {
                sqlstr.Append(" and UserName=N'" + txtName.Text.Trim() + "'");
            }
             string sqlsel = sqlstr.ToString();
            //string sqlstr = "select UserName,Usey from tb_User where Usey <> 'N' and UserID='" + txtID.Text.Trim() + "'";
             using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlsel))
            {
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                //string hendertext = "员工编号|员工姓名";
                //string[] hender = hendertext.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                // for (int i = 0; i <dt.Rows.Count; i++)
                // {
                     
                //     dataGridView1.Columns[i].HeaderText = hender[i];
                // }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        public event backParameter WinPar;
        private void btnOk_Click(object sender, EventArgs e)
        {
            //if (!checkData())
            //{
            //    return;
            //}
            WinPar(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            this.Close();
        }
        private bool checkData()
        {
            if (txtID.Text == "")
            {
                label3.Text = "员工编号不能为空";
                return false;
            }
            else if (txtName.Text == "")
            {
                label3.Text = "员工姓名不能为空";
                return false;
            }
            else
            {
                return true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            btnOk_Click(this, null);
        }
    }
}
