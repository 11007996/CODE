using Common;
using Common.Utils;
using DevComponents.DotNetBar.Controls;
using System;
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
    public partial class FrmLineInfo : Form
    {
        //编辑前的数据
        public FrmLineInfo()
        {
            InitializeComponent();
        }

        private void FrmLineInfo_Load(object sender, EventArgs e)
        {
            RefreshLineInfo();
        }

        private void RefreshLineInfo()
        {
            dgvLineInfo.DataSource = DBUtil.GetDataTable("SELECT * FROM M_LineInfo_T Order By Area ASC");
        }


        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            //数据检查
            if (string.IsNullOrWhiteSpace(tbArea.Text))
            {
                labMessage.Text = "区域不能为空";
                return;
            }
            if (string.IsNullOrWhiteSpace(tbLine.Text))
            {
                labMessage.Text = "线别不能为空";
                return;
            }
            try
            {
                string sql = string.Format("SELECT 1 FROM M_LineInfo_T WHERE Factory=N'{0}' AND Area=N'{1}' AND Line=N'{2}'", BaseInfo.Factory, tbArea.Text.Trim(), tbLine.Text.Trim());
                DataTable dt = DBUtil.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    labMessage.Text = "已存在相同数据";
                    return;
                }
                //新增
                sql = string.Format("INSERT INTO M_LineInfo_T(Factory,Area,Line) VALUES(N'{0}',N'{1}',N'{2}')", BaseInfo.Factory, tbArea.Text.Trim(), tbLine.Text.Trim());
                DBUtil.ExecSQL(sql);
                RefreshLineInfo();

                labMessage.ForeColor = Color.Green;
                labMessage.Text = "【提示】操作成功";
            }
            catch (Exception)
            {
                labMessage.Text = "【警告】操作失败";
            }
        }

        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            if (dgvLineInfo.SelectedRows.Count != 1)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "请选中一行数据进行删除。";
                return;
            }
            string area = dgvLineInfo.CurrentRow.Cells[1].Value.ToString();
            string line = dgvLineInfo.CurrentRow.Cells[2].Value.ToString();
            if (MessageBox.Show("确定要删除" + area + "区 > " + line + "吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sqlEdit = string.Format("DELETE M_LineInfo_T  WHERE Factory=N'{0}' AND Area=N'{1}' AND Line=N'{2}'", BaseInfo.Factory, area, line);
                int execCount = DBUtil.ExecSQL(sqlEdit);
                //显示操作结果
                if (execCount > 0)
                {
                    RefreshLineInfo();
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "【提示】删除成功";
                }
                else
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "【警告】删除失败";
                }
            }

        }
    }
}
