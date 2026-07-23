using ComTools.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ComTools.Automation
{
    public partial class FrmSelectWindow : Form
    {
        //当前选中的窗口名称
        public WindowInfo SelectedWindow;

        public FrmSelectWindow()
        {
            InitializeComponent();
            dgvWindow.AutoGenerateColumns = false;
        }

        private void dgvWindow_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectedWindow = dgvWindow.Rows[e.RowIndex].DataBoundItem as WindowInfo;
            this.Close();
        }

        private void FrmSelectWindow_Load(object sender, EventArgs e)
        {
            List<WindowInfo> windows = WindowEnumerator.GetAllWindows();
            dgvWindow.DataSource = windows;
        }
    }
}