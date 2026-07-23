using Common.Utils;
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

namespace CallSys
{
    public partial class FrmMsgPushSet : Form
    {
        public FrmMsgPushSet()
        {
            InitializeComponent();
        }

        private void FrmMsgPushSet_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT Area From M_LineInfo_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
            {
                cmbArea.DataSource = dt.AsEnumerable().Select(t => t.Field<string>("Area")).ToList();
            }
            RefreshContactPerson();
        }


        private void RefreshContactPerson()
        {
            string sql = "SELECT WorkCode+'-'+RealName contact FROM M_ContactPerson_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
                chklsbContactPerson.DataSource = dt.AsEnumerable().Select(t => t.Field<string>("Contact")).ToList();
        }



        #region 通知人维护
        //添加联系人
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbWorkCode.Text))
            {
                MessageBox.Show("工号不能为空", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbRealName.Text))
            {
                MessageBox.Show("姓名不能为空", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string sql = string.Format("SELECT 1 FROM M_ContactPerson_T WHERE WorkCode='{0}'", tbWorkCode.Text.Trim());
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show("已存在此工号", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sql = string.Format("INSERT INTO M_ContactPerson_T VALUES('{0}',N'{1}')", tbWorkCode.Text.Trim(), tbRealName.Text.Trim());
            DBUtil.ExecSQL(sql);
            RefreshContactPerson();
            MessageBox.Show("添加成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //删除联系人
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (chklsbContactPerson.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请勾选要删除的数据", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<string> persons = new List<string>();
            foreach (var item in chklsbContactPerson.CheckedItems)
            {
                persons.Add(item.ToString().Split('-')[0]);
            }
            string strPersons = string.Join(",", persons);
            if (MessageBox.Show("确定要删除【" + strPersons + "】吗?", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                string sql = string.Format("DELETE  M_ContactPerson_T WHERE WorkCode in ('{0}');DELETE M_MsgReceiver_T WHERE WorkCode in('{0}');", string.Join("','", persons));
                DBUtil.ExecSQL(sql);
                RefreshContactPerson();
                MessageBox.Show("删除成功", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        #endregion

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
                    string data = string.Format("('{0}','{1}','{2}')", cmbArea.Text.Trim(), stageType, persons[i].ToString().Split('-')[0]);
                    rowData.Add(data);
                }
                sql += "INSERT INTO M_MsgReceiver_T VALUES " + string.Join(",", rowData);
            }
            DBUtil.ExecSQL(sql);
            RefreshReceiverData();
        }


        //刷新接收人数据
        private void RefreshReceiverData()
        {
            string sql = string.Format("SELECT StageType,r.WorkCode+'-'+c.RealName Person FROM M_MsgReceiver_T r LEFT JOIN M_ContactPerson_T c on r.WorkCode=c.WorkCode WHERE r.Area='{0}' ", cmbArea.Text);
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


    }
}
