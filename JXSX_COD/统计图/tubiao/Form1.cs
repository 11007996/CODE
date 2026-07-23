using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace tubiao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 定义图形画在哪个Series
            Series series = chart1.Series[0];
            // 定义图形样式（折线图）
            series.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series.BorderWidth = 2;
            // 线的颜色：红色
            series.Color = System.Drawing.Color.Blue;
            // 图示上的文字
            series.LegendText = "演示曲线";
            //显示坐标点值
            series.IsValueShownAsLabel = true;
            //设置坐标点值背景色
            series.LabelBackColor = System.Drawing.Color.Teal;
            //设置数据边框的颜色
            series.BorderColor = System.Drawing.Color.Red;
            
            // 准备数据
            float[] values = { 95, 30, 20, 23, 60, 87, 42, 77, 92, 51, 95 };

            // 在chart中显示数据
            int x = 0;
            foreach (float v in values)
            {
                series.Points.AddXY(x, v);
                x++;
            }

            // 设置显示范围
            ChartArea chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.Minimum = 0;   //X轴最小值
            chartArea.AxisX.Maximum = 10;   //X轴最大值
            chartArea.AxisY.Minimum = 0d;   //Y轴最小值
            chartArea.AxisY.Maximum = 100d;  //Y轴最大值
            chartArea.AxisX.Interval = 2;    //X轴的间隔
            chartArea.AxisY.Interval = 20;   //Y轴的间隔
            //chartArea.AxisY.LineColor = System.Drawing.Color.Yellow;
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Yellow;
        }
    }
}