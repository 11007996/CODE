using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Util;
using Common.Base;
using Common;
using Call.Base;


namespace Call
{
    public partial class FrmBaseUser : Form
    {
        private OperateStateEnum operateFlag = OperateStateEnum.Defualt;//处理人信息操作状态标记

        public FrmBaseUser()
        {
            InitializeComponent();
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseUser_Load(object sender, EventArgs e)
        {
            //------------初始化处理人信息选项卡组件-----------
            //处理人权限
            IList RightList = new List<DictionaryEntry>();
            RightList.Add(new DictionaryEntry("", ""));
            RightList.Add(new DictionaryEntry("A", "超级管理员"));
            RightList.Add(new DictionaryEntry("B", "一般管理员"));
            cmbUserRight.DataSource = RightList;
            cmbUserRight.ValueMember = "Key";
            cmbUserRight.DisplayMember = "Value";
            //处理人等级
            IList levelList = new List<DictionaryEntry>();
            levelList.Add(new DictionaryEntry("", ""));
            levelList.Add(new DictionaryEntry("1", "工程师"));
            levelList.Add(new DictionaryEntry("2", "高级工程师"));
            cmbUserLevel.DataSource = levelList;
            cmbUserLevel.ValueMember = "Key";
            cmbUserLevel.DisplayMember = "Value";
            //处理人状态
            IList stateList = new List<DictionaryEntry>();
            stateList.Add(new DictionaryEntry("", ""));
            stateList.Add(new DictionaryEntry("W", "等待中"));
            stateList.Add(new DictionaryEntry("H", "对应中"));
            cmbUserState.DataSource = stateList;
            cmbUserState.ValueMember = "Key";
            cmbUserState.DisplayMember = "Value";
            //区域
            string sql = "select DISTINCT Area  FROM S_LineInfo_T ";
            DataTable dtArea = DBUtil.GetDataTable(sql);
            if (dtArea != null)
            {
                foreach (DataRow row in dtArea.Rows)
                {
                    clbArea.Items.Add(row["Area"].ToString(), false);
                }
            }
            clbArea.BringToFront();
            //禁用自动添加列
            dgvUser.AutoGenerateColumns = false;
            //刷新
            RefreshGridData();
            RefreshControlState();
            labMessage.Text = "";
        }
        #endregion


        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Add;
            RefreshControlState();
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            string userNo = dgvUser.CurrentRow.Cells["dgcUserNo"].Value.ToString();
            if (dgvUser.CurrentRow == null || userNo == null || string.IsNullOrWhiteSpace(userNo))
            {
                labMessage.Text = "请至少选择一条数据";
                return;
            }
            operateFlag = OperateStateEnum.Edit;
            RefreshControlState();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "";
                //检查数据
                if (BaseCheck() == false)
                {
                    return;
                }
                //处理数据
                FormUtil.ClearControlsSpace(gbUser);
                string useFlag = chkUseFlag.Checked ? "Y" : "N";
                List<string> areaList = new List<string>();
                for (int i = 0; i < clbArea.CheckedItems.Count; i++)
                {
                    areaList.Add(clbArea.CheckedItems[i].ToString());
                }
                string area = string.Join(",", areaList);
                //新增
                if (operateFlag == OperateStateEnum.Add)
                {
                    strSql = string.Format(@"insert into S_User_T (UserNo,UserName,Dept,UserState,Pwd,UserRight,UserLevel,Area,UseFlag,UpdateUser,UpdateTime)
                                            values('{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}','{8}',N'{9}',GETDATE())",
                                             tbUserNo.Text, tbUserName.Text, cmbDept.Text, cmbUserState.SelectedValue, tbPwd.Text, cmbUserRight.SelectedValue, cmbUserLevel.SelectedValue, area, useFlag, BaseInfo.LoginUserNo);
                    DBUtil.ExecSQL(strSql);
                }
                //修改
                if (operateFlag == OperateStateEnum.Edit)
                {
                    strSql = string.Format("update S_User_T set UserName=N'{1}',Dept=N'{2}',UserState=N'{3}',Pwd=N'{4}',UserRight=N'{5}',UserLevel=N'{6}',Area=N'{7}',UseFlag='{8}',UpdateUser=N'{9}',UpdateTime=GETDATE() where UserNo=N'{0}'",
                        tbUserNo.Text, tbUserName.Text, cmbDept.Text, cmbUserState.SelectedValue, tbPwd.Text, cmbUserRight.SelectedValue, cmbUserLevel.SelectedValue, area, useFlag, BaseInfo.LoginUserNo);
                    DBUtil.ExecSQL(strSql);
                }
                //上传图片
                if (!string.IsNullOrWhiteSpace(tbPicPath.Text) && File.Exists(tbPicPath.Text))
                {
                    FileStream fs = new FileStream(tbPicPath.Text, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    //文件流的长度，用字节表示 
                    byte[] imgBytesIn = br.ReadBytes((int)fs.Length);

                    //更新文件到数据库
                    string sql = string.Format("UPDATE S_User_T SET ImageUpdateTime=GETDATE(), UserImage=@UserImage WHERE UserNo=N'{0}'", tbUserNo.Text);
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@UserImage", imgBytesIn);
                    DBUtil.ExecSQL(sql, param);
                }
                FormUtil.ClearControls(gbUser);
                operateFlag = OperateStateEnum.Defualt;
                RefreshControlState();
                RefreshGridData();
                labMessage.Text = "操作成功";
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSelect_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Query;
            RefreshControlState();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 输入查询条件后刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            //刷新数据
            RefreshGridData();
            labMessage.Text = "数据已加载完成！";
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvUser.CurrentRow.Index;//选中行
                if (rowIndex < 0)
                {
                    labMessage.Text = "请至少选择一条数据";
                    return;
                }
                string userNo = Convert.ToString(dgvUser.Rows[rowIndex].Cells[0].Value).Trim();//该行的处理人工号

                if (MessageBox.Show("确定要删除工号为" + "'" + userNo + "'" + "人员信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string strSQL1 = "delete from S_User_T where UserNo='" + userNo + "'";
                    DBUtil.ExecSQL(strSQL1);//数据库删除掉记录
                    dgvUser.Rows.RemoveAt(rowIndex);//将改行从datagridview中移除
                    labMessage.Text = "删除成功";
                    FormUtil.ClearControls(gbUser);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 返回
        /// <summary>
        /// 撤销所有操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBack_Click(object sender, EventArgs e)
        {
            operateFlag = OperateStateEnum.Defualt;
            RefreshControlState();
        }
        #endregion

        #region 导入
        private void tsmiImport_Click(object sender, EventArgs e)
        {
            try
            {
                labMessage.Text = "";
                //打开文件选择
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "请上传处理人信息";
                ofd.Filter = "Excel 文件|*.xls;*.xlsx";
                ofd.ShowDialog();
                string SelectedFilePath = ofd.FileName;
                string SelectedFileName = ofd.SafeFileName;
                if (SelectedFilePath == "" || SelectedFileName == "")
                {
                    return;
                }
                if (!File.Exists(SelectedFilePath))
                {
                    labMessage.Text = "文件不存在！";
                    return;
                }
                //读取文件数据到DataTable类型中。
                DataTable dt = ExcelUtil.ReadFromExcelFile(SelectedFilePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //判断导入字段完整性
                    if (!dt.Columns.Contains("工号") || !dt.Columns.Contains("姓名") || !dt.Columns.Contains("部门") || !dt.Columns.Contains("状态") ||
                        !dt.Columns.Contains("权限") || !dt.Columns.Contains("类型") || !dt.Columns.Contains("区域") || !dt.Columns.Contains("是否可用"))
                    {
                        labMessage.ForeColor = Color.Red;
                        labMessage.Text = "字段名称异常，请检查excel文件";
                        return;
                    }
                    //sql拼接
                    string sql = "";
                    //遍历数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        //清楚换行，并转换数据
                        string userNo = Regex.Replace(row["工号"].ToString(), @"[\r\n]", "").Trim();
                        string userName = Regex.Replace(row["姓名"].ToString(), @"[\r\n]", "").Trim();
                        string dept = Regex.Replace(row["部门"].ToString(), @"[\r\n]", "").Trim();
                        string userState = Regex.Replace(row["状态"].ToString(), @"[\r\n]", "");
                        userState = "等待中".Equals(userState) ? "W" : "对应中".Equals(userState) ? "H" : "W";
                        string userRight = Regex.Replace(row["权限"].ToString(), @"[\r\n]", "");
                        userRight = "超级管理员".Equals(userRight) ? "A" : "一般管理员".Equals(userRight) ? "B" : "B";
                        string userLevel = Regex.Replace(row["类型"].ToString(), @"[\r\n]", "");
                        userLevel = "工程师".Equals(userLevel) ? "1" : "高级工程师".Equals(userLevel) ? "2" : "1";
                        string area = Regex.Replace(row["区域"].ToString(), @"[\r\n]", "").Trim();
                        string useFlag = Regex.Replace(row["是否可用"].ToString(), @"[\r\n]", "").Trim();
                        useFlag = "是".Equals(useFlag) ? "Y" : "否".Equals(useFlag) ? "N" : "N";

                        sql += string.Format(@"
                                 IF EXISTS(SELECT 1 FROM S_User_T WHERE UserNo='{0}') BEGIN                                  
                                 DELETE S_User_T WHERE UserNo='{0}';
                                 END
                                 INSERT INTO S_User_T(UserNo,UserName,Dept,UserState,Pwd,UserRight,UserLevel,Area,UseFlag,UpdateUser,UpdateTime) 
                                 VALUES('{0}',N'{1}',N'{2}','{3}',N'123','{4}','{5}','{6}','{7}','{8}',GETDATE());",
                                 userNo, userName, dept, userState, userRight, userLevel, area, useFlag, BaseInfo.LoginUserNo);
                        if (i % 50 == 0 || i == dt.Rows.Count - 1)
                        {
                            DBUtil.ExecSQL(sql);
                            sql = "";
                        }
                    }
                    //导入结果判断
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "导入成功";
                    RefreshGridData();
                }
            }
            catch (Exception ex)
            {
                labMessage.ForeColor = Color.Red;
                labMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 导出
        private void tsmiExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // 列名称强制转换
                for (int c = 0; c < dgvUser.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvUser.Columns[c].Name.ToString());
                    dc.Caption = dgvUser.Columns[c].HeaderText.ToString();
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvUser.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvUser.Columns.Count; c++)
                    {
                        dr[c] = Convert.ToString(dgvUser.Rows[r].Cells[c].EditedFormattedValue);
                    }
                    dt.Rows.Add(dr);
                }
                //移除头像列
                dt.Columns.Remove("dgcUserImage");
                //打开保存对话框保存
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Execl Files (*.xlsx)|*.xlsx|Execl Files (*.xls)|*.xls";
                dlg.FilterIndex = 0;
                dlg.RestoreDirectory = true;
                dlg.Title = "保存为Excel文件";
                dlg.FileName = "处理人信息";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExcelUtil.WriteToExcel(dlg.FileName, dt, true, true, true);
                    MessageBox.Show("导出[" + dlg.FileName + "]成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 重置状态
        /// <summary>
        /// 重置人员状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要将【" + BaseInfo.Area + "区】所有人状态重置为等待中吗?", "状态重置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string strSQL = string.Format("UPDATE S_User_T SET UserState='W' WHERE Area like '%{0}%'", BaseInfo.Area);
                    DBUtil.ExecSQL(strSQL);
                    RefreshGridData();
                    labMessage.Text = "重置成功";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 批量上传
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmsBatchUpload_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件(*.jpg)|*.jpg;*.JPG";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (ofd.SafeFileNames.Length > 50)
                    {
                        labMessage.ForeColor = Color.Red;
                        labMessage.Text = "单次操作不要超过50个文件";
                        return;
                    }
                    //遍历选中的文件
                    for (int i = 0; i < ofd.SafeFileNames.Length; i++)
                    {
                        string userNo = ofd.SafeFileNames[i].Split('.')[0];
                        FileStream fs = new FileStream(ofd.FileNames[i], FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        //文件流的长度，用字节表示 
                        byte[] imgBytesIn = br.ReadBytes((int)fs.Length);
                        //更新文件到数据库
                        string sql = string.Format("UPDATE S_User_T SET ImageUpdateTime=GETDATE(), UserImage=@UserImage WHERE UserNo=N'{0}'", userNo);
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@UserImage", imgBytesIn);
                        DBUtil.ExecSQL(sql, param);
                    }
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "批量上传成功";
                    RefreshGridData();
                }
                catch
                {
                    labMessage.ForeColor = Color.Red;
                    labMessage.Text = "批量上传失败";
                }
            }
            return;
        }
        #endregion

        #region 同步联系人
        private void tsmiSyncaContact_Click(object sender, EventArgs e)
        {
            labMessage.Text = "";
            labMessage.ForeColor = Color.Red;
            if (DialogResult.Yes == MessageBox.Show("是否同步所有用户到联系人列表？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                try
                {
                    string sql = "Insert into S_ContactPerson_T(WorkCode,RealName)  SELECT UserNo,UserName FROM S_User_T WHERE UserNo NOT IN(Select WorkCode FROM S_ContactPerson_T);";
                    DBUtil.ExecSQL(sql);
                    labMessage.ForeColor = Color.Green;
                    labMessage.Text = "同步完成";
                }
                catch (Exception ex)
                {
                    labMessage.Text = "同步失败,原因：" + ex.Message;
                }
            }
        }
        #endregion

        #region 其他

        #region 数据检查
        /// <summary>
        /// 基础信息填写检查
        /// </summary>
        /// <returns></returns>
        private bool BaseCheck()
        {
            if (tbUserNo.Text == "")
            {
                labMessage.Text = "工号不能为空";
                return false;
            }
            if (tbUserName.Text == "")
            {
                labMessage.Text = "姓名不能为空";
                return false;
            }
            if (cmbDept.Text == "")
            {
                labMessage.Text = "部门不能为空";
                return false;
            }
            if (tbPwd.Text == "")
            {
                labMessage.Text = "密码不能为空";
                return false;
            }
            if (cmbUserRight.Text == "")
            {
                labMessage.Text = "权限不能为空";
                return false;
            }
            if (cmbUserLevel.Text == "")
            {
                labMessage.Text = "类型不能为空";
                return false;
            }
            return true;
        }
        #endregion

        #region 上传图片
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.JPG|*.jpg";
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
                    if (SelectedFileName.Substring(0, SelectedFileName.Length - 4) != tbUserNo.Text.Trim())
                    {
                        labMessage.Text = "图片名称必须以工号命名！";
                        return;
                    }

                    if (File.Exists(filePath))
                    {
                        tbPicPath.Text = filePath;
                        pbUserImage.Image = Image.FromFile(filePath);
                    }
                }
                catch
                {
                    MessageBox.Show("您选择的图片不能被读取或文件类型不对！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        #endregion

        #region 单元格格式化
        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUser.Columns[e.ColumnIndex].HeaderText.Equals("状态"))
            {
                string state = dgvUser[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (state == "H")
                {
                    e.Value = "对应中";
                }
                else if (state == "W")
                {
                    e.Value = "等待中";
                }
                return;
            }
            if (dgvUser.Columns[e.ColumnIndex].HeaderText.Equals("权限"))
            {
                string right = dgvUser[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (right == "A")
                {
                    e.Value = "超级管理员";
                }
                else if (right == "B")
                {
                    e.Value = "一般管理员";
                }
                return;
            }
            if (dgvUser.Columns[e.ColumnIndex].HeaderText.Equals("类型"))
            {
                string level = dgvUser[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (level == "1")
                {
                    e.Value = "工程师";
                }
                else if (level == "2")
                {
                    e.Value = "高级工程师";
                }
                return;
            }
            if (dgvUser.Columns[e.ColumnIndex].HeaderText.Equals("是否可用"))
            {
                string useFlag = dgvUser[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (useFlag == "Y")
                {
                    e.Value = "是";
                }
                else if (useFlag == "N")
                {
                    e.Value = "否";
                }
                return;
            }
        }
        #endregion

        #region 单元格点击
        /// <summary>
        /// 单元格点击的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    DataRow row = ((DataRowView)dgvUser.Rows[e.RowIndex].DataBoundItem).Row;
                    tbUserNo.Text = row["UserNo"].ToString();
                    tbUserName.Text = row["UserName"].ToString();
                    cmbDept.Text = row["Dept"].ToString();
                    chkUseFlag.Checked = row["UseFlag"].ToString() == "Y" ? true : false;
                    cmbUserRight.SelectedValue = row["UserRight"].ToString();
                    cmbUserLevel.SelectedValue = row["UserLevel"].ToString();
                    cmbUserState.SelectedValue = row["UserState"].ToString();
                    tbPicPath.Text = "";
                    string userNo = row["UserNo"].ToString();
                    //区域
                    string area = row["Area"].ToString();
                    for (int i = 0; i < clbArea.Items.Count; i++)
                    {
                        if (area.Contains(clbArea.Items[i].ToString()))
                        {
                            clbArea.SetItemChecked(i, true);
                        }
                        else
                        {
                            clbArea.SetItemChecked(i, false);
                        }
                    }
                    //图片
                    pbUserImage.Image = UserImageUtil.GetNewCacheImage(userNo);
                 
                    //密码
                     if (userNo == BaseInfo.LoginUserNo || BaseInfo.LoginUserRight == "A")
                    {
                        tbPwd.Text = row["Pwd"].ToString();
                    }
                    else
                    {
                        tbPwd.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 刷新表数据
        /// <summary>
        /// 刷新表数据
        /// </summary>
        private void RefreshGridData()
        {
            try
            {
                string strSQL = @"SELECT UserNo ,UserName,Dept,UserState,UserRight,UserLevel,Area,UseFlag,Pwd,
                                Case ISNULL(DATALENGTH(UserImage),0) WHEN 0 THEN '无' ELSE  '有' END  HasUserImage,UpdateUser,UpdateTime
	                            FROM S_User_T where 1=1 ";
                FormUtil.ClearControlsSpace(gbUser);
                if (tbUserNo.Text != "")
                {
                    strSQL += "and UserNo like " + "N'%" + tbUserNo.Text + "%' ";
                }
                if (tbUserName.Text != "")
                {
                    strSQL += "and UserName like " + "N'%" + tbUserName.Text + "%' ";
                }
                if (cmbDept.Text != "")
                {
                    strSQL += "and Dept like " + "N'%" + cmbDept.Text + "%' ";
                }
                if (cmbUserState.Text != "")
                {
                    strSQL += "and UserState = '" + cmbUserState.SelectedValue + "' ";
                }
                if (cmbUserRight.Text != "")
                {
                    strSQL += "and UserRight ='" + cmbUserRight.SelectedValue + "' ";
                }
                if (cmbUserLevel.Text != "")
                {
                    strSQL += "and UserLevel ='" + cmbUserLevel.SelectedValue + "' ";
                }
                DataTable dt = DBUtil.GetDataTable(strSQL);
                dgvUser.DataSource = dt;

                //部门下拉选数据源
                if (dt != null)
                {
                    cmbDept.Items.Clear();
                    List<string> depts = dt.AsEnumerable().Select(t => t.Field<string>("Dept")).ToList().Distinct().ToList();
                    cmbDept.Items.AddRange(depts.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseUser), null, ex);
            }
        }
        #endregion

        #region 刷新控件状态
        /// <summary>
        /// 刷新控件状态(处理人信息)
        /// </summary>
        private void RefreshControlState()
        {

            switch (operateFlag)
            {
                case OperateStateEnum.Add:
                    //开启新增状态
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    FormUtil.ClearControls(gbUser);
                    tbUserNo.Enabled = true;
                    cmbDept.Enabled = true;
                    cmbUserLevel.Enabled = true;
                    cmbUserRight.Enabled = BaseInfo.LoginUserRight == "A" ? true : false;
                    cmbUserRight.Text = "一般管理员";
                    tbUserName.Enabled = true;
                    cmbUserState.Enabled = false;
                    cmbUserState.Text = "等待中";
                    tbPwd.Enabled = true;
                    btnUpload.Enabled = true;
                    chkUseFlag.Checked = true;
                    dgvUser.Enabled = false;
                    break;

                case OperateStateEnum.Edit:
                    //开启编辑状态
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = true;
                    tsmiDelete.Enabled = false;
                    tsmiBack.Enabled = true;

                    tbUserNo.Enabled = false;
                    tbUserName.Enabled = true;
                    cmbDept.Enabled = true;
                    cmbUserState.Enabled = true;
                    cmbUserRight.Enabled = BaseInfo.LoginUserRight == "A" ? true : false;
                    cmbUserLevel.Enabled = true;
                    tbPwd.Enabled = true;
                    btnUpload.Enabled = true;

                    dgvUser.Enabled = false;
                    break;
                case OperateStateEnum.Query:
                    //开启查询状态
                    tsmiAdd.Enabled = false;
                    tsmiUpdate.Enabled = false;
                    tsmiSave.Enabled = false;
                    tsmiSelect.Enabled = false;
                    tsmiDelete.Enabled = false;
                    tsmiRefresh.Enabled = true;
                    tsmiBack.Enabled = true;

                    FormUtil.ClearControls(gbUser);
                    tbUserNo.Enabled = true;
                    cmbDept.Enabled = true;
                    cmbUserLevel.Enabled = true;
                    cmbUserRight.Enabled = true;
                    tbUserName.Enabled = true;
                    cmbUserState.Enabled = true;
                    tbPwd.Enabled = false;
                    btnUpload.Enabled = false;
                    break;
                case OperateStateEnum.Defualt:
                    //返回状态
                    tsmiSave.Enabled = false;
                    tsmiSelect.Enabled = true;
                    tsmiRefresh.Enabled = true;
                    tsmiBack.Enabled = false;
                    tsmiExport.Enabled = true;
                    tsmiAdd.Enabled = true;
                    tsmiUpdate.Enabled = true;
                    tsmiDelete.Enabled = true;
                    tsmiReset.Enabled = true;

                    FormUtil.ClearControls(gbUser);
                    tbUserNo.Enabled = true;
                    cmbDept.Enabled = true;
                    cmbUserLevel.Enabled = true;
                    cmbUserRight.Enabled = true;
                    tbUserName.Enabled = true;
                    cmbUserState.Enabled = true;
                    tbPwd.Enabled = false;
                    btnUpload.Enabled = false;

                    dgvUser.Enabled = true;
                    //清除选中状态及数据
                    dgvUser.ClearSelection();
                    break;
            }
        }
        #endregion




        #endregion



    }
}
