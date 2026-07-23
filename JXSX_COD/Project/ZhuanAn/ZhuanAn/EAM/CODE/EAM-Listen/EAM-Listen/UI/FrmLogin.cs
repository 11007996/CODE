using EAM.Listen.Common.Utils;
using EAM.Listen.Model;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        //登入完成后执行
        public void LoginFinishEvent()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == typeof(MainForm))
                {
                    MainForm a = frm as MainForm;
                    a.RefreshNotifyIconText();
                }
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
                if (tbUserName.Text == "" || tbPassword.Text == "")
                {
                    labMessage.Text = "工号或密码不能为空";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    string username = ConfigurationManager.AppSettings["LoginUserName"];
                    string password = ConfigurationManager.AppSettings["LoginPassword"];
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return;
                    if (username != tbUserName.Text || password != tbPassword.Text)
                    {
                        labMessage.Text = "工号或密码有误";
                        labMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        LoginInfo.LoginUserName = tbUserName.Text;
                        LoginFinishEvent();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmLogin), null, ex);
                return;
            }
        }
    }
}