using Common.Base;
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
    public partial class FrmHolidayDicData : Form
    {
        private int DicCatalogId;
        public FrmHolidayDicData()
        {
            InitializeComponent();
        }

        private void FrmHolidayDicData_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            dgvHolidayDic.AutoGenerateColumns = false;
            // 字典目录 
            string sql = string.Format("SELECT Id FROM S_SysDic WHERE Catalog='{0}'", DicCatalogEnum.HOLIDAY_DATE_DAY);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DicCatalogId = Convert.ToInt32(dt.Rows[0][0]);
                RefreshDataGrid();
            }
            else
            {
                labMessage.Text = "未维护数据字典：【" + DicCatalogEnum.HOLIDAY_DATE_DAY + "】，请联系管理员";
                labMessage.ForeColor = Color.Red;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //数据检查
            if (DicCatalogId <=0)
            {
                labMessage.Text = "未维护数据字典：【" + DicCatalogEnum.HOLIDAY_DATE_DAY + "】，请联系管理员";
                labMessage.ForeColor = Color.Red;
                return;
            }
            //参数
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            string isHoliday = swBtnHoliday.Value?"Y":"N";
            //检查是否重复
            string sql = string.Format("Select 1 From S_SysDicDetial WHERE CatalogId='{0}' AND DataKey='{1}'", DicCatalogId, date);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                labMessage.Text = "保存失败:重复";
                labMessage.ForeColor = Color.Red;
                return;
            }

            //新增
             sql = string.Format("Insert S_SysDicDetial(CatalogId,DataKey,DataValue,SortNo)Values('{0}','{1}','{2}',1);",DicCatalogId,date,isHoliday);
          
            DBUtil.ExecSQL(sql);
            RefreshDataGrid();
            labMessage.Text = "保存成功";
            labMessage.ForeColor = Color.Green;
            return;
        }


        private void RefreshDataGrid()
        {
            dgvHolidayDic.DataSource = null;
            //字典详情
            string sql = string.Format("SELECT * FROM S_SysDicDetial WHERE CatalogId='{0}' order by DataKey Desc", DicCatalogId);
            DataTable dicDetailDT = DBUtil.GetDataTable(sql);
            dgvHolidayDic.DataSource = dicDetailDT;
        }

        //删除
        private void dgvHolidayDic_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
            labMessage.Text = "";
            DataRow row = ((DataRowView)e.Row.DataBoundItem).Row;
            string date = row["DataKey"].ToString();
            string dicDetialId = row["Id"].ToString();
            if (MessageBox.Show("确定要删除节日【" + date + "】设置吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string sql = string.Format("Delete S_SysDicDetial WHERE Id='{0}'", dicDetialId);
                DBUtil.ExecSQL(sql);
                RefreshDataGrid();
                labMessage.Text = "删除成功";
                labMessage.ForeColor = Color.Green;
            }

        }

    }
}
