using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmMaintenanceItem : Form
    {
        private DataTable AssetDT;
        private DataTable AssetItemDT;
        public FrmMaintenanceItem()
        {
            InitializeComponent();
        }


        private void FrmMaintenanceItem_Load(object sender, EventArgs e)
        {
            dgvMaintenanceItem.AutoGenerateColumns = false;
            //维护周期
            Dictionary<string, string> timeMarkDic = new Dictionary<string, string>();
            timeMarkDic.Add("", "");
            timeMarkDic.Add("D", "(D)日");
            timeMarkDic.Add("W", "(W)周");
            timeMarkDic.Add("M", "(M)月");
            timeMarkDic.Add("Q", "(Q)季");
            timeMarkDic.Add("Y", "(Y)年");
            List<KeyValuePair<string, string>> timeMarks = timeMarkDic.AsEnumerable<KeyValuePair<string, string>>().ToList();
            cmbTimeMark.DisplayMember = "Value";
            cmbTimeMark.ValueMember = "Key";
            cmbTimeMark.DataSource = timeMarks;
            //资产编号
            string sql = "SELECT AssetNo FROM A_AssetInfo_T ;";
            AssetDT = DBUtil.GetDataTable(sql);
            DataRow row = AssetDT.NewRow();
            AssetDT.Rows.InsertAt(row, 0);
            cmbAssetNo.DisplayMember = "AssetNo";
            cmbAssetNo.ValueMember = "AssetNo";
            cmbAssetNo.DataSource = AssetDT;
            RefreshDataGrid();
        }

        //刷新数据
        private void RefreshDataGrid()
        {
            dgvMaintenanceItem.DataSource = null;
            if (cmbAssetNo.SelectedIndex <= 0) return;
            string assetNo = cmbAssetNo.Text.Trim();
            string sql = string.Format("SELECT * FROM A_MaintenanceItem_T  WHERE 1=1 ");
           
            if (!string.IsNullOrWhiteSpace(assetNo))
            {
                sql += string.Format(" AND AssetNo=N'{0}'", assetNo);
            }
            if (cmbTimeMark.SelectedValue != null && !string.IsNullOrWhiteSpace(cmbTimeMark.SelectedValue.ToString()))
            {
                sql += string.Format(" AND TimeMark='{0}'", cmbTimeMark.SelectedValue.ToString());
            }
            sql += " Order By CHARINDEX(TimeMark+',','D,W,M,Q,Y,') ,SortNo ";
            AssetItemDT = DBUtil.GetDataTable(sql);
            dgvMaintenanceItem.DataSource = AssetItemDT;
        }


        //保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                //提交检查
                if (!CheckFormData()) return;
                //检查项目是否重复
                string sql = string.Format("SELECT 1 FROM A_MaintenanceItem_T WHERE AssetNo=N'{0}' AND TimeMark='{1}' AND ItemName=N'{2}';", cmbAssetNo.Text.Trim(), cmbTimeMark.SelectedValue.ToString(), txbItemName.Text.Trim());
                if (DBUtil.GetDataTable(sql).Rows.Count > 0)
                {
                    MessageBox.Show("已存在相同维护项目", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
          
                sql = string.Format("INSERT INTO A_MaintenanceItem_T(AssetNo,TimeMark,ItemName,SortNo)VALUES(N'{0}','{1}',N'{2}','{3}')", cmbAssetNo.Text.Trim(), cmbTimeMark.SelectedValue.ToString(), txbItemName.Text.Trim(), nudSortNo.Value);
                if (DBUtil.ExecSQL(sql) > 0)
                {
                    MessageBox.Show("添加维护项目成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    RefreshDataGrid();
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaintenanceItem), ex.Message);
                MessageBox.Show("异常报错：" + ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //检查表单
        private bool CheckFormData()
        {
            if (string.IsNullOrWhiteSpace(cmbAssetNo.Text.Trim()))
            {
                MessageBox.Show("未输入资产编号", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbAssetNo.Text.Length!=22)
            {
                MessageBox.Show("输入的资产编号非法", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbItemName.Text.Trim()))
            {
                MessageBox.Show("未输入维护项目名称", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txbItemName.Text.Trim() == "保养人签名")
            {
                MessageBox.Show("禁止创建【保养人签名】的项目，此项目为在创建报表时自动追加。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbTimeMark.SelectedIndex <= 0 || string.IsNullOrWhiteSpace(cmbTimeMark.Text))
            {
                MessageBox.Show("未选择维护周期标识", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        //删除指定记录
        private void tsmiDel_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvMaintenanceItem.SelectedRows[0].Index;
            if (rowIndex >= 0)
            {
                DataRow row = ((DataRowView)dgvMaintenanceItem.Rows[rowIndex].DataBoundItem).Row;
                string assetNo = row["AssetNo"].ToString();
                string timeMark = row["TimeMark"].ToString();
                string itemName = row["ItemName"].ToString();
                if (!string.IsNullOrWhiteSpace(assetNo) && !string.IsNullOrWhiteSpace(timeMark) && !string.IsNullOrWhiteSpace(itemName))
                {
                    if (DialogResult.Yes == MessageBox.Show("确定要删除这条记录吗", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Hand))
                    {
                        string sql = string.Format("DELETE A_MaintenanceItem_T WHERE AssetNo=N'{0}' AND TimeMark='{1}' AND ItemName=N'{2}'", assetNo, timeMark, itemName);
                        DBUtil.ExecSQL(sql);
                        RefreshDataGrid();
                    }
                }
            }

        }

        private void cmbAssetNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void cmbTimeMark_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        //导入
        private void tsmiImport_Click(object sender, EventArgs e)
        {
            try
            {
                //打开文件选择
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请上传设备保养项目信息";
                ofd.Filter = "Excel 文件|*.xls;*.xlsx";
                ofd.ShowDialog();
                string SelectedFilePath = ofd.FileName;
                string SelectedFileName = ofd.SafeFileName;
                if (SelectedFilePath == "" || SelectedFileName == "")
                {
                    return;
                }
                if (!File.Exists(SelectedFilePath))
                {
                    MessageBox.Show("文件不存在", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //文件数据导入到DataTable数据类型中
                DataTable dt = ExcelUtil.ReadFromExcelFile(SelectedFilePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //判断字段是否合规
                    if (!dt.Columns.Contains("资产编号") || !dt.Columns.Contains("时间周期") || !dt.Columns.Contains("项目名称"))
                    {
                        MessageBox.Show("字段名称异常", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //插入
                    List<string> rowData = new List<string>();
                    //遍历数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string assetNo = dt.Rows[i]["资产编号"].ToString().Trim();
                        string timeMark = dt.Rows[i]["时间周期"].ToString().Trim();
                        string itemName = dt.Rows[i]["项目名称"].ToString().Trim();
                        if (assetNo.Length!=22)
                        {
                            MessageBox.Show("导入失败，第" + (i + 2) + "行存在非法的资产编号。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(itemName)) {
                            MessageBox.Show("导入失败，第"+(i+2)+"行存在为空的项目名称。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (itemName == "保养人签名")
                        {
                            MessageBox.Show("导入失败，项目中存在【保养人签名】此非法名称", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //判断是否存在，不存在插入,存在更新排序
                        rowData.Add(string.Format(@" IF NOT EXISTS(SELECT * FROM A_MaintenanceItem_T WHERE AssetNo='{0}' AND TimeMark= '{1}' AND ItemName=N'{2}')BEGIN
                                                        INSERT INTO A_MaintenanceItem_T(AssetNo,TimeMark,ItemName,SortNo)  VALUES ('{0}','{1}',NULLIF(N'{2}',''),'{3}') 
                                                    END 
                                                    ELSE BEGIN
                                                        UPDATE A_MaintenanceItem_T SET SortNo='{3}' WHERE AssetNo='{0}' AND TimeMark= '{1}' AND ItemName=NULLIF(N'{2}','')
                                                    END  ", assetNo, timeMark, itemName, i));
                    }

                    string sql = string.Join("\r", rowData);
                    DBUtil.ExecSQL(sql);
                    MessageBox.Show("导入成功", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    RefreshDataGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败:" + ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogHelper.Error(typeof(FrmMaintenanceItem), null, ex);
            }
        }

        //导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = AssetItemDT.Copy();
                //列标题 
                dt.Columns["AssetNo"].Caption = "资产编号";
                dt.Columns["TimeMark"].Caption = "时间周期";
                dt.Columns["ItemName"].Caption = "项目名称";
                dt.Columns.Remove("SortNo");

                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "设备保养项目信息";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaintenanceItem), null, ex);
                MessageBox.Show("导出失败:\r" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }



    }
}
