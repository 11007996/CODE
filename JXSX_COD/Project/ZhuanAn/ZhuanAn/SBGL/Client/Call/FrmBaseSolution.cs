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
using System.Speech.Synthesis;


namespace Call
{
    public partial class FrmBaseSolution : Form
    {
        //故障方案信息tab相关数据
        private List<string> tempFaultItems = new List<string>();//选中行的故障内容拆分数据
        private List<string> tempSolutionItems = new List<string>();//选中行的解决方案拆分数据
        //语音合成器
        SpeechSynthesizer ss = new SpeechSynthesizer();

        private OperateStateEnum operateFlag = OperateStateEnum.Defualt;//故障方案信息操作状态标记

        public FrmBaseSolution()
        {
            InitializeComponent();
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseSolution_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT FaultType  FROM M_FaultSolution_T ";
            DataTable dtFaultType = DBUtil.GetDataTable(sql);
            if (dtFaultType != null)
            {
                List<string> faultTypes = dtFaultType.AsEnumerable().Select(t => t.Field<string>("FaultType")).ToList();
                faultTypes.Insert(0, "");
                cmbFaultType.DataSource = faultTypes;
            }
            //刷新
            RefreshControlState();
            RefreshGridData();
        }
        #endregion

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshGridData();
        }
        #endregion

        #region 添加
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            tempFaultItems.Clear();
            tempSolutionItems.Clear();
            operateFlag = OperateStateEnum.Add;
            RefreshControlState();
        }
        #endregion

        #region 修改
        //修改
        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            if (dgvFaultSolution.CurrentRow.Index < 0)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            operateFlag = OperateStateEnum.Edit;
            RefreshControlState();

            dgvFaultSolution_CellClick(null, null);

        }
        #endregion

        #region 保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //控件数据
            string faultType = cmbFaultType.Text.Trim();
            string machineType = tbMachineType.Text.Trim();
            string autoHelpFlag = chkAutoHelpFlag.Checked ? "Y" : "N";
            int maxHandleTimes = Convert.ToInt32(nudMaxHandleTimes.Value);
            int maxHelpTimes = Convert.ToInt32(nudMaxHelpTimes.Value);
            string faults = string.Join("/", tempFaultItems);
            string solutions = string.Join("/", tempSolutionItems);
            string imitateSound = tbImitateSound.Text.Trim();

            //数据验证
            if (string.IsNullOrWhiteSpace(faultType))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "故障类型不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(machineType))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "机台类型不能为空";
                return;
            }
            if (tempFaultItems.Count != tempSolutionItems.Count)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "故障项数与解决方案项数不一致";
                return;
            }

            //数据库是否有相关数据
            string sqlStr = string.Format("SELECT 1 FROM	M_MachineType_T WHERE	MachineType = '{0}';SELECT 1 FROM	M_FaultSolution_T WHERE	MachineType = '{0}';", machineType);
            DataSet ds = DBUtil.GetDataSet(sqlStr, new string[] { "dtMachineType", "dtFaultSolution" });
            if (ds == null) return;
            DataTable dtType = ds.Tables["dtMachineType"];
            DataTable dtSolution = ds.Tables["dtFaultSolution"];

            string strSql = string.Empty;
            //新增类型
            if (operateFlag == OperateStateEnum.Add)
            {
                if (dtType.Rows.Count > 0)
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "机台类型名称重复";
                    return;
                }
                strSql = string.Format("INSERT INTO M_MachineType_T VALUES('{0}');", machineType);
            }
            if (operateFlag == OperateStateEnum.Edit && dtType.Rows.Count <= 0)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "机台类型不存在";
                return;
            }
            //故障解决方案
            if (dtSolution.Rows.Count > 0)
            {
                strSql += string.Format("UPDATE M_FaultSolution_T SET FaultType=N'{1}',FaultItems=N'{2}',SolutionItems=N'{3}',MaxHandleTimes='{4}',MaxHelpTimes='{5}',AutoHelpFlag='{6}',ImitateSound=N'{7}' WHERE MachineType='{0}';", machineType, faultType, faults, solutions, maxHandleTimes, maxHelpTimes, autoHelpFlag, imitateSound);
            }
            else
            {
                strSql += string.Format("INSERT INTO M_FaultSolution_T(MachineType,FaultType,FaultItems,SolutionItems,MaxHandleTimes,MaxHelpTimes,AutoHelpFlag,ImitateSound) VALUES(N'{0}',N'{1}',N'{2}',N'{3}','{4}','{5}','{6}',N'{7}');", machineType, faultType, faults, solutions, maxHandleTimes, maxHelpTimes, autoHelpFlag, imitateSound);
            }
            DBUtil.ExecSQL(strSql);

            operateFlag = OperateStateEnum.Defualt;
            FormUtil.ClearControls(this.gbSolution);
            RefreshControlState();
            RefreshGridData();
        }
        #endregion

        #region 删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvFaultSolution.CurrentRow.Index < 0)
            {
                labMessage.Text = "请选中一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            string machineType = dgvFaultSolution[0, dgvFaultSolution.CurrentRow.Index].Value.ToString().Trim();
            if (MessageBox.Show("你确定要删除:【 " + machineType + " 】吗？\n提示:\n\t关联的处理时间\n\t关联的故障解决方案\n\t线体关联的此机台\n都将删除。", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string strSql = string.Format(@"DELETE M_MachineType_T WHERE MachineType='{0}';
                                                DELETE M_FaultSolution_T WHERE MachineType='{0}' ;
                                                DELETE M_LineMachines_T WHERE Machine='{0}';", machineType);
                DBUtil.ExecSQL(strSql);
                FormUtil.ClearControls(this.gbSolution);
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
                ofd.Title = "请上传故障方案信息";
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
                    if (!dt.Columns.Contains("机台类型") || !dt.Columns.Contains("故障类型") || !dt.Columns.Contains("最大处理时间(分钟)") || !dt.Columns.Contains("最大支援时间(分钟)") || !dt.Columns.Contains("自动支援") || !dt.Columns.Contains("故障内容") || !dt.Columns.Contains("解决方案") || !dt.Columns.Contains("模拟声音"))
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
                        string machineType = Regex.Replace(row["机台类型"].ToString(), @"[\r\n]", "");
                        string faultType = Regex.Replace(row["故障类型"].ToString(), @"[\r\n]", "");
                        string faultItems = Regex.Replace(row["故障内容"].ToString(), @"[\r\n]", "");
                        string solutionItems = Regex.Replace(row["解决方案"].ToString(), @"[\r\n]", "");
                        string maxHandleTimes = row["最大处理时间(分钟)"].ToString();
                        string maxHelpTimes = row["最大支援时间(分钟)"].ToString();
                        string autoHelpFlag = row["自动支援"].ToString();
                        string imitateSound = row["模拟声音"].ToString();

                        sql += string.Format(@"
                                IF NOT EXISTS(SELECT 1 FROM M_MachineType_T WHERE MachineType=N'{0}') BEGIN                                  
                                    INSERT INTO M_MachineType_T(MachineType) VALUES(N'{0}');
                                END
                                IF  EXISTS(SELECT 1 FROM M_FaultSolution_T WHERE MachineType=N'{0}') BEGIN                                  
                                  DELETE M_FaultSolution_T WHERE MachineType=N'{0}';
                                END
                                INSERT INTO M_FaultSolution_T(MachineType,FaultType,FaultItems,SolutionItems,MaxHandleTimes,MaxHelpTimes,AutoHelpFlag,ImitateSound) 
                                VALUES (N'{0}',N'{1}',N'{2}',N'{3}','{4}','{5}','{6}',N'{7}');
                                ", machineType, faultType, faultItems, solutionItems, maxHandleTimes, maxHelpTimes, autoHelpFlag, imitateSound);
                        if (i % 50 == 0 || i == dt.Rows.Count - 1)
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
                LogHelper.Error(typeof(FrmBaseSolution), null, ex);
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
                for (int c = 0; c < dgvFaultSolution.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvFaultSolution.Columns[c].Name.ToString());
                    dc.Caption = dgvFaultSolution.Columns[c].HeaderText.ToString();
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvFaultSolution.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvFaultSolution.Columns.Count; c++)
                    {
                        dr[c] = Convert.ToString(dgvFaultSolution.Rows[r].Cells[c].EditedFormattedValue);
                    }
                    dt.Rows.Add(dr);
                }
                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "故障方案";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseSolution), null, ex);
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

        #region 产线机台
        private void tsmLineMachine_Click(object sender, EventArgs e)
        {
            FrmLineMac fr = new FrmLineMac();
            fr.ShowDialog();
        }
        #endregion

        #region 产线品管
        private void tsmiLineQC_Click(object sender, EventArgs e)
        {
            FrmLineQC fr = new FrmLineQC();
            fr.ShowDialog();
        }
        #endregion

        #region 其他
        /// <summary>
        /// 刷新故障方案数据表的数据源
        /// </summary>
        private void RefreshGridData()
        {
            string strSql = @"SELECT MachineType,FaultType,MaxHandleTimes,MaxHelpTimes,AutoHelpFlag,FaultItems,SolutionItems,'' Validity,ImitateSound
                            FROM	M_FaultSolution_T  WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(cmbFaultType.Text.Trim()))
            {
                strSql += string.Format(" AND FaultType like '{0}'", cmbFaultType.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(tbMachineType.Text.Trim()))
            {
                strSql += string.Format(" AND MachineType like '{0}'", tbMachineType.Text.Trim());
            }
            strSql += " ORDER BY FaultType desc,MachineType";
            dgvFaultSolution.DataSource = DBUtil.GetDataTable(strSql);
            labMessage.Text = "数据加载完成";
        }

        /// <summary>
        /// 添加故障项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultItemsAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string text = tbFaultItem.Text.Trim();
            if (string.IsNullOrWhiteSpace(text) || text.Contains("/"))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "添加的内容非法(不能为空或包含'/'字符)";
                return;
            }
            if (lsbFaultItems.Items.Contains(text))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "已存在相同的内容,重复添加";
                return;
            }
            lsbFaultItems.DataSource = null;
            tempFaultItems.Add(text);
            lsbFaultItems.DataSource = tempFaultItems;
            labFaultCount.Text = tempFaultItems.Count.ToString();
        }

        /// <summary>
        /// 删除故障项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFaultItemsDel_Click(object sender, EventArgs e)
        {
            if (lsbFaultItems.SelectedIndex < 0)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "未选中故障内容项";
                return;
            }
            if (MessageBox.Show("你确定要删除:【 " + lsbFaultItems.SelectedItem.ToString().Trim() + " 】吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tempFaultItems.RemoveAt(lsbFaultItems.SelectedIndex);
                lsbFaultItems.DataSource = null;
                lsbFaultItems.DataSource = tempFaultItems;
                labFaultCount.Text = tempFaultItems.Count.ToString();
            }
        }

        /// <summary>
        /// 添加解决方案项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSolutionItemsAdd_Click(object sender, EventArgs e)
        {
            string text = tbSolutionItem.Text.Trim();
            if (string.IsNullOrWhiteSpace(text) || text.Contains("/"))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "添加的内容非法(不能为空或包含'/'字符)";
                return;
            }
            lsbSolutionItems.DataSource = null;
            tempSolutionItems.Add(text);
            lsbSolutionItems.DataSource = tempSolutionItems;
            labSolutionCount.Text = tempSolutionItems.Count.ToString();
        }

        /// <summary>
        /// 删除解决方案项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSolutionItemsDel_Click(object sender, EventArgs e)
        {
            if (lsbSolutionItems.SelectedIndex < 0)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "未选中解决方案项";
                return;
            }
            if (MessageBox.Show("你确定要删除:【 " + lsbSolutionItems.SelectedItem.ToString().Trim() + " 】吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tempSolutionItems.RemoveAt(lsbSolutionItems.SelectedIndex);
                lsbSolutionItems.DataSource = null;
                lsbSolutionItems.DataSource = tempSolutionItems;
                labSolutionCount.Text = tempSolutionItems.Count.ToString();
            }
        }

        /// <summary>
        /// 单击故障方案表的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFaultSolution_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFaultSolution.CurrentRow.Index >= 0)
            {
                //--------------将数据行信息填入表单控件中---------------
                //水平滚动条
                lsbSolutionItems.HorizontalScrollbar = false;
                //数据信息
                int rowindex = dgvFaultSolution.CurrentRow.Index;
                cmbFaultType.Text = dgvFaultSolution["dgcFaultType", rowindex].Value.ToString();
                tbMachineType.Text = dgvFaultSolution["dgcMachineType", rowindex].Value.ToString();
                nudMaxHandleTimes.Text = dgvFaultSolution["dgcMaxHandleTimes", rowindex].Value.ToString();
                nudMaxHelpTimes.Text = dgvFaultSolution["dgcMaxHelpTimes", rowindex].Value.ToString();
                chkAutoHelpFlag.Checked = dgvFaultSolution["dgcAutoHelpFlag", rowindex].Value.ToString().Equals("Y") ? true : false;
                tbImitateSound.Text = dgvFaultSolution["dgcImitateSound", rowindex].Value.ToString();
                //故障方案
                tempFaultItems = dgvFaultSolution["dgcFaultItems", rowindex].Value.ToString().Split('/').ToList();
                lsbFaultItems.DataSource = tempFaultItems;
                tempSolutionItems = dgvFaultSolution["dgcSolutionItems", rowindex].Value.ToString().Split('/').ToList();
                lsbSolutionItems.DataSource = tempSolutionItems;
                labFaultCount.Text = tempFaultItems.Count.ToString();
                labSolutionCount.Text = tempSolutionItems.Count.ToString();

                lsbSolutionItems.HorizontalScrollbar = true;
            }
        }

        /// <summary>
        /// 格式化故障方案表的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFaultSolution_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // （故障内容与解决方案的数量与顺序校验）
            if (dgvFaultSolution.Columns[e.ColumnIndex].HeaderText.Equals("校验状态"))
            {
                string fault = dgvFaultSolution["dgcFaultItems", e.RowIndex].Value.ToString();
                string[] faultArr = fault.Split('/');
                string solution = dgvFaultSolution["dgcSolutionItems", e.RowIndex].Value.ToString();
                string[] solutionArr = solution.Split('/');
                if (faultArr.Length != solutionArr.Length)
                {
                    e.Value = "项数异常";
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                    return;
                }
                for (int i = 0; i < faultArr.Length; i++)
                {
                    if (!solutionArr[i].Contains(faultArr[i]))
                    {
                        e.Value = "匹配异常";
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                        return;
                    }
                }
                e.Value = "通过";
                e.CellStyle.BackColor = Color.Green;
                e.CellStyle.ForeColor = Color.White;
            }
        }

        //播放
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (tbImitateSound.Text.Length > 0)
            {
                ss.SpeakAsync(tbImitateSound.Text);
            }
            else
            {
                ss.SpeakAsync(tbMachineType.Text);
            }
        }

        /// <summary>
        /// 刷新故障方案信息控件状态
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

                    tbMachineType.Enabled = true;
                    chkAutoHelpFlag.Enabled = true;
                    nudMaxHandleTimes.Enabled = true;
                    nudMaxHelpTimes.Enabled = true;
                    tbImitateSound.Enabled = true;
                    tbFaultItem.Enabled = true;
                    tbSolutionItem.Enabled = true;
                    btnFaultItemsAdd.Enabled = true;
                    btnFaultItemsDel.Enabled = true;
                    btnSolutionItemsAdd.Enabled = true;
                    btnSolutionItemsDel.Enabled = true;
                    labFaultCount.Text = "0";
                    labSolutionCount.Text = "0";
                    dgvFaultSolution.Enabled = false;
                    FormUtil.ClearControls(this.gbSolution);
                    break;
                case OperateStateEnum.Edit:
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    tbMachineType.Enabled = false;
                    chkAutoHelpFlag.Enabled = true;
                    nudMaxHandleTimes.Enabled = true;
                    nudMaxHelpTimes.Enabled = true;
                    tbImitateSound.Enabled = true;
                    tbFaultItem.Enabled = true;
                    tbSolutionItem.Enabled = true;
                    btnFaultItemsAdd.Enabled = true;
                    btnFaultItemsDel.Enabled = true;
                    btnSolutionItemsAdd.Enabled = true;
                    btnSolutionItemsDel.Enabled = true;

                    dgvFaultSolution.Enabled = false;
                    break;
                case OperateStateEnum.Defualt:
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiSave.Enabled = false;
                    tsmiDelete.Enabled = true;
                    tsmiBack.Enabled = false;

                    tbMachineType.Enabled = true;
                    chkAutoHelpFlag.Enabled = false;
                    nudMaxHandleTimes.Enabled = false;
                    nudMaxHelpTimes.Enabled = false;
                    tbImitateSound.Enabled = false;
                    tbFaultItem.Enabled = false;
                    tbSolutionItem.Enabled = false;
                    btnFaultItemsAdd.Enabled = false;
                    btnFaultItemsDel.Enabled = false;
                    btnSolutionItemsAdd.Enabled = false;
                    btnSolutionItemsDel.Enabled = false;
                    labFaultCount.Text = "0";
                    labSolutionCount.Text = "0";
                    dgvFaultSolution.Enabled = true;
                    FormUtil.ClearControls(this.gbSolution);
                    break;
            }
        }
        #endregion

     


    }
}
