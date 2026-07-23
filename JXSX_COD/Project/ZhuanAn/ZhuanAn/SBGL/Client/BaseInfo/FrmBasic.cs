using Common;
using Common.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class FrmBasic : Form
    {

        public FrmBasic()
        {

            InitializeComponent();
        }

        private void FrmBaseBasic_Load(object sender, EventArgs e)
        {

        }

        private void tsmiLine_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            FrmLineInfo frm = new FrmLineInfo();
            frm.TopLevel = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Parent = panelContent;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void tsmiContact_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            FrmContactPerson frm = new FrmContactPerson();
            frm.TopLevel = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Parent = panelContent;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void tsmiFileManage_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            FrmFileManage frm = new FrmFileManage();
            frm.TopLevel = false;
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frm.Parent = panelContent;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }
    }
}
