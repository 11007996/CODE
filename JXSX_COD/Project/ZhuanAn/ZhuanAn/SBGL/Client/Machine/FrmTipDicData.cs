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
    public partial class FrmTipDicData : Form
    {
        private int DicCatalogId;
        private int MonthDicDetailId;
        private int YearDicDetailId;
        private DataTable DicDetailDT;
        public FrmTipDicData()
        {
            InitializeComponent();
        }

        private void FrmTipDicData_Load(object sender, EventArgs e)
        {
            // 字典目录 
            string sql = string.Format("SELECT Id FROM S_SysDic WHERE Catalog='{0}'", DicCatalogEnum.MAINTENANCE_REPORT_TIP);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                DicCatalogId = Convert.ToInt32(dt.Rows[0][0]);
                GetDicDetail();
            }
            else
            {
                MessageBox.Show("未维护数据字典：【" + DicCatalogEnum.MAINTENANCE_REPORT_TIP + "】，请联系管理员", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DicCatalogId <=0)
            {
                MessageBox.Show("未维护数据字典：【" + DicCatalogEnum.MAINTENANCE_REPORT_TIP + "】，请联系管理员", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(rtxbMonthTip.Text))
            {
                MessageBox.Show("月保养提示不能为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(rtxbYearTip.Text))
            {
                MessageBox.Show("年保养提示不能为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql ="";
            //月
            if (MonthDicDetailId<=0)
            {//新增
                sql += string.Format("Insert S_SysDicDetial(CatalogId,DataKey,DataValue,SortNo)Values('{0}','Month',N'{1}',1);",DicCatalogId,rtxbMonthTip.Text);
            }
            else
            {//修改
                sql += string.Format("Update S_SysDicDetial Set DataValue=N'{1}' WHERE Id='{0}';", MonthDicDetailId, rtxbMonthTip.Text);
            }
            //年
            if (YearDicDetailId <=0)
            {//新增
                sql += string.Format("Insert S_SysDicDetial(CatalogId,DataKey,DataValue,SortNo)Values('{0}','Year',N'{1}',2);", DicCatalogId, rtxbYearTip.Text);
            }
            else
            {//修改
                sql += string.Format("Update S_SysDicDetial Set DataValue=N'{1}' WHERE Id='{0}';", YearDicDetailId, rtxbYearTip.Text);
            }
            DBUtil.ExecSQL(sql);
            MessageBox.Show("保存成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            GetDicDetail();
        }


        private void GetDicDetail()
        {
            //字典详情
            string sql = string.Format("SELECT * FROM S_SysDicDetial WHERE CatalogId='{0}'", DicCatalogId);
            DicDetailDT = DBUtil.GetDataTable(sql);
            if (DicDetailDT != null)
            {
                foreach (DataRow row in DicDetailDT.Rows)
                {
                    if (row["DataKey"].ToString() == "Month")
                    {
                        rtxbMonthTip.Text = row["DataValue"].ToString();
                        MonthDicDetailId = Convert.ToInt32(row["Id"]);
                    }
                    if (row["DataKey"].ToString() == "Year")
                    {
                        rtxbYearTip.Text = row["DataValue"].ToString();
                        YearDicDetailId = Convert.ToInt32(row["Id"]);
                    }
                }
            }
        }

    }
}
