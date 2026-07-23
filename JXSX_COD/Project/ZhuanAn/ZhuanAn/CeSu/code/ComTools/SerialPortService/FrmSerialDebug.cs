using ComTools.Util;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ComTools.SerialPortService
{
    public partial class FrmSerialDebug
    {
        private SerialPortProxy _SerialPortProxy = null;
        private SerialPortConfig Config = new SerialPortConfig();
        private SynchronizationContext SyncContext = null;//异步上下文

        public FrmSerialDebug()
        {
            InitializeComponent();
            InitControlBind();
            _SerialPortProxy = new SerialPortProxy();
            SyncContext = SynchronizationContext.Current;
        }

        public FrmSerialDebug(SerialPortProxy serialPortProxy)
        {
            InitializeComponent();
            InitControlBind();
            _SerialPortProxy = serialPortProxy;
            SyncContext = SynchronizationContext.Current;
        }

        private void InitControlBind()
        {
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
        }

        private void FrmSerialDebug_Load(object sender, EventArgs e)
        {
            UpdateSerialPortName();
            RefreshControls();
        }

        #region 打开

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //串口参数
            int baudRate = 0;
            int dataBits = 0;
            //校验参数
            if (!int.TryParse(tbBaudRate.Text, out baudRate))
            {
                MessageBox.Show("波特率输入错误！");
                return;
            }
            if (!int.TryParse(tbDataBits.Text, out dataBits))
            {
                MessageBox.Show("数据位输入错误！");
                return;
            }

            Config.PortName = cmbCom.Text;
            Config.BaudRate = baudRate;
            Config.DataBits = dataBits;
            Config.StopBits = (StopBits)cmbStopBits.SelectedItem;
            Config.Parity = (Parity)cmbParity.SelectedItem;
            try
            {
                _SerialPortProxy.Open(Config, this.HandleMsg);
                //刷新控件
                RefreshControls();
            }
            catch (Exception ex)
            {
                SoftBasic.ShowExceptionMessage(ex);
            }
        }

        #endregion 打开

        #region 关闭

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                _SerialPortProxy.Close();
                RefreshControls();
            }
            catch (Exception ex)
            {
                SoftBasic.ShowExceptionMessage(ex);
            }
        }

        #endregion 关闭

        #region 发送

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] send = null;
            send = ((!ckbHex.Checked) ? Encoding.ASCII.GetBytes(tbSendData.Text) : SoftBasic.HexStringToBytes(tbSendData.Text));

            if (chkShowSend.Checked)
            {
                if (chkShowTime.Checked)
                {
                    tbData.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "][发]   " + SoftBasic.ByteToHexString(send, ' ') + Environment.NewLine);
                }
                else
                {
                    tbData.AppendText(SoftBasic.ByteToHexString(send, ' ') + Environment.NewLine);
                }
            }
            _SerialPortProxy.SendMessage(send);
        }

        #endregion 发送

        #region 控件状态

        private void RefreshControls()
        {
            cmbCom.Text = Config.PortName;
            tbBaudRate.Text = Config.BaudRate.ToString();
            tbDataBits.Text = Config.DataBits.ToString();
            cmbStopBits.SelectedItem = Config.StopBits;
            cmbParity.SelectedItem = Config.Parity;
            if (_SerialPortProxy.SerialListenState)
            {
                cmbCom.Enabled = false;
                tbBaudRate.Enabled = false;
                tbDataBits.Enabled = false;
                cmbStopBits.Enabled = false;
                cmbParity.Enabled = false;
                panelData.Enabled = true;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
            }
            else
            {
                cmbCom.Enabled = true;
                tbBaudRate.Enabled = true;
                tbDataBits.Enabled = true;
                cmbStopBits.Enabled = true;
                cmbParity.Enabled = true;
                panelData.Enabled = false;
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
            }
        }

        #endregion 控件状态

        #region 其他

        //系统消息，处理串口的热拔插事件
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.WM_DEVICE_CHANGE://设备改变
                    switch (m.WParam.ToInt32())
                    {
                        case WM.DBT_DEVICE_REMOVE_COMPLETE://设备移除
                        case WM.DBT_DEVICEARRIVAL://设备插入
                            UpdateSerialPortName();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        //回车键发送
        private void tbSendData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSend.PerformClick();
            }
        }

        //串口扫描
        private void UpdateSerialPortName()//扫描串口方法
        {
            //定义字符串数组，数组名为 ArryPort
            string[] ArryPort = SerialPort.GetPortNames();
            //清除当前组合框下拉菜单内容
            cmbCom.Items.Clear();
            //将所有的可用串口号添加到  端口 对应的组合框中
            for (int i = 0; i < ArryPort.Length; i++)
            {
                cmbCom.Items.Add(ArryPort[i]);
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbData.Text = "";
        }

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
            byte[] buffer = state as byte[];
            string text = ((!ckbHex.Checked) ? Encoding.ASCII.GetString(buffer) : SoftBasic.ByteToHexString(buffer, ' '));
            if (chkShowTime.Checked)
            {
                tbData.AppendText($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}][收]   {text}{Environment.NewLine}");
            }
            else
            {
                tbData.AppendText("[收]   " + text + Environment.NewLine);
            }
        }

        #endregion 接收消息处理

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSerialDebug_FormClosed(object sender, FormClosedEventArgs e)
        {
            _SerialPortProxy.Close();
        }

        #endregion 其他
    }
}