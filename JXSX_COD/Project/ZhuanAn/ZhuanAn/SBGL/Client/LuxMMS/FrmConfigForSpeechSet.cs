using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LuxMMS
{
    public partial class FrmConfigForSpeechSet : Form
    {

        public FrmConfigForSpeechSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfigForSpeechSet_Load(object sender, EventArgs e)
        {
            //广播设置
            trackBarSpeechRate.Value = BaseInfo.SpeechRate;
            nudSpeechSpanMinute.Value = BaseInfo.SpeechSpanMinute;
            swBtnCallHSpeechFlag.Value = BaseInfo.CallHandlerSpeechFlag;
        }


        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //广播
            dic.Add("speechRate", trackBarSpeechRate.Value.ToString());
            dic.Add("speechSpanMinute", nudSpeechSpanMinute.Value.ToString());
            dic.Add("callHandlerSpeechFlag", swBtnCallHSpeechFlag.Value ? "Y" : "N");
            ConfigUtil.ModifyXmlConfig(dic);
        }

    }
}
