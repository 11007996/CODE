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
using Common.Utils;


namespace Machine
{
    public partial class FrmResumeReport_BK : Form
    {

        private DataTable ResumeDT = null;
        private DataTable RepairDT = null;

        public FrmResumeReport_BK(string assetNo)
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

            dgvResume.AutoGenerateColumns = false;
            dgvRepair.AutoGenerateColumns = false;
            dgvResume.DataSource = ResumeDT;
            dgvRepair.DataSource = RepairDT;

            //资产信息
            string sql = string.Format("SELECT * FROM A_AssetInfo_T WHERE AssetNo='{0}'", txbAssetNo.Text);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt.Rows.Count == 1)
            {
                txbAssetName.Text = dt.Rows[0]["AssetName"].ToString();
                txbAssetClass.Text = dt.Rows[0]["AssetClass"].ToString();
                txbModel.Text = dt.Rows[0]["Model"].ToString();
                txbMadeFactory.Text = dt.Rows[0]["MadeFactory"].ToString();
                dtpEntryDate.Value = Convert.ToDateTime(dt.Rows[0]["EntryDate"]);
                txbControlNo.Text = dt.Rows[0]["ControlNo"].ToString();
                txbTakenDept.Text = dt.Rows[0]["CostCenter"].ToString();

            }
            txbExecuteUser.Text = BaseInfo.LoginUserName;

            //初始页码
            GetMaxPageNo();

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

                labPageNo.Text = "共" + pageNo + "页";
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
                    dic.Add("AssetClass", txbAssetClass.Text);
                    dic.Add("MadeFactory", txbMadeFactory.Text);
                    dic.Add("Model", txbModel.Text);
                    dic.Add("EntryDate", dtpEntryDate.Value.ToString("yyyy-MM-dd"));
                    dic.Add("ControlNo", txbControlNo.Text);

                    int pageNO = (int)nudPageNo.Value;
                    ResumeWord.ExportToWordByTemplate(folderBrowser.SelectedPath + "\\" + txbAssetNo.Text.Trim() + "固定资产履历表" + "[" + pageNO + "].docx", dic, ResumeDT, RepairDT);
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
                        dic.Add("AssetClass", txbAssetClass.Text);
                        dic.Add("MadeFactory", txbMadeFactory.Text);
                        dic.Add("Model", txbModel.Text);
                        dic.Add("EntryDate", dtpEntryDate.Value.ToString("yyyy-MM-dd"));
                        dic.Add("ControlNo", txbControlNo.Text);

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
                            DataTable tempResumeDT = ds.Tables["ResumeDT"];
                            DataTable tempRepairDT = ds.Tables["RepairDT"];
                            ResumeWord.ExportToWordByTemplate(folderBrowser.SelectedPath + "\\" + txbAssetNo.Text.Trim() + "固定资产履历表" + "[" + pageNO + "].docx", dic, tempResumeDT, tempRepairDT);
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

        #region 数据表相关
        /// <summary>
        /// 刷新机台数据表的数据源
        /// </summary>
        private void RefreshDataGrid()
        {
            labMessage.Text = "";
            string assetNo = txbAssetNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(assetNo))
            {
                labMessage.Text = "未输入资产编号";
                labMessage.ForeColor = Color.Red;
                return;
            }

            string strSql = "";

            //是否按页码查看
            if (swBtnPage.Value)
            {//是
                strSql = string.Format(@"DECLARE @ReportId int;
                                         SELECT @ReportId=Id FROM A_ResumeReport_T WHERE AssetNo='{0}' AND PageNo={1};
                                         SELECT Id,ExecuteCategory,CASE ExecuteCategory WHEN '保养' THEN '√' END IsMaintenance,
                                            CASE ExecuteCategory WHEN '校验' THEN '√' END IsCheck,
                                            CONVERT(VARCHAR(10),ExecuteDate,120) ExecuteDate,TakenDept,
                                            ExecuteState,ExecuteUser, CONVERT(VARCHAR(10),NextExecuteDate,120) NextExecuteDate,Remark
                                         FROM A_ResumeMaintenanceD_T WHERE AssetNo='{0}' AND ReportId=@ReportId ORDER BY ExecuteDate DESC;
                                         SELECT Id, CONVERT(VARCHAR(10),RepairDate,120) RepairDate, AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark
                                         FROM A_ResumeRepairD_T WHERE AssetNo='{0}'  AND ReportId=@ReportId ORDER BY RepairDate DESC; ", assetNo, nudPageNo.Value);
            }
            else
            {
                strSql = string.Format(@"SELECT Id,ExecuteCategory,CASE ExecuteCategory WHEN '保养' THEN '√' END IsMaintenance,
                                            CASE ExecuteCategory WHEN '校验' THEN '√' END IsCheck,
                                            CONVERT(VARCHAR(10),ExecuteDate,120) ExecuteDate,TakenDept,
                                           ExecuteState,ExecuteUser, CONVERT(VARCHAR(10),NextExecuteDate,120) NextExecuteDate,Remark
                                           FROM A_ResumeMaintenanceD_T WHERE AssetNo='{0}' ORDER BY ExecuteDate DESC;
                                           SELECT Id, CONVERT(VARCHAR(10),RepairDate,120) RepairDate, AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark
                                           FROM A_ResumeRepairD_T WHERE AssetNo='{0}' ORDER BY RepairDate DESC; ", assetNo);
            }


            DataSet ds = DBUtil.GetDataSet(strSql, new string[] { "ResumeDT", "RepairDT" });

            ResumeDT = null;
            ResumeDT = null;
            if (ds != null && ds.Tables.Count == 2)
            {
                ResumeDT = ds.Tables["ResumeDT"];
                RepairDT = ds.Tables["RepairDT"];
                dgvResume.DataSource = ResumeDT;
                dgvRepair.DataSource = RepairDT;
                gbResume.Text = "保养、校验记录【" + ResumeDT.Rows.Count + "】";
                gbRepair.Text = "维修记录【" + RepairDT.Rows.Count + "】";
            }
        }

        #endregion

        #region 保存[保养、校验]
        //保养、校验记录保存
        private void btnResumeSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string msg = "";
            //数据检查
            if (!CheckResumeForm(ref msg))
            {
                labMessage.Text = msg;
                labMessage.ForeColor = Color.Red;
                return;
            }

            //基础数据
            string reportId = "";
            string assetNo = txbAssetNo.Text.Trim();
            string executeCategory = cmbExecuteCategory.Text;
            DateTime executeDate = dtpExecuteDate.Value;
            string takenDept = txbTakenDept.Text.Trim();
            string executeState = txbExecuteState.Text.Trim();
            string executeUser = txbExecuteUser.Text.Trim();
            DateTime nextDate = dtpNextExecuteDate.Value;
            string remark = txbResumeRemark.Text.Trim();

            try
            {
                //查找报表ID
                reportId = GetCurrReportId();
                if (string.IsNullOrWhiteSpace(reportId))
                {
                    labMessage.Text = "未找到相关报表，页码：" + nudPageNo.Value + "，请先创建报表";
                    labMessage.ForeColor = Color.Red;
                    return;
                }

                //判断是否超出模板行数（）
                string sql = string.Format("SELECT Count(1) count FROM A_ResumeMaintenanceD_T WHERE ReportId='{0}' ", reportId);
                DataTable dt2 = DBUtil.GetDataTable(sql);
                int count = Convert.ToInt32(dt2.Rows[0][0]);
                if (count >= 16)
                {
                    labMessage.Text = "当前报表保养、校验记录已达最大值，请先创建新的报表";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                //保存
                sql = string.Format(@"INSERT INTO A_ResumeMaintenanceD_T(ReportId,AssetNo,ExecuteCategory,ExecuteDate,TakenDept,ExecuteState,ExecuteUser,NextExecuteDate,Remark,UpdateTime,UpdateUser)
                                      VALUES('{0}','{1}',N'{2}','{3}',N'{4}',N'{5}',N'{6}','{7}',N'{8}',GETDATE(),'{9}')",
                             reportId, assetNo, executeCategory, executeDate, takenDept, executeState, executeUser, nextDate, remark, BaseInfo.LoginUserNo);
                DBUtil.ExecSQL(sql);

                RefreshDataGrid();
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "保存成功";
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "保存失败";
                LogHelper.Error(typeof(FrmResumeReport), ex.Message);
            }
        }

        private bool CheckResumeForm(ref string msg)
        {
            if (string.IsNullOrWhiteSpace(txbAssetNo.Text.Trim()))
            {
                msg = "资产编号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbExecuteCategory.Text.Trim()))
            {
                msg = "实施类别不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbExecuteUser.Text.Trim()))
            {
                msg = "实施人员不能为空";
                return false;
            }
            return true;
        }
        #endregion

        #region 保存[维修]
        //维修记录保存
        private void btnRepairSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string msg = "";
            //数据检查
            if (!CheckRepairForm(ref msg))
            {
                labMessage.Text = msg;
                labMessage.ForeColor = Color.Red;
                return;
            }

            //基础数据
            string reportId = "";
            string assetNo = txbAssetNo.Text.Trim();
            DateTime repairDate = dtpRepairDate.Value;
            string abnormalDesc = txbAbnormalDesc.Text.Trim();
            string repairReason = txbRepairReason.Text.Trim();
            string repairUser = txbRepairUser.Text.Trim();
            string checkResult = txbCheckResult.Text.Trim();
            string remark = txbRepairRemark.Text.Trim();

            try
            {
                //查找报表ID
                reportId = GetCurrReportId();
                if (string.IsNullOrWhiteSpace(reportId))
                {
                    labMessage.Text = "未找到相关报表，页码：" + nudPageNo.Value + "，请先创建报表";
                    labMessage.ForeColor = Color.Red;
                    return;
                }

                //判断是否超出模板行数（）
                string sql = string.Format("SELECT Count(1) count FROM A_ResumeRepairD_T WHERE ReportId='{0}' ", reportId);
                DataTable dt2 = DBUtil.GetDataTable(sql);
                int count = Convert.ToInt32(dt2.Rows[0][0]);
                if (count >= 7)
                {
                    labMessage.Text = "当前报表维修记录已达最大值，请先创建新的报表";
                    labMessage.ForeColor = Color.Red;
                    return;
                }

                //保存
                sql = string.Format(@"INSERT INTO A_ResumeRepairD_T(ReportId,AssetNo,RepairDate,AbnormalDesc,RepairReason,RepairUser,CheckResult,Remark,UpdateTime,UpdateUser)
                                            VALUES('{0}','{1}','{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',GETDATE(),'{8}')",
                                           reportId, assetNo, repairDate, abnormalDesc, repairReason, repairUser, checkResult, remark, BaseInfo.LoginUserNo);
                DBUtil.ExecSQL(sql);

                RefreshDataGrid();
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "保存成功";
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "保存失败";
                LogHelper.Error(typeof(FrmResumeReport), ex.Message);
            }
        }


        private bool CheckRepairForm(ref string msg)
        {
            if (string.IsNullOrWhiteSpace(txbAssetNo.Text.Trim()))
            {
                msg = "资产编号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbCheckResult.Text.Trim()))
            {
                msg = "验收结果不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbRepairUser.Text.Trim()))
            {
                msg = "维修人员不能为空";
                return false;
            }
            return true;
        }

        #endregion

        #region 事件
        private void swBtnPage_ValueChanged(object sender, EventArgs e)
        {
            SwitchButton sw = (SwitchButton)sender;
            if (sw.Value)
            {
                nudPageNo.Enabled = true;
                btnResumeSave.Enabled = true;
                btnRepairSave.Enabled = true;
            }
            else
            {
                nudPageNo.Enabled = false;
                btnResumeSave.Enabled = false;
                btnRepairSave.Enabled = false;
            }
            RefreshDataGrid();
        }

        private void nudPageNo_ValueChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
        #endregion

        #region 获取当前页码报表ID
        private string GetCurrReportId()
        {
            string sql = string.Format("SELECT Id FROM A_ResumeReport_T WHERE AssetNo='{0}' AND PageNo={1}", txbAssetNo.Text, nudPageNo.Value);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            return dt.Rows[0]["Id"].ToString();
        }

        private int GetMaxPageNo()
        {
            string sql = string.Format("SELECT ISNULL(Max(PageNo),0) as PageNo FROM A_ResumeReport_T WHERE AssetNo='{0}';", txbAssetNo.Text);
            DataTable dt2 = DBUtil.GetDataTable(sql);
            int maxPageNo = Convert.ToInt32(dt2.Rows[0]["PageNo"]);
            labPageNo.Text = "共" + maxPageNo + "页";
            nudPageNo.Maximum = maxPageNo;
            nudPageNo.Value = maxPageNo;
            return maxPageNo;
        }
        #endregion


    }
}
