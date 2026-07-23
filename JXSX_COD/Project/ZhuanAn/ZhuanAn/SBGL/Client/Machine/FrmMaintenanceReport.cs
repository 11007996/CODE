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
using DevComponents.DotNetBar.SuperGrid;
using Newtonsoft.Json;
using DevComponents.DotNetBar.Controls;
using Common;
using System.Globalization;


namespace Machine
{
    public partial class FrmMaintenanceReport : Form
    {
        //数据库源数据
        private DataTable ItemDT;//源数据：保养项目

        //DataGrid绑定数据源（源数据解析后得到）
        private DataTable DayDT;//模板数据：日表
        private DataTable WeekDT;//模板数据：周表
        private DataTable MonthDT;//模板数据：月表
        private DataTable YearMonthDT;//模板数据：年月表
        private DataTable QuarterDT;//模板数据：季表
        private DataTable YearDT;//模板数据：年表

        private DataTable EditMarkDT;//编辑过的表与列标记

        private OperateStateEnum operateFlag = OperateStateEnum.Defualt;//基础信息操作状态标记

        //日期相关标记值
        private List<int> DayStampList = new List<int>();
        private List<int> WeekStampList = new List<int>();
        private List<int> MonthStampList = new List<int>();
        private List<int> YearMonthStampList = new List<int>();
        private List<int> QuarterStampList = new List<int>();
        private List<int> YearStampList = new List<int>();

        public FrmMaintenanceReport(string assetNo, string year, string month)
        {
            InitializeComponent();
            InitDataGrid();

            //资产信息
            txbAssetNo.Text = assetNo;
            string sql = string.Format("SELECT * FROM A_AssetInfo_T WHERE AssetNo='{0}'", assetNo);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt.Rows.Count == 1)
            {
                txbAssetName.Text = dt.Rows[0]["AssetName"].ToString();
                txbModel.Text = dt.Rows[0]["Model"].ToString();
                txbCostCenter.Text = dt.Rows[0]["CostCenter"].ToString();
            }

            //保养表提示
            SetReportTip();

            //年份
            IList<int> years = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                years.Add(DateTime.Now.Year - i);
            }
            cmbYear.DataSource = years;
            cmbYear.Text = year;
            //月份
            cmbMonth.Text = month;
        }

        #region 初始化DataGridView控件布局
        //初始化网格数据控件
        private void InitDataGrid()
        {
            //维护周期表:日
            List<DataGridViewTextBoxColumn> days = new List<DataGridViewTextBoxColumn>();
            for (int i = 1; i <= 31; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                col.Name = "dgcDay_" + i;
                col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                days.Add(col);
            }
            dgvDay.AutoGenerateColumns = false;
            dgvDay.Columns.AddRange(days.ToArray());
            dgvDay.TopLeftHeaderCell.ToolTipText = "日表";

            //维护周期表:周
            List<DataGridViewTextBoxColumn> weeks = new List<DataGridViewTextBoxColumn>();
            for (int i = 1; i <= 5; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                col.Name = "dgcWeek_" + i;
                col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = (7F / 31F) * 100;
                if (i == 5)
                {
                    col.FillWeight = (3F / 31F) * 100;
                }
                weeks.Add(col);
            }
            dgvWeek.AutoGenerateColumns = false;
            dgvWeek.Columns.AddRange(weeks.ToArray());
            dgvWeek.TopLeftHeaderCell.ToolTipText = "周表";

            //维护周期表:月
            DataGridViewTextBoxColumn month = new DataGridViewTextBoxColumn();
            month.HeaderText = "1";
            month.Name = "dgcMonth";
            month.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            month.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvMonth.AutoGenerateColumns = false;
            dgvMonth.Columns.Add(month);
            dgvMonth.TopLeftHeaderCell.ToolTipText = "月表";

            //维护周期表:年月
            List<DataGridViewTextBoxColumn> months = new List<DataGridViewTextBoxColumn>();
            for (int i = 1; i <= 12; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                col.Name = "dgcYearMonth_" + i;
                col.DataPropertyName = i.ToString();
                col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                months.Add(col);
            }
            dgvYearMonth.AutoGenerateColumns = false;
            dgvYearMonth.Columns.AddRange(months.ToArray());
            dgvYearMonth.TopLeftHeaderCell.ToolTipText = "月表";

            //维护周期表:季
            List<DataGridViewTextBoxColumn> quarters = new List<DataGridViewTextBoxColumn>();
            for (int i = 1; i <= 4; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                col.Name = "dgcQuarter_" + i;
                col.DataPropertyName = i.ToString();
                col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = 25;
                quarters.Add(col);
            }
            dgvQuarter.AutoGenerateColumns = false;
            dgvQuarter.Columns.AddRange(quarters.ToArray());
            dgvQuarter.TopLeftHeaderCell.ToolTipText = "季表";

            //维护周期表:年
            DataGridViewTextBoxColumn year = new DataGridViewTextBoxColumn();
            year.HeaderText = "1";
            year.DataPropertyName = "1";
            year.Name = "dgcYear";
            year.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            year.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvYear.AutoGenerateColumns = false;
            dgvYear.Columns.Add(year);
            dgvYear.TopLeftHeaderCell.ToolTipText = "年表";
            //绑定属性名称
            BindDgvColmnDataPropertyName();
        }

        

        //绑定属性名,主要针对月表
        private void BindDgvColmnDataPropertyName()
        {
            if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
            {
                DayStampList.Clear();
                WeekStampList.Clear();
                MonthStampList.Clear();
                int year = Convert.ToInt32(cmbYear.Text);
                int month = Convert.ToInt32(cmbMonth.Text);
                int days = DateTime.DaysInMonth(year, month);//指定月份天数
                DateTime dayOne = Convert.ToDateTime(year + "-" + month + "-1");//指定月份第一天
                DateTime dayLast = Convert.ToDateTime(year + "-" + month + "-" + days);//指定月份最后一天
                //日表
                int dayStamp = dayOne.DayOfYear;

                for (int i = 1; i < dgvDay.Columns.Count; i++)
                {
                    if (i <= days)
                    {
                        dgvDay.Columns[i].DataPropertyName = (dayStamp + i - 1).ToString();
                        DayStampList.Add(dayStamp + i - 1);
                    }
                    else
                    {
                        dgvDay.Columns[i].DataPropertyName = null;
                    }
                }
                //周表
                CreateWeekLayout(year,month);
                GregorianCalendar gregorianCalendar = new GregorianCalendar();
                int weekStamp = gregorianCalendar.GetWeekOfYear(dayOne, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//第一天所属周
                int lastWeekStamp = gregorianCalendar.GetWeekOfYear(dayLast, CalendarWeekRule.FirstDay, DayOfWeek.Monday);//最后一天所属性周
                for (int i = 1; i < dgvWeek.Columns.Count; i++)
                {
                    if ((weekStamp + i - 1) <= lastWeekStamp)
                    {
                        dgvWeek.Columns[i].DataPropertyName = (weekStamp + i - 1).ToString();
                        WeekStampList.Add((weekStamp + i - 1));
                    }
                    else
                    {
                        dgvWeek.Columns[i].DataPropertyName = null;
                    }
                }
                //月表
                dgvMonth.Columns[1].DataPropertyName = cmbMonth.Text;
                MonthStampList.Add(Convert.ToInt32(cmbMonth.Text));
            }
            else
            {
                YearMonthStampList.Clear();
                QuarterStampList.Clear();
                YearStampList.Clear();

                //月表
                for (int i = 1; i <= 12; i++)
                {
                    dgvYearMonth.Columns[i].DataPropertyName = i.ToString();
                    YearMonthStampList.Add(i);
                }
                //季表
                for (int i = 1; i <= 4; i++)
                {
                    dgvQuarter.Columns[i].DataPropertyName = i.ToString();
                    QuarterStampList.Add(i);
                }
                //年表
                dgvYear.Columns[1].DataPropertyName = "1";
                YearStampList.Add(1);
            }
        }


        private void CreateWeekLayout(int year, int month)
        {
            //清除原有的列
            for (int i = dgvWeek.Columns.Count - 1; i > 0;i-- )
            {
                dgvWeek.Columns.RemoveAt(i);
            }
            DateTime dayOne = Convert.ToDateTime(year + "-" + month + "-1");//指定月份第一天
            //星期
            DayOfWeek week = dayOne.DayOfWeek;
            int weekMark = 0;
            if (week == DayOfWeek.Sunday)
            {
                weekMark = 7;
            }
            else
            {
                weekMark = (int)week;
            }

            int firstWeekDays = 7;//第一周的天数
            int weekCount = 0;//显示的周数
            if (weekMark > 1)
            {
                firstWeekDays = 7 - weekMark + 1;
                weekCount = 1 + (int)(Math.Ceiling((decimal)(31 - firstWeekDays) / 7));
            }
            else
            {
                weekCount = (int)(Math.Ceiling((decimal)31 / 7));
            }

            List<DataGridViewTextBoxColumn> weeks = new List<DataGridViewTextBoxColumn>();
            for (int i = 1; i <= weekCount; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = i.ToString();
                col.Name = "dgcWeek_" + i;
                col.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                col.FillWeight = (7F / 31F) * 100;
                if (i == 1 && weekMark > 1)
                {//第一列，不是星期一特殊处理
                    col.FillWeight = (firstWeekDays / 31F) * 100;
                }
                if (i == weekCount)//最后一列处理
                {
                    int lastWeekDays = 31 - firstWeekDays - ((i - 2) * 7);
                    col.FillWeight = (lastWeekDays / 31F) * 100;
                }
                weeks.Add(col);
            }
            dgvWeek.AutoGenerateColumns = false;
            dgvWeek.Columns.AddRange(weeks.ToArray());
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseInfoMaintenance_Load(object sender, EventArgs e)
        {
            //权限
            if (BaseInfo.LoginUserRight == "A")
            {
                tsmiDelete.Visible = true;
            }
            else
            {
                tsmiDelete.Visible = false;
            }
            //编辑记录初始化
            EditMarkDT = new DataTable();
            EditMarkDT.Columns.Add("DataGrid", typeof(DataGridViewX));
            EditMarkDT.Columns.Add("ColumnIndex", typeof(int));

            //刷新
            RefreshDataGrid();
            RefreshControlState();
        }
        #endregion

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            RefreshDataGrid();
            RefreshControlState();
            labMessage.Text = "刷新完成";
            labMessage.ForeColor = Color.Green;
        }
        #endregion

        #region 添加
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            //检查数据
            string assetNo = txbAssetNo.Text;
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            if (string.IsNullOrWhiteSpace(assetNo))
            {
                labMessage.Text = "无资产编码，无法创建";
                labMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                labMessage.Text = "年份为空，无法创建";
                labMessage.ForeColor = Color.Red;
                return;
            }

            if (CheckExitisMaintenanceReport(txbAssetNo.Text.Trim(), year, month))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "添加失败，已存在保养报表";
                return;
            }
            string msg = string.Format("你确定要添加此保养报表吗？\r\r资产名称：{0}\r资产编号：{1}\r报表时间：{2}年", txbAssetName.Text, assetNo, year);
            msg += string.IsNullOrWhiteSpace(month) ? "" : month + "月";
            if (MessageBox.Show(msg, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                string sql = string.Format("INSERT INTO A_MaintenanceReport_T(AssetNo,Year,Month) VALUES('{0}',{1},NULLIF('{2}', '')) ;", assetNo, year, month);
                DBUtil.ExecSQL(sql);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "创建报表成功";
            }
        }
        #endregion

        #region 修改
        //修改
        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            if (!CheckExitisMaintenanceReport(txbAssetNo.Text.Trim(), year, month))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "修改失败，未找到保养报表";
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
            //数据验证
            //控件数据
            string assetNo = txbAssetNo.Text.Trim();
            string year = cmbYear.Text;
            string month = cmbMonth.Text.Trim();

            if (string.IsNullOrWhiteSpace(assetNo))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "资产编号不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "年份不能为空";
                return;
            }

            //添加时检查是否存在保养报表
            if (operateFlag == OperateStateEnum.Add)
            {
                if (!CheckExitisMaintenanceReport(assetNo, year, month))
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "添加失败，保养报表不存在";
                    return;
                }
            }

            try
            {
                //编辑去重
                DataView dv = EditMarkDT.DefaultView;
                List<string> colNams = new List<string>();
                foreach (DataColumn col in EditMarkDT.Columns) colNams.Add(col.ColumnName);
                EditMarkDT = dv.ToTable("EditMarkDT", true, colNams.ToArray());

                string sql = "";
                //遍历所有修改列
                foreach (DataRow row in EditMarkDT.Rows)
                {
                    string timeMark = ((DataGridViewX)row["DataGrid"]).Tag.ToString();
                    DataGridViewX dgv = (DataGridViewX)row["DataGrid"];
                    int columnIndex = Convert.ToInt32(row["ColumnIndex"]);
                    if (columnIndex > 0)
                    { //更新报表保养项目
                        string timeMarkStamp = dgv.Columns[columnIndex].DataPropertyName;
                        string rdIdName = "@RId_" + timeMark + "_" + timeMarkStamp;//变量名
                        //获取列维护记录详情
                        List<string> dvs = new List<string>();
                        foreach (DataGridViewRow viewR in dgv.Rows)
                        {
                            if (viewR.Cells[columnIndex].Value != null )
                            {
                                dvs.Add(string.Format("({0},N'{1}','{2}')", rdIdName, Convert.ToString(viewR.Cells[0].Value), Convert.ToString(viewR.Cells[columnIndex].Value)));
                            }
                        }

                        sql += string.Format(@"DECLARE {0} INT; 
                                            SELECT TOP(1) {0}=Id FROM A_MaintenanceReportD_T WHERE AssetNo='{1}' AND [Year]='{2}' AND TimeMark='{3}' AND TimeMarkStamp='{4}';
                                            --更新记录
                                            IF ISNULL({0},'')=''
                                            BEGIN
	                                            INSERT INTO A_MaintenanceReportD_T(AssetNo, [Year],  TimeMark,  TimeMarkStamp,UpdateUser,UpdateTime)VALUES('{1}','{2}','{3}','{4}','{5}',GETDATE())
	                                            SELECT {0} =  @@IDENTITY
                                            END
                                            ELSE
                                            BEGIN
	                                            UPDATE A_MaintenanceReportD_T SET UpdateUser='',UpdateTime=GETDATE() WHERE Id={0};
                                            END
                                            --删除原有的记录详情
                                            DELETE A_MaintenanceReportDV_T WHERE MRDId={0};", rdIdName, assetNo, year, timeMark, timeMarkStamp, BaseInfo.LoginUserNo);
                        if (dvs.Count > 0)
                        {
                            sql += string.Format("INSERT INTO A_MaintenanceReportDV_T(MRDId,ItemName,ItemValue)VALUES {0};", string.Join(",", dvs));
                        }
                    }
                }

                //执行更新
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    DBUtil.ExecSQL(sql);
                    EditMarkDT.Clear();
                    RefreshDataGrid();
                    operateFlag = OperateStateEnum.Defualt;
                    RefreshControlState();
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "保存成功";
                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "保存失败";
                LogHelper.Error(typeof(FrmMaintenanceReport), ex.Message);
            }
        }

        #endregion

        #region 删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string assetNo = txbAssetNo.Text;
            string year = cmbYear.Text.Trim();
            string month = cmbMonth.Text.Trim();

            string msg = string.Format("你确定要删除此保养报表吗？\r\r资产名称：{0}\r资产编号：{1}\r报表时间：{2}年{3}", txbAssetName.Text, assetNo, year, month);
            if (MessageBox.Show(msg, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string strSql = string.Format(@" DELETE A_MaintenanceReport_T WHERE AssetNo='{0}' AND [Year]='{1}' AND ISNULL([Month],'')='{2}';", assetNo, year, month);
                DBUtil.ExecSQL(strSql);
                RefreshDataGrid();
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
            }
        }
        #endregion

        #region 返回
        private void tsmiBack_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Defualt;
            RefreshDataGrid();
            RefreshControlState();
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
                ofd.Title = "请上传保养报表信息";
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
                string msg = "";
                bool isOk = false;
                if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
                {//导入月报表
                    DataTable oldDayDT = DayDT.Copy();
                    DataTable oldWeekDT = WeekDT.Copy();
                    DataTable oldMonthDT = MonthDT.Copy();
                    isOk = MaintenanceExcel.ReadFromExcelFile(SelectedFilePath, "M", DayDT, WeekDT, MonthDT, ref msg);
                    if (isOk)
                    {
                        EditMarkDT.Clear();
                        DiffViewWithImport(dgvDay, oldDayDT, DayDT);
                        DiffViewWithImport(dgvWeek, oldWeekDT, WeekDT);
                        DiffViewWithImport(dgvMonth, oldMonthDT, MonthDT);
                    }
                }
                else
                {//导入年报表
                    DataTable oldYearMonthDT = YearMonthDT.Copy();
                    DataTable oldQuarterDT = QuarterDT.Copy();
                    DataTable oldYearDT = YearDT.Copy();
                    isOk = MaintenanceExcel.ReadFromExcelFile(SelectedFilePath, "Y", YearMonthDT, QuarterDT, YearDT, ref msg);
                    if (isOk)
                    {
                        EditMarkDT.Clear();
                        DiffViewWithImport(dgvYearMonth, oldYearMonthDT, YearMonthDT);
                        DiffViewWithImport(dgvQuarter, oldQuarterDT, QuarterDT);
                        DiffViewWithImport(dgvYear, oldYearDT, YearDT);
                    }
                }

                //更新操作状态
                if (!isOk)
                {
                    labMessage.Text = msg;
                    labMessage.ForeColor = Color.Red;
                    RefreshDataGrid();
                    return;
                }
                else
                {
                    string year = cmbYear.Text;
                    string month = cmbMonth.Text;
                    if (CheckExitisMaintenanceReport(txbAssetNo.Text.Trim(), year, month))
                        operateFlag = OperateStateEnum.Edit;
                    else
                        operateFlag = OperateStateEnum.Add;
                    RefreshControlState();
                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmMaintenanceReport), null, ex);
            }
        }

        //比较原有数据与导入数据并更新单元格状态
        private void DiffViewWithImport(DataGridViewX dgv, DataTable oldDT, DataTable newDT)
        {

            bool allFlag = false;
            //行数或项目名称不相等则更新所有数据
            if (oldDT.Rows.Count == newDT.Rows.Count)
            {
                for (int i = 0; i < oldDT.Rows.Count; i++)
                {
                    if (Convert.ToString(oldDT.Rows[i]["ItemName"]) != Convert.ToString(newDT.Rows[i]["ItemName"]))
                    {
                        allFlag = true;
                        break;
                    }
                }
            }
            else
            {
                allFlag = true;
            }


            //单元格修改更新
            for (int r = 0; r < newDT.Rows.Count; r++)
            {
                for (int c = 0; c < newDT.Columns.Count; c++)
                {
                    if (allFlag || Convert.ToString(oldDT.Rows[r][c]) != Convert.ToString(newDT.Rows[r][c]))
                    {
                        EditMarkDT.Rows.Add(dgv, c);
                        dgv.Rows[r].Cells[c].Style.BackColor = Color.DarkOrange;
                        dgv.Rows[r].Cells[c].Style.ForeColor = Color.White;
                    }
                }
            }
        }
        #endregion

        #region 导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            labMessage.Text = "" ;
            try
            {
                string assetName = txbAssetName.Text;
                ////打开保存对话框保存
                string model = txbModel.Text.Trim();
                string assetNo = txbAssetNo.Text;
                string year = cmbYear.Text;
                string month = cmbMonth.Text;

                string info = ConvertReportInfo(assetName, model, assetNo, year, month);

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";

                dlg.FileName = assetName + "定期点检保养表" + year + month;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
                        MaintenanceExcel.WriteToExcel(dlg.FileName, "M", assetName, info, DayDT, WeekDT, MonthDT, rtxbMonthTip.Text);
                    else
                        MaintenanceExcel.WriteToExcel(dlg.FileName, "Y", assetName, info, YearMonthDT, QuarterDT, YearDT, rtxbYearTip.Text);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmMaintenanceReport), null, ex);
                labMessage.Text = "导出失败：原因" + ex.Message;
                labMessage.ForeColor = Color.Red;
            }
        }
        #endregion


        #region 数据表相关
        private void RefreshDataGrid()
        {
            labMessage.Text = "";
            //清楚原有数据
            dgvDay.DataSource = null;
            dgvWeek.DataSource = null;
            dgvMonth.DataSource = null;
            dgvYearMonth.DataSource = null;
            dgvQuarter.DataSource = null;
            dgvYear.DataSource = null;

            //数据检查
            string assetNo = txbAssetNo.Text;
            string year = cmbYear.Text;
            string month = cmbMonth.Text;
            if (string.IsNullOrWhiteSpace(assetNo))
            {
                labMessage.Text = "请输入资产编号";
                labMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrWhiteSpace(year))
            {
                labMessage.Text = "未知的年份";
                labMessage.ForeColor = Color.Red;
                return;
            }

            //执行查询
            string sql = "";
            if (!string.IsNullOrWhiteSpace(month))
            {//月表
                //日表
                sql += GetMaintenanceRecordSql(assetNo, year, "D", DayStampList);
                //周
                sql += GetMaintenanceRecordSql(assetNo, year, "W", WeekStampList);
                //月
                sql += GetMaintenanceRecordSql(assetNo, year, "M", MonthStampList);
                //执行sql查询
                DataSet ds = DBUtil.GetDataSet(sql, new string[] { "DayDT", "WeekDT", "MonthDT" });
                DayDT = ConvertDataToMode(ds.Tables["DayDT"], "D");
                WeekDT = ConvertDataToMode(ds.Tables["WeekDT"], "W");
                MonthDT = ConvertDataToMode(ds.Tables["MonthDT"], "M");
                dgvDay.DataSource = DayDT;
                dgvWeek.DataSource = WeekDT;
                dgvMonth.DataSource = MonthDT;
            }
            else
            {//年表
                //月表
                sql += GetMaintenanceRecordSql(assetNo, year, "M", YearMonthStampList);
                //周
                sql += GetMaintenanceRecordSql(assetNo, year, "Q", QuarterStampList);
                //月
                sql += GetMaintenanceRecordSql(assetNo, year, "Y", YearStampList);
                //执行sql查询
                DataSet ds = DBUtil.GetDataSet(sql, new string[] { "MonthDT", "QuarterDT", "YearDT" });
                YearMonthDT = ConvertDataToMode(ds.Tables["MonthDT"], "M");
                QuarterDT = ConvertDataToMode(ds.Tables["QuarterDT"], "Q");
                YearDT = ConvertDataToMode(ds.Tables["YearDT"], "Y");
                dgvYearMonth.DataSource = YearMonthDT;
                dgvQuarter.DataSource = QuarterDT;
                dgvYear.DataSource = YearDT;
            }
        }

        //获取接接的保养记录执行的SQL
        private string GetMaintenanceRecordSql(string assetNo, string year, string timeMark, List<int> stampList)
        {
            string sql = "";
            List<string> stampColNames = new List<string>();
            List<string> stampValues = new List<string>();
            foreach (int stamp in stampList)
            {
                stampColNames.Add("Max([" + stamp + "]) AS [" + stamp + "]");
                stampValues.Add("[" + stamp + "]");
            }
            sql = string.Format(@"SELECT ItemName,{0}  FROM( 
                        SELECT * FROM(
                        SELECT r.Id,r.TimeMarkStamp,i.ItemName,v.ItemValue
                        FROM A_MaintenanceReportD_T r 
                        LEFT JOIN A_MaintenanceReportItem_T i  ON r.AssetNo = i.AssetNo AND i.TimeMark='{4}'
                        LEFT JOIN A_MaintenanceReportDV_T v ON r.Id =v.MRDId AND v.ItemName=i.ItemName
                        WHERE r.[AssetNo]='{2}' AND  ISNULL(r.IsVisible,'Y')<>'N' AND r.[Year]='{3}' AND r.TimeMark='{4}'  AND r.TimeMarkStamp>={5}  AND r.TimeMarkStamp<={6}
                        )  AS d
                        PIVOT (
                            MAX(ItemValue)  FOR [TimeMarkStamp ] IN ({1})
                        ) AS PT) as g 
                        GROUP BY ItemName;", string.Join(",", stampColNames), string.Join(",", stampValues), assetNo, year, timeMark, stampList[0], stampList[stampList.Count - 1]);
            return sql;
        }

        //将源数据转为模板数据
        private DataTable ConvertDataToMode(DataTable dataDT, string timeMark)
        {
            if (dataDT == null || string.IsNullOrWhiteSpace(timeMark) || ItemDT == null) return null;
            //结构复制给绑定的表
            DataTable bindDT = dataDT.Clone();
            DataRow[] itemRows = ItemDT.Select("TimeMark='" + timeMark + "'");
            foreach (DataRow itemR in itemRows)
            {
                DataRow newR = bindDT.NewRow();
                DataRow[] dRow = dataDT.Select("ItemName='" + itemR["ItemName"].ToString() + "'");
                if (dRow != null && dRow.Length == 1)
                {
                    newR.ItemArray = dRow[0].ItemArray;
                }
                else
                {
                    newR["ItemName"] = itemR["ItemName"].ToString();
                }
                bindDT.Rows.Add(newR);
            }
            return bindDT;
        }

        //获取指定年的所有保养项目
        private void GetMaintenanceItem()
        {
            if (!string.IsNullOrWhiteSpace(cmbYear.Text) && !string.IsNullOrWhiteSpace(txbAssetNo.Text))
            {
                //当年设置的保养项目
                string sql = string.Format("SELECT * FROM A_MaintenanceReportItem_T WHERE AssetNo='{0}' AND Year='{1}' order by SortNo", txbAssetNo.Text, cmbYear.Text);
                ItemDT = DBUtil.GetDataTable(sql);
            }
        }
        #endregion

        #region 交互事件
        //月份选择事件
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDgvColmnDataPropertyName();
            RefreshDataGrid();
            RefreshControlState();
        }

        //年份选择事件
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaintenanceItem();
            BindDgvColmnDataPropertyName();
            RefreshDataGrid();
            RefreshControlState();
        }

        //单元格值改变事件
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewX dgv = (DataGridViewX)sender;

                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkOrange;
                dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                EditMarkDT.Rows.Add(dgv, e.ColumnIndex);
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value.ToString() == "保养人签名")
                    {
                        row.Cells[e.ColumnIndex].Value = BaseInfo.LoginUserName;
                        break;
                    }
                }
            }
        }

        //单元格输入值验证事件
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewX dgv = (DataGridViewX)sender;
            // 如果当前单元格处于编辑模式
            if (dgv.IsCurrentCellInEditMode)
            {
                string columnName = dgv.Columns[e.ColumnIndex].HeaderText;
                object cellValue = e.FormattedValue;

                // 针对不同的列名进行验证
                if (columnName != "保养项目")
                {
                    //值是否有效
                    string val = e.FormattedValue.ToString();
                    if (!string.IsNullOrWhiteSpace(val) && val != "V" && val != "X")
                    {
                        MessageBox.Show("单元格内容只能是‘V’或‘X’或空");
                        e.Cancel = true;
                    }
                }
            }
        }
        #endregion

        #region 刷新控件状态
        // 刷新机台信息控件状态
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
                    gbMaintenance.Enabled = false;

                    SetDgvEditScope();
                    break;
                case OperateStateEnum.Edit:
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;
                    gbMaintenance.Enabled = false;

                    SetDgvEditScope();
                    break;
                case OperateStateEnum.Defualt:
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiSave.Enabled = false;
                    tsmiDelete.Enabled = true;
                    tsmiBack.Enabled = false;
                    gbMaintenance.Enabled = true;

                    ClearDgvEditScope();
                    break;
            }
            labMessage.Enabled = true;
            ChangeDgvLayout();
        }

        //调整表显示与表高度
        private void ChangeDgvLayout()
        {
            if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
            {//月报表
                tabPanelMonth.Visible = true;
                tabPanelYear.Visible = false;
                ChangeDgvHeight(dgvDay);
                ChangeDgvHeight(dgvWeek);
                ChangeDgvHeight(dgvMonth);
            }
            else
            {//年报表
                tabPanelMonth.Visible = false;
                tabPanelYear.Visible = true;
                ChangeDgvHeight(dgvYearMonth);
                ChangeDgvHeight(dgvQuarter);
                ChangeDgvHeight(dgvYear);
            }
        }

        //修改表的高度
        private void ChangeDgvHeight(DataGridViewX dgv)
        {
            int height = 5;
            if (dgv.ColumnHeadersVisible == true)
            {
                height += dgv.ColumnHeadersHeight;
            }
            height += (dgv.Rows.Count * dgv.RowTemplate.Height);
            dgv.Height = height;
        }

        //清除表可编辑状态
        private void ClearDgvEditScope()
        {
            if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
            {//月报表
                ClearDgvEditColumn(dgvDay);
                ClearDgvEditColumn(dgvWeek);
                ClearDgvEditColumn(dgvMonth);
            }
            else
            {//年报表
                ClearDgvEditColumn(dgvYearMonth);
                ClearDgvEditColumn(dgvQuarter);
                ClearDgvEditColumn(dgvYear);
            }
        }

        //清除编辑列状态
        private void ClearDgvEditColumn(DataGridViewX dgv)
        {
            dgv.ReadOnly = true;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.ForeColor = Color.Black;
                col.DefaultCellStyle.BackColor = Color.White;
            }
        }

        //设置表可编辑范围
        private void SetDgvEditScope()
        {
            if (!string.IsNullOrWhiteSpace(cmbMonth.Text))
            {//月报表
                SetDgvEditColumn(dgvDay);
                SetDgvEditColumn(dgvWeek);
                SetDgvEditColumn(dgvMonth);
            }
            else
            {//年报表
                SetDgvEditColumn(dgvYearMonth);
                SetDgvEditColumn(dgvQuarter);
                SetDgvEditColumn(dgvYear);
            }
        }

        //设置表可编辑列
        private void SetDgvEditColumn(DataGridViewX dgv)
        {
            labMessage.Text = "";
            bool limit = BaseInfo.LoginUserRight == "A" ? false : true;
            DateTime now = DateTime.Now;

            //是否限制编辑日期，如果限制，判断当前年、月是否合规则
            if (limit)
            {
                //判断年份
                if (cmbYear.Text != now.Year.ToString())
                {
                    labMessage.Text = "非当前年份，无法编辑";
                    labMessage.ForeColor = Color.Red;
                    return;
                }

                //判断月份
                if (!string.IsNullOrWhiteSpace(cmbMonth.Text) && cmbMonth.Text != now.ToString("MM"))
                {
                    labMessage.Text = "非当前月份，无法编辑";
                    labMessage.ForeColor = Color.Red;
                    return;
                }
            }

            //不限制的日期列索引
            int notLimitIndex = -1;
            if (limit)
            {
                switch (dgv.Name)
                {
                    case "dgvDay":
                        notLimitIndex = now.Day;
                        break;
                    case "dgvWeek":
                        notLimitIndex = (int)Math.Ceiling(now.Day / 7.0);
                        break;
                    case "dgvMonth":
                        if (now.Day >= 25)
                            notLimitIndex = 1;
                        break;
                    case "dgvYearMonth":
                        notLimitIndex = now.Month;
                        break;
                    case "dgvQuarter":
                        notLimitIndex = (int)Math.Ceiling(now.Month / 3.0);
                        break;
                    case "dgvYear":
                        notLimitIndex = 1;
                        break;
                }
            }

            dgv.ReadOnly = false;
            //保养项目&保养日期：列只读
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (limit)
                {
                    if (col.Index != notLimitIndex)
                    {
                        col.ReadOnly = true;
                    }
                    else
                    {
                        col.DefaultCellStyle.BackColor = Color.LightCyan;
                    }
                }
                else
                {
                    if (col.HeaderText == "保养项目")
                    {
                        col.ReadOnly = true;
                    }
                    else
                    {
                        //当为 日、周表时，判断日期是否超出
                        if (dgv.Name == "dgvDay")
                        {
                            int days = DateTime.DaysInMonth(Int32.Parse(cmbYear.Text), Int32.Parse(cmbMonth.Text));
                            if (Int32.Parse(col.HeaderText) > days)
                                col.ReadOnly = true;
                            else
                                col.DefaultCellStyle.BackColor = Color.LightCyan;
                        }
                        else if (dgv.Name == "dgvWeek")
                        {
                            int days = DateTime.DaysInMonth(Int32.Parse(cmbYear.Text), Int32.Parse(cmbMonth.Text));
                            if (Int32.Parse(col.HeaderText) > (int)Math.Ceiling(days / 7.0))
                                col.ReadOnly = true;
                            else
                                col.DefaultCellStyle.BackColor = Color.LightCyan;
                        }
                        else
                        {
                            col.DefaultCellStyle.BackColor = Color.LightCyan;
                        }
                    }
                }
            }

            //保养人签名：行只读
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value.ToString() == "保养人签名")
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.White;
                    break;
                }
            }
        }
        #endregion

        #region 其他

        //检查是否有重复的报告
        private bool CheckExitisMaintenanceReport(string assetNo, string year, string month)
        {
            string sql = string.Format("SELECT 1 FROM A_MaintenanceReport_T WHERE AssetNo='{0}'  AND Year='{1}' AND ISNULL(Month,'')='{2}' ", assetNo, year, month);
            DataTable dt = DBUtil.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        //获取保养表提示
        private void SetReportTip()
        {
            // 字典目录 
            string sql = string.Format("SELECT Id FROM S_SysDic WHERE Catalog='{0}'", DicCatalogEnum.MAINTENANCE_REPORT_TIP);
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                int dicCatalogId = Convert.ToInt32(dt.Rows[0][0]);
                //字典详情
                sql = string.Format("SELECT * FROM S_SysDicDetial WHERE CatalogId='{0}'", dicCatalogId);
                DataTable DicDetailDT = DBUtil.GetDataTable(sql);
                if (DicDetailDT != null)
                {
                    foreach (DataRow row in DicDetailDT.Rows)
                    {
                        if (row["DataKey"].ToString() == "Month") rtxbMonthTip.Text = row["DataValue"].ToString();
                        if (row["DataKey"].ToString() == "Year") rtxbYearTip.Text = row["DataValue"].ToString();
                    }
                }
            }
        }
        #endregion

        #region 设备信息拼接
        //根据模板，拼结报表相关参数
        private string ConvertReportInfo(string paramAssetName, string paramModel, string paramAssetNo, string paramYear, string paramMonth)
        {
            //默认字符串格式
            string assetName = "                                ", model = "                 ", assetNo = "                               ", year = "    ", month = "    ";
            //替换字符串
            assetName = ReplaceChar(assetName, paramAssetName, "L", "楷体", 12);
            model = ReplaceChar(model, paramModel, "L", "楷体", 12);
            assetNo = ReplaceChar(assetNo, paramAssetNo, "L", "楷体", 12);
            year = ReplaceChar(year, paramYear, "L", "楷体", 12);
            month = ReplaceChar(month, paramMonth, "R", "楷体", 12);
            return string.Format("设备名称:{0}机型：{1}资产单位：  生技课             编号:{2}{3}年{4}月", assetName, model, assetNo, year, month);
        }

        /// <summary>
        /// 根据字体大小，将字符替换相同空间大小的空格
        /// </summary>
        /// <param name="space">空格字符串</param>
        /// <param name="text">替换字符</param>
        /// <param name="direction">方向（L:从左开始，R：从右开始）</param>
        /// <param name="fontName">字体名称</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns></returns>
        private string ReplaceChar(string space, string text, string direction, string fontName, float fontSize)
        {
            Graphics g = this.CreateGraphics();
            // 指定字符串格式
            StringFormat sf = StringFormat.GenericTypographic;
            //StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            // 1. 计算字符串的高度和宽度（不限定高度和宽度）
            SizeF sizeF = g.MeasureString(text, CreateFont(fontName, Convert.ToSingle(fontSize)), new PointF(0, 0), sf);
            SizeF sizeF_space = g.MeasureString(" ", CreateFont(fontName, Convert.ToSingle(fontSize)), new PointF(0, 0), sf);
            int zy = (int)(sizeF.Width / sizeF_space.Width);
            if (zy == 0)
            {
                return space;
            }
            else if (zy > space.Length)
            {
                return text;
            }
            else
            {
                if (direction == "L")
                {
                    return text + space.Substring(zy);
                }
                else
                {
                    return space.Substring(zy) + text;
                }
            }
        }

        Font CreateFont(string fontName, float fontSize)
        {
            return new Font(fontName, fontSize, GraphicsUnit.Pixel);
        }
        #endregion



    }
}
