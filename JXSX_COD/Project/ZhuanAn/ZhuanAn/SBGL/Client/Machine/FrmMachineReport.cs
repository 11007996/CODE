using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmMachineReport : Form
    {
        private int MachineCode = 0;
        private DataTable MachineDT;
        private int RowIndex = -1;//右键所在数据行索引
        public FrmMachineReport()
        {
            InitializeComponent();
        }

        public FrmMachineReport(int machineCode)
        {
            InitializeComponent();
            MachineCode = machineCode;
        }

        private void FrmMachineReport_Load(object sender, EventArgs e)
        {
            dgvMachineReport.AutoGenerateColumns = false;
            string sql = "SELECT CONCAT('(',MachineCode,')',MachineName) MachineName,MachineCode FROM M_Machine_T;";
            MachineDT = DBUtil.GetDataTable(sql);
            if (MachineDT != null)
            {
                DataRow row = MachineDT.NewRow();
                row["MachineCode"] = 0;
                MachineDT.Rows.InsertAt(row,0);
                cmbMachineCode.DisplayMember = "MachineName";
                cmbMachineCode.ValueMember = "MachineCode";
                cmbMachineCode.DataSource = MachineDT;
                if (MachineCode > 0)
                {
                    cmbMachineCode.SelectedValue = MachineCode;
                }
            }
            RefreshGridData();
        }

        //刷新数据
        private void RefreshGridData()
        {
            if (cmbMachineCode.SelectedItem == null || cmbMachineCode.SelectedIndex < 0)
            {
                return;
            }
            string sql = string.Format("SELECT * FROM M_MachineReport_T WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(cmbMachineCode.SelectedValue.ToString()))
            {
                sql += string.Format(" AND MachineCode='{0}' ", cmbMachineCode.SelectedValue.ToString());
            }
            sql += string.Format(" AND CreateTime between '{0}' AND '{1}' order by  CreateTime desc", dtpStartTime.Value.ToString("yyyy-MM-dd 00:00:00"), Convert.ToDateTime(dtpEndTime.Value.ToString()).AddDays(1).Date);
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvMachineReport.DataSource = dt;
        }

        private void cmbMachineCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }

        //删除指定记录
        private void tmsDel_Click(object sender, EventArgs e)
        {
            if (RowIndex >= 0)
            {
                DataRow row = ((DataRowView)dgvMachineReport.Rows[RowIndex].DataBoundItem).Row;
                string machineCode = row["MachineCode"].ToString();
                DateTime createTime = Convert.ToDateTime(row["CreateTime"]);
                if (!string.IsNullOrWhiteSpace(machineCode))
                {
                    if (DialogResult.Yes == MessageBox.Show("确定要删除这条记录吗", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand))
                    {
                        string sql = string.Format("DELETE M_MachineReport_T WHERE MachineCode='{0}' AND CreateTime='{1}'", machineCode, createTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        DBUtil.ExecSQL(sql);
                        RefreshGridData();
                    }
                }
            }

        }

        private void dgvMachineReport_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RowIndex = -1;
            if (e is MouseEventArgs && e.RowIndex >= 0)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Right)
                {
                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    int screenHeight = rect.Height;
                    int screenWidth = rect.Width;
                    int x = screenWidth - MousePosition.X < contextMenuStrip.Width ? MousePosition.X - contextMenuStrip.Width : MousePosition.X;
                    int y = screenHeight - MousePosition.Y < contextMenuStrip.Height ? MousePosition.Y - contextMenuStrip.Height : MousePosition.Y;
                    contextMenuStrip.Show(x, y);
                    RowIndex = e.RowIndex;
                }
            }

        }


    }
}
