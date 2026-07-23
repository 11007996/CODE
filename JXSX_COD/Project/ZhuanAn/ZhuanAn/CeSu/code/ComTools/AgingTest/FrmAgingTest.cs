using ComTools.SerialPortService;
using ComTools.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ComTools.AgingTest
{
    public partial class FrmAgingTest : Form
    {
        private static readonly string START = "Start";
        private static readonly string STOP = "Stop";
        private static ILog log = LogManager.GetLogger(typeof(FrmAgingTest));
        private TestStateEnum GlobalTestState = TestStateEnum.Ready;
        private Dictionary<string, SerialPortProxy> SerialPortProxyDict = new Dictionary<string, SerialPortProxy>();
        private SynchronizationContext SyncContext = null;//异步上下文

        //串口通信代理
        private Dictionary<string, TestResult> TestResultDict = new Dictionary<string, TestResult>();//项目测试结果字典key:测试项目名称，value:测试项目结果

        //全局测试状态

        public FrmAgingTest()
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

            //控件赋值绑定
            nudDeviceCount.DataBindings.Add("Value", Setting.AgingTest, "DeviceCount");
            nudLayoutColCount.DataBindings.Add("Value", Setting.AgingTest, "LayoutColCount");
            nudTestCount.DataBindings.Add("Value", Setting.AgingTest, "TestCount");
            nudPassCount.DataBindings.Add("Value", Setting.AgingTest, "PassCount");

            cmbBaudRate.DataBindings.Add("Text", Setting.AgingTest, "BaudRate");
            tbDataBits.DataBindings.Add("Text", Setting.AgingTest, "DataBits");
            cmbStopBits.DataBindings.Add("SelectedItem", Setting.AgingTest, "StopBits");
            cmbParity.DataBindings.Add("SelectedItem", Setting.AgingTest, "Parity");

            dgvTestItem.DataSource = Setting.AgingTest.TestItems;

            InitItemResultLayout();
        }

        #endregion 窗口初始化

        #region 窗体关闭

        private void FrmDPLineTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTest();
            foreach (SerialPortProxy port in SerialPortProxyDict.Values)
            {
                port.Close();
            }
        }

        #endregion 窗体关闭

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
            InitAllSerialPortProxy();
            AddLog("初始化完成");

            GlobalTestState = TestStateEnum.Testing;

            //每个设备开启一个线程测试。
            foreach (string item in Setting.AgingTest.SerialPortNames)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(StartTestThreadTask));
                    thread.IsBackground = true;
                    thread.Start(TestResultDict[item]);
                }
            }
        }

        private void StopTest()
        {
            btnFlag.Text = START;
            btnFlag.BackColor = Color.Green;
            GlobalTestState = TestStateEnum.Complete;

            AddLog("结束测试" + Environment.NewLine + "------------------");
        }

        #endregion 测试开关

        #region 异步线程任务

        //检查所有测试设备是否完成测试。
        private void CheckAllProcessCallBack(object state)
        {
            bool isComplate = true;
            foreach (TestResult r in TestResultDict.Values)
            {
                //当存在最少一个 设备状态不为【Pass,Fail,Complete】的设备时，表示全局的测试状态还未完成
                if (r.State != TestStateEnum.Pass && r.State != TestStateEnum.Fail && r.State != TestStateEnum.Complete)
                {
                    isComplate = false;
                    break;
                }
            }

            if (isComplate)
            {
                StopTest();
            }
        }

        /// <summary>
        /// 起动异步任务
        /// </summary>
        /// <param name="obj"></param>
        private void StartTestThreadTask(object obj)
        {
            TestResult result = obj as TestResult;
            if (GlobalTestState == TestStateEnum.Testing && result.State == TestStateEnum.Ready)
            {
                result.State = TestStateEnum.Testing;
                SyncContext.Post(TestProcessCallBack, result);

                //测试次数
                for (int i = 1; i <= Setting.AgingTest.TestCount; i++)
                {
                    //判断是否中止测试
                    if (GlobalTestState != TestStateEnum.Testing)
                        break;

                    SyncContext.Post(AddLog, result.PortName + ":开始第【" + i + "】次测试");
                    result.Message = "当前第" + i + "次测试";
                    SyncContext.Post(TestProcessCallBack, result);

                    //循环测试项目
                    foreach (AgingTestItemConfig item in Setting.AgingTest.TestItems)
                    {
                        string errorMsg = "";
                        if (!TestItem(result, item, ref errorMsg))
                            break;
                    }

                    //判断本次所有测试项目是否成功
                    bool r = true;
                    foreach (KeyValuePair<string, string> kv in result.ItemResult)
                    {
                        if (kv.Value != "PASS")
                            r = false;
                    }
                    if (r)
                        result.PassCount++;
                    else
                        result.FailCount++;
                    SyncContext.Post(AddLog, result.PortName + ":第【" + i + "】次测试结果:" + (r ? "PASS" : "FAIL"));

                    //更新进度
                    result.Progress = (int)Math.Floor(((double)i / Setting.AgingTest.TestCount) * 100);
                    SyncContext.Post(TestProcessCallBack, result);
                }

                //判断测试成功次数是否达标
                if (result.PassCount < Setting.AgingTest.PassCount)
                {//未通过
                    result.State = TestStateEnum.Fail;
                }
                else
                {//通过
                    result.State = TestStateEnum.Pass;
                }
                result.Message = "PASS次数:" + result.PassCount + ",FAIL次数:" + result.FailCount;
                SyncContext.Post(TestProcessCallBack, result);
                SyncContext.Post(CheckAllProcessCallBack, result);
            }
        }

        /// <summary>
        /// 测试项目
        /// </summary>
        /// <param name="result"></param>
        /// <param name="config"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private bool TestItem(TestResult result, AgingTestItemConfig config, ref string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                //初始化当前测试项目
                result.CurrTestItemName = config.TestItem;
                result.ItemResult[config.TestItem] = null;
                //当前测试设备串口
                SerialPortProxy serialPortProxy = SerialPortProxyDict[result.PortName];

                Stopwatch hiStopWatch = new Stopwatch();
                hiStopWatch.Start();

                //测试项目：启动刷屏测试
                if (config.TestItem == EnumHelper.GetEnumDescription(AgingTestItemEnum.Start_Screen_Refresh))
                {//单次发送，防止重复启动
                    //发送测试指令
                    SendCodeToPLC(serialPortProxy, config);
                    //判断 是否超时  当前测试项目是否结结束
                    while (GlobalTestState == TestStateEnum.Testing && hiStopWatch.ElapsedMilliseconds < config.OverTime * 1000 && result.CurrTestItemName == config.TestItem)
                    {
                        Thread.Sleep(1000);
                    }
                }

                //测试项目：获取刷屏结果
                if (config.TestItem == EnumHelper.GetEnumDescription(AgingTestItemEnum.Screen_Refresh_Result))
                {
                    //判断 是否超时  当前测试项目是否结结束
                    while (GlobalTestState == TestStateEnum.Testing && hiStopWatch.ElapsedMilliseconds < config.OverTime * 1000 && result.CurrTestItemName == config.TestItem)
                    {
                        //发送测试指令
                        SendCodeToPLC(serialPortProxy, config);
                        Thread.Sleep(1000);
                    }
                }

                //判断测试项目结果
                if (result.ItemResult[config.TestItem] == "PASS")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                errorMsg = "程序异常:" + ex.Message;
                return false;
            }
        }

        //测试进度回调
        private void TestProcessCallBack(object state)
        {
            if (state != null)
            { //测试项目
                TestResult item = state as TestResult;
                ShowTestResult(item);
            }
        }

        #endregion 异步线程任务

        #region 添加测试项目

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            dgvTestItem.DataSource = null;
            Setting.AgingTest.TestItems.Add(new AgingTestItemConfig());
            dgvTestItem.DataSource = Setting.AgingTest.TestItems;
        }

        #endregion 添加测试项目

        #region 删除测试项目

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvTestItem.CurrentCell.RowIndex >= 0)
            {
                AgingTestItemConfig config = dgvTestItem.Rows[dgvTestItem.CurrentCell.RowIndex].DataBoundItem as AgingTestItemConfig;
                Setting.AgingTest.TestItems.Remove(config);
            }
            else
            {
                MessageBox.Show("未选中行");
            }
            dgvTestItem.DataSource = null;
            dgvTestItem.DataSource = Setting.AgingTest.TestItems;
        }

        #endregion 删除测试项目

        #region 测试结果布局

        //调整列宽占比
        private void ChangeColumnPercent(TableLayoutPanel tableLayout)
        {
            int columnCount = tableLayout.ColumnCount;
            float percent = 100F / columnCount; // 计算当前每列所占的百分比
            tableLayout.ColumnStyles.Clear();
            // 将所有列设置为相同的百分比
            for (int i = 0; i < tableLayout.ColumnCount; i++)
            {
                tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
            }
        }

        //调整行高占比
        private void ChangeRowPercent(TableLayoutPanel tableLayout)
        {
            int rowCount = tableLayout.RowCount;
            float percent = 100F / rowCount; // 计算当前每行所占的百分比
            // 将所有行设置为相同的百分比
            tableLayout.RowStyles.Clear();
            for (int i = 0; i < tableLayout.RowCount; i++)
            {
                tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, percent));
            }
        }

        /// <summary>
        /// 初始化项目结果的布局
        /// </summary>
        private void InitItemResultLayout()
        {
            //计算行列数量
            int count = (int)nudDeviceCount.Value;
            int colCount = (int)nudLayoutColCount.Value;
            int rowCount = (int)Math.Ceiling((double)count / colCount);

            //获取当前所有串口名称
            string[] portNames = SerialPort.GetPortNames();

            //字体大小
            float fontSize = 45F;
            fontSize = fontSize * ((float)1 / colCount);
            if (fontSize < 9F)
                fontSize = 9F;

            tlPanelResult.Controls.Clear();
            tlPanelResult.ColumnCount = colCount;
            tlPanelResult.RowCount = rowCount;
            //调整占比
            ChangeRowPercent(tlPanelResult);
            ChangeColumnPercent(tlPanelResult);

            List<System.Windows.Forms.Control> controls = new List<System.Windows.Forms.Control>();
            for (int i = 0; i < Setting.AgingTest.SerialPortNames.Count; i++)
            {
                string item = Setting.AgingTest.SerialPortNames[i];
                // panelResult
                Panel panelResult = new Panel();
                panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
                panelResult.Location = new System.Drawing.Point(3, 3);
                panelResult.Size = new System.Drawing.Size(286, 139);
                panelResult.TabIndex = 0;
                panelResult.Tag = item;

                // cmbSerialPortName
                int tempCount = Setting.AgingTest.SerialPortNames.Count(t => t == item);
                ComboBox cmbSerialPortName = new ComboBox();
                cmbSerialPortName.BackColor = tempCount != 1 ? Color.Red : string.IsNullOrWhiteSpace(item) ? Color.Red : System.Drawing.Color.Blue;
                cmbSerialPortName.Dock = System.Windows.Forms.DockStyle.Top;
                cmbSerialPortName.ForeColor = System.Drawing.Color.White;
                cmbSerialPortName.FormattingEnabled = true;
                cmbSerialPortName.Location = new System.Drawing.Point(0, 0);
                cmbSerialPortName.Size = new System.Drawing.Size(485, 23);
                cmbSerialPortName.TabIndex = 4;
                cmbSerialPortName.Text = item;
                cmbSerialPortName.Items.AddRange(portNames);
                cmbSerialPortName.Tag = i;
                cmbSerialPortName.SelectedIndexChanged += new System.EventHandler(this.cmbSerialPort_SelectedIndexChanged);

                // labResult
                Label labResult = new Label();
                labResult.BackColor = System.Drawing.Color.Blue;
                labResult.Dock = System.Windows.Forms.DockStyle.Fill;
                labResult.Font = new System.Drawing.Font("宋体", fontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                labResult.ForeColor = System.Drawing.Color.White;
                labResult.Location = new System.Drawing.Point(0, 25);
                labResult.Size = new System.Drawing.Size(286, 89);
                labResult.TabIndex = 1;
                labResult.Text = "Ready";
                labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                labResult.Tag = "Result";

                //progressBar
                ProgressBar progressBar = new ProgressBar();
                progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
                progressBar.Location = new System.Drawing.Point(0, 81);
                progressBar.Size = new System.Drawing.Size(371, 10);
                progressBar.TabIndex = 3;
                progressBar.Tag = "Progress";

                panelResult.Controls.Add(progressBar);
                panelResult.Controls.Add(labResult);
                panelResult.Controls.Add(cmbSerialPortName);

                controls.Add(panelResult);
            }
            tlPanelResult.Controls.AddRange(controls.ToArray());
        }

        /// <summary>
        /// 初始化设备串口
        /// </summary>
        private void InitItemResultPort()
        {
            int count = (int)nudDeviceCount.Value;
            if (Setting.AgingTest.SerialPortNames.Count > count)
            {
                Setting.AgingTest.SerialPortNames = Setting.AgingTest.SerialPortNames.GetRange(0, count);
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i >= Setting.AgingTest.SerialPortNames.Count)
                        Setting.AgingTest.SerialPortNames.Add("");
                }
            }
        }

        /// <summary>
        /// 计算列数
        /// </summary>
        /// <returns></returns>
        private void MathRowColumnCount(ref int rowCount, ref int colCount)
        {
            int count = (int)nudDeviceCount.Value;
            double r = System.Math.Sqrt(count);//开根
            colCount = (int)Math.Ceiling(r);
            rowCount = (int)Math.Floor(r);
            if (colCount * rowCount < count)
                rowCount++;
        }

        #endregion 测试结果布局

        #region 数据表格事件

        //单击单元格
        private void dgvTestItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AgingTestItemConfig testItem = dgvTestItem.Rows[e.RowIndex].DataBoundItem as AgingTestItemConfig;
                int idx = Setting.AgingTest.TestItems.IndexOf(testItem);
                //项目配置
                if (dgvTestItem.Columns[e.ColumnIndex].Name == "dgcTestConfig")
                {
                    List<string> list = EnumHelper.GetEnumDescStr<AgingTestItemEnum>();
                    FrmAgingTestItemConfig frm = new FrmAgingTestItemConfig(testItem, list);
                    frm.ShowDialog();
                    if (frm.TestItemConfig != null)
                    {
                        Setting.AgingTest.TestItems[idx] = frm.TestItemConfig;
                        dgvTestItem.DataSource = null;
                        dgvTestItem.DataSource = Setting.AgingTest.TestItems;
                    }
                }
            }
        }

        #endregion 数据表格事件

        #region 重置

        private void btnReset_Click(object sender, EventArgs e)
        {
            StopTest();
            InitAllTestItemResult();
        }

        #endregion 重置

        #region 最大化

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (btnMax.Text == "最大化")
            {
                btnMax.Text = "还原";
                tlPanelConfig.Visible = false;
                tlPanelItemConfig.Visible = false;
                rtbLog.Visible = false;
            }
            else
            {
                btnMax.Text = "最大化";
                tlPanelConfig.Visible = true;
                tlPanelItemConfig.Visible = true;
                rtbLog.Visible = true;
            }
        }

        #endregion 最大化

        #region 初始化所有测试的结果

        /// <summary>
        /// 初始化所有串口
        /// </summary>
        private void InitAllSerialPortProxy()
        {
            foreach (string item in Setting.AgingTest.SerialPortNames)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                try
                {
                    if (!SerialPortProxyDict.Keys.Contains(item))
                    {
                        SerialPortConfig serialPort = new SerialPortConfig();
                        serialPort.PortName = item;
                        serialPort.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
                        serialPort.StopBits = (StopBits)cmbStopBits.SelectedValue;
                        serialPort.DataBits = Convert.ToInt32(tbDataBits.Text);
                        serialPort.Parity = (Parity)cmbParity.SelectedValue;
                        SerialPortProxy sp = new SerialPortProxy();
                        sp.Open(serialPort, this.HandleMsg);
                        SerialPortProxyDict.Add(item, sp);
                    }
                }
                catch (Exception ex)
                {
                    AddLog(item + "串口初始化失败");
                }
            }
        }

        /// <summary>
        /// 初始化所有测试的结果
        /// </summary>
        private void InitAllTestItemResult()
        {
            foreach (string device in Setting.AgingTest.SerialPortNames)
            {
                if (string.IsNullOrEmpty(device))
                    continue;
                TestResult result;
                if (TestResultDict.Keys.Contains(device))
                    result = TestResultDict[device];
                else
                {
                    result = new TestResult();
                    result.PortName = device;
                    TestResultDict.Add(device, result);
                }
                result.State = TestStateEnum.Ready;
                result.PortState = null;
                result.Message = "";
                result.Progress = 0;
                result.PassCount = 0;
                result.FailCount = 0;
                result.ItemResult.Clear();
                foreach (AgingTestItemConfig item in Setting.AgingTest.TestItems)
                {
                    result.ItemResult.Add(item.TestItem, null);
                }
                ShowTestResult(result);
            }
        }

        #endregion 初始化所有测试的结果

        #region 更新测试结果展示

        //显示测试项目结果
        public void ShowTestResult(TestResult result)
        {
            string testItemName = result.PortName;
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
                                color = Color.Orange; break;
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
                            control.Text = result.State.ToString() + (string.IsNullOrEmpty(result.Message) ? "" : Environment.NewLine + "[" + result.Message + "]");
                        }
                        else if ("Progress".Equals(control.Tag))
                        {
                            (control as ProgressBar).Value = result.Progress;
                        }
                        else if (control is ComboBox)
                        {//串口状态
                            if (result.PortState == null)
                                control.BackColor = Color.Blue;
                            else if (result.PortState == true)
                                control.BackColor = Color.Green;
                            else control.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        #endregion 更新测试结果展示

        #region 发送消息

        /// <summary>
        /// 发送指令到测试设备
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void SendCodeToPLC(SerialPortProxy proxy, AgingTestItemConfig item)
        {
            if (item == null) return;
            byte[] bytes = new byte[item.RequestByteSize];
            byte[] codes = SoftBasic.HexStringToBytes(item.RequestHexCode);
            for (int i = 0; i < codes.Length && i < bytes.Length; i++)
            {
                bytes[i] = codes[i];
            }
            //判断状态，尝试启动串口
            if (!proxy.SerialListenState)
                proxy.TryReOpen();
            TestResult result = TestResultDict[proxy.GetSerialPort().PortName];
            //发送数据
            if (proxy.SendMessage(bytes))
            {//发送成功
                string hexText = SoftBasic.ByteToHexString(bytes);
                SyncContext.Post(AddLog, proxy.GetSerialPort().PortName + ":发送" + hexText);
            }
            result.PortState = proxy.SerialListenState;
        }

        #endregion 发送消息

        #region 接收消息处理

        public byte[] HandleMsg(SerialDataReceivedArgs args)
        {
            try
            {
                byte[] data = args.Data;
                string hexStr = BitConverter.ToString(data);
                SyncContext.Post(AddLog, args.SerialPort.PortName + ":接收" + hexStr);
                TestResult result = TestResultDict[args.SerialPort.PortName];
                //判断回调信息
                foreach (AgingTestItemConfig config in Setting.AgingTest.TestItems)
                {
                    if (hexStr.StartsWith(config.ResponseHexCode) && data.Length == config.ResponseByteSize)
                    {
                        //int prefixSize = string.IsNullOrWhiteSpace(BaseInfo.PrefixHexCode) ? 0 : BaseInfo.PrefixHexCode.Split('-').Length;//前缀字节个数
                        int dataSize = Convert.ToInt16(data[config.ResponseHexCode.Split('-').Length]);// data.Length - suffixSize;//有效数据的字节个数
                        string[] sourceCode = hexStr.Split('-');
                        string[] codes = new string[dataSize];
                        Array.Copy(sourceCode, 3, codes, 0, dataSize);
                        ////hexStr = BitConverter.ToString(data);
                        ////请求代码
                        string state = string.Join("-", codes);
                        //判断响应的状态
                        if (state == config.PassHexCode)
                        {
                            result.ItemResult[config.TestItem] = "PASS";
                            result.CurrTestItemName = null;
                        }
                        else if (state == config.FailHexCode)
                        {
                            result.ItemResult[config.TestItem] = "FAIL";
                            result.CurrTestItemName = null;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        //private void HandleMsgCallBack(object state)
        //{
        //    byte[] data = state as byte[];
        //    string hexStr = BitConverter.ToString(data);
        //    AddLog($"接收指令:{hexStr}");
        //    //取前面2字节，表示请求
        //    if (hexStr.EndsWith(Setting.DPLine.SuffixHexCode))
        //    {
        //        //int prefixSize = string.IsNullOrWhiteSpace(BaseInfo.PrefixHexCode) ? 0 : BaseInfo.PrefixHexCode.Split('-').Length;//前缀字节个数
        //        int suffixSize = string.IsNullOrWhiteSpace(Setting.DPLine.SuffixHexCode) ? 0 : Setting.DPLine.SuffixHexCode.Split('-').Length;//后缀字节个数
        //        int dataSize = data.Length - suffixSize;//有效数据的字节个数
        //        string[] sourceCode = hexStr.Split('-');
        //        string[] codes = new string[dataSize];
        //        Array.Copy(sourceCode, 0, codes, 0, dataSize);
        //        ////hexStr = BitConverter.ToString(data);
        //        ////请求代码
        //        //if (codes[0] == Setting.DPLine.StartTestHexCode)
        //        //{ //请求开始测试
        //        //    btnFlag_Click(null, null);
        //        //}
        //        //else
        //        //{ //请求测试结果
        //        //    SendTestItemResultToPLC(GetTestItemByRequestCode(codes[0]));
        //        //}
        //    }
        //}

        #endregion 接收消息处理

        #region 添加日志

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="msg"></param>
        public void AddLog(string msg)
        {
            if (rtbLog.TextLength > 10000)
                rtbLog.Clear();
            rtbLog.SelectionColor = Color.Green;
            rtbLog.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + Environment.NewLine);
            rtbLog.SelectionColor = rtbLog.ForeColor;
            // 添加文本
            rtbLog.AppendText(msg + Environment.NewLine);
            rtbLog.Focus();
            btnFlag.Focus();
        }

        /// <summary>
        /// 异常线程日志
        /// </summary>
        /// <param name="msg"></param>
        public void AddLog(object msg)
        {
            AddLog(msg.ToString());
        }

        #endregion 添加日志

        #region 控件交互

        //串口选择事件
        private void cmbSerialPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            string serialPort = combo.Text;
            int index = Convert.ToInt32(combo.Tag);
            Setting.AgingTest.SerialPortNames[index] = serialPort;
            combo.Parent.Tag = serialPort;

            //检查是否有重复
            ComboBox tempCmb;
            foreach (Control panel in tlPanelResult.Controls)
            {
                tempCmb = panel.Controls[2] as ComboBox;
                int tempCount = Setting.AgingTest.SerialPortNames.Count(t => t == tempCmb.Text);
                if (tempCount == 1)
                {
                    tempCmb.BackColor = Color.Blue;
                }
                else
                {
                    tempCmb.BackColor = Color.Red;
                }
            }
        }

        //设备总数变更
        private void nudDeviceCount_ValueChanged(object sender, EventArgs e)
        {
            InitItemResultPort();
            int row = 1;
            int col = 1;
            MathRowColumnCount(ref row, ref col);
            Setting.AgingTest.LayoutColCount = col;
            nudLayoutColCount_ValueChanged(null, null);
        }

        //设备行列布局变更
        private void nudLayoutColCount_ValueChanged(object sender, EventArgs e)
        {
            InitItemResultLayout();
        }

        #endregion 控件交互
    }
}