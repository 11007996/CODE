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
    public partial class FrmAssetReceiveRecord : Form
    {
        public FrmAssetReceiveRecord()
        {
            InitializeComponent();
        }

        private void FrmAssetReceiveRecord_Load(object sender, EventArgs e)
        {
            dgvLineAsset.AutoGenerateColumns = false;
            //产线
            string sql = "SELECT Line FROM S_LineInfo_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            DataRow r = dt.NewRow();
            dt.Rows.InsertAt(r, 0);
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = dt;
            cmbLine.Text = BaseInfo.Line;
        }

        public void RefreshDataGrid()
        {
            string sql = "SELECT r.Line,r.AssetNo,r.StartTime,r.EndTime,a.AssetName FROM A_AssetReceive_T  r LEFT JOIN A_AssetInfo_T a ON a.AssetNo=r.AssetNo WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(cmbLine.Text))
            {
                sql +=  string.Format(" AND r.Line like '%{0}%' ",cmbLine.Text);
            }
            if (!string.IsNullOrWhiteSpace(txbAsset.Text))
            {
                sql += string.Format(" AND( r.AssetNo like '%{0}%' OR a.AssetName like '%{0}%') ", txbAsset.Text);
            }
            if (chkNotReturn.Checked)
            {
                sql += string.Format(" AND EndTime IS NULL ", txbAsset.Text);
            }
            sql += " ORDER BY StartTime DESC";
            DataTable dt =  DBUtil.GetDataTable(sql);
            dgvLineAsset.DataSource = dt;
        }

        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void chkNotReturn_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
      

        private void txbAsset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)(13)){
                RefreshDataGrid();
            }
        }

    
    }
}
