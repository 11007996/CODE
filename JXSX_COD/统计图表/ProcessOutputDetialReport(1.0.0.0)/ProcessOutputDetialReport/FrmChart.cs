using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProcessOutputDetialReport
{
    public partial class FrmChart : Form
    {
        public FrmChart()
        {
            InitializeComponent();
        }




        //查询的时间段
        private string _dateStart;
        private string _dateEnd;
        private string _lineName;
        public FrmChart(string dateStart, string dateEnd, string lineName)
        {
            InitializeComponent();
            _dateStart = dateStart;
            _dateEnd = dateEnd;
            _lineName = lineName;
            Lb_SearchCondition.Text = "开始时间:" + dateStart + ":00:00 结束时间:" + dateEnd + ":59:59";

        }

        private void ShowDefectData(string LineName)
        {
            dgvDetial.DataSource = null;

            LineName = LineName.Split('-')[0].Trim();
            string sql = @"
                SELECT G.MODEL_NAME 机种,
                   b.pdline_name 拉线,
                   c.process_name 制程,
                   d.terminal_name 工站,
                   a.work_order 工单,
                   a.serial_number SN,
                   e.defect_code || '(' || e.defect_desc || ')' 不良现象 ,
                   a.rec_time 不良时间 
              FROM sajet.g_sn_defect  a,
                   sajet.sys_pdline   b,
                   sajet.sys_process  c,
                   sajet.sys_terminal d,
                   SAJET.SYS_DEFECT   e,
                   SAJET.SYs_part     f,
                   sajet.sys_model    g
             WHERE A.PDLINE_ID = B.PDLINE_ID
               AND B.ENABLED = 'Y'
               AND A.PROCESS_ID = C.PROCESS_ID
               AND C.ENABLED = 'Y'
               AND A.TERMINAL_ID = D.TERMINAL_ID
               AND D.ENABLED = 'Y'
               AND A.DEFECT_ID = E.DEFECT_ID
               AND a.part_id = f.part_id
               AND f.model_id = g.model_id
               and b.pdline_name='" + LineName + @"'
               and a.rec_time >= TO_DATE('" + _dateStart.Replace("-", "").Trim() + @"' || '0000', 'yyyymmddhh24miss')
               AND a.rec_time <= TO_DATE('" + _dateEnd.Replace("-", "").Trim() + @"' || '5959', 'yyyymmddhh24miss')
             ORDER BY B.PDLINE_NAME, C.PROCESS_name, d.terminal_name
            ";

            dgvDetial.DataSource = ClientUtils.ExecuteSQL(sql).Tables[0];





        }

        private void GetDefectData(ref List<string> xData, ref List<int> yData)
        {
            string sql = @"                
                    SELECT (b.pdline_name||' - '||e.defect_desc) defect_desc,
                           count(G.MODEL_NAME || '-' || b.pdline_name || '-' || c.process_name || '-' ||
                                 e.defect_code || '(' || e.defect_desc || ')') count_qty
                      FROM sajet.g_sn_defect  a,
                           sajet.sys_pdline   b,
                           sajet.sys_process  c,
                           sajet.sys_terminal d,
                           SAJET.SYS_DEFECT   e,
                           SAJET.SYs_part     f,
                           sajet.sys_model    g
                     WHERE A.PDLINE_ID = B.PDLINE_ID
                       AND B.ENABLED = 'Y'
                       AND A.PROCESS_ID = C.PROCESS_ID
                       AND C.ENABLED = 'Y'
                       AND A.TERMINAL_ID = D.TERMINAL_ID
                       AND D.ENABLED = 'Y'
                       AND A.DEFECT_ID = E.DEFECT_ID
                       AND a.part_id = f.part_id
                       AND f.model_id = g.model_id
                       AND b.PDLINE_NAME='" + _lineName + @"'
                       and a.rec_time >= TO_DATE('" + _dateStart.Replace("-", "").Trim() + @"' || '0000', 'yyyymmddhh24miss')
                       AND a.rec_time <= TO_DATE('" + _dateEnd.Replace("-", "").Trim() + @"' || '5959', 'yyyymmddhh24miss')
                     group by b.pdline_name,e.defect_desc
                     order by count(G.MODEL_NAME || '-' || b.pdline_name || '-' || c.process_name || '-' ||
                                 e.defect_code || '(' || e.defect_desc || ')') desc
            ";


            xData = new List<string>();
            yData = new List<int>();
            foreach (DataRow item in ClientUtils.ExecuteSQL(sql).Tables[0].Rows)
            {
                xData.Add(item["defect_desc"].ToString());
                yData.Add(Convert.ToInt32(item["count_qty"]));
            }


        }



        private void FrmChart_Load(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Maximized;
            List<string> xData = new List<string>() { "A", "B", "C", "D" };
            List<int> yData = new List<int>() { 10, 20, 30, 40 };

            GetDefectData(ref xData, ref yData);



            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[0]["PieLineColor"] = "Gray";//绘制黑色的连线。
            chart1.Series[0].IsVisibleInLegend = true;

            chart1.Series[0].Points.DataBindXY(xData, yData);
        }

        private int pieHitPointIndex(Chart pie, MouseEventArgs e)
        {
            HitTestResult hitPiece = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint);
            HitTestResult hitLegend = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem);
            int pointIndex = -1;
            if (hitPiece.Series != null) pointIndex = hitPiece.PointIndex;
            if (hitLegend.Series != null) pointIndex = hitLegend.PointIndex;
            return pointIndex;
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = pieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
                DataPoint dp = pie.Series[0].Points[pointIndex];
                // do what you want to do with a click
                panel2.Dock = DockStyle.Left;
                panel2.Width = Convert.ToInt32(ScreenArea.Width * 0.5);
                panel3.Visible = true;
                ShowDefectData(dp.AxisLabel);

            }
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Chart pie = (Chart)sender;
            int pointIndex = pieHitPointIndex(pie, e);
            if (pointIndex >= 0)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgvDetial_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvDetial.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvDetial.RowHeadersDefaultCellStyle.Font, rectangle, dgvDetial.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            if ((e.RowIndex + 1) % 2 == 0)
                dgvDetial.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
            else
            {
                dgvDetial.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
            }
        }

        private void 导出数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "All Files(*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string sFileName = saveFileDialog1.FileName;

            ExportExcel.CreateExcel Export = new ExportExcel.CreateExcel(sFileName);
            Export.ExportToExcel(dgvDetial);
        }

    }
}
