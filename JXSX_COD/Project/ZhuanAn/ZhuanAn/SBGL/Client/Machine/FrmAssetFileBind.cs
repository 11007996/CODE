using Basic;
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
    public partial class FrmAssetFileBind : Form
    {

        public FrmAssetFileBind()
        {
            InitializeComponent();
        }

        private void FrmAssetFileBind_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            dgvAssetInfo.AutoGenerateColumns = false;
            dgvFileInfo.AutoGenerateColumns = false;
            RefreshAssetGridData();
            RefreshFileGridData();
        }

        #region 数据刷新
        /// <summary>
        /// 刷新资产视图表
        /// </summary>
        private void RefreshAssetGridData()
        {
            //所有资产
            dgvAssetInfo.DataSource = null;
            string sql = @"SELECT a.AssetNo,a.AssetName,b.* FROM A_AssetInfo_T a 
                            LEFT JOIN(
	                            SELECT AssetNo,MAX(MSOP) AS 'MSOP',MAX(MMI) AS 'MMI',MAX(f.MMOS) AS 'MMOS' FROM (
		                            SELECT AssetNo,
		                            CASE WHEN FileClass =1 THEN Max(FileId) END 'MSOP',
		                            CASE WHEN FileClass =2 THEN Max(FileId) END 'MMI',
		                            CASE WHEN FileClass =3 THEN Max(FileId) END 'MMOS'
		                            FROM A_AssetFile_T GROUP BY AssetNo,FileClass
		                            ) f
		                            GROUP BY f.AssetNo
                            ) b ON a.AssetNo=b.AssetNo
                          WHERE 1=1 ";
            string keyword = txbAssetKeyword.Text;
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += $" AND( a.AssetNo like '%{keyword}%' OR a.AssetName like '%{keyword}%' )";
            }
            //查询
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvAssetInfo.DataSource = dt;
        }

        /// <summary>
        /// 刷新文件视图表
        /// </summary>
        private void RefreshFileGridData()
        {
            string sql = "select  'false' FileCheck,*,case FileClass when 1 then '设备操作SOP' when 2 then '设备保养周期表' when 3 then '设备保养作业标准书' else '' end  as FileClassText FROM S_FileInfo  Where  ";
            List<string> classList = new List<string>();
            if (chkFileMSOP.Checked) classList.Add(chkFileMSOP.Tag.ToString());
            if (chkFileMMI.Checked) classList.Add(chkFileMMI.Tag.ToString());
            if (chkFileMMOS.Checked) classList.Add(chkFileMMOS.Tag.ToString());
            sql += $"  FIleClass IN('{string.Join("','", classList)}')";

            int fileId = -1;
            string keyword = txbFileKeyword.Text.Trim();
            if (int.TryParse(txbFileKeyword.Text, out fileId))
            {
                sql += $" AND (FileId like '%{fileId}%' or  FileAliasName like'%{keyword}%') ";
            }
            else if (!string.IsNullOrEmpty(keyword))
            {
                sql += $" AND FileAliasName like '%{keyword}%' ";
            }

            DataTable dt = DBUtil.GetDataTable(sql);
            dgvFileInfo.DataSource = dt;
        }
        #endregion

        #region 关联
        private void btnBind_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string tip = "确定要将左侧选中的资产设备关联这些文件吗?\r这个操作将覆盖之前的绑定。";
            if (MessageBox.Show(tip, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string sql = "";
                string assetNo = "";
                foreach (DataGridViewRow assetRowV in dgvAssetInfo.Rows)
                {
                    if (Convert.ToBoolean(assetRowV.Cells[0].Value) == true)
                    {
                        assetNo = assetRowV.Cells["dgcAssetNo"].Value.ToString();
                        DataRow tempRow = null;
                        foreach (DataGridViewRow fileRowV in dgvFileInfo.Rows)
                        {
                            if (Convert.ToBoolean(fileRowV.Cells[0].Value) == true)
                            {
                                tempRow = ((DataRowView)fileRowV.DataBoundItem).Row;
                                sql += $"delete from A_AssetFile_T WHERE AssetNo='{assetNo}' AND FileClass='{tempRow["FileClass"]}';";
                                sql += $"insert into A_AssetFIle_T(AssetNo,FileId,FileClass)Values('{assetNo}','{tempRow["FileId"]}','{tempRow["FileClass"]}');";
                            }
                        }
                        DBUtil.ExecSQL(sql);
                    }
                }

                //刷新数据
                RefreshAssetGridData();
                RefreshFileGridData();
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "关联完成";
            }
        }
        #endregion

        #region 解绑
        private void btnUnBind_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string tip = "确定要将左侧选中的资产设备全部解除文件关联吗?";
            if (MessageBox.Show(tip, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string sql = "";
                string assetNo = "";
                foreach (DataGridViewRow assetRowV in dgvAssetInfo.Rows)
                {
                    if (Convert.ToBoolean(assetRowV.Cells[0].Value) == true)
                    {
                        assetNo = assetRowV.Cells["dgcAssetNo"].Value.ToString();
                        sql += $"delete from A_AssetFile_T WHERE AssetNo='{assetNo}';";

                    }
                }
                if (!string.IsNullOrEmpty(sql))
                {
                    DBUtil.ExecSQL(sql);
                    //刷新数据
                    RefreshAssetGridData();
                    RefreshFileGridData();
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "解绑完成";
                }
            }
        }
        #endregion

        #region 交互事件
        //资产表单元格内容单击
        private void dgvAssetInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvAssetInfo.Columns[e.ColumnIndex].Name;
            //单击文件列
            if (colName == "dgcAssetMSOP" || colName == "dgcAssetMMI" || colName == "dgcAssetMMOS")
            {
                DataRow row = ((DataRowView)dgvAssetInfo.Rows[e.RowIndex].DataBoundItem).Row;
                string fileId = "";
                switch (colName)
                {
                    case "dgcAssetMSOP":
                        fileId = row["MSOP"].ToString();
                        break;
                    case "dgcAssetMMI":
                        fileId = row["MMI"].ToString();
                        break;
                    case "dgcAssetMMOS":
                        fileId = row["MMOS"].ToString();
                        break;
                }
                if (!string.IsNullOrEmpty(fileId))
                {
                    FrmFilePreview frm = new FrmFilePreview(Convert.ToInt32(fileId));
                    frm.ShowDialog();
                }
            }
        }

        //文件表单元格内容单击
        private void dgvFileInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //单击复选框时
            if (e.ColumnIndex == 0)
            {
                string tempFileClass = "";
                List<string> fileClassList = new List<string>();
                foreach (DataGridViewRow rowV in dgvFileInfo.Rows)
                {
                    if ((bool)rowV.Cells[0].EditedFormattedValue == true)
                    {
                        DataRow row = ((DataRowView)rowV.DataBoundItem).Row;
                        tempFileClass = row["FileClass"].ToString();
                        if (fileClassList.Contains(tempFileClass))
                        {
                            MessageBox.Show("同一个文件分类只能勾选一个", "操作异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DataRow curRow = ((DataRowView)dgvFileInfo.Rows[e.RowIndex].DataBoundItem).Row;
                            curRow["FileCheck"] = "False";
                        }
                        else
                        {
                            fileClassList.Add(tempFileClass);
                        }
                    }
                }
            }

            //单击文件名
            if (dgvFileInfo.Columns[e.ColumnIndex].Name == "dgcFileAliasName")
            {
                DataRow row = ((DataRowView)dgvFileInfo.Rows[e.RowIndex].DataBoundItem).Row;
                if (row["FileId"] != DBNull.Value)
                {
                    FrmFilePreview frm = new FrmFilePreview(Convert.ToInt32(row["FileId"]));
                    frm.ShowDialog();
                }
            }

        }

        private void txbFileKeyword_TextChanged(object sender, EventArgs e)
        {
            RefreshFileGridData();
        }

        private void txbAssetKeyword_TextChanged(object sender, EventArgs e)
        {
            RefreshAssetGridData();
        }

        #endregion

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshFileGridData();
        }
    }
}
