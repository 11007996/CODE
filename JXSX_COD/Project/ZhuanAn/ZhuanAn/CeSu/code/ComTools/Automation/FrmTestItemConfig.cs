using ComTools.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComTools.Automation
{
    public partial class FrmTestItemConfig : Form
    {
        public FrmTestItemConfig()
        {
            InitializeComponent();
        }

        public FrmTestItemConfig(TestItemConfig config)
        {
            InitializeComponent();
            TestItemConfig = config;
        }

        public FrmTestItemConfig(TestItemConfig config, List<string> testItems)
        {
            InitializeComponent();
            cmbTestItem.DataSource = testItems;
            TestItemConfig = config;
        }

        public TestItemConfig TestItemConfig { get; set; }

        #region 初始化

        private void FrmTestItemConfig_Load(object sender, EventArgs e)
        {
            InitControl();
            InitControlData();
        }

        private void InitControl()
        {
            //运算符
            IList<KeyValuePair<OperatorEnum, string>> operatorItem = EnumHelper.GetEnumDescriptions<OperatorEnum>();
            cmbOperator.DisplayMember = "Value";
            cmbOperator.ValueMember = "Key";
            cmbOperator.DataSource = operatorItem;
            //前置操作类型
            IList<KeyValuePair<OperateTypeEnum, string>> preOperateType = EnumHelper.GetEnumDescriptions<OperateTypeEnum>();
            cmbPrefixOperateType.DisplayMember = "Value";
            cmbPrefixOperateType.ValueMember = "Key";
            cmbPrefixOperateType.DataSource = preOperateType;
            //后置操作类型
            IList<KeyValuePair<OperateTypeEnum, string>> sufOperateType = EnumHelper.GetEnumDescriptions<OperateTypeEnum>();
            cmbSuffixOperateType.DisplayMember = "Value";
            cmbSuffixOperateType.ValueMember = "Key";
            cmbSuffixOperateType.DataSource = sufOperateType;
        }

        private void InitControlData()
        {
            if (TestItemConfig != null)
            {
                //测试项目
                cmbTestItem.Text = TestItemConfig.TestItem;
                nudOvertime.Value = TestItemConfig.Overtime;
                txbWindowName.Text = TestItemConfig.WindowName;
                txbControlType.Text = TestItemConfig.ControlType;
                txbAutomationId.Text = TestItemConfig.AutomationID;
                cmbOperator.SelectedValue = TestItemConfig.Operator;
                txbWhereValue.Text = TestItemConfig.WhereValue;
                txbFilterFormat.Text = TestItemConfig.FilterFormat;
                //前置
                if (TestItemConfig.PrefixOperate != null)
                {
                    cmbPrefixOperateType.SelectedValue = TestItemConfig.PrefixOperate.OperateType;
                    txbPrefixAppPath.Text = TestItemConfig.PrefixOperate.AppPath;
                    txbPrefixWindowName.Text = TestItemConfig.PrefixOperate.WindowName;
                    txbPrefixControlType.Text = TestItemConfig.PrefixOperate.ControlType;
                    txbPrefixAutomationId.Text = TestItemConfig.PrefixOperate.AutomationId;
                }
                //后置
                if (TestItemConfig.SuffixOperate != null)
                {
                    cmbSuffixOperateType.SelectedValue = TestItemConfig.SuffixOperate.OperateType;
                    txbSuffixAppPath.Text = TestItemConfig.SuffixOperate.AppPath;
                    txbSuffixWindowName.Text = TestItemConfig.SuffixOperate.WindowName;
                    txbSuffixControlType.Text = TestItemConfig.SuffixOperate.ControlType;
                    txbSuffixAutomationId.Text = TestItemConfig.SuffixOperate.AutomationId;
                }
                //通信
                txbRequestCode.Text = TestItemConfig.RequestHexCode;
                txbResponsePassCode.Text = TestItemConfig.ResponsePassHexCode;
                txbResponseFailCode.Text = TestItemConfig.ResponseFailHexCode;
            }
        }

        #endregion 初始化

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestItemConfig == null)
                TestItemConfig = new TestItemConfig();
            //测试项目基础
            TestItemConfig.TestItem = cmbTestItem.Text;
            TestItemConfig.Overtime = nudOvertime.Value;
            TestItemConfig.WindowName = txbWindowName.Text;
            TestItemConfig.ControlType = txbControlType.Text;
            TestItemConfig.AutomationID = txbAutomationId.Text;
            TestItemConfig.Operator = (OperatorEnum)cmbOperator.SelectedValue;
            TestItemConfig.WhereValue = txbWhereValue.Text;
            TestItemConfig.FilterFormat = txbFilterFormat.Text;
            //前置
            if ((OperateTypeEnum)cmbPrefixOperateType.SelectedValue == OperateTypeEnum.NONE)
            {
                TestItemConfig.PrefixOperate = null;
            }
            else
            {
                TestItemConfig.PrefixOperate = new ExtendOperateConfig();
                TestItemConfig.PrefixOperate.OperateType = (OperateTypeEnum)cmbPrefixOperateType.SelectedValue;
                TestItemConfig.PrefixOperate.AppPath = txbPrefixAppPath.Text;
                TestItemConfig.PrefixOperate.WindowName = txbPrefixWindowName.Text;
                TestItemConfig.PrefixOperate.ControlType = txbPrefixControlType.Text;
                TestItemConfig.PrefixOperate.AutomationId = txbPrefixAutomationId.Text;
            }
            //后置
            if ((OperateTypeEnum)cmbSuffixOperateType.SelectedValue == OperateTypeEnum.NONE)
            {
                TestItemConfig.SuffixOperate = null;
            }
            else
            {
                TestItemConfig.SuffixOperate = new ExtendOperateConfig();
                TestItemConfig.SuffixOperate.OperateType = (OperateTypeEnum)cmbSuffixOperateType.SelectedValue;
                TestItemConfig.SuffixOperate.AppPath = txbSuffixAppPath.Text;
                TestItemConfig.SuffixOperate.WindowName = txbSuffixWindowName.Text;
                TestItemConfig.SuffixOperate.ControlType = txbSuffixControlType.Text;
                TestItemConfig.SuffixOperate.AutomationId = txbSuffixAutomationId.Text;
            }
            //通信
            TestItemConfig.RequestHexCode = txbRequestCode.Text;
            TestItemConfig.ResponsePassHexCode = txbResponsePassCode.Text;
            TestItemConfig.ResponseFailHexCode = txbResponseFailCode.Text;
            this.Close();
        }

        private void btnSelectControl_Click(object sender, EventArgs e)
        {
            FrmModal frm = new FrmModal();
            frm.ShowDialog();
            if (frm.SelectedElement != null)
            {
                txbWindowName.Text = frm.SelectedElement.WindowName;
                txbControlType.Text = frm.SelectedElement.ControlType;
                txbAutomationId.Text = frm.SelectedElement.AutomationID;
            }
        }

        private void btnSelectWindow_Click(object sender, EventArgs e)
        {
            FrmSelectWindow frm = new FrmSelectWindow();
            frm.ShowDialog();

            if (frm.SelectedWindow != null)
            {
                txbWindowName.Text = frm.SelectedWindow.Title;
            }
        }

        private void btnSetPrefixOperate_Click(object sender, EventArgs e)
        {
            FrmExtendOperate frm;
            if (TestItemConfig != null)
                frm = new FrmExtendOperate(TestItemConfig.PrefixOperate);
            else
                frm = new FrmExtendOperate();
            frm.ShowDialog();
            cmbPrefixOperateType.SelectedValue = frm.OperateConfig == null ? OperateTypeEnum.NONE : frm.OperateConfig.OperateType;
            txbPrefixAppPath.Text = frm.OperateConfig?.AppPath;
            txbPrefixWindowName.Text = frm.OperateConfig?.WindowName;
            txbPrefixControlType.Text = frm.OperateConfig?.ControlType;
            txbPrefixAutomationId.Text = frm.OperateConfig?.AutomationId;
        }

        private void btnSetSuffixOperate_Click(object sender, EventArgs e)
        {
            FrmExtendOperate frm;
            if (TestItemConfig != null)
                frm = new FrmExtendOperate(TestItemConfig.SuffixOperate);
            else
                frm = new FrmExtendOperate();
            frm.ShowDialog();
            cmbSuffixOperateType.SelectedValue = frm.OperateConfig == null ? OperateTypeEnum.NONE : frm.OperateConfig.OperateType;
            txbSuffixAppPath.Text = frm.OperateConfig?.AppPath;
            txbSuffixWindowName.Text = frm.OperateConfig?.WindowName;
            txbSuffixControlType.Text = frm.OperateConfig?.ControlType;
            txbSuffixAutomationId.Text = frm.OperateConfig?.AutomationId;
        }
    }
}