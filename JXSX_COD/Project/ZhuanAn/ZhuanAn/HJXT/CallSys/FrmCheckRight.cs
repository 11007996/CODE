using CallSys;
using CallSys.Utils;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CallSys
{
    public partial class FrmCheckRight : Form
    {
        public FrmCheckRight()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCheckRight_Load(object sender, EventArgs e)
        {
            if (!VirtualKeyboardHelper.IsKeyboardAttached())
            {
                VirtualKeyboardHelper.ShowVirtualKeyboard();
            }
        }

        /// <summary>
        /// 产线人员勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbLine_CheckedChanged(object sender, EventArgs e)
        {
            tbHandlerNo.Enabled = false;
            tbHandlerPwd.Enabled = false;
        }

        /// <summary>
        /// 处理人员勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbHandler_CheckedChanged(object sender, EventArgs e)
        {
            tbHandlerNo.Enabled = true;
            tbHandlerPwd.Enabled = true;
        }

        /// <summary>
        /// 确定提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                labMessage.Text = "";
                if (tbHandlerNo.Text == "" || tbHandlerPwd.Text == "")
                {
                    labMessage.Text = "工号或密码不能为空";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    string strSQL = string.Format("SELECT HandlerNo,HandlerName,UseFlag,HandlerRight,HandlerDept,HandlerLevel FROM m_HandlerInfo_t WHERE HandlerNo='{0}' AND HandlerPwd='{1}'", tbHandlerNo.Text.Trim(), tbHandlerPwd.Text.Trim());
                    DataTable dt = DBUtil.GetDataTable(strSQL);
                    if (dt == null) return;
                    if (dt.Rows.Count < 1)
                    {
                        labMessage.Text = "工号或密码有误";
                        labMessage.ForeColor = Color.Red;
                        return;
                    }
                    else if (!"Y".Equals(dt.Rows[0]["UseFlag"].ToString()))
                    {
                        labMessage.Text = "账号已被禁用";
                        labMessage.ForeColor = Color.Red;
                        return;
                    }
                    else if (!"A".Equals(dt.Rows[0]["HandlerRight"].ToString()) && !"2".Equals(dt.Rows[0]["HandlerLevel"].ToString()))
                    {
                        labMessage.Text = "账号权限或工程师等级不足";
                        labMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        BaseInfo.LoginHandlerNo = tbHandlerNo.Text.Trim();
                        BaseInfo.LoginHandlerName = dt.Rows[0]["HandlerName"].ToString();
                        BaseInfo.LoginHandlerRight = dt.Rows[0]["HandlerRight"].ToString();
                        BaseInfo.LoginHandlerDept = dt.Rows[0]["HandlerDept"].ToString();
                        LoginFinishEvent();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmCheckRight), null, ex);
                return;
            }
        }

        //登入完成后执行
        public void LoginFinishEvent()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(Auxiliary))
                {
                    Auxiliary a = frm as Auxiliary;
                    a.RefreshNotifyIconText();
                }
            }
        }
    }
}
