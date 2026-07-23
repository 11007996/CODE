using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Util;
using Common.Base;
using Machine;
using DevComponents.DotNetBar.SuperGrid;
using Common;


namespace Machine
{
    public partial class FrmBaseMachine : Form
    {
        private DataTable MachineDT;
        private OperateStateEnum operateFlag = OperateStateEnum.Defualt;//机台基础信息操作状态标记
        private DataTable AssetDT;

        public FrmBaseMachine()
        {
            InitializeComponent();
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseMachine_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            sgridMachine.PrimaryGrid.AutoGenerateColumns = false;
            dgvWarnCode.AutoGenerateColumns = false;
            //权限
            if (BaseInfo.LoginUserRight != "A")
            {
                tsmiDelete.Visible = false;
            }
            //产线
            string sql = "SELECT DISTINCT Line  FROM S_LineInfo_T ";
            DataTable dtLines = DBUtil.GetDataTable(sql);
            if (dtLines != null)
            {
                List<string> lines = dtLines.AsEnumerable().Select(t => t.Field<string>("Line")).ToList();
                lines.Insert(0, "");
                cmbLine.DataSource = lines;
            }
            //资产
            sql = "SELECT AssetNo,AssetName  FROM A_AssetInfo_T Order By AssetNo";
            AssetDT = DBUtil.GetDataTable(sql);
            DataRow row = AssetDT.NewRow();
            //row["AssetNo"] = "";
            //row["AssetName"] = "";
            AssetDT.Rows.InsertAt(row, 0);
            cmbAssetNo.DisplayMember = "AssetNo";
            cmbAssetNo.ValueMember = "AssetNo";
            cmbAssetNo.DataSource = AssetDT;

            //if (AssetDT != null)
            //{
            //    string[] assets = AssetDT.AsEnumerable().Select(t => t.Field<string>("AssetNo")).ToArray();
            //    cmbAssetNo.Items.AddRange(assets);
            //}

            //刷新
            RefreshControlState();
            RefreshGridData();
        }
        #endregion

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshGridData();
            labMessage.ForeColor = Color.Green;
            labMessage.Text = "数据加载完成";
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
            if (sgridMachine.ActiveRow == null || sgridMachine.ActiveRow.Rows.Count > 0)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            operateFlag = OperateStateEnum.Edit;
            RefreshControlState();

            sgridMachine_SelectionChanged(null, null);
        }
        #endregion

        #region 保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //数据验证
            string msg = CheckFormData();
            if (msg.Length > 0)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = msg;
                return;
            }
            //控件数据
            string assetNo = cmbAssetNo.Text.Trim();
            string machineCode = tbMachineCode.Text.Trim();
            string machineNo = tbMachineNo.Text.Trim();
            string machineName = tbMachineName.Text.Trim();
            string machineCategory = tbMachineCategory.Text.Trim();
            decimal theoryCT = nudTheoryCT.Value;
            decimal power = nudPower.Value;
            string line = cmbLine.Text.Trim();
            string isLink = chkIsLink.Checked ? "Y" : "N";

            //数据库是否有相关数据

            string strSql = string.Empty;
            //新增类型
            if (operateFlag == OperateStateEnum.Add)
            {
                string sqlStr = string.Format("SELECT 1 FROM  M_Machine_T WHERE	AssetNo='{0}';", assetNo);
                DataTable dt = DBUtil.GetDataTable(sqlStr);
                if (dt.Rows.Count > 0)
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "已有【" + assetNo + "】相同的记录，重复";
                    return;
                }
                strSql = string.Format(@"INSERT INTO M_Machine_T(AssetNo,MachineName,MachineNo,TheoryCT,Power,MachineCategory,IsLink,Line) 
                         VALUES('{0}',N'{1}','{2}','{3}','{4}',N'{5}','{6}',N'{7}');", assetNo, machineName, machineNo, theoryCT, power, machineCategory, isLink, line);
            }
            if (operateFlag == OperateStateEnum.Edit)
            {
                strSql = string.Format("UPDATE M_Machine_T SET MachineName=N'{1}',MachineNo='{2}',TheoryCT='{3}',Power='{4}',MachineCategory=N'{5}',IsLink='{6}',Line=N'{7}' WHERE MachineCode='{0}';",
                                              machineCode, machineName, machineNo, theoryCT, power, machineCategory, isLink, line);
            }

            DBUtil.ExecSQL(strSql);

            operateFlag = OperateStateEnum.Defualt;
            FormUtil.ClearControls(this.gbMachine);
            RefreshControlState();
            RefreshGridData();
        }
        #endregion

        #region 删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (sgridMachine.ActiveRow == null || sgridMachine.ActiveRow.Rows.Count > 0)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            GridRow row = (GridRow)sgridMachine.ActiveRow;
            string machineCode = row["gcMachineCode"].Value.ToString().Trim();
            if (MessageBox.Show("你确定要删除:【 编码:" + machineCode + " 】吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string strSql = string.Format(@"DELETE M_Machine_T WHERE MachineCode='{0}'; ", machineCode);
                DBUtil.ExecSQL(strSql);
                FormUtil.ClearControls(this.gbMachine);
                RefreshGridData();
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
                ofd.Title = "请上传设备信息";
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
                //文件数据导入到DataTable数据类型中
                DataTable dt = ExcelUtil.ReadFromExcelFile(SelectedFilePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //判断字段是否合规
                    if (!dt.Columns.Contains("资产编号") || !dt.Columns.Contains("设备名称") || !dt.Columns.Contains("设备编号") ||
                         !dt.Columns.Contains("理论CT/S") || !dt.Columns.Contains("功率(KW)") || !dt.Columns.Contains("设备类型") ||
                         !dt.Columns.Contains("是否连接")||!dt.Columns.Contains("产线") )
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
                        string assetNo = row["资产编号"].ToString();
                        string machineName = row["设备名称"].ToString();
                        string machineNo = row["设备编号"].ToString();
                        string theoryCT = row["理论CT/S"].ToString().Length > 0 ? row["理论CT/S"].ToString() : "0";
                        string power = row["功率(KW)"].ToString().Length > 0 ? row["功率(KW)"].ToString() : "0";
                        string machineCategory = row["设备类型"].ToString();
                        string isLink = row["是否连接"].ToString();
                        string line = row["产线"].ToString();


                        sql += string.Format(@"
                                   IF NOT EXISTS(SELECT 1 FROM M_Machine_T WHERE AssetNo='{0}' ) BEGIN
                                        INSERT INTO M_Machine_T(AssetNo,MachineName,MachineNo,TheoryCT,Power,MachineCategory,IsLink,Line)  
                                        VALUES('{0}',N'{1}','{2}','{3}','{4}',N'{5}','{6}',N'{7}');
                                   END",
                                   assetNo, machineName, machineNo, theoryCT, power, machineCategory,isLink, line);
                        if (i % 100 == 0 || i == dt.Rows.Count - 1)
                        {
                            DBUtil.ExecSQL(sql);
                            sql = "";
                        }
                    }

                    //判断导入结果
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "导入成功";
                    RefreshGridData();
                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmBaseMachine), null, ex);
            }
        }
        #endregion

        #region 导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = MachineDT;
                //列标题 
                dt.Columns["AssetNo"].Caption = "资产编号";
                dt.Columns["MachineCode"].Caption = "设备编码";
                dt.Columns["MachineName"].Caption = "设备名称";
                dt.Columns["MachineNo"].Caption = "设备编号";
                dt.Columns["TheoryCT"].Caption = "理论CT/S";
                dt.Columns["Power"].Caption = "功率(KW)";
                dt.Columns["MachineCategory"].Caption = "设备类型";
                dt.Columns["IsLink"].Caption = "是否连接";
                dt.Columns["Line"].Caption = "产线";

                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "设备信息";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseMachine), null, ex);
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

        #region 上报记录
        private void tsmiMachineReport_Click(object sender, EventArgs e)
        {
            FrmMachineReport frm = null;
            if (string.IsNullOrWhiteSpace(tbMachineCode.Text))
            {
                frm = new FrmMachineReport();
            }
            else
            {
                frm = new FrmMachineReport(Convert.ToInt32(tbMachineCode.Text));
            }
            frm.ShowDialog();
        }
        #endregion

        #region 报警代码
        private void tsmiWarnCode_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (string.IsNullOrWhiteSpace(tbMachineCode.Text))
            {
                labMessage.Text = "请选中一个设备";
                labMessage.ForeColor = Color.Red;
                return;
            }
            FrmWarnCode frm = null;
            if (string.IsNullOrWhiteSpace(tbMachineCode.Text))
            {
                frm = new FrmWarnCode();
            }
            else
            {
                frm = new FrmWarnCode(tbMachineCode.Text);
            }
            frm.ShowDialog();
        }
        #endregion

        #region 设备分布
        private void tsmiMachinePoint_Click(object sender, EventArgs e)
        {
            FrmMachineDistribute frm = new FrmMachineDistribute();
            frm.ShowDialog();
        }
        #endregion

        #region 设备状态
        private void tmsiMachineState_Click(object sender, EventArgs e)
        {
            FrmMachineState frm = new FrmMachineState();
            frm.ShowDialog();
        }
        #endregion

        #region 数据表相关
        /// <summary>
        /// 刷新机台数据表的数据源
        /// </summary>
        private void RefreshGridData()
        {
            string strSql = @"SELECT AssetNo,MachineCode,MachineName,MachineNo,TheoryCT,Power,MachineCategory,IsLink,Line FROM	M_Machine_T  WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(cmbAssetNo.Text.Trim()))
            {
                strSql += string.Format(" AND AssetNo like '%{0}%'", cmbAssetNo.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(tbMachineCode.Text.Trim()))
            {
                strSql += string.Format(" AND MachineCode='{0}'", tbMachineCode.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(tbMachineName.Text.Trim()))
            {
                strSql += string.Format(" AND MachineName like '%{0}%'", tbMachineName.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(cmbLine.Text.Trim()))
            {
                strSql += string.Format(" AND Line = '{0}'", cmbLine.Text.Trim());
            }
            strSql += " Order by MachineCode ASC";
            MachineDT = DBUtil.GetDataTable(strSql);

            dgvWarnCode.DataSource = null;
            sgridMachine.PrimaryGrid.Rows.Clear();

            //根据什么来分组(设备或产线)
            string groupColName = swbtnShow.Value ? "MachineName" : "Line";
            List<string> group = MachineDT.AsEnumerable().Select(t => t.Field<string>(groupColName)).ToList();
            group = group.Distinct().ToList();
            GridPanel panel = sgridMachine.PrimaryGrid;
            foreach (string groupValue in group)
            {
                DataRow[] rows = MachineDT.Select(groupColName + "='" + groupValue + "'");
                GridRow gr = GetNewParentRow(rows[0], rows.Length);
                gr.CellStyles.Default.Background.Color1 = Color.GhostWhite;
                foreach (DataRow row in rows)
                {
                    gr.Rows.Add(GetNewRow(row));
                }
                panel.Rows.Add(gr);
            }
            sgridMachine.PrimaryGrid.ClearSelectedRows();

        }


        //数据检查
        private string CheckFormData()
        {
            string msg = "";
            if (cmbAssetNo.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cmbAssetNo.Text))
            {
                msg = "资产编号未选择";
            }
            else if (string.IsNullOrWhiteSpace(tbMachineNo.Text))
            {
                msg = "设备编号不能为空";
            }
            else if (string.IsNullOrWhiteSpace(tbMachineName.Text))
            {
                msg = "设备别名不能为空";
            }
            return msg;
        }

        //创建数据表根行
        private GridRow GetNewParentRow(DataRow row, int count)
        {
            return new GridRow(
                "(" + count + "台)",
                null,
                row["MachineName"],
                null,
                null,
                null,
                row["MachineCategory"],
                null,
                row["Line"]);
        }
        //创建数据表行
        private GridRow GetNewRow(DataRow row)
        {
            return new GridRow(
                row["AssetNo"],
                row["MachineCode"],
                row["MachineName"],
                row["MachineNo"],
                row["TheoryCT"],
                row["Power"],
                row["MachineCategory"],
                row["IsLink"],
                row["Line"]);
        }

        //数据表行选中事件
        private void sgridMachine_SelectionChanged(object sender, GridEventArgs e)
        {
            GridRow gc = sgridMachine.ActiveRow as GridRow;
            if (gc == null) return;
          
            //如果没有子节点:表示选中具体机台
            if (gc.Rows.Count <= 0)
            {
                cmbAssetNo.Text = gc["gcAssetNo"].Value == null ? null : gc["gcAssetNo"].Value.ToString();
                //加载报警代码
                string sql = string.Format("SELECT WarnCode,WarnDesc FROM M_MachineWarnCode_T WHERE MachineCode=N'{0}';", gc["gcMachineCode"].Value.ToString());
                DataTable dt = DBUtil.GetDataTable(sql);
                dgvWarnCode.DataSource = dt;
            }
            else
            {
                cmbAssetNo.Text = null;
                dgvWarnCode.DataSource = null;
            }
            tbMachineCode.Text = gc["gcMachineCode"].Value == null ? null : gc["gcMachineCode"].Value.ToString();
            tbMachineName.Text = gc["gcMachineName"].Value == null ? null : gc["gcMachineName"].Value.ToString();
            tbMachineNo.Text = gc["gcMachineNo"].Value == null ? null : gc["gcMachineNo"].Value.ToString();
            nudTheoryCT.Text = gc["gcTheoryCT"].Value == null ? null : gc["gcTheoryCT"].Value.ToString();
            nudPower.Text = gc["gcPower"].Value == null ? null : gc["gcPower"].Value.ToString();
            tbMachineCategory.Text = gc["gcMachineCategory"].Value == null ? null : gc["gcMachineCategory"].Value.ToString();
            chkIsLink.Checked = gc["gcIsLink"].Value == null ? false : gc["gcIsLink"].Value.ToString() == "Y" ? true : false;
            cmbLine.Text = gc["gcLine"].Value == null ? null : gc["gcLine"].Value.ToString();

        }
        #endregion

        #region 其他

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

                    cmbAssetNo.Enabled = true;
                    tbMachineCode.Enabled = false;
                    tbMachineName.Enabled = true;
                    tbMachineNo.Enabled = true;
                    tbMachineCategory.Enabled = true;
                    nudTheoryCT.Enabled = true;
                    nudPower.Enabled = true;
                    chkIsLink.Enabled = true;
                    cmbLine.Enabled = true;
                    sgridMachine.Enabled = false;
                    FormUtil.ClearControls(this.gbMachine);
                    break;
                case OperateStateEnum.Edit:
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    cmbAssetNo.Enabled = false;
                    tbMachineCode.Enabled = false;
                    tbMachineName.Enabled = true;
                    tbMachineNo.Enabled = true;
                    tbMachineCategory.Enabled = true;
                    nudTheoryCT.Enabled = true;
                    nudPower.Enabled = true;
                    chkIsLink.Enabled = true;
                    cmbLine.Enabled = true;
                    sgridMachine.Enabled = false;
                    break;
                case OperateStateEnum.Defualt:
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiSave.Enabled = false;
                    tsmiDelete.Enabled = true;
                    tsmiBack.Enabled = false;

                    cmbAssetNo.Enabled = true;
                    tbMachineName.Enabled = true;
                    tbMachineCode.Enabled = true;
                    tbMachineNo.Enabled = false;
                    nudTheoryCT.Enabled = false;
                    nudPower.Enabled = false;
                    tbMachineCategory.Enabled = false;
                    chkIsLink.Enabled = false;
                    cmbLine.Enabled = true;
                    sgridMachine.Enabled = true;
                    FormUtil.ClearControls(this.gbMachine);
                    break;
            }
        }
        #endregion

        private void cmbAssetNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AssetDT != null)
            {
                DataRow[] rows = AssetDT.Select(" AssetNo='" + cmbAssetNo.Text + "'");
                if (rows.Length == 1)
                {
                    txbAssetName.Text = rows[0]["AssetName"].ToString();
                }
            }
        }

        private void swbtnShow_ValueChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }

    }
}
