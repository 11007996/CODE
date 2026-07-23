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

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Series series = new Series();
            ChartArea chartarea = new ChartArea();
            chartarea.AxisX.Interval = 5000;
            DataTable dt = default(DataTable);
            dt = CreateDataTable();

            chart1.DataSource = dt;

            chart1.Series[0].YValueMembers = "Volume1";
            chart1.Series[1].YValueMembers = "Volume2";
            chart1.Series[2].YValueMembers = "Volume3";

            chart1.Series[1].XValueMember = "Date";
            chart1.DataBind();
        }
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Date");
            dt.Columns.Add("Volume1");
            dt.Columns.Add("Volume2");
            dt.Columns.Add("Volume3");

            DataRow dr;

            dr = dt.NewRow();
            dr["Date"] = "Jan";
            dr["Volume1"] = 3731;
            dr["Volume2"] = 4101;
            dr["Volume3"] = 101;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Date"] = "Feb";
            dr["Volume1"] = 6024;
            dr["Volume2"] = 4324;
            dr["Volume3"] = 1101;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Date"] = "Mar";
            dr["Volume1"] = 7935;
            dr["Volume2"] = 6700;
            dr["Volume3"] = 2101;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Date"] = "Apr";
            dr["Volume1"] = 4466;
            dr["Volume2"] = 5644;
            dr["Volume3"] = 3101;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Date"] = "May";
            dr["Volume1"] = 5117;
            dr["Volume2"] = 5671;
            dr["Volume3"] = 4101;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Date"] = "Jun";
            dr["Volume1"] = 1546;
            dr["Volume2"] = 7646;
            dr["Volume3"] = 5101;
            dt.Rows.Add(dr);

            return dt;
        }
  

        private void Form2_Load_1(object sender, EventArgs e)
        {
            //DataTable dt = default(DataTable);
            ////dt = CreateDataTable();

            //chart1.DataSource = dt;
            
            //chart1.Series[0].YValueMembers = "Volume1";
            //chart1.Series[1].YValueMembers = "Volume2";
            //chart1.Series[2].YValueMembers = "Volume3";

            //chart1.Series[1].XValueMember = "Date";
            //chart1.DataBind();
            //String str = chart1.Series[0].YValueMembers.ToString();
            //label1.Text = str;

        }
        
    }
}
