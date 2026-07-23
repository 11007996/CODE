using Common.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Call
{
    public partial class FrmMsgPushSet : Form
    {
        public FrmMsgPushSet()
        {
            InitializeComponent();
        }

        private void FrmMsgPushSet_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT Area From S_LineInfo_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
            {
                cmbArea.DataSource = dt.AsEnumerable().Select(t => t.Field<string>("Area")).ToList();
            }
            RefreshContactPerson();
        }


        private void RefreshContactPerson()
        {
            btnCancelChecked_Click(null,null);
            string sql = "SELECT WorkCode+'-'+RealName contact FROM S_ContactPerson_T WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(tbKeyWord.Text))
            {
                sql += string.Format(" AND WorkCode like '%{0}%' OR RealName like '%{0}%'", tbKeyWord.Text);
            }
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
                chklsbContactPerson.DataSource = dt.AsEnumerable().Select(t => t.Field<string>("Contact")).ToList();
        }


        #region 呼叫支援操作
        //增加呼叫支援消息接收人
        private void btnStage1Add_Click(object sender, EventArgs e)
        {
            if (chklsbContactPerson.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请勾选要增加的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in chklsbContactPerson.CheckedItems)
            {
                string person = item.ToString();
                lsbStage1.Items.Remove(person);
                lsbStage1.Items.Add(person);
            }
            UpdateReceiver("1");
        }

        //删除呼叫支援消息接收人
        private void btnStage1Del_Click(object sender, EventArgs e)
        {
            if (lsbStage1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选中要删除的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = lsbStage1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                lsbStage1.Items.RemoveAt(lsbStage1.SelectedIndices[i]);
            }
            UpdateReceiver("1");
        }
        #endregion

        #region 呼叫超时操作
        //增加支援超时消息接收人
        private void btnStage2Add_Click(object sender, EventArgs e)
        {
            if (chklsbContactPerson.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请勾选要增加的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in chklsbContactPerson.CheckedItems)
            {
                string person = item.ToString();
                lsbStage2.Items.Remove(person);
                lsbStage2.Items.Add(person);
            }
            UpdateReceiver("2");
        }

        //删除支援超时消息接收人
        private void btnStage2Del_Click(object sender, EventArgs e)
        {
            if (lsbStage2.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选中要删除的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = lsbStage2.SelectedIndices.Count - 1; i >= 0; i--)
            {
                lsbStage2.Items.RemoveAt(lsbStage2.SelectedIndices[i]);
            }
            UpdateReceiver("2");
        }
        #endregion



        //区域变化事件
        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReceiverData();
        }


        private void UpdateReceiver(string stageType)
        {
            if (string.IsNullOrWhiteSpace(cmbArea.Text))
            {
                MessageBox.Show("请选择区域", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = string.Format("DELETE  M_MsgReceiver_T  WHERE Area='{0}' AND StageType='{1}';", cmbArea.Text, stageType);

            List<string> persons = "1".Equals(stageType) ? lsbStage1.Items.Cast<string>().ToList() : "2".Equals(stageType) ? lsbStage2.Items.Cast<string>().ToList() : null;

            if (persons != null && persons.Count > 0)
            {
                List<string> rowData = new List<string>();
                //取工号
                for (int i = 0; i < persons.Count; i++)
                {
                    string data = string.Format("('{0}','{1}','{2}')", stageType, persons[i].ToString().Split('-')[0], cmbArea.Text.Trim());
                    rowData.Add(data);
                }
                sql += "INSERT INTO M_MsgReceiver_T(StageType,WorkCode,Area) VALUES  " + string.Join(",", rowData);
            }
            DBUtil.ExecSQL(sql);
            RefreshReceiverData();
        }


        //刷新接收人数据
        private void RefreshReceiverData()
        {
            string sql = string.Format("SELECT StageType,r.WorkCode+'-'+c.RealName Person FROM M_MsgReceiver_T r LEFT JOIN S_ContactPerson_T c on r.WorkCode=c.WorkCode WHERE r.Area='{0}' ", cmbArea.Text);
            lsbStage1.Items.Clear();
            lsbStage2.Items.Clear();
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
            {
                string[] stage1 = dt.AsEnumerable().Where(t => t.Field<string>("StageType") == "1").Select(t => t.Field<string>("Person")).ToArray();
                string[] stage2 = dt.AsEnumerable().Where(t => t.Field<string>("StageType") == "2").Select(t => t.Field<string>("Person")).ToArray();
                lsbStage1.Items.AddRange(stage1);
                lsbStage2.Items.AddRange(stage2);
            }
        }

        //清除勾选
        private void btnCancelChecked_Click(object sender, EventArgs e)
        {
            foreach (var item in chklsbContactPerson.CheckedIndices)
            {
                chklsbContactPerson.SetItemChecked((int)item, false);
            }
        }

        private void tbKeyWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(13))
            {
                RefreshContactPerson();
            }
        }
       
    }
}
