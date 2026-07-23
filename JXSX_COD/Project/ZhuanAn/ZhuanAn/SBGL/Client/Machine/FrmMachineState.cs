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
    public partial class FrmMachineState : Form
    {
        public FrmMachineState()
        {
            InitializeComponent();
        }

        private void FrmMachineDistribute_Load(object sender, EventArgs e)
        {
            dgvMachineState.AutoGenerateColumns = false;
            RefreshDataGrid();
            labMessage.Text = "";
        }


        // 刷新
        public void RefreshDataGrid()
        {
            string sqlstr = "SELECT StateName,Count  FROM O_MachineState_T ";
            dgvMachineState.DataSource = DBUtil.GetDataTable(sqlstr);
            dgvMachineState.ClearSelection();
        }

        //保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            string stateName = tbxStateName.Text.Trim();
            int count = (int)nudCount.Value;
            //数据检查
            if (string.IsNullOrWhiteSpace(stateName))
            {
                labMessage.Text = "状态名称不能为空";
                return;
            }
            if (count < 0)
            {
                labMessage.Text = "设备数量不能小于0";
                return;
            }

            //检查是否存在
            string sqlStr = string.Format("SELECT 1  FROM O_MachineState_T WHERE  StateName=N'{0}'", stateName);
            DataTable dt = DBUtil.GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                //修改
                sqlStr = string.Format("Update  O_MachineState_T set  Count= '{1}' where StateName=N'{0}'", stateName, count);
                DBUtil.ExecSQL(sqlStr);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "修改成功";
                RefreshDataGrid();
            }
            else
            {
                //添加
                sqlStr = string.Format("INSERT INTO O_MachineState_T (StateName,Count) VALUES (N'{0}','{1}')", stateName, count);
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
            if (dgvMachineState.SelectedRows.Count <= 0)
            {
                labMessage.Text = "请选择一条数据（整行选中）";
                return;
            }
            string stateName = dgvMachineState.SelectedRows[0].Cells["dgcStateName"].Value.ToString();
            if (MessageBox.Show("确定要删除【状态名称:" + stateName + "】吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_delete = string.Format("DELETE O_MachineState_T WHERE StateName=N'{0}'", stateName);
                DBUtil.ExecSQL(sql_delete);

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
                RefreshDataGrid();
            }
        }


        private void dgvMachineState_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    int rowIndex = dgvMachineState.CurrentCell.RowIndex;//选中行的索引
                    tbxStateName.Text = dgvMachineState.Rows[rowIndex].Cells["dgcStateName"].Value.ToString();//处理信息的ID
                    nudCount.Text = dgvMachineState.Rows[rowIndex].Cells["dgcCount"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMachineState), null, ex);
            }
        }
    }
}
