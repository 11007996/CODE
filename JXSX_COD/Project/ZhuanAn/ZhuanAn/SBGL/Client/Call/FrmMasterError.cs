using Call.Base;
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

namespace Call
{
    public partial class FrmMasterError : Form
    {
        private FrmMaster _owner;//打开此窗体的所有者
        public int SelectedErrorId;//当前选中的故障Id
        private DataTable Errors;//故障数据

        public FrmMasterError()
        {
            InitializeComponent();
        }

        //重定位
        public void ReLocationForm()
        {
            Rectangle rec = new Rectangle(new Point(_owner.Location.X + _owner.Width, _owner.Location.Y), new Size(400, _owner.Height));
            this.Location = rec.Location;
            this.Height = rec.Size.Height;
            this.Width = rec.Size.Width;
        }

        //加载
        private void FrmMasterError_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                _owner = this.Owner as FrmMaster;
                ReLocationForm();
            }
            this.dgvError.AutoGenerateColumns = false;
            RefreshError();
        }

        //刷新故障列表
        public void RefreshError()
        {
            //查找未完成故障
            string sql = string.Format(@"SELECT TOP 100 Id,e.Area,e.Line,e.Dept,TargetHandler,h3.UserName TargetHandlerName, Machine,CallReason,MaxHandleTimes,MaxHelpTimes,StartTime, ComeTime,EndTime,
                                    e.HandlerNo,h1.UserName HandlerName,e.HelperNo,h2.UserName HelperName,Status,ErrorReason,FaultType,FaultContent,SolutionContent,ProdCount,SolverNo,SolverName
                                    FROM M_ErrorRecord_T e
                                    LEFT JOIN S_User_T h1 ON h1.UserNo=e.HandlerNo 
                                    LEFT JOIN S_User_T h2 ON h2.UserNo=e.HelperNo
                                    LEFT JOIN S_User_T h3 ON h3.UserNo=e.TargetHandler
                                    WHERE  e.Line='{0}' AND e.Status<>'N' AND e.Status<>'Y'  ", _owner._Line);
            if (!string.IsNullOrWhiteSpace(_owner._Machine)) sql += string.Format(" AND (e.Machine=N'{0}' OR e.Machine=N'换线') ", _owner._Machine);
            sql += " ORDER BY StartTime DESC";
            DataTable dtErrors = DBUtil.GetDataTable(sql);
            if (dtErrors == null) return;

            //----------------------------数据表源-----------------------------
            Errors = dtErrors;
            dgvError.DataSource = Errors;
            if (SelectedErrorId > 0)
            {
                DataRow dr = dtErrors.Select("Id=" + SelectedErrorId).FirstOrDefault();
                int index = dtErrors.Rows.IndexOf(dr);
                if (index >= 0) { dgvError.Rows[index].Selected = true; } else { dgvError.ClearSelection(); }
            }
            else
            {
                dgvError.ClearSelection();
            }
            //-----------------------------检查是否超时-------------------------
            //检找故障相关刷卡记录
            DataTable dtScan = null;
            if (dtErrors.Rows.Count > 0)
            {
                //刷卡信息
                List<int> Ids = dtErrors.AsEnumerable().Select(t => t.Field<int>("Id")).ToList();
                sql = string.Format(@"SELECT  ErrorId,HandlerNo,HandlerName,HandlerLevel,ErrorStatus,CreateTime
                                FROM M_ErrorHandleScan_T WHERE ErrorId in({0}) ORDER BY CreateTime ASC", string.Join(",", Ids));
                dtScan = DBUtil.GetDataTable(sql);
            }
            //循环检查是否超时
            if (dtErrors != null && dtScan != null)
            {
                foreach (DataRow row in dtErrors.Rows)
                {
                    DataTable dt = dtScan.Clone();
                    DataRow[] drArr = dtScan.Select(" ErrorId='" + row["Id"].ToString() + "'");
                    foreach (DataRow r in drArr)
                    {
                        dt.ImportRow(r);
                    }
                    ErrorInfo e = _owner.MappingErrorInfo(row, dt);
                    _owner.CheckErrorOverTime(e);
                }
            }
        }


        //单元格格式化
        private void dgvError_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvError.Columns[e.ColumnIndex].HeaderText == "状态")
                {
                    ErrorStatus status = (ErrorStatus)Enum.Parse(typeof(ErrorStatus), e.Value.ToString());
                    switch (status)
                    {
                        case ErrorStatus.A:
                            e.Value = "待处理";
                            break;
                        case ErrorStatus.B:
                            e.Value = "处理中";
                            break;
                        case ErrorStatus.C:
                            e.Value = "待支援";
                            break;
                        case ErrorStatus.D:
                            e.Value = "支援中";
                            break;
                        case ErrorStatus.E:
                            e.Value = "待完成";
                            break;
                        case ErrorStatus.N:
                            e.Value = "呼叫解除";
                            break;
                        case ErrorStatus.Y:
                            e.Value = "已完成";
                            break;
                    }
                }
            }
        }

        //取消选中
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgvError.ClearSelection();
            SelectedErrorId = 0;
            _owner.ColseScanFrm();
            _owner.LoadErrorInfo(null);
        }

        //定时刷新
        private void timerRefreshError_Tick(object sender, EventArgs e)
        {
            timerRefreshError.Enabled = false;
            RefreshError();
            if (Errors != null && Errors.Rows.Count > 0)
            {
                timerRefreshError.Enabled = true;
            }
        }

        //单元格单击事件
        private void dgvError_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvError.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgvError.SelectedRows[0];
                SelectedErrorId = Int32.Parse(row.Cells["dgcErrorId"].Value.ToString());
                _owner.ColseScanFrm();
                _owner.RefreshErrorInfo(SelectedErrorId);
            }
        }


    }

}
