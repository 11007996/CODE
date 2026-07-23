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
using Common.Base;
using DevComponents.DotNetBar.SuperGrid;
using Common;
using DevComponents.DotNetBar.Controls;
using Common.Util;


namespace Machine
{
    public partial class FrmResumeReport : Form
    {
        private int? ReportId;//报表ID
        private string TakenDept = "";//保管部门
        private readonly byte MaintenanceMaxRowCount = 16;//保养表最大行数
        private readonly byte RepairMaxRowCount = 7;//维护表最大行数
        private DataTable MaintenanceDT = null;
        private DataTable RepairDT = null;

        public FrmResumeReport(string assetNo)
        {
            InitializeComponent();
            txbAssetNo.Text = assetNo;
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmResumeReport_Load(object sender, EventArgs e)
        {
            //权限判断
            if (BaseInfo.LoginUserRight != "A")
            {
                dgvMaintenanceMenuDelete.Visible = false;
                dgvRepairMenuDelete.Visible = false;
            }

            dgvMaintenance.AutoGenerateColumns = false;
            dgvRepair.AutoGenerateColumns = false;
            dgvMaintenance.DataSource = MaintenanceDT;
            dgvRepair.DataSource = RepairDT;
            labMessage.Text = "";

            //资产信息
            string sql = string.Format("SELECT * FROM A_AssetInfo_T WHERE AssetNo='{0}'", txbAssetNo.Text);
            DataTable dt = DBUtil.GetDataTable(sql);

            if (dt.Rows.Count == 1)
            {
                //初始控件信息
                txbAssetName.Text = dt.Rows[0]["AssetName"].ToString();
                txbModel.Text = dt.Rows[0]["Model"].ToString();
                txbMadeFactory.Text = dt.Rows[0]["MadeFactory"].ToString();
                txbEntryDate.Text = Convert.ToDateTime(dt.Rows[0]["EntryDate"].ToString()).ToString("yyyy-MM-dd");
                txbControlNo.Text = dt.Rows[0]["ControlNo"].ToString();
                TakenDept = dt.Rows[0]["CostCenter"].ToString();

                string assetClass = dt.Rows[0]["AssetClass"].ToString();
                if (assetClass.Contains("检测仪器"))
                    chkA.Checked = true;
                else if (assetClass.Contains("生产设备"))
                    chkB.Checked = true;
                else
                    chkC.Checked = true;
            }

            //初始页码
            GetMaxPageNo();
            InitReportId();
            //刷新
            RefreshDataGrid();
        }
        #endregion

        #region 刷新
        private void tsmRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
            labMessage.ForeColor = Color.Green;
            labMessage.Text = "数据加载完成";
        }
        #endregion

        #region 创建
        private void tsmiCreate_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string assetNo = txbAssetNo.Text;
            if (string.IsNullOrWhiteSpace(assetNo))
            {
                labMessage.Text = "资产编号不能为空";
                labMessage.ForeColor = Color.Red;
                return;
            }
            //查找新的页码
            int pageNo = GetMaxPageNo() + 1;

            if (DialogResult.Yes == MessageBox.Show("确定要创建履历报表吗?\r资产编号：" + assetNo + "\r页码：" + pageNo, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                //创建
                string sql = string.Format("Insert into A_ResumeReport_T(AssetNo,PageNo)Values('{0}',{1})", assetNo, pageNo);
                DBUtil.ExecSQL(sql);
                labMessage.Text = "创建成功";
                labMessage.ForeColor = Color.Green;

                labPageNo.Text = "页，共" + pageNo + "页";
                nudPageNo.Maximum = pageNo;
                nudPageNo.Value = pageNo;
            }
        }
        #endregion

        #region 导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                //文件夹选择
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "请选择文件保存位置";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("AssetNo", txbAssetNo.Text);
                    dic.Add("AssetName", txbAssetName.Text);
                    //dic.Add("AssetClass", AssetClass);
                    dic.Add("MadeFactory", txbMadeFactory.Text);
                    dic.Add("Model", txbModel.Text);
                    dic.Add("EntryDate", txbEntryDate.Text);
                    dic.Add("ControlNo", txbControlNo.Text);

                    string newText = "固定资产种类：□A类检测仪器　□B类生产设备　□C类基础设施";
                    if (chkA.Checked)
                        newText = "固定资产种类：☑A类检测仪器　□B类生产设备　□C类基础设施";
                    else if (chkB.Checked)
                        newText = "固定资产种类：□A类检测仪器　☑B类生产设备　□C类基础设施";
                    else
                        newText = "固定资产种类：□A类检测仪器　□B类生产设备　☑C类基础设施";
                    dic.Add("AssetClassText", newText);

                    int pageNO = (int)nudPageNo.Value;
                    ResumeWord.ExportToWordByTemplate(folderBrowser.SelectedPath + "\\" + txbAssetNo.Text.Trim() + "固定资产履历表" + "[" + pageNO + "].docx", dic, MaintenanceDT, RepairDT);
                    MessageBox.Show("导出成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmResumeReport), null, ex);
                MessageBox.Show("导出失败:" + ex.Message);
            }
        }
        #endregion

        #region 全部导出
        private void tsmiExportAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要导出【" + txbAssetNo.Text + "】的所有履历报表吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                //文件夹选择
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "请选择文件保存位置";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        string sql = string.Format("SELECT Id,PageNo FROM A_ResumeReport_T WHERE AssetNo='{0}'", txbAssetNo.Text);
                        DataTable resumeDT = DBUtil.GetDataTable(sql);
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("AssetNo", txbAssetNo.Text);
                        dic.Add("AssetName", txbAssetName.Text);
                        //dic.Add("AssetClass", AssetClass);
                        dic.Add("MadeFactory", txbMadeFactory.Text);
                        dic.Add("Model", txbModel.Text);
                        dic.Add("EntryDate", txbEntryDate.Text);
                        dic.Add("ControlNo", txbControlNo.Text);

                        string newText = "固定资产种类：□A类检测仪器　□B类生产设备　□C类基础设施";
                        if (chkA.Checked)
                            newText = "固定资产种类：☑A类检测仪器　□B类生产设备　□C类基础设施";
                        else if (chkB.Checked)
                            newText = "固定资产种类：□A类检测仪器　☑B类生产设备　□C类基础设施";
                        else
                            newText = "固定资产种类：□A类检测仪器　□B类生产设备　☑C类基础设施";
                        dic.Add("AssetClassText", newText);

                        foreach (DataRow row in resumeDT.Rows)
                        {
                            int pageNO = Convert.ToInt32(row["PageNo"]);
                            string reportId = row["Id"].ToString();
                            sql = string.Format(@"SELECT Id,ExecuteCategory,CASE ExecuteCategory WHEN '保养' THEN '√' END IsMaintenance,
                                            CASE ExecuteCategory WHEN '校验' THEN '√' END IsCheck,
                                            CONVERT(VARCHAR(10),ExecuteDate,120) ExecuteDate,TakenDept,
                                            ExecuteState,ExecuteUser, CONVERT(VARCHAR(10),NextExecuteDate,120) NextExecuteDate,Remark
                                         FROM A_ResumeMaintenanceD_T WHERE AssetNo='{0}' AND ReportId='{1}' ORDER BY ExecuteDate DESC;
                                         SELECT Id, CONVERT(VARCHAR(10),RepairDate,120) RepairDate, AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark
                                         FROM A_ResumeRepairD_T WHERE AssetNo='{0}'  AND ReportId='{1}' ORDER BY RepairDate DESC; ", txbAssetNo.Text, reportId); ;
                            DataSet ds = DBUtil.GetDataSet(sql, new string[] { "ResumeDT", "RepairDT" });
                            DataTable tempMaintenanceDT = ds.Tables["ResumeDT"];
                            DataTable tempRepairDT = ds.Tables["RepairDT"];
                            ResumeWord.ExportToWordByTemplate(folderBrowser.SelectedPath + "\\" + txbAssetNo.Text.Trim() + "固定资产履历表" + "[" + pageNO + "].docx", dic, tempMaintenanceDT, tempRepairDT);
                        }
                        MessageBox.Show("导出成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败，原因：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                }

            }
        }
        #endregion

        #region 刷新数据
        /// <summary>
        /// 刷新机台数据表的数据源
        /// </summary>
        private void RefreshDataGrid()
        {
            //数据检查
            string assetNo = txbAssetNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(assetNo))
                return;
            if (ReportId == null || ReportId <= 0)
                return;
            //查询
            string strSql = string.Format(@"SELECT Id,ExecuteCategory,CASE ExecuteCategory WHEN '保养' THEN '√' END IsMaintenance,
                                         CASE ExecuteCategory WHEN '校验' THEN '√' END IsCheck,
                                         CONVERT(VARCHAR(10),ExecuteDate,120) ExecuteDate,TakenDept,
                                         ExecuteState,ExecuteUser, CONVERT(VARCHAR(10),NextExecuteDate,120) NextExecuteDate,Remark
                                         FROM A_ResumeMaintenanceD_T WHERE AssetNo='{0}' AND ReportId='{1}' ORDER BY ExecuteDate ASC;
                                         SELECT Id, CONVERT(VARCHAR(10),RepairDate,120) RepairDate, AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark
                                             FROM A_ResumeRepairD_T WHERE AssetNo='{0}'  AND ReportId='{1}' ORDER BY RepairDate ASC; ", assetNo, ReportId);
            DataSet ds = DBUtil.GetDataSet(strSql, new string[] { "MaintenanceDT", "RepairDT" });
            //数据处理
            MaintenanceDT = null;
            RepairDT = null;
            if (ds != null && ds.Tables.Count == 2)
            {
                //保养表
                MaintenanceDT = ds.Tables["MaintenanceDT"];
                for (int i = 1; i <= MaintenanceMaxRowCount; i++)
                {
                    if (i > MaintenanceDT.Rows.Count)
                        MaintenanceDT.Rows.Add();
                }
                dgvMaintenance.DataSource = MaintenanceDT;
                //维修表
                RepairDT = ds.Tables["RepairDT"];
                for (int i = 1; i <= RepairMaxRowCount; i++)
                {
                    if (i > RepairDT.Rows.Count)
                        RepairDT.Rows.Add();
                }
                dgvRepair.DataSource = RepairDT;
            }
        }
        #endregion

        #region 页码事件
        //根据页码初始化当前报表ID
        private void InitReportId()
        {
            ReportId = null;
            string sql = string.Format("SELECT Id FROM A_ResumeReport_T WHERE AssetNo='{0}' AND PageNo={1}", txbAssetNo.Text, nudPageNo.Value);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count == 1)
            {

                ReportId = Convert.ToInt32(dt.Rows[0]["Id"]);
            }
        }

        //获取最大页码
        private int GetMaxPageNo()
        {
            string sql = string.Format("SELECT ISNULL(Max(PageNo),0) as PageNo FROM A_ResumeReport_T WHERE AssetNo='{0}';", txbAssetNo.Text);
            DataTable dt2 = DBUtil.GetDataTable(sql);
            int maxPageNo = Convert.ToInt32(dt2.Rows[0]["PageNo"]);
            labPageNo.Text = "页，共" + maxPageNo + "页";
            nudPageNo.Maximum = maxPageNo;
            nudPageNo.Value = maxPageNo;
            return maxPageNo;
        }

        //页码值变更
        private void nudPageNo_ValueChanged(object sender, EventArgs e)
        {
            InitReportId();
            RefreshDataGrid();
        }
        #endregion

        #region 保养操作
        //右健菜单【保存】
        private void dgvMaintenanceMenuSave_Click(object sender, EventArgs e)
        {
            //获取选中行的绑定的数据
            DataGridViewRow rowView = dgvMaintenance.SelectedRows[0];
            DataRow row = ((DataRowView)dgvMaintenance.SelectedRows[0].DataBoundItem).Row;
            string msg = "";
            bool saveFlag = false;//是否保存成功

            //基础数据
            string assetNo = txbAssetNo.Text.Trim();
            string executeCategory = row["ExecuteCategory"].ToString();
            string executeDate = row["ExecuteDate"].ToString();
            string takenDept = row["TakenDept"].ToString();
            string executeState = row["ExecuteState"].ToString();
            string executeUser = row["ExecuteUser"].ToString();
            string nextDate = row["NextExecuteDate"].ToString();
            string remark = row["Remark"].ToString();
            //数据检查
            if (!CheckMaintenanceForm(assetNo,executeCategory, executeDate, takenDept, executeState, executeUser, nextDate, ref msg))
            {
                MessageBox.Show(msg, "参数异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //保存
                string sql = $@"INSERT INTO A_ResumeMaintenanceD_T(ReportId,AssetNo,ExecuteCategory,ExecuteDate,TakenDept,ExecuteState,ExecuteUser,NextExecuteDate,Remark,UpdateTime,UpdateUser)
                                VALUES('{ReportId}','{assetNo}',N'{executeCategory}','{executeDate}',N'{takenDept}',N'{executeState}',N'{executeUser}','{nextDate}',N'{remark}',GETDATE(),'{BaseInfo.LoginUserNo}')
                                Select SCOPE_IDENTITY() AS 'Identity'";
                DataTable dt = DBUtil.GetDataTable(sql);

                if (dt != null && dt.Rows.Count == 1)
                {
                    saveFlag = true;
                    row["Id"] = dt.Rows[0][0];
                }
                MessageBox.Show("保存成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败,异常信息:" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.Error(typeof(FrmResumeReport), ex.Message);
            }

            //更新行状态
            if (saveFlag)
            {
                rowView.DefaultCellStyle.BackColor = Color.White;
            }
        }

        //检查提交的数据
        private bool CheckMaintenanceForm(string assetNo,string executeCategory, string executeDate, string takenDept, string executeState, string executeUser, string nextDate, ref string msg)
        {
            msg = "";
            if (ReportId == null || ReportId <= 0)
                msg = "未找到相关报表，页码：" + nudPageNo.Value + "，请先创建报表";
            else if (string.IsNullOrWhiteSpace(assetNo))
                msg = "【资产编号】不能为空";
            else if (string.IsNullOrWhiteSpace(executeCategory))
                msg = "【实施类别】不能为空";
            else if (string.IsNullOrWhiteSpace(executeDate))
                msg = "【实施日期】不能为空";
            else if (string.IsNullOrWhiteSpace(nextDate))
                msg = "【下次实施日期】不能为空";
            else if (Convert.ToDateTime(executeDate) >= Convert.ToDateTime(nextDate))
                msg = "【下次实施日期】需要大于【实施日期】";
            else if (string.IsNullOrWhiteSpace(executeUser))
                msg = "【实施人员】不能为空";
            return msg.Length <= 0;
        }


        //右健菜单【删除】
        private void dgvMaintenanceMenuDelete_Click(object sender, EventArgs e)
        {
            //获取选中行的绑定的数据
            DataGridViewRow rowView = dgvMaintenance.SelectedRows[0];
            DataRow row = ((DataRowView)dgvMaintenance.SelectedRows[0].DataBoundItem).Row;
            string id = row["Id"].ToString();
            bool deletedFlag = false;
            //判断绑定的数据是否有id,如果有需要确认提示，再删除数据库内的数据
            if (string.IsNullOrEmpty(id))
            {
                deletedFlag = true;
            }
            else
            {
                //删除数据
                if (MessageBox.Show($"确定要删除吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string sql = $"DELETE FROM A_ResumeMaintenanceD_T WHERE Id='{id}'";
                    DBUtil.ExecSQL(sql);
                    deletedFlag = true;
                }
            }
            //判断是否删除成功，并清空控件上的数据
            if (deletedFlag)
            {
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    row[i] = DBNull.Value;
                }
                rowView.DefaultCellStyle.BackColor = Color.White;
            }
        }
        #endregion

        #region 维修操作
        //右健菜单【保存】
        private void dgvRepairMenuSave_Click(object sender, EventArgs e)
        {
            //获取选中行的绑定的数据
            DataGridViewRow rowView = dgvRepair.SelectedRows[0];
            DataRow row = ((DataRowView)dgvRepair.SelectedRows[0].DataBoundItem).Row;
            string msg = "";
            bool saveFlag = false;//是否保存成功

            //基础数据
            string assetNo = txbAssetNo.Text.Trim();
            string repairDate = row["RepairDate"].ToString();
            string abnormalDesc = row["AbnormalDesc"].ToString();
            string repairReason = row["RepairReason"].ToString();
            string repairUser = row["RepairUser"].ToString();
            string checkResult = row["CheckResult"].ToString();
            string remark = row["Remark"].ToString();
            //数据检查
            if (!CheckRepairForm(assetNo, repairDate, checkResult, repairUser, ref msg))
            {
                MessageBox.Show(msg, "参数异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //保存
                string sql = $@"INSERT INTO A_ResumeRepairD_T(ReportId,AssetNo,RepairDate,AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark,UpdateTime,UpdateUser)
                         VALUES('{ReportId}','{assetNo}','{repairDate}',N'{abnormalDesc}',N'{repairReason}',N'{repairUser}',N'{checkResult}',N'{remark}',GETDATE(),'{BaseInfo.LoginUserNo}')
                        Select SCOPE_IDENTITY() AS 'Identity'";
                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    saveFlag = true;
                    row["Id"] = dt.Rows[0][0];
                }
                MessageBox.Show("保存成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败,异常信息:" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.Error(typeof(FrmResumeReport), ex.Message);
            }

            //更新行状态
            if (saveFlag)
            {
                rowView.DefaultCellStyle.BackColor = Color.White;
            }
        }

        //检查参数
        private bool CheckRepairForm(string assetNo, string repairDate, string checkResult, string repairUser, ref string msg)
        {
            msg = "";
            if (ReportId == null || ReportId <= 0)
                msg = "未找到相关报表，页码：" + nudPageNo.Value + "，请先创建报表";
            else if (string.IsNullOrWhiteSpace(assetNo))
                msg = "【资产编号】不能为空";
            else if (string.IsNullOrWhiteSpace(repairDate))
                msg = "【维修日期】不能为空";
            else if (string.IsNullOrWhiteSpace(checkResult))
                msg = "【验收结果】不能为空";
            else if (string.IsNullOrWhiteSpace(repairUser))
                msg = "【维修人员】不能为空";
            return msg.Length <= 0;
        }

        //右健菜单【删除】
        private void dgvRepairMenuDelete_Click(object sender, EventArgs e)
        {
            //获取绑定的数据
            DataGridViewRow rowView = dgvRepair.SelectedRows[0];
            DataRow row = ((DataRowView)dgvRepair.SelectedRows[0].DataBoundItem).Row;
            string id = row["Id"].ToString();
            bool deletedFlag = false;
            //判断绑定的数据是否有id,如果有需要确认提示，再删除数据库内的数据
            if (string.IsNullOrEmpty(id))
            {
                deletedFlag = true;
            }
            else
            {
                //删除数据
                if (MessageBox.Show($"确定要删除吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string sql = $"DELETE FROM A_ResumeRepairD_T WHERE Id='{id}'";
                    DBUtil.ExecSQL(sql);
                    deletedFlag = true;
                }
            }
            //判断是否删除成功，并清空控件上的数据
            if (deletedFlag)
            {
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    row[i] = DBNull.Value;
                }
                rowView.DefaultCellStyle.BackColor = Color.White;
            }
        }
        #endregion

        #region 通用数据表事件
        //单元格值变更
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewX dgv = (DataGridViewX)sender;
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Coral;
                //保养表特殊处理
                if (dgv == dgvMaintenance)
                {
                    //自动补全数据
                    DataRow row = ((DataRowView)dgvMaintenance.Rows[e.RowIndex].DataBoundItem).Row;
                    if (string.IsNullOrWhiteSpace(row["ExecuteDate"].ToString()))
                        row["ExecuteDate"] = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(row["TakenDept"].ToString()))
                        row["TakenDept"] = TakenDept;
                    if (string.IsNullOrWhiteSpace(row["ExecuteState"].ToString()))
                        row["ExecuteState"] = "OK";
                    if (string.IsNullOrWhiteSpace(row["ExecuteUser"].ToString()))
                        row["ExecuteUser"] = BaseInfo.LoginUserName;
                    if (string.IsNullOrWhiteSpace(row["NextExecuteDate"].ToString()))
                        row["NextExecuteDate"] = DateTime.Now;
                }
                //维修表特殊处理
                if (dgv == dgvRepair)
                {
                    //自动补全数据
                    DataRow row = ((DataRowView)dgvRepair.Rows[e.RowIndex].DataBoundItem).Row;
                    if (string.IsNullOrWhiteSpace(row["RepairDate"].ToString()))
                        row["RepairDate"] = DateTime.Now;
                    if (string.IsNullOrWhiteSpace(row["RepairUser"].ToString()))
                        row["RepairUser"] = BaseInfo.LoginUserName;
                }

            }
        }

        //右键选中事先
        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewX dgv = (DataGridViewX)sender;
                    dgv.EndEdit();
                    dgv.ClearSelection();
                    dgv.Rows[e.RowIndex].Selected = true;
                    //已保存的不可以编辑保存了
                    string Id = ((DataRowView)dgv.Rows[e.RowIndex].DataBoundItem).Row["Id"].ToString();
                    //保养表
                    if (dgv == dgvMaintenance)
                    {

                        if (!string.IsNullOrWhiteSpace(Id))
                        {
                            dgvMaintenanceMenuSave.Enabled = false;

                        }
                        else
                        {
                            dgvMaintenanceMenuSave.Enabled = true;
                        }
                        dgvMaintenanceMenu.Show(Control.MousePosition);
                    }
                    //维修表
                    if (dgv == dgvRepair)
                    {
                        if (!string.IsNullOrWhiteSpace(Id))
                        {
                            dgvRepairMenuSave.Enabled = false;

                        }
                        else
                        {
                            dgvRepairMenuSave.Enabled = true;
                        }
                        dgvRepairMenu.Show(Control.MousePosition);
                    }

                }
            }
        }

        #endregion


    }
}
