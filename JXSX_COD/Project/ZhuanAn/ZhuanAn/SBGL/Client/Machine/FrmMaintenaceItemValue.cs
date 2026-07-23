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
    public partial class FrmMaintenaceItemValue : Form
    {
        public FrmMaintenaceItemValue()
        {
            InitializeComponent();
        }

        public FrmMaintenaceItemValue(string assetNo, string assetName, DateTime day)
        {
            InitializeComponent();
            txbAssetNo.Text = assetNo;
            txbAssetName.Text = assetName;
            dtiDay.Value = day;
        }

        private void FrmMaintenaceItemValue_Load(object sender, EventArgs e)
        {
            dgvItem.AutoGenerateColumns = false;
            RefreshGridData();
        }

        private void RefreshGridData()
        {
            dgvItem.DataSource = null;
            string assetNo = txbAssetNo.Text;
            int year = dtiDay.Value.Year;
            string timeMark = "D";
            int stamp = dtiDay.Value.DayOfYear;

            string sql = string.Format(@"SELECT ItemName,'' ItemValue FROM A_MaintenanceReportItem_T  WHERE AssetNo='{0}' AND [Year]='{1}' AND TimeMark='{2}' Order BY SortNo;", assetNo, year, timeMark);
            sql += string.Format("SELECT ItemName,ItemValue FROM A_MaintenanceReportDV_T WHERE MRDId=(SELECT TOP 1 Id FROM A_MaintenanceReportD_T WHERE AssetNo='{0}' AND [Year]='{1}' AND TimeMark='{2}' AND TimeMarkStamp='{3}')", assetNo, year, timeMark, stamp);
            DataSet ds = DBUtil.GetDataSet(sql, new string[] { "ItemDT", "ValueDT" });
            DataTable itemDT = ds.Tables["ItemDT"];
            DataTable valueDT = ds.Tables["ValueDT"];
            if (itemDT != null)
            {
                string itemName = "";
                foreach (DataRow row in itemDT.Rows)
                {
                    itemName = row["ItemName"].ToString();
                    DataRow[] valR = valueDT.Select("ItemName='" + itemName + "'");
                    if (valR != null && valR.Length > 0)
                    {
                        row["ItemValue"] = valR[0]["ItemValue"];
                    }
                }
            }
            dgvItem.DataSource = itemDT;
        }
    }
}
