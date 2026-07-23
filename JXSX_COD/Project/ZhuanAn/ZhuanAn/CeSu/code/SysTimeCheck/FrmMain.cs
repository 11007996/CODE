using System;
using System.Windows.Forms;

namespace SysTimeCheck
{
    public partial class FrmMain : Form
    {
        private static int ErrorCount = 0;
        private static DateTime PreTime = DateTime.Now;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += $"  启动时间[{DateTime.Now}]";
        }

        private void Time1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            // 检查前置时间是否大于或等于当前时间
            if (PreTime >= now)
            {
                ErrorCount++;
                AddErrorMsg(now, PreTime, "前一秒时间大于或等于当前时间");
            }
            // 检查当前时间是否比前置时间加两秒还要大
            else if (now > PreTime.AddSeconds(2))
            {
                ErrorCount++;
                AddErrorMsg(now, PreTime, "比上一秒的时间隔天大于2秒以上");
            }
            // 更新前置时间为当前时间
            PreTime = now;
            // 更新时间标签显示当前时间
            labTime.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            // 更新错误计数标签显示当前错误计数
            labCount.Text = ErrorCount.ToString();
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="now"></param>
        /// <param name="preTime"></param>
        /// <param name="errorMsg"></param>
        private void AddErrorMsg(DateTime now, DateTime preTime, string errorMsg)
        {
            // 如果错误记录表格中的行数超过1000行，则移除第1001行
            if (dgvErrorRecord.Rows.Count > 1000)
            {
                dgvErrorRecord.Rows.RemoveAt(dgvErrorRecord.Rows.Count - 1);
            }
            dgvErrorRecord.Rows.Insert(0, now, preTime, errorMsg);
        }
    }
}