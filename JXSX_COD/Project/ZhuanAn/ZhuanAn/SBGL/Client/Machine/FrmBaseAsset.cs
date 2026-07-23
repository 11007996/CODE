using Common;
using Common.Base;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class FrmBaseAsset : Form
    {
        private OperateStateEnum operateFlag = OperateStateEnum.Defualt;//机台基础信息操作状态标记
        private string CurrentAssetNo;

        public FrmBaseAsset()
        {
            InitializeComponent();
        }

        private void FrmBaseAsset_Load(object sender, EventArgs e)
        {
            //判断是否有删除权限
            if (BaseInfo.LoginDept != "资讯" && BaseInfo.LoginUserRight != "A")
            {
                tsmiDelete.Visible = false;
            }
            dgvAsset.AutoGenerateColumns = false;
            labMessage.Text = "";
            RefreshDataGrid();
            RefreshControlState();
        }

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            RefreshDataGrid();
            labMessage.Text = "数据加载完成";
            labMessage.ForeColor = Color.Green;
        }
        #endregion

        #region 添加
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Add;
            RefreshControlState();
        }
        #endregion

        #region 修改
        //修改
        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAsset.CurrentRow == null)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            operateFlag = OperateStateEnum.Edit;
            RefreshControlState();
        }
        #endregion

        #region 保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string msg = "";

            //表单数据
            string factoryCode = txbFactoryCode.Text.Trim();
            string assetMaineNo = txbAssetMainNo.Text.Trim();
            string assetSubNo = txbAssetSubNo.Text.Trim();
            string assetName = txbAssetName.Text.Trim();
            string assetClass = txbAssetClass.Text.Split('-')[0];
            string model = txbModel.Text.Trim();
            DateTime entryDate = dtpEntryDate.Value;
            string costCenter = txbCostCenter.Text.Trim();
            int durableYear = (int)nudDurableYear.Value;
            int durableMonth = (int)nudDurableMonth.Value;
            string madeFactory = txbMadeFactory.Text.Trim();
            string controlNo = txbControlNo.Text.Trim();

            string sql = "";
            if (operateFlag == OperateStateEnum.Add)
            { //增加
                //数据检查
                if (!CheckForm(ref msg))
                {
                    labMessage.Text = msg;
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                string assetNo = factoryCode + "-" + assetMaineNo + "-" + assetSubNo;
                if (assetNo.Length != 22)
                {
                    labMessage.Text = "资产编号【"+assetNo+"】字符长度非法";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
                //是否已存在相同资产编号
                sql = string.Format("SELECT 1 FROM A_AssetInfo_T WHERE AssetNo='{0}';", assetNo);
                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    labMessage.Text = "保存失败,已存在相同的资产编号";
                    labMessage.ForeColor = Color.Red;
                    return;
                }

                sql = string.Format(@"INSERT INTO A_AssetInfo_T
                                (AssetNo,FactoryCode,AssetMainNo,AssetSubNo,AssetName,AssetClass,Model,EntryDate,CostCenter,DurableYear,DurableMonth,MadeFactory,ControlNo,UpdateTime,UpdateUser)
                                VALUES('{0}','{1}','{2}','{3}', N'{4}',N'{5}',N'{6}','{7}',N'{8}','{9}','{10}',N'{11}',N'{12}',GETDATE(),'{13}')",
                                assetNo, factoryCode, assetMaineNo, assetSubNo, assetName, assetClass, model, entryDate, costCenter, durableYear, durableMonth, madeFactory, controlNo, BaseInfo.LoginUserNo);

            }
            else if (operateFlag == OperateStateEnum.Edit)
            {//修改
                string assetNo = CurrentAssetNo;
                sql = string.Format(@"UPDATE A_AssetInfo_T SET AssetName=N'{1}',AssetClass=N'{2}',Model=N'{3}',EntryDate='{4}',CostCenter=N'{5}',DurableYear='{6}',DurableMonth='{7}',MadeFactory=N'{8}',ControlNo=N'{9}',UpdateTime=GETDATE(),UpdateUser='{10}'
                                 WHERE AssetNo='{0}'",
                              assetNo, assetName, assetClass, model, entryDate, costCenter, durableYear, durableMonth, madeFactory, controlNo, BaseInfo.LoginUserNo);
            }
            try
            {
                DBUtil.ExecSQL(sql);
                RefreshDataGrid();
                operateFlag = OperateStateEnum.Defualt;
                RefreshControlState();
                labMessage.Text = "保存成功";
                labMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "保存失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmBaseAsset), null, ex);
            }
        }
        #endregion

        #region 删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (dgvAsset.CurrentRow == null)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }

            DataRow row = ((DataRowView)dgvAsset.CurrentRow.DataBoundItem).Row;
            string assetNo = Convert.ToString(row["AssetNo"]);
            if (DialogResult.Yes == MessageBox.Show("确定要删除【" + assetNo + "】吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                try
                {
                    string sql = string.Format("Delete A_AssetInfo_T WHERE AssetNo ='{0}';", assetNo);
                    DBUtil.ExecSQL(sql);
                    labMessage.Text = "删除成功 ";
                    labMessage.ForeColor = Color.Green;
                    FormUtil.ClearControls(this.gpAssetInfo);
                    RefreshDataGrid();
                }
                catch (Exception ex)
                {
                    labMessage.Text = "删除失败:" + ex.Message;
                    labMessage.ForeColor = Color.Red;
                }
            }
        }
        #endregion

        #region 导入
        private void tsmiImport_Click(object sender, EventArgs e)
        {
            try
            {
                labMessage.Text = "";
                //打开文件选择
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请上传文件";
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
                    labMessage.Text = "文件不存在！";
                    return;
                }
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                //文件数据导入到DataTable数据类型中
                DataTable dt = ExcelUtil.ReadFromExcelFile(SelectedFilePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //判断字段是否合规	公司代码	资产主编号	资产子编号	资产名称	资产分类	型号规格	购置日期	成本中心	耐用年限	耐用月数	供应商名称	管制编号
                    if (!dt.Columns.Contains("公司代码") || !dt.Columns.Contains("资产主编号") || !dt.Columns.Contains("资产子编号") ||
                        !dt.Columns.Contains("资产名称") || !dt.Columns.Contains("资产分类") || !dt.Columns.Contains("型号规格") ||
                        !dt.Columns.Contains("购置日期") || !dt.Columns.Contains("成本中心") || !dt.Columns.Contains("耐用年限")
                        || !dt.Columns.Contains("耐用月数") || !dt.Columns.Contains("供应商名称") || !dt.Columns.Contains("管制编号"))
                    {
                        labMessage.ForeColor = Color.Red;
                        labMessage.Text = "字段名称异常";
                        return;
                    }

                    string sql = "";
                    //遍历数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        //公司代码	资产主编号	资产子编号	资产名称	资产分类	型号规格	购置日期	成本中心	耐用年限	耐用月数	供应商名称	管制编号
                        string factoryCode = row["公司代码"].ToString().Trim();
                        string assetMaineNo = row["资产主编号"].ToString().Trim();
                        string assetSubNo = row["资产子编号"].ToString().Trim();
                        string assetName = row["资产名称"].ToString().Trim();
                        string assetClass = row["资产分类"].ToString();
                        string model = row["型号规格"].ToString();
                        string entryDate = row["购置日期"].ToString();
                        string costCenter = row["成本中心"].ToString();
                        string durableYear = row["耐用年限"].ToString();
                        string durableMonth = row["耐用月数"].ToString();
                        string madeFactory = row["供应商名称"].ToString();
                        string controlNo = row["管制编号"].ToString();
                        string assetNo = "";
                        //-----数据检查---
                        if (string.IsNullOrWhiteSpace(factoryCode))
                        {
                            MessageBox.Show("第"+(i+2)+"行,公司代码不能为空。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(assetMaineNo))
                        {
                            MessageBox.Show("第" + (i + 2) + "行,资产主编号不能为空。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(assetSubNo))
                        {
                            MessageBox.Show("第" + (i + 2) + "行,资产子编号不能为空。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(assetName))
                        {
                            MessageBox.Show("第" + (i + 2) + "行,资产名称不能为空。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                       
                        if (!string.IsNullOrWhiteSpace(factoryCode))
                        {
                            assetNo += factoryCode + "-";
                        }
                        assetNo += assetMaineNo;
                        if (!string.IsNullOrWhiteSpace(assetSubNo))
                        {
                            assetNo += ("-" + assetSubNo);
                        }
                        if (assetNo.Length != 22)
                        {
                            MessageBox.Show("第" + (i + 2) + "行,资产编号【"+assetNo+"】字符长度非法，请检查。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        sql += string.Format(@"
                                IF EXISTS(SELECT 1 FROM A_AssetInfo_T WHERE AssetNo='{0}') BEGIN
	                                 DELETE A_AssetInfo_T WHERE AssetNo='{0}'
                                 END
                                 INSERT INTO A_AssetInfo_T(AssetNo,FactoryCode,AssetMainNo,AssetSubNo,AssetName,AssetClass,Model,EntryDate,CostCenter,DurableYear,DurableMonth,MadeFactory,ControlNo,UpdateTime,UpdateUser) 
                                 VALUES ('{0}','{1}','{2}','{3}', N'{4}',N'{5}',N'{6}','{7}',N'{8}','{9}','{10}',N'{11}',N'{12}',GETDATE(),'{13}');",
                                 assetNo, factoryCode, assetMaineNo, assetSubNo, assetName, assetClass, model, entryDate, costCenter, durableYear, durableMonth, madeFactory, controlNo, BaseInfo.LoginUserNo);
                        if (i % 100 == 0 || i == dt.Rows.Count - 1)
                        {
                            DBUtil.ExecSQL(sql);
                            sql = "";
                        }
                    }

                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "导入成功";
                    RefreshDataGrid();

                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmBaseAsset), null, ex);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }
        #endregion

        #region 导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // 列名称强制转换
                for (int c = 0; c < dgvAsset.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvAsset.Columns[c].Name.ToString());
                    dc.Caption = dgvAsset.Columns[c].HeaderText.ToString();
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvAsset.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvAsset.Columns.Count; c++)
                    {
                        dr[c] = Convert.ToString(dgvAsset.Rows[r].Cells[c].EditedFormattedValue);
                    }
                    dt.Rows.Add(dr);
                }
                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "资产清册";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseAsset), null, ex);
            }
        }
        #endregion

        #region 返回
        private void tsmiBack_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Defualt;
            RefreshControlState();
        }
        #endregion

        #region 二维码
        private void tsmiQRCode_Click(object sender, EventArgs e)
        {
            FrmAssetQRCode frm = new FrmAssetQRCode();
            frm.ShowDialog();
        }
        #endregion

        #region 其他

        //刷新数据表格
        private void RefreshDataGrid()
        {
            string sql = "SELECT * FROM A_AssetInfo_T WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(txbAssetMainNo.Text))
            {
                sql += " AND AssetMainNo='" + txbAssetMainNo.Text + "'";
            }
            if (!string.IsNullOrWhiteSpace(txbAssetName.Text))
            {
                sql += " AND AssetName like N'%" + txbAssetName.Text + "%'";
            }
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvAsset.DataSource = dt;
        }

        //检查表单
        private bool CheckForm(ref string msg)
        {
            if (string.IsNullOrWhiteSpace(txbFactoryCode.Text.Trim()))
            {
                msg = "公司代码不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbAssetMainNo.Text.Trim()))
            {
                msg = "资产主编号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbAssetSubNo.Text.Trim()))
            {
                msg = "资产子编号不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txbAssetSubNo.Text.Trim()))
            {
                msg = "资产名称不能为空";
                return false;
            }
            return true;
        }


        /// <summary>
        /// 刷新控件状态
        /// </summary>
        private void RefreshControlState()
        {
            switch (operateFlag)
            {
                case OperateStateEnum.Add:
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    txbFactoryCode.Enabled = true;
                    txbAssetMainNo.Enabled = true;
                    txbAssetSubNo.Enabled = true;
                    txbAssetName.Enabled = true;
                    txbAssetClass.Enabled = true;
                    txbModel.Enabled = true;
                    dtpEntryDate.Enabled = true;
                    txbCostCenter.Enabled = true;
                    nudDurableYear.Enabled = true;
                    nudDurableMonth.Enabled = true;
                    txbMadeFactory.Enabled = true;
                    txbControlNo.Enabled = true;
                    dgvAsset.Enabled = false;
                    FormUtil.ClearControls(this.gpAssetInfo);
                    break;
                case OperateStateEnum.Edit:
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    txbFactoryCode.Enabled = false;
                    txbAssetMainNo.Enabled = false;
                    txbAssetSubNo.Enabled = false;
                    txbAssetName.Enabled = false;
                    txbAssetClass.Enabled = true;
                    txbModel.Enabled = true;
                    dtpEntryDate.Enabled = true;
                    txbCostCenter.Enabled = true;
                    nudDurableYear.Enabled = true;
                    nudDurableMonth.Enabled = true;
                    txbMadeFactory.Enabled = true;
                    txbControlNo.Enabled = true;
                    dgvAsset.Enabled = false;
                    break;
                case OperateStateEnum.Defualt:
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiSave.Enabled = false;
                    tsmiDelete.Enabled = true;
                    tsmiBack.Enabled = false;

                    txbFactoryCode.Enabled = false;
                    txbAssetMainNo.Enabled = true;
                    txbAssetSubNo.Enabled = false;
                    txbAssetName.Enabled = true;
                    txbAssetClass.Enabled = false;
                    txbModel.Enabled = false;
                    dtpEntryDate.Enabled = false;
                    txbCostCenter.Enabled = false;
                    nudDurableYear.Enabled = false;
                    nudDurableMonth.Enabled = false;
                    txbMadeFactory.Enabled = false;
                    txbControlNo.Enabled = false;
                    dgvAsset.Enabled = true;
                    FormUtil.ClearControls(this.gpAssetInfo);
                    break;
            }
        }


        //单元格单击事件
        private void dgvAsset_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAsset.CurrentRow.Index >= 0)
            {
                //--------------将数据行信息填入表单控件中---------------
                ////数据信息
                int rowindex = dgvAsset.CurrentRow.Index;
                txbFactoryCode.Text = dgvAsset["dgcFactoryCode", rowindex].Value.ToString();
                txbAssetMainNo.Text = dgvAsset["dgcAssetMainNo", rowindex].Value.ToString();
                txbAssetSubNo.Text = dgvAsset["dgcAssetSubNo", rowindex].Value.ToString();
                txbAssetName.Text = dgvAsset["dgcAssetName", rowindex].Value.ToString();
                txbAssetClass.Text = dgvAsset["dgcAssetClass", rowindex].Value.ToString();
                txbModel.Text = dgvAsset["dgcModel", rowindex].Value.ToString();
                dtpEntryDate.Text = dgvAsset["dgcEntryDate", rowindex].Value.ToString();
                txbCostCenter.Text = dgvAsset["dgcCostCenter", rowindex].Value.ToString();
                nudDurableYear.Text = dgvAsset["dgcDurableYear", rowindex].Value.ToString();
                nudDurableMonth.Text = dgvAsset["dgcDurableMonth", rowindex].Value.ToString();
                txbMadeFactory.Text = dgvAsset["dgcMadeFactory", rowindex].Value.ToString();
                txbControlNo.Text = dgvAsset["dgcControlNo", rowindex].Value.ToString();
                CurrentAssetNo = ((DataRowView)dgvAsset.Rows[e.RowIndex].DataBoundItem).Row["AssetNo"].ToString();
            }
        }


        #endregion

        #region 文件绑定
        private void tsmiFileBind_Click(object sender, EventArgs e)
        {
            FrmAssetFileBind frm = new FrmAssetFileBind();
            frm.ShowDialog();
        }
        #endregion
    }
}
