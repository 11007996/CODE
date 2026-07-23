using LuxMMS;
using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using ApiManager.AssetSystem;
using System.Threading.Tasks;

namespace LuxMMS
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
            labMessage.Text = "";
            if (!VirtualKeyboardHelper.IsKeyboardAttached())
            {
                VirtualKeyboardHelper.ShowVirtualKeyboard();
            }
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
                if (tbUserNo.Text == "" || tbPwd.Text == "")
                {
                    labMessage.Text = "工号或密码不能为空";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    string strSQL = string.Format("SELECT UserNo,UserName,UseFlag,UserRight,Dept,UserLevel FROM S_User_T WHERE UserNo='{0}' AND Pwd='{1}'", tbUserNo.Text.Trim(), tbPwd.Text.Trim());
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
                    else if (!"A".Equals(dt.Rows[0]["UserRight"].ToString()) && !"2".Equals(dt.Rows[0]["UserLevel"].ToString()))
                    {
                        labMessage.Text = "账号权限或工程师等级不足";
                        labMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        BaseInfo.LoginUserNo = tbUserNo.Text.Trim();
                        BaseInfo.LoginUserName = dt.Rows[0]["UserName"].ToString();
                        BaseInfo.LoginUserRight = dt.Rows[0]["UserRight"].ToString();
                        BaseInfo.LoginDept = dt.Rows[0]["Dept"].ToString();
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
                    string workcode = tbUserNo.Text;
                    string pwd = tbPwd.Text;
                    Task.Run(() =>
                    {
                        AssetSystemApi.InitToken(workcode, pwd);
                    });
                  
                }
            }
        }

    }
}
