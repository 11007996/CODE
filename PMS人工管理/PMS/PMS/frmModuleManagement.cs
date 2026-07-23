using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PMS
{
    public partial class frmModuleManagement : Form
    {
        int statusID;  //1.新增   2.修改
        public frmModuleManagement()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (statusID == 1)   //新增保存
            {
            string sqlstr = "";
            checkData();
            //加菜单节点
            if (txtChildMenuName.Text == "" || txtChildMenuID.Text == "")
            {
                label7.Text = "子菜单名称及编号不能为空";
            }
            else
            {
                sqlstr = "select top 1 1 from tb_sysModule where menuID='" + txtChildMenuID.Text.Trim() + "'";
                using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        label7.Text = "已存在此节点，不能增加";
                    }
                    else
                    {
                        sqlstr = "insert into [dbo].[tb_sysModule](menuID,menuName,parentID,controlName,createTime,createUser) values('" + txtChildMenuID.Text.Trim() + "','" + txtChildMenuName.Text.Trim() + "','" + txtMenuID.Text.Trim() + "','" + txtFormName.Text.Trim() + "','"+ sysInformation.sysLogTime + "','" + sysInformation.sysUserid + "')";
                        sqlhelpdb.ExecuteNonQuery(sqlstr);
                        frmModuleManagement_Load(this, null);
                    }
                }
            }
            }
            else if (statusID == 2)   //修改保存
            {
                if (txtChildMenuName.Text.Trim() != null)
                {
                    string sqlstr = "update tb_sysModule set menuName = N'" + txtChildMenuName.Text.Trim() + "',controlName='" + txtFormName.Text.Trim() + "'where menuID ='" + txtChildMenuID.Text.Trim() + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr);
                    this.frmModuleManagement_Load(this, null);
                    label7.Text = "修改完成";
                }
            }
        }


        private int checkData()
        {
            if (txtMenuID.Text == "")
            {
                label7.Text = "菜单编号不能为空";
                return 0;
            }
            else if (txtMenuName.Text == "")
            {
                label7.Text = "菜单名称不能为空";
                return 0;
            }
            return 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sqlstr = "";
            string sqladd ="";
            int depInt = 1;
            string nodeid = "";
            string Nodetext = treeView1.SelectedNode.Text.ToString();
            int nodeDep = treeView1.SelectedNode.Level + 1;
            if (treeView1.SelectedNode != null)
            {
                //所选节点的编号及名称
                sqlstr = "select menuID,menuName from tb_sysModule where menuName = N'" + Nodetext + "'";
                using(DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        txtMenuID.Text = dt.Rows[0][0].ToString();
                        txtMenuName.Text = dt.Rows[0][1].ToString();
                        txtMenuName.Enabled = false;
                        txtMenuID.Enabled = false;
                    }
                }
                
                    //通过节点层级生成ID的位数
                    for(int i = 0; i <nodeDep + 2; i ++)
                    {
                        depInt = depInt * 10;
                    }
                   //获取父节点下的最大节点
                    sqladd = "select isnull(max(menuID),'') from tb_sysModule where parentID='" + txtMenuID.Text.ToString() + "'";
                    using(DataSet ds1 = sqlhelpdb.ExecuteDataset(sqladd))
                    {
                        DataTable dt1 = ds1.Tables[0];
                        string q = dt1.Rows[0][0].ToString();
                        if (dt1.Rows.Count > 0 && dt1.Rows[0][0].ToString() != "")
                        {
                            //获取下一节点ID
                            int Curid = int.Parse(dt1.Rows[0][0].ToString().ToString().Substring(dt1.Rows[0][0].ToString().ToString().Length - 2).TrimStart('0')) + 1 + 100;
                            //int id = depInt + Curid;
                            nodeid = txtMenuID.Text.ToString() + Curid.ToString().Substring(Curid.ToString().Length - 2, 2);
                            txtChildMenuID.Text = nodeid;
                        }
                        else
                        {
                            //生成新的父节点下一子节点ID
                            int id = 100 + 1;
                            nodeid = txtMenuID.Text.ToString() + id.ToString().Substring(1);
                            txtChildMenuID.Text = nodeid;
                        }
                        statusID = 1;
                        txtChildMenuID.Enabled = false;
                        txtChildMenuName.Enabled = true;
                    }                    
                }
        }

        private void frmModuleManagement_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            //找到根节点，从根节点开始
            string sqlstr = " select menuID,menuName,parentID from tb_sysModule where menuName=N'系统结构'";
            using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    nodeload(treeView1.Nodes.Add(dt.Rows[0][1].ToString()), dt.Rows[0][0].ToString());
                }
                treeView1.ExpandAll();
                txtFormName.Clear();
                txtFormName.Enabled = true;

            }
        }
        private void nodeload(TreeNode nodeName,string nodeParent)
        {
            //找到根节点下的子节点
            string sqlstr = "select menuID,menuName,parentID from tb_sysModule where parentID='" + nodeParent + "'";
            DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //递归生成节点
                        nodeload(nodeName.Nodes.Add(dt.Rows[i][1].ToString()), dt.Rows[i][0].ToString());
                    }
                }
            
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                try
                {
                    string nodeText = treeView1.SelectedNode.Text.ToString();
                    string sqlstr = "delete from tb_sysModule where menuName = N'" + nodeText + " '";
                    sqlhelpdb.ExecuteNonQuery(sqlstr);
                    frmModuleManagement_Load(this, null);
                    label7.Text = "已删除";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            
        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.Level != 0)
                    {
                        string strparent = treeView1.SelectedNode.Parent.Text.ToString();
                        string strselect = treeView1.SelectedNode.Text.ToString();
                        string sqlstr = " select menuID,menuName,controlName from [tb_sysModule] where menuName =N'" + strparent + "'union select menuID,menuName,controlName from tb_sysModule where menuName =N'" + strselect + "'";

                        using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                        {
                            DataTable dt = ds.Tables[0];
                            txtChildMenuID.Text = dt.Rows[1][0].ToString();
                            txtChildMenuName.Text = dt.Rows[1][1].ToString();
                            txtFormName.Text = dt.Rows[1][2].ToString();
                            txtMenuID.Text = dt.Rows[0][0].ToString();
                            txtMenuName.Text = dt.Rows[0][1].ToString();

                        }
                        txtMenuID.Enabled = false;
                        txtMenuName.Enabled = false;
                        txtChildMenuName.Enabled = false;
                        txtChildMenuID.Enabled = false;
                        txtFormName.Enabled = false;
                    }
                    else
                    {
                        string strselect = treeView1.SelectedNode.Text.ToString();
                        string sqlstr = "select menuID,menuName,controlName from tb_sysModule where menuName =N'" + strselect + "'";
                        using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                        {
                            DataTable dt = ds.Tables[0];
                            txtChildMenuID.Text = dt.Rows[0][0].ToString();
                            txtChildMenuName.Text = dt.Rows[0][1].ToString();
                            txtFormName.Text = dt.Rows[0][2].ToString();
                        }
                        txtMenuID.Enabled = false;
                        txtMenuName.Enabled = false;
                        txtChildMenuName.Enabled = false;
                        txtChildMenuID.Enabled = false;
                        txtFormName.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModfiy_Click(object sender, EventArgs e)
        {
            txtChildMenuName.Enabled = true;
            txtFormName.Enabled = true;
            statusID = 2;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtMenuID.Clear();
            txtMenuName.Clear();
            txtChildMenuID.Clear();
            txtChildMenuName.Clear();
            txtFormName.Clear();
        }
    }
}
