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
    public partial class FrmMachineDistribute : Form
    {
        public FrmMachineDistribute()
        {
            InitializeComponent();
        }

        private void FrmMachineDistribute_Load(object sender, EventArgs e)
        {
            dgvMachineDistribute.AutoGenerateColumns = false;
            RefreshDataGrid();
            labMessage.Text = "";
        }


        // 刷新
        public void RefreshDataGrid()
        {
            string sqlstr = "SELECT PointName,Count  FROM O_MachineDistribute_T ";
            dgvMachineDistribute.DataSource = DBUtil.GetDataTable(sqlstr);
            dgvMachineDistribute.ClearSelection();
        }

        //保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            string pointName = tbxPointName.Text.Trim();
            int count = (int)nudCount.Value;
            //数据检查
            if (string.IsNullOrWhiteSpace(pointName))
            {
                labMessage.Text = "点位名称不能为空";
                return;
            }
            if (count < 0)
            {
                labMessage.Text = "设备数量不能小于0";
                return;
            }

            //检查是否存在
            string sqlStr = string.Format("SELECT 1  FROM O_MachineDistribute_T WHERE  PointName=N'{0}'", pointName);
            DataTable dt = DBUtil.GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                //修改
                sqlStr = string.Format("Update  O_MachineDistribute_T set  Count= '{1}' where PointName=N'{0}'", pointName,count);
                DBUtil.ExecSQL(sqlStr);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "修改成功";
                RefreshDataGrid();
            }
            else
            {
                //添加
                sqlStr = string.Format("INSERT INTO O_MachineDistribute_T (PointName,Count) VALUES (N'{0}','{1}')", pointName, count);
                DBUtil.ExecSQL(sqlStr);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "添加成功";
                RefreshDataGrid();
            }
        }

        //删除
        private void tsmiDel_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            if (dgvMachineDistribute.SelectedRows.Count <= 0)
            {
                labMessage.Text = "请选择一条数据（整行选中）";
                return;
            }
            string pointName = dgvMachineDistribute.SelectedRows[0].Cells["dgcPointName"].Value.ToString();
            if (MessageBox.Show("确定要删除【点位名称:" + pointName + "】吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_delete = string.Format("DELETE O_MachineDistribute_T WHERE PointName=N'{0}'", pointName);
                DBUtil.ExecSQL(sql_delete);

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
                RefreshDataGrid();
            }
        }


        private void dgvMachineDistribute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    int rowIndex = dgvMachineDistribute.CurrentCell.RowIndex;//选中行的索引
                    tbxPointName.Text = dgvMachineDistribute.Rows[rowIndex].Cells["dgcPointName"].Value.ToString();//处理信息的ID
                    nudCount.Text = dgvMachineDistribute.Rows[rowIndex].Cells["dgcCount"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMachineDistribute), null, ex);
            }
        }
    }
}
