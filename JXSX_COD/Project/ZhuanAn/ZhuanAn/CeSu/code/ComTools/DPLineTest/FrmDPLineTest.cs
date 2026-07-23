using ComTools.Automation;
using ComTools.SerialPortService;
using ComTools.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ComTools.DPLineTest
{
    public partial class FrmDPLineTest : Form
    {
        private static readonly string START = "Start";
        private static readonly string STOP = "Stop";
        private static ILog log = LogManager.GetLogger(typeof(FrmAutomationTest));
        private readonly Stopwatch HiStopWatch = new Stopwatch();//高精度时间测量
        private SynchronizationContext SyncContext = null;//异步上下文
        private Dictionary<string, TestResult> TestResultDict = new Dictionary<string, TestResult>();//项目测试结果字典key:测试项目名称，value:测试项目结果
        private TestStateEnum GlobalTestState = TestStateEnum.Ready;//全局测试状态
        private SerialPortProxy SerialPortProxy = new SerialPortProxy(); //串口通信代理
        private bool DEVICE_ARRIVAL_FLAG = false;//设备是否就绪

        public FrmDPLineTest()
        {
            InitializeComponent();
            tlPanelResult.Controls.Clear();
            tlPanelResult.ColumnCount = 1;
            tlPanelResult.RowCount = 1;
            dgvTestItem.AutoGenerateColumns = false;
            SyncContext = SynchronizationContext.Current;
        }

        #region 窗口初始化

        private void FrmDPLineTest_Load(object sender, EventArgs e)
        {
            //--------------数据表格控件绑定---------------------
            //启动模式
            IList<TestStartModelEnum> modelList = new List<TestStartModelEnum>();
            modelList.Add(TestStartModelEnum.手动);
            modelList.Add(TestStartModelEnum.设备就绪);
            modelList.Add(TestStartModelEnum.上位机);
            cmbModel.DataSource = modelList;
            //运算符
            //IList<KeyValuePair<OperatorEnum, string>> operatorItem = EnumHelper.GetEnumDescriptions<OperatorEnum>();
            //DataGridViewComboBoxCell cmbOperator = new DataGridViewComboBoxCell();
            //cmbOperator.DataSource = operatorItem;
            //cmbOperator.DisplayMember = "Value";
            //cmbOperator.ValueMember = "Key";
            //dgvTestItem.Columns["dgcOperator"].CellTemplate = cmbOperator;

            //----------------串口相关控件的绑定------------------
            //串口(停止位)
            IList<StopBits> stopBitsList = new List<StopBits>();
            stopBitsList.Add(StopBits.None);
            stopBitsList.Add(StopBits.One);
            stopBitsList.Add(StopBits.Two);
            stopBitsList.Add(StopBits.OnePointFive);
            cmbStopBits.DataSource = stopBitsList;
            //串口(奇偶校验)
            IList<Parity> parityList = new List<Parity>();
            parityList.Add(Parity.None);
            parityList.Add(Parity.Odd);
            parityList.Add(Parity.Even);
            parityList.Add(Parity.Mark);
            parityList.Add(Parity.Space);
            cmbParity.DataSource = parityList;

            //-----------------基本设置绑定------------------
            if (Setting.DPLine.TestItems.Count == 0)
                Setting.DPLine.TestItems.Add(new TestItemConfig());
            dgvTestItem.DataSource = Setting.DPLine.TestItems;
            cmbModel.DataBindings.Add("SelectedItem", Setting.DPLine, "Model");
            if (Setting.DPLine.SerialPort == null)
                Setting.DPLine.SerialPort = new SerialPortConfig();
            //串口
            txbStartTestHexCode.DataBindings.Add("Text", Setting.DPLine, "StartTestHexCode");
            txbEndTestHexCode.DataBindings.Add("Text", Setting.DPLine, "EndTestHexCode");
            txbHexSuffix.DataBindings.Add("Text", Setting.DPLine, "SuffixHexCode");

            cmbPortName.DataBindings.Add("SelectedItem", Setting.DPLine.SerialPort, "PortName");
            cmbBaudRate.DataBindings.Add("Text", Setting.DPLine.SerialPort, "BaudRate");
            tbDataBits.DataBindings.Add("Text", Setting.DPLine.SerialPort, "DataBits");
            cmbStopBits.DataBindings.Add("SelectedItem", Setting.DPLine.SerialPort, "StopBits");
            cmbParity.DataBindings.Add("SelectedItem", Setting.DPLine.SerialPort, "Parity");
            chkSerailListenFlag.DataBindings.Add("Checked", Setting.DPLine, "SerailListenFlag");
            nudStartTestOvertime.DataBindings.Add("Value", Setting.DPLine, "StartTestOverTime");
        }

        #endregion 窗口初始化

        #region 窗体关闭

        private void FrmDPLineTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTest();
            bgWorkTest.CancelAsync();
            SerialPortProxy.Close();
        }

        #endregion 窗体关闭

        #region 添加日志

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="msg"></param>
        public void AddLog(string msg)
        {
            rtbLog.SelectionColor = Color.Green;
            rtbLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + Environment.NewLine);
            rtbLog.SelectionColor = rtbLog.ForeColor;
            // 添加文本
            rtbLog.AppendText(msg + Environment.NewLine);
            rtbLog.Focus();
            btnFlag.Focus();
        }

        public void AddLog(object msg)
        {
            AddLog(msg.ToString());
        }

        #endregion 添加日志

        #region 测试开关

        private void btnFlag_Click(object sender, EventArgs e)
        {
            if (btnFlag.Text == START)
            {
                StartTest();
            }
            else
            {
                StopTest();
            }
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void StartTest()
        {
            if (rtbLog.Text.Length > 1000)
                rtbLog.Text = "";
            btnFlag.Text = STOP;
            btnFlag.BackColor = Color.Red;
            AddLog("开始测试");
            InitAllTestItemResult();
            GlobalTestState = TestStateEnum.Ready;
            Thread thread = new Thread(new ParameterizedThreadStart(StartTestThreadTask));
            thread.IsBackground = true;
            thread.Start();
        }

        private void StopTest()
        {
            btnFlag.Text = START;
            btnFlag.BackColor = Color.Green;
            GlobalTestState = TestStateEnum.Complete;

            AddLog("结束测试" + Environment.NewLine + "------------------");
            //判断是否要发送测试结束指令给PLC
            if (Setting.DPLine.Model == TestStartModelEnum.上位机 && chkSerailListenFlag.Checked && !string.IsNullOrEmpty(Setting.DPLine.EndTestHexCode))
            {
                string hex = Setting.DPLine.EndTestHexCode + "-" + Setting.DPLine.SuffixHexCode;
                SendMessageToPLC(hex);
            }
        }

        #endregion 测试开关

        #region 异步线程任务

        /// <summary>
        /// 起动异步任务
        /// </summary>
        /// <param name="obj"></param>
        private void StartTestThreadTask(object obj)
        {
            if (GlobalTestState == TestStateEnum.Ready)
            {
                GlobalTestState = TestStateEnum.Testing;
                if (Setting.DPLine.Model == TestStartModelEnum.设备就绪 || Setting.DPLine.Model == TestStartModelEnum.上位机)
                {
                    HiStopWatch.Restart();
                    int totalMilliseconds = 0;
                    //等待设备就绪
                    while (!DEVICE_ARRIVAL_FLAG && GlobalTestState == TestStateEnum.Testing)
                    {
                        Thread.Sleep(100);
                        //判断是否有设置启动超时
                        totalMilliseconds = (int)HiStopWatch.Elapsed.TotalMilliseconds;
                        if (Setting.DPLine.StartTestOverTime > 0 && totalMilliseconds > Setting.DPLine.StartTestOverTime * 1000)
                        {
                            SyncContext.Post(HandleStartTestOvertime, null);
                            HiStopWatch.Stop();
                            return;
                        }
                    }
                }

                foreach (TestItemConfig item in Setting.DPLine.TestItems)
                {
                    if (GlobalTestState == TestStateEnum.Testing)
                        SyncContext.Post(TestItemCallBack, item);
                    while (GlobalTestState == TestStateEnum.Testing)
                    {
                        //检查当前测试项目的测试进度,确保测试项目的按顺序执行
                        if (TestResultDict[item.TestItem].Progress >= 100 && !bgWorkTest.IsBusy)
                            break;
                        else
                            Thread.Sleep(100);
                    }
                }
                SyncContext.Post(TestItemCallBack, null);
                GlobalTestState = TestStateEnum.Complete;
            }
        }

        private void HandleStartTestOvertime(object state)
        {
            foreach (var item in TestResultDict)
            {
                item.Value.Message = "启动测试超时";
                item.Value.State = TestStateEnum.Error;
                ShowTestResult(item.Value);
            }
            StopTest();
        }

        private void TestItemCallBack(object state)
        {
            if (state != null)
            { //测试项目
                TestItemConfig item = state as TestItemConfig;
                AddLog($"开始测试:{item.TestItem}");
                bgWorkTest.RunWorkerAsync(item);
            }
            else
            {//停止测试
                StopTest();
            }
        }

        #endregion 异步线程任务

        #region 设备监听

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
            base.WndProc(ref m);
            if (m.Msg == WM_DEVICECHANGE)
            {
                switch ((int)m.WParam)
                {
                    case DBT_DEVICEARRIVAL:
                        //防止重复执行
                        if (!DEVICE_ARRIVAL_FLAG)
                        {
                            AddLog("设备已插入");
                            if ((TestStartModelEnum)cmbModel.SelectedItem == TestStartModelEnum.设备就绪)
                            {
                                StartTest();
                            }
                        }
                        DEVICE_ARRIVAL_FLAG = true;
                        break;

                    case DBT_DEVICEREMOVECOMPLETE:
                        //防止重复执行
                        if (DEVICE_ARRIVAL_FLAG)
                        {
                            AddLog("设备已移除");
                            StopTest();
                        }
                        DEVICE_ARRIVAL_FLAG = false;
                        break;
                }
            }
        }

        #endregion 设备监听

        #region 添加测试项目

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            dgvTestItem.DataSource = null;
            Setting.DPLine.TestItems.Add(new TestItemConfig());
            dgvTestItem.DataSource = Setting.DPLine.TestItems;
        }

        #endregion 添加测试项目

        #region 删除测试项目

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvTestItem.CurrentCell.RowIndex >= 0)
            {
                TestItemConfig config = dgvTestItem.Rows[dgvTestItem.CurrentCell.RowIndex].DataBoundItem as TestItemConfig;
                Setting.DPLine.TestItems.Remove(config);
            }
            else
            {
                MessageBox.Show("未选中行");
            }
            dgvTestItem.DataSource = null;
            dgvTestItem.DataSource = Setting.DPLine.TestItems;
        }

        #endregion 删除测试项目

        #region 测试结果布局

        /// <summary>
        /// 初始化项目结果的布局
        /// </summary>
        private void InitItemResultLayout()
        {
            tlPanelResult.Controls.Clear();
            //计算行列数量
            tlPanelResult.ColumnCount = MathColumnCount();
            tlPanelResult.RowCount = 1;
            // tlPanelResult.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

            List<System.Windows.Forms.Control> controls = new List<System.Windows.Forms.Control>();
            foreach (var item in Setting.DPLine.TestItems)
            {
                // panelResult
                Panel panelResult = new Panel();
                panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
                panelResult.Location = new System.Drawing.Point(3, 3);
                panelResult.Size = new System.Drawing.Size(286, 139);
                panelResult.TabIndex = 0;
                panelResult.Tag = item.TestItem;
                // labResultItemName
                Label labResultItemName = new Label();
                labResultItemName.BackColor = System.Drawing.Color.MediumBlue;
                labResultItemName.Dock = System.Windows.Forms.DockStyle.Top;
                labResultItemName.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labResultItemName.ForeColor = System.Drawing.Color.White;
                labResultItemName.Location = new System.Drawing.Point(0, 0);
                labResultItemName.Size = new System.Drawing.Size(286, 25);
                labResultItemName.TabIndex = 0;
                labResultItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                labResultItemName.Text = item.TestItem;
                // labResult
                Label labResult = new Label();
                labResult.BackColor = System.Drawing.Color.Blue;
                labResult.Dock = System.Windows.Forms.DockStyle.Fill;
                labResult.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labResult.ForeColor = System.Drawing.Color.White;
                labResult.Location = new System.Drawing.Point(0, 25);
                labResult.Size = new System.Drawing.Size(286, 89);
                labResult.TabIndex = 1;
                labResult.Text = "Ready";
                labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labResult.Tag = "Result";
                // labMessage
                Label labMessage = new Label();
                labMessage.BackColor = System.Drawing.Color.Blue;
                labMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
                labMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labMessage.ForeColor = System.Drawing.Color.White;
                labMessage.Location = new System.Drawing.Point(0, 114);
                labMessage.Size = new System.Drawing.Size(286, 25);
                labMessage.TabIndex = 2;
                labMessage.Text = "";
                labMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labMessage.Tag = "Message";

                //progressBar
                ProgressBar progressBar = new ProgressBar();
                progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
                progressBar.Location = new System.Drawing.Point(0, 81);
                progressBar.Size = new System.Drawing.Size(371, 10);
                progressBar.TabIndex = 3;
                progressBar.Tag = "Progress";

                panelResult.Controls.Add(labMessage);
                panelResult.Controls.Add(progressBar);
                panelResult.Controls.Add(labResult);
                panelResult.Controls.Add(labResultItemName);

                controls.Add(panelResult);
            }
            tlPanelResult.Controls.AddRange(controls.ToArray());
        }

        /// <summary>
        /// 计算列数
        /// </summary>
        /// <returns></returns>
        private int MathColumnCount()
        {
            int columnCunt = 1;
            int count = dgvTestItem.RowCount;
            if (count <= 3)
            {
                columnCunt = count;
            }
            else if (count == 4)
            {
                columnCunt = 2;
            }
            else
            {
                columnCunt = 3;
            }
            return columnCunt;
        }

        #endregion 测试结果布局

        #region 数据表格事件

        //单击单元格
        private void dgvTestItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TestItemConfig testItem = dgvTestItem.Rows[e.RowIndex].DataBoundItem as TestItemConfig;
                int idx = Setting.DPLine.TestItems.IndexOf(testItem);
                //项目配置
                if (dgvTestItem.Columns[e.ColumnIndex].Name == "dgcTestConfig")
                {
                    List<string> list = EnumHelper.GetEnumDescStr<TestItemEnum>();
                    FrmTestItemConfig frm = new FrmTestItemConfig(testItem, list);
                    frm.ShowDialog();
                    if (frm.TestItemConfig != null)
                    {
                        Setting.DPLine.TestItems[idx] = frm.TestItemConfig;
                        dgvTestItem.DataSource = null;
                        dgvTestItem.DataSource = Setting.DPLine.TestItems;
                    }
                }
            }
        }

        //添加测试项目
        private void dgvTestItem_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            InitItemResultLayout();
        }

        //删除测试项目
        private void dgvTestItem_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            InitItemResultLayout();
        }

        #endregion 数据表格事件

        #region 交互事件

        private void btnReset_Click(object sender, EventArgs e)
        {
            StopTest();
            InitAllTestItemResult();
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((TestStartModelEnum)cmbModel.SelectedItem == TestStartModelEnum.上位机)
            {
                tlPanelSerialPort.Visible = true;
            }
            else
            {
                tlPanelSerialPort.Visible = false;
                chkSerailListenFlag.Checked = false;
            }
        }

        private void chkSerailListenFlag_CheckedChanged(object sender, EventArgs e)
        {
            InitSerailPort();
        }

        //初始化串口
        private void InitSerailPort()
        {
            if (chkSerailListenFlag.Checked)
            {
                SerialPortProxy.Open(Setting.DPLine.SerialPort, this.HandleMsg);
            }
            else
            {
                SerialPortProxy.Close();
            }
        }

        #endregion 交互事件

        #region 初始化所有测试的结果

        /// <summary>
        /// 初始化所有测试的结果
        /// </summary>
        private void InitAllTestItemResult()
        {
            string oldValue = "";
            foreach (TestItemConfig item in Setting.DPLine.TestItems)
            {
                try
                {
                    oldValue = AutomationUtil.GetControlValue(item.WindowName, item.AutomationID, AutomationUtil.ConvertToControlType(item.ControlType));
                }
                catch (Exception)
                {
                }
                TestResult result; ;
                if (TestResultDict.Keys.Contains(item.TestItem))
                    result = TestResultDict[item.TestItem];
                else
                {
                    result = new TestResult();
                    result.TestItem = item.TestItem;
                    TestResultDict.Add(item.TestItem, result);
                }
                result.State = TestStateEnum.Ready;
                result.oldValue = oldValue;
                result.Message = "初始化";
                result.Progress = 0;
                ShowTestResult(result);
            }
        }

        #endregion 初始化所有测试的结果

        #region 异步工作线程

        private void bgWorkTest_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            TestItemConfig item = e.Argument as TestItemConfig;
            HiStopWatch.Restart();
            long totalMilliseconds = (int)HiStopWatch.Elapsed.TotalMilliseconds;
            string newValue = "";
            string errorMsg = "";
            bool reTestEnable = true;//是否可以重测
            //开始测试
            TestResultDict[item.TestItem].State = TestStateEnum.Testing;
            TestResultDict[item.TestItem].Message = "";
            TestResultDict[item.TestItem].Progress = 0;
            bgWorker.ReportProgress(0, item);

            //前置操作
            TestResultDict[item.TestItem].Message = "开始执行前置操作";
            TestResultDict[item.TestItem].Progress = 10;
            bgWorker.ReportProgress(10, item);
            if (item.PrefixOperate != null)
            {
                errorMsg = Task.Run(() =>
                {
                    InvokExtendOperate(item.PrefixOperate, ref errorMsg);
                    if (!string.IsNullOrEmpty(errorMsg))
                        return errorMsg;
                    return null;
                }).Result;
            }

            //测试项目
            TestResultDict[item.TestItem].Message = "开始获取测试项目控件值";
            TestResultDict[item.TestItem].Progress = 20;
            bgWorker.ReportProgress(20, item);
        //重测
        retest:
            //获取原来的测试控件的值
            string oldValue = TestResultDict[item.TestItem].oldValue;
            newValue = oldValue;
            //比较值是否有变化
            while (newValue == oldValue && string.IsNullOrEmpty(errorMsg))
            {
                newValue = AutomationUtil.GetControlValue(item.WindowName, item.AutomationID, AutomationUtil.ConvertToControlType(item.ControlType));
                totalMilliseconds = (int)HiStopWatch.Elapsed.TotalMilliseconds;
                if (item.Overtime > 0 && totalMilliseconds > item.Overtime * 1000)
                    break;
                else
                    Thread.Sleep(200);
            }

            //控件值过滤
            if (string.IsNullOrEmpty(errorMsg))
            {
                TestResultDict[item.TestItem].Message = "控件值过滤";
                TestResultDict[item.TestItem].Progress = 60;
                bgWorker.ReportProgress(60, item);
                FilterFormatValue(item.FilterFormat, ref newValue, ref errorMsg);
            }
            TestResultDict[item.TestItem].newValue = newValue;

            //比较结果
            if (string.IsNullOrEmpty(errorMsg))
            {
                TestResultDict[item.TestItem].Message = "比较结果";
                TestResultDict[item.TestItem].Progress = 70;
                bgWorker.ReportProgress(70, item);
                bool testFlag = DiffValue(item.WhereValue, newValue, item.Operator, ref errorMsg);
                if (testFlag)
                    TestResultDict[item.TestItem].State = TestStateEnum.Pass;
                else
                {
                    TestResultDict[item.TestItem].State = TestStateEnum.Fail;
                    //是否重测
                    if (reTestEnable)
                    {
                        reTestEnable = false;
                        TestResultDict[item.TestItem].oldValue = newValue;
                        goto retest;
                    }
                }
            }

            //后置操作
            TestResultDict[item.TestItem].Message = "开始执行后置操作";
            TestResultDict[item.TestItem].Progress = 80;
            bgWorker.ReportProgress(80, item);
            if (item.SuffixOperate != null)
            {
                string expError = "";
                InvokExtendOperate(item.SuffixOperate, ref expError);
                errorMsg = string.IsNullOrEmpty(errorMsg) ? expError : errorMsg;
            }
            goto stop;

        //停止测试，并返回测试结果
        stop:
            HiStopWatch.Stop();
            TestResultDict[item.TestItem].Progress = 100;
            if (!string.IsNullOrEmpty(errorMsg))
            {
                TestResultDict[item.TestItem].State = TestStateEnum.Error;
                TestResultDict[item.TestItem].Message = errorMsg;
            }
            else
            {
                TestResultDict[item.TestItem].Message = $"测试完成，原值:{oldValue}，新值:{newValue}";
            }
            bgWorker.ReportProgress(100, item);
            return;
        }

        private void bgWorkTest_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            TestItemConfig item = e.UserState as TestItemConfig;
            //显示结果
            ShowTestResult(TestResultDict[item.TestItem]);
            if (e.ProgressPercentage == 100)
            {
                AddLog($"{item.TestItem}测试结果:{TestResultDict[item.TestItem].State.ToString()}");
            }
        }

        #endregion 异步工作线程

        #region 执行扩展操作

        /// <summary>
        /// 执行扩展操作
        /// </summary>
        /// <param name="operateConfig"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool InvokExtendOperate(ExtendOperateConfig operateConfig, ref string errorMsg)
        {
            errorMsg = "";
            if (operateConfig == null)
            {
                errorMsg = $"扩展操作未传递有效参数";
                return false;
            }
            try
            {
                switch (operateConfig.OperateType)
                {
                    case OperateTypeEnum.NONE:
                        return true;

                    case OperateTypeEnum.MOUSE_CLICK:
                        AutomationElement element = AutomationUtil.GetAutomationElement(operateConfig.WindowName, operateConfig.AutomationId, AutomationUtil.ConvertToControlType(operateConfig.ControlType));
                        if (element == null)
                        {
                            errorMsg = $"扩展操作[{operateConfig.OperateType}]未找到有效的自动化元素";
                        }
                        else
                        {
                            EventHandle.MouseClickElement(element);
                        }
                        break;

                    case OperateTypeEnum.OPEN_APP:
                        if (string.IsNullOrEmpty(operateConfig.AppPath) || !File.Exists(operateConfig.AppPath))
                        {
                            errorMsg = $"扩展操作[{operateConfig.OperateType}]未配置有效的应用路径";
                        }
                        else
                        {
                            EventHandle.OpenApp(operateConfig.AppPath);
                        }
                        break;

                    case OperateTypeEnum.CLOSE_WINDOW://关闭窗口
                        if (string.IsNullOrEmpty(operateConfig.WindowName))
                        {
                            errorMsg = $"扩展操作[{operateConfig.OperateType}]未配置有效的窗口名称";
                        }
                        else
                        {
                            EventHandle.CloseWindowByName(operateConfig.WindowName);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                errorMsg = $"扩展操作[{operateConfig.OperateType}]执行异常";
                log.Error(errorMsg, ex);
            }

            return errorMsg.Length <= 0;
        }

        #endregion 执行扩展操作

        #region 过滤取值

        /// <summary>
        /// 获取过滤后的值
        /// </summary>
        /// <param name="result"></param>
        public void FilterFormatValue(string filterFormat, ref string value, ref string errorMsg)
        {
            errorMsg = "";
            try
            {
                if (filterFormat != null && filterFormat.Contains("@value"))
                {
                    int startIndex = filterFormat.IndexOf("@value");
                    //过滤前面的值
                    value = value.Substring(filterFormat.IndexOf("@value"));
                    //过滤后面的值
                    int endLen = filterFormat.Length - filterFormat.IndexOf("@value") - "@value".Length;//后面无用的值个数
                    value = value.Substring(0, value.Length - endLen);
                }
            }
            catch (Exception ex)
            {
                errorMsg = $"过滤取值失败,过滤格式:{filterFormat},控件值:{value}";
            }
        }

        #endregion 过滤取值

        #region 比较两个值

        /// <summary>
        /// 比较两个值
        /// </summary>
        /// <param name="whereValue"></param>
        /// <param name="newValue"></param>
        /// <param name="operatorEnum"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool DiffValue(string whereValue, string newValue, OperatorEnum operatorEnum, ref string errorMsg)
        {
            errorMsg = "";
            bool result = false;
            try
            {
                switch (operatorEnum)
                {
                    case OperatorEnum.等于:
                        result = whereValue.Equals(newValue);
                        break;

                    case OperatorEnum.不等于:
                        result = !whereValue.Equals(newValue);
                        break;

                    case OperatorEnum.小于:
                        result = decimal.Parse(newValue) < decimal.Parse(whereValue);
                        break;

                    case OperatorEnum.小于等于:
                        result = decimal.Parse(newValue) <= decimal.Parse(whereValue);
                        break;

                    case OperatorEnum.大于:
                        result = decimal.Parse(newValue) > decimal.Parse(whereValue);
                        break;

                    case OperatorEnum.大于等于:
                        result = decimal.Parse(newValue) >= decimal.Parse(whereValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                result = false;
                errorMsg = $"运算比对错误,运算符:{operatorEnum.ToString()},当前值:{newValue},条件值:{whereValue}";
            }
            return result;
        }

        #endregion 比较两个值

        #region 更新测试结果展示

        //显示测试项目结果
        public void ShowTestResult(TestResult result)
        {
            string testItemName = result.TestItem;
            foreach (System.Windows.Forms.Control panel in tlPanelResult.Controls)
            {
                if (testItemName.Equals(panel.Tag))
                {
                    foreach (System.Windows.Forms.Control control in panel.Controls)
                    {
                        Color color = Color.Blue;
                        switch (result.State)
                        {
                            case TestStateEnum.Ready:
                                color = Color.Blue; break;
                            case TestStateEnum.Testing:
                                color = Color.Yellow; break;
                            case TestStateEnum.Error:
                                color = Color.Red; break;
                            case TestStateEnum.Pass:
                                color = Color.Green; break;
                            case TestStateEnum.Fail:
                                color = Color.Red; break;
                        };
                        if ("Result".Equals(control.Tag))
                        {
                            control.BackColor = color;
                            control.Text = result.State.ToString();
                        }
                        else if ("Message".Equals(control.Tag))
                        {
                            control.BackColor = color;
                            control.Text = result.Message;
                        }
                        else if ("Progress".Equals(control.Tag))
                        {
                            (control as ProgressBar).Value = result.Progress;
                        }
                    }
                }
            }
        }

        #endregion 更新测试结果展示

        #region 发送消息

        private void SendMessageToPLC(string hexText)
        {
            //结束测试，发送给PLC
            if (Setting.DPLine.SerailListenFlag)
            {
                byte[] bytes = SoftBasic.HexStringToBytes(hexText);
                hexText = SoftBasic.ByteToHexString(bytes);
                if (SerialPortProxy.SendMessage(bytes))
                    AddLog($"发送指令:{hexText}");
                else
                    AddLog($"发送指令失败，内容:{hexText}");
            }
        }

        #endregion 发送消息

        #region 接收消息处理

        public byte[] HandleMsg(SerialDataReceivedArgs args)
        {
            try
            {
                SyncContext.Post(HandleMsgCallBack, args.Data);
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        private void HandleMsgCallBack(object state)
        {
            byte[] data = state as byte[];
            string hexStr = BitConverter.ToString(data);
            AddLog($"接收指令:{hexStr}");
            //取前面2字节，表示请求
            if (hexStr.EndsWith(Setting.DPLine.SuffixHexCode))
            {
                //int prefixSize = string.IsNullOrWhiteSpace(BaseInfo.PrefixHexCode) ? 0 : BaseInfo.PrefixHexCode.Split('-').Length;//前缀字节个数
                int suffixSize = string.IsNullOrWhiteSpace(Setting.DPLine.SuffixHexCode) ? 0 : Setting.DPLine.SuffixHexCode.Split('-').Length;//后缀字节个数
                int dataSize = data.Length - suffixSize;//有效数据的字节个数
                string[] sourceCode = hexStr.Split('-');
                string[] codes = new string[dataSize];
                Array.Copy(sourceCode, 0, codes, 0, dataSize);
                //hexStr = BitConverter.ToString(data);
                //请求代码
                if (codes[0] == Setting.DPLine.StartTestHexCode)
                { //请求开始测试
                    btnFlag_Click(null, null);
                }
                else
                { //请求测试结果
                    SendTestItemResultToPLC(GetTestItemByRequestCode(codes[0]));
                }
            }
        }

        /// <summary>
        /// 获取测试结果给到PLC
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void SendTestItemResultToPLC(TestItemConfig item)
        {
            if (item == null) return;
            if (GlobalTestState == TestStateEnum.Complete)
            {
                if (Setting.DPLine.SerailListenFlag)
                {
                    string hexText = "";
                    if (TestResultDict[item.TestItem].State == TestStateEnum.Pass)
                        hexText = item.ResponsePassHexCode;
                    else
                        hexText = item.ResponseFailHexCode;
                    if (!string.IsNullOrEmpty(Setting.DPLine.SuffixHexCode))
                        hexText += ("-" + Setting.DPLine.SuffixHexCode);
                    SendMessageToPLC(hexText);
                }
            }
        }

        #endregion 接收消息处理

        /// <summary>
        /// 根据请求代码获取测试项目
        /// </summary>
        /// <param name="requestCode"></param>
        /// <returns></returns>
        private TestItemConfig GetTestItemByRequestCode(string requestCode)
        {
            if (string.IsNullOrEmpty(requestCode)) return null;
            TestItemConfig testItem = null;
            foreach (TestItemConfig item in Setting.DPLine.TestItems)
            {
                if (item.RequestHexCode == requestCode)
                {
                    testItem = item;
                    break;
                }
            }
            return testItem;
        }
    }
}