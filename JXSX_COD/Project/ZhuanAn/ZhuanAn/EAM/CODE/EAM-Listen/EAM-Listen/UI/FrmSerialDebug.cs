using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmSerialDebug
    {
        public FrmSerialDebug()
        {
            InitializeComponent();
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
            SerialPortProxy.OpenListenView = true;
            timerShowData.Enabled = true;
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

            Setting.SerialPortConfig.PortName = cmbCom.Text;
            Setting.SerialPortConfig.BaudRate = baudRate;
            Setting.SerialPortConfig.DataBits = dataBits;
            Setting.SerialPortConfig.StopBits = (StopBits)cmbStopBits.SelectedItem;
            Setting.SerialPortConfig.Parity = (Parity)cmbParity.SelectedItem;
            try
            {
                SerialPortProxy.Open();
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
                SerialPortProxy.Close();
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
            SerialPortProxy.SendMessage(send);
        }

        #endregion 发送

        #region 控件状态

        private void RefreshControls()
        {
            cmbCom.Text = Setting.SerialPortConfig.PortName;
            tbBaudRate.Text = Setting.SerialPortConfig.BaudRate.ToString();
            tbDataBits.Text = Setting.SerialPortConfig.DataBits.ToString();
            cmbStopBits.SelectedItem = Setting.SerialPortConfig.StopBits;
            cmbParity.SelectedItem = Setting.SerialPortConfig.Parity;
            if (SerialPortProxy.SerialListenState)
            {
                cmbCom.Enabled = false;
                tbBaudRate.Enabled = false;
                tbDataBits.Enabled = false;
                cmbStopBits.Enabled = false;
                cmbParity.Enabled = false;
                panelData.Enabled = true;
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                timerShowData.Enabled = true;
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
                timerShowData.Enabled = false;
            }
        }

        #endregion 控件状态

        #region 定时任务

        //定时任务
        private void timerShowData_Tick(object sender, EventArgs e)
        {
            ShowReceivedData();
            ShowSendData();
            //数据过多时自动清理
            if (tbData.Text.Length > 5000)
            {
                int index = tbData.Text.IndexOf(Environment.NewLine, 4000, 300);
                if (index > 0)
                {
                    tbData.Text = tbData.Text.Substring(index);
                }
            }
        }

        /// <summary>
        /// 显示接收的数据
        /// </summary>
        private void ShowReceivedData()
        {
            if (SerialPortProxy.SerialListenState && SerialPortProxy.revData.Count > 0)
            {
                for (int i = SerialPortProxy.revData.Count - 1; i >= 0; i--)
                {
                    byte[] buffer = SerialPortProxy.revData[i];
                    string text = ((!ckbHex.Checked) ? Encoding.ASCII.GetString(buffer) : SoftBasic.ByteToHexString(buffer, ' '));
                    if (chkShowTime.Checked)
                    {
                        tbData.AppendText($"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") }][收]   {text}{Environment.NewLine}");
                    }
                    else
                    {
                        tbData.AppendText("[收]   " + text + Environment.NewLine);
                    }
                    SerialPortProxy.revData.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 显示发送的数据
        /// </summary>
        private void ShowSendData()
        {
            if (SerialPortProxy.SerialListenState && SerialPortProxy.resData.Count > 0)
            {
                for (int i = SerialPortProxy.resData.Count - 1; i >= 0; i--)
                {
                    byte[] buffer = SerialPortProxy.resData[i];
                    string data = ((!ckbHex.Checked) ? Encoding.ASCII.GetString(buffer) : SoftBasic.ByteToHexString(buffer, ' '));
                    if (chkShowSend.Checked)
                    {
                        if (chkShowTime.Checked)
                        {
                            tbData.AppendText("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "][发]   " + data + Environment.NewLine);
                        }
                        else
                        {
                            tbData.AppendText(data + Environment.NewLine);
                        }
                    }
                    SerialPortProxy.resData.RemoveAt(i);
                }
            }
        }

        #endregion 定时任务

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

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSerialServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerialPortProxy.OpenListenView = false;
            timerShowData.Enabled = false;
        }

        #endregion 其他
    }
}