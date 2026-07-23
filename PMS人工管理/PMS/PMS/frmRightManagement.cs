using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
namespace PMS
{
    public partial class frmRightManagement : Form
    {
        public frmRightManagement()
        {
            InitializeComponent();
        }
        //查询加载TreeView节点
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList ALList = new ArrayList();
                treeView1.Nodes.Clear();
                //加载TreeView
                if (txtUserID.Text.Trim() == "" && txtUserName.Text.Trim() == "")
                {
                    return;
                }
                StringBuilder sqlstr = new StringBuilder();
                //设置查询条件
                sqlstr.Append("select UserID,UserName from tb_User where 1 =1");
                if (txtUserName.Text.Trim() != "")
                {
                    sqlstr.Append("and UserName=N'" + txtUserName.Text.Trim() + "'");
                }
                if (txtUserID.Text.Trim() != "")
                {
                    sqlstr.Append("and UserID='" + txtUserID.Text.Trim() + "'");
                }
                string sqlstr1 = sqlstr.ToString();
                using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr1))
                {
                    DataTable dt = ds.Tables[0];
                    txtUserID.Text = dt.Rows[0][0].ToString().Trim();
                    txtUserName.Text = dt.Rows[0][1].ToString().Trim();
                    if (dt.Rows.Count >= 1)
                    {
                        groupBox1.Text = dt.Rows[0]["UserName"].ToString() + "权限";
                        //创建根节点
                        nodeload(treeView1.Nodes.Add("系统结构"), "m101");
                    }
                }


                //确定权限列表
                string sqlstr2 = "select menuName from [tb_UserRight] where UserID='" + txtUserID.Text.Trim() + "'";
                using (DataSet ds2 = sqlhelpdb.ExecuteDataset(sqlstr2))
                {
                    DataTable dt2 = ds2.Tables[0];
                    if (dt2.Rows.Count < 0)
                    {
                        return;
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        ALList.Add(dt2.Rows[i][0].ToString());
                    }
                    //勾选权限
                    checkRight(treeView1.Nodes, ALList);
                }
                //展开全部节点
                treeView1.ExpandAll();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void checkRight(TreeNodeCollection tn, ArrayList dtList)
        {
            try
            {
                for (int i = 0; i < dtList.Count;i++)
                {
                    for (int j = 0; j < tn.Count; j++)
                    {
                        //权限与TreeView节点文本相同则此节点被勾选
                        if (tn[j].Text.Trim().ToString() == dtList[i].ToString())
                        {
                            tn[j].Checked = true;
                            //勾选父节点
                            ParentNode(tn[j]);
                        }
                        //递归勾选节点
                        checkRight(tn[j].Nodes, dtList);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ParentNode(TreeNode t)
        {
            try
            {
                //递归勾选父节点
                t.Parent.Checked = true;
                ParentNode(t.Parent);
            }
            catch
            {

            }
            
        }
        private void nodeload(TreeNode nodeName, string nodeParent)
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

        private void addParent(TreeNode pNode,string uID,string uName)
        {
            string parentID = "";
            string patentName = "";
            //获取节点的父节点
            string sqlstr = "select menuID,menuName from [tb_sysModule] where menuID = (select parentID from [tb_sysModule] where menuName=N'" + pNode.Text.Trim() + "')";
            //确定父节点的是否已有权限
            string sqlstr2 = "select top 1 1 from [tb_UserRight] where menuName='" + pNode.Parent.Text.Trim() + "'and userId='" + uID + "'";
            try
            {
                //如果勾选的是根节点，则其没有父节点，不作操作
                if (pNode == treeView1.TopNode)
                {
                    //do nothing
                }
                else
                {
                    using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                    {
                        parentID = ds.Tables[0].Rows[0][0].ToString();
                        patentName = ds.Tables[0].Rows[0][1].ToString();
                    }
                    using (DataSet ds2 = sqlhelpdb.ExecuteDataset(sqlstr2))
                    {
                        //父节点已有权限，不作操作
                        DataTable dt2 = ds2.Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            return;
                        }
                        else
                        {
                            //父节点没有权限，则插入权限表
                            string sqlstr1 = "insert into tb_UserRight(UserID,UserName,menuID,menuName)values('" + uID + "','" + uName + "','" + parentID + "','" + patentName + "')";
                            sqlhelpdb.ExecuteNonQuery(sqlstr1);
                            pNode.Parent.Checked = true;
                            //递归勾选父节点权限
                            addParent(pNode.Parent, uID, uName);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void addChildChecked(TreeNode pNode)
        {
            foreach(TreeNode childChecked in pNode.Nodes)
            {
                childChecked.Checked = true;
                //递归勾选子节点
                addChildChecked(childChecked);
            }
        }
        private void addChild(TreeNode pNode, string uID, string uName)
        {
            try
            {
                foreach (TreeNode cNode in pNode.Nodes)
                {
                    //获取子节点中还未被赋权限的节点
                    string sqlstr = "select menuID,menuName from [tb_sysModule] where parentID=(select menuID from [tb_sysModule] where menuName=N'" + pNode.Text.Trim() + "') except select menuID,menuName from tb_UserRight where UserID='" + uID + "'";
                    using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //将未赋权限的节点插入权限表
                                string sqlstr1 = "insert into tb_UserRight(UserID,UserName,menuID,menuName)values('" + uID + "','" + uName + "','" + dt.Rows[i][0].ToString() + "','" + dt.Rows[i][1].ToString() + "')";
                                sqlhelpdb.ExecuteNonQuery(sqlstr1);
                            }
                        }
                    }
                    //递归插入权限表
                    addChild(cNode,uID,uName);
                }
                //勾选子节点
                addChildChecked(pNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delChildChecked(TreeNode pNode)
        {
            foreach (TreeNode childChecked in pNode.Nodes)
            {
                childChecked.Checked = false;
                //取消勾选子节点的子节点
                delChildChecked(childChecked);
            }
        }

        private void delChild(TreeNode pNode,string uID)
        {
            ArrayList nodes = new ArrayList();
            try
            {
                foreach (TreeNode nextNode in pNode.Nodes)
                {
                    //获取勾选节点的子节点
                    string sqlstr = "select a.menuID from [tb_sysModule] a join tb_UserRight b on a.menuID=b.menuID where a.parentID= (select menuID from [tb_sysModule] where menuName=N'" + pNode.Text.Trim() + "') and b.UserID='" + uID + "'";
                    using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                nodes.Add(dt.Rows[i][0].ToString());
                            }
                        }
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            string sqlstr1 = "delete from tb_UserRight where menuID='" + nodes[i] + "' and UserID='" + uID + "'";
                            sqlhelpdb.ExecuteNonQuery(sqlstr1);
                            //delChild(pNode.Nodes[0],uId);
                        }
                    }
                    //递归删除子节点的子节点
                    delChild(nextNode, uID);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //查询勾选节点也会触发此事件，此语表示通过鼠标勾选才执行操作
            if (e.Action != TreeViewAction.ByMouse)
            {
                return;
            }
            string menuID = "";
            string menuName = "";
            string userId = txtUserID.Text.Trim();
            string useName = txtUserName.Text.Trim();
            //获取勾选的节点的文本
            string NodeName = e.Node.Text.Trim().ToString();
            //通过勾选的文本获取节点ID
            string sqlstr = "select menuID,menuName from tb_sysModule where menuName=N'" + NodeName + "'";
            //确定是否已勾选此权限
            string sqlstr2 = "select top 1 1 from [tb_UserRight] where menuName='" + NodeName + "'and userId='" + userId + "'";
            try
            {
                using (DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
                {
                    menuID = ds.Tables[0].Rows[0][0].ToString();
                    menuName = ds.Tables[0].Rows[0][1].ToString();
                }
                //触发事件后节点被勾选
                if (e.Node.Checked == true)
                {
                    using (DataSet ds2 = sqlhelpdb.ExecuteDataset(sqlstr2))
                    {
                        //如果节点已有权限，则不重复插入权限表
                        DataTable dt2 = ds2.Tables[0];
                        if (dt2.Rows.Count > 0)
                        {
                            return;
                        }
                    }
                    //节点没有权限则插入权限表
                    string sqlstr1 = "insert into tb_UserRight(UserID,UserName,menuID,menuName)values('" + userId + "','" + useName + "','" + menuID + "','" + menuName + "')";
                    sqlhelpdb.ExecuteNonQuery(sqlstr1);
                    if(e.Node != treeView1.TopNode)
                    {
                        //勾选权限的父节点也要同时勾选，递归操作
                        addParent(e.Node, userId, useName);
                    }
                    //节点的子节点同时也要勾选权限
                    addChild(e.Node, userId, useName);
                }
                else
                {
                    //删除勾选的节点权限
                    string sqlstr1 = "delete from tb_UserRight where menuName=N'" + menuName + "' and UserID=N'" + userId + "'";
                    sqlhelpdb.ExecuteNonQuery(sqlstr1);
                    //同时要删除勾选的节点的子节点权限
                    delChild(e.Node, userId);
                    //取消勾选子节点
                    delChildChecked(e.Node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
