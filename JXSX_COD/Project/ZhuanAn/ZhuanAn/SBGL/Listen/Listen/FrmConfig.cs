using Listen;
using Listen.Base;
using Listen.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Listen
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

            //--------------------数据设置--------------------
            //操作
            chkDayMaintenanceFlag.Checked = BaseInfo.DayMaintenanceCheckFlag;
            chkWeekMaintenanceFlag.Checked = BaseInfo.WeekMaintenanceCheckFlag;
            chkMonthMaintenanceFlag.Checked = BaseInfo.MonthMaintenanceCheckFlag;
            //通信编码
            tbPrefix.Text = BaseInfo.PrefixHexCode;
            tbSuffix.Text = BaseInfo.SuffixHexCode;
            //TCP
            chkTcpListenFlag.Checked = BaseInfo.TCPListenFlag;
            nudPort.Value = BaseInfo.Port;
            nudReceiveTimeout.Value = BaseInfo.ReceiveTimeout;
            cbListenIP.Text = BaseInfo.ListenIP.ToString();
            //串口
            chkSerailListenFlag.Checked = BaseInfo.SerialListenFlag;
            cmbPortName.Text = BaseInfo.PortName;
            cmbBaudRate.Text = BaseInfo.BaudRate.ToString();
            tbDataBits.Text = BaseInfo.DataBits.ToString();
            cmbStopBits.SelectedItem = BaseInfo.StopBits;
            cmbParity.SelectedItem = BaseInfo.Parity;
        }

        //关闭时保存配置文件
        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Net.IPAddress checkIP = null;
            if (!System.Net.IPAddress.TryParse(cbListenIP.Text, out checkIP))
            {
                MessageBox.Show("IP地址异常");
                e.Cancel = true;
                return;
            }
            if (!IsHexWithHyphen(tbPrefix.Text))
            {
                MessageBox.Show("前缀编码异常");
                e.Cancel = true;
                return;
            }
            if (!IsHexWithHyphen(tbSuffix.Text))
            {
                MessageBox.Show("后缀编码异常");
                e.Cancel = true;
                return;
            }
            //保养操作
            dic.Add("dayMaintenanceCheckFlag", chkDayMaintenanceFlag.Checked ? "Y" : "N");
            dic.Add("weekMaintenanceCheckFlag", chkWeekMaintenanceFlag.Checked ? "Y" : "N");
            dic.Add("monthMaintenanceCheckFlag", chkMonthMaintenanceFlag.Checked ? "Y" : "N");
            //通信编码
            dic.Add("prefixHexCode", tbPrefix.Text.Trim());
            dic.Add("suffixHexCode", tbSuffix.Text.Trim());
            int codeSize = Convert.ToInt32(tbPrefixSize.Text) + Convert.ToInt32(tbMachineSize.Text) + Convert.ToInt32(tbOperateSize.Text) + Convert.ToInt32(tbRunStateSize.Text) + Convert.ToInt32(tbLineSize.Text) + Convert.ToInt32(tbRemarkSize.Text)
                           + Convert.ToInt32(tbGoodSize.Text) + Convert.ToInt32(tbNGSize.Text) + Convert.ToInt32(tbWarnCodeSize.Text) + Convert.ToInt32(tbWarnDescSize.Text) + Convert.ToInt32(tbSuffixSize.Text);
            dic.Add("hexCodeByteSize", codeSize.ToString());
            //TCP
            dic.Add("tcpListenFlag", chkTcpListenFlag.Checked ? "Y" : "N");
            dic.Add("listenIP", cbListenIP.Text);
            dic.Add("listenPort", nudPort.Value.ToString());
            dic.Add("receiveTimeout", nudReceiveTimeout.Value.ToString());
            //串口
            dic.Add("serialListenFlag", chkSerailListenFlag.Checked ? "Y" : "N");
            dic.Add("listenPortName", cmbPortName.Text.Trim());
            dic.Add("baudRate", cmbBaudRate.Text.Trim());
            dic.Add("dataBits", tbDataBits.Text.Trim());
            dic.Add("stopBits", ((int)cmbStopBits.SelectedItem).ToString());
            dic.Add("parity", ((int)cmbParity.SelectedItem).ToString());

            ConfigUtil.ModifyXmlConfig(dic);
        }

        //端口检查
        private void btnCheckPort_Click(object sender, EventArgs e)
        {
            bool flag = NetWorkHelper.PortInUse((int)nudPort.Value, PortType.TCP);
            if (flag)
            {
                MessageBox.Show("端口已被占用", "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("端口可用", "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }


        #region 前缀事件
        //字符检查
        private void tbPrefix_KeyPress(object sender, KeyPressEventArgs e)
        {
            //过滤掉不符合16进制的字符
            if (!IsHexChar(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        //内容修正
        private void tbPrefix_TextChanged(object sender, EventArgs e)
        {
            string prefix = tbPrefix.Text.Trim();

            if (string.IsNullOrWhiteSpace(prefix))
            {
                tbPrefixSize.Text = "0";
            }
            else
            {
                //字符处理
                string newCode = ConvertText(prefix);
                if (newCode != prefix)
                {
                    tbPrefix.Text = newCode;
                }
                //光标设置在内容结尾
                tbPrefix.Select(tbPrefix.Text.Length, 0);
                tbPrefixSize.Text = tbPrefix.Text.Split('-').Length.ToString();
            }
        }
        #endregion

        #region 后缀事件
        private void tbSuffix_KeyPress(object sender, KeyPressEventArgs e)
        {
            //过滤掉不符合16进制的字符
            if (!IsHexChar(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        private void tbSuffix_TextChanged(object sender, EventArgs e)
        {
            string suffix = tbSuffix.Text.Trim();
            if (string.IsNullOrWhiteSpace(suffix))
            {
                tbSuffixSize.Text = "0";
            }
            else
            {
                //字符处理
                string newCode = ConvertText(suffix);
                if (newCode != suffix)
                {
                    tbSuffix.Text = newCode;
                }
                //光标设置在内容结尾
                tbSuffix.Select(tbSuffix.Text.Length, 0);
                tbSuffixSize.Text = tbSuffix.Text.Split('-').Length.ToString();
            }
        }
        #endregion

        #region 字符处理
        /// <summary>
        /// 判断字符是否是16进制的字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsHexChar(string input)
        {
            string pattern = "[A-Fa-f0-9]+|\b|-"; //Hex16进制表字
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断字符串是否符合指定正则(16进制)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsHexWithHyphen(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return true;
            // 使用正则表达式匹配16进制字符串和连字符
            string pattern = @"^([0-9A-Fa-f]{2}-)*[0-9A-Fa-f]{2}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }


        /// <summary>
        /// 转换字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string ConvertText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            if (text.IndexOf(' ') > 0)
            {
                text = text.Replace(' ', '-');
            }
            if (!IsHexWithHyphen(text))
            {
                string[] strArr = text.Split('-');
                //检查最后输入的字符，判断是否要追加插入'-'字符。
                if (strArr[strArr.Length - 1].Length > 2)
                {
                    strArr[strArr.Length - 1] = strArr[strArr.Length - 1].Substring(0, 2) + "-" + strArr[strArr.Length - 1].Substring(2, 1);
                    text = string.Join("-", strArr);
                }
            }
            return text;
        }
        #endregion

    }
}
