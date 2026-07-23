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
    public partial class FrmMsgPushSetForTarger : Form
    {
        public FrmMsgPushSetForTarger()
        {
            InitializeComponent();
        }

        private void FrmMsgPushSetForTarger_Load(object sender, EventArgs e)
        {
            string sql = "SELECT DISTINCT Dept From S_User_T";
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
            {
                cmbDept.DataSource = dt.AsEnumerable().Select(t => t.Field<string>("Dept")).ToList();
            }
            RefreshContactPerson();
        }


        private void RefreshContactPerson()
        {
            btnCancelChecked_Click(null, null);
            string sql = "SELECT WorkCode+'-'+RealName Contact FROM S_ContactPerson_T WHERE 1=1 ";
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
        private void btnStage3Add_Click(object sender, EventArgs e)
        {
            if (chklsbContactPerson.CheckedItems.Count <= 0)
            {
                MessageBox.Show("请勾选要增加的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in chklsbContactPerson.CheckedItems)
            {
                string person = item.ToString();
                lsbStage3.Items.Remove(person);
                lsbStage3.Items.Add(person);
            }
            UpdateReceiver("3");
        }

        //删除呼叫支援消息接收人
        private void btnStage3Del_Click(object sender, EventArgs e)
        {
            if (lsbStage3.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选中要删除的联系人", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = lsbStage3.SelectedIndices.Count - 1; i >= 0; i--)
            {
                lsbStage3.Items.RemoveAt(lsbStage3.SelectedIndices[i]);
            }
            UpdateReceiver("3");
        }
        #endregion


        //区域变化事件
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReceiverData();
        }


        private void UpdateReceiver(string stageType)
        {
            if (string.IsNullOrWhiteSpace(cmbDept.Text))
            {
                MessageBox.Show("请选择部门", "操作错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sql = string.Format("DELETE  M_MsgReceiver_T  WHERE Dept='{0}' AND StageType='{1}';", cmbDept.Text, stageType);

            List<string> persons = "3".Equals(stageType) ? lsbStage3.Items.Cast<string>().ToList() : null;

            if (persons != null && persons.Count > 0)
            {
                List<string> rowData = new List<string>();
                //取工号
                for (int i = 0; i < persons.Count; i++)
                {
                    string data = string.Format("('{0}','{1}',N'{2}')",  stageType, persons[i].ToString().Split('-')[0],cmbDept.Text.Trim());
                    rowData.Add(data);
                }
                sql += "INSERT INTO M_MsgReceiver_T(StageType,WorkCode,Dept) VALUES " + string.Join(",", rowData);
            }
            DBUtil.ExecSQL(sql);
            RefreshReceiverData();
        }


        //刷新接收人数据
        private void RefreshReceiverData()
        {
            string sql = string.Format("SELECT StageType,r.WorkCode+'-'+c.RealName Person FROM M_MsgReceiver_T r LEFT JOIN S_ContactPerson_T c on r.WorkCode=c.WorkCode WHERE r.Dept='{0}' ", cmbDept.Text);
            lsbStage3.Items.Clear();
            DataTable dt = DBUtil.GetDataTable(sql);
            if (dt != null)
            {
                string[] stage1 = dt.AsEnumerable().Where(t => t.Field<string>("StageType") == "3").Select(t => t.Field<string>("Person")).ToArray();
                lsbStage3.Items.AddRange(stage1);
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
