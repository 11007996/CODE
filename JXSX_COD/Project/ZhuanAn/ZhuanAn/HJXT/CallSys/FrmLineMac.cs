using CallSys;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CallSys
{
    /// <summary>
    /// 产线模组窗口
    /// </summary>
    public partial class FrmLineMac : Form
    {
        private List<string> MachineTypeList;//机台类型列表。

        public FrmLineMac()
        {
            InitializeComponent();
        }

        private void FrmLineMac_Load(object sender, EventArgs e)
        {
            string strSql = "SELECT Line FROM M_LineInfo_T";
            cmbLine.DataSource = DBUtil.GetDataTable(strSql);
            cmbLine.ValueMember = "Line";
            cmbLine.Text = BaseInfo.Line;
            RefreshLineMachines();

            strSql = "SELECT MachineType FROM M_MachineType_T";
            DataTable dt = DBUtil.GetDataTable(strSql);
            if (dt != null)
            {
                MachineTypeList = dt.AsEnumerable().Select(t => t.Field<string>("MachineType")).ToList();
                MachineTypeList.Insert(0, "");
                cmbMachine.DataSource = MachineTypeList;
            }

        }

        /// <summary>
        /// 添加一个模组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            string machine = cmbMachine.Text.Trim();
            string line = cmbLine.Text.Trim();
            if (string.IsNullOrWhiteSpace(cmbMachine.Text))
            {
                labMessage.Text = "机台不能为空";
                return;
            }

            //输入的机台名称是否合规
            string machineType = machine.Split('*')[0];
            if (!MachineTypeList.Contains(machineType))
            {
                labMessage.Text = "机台类型【" + machineType + "】不存在";
                return;
            }

            //检查是否重复添加
            string sqlStr = string.Format("SELECT [machine]  FROM [dbo].[M_LineMachines_T] where [line]='{0}' and machine='{1}'", line, machine);
            DataTable dt = DBUtil.GetDataTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                labMessage.Text = "机台已经存在，请不要重复添加";
                return;
            }
            else
            {
                //添加
                string sql_Insert = string.Format("insert [dbo].[M_LineMachines_T]([line],[machine]) values ('{0}','{1}')", line, machine);
                DBUtil.ExecSQL(sql_Insert);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "机台添加成功";
                RefreshLineMachines();
            }
        }

        /// <summary>
        /// 删除一个模组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            if (lbMachine.SelectedIndex < 0)
            {
                labMessage.Text = "请选择一条数据";
                return;
            }
            if (MessageBox.Show("确定要删除【" + lbMachine.SelectedValue + "】吗?", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql_delete = string.Format("delete [M_LineMachines_T] where [line]='{0}' and [machine]='{1}'", cmbLine.Text.Trim(), lbMachine.SelectedValue.ToString());
                DBUtil.ExecSQL(sql_delete);

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
                RefreshLineMachines();
            }
        }

        /// <summary>
        /// 刷新模组列表
        /// </summary>
        public void RefreshLineMachines()
        {
            string line = cmbLine.Text.Trim();
            string sqlstr = string.Format("SELECT [machine]  FROM [dbo].[M_LineMachines_T] WHERE [line]='{0}'", line);
            lbMachine.DataSource = DBUtil.GetDataTable(sqlstr);
            lbMachine.ValueMember = "machine";
        }

        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLineMachines();
        }


    }
}
