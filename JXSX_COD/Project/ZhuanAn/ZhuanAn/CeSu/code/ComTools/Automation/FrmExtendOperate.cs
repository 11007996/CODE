using ComTools.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComTools.Automation
{
    public partial class FrmExtendOperate : Form
    {
        public FrmExtendOperate()
        {
            InitializeComponent();
        }

        public FrmExtendOperate(ExtendOperateConfig operateInfo)
        {
            InitializeComponent();
            OperateConfig = operateInfo;
        }

        public ExtendOperateConfig OperateConfig { get; set; }

        #region 初始化

        private void FrmExtendOperate_Load(object sender, EventArgs e)
        {
            IList<KeyValuePair<OperateTypeEnum, string>> operateTypes = EnumHelper.GetEnumDescriptions<OperateTypeEnum>();
            cmbOperateType.DisplayMember = "Value";
            cmbOperateType.ValueMember = "Key";
            cmbOperateType.DataSource = operateTypes;

            if (OperateConfig != null)
            {
                cmbOperateType.SelectedValue = OperateConfig.OperateType;
                txbWindowName.Text = OperateConfig.WindowName;
                txbControlType.Text = OperateConfig.ControlType;
                txbAutomationId.Text = OperateConfig.AutomationId;
                txbAppPath.Text = OperateConfig.AppPath;
            }
        }

        #endregion 初始化

        #region 选择窗口

        private void btnSelectWindow_Click(object sender, EventArgs e)
        {
            FrmSelectWindow frm = new FrmSelectWindow();
            frm.ShowDialog();
            if (frm.SelectedWindow != null)
            {
                txbWindowName.Text = frm.SelectedWindow.Title;
            }
        }

        #endregion 选择窗口

        #region 选择控件

        private void btnSelectControl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbWindowName.Text))
            {
                MessageBox.Show("请先选择窗口", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmModal frm = new FrmModal(txbWindowName.Text);
            frm.ShowDialog();

            txbControlType.Text = frm.SelectedElement.ControlType;
            txbAutomationId.Text = frm.SelectedElement.AutomationID;
        }

        #endregion 选择控件

        #region 选择程序

        private void btnSelectApp_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择启动软件";
            ofd.Filter = "执行程序(*.exe)|*.exe";
            ofd.Multiselect = false;
            ofd.ShowDialog();
            txbAppPath.Text = ofd.FileName;
        }

        #endregion 选择程序

        #region 确认

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            OperateConfig = new ExtendOperateConfig();
            KeyValuePair<OperateTypeEnum, string> operateType = (KeyValuePair<OperateTypeEnum, string>)cmbOperateType.SelectedItem;
            OperateConfig.OperateType = operateType.Key;
            OperateConfig.WindowName = txbWindowName.Text;
            OperateConfig.ControlType = txbControlType.Text;
            OperateConfig.AutomationId = txbAutomationId.Text;
            OperateConfig.AppPath = txbAppPath.Text;
            this.Close();
        }

        #endregion 确认

        #region 删除

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OperateConfig = null;
            this.Close();
        }

        #endregion 删除

        #region 事件交互

        private void cmbOperateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<OperateTypeEnum, string> operateType = (KeyValuePair<OperateTypeEnum, string>)cmbOperateType.SelectedItem;
            SetControlState(operateType.Key);
        }

        private void SetControlState(OperateTypeEnum operateEnum)
        {
            switch (operateEnum)
            {
                case OperateTypeEnum.MOUSE_CLICK://鼠标单击
                    btnSelectWindow.Enabled = true;
                    btnSelectControl.Enabled = true;
                    btnSelectApp.Enabled = false;
                    txbAppPath.Text = "";
                    break;

                case OperateTypeEnum.OPEN_APP://打开应用
                    btnSelectWindow.Enabled = false;
                    btnSelectControl.Enabled = false;
                    btnSelectApp.Enabled = true;
                    txbWindowName.Text = "";
                    txbControlType.Text = "";
                    txbAutomationId.Text = "";
                    break;

                case OperateTypeEnum.CLOSE_WINDOW://关闭窗口
                    btnSelectWindow.Enabled = true;
                    btnSelectControl.Enabled = false;
                    btnSelectApp.Enabled = false;
                    txbControlType.Text = "";
                    txbAutomationId.Text = "";
                    txbAppPath.Text = "";
                    break;
            }
        }

        #endregion 事件交互
    }
}