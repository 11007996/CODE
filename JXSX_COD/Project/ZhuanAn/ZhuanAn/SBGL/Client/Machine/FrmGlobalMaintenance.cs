using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmGlobalMaintenance : Form
    {
        public FrmGlobalMaintenance()
        {
            InitializeComponent();
        }

        private void FrmGlobalMaintenance_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //年份
            IList<int> years = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                years.Add(DateTime.Now.Year - i);
            }
            cmbYear.DataSource = years;
            cmbYear.Text = DateTime.Now.Year.ToString();
            cmbMonth.Text = DateTime.Now.Month.ToString();

            //用户
            string sql = string.Format("SELECT UserNo,UserName  FROM S_User_T WHERE Dept like N'{0}%'", BaseInfo.Dept);
            DataTable userDT = DBUtil.GetDataTable(sql);
            if (userDT != null)
            {
                foreach (DataRow row in userDT.Rows)
                {
                    chklbUsers.Items.Add(row["UserName"]);
                }
            }
        }

        #region 下拉事件
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth_SelectedIndexChanged(null, null);
        }


        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTimeMark.Items.Clear();
            if (cmbMonth.SelectedIndex > 0)
            {//月
                cmbTimeMark.Items.Add("D-(日)");
                cmbTimeMark.Items.Add("W-(周)");
                cmbTimeMark.Items.Add("M-(月)");
            }
            else
            {//年
                cmbTimeMark.Items.Add("M-(月)");
                cmbTimeMark.Items.Add("Q-(季)");
                cmbTimeMark.Items.Add("Y-(年)");
            }
            cmbTimeMark.SelectedIndex = 0;
        }

        private void cmbTimeMark_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTimeMarkValue.Items.Clear();
            string timeMark = cmbTimeMark.Text.Split('-')[0];
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            int maxTimeValue = 1;
            switch (timeMark)
            {
                case "D":
                    maxTimeValue = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month));
                    break;
                case "W":
                    int days = DateTime.DaysInMonth(Int32.Parse(year), Int32.Parse(month));
                    maxTimeValue = (int)Math.Ceiling(days / 7.0);
                    break;
                case "M":
                    if (!string.IsNullOrWhiteSpace(month))
                        maxTimeValue = 1;//月表
                    else
                        maxTimeValue = 12;//年表
                    break;
                case "Q":
                    maxTimeValue = 4;
                    break;
                case "Y":
                    maxTimeValue = 1;
                    break;
            }

            //添加可用的标记值
            for (int i = 1; i <= maxTimeValue; i++)
            {
                cmbTimeMarkValue.Items.Add(i);
            }
            cmbTimeMarkValue.SelectedIndex = 0;
        }
        #endregion

        #region 更新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //时间检查
            if (!Check())
            {
                labMessage.Text = "日期时间不合法";
                labMessage.ForeColor = Color.Red;
                return;
            }

            //人员检查
            if (chklbUsers.CheckedItems.Count <= 0)
            {
                labMessage.Text = "最少选择一个用户作为保养人";
                labMessage.ForeColor = Color.Red;
                return;
            }
            //参数
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            string timeMark = cmbTimeMark.Text.Split('-')[0];
            string timeMarkVal = cmbTimeMarkValue.Text;
            string[] users = new string[chklbUsers.CheckedItems.Count];
            string isForce = chkIsForce.Checked ? "Y" : "N";
            for (int c = 0; c < chklbUsers.CheckedItems.Count; c++)
            {
                users[c] = chklbUsers.CheckedItems[c].ToString();
            }

            //获取所有资产
            string sql = "SELECT AssetNo FROM A_AssetInfo_T;";
            DataTable assetDT = DBUtil.GetDataTable(sql);

            //遍历资产执行添加保养记录
            if (assetDT != null)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    sql = "";
                    for (int i = 0; i < assetDT.Rows.Count; i++)
                    {
                        sql += string.Format("EXEC AddMaintenanceRecord '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','',''; \r",  assetDT.Rows[i]["AssetNo"], year, month, timeMark, timeMarkVal, users[i % users.Length], BaseInfo.LoginUserNo, isForce);
                        if (i % 200 == 0 || i == (assetDT.Rows.Count - 1))
                        {
                            DBUtil.ExecSQL(sql);
                            sql = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "执行异常，原因："+ex.Message;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            labMessage.ForeColor = Color.Green;
            labMessage.Text = "更新完成";
        }

        private bool Check()
        {
            string timeMark = cmbTimeMark.Text.Split('-')[0];
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            string markValue = cmbTimeMarkValue.Text;
            DateTime now = DateTime.Now;
            if (Int32.Parse(year) < now.Year) return true;//小于当年
            if (Int32.Parse(year) > now.Year) return false;//大于当年
            //日期是否超过当天
            switch (timeMark)
            {
                case "D":
                    DateTime d = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(markValue));
                    if (d > now)
                        return false;
                    break;
                case "W":
                    DateTime w = new DateTime(Int32.Parse(year), Int32.Parse(month), (Int32.Parse(markValue) * 7 - 6));
                    if (w > now)
                        return false;
                    break;
                case "M":
                    DateTime m;
                    if (!string.IsNullOrWhiteSpace(month))
                    {//月表
                        m = new DateTime(Int32.Parse(year), Int32.Parse(month), 1);
                    }
                    else
                    {//年表
                        m = new DateTime(Int32.Parse(year), Int32.Parse(markValue), 1);
                    }
                    if (m > now)
                        return false;
                    break;
                case "Q":
                    DateTime q = new DateTime(Int32.Parse(year), (Int32.Parse(markValue) * 3 - 2), 1);
                    if (q > now)
                        return false;
                    break;
                case "Y":
                    if (Int32.Parse(year) > now.Year)
                        return false;
                    break;
            }
            return true;
        }
        #endregion


    }
}
