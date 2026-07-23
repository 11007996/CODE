using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encrypt
{
    public partial class FrmDb : Form
    {
        public FrmDb()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string dataSource = txtServer.Text.Trim();
            string dbCatalog = txtDb.Text.Trim();
            string uid = txtUserName.Text.Trim();
            string passWord = txtPassword.Text.Trim();

            if (!CheckEmpty()) return;
            //passWord = GetMd5_32byte(passWord);   //MD5加密
            passWord = DESEncrypt.Encrypt(passWord, "luxshare"); //DES加密
            string connectStr= "data source={0};initial catalog={1};uid={2};pwd={3}";
            connectStr = string.Format(connectStr, dataSource, dbCatalog, uid, passWord);
            txtconStr.Text = connectStr;
           // string aa = DESEncrypt.Decrypt(passWord, "luxshare");//Des解密
        }

        #region MD5加密算法
        private string GetMd5_16byte(string str)
        {
            string md5Pwd = string.Empty;

            //使用加密服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //将指定的字节子数组中的每个元素的数值转换为它的等效十六制字符串表示形式
            md5Pwd = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
            md5Pwd = md5Pwd.Replace("-", "");
            return md5Pwd;
        }

        private string GetMd5_32byte(string str)
        {
            string pwd = string.Empty;
            //实例化一个md5对象
            MD5 md5 = MD5.Create();

            //加密后是一个字节类型的数组 这里要注意编码UTF8/Unicode的选择
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            //通过使用循环 将字节类型的数组转换为字符串 此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                //将得到的字符串使用十六进制类型格式 格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字母
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }
        #endregion

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPassword.PasswordChar = new char();
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private Boolean CheckEmpty()
        {
            if (txtServer.Text.Trim() == "" || txtDb.Text.Trim() == "" || txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("数据库信息不完整", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
