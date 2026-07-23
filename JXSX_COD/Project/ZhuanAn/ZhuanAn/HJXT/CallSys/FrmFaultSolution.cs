using CallSys;
using CallSys.Base;
using CallSys.Utils;
using Common.Utils;
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

namespace CallSys
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
            labHandler.Text = _ErrorInfo.SolverScanInfo.HandlerName;
            cmbFaultType.Text = _ErrorInfo.CallReason;
            cmbMachineType.Text = _ErrorInfo.MachineType;
            string sql = string.Format("SELECT TOP 2 * FROM M_ErrorHandleScan_T WHERE ErrorId={0} AND HandlerNo=N'{1}' ORDER BY CreateTime DESC", _ErrorInfo.Id, _ErrorInfo.SolverScanInfo.HandlerNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count >= 2)
            {
                DateTime start = Convert.ToDateTime(dt.Rows[1]["CreateTime"].ToString());
                DateTime end = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                labTotalTime.Text = TimeUtil.ConvertDiffTime(start, end);
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
            //校验数据
            if (nudProdCount.Text.Trim().Length == 0)
            {
                MessageBox.Show("制品跟踪数不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbFaultType.Text))
            {
                MessageBox.Show("故障类别不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbMachineType.Text))
            {
                MessageBox.Show("机台类型不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbFault.Text))
            {
                MessageBox.Show("故障内容必须填写", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbSolution.Text))
            {
                MessageBox.Show("解决方案必须填写", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //返回给呼叫处理窗口
            if (_ErrorInfo == null)
            {
                MessageBox.Show("无法确认,请从呼叫面板确认完成。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _ErrorInfo.Machine = tbMachineNo.Text;
                _ErrorInfo.ProdCount = Int32.Parse(nudProdCount.Text);
                _ErrorInfo.ErrorReason = cmbFaultType.Text.Trim();
                _ErrorInfo.FaultType = cmbMachineType.Text.Trim();
                _ErrorInfo.FaultContent = lsbFaultItems.Text.Trim();
                _ErrorInfo.SolutionContent = lsbSolutionItems.Text.Trim();
                FrmMaster fm = (FrmMaster)this.Owner;
                string msg = "";
                fm.SolveError_Handle(Int32.Parse(nudProdCount.Text), cmbFaultType.Text.Trim(), cmbMachineType.Text.Trim(), cmbFault.Text.Trim(), cmbSolution.Text.Trim(), ref msg);
                if (msg.Length > 0)
                {
                    MessageBox.Show(msg, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            this.Close();
            this.Dispose();
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
