using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComTools.AgingTest
{
    public partial class FrmAgingTestItemConfig : Form
    {
        public FrmAgingTestItemConfig()
        {
            InitializeComponent();
        }

        public FrmAgingTestItemConfig(AgingTestItemConfig config)
        {
            InitializeComponent();
            TestItemConfig = config;
        }

        public FrmAgingTestItemConfig(AgingTestItemConfig config, List<string> testItems)
        {
            InitializeComponent();
            cmbTestItem.DataSource = testItems;
            TestItemConfig = config;
        }

        public AgingTestItemConfig TestItemConfig { get; set; }

        #region 初始化

        private void FrmTestItemConfig_Load(object sender, EventArgs e)
        {
            InitControlData();
        }

        private void InitControlData()
        {
            if (TestItemConfig != null)
            {
                //测试项目
                cmbTestItem.Text = TestItemConfig.TestItem;
                nudOvertime.Value = TestItemConfig.OverTime;
                //请求
                nudRequestByteSize.Value = TestItemConfig.RequestByteSize;
                txbRequestHexCode.Text = TestItemConfig.RequestHexCode;
                //响应
                nudResponseByteSize.Value = TestItemConfig.ResponseByteSize;
                txbResponseHexCode.Text = TestItemConfig.ResponseHexCode;
                txbPassHexCode.Text = TestItemConfig.PassHexCode;
                txbFailHexCode.Text = TestItemConfig.FailHexCode;
                txbWaitHexCode.Text = TestItemConfig.WaitHexCode;
            }
        }

        #endregion 初始化

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestItemConfig == null)
                TestItemConfig = new AgingTestItemConfig();
            //测试项目基础
            TestItemConfig.TestItem = cmbTestItem.Text;
            TestItemConfig.OverTime = (int)nudOvertime.Value;
            //请求
            TestItemConfig.RequestByteSize = (int)nudRequestByteSize.Value;
            TestItemConfig.RequestHexCode = txbRequestHexCode.Text;
            //响应
            TestItemConfig.ResponseByteSize = (int)nudResponseByteSize.Value;
            TestItemConfig.ResponseHexCode = txbResponseHexCode.Text;
            TestItemConfig.PassHexCode = txbPassHexCode.Text;
            TestItemConfig.FailHexCode = txbFailHexCode.Text;
            TestItemConfig.WaitHexCode = txbWaitHexCode.Text;
            this.Close();
        }
    }
}