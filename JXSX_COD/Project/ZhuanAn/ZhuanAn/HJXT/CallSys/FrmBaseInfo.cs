using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using CallSys.Utils;
using CallSys.Base;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common;


namespace CallSys
{
    public partial class FrmBaseInfo : Form
    {
        public FrmBaseInfo()
        {
            InitializeComponent();
            //窗体最大化不遮挡任务栏
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
        }

        #region 窗体事件
        //窗体加载
        private void FrmBaseInfo_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            tabItemHandler_Click(null, null);
        }

        //解决闪烁问题
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        //窗体焦点事件
        private void FrmBaseInfo_Activated(object sender, EventArgs e)
        {
            GlobalData.CurrFrmType = typeof(FrmBaseInfo);
            this.Refresh();
        }
        #endregion

        //处理人选项卡
        private void tabItemHandler_Click(object sender, EventArgs e)
        {
            if (tabPanelHandler.Controls.Count <= 0)
            {
                FrmBaseInfoHandler frm = new FrmBaseInfoHandler();
                frm.TopLevel = false;
                frm.Parent = tabPanelHandler;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

        //异常记录选项卡
        private void tabItemError_Click(object sender, EventArgs e)
        {
            if (tabPanelError.Controls.Count <= 0)
            {
                FrmBaseInfoError frm = new FrmBaseInfoError();
                frm.TopLevel = false;
                frm.Parent = tabPanelError;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

        //故障方案选项卡
        private void tabItemFault_Click(object sender, EventArgs e)
        {
            if (tabPanelFault.Controls.Count <= 0)
            {
                FrmBaseInfoSolution frm = new FrmBaseInfoSolution();
                frm.TopLevel = false;
                frm.Parent = tabPanelFault;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

    }
}
