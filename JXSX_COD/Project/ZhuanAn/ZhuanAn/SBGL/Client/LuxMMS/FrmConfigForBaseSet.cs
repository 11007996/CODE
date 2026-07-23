using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LuxMMS
{
    public partial class FrmConfigForBaseSet : Form
    {
        private DataTable LineInfo;
        private DataTable AreaInfo;
        private DataTable MachineInfo;

        public FrmConfigForBaseSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfigForBaseSet_Load(object sender, EventArgs e)
        {
            //-------------数据准备---------------
            string sql = @"SELECT DISTINCT  Area FROM S_LineInfo_T;
                            SELECT  * FROM S_LineInfo_T;
                            SELECT * FROM M_LineMachines_T;";
            DataSet ds = DBUtil.GetDataSet(sql, new string[] { "areaDT", "lineDT", "machineDT" });

            AreaInfo = ds.Tables["areaDT"];
            DataRow dr = AreaInfo.NewRow();
            AreaInfo.Rows.InsertAt(dr, 0);

            LineInfo = ds.Tables["lineDT"];
            DataRow dr2 = LineInfo.NewRow();
            LineInfo.Rows.InsertAt(dr2, 0);

            MachineInfo = ds.Tables["machineDT"];
            DataRow dr3 = MachineInfo.NewRow();
            MachineInfo.Rows.InsertAt(dr3, 0);

            //区域、线别、机台
            cmbArea.DisplayMember = "Area";
            cmbArea.ValueMember = "Area";
            cmbArea.DataSource = AreaInfo;
            cmbLine.DisplayMember = "Line";
            cmbLine.ValueMember = "Line";
            cmbLine.DataSource = LineInfo;
            cmbMachine.DisplayMember = "Machine";
            cmbMachine.ValueMember = "Machine";
            cmbMachine.DataSource = MachineInfo;

            //--------------------数据设置--------------------
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
            if (BaseInfo.CallLimit)
            {
                radOne.Checked = true;
            }
            else
            {
                radMultiple.Checked = true;
            }
        }


        #region 配置修改事件
        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLine.DataSource = null;
            cmbMachine.DataSource = null;
            DataTable newLines = LineInfo.Copy();
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
            DataTable newMachines = MachineInfo.Copy();
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
        #endregion


        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //基本
            dic.Add("areaName", cmbArea.Text.Trim());
            dic.Add("lineName", cmbLine.Text.Trim());
            dic.Add("machineName", cmbMachine.Text.Trim());
            dic.Add("callLimit", radOne.Checked ? "Y" : "N");
            ConfigUtil.ModifyXmlConfig(dic);
        }

    }
}
