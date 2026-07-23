using LuxMMS;
using Common;
using Common.Base;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LuxMMS
{
    public partial class FrmConfig : Form
    {
        private FrmConfigForBaseSet frmBaseSet;
        private FrmConfigForKanBanSet frmKanBanSet;
        private FrmConfigForSpeechSet frmSpeechSet;
        private FrmConfigForSysSet frmSysSet;
        public FrmConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            tsmiBase_Click(null, null);
        }



        #region 侧栏菜单事件
        private void tsmiBase_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            if (frmBaseSet == null)
            {
                frmBaseSet = new FrmConfigForBaseSet();
                frmBaseSet.TopLevel = false;
                frmBaseSet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmBaseSet.Dock = DockStyle.Fill;
            }
            frmBaseSet.Parent = panelContent;
            frmBaseSet.Show();
        }

        private void tsmiKanban_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            if (frmKanBanSet == null)
            {
                frmKanBanSet = new FrmConfigForKanBanSet();
                frmKanBanSet.TopLevel = false;
                frmKanBanSet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmKanBanSet.Dock = DockStyle.Fill;
            }
            frmKanBanSet.Parent = panelContent;
            frmKanBanSet.Show();
        }

        private void tsmiSpeech_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            if (frmSpeechSet == null)
            {
                frmSpeechSet = new FrmConfigForSpeechSet();
                frmSpeechSet.TopLevel = false;
                frmSpeechSet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmSpeechSet.Dock = DockStyle.Fill;
            }
            frmSpeechSet.Parent = panelContent;
            frmSpeechSet.Show();
        }

        private void tsmiSystem_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            if (frmSysSet == null)
            {
                frmSysSet = new FrmConfigForSysSet();
                frmSysSet.TopLevel = false;
                frmSysSet.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmSysSet.Dock = DockStyle.Fill;

            }
            frmSysSet.Parent = panelContent;
            frmSysSet.Show();
        }

        #endregion


        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmBaseSet != null) frmBaseSet.Close();
            if (frmKanBanSet != null) frmKanBanSet.Close();
            if (frmSpeechSet != null) frmSpeechSet.Close();
            if (frmSysSet != null) frmSysSet.Close();
        }

    }
}
