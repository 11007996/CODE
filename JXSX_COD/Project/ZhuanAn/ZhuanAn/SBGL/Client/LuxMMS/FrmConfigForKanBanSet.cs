using Common;
using Common.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LuxMMS
{
    public partial class FrmConfigForKanBanSet : Form
    {

        public FrmConfigForKanBanSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmConfigForKanBanSet_Load(object sender, EventArgs e)
        {
            //-------------数据准备---------------
            string sql = @"SELECT DISTINCT Dept FROM S_User_T;";
            DataSet ds = DBUtil.GetDataSet(sql, new string[] {  "deptDT" });
            DataTable deptDT = ds.Tables["deptDT"];
            DataRow dr = deptDT.NewRow();
            deptDT.Rows.InsertAt(dr, 0);
          
            //部门
            cmbDept.DisplayMember = "Dept";
            cmbDept.ValueMember = "Dept";
            cmbDept.DataSource = deptDT;
        
            //--------------------数据设置--------------------
            //看板设置
            cmbDept.Text = BaseInfo.Dept;
            nudMachineNum.Value = BaseInfo.MachineNum;
            nudPreWeekNum.Value = BaseInfo.PreWeekNum;
            cmbKanBanShow.Text = BaseInfo.KanBanShow;
            swBtnCallHandlerShowFlag.Value = BaseInfo.CallHandlerShowFlag;
        }


        //关闭时保存配置文件
        private void FrmSystemConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //看板
            dic.Add("deptName", cmbDept.Text.Trim());
            dic.Add("machineNum", nudMachineNum.Value.ToString());
            dic.Add("preWeekNum", nudPreWeekNum.Value.ToString());
            dic.Add("kanbanShow", cmbKanBanShow.Text.ToString());
            dic.Add("callHandlerShowFlag", swBtnCallHandlerShowFlag.Value ? "Y" : "N");
            ConfigUtil.ModifyXmlConfig(dic);
        }

    }
}
