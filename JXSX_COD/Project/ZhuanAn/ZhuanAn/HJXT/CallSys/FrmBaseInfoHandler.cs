using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using CallSys.Utils;
using CallSys.Base;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Utils;
using Common.Base;
using Common;


namespace CallSys
{
    public partial class FrmBaseInfoHandler : Form
    {
        private OperateState operateFlag = OperateState.Defualt;//处理人信息操作状态标记

        public FrmBaseInfoHandler()
        {
            InitializeComponent();
        }

        #region 窗体事件
        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBaseInfoHandler_Load(object sender, EventArgs e)
        {
            //------------初始化处理人信息选项卡组件-----------
            //处理人权限
            IList RightList = new List<DictionaryEntry>();
            RightList.Add(new DictionaryEntry("", ""));
            RightList.Add(new DictionaryEntry("A", "超级管理员"));
            RightList.Add(new DictionaryEntry("B", "一般管理员"));
            cmbHandlerRight.DataSource = RightList;
            cmbHandlerRight.ValueMember = "Key";
            cmbHandlerRight.DisplayMember = "Value";
            //处理人等级
            IList levelList = new List<DictionaryEntry>();
            levelList.Add(new DictionaryEntry("", ""));
            levelList.Add(new DictionaryEntry("1", "工程师"));
            levelList.Add(new DictionaryEntry("2", "高级工程师"));
            cmbHandlerLevel.DataSource = levelList;
            cmbHandlerLevel.ValueMember = "Key";
            cmbHandlerLevel.DisplayMember = "Value";
            //处理人状态
            IList stateList = new List<DictionaryEntry>();
            stateList.Add(new DictionaryEntry("", ""));
            stateList.Add(new DictionaryEntry("W", "等待中"));
            stateList.Add(new DictionaryEntry("H", "对应中"));
            cmbHandlerState.DataSource = stateList;
            cmbHandlerState.ValueMember = "Key";
            cmbHandlerState.DisplayMember = "Value";
            //区域
            string sql = "select DISTINCT Area  FROM M_LineInfo_T ";
            DataTable dtArea = DBUtil.GetDataTable(sql);
            if (dtArea != null)
            {
                foreach (DataRow row in dtArea.Rows)
                {
                    clbHandlerArea.Items.Add(row["Area"].ToString(), false);
                }
            }
            clbHandlerArea.BringToFront();
            //禁用自动添加列
            dgvHandler.AutoGenerateColumns = false;
            //刷新
            RefreshHandlerDataGrid();
            RefreshHandlerControlState();
        }
        #endregion


        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerAdd_Click(object sender, EventArgs e)
        {
            operateFlag = OperateState.Add;
            RefreshHandlerControlState();
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerUpdate_Click(object sender, EventArgs e)
        {
            string HandlerNo = HandlerNo = dgvHandler.CurrentRow.Cells["dgcHandlerNo"].Value.ToString();
            if (dgvHandler.CurrentRow == null || HandlerNo == null || string.IsNullOrWhiteSpace(HandlerNo))
            {
                labHandlerMessage.Text = "请至少选择一条数据";
                return;
            }
            operateFlag = OperateState.Edit;
            RefreshHandlerControlState();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerSave_Click(object sender, EventArgs e)
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
                FormUtil.ClearControlsSpace(gbHandler);
                string useFlag = chkUseFlag.Checked ? "Y" : "N";
                List<string> areaList = new List<string>();
                for (int i = 0; i < clbHandlerArea.CheckedItems.Count; i++)
                {
                    areaList.Add(clbHandlerArea.CheckedItems[i].ToString());
                }
                string area = string.Join(",", areaList);
                //新增
                if (operateFlag == OperateState.Add)
                {
                    strSql = string.Format(@"insert into M_HandlerInfo_T (HandlerNo,HandlerName,HandlerDept,HandlerState,HandlerPwd,HandlerRight,HandlerLevel,Area,UseFlag,UpdateUser,UpdateTime)
                                            values('{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}','{8}',N'{9}',GETDATE())",
                                             tbHandlerNo.Text, tbHandlerName.Text, cmbHandlerDept.Text, cmbHandlerState.SelectedValue, tbHandlerPwd.Text, cmbHandlerRight.SelectedValue, cmbHandlerLevel.SelectedValue, area, useFlag, BaseInfo.LoginHandlerNo);
                    DBUtil.ExecSQL(strSql);
                }
                //修改
                if (operateFlag == OperateState.Edit)
                {
                    strSql = string.Format("update M_HandlerInfo_T set HandlerName=N'{1}',HandlerDept=N'{2}',HandlerState=N'{3}',HandlerPwd=N'{4}',HandlerRight=N'{5}',HandlerLevel=N'{6}',Area=N'{7}',UseFlag='{8}',UpdateUser=N'{9}',UpdateTime=GETDATE() where HandlerNo=N'{0}'",
                        tbHandlerNo.Text, tbHandlerName.Text, cmbHandlerDept.Text, cmbHandlerState.SelectedValue, tbHandlerPwd.Text, cmbHandlerRight.SelectedValue, cmbHandlerLevel.SelectedValue, area, useFlag, BaseInfo.LoginHandlerNo);
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
                    string sql = string.Format("UPDATE M_HandlerInfo_T SET ImageUpdateTime=GETDATE(), HandlerImage=@HandlerImage WHERE HandlerNo=N'{0}'", tbHandlerNo.Text);
                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@HandlerImage", imgBytesIn);
                    DBUtil.ExecSQL(sql, param);
                }
                FormUtil.ClearControls(gbHandler);
                operateFlag = OperateState.Defualt;
                RefreshHandlerControlState();
                RefreshHandlerDataGrid();
                labHandlerMessage.Text = "操作成功";
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerSelect_Click(object sender, EventArgs e)
        {
            operateFlag = OperateState.Query;
            RefreshHandlerControlState();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 输入查询条件后刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerRefresh_Click(object sender, EventArgs e)
        {
            //刷新数据
            RefreshHandlerDataGrid();
            labHandlerMessage.Text = "数据已加载完成！";
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dgvHandler.CurrentRow.Index;//选中行
                if (rowIndex < 0)
                {
                    labHandlerMessage.Text = "请至少选择一条数据";
                    return;
                }
                string HandlerNo = Convert.ToString(dgvHandler.Rows[rowIndex].Cells[0].Value).Trim();//该行的处理人工号

                if (MessageBox.Show("确定要删除工号为" + "'" + HandlerNo + "'" + "人员信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    string strSQL1 = "delete from m_HandlerInfo_t where HandlerNo='" + HandlerNo + "'";
                    DBUtil.ExecSQL(strSQL1);//数据库删除掉记录
                    dgvHandler.Rows.RemoveAt(rowIndex);//将改行从datagridview中移除
                    labHandlerMessage.Text = "删除成功";
                    FormUtil.ClearControls(gbHandler);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 返回
        /// <summary>
        /// 撤销所有操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerBack_Click(object sender, EventArgs e)
        {
            operateFlag = OperateState.Defualt;
            RefreshHandlerControlState();
        }
        #endregion

        #region 导入
        private void tsmHandlerImport_Click(object sender, EventArgs e)
        {
            try
            {
                labHandlerMessage.Text = "";
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
                    labHandlerMessage.Text = "文件不存在！";
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
                        labHandlerMessage.ForeColor = Color.Red;
                        labHandlerMessage.Text = "字段名称异常，请检查excel文件";
                        return;
                    }
                    //sql拼接
                    string sql = @"DELETE M_HandlerInfo_T;
                                  INSERT INTO M_HandlerInfo_T(HandlerNo,HandlerName,HandlerDept,HandlerState,HandlerPwd,HandlerRight,HandlerLevel,Area,UseFlag,UpdateUser,UpdateTime) VALUES";
                    List<string> handler = new List<string>();
                    //遍历数据
                    foreach (DataRow row in dt.Rows)
                    {
                        //清楚换行，并转换数据
                        string handlerNo = Regex.Replace(row["工号"].ToString(), @"[\r\n]", "").Trim();
                        string handlerName = Regex.Replace(row["姓名"].ToString(), @"[\r\n]", "").Trim();
                        string handlerDept = Regex.Replace(row["部门"].ToString(), @"[\r\n]", "").Trim();
                        string handlerState = Regex.Replace(row["状态"].ToString(), @"[\r\n]", "");
                        handlerState = "等待中".Equals(handlerState) ? "W" : "对应中".Equals(handlerState) ? "H" : "W";
                        string handlerRight = Regex.Replace(row["权限"].ToString(), @"[\r\n]", "");
                        handlerRight = "超级管理员".Equals(handlerRight) ? "A" : "一般管理员".Equals(handlerRight) ? "B" : "B";
                        string handlerLevel = Regex.Replace(row["类型"].ToString(), @"[\r\n]", "");
                        handlerLevel = "工程师".Equals(handlerLevel) ? "1" : "高级工程师".Equals(handlerLevel) ? "2" : "1";
                        string area = Regex.Replace(row["区域"].ToString(), @"[\r\n]", "").Trim();
                        string useFlag = Regex.Replace(row["是否可用"].ToString(), @"[\r\n]", "").Trim();
                        useFlag = "是".Equals(useFlag) ? "Y" : "否".Equals(useFlag) ? "N" : "N";
                        handler.Add(string.Format("('{0}',N'{1}',N'{2}','{3}',N'123','{4}','{5}','{6}','{7}','{8}',GETDATE())", handlerNo, handlerName, handlerDept, handlerState, handlerRight, handlerLevel, area, useFlag, BaseInfo.LoginHandlerNo));
                    }
                    sql += string.Join(",", handler);
                    int r = DBUtil.ExecSQL(sql);
                    //导入结果判断
                    if (r > 0)
                    {
                        labHandlerMessage.ForeColor = Color.Green;
                        labHandlerMessage.Text = "导入成功";
                        RefreshHandlerDataGrid();
                    }
                    else
                    {
                        labHandlerMessage.ForeColor = Color.Red;
                        labHandlerMessage.Text = "导入失败";
                    }
                }
            }
            catch (Exception ex)
            {
                labHandlerMessage.ForeColor = Color.Red;
                labHandlerMessage.Text = "导入失败,原因：" + ex.Message;
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 导出
        private void tsmHandlerExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                // 列名称强制转换
                for (int c = 0; c < dgvHandler.Columns.Count; c++)
                {
                    DataColumn dc = new DataColumn(dgvHandler.Columns[c].Name.ToString());
                    dc.Caption = dgvHandler.Columns[c].HeaderText.ToString();
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int r = 0; r < dgvHandler.Rows.Count; r++)
                {
                    DataRow dr = dt.NewRow();
                    for (int c = 0; c < dgvHandler.Columns.Count; c++)
                    {
                        dr[c] = Convert.ToString(dgvHandler.Rows[r].Cells[c].EditedFormattedValue);
                    }
                    dt.Rows.Add(dr);
                }
                //移除头像列
                dt.Columns.Remove("dgcHandlerImage");
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
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 重置状态
        /// <summary>
        /// 重置人员状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmHandlerReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要将【" + BaseInfo.Area + "区】所有人状态重置为等待中吗?", "状态重置", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string strSQL = string.Format("UPDATE m_HandlerInfo_t SET HandlerState='W' WHERE Area like '%{0}%'", BaseInfo.Area);
                    DBUtil.ExecSQL(strSQL);
                    RefreshHandlerDataGrid();
                    labHandlerMessage.Text = "重置成功";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 区域线别
        private void tsmLineInfo_Click(object sender, EventArgs e)
        {
            FrmLineInfo line = new FrmLineInfo();
            line.ShowDialog();
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
            labHandlerMessage.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件(*.jpg)|*.jpg;*.JPG";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (ofd.SafeFileNames.Length > 50)
                    {
                        labHandlerMessage.ForeColor = Color.Red;
                        labHandlerMessage.Text = "单次操作不要超过50个文件";
                        return;
                    }
                    //遍历选中的文件
                    for (int i = 0; i < ofd.SafeFileNames.Length; i++)
                    {
                        string handlerNo = ofd.SafeFileNames[i].Split('.')[0];
                        FileStream fs = new FileStream(ofd.FileNames[i], FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        //文件流的长度，用字节表示 
                        byte[] imgBytesIn = br.ReadBytes((int)fs.Length);
                        //更新文件到数据库
                        string sql = string.Format("UPDATE M_HandlerInfo_T SET ImageUpdateTime=GETDATE(), HandlerImage=@HandlerImage WHERE HandlerNo=N'{0}'", handlerNo);
                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@HandlerImage", imgBytesIn);
                        DBUtil.ExecSQL(sql, param);
                    }
                    labHandlerMessage.ForeColor = Color.Green;
                    labHandlerMessage.Text = "批量上传成功";
                    RefreshHandlerDataGrid();
                }
                catch
                {
                    labHandlerMessage.ForeColor = Color.Red;
                    labHandlerMessage.Text = "批量上传失败";
                }
            }
            return;
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
            if (tbHandlerNo.Text == "")
            {
                labHandlerMessage.Text = "工号不能为空";
                return false;
            }
            if (tbHandlerName.Text == "")
            {
                labHandlerMessage.Text = "姓名不能为空";
                return false;
            }
            if (cmbHandlerDept.Text == "")
            {
                labHandlerMessage.Text = "部门不能为空";
                return false;
            }
            if (tbHandlerPwd.Text == "")
            {
                labHandlerMessage.Text = "密码不能为空";
                return false;
            }
            if (cmbHandlerRight.Text == "")
            {
                labHandlerMessage.Text = "权限不能为空";
                return false;
            }
            if (cmbHandlerLevel.Text == "")
            {
                labHandlerMessage.Text = "类型不能为空";
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
        private void btnHandlerUpload_Click(object sender, EventArgs e)
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
                    if (SelectedFileName.Substring(0, SelectedFileName.Length - 4) != tbHandlerNo.Text.Trim())
                    {
                        labHandlerMessage.Text = "图片名称必须以工号命名！";
                        return;
                    }

                    if (File.Exists(filePath))
                    {
                        tbPicPath.Text = filePath;
                        pbHandlerPic.Image = Image.FromFile(filePath);
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
        private void dgvHandler_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHandler.Columns[e.ColumnIndex].HeaderText.Equals("头像"))
            {
                DataRow row = ((DataRowView)dgvHandler.Rows[e.RowIndex].DataBoundItem).Row;
                if (row["HandlerImage"] != DBNull.Value)
                {
                    string path = FileUtil.CreateNewCacheFile(row["HandlerNo"].ToString(), row["HandlerImage"] as byte[]);
                    e.Value = GetImage(path);
                }
                else
                {
                    e.Value = global::CallSys.Properties.Resources.defualt_face;
                }
            }
            if (dgvHandler.Columns[e.ColumnIndex].HeaderText.Equals("状态"))
            {
                string state = dgvHandler[e.ColumnIndex, e.RowIndex].Value.ToString();
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
            if (dgvHandler.Columns[e.ColumnIndex].HeaderText.Equals("权限"))
            {
                string right = dgvHandler[e.ColumnIndex, e.RowIndex].Value.ToString();
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
            if (dgvHandler.Columns[e.ColumnIndex].HeaderText.Equals("类型"))
            {
                string level = dgvHandler[e.ColumnIndex, e.RowIndex].Value.ToString();
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
            if (dgvHandler.Columns[e.ColumnIndex].HeaderText.Equals("是否可用"))
            {
                string useFlag = dgvHandler[e.ColumnIndex, e.RowIndex].Value.ToString();
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
        public System.Drawing.Image GetImage(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return img;
        }
        #endregion

        #region 单元格点击
        /// <summary>
        /// 单元格点击的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHandler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    int rowIndex = dgvHandler.CurrentRow.Index;//选中行
                    string HandlerNo = dgvHandler.CurrentRow.Cells["dgcHandlerNo"].Value.ToString();//该行的处理人工号
                    string strSQL = "select * from m_HandlerInfo_t where HandlerNo='" + HandlerNo + "'";
                    DataTable dt = DBUtil.GetDataTable(strSQL);
                    if (dt == null) return;
                    tbHandlerNo.Text = dt.Rows[0]["HandlerNo"].ToString();
                    tbHandlerName.Text = dt.Rows[0]["HandlerName"].ToString();
                    cmbHandlerDept.Text = dt.Rows[0]["HandlerDept"].ToString();
                    chkUseFlag.Checked = dt.Rows[0]["UseFlag"].ToString() == "Y" ? true : false;
                    cmbHandlerRight.SelectedItem = cmbHandlerRight.Items.Cast<DictionaryEntry>().FirstOrDefault(x => x.Key.ToString() == dt.Rows[0]["HandlerRight"].ToString().Trim());
                    cmbHandlerLevel.SelectedItem = cmbHandlerLevel.Items.Cast<DictionaryEntry>().FirstOrDefault(x => x.Key.ToString() == dt.Rows[0]["HandlerLevel"].ToString().Trim());
                    cmbHandlerState.SelectedItem = cmbHandlerState.Items.Cast<DictionaryEntry>().FirstOrDefault(x => x.Key.ToString() == dt.Rows[0]["HandlerState"].ToString().Trim());
                    tbPicPath.Text = "";
                    //区域
                    string area = dt.Rows[0]["Area"].ToString();
                    for (int i = 0; i < clbHandlerArea.Items.Count; i++)
                    {
                        if (area.Contains(clbHandlerArea.Items[i].ToString()))
                        {
                            clbHandlerArea.SetItemChecked(i, true);
                        }
                        else
                        {
                            clbHandlerArea.SetItemChecked(i, false);
                        }
                    }
                    //图片
                    if (dt.Rows[0]["HandlerImage"] == DBNull.Value)
                    {
                        pbHandlerPic.Image = null;
                    }
                    else
                    {
                        pbHandlerPic.Image = FileUtil.GetCacheFile(HandlerNo, Convert.ToDateTime(dt.Rows[0]["ImageUpdateTime"]));
                    }
                    //密码
                    if (HandlerNo == BaseInfo.LoginHandlerNo || BaseInfo.LoginHandlerRight == "A")
                    {
                        tbHandlerPwd.Text = dt.Rows[0]["HandlerPwd"].ToString();
                    }
                    else
                    {
                        tbHandlerPwd.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 刷新处理人表数据
        /// <summary>
        /// 刷新处理人表数据
        /// </summary>
        private void RefreshHandlerDataGrid()
        {
            try
            {
                string strSQL = @"SELECT HandlerNo,HandlerImage,HandlerName,HandlerDept,HandlerState,HandlerRight,HandlerLevel,Area,UseFlag,UpdateUser,UpdateTime
	                            FROM M_HandlerInfo_T where 1=1 ";
                FormUtil.ClearControlsSpace(gbHandler);
                if (tbHandlerNo.Text != "")
                {
                    strSQL += "and HandlerNo like " + "N'%" + tbHandlerNo.Text + "%' ";
                }
                if (tbHandlerName.Text != "")
                {
                    strSQL += "and HandlerName like " + "N'%" + tbHandlerName.Text + "%' ";
                }
                if (cmbHandlerDept.Text != "")
                {
                    strSQL += "and HandlerDept like " + "N'%" + cmbHandlerDept.Text + "%' ";
                }
                if (cmbHandlerState.Text != "")
                {
                    strSQL += "and HandlerState = '" + cmbHandlerState.SelectedValue + "' ";
                }
                if (cmbHandlerRight.Text != "")
                {
                    strSQL += "and HandlerRight ='" + cmbHandlerRight.SelectedValue + "' ";
                }
                if (cmbHandlerLevel.Text != "")
                {
                    strSQL += "and HandlerLevel ='" + cmbHandlerLevel.SelectedValue + "' ";
                }
                DataTable dt = DBUtil.GetDataTable(strSQL);
                dgvHandler.DataSource = dt;

                //部门下拉选数据源
                if (dt != null)
                {
                    cmbHandlerDept.Items.Clear();
                    List<string> depts = dt.AsEnumerable().Select(t => t.Field<string>("HandlerDept")).ToList().Distinct().ToList();
                    cmbHandlerDept.Items.AddRange(depts.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(FrmBaseInfoHandler), null, ex);
            }
        }
        #endregion

        #region 刷新控件状态
        /// <summary>
        /// 刷新控件状态(处理人信息)
        /// </summary>
        private void RefreshHandlerControlState()
        {

            switch (operateFlag)
            {
                case OperateState.Add:
                    //开启新增状态
                    tsmHandlerAdd.Enabled = false;
                    tsmHandlerUpdate.Enabled = false;
                    tsmHandlerSave.Enabled = true;
                    tsmHandlerDelete.Enabled = false;
                    tsmHandlerBack.Enabled = true;

                    FormUtil.ClearControls(gbHandler);
                    tbHandlerNo.Enabled = true;
                    cmbHandlerDept.Enabled = true;
                    cmbHandlerLevel.Enabled = true;
                    cmbHandlerRight.Enabled = BaseInfo.LoginHandlerRight == "A" ? true : false;
                    cmbHandlerRight.Text = "一般管理员";
                    tbHandlerName.Enabled = true;
                    cmbHandlerState.Enabled = false;
                    cmbHandlerState.Text = "等待中";
                    tbHandlerPwd.Enabled = true;
                    btnUpload.Enabled = true;
                    chkUseFlag.Checked = true;
                    dgvHandler.Enabled = false;
                    break;

                case OperateState.Edit:
                    //开启编辑状态
                    tsmHandlerAdd.Enabled = false;
                    tsmHandlerUpdate.Enabled = false;
                    tsmHandlerSave.Enabled = true;
                    tsmHandlerDelete.Enabled = false;
                    tsmHandlerBack.Enabled = true;

                    tbHandlerNo.Enabled = false;
                    tbHandlerName.Enabled = true;
                    cmbHandlerDept.Enabled = true;
                    cmbHandlerState.Enabled = true;
                    cmbHandlerRight.Enabled = BaseInfo.LoginHandlerRight == "A" ? true : false;
                    cmbHandlerLevel.Enabled = true;
                    tbHandlerPwd.Enabled = true;
                    btnUpload.Enabled = true;

                    dgvHandler.Enabled = false;
                    break;
                case OperateState.Query:
                    //开启查询状态
                    tsmHandlerAdd.Enabled = false;
                    tsmHandlerUpdate.Enabled = false;
                    tsmHandlerSave.Enabled = false;
                    tsmHandlerSelect.Enabled = false;
                    tsmHandlerDelete.Enabled = false;
                    tsmHandlerRefresh.Enabled = true;
                    tsmHandlerBack.Enabled = true;

                    FormUtil.ClearControls(gbHandler);
                    tbHandlerNo.Enabled = true;
                    cmbHandlerDept.Enabled = true;
                    cmbHandlerLevel.Enabled = true;
                    cmbHandlerRight.Enabled = true;
                    tbHandlerName.Enabled = true;
                    cmbHandlerState.Enabled = true;
                    tbHandlerPwd.Enabled = false;
                    btnUpload.Enabled = false;
                    break;
                case OperateState.Defualt:
                    //返回状态
                    tsmHandlerSave.Enabled = false;
                    tsmHandlerSelect.Enabled = true;
                    tsmHandlerRefresh.Enabled = true;
                    tsmHandlerBack.Enabled = false;
                    tsmHandlerExport.Enabled = true;
                    tsmHandlerAdd.Enabled = true;
                    tsmHandlerUpdate.Enabled = true;
                    tsmHandlerDelete.Enabled = true;
                    tsmHandlerReset.Enabled = true;

                    FormUtil.ClearControls(gbHandler);
                    tbHandlerNo.Enabled = true;
                    cmbHandlerDept.Enabled = true;
                    cmbHandlerLevel.Enabled = true;
                    cmbHandlerRight.Enabled = true;
                    tbHandlerName.Enabled = true;
                    cmbHandlerState.Enabled = true;
                    tbHandlerPwd.Enabled = false;
                    btnUpload.Enabled = false;

                    dgvHandler.Enabled = true;
                    //清除选中状态及数据
                    dgvHandler.ClearSelection();
                    break;
            }
        }
        #endregion
        #endregion



    }
}
