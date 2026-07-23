using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using Machine;
using Common.Util;
using Call;
using Basic;


namespace LuxMMS
{
    public partial class FrmTabFrame : Form
    {
        public FrmTabFrame()
        {
            InitializeComponent();
            //窗体最大化不遮挡任务栏
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
        }

        #region 窗体事件
        //窗体加载
        private void FrmTabFrame_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            tabItemError_Click(null, null);
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
            BaseInfo.CurrFrmType = typeof(FrmTabFrame);
            this.Refresh();
        }
        #endregion

        //异常记录选项卡
        private void tabItemError_Click(object sender, EventArgs e)
        {
            if (tabPanelError.Controls.Count <= 0)
            {
                FrmBaseError frm = new FrmBaseError();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
                FrmBaseSolution frm = new FrmBaseSolution();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelFault;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

        //处理人选项卡
        private void tabItemUser_Click(object sender, EventArgs e)
        {
            if (tabPanelUser.Controls.Count <= 0)
            {
                FrmBaseUser frm = new FrmBaseUser();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelUser;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }


        //机台管理选项卡
        private void tabItemMachine_Click(object sender, EventArgs e)
        {
            if (tabPanelMachine.Controls.Count <= 0)
            {
                FrmBaseMachine frm = new FrmBaseMachine();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelMachine;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }


        //资产清册
        private void tabItemAsset_Click(object sender, EventArgs e)
        {
            if (tabPanelAsset.Controls.Count <= 0)
            {
                FrmBaseAsset frm = new FrmBaseAsset();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelAsset;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }


        //资产保养与履历
        private void tabItemMaintenance_Click(object sender, EventArgs e)
        {
            if (tabPanelMaintenance.Controls.Count <= 0)
            {
                FrmBaseMaintenance frm = new FrmBaseMaintenance();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelMaintenance;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }
        }

        //基础信息
        private void tabItemBasic_Click(object sender, EventArgs e)
        {
            if (tabPanelBasic.Controls.Count <= 0)
            {
                FrmBasic frm = new FrmBasic();
                frm.TopLevel = false;
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frm.Parent = tabPanelBasic;
                frm.Dock = DockStyle.Fill;
                frm.Show();
            }

        }



    }
}
