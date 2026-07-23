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
    public partial class FrmGlobalCreateReport : Form
    {
        public FrmGlobalCreateReport()
        {
            InitializeComponent();
        }

        private void FrmGlobalCreateReport_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            chkAll.Checked = false;
            //年份
            IList<int> years = new List<int>();
            for (int i = -1; i < 3; i++)
            {
                years.Add(DateTime.Now.Year - i);
            }
            cmbYear.DataSource = years;
            cmbYear.Text = DateTime.Now.Year.ToString();
            //月份
            cmbMonth.Text = DateTime.Now.Month.ToString();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                cmbMonth.Enabled = false;
            }
            else
            {
                cmbMonth.Enabled = true;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //数据
            string year = cmbYear.Text;//年份
            List<string> months = new List<string>();//月份值
            string tip = "";//提示信息
            if (chkAll.Checked)
            {
                tip = "确定要创建所有资产的【" + cmbYear.Text + "】年的所有保养报表吗?";
                for (int i = 0; i < cmbMonth.Items.Count; i++)
                {
                    months.Add(cmbMonth.Items[i].ToString());
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
                    //月
                    tip = "确定要创建所有资产的【" + cmbYear.Text + "】年【" + cmbMonth.Text + "】月的所有保养报表吗?";
                else
                    //年
                    tip = "确定要创建所有资产的【" + cmbYear.Text + "】年的年保养报表吗?";
                months.Add(cmbMonth.Text);
            }

            //弹窗确认
            if (MessageBox.Show(tip, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //参数
                string sql = "";

                //资产遍历
                foreach (string month in months)
                {
                    //删除原有的
                    sql += string.Format("DELETE A_MaintenanceReport_T WHERE [Year]='{0}' AND ISNULL([Month],'')='{1}';", year, month);
                    //插入新的
                    sql += string.Format("INSERT INTO A_MaintenanceReport_T(AssetNo,[Year],[Month]) SELECT AssetNo,'{0}',NULLIF('{1}','') FROM A_AssetInfo_T;", year, month);
                }
                DBUtil.ExecSQL(sql);
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "创建失败,原因：" + ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            //刷新结果
            labMessage.ForeColor = Color.Green;
            labMessage.Text = "创建完成";
        }


    }
}
