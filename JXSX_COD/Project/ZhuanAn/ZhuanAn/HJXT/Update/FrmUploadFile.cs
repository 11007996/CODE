using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Update.Utils;

namespace Update
{
    public partial class FrmUploadFile : Form
    {

        private string opreateFlag = "默认";//操作标记（默认，添加，修改）

        public FrmUploadFile()
        {
            InitializeComponent();
        }

        #region 查询
        private void tsmiSelect_Click(object sender, EventArgs e)
        {
            opreateFlag = "默认";
            RefreshControlsState();
        }
        #endregion

        #region 刷新
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            RefreshFileInfoData();
            labMessage.ForeColor = Color.Green;
            labMessage.Text = "刷新成功";
        }
        #endregion

        #region 新增
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            opreateFlag = "新增";
            RefreshControlsState();
        }
        #endregion

        #region 修改
        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            labMessage.ForeColor = Color.Red;
            labMessage.Text = "";
            if (string.IsNullOrWhiteSpace(labFileId.Text))
            {
                labMessage.Text = "请选择一条数据";
                return;
            }
            opreateFlag = "修改";
            RefreshControlsState();
        }
        #endregion

        #region 保存
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string fileId = labFileId.Text;
            string programVersion = tbProgramVersion.Text;
            string updateStatus = chkUpdateStatus.Checked ? "Y" : "N";
            string updateMode = cmbUpdateMode.Text == "强制" ? "0" : "1";
            string remark = tbRemark.Text;
            string useFlag = chkUseFlag.Checked ? "Y" : "N";

            if ("新增".Equals(opreateFlag))
            {
                string filePath = tbUploadFilePath.Text;
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
                {
                    MessageBox.Show("请选择要上传的文件", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    FileInfo fi = new FileInfo(filePath);
                    if (fi.Exists)
                    {
                        string fileName = fi.Name;
                        long fileSize = fi.Length;
                        System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
                        string fileVersion = info.FileVersion;
                        string type = fi.Extension;
                        //DateTime fileTime = fi.CreationTime;

                        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        //文件流的长度，用字节表示 
                        byte[] file = br.ReadBytes((int)fs.Length);
                        string sql = string.Format(@"UPDATE S_SystemFile_T SET UseFlag='N' WHERE FileName=N'{0}';
                            INSERT INTO S_SystemFile_T (FileName,FileSize,FileVersion,FileType,FileTime,ProgramVersion,UpdateStatus,UpdateMode,Remark,UseFlag,UpdateUser,UpdateTime,FileContent)
                            VALUES(N'{0}',{1},'{2}','{3}',GETDATE(),'{4}','{5}','{6}','{7}',N'{8}','{9}',GETDATE(),@fileContent)",
                            fileName, fileSize, fileVersion, type, programVersion, updateStatus, updateMode, remark, useFlag, GlobalData.UserName);

                        SqlConnection con = DBUtil.CallCon();
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@fileContent", SqlDbType.VarBinary);
                        cmd.Parameters["@fileContent"].Value = file;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if ("修改".Equals(opreateFlag))
            {
                if (string.IsNullOrWhiteSpace(fileId))
                {
                    MessageBox.Show("请选择要修改的数据", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string sql = string.Format(@"UPDATE S_SystemFile_T SET UseFlag='N'  WHERE FileName=N'{7}';
                        UPDATE S_SystemFile_T SET ProgramVersion='{1}',UpdateStatus='{2}',UpdateMode='{3}',Remark=N'{4}',UseFlag='{5}',UpdateUser='{6}',UpdateTime=GETDATE()  WHERE FileId={0}",
                        fileId, programVersion, updateStatus, updateMode, remark, useFlag, GlobalData.UserName, labFileName.Text);
                    DBUtil.ExecSQL(sql);
                }
            }
            opreateFlag = "默认";
            RefreshControlsState();
            RefreshFileInfoData();
            labMessage.Text = "保存成功";
        }
        #endregion

        #region  删除
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            labMessage.ForeColor = Color.Red;
            labMessage.Text = "";
            if (string.IsNullOrWhiteSpace(labFileId.Text))
            {
                labMessage.Text = "请选择一条数据";
                return;
            }
            if (MessageBox.Show("确定要删除吗?", "删除文件", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = string.Format("Delete S_SystemFile_T WHERE FileId={0}", labFileId.Text);
                DBUtil.ExecSQL(sql);
                labMessage.ForeColor = Color.Green;
                labMessage.Text = "删除成功";
            }
            RefreshFileInfoData();
        }
        #endregion

        #region 返回
        private void tsmiBack_Click(object sender, EventArgs e)
        {
            opreateFlag = "默认";
            RefreshControlsState();
        }
        #endregion

        #region 上传
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //获取文件对话框中选定的文件名的字符串，包括文件路径 
                    string filePath = ofd.FileName.ToString();
                    string SelectedFileName = ofd.SafeFileName;
                    if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(SelectedFileName))
                    {
                        return;
                    }

                    FileInfo fi = new FileInfo(filePath);
                    if (fi.Exists)
                    {

                        tbUploadFilePath.Text = filePath;

                        labFileName.Text = fi.Name;
                        labFileSize.Text = fi.Length.ToString();
                        System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(filePath);
                        labFileVersion.Text = info.FileVersion;
                        labFileType.Text = fi.Extension;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "异常信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region  刷新数据
        private void RefreshFileInfoData()
        {
            string fileName = tbFileName.Text;
            string useFlag = chkUseFlag.Checked ? "Y" : "";
            string sql = string.Format("SELECT FileId,FileName,FileSize,FileVersion,FileType,FileTime,ProgramVersion,UpdateStatus,Case UpdateMode WHEN '0' THEN '强制' WHEN '1' THEN '可选' END UpdateMode,Remark,UseFlag,UpdateUser,UpdateTime FROM S_SystemFile_T WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(tbFileName.Text))
            {
                sql += " AND FileName=N'" + tbFileName.Text.Trim() + "'";
            }
            if (!string.IsNullOrWhiteSpace(useFlag))
            {
                sql += " AND UseFlag='" + useFlag + "'";
            }
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvFileInfo.DataSource = dt;
        }
        #endregion

        #region 单元格点击
        private void dgvFileInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int rowIndex = dgvFileInfo.CurrentRow.Index;//选中行


                tbFileName.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileName"].Value.ToString();
                tbProgramVersion.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcProgramVersion"].Value.ToString();
                tbRemark.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcRemark"].Value.ToString();
                chkUseFlag.Checked = dgvFileInfo.Rows[rowIndex].Cells["dgcUseFlag"].Value.ToString() == "Y" ? true : false;
                chkUpdateStatus.Checked = dgvFileInfo.Rows[rowIndex].Cells["dgcUpdateStatus"].Value.ToString() == "Y" ? true : false;
                cmbUpdateMode.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcUpdateMode"].Value.ToString();

                labFileId.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileId"].Value.ToString();
                labFileName.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileName"].Value.ToString();
                labFileSize.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileSize"].Value.ToString();
                labFileVersion.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileVersion"].Value.ToString();
                labFileType.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcFileType"].Value.ToString();
                labUpdateUser.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcUpdateUser"].Value.ToString();
                labUpdateTime.Text = dgvFileInfo.Rows[rowIndex].Cells["dgcUpdateTime"].Value.ToString();

            }
        }
        #endregion

        #region 刷新控件状态
        private void RefreshControlsState()
        {
            switch (opreateFlag)
            {
                case "默认":
                    tsmiSelect.Enabled = true;
                    tsmiRefresh.Enabled = true;
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiDelete.Enabled = true;
                    tsmiBack.Enabled = false;
                    tsmiSave.Enabled = false;

                    tbFileName.Enabled = true;
                    btnUpload.Enabled = false;
                    dgvFileInfo.Enabled = true;
                    ClearControlText();
                    break;
                case "新增":
                    tsmiSelect.Enabled = false;
                    tsmiRefresh.Enabled = false;
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;
                    tsmiSave.Enabled = true;

                    tbFileName.Enabled = false;
                    btnUpload.Enabled = true;
                    dgvFileInfo.Enabled = false;
                    ClearControlText();
                    break;
                case "修改":
                    tsmiSelect.Enabled = false;
                    tsmiRefresh.Enabled = false;
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;
                    tsmiSave.Enabled = true;

                    tbFileName.Enabled = false;
                    btnUpload.Enabled = false;
                    dgvFileInfo.Enabled = false;
                    break;
            }
        }
        #endregion

        #region 清空控件值
        private void ClearControlText()
        {
            foreach (Control c in gbFileInfo.Controls)
            {
                if (c is TextBox || c is ComboBox)
                    c.Text = "";
                if (c is CheckBox)
                    ((CheckBox)c).Checked = false;
            }
            labFileId.Text = "";
            labFileName.Text = "";
            labFileSize.Text = "";
            labFileVersion.Text = "";
            labFileType.Text = "";
            labUpdateUser.Text = "";
            labUpdateTime.Text = "";
        }
        #endregion

        private void FrmUploadFile_Load(object sender, EventArgs e)
        {
            dgvFileInfo.DefaultCellStyle.DataSourceNullValue = "";
            opreateFlag = "默认";
            RefreshControlsState();
        }


    }
}
