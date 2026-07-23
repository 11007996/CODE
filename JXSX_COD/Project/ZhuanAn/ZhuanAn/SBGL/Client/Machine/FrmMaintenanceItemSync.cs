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
    public partial class FrmMaintenanceItemSync : Form
    {

        public FrmMaintenanceItemSync()
        {
            InitializeComponent();
        }


        private void FrmMaintenanceItemSync_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";

            //年份
            IList<int> years = new List<int>();
            for (int i = -1; i < 3; i++)
            {
                years.Add(DateTime.Now.Year - i);
            }
            cmbYear.DataSource = years;
            cmbYear.Text = DateTime.Now.Year.ToString();
        }


        #region 更新
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //参数检查
            if (cmbTimeMark.SelectedIndex < 0)
            {
                labMessage.Text = "未选中任意一个日期标志";
                labMessage.ForeColor = Color.Red;
                return;
            }

            //参数
            string year = cmbYear.Text;
            string timeMark = cmbTimeMark.Text;
            string isSyncDetial = chkAddV.Checked ? "Y" : "N";

            try
            {
                this.Cursor = Cursors.WaitCursor;
                //遍历资产名称保养报表
                string sql = "";
                sql += string.Format("EXEC  [MaintenanceItemSync] '{0}' ,'{1}' ,'{2}' ,'{3}','' ,'';",  year, timeMark, isSyncDetial, BaseInfo.LoginUserNo);
                DBUtil.ExecSQL(sql);
              
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "更新完成";
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "执行异常，原因：" + ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

    }
}
