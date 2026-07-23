using Common;
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
    public partial class FrmLineMaintenance : Form
    {
        private int Year = DateTime.Now.Year;//当年
        private List<int> DayStamp = new List<int>();//日期标记值
        private int CatalogId;//节日设置字典目录ID
        private Dictionary<int, bool> DayStampWorkDic = new Dictionary<int, bool>();//日期标记值 是否节日
        private DataTable ReceiveDT; //领用记录
        public FrmLineMaintenance()
        {
            InitializeComponent();
        }

        private void FrmLineMaintenance_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            dgvLineMaintenance.AutoGenerateColumns = false;
            //权限
            if (string.IsNullOrWhiteSpace(BaseInfo.LoginUserNo))
                cmbLine.Enabled = false;

            //获取节日设置数据字典目录id
            string sql = string.Format("SELECT Id FROM S_SysDic WHERE Catalog='{0}'", DicCatalogEnum.HOLIDAY_DATE_DAY);
            DataTable dt2 = DBUtil.GetDataTable(sql);
            if (dt2 != null && dt2.Rows.Count == 1)
            {
                CatalogId = Convert.ToInt32(dt2.Rows[0][0]);
            }

            //产线
            sql = "SELECT Line FROM S_LineInfo_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            DataRow newR = dt.NewRow();
            dt.Rows.InsertAt(newR,0);
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = dt;
            cmbLine.Text = BaseInfo.Line;

            //月份
            List<string> months = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i.ToString());
            }
            cmbMonth.Items.AddRange(months.ToArray());
            cmbMonth.Text = DateTime.Now.Month.ToString();
        }

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshGridData();
        }
        #endregion

        #region 提示
        private void tsmiTip_Click(object sender, EventArgs e)
        {
            FrmMaintenanceTip frm = new FrmMaintenanceTip();
            frm.ShowDialog();
        }
        #endregion

        #region 事件
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            labMessage.Text = "";
            InitDgvColumn();
            RefreshGridData();
            labMessage.Text = "刷新成功";
            labMessage.ForeColor = Color.Green;
        }

        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridData();
        }
        #endregion

        #region 数据表相关
        //初始化数据表的列控件
        private void InitDgvColumn()
        {
            //参数判断
            string month = cmbMonth.Text;
            if (string.IsNullOrWhiteSpace(month)) return;

            //获取指定月份的所有节假日设置
            string sql = string.Format("SELECT DataKey,DataValue FROM S_SysDicDetial WHERE CatalogId='{0}' AND DatePart(year,DataKey)='{1}' AND DatePart(month,DataKey)='{2}' ;", CatalogId, Year, month);
            DataTable holidayDT = DBUtil.GetDataTable(sql);

            //清理原有数据
            int startColIndex = 2;
            DayStamp.Clear();
            DayStampWorkDic.Clear();
            for (int i = dgvLineMaintenance.Columns.Count - 1; i >= startColIndex; i--)
            {
                dgvLineMaintenance.Columns.RemoveAt(i);
            }

            int firstDayStamp = Convert.ToDateTime(Year + "-" + month + "-1").DayOfYear;
            int days = DateTime.DaysInMonth(Year, Int32.Parse(month));

            DataGridViewTextBoxColumn[] cols = new DataGridViewTextBoxColumn[days];
            //遍历数据表的日期列
            for (int i = 0; i < days; i++)
            {
                //控件
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
                col.HeaderText = (i + 1).ToString();
                col.Name = "dgcDay" + (i + 1);
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DataPropertyName = (firstDayStamp + i).ToString();//设置对应列的数据属性名为相应的日期标记值

                //日期标记值
                DayStamp.Add(firstDayStamp + i);
                //---------判断当天是否工作日--------------
                bool isHoliday = false;
                //检查是否有节日设置
                DateTime day = new DateTime(Year, int.Parse(month), (i + 1));
                DataRow[] rows = holidayDT.Select(" DataKey='" + day.ToString("yyyy-MM-dd") + "'");
                if (rows != null && rows.Length > 0)
                {//优化节日判断
                    isHoliday = Convert.ToString(rows[0]["DataValue"]) == "Y" ? true : false;
                }
                else if (day.DayOfWeek == DayOfWeek.Sunday)
                {//周日判断
                    isHoliday = true;
                }
                DayStampWorkDic.Add(firstDayStamp + i, isHoliday);

                //设置节日列背影为灰色
                if (isHoliday) col.DefaultCellStyle.BackColor = Color.LightGreen;
                cols[i] = col;
            }
            //添加到数据表控件上
            dgvLineMaintenance.Columns.AddRange(cols);
        }

        //刷新数据
        private void RefreshGridData()
        {
            string month = cmbMonth.Text;
            if (string.IsNullOrWhiteSpace(month)) return;
            if (DayStamp.Count == 0) return;
            dgvLineMaintenance.DataSource = null;

            string stratTime = Year + "-" + month + "-1";
            string endTime = Year + "-" + month + "-" + DateTime.DaysInMonth(Year, Int32.Parse(month));
            int firstStamp = DayStamp[0];

            //获取领用记录
            string sql = string.Format("SELECT a.AssetNo,r.Line,a.AssetName,CAST(r.StartTime AS DATE) StartTime,CAST(ISNULL(r.EndTime,GETDATE()) AS DATE) EndTime FROM A_AssetReceive_T r LEFT JOIN A_AssetInfo_T a ON a.AssetNo=r.AssetNo   WHERE  r.StartTime <='{1}' AND ISNULL(r.EndTime,GETDATE())>='{0}' ", stratTime, endTime);
            if (!string.IsNullOrWhiteSpace(cmbLine.Text))
            {
                sql += string.Format(" AND r.Line=N'{0}' AND r.EndTime IS NULL ", cmbLine.Text);
            }
            ReceiveDT = DBUtil.GetDataTable(sql);

            //过滤资产编号，去重
            List<string> assets = ReceiveDT.AsEnumerable().Select(t => t.Field<string>("AssetNo")).ToList().Distinct().ToList();
            //日期标记值
            List<string> stampCols = new List<string>();
            foreach (int stamp in DayStamp)
            {
                stampCols.Add("[" + stamp + "]");
            }
            string timeMark = "D";
            sql = string.Format(@"SELECT a.AssetNo,a.AssetName,b.* FROM A_AssetInfo_T a 
                                LEFT JOIN(
                                    SELECT * FROM (SELECT AssetNo,TimeMarkStamp,
                                       ((SELECT COUNT(1) FROM A_MaintenanceReportItem_T   WHERE AssetNo=A_MaintenanceReportD_T.AssetNo  AND Year='{1}' AND TimeMark='{2}') -
	                                    (SELECT COUNT(1) FROM A_MaintenanceReportDV_T WHERE MRDId=A_MaintenanceReportD_T.Id AND ISNULL(ItemValue,'')<>'')) AS [State] 
                                    FROM A_MaintenanceReportD_T 
                                    WHERE AssetNo IN ('{0}') AND  [Year]='{1}' AND TimeMark='{2}'AND TimeMarkStamp>={3} AND TimeMarkStamp<={4}) AS MRD
                                    PIVOT ( MAX([State]) FOR [TimeMarkStamp] IN ({5})) AS PT) b 
                                ON a.AssetNo = b.AssetNo WHERE a.AssetNo IN('{0}') ;
                                ", string.Join("','", assets), Year, timeMark, DayStamp[0], DayStamp.LastOrDefault(), string.Join(",", stampCols));
            //查询
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvLineMaintenance.DataSource = dt;
        }

        //单元格格式化
        private void dgvLineMaintenance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //判断是否日期列，及有效行数据
            if (e.ColumnIndex >= 2 && e.RowIndex >= 0)
            {
                int dayNum = int.Parse(dgvLineMaintenance.Columns[e.ColumnIndex].HeaderText);
                DateTime day = new DateTime(Year, int.Parse(cmbMonth.Text), dayNum);

                //超出当前日期
                if (day > DateTime.Now)
                {
                    e.Value = "";
                    return;
                }

                //判断资产在当前单元格日期是否有被领用
                if (ReceiveDT != null)
                {
                    string assetNo = dgvLineMaintenance.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DataRow[] rows = ReceiveDT.Select(string.Format("AssetNo='{0}' AND StartTime<='{1}' AND  EndTime>='{1}' ", assetNo, day.ToString("yyyy-MM-dd")));
                    if (rows != null && rows.Length > 0)
                    {//有被领用
                        if (e.CellStyle.BackColor != Color.LightGreen)
                        { //Color.LightGreen：表示节假日
                            e.CellStyle.BackColor = Color.MistyRose;
                        }
                        //当产线为空时，增加tip提示
                        if (string.IsNullOrWhiteSpace(cmbLine.Text))
                        {
                            List<string> lines = new List<string>();
                            foreach (DataRow r in rows)
                            {
                                lines.Add(r["Line"].ToString());
                            }
                            dgvLineMaintenance.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = string.Join(",", lines);
                        }
                    }
                }

                //如果有保养记录
                string value = Convert.ToString(e.Value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (value == "0")
                    {
                        e.Value = "V";
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Value = "E";
                        e.CellStyle.ForeColor = Color.Blue;
                    }
                    return;
                }

                //没有保养记录时，判断是否为节日休假
                int stamp = int.Parse(dgvLineMaintenance.Columns[e.ColumnIndex].DataPropertyName);
                if (DayStampWorkDic.Keys.Contains(stamp) && DayStampWorkDic[stamp])
                {
                    e.Value = "/";
                    e.CellStyle.ForeColor = Color.Green;
                    return;
                }

                //没有保养记录时，且不为节日休假，当被领用时
                //if (isReceive)
                //{
                //    e.Value = "X";
                //    e.CellStyle.ForeColor = Color.Red;
                //}
            }
        }

        //单元格单击
        private void dgvLineMaintenance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 2 && e.RowIndex >= 0)
            {
                string assetNo = dgvLineMaintenance.Rows[e.RowIndex].Cells[0].Value.ToString();
                string assetName = dgvLineMaintenance.Rows[e.RowIndex].Cells[1].Value.ToString();
                string dayMark = dgvLineMaintenance.Columns[e.ColumnIndex].HeaderText;
                DateTime day = new DateTime(Year, int.Parse(cmbMonth.Text), int.Parse(dayMark));
                FrmMaintenaceItemValue frm = new FrmMaintenaceItemValue(assetNo, assetName, day);
                frm.ShowDialog();
            }
        }
        #endregion
    }
}
