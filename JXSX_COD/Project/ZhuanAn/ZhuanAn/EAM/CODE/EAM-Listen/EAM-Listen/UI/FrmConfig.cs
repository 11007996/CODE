using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace EAM.Listen.UI
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {
            InitializeComponent();
            SetFormRightLocaltion();
        }

        /// <summary>
        /// 设备窗口在右侧位置打开
        /// </summary>
        private void SetFormRightLocaltion()
        {
            var screen = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));
            var x = screen.WorkingArea.X + screen.WorkingArea.Width - this.Width;
            var y = screen.WorkingArea.Y + screen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            //TCP IP地址集合
            List<string> IPs = NetWorkHelper.GetAllLocalIPv4Address();
            cbListenIP.DataSource = IPs;
            cbMqttHost.DataSource = IPs;
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

            //--------------------数据绑定--------------------
            //Mqtt配置
            ckbMqttSubFlag.DataBindings.Add(nameof(ckbMqttSubFlag.Checked), Setting.MqttConfig, nameof(Setting.MqttConfig.MqttSubFlag));
            cbMqttHost.DataBindings.Add(nameof(cbMqttHost.Text), Setting.MqttConfig, nameof(Setting.MqttConfig.Host));
            nudMqttPort.DataBindings.Add(nameof(nudMqttPort.Value), Setting.MqttConfig, nameof(Setting.MqttConfig.Port));
            cbVersion.DataBindings.Add(nameof(cbVersion.Text), Setting.MqttConfig, nameof(Setting.MqttConfig.MqttProtocolVersion));
            tbUsername.DataBindings.Add(nameof(tbUsername.Text), Setting.MqttConfig, nameof(Setting.MqttConfig.Username));
            tbPassword.DataBindings.Add(nameof(tbPassword.Text), Setting.MqttConfig, nameof(Setting.MqttConfig.Password));
            tbClientId.DataBindings.Add(nameof(tbClientId.Text), Setting.MqttConfig, nameof(Setting.MqttConfig.ClientId));
            ckbCleanSession.DataBindings.Add(nameof(ckbCleanSession.Checked), Setting.MqttConfig, nameof(Setting.MqttConfig.CleanSession));
            nudKeepAliveSeconds.DataBindings.Add(nameof(nudKeepAliveSeconds.Value), Setting.MqttConfig, nameof(Setting.MqttConfig.KeepAliveSeconds));
            cbTopicList.DataBindings.Add(nameof(cbTopicList.DataSource), Setting.MqttConfig, nameof(Setting.MqttConfig.SubTopicList));
            //HTTP
            ckbHttpListenFlag.DataBindings.Add(nameof(ckbHttpListenFlag.Checked), Setting.HttpConfig, nameof(Setting.HttpConfig.HttpListenFlag));
            nudHttpPort.DataBindings.Add(nameof(nudHttpPort.Value), Setting.HttpConfig, nameof(Setting.HttpConfig.Port));
            //自定义TCP
            ckbTcpListenFlag.DataBindings.Add(nameof(ckbTcpListenFlag.Checked), Setting.TCPConfig, nameof(Setting.TCPConfig.TCPListenFlag));
            cbListenIP.DataBindings.Add(nameof(cbListenIP.Text), Setting.TCPConfig, nameof(Setting.TCPConfig.ListenIP));
            nudPort.DataBindings.Add(nameof(nudPort.Value), Setting.TCPConfig, nameof(Setting.TCPConfig.Port));
            nudReceiveTimeout.DataBindings.Add(nameof(nudReceiveTimeout.Value), Setting.TCPConfig, nameof(Setting.TCPConfig.ReceiveTimeout));
            //串口
            ckbSerailListenFlag.DataBindings.Add(nameof(ckbSerailListenFlag.Checked), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.SerialListenFlag));
            cmbPortName.DataBindings.Add(nameof(cmbPortName.Text), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.PortName));
            cmbBaudRate.DataBindings.Add(nameof(cmbBaudRate.Text), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.BaudRate));
            tbDataBits.DataBindings.Add(nameof(tbDataBits.Text), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.DataBits));
            cmbStopBits.DataBindings.Add(nameof(cmbStopBits.SelectedItem), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.StopBits));
            cmbParity.DataBindings.Add(nameof(cmbParity.SelectedItem), Setting.SerialPortConfig, nameof(Setting.SerialPortConfig.Parity));
            //通信
            ckbUploadFlag.DataBindings.Add(nameof(ckbUploadFlag.Checked), FactoryBaseConfig.SignalConfig, nameof(FactoryBaseConfig.SignalConfig.UploadDataFlag));
            //操作
            chkDayMaintenanceFlag.DataBindings.Add(nameof(chkDayMaintenanceFlag.Checked), FactoryBaseConfig.MaintainConfig, nameof(FactoryBaseConfig.MaintainConfig.DayMaintainFlag));
            chkWeekMaintenanceFlag.DataBindings.Add(nameof(chkWeekMaintenanceFlag.Checked), FactoryBaseConfig.MaintainConfig, nameof(FactoryBaseConfig.MaintainConfig.WeekMaintainFlag));
            chkMonthMaintenanceFlag.DataBindings.Add(nameof(chkMonthMaintenanceFlag.Checked), FactoryBaseConfig.MaintainConfig, nameof(FactoryBaseConfig.MaintainConfig.MonthMaintainFlag));
        }

        //关闭时保存配置文件
        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Setting.SaveConfig();
        }

        //端口检查
        private void btnCheckPort_Click(object sender, EventArgs e)
        {
            bool flag = NetWorkHelper.PortInUse((int)nudPort.Value, PortTypeEnum.TCP);
            if (flag)
            {
                MessageBox.Show("端口已被占用", "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("端口可用", "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnReceiveCodeConfig_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> itemNameDict = ConstantsHelper.GetConstantsDictionary<ReceiveItemNameConstant>();
            FrmItemCodeConfig frm = new FrmItemCodeConfig(FactoryBaseConfig.SignalConfig.ReceiveCodeItems, itemNameDict);
            frm.ShowDialog();
        }

        private void btn_SendCodeConfig_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> itemNameDict = ConstantsHelper.GetConstantsDictionary<SendItemNameConstant>();
            FrmItemCodeConfig frm = new FrmItemCodeConfig(FactoryBaseConfig.SignalConfig.SendCodeItems, itemNameDict);
            frm.ShowDialog();
        }

        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTopic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbTopicList.Text.Trim()))
                return;

            if (Setting.MqttConfig.SubTopicList.Contains(cbTopicList.Text.Trim()))
            {//存在删除
                Setting.MqttConfig.SubTopicList.Remove(cbTopicList.Text.Trim());
            }
            else
            {//不存在增加
                Setting.MqttConfig.SubTopicList.Add(cbTopicList.Text.Trim());
            }
            cbTopicList.DataSource = null;
            cbTopicList.DataSource = Setting.MqttConfig.SubTopicList;
        }

        private void cbTopicList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbTopicList_TextChanged(object sender, EventArgs e)
        {
            if (Setting.MqttConfig.SubTopicList.Contains(cbTopicList.Text.Trim()))
            {
                btnAddTopic.Text = "删除";
            }
            else
            {
                btnAddTopic.Text = "添加";
            }
        }
    }
}