using Call.Base;
using Common.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Call
{
    public partial class FrmFaultSolution : Form
    {
        private IList<string> FaultItems = new List<string>();///绑定的故障内容数据源
        private IList<string> SolutionItems = new List<string>();//绑定的解决方案数据源

        private ErrorInfo _ErrorInfo;//来源为[呼叫处理完成]时传进来的对象，需做回传处理
        public FrmFaultSolution(ErrorInfo errorInfo)
        {
            InitializeComponent();
            _ErrorInfo = errorInfo;
        }

        private void FrmFaultSolution_Load(object sender, EventArgs e)
        {

            RefreshMachineType();
            tbMachineNo.Text = _ErrorInfo.Machine;
            txbSolver.Text = _ErrorInfo.SolverScanInfo.UserName;
            cmbFaultType.Text = _ErrorInfo.CallReason;
            cmbMachineType.Text = _ErrorInfo.MachineType;
            labMessage.Text = "";

            //维护人扫描记录
            string sql = string.Format("SELECT TOP 2 * FROM M_ErrorHandleScan_T WHERE ErrorId={0} AND HandlerNo=N'{1}' ORDER BY CreateTime DESC", _ErrorInfo.Id, _ErrorInfo.SolverScanInfo.UserNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count >= 2)
            {
                DateTime start = Convert.ToDateTime(dt.Rows[1]["CreateTime"].ToString());
                DateTime end = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                labTotalTime.Text = TimeUtil.ConvertDiffTime(start, end);
            }

            //初始化品管控件
            sql =string.Format( "SELECT * FROM M_LineQC_T WHERE Line=N'{0}'",_ErrorInfo.Line);
            DataTable qcDT = DBUtil.GetDataTable(sql);
            if (qcDT != null && qcDT.Rows.Count > 0)
            {
                txbQCName.Text = qcDT.Rows[0]["QCName"].ToString();
            }
        }

        private void RefreshMachineType()
        {
            cmbMachineType.DataSource = DBUtil.GetDataTable("SELECT MachineType FROM M_FaultSolution_T");
        }


        /// <summary>
        /// 单击确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //数据检查
            if (!CheckData()) return;
            //返回给呼叫处理窗口
            if (_ErrorInfo == null)
            {
                MessageBox.Show("无法确认,请从呼叫面板确认完成。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _ErrorInfo.Machine = tbMachineNo.Text;
                string QCName = txbQCName.Text;
                int prodCount = (int)nudProdCount.Value;
                int passCount = (int)nudPassCount.Value;
                int NGCount = (int)nudNGCount.Value;
                string errorReason = cmbFaultType.Text.Trim();
                string faultType = cmbMachineType.Text.Trim();
                string faultContent = cmbFault.Text.Trim();
                string solutionContent = cmbSolution.Text.Trim();
                string sql = string.Format(@"UPDATE M_ErrorRecord_T SET ErrorReason = N'{1}',FaultType=N'{2}',FaultContent = N'{3}',SolutionContent = N'{4}', ProdCount={5},PassCount={6},NGCount={7},QCName=N'{8}',UpdateUser='{9}',UpdateTime=GETDATE() WHERE	Id={0}",
                                       _ErrorInfo.Id, errorReason, faultType, faultContent,solutionContent, prodCount,passCount,NGCount,QCName, _ErrorInfo.SolverScanInfo.UserNo);
                DBUtil.ExecSQL(sql);
            }
            this.Close();
            this.Dispose();
        }

        private bool CheckData()
        {
            //校验数据
            if (nudProdCount.Text.Trim().Length == 0)
            {
                MessageBox.Show("调机品数不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nudProdCount.Value != (nudPassCount.Value + nudNGCount.Value))
            {
                MessageBox.Show("调机品数不等于（良品数+不良品数）", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbFaultType.Text))
            {
                MessageBox.Show("故障类别不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbMachineType.Text))
            {
                MessageBox.Show("机台类型不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbFault.Text))
            {
                MessageBox.Show("故障内容必须填写", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmbSolution.Text))
            {
                MessageBox.Show("解决方案必须填写", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #region 事件
        private void cmbFaultType_TextChanged(object sender, EventArgs e)
        {
            string faultType = cmbFaultType.Text.Trim();
            switch (faultType)
            {
                case "机台故障":
                    cmbMachineType.Text = _ErrorInfo.MachineType;
                    break;
                case "换线":
                    cmbMachineType.Text = "换线";
                    break;
                case "操作异常":
                    cmbMachineType.Text = "操作异常";
                    break;
            }
        }

        private void cmbMachineType_TextChanged(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string machineType = cmbMachineType.Text.Trim();
            if (string.IsNullOrWhiteSpace(machineType))
            {
                labMessage.Text = "机台类型不能为空";
                return;
            }
            FaultItems.Clear();
            SolutionItems.Clear();
            lsbFaultItems.DataSource = null;
            lsbSolutionItems.DataSource = null;
            cmbFault.DataSource = null;
            cmbSolution.DataSource = null;
            string strSql = string.Format("SELECT MachineType,FaultType,FaultItems,SolutionItems FROM M_FaultSolution_T WHERE MachineType='{0}'", machineType);
            DataTable dt = DBUtil.GetDataTable(strSql);
            if (dt != null && dt.Rows.Count == 1)
            {
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["FaultItems"].ToString()))
                    FaultItems = dt.Rows[0]["FaultItems"].ToString().Split('/').ToList();
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SolutionItems"].ToString()))
                    SolutionItems = dt.Rows[0]["SolutionItems"].ToString().Split('/').ToList();
                lsbFaultItems.DataSource = FaultItems;
                lsbSolutionItems.DataSource = SolutionItems;
                cmbFault.DataSource = FaultItems;
                cmbSolution.DataSource = SolutionItems;
            }
        }


        private void cmbFaultShort_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbFaultItems.SelectedIndex = cmbFault.SelectedIndex;
            lsbSolutionItems.SelectedIndex = cmbFault.SelectedIndex;
        }


        private void cmbSolutionShort_SelectedIndexChanged(object sender, EventArgs e)
        {
            lsbFaultItems.SelectedIndex = cmbSolution.SelectedIndex;
            lsbSolutionItems.SelectedIndex = cmbSolution.SelectedIndex;
        }
        #endregion

    }
}
