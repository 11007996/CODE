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
    public partial class FrmAssetReceive : Form
    {
        private DataTable AssetDT;

        public FrmAssetReceive()
        {
            InitializeComponent();
        }

        private void FrmAssetReceive_Load(object sender, EventArgs e)
        {


            labMessage.Text = "";
            dgvAssetInfo.AutoGenerateColumns = false;
            dgvLineAsset.AutoGenerateColumns = false;
            //权限
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
                cmbLine.Enabled = false;
            //产线
            string sql = "SELECT Line FROM S_LineInfo_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            DataRow r = dt.NewRow();
            dt.Rows.InsertAt(r,0);
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = dt;
            cmbLine.Text = BaseInfo.Line;

            RefreshAssetGridData();
            RefreshLineGridData();
        }



        private void RefreshAssetGridData()
        {
            //所有资产
            dgvAssetInfo.DataSource = null;
            string sql = "SELECT a.AssetNo,a.AssetName,r.Line FROM A_AssetInfo_T a LEFT JOIN A_AssetReceive_T r ON a.AssetNo=r.AssetNo And EndTime IS NULL ";
            //查询
            AssetDT = DBUtil.GetDataTable(sql);
            dgvAssetInfo.DataSource = AssetDT;
        }

        private void RefreshLineGridData()
        {
            //产线的资产
            dgvLineAsset.DataSource = null;
            string sql = string.Format("SELECT a.AssetNo,a.AssetName,r.StartTime FROM A_AssetReceive_T r LEFT JOIN A_AssetInfo_T a ON a.AssetNo=r.AssetNo  WHERE r.Line=N'{0}' And r.EndTime IS NULL ORDER BY r.StartTime DESC;", cmbLine.Text);
            //查询
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvLineAsset.DataSource = dt;
        }

        private void txbAsset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbAsset.Text))
            {
                dgvAssetInfo.DataSource = AssetDT;
            }
            else
            {
                DataTable dt = AssetDT.Clone();
                DataRow[] rows = AssetDT.Select(string.Format(" AssetNo like '%{0}%' OR AssetName like '%{0}%'", txbAsset.Text));
                if (rows != null)
                {
                    foreach (DataRow row in rows)
                    {
                        DataRow newR = dt.NewRow();
                        newR.ItemArray = row.ItemArray;
                        dt.Rows.Add(newR);
                    }
                }
                dgvAssetInfo.DataSource = dt;
            }
        }

        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshLineGridData();
        }

        private void btnRecevie_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (dgvAssetInfo.SelectedRows.Count <= 0)
            {
                labMessage.Text = "未选中任意一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbLine.Text))
            {
                labMessage.Text = "未选择产线";
                labMessage.ForeColor = Color.Red;
                return;
            }
            DataGridViewRow row = dgvAssetInfo.SelectedRows[0];
            string line = cmbLine.Text;
            string assetNo = row.Cells["dgcAssetNo"].Value.ToString();
            string assetName = row.Cells["dgcAssetName"].Value.ToString();
            string tip = "确定要领用此资产吗?\r" + "资产名称：" + assetName + "\r资产编号：" + assetNo;
            if (MessageBox.Show(tip, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                //判断是否已经被此产线领用且未归还。
                string sql = string.Format("SELECT 1 FROM  A_AssetReceive_T  WHERE AssetNo='{0}' AND  Line='{1}' AND EndTime IS  NULL ; ", assetNo, line);
                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    labMessage.Text = "此资产已经被当前产线领用，无法再次领用";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                //更新原有领用记录
                sql = string.Format("Update A_AssetReceive_T SET EndTime=GETDATE(),ReturnMode='A',ReturnUser='{1}' WHERE AssetNo='{0}' AND EndTime IS  NULL;", assetNo, BaseInfo.LoginUserNo);
                //插入新的领用记录
                sql += string.Format("INSERT INTO A_AssetReceive_T(AssetNo,Line,StartTime,ReceiveMode,ReceiveUser)VALUES('{0}',N'{1}',GETDATE(),'A','{2}');", assetNo, line, BaseInfo.LoginUserNo);
                DBUtil.ExecSQL(sql);

                //刷新数据
                RefreshAssetGridData();
                RefreshLineGridData();
                labMessage.Text = "领用成功";
                labMessage.ForeColor = Color.Green;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (dgvLineAsset.SelectedRows.Count <= 0)
            {
                labMessage.Text = "未选中任意一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            DataGridViewRow row = dgvLineAsset.SelectedRows[0];
            string line = cmbLine.Text;
            string assetNo = row.Cells["dgcLineAssetNo"].Value.ToString();
            string assetName = row.Cells["dgcLineAssetName"].Value.ToString();
            string tip = "确定要归还此资产吗?\r" + "资产名称：" + assetName + "\r资产编号：" + assetNo;
            if (MessageBox.Show(tip, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string sql = string.Format("Update A_AssetReceive_T SET EndTime=GETDATE(),ReturnMode='A',ReturnUser='{1}' WHERE AssetNo='{0}' AND EndTime IS  NULL;", assetNo, BaseInfo.LoginUserNo);
                //插入新的领用记录
                DBUtil.ExecSQL(sql);

                //刷新数据
                RefreshAssetGridData();
                RefreshLineGridData();
                labMessage.Text = "归还成功";
                labMessage.ForeColor = Color.Green;
            }
        }

        private void tsmiReceiveRecord_Click(object sender, EventArgs e)
        {
            FrmAssetReceiveRecord frm = new FrmAssetReceiveRecord();
            frm.ShowDialog();
        }
    }
}
