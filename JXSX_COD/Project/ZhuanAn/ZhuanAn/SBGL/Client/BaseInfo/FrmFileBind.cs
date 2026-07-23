using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class FrmFileBind : Form
    {
        private int FileId;
        public FrmFileBind()
        {
            InitializeComponent();
        }

        public FrmFileBind(int fileId)
        {
            InitializeComponent();
            FileId = fileId;
        }

        private void FrmFileBind_Load(object sender, EventArgs e)
        {
            dgvFileBind.AutoGenerateColumns = false;
            //初始按件数据
            txbFileId.Text = FileId.ToString();

            string sql = $@"SELECT f.FileAliasName,a.AssetNo,a.AssetName FROM A_AssetFile_T af 
                                LEFT JOIN S_FileInfo f ON af.FileId=f.FileId 
                                LEFT JOIN A_AssetInfo_T a ON af.AssetNo=a.AssetNo 
                            WHERE af.FileId='{FileId}' ;";
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvFileBind.DataSource = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                txbFileAliasName.Text = Convert.ToString(dt.Rows[0]["FileAliasName"]);
                labCount.Text = dt.Rows.Count.ToString();
            }
        }
    }
}
