using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Net;
using System.Xml;


namespace PMS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.ActiveControl = txtUserName;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userID = txtUserName.Text.Trim().ToString();
            string password = txtPassWord.Text.Trim().ToString();
            int useResult;
            if (txtUserName.Text == "") 
            {
                txtlogMessage.Visible = true;
                txtlogMessage.ForeColor = System.Drawing.Color.Red;
                txtlogMessage.Text = "用户名不能为空";
                return;
            }
            if (txtPassWord.Text =="")
            {
                txtlogMessage.Visible = true;
                txtlogMessage.ForeColor = System.Drawing.Color.Red;
                txtlogMessage.Text = "密码不能为空";
                return;
            }
            useResult = checkRight(userID, password);
            if (useResult == 2)
            {
                frmMain mainfrm = new frmMain();
                sysInformation.sysUserid = userID;
                sysInformation.sysLogTime = DateTime.Now.ToString();
                System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                IPAddress[] remoteIP = IpEntry.AddressList;
                sysInformation.sysLogIP = remoteIP[1].ToString();
                modify();
                mainfrm.Show();
               this.Hide();
            }
            else if (useResult == 1)
            {
                frmMain mainfrm = new frmMain();
                sysInformation.sysUserid = userID;
                sysInformation.sysLogTime = DateTime.Now.ToString();
                System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                IPAddress[] remoteIP = IpEntry.AddressList;
                sysInformation.sysLogIP = remoteIP[1].ToString();
                modify();
                mainfrm.Show();
                this.Hide();
            }
            else
            {
                txtPassWord.Clear();
                txtPassWord.Focus();
                string err = sysInformation.msgshow("m001");
                MessageBox.Show(err,"提示信息",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //txtlogMessage.Text = "用户无权限或密码错误";
            }
        }
        /// <summary>
        /// 验证用户密码，确认登录权限
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private int checkRight(string username,string password)
        {
            string passValue = sysInformation.GetMD5(password);
            string sqlstr = "select password,Usey from tb_User where userID = " + username;
            DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //string sqlPassword = dt.Columns[0];
                string sqlPassword = dt.Rows[0][0].ToString();
                string sqlUsey = dt.Rows[0][1].ToString();
                if (sqlUsey == "N")
                {
                    string err = sysInformation.msgshow("m012");
                    MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }
                if (sqlPassword == passValue && sqlUsey == "G")
                {
                    return 2;
                }
                else if (sqlPassword == passValue)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //string err = sysInformation.msgshow("m002");
                //MessageBox.Show(err, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }
        
       

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load("Login.xml");
            XmlNode Xdn = xmlfile.SelectSingleNode("fileList/loginName");
            txtUserName.Text = Xdn.InnerText;
            //XmlNodeList XdL = Xdn.ChildNodes;
            //foreach( XmlNode XdNo in XdL)
            //{
            //   txtUserName.Text = XdNo.InnerText;
            //}
        }
        private void modify()
        {
            XmlDocument xmlfile = new XmlDocument();
            xmlfile.Load("Login.xml");
            XmlNode Xdn = xmlfile.SelectSingleNode("fileList/loginName");
            Xdn.InnerText = sysInformation.sysUserid;
            xmlfile.Save("Login.xml");
        }

        private void txtPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin_Click(this, null);
            }
        }

    }
}
