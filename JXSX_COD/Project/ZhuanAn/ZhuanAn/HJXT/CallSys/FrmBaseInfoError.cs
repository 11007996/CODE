using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using CallSys.Utils;
using CallSys.Base;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Utils;
using Common.Base;
using Common;


namespace CallSys
{
    public partial class FrmBaseInfoError : Form
    {
        private OperateState operateFlag = OperateState.Defualt;//故障基础信息操作状态标记

        public FrmBaseInfoError()
        {
            InitializeComponent();
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseInfoError_Load(object sender, EventArgs e)
        {
            //------------初始化异常信息选项卡组件------------
            //菜单
            tsmErrorDelete.Visible = BaseInfo.LoginHandlerRight == "A" ? true : false;
            //所有工程师
            string sql = "SELECT HandlerNo,HandlerName  FROM M_HandlerInfo_T ";
            DataTable dtHandlers = DBUtil.GetDataTable(sql);
            List<string> handlers = null;
            if (dtHandlers != null)
            {
                handlers = dtHandlers.AsEnumerable().Select(t => t.Field<string>("HandlerName")).ToList();
                handlers.Insert(0, "");
            }
            cmbErrorHandler.DataSource = handlers;
            //高级工程师
            sql = "select HandlerNo,HandlerName  FROM M_HandlerInfo_T WHERE HandlerLevel='2'";
            DataTable dtHelpers = DBUtil.GetDataTable(sql);
            List<string> helpers = null;
            if (dtHelpers != null)
            {
                helpers = dtHelpers.AsEnumerable().Select(t => t.Field<string>("HandlerName")).ToList();
                helpers.Insert(0, "");
            }
            cmbErrorHelper.DataSource = helpers;
            //故障原因
            IList errorReason = new List<string>();
            errorReason.Add("");
            errorReason.Add("机台故障");
            errorReason.Add("换线");
            cmbErrorReason.DataSource = errorReason;
            //机台类型
            sql = "select MachineType  FROM M_FaultSolution_T ";
            DataTable dtMachineType = DBUtil.GetDataTable(sql);
            List<string> machines = null;
            if (dtMachineType != null)
            {
                machines = dtMachineType.AsEnumerable().Select(t => t.Field<string>("MachineType")).ToList();
                machines.Insert(0, "");
            }
            cmbMachineType.DataSource = machines;
            //状态
            IList errorStateList = new List<string>();
            errorStateList.Add("");
            errorStateList.Add("未完成");
            errorStateList.Add("已完成");
            errorStateList.Add("解除呼叫");
            cmbErrorStatus.DataSource = errorStateList;
            //区域
            sql = "SELECT DISTINCT Area  FROM M_LineInfo_T ";
            DataTable dtArea = DBUtil.GetDataTable(sql);
            cmbArea.Items.Add("");
            if (dtArea != null)
            {
                foreach (DataRow row in dtArea.Rows)
                {
                    cmbArea.Items.Add(row["Area"].ToString());
                    cmbArea.Text = BaseInfo.Area;
                }
            }

            //时间
            dtpComeTimeStart.Value = TimeUtil.Now;
            //数据表格
            dgvError.AutoGenerateColumns = false;
            //刷新
            RefreshErrorControlState();
            RefreshErrorDataGrid();
        }
        #endregion


        #region 查询
        private void tsmErrorSelect_Click(object sender, EventArgs e)
        {
            FormUtil.ClearControls(gbError);
            operateFlag = OperateState.Query;
            RefreshErrorControlState();
        }
        #endregion

        #region 刷新
        private void tsmErrorRefresh_Click(object sender, EventArgs e)
        {

            try
            {
                labErrorMessage.Text = "";
                RefreshErrorDataGrid();
                labErrorMessage.ForeColor = Color.Green;
                labErrorMessage.Text = "数据已加载完成";
            }
            catch (Exception ex)
            {
                labErrorMessage.ForeColor = Color.Red;
                labErrorMessage.Text = "数据加载失败";
                LogHelper.Error(typeof(FrmBaseInfoError), null, ex);
            }
        }
        #endregion

        #region 维护
        private void tsmErrorUpdate_Click(object sender, EventArgs e)
        {
            labErrorMessage.Text = "";
            if (dgvError.CurrentRow == null || dgvError.CurrentRow.Index < 0 || tbErrorId.Text == "")
            {
                labErrorMessage.ForeColor = Color.Red;
                labErrorMessage.Text = "请至少选择一条数据";
                return;
            }
            operateFlag = OperateState.Edit;
            RefreshErrorControlState();
        }
        #endregion

        #region 保存
        private void tsmErrorSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckErrorData() == false)
                {
                    return;
                }
                string strSql = string.Format("UPDATE M_ErrorRecord_T SET ErrorReason=N'{1}',ProdCount=N'{2}',FaultType=N'{3}',FaultContent=N'{4}',SolutionContent=N'{5}',UpdateUser=N'{6}',UpdateTime=GETDATE() WHERE Id='{0}'",
                 tbErrorId.Text, cmbErrorReason.Text.Trim(), nudProdCount.Text.Trim(), cmbMachineType.Text.Trim(), tbFaultContent.Text.Trim(), tbSolutionContent.Text.Trim(), BaseInfo.LoginHandlerNo);
                DBUtil.ExecSQL(strSql);
                labErrorMessage.Text = "维修成功";

                RefreshErrorDataGrid();
                FormUtil.ClearControls(gbError);
                operateFlag = OperateState.Defualt;
                RefreshErrorControlState();
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoError), null, ex);
            }
        }
        #endregion

        #region
        private void tsmErrorDelete_Click(object sender, EventArgs e)
        {
            labErrorMessage.Text = "";
            if (dgvError.CurrentRow == null || dgvError.CurrentRow.Index < 0 || tbErrorId.Text == "")
            {
                labErrorMessage.ForeColor = Color.Red;
                labErrorMessage.Text = "请至少选择一条数据";
                return;
            }
            if (MessageBox.Show("确定要删除【" + tbErrorId.Text + "】这条记录吗?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = string.Format("DELETE M_ErrorRecord_T WHERE Id={0}", tbErrorId.Text);
                DBUtil.ExecSQL(sql);
                labErrorMessage.Text = "删除成功";
                FormUtil.ClearControls(gbError);
                RefreshErrorDataGrid();
            }
        }
        #endregion

        #region 返回
        private void tsmErrorBack_Click(object sender, EventArgs e)
        {
            FormUtil.ClearControls(gbError);
            operateFlag = OperateState.Defualt;
            RefreshErrorControlState();
        }
        #endregion

        #region 导出
        private void tsmErrorExport_Click(object sender, EventArgs e)
        {
            try
            {
                labErrorMessage.Text = "";
                if (dgvError.Rows.Count < 1)
                {
                    labErrorMessage.ForeColor = Color.Red;
                    labErrorMessage.Text = "请查询出要导出的数据";
                    return;
                }
                DataTable dt = new DataTable();
                // 列名称
                for (int c = 0; c < dgvError.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvError.Columns[c].Name.ToString());
                    dc.Caption = dgvError.Columns[c].HeaderText.ToString();
                    //特殊类型列
                    if (dc.Caption == "索引" || dc.Caption == "等待时间" || dc.Caption == "处理时间" || dc.Caption == "支援到时" || dc.Caption == "支援时间" || dc.Caption == "制品数")
                    {
                        dc.DataType = typeof(int);
                    }
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvError.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvError.Columns.Count; c++)
                    {
                        //特殊列，时间计算
                        if (dgvError.Columns[c].HeaderText == "等待时间")
                        {
                            DataRow row = ((DataRowView)dgvError.Rows[r].DataBoundItem).Row;
                            dr[c] = GetTimeSpan(row["StartTime"], row["ComeTime"]);
                        }
                        else if (dgvError.Columns[c].HeaderText == "处理时间")
                        {
                            DataRow row = ((DataRowView)dgvError.Rows[r].DataBoundItem).Row;
                            if (row["CallHelpTime"] == null || row["CallHelpTime"] == DBNull.Value)
                            {
                                dr[c] = GetTimeSpan(row["ComeTime"], row["EndTime"]);
                            }
                            else
                            {
                                dr[c] = GetTimeSpan(row["ComeTime"], row["CallHelpTime"]);
                            }
                        }
                        else if (dgvError.Columns[c].HeaderText == "支援到时")
                        {
                            DataRow row = ((DataRowView)dgvError.Rows[r].DataBoundItem).Row;
                            dr[c] = GetTimeSpan(row["CallHelpTime"], row["HelpComeTime"]);
                        }
                        else if (dgvError.Columns[c].HeaderText == "支援时间")
                        {
                            DataRow row = ((DataRowView)dgvError.Rows[r].DataBoundItem).Row;
                            dr[c] = GetTimeSpan(row["HelpComeTime"], row["EndTime"]);
                        }
                        else if (dgvError.Columns[c].HeaderText == "制品数")
                        {   //特殊列，制品数
                            dr[c] = string.IsNullOrWhiteSpace(dgvError.Rows[r].Cells[c].EditedFormattedValue.ToString()) ? 0 : dgvError.Rows[r].Cells[c].EditedFormattedValue;
                        }
                        else
                        {
                            dr[c] = Convert.ToString(dgvError.Rows[r].Cells[c].EditedFormattedValue).Replace("\r\n", "");
                        }
                    }
                    dt.Rows.Add(dr);
                }
                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "设备维护信息";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoError), null, ex);
            }
        }
        //计算两个时间之间的秒数
        private int GetTimeSpan(object start, object end)
        {
            if (start == DBNull.Value || start == null || string.IsNullOrWhiteSpace(start.ToString())) return 0;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime;
            if (end == DBNull.Value || end == null || string.IsNullOrWhiteSpace(end.ToString()))
            {
                endTime = TimeUtil.Now;
            }
            else
            {
                endTime = Convert.ToDateTime(end);
            }
            TimeSpan span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return (int)span.TotalSeconds;
        }
        #endregion

        #region 重置看板
        private void tsmErrorReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要清除【" + BaseInfo.Area + "区】看板中的所有信息吗?", "呼叫清除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    labErrorMessage.Text = "";
                    string strSQL = string.Format("UPDATE M_ErrorRecord_T SET EndTime=GETDATE(),Status='N',UpdateUser='{0}',UpdateTime=GETDATE() WHERE Area='{1}' AND Status<>'N' AND Status<>'Y' ", BaseInfo.LoginHandlerNo, BaseInfo.Area);
                    DBUtil.ExecSQL(strSQL);
                    labErrorMessage.Text = "重置成功";
                    RefreshErrorDataGrid();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoError), null, ex);
            }
        }
        #endregion


        #region 消息设置
        private void tsmMsgPushSet_Click(object sender, EventArgs e)
        {
            FrmMsgPushSet frm = new FrmMsgPushSet();
            frm.ShowDialog();
        }
        #endregion

        #region 其他
        #region 刷新数据表格
        private void RefreshErrorDataGrid()
        {
            string strSQL = string.Format(@"SELECT e.Id,e.Line+e.Machine MachineNo,'' WaitedTime,'' SolvedTime,h1.HandlerName HandlerName,h2.HandlerName HelperName,
                        e.ErrorReason,e.FaultContent,e.SolutionContent,e.ProdCount,e.ComeTime,e.SolverName,e.FaultType,e.Status,e.StartTime,e.CallHelpTime,ehs.CreateTime as HelpComeTime,e.EndTime
                        FROM M_ErrorRecord_T  e 
                        LEFT JOIN M_HandlerInfo_T h1 ON e.HandlerNo = h1.HandlerNo
                        LEFT JOIN M_HandlerInfo_T h2 ON e.HelperNo = h2.HandlerNo
                        LEFT JOIN M_ErrorHandleScan_T  ehs ON ehs.ErrorStatus='D' AND ehs.ErrorId=e.Id AND ehs.HandlerNo=e.HelperNo
                        WHERE 1=1  ");
            if (cmbErrorStatus.Text != "")
            {
                if (cmbErrorStatus.Text == "未完成")
                {
                    strSQL += " AND e.Status<>'Y' AND e.Status<>'N' ";
                }
                else if (cmbErrorStatus.Text == "已完成")
                {
                    strSQL += " AND e.Status='Y' ";
                }
                else if (cmbErrorStatus.Text == "解除呼叫")
                {
                    strSQL += " AND e.Status='N' ";
                }
            }
            if (!string.IsNullOrWhiteSpace(cmbArea.Text))
            {
                strSQL += " AND e.Area='" + cmbArea.Text.Trim() + "' ";
            }
            if (tbMachineNo.Text != "")
            {
                strSQL += " AND e.Line+e.Machine like N'%" + tbMachineNo.Text.Trim() + "%'";
            }
            if (cmbErrorHandler.Text != "")
            {
                strSQL += " AND h1.HandlerName like N'%" + cmbErrorHandler.Text + "%'";
            }
            if (cmbErrorHelper.Text != "")
            {
                strSQL += " AND h2.HandlerName like  N'%" + cmbErrorHelper.Text + "%'";
            }
            if (cmbMachineType.Text != "")
            {
                strSQL += " AND FaultType like N'%" + cmbMachineType.Text.Trim() + "%'";
            }
            strSQL += string.Format(" AND ComeTime between '{0}' AND '{1}' order by  ComeTime desc", dtpComeTimeStart.Value.ToString("yyyy-MM-dd 00:00:00"), Convert.ToDateTime(dtpComeTimeEnd.Value.ToString()).AddDays(1).Date);
            dgvError.DataSource = DBUtil.GetDataTable(strSQL);
        }
        #endregion

        #region 单击格单元格
        private void dgvError_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    int rowIndex = dgvError.CurrentCell.RowIndex;//选中行的索引
                    tbErrorId.Text = dgvError.Rows[rowIndex].Cells["dgcErrorId"].Value.ToString();//处理信息的ID
                    cmbErrorHandler.Text = dgvError.Rows[rowIndex].Cells["dgcErrorHandlerName"].Value.ToString();
                    cmbErrorHelper.Text = dgvError.Rows[rowIndex].Cells["dgcHelperName"].Value.ToString();
                    tbMachineNo.Text = dgvError.Rows[rowIndex].Cells["dgcMachineNo"].Value.ToString();
                    cmbErrorReason.Text = dgvError.Rows[rowIndex].Cells["dgcErrorReason"].Value.ToString();
                    nudProdCount.Text = dgvError.Rows[rowIndex].Cells["dgcProdCount"].Value.ToString();
                    cmbMachineType.Text = dgvError.Rows[rowIndex].Cells["dgcFaultType"].Value.ToString();
                    tbFaultContent.Text = dgvError.Rows[rowIndex].Cells["dgcFaultContent"].Value.ToString();
                    tbSolutionContent.Text = dgvError.Rows[rowIndex].Cells["dgcSolutionContent"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoError), null, ex);
            }
        }
        #endregion

        #region 格式化单元格
        private void dgvError_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //计算等待时间
            if (dgvError.Columns[e.ColumnIndex].HeaderText.Equals("等待时间"))
            {
                DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                e.Value = GetConvertTime(row["StartTime"], row["ComeTime"]);
                return;
            }
            //计算处理时间
            if (dgvError.Columns[e.ColumnIndex].HeaderText.Equals("处理时间"))
            {
                DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                if (row["CallHelpTime"] == null || row["CallHelpTime"] == DBNull.Value)
                {
                    e.Value = GetConvertTime(row["ComeTime"], row["EndTime"]);
                }
                else
                {
                    e.Value = GetConvertTime(row["ComeTime"], row["CallHelpTime"]);
                }
                return;
            }
            //计算支援到场时间
            if (dgvError.Columns[e.ColumnIndex].HeaderText.Equals("支援到时"))
            {
                DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                e.Value = GetConvertTime(row["CallHelpTime"], row["HelpComeTime"]);
                return;
            }
            //支援人处理时间
            if (dgvError.Columns[e.ColumnIndex].HeaderText.Equals("支援时间"))
            {
                DataRow row = ((DataRowView)dgvError.Rows[e.RowIndex].DataBoundItem).Row;
                e.Value = GetConvertTime(row["HelpComeTime"], row["EndTime"]);
                return;
            }
            //状态转换
            if (dgvError.Columns[e.ColumnIndex].HeaderText.Equals("状态"))
            {
                string status = dgvError.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                switch (status)
                {
                    case "Y":
                        e.Value = "已完成";
                        break;
                    case "N":
                        e.Value = "呼叫解除";
                        break;
                    default:
                        e.Value = "未完成";
                        e.CellStyle.BackColor = Color.Red;
                        break;
                }
                return;
            }
        }

        //获取两个时间差，并转换。结束时间为空时，以当前时间计算
        private string GetConvertTime(object start, object end)
        {
            if (start == DBNull.Value || start == null || string.IsNullOrWhiteSpace(start.ToString())) return null;
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime;
            if (end == DBNull.Value || end == null || string.IsNullOrWhiteSpace(end.ToString()))
            {
                endTime = TimeUtil.Now;
            }
            else
            {
                endTime = Convert.ToDateTime(end);
            }
            return TimeUtil.ConvertDiffTime(startTime, endTime);
        }
        #endregion

        #region 检查数据
        /// <summary>
        /// 基础信息填写是否完整
        /// </summary>
        /// <returns></returns>
        public bool CheckErrorData()
        {
            if (string.IsNullOrWhiteSpace(cmbErrorReason.Text))
            {
                labErrorMessage.Text = "故障原因不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(nudProdCount.Text))
            {
                labErrorMessage.Text = "制品跟踪确认LOT数及本数不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbMachineType.Text))
            {
                labErrorMessage.Text = "故障类别不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbFaultContent.Text))
            {
                labErrorMessage.Text = "故障内容不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbSolutionContent.Text))
            {
                labErrorMessage.Text = "故障解决方案不能为空";
                return false;
            }
            return true;
        }
        #endregion

        #region 刷新控件状态
        /// <summary>
        /// 刷新控件状态
        /// </summary>
        private void RefreshErrorControlState()
        {
            switch (operateFlag)
            {
                case OperateState.Defualt:
                    tsmErrorSelect.Enabled = true;
                    tsmErrorRefresh.Enabled = true;
                    tsmErrorUpdate.Enabled = true;
                    tsmErrorSave.Enabled = false;
                    tsmErrorBack.Enabled = false;
                    tsmErrorExport.Enabled = true;
                    tsmErrorReset.Enabled = true;


                    tbMachineNo.Enabled = true;
                    cmbErrorHandler.Enabled = true;
                    cmbErrorHelper.Enabled = true;
                    cmbErrorStatus.Enabled = true;
                    cmbErrorReason.Enabled = false;
                    nudProdCount.Enabled = false;
                    cmbMachineType.Enabled = true;
                    tbFaultContent.Enabled = false;
                    tbSolutionContent.Enabled = false;
                    dtpComeTimeStart.Enabled = true;
                    dtpComeTimeEnd.Enabled = true;
                    break;
                case OperateState.Edit:
                    tsmErrorSelect.Enabled = false;
                    tsmErrorRefresh.Enabled = false;
                    tsmErrorUpdate.Enabled = false;
                    tsmErrorSave.Enabled = true;
                    tsmErrorBack.Enabled = true;
                    tsmErrorExport.Enabled = true;
                    tsmErrorReset.Enabled = true;

                    tbMachineNo.Enabled = false;
                    cmbErrorHandler.Enabled = false;
                    cmbErrorHelper.Enabled = false;
                    cmbErrorStatus.Enabled = false;
                    cmbErrorReason.Enabled = true;
                    nudProdCount.Enabled = true;
                    cmbMachineType.Enabled = true;
                    tbFaultContent.Enabled = true;
                    tbSolutionContent.Enabled = true;
                    dtpComeTimeStart.Enabled = false;
                    dtpComeTimeEnd.Enabled = false;
                    break;
                case OperateState.Query:
                    tsmErrorSelect.Enabled = false;
                    tsmErrorRefresh.Enabled = true;
                    tsmErrorUpdate.Enabled = false;
                    tsmErrorSave.Enabled = false;
                    tsmErrorBack.Enabled = true;
                    tsmErrorExport.Enabled = true;
                    tsmErrorReset.Enabled = true;

                    tbMachineNo.Enabled = true;
                    cmbErrorHandler.Enabled = true;
                    cmbErrorHelper.Enabled = true;
                    cmbErrorStatus.Enabled = true;
                    cmbErrorReason.Enabled = false;
                    nudProdCount.Enabled = false;
                    cmbMachineType.Enabled = true;
                    tbFaultContent.Enabled = false;
                    tbSolutionContent.Enabled = false;
                    dtpComeTimeStart.Enabled = true;
                    dtpComeTimeEnd.Enabled = true;
                    break;
            }
        }
        #endregion
        #endregion

    }
}
