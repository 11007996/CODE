using CallSys;
using CallSys.Utils;
using Common;
using Common.Base;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CallSys
{
    public partial class FrmSystemConfig : Form
    {
        private DataTable lineInfo;
        private DataTable areaInfo;
        private DataTable machineInfo;

        public FrmSystemConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSystemConfig_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT  Area FROM M_LineInfo_T;";
            areaInfo = DBUtil.GetDataTable(sql);
            DataRow dr = areaInfo.NewRow();
            areaInfo.Rows.InsertAt(dr, 0);

            sql = "SELECT  * FROM M_LineInfo_T;";
            lineInfo = DBUtil.GetDataTable(sql);
            DataRow dr2 = lineInfo.NewRow();
            lineInfo.Rows.InsertAt(dr2, 0);

            sql = "SELECT * FROM M_LineMachines_T";
            machineInfo = DBUtil.GetDataTable(sql);
            DataRow dr3 = machineInfo.NewRow();
            machineInfo.Rows.InsertAt(dr3, 0);


            cmbArea.DisplayMember = "Area";
            cmbArea.ValueMember = "Area";
            cmbArea.DataSource = areaInfo;
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = lineInfo;
            cmbMachine.DisplayMember = "Machine";
            cmbMachine.ValueMember = "Machine";
            cmbMachine.DataSource = machineInfo;
            //基本信息
            if (!string.IsNullOrWhiteSpace(BaseInfo.Area))
            {
                cmbArea.Text = BaseInfo.Area;
            }
            if (!string.IsNullOrWhiteSpace(BaseInfo.Line))
            {
                cmbLine.Text = BaseInfo.Line;
            }
            if (!string.IsNullOrWhiteSpace(BaseInfo.Machine))
            {
                cmbMachine.Text = BaseInfo.Machine;
            }
            //看板设置
            nudMachineNum.Value = BaseInfo.MachineNum;
            nudPreWeekNum.Value = BaseInfo.PreWeekNum;
            //呼叫设置
            if (BaseInfo.CallLimit)
            {
                radOne.Checked = true;
            }
            else
            {
                radMultiple.Checked = true;
            }
            //系统设置
            tbHandlerPicDir.Text = BaseInfo.PicCachePath;
            chkAutoUpdate.Checked = BaseInfo.AutoUpdate;


            btnBase_Click(null, null);
        }



        #region 侧栏菜单事件
        private void btnBase_Click(object sender, EventArgs e)
        {
            panelBaseInfo.Visible = true;
            panelKanBan.Visible = false;
            panelCall.Visible = false;
            panelSystem.Visible = false;
        }

        private void btnKanBan_Click(object sender, EventArgs e)
        {
            panelBaseInfo.Visible = false;
            panelKanBan.Visible = true;
            panelCall.Visible = false;
            panelSystem.Visible = false;
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            panelBaseInfo.Visible = false;
            panelKanBan.Visible = false;
            panelCall.Visible = true;
            panelSystem.Visible = false;
        }

        private void btnTcp_Click(object sender, EventArgs e)
        {
            panelBaseInfo.Visible = false;
            panelKanBan.Visible = false;
            panelCall.Visible = false;
            panelSystem.Visible = false;
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            panelBaseInfo.Visible = false;
            panelKanBan.Visible = false;
            panelCall.Visible = false;
            panelSystem.Visible = true;
        }
        #endregion

        #region 配置修改事件
        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLine.DataSource = null;
            cmbMachine.DataSource = null;
            DataTable newLines = lineInfo.Copy();
            for (int i = newLines.Rows.Count - 1; i >= 0; i--)
            {
                if (newLines.Rows[i]["Area"].ToString() != cmbArea.Text)
                {
                    newLines.Rows.RemoveAt(i);
                }
            }
            DataRow nullRow = newLines.NewRow();
            newLines.Rows.InsertAt(nullRow, 0);
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = newLines;
            cmbLine.Text = "";
        }

        private void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMachine.DataSource = null;
            DataTable newMachines = machineInfo.Copy();
            for (int i = newMachines.Rows.Count - 1; i >= 0; i--)
            {
                if (newMachines.Rows[i]["Line"].ToString() != cmbLine.Text)
                {
                    newMachines.Rows.RemoveAt(i);
                }
            }
            DataRow nullRow = newMachines.NewRow();
            newMachines.Rows.InsertAt(nullRow, 0);
            cmbMachine.DisplayMember = "Machine";
            cmbMachine.ValueMember = "Machine";
            cmbMachine.DataSource = newMachines;
            cmbMachine.Text = "";
        }

        private void btnFolderBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "选择头像缓存位置";
            dlg.SelectedPath = BaseInfo.PicCachePath;
            dlg.ShowDialog();
            tbHandlerPicDir.Text = dlg.SelectedPath;
        }
        #endregion




        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //基本
            dic.Add("areaName", cmbArea.Text.Trim());
            dic.Add("lineName", cmbLine.Text.Trim());
            dic.Add("machineName", cmbMachine.Text.Trim());
            //看板
            dic.Add("machineNum", nudMachineNum.Value.ToString());
            dic.Add("preWeekNum", nudPreWeekNum.Value.ToString());
            //呼叫
            dic.Add("callLimit", radOne.Checked ? "Y" : "N");
            //系统
            dic.Add("picCachePath", tbHandlerPicDir.Text.Trim());
            dic.Add("autoUpdate", chkAutoUpdate.Checked ? "Y" : "N");
            ConfigUtil.ModifyXmlConfig(dic);
        }

    }
}
