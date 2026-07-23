using ComTools.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ComTools
{
    public partial class FrmDiskCapacity : Form
    {
        private static readonly string START = "Start";
        private static readonly string STOP = "Stop";

        private readonly Stopwatch HiStopWatch = new Stopwatch();//高精度时间测量
        private List<string> Disks = new List<string>();
        private SynchronizationContext SyncContext = null;//异步上下文

        public FrmDiskCapacity()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        #region 初始化

        private void FrmDiskCapacity_Load(object sender, EventArgs e)
        {
            InitControlBind();
            StartWatch();
            SetState(0, null);
        }

        //初始化控件
        private void InitControlBind()
        {
            List<string> disks = DiskHelper.GetDisks();
            if (!disks.Contains(Setting.DiskCapacity.TargetDisk))
                disks.Add(Setting.DiskCapacity.TargetDisk);
            cbTargetDisk.DataSource = disks;
            cbTargetDisk.Text = Setting.DiskCapacity.TargetDisk;
            //自动执行
            List<KeyValuePair<string, bool>> autoFlagData = new List<KeyValuePair<string, bool>>();
            autoFlagData.Add(new KeyValuePair<string, bool>("自动", true));
            autoFlagData.Add(new KeyValuePair<string, bool>("手动", false));
            cbAutoRun.DisplayMember = "key";
            cbAutoRun.ValueMember = "value";
            cbAutoRun.DataSource = autoFlagData;
            cbAutoRun.SelectedValue = Setting.DiskCapacity.AutoFlag;
        }

        #endregion 初始化

        #region 异步线程监听

        private void StartWatch()
        {
            Thread thread = new Thread(new ParameterizedThreadStart(WatchThreadStart));
            thread.IsBackground = true;
            thread.Start();
        }

        private void SyncWatchResult(object state)
        {
            SetState(3, "测试超时");
        }

        private void WatchThreadStart(object obj)
        {
            while (true)
            {
                if (HiStopWatch.IsRunning && HiStopWatch.ElapsedMilliseconds > 4000)
                {//如果插入硬盘超过
                    SyncContext.Post(SyncWatchResult, null);
                }
                Thread.Sleep(100);
            }
        }

        #endregion 异步线程监听

        #region 测试开关

        /// <summary>
        /// 启动或停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlag_Click(object sender, EventArgs e)
        {
            if (btnFlag.Text == START)
            {
                SetState(1, null);
                StartTest();
            }
            else
                SetState(0, null);
        }

        #endregion 测试开关

        #region 设备事件

        // 定义必要的常量
        public const int WM_DEVICECHANGE = 0x0219;//设备状态改变

        private const int DBT_DEVICEARRIVAL = 0x8000;//设备准备就绪
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;//设备从系统移除

        /// <summary>
        /// 重写磁盘消息处理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                string deviceName = null;
                switch ((int)m.WParam)
                {
                    case DBT_DEVICEARRIVAL:
                        deviceName = DiskHelper.GetDeviceName(m);
                        Console.WriteLine($"{deviceName}设备已插入");
                        if (deviceName == null || !deviceName.StartsWith(cbTargetDisk.Text))
                            return;
                        AddLog($"{deviceName}设备已插入");
                        bool autoRun = Convert.ToBoolean(cbAutoRun.SelectedValue);
                        if (autoRun)
                        {
                            SetState(1, null);
                            StartTest();
                        }
                        break;

                    case DBT_DEVICEREMOVECOMPLETE:
                        //deviceName = DiskHelper.GetDeviceName(m);
                        //List<string> disks = DiskHelper.GetDisks();
                        //if (disks.Contains(cbTargetDisk.Text))
                        //    return;
                        //if (deviceName != null && deviceName.StartsWith(cbTargetDisk.Text))
                        //{
                        //    SetState(0, null);
                        //    AddLog("设备已移除");
                        //}
                        SetState(0, null);
                        AddLog("设备已移除");
                        break;

                    default:
                        break;
                }
            }

            base.WndProc(ref m);
        }

        #endregion 设备事件

        #region 开始测试

        private bool GetDiskCapacity(string targetDisk)
        {
            try
            {
                // 指定驱动器的字母，例如 "C:"
                var drive = new DriveInfo(targetDisk);
                // 获取总容量（以字节为单位）
                long totalCapacity = drive.TotalSize;
                // 获取可用空间（以字节为单位）
                long freeSpace = drive.AvailableFreeSpace;

                // 可能需要转换为GB等更易读的形式
                double totalCapacityGb = totalCapacity / (1024.0 * 1024.0 * 1024.0);
                double freeSpaceGb = freeSpace / (1024.0 * 1024.0 * 1024.0);

                string msg = $"磁盘{targetDisk}{Environment.NewLine}总容量: {totalCapacityGb:N2} GB;{Environment.NewLine}可用空间: {freeSpaceGb:N2} GB";
                labAllCapacity.Text = $"{totalCapacityGb:N2}".ToString();
                labRemainingCapacity.Text = $"{freeSpaceGb:N2}".ToString();
                AddLog(msg);
                return true;
            }
            catch (System.IO.DriveNotFoundException)
            {
                AddLog($"未找到磁盘驱动【{targetDisk}】");
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void StartTest()
        {
            try
            {
                if (GetDiskCapacity(cbTargetDisk.Text))
                {
                    SetState(2, null);
                }
                else
                {
                    SetState(3, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"异常: {ex.Message}", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetState(3, null);
            }
        }

        #endregion 开始测试

        #region 设置状态

        /// <summary>
        /// 设置状态
        /// </summary>
        private void SetState(int state, string msg)
        {
            switch (state)
            {
                case 0://默认准备
                    btnFlag.Text = START;
                    btnFlag.BackColor = Color.Green;
                    labAllCapacity.Text = "0";
                    labRemainingCapacity.Text = "0";
                    labReadResult.Text = "Ready";
                    labReadResult.BackColor = Color.Blue;
                    Watch(false);
                    break;

                case 1://开始测试
                    btnFlag.Text = STOP;
                    btnFlag.BackColor = Color.Red;
                    labAllCapacity.Text = "0";
                    labRemainingCapacity.Text = "0";
                    labReadResult.Text = "Testing";
                    labReadResult.BackColor = Color.Yellow;
                    Watch(true);
                    break;

                case 2://成功
                    btnFlag.Text = START;
                    btnFlag.BackColor = Color.Green;
                    //labAllCapacity.Text = "0";
                    //labRemainingCapacity.Text = "0";
                    labReadResult.Text = "PASS";
                    labReadResult.BackColor = Color.Green;
                    Watch(false);
                    break;

                case 3://失败
                    btnFlag.Text = START;
                    btnFlag.BackColor = Color.Green;
                    labAllCapacity.Text = "0";
                    labRemainingCapacity.Text = "0";
                    labReadResult.Text = "FAIL";
                    labReadResult.BackColor = Color.Red;
                    Watch(false);
                    break;
            }
            if (rtbLog.Text.Length > 2000)
                rtbLog.Text = "";
            if (!string.IsNullOrEmpty(msg))
                AddLog(msg);
        }

        private void Watch(bool flag)
        {
            if (flag == true)
            {
                HiStopWatch.Restart();
            }
            else
            {
                HiStopWatch.Stop();
            }
        }

        #endregion 设置状态

        #region 添加日志

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="msg"></param>
        private void AddLog(string msg)
        {
            rtbLog.SelectionColor = Color.Green;
            rtbLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + Environment.NewLine);
            rtbLog.SelectionColor = rtbLog.ForeColor;
            // 添加文本
            rtbLog.AppendText(msg + Environment.NewLine);
            rtbLog.Focus();
            btnFlag.Focus();
        }

        #endregion 添加日志

        #region 窗体关闭

        private void FormDiskCapacity_FormClosing(object sender, FormClosingEventArgs e)
        {
            Setting.DiskCapacity.TargetDisk = cbTargetDisk.Text;
            Setting.DiskCapacity.AutoFlag = Convert.ToBoolean(cbAutoRun.SelectedValue);
        }

        #endregion 窗体关闭
    }
}