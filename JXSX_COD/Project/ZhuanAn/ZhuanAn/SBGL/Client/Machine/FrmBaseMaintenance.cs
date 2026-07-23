using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Util;
using Common.Base;
using DevComponents.DotNetBar.SuperGrid;
using Newtonsoft.Json;
using DevComponents.DotNetBar.Controls;
using Common;


namespace Machine
{
    public partial class FrmBaseMaintenance : Form
    {
        private DataTable AssetDT;

        public FrmBaseMaintenance()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            InitializeComponent();
        }

        //解决闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseMaintenance_Load(object sender, EventArgs e)
        {
            //菜单权限检查
            if (BaseInfo.LoginUserRight == "A")
            {
                tsmiBatchCreate.Visible = true;
                tsmiStat.Visible = true;
                tsmiGlobalUpdate.Visible = true;
                tsmiSyncItem.Visible = true;
            }
            else
            {
                tsmiBatchCreate.Visible = false;
                tsmiStat.Visible = false;
                tsmiGlobalUpdate.Visible = false;
                tsmiSyncItem.Visible = false;
            }

            labMessage.Text = "";
            dgvMaintenance.AutoGenerateColumns = false;

            //资产
            string sql = "SELECT AssetName,AssetNo FROM A_AssetInfo_T";
            AssetDT = DBUtil.GetDataTable(sql);
            //资产名称
            List<string> assetNames = AssetDT.AsEnumerable().Select(t => t.Field<string>("AssetName")).ToList().Distinct().ToList();
            assetNames.Insert(0, "");
            cmbAssetName.Items.AddRange(assetNames.ToArray());
            //资产编号
            AssetDT.Rows.InsertAt(AssetDT.NewRow(), 0);
            cmbAssetNo.DisplayMember = "AssetNo";
            cmbAssetNo.ValueMember = "AssetNo";
            cmbAssetNo.DataSource = AssetDT;

            //年份
            IList<int> years = new List<int>();
            for (int i = -1; i < 3; i++)
            {
                years.Add(DateTime.Now.Year - i);
            }
            cmbYear.DataSource = years;
            cmbYear.Text = DateTime.Now.Year.ToString();
        }
        #endregion

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            RefreshGridData();
            labMessage.Text = "数据加载完成";
            labMessage.ForeColor = Color.Green;
        }


        #endregion

        #region 保养项目
        private void tsmiMaintenanceItems_Click(object sender, EventArgs e)
        {
            FrmMaintenanceItem frm = frm = new FrmMaintenanceItem();
            frm.ShowDialog();
        }
        #endregion

        #region 批量创建
        private void tsmiBatchCreate_Click(object sender, EventArgs e)
        {
            FrmGlobalCreateReport frm = new FrmGlobalCreateReport();
            frm.ShowDialog();
        }


        #endregion

        #region 提示维护
        private void tsmiTipDicData_Click(object sender, EventArgs e)
        {
            FrmTipDicData frm = new FrmTipDicData();
            frm.ShowDialog();
        }
        #endregion

        #region 节日维护
        private void tsmiHoliday_Click(object sender, EventArgs e)
        {
            FrmHolidayDicData frm = new FrmHolidayDicData();
            frm.ShowDialog();
        }
        #endregion


        #region 更新状态
        private void tsmiStat_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (MessageBox.Show("确定要刷新报表的状态吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string sql = string.Format("EXEC StatMaintenanceReportStatus '{0}'", cmbYear.Text);
                    DBUtil.ExecSQL(sql);
                }
                catch (Exception ex)
                {
                    labMessage.Text = "更新失败,原因：" + ex.Message;
                    labMessage.ForeColor = Color.Red;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                RefreshGridData();
                labMessage.Text = "更新成功";
                labMessage.ForeColor = Color.Green;
            }
        }
        #endregion

        #region 全局维护
        private void tsmiGlobalUpdate_Click(object sender, EventArgs e)
        {
            FrmGlobalMaintenance frm = new FrmGlobalMaintenance();
            frm.ShowDialog();
        }
        #endregion

        #region 同步项目
        private void tsmiSyncItem_Click(object sender, EventArgs e)
        {
            FrmMaintenanceItemSync frm = new FrmMaintenanceItemSync();
            frm.ShowDialog();
        }
        #endregion

        #region 事件
        //资产名称下拉选事件
        private void cmbAssetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAssetNo.DataSource = null;
            if (string.IsNullOrWhiteSpace(cmbAssetName.Text))
            {
                cmbAssetNo.DisplayMember = "AssetNo";
                cmbAssetNo.ValueMember = "AssetNo";
                cmbAssetNo.DataSource = AssetDT;
            }
            else
            {
                DataTable dt = AssetDT.Clone();
                dt = AssetDT.Select("AssetName='" + cmbAssetName.Text + "'").CopyToDataTable();
                cmbAssetNo.DisplayMember = "AssetNo";
                cmbAssetNo.ValueMember = "AssetNo";
                cmbAssetNo.DataSource = dt;
                cmbAssetNo.SelectedIndex = 0;
            }
        }

        //年份选择事件
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }

        //单元格格式化
        private void dgvMaintenance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // （故障内容与解决方案的数量与顺序校验）

            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 14 && e.RowIndex >= 0 && e.Value != null)
            {
                string cell = e.Value.ToString();
                if (!string.IsNullOrWhiteSpace(cell) && cell == "1")
                {
                    e.Value = "查看";
                    DataRow row = ((DataRowView)(dgvMaintenance.Rows[e.RowIndex].DataBoundItem)).Row;
                    string propName = dgvMaintenance.Columns[e.ColumnIndex].DataPropertyName;
                    if (row["S" + propName] == DBNull.Value)
                    {
                        e.CellStyle.ForeColor = Color.Gray;
                    }
                    else if (row["S" + propName].ToString() == "Y")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
                else
                {
                    e.Value = null;
                }
            }
            //履历
            if (e.ColumnIndex >= 0 && dgvMaintenance.Columns[e.ColumnIndex].HeaderText == "履历" && e.RowIndex >= 0)
            {
                e.Value = "查看";
            }
        }

        //单元格点击
        private void dgvMaintenance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //打开保养报表窗口
            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 14 && e.RowIndex >= 0 && dgvMaintenance.Rows[e.RowIndex].Cells[e.ColumnIndex] != null)
            {
                string assetNo = dgvMaintenance.Rows[e.RowIndex].Cells["dgcAssetNo"].Value.ToString();
                string year = cmbYear.Text;
                string month = dgvMaintenance.Columns[e.ColumnIndex].DataPropertyName;
                FrmMaintenanceReport frm = new FrmMaintenanceReport(assetNo, year, month);
                frm.ShowDialog();
            }
            //打开履历
            if (e.ColumnIndex >= 0 && dgvMaintenance.Columns[e.ColumnIndex].HeaderText == "履历" && e.RowIndex >= 0)
            {
                string assetNo = dgvMaintenance.Rows[e.RowIndex].Cells["dgcAssetNo"].Value.ToString();
                FrmResumeReport frm = new FrmResumeReport(assetNo);
                frm.ShowDialog();
            }
        }
        #endregion

        #region 其他
        //刷新网格数据
        private void RefreshGridData()
        {

            string sql = string.Format(@"SELECT ai.AssetNo,ai.AssetName,a.*,b.* FROM A_AssetInfo_T ai
                                        LEFT JOIN
	                                        (SELECT * FROM (
			                                        SELECT AssetNo, ISNULL([Month],'0') [Month] FROM A_MaintenanceReport_T WHERE  [Year]='{0}'
	                                        ) AS MR
	                                        PIVOT (
			                                        COUNT([Month]) FOR [Month ] IN ([1], [2], [3],[4], [5], [6],[7], [8], [9],[10], [11], [12],[0])
	                                        ) AS PT) a
                                        ON ai.AssetNo = a.AssetNo
                                        LEFT JOIN 
                                            (SELECT AssetNo,[1] S1, [2] S2, [3]S3,[4]S4, [5]S5, [6]S6,[7]S7, [8]S8, [9]S9,[10]S10, [11]S11, [12]S12,[0]S0  FROM (
                                                SELECT AssetNo,Status, ISNULL([Month],'0') [Month] FROM A_MaintenanceReport_T WHERE [Year]='{0}' 
                                            ) AS MR
                                            PIVOT (
                                                MAX(Status)  FOR [Month ] IN ([1], [2], [3],[4], [5], [6],[7], [8], [9],[10], [11], [12],[0])
                                            ) AS PT) b 
                                        ON ai.AssetNo=b.AssetNo  WHERE 1=1 ", cmbYear.Text);
            string where = string.Format(" AND [year]={0}", cmbYear.Text);

            if (!string.IsNullOrWhiteSpace(cmbAssetName.Text))
            {
                sql += string.Format("  AND ai.AssetName like N'%{0}%'", cmbAssetName.Text);
            }
            if (!string.IsNullOrWhiteSpace(cmbAssetNo.Text))
            {
                sql += string.Format("  AND ai.AssetNo='{0}'", cmbAssetNo.Text);
            }
            sql += " Order by ai.AssetNo";
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvMaintenance.DataSource = dt;
        }

        #endregion


    }
}
