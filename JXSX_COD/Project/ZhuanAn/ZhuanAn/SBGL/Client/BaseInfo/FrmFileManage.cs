using ApiManager.AssetSystem;
using ApiManager.AssetSystem.Base;
using Common;
using Common.Base;
using Common.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic
{
    public partial class FrmFileManage : Form
    {

        public FrmFileManage()
        {
            InitializeComponent();
        }

        private void FrmFileManage_Load(object sender, EventArgs e)
        {
            labMessage.Text = "";
            lbFiles.DisplayMember = "Key";
            lbFiles.ValueMember = "Value";
            dgvFile.AutoGenerateColumns = false;

            //控件数据绑定
            System.Array values = System.Enum.GetValues(typeof(FileClassEnum));
            List<KeyValuePair<int, string>> fileClass = new List<KeyValuePair<int, string>>();
            foreach (var value in values)
            {
                string desc = EnumExtension.ToDescription((FileClassEnum)Enum.Parse(typeof(FileClassEnum), value.ToString()));
                fileClass.Add(new KeyValuePair<int, string>((int)value, desc));
            }
            cbFileClass.DataSource = fileClass;
            cbFileClass.DisplayMember = "Value";
            cbFileClass.ValueMember = "Key";
            cbConditionFileClass.DataSource = fileClass;
            cbConditionFileClass.DisplayMember = "Value";
            cbConditionFileClass.ValueMember = "Key";


            tsmiQuery_Click(null, null);
            btnSelect_Click(null, null);
        }

        #region 文件查询
        private void btnSelect_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            string fileClass = cbFileClass.SelectedValue.ToString();
            string fileName = tbConditionFileName.Text;
            string sql = "SELECT f.*,u.UserName,(Select Count(1) FROM A_AssetFile_T WHERE FileId=f.FileId) BindCount FROM S_FileInfo f Left Join S_User_T u ON f.UpdateUser=u.UserNo WHERE 1=1 ";
            if (!string.IsNullOrEmpty(fileClass) && int.Parse(fileClass) > 0)
            {
                sql += $" AND FileClass='{fileClass}'";
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                sql += $" AND FileAliasName like '%{fileName}%'";
            }
            DataTable dt = DBUtil.GetDataTable(sql);
            dgvFile.DataSource = dt;
        }
        #endregion

        #region 文件上传
        private void btnUpload_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string msg = string.Empty;
            if(cbFileClass.SelectedValue==null || int.Parse(cbFileClass.SelectedValue.ToString())<=0)
            {
                labMessage.Text = "请选择文件分类";
                labMessage.ForeColor = Color.Red;
                return;
            }
            FileClassEnum fileClass = (FileClassEnum)(cbFileClass.SelectedValue);
            List<FileInfo> fileInfos = new List<FileInfo>();
            List<KeyValuePair<string, string>> files = lbFiles.DataSource as List<KeyValuePair<string, string>>;
            if (files == null || files.Count == 0)
            {
                labMessage.Text = "请选择文件";
                labMessage.ForeColor = Color.Red;
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (KeyValuePair<string, string> fileInfo in files)
                {
                    FileInfo fi = new FileInfo(fileInfo.Value);
                    fileInfos.Add(fi);
                }
                if (BatchUploadOrOneReplaceFile(fileInfos, null, fileClass, ref msg))
                        labMessage.ForeColor = Color.Green;
                else
                    labMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                msg = "上传失败,异常：" + ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                labMessage.Text = msg;
                RefreshDataGrid();
            }
        }
        #endregion

        #region 文件替换
        private void btnChange_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            string msg = string.Empty;
            //FileClassEnum fileClass = (FileClassEnum)(cbFileClass.SelectedValue);
            List<FileInfo> fileInfos = new List<FileInfo>();
            List<KeyValuePair<string, string>> files = lbFiles.DataSource as List<KeyValuePair<string, string>>;

            if (lbFiles.Items.Count != 1)
            {
                labMessage.Text = "使用(上传[替换])时只能选择一个文件";
                labMessage.ForeColor = Color.Red;
                return;
            }
            string fileName = lbFiles.GetItemText(lbFiles.Items[0]);
            if (dgvFile.SelectedRows.Count <= 0)
            {
                labMessage.Text = "选择一行数据";
                labMessage.ForeColor = Color.Red;
                return;
            }
            DataRow row = ((DataRowView)dgvFile.SelectedRows[0].DataBoundItem).Row;
            string fileId = row["fileId"].ToString();
            string tip = $"确定在替换这个文件吗？{Environment.NewLine}被替换文件Id:{fileId}{Environment.NewLine}替换文件:{fileName}";
            if (MessageBox.Show(tip, "替换提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (KeyValuePair<string, string> fileInfo in files)
                {
                    FileInfo fi = new FileInfo(fileInfo.Value);
                    fileInfos.Add(fi);
                }
                if (BatchUploadOrOneReplaceFile(fileInfos, fileId, null, ref msg))
                    labMessage.ForeColor = Color.Green;
                else
                    labMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                msg = "上传失败,异常：" + ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                labMessage.Text = msg;
                RefreshDataGrid();
            }
        }
        #endregion

        #region 文件删除
        private void dgvFile_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRow row = ((DataRowView)e.Row.DataBoundItem).Row;
            string fileId = row["FileId"].ToString();
            string fileAlisaName = row["FileAliasName"].ToString();
            string tip = $"确定在删除这个文件吗？{Environment.NewLine}文件Id:{fileId}{Environment.NewLine}文件名:{fileAlisaName}";
            if (MessageBox.Show(tip, "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string sql = $@"DELETE From S_FileInfo WHERE FileId='{fileId}'
                                DELETE From A_AssetFile_T WHERE FileId='{fileId}'";
                DBUtil.ExecSQL(sql);
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 控件交互
        /// <summary>
        /// 上传菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiUpload_Click(object sender, EventArgs e)
        {
            gbSelect.Visible = false;
            gbUpload.Visible = true;
        }


        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            gbSelect.Visible = true;
            gbUpload.Visible = false;
        }

        /// <summary>
        /// 重置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            cbConditionFileClass.Text = string.Empty;
            tbConditionFileName.Text = string.Empty;
        }

        /// <summary>
        /// 文件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            //文件清单,key:文件名，value:文件完整名称
            List<KeyValuePair<string, string>> SelectedFiles = new List<KeyValuePair<string, string>>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文件(*.*)|*.*";
            openFileDialog.Multiselect = true; //是否可以多选true=ok/false=no
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //多个文件
                string[] strNames = openFileDialog.FileNames;
                string tempName;
                for (int i = 0; i < strNames.Length; i++)
                {
                    tempName = Path.GetFileName(strNames[i]);
                    SelectedFiles.Add(new KeyValuePair<string, string>(tempName, strNames[i]));
                }
            }
            labFileCount.Text = SelectedFiles.Count.ToString();
            lbFiles.DataSource = SelectedFiles;
        }

        /// <summary>
        /// 单元格内容单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //预览文件内容
            if (dgvFile.Columns[e.ColumnIndex].Name == "dgcFileAliasName")
            {
                DataRow row = ((DataRowView)dgvFile.Rows[e.RowIndex].DataBoundItem).Row;
                FrmFilePreview frm = new FrmFilePreview(int.Parse(row["FileId"].ToString()));
                frm.ShowDialog();
            }
            //查看文件关联信息
            if (dgvFile.Columns[e.ColumnIndex].Name == "dgcBindCount")
            {
                DataRow row = ((DataRowView)dgvFile.Rows[e.RowIndex].DataBoundItem).Row;
                FrmFileBind frm = new FrmFileBind(int.Parse(row["FileId"].ToString()));
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 上传条件[文件分类]切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFileClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFiles.DataSource = null;
        }


        /// <summary>
        /// 表数据格式化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFile_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //格式化【文件分类】列的值
            if (dgvFile.Columns[e.ColumnIndex].Name == "dgcFileClass")
            {
                if(e.Value!=null)
                {
                    string desc = EnumExtension.ToDescription((FileClassEnum)Enum.Parse(typeof(FileClassEnum), e.Value.ToString()));
                    e.Value = desc;
                }
            }
        }
        #endregion

        #region 方法封装
        /// <summary>
        /// 批量上传或单个替换文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="fileId"></param>
        /// <param name="fileClass"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool BatchUploadOrOneReplaceFile(List<FileInfo> files, string fileId, FileClassEnum? fileClass, ref string msg)
        {
            msg = "";
            if (files.Count <= 0)
            {
                msg = "未选择文件";
                return false;
            }
            int uploadSucessCount = 0;
            foreach (FileInfo fileInfo in files)
            {
                msg += "【" + fileInfo.Name + "】";
                //检查文件是否存在
                if (fileInfo.Exists)
                {
                    //开始上传
                    string tempMsg = "";
                    if (AssetSystemApi.UploadFile(fileInfo.FullName, fileId, fileClass, ref tempMsg))
                    {
                        uploadSucessCount++;
                        msg += "上传成功" + Environment.NewLine;
                    }
                    else
                        msg += "上传失败，原因:" + tempMsg + Environment.NewLine;
                }
                else
                {
                    msg += "上传失败，原因:本地文件不存在";
                }
            }
            msg = $"上传成功{uploadSucessCount}个,失败{files.Count - uploadSucessCount}个" + Environment.NewLine + msg;
            return !(files.Count - uploadSucessCount > 0);
        }
        #endregion


       
    }
}
