using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LuxMMS
{
    public partial class FrmConfigForSysSet : Form
    {

        public FrmConfigForSysSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfigForSysSet_Load(object sender, EventArgs e)
        {
            //系统设置
            tbUserPicDir.Text = BaseInfo.PicCachePath;
            swBtnAutoUpdate.Value = BaseInfo.AutoUpdate;
        }

        private void btnFolderBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "选择头像缓存位置";
            dlg.SelectedPath = BaseInfo.PicCachePath;
            dlg.ShowDialog();
            tbUserPicDir.Text = dlg.SelectedPath;
        }

        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //系统
            dic.Add("picCachePath", tbUserPicDir.Text.Trim());
            dic.Add("autoUpdate", swBtnAutoUpdate.Value ? "Y" : "N");
            ConfigUtil.ModifyXmlConfig(dic);
        }

    }
}
