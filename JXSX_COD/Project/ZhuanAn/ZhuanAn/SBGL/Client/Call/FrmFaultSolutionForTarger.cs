using Call;
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
    public partial class FrmFaultSolutionForTarger : Form
    {

        private ErrorInfo _ErrorInfo;//来源为[呼叫处理完成]时传进来的对象，需做回传处理
        public FrmFaultSolutionForTarger(ErrorInfo errorInfo)
        {
            InitializeComponent();
            _ErrorInfo = errorInfo;
        }

        private void FrmFaultSolutionForTarger_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            tbLine.Text = _ErrorInfo.Line;
            tbxSolver.Text = _ErrorInfo.SolverScanInfo.UserName;
            string sql = string.Format("SELECT TOP 2 * FROM M_ErrorHandleScan_T WHERE ErrorId={0} AND HandlerNo=N'{1}' ORDER BY CreateTime DESC", _ErrorInfo.Id, _ErrorInfo.SolverScanInfo.UserNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count >= 2)
            {
                DateTime start = Convert.ToDateTime(dt.Rows[1]["CreateTime"].ToString());
                DateTime end = Convert.ToDateTime(dt.Rows[0]["CreateTime"].ToString());
                labTotalTime.Text = TimeUtil.ConvertDiffTime(start, end);
            }
        }



        /// <summary>
        /// 单击确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //校验数据
            if (string.IsNullOrWhiteSpace(tbxFault.Text))
            {
                MessageBox.Show("故障内容必须填写", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbxSolution.Text))
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

                string faultContent = tbxFault.Text.Trim();
                string solutionContent = tbxSolution.Text.Trim();
                string sql = string.Format(@"UPDATE M_ErrorRecord_T SET FaultContent = N'{1}',SolutionContent = N'{2}',UpdateUser='{3}',UpdateTime=GETDATE() WHERE	Id={0}",
                                             _ErrorInfo.Id, faultContent, solutionContent, _ErrorInfo.SolverScanInfo.UserNo);
                DBUtil.ExecSQL(sql);
            }
            this.Close();
            this.Dispose();
        }

    }
}
