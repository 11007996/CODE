using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PMS
{
    
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
        }
        private delegate void closeWin();
        private void winC()
        {
            frmLogin f1;
            f1 = (frmLogin)this.Owner;
            f1.Dispose();
        }
        private void TSMenuItem5_Click(object sender, EventArgs e)
        {
            closeWin cw = new closeWin(winC);
            cw();
            this.Dispose();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            StatusLabel1.Text = "登录用户：" + sysInformation.sysUserid;
            StatusLabel2.Text = "登录时间：" + sysInformation.sysLogTime;
            this.Text = "人事工资管理系统(用户IP:" + sysInformation.sysLogIP + ")";
            string sqlstr = "select b.menuName from tb_sysModule a join tb_UserRight b on a.menuID=b.menuID where b.UserID='" + sysInformation.sysUserid + "'";
            using(DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr))
            {
                DataTable dt = ds.Tables[0];
            for (int i =0;i<this.menuStrip1.Items.Count;i++)
            {
               bool Y0rN = chkWinRight(dt,this.menuStrip1.Items[i]);
               if (!Y0rN)
                {
                    this.menuStrip1.Items[i].Visible = false;
                }
            }

                //for (int i =0;i<this.menuStrip1.MdiWindowListItem.DropDownItems.Count;i++)
                //{
                //    //this.menuStrip1.MdiWindowListItem.DropDownItems[0].Visible
                //   bool Y0rN = chkWinRight(dt,this.menuStrip1.MdiWindowListItem.DropDownItems[i]);
                //   if (!Y0rN)
                //    {
                //        this.menuStrip1.MdiWindowListItem.DropDownItems[i].Visible = false;
                //    }
                //}
            }

        }

        //根据权限显示不同菜单栏
        public bool chkWinRight(DataTable dt,ToolStripItem ti)
        {
            
            //toolStripMenuItem1.DropDownItems
            string ToolName = ti.Text.ToString();
            if (dt.Select("menuName='" + ToolName + "'").Length >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void userManagement_Click(object sender, EventArgs e)
        {
            frmUserManagement frm = new frmUserManagement();
            frm.ShowDialog();
        }

        private void ToolManagement_Click(object sender, EventArgs e)
        {
            frmModuleManagement frm = new frmModuleManagement();
            frm.ShowDialog();
        }

        private void OperatorChange_Click(object sender, EventArgs e)
        {
            frmOperatorChange frm = new frmOperatorChange();
            frm.ShowDialog();
        }

        private void depManagement_Click(object sender, EventArgs e)
        {
            frmDepManagement frm = new frmDepManagement();
            frm.ShowDialog();
        }

        private void rightManagement_Click(object sender, EventArgs e)
        {
            frmRightManagement frm = new frmRightManagement();
            frm.ShowDialog();
        }
        private int showItem(ToolStripMenuItem obj)
        {
            try
            {
                string sqlstr = "select menuName from tb_UserRight where UserID='" + sysInformation.sysUserid + "'";
                DataSet ds = sqlhelpdb.ExecuteDataset(sqlstr);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (dt.Select("menuName='" + obj.Text.Trim() + "'").Length > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 3;
            }
        }

        private void toolUser_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in tm.DropDownItems)
            {
                int YorN = showItem(i);
                if (YorN == 0)
                {
                    i.Visible = false;
                }
                else if (YorN == 1)
                {
                    i.Visible = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void toolHuman_Click(object sender, EventArgs e) 
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in tm.DropDownItems)
            {
                int YorN = showItem(i);
                if (YorN == 0)
                {
                    i.Visible = false;
                }
                else if (YorN == 1)
                {
                    i.Visible = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void toolSalaryManagemeng_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in tm.DropDownItems)
            {
                int YorN = showItem(i);
                if (YorN == 0)
                {
                    i.Visible = false;
                }
                else if (YorN == 1)
                {
                    i.Visible = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void toolSysManagement_Click(object sender, EventArgs e)
        {
            //根据权限显示子菜单
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem i in tm.DropDownItems)
            {
                int YorN = showItem(i);
                if (YorN == 0)
                {
                    i.Visible = false;
                }
                else if (YorN == 1)
                {
                    i.Visible = true;
                }
                else
                {
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] tj = Process.GetProcessesByName("win32calc");
            if(tj.Length > 0)
            {
                return;
            }
            //Process jsq = new Process();
            //jsq.StartInfo.FileName = "calc.exe";
            //jsq.Start();
            Process ps = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("calc.exe");
            ps.StartInfo = psi;
            ps.Start();

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
            //退出进程
            Application.Exit();
        }   

    }
}
