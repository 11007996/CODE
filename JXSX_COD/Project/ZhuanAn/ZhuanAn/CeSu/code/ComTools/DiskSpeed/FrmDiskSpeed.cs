using ComTools.Util;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTools
{
    public partial class FrmDiskSpeed : Form
    {
        private static readonly string START = "Start";
        private static readonly string STOP = "Stop";
        private static readonly string TestFileName = "DiskSpeedTest.dat";//.dat
        private static bool TestingState = false;//测试状态,false：表示中断测试，true表示不中断
        private readonly Stopwatch HiStopWatch = new Stopwatch();
        private byte[] BlockBytes;

        //高精度时间测量
        private SynchronizationContext SyncContext = null;

        private long TotalReadBytes = 0;
        private long TotalWriteBytes = 0; //总写入字节数
                                          //总读取字节数
                                          //块大小缓存字节

        //异步上下文

        public FrmDiskSpeed()
        {
            InitializeComponent();
            SyncContext = SynchronizationContext.Current;
        }

        #region 初始化

        private void DiskSpeedTestForm_Load(object sender, EventArgs e)
        {
            InitControlBind();
        }

        //初始化控件
        private void InitControlBind()
        {
            //文件大小
            List<KeyValuePair<string, long>> fileSizeData = new List<KeyValuePair<string, long>>();
            fileSizeData.Add(new KeyValuePair<string, long>("64 MB", 64L * 1024 * 1024));
            fileSizeData.Add(new KeyValuePair<string, long>("512 MB", 512L * 1024 * 1024));
            fileSizeData.Add(new KeyValuePair<string, long>("1 GB", 1L * 1024 * 1024 * 1024));
            fileSizeData.Add(new KeyValuePair<string, long>("2 GB", 2L * 1024 * 1024 * 1024));
            fileSizeData.Add(new KeyValuePair<string, long>("4 GB", 4L * 1024 * 1024 * 1024));
            cbFileSize.DisplayMember = "key";
            cbFileSize.ValueMember = "value";
            cbFileSize.DataSource = fileSizeData;
            cbFileSize.SelectedValue = Setting.DiskSpeed.FileSize;

            //块大小
            List<KeyValuePair<string, int>> blockSizeData = new List<KeyValuePair<string, int>>();
            blockSizeData.Add(new KeyValuePair<string, int>("4 KB", 4 * 1024));
            blockSizeData.Add(new KeyValuePair<string, int>("64 KB", 64 * 1024));
            blockSizeData.Add(new KeyValuePair<string, int>("512 KB", 512 * 1024));
            blockSizeData.Add(new KeyValuePair<string, int>("1 MB", 1 * 1024 * 1024));
            blockSizeData.Add(new KeyValuePair<string, int>("16 MB", 16 * 1024 * 1024));
            cbBlockSize.DisplayMember = "key";
            cbBlockSize.ValueMember = "value";
            cbBlockSize.DataSource = blockSizeData;
            cbBlockSize.SelectedValue = Setting.DiskSpeed.BlockSize;

            //磁盘
            List<string> disks = DiskHelper.GetDisks();
            if (!disks.Contains(Setting.DiskSpeed.TargetDisk))
                disks.Add(Setting.DiskSpeed.TargetDisk);
            cbTargetDisk.DataSource = disks;
            cbTargetDisk.Text = Setting.DiskSpeed.TargetDisk;

            //运行次数
            List<KeyValuePair<string, int>> runCountDiskData = new List<KeyValuePair<string, int>>();
            runCountDiskData.Add(new KeyValuePair<string, int>("1次", 1));
            runCountDiskData.Add(new KeyValuePair<string, int>("2次", 2));
            runCountDiskData.Add(new KeyValuePair<string, int>("3次", 3));
            runCountDiskData.Add(new KeyValuePair<string, int>("循环", -1));
            cbRunCount.DisplayMember = "key";
            cbRunCount.ValueMember = "value";
            cbRunCount.DataSource = runCountDiskData;
            cbRunCount.SelectedValue = Setting.DiskSpeed.RunCount;

            //自动执行
            List<KeyValuePair<string, bool>> autoFlagData = new List<KeyValuePair<string, bool>>();
            autoFlagData.Add(new KeyValuePair<string, bool>("自动", true));
            autoFlagData.Add(new KeyValuePair<string, bool>("手动", false));
            cbAutoRun.DisplayMember = "key";
            cbAutoRun.ValueMember = "value";
            cbAutoRun.DataSource = autoFlagData;
            cbAutoRun.SelectedValue = Setting.DiskSpeed.AutoFlag;

            //写入速度设置
            nudWriteSet.Value = Setting.DiskSpeed.WriteSpeedRange;

            //读取速度设置
            nudReadSet.Value = Setting.DiskSpeed.ReadSpeedRange;
        }

        #endregion 初始化

        #region 测试开关

        /// <summary>
        /// 启动或停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFlag_Click(object sender, EventArgs e)
        {
            if (btnFlag.Text == START)
                StartTest();
            else
                StopTest();
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
                            StartTest();
                        break;

                    case DBT_DEVICEREMOVECOMPLETE:
                        //deviceName = DiskHelper.GetDeviceName(m);
                        //List<string> disks = DiskHelper.GetDisks();
                        //if (disks.Contains(cbTargetDisk.Text))
                        //    return;
                        //if (deviceName != null && deviceName.StartsWith(cbTargetDisk.Text))
                        //{
                        //    AddLog($"{deviceName}设备已移除");
                        //    StopTest();
                        //    SetState(0);
                        //}
                        AddLog($"{deviceName}设备已移除");
                        StopTest();
                        SetState(0);
                        break;
                }
            }

            base.WndProc(ref m);
        }

        #endregion 设备事件

        #region 开始测试

        /// <summary>
        /// 开始测试
        /// </summary>
        private void StartTest()
        {
            try
            {
                //防止日志内容过多
                if (rtbLog.Text.Length > 2000)
                    rtbLog.Text = "";
                //状态设置
                TestingState = true;
                btnFlag.Text = STOP;
                btnFlag.BackColor = Color.Red;
                progressTimer.Enabled = true;

                //异步执行
                Thread thread = new Thread(new ParameterizedThreadStart(TestSpeedThreadStart));
                thread.IsBackground = true;
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("FileSize", Convert.ToUInt64(cbFileSize.SelectedValue));
                param.Add("BlockSize", Convert.ToUInt32(cbBlockSize.SelectedValue));
                param.Add("TargetDisk", Convert.ToString(cbTargetDisk.Text));
                param.Add("RunCount", Convert.ToInt32(cbRunCount.SelectedValue));
                thread.Start(param);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"异常: {ex.Message}", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion 开始测试

        #region 异步线程

        //线程消息处理
        private void HandleThreadMessage(object state)
        {
            ProgressMsg progressMsg = state as ProgressMsg;

            if (progressMsg.Completed)
            {
                progressTimer_Tick(null, null);
                StopTest();
            }
            else
            {
                if (!string.IsNullOrEmpty(progressMsg.Text))
                    AddLog(progressMsg.Text);
            }
            //读取结果处理
            labWriteSpeed.Text = progressMsg.WriteSpeed.ToString();
            if (progressMsg.WriteSpeed >= nudWriteSet.Value)
            {
                labWriteResult.Text = "PASS";
                labWriteResult.BackColor = Color.Green;
            }
            else if (progressMsg.WriteSpeed < nudWriteSet.Value && progressMsg.WriteSpeed > 0)
            {
                labWriteResult.Text = "FIAL";
                labWriteResult.BackColor = Color.Red;
            }
            else
            {
                labWriteResult.Text = "Testing";
                labWriteResult.BackColor = Color.Orange;
            }
            //读取结果处理
            labReadSpeed.Text = progressMsg.ReadSpeed.ToString();
            if (progressMsg.ReadSpeed >= nudReadSet.Value)
            {
                labReadResult.Text = "PASS";
                labReadResult.BackColor = Color.Green;
            }
            else if (progressMsg.ReadSpeed < nudReadSet.Value && progressMsg.ReadSpeed > 0)
            {
                labReadResult.Text = "FIAL";
                labReadResult.BackColor = Color.Red;
            }
            else
            {
                labReadResult.Text = "Testing";
                labReadResult.BackColor = Color.Orange;
            }
        }

        //线程方法
        private async void TestSpeedThreadStart(object threadParam)
        {
            //报告进度参数
            ProgressMsg proMsg = new ProgressMsg();
            proMsg.Completed = false;
            try
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                //参数
                Dictionary<string, object> param = threadParam as Dictionary<string, object>;
                long fileSize = Convert.ToInt64(param["FileSize"]);
                int blockSize = Convert.ToInt32(param["BlockSize"]);
                string targetFile = Convert.ToString(param["TargetDisk"]) + "\\" + TestFileName;
                int runCount = Convert.ToInt32(param["RunCount"]);
                // 生成随机数据
                using (var randomData = new RNGCryptoServiceProvider())
                {
                    BlockBytes = new byte[blockSize];
                    randomData.GetBytes(BlockBytes);
                }

                int currRunCount = 0;
                //判断循环（TestingState：ture 且 （运行次数小于等于0[一直循环] 或 当前运行次数小于指定次数））
                while (TestingState && (runCount <= 0 || currRunCount < runCount))
                {
                    currRunCount += 1;
                    //初始化线程消息
                    proMsg.Text = "";
                    proMsg.WriteSpeed = 0;
                    proMsg.ReadSpeed = 0;
                    TotalWriteBytes = 0;
                    TotalReadBytes = 0;

                    proMsg.Text = "开始测试";
                    SyncContext.Post(HandleThreadMessage, proMsg);
                    //开始写入测试
                    HiStopWatch.Restart();
                    await Task.Run(() => SeqWriteFile(targetFile, fileSize));
                    long totalMilliseconds = (int)HiStopWatch.Elapsed.TotalMilliseconds;
                    HiStopWatch.Stop();
                    double readSpeedMBps = ((double)(TotalWriteBytes / (1024 * 1024))) / totalMilliseconds * 1000;
                    proMsg.WriteSpeed = (int)readSpeedMBps;
                    proMsg.Text = $"写入速度{proMsg.WriteSpeed}Mbps";
                    SyncContext.Post(HandleThreadMessage, proMsg);

                    //读取
                    HiStopWatch.Restart();
                    await Task.Run(() => SeqReadFile(targetFile, fileSize));
                    totalMilliseconds = (int)HiStopWatch.Elapsed.TotalMilliseconds;
                    HiStopWatch.Stop();
                    double readReadMBps = ((double)(TotalReadBytes / (1024 * 1024))) / totalMilliseconds * 1000;
                    proMsg.ReadSpeed = (int)readReadMBps;
                    proMsg.Text = $"读取速度{proMsg.ReadSpeed}Mbps";
                    SyncContext.Post(HandleThreadMessage, proMsg);

                    //删除文件
                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                        proMsg.Text = "已删除写入文件" + Environment.NewLine + "------------------------";
                        SyncContext.Post(HandleThreadMessage, proMsg);
                    }
                    System.Threading.Thread.Sleep(500);
                }
                proMsg.Completed = true;
                proMsg.Text = "";
                SyncContext.Post(HandleThreadMessage, proMsg);
            }
            catch (Exception ex)
            {
                proMsg.Text = "异常中断";
                SyncContext.Post(HandleThreadMessage, proMsg);
            }
        }

        #endregion 异步线程

        #region 顺序写入文件

        /// <summary>
        /// 顺序写入文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileSize"></param>
        private void SeqWriteFile(string filePath, long fileSize)
        {
            try
            {
                // 生成随机数据
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists)
                    fi.Delete();
                SafeFileHandle safeFileHandle = WinApiFunctions.CreateFile(filePath, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.CreateNew, WinApiFunctions.FILE_FLAG_NO_BUFFERING | WinApiFunctions.FILE_FLAG_SEQUENTIAL_SCAN, IntPtr.Zero);
                if (safeFileHandle.IsInvalid)
                {
                    throw new IOException("无法打开文件流.", new Win32Exception());
                }
                FileStream fileStream = new FileStream(safeFileHandle, FileAccess.ReadWrite, BlockBytes.Length, false);
                TotalWriteBytes = 0;
                while (TotalWriteBytes < fileSize)
                {
                    fileStream.Write(BlockBytes, 0, BlockBytes.Length);
                    TotalWriteBytes += BlockBytes.Length;
                }
                fileStream.Flush();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                ProgressMsg proMsg = new ProgressMsg();
                proMsg.Completed = false;
                proMsg.Text = "写入时异常中断";
                SyncContext.Post(HandleThreadMessage, proMsg);
            }
        }

        #endregion 顺序写入文件

        #region 顺序读取文件

        /// <summary>
        /// 顺序读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileSize"></param>
        /// <exception cref="IOException"></exception>
        private unsafe void SeqReadFile(string filePath, long fileSize)
        {
            try
            {
                //非托管
                IntPtr hglobal = Marshal.AllocHGlobal(BlockBytes.Length);
                uint bytesRead = 0;
                SafeFileHandle safeFileHandle = WinApiFunctions.CreateFile(filePath, FileAccess.Read, FileShare.None, IntPtr.Zero, FileMode.Open, WinApiFunctions.FILE_FLAG_NO_BUFFERING | WinApiFunctions.FILE_FLAG_SEQUENTIAL_SCAN, IntPtr.Zero);
                if (safeFileHandle.IsInvalid)
                {
                    throw new IOException("无法打开文件流.", new Win32Exception());
                }
                FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Read, BlockBytes.Length, false);
                TotalReadBytes = 0;
                while (TotalReadBytes < fileSize)
                {
                    WinApiFunctions.ReadFile(safeFileHandle, hglobal.ToPointer(), BlockBytes.Length, out bytesRead, IntPtr.Zero);
                    TotalReadBytes += bytesRead;
                }
                Marshal.FreeHGlobal(hglobal);
                fileStream.Close();
            }
            catch (Exception ex)
            {
                ProgressMsg proMsg = new ProgressMsg();
                proMsg.Completed = false;
                proMsg.Text = "读取时异常中断";
                SyncContext.Post(HandleThreadMessage, proMsg);
            }
        }

        #endregion 顺序读取文件

        #region 停止测试

        private void SetState(int state)
        {
            switch (state)
            {
                case 0:
                    TestingState = false;
                    btnFlag.Text = START;
                    btnFlag.BackColor = Color.Green;
                    progressTimer.Enabled = false;
                    labWriteResult.BackColor = Color.Blue;
                    labWriteResult.Text = "Ready";
                    labReadResult.BackColor = Color.Blue;
                    labReadResult.Text = "Ready";
                    writeProgressBar.Value = 0;
                    readProgressBar.Value = 0;
                    labWriteSpeed.Text = "0";
                    labReadSpeed.Text = "0";
                    break;
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        private void StopTest()
        {
            TestingState = false;
            btnFlag.Text = START;
            btnFlag.BackColor = Color.Green;
            progressTimer.Enabled = false;
        }

        #endregion 停止测试

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

        #region 定时任务

        //更新进度条
        private void progressTimer_Tick(object sender, EventArgs e)
        {
            long fileSize = Convert.ToInt64(cbFileSize.SelectedValue);
            //写入进度
            int writeProgress = (int)(((decimal)TotalWriteBytes / fileSize) * 100);
            writeProgressBar.Value = writeProgress;
            //读取进度
            int readProgress = (int)(((decimal)TotalReadBytes / fileSize) * 100);
            readProgressBar.Value = readProgress;
        }

        #endregion 定时任务

        #region 窗体关闭

        private void DiskSpeedTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            progressTimer.Enabled = false;
            Setting.DiskSpeed.FileSize = Convert.ToInt64(cbFileSize.SelectedValue);
            Setting.DiskSpeed.BlockSize = Convert.ToInt32(cbBlockSize.SelectedValue);
            Setting.DiskSpeed.TargetDisk = cbTargetDisk.Text;
            Setting.DiskSpeed.RunCount = Convert.ToInt32(cbRunCount.SelectedValue);
            Setting.DiskSpeed.AutoFlag = Convert.ToBoolean(cbAutoRun.SelectedValue);
            Setting.DiskSpeed.WriteSpeedRange = (int)nudWriteSet.Value;
            Setting.DiskSpeed.ReadSpeedRange = (int)nudReadSet.Value;
        }

        #endregion 窗体关闭
    }

    //线程消息类
    internal class ProgressMsg
    {
        public bool Completed { get; set; }
        public int ReadSpeed { get; set; }
        public string Text { get; set; }
        public int WriteSpeed { get; set; }
    }
}